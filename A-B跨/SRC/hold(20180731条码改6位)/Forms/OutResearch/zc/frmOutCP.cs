using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using PsionTeklogix.Keyboard;
using PsionTeklogix.Keyboard.NoChording;

namespace hStore.Forms.OutResearch.zc
{
    public partial class frmOutCP : Form
    {
        private DataTable dtCp;
        private int curRow;

        public frmOutCP()
        {
            InitializeComponent();
        }

        private void frmOutCP_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        public void LoadData()
        {
            dtCp = Storage.GetExpShip(Global.sKb);
            //dtCp = new DataTable();
            //dtCp.TableName = "ExportStoragePlan";
            //dtCp.Columns.Add("CPH");
            //dtCp.Columns.Add("CM");
            //dtCp.Columns.Add("SYJS");
            //dtCp.Columns.Add("WCJS");

            //dtCp.Rows.Add(new object[]{"aaaa","cccc",0,0});
            //dtCp.Rows.Add(new object[] {"bbbb","ffff",1,1 });

            curRow = -1;

            dgCp.DataSource = dtCp;
            dgCp.TableStyles.Clear();

            DataGridTableStyle cpStyle = new DataGridTableStyle();
            cpStyle.MappingName = dtCp.TableName;

            cpStyle.GridColumnStyles.Add(new ColumnStyle(0, "CPH", "船批号", 70, ""));
            cpStyle.GridColumnStyles.Add(new ColumnStyle(1, "CM", "船名", 100, ""));
            cpStyle.GridColumnStyles.Add(new ColumnStyle(2, "SYJS", "件数", 50, ""));
            cpStyle.GridColumnStyles.Add(new ColumnStyle(3, "WCJS", "完件", 50, ""));

            dgCp.TableStyles.Add(cpStyle);
        }

        private void frmOutCP_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Global.storage.cm = "";
                Global.storage.cph = "";
                Global.frmCurrent = this.Owner;
                this.Owner.Show();
                this.Owner = null;
                this.Close();
            }

        }

        private void dgCp_CurrentCellChanged(object sender, EventArgs e)
        {
            curRow = dgCp.CurrentCell.RowNumber;
            dgCp.Select(dgCp.CurrentCell.RowNumber);
        }

        private void dgCp_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (curRow >= 0)
                {
                    Global.storage.cm = dtCp.Rows[curRow]["CM"].ToString();
                    Global.storage.cph = dtCp.Rows[curRow]["CPH"].ToString();
                    frmOutPlan frm = new frmOutPlan();
                    Global.frmCurrent = frm;
                    frm.Owner = this;
                    frm.Show();
                    this.Hide();
                }
            }
        }

        private void frmOutCP_Paint(object sender, PaintEventArgs e)
        {
            if (!ScanCodeRemapping.NormalTable.Contains(0x003A))
            {
                ScanCodeRemapping.NormalTable.Add(0x003A, new RemappingTable.Remapping(VirtualKey.VK_F24, Function.SendCode));
            }
        }
    }
}