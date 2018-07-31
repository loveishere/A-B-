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
    public partial class frmInPlan : Form
    {

        private DataTable dtPlan;
        private int curRow;

        public frmInPlan()
        {
            InitializeComponent();
        }

        private void frmInPlan_Load(object sender, EventArgs e)
        {

            LoadData();

            dgPlan.TableStyles.Clear();

            DataGridTableStyle planStyle = new DataGridTableStyle();
            planStyle.MappingName = dtPlan.TableName;

            planStyle.GridColumnStyles.Add(new ColumnStyle(0, "DJ", "等级", 40, ""));
            planStyle.GridColumnStyles.Add(new ColumnStyle(1, "JHH", "计划号", 110, ""));
            planStyle.GridColumnStyles.Add(new ColumnStyle(2, "SYJS", "件数", 75, ""));
            planStyle.GridColumnStyles.Add(new ColumnStyle(3, "SYJZ", "净重", 65, ""));
            planStyle.GridColumnStyles.Add(new ColumnStyle(4, "SYMZ", "毛重", 65, ""));
            planStyle.GridColumnStyles.Add(new ColumnStyle(5, "WCJS", "完件", 75, ""));
            planStyle.GridColumnStyles.Add(new ColumnStyle(6, "WCJZ", "完净", 75, ""));
            planStyle.GridColumnStyles.Add(new ColumnStyle(7, "WCMZ", "完毛", 75, ""));


            dgPlan.TableStyles.Add(planStyle);

        }

        private void sumPlan()
        {
            int zjs = 0;
            int wcjs = 0;

            for (int i = 0; i < dtPlan.Rows.Count; i++)
            {
                if (dtPlan.Rows[i]["JS"].ToString() != "")
                {
                    zjs += Convert.ToInt32(dtPlan.Rows[i]["JS"]);
                }
                if (dtPlan.Rows[i]["WCJS"].ToString() != "")
                {
                    wcjs += Convert.ToInt32(dtPlan.Rows[i]["WCJS"]);
                }
            }

            txtZjs.Text = zjs.ToString();
            txtWc.Text = wcjs.ToString();
        }

        private void frmInPlan_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Global.storage.make = "";
                Global.storage.jhh = "";
                Global.frmCurrent = this.Owner;
                Storage.ClearZF2KW();
                this.Owner.Show();
                this.Owner = null;
                this.Close();
            }
        }

        private void dgPlan_CurrentCellChanged(object sender, EventArgs e)
        {
            curRow = dgPlan.CurrentCell.RowNumber;
            dgPlan.Select(dgPlan.CurrentCell.RowNumber);
        }

        private void btnResearch_Click(object sender, EventArgs e)
        {
            LoadData();
        }


        public void LoadData()
        {
            dtPlan = Storage.GetImpPlan(Global.sKb, txtJhh.Text);
            dgPlan.DataSource = dtPlan;
            sumPlan();
            curRow = -1;
        }


        private void dgPlan_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (curRow >= 0)
                {
                    Global.storage.make = dtPlan.Rows[curRow]["Make"].ToString();
                    Global.storage.jhh = dtPlan.Rows[curRow]["JHH"].ToString();
                    frmInPlanZF frm = new frmInPlanZF();
                    Global.frmCurrent = frm;
                    frm.Owner = this;
                    frm.Show();
                    this.Hide();
                }
            }

            if (e.KeyCode == Keys.F5)
            {
                if (curRow >= 0)
                {
                    string sjhh = dtPlan.Rows[curRow]["JHH"].ToString();
                    string sMake = dtPlan.Rows[curRow]["Make"].ToString();
                    frmMessage frmMessage = new frmMessage();
                    frmMessage.ShowDialog("入库计划" + sjhh + "确认删除？", "提示", "确认", "取消");
                    bool ret = frmMessage.ret;
                    frmMessage.Dispose();
                    if (ret)
                    {
                        Storage.DeleteImpPlan(sjhh,sMake);
                        LoadData();
                    }
                }
            }
        }

        private void frmInPlan_Paint(object sender, PaintEventArgs e)
        {
            if (!ScanCodeRemapping.NormalTable.Contains(0x003A))
            {
                ScanCodeRemapping.NormalTable.Add(0x003A, new RemappingTable.Remapping(VirtualKey.VK_F24, Function.SendCode));
            }
        }
    }
}