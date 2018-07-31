using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace hStore.Forms
{
    public partial class frmWtdOut : Form
    {
        public frmWtdOut()
        {
            InitializeComponent();
        }

        private void frmWtdOut_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Global.frmCurrent = this.Owner;
                this.Owner.Show();
                this.Owner = null;
                this.Close();
            }
            if (e.KeyCode == Keys.F21)
            {
                int js = Storage.GetWtdScanJs(Global.storage.jhh);
                if (js > 0)
                {
                    frmSend frmSend = new frmSend();
                    Global.frmCurrent = frmSend;
                    frmSend.Owner = this;
                    frmSend.Show();
                    this.Hide();
                }
            }
        }
    }
}