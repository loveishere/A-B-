using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlServerCe;
using System.Threading;

namespace hStore.Forms
{
    public partial class frmSetup : Form
    {
        public frmSetup()
        {
            InitializeComponent();
        }

        private void btnQd_Click(object sender, EventArgs e)
        {
            if (txtServerIP.Text != "" && txtPort.Text != "" && txtKb.Text!="" && cboDevice.SelectedIndex>=0 && cboDebug.SelectedIndex>=0)
            {
                Global.port = Convert.ToInt32(txtPort.Text);
                Global.ip = txtServerIP.Text;
                Global.sKb = txtKb.Text;
                Global.sDevice = cboDevice.Text;
                Global.sDebug = cboDebug.Text;

                Business.SaveConfig(Global.appPath + @"\config.xml",txtServerIP.Text,txtPort.Text,txtKb.Text,cboDevice.Text,cboDebug.Text);

                Global.socketClient.Stop();
                Thread.Sleep(500);
                Global.socketClient.ServerIP = Global.ip;
                Global.socketClient.ServerPort = Global.port;
                Global.socketClient.Start();
                
                Global.frmCurrent = this.Owner;
                this.Owner.Show();
                this.Owner = null;
                this.Close();
            }
        }

        private void frmSetup_Load(object sender, EventArgs e)
        {
            if (Global.sUserId != "BS")
            {
                txtServerIP.Enabled = false;
                txtPort.Enabled = false;
                txtKb.Enabled = false;
                cboDevice.Enabled = false;
                cboDebug.Enabled = false;
                btnQd.Enabled = false;
            }
            txtServerIP.Text = Global.ip;
            txtPort.Text = Global.port.ToString();
            txtKb.Text = Global.sKb;
            cboDevice.Text = Global.sDevice;
            cboDebug.Text = Global.sDebug;
        }

        private void frmSetup_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Global.frmCurrent=this.Owner;
                this.Owner.Show();
                this.Owner = null;
                this.Close();
            }
        }
    }
}