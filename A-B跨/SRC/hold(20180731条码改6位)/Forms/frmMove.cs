using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using PsionTeklogix.Keyboard;
using PsionTeklogix.Keyboard.NoChording;

namespace hStore.Forms
{
    public partial class frmMove : Form
    {
        private delegate void setImage();
        private delegate void setText(string msg);

        public frmMove()
        {
            InitializeComponent();
        }

        private void frmMove_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                App.LinkedListForm.RemoveLast();
                this.Close();
            }
        }

        private void scanner_ScanCompleteEvent(string text)
        {
            bool bval = false;

            if (text.Length < 7) return;

            char[] chr = text.ToCharArray();

            for (int i = 0; i < chr.Length; i++)
            {
                if ((chr[i] < '0' || chr[i] > '9') && (chr[i] < 'A' || chr[i] > 'Z'))
                {
                    bval = true;
                    break;
                }
            }

            if (bval) return;

            txtClh.Text = text;
        }

        private void btnQd_Click(object sender, EventArgs e)
        {
            string gram = App.sKb+";";
            gram += txtClh.Text+ ";";
            gram += txtYkw.Text+";";
            gram += txtXkw.Text;
            App.cli.SendText(hStore.Gram.Message.Package("ZDWX53",gram)+App.cli.Resovlver.EndTag);
        }

        private void frmMove_Load(object sender, EventArgs e)
        {
            showConnect();
            ScanCodeRemapping.NormalTable.Remove(0x003A);
        }

        private void frmMove_Closed(object sender, EventArgs e)
        {
            ScanCodeRemapping.NormalTable.Add(0x003A, new RemappingTable.Remapping(VirtualKey.VK_F24, Function.SendCode));
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
    }
}