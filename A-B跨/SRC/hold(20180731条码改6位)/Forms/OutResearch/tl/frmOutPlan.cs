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
    public partial class frmOutPlan : Form
    {
        private DataTable dtTl;
        private int curRow;
        //public string m_jhh;
        //public string m_dz;

        public frmOutPlan()
        {
            InitializeComponent();
        }

        private void frmOutPlan_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        public void LoadData()
        {
            //string sql = "select JHH,JHDD,SHDW,";
            //sql += "JS,WCJS,JS-(case isnull(WCJS) when 1 then 0 else WCJS end) SYJS, ";
            //sql += "JZ,WCJZ,JZ-(case isnull(WCJZ) when 1 then 0 else WCJZ end) SYJZ, ";
            //sql += "MZ,WCMZ,MZ-(case isnull(WCJZ) when 1 then 0 else WCMZ end) SYMZ ";
            //sql += "from ExportStoragePlan where CKB='"+ Global.sKb +"' and JHH like '2%' ";

            //dtTl = SqlCe.ExecuteQuery(sql);
            dtTl = Storage.GetExpPlanF(Global.sKb, "", "tl");
            dgTl.DataSource = dtTl;
            txtDdjs.Text = "";
            txtDdmz.Text = "";
            txtWcjs.Text = "";
            txtWcmz.Text = "";
            
            dgTl.TableStyles.Clear();

            DataGridTableStyle tlStyle = new DataGridTableStyle();
            tlStyle.MappingName = dtTl.TableName;
            tlStyle.GridColumnStyles.Add(new ColumnStyle(0, "JHH", "提单号", 120, ""));
            tlStyle.GridColumnStyles.Add(new ColumnStyle(1, "JHDD", "到站", 60, ""));
            tlStyle.GridColumnStyles.Add(new ColumnStyle(2, "SYJS", "件数", 50, ""));
            tlStyle.GridColumnStyles.Add(new ColumnStyle(3, "SYMZ", "毛重", 50, ""));
            tlStyle.GridColumnStyles.Add(new ColumnStyle(4, "WCJS", "完件", 50, ""));
            tlStyle.GridColumnStyles.Add(new ColumnStyle(5, "WCMZ", "完毛", 50, ""));
            tlStyle.GridColumnStyles.Add(new ColumnStyle(6, "SHDW", "收货单位", 130, ""));

            curRow = -1;
            dgTl.TableStyles.Add(tlStyle);

        }

        private void frmOutPlan_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Global.storage.make = "";
                Global.storage.jhh = "";
                Global.storage.jhdd = "";
                Global.frmCurrent = this.Owner;
                this.Owner.Show();
                this.Owner = null;
                this.Close();
            }
            //else if (e.KeyCode == Keys.F2)
            //{
            //    frmZPAll frm = new frmZPAll();
            //    frm.Show();
            //    App.LinkedListForm.AddLast(frm);
            //}
        }

        private void dgTl_CurrentCellChanged(object sender, EventArgs e)
        {
            curRow = dgTl.CurrentCell.RowNumber;
            txtWcjs.Text = dtTl.Rows[curRow]["WCJS"].ToString();
            txtWcmz.Text = dtTl.Rows[curRow]["WCMZ"].ToString();

            string sql="select count(*) JS,sum(MZ) MZ from ExportStorageAcceptOrder ";
            sql += "where JHH='"+ dtTl.Rows[curRow]["JHH"].ToString() +"' and KW<>'' and (WCFlag is Null or WCFlag<>2)";
            DataTable dt = SqlCe.ExecuteQuery(sql);
            if (dt.Rows.Count > 0)
            {
                txtDdjs.Text = dt.Rows[0]["JS"].ToString();
                txtDdmz.Text = dt.Rows[0]["MZ"].ToString();
            }
            dt.Dispose();
            dgTl.Select(dgTl.CurrentCell.RowNumber);
        }

        private void dgTl_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (curRow >= 0)
                {
                    Global.storage.make = dtTl.Rows[curRow]["Make"].ToString();
                    Global.storage.jhh = dtTl.Rows[curRow]["JHH"].ToString();
                    Global.storage.jhdd = dtTl.Rows[curRow]["JHDD"].ToString();
                    hStore.Forms.OutResearch.zc.frmOutPlanZF frm = new hStore.Forms.OutResearch.zc.frmOutPlanZF();
                    Global.frmCurrent = frm;
                    frm.Owner = this;
                    frm.Show();
                    this.Hide();
                }
            }
            //if (e.KeyCode == Keys.F5)
            //{
            //    if (curRow >= 0)
            //    {
            //        string smake = dtTl.Rows[curRow]["Make"].ToString();
            //        string sjhh = dtTl.Rows[curRow]["JHH"].ToString();
            //        frmMessage frmMessage = new frmMessage();
            //        frmMessage.ShowDialog("出库计划" + sjhh + "是否确认删除？", "提示", "确认", "取消");
            //        bool ret = frmMessage.ret;
            //        frmMessage.Dispose();
            //        if (ret)
            //        {

            //            Storage.DeleteExpPlan(sjhh,smake);
            //            LoadData();
            //        }
            //    }
            //}

        }

        private void frmOutPlan_Paint(object sender, PaintEventArgs e)
        {
            if (!ScanCodeRemapping.NormalTable.Contains(0x003A))
            {
                ScanCodeRemapping.NormalTable.Add(0x003A, new RemappingTable.Remapping(VirtualKey.VK_F24, Function.SendCode));
            }
        }
    }
}