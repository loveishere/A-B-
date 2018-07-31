using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace hStore.Forms.OutResearch
{
    public partial class frmOutPlanCL : Form
    {
        private DataTable dtCl;
        //private string m_zfh;
        //private string m_jhh;
        private int curRow;

        public frmOutPlanCL()
        {
            InitializeComponent();
        }

        private void frmOutPlanCL_Load(object sender, EventArgs e)
        {

            //frmOutPlanZF frmOutPlanZF = (frmOutPlanZF)Global.frmCurrent;
            //m_zfh = frmOutPlanZF.m_zfh;
            //m_jhh = frmOutPlanZF.m_jhh;
            lblInfo.Text = "计划号："+ Global.storage.jhh +"\r\n";
            lblInfo.Text += "准发号：" + Global.storage.zfh + "\r\n";


            //string sql = "select ZFH,CLH,MZ,JZ,substring(KW,4,8) KW,";
            //sql += "Height+'×'+Width+'×'+Long GG,";
            //sql += "WCFlag,SCANTIME from ";
            //sql += "ExportStorageAcceptOrder where (WCFlag is null or WCFlag<>2) ";


            //sql += "and ZFH='" + m_zfh + "' and JHH='"+ m_jhh +"' ";
            //dtCl = SqlCe.ExecuteQuery(sql);

            dtCl = Storage.GetExpCL(Global.storage.jhh,Global.storage.make, Global.storage.zfh);
            curRow = -1;


            dgCl.DataSource = dtCl;
            dgCl.TableStyles.Clear();

            DataGridTableStyle clStyle = new DataGridTableStyle();
            clStyle.MappingName = dtCl.TableName;
            CheckCellEventHandler cellEvent = new CheckCellEventHandler(CheckCellColor);

            clStyle.GridColumnStyles.Add(new ColumnStyle(0, "CLH", "材料号", 110, "", cellEvent));
            //clStyle.GridColumnStyles.Add(new ColumnStyle(1, "KW", "库位", 70, "", cellEvent));
            clStyle.GridColumnStyles.Add(new ColumnStyle(1, "MZ", "毛重", 50, "", cellEvent));
            clStyle.GridColumnStyles.Add(new ColumnStyle(2, "JZ", "净重", 50, "", cellEvent));
            clStyle.GridColumnStyles.Add(new ColumnStyle(3, "GG", "规格", 170, "", cellEvent));


            dgCl.TableStyles.Add(clStyle);

        }

        public void CheckCellColor(object sender, DataGridEnableEventArgs e)
        {
            if (dgCl.CurrentCell.RowNumber == e.Row && curRow != -1)
                e.MeetsCriteria = false;
            else
            {
                //if ((dtCl.Rows[e.Row]["WCFlag"].ToString() == "0" || dtCl.Rows[e.Row]["WCFlag"].ToString() == "") && dtCl.Rows[e.Row]["KW"].ToString() != "")
                //{
                //    e.MeetsCriteria = true;
                //    e.BackColor = Color.Cyan;
                //    e.ForeColor = SystemColors.WindowText;
                //}
                //else if (dtCl.Rows[e.Row]["WCFlag"].ToString() == "1")
                //{
                //    e.MeetsCriteria = true;
                //    e.BackColor = Color.Salmon;
                //    e.ForeColor = SystemColors.WindowText;
                //}
                //else
                //{
                    e.MeetsCriteria = true;
                    e.BackColor = SystemColors.Window;
                    e.ForeColor = SystemColors.WindowText;
                //}
            }
        }

        private void frmOutPlanCL_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Global.frmCurrent = this.Owner;
                this.Owner.Show();
                this.Owner = null;
                this.Close();
            }
        }

        private void dgCl_CurrentCellChanged(object sender, EventArgs e)
        {
            curRow = dgCl.CurrentCell.RowNumber;
            dgCl.Select(dgCl.CurrentCell.RowNumber);
        }
    }
}