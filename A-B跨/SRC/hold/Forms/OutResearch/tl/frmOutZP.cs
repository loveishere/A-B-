using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using PsionTeklogix.Keyboard;
using PsionTeklogix.Keyboard.NoChording;

namespace hStore.Forms.OutResearch.tl
{
    public partial class frmOutZP : Form
    {
        private DataTable dtCl;
        public string m_zph;
        private string m_jhdd;
        private string m_js;
        private string m_mz;
        private string gram;
        private int curRow;

        public frmOutZP()
        {
            InitializeComponent();
        }

        private void frmOutZP_Load(object sender, EventArgs e)
        {
            ScanCodeRemapping.NormalTable.Remove(0x003A);

            frmZPAll frmZPAll = (frmZPAll)App.LinkedListForm.Last.Value;
            m_zph = frmZPAll.m_zph;
            m_jhdd = frmZPAll.m_jhdd;
            m_js = frmZPAll.m_js;
            m_mz = frmZPAll.m_mz;

            lblInfo.Text = "组批号：" + m_zph + " 到站：" + m_jhdd + "\r\n";
            lblInfo.Text += "总毛重：" + m_mz + " 件数：" + m_js;

            LoadData();

        }

        public void LoadData()
        {
            string sql = "select ZPH,CLH,MZ,KW,WCFlag,";
            sql += "Height + '×' + Width + '×' + Long GG,SCANTIME from ExportStorageAcceptOrder ";
            sql += "where ZPH='" + m_zph + "'";
            dtCl = App.ExecuteQuery(sql);
            curRow = -1;
            dgCl.DataSource = dtCl;
            dgCl.TableStyles.Clear();

            DataGridTableStyle clStyle = new DataGridTableStyle();
            clStyle.MappingName = dtCl.TableName;
            CheckCellEventHandler cellEvent = new CheckCellEventHandler(CheckCellColor);

            clStyle.GridColumnStyles.Add(new ColumnStyle(0, "CLH", "材料号", 120, "", cellEvent));
            clStyle.GridColumnStyles.Add(new ColumnStyle(1, "KW", "库位", 70, "", cellEvent));
            clStyle.GridColumnStyles.Add(new ColumnStyle(2, "MZ", "毛重", 70, "", cellEvent));
            clStyle.GridColumnStyles.Add(new ColumnStyle(3, "GG", "规格", 70, "", cellEvent));


            dgCl.TableStyles.Add(clStyle);
            RefreshJS();
        }

        public void CheckCellColor(object sender, DataGridEnableEventArgs e)
        {
            if (dgCl.CurrentCell.RowNumber == e.Row && curRow != -1)
                e.MeetsCriteria = false;
            else
            {
                if (dtCl.Rows[e.Row]["WCFlag"].ToString() == "0" || dtCl.Rows[e.Row]["WCFlag"].ToString() == "")
                {
                    e.MeetsCriteria = true;
                    e.BackColor = Color.Cyan;
                    e.ForeColor = SystemColors.WindowText;
                }
                else if (dtCl.Rows[e.Row]["WCFlag"].ToString() == "1")
                {
                    e.MeetsCriteria = true;
                    e.BackColor = Color.Salmon;
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

        private void frmOutZP_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                App.LinkedListForm.RemoveLast();
                App.InvokeMethod(App.LinkedListForm.Last.Value, "LoadData", new object[] { });
                this.Close();
            }
            if (e.KeyCode == Keys.F21)
            {
                if (txtJs.Text != "")
                {
                    frmSend frmSend = new frmSend();
                    frmSend.js = txtJs.Text;
                    frmSend.mz = txtMz.Text;
                    frmSend.gram = gram;
                    frmSend.Show();
                    App.LinkedListForm.AddLast(frmSend);
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

            addCL(text);
        }

        private void dgCl_CurrentCellChanged(object sender, EventArgs e)
        {
            curRow = dgCl.CurrentCell.RowNumber;
            dgCl.Select(dgCl.CurrentCell.RowNumber);
        }

        private void txtClh_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtClh.Text.Length > 4)
                {
                    addCL(txtClh.Text);
                }
            }
        }

        private void addCL(string text)
        {
            for (int i = 0; i < dtCl.Rows.Count; i++)
            {
                if (isSameCLH(dtCl.Rows[i]["CLH"].ToString(), text))
                {
                    if (dtCl.Rows[i]["WCFlag"].ToString() == "" || dtCl.Rows[i]["WCFlag"].ToString() == "0")
                    {
                        dtCl.Rows[i]["SCANTIME"] = System.DateTime.Now.ToString("yyyyMMddHHmmss");
                        dtCl.Rows[i]["WCFlag"] = 1;
                        App.ExecuteNonQuery("update ExportStorageAcceptOrder set WCFlag=1,SCANTIME='" + dtCl.Rows[i]["SCANTIME"].ToString() + "' where CLH='" + dtCl.Rows[i]["CLH"].ToString() + "' and ZFH='" + dtCl.Rows[curRow]["ZFH"].ToString() + "'");
                        PsionTeklogix.Sound.Beeper.Beeper.PlayTone(5000, 500, 100);
                    }
                    else if (dtCl.Rows[i]["WCFlag"].ToString() == "1")
                    {
                        frmMessage frm = new frmMessage();
                        frm.ShowDialog("是否取消消帐材料" + text, "提示");
                        if (frm.ret)
                        {
                            App.ExecuteNonQuery("update ExportStorageAcceptOrder set WCFlag=0,SCANTIME='' where CLH='" + dtCl.Rows[i]["CLH"].ToString() + "' and ZFH='" + dtCl.Rows[curRow]["ZFH"].ToString() + "'");
                            dtCl.Rows[i]["WCFlag"] = 0;
                            dtCl.Rows[i]["SCANTIME"] = "";
                        }
                        frm.Dispose();
                    }
                    break;
                }
            }

            RefreshJS();
        }

        private bool isSameCLH(string clh, string text)
        {
            if (text.Length != 9)
            {
                return (clh == text);
            }
            else
            {
                return (clh.EndsWith(text));
            }
        }

        private void RefreshJS()
        {
            int js = 0;
            int mz = 0;
            gram = "";

            for (int i = 0; i < dtCl.Rows.Count; i++)
            {
                if (dtCl.Rows[i]["WCFlag"].ToString() == "1")
                {
                    gram += dtCl.Rows[i]["CLH"].ToString() + ";";
                    gram += dtCl.Rows[i]["SCANTIME"].ToString() + ";";
                    js++;
                    if (dtCl.Rows[i]["MZ"].ToString() != "")
                    {
                        mz += Convert.ToInt32(dtCl.Rows[i]["MZ"]);
                    }
                }
            }
            if (gram.Length > 0) gram = gram.Substring(0, gram.Length - 1);
            txtJs.Text = js.ToString();
            txtMz.Text = mz.ToString();
        }

        private void frmOutZP_Closed(object sender, EventArgs e)
        {
            ScanCodeRemapping.NormalTable.Add(0x003A, new RemappingTable.Remapping(VirtualKey.VK_F24, Function.SendCode));
        }

        private void dgCl_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (curRow >= 0)
                {
                    if (dtCl.Rows[curRow]["WCFlag"].ToString() == "" || dtCl.Rows[curRow]["WCFlag"].ToString() == "0")
                    {
                        dtCl.Rows[curRow]["SCANTIME"] = System.DateTime.Now.ToString("yyyyMMddHHmmss");
                        dtCl.Rows[curRow]["WCFlag"] = 1;
                        App.ExecuteNonQuery("update ExportStorageAcceptOrder set WCFlag=1,SCANTIME='" + dtCl.Rows[curRow]["SCANTIME"].ToString() + "' where CLH='" + dtCl.Rows[curRow]["CLH"].ToString() + "' and ZFH='" + dtCl.Rows[curRow]["ZFH"].ToString() + "'");
                        PsionTeklogix.Sound.Beeper.Beeper.PlayTone(5000, 500, 100);
                    }
                    else if (dtCl.Rows[curRow]["WCFlag"].ToString() == "1")
                    {
                        frmMessage frm = new frmMessage();
                        frm.ShowDialog("是否取消消帐材料" + dtCl.Rows[curRow]["CLH"].ToString(), "提示");
                        if (frm.ret)
                        {
                            App.ExecuteNonQuery("update ExportStorageAcceptOrder set WCFlag=0,SCANTIME='' where CLH='" + dtCl.Rows[curRow]["CLH"].ToString() + "' and ZFH='" + dtCl.Rows[curRow]["ZFH"].ToString() + "'");
                            dtCl.Rows[curRow]["WCFlag"] = 0;
                            dtCl.Rows[curRow]["SCANTIME"] = "";
                        }
                        frm.Dispose();
                    }
                    RefreshJS();
                }
            }
        }

    }
}