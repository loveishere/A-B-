using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using PsionTeklogix.Keyboard;
using PsionTeklogix.Keyboard.NoChording;

namespace hStore.Forms.InResearch
{
    public partial class frmFrameIn : Form
    {
        private DataTable dtCl;
        private int curRow;
        private bool bFrameApply;
        //private delegate void getDataSource();
        //private delegate void setText(string str);

        public frmFrameIn()
        {
            InitializeComponent();
        }

        private void frmFrameIn_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.applyTimer.Enabled = false;
                Global.frmCurrent = this.Owner;
                this.Owner.Show();
                this.Owner = null;
                Business.InvokeMethod(Global.frmCurrent, "LoadData", new object[] { });
                this.Close();
            }
            /*
            if (e.KeyCode == Keys.F5)
            {
                if (txtKjh.Text.Length == 4)
                {
                    Global.storage.kjh = txtKjh.Text;
                }
                applyTimer.Enabled = false;
                bFrameApply = true;
                frmIn frmIn = new frmIn();
                Global.frmCurrent = frmIn;
                frmIn.Owner = this;
                frmIn.Show();
                this.Hide();
            }
            */
            if (e.KeyCode == Keys.F21)
            {
                if (txtKjh.Text.Length == 4)
                {
                    Global.storage.kjh = txtKjh.Text;
                    int js = Storage.GetImpScanJs(Global.storage.jhh, Global.storage.make);
                    if (js > 0)
                    {
                        frmSend frmSend = new frmSend();
                        Global.frmCurrent = frmSend;
                        frmSend.Owner = this;
                        frmSend.Show();
                        this.Hide();
                    }
                }
            }
        }

        public void SetJs(string js)
        {
            Global.UIThread(txtJs, delegate { txtJs.Text = js; });
            //if (txtJs.InvokeRequired)
            //{
            //    txtJs.Invoke(new setText(SetJs), new object[] { js });
            //}
            //else
            //{
            //    txtJs.Text = js;
            //}
        }

        public void SetMz(string mz)
        {
            Global.UIThread(txtMz, delegate { txtMz.Text = mz; });
            //if (txtMz.InvokeRequired)
            //{
            //    txtMz.Invoke(new setText(SetMz), new object[] { mz });
            //}
            //else
            //{
            //    txtMz.Text = mz;
            //}
        }

        public void RefreshFrameInfo()
        {
            bFrameApply = true;

            int js = 0;
            int mz = 0;

            Global.UIThread(this, delegate {

                dtCl = Storage.GetImpFrameCL(Global.sKb, Global.storage.kjh);

                js = dtCl.Rows.Count;
                for (int i = 0; i < dtCl.Rows.Count; i++)
                {
                    if (dtCl.Rows[i]["MZ"] != DBNull.Value)
                    {
                        mz += Convert.ToInt32(dtCl.Rows[i]["MZ"]);
                    }
                }
                curRow = -1;
                dgCl.DataSource = dtCl;

                btnApply.Enabled = true;
                btnApply.Text = "申请";

            });

            /*
            if (dgCl.InvokeRequired)
            {
                dgCl.Invoke(new getDataSource(RefreshFrameInfo), new object[] { });
            }
            else
            {

                dtCl = Storage.GetImpFrameCL(Global.sKb, Global.storage.kjh);

                js = dtCl.Rows.Count;
                for (int i = 0; i < dtCl.Rows.Count; i++)
                {
                    if (dtCl.Rows[i]["MZ"] != DBNull.Value)
                    {
                        mz += Convert.ToInt32(dtCl.Rows[i]["MZ"]);
                    }
                }
                curRow = -1;
                dgCl.DataSource = dtCl;


            }

            if (btnApply.InvokeRequired)
            {
                btnApply.Invoke(new getDataSource(RefreshFrameInfo), new object[] { });
            }
            else
            {
                btnApply.Enabled = true;
                btnApply.Text = "申请";
            }
            */ 


            SetJs(js.ToString());

            SetMz(mz.ToString());



        }

        private void frmFrameIn_Load(object sender, EventArgs e)
        {
            if (ScanCodeRemapping.NormalTable.Contains(0x003A))
            {
                ScanCodeRemapping.NormalTable.Remove(0x003A);
            }
            Global.storage.kjh = "";

            string sql = "select CLH,WZ,substring(KW,4,len(KW)-3) KW,Height+'×'+Width+'×'+Long GG,MZ,WCFlag,SCANTIME,ZFH,TDH,Make,CH,QA,'0' ExFlag from FrameDetail where 1=2";

            dtCl = SqlCe.ExecuteQuery(sql);
            dgCl.DataSource = dtCl;
            dgCl.TableStyles.Clear();

            DataGridTableStyle clStyle = new DataGridTableStyle();
            clStyle.MappingName = dtCl.TableName;
            CheckCellEventHandler cellEvent = new CheckCellEventHandler(CheckCellColor);

            clStyle.GridColumnStyles.Add(new ColumnStyle(0, "CLH", "材料号", 110, "", cellEvent));
            clStyle.GridColumnStyles.Add(new ColumnStyle(1, "KW", "库位", 50, "", cellEvent));
            clStyle.GridColumnStyles.Add(new ColumnStyle(2, "ZFH", "准发号", 100, "", cellEvent));
            clStyle.GridColumnStyles.Add(new ColumnStyle(3, "WZ", "库别", 40, "", cellEvent));
            clStyle.GridColumnStyles.Add(new ColumnStyle(4, "GG", "规格", 40, "", cellEvent));
            clStyle.GridColumnStyles.Add(new ColumnStyle(5, "MZ", "毛重", 50, "", cellEvent));
            clStyle.GridColumnStyles.Add(new ColumnStyle(6, "CH", "层号", 50, "", cellEvent));

            curRow = -1;
            dgCl.TableStyles.Add(clStyle);
        }

        public void InStore()
        {
            string sql;

            for (int i = 0; i < dtCl.Rows.Count; i++)
            {
                if (dtCl.Rows[i]["WCFlag"].ToString() == "1")
                {
                    sql = "update ImportStorageAcceptOrder set WCFlag=2 ,SCANTIME='" + dtCl.Rows[i]["SCANTIME"].ToString() + "' where CLH='" + dtCl.Rows[i]["CLH"].ToString() + "' and ZFH='" + dtCl.Rows[i]["ZFH"].ToString() + "' and JHH='"+ dtCl.Rows[i]["TDH"].ToString() +"' and Make='"+ dtCl.Rows[i]["Make"].ToString() +"' and CH='"+ Global.storage.kjh +"'";
                    SqlCe.ExecuteNonQuery(sql);
                    sql = "update ExportStorageAcceptOrder set KW='" + Global.sKb + dtCl.Rows[i]["KW"].ToString() + "' where CLH='" + dtCl.Rows[i]["CLH"].ToString() + "' and ZFH='" + dtCl.Rows[i]["ZFH"].ToString() + "' and Make='"+ dtCl.Rows[i]["Make"].ToString() +"'";
                    SqlCe.ExecuteNonQuery(sql);
                }
            }
            LoadData();

            Storage.UpdateImpWcjs();

        }

        public void CheckCellColor(object sender, DataGridEnableEventArgs e)
        {
            //WCFlag 1:待发送 2:已消帐 0,Null:待消帐 -1:非法材料
            if (dgCl.CurrentCell.RowNumber == e.Row && curRow != -1)
                e.MeetsCriteria = false;
            else
            {
                if (dtCl.Rows[e.Row]["WCFlag"].ToString() == "1")
                {
                    e.MeetsCriteria = true;
                    if (dtCl.Rows[e.Row]["QA"].ToString() != "" && dtCl.Rows[e.Row]["QA"].ToString() != "0")
                    {
                        e.BackColor = Color.BlueViolet;
                    }
                    else
                    {
                        e.BackColor = Color.Salmon;
                    }
                    if (dtCl.Rows[e.Row]["ExFlag"].ToString() == "1")
                    {
                        e.ForeColor = Color.Brown;
                    }
                    else
                    {
                        e.ForeColor = SystemColors.WindowText;
                    }
                }
                else if (dtCl.Rows[e.Row]["WCFlag"].ToString() == "2" || dtCl.Rows[e.Row]["WCFlag"].ToString() == "-1")
                {
                    e.MeetsCriteria = true;
                    e.BackColor = Color.Silver;
                    if (dtCl.Rows[e.Row]["ExFlag"].ToString() == "1")
                    {
                        e.ForeColor = Color.Brown;
                    }
                    else
                    {
                        e.ForeColor = SystemColors.WindowText;
                    }
                }
                else
                {
                    e.MeetsCriteria = true;
                    e.BackColor = Color.Cyan;
                    if (dtCl.Rows[e.Row]["ExFlag"].ToString() == "1")
                    {
                        e.ForeColor = Color.Brown;
                        
                    }
                    else
                    {
                        e.ForeColor = SystemColors.WindowText;
                    }
                }
            }
        }

        private void zf2kw(int index)
        {
            string make = dtCl.Rows[index]["Make"].ToString();
            string jhh = dtCl.Rows[index]["TDH"].ToString();
            string zfh = dtCl.Rows[index]["ZFH"].ToString();
            string clh = dtCl.Rows[index]["CLH"].ToString();
            string kw = dtCl.Rows[index]["KW"].ToString();
            int mz = 0;
            if (dtCl.Rows[index]["MZ"] != DBNull.Value) mz = Convert.ToInt32(dtCl.Rows[index]["MZ"]);
            string now = DateTime.Now.ToString("yyyyMMddHHmmss");

            string kw2 = Storage.GetCurrentKw(zfh,jhh,make);
            if(kw2!="")
            {
                dtCl.Rows[index]["KW"] = kw2;
                dtCl.Rows[index]["SCANTIME"] = now;
                dtCl.Rows[index]["WCFlag"] = 1;
                Storage.SetImpScanStatus(jhh,make,zfh,clh,Global.sKb+kw2,Global.storage.kjh,now);
                PsionTeklogix.Sound.Beeper.Beeper.PlayTone(5000, 500, 100);
            }
            else
            {
                string remain = Storage.GetRemainCls(jhh,make,zfh);

                frmInKW frm = new frmInKW();

                frm.ShowDialog(clh,kw,remain);
                if (frm.ret)
                {
                    kw2 = frm.m_kwnow;
                    dtCl.Rows[index]["KW"] = kw2;
                    Storage.SaveCurrentKw(zfh,jhh,make, kw2);
                    dtCl.Rows[index]["SCANTIME"] = now;
                    dtCl.Rows[index]["WCFlag"] = 1;
                    Storage.SetImpScanStatus(jhh,make, zfh, clh, Global.sKb + kw2,Global.storage.kjh, now);
                    PsionTeklogix.Sound.Beeper.Beeper.PlayTone(5000, 500, 100);
                }
                frm.Dispose();
            }
        }

        private void dgCl_KeyUp(object sender, KeyEventArgs e)
        {
            if (curRow >= 0)
            {
                string make = dtCl.Rows[curRow]["Make"].ToString();
                string jhh = dtCl.Rows[curRow]["TDH"].ToString();
                string zfh = dtCl.Rows[curRow]["ZFH"].ToString();
                string clh = dtCl.Rows[curRow]["CLH"].ToString();
                string qa = dtCl.Rows[curRow]["QA"].ToString();
                string kw = dtCl.Rows[curRow]["KW"].ToString();

                if (e.KeyCode == Keys.Enter)
                {
                    if (dtCl.Rows[curRow]["WZ"].ToString() == Global.sKb)
                    {
                        if (dtCl.Rows[curRow]["WCFlag"].ToString() == "1")
                        {
                            frmMessage frm = new frmMessage();
                            frm.ShowDialog("是否取消消帐材料" + clh, "提示");
                            if (frm.ret)
                            {
                                Storage.ClearImpScanStatus(jhh,make, zfh, clh);
                                dtCl.Rows[curRow]["WCFlag"] = 0;
                                dtCl.Rows[curRow]["SCANTIME"] = "";
                                dtCl.Rows[curRow]["KW"] = "";
                            }
                            frm.Dispose();
                        }
                        else if (dtCl.Rows[curRow]["WCFlag"].ToString() == "0" || dtCl.Rows[curRow]["WCFlag"].ToString() == "")
                        {
                            zf2kw(curRow);
                        }
                    }
                }

                if (e.KeyCode == Keys.F2)
                {
                    if (dtCl.Rows[curRow]["WCFlag"].ToString() == "1")
                    {
                        string remain = Storage.GetRemainCls(jhh,make,zfh);

                        frmInKW frm = new frmInKW();
                        frm.ShowDialog(clh,kw,remain);
                        if (frm.ret)
                        {
                            string kw2 = frm.m_kwnow;
                            dtCl.Rows[curRow]["KW"] = kw2;
                            Storage.SaveCurrentKw(zfh,jhh,make, kw2);
                            Storage.UpdateImpKw(jhh,make, zfh, clh, Global.sKb+kw2);
                        }
                        frm.Dispose();
                    }
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

        private void dgCl_CurrentCellChanged(object sender, EventArgs e)
        {
            curRow = dgCl.CurrentCell.RowNumber;
            dgCl.Select(dgCl.CurrentCell.RowNumber);
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

        private void btnApply_Click(object sender, EventArgs e)
        {
            if (txtKjh.Text.Length==4)
            {
                Global.storage.kjh = txtKjh.Text;
                LoadData();
                Business.SendText(Business.msg.Package("ZDWX03", Global.storage.kjh));
                btnApply.Enabled = false;
                btnApply.Text = "申请中";
                txtJs.Text = "";
                txtMz.Text = "";
                applyTimer.Enabled = true;
                bFrameApply = false;
            }
        }


        private void LoadData()
        {
            int js = 0;
            int mz = 0;

            dtCl = Storage.GetImpFrameCL(Global.sKb, Global.storage.kjh);
            js = dtCl.Rows.Count;
            for (int i = 0; i < dtCl.Rows.Count; i++)
            {
                if (dtCl.Rows[i]["MZ"] != DBNull.Value)
                {
                    mz += Convert.ToInt32(dtCl.Rows[i]["MZ"]);
                }
            }

            dgCl.DataSource = dtCl;
            curRow = -1;

            SetJs(js.ToString());

            SetMz(mz.ToString());
        }

        private void addCL(string text)
        {
            for (int i = 0; i < dtCl.Rows.Count; i++)
            {
                string clh = dtCl.Rows[i]["CLH"].ToString();
                if (Storage.isSameCLH(clh, text))
                {
                    string make = dtCl.Rows[i]["Make"].ToString();
                    string jhh = dtCl.Rows[i]["TDH"].ToString();
                    string zhf = dtCl.Rows[i]["ZFH"].ToString();
                    

                    if (dtCl.Rows[i]["WCFlag"].ToString() == "1")
                    {
                        frmMessage frm = new frmMessage();
                        frm.ShowDialog("是否取消消帐材料" + text, "提示");
                        if (frm.ret)
                        {
                            Storage.ClearImpScanStatus(jhh, make,zhf, clh);
                            dtCl.Rows[i]["WCFlag"] = 0;
                            dtCl.Rows[i]["SCANTIME"] = "";
                            dtCl.Rows[i]["KW"] = "";
                        }
                        frm.Dispose();
                    }
                    else if (dtCl.Rows[i]["WCFlag"].ToString() == "0" || dtCl.Rows[i]["WCFlag"].ToString() == "")
                    {
                        zf2kw(i);
                    }
                    txtClh.Text = "";
                    break;
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
                if (txtClh.Text.Length > 0)
                {
                    addCL(txtClh.Text);
                }
            }

            if (txtClh.Text.Length == 5)
            {

                string sql = "select A.CLH from FrameDetail A left join ImportStorageAcceptOrder B on A.CLH=B.CLH and A.Make=B.Make and (B.WCFlag is null or B.WCFlag<>2) and B.RKB='" + Global.sKb + "' ";
                sql += "where A.KJH='" + Global.storage.kjh + "' and  A.CLH like '%" + txtClh.Text + "' ";


                DataTable dt = SqlCe.ExecuteQuery(sql);
                if (dt.Rows.Count == 1)
                {
                    txtClh.Text = dt.Rows[0]["CLH"].ToString();
                    txtClh.SelectionStart = txtClh.Text.Length;
                }
                else if (dt.Rows.Count > 1)
                {
                    lstCLH.Items.Clear();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        lstCLH.Items.Add(dt.Rows[i]["CLH"].ToString());
                    }
                    lstCLH.Visible = true;
                }
                dt.Dispose();
            }
        }

        private void lstCLH_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtClh.Text = lstCLH.Items[lstCLH.SelectedIndex].ToString();
            txtClh.SelectionStart = txtClh.Text.Length;
            lstCLH.Visible = false;
        }

        private void applyTimer_Tick(object sender, EventArgs e)
        {
            applyTimer.Enabled = false;

            if (bFrameApply == false)
            {
                frmIn frmIn = new frmIn();
                Global.frmCurrent = frmIn;
                frmIn.Owner = this;
                frmIn.Show();
                this.Hide();
            }
            btnApply.Text = "申请";
            btnApply.Enabled = true;
        }

        private void txtKjh_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtKjh.Text.Length == 4)
                {
                    Global.storage.kjh = txtKjh.Text;
                    LoadData();
                }
            }
        }

    }
}