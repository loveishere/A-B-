using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace hStore.Forms
{

    public partial class frmApply : Form
    {

        private delegate void setText(string msg);
        private delegate void setImage();

        public frmApply()
        {
            InitializeComponent();
        }

        private void frmApply_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                App.LinkedListForm.RemoveLast();
                this.Close();
            }
        }

        private void btnApplyAll_Click(object sender, EventArgs e)
        {
            App.ResetRecvMsg();
            string gram = "All;"+App.sKb;
            App.cli.SendText(hStore.Gram.Message.Package("ZDWX51",gram)+App.cli.Resovlver.EndTag);
        }

        public void showConnect()
        {
            Bitmap bitmap = null;
            if (App.cli.IsConnected)
            {
                bitmap = new Bitmap(@"\SD-MMC Card\connect.bmp");
            }
            else
            {
                bitmap = new Bitmap(@"\SD-MMC Card\disconnect.bmp");
            }
            if (picCon.InvokeRequired)
            {
                picCon.Invoke(new setImage(showConnect), new object[] { });
            }
            else
            {
                picCon.Image = Image.FromHbitmap(bitmap.GetHbitmap());
            }
        }

        public void showMessage(string msg)
        {
            if (lblMsg.InvokeRequired)
            {
                lblMsg.Invoke(new setText(showMessage), new object[] { msg });
            }
            else
            {
                this.lblMsg.Text = msg;
            }
        }

        private void frmApply_Load(object sender, EventArgs e)
        {
            btnApplyAll.Text = App.sKb+"计划下载";
            showConnect();
        }

        private void btnApplyOne_Click(object sender, EventArgs e)
        {
            App.ResetRecvMsg();
            string gram = txtJhh.Text+";" + App.sKb;
            App.cli.SendText(hStore.Gram.Message.Package("ZDWX51", gram) + App.cli.Resovlver.EndTag);
        }
    }
}