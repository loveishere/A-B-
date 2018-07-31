using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace hStore.Forms
{
    public partial class frmMessage : Form
    {
        public bool ret;

        public frmMessage()
        {
            InitializeComponent();
        }

        public void ShowDialog(string msg, string title)
        {
            lblMessage.Text = msg;
            this.Text = title;
            this.ShowDialog();
        }

        public void ShowDialog(string msg, string title, string btntext1)
        {
            lblMessage.Text = msg;
            this.Text = title;
            this.btnOK.Text = btntext1;
            this.btnOK.Left = (this.Width - this.btnOK.Width) / 2;
            this.btnCancel.Visible = false;
            this.ShowDialog();

        }

        public void ShowDialog(string msg, string title, string btntext1, string btntext2)
        {
            this.btnOK.Text = btntext1;
            this.btnCancel.Text = btntext2;
            lblMessage.Text = msg;
            this.Text = title;
            this.ShowDialog();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            ret = true;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ret = false;
            this.Close();
        }
    }
}