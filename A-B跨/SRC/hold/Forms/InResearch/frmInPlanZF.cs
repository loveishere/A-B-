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
    public partial class frmInPlanZF : Form
    {

        private DataTable dtZf;
        //public string jhh;
        //public string zfh;
        //public string hth;
        //public string pm;
        private int curRow;

        public frmInPlanZF()
        {
            InitializeComponent();
        }

        private void frmInPlanZF_Load(object sender, EventArgs e)
        {
            //frmInPlan frmInPlan=(frmInPlan)Global.frmCurrent;
            //jhh = frmInPlan.jhh;
            txtInfo.Text = "计划号："+Global.storage.jhh;

            LoadData();

            dgZf.TableStyles.Clear();

            DataGridTableStyle zfStyle = new DataGridTableStyle();
            zfStyle.MappingName = dtZf.TableName;

            zfStyle.GridColumnStyles.Add(new ColumnStyle(0, "DJ", "等级", 40, ""));
            zfStyle.GridColumnStyles.Add(new ColumnStyle(1, "ZFH", "准发号", 100, ""));
            //zfStyle.GridColumnStyles.Add(new ColumnStyle(2, "KW", "库位", 90, ""));
            zfStyle.GridColumnStyles.Add(new ColumnStyle(2, "SYJS", "件数", 50, ""));
            zfStyle.GridColumnStyles.Add(new ColumnStyle(3, "SYMZ", "毛重", 60, ""));
            zfStyle.GridColumnStyles.Add(new ColumnStyle(4, "SYJZ", "净重", 60, ""));
            zfStyle.GridColumnStyles.Add(new ColumnStyle(5, "GG", "规格", 160, ""));
            zfStyle.GridColumnStyles.Add(new ColumnStyle(6, "WCJS", "完件", 50, ""));
            zfStyle.GridColumnStyles.Add(new ColumnStyle(7, "WCMZ", "完毛", 60, ""));
            zfStyle.GridColumnStyles.Add(new ColumnStyle(8, "WCJZ", "完净", 60, ""));
            zfStyle.GridColumnStyles.Add(new ColumnStyle(9, "PM", "品名", 90, ""));
            zfStyle.GridColumnStyles.Add(new ColumnStyle(10, "HTH", "合同号", 80, ""));


            dgZf.TableStyles.Add(zfStyle);
            
        }

        public void LoadData()
        {
            //string sql = "select A.ZFH,B.KW,sum(case B.WCFlag when 2 then 0 else 1 end) SYJS, sum(case B.WCFlag when 2 then 1 else 0 end) WCJS, count(*) JS, ";
            //sql += "sum(case B.WCFlag when 2 then 0 else B.MZ end) SYMZ, sum(case B.WCFlag when 2 then B.MZ else 0 end) WCMZ, sum(B.MZ) MZ,";
            //sql += "sum(case B.WCFlag when 2 then 0 else B.JZ end) SYJZ, sum(case B.WCFlag when 2 then B.JZ else 0 end) WCJZ, sum(B.MZ) JZ,";
            //sql += "max(A.HTH) HTH,max(A.PM) PM ";
            //sql += "from ImportStorageOrder A,ImportStorageAcceptOrder B ";
            //sql += "where A.ZFH=B.ZFH and A.JHH=B.JHH ";
            //sql += "and A.JHH='" + jhh + "' ";
            //sql += "group by A.ZFH,B.KW ";

            //dtZf = SqlCe.ExecuteQuery(sql);
            dtZf = Storage.GetImpZF(Global.storage.jhh,Global.storage.make);
            dgZf.DataSource = dtZf;
            curRow = -1;
        }

        private void frmInPlanZF_KeyUp(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Escape)
            {
                Global.storage.zfh = "";
                Global.storage.hth = "";
                Global.storage.pm = "";
                Global.frmCurrent = this.Owner;
                this.Owner.Show();
                this.Owner = null;
                Business.InvokeMethod(Global.frmCurrent, "LoadData", new object[] { });
                this.Close();
            }
        }

        private void dgZf_CurrentCellChanged(object sender, EventArgs e)
        {
            curRow = dgZf.CurrentCell.RowNumber;
            dgZf.Select(dgZf.CurrentCell.RowNumber);
        }

        private void dgZf_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (curRow >= 0)
                {
                    Global.storage.zfh = dtZf.Rows[curRow]["ZFH"].ToString();
                    Global.storage.hth = dtZf.Rows[curRow]["HTH"].ToString();
                    Global.storage.pm = dtZf.Rows[curRow]["PM"].ToString();
                    frmInPlanCL frm = new frmInPlanCL();
                    Global.frmCurrent = frm;
                    frm.Owner = this;
                    frm.Show();
                    this.Hide();
                }
            }
        }

        private void frmInPlanZF_Paint(object sender, PaintEventArgs e)
        {
            if (!ScanCodeRemapping.NormalTable.Contains(0x003A))
            {
                ScanCodeRemapping.NormalTable.Add(0x003A, new RemappingTable.Remapping(VirtualKey.VK_F24, Function.SendCode));
            }
        }
    }
}