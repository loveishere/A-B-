using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace hStore.Forms.InResearch
{
    public partial class frmInKW : Form
    {
        private string m_clh;
        private string m_kwori;
        public string m_kwnow;
        private string m_remain;
        public bool ret;

        public frmInKW()
        {
            InitializeComponent();
        }

        public void ShowDialog(string clh,string kw,string remain)
        {
            m_clh = clh;
            m_kwori = kw;
            m_remain = remain;
            this.ShowDialog();
        }

        private void btnQd_Click(object sender, EventArgs e)
        {
            if (txtKwNow.Text.Length != 5)
            {
                frmMessage frmMessage = new frmMessage();
                frmMessage.ShowDialog("库位需要输入5位字符！","提示","确定");
                frmMessage.Dispose();
                return;
            }
            ret = true;
            m_kwnow = txtKwNow.Text;
            Global.sKw = txtKwNow.Text;
            this.Close();
        }

        private void frmInKW_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                ret = false;
                this.Close();
            }
        }

        private void frmInKW_Load(object sender, EventArgs e)
        {
            txtClh.Text = m_clh;
            txtKwOri.Text = m_kwori;
            if (Global.sKw == "")
            {
                txtKwNow.Text = Global.sKb.Substring(2, 1);
            }
            else
            {
                txtKwNow.Text = Global.sKw;
            }
            txtKwNow.SelectionStart = txtKwNow.Text.Length;
            txtRemain.Text = m_remain;
            txtKwNow.Focus();
        }

        private void txtKwNow_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode >= Keys.A && e.KeyCode <= Keys.Z)
            {
                txtKwNow.Text = txtKwNow.Text.ToUpper();
                txtKwNow.SelectionStart = txtKwNow.Text.Length;
            }
        }

        private void txtClh_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode >= Keys.A && e.KeyCode <= Keys.Z)
            {
                txtClh.Text = txtClh.Text.ToUpper();
                txtClh.SelectionStart = txtClh.Text.Length;
            }
        }

        private void txtKwOri_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode >= Keys.A && e.KeyCode <= Keys.Z)
            {
                txtKwOri.Text = txtKwOri.Text.ToUpper();
                txtKwOri.SelectionStart = txtKwOri.Text.Length;
            }
        }


    }
}