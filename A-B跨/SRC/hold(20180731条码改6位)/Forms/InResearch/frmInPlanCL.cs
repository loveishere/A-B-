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
    public partial class frmInPlanCL : Form
    {
        private DataTable dtCl;
        private int curRow;

        public frmInPlanCL()
        {
            InitializeComponent();
        }

        private void frmInPlanCL_Load(object sender, EventArgs e)
        {
            if (ScanCodeRemapping.NormalTable.Contains(0x003A))
            {
                ScanCodeRemapping.NormalTable.Remove(0x003A);
            }

            this.txtInfo.Text = "计划号："+ Global.storage.jhh +"\r\n";
            this.txtInfo.Text += "准发号："+ Global.storage.zfh +"\r\n";
            this.txtInfo.Text += "品名："+ Global.storage.pm +"\r\n";
            this.txtInfo.Text += "合同号："+Global.storage.hth;

            LoadData();

            dgCl.TableStyles.Clear();

            DataGridTableStyle clStyle = new DataGridTableStyle();
            clStyle.MappingName = dtCl.TableName;
            CheckCellEventHandler cellEvent = new CheckCellEventHandler(CheckCellColor);

            clStyle.GridColumnStyles.Add(new ColumnStyle(0, "DJ", "等级", 40, "", cellEvent));
            clStyle.GridColumnStyles.Add(new ColumnStyle(1, "CLH", "材料号", 100, "", cellEvent));
            //clStyle.GridColumnStyles.Add(new ColumnStyle(2, "KW", "库位", 50, "", cellEvent));
            clStyle.GridColumnStyles.Add(new ColumnStyle(2, "JZ", "净重", 50, "", cellEvent));
            clStyle.GridColumnStyles.Add(new ColumnStyle(3, "MZ", "毛重", 50, "", cellEvent));
            clStyle.GridColumnStyles.Add(new ColumnStyle(4, "GG", "规格", 150, "", cellEvent));


            dgCl.TableStyles.Add(clStyle);
            if(dtCl.Rows.Count>0) dgCl.Select(0);
        }

        public void LoadData()
        {
            dtCl = Storage.GetImpCL(Global.sKb, Global.storage.jhh,Global.storage.make, Global.storage.zfh);
            curRow = -1;
            dgCl.DataSource = dtCl;
        }

        public void CheckCellColor(object sender, DataGridEnableEventArgs e)
        {
            if (dgCl.CurrentCell.RowNumber == e.Row && curRow != -1)
                e.MeetsCriteria = false;
            else
            {
                //if (dtCl.Rows[e.Row]["WCFlag"].ToString() == "" || dtCl.Rows[e.Row]["WCFlag"].ToString()=="0")
                //{
                //    e.MeetsCriteria = true;
                //    e.BackColor = Color.Cyan;
                //    if (dtCl.Rows[e.Row]["ExFlag"].ToString() == "1")
                //    {
                //        e.ForeColor = Color.Brown;
                //    }
                //    else
                //    {
                //        e.ForeColor = SystemColors.WindowText;
                //    }
                //}
                //else if (dtCl.Rows[e.Row]["WCFlag"].ToString() == "1")
                //{
                //    e.MeetsCriteria = true;
                //    if (dtCl.Rows[e.Row]["QA"].ToString() != "" && dtCl.Rows[e.Row]["QA"].ToString() != "0")
                //    {
                //        e.BackColor = Color.BlueViolet;
                //    }
                //    else
                //    {
                //        e.BackColor = Color.Salmon;
                //    }
                    
                //    if (dtCl.Rows[e.Row]["ExFlag"].ToString() == "1")
                //    {
                //        e.ForeColor = Color.Brown;
                //    }
                //    else
                //    {
                //        e.ForeColor = SystemColors.WindowText;
                //    }
                //}
                //else
                //{
                    e.MeetsCriteria = true;
                    e.BackColor = SystemColors.Window;
                    if (dtCl.Rows[e.Row]["ExFlag"].ToString() == "1")
                    {
                        e.ForeColor = Color.Brown;
                    }
                    else
                    {
                        e.ForeColor = SystemColors.WindowText;
                    }
                //}
            }
        }

        private void frmInPlanCL_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Global.frmCurrent = this.Owner;
                this.Owner.Show();
                this.Owner = null;
                Business.InvokeMethod(Global.frmCurrent, "LoadData", new object[] { });
                this.Close();
            }
        }

        private void dgCl_CurrentCellChanged(object sender, EventArgs e)
        {
            curRow = dgCl.CurrentCell.RowNumber;
            dgCl.Select(dgCl.CurrentCell.RowNumber);
        }

        private void frmInPlanCL_Closed(object sender, EventArgs e)
        {
        }

        private void dgCl_KeyUp(object sender, KeyEventArgs e)
        {
        }
    }
}