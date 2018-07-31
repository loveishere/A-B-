using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;

namespace hStore.Forms
{
    public partial class frmDebug : Form
    {
        private string header;
        private string tailer;

        //private delegate void setText(string text);

        public frmDebug()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            statusTimer.Enabled = false;

            Global.frmCurrent = this.Owner;
            this.Owner.Show();
            this.Owner = null;
            this.Close();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtRecieve.Text = "";
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (txtSend.Text != "")
            {
                Global.socketClient.SendToSession(header + txtSend.Text + tailer);
                txtRecieve.Text += "发送：" + header + txtSend.Text + tailer + "\r\n";
            }
        }

        private void statusTimer_Tick(object sender, EventArgs e)
        {
            string txt = "";

            if (Global.socketClient.Connected)
            {
                txt += "通讯：正常    ";
            }
            else
            {
                txt += "通讯：关闭    ";
            }

            txt += "服务器：" + Global.ip + " " + Global.port.ToString() + "    ";

            string strHostName = Dns.GetHostName();      //得到本机的主机名
            IPHostEntry ipEntry = Dns.GetHostEntry(strHostName); //取得本机IP

            string ip = "";
            foreach (System.Net.IPAddress ipaddr in ipEntry.AddressList)
            {
                ip += ipaddr.ToString() + " ";
            }
            txt += "本机：" + ip;



            statusBar.Text = txt;

            string msg = "";

            if (Global.socketExceptionQueue.Count > 0)
            {
                msg = Global.socketExceptionQueue.Dequeue();
                txtRecieve.Text += msg + "\r\n";
            }

            if (Global.socketDataGramQueue.Count > 0)
            {
                msg = Global.socketDataGramQueue.Dequeue();
                if (txtRecieve.Text.Length + msg.Length > 3000)
                {
                    txtRecieve.Text = msg + "\r\n";
                }
                else
                {
                    txtRecieve.Text += msg + "\r\n";
                }
                txtRecieve.ScrollToCaret();
            }
        }

        private void frmDebug_Load(object sender, EventArgs e)
        {
            header = "##";
            tailer = "@@";
        }

        private void frmDebug_KeyDown(object sender, KeyEventArgs e)
        {
            string appPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.FullyQualifiedName);

            if (e.KeyCode == Keys.Escape)
            {
                statusTimer.Enabled = false;

                Global.frmCurrent = this.Owner;
                this.Owner.Show();
                this.Owner = null;
                this.Close();
            }
        }

    }
}