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
    public partial class frmOut : Form
    {
        private string _tranType;
        public frmOut()
        {
            InitializeComponent();
        }

        public void Show(string TranType)
        {
            _tranType = TranType;
            this.Show();
        }

        private void dataGrid1_CurrentCellChanged(object sender, EventArgs e)
        {
            dataGrid1.Select(dataGrid1.CurrentCell.RowNumber);
        }

        private void frmOut_Load(object sender, EventArgs e)
        {
            if (_tranType == "装船" || _tranType == "自提")
            {
                lblKb.Visible = false;
                txtKb.Visible = false;
                dataGrid1.Top = dataGrid1.Top - txtKb.Height;
                dataGrid1.Height = dataGrid1.Height + txtKb.Height;
            }

            if (_tranType == "装船")
            {
                System.Collections.ArrayList list = new System.Collections.ArrayList();
                list.Add(new outwarehousezc("I0090309A00","0003407", "A3250", 5670, "HOUSTON"));
                list.Add(new outwarehousezc("I1150307A00","0006841", "A3250", 1970, "HOUSTON"));
                list.Add(new outwarehousezc("I9870307C00","0004901","A3250", 112, "HOUSTON"));
                dataGrid1.DataSource = list;
                dataGrid1.TableStyles.Clear();

                DataGridTableStyle ts1 = new DataGridTableStyle();
                ts1.MappingName = "ArrayList";

                ts1.GridColumnStyles.Add(new ColumnStyle(0, "clh", "材料号", 120, ""));
                ts1.GridColumnStyles.Add(new ColumnStyle(1, "kw", "库位", 70, ""));
                ts1.GridColumnStyles.Add(new ColumnStyle(2, "gd", "关单", 70, ""));
                ts1.GridColumnStyles.Add(new ColumnStyle(3, "mz", "毛重", 70, ""));
                ts1.GridColumnStyles.Add(new ColumnStyle(4, "dd", "到港", 70, ""));


                dataGrid1.TableStyles.Add(ts1);
                dataGrid1.Select(0);
            }

            if (_tranType == "铁路")
            {
                lblKb.Text = "到站";
                txtKb.Text = "中牟站";
                lblkjh.Text = "组批号";
                System.Collections.ArrayList list = new System.Collections.ArrayList();
                list.Add(new outwarehousetl("I0090309A00",  5670, "1234567"));
                list.Add(new outwarehousetl("I1150307A00",  1970, "1234567"));
                list.Add(new outwarehousetl("I9870307C00",  112, "1234567"));
                dataGrid1.DataSource = list;
                dataGrid1.TableStyles.Clear();

                DataGridTableStyle ts1 = new DataGridTableStyle();
                ts1.MappingName = "ArrayList";

                ts1.GridColumnStyles.Add(new ColumnStyle(0, "clh", "材料号", 120, ""));
                ts1.GridColumnStyles.Add(new ColumnStyle(1, "mz", "毛重", 70, ""));
                ts1.GridColumnStyles.Add(new ColumnStyle(2, "zph", "组批号", 70, ""));


                dataGrid1.TableStyles.Add(ts1);
                dataGrid1.Select(0);
            }

            if (_tranType == "转库")
            {
                System.Collections.ArrayList list = new System.Collections.ArrayList();
                list.Add(new outwarehousezk("I0090309A00", "004", 5670));
                list.Add(new outwarehousezk("I1150307A00", "004", 1970));
                list.Add(new outwarehousezk("I9870307C00", "005", 112));
                dataGrid1.DataSource = list;
                dataGrid1.TableStyles.Clear();

                DataGridTableStyle ts1 = new DataGridTableStyle();
                ts1.MappingName = "ArrayList";

                ts1.GridColumnStyles.Add(new ColumnStyle(0, "clh", "材料号", 120, ""));
                ts1.GridColumnStyles.Add(new ColumnStyle(1, "dd", "库位", 70, ""));
                ts1.GridColumnStyles.Add(new ColumnStyle(2, "mz", "毛重", 70, ""));


                dataGrid1.TableStyles.Add(ts1);
                dataGrid1.Select(0);
            }

            if (_tranType == "自提")
            {
                lblkjh.Text = "自提车号";
                System.Collections.ArrayList list = new System.Collections.ArrayList();
                list.Add(new outwarehousezt("I0090309A00", 5670,"002"));
                list.Add(new outwarehousezt("I1150307A00", 1970,"002"));
                list.Add(new outwarehousezt("I9870307C00", 112,"003"));
                dataGrid1.DataSource = list;
                dataGrid1.TableStyles.Clear();

                DataGridTableStyle ts1 = new DataGridTableStyle();
                ts1.MappingName = "ArrayList";

                ts1.GridColumnStyles.Add(new ColumnStyle(0, "clh", "材料号", 120, ""));
                ts1.GridColumnStyles.Add(new ColumnStyle(1, "mz", "毛重", 70, ""));
                ts1.GridColumnStyles.Add(new ColumnStyle(2, "kw", "库位", 70, ""));


                dataGrid1.TableStyles.Add(ts1);
                dataGrid1.Select(0);
            }

        }

        private void frmOut_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                App.LinkedListForm.RemoveLast();
                this.Close();
            }
        }
    }
}