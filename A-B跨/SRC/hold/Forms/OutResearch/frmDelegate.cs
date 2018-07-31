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
    public partial class frmDelegate : Form
    {
        private DataTable dtPlan;
        private int curRow;
        //public string m_jhh;
        //public string m_jhdd;

        public frmDelegate()
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

            dtPlan = Storage.GetWtd(Global.sKb);
            dgPlan.DataSource = dtPlan;

            dgPlan.TableStyles.Clear();

            DataGridTableStyle tlStyle = new DataGridTableStyle();
            tlStyle.MappingName = dtPlan.TableName;
            tlStyle.GridColumnStyles.Add(new ColumnStyle(0, "WTDH", "委托单", 120, ""));
            tlStyle.GridColumnStyles.Add(new ColumnStyle(1, "HPMC", "货名", 60, ""));
            tlStyle.GridColumnStyles.Add(new ColumnStyle(2, "ZJS", "件数", 50, ""));
            tlStyle.GridColumnStyles.Add(new ColumnStyle(3, "ZZL", "重量", 50, ""));

            curRow = -1;
            dgPlan.TableStyles.Add(tlStyle);

        }

        private void frmOutPlan_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Global.frmCurrent = this.Owner;
                this.Owner.Show();
                this.Owner = null;
                this.Close();
            }
        }

        private void dgPlan_CurrentCellChanged(object sender, EventArgs e)
        {
            curRow = dgPlan.CurrentCell.RowNumber;
            Global.storage.jhh = dtPlan.Rows[curRow]["JHH"].ToString();
            dgPlan.Select(dgPlan.CurrentCell.RowNumber);
        }

        private void dgPlan_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                doOper();
            }
        }

        private void doOper()
        {
            if (curRow >= 0)
            {
                string sjfs = dtPlan.Rows[curRow]["SJFS"].ToString();
                string wtdh = dtPlan.Rows[curRow]["WTDH"].ToString();
                string pm = dtPlan.Rows[curRow]["HPMC"].ToString();
                bool crflag = Convert.ToBoolean(dtPlan.Rows[curRow]["CRFLAG"]);
                Global.storage.jhh = wtdh;
                Global.storage.sjfs = sjfs;
                Global.storage.pm = pm;
                if (crflag)//入库
                {
                    frmWtdIn frm = new frmWtdIn();
                    Global.frmCurrent = frm;
                    frm.Owner = this;
                    frm.Show();
                    this.Hide();
                }
                else//出库
                {
                    frmWtdOut frm = new frmWtdOut();
                    Global.frmCurrent = frm;
                    frm.Owner = this;
                    frm.Show();
                    this.Hide();
                }

            }
        }

    }
}