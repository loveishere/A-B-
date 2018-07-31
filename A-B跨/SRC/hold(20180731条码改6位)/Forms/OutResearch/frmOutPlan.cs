using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
//using hStore.Model.OutResearch;

namespace hStore.Forms.OutResearch
{
    public partial class frmOutPlan : Form
    {
        private DataTable dtPlan;
        private int curRow;
        //public string m_jhh;
        //public string m_jhdd;

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
            //sql += "from ExportStoragePlan where CKB='" + Global.sKb + "'";

            //dtPlan = SqlCe.ExecuteQuery(sql);

            dtPlan = Storage.GetExpPlan(Global.sKb,"");
            dgPlan.DataSource = dtPlan;
            txtWcjs.Text = "";
            txtWcmz.Text = "";

            dgPlan.TableStyles.Clear();

            DataGridTableStyle tlStyle = new DataGridTableStyle();
            tlStyle.MappingName = dtPlan.TableName;
            tlStyle.GridColumnStyles.Add(new ColumnStyle(0, "JHH", "提单号", 120, ""));
            tlStyle.GridColumnStyles.Add(new ColumnStyle(1, "JHDD", "到站", 60, ""));
            tlStyle.GridColumnStyles.Add(new ColumnStyle(2, "SYJS", "件数", 50, ""));
            tlStyle.GridColumnStyles.Add(new ColumnStyle(3, "SYMZ", "毛重", 50, ""));
            tlStyle.GridColumnStyles.Add(new ColumnStyle(4, "WCJS", "完件", 50, ""));
            tlStyle.GridColumnStyles.Add(new ColumnStyle(5, "WCMZ", "完毛", 50, ""));
            tlStyle.GridColumnStyles.Add(new ColumnStyle(6, "SHDW", "收货单位", 130, ""));

            curRow = -1;
            dgPlan.TableStyles.Add(tlStyle);

        }

        private void frmOutPlan_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Global.storage.jhh = "";
                Global.storage.make = "";
                Global.storage.jhdd = "";
                Global.frmCurrent = this.Owner;
                this.Owner.Show();
                this.Owner = null;
                this.Close();
            }
        }

        private void dgPlan_CurrentCellChanged(object sender, EventArgs e)
        {
            curRow = dgPlan.CurrentCell.RowNumber;
            Global.storage.make = dtPlan.Rows[curRow]["Make"].ToString();
            Global.storage.jhh = dtPlan.Rows[curRow]["JHH"].ToString();
            Global.storage.jhdd = dtPlan.Rows[curRow]["JHDD"].ToString();
            txtWcjs.Text = dtPlan.Rows[curRow]["WCJS"].ToString();
            txtWcmz.Text = dtPlan.Rows[curRow]["WCMZ"].ToString();
            dgPlan.Select(dgPlan.CurrentCell.RowNumber);
        }

        private void dgPlan_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (curRow >= 0)
                {
                    frmOutPlanZF frm = new frmOutPlanZF();
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
                    string smake = dtPlan.Rows[curRow]["Make"].ToString();
                    string sjhh = dtPlan.Rows[curRow]["JHH"].ToString();
                    frmMessage frmMessage = new frmMessage();
                    frmMessage.ShowDialog("出库计划" + sjhh + "是否确认删除？", "提示", "确认", "取消");
                    bool ret = frmMessage.ret;
                    frmMessage.Dispose();
                    if (ret)
                    {
                        Storage.DeleteExpPlan(sjhh,smake);
                        //string sql = "delete from ExportStorageAcceptOrder where JHH='" + sjhh + "'";
                        //SqlCe.ExecuteNonQuery(sql);
                        //sql = "delete from ExportStorageOrder where JHH='" + sjhh + "'";
                        //SqlCe.ExecuteNonQuery(sql);
                        //sql = "delete from ExportStoragePlan where JHH='" + sjhh + "'";
                        //SqlCe.ExecuteNonQuery(sql);
                        LoadData();
                    }
                }
            }
        }

        private void btnResearch_Click(object sender, EventArgs e)
        {
            if (txtJhh.Text != "")
            {
                //string sql = "select JHH,JHDD,SHDW,CKB,";
                //sql += "JS,WCJS,JS-(case isnull(WCJS) when 1 then 0 else WCJS end) SYJS, ";
                //sql += "JZ,WCJZ,JZ-(case isnull(WCJZ) when 1 then 0 else WCJZ end) SYJZ, ";
                //sql += "MZ,WCMZ,MZ-(case isnull(WCJZ) when 1 then 0 else WCMZ end) SYMZ ";
                //sql += "from ExportStoragePlan ";
                //sql += "where JHH='" + txtJhh.Text + "'";

                //dtPlan = SqlCe.ExecuteQuery(sql);

                dtPlan = Storage.GetExpPlan(Global.sKb, txtJhh.Text);
                dgPlan.DataSource = dtPlan;
                curRow = -1;
            }
        }
    }
}