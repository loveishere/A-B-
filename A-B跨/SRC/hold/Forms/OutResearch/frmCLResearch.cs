using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace hStore.Forms.OutResearch
{
    public partial class frmCLResearch : Form
    {
        public frmCLResearch()
        {
            InitializeComponent();
        }

        private void frmCLResearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Global.frmCurrent = this.Owner;
                this.Owner.Show();
                this.Owner = null;
                this.Close();
            }
        }

        private void btnResearch_Click(object sender, EventArgs e)
        {
            if (txtClh.Text == "") return;

            string sql = "select A.JHH,B.ZFH,B.PM,B.JZ,B.MZ,B.Height + '×' + B.Width + '×' + B.Long GG,";
            sql += "B.KW,A.JHDD,A.SHDW,A.YSFS,A.CM from ExportStoragePlan A,ExportStorageAcceptOrder B ";
            sql += "where A.JHH=B.JHH and A.MAKE=B.MAKE ";
            sql += "and B.CLH='"+ txtClh.Text +"'";

            DataTable dt = SqlCe.ExecuteQuery(sql);

            if (dt.Rows.Count > 0)
            {
                lblInfo.Text = "计划号："+ dt.Rows[0]["JHH"].ToString() +"\r\n";
                lblInfo.Text += "准发号：" + dt.Rows[0]["ZFH"].ToString() + "\r\n";
                lblInfo.Text += "品名：" + dt.Rows[0]["PM"].ToString() + "\r\n";
                lblInfo.Text += "净重：" + dt.Rows[0]["JZ"].ToString() + "\r\n";
                lblInfo.Text += "毛重：" + dt.Rows[0]["MZ"].ToString() + "\r\n";
                lblInfo.Text += "规格：" + dt.Rows[0]["GG"].ToString() + "\r\n";
                lblInfo.Text += "库位：" + dt.Rows[0]["KW"].ToString() + "\r\n";
                lblInfo.Text += "交货地点：" + dt.Rows[0]["JHDD"].ToString() + "\r\n";
                lblInfo.Text += "收货单位：" + dt.Rows[0]["SHDW"].ToString() + "\r\n";
                lblInfo.Text += "运输方式：" + dt.Rows[0]["YSFS"].ToString() + "\r\n";
                lblInfo.Text += "船名：" + dt.Rows[0]["CM"].ToString() + "\r\n";
            }
            else
            {
                lblInfo.Text = "";
            }
        }
    }
}