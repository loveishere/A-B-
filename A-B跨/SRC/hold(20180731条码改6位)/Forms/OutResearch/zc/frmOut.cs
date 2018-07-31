using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using PsionTeklogix.Keyboard;
using PsionTeklogix.Keyboard.NoChording;

namespace hStore.Forms.OutResearch
{
    public partial class frmOut : Form
    {
        private DataTable dtOut;
        private int curRow;

        public frmOut()
        {
            InitializeComponent();
        }

        private void frmOut_Load(object sender, EventArgs e)
        {
            if (ScanCodeRemapping.NormalTable.Contains(0x003A))
            {
                ScanCodeRemapping.NormalTable.Remove(0x003A);
            }

            LoadData();

        }

        public void LoadData()
        {
            switch (Global.storage.expflag)
            {
                case "tl":
                    lblInfo.Text = "提单号：" + Global.storage.jhh + "\r\n";
                    lblInfo.Text += "到站：" + Global.storage.jhdd + "\r\n";
                    break;
                case "zc":
                    lblInfo.Text = "提单号：" + Global.storage.jhh + "\r\n";
                    lblInfo.Text += "关单号：" + Global.storage.gdh + "\r\n";
                    break;
                case "zk":
                    lblInfo.Text = "计划号：" + Global.storage.jhh + "\r\n";
                    lblInfo.Text += "入库别：" + Global.storage.jhdd + "\r\n";
                    break;
                case "zt":
                    lblInfo.Text = "提单号：" + Global.storage.jhh + "\r\n";
                    lblInfo.Text += "收货单位：" + Global.storage.shdw + "\r\n";
                    break;
            }

            string value = "";
            switch (Global.storage.cltype)
            {
                case 0:
                    lblInfo.Text += "准发号：" + Global.storage.zfh;
                    value = Global.storage.zfh;
                    break;
                case 1:
                    lblInfo.Text += "炉号：" + Global.storage.lh;
                    value = Global.storage.lh;
                    break;
                case 2:
                    lblInfo.Text += "合同号：" + Global.storage.hth;
                    value = Global.storage.hth;
                    break;
                case 3:
                    lblInfo.Text += "扎批号：" + Global.storage.zph;
                    value = Global.storage.zph;
                    break;
                default:
                    break;
            }

            dtOut = Storage.GetExpCLkb(Global.sKb, Global.storage.cph, Global.storage.jhh,Global.storage.make, Global.storage.cltype, value);
            curRow = -1;
            dgOut.DataSource = dtOut;
            dgOut.TableStyles.Clear();

            DataGridTableStyle outStyle = new DataGridTableStyle();
            outStyle.MappingName = dtOut.TableName;
            CheckCellEventHandler cellEvent = new CheckCellEventHandler(CheckCellColor);

            outStyle.GridColumnStyles.Add(new ColumnStyle(0, "CLH", "材料号", 100, "", cellEvent));
            outStyle.GridColumnStyles.Add(new ColumnStyle(1, "KW", "库位", 60, "", cellEvent));
            outStyle.GridColumnStyles.Add(new ColumnStyle(2, "MZ", "毛重", 60, "", cellEvent));
            outStyle.GridColumnStyles.Add(new ColumnStyle(3, "JZ", "净重", 60, "", cellEvent));
            outStyle.GridColumnStyles.Add(new ColumnStyle(4, "GG", "规格", 150, "", cellEvent));


            dgOut.TableStyles.Add(outStyle);
            RefreshJS();
        }


        public void CheckCellColor(object sender, DataGridEnableEventArgs e)
        {
            if (dgOut.CurrentCell.RowNumber == e.Row && curRow != -1)
                e.MeetsCriteria = false;
            else
            {
                if (dtOut.Rows[e.Row]["WCFlag"].ToString() == "0" || dtOut.Rows[e.Row]["WCFlag"].ToString() == "")
                {
                    e.MeetsCriteria = true;
                    e.BackColor = Color.Cyan;
                    e.ForeColor = SystemColors.WindowText;
                }
                else if (dtOut.Rows[e.Row]["WCFlag"].ToString() == "1")
                {
                    e.MeetsCriteria = true;
                    if (dtOut.Rows[e.Row]["QA"].ToString() != "" && dtOut.Rows[e.Row]["QA"].ToString() != "0")
                    {
                        e.BackColor = Color.BlueViolet;
                    }
                    else
                    {
                        e.BackColor = Color.Salmon;
                    }
                    e.ForeColor = SystemColors.WindowText;
                }
                else
                {
                    e.MeetsCriteria = true;
                    e.BackColor = Color.Silver;
                    e.ForeColor = SystemColors.WindowText;
                }
            }
        }

        private void scanner_ScanCompleteEvent(string text)
        {
            bool bval = false;

            if (text.Length < 5) return;

            char[] chr = text.ToCharArray();

            for (int i = 0; i < chr.Length; i++)
            {

                if ((chr[i] < '0' || chr[i] > '9') && (chr[i] < 'A' || chr[i] > 'Z'))
                {
                    bval = true;
                    break;
                }
            }

            if (bval) return;

            if (text.StartsWith("L")) text = text.Substring(1, text.Length - 1);

            addCL(text);
        }

        private void addCL(string text)
        {
            for (int i = 0; i < dtOut.Rows.Count; i++)
            {
                string make = dtOut.Rows[i]["Make"].ToString();
                string jhh = dtOut.Rows[i]["JHH"].ToString();
                string zfh = dtOut.Rows[i]["ZFH"].ToString();
                string clh = dtOut.Rows[i]["CLH"].ToString();

                if (Storage.isSameCLH(dtOut.Rows[i]["CLH2"].ToString(), text))
                {
                    if (dtOut.Rows[i]["WCFlag"].ToString() == "" || dtOut.Rows[i]["WCFlag"].ToString() == "0")
                    {
                        string now = DateTime.Now.ToString("yyyyMMddHHmmss");
                        dtOut.Rows[i]["SCANTIME"] = now;
                        dtOut.Rows[i]["WCFlag"] = 1;
                        Storage.SetExpScanStatus(jhh,make, zfh, clh, now);
                        //SqlCe.ExecuteNonQuery("update ExportStorageAcceptOrder set WCFlag=1,SCANTIME='" + dtOut.Rows[i]["SCANTIME"].ToString() + "' where CLH='" + dtOut.Rows[i]["CLH"].ToString() + "' and ZFH='" + dtOut.Rows[i]["ZFH"].ToString() + "' and JHH='" + dtOut.Rows[i]["JHH"].ToString() + "'");
                        PsionTeklogix.Sound.Beeper.Beeper.PlayTone(5000, 500, 100);
                    }
                    else if (dtOut.Rows[i]["WCFlag"].ToString() == "1")
                    {
                        frmMessage frm = new frmMessage();
                        frm.ShowDialog("是否取消消帐材料" + text, "提示");
                        if (frm.ret)
                        {
                            dtOut.Rows[i]["WCFlag"] = 0;
                            dtOut.Rows[i]["SCANTIME"] = "";
                            Storage.ClearExpScanStatus(jhh,make, zfh, clh);
                            //SqlCe.ExecuteNonQuery("update ExportStorageAcceptOrder set WCFlag=0,SCANTIME='' where CLH='" + dtOut.Rows[i]["CLH"].ToString() + "' and ZFH='" + dtOut.Rows[i]["ZFH"].ToString() + "' and JHH='" + dtOut.Rows[i]["JHH"].ToString() + "'");
                        }
                        frm.Dispose();
                    }
                    
                    break;
                }
            }
            RefreshJS();
        }

        //private bool isSameCLH(string clh, string text)
        //{
        //    if (text.Length == 9 || text.Length == 10)
        //    {
        //        return (clh.EndsWith(text));
        //    }
        //    else
        //    {
        //        return (clh == text);
        //    }
        //}

        private void frmOut_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Global.frmCurrent = this.Owner;
                this.Owner.Show();
                this.Owner = null;
                Business.InvokeMethod(Global.frmCurrent, "LoadData", new object[] { });
                this.Close();
            }
            else if (e.KeyCode == Keys.F21)
            {
                if (txtJs.Text != "" && txtJs.Text != "0")
                {
                    hStore.Forms.OutResearch.zc.frmSend frmSend = new hStore.Forms.OutResearch.zc.frmSend();
                    //frmSend.js = txtJs.Text;
                    //frmSend.mz = txtMz.Text;
                    //frmSend.clhs = clhs;
                    Global.frmCurrent = frmSend;
                    frmSend.Owner = this;
                    frmSend.Show();
                    this.Hide();
                }
            }
        }

        private void dgOut_CurrentCellChanged(object sender, EventArgs e)
        {
            curRow = dgOut.CurrentCell.RowNumber;
            dgOut.Select(dgOut.CurrentCell.RowNumber);
        }

        private void frmOut_Closed(object sender, EventArgs e)
        {
            //ScanCodeRemapping.NormalTable.Add(0x003A, new RemappingTable.Remapping(VirtualKey.VK_F24, Function.SendCode));
        }

        private void dgOut_KeyUp(object sender, KeyEventArgs e)
        {
            if (curRow >= 0)
            {
                string make = dtOut.Rows[curRow]["Make"].ToString();
                string jhh = dtOut.Rows[curRow]["JHH"].ToString();
                string zfh = dtOut.Rows[curRow]["ZFH"].ToString();
                string clh = dtOut.Rows[curRow]["CLH"].ToString();
                string qa = dtOut.Rows[curRow]["QA"].ToString();
                if (e.KeyCode == Keys.Enter)
                {
                    if (dtOut.Rows[curRow]["WCFlag"].ToString() == "" || dtOut.Rows[curRow]["WCFlag"].ToString() == "0")
                    {
                        string now = DateTime.Now.ToString("yyyyMMddHHmmss");
                        dtOut.Rows[curRow]["SCANTIME"] = now;
                        dtOut.Rows[curRow]["WCFlag"] = 1;
                        Storage.SetExpScanStatus(jhh,make, zfh, clh, now);
                        //SqlCe.ExecuteNonQuery("update ExportStorageAcceptOrder set WCFlag=1,SCANTIME='" + dtOut.Rows[curRow]["SCANTIME"].ToString() + "' where CLH='" + dtOut.Rows[curRow]["CLH"].ToString() + "' and ZFH='" + dtOut.Rows[curRow]["ZFH"].ToString() + "' and JHH='" + dtOut.Rows[curRow]["JHH"].ToString() + "'");
                        PsionTeklogix.Sound.Beeper.Beeper.PlayTone(5000, 500, 100);
                    }
                    else if (dtOut.Rows[curRow]["WCFlag"].ToString() == "1")
                    {
                        frmMessage frm = new frmMessage();
                        frm.ShowDialog("是否取消消帐材料" + clh, "提示");
                        if (frm.ret)
                        {
                            Storage.ClearExpScanStatus(jhh,make, zfh, clh);
                            //SqlCe.ExecuteNonQuery("update ExportStorageAcceptOrder set WCFlag=0,SCANTIME='' where CLH='" + dtOut.Rows[curRow]["CLH"].ToString() + "' and ZFH='" + dtOut.Rows[curRow]["ZFH"].ToString() + "' and JHH='" + dtOut.Rows[curRow]["JHH"].ToString() + "'");
                            dtOut.Rows[curRow]["WCFlag"] = 0;
                            dtOut.Rows[curRow]["SCANTIME"] = "";
                        }
                        frm.Dispose();
                    }
                    RefreshJS();
                }
                if (e.KeyCode == Keys.F22 || e.KeyCode == Keys.F23)
                {
                    frmQuality frmQuality = new frmQuality();
                    Global.frmCurrent = frmQuality;
                    frmQuality.Owner = this;
                    this.Hide();
                    frmQuality.Show(clh,qa);
                }
            }
        }

        private void txtClh_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) return;

            if (e.KeyCode >= Keys.A && e.KeyCode <= Keys.Z)
            {
                txtClh.Text = txtClh.Text.ToUpper();
                txtClh.SelectionStart = txtClh.Text.Length;
            }

            if (e.KeyCode == Keys.Enter)
            {
                if (txtClh.Text.Length > 4)
                {
                    addCL(txtClh.Text);
                    txtClh.Text = "";
                }
            }

            if (txtClh.Text.Length == 5)
            {
                string value = "";

                //string sql = "select B.CLH2 from ExportStorageOrder A,ExportStorageAcceptOrder B where A.ZFH=B.ZFH and A.JHH=B.JHH and (B.WCFlag is null or B.WCFlag<>2) and B.KW<>'' ";
                //sql += " and B.CLH2 like '%" + txtClh.Text + "'and B.CKB='" + Global.sKb + "' and A.JHH like '3%' ";
                //sql += "and A.JHH='" + m_tdh + "' ";
                switch (Global.storage.cltype)
                {
                    case 0:
                        //sql += "and A.ZFH='" + m_zfh + "'";
                        value = Global.storage.zfh;
                        break;
                    case 1:
                        //sql += "and B.LH='" + m_lh + "'";
                        value = Global.storage.lh;
                        break;
                    case 2:
                        //sql += "and A.HTH='" + m_hth + "'";
                        value = Global.storage.hth;
                        break;
                    case 3:
                        //sql += "and substring(B.CLH,1,6)='" + m_zph + "'";
                        value = Global.storage.zph;
                        break;
                    default:
                        break;
                }

                DataTable dt = Storage.GetExpCLkb(Global.sKb,Global.storage.cph, Global.storage.jhh,Global.storage.make, Global.storage.cltype, value, txtClh.Text);
                if (dt.Rows.Count == 1)
                {
                    txtClh.Text = dt.Rows[0]["CLH2"].ToString();
                    txtClh.SelectionStart = txtClh.Text.Length;
                }
                else if (dt.Rows.Count > 1)
                {
                    lstCLH.Items.Clear();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        lstCLH.Items.Add(dt.Rows[i]["CLH2"].ToString());
                    }
                    lstCLH.Visible = true;
                }
                dt.Dispose();
            }
        }

        private void RefreshJS()
        {
            int js = 0;
            int mz = 0;
            //clhs = "";

            for (int i = 0; i < dtOut.Rows.Count; i++)
            {
                if (dtOut.Rows[i]["WCFlag"].ToString() == "1")
                {
                    //clhs += "'"+dtOut.Rows[i]["CLH"].ToString() + "',";
                    js++;
                    if (dtOut.Rows[i]["MZ"].ToString() != "")
                    {
                        mz += Convert.ToInt32(dtOut.Rows[i]["MZ"]);
                    }
                }
            }
            //if (clhs.Length > 0) clhs = clhs.Substring(0, clhs.Length - 1);
            txtJs.Text = js.ToString();
            txtMz.Text = mz.ToString();
        }

        private void lstCLH_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtClh.Text = lstCLH.Items[lstCLH.SelectedIndex].ToString();
            txtClh.SelectionStart = txtClh.Text.Length;
            lstCLH.Visible = false;
        }


    }
}