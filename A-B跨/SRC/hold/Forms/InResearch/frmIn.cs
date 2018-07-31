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
    public partial class frmIn : Form
    {
        private DataTable dtCl;
        private int curRow;

        public frmIn()
        {
            InitializeComponent();
        }

        private void frmIn_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Global.frmCurrent = this.Owner;
                this.Owner.Show();
                this.Owner = null;
                Business.InvokeMethod(Global.frmCurrent, "LoadData", new object[] { });
                this.Close();
            }
            if (e.KeyCode == Keys.F21)
            {
                int js = Storage.GetImpScanJs(Global.storage.jhh,Global.storage.make);
                if (js > 0)
                {
                    frmSend frmSend = new frmSend();
                    Global.frmCurrent = frmSend;
                    frmSend.Owner = this;
                    frmSend.Show();
                    this.Hide();
                }
            }
            if (e.KeyCode == Keys.F5)
            {
                frmFrameIn frmFrameIn = new frmFrameIn();
                Global.frmCurrent = frmFrameIn;
                frmFrameIn.Owner = this;
                frmFrameIn.Show();
                this.Hide();
            }
        }

        private void frmIn_Load(object sender, EventArgs e)
        {
            if (ScanCodeRemapping.NormalTable.Contains(0x003A))
            {
                ScanCodeRemapping.NormalTable.Remove(0x003A);
            }

            LoadData();
            dgCl.TableStyles.Clear();

            DataGridTableStyle clStyle = new DataGridTableStyle();
            clStyle.MappingName = dtCl.TableName;
            CheckCellEventHandler cellEvent = new CheckCellEventHandler(CheckCellColor);

            clStyle.GridColumnStyles.Add(new ColumnStyle(0, "CLH", "材料号", 110, "", cellEvent));
            clStyle.GridColumnStyles.Add(new ColumnStyle(1, "KW", "库位", 60, "", cellEvent));
            clStyle.GridColumnStyles.Add(new ColumnStyle(2, "GG", "规格", 160, "", cellEvent));
            clStyle.GridColumnStyles.Add(new ColumnStyle(3, "MZ", "毛重", 50, "", cellEvent));
            clStyle.GridColumnStyles.Add(new ColumnStyle(4, "ZFH", "准发号", 100, "", cellEvent));
            clStyle.GridColumnStyles.Add(new ColumnStyle(5, "JHH", "计划号", 100, "", cellEvent));

            
            dgCl.TableStyles.Add(clStyle);
        }

        private void LoadData()
        {
            string sql = "select CLH,'" + Global.sKb + "' WZ,substring(KW,4,len(KW)-3) KW,Height+'×'+Width+'×'+Long GG, MZ,ZFH,JHH,Make,WCFlag,SCANTIME,QA,'0' ExFlag from ImportStorageAcceptOrder where WCFlag=1 ";
            if(Global.storage.jhh != "")
            {
                sql += " and JHH='"+ Global.storage.jhh +"' and Make='"+ Global.storage.make +"'";
            }
            dtCl = SqlCe.ExecuteQuery(sql);
            dgCl.DataSource = dtCl;
            curRow = -1;
        }


        public void InStore()
        {
            string sql;

            for (int i = 0; i < dtCl.Rows.Count; i++)
            {
                if (dtCl.Rows[i]["WCFlag"].ToString() == "1")
                {
                    sql = "update ImportStorageAcceptOrder set WCFlag=2 where CLH='" + dtCl.Rows[i]["CLH"].ToString() + "' and ZFH='" + dtCl.Rows[i]["ZFH"].ToString() + "' and JHH='" + dtCl.Rows[i]["JHH"].ToString() + "' and Make='"+ dtCl.Rows[i]["Make"].ToString() +"'";
                    SqlCe.ExecuteNonQuery(sql);
                    sql = "update ExportStorageAcceptOrder set KW='" +  Global.sKb + dtCl.Rows[i]["KW"].ToString() + "' where CLH='" + dtCl.Rows[i]["CLH"].ToString() + "' and ZFH='" + dtCl.Rows[i]["ZFH"].ToString() + "' and Make='"+ dtCl.Rows[i]["Make"].ToString() +"'";
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
                else if (dtCl.Rows[e.Row]["WCFlag"].ToString() == "2")
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

        private void zf2kw(string clh,string zfh,string jhh,string make,string kw,string gg,int mz,string qa,string exflag)
        {
            string kw2 = Storage.GetCurrentKw(zfh,jhh,make);
            string now = DateTime.Now.ToString("yyyyMMddHHmmss");

            if(kw2!="")
            {
                dtCl.Rows.Add(new object[] { clh, Global.sKb, kw2,gg, mz, zfh,jhh,make, 1, now,qa,exflag });
                Storage.SetImpScanStatus(jhh, make, zfh, clh, Global.sKb+kw2, now);
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
                    Storage.SaveCurrentKw(zfh,jhh,make, kw2);
                    dtCl.Rows.Add(new object[] { clh, Global.sKb, kw2,gg, mz, zfh,jhh,make, 1, now,qa,exflag });
                    Storage.SetImpScanStatus(jhh, make,zfh, clh, Global.sKb+kw2, now);
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
                string jhh = dtCl.Rows[curRow]["JHH"].ToString();
                string zfh = dtCl.Rows[curRow]["ZFH"].ToString();
                string clh = dtCl.Rows[curRow]["CLH"].ToString();
                string qa = dtCl.Rows[curRow]["QA"].ToString();
                string kw = dtCl.Rows[curRow]["KW"].ToString();

                if (e.KeyCode == Keys.Enter)
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
                            dtCl.Rows.RemoveAt(curRow);
                            curRow = -1;
                        }
                        frm.Dispose();
                    }
                }

                if (e.KeyCode == Keys.F2)
                {
                    if (dtCl.Rows[curRow]["WCFlag"].ToString() == "1")
                    {
                        string remain = Storage.GetRemainCls(jhh,make, zfh);

                        frmInKW frm = new frmInKW();
                        frm.ShowDialog(clh,kw,remain);
                        if (frm.ret)
                        {
                            string kw2 = frm.m_kwnow;
                            dtCl.Rows[curRow]["KW"] = kw2;
                            Storage.SaveCurrentKw(zfh,jhh,make, kw2);
                            Storage.UpdateImpKw(jhh, make,zfh, clh, Global.sKb+kw2);
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

        private void addCL(string barcode)
        {
            string sql = "select CLH,substring(KW,4,len(KW)-3) KW,MZ,ZFH,Height,Width,Long,JHH,Make,QA from ImportStorageAcceptOrder where (WCFlag is null or WCFlag<>2) and RKB='" + Global.sKb + "' ";
            if (barcode.Length == 9 || barcode.Length == 10)
            {
                sql += "and (CLH2='" + barcode + "' or ( len(CLH2)=11 and charindex('" + barcode + "',CLH2)>0 ) ) ";
            }
            else
            {
                sql += "and CLH2='" + barcode + "'";
            }
            if (Global.storage.jhh != "")
            {
                sql += " and JHH='"+ Global.storage.jhh +"' and Make='"+ Global.storage.make +"'";
            }

            DataTable dt = SqlCe.ExecuteQuery(sql);

            if (dt.Rows.Count > 0)
            {
                barcode = dt.Rows[0]["CLH"].ToString();
                string kw = dt.Rows[0]["KW"].ToString();
                int mz = 0;
                if (dt.Rows[0]["MZ"] != DBNull.Value) mz = Convert.ToInt32(dt.Rows[0]["MZ"]);
                string zfh = dt.Rows[0]["ZFH"].ToString();
                string gg=dt.Rows[0]["Height"].ToString()+"×"+dt.Rows[0]["Width"].ToString()+"×"+dt.Rows[0]["Long"].ToString();
                string jhh = dt.Rows[0]["JHH"].ToString();
                string make = dt.Rows[0]["Make"].ToString();
                string qa = dt.Rows[0]["QA"].ToString();

                bool bAlreadyIn = false;
                for (int i = 0; i < dtCl.Rows.Count; i++)
                {
                    if (dtCl.Rows[i]["CLH"].ToString() == barcode)
                    {
                        bAlreadyIn = true;

                        frmMessage frm = new frmMessage();
                        frm.ShowDialog("是否取消消帐材料" + barcode, "提示");
                        if (frm.ret)
                        {
                            dtCl.Rows[i]["WCFlag"] = 0;
                            dtCl.Rows[i]["SCANTIME"] = "";
                            dtCl.Rows[i]["KW"] = "";
                            Storage.ClearImpScanStatus(jhh,make, zfh, barcode);
                            dtCl.Rows.RemoveAt(i);
                            curRow = -1;
                        }
                        frm.Dispose();

                        break;
                    }
                }

                if (bAlreadyIn == false)
                {
                    string exflag = Storage.GetExportFlag(barcode);
                    zf2kw(barcode, zfh,jhh,make, kw,gg, mz,qa, exflag);
                }
                
            }

            dt.Dispose();
            
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
                    txtClh.Text = "";
                }
            }

            if (txtClh.Text.Length == 5)
            {
                string sql = "select CLH2 from ImportStorageAcceptOrder where (WCFlag is null or WCFlag<>2) and  CLH2 like '%" + txtClh.Text + "' and RKB='"+ Global.sKb +"'";
                if (Global.storage.jhh != "")
                {
                    sql += " and JHH='" + Global.storage.jhh + "' and Make='" + Global.storage.make + "'";
                }

                DataTable dt = SqlCe.ExecuteQuery(sql);
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

        private void lstCLH_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtClh.Text = lstCLH.Items[lstCLH.SelectedIndex].ToString();
            txtClh.SelectionStart = txtClh.Text.Length;
            lstCLH.Visible = false;
        }

    }
}