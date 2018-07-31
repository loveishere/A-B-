using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using hStore.Model.OutResearch;

namespace hStore.Forms.OutResearch
{
    public partial class frmOutCP : Form
    {
        public frmOutCP()
        {
            InitializeComponent();
        }

        private void dataGrid1_CurrentCellChanged(object sender, EventArgs e)
        {
            dataGrid1.Select(dataGrid1.CurrentCell.RowNumber);
        }

        private void frmOutCP_Load(object sender, EventArgs e)
        {
            System.Collections.ArrayList list = new System.Collections.ArrayList();
            list.Add(new shipselect("A8030712", "新宏泰",1200,1150));
            list.Add(new shipselect("A8030611", "CHING HO",2500,null));
            list.Add(new shipselect("A8030611", "CHING HO2", 2500, null));
            dataGrid1.DataSource = list;
            dataGrid1.TableStyles.Clear();

            DataGridTableStyle ts1 = new DataGridTableStyle();
            ts1.MappingName = "ArrayList";
            dataGrid1.SelectionBackColor = System.Drawing.Color.Brown;
            dataGrid1.SelectionForeColor = System.Drawing.Color.Yellow;
            dataGrid1.ForeColor = System.Drawing.Color.Coral;
            dataGrid1.BackColor = System.Drawing.Color.Cyan;

            ts1.GridColumnStyles.Add(new ColumnStyle(0, "cph", "船批号", 80, ""));
            ts1.GridColumnStyles.Add(new ColumnStyle(1, "cm", "船名", 70, ""));
            ts1.GridColumnStyles.Add(new ColumnStyle(2, "zjs", "总件数", 80, ""));
            ts1.GridColumnStyles.Add(new ColumnStyle(3, "wcjs", "完成", 80, ""));

            dataGrid1.TableStyles.Add(ts1);
            dataGrid1.Select(0);
        }

        private void frmOutCP_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                App.LinkedListForm.RemoveLast();
                this.Close();
            }
        }

        private void btnQd_Click(object sender, EventArgs e)
        {
            frmOutPlan frm = new frmOutPlan();
            frm.Show("装船");
            App.LinkedListForm.AddLast(frm);
        }
    }
}