using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace hStore.Forms.OutResearch
{
    public partial class frmOutPlanZF : Form
    {
        private DataTable dtZf;
        private int curRow;
        //public string m_jhh;
        //public string m_jhdd;
        //public string m_zfh;

        public frmOutPlanZF()
        {
            InitializeComponent();
        }

        private void frmOutPlanZF_Load(object sender, EventArgs e)
        {
            //frmOutPlan frmOutPlan = (frmOutPlan)Global.frmCurrent;
            //m_jhh = frmOutPlan.m_jhh;
            //m_jhdd = frmOutPlan.m_jhdd;
            lblInfo.Text = "计划号："+ Global.storage.jhh +"\r\n";
            lblInfo.Text += "交货地点："+Global.storage.jhdd;

            //string sql = "substring(B.KW,4,8) KW,sum(case B.WCFlag when 2 then 0 else 1 end) SYJS, sum(case B.WCFlag when 2 then 1 else 0 end) WCJS, count(*) JS, ";
            //sql += "sum(case B.WCFlag when 2 then 0 else B.MZ end) SYMZ, sum(case B.WCFlag when 2 then B.MZ else 0 end) WCMZ, sum(B.MZ) MZ,";
            //sql += "sum(case B.WCFlag when 2 then 0 else B.JZ end) SYJZ, sum(case B.WCFlag when 2 then B.JZ else 0 end) WCJZ, sum(B.MZ) JZ,";
            //sql += "max(A.PM) PM,max(B.Height+'×'+B.Width+'×'+B.Long) GG ";
            //sql += "from ExportStorageOrder A,ExportStorageAcceptOrder B ";
            //sql += "where A.ZFH=B.ZFH and A.JHH=B.JHH ";
            //sql += "and A.JHH='" + m_jhh + "' ";
            //sql = "select A.ZFH," + sql + "group by A.ZFH,B.KW ";
            //dtZf = SqlCe.ExecuteQuery(sql);

            dtZf = Storage.GetExpZF(Global.storage.jhh,Global.storage.make);
            curRow = -1;
            txtWcjs.Text = "";
            txtWcmz.Text = "";
            dgZf.DataSource = dtZf;
            dgZf.TableStyles.Clear();
            DataGridTableStyle zfStyle = new DataGridTableStyle();

            zfStyle.MappingName = dtZf.TableName;

            zfStyle.GridColumnStyles.Add(new ColumnStyle(0, "ZFH", "准发号", 90, ""));
            zfStyle.GridColumnStyles.Add(new ColumnStyle(1, "KW", "库位", 70, ""));
            zfStyle.GridColumnStyles.Add(new ColumnStyle(2, "GG", "规格", 170, ""));
            zfStyle.GridColumnStyles.Add(new ColumnStyle(3, "SYMZ", "毛重", 50, ""));
            zfStyle.GridColumnStyles.Add(new ColumnStyle(4, "SYJZ", "净重", 50, ""));
            zfStyle.GridColumnStyles.Add(new ColumnStyle(5, "SYJS", "件数", 50, ""));
            zfStyle.GridColumnStyles.Add(new ColumnStyle(6, "WCJS", "完件", 50, ""));
            zfStyle.GridColumnStyles.Add(new ColumnStyle(7, "PM", "品名", 80, ""));


            dgZf.TableStyles.Add(zfStyle);

        }

        private void frmOutPlanZF_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Global.storage.zfh = "";
                Global.frmCurrent = this.Owner;
                this.Owner.Show();
                this.Owner = null;
                this.Close();
            }
        }

        private void dgZf_CurrentCellChanged(object sender, EventArgs e)
        {
            curRow = dgZf.CurrentCell.RowNumber;
            txtWcjs.Text = dtZf.Rows[curRow]["WCJS"].ToString();
            txtWcmz.Text = dtZf.Rows[curRow]["WCMZ"].ToString();
            Global.storage.zfh = dtZf.Rows[curRow]["ZFH"].ToString();
            dgZf.Select(dgZf.CurrentCell.RowNumber);
        }

        private void dgZf_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (curRow >= 0)
                {
                    frmOutPlanCL frm = new frmOutPlanCL();
                    Global.frmCurrent = frm;
                    frm.Owner = this;
                    frm.Show();
                    this.Hide();
                }
            }
        }
    }
}