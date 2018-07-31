using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using PsionTeklogix.Keyboard;
using PsionTeklogix.Keyboard.NoChording;

namespace hStore.Forms.OutResearch.zc
{
    public partial class frmOutPlanCL : Form
    {
        private DataTable dtCl;
        private int curRow;

        public frmOutPlanCL()
        {
            InitializeComponent();
        }

        public void scanner_ScanCompleteEvent(string text)
        {
            //string barcode = text;

            //bool bval = false;

            //if (barcode.Length < 5) return;

            //char[] chr = barcode.ToCharArray();

            //for (int i = 0; i < chr.Length; i++)
            //{

            //    if ((chr[i] < '0' || chr[i] > '9') && (chr[i] < 'A' || chr[i] > 'Z'))
            //    {
            //        bval = true;
            //        break;
            //    }
            //}

            //if (bval) return;

            //if (text.StartsWith("L")) text = text.Substring(1, text.Length - 1);

            //addCL(barcode);

        }

        //private void addCL(string text)
        //{
        //    for (int i = 0; i < dtCl.Rows.Count; i++)
        //    {
        //        string make = dtCl.Rows[i]["Make"].ToString();
        //        string jhh = dtCl.Rows[i]["JHH"].ToString();
        //        string zfh = dtCl.Rows[i]["ZFH"].ToString();
        //        string clh = dtCl.Rows[i]["CLH"].ToString();

        //        if (Storage.isSameCLH(dtCl.Rows[i]["CLH2"].ToString(), text))
        //        {
        //            if (dtCl.Rows[i]["WCFlag"].ToString() == "0")
        //            {
        //                string now = DateTime.Now.ToString("yyyyMMddHHmmss");
        //                dtCl.Rows[i]["WCFlag"] = 1;
        //                dtCl.Rows[i]["SCANTIME"] = now;
        //                Storage.SetExpScanStatus(jhh,make, zfh, clh, now);
        //                PsionTeklogix.Sound.Beeper.Beeper.PlayTone(5000, 500, 100);
        //            }
        //            else if (dtCl.Rows[i]["WCFlag"].ToString() == "1")
        //            {
        //                frmMessage frm = new frmMessage();
        //                frm.ShowDialog("是否取消消帐材料" + text, "提示");
        //                if (frm.ret)
        //                {
        //                    dtCl.Rows[i]["WCFlag"] = 0;
        //                    dtCl.Rows[i]["SCANTIME"] = "";
        //                    Storage.ClearExpScanStatus(jhh,make, zfh, clh);
        //                }
        //                frm.Dispose();
        //            }
        //            break;
        //        }
        //    }
        //}

        private void frmOutPlanCL_Load(object sender, EventArgs e)
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
                    lblTitle.Text = "铁路出厂－材料";
                    lblInfo.Text = "提单号：" + Global.storage.jhh + "\r\n";
                    lblInfo.Text += "到站："+Global.storage.jhdd + "\r\n";
                    break;
                case "zc":
                    lblTitle.Text = "装船出厂－材料";
                    lblInfo.Text = "提单号：" + Global.storage.jhh + "\r\n";
                    lblInfo.Text += "关单号：" + Global.storage.gdh + "\r\n";
                    break;
                case "zk":
                    lblTitle.Text = "转库出库－材料";
                    lblInfo.Text = "计划号：" + Global.storage.jhh + "\r\n";
                    lblInfo.Text += "入库别：" + Global.storage.jhdd + "\r\n";
                    break;
                case "zt":
                    lblTitle.Text = "自提出厂－材料";
                    lblInfo.Text = "提单号：" + Global.storage.jhh + "\r\n";
                    lblInfo.Text += "收货单位：" + Global.storage.shdw + "\r\n";
                    break;
            }

            string value = "";
            switch (Global.storage.cltype)
            {
                case 0:
                    lblInfo.Text += "准发号：" + Global.storage.zfh ;
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
                    lblInfo.Text += "轧批号：" + Global.storage.zph;
                    value = Global.storage.zph;
                    break;
                default:
                    break;
            }

            dtCl = Storage.GetExpCL(Global.storage.jhh,Global.storage.make,Global.storage.cltype, value);
            curRow = -1;

            dgCl.DataSource = dtCl;
            dgCl.TableStyles.Clear();

            DataGridTableStyle clStyle = new DataGridTableStyle();
            clStyle.MappingName = dtCl.TableName;
            CheckCellEventHandler cellEvent = new CheckCellEventHandler(CheckCellColor);

            clStyle.GridColumnStyles.Add(new ColumnStyle(0, "CLH", "材料号", 110, "", cellEvent));
            //clStyle.GridColumnStyles.Add(new ColumnStyle(1, "KW", "库位", 70, "", cellEvent));
            clStyle.GridColumnStyles.Add(new ColumnStyle(1, "MZ", "毛重", 60, "", cellEvent));
            clStyle.GridColumnStyles.Add(new ColumnStyle(2, "JZ", "净重", 60, "", cellEvent));
            clStyle.GridColumnStyles.Add(new ColumnStyle(3, "GG", "规格", 170, "", cellEvent));


            dgCl.TableStyles.Add(clStyle);
        }

        public void CheckCellColor(object sender, DataGridEnableEventArgs e)
        {
            if (dgCl.CurrentCell.RowNumber == e.Row && curRow != -1)
                e.MeetsCriteria = false;
            else
            {
                //if ((dtCl.Rows[e.Row]["WCFlag"].ToString() == "0" || dtCl.Rows[e.Row]["WCFlag"].ToString() == "") && dtCl.Rows[e.Row]["KW"].ToString() != "")
                //{
                //    e.MeetsCriteria = true;
                //    e.BackColor = Color.Cyan;
                //    e.ForeColor = SystemColors.WindowText;
                //}
                //else if (dtCl.Rows[e.Row]["WCFlag"].ToString() == "1")
                //{
                //    e.MeetsCriteria = true;
                //    if (dtCl.Rows[e.Row]["QA"].ToString() != "" && dtCl.Rows[e.Row]["QA"].ToString() != "0")
                //    {
                //        e.BackColor = Color.BlueViolet;
                //    }
                //    else
                //    {
                //        e.BackColor = Color.Salmon;
                //    }
                //    e.ForeColor = SystemColors.WindowText;
                //}
                //else
                //{
                    e.MeetsCriteria = true;
                    e.BackColor = SystemColors.Window;
                    e.ForeColor = SystemColors.WindowText;
                //}
            }
        }

        private void frmOutPlanCL_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Global.frmCurrent = this.Owner;
                this.Owner.Show();
                this.Owner = null;
                Business.InvokeMethod(Global.frmCurrent, "LoadData", new object[] { });
                this.Close();
            }
        }

        //public void OutStore()
        //{
        //    string sql;

        //    for (int i = 0; i < dtCl.Rows.Count; i++)
        //    {
        //        if (dtCl.Rows[i]["WCFlag"].ToString() == "1")
        //        {
        //            sql = "update ExportStorageAcceptOrder set WCFlag=2 where CLH='" + dtCl.Rows[i]["CLH"].ToString() + "' and ZFH='" + dtCl.Rows[i]["ZFH"].ToString() + "' and JHH='" + dtCl.Rows[i]["JHH"].ToString() + "'";
        //            SqlCe.ExecuteNonQuery(sql);
        //        }
        //    }
        //    LoadData();

        //    Storage.UpdateExpWcjs();

        //}

        private void dgCl_CurrentCellChanged(object sender, EventArgs e)
        {
            curRow = dgCl.CurrentCell.RowNumber;
            dgCl.Select(dgCl.CurrentCell.RowNumber);
        }

        private void frmOutPlanCL_Closed(object sender, EventArgs e)
        {
        }

        private void dgCl_KeyUp(object sender, KeyEventArgs e)
        {
            if (curRow >= 0)
            {
                string make = dtCl.Rows[curRow]["Make"].ToString();
                string jhh = dtCl.Rows[curRow]["JHH"].ToString();
                string zfh = dtCl.Rows[curRow]["ZFH"].ToString();
                string clh = dtCl.Rows[curRow]["CLH"].ToString();
                string qa = dtCl.Rows[curRow]["QA"].ToString();

                //if (e.KeyCode == Keys.Enter)
                //{
                //    if (dtCl.Rows[curRow]["WCFlag"].ToString() == "0")
                //    {
                //        if (dtCl.Rows[curRow]["KW"].ToString() != "")
                //        {
                //            string now = DateTime.Now.ToString("yyyyMMddHHmmss");
                //            dtCl.Rows[curRow]["WCFlag"] = 1;
                //            dtCl.Rows[curRow]["SCANTIME"] = now;
                //            Storage.SetExpScanStatus(jhh,make, zfh, clh, now);
                //            PsionTeklogix.Sound.Beeper.Beeper.PlayTone(5000, 500, 100);
                //        }
                //    }
                //    else if (dtCl.Rows[curRow]["WCFlag"].ToString() == "1")
                //    {
                //        frmMessage frmMessage = new frmMessage();
                //        frmMessage.ShowDialog("是否取消消帐材料" + clh, "提示");
                //        if (frmMessage.ret)
                //        {
                //            dtCl.Rows[curRow]["WCFlag"] = 0;
                //            dtCl.Rows[curRow]["SCANTIME"] = "";
                //            Storage.ClearExpScanStatus(jhh,make, zfh, clh);
                //        }
                //    }
                //}
                //if (e.KeyCode == Keys.F22 || e.KeyCode == Keys.F23)
                //{
                //    frmQuality frmQuality = new frmQuality();
                //    Global.frmCurrent = frmQuality;
                //    frmQuality.Owner = this;
                //    this.Hide();
                //    frmQuality.Show(clh,qa);
                //}
            }
        }
    }
}