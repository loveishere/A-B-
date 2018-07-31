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
    public partial class frmOutPlanCL : Form
    {
        public string m_jhh;
        public string m_shdw;
        public string m_zfh;
        public string m_hth;
        public string m_zph;
        public string m_lh;
        public int m_cltype;
        private DataTable dtCl;
        private int curRow;

        public frmOutPlanCL()
        {
            InitializeComponent();
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

            if (text.StartsWith("L")) text = text.Substring(1, text.Length - 1);

            addCL(barcode);

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

        private void addCL(string clh)
        {
            for (int i = 0; i < dtCl.Rows.Count; i++)
            {
                if (isSameCLH(dtCl.Rows[i]["CLH2"].ToString(), clh))
                {
                    if (dtCl.Rows[i]["WCFlag"].ToString() == "0")
                    {
                        dtCl.Rows[i]["WCFlag"] = 1;
                        dtCl.Rows[i]["SCANTIME"] = System.DateTime.Now.ToString("yyyyMMddHHmmss");
                        SqlCe.ExecuteNonQuery("update ExportStorageAcceptOrder set WCFlag=1,SCANTIME='" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + "' where CLH='" + dtCl.Rows[i]["CLH"].ToString() + "' and ZFH='" + dtCl.Rows[i]["ZFH"].ToString() + "' and JHH='" + dtCl.Rows[i]["JHH"].ToString() + "'");
                        PsionTeklogix.Sound.Beeper.Beeper.PlayTone(5000, 500, 100);
                    }
                    else if (dtCl.Rows[i]["WCFlag"].ToString() == "1")
                    {
                        frmMessage frm = new frmMessage();
                        frm.ShowDialog("是否取消消帐材料" + clh, "提示");
                        if (frm.ret)
                        {
                            dtCl.Rows[i]["WCFlag"] = 0;
                            dtCl.Rows[i]["SCANTIME"] = "";
                            SqlCe.ExecuteNonQuery("update ExportStorageAcceptOrder set WCFlag=0,SCANTIME='' where CLH='" + dtCl.Rows[i]["CLH"].ToString() + "' and ZFH='" + dtCl.Rows[i]["ZFH"].ToString() + "' and JHH='" + dtCl.Rows[i]["JHH"].ToString() + "'");
                        }
                        frm.Dispose();
                    }
                    break;
                }
            }
        }

        private void frmOutPlanCL_Load(object sender, EventArgs e)
        {
            ScanCodeRemapping.NormalTable.Remove(0x003A);

            frmOutPlanZF frmOutPlanZF = (frmOutPlanZF)Global.frmCurrent;
            m_jhh = frmOutPlanZF.m_jhh;
            m_shdw = frmOutPlanZF.m_shdw;
            m_hth = frmOutPlanZF.m_hth;
            m_zfh = frmOutPlanZF.m_zfh;
            m_lh = frmOutPlanZF.m_lh;
            m_zph = frmOutPlanZF.m_zph;
            m_cltype = frmOutPlanZF.m_cltype;

            lblJhh.Text = "提单号："+ m_jhh +"\r\n";
            lblShdw.Text = "收货单位：" + m_shdw + "\r\n";
            

            LoadData();

        }

        public void LoadData()
        {
            string sql = "select B.JHH,B.ZFH,B.CLH,B.CLH2,B.MZ,B.JZ,substring(B.KW,4,8) KW,";
            sql += "B.Height+'×'+B.Width+'×'+B.Long GG,";
            sql += "B.WCFlag,B.SCANTIME from ";
            sql += "ExportStorageOrder A,ExportStorageAcceptOrder B where A.ZFH=B.ZFH and A.JHH=B.JHH and (B.WCFlag is null or B.WCFlag<>2) ";

            switch (m_cltype)
            {
                case 0:
                    lblZfh.Text += "准发号：" + m_zfh;
                    sql += "and A.ZFH='" + m_zfh + "'";
                    break;
                case 1:
                    lblZfh.Text += "炉号：" + m_lh;
                    sql += "and B.LH='" + m_lh + "'";
                    break;
                case 2:
                    lblZfh.Text += "合同号：" + m_hth;
                    sql += "and A.HTH='" + m_hth + "'";
                    break;
                case 3:
                    lblZfh.Text += "扎批号：" + m_zph;
                    sql += "and substring(B.CLH,1,6)='" + m_zph + "'";
                    break;
                default:
                    break;
            }

            dtCl = SqlCe.ExecuteQuery(sql);
            curRow = -1;

            dgCl.DataSource = dtCl;
            dgCl.TableStyles.Clear();

            DataGridTableStyle clStyle = new DataGridTableStyle();
            clStyle.MappingName = dtCl.TableName;
            CheckCellEventHandler cellEvent = new CheckCellEventHandler(CheckCellColor);

            clStyle.GridColumnStyles.Add(new ColumnStyle(0, "CLH", "材料号", 100, "", cellEvent));
            clStyle.GridColumnStyles.Add(new ColumnStyle(1, "KW", "库位", 70, "", cellEvent));
            clStyle.GridColumnStyles.Add(new ColumnStyle(2, "GG", "规格", 170, "", cellEvent));
            clStyle.GridColumnStyles.Add(new ColumnStyle(3, "JZ", "净重", 60, "", cellEvent));
            clStyle.GridColumnStyles.Add(new ColumnStyle(4, "MZ", "毛重", 60, "", cellEvent));

            dgCl.TableStyles.Add(clStyle);
        }

        public void CheckCellColor(object sender, DataGridEnableEventArgs e)
        {
            if (dgCl.CurrentCell.RowNumber == e.Row && curRow != -1)
                e.MeetsCriteria = false;
            else
            {
                if ((dtCl.Rows[e.Row]["WCFlag"].ToString() == "0" || dtCl.Rows[e.Row]["WCFlag"].ToString() == "") && dtCl.Rows[e.Row]["KW"].ToString() != "")
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
            else if (e.KeyCode == Keys.F24)
            {
                frmOut frm = new frmOut();
                frm.m_jhh = m_jhh;
                frm.m_zfh = m_zfh;
                frm.m_zph = m_zph;
                frm.m_lh = m_lh;
                frm.m_cltype = m_cltype;
                frm.m_hth = m_hth;
                Global.frmCurrent = frm;
                frm.Owner = this;
                frm.Show();
                this.Hide();
            }
            else if (e.KeyCode == Keys.F21)
            {

                int js = 0;
                int mz = 0;
                string clhs = "";
                for (int i = 0; i < dtCl.Rows.Count; i++)
                {
                    if (dtCl.Rows[i]["WCFlag"].ToString() == "1")
                    {
                        clhs += "'"+dtCl.Rows[i]["CLH"].ToString() + "',";
                        js++;
                        if (dtCl.Rows[i]["MZ"].ToString() != "")
                        {
                            mz += Convert.ToInt32(dtCl.Rows[i]["MZ"]);
                        }
                    }
                }
                if (clhs.Length > 0) clhs = clhs.Substring(0, clhs.Length - 1);

                if (js > 0)
                {
                    frmSend frmSend = new frmSend();
                    frmSend.js = js.ToString();
                    frmSend.mz = mz.ToString();
                    frmSend.clhs = clhs;
                    Global.frmCurrent = frmSend;
                    frmSend.Owner = this;
                    frmSend.Show();
                    this.Hide();
                }
            }
        }

        public void OutStore()
        {
            string sql;

            for (int i = 0; i < dtCl.Rows.Count; i++)
            {
                if (dtCl.Rows[i]["WCFlag"].ToString() == "1")
                {
                    sql = "update ExportStorageAcceptOrder set WCFlag=2 where CLH='" + dtCl.Rows[i]["CLH"].ToString() + "' and ZFH='" + dtCl.Rows[i]["ZFH"].ToString() + "' and JHH='" + dtCl.Rows[i]["JHH"].ToString() + "'";
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

        private void dgCl_CurrentCellChanged(object sender, EventArgs e)
        {
            curRow = dgCl.CurrentCell.RowNumber;
            dgCl.Select(dgCl.CurrentCell.RowNumber);
        }

        private void frmOutPlanCL_Closed(object sender, EventArgs e)
        {
            ScanCodeRemapping.NormalTable.Add(0x003A, new RemappingTable.Remapping(VirtualKey.VK_F24, Function.SendCode));
        }

        private void dgCl_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (curRow >= 0)
                {
                    if (dtCl.Rows[curRow]["WCFlag"].ToString() == "0")
                    {
                        if (dtCl.Rows[curRow]["KW"].ToString() != "")
                        {
                            dtCl.Rows[curRow]["WCFlag"] = 1;
                            dtCl.Rows[curRow]["SCANTIME"] = System.DateTime.Now.ToString("yyyyMMddHHmmss");
                            SqlCe.ExecuteNonQuery("update ExportStorageAcceptOrder set WCFlag=1,SCANTIME='" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + "' where CLH='" + dtCl.Rows[curRow]["CLH"].ToString() + "' and ZFH='" + dtCl.Rows[curRow]["ZFH"].ToString() + "' and JHH='" + dtCl.Rows[curRow]["JHH"].ToString() + "'");
                            PsionTeklogix.Sound.Beeper.Beeper.PlayTone(5000, 500, 100);
                        }
                    }
                    else if (dtCl.Rows[curRow]["WCFlag"].ToString() == "1")
                    {
                        frmMessage frmMessage = new frmMessage();
                        frmMessage.ShowDialog("是否取消消帐材料" + dtCl.Rows[curRow]["CLH"].ToString(), "提示");
                        if (frmMessage.ret)
                        {
                            dtCl.Rows[curRow]["WCFlag"] = 0;
                            dtCl.Rows[curRow]["SCANTIME"] = "";
                            SqlCe.ExecuteNonQuery("update ExportStorageAcceptOrder set WCFlag=0,SCANTIME='' where CLH='" + dtCl.Rows[curRow]["CLH"].ToString() + "' and ZFH='" + dtCl.Rows[curRow]["ZFH"].ToString() + "' and JHH='" + dtCl.Rows[curRow]["JHH"].ToString() + "'");
                        }
                    }
                }
            }
        }
    }
}