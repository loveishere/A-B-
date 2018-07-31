using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace hStore.Forms
{
    public partial class frmWtdInCl : Form
    {
        private string clh;
        public bool ret;
        public string sJs;
        public string sZL;
        public string sKw;
        public string sLbl;

        public frmWtdInCl()
        {
            InitializeComponent();
        }

        public void ShowDialog(string clh,string lbl,string kw)
        {
            this.clh = clh;
            this.sKw = kw;
            this.sJs = Global.sJs;
            this.sZL = Global.sZl;
            this.sLbl = lbl;
            this.ShowDialog();
        }

        public void scanner_ScanCompleteEvent(string text)
        {
            string barcode = text;

            bool bval = false;

            if (barcode.Length != 7) bval = true;

            char[] chr = barcode.ToCharArray();

            for (int i = 0; i < chr.Length; i++)
            {

                if ((chr[i] < '0' || chr[i] > '9') && (chr[i] < 'A' || chr[i] > 'Z'))
                {
                    bval = true;
                    break;
                }
            }

            if (bval) return;

            txtLabel.Text = barcode;

        }

        public void ShowDialog(string clh,string lbl, string kw, string sJs, string sZl)
        {
            this.clh = clh;
            this.sKw = kw;
            this.sJs = sJs;
            this.sZL = sZl;
            this.sLbl = lbl;

            this.ShowDialog();
        }

        private void frmWtdInCl_Load(object sender, EventArgs e)
        {
            txtClh.Text = clh;

            txtJs.Text = sJs;

            txtZl.Text = sZL;

            txtLabel.Text = sLbl;

            if (sKw == "")
            {
                txtKw.Text = Global.sKb.Substring(2, 1);
            }
            else
            {
                txtKw.Text = sKw;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            frmMessage frmMessage = new frmMessage();

            if (Global.IsNumberic(txtJs.Text) == false)
            {
                frmMessage.ShowDialog("请输入材料件数！", "提示", "确定");
                return;
            }

            if (Global.IsNumberic(txtZl.Text) == false && txtZl.Text!="")
            {
                frmMessage.ShowDialog("请输入材料重量！", "提示", "确定");
                return;
            }

            if (txtLabel.Text.Trim() == "")
            {
                frmMessage.ShowDialog("请绑定条码！", "提示", "确定");
                return;
            }

            sJs = txtJs.Text.Trim();

            sZL = txtZl.Text.Trim();

            if (sZL == "") sZL = "0";

            sKw = txtKw.Text.Trim();

            sLbl = txtLabel.Text.Trim();

            Global.sJs = sJs;//用于记忆功能

            Global.sZl = sZL;

            Global.sKw = txtKw.Text;

            ret = true;

            this.Close();
        }

        private void frmWtdInCl_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                ret = false;
                this.Close();
            }
        }

        private void txtKw_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode >= Keys.A && e.KeyCode <= Keys.Z)
            {
                txtKw.Text = txtKw.Text.ToUpper();
                txtKw.SelectionStart = txtKw.Text.Length;
            }
        }
    }
}