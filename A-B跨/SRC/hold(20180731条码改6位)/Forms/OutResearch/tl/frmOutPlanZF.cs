using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace hStore.Forms.OutResearch.tl
{
    public partial class frmOutPlanZF : Form
    {
        private DataTable dtZf;
        private int curRow;

        public frmOutPlanZF()
        {
            InitializeComponent();
        }


        private void frmOutPlanZF_Load(object sender, EventArgs e)
        {

            cboCLType.SelectedIndex = 0;
            Global.storage.cltype = 0;

            lblInfo.Text = "提单号："+ Global.storage.jhh;

        }

        private void frmOutPlanZF_KeyUp(object sender, KeyEventArgs e)
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
                if (curRow >= 0)
                {
                    frmOut frm = new frmOut();
                    Global.frmCurrent = frm;
                    frm.Owner = this;
                    frm.Show();
                    this.Hide();
                }
            }
        }

        private void dgZf_CurrentCellChanged(object sender, EventArgs e)
        {
            curRow = dgZf.CurrentCell.RowNumber;
            Global.storage.cltype = cboCLType.SelectedIndex;
            
            Global.storage.pm = dtZf.Rows[curRow]["PM"].ToString();
            txtWcjs.Text = dtZf.Rows[curRow]["WCJS"].ToString();
            txtWcmz.Text = dtZf.Rows[curRow]["WCMZ"].ToString();
            string kw = dtZf.Rows[curRow]["KW"].ToString();
            string value = "";

            switch (Global.storage.cltype)
            {
                case 0:
                    Global.storage.zfh = dtZf.Rows[curRow]["ZFH"].ToString();
                    value = Global.storage.zfh;
                    break;
                case 1:
                    Global.storage.lh = dtZf.Rows[curRow]["LH"].ToString();
                    value = Global.storage.lh;
                    break;
                case 2:
                    Global.storage.hth = dtZf.Rows[curRow]["HTH"].ToString();
                    value = Global.storage.hth;
                    break;
                case 3:
                    Global.storage.zph = dtZf.Rows[curRow]["ZPH"].ToString();
                    value = Global.storage.zph;
                    break;
                default:
                    break;
            }

            DataTable dt = Storage.GetExpZF(Global.storage.jhh, Global.storage.cltype, value, Global.sKb + kw);
            if (dt.Rows.Count > 0)
            {
                txtDdjs.Text = dt.Rows[0]["JS"].ToString();
                txtDdmz.Text = dt.Rows[0]["MZ"].ToString();
            }
            else
            {
                txtDdjs.Text = "";
                txtDdmz.Text = "";
            }
            dt.Dispose();

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

        public void LoadData()
        {
            Global.storage.cltype = cboCLType.SelectedIndex;
            DataGridTableStyle zfStyle = new DataGridTableStyle();

            switch (Global.storage.cltype)
            {
                case 0:
                    zfStyle.GridColumnStyles.Add(new ColumnStyle(0, "ZFH", "准发号", 90, ""));
                    break;
                case 1:
                    zfStyle.GridColumnStyles.Add(new ColumnStyle(0, "LH", "炉号", 60, ""));
                    break;
                case 2:
                    zfStyle.GridColumnStyles.Add(new ColumnStyle(0, "HTH", "合同号", 90, ""));
                    break;
                case 3:
                    zfStyle.GridColumnStyles.Add(new ColumnStyle(0, "ZPH", "扎批号", 60, ""));
                    break;
                default:
                    break;
            }

            dtZf = Storage.GetExpZF(Global.storage.jhh, Global.storage.cltype);
            curRow = -1;
            txtDdjs.Text = "";
            txtDdmz.Text = "";
            txtWcjs.Text = "";
            txtWcmz.Text = "";
            dgZf.DataSource = dtZf;
            dgZf.TableStyles.Clear();

            zfStyle.MappingName = dtZf.TableName;
            

            zfStyle.GridColumnStyles.Add(new ColumnStyle(1, "KW", "库位", 70, ""));
            zfStyle.GridColumnStyles.Add(new ColumnStyle(2, "GG", "规格", 170, ""));
            zfStyle.GridColumnStyles.Add(new ColumnStyle(3, "SYMZ", "毛重", 50, ""));
            zfStyle.GridColumnStyles.Add(new ColumnStyle(4, "SYJZ", "净重", 50, ""));
            zfStyle.GridColumnStyles.Add(new ColumnStyle(5, "SYJS", "件数", 50, ""));
            zfStyle.GridColumnStyles.Add(new ColumnStyle(6, "WCJS", "完件", 50, ""));
            zfStyle.GridColumnStyles.Add(new ColumnStyle(7, "PM", "品名", 80, ""));


            dgZf.TableStyles.Add(zfStyle);
        }

        private void cboCLType_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}