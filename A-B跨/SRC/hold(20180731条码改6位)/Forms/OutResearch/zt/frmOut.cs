using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using PsionTeklogix.Keyboard;
using PsionTeklogix.Keyboard.NoChording;

namespace hStore.Forms.OutResearch.zt
{
    public partial class frmOut : Form
    {
        public string m_zfh;
        public string m_jhh;
        public string m_hth;
        public string m_zph;
        public string m_lh;
        public int m_cltype;
        private DataTable dtOut;
        private int curRow;
        private string clhs;

        public frmOut()
        {
            InitializeComponent();
        }


        private void frmOut_Load(object sender, EventArgs e)
        {
            //ScanCodeRemapping.NormalTable.Remove(0x003A);

            LoadData();
        }

        public void LoadData()
        {
            lblJhh.Text = "提单号：" + m_jhh;
            string sql = "select B.JHH,B.ZFH,B.CLH,B.CLH2,B.MZ,B.JZ,substring(B.KW,4,len(B.KW)-3) KW,";
            sql += "B.Height+'×'+B.Width+'×'+B.Long GG,";
            sql += "B.WCFlag,B.SCANTIME from ";
            sql += "ExportStorageOrder A,ExportStorageAcceptOrder B where A.ZFH=B.ZFH and A.JHH=B.JHH and (B.WCFlag is null or B.WCFlag<>2) and B.KW<>'' ";
            sql += "and B.CKB='" + Global.sKb + "' and A.JHH like '1%' ";
            sql += "and A.JHH='" + m_jhh + "' ";

            switch (m_cltype)
            {
                case 0:
                    sql += "and A.ZFH='" + m_zfh + "'";
                    lblZfh.Text += "准发号：" + m_zfh;
                    break;
                case 1:
                    sql += "and B.LH='" + m_lh + "'";
                    lblZfh.Text += "炉号：" + m_lh;
                    break;
                case 2:
                    sql += "and A.HTH='" + m_hth + "'";
                    lblZfh.Text += "合同号：" + m_hth;
                    break;
                case 3:
                    lblZfh.Text += "扎批号：" + m_zph;
                    sql += "and substring(B.CLH,1,6)='" + m_zph + "'";
                    break;
                default:
                    break;
            }

            dtOut = SqlCe.ExecuteQuery(sql);
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
                if (isSameCLH(dtOut.Rows[i]["CLH2"].ToString(), text))
                {
                    if (dtOut.Rows[i]["WCFlag"].ToString() == "" || dtOut.Rows[i]["WCFlag"].ToString()=="0")
                    {
                        dtOut.Rows[i]["SCANTIME"] = System.DateTime.Now.ToString("yyyyMMddHHmmss");
                        dtOut.Rows[i]["WCFlag"] = 1;
                        SqlCe.ExecuteNonQuery("update ExportStorageAcceptOrder set WCFlag=1,SCANTIME='" + dtOut.Rows[i]["SCANTIME"].ToString() + "' where CLH='" + dtOut.Rows[i]["CLH"].ToString() + "' and ZFH='" + dtOut.Rows[i]["ZFH"].ToString() + "' and JHH='" + dtOut.Rows[i]["JHH"].ToString() + "'");
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
                            SqlCe.ExecuteNonQuery("update ExportStorageAcceptOrder set WCFlag=0,SCANTIME='' where CLH='" + dtOut.Rows[i]["CLH"].ToString() + "' and ZFH='" + dtOut.Rows[i]["ZFH"].ToString() + "' and JHH='" + dtOut.Rows[i]["JHH"].ToString() + "'");
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
            if (text.Length == 9 || text.Length == 10)
            {
                return (clh.EndsWith(text));
            }
            else
            {
                return (clh == text);
            }
        }

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
                if (txtJs.Text != "" && txtJs.Text != "0" )
                {
                    frmSend frmSend = new frmSend();
                    frmSend.js = txtJs.Text;
                    frmSend.mz = txtMz.Text;
                    frmSend.clhs = clhs;
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
                }
            }

            if (txtClh.Text.Length == 5)
            {
                string sql = "select B.CLH2 from ExportStorageOrder A,ExportStorageAcceptOrder B where A.ZFH=B.ZFH and (B.WCFlag is null or B.WCFlag<>2) and B.KW<>'' ";
                sql += " and B.CLH2 like '%" + txtClh.Text + "' and B.CKB='" + Global.sKb + "' and A.JHH like '1%' ";
                sql += "and A.JHH='" + m_jhh + "' ";
                switch (m_cltype)
                {
                    case 0:
                        sql += "and A.ZFH='" + m_zfh + "'";
                        break;
                    case 1:
                        sql += "and B.LH='" + m_lh + "'";
                        break;
                    case 2:
                        sql += "and A.HTH='" + m_hth + "'";
                        break;
                    case 3:
                        sql += "and substring(B.CLH,1,6)='" + m_zph + "'";
                        break;
                    default:
                        break;
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

        private void dgOut_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (curRow >= 0)
                {
                    if (dtOut.Rows[curRow]["WCFlag"].ToString() == "" || dtOut.Rows[curRow]["WCFlag"].ToString() == "0")
                    {
                        dtOut.Rows[curRow]["SCANTIME"] = System.DateTime.Now.ToString("yyyyMMddHHmmss");
                        dtOut.Rows[curRow]["WCFlag"] = 1;
                        SqlCe.ExecuteNonQuery("update ExportStorageAcceptOrder set WCFlag=1,SCANTIME='" + dtOut.Rows[curRow]["SCANTIME"].ToString() + "' where CLH='" + dtOut.Rows[curRow]["CLH"].ToString() + "' and ZFH='" + dtOut.Rows[curRow]["ZFH"].ToString() + "' and JHH='" + dtOut.Rows[curRow]["JHH"].ToString() + "'");
                        PsionTeklogix.Sound.Beeper.Beeper.PlayTone(5000, 500, 100);
                    }
                    else if (dtOut.Rows[curRow]["WCFlag"].ToString() == "1")
                    {
                        frmMessage frm = new frmMessage();
                        frm.ShowDialog("是否取消消帐材料" + dtOut.Rows[curRow]["CLH"].ToString(), "提示");
                        if (frm.ret)
                        {
                            SqlCe.ExecuteNonQuery("update ExportStorageAcceptOrder set WCFlag=0,SCANTIME='' where CLH='" + dtOut.Rows[curRow]["CLH"].ToString() + "' and ZFH='" + dtOut.Rows[curRow]["ZFH"].ToString() + "' and JHH='" + dtOut.Rows[curRow]["JHH"].ToString() + "'");
                            dtOut.Rows[curRow]["WCFlag"] = 0;
                            dtOut.Rows[curRow]["SCANTIME"] = "";
                        }
                        frm.Dispose();
                    }
                    RefreshJS();
                }
            }
        }

        public void OutStore()
        {
            string sql;

            for (int i = 0; i < dtOut.Rows.Count; i++)
            {
                if (dtOut.Rows[i]["WCFlag"].ToString() == "1")
                {
                    sql = "update ExportStorageAcceptOrder set WCFlag=2 where CLH='" + dtOut.Rows[i]["CLH"].ToString() + "' and ZFH='" + dtOut.Rows[i]["ZFH"].ToString() + "' and JHH='" + dtOut.Rows[i]["JHH"].ToString() + "'";
                    SqlCe.ExecuteNonQuery(sql);
                }
            }
            LoadData();

            Storage.UpdateExpWcjs();

            //sql = "select A.JHH,A.ZFH,count(*) WCJS,sum(B.MZ) WCMZ,sum(B.JZ) WCJZ from ExportStorageOrder A,ExportStorageAcceptOrder B ";
            //sql += "where A.ZFH=B.ZFH and A.JHH=B.JHH and B.WCFlag=2 group by A.JHH,A.ZFH";

            //DataTable dt = SqlCe.ExecuteQuery(sql);

            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    SqlCe.ExecuteNonQuery("update ExportStorageOrder set WCJS=" + dt.Rows[i]["WCJS"].ToString() + ",WCMZ=" + dt.Rows[i]["WCMZ"].ToString() + ",WCJZ=" + dt.Rows[i]["WCJZ"].ToString() + " where ZFH='" + dt.Rows[i]["ZFH"].ToString() + "' and JHH='" + dt.Rows[i]["JHH"].ToString() + "'");
            //}

            //sql = "select A.JHH,sum(B.WCJS) WCJS,sum(B.WCMZ) WCMZ,sum(B.WCJZ) WCJZ  from ExportStoragePlan A,ExportStorageOrder B ";
            //sql += "where A.JHH=B.JHH group by A.JHH";

            //dt = SqlCe.ExecuteQuery(sql);

            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    SqlCe.ExecuteNonQuery("update ExportStoragePlan set WCJS=" + dt.Rows[i]["WCJS"].ToString() + ",WCMZ=" + dt.Rows[i]["WCMZ"].ToString() + ",WCJZ=" + dt.Rows[i]["WCJZ"].ToString() + " where JHH='" + dt.Rows[i]["JHH"].ToString() + "'");
            //}

            //dt.Dispose();

            //sql = "delete from ExportStorageAcceptOrder where exists(select * from ExportStoragePlan where JS=WCJS and JHH=ExportStorageAcceptOrder.JHH)";
            //SqlCe.ExecuteNonQuery(sql);
            //sql = "delete from ExportStorageOrder where exists(select * from ExportStoragePlan where JS=WCJS and JHH=ExportStorageOrder.JHH)";
            //SqlCe.ExecuteNonQuery(sql);
            //sql = "delete from ExportStoragePlan where JS=WCJS";
            //SqlCe.ExecuteNonQuery(sql);
        }

        private void RefreshJS()
        {
            int js = 0;
            int mz = 0;
            clhs = "";

            for (int i = 0; i < dtOut.Rows.Count; i++)
            {
                if (dtOut.Rows[i]["WCFlag"].ToString() == "1")
                {
                    clhs += "'"+dtOut.Rows[i]["CLH"].ToString() + "',";
                    js++;
                    if (dtOut.Rows[i]["MZ"].ToString() != "")
                    {
                        mz += Convert.ToInt32(dtOut.Rows[i]["MZ"]);
                    }
                }
            }
            if (clhs.Length > 0) clhs = clhs.Substring(0, clhs.Length - 1);
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