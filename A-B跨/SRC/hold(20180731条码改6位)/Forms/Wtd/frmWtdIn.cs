using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using PsionTeklogix.Keyboard;
using PsionTeklogix.Keyboard.NoChording;

namespace hStore.Forms
{
    public partial class frmWtdIn : Form
    {
        private DataTable dtCl;
        private int curRow;

        public frmWtdIn()
        {
            InitializeComponent();
        }

        private void dgWtdCl_KeyUp(object sender, KeyEventArgs e)
        {
            if (curRow >= 0)
            {
                string jhh = Global.storage.jhh;
                string clh = dtCl.Rows[curRow]["CLH"].ToString();
                string lbl = dtCl.Rows[curRow]["LBL"].ToString();
                string qa = dtCl.Rows[curRow]["QA"].ToString();
                string kw = dtCl.Rows[curRow]["KW"].ToString();
                string sJs = dtCl.Rows[curRow]["JS"].ToString();
                string sZl = dtCl.Rows[curRow]["ZL"].ToString();
                string sFlag = dtCl.Rows[curRow]["FLAG"].ToString();

                if (e.KeyCode == Keys.Enter)
                {
                    if (dtCl.Rows[curRow]["WCFlag"].ToString() == "0" || dtCl.Rows[curRow]["WCFlag"].ToString() == "")
                    {
                        //弹出窗体，输入件数、重量
                        //条码字段为空，要求重新绑定条码
                        wtd2kw(clh,"", jhh,sJs,sZl);
                    }
                    else if (dtCl.Rows[curRow]["WCFlag"].ToString() == "1")
                    {
                        frmMessage frm = new frmMessage();
                        frm.ShowDialog("是否取消消帐材料" + clh, "提示");
                        if (frm.ret)
                        {
                            Storage.ClearWtdInScanStatus(jhh, lbl,sFlag);
                            RefreshData();
                            curRow = -1;
                        }
                        frm.Dispose();
                    }
                }

                if (e.KeyCode == Keys.F2)
                {
                    if (dtCl.Rows[curRow]["WCFlag"].ToString() == "1")
                    {
                        frmWtdInCl frmWtdInCl = new frmWtdInCl();
                        frmWtdInCl.ShowDialog(clh, lbl, kw, sJs, sZl);
                        if (frmWtdInCl.ret)
                        {
                            kw = frmWtdInCl.sKw;
                            sJs = frmWtdInCl.sJs;
                            sZl = frmWtdInCl.sZL;
                            lbl = frmWtdInCl.sLbl;

                            dtCl.Rows[curRow]["KW"] = kw;
                            dtCl.Rows[curRow]["JS"] = sJs;
                            dtCl.Rows[curRow]["ZL"] = sZl;


                            Storage.SaveCurrentKw("0000000000", jhh, "SGYS", kw);
                            Storage.UpdateWtdInKw(jhh, lbl, Global.sKb + kw, sJs, sZl);

                            RefreshData();
                        }

                        frmWtdInCl.Dispose();
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

        public void scanner_ScanCompleteEvent(string text)
        {
            string barcode = text;

            bool bval = false;

            if (barcode.Length < 5) return;

            char[] chr = barcode.ToCharArray();

            for (int i = 0; i < chr.Length; i++)
            {

                if ((chr[i] < '0' || chr[i] > '9') && (chr[i] < 'A' || chr[i] > 'Z'))
                {
                    bval = true;
                    break;
                }
            }

            if (bval) return;

            addCL(barcode);

        }

        private void addCL(string text)
        {
            bool bAlreadyIn = false;

            for (int i = 0; i < dtCl.Rows.Count; i++)
            {
                string clh = dtCl.Rows[i]["CLH"].ToString();
                string lbl = dtCl.Rows[i]["LBL"].ToString();
                string jhh = Global.storage.jhh;
                string flag = dtCl.Rows[i]["FLAG"].ToString();

                if (text==lbl)
                {
                    bAlreadyIn = true;
                    frmMessage frm = new frmMessage();
                    frm.ShowDialog("是否取消消帐材料" + text, "提示");
                    if (frm.ret)
                    {
                        dtCl.Rows[i]["WCFlag"] = 0;
                        dtCl.Rows[i]["SCANTIME"] = "";
                        dtCl.Rows[i]["KW"] = "";
                        Storage.ClearWtdInScanStatus(jhh,lbl,flag);
                        RefreshData();
                        curRow = -1;
                    }
                    frm.Dispose();

                    break;
                }
            }

            if (bAlreadyIn == false)
            {
                //弹出窗体，输入件数、重量
                wtd2kw("", text, Global.storage.jhh);
            }
        }

        private void wtd2kw(string clh, string lbl, string jhh, string js, string zl)
        {
            string kw2 = Storage.GetCurrentKw("0000000000",jhh,"SGYS");
            string now = DateTime.Now.ToString("yyyyMMddHHmmss");

            frmWtdInCl frmWtdInCl = new frmWtdInCl();
            //***********************************************
            //此次需要重新考虑，建议使用Show()来代替ShowDialog()，使得条码扫描能正确地调用scanner_ScanCompleteEvent()方法
            frmWtdInCl.ShowDialog(clh, lbl, kw2, js, zl);
            if (frmWtdInCl.ret)
            {
                string sJs = frmWtdInCl.sJs;
                string sZl = frmWtdInCl.sZL;
                string sKw = frmWtdInCl.sKw;
                string sLbl = frmWtdInCl.sLbl;

                Storage.SetWtdInScanStatus(jhh, clh, Global.sKb + sKw, sJs, sZl, now, sLbl);
                PsionTeklogix.Sound.Beeper.Beeper.PlayTone(5000, 500, 100);

                if (sKw != kw2)
                {
                    Storage.SaveCurrentKw("0000000000",jhh,"SGYS", sKw);
                }
                RefreshData();
            }

            frmWtdInCl.Dispose();
        }

        private void wtd2kw(string clh,string lbl,string jhh)
        {
            string kw2 = Storage.GetCurrentKw("0000000000", jhh, "SGYS");
            string now = DateTime.Now.ToString("yyyyMMddHHmmss");

            frmWtdInCl frmWtdInCl = new frmWtdInCl();
            frmWtdInCl.ShowDialog(clh , lbl, kw2);
            if (frmWtdInCl.ret)
            {
                string sJs = frmWtdInCl.sJs;
                string sZl = frmWtdInCl.sZL;
                string sKw = frmWtdInCl.sKw;
                string sLbl = frmWtdInCl.sLbl;

                Storage.SetWtdInScanStatus(jhh,clh,Global.sKb + sKw,sJs,sZl,now,sLbl);
                PsionTeklogix.Sound.Beeper.Beeper.PlayTone(5000, 500, 100);

                if (sKw != kw2)
                {
                    Storage.SaveCurrentKw("0000000000",jhh,"SGYS", sKw);
                }

                RefreshData();
            }

            frmWtdInCl.Dispose();
        }

        private void frmWtdIn_Load(object sender, EventArgs e)
        {
            if (ScanCodeRemapping.NormalTable.Contains(0x003A))
            {
                ScanCodeRemapping.NormalTable.Remove(0x003A);
            }

            txtWtdInf.Text =  Global.storage.jhh ;

            RefreshData();

            DataGridTableStyle wtdclStyle = new DataGridTableStyle();
            wtdclStyle.MappingName = dtCl.TableName;
            CheckCellEventHandler cellEvent = new CheckCellEventHandler(CheckCellColor);

            wtdclStyle.GridColumnStyles.Add(new ColumnStyle(0, "lbl", "材料号", 80, "", cellEvent));
            wtdclStyle.GridColumnStyles.Add(new ColumnStyle(1, "KW", "库位", 55, "", cellEvent));
            wtdclStyle.GridColumnStyles.Add(new ColumnStyle(2, "js", "件数", 45, "", cellEvent));
            wtdclStyle.GridColumnStyles.Add(new ColumnStyle(3, "zl", "重量", 50, "", cellEvent));

            dgWtdCl.TableStyles.Add(wtdclStyle);
        }

        public void RefreshData()
        {
            dtCl = Storage.GetWtdCl(Global.storage.jhh);
            curRow = -1;
            dgWtdCl.DataSource = dtCl;
        }

        public void CheckCellColor(object sender, DataGridEnableEventArgs e)
        {
            if (dgWtdCl.CurrentCell.RowNumber == e.Row && curRow != -1)
                e.MeetsCriteria = false;
            else
            {
                string wcflag=dtCl.Rows[e.Row]["WCFlag"].ToString();
                string qa = dtCl.Rows[e.Row]["QA"].ToString();
                if (wcflag == "" || wcflag == "0")
                {
                    e.MeetsCriteria = true;
                    e.BackColor = Color.Cyan;
                    e.ForeColor = SystemColors.WindowText;
                }
                else if (wcflag == "1")
                {
                    e.MeetsCriteria = true;
                    if (qa != "" && qa != "0")
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

        private void frmWtdIn_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Global.frmCurrent = this.Owner;
                this.Owner.Show();
                this.Owner = null;
                this.Close();
            }
            if (e.KeyCode == Keys.F21)
            {
                int js = Storage.GetWtdScanJs(Global.storage.jhh);
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

        private void dgWtdCl_CurrentCellChanged(object sender, EventArgs e)
        {
            curRow = dgWtdCl.CurrentCell.RowNumber;
            dgWtdCl.Select(dgWtdCl.CurrentCell.RowNumber);
        }
    }
}