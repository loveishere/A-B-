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
    public partial class frmSend : Form
    {
        private List<string> grams;

        public frmSend()
        {
            InitializeComponent();
        }

        private void frmSend_Load(object sender, EventArgs e)
        {
            int js = 0;
            int mz = 0;
            string gram = "";
            grams = new List<string>();
            int j = 0;


            DataTable dt = Storage.GetImpScanCL(Global.storage.jhh, Global.storage.make);
            js = dt.Rows.Count;


            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["MZ"] != DBNull.Value)
                {
                    mz += Convert.ToInt32(dt.Rows[i]["MZ"]);
                }

                gram += dt.Rows[i]["Make"].ToString() + ";" + dt.Rows[i]["JHH"].ToString() + ";" + dt.Rows[i]["CLH"].ToString() + ";" + dt.Rows[i]["KW"].ToString() + ";" + dt.Rows[i]["SCANTIME"].ToString() + ";" + dt.Rows[i]["QA"].ToString() + ";";
                j = i + 1;
                if (j % 50 == 0 || j == js)
                {
                    gram = gram.Substring(0, gram.Length - 1);
                    grams.Add(gram);
                    gram = "";
                }
            }

            txtJs.Text = js.ToString();
            txtMz.Text = mz.ToString();
            txtKjh.Text = Global.storage.kjh;


            txtJxh.Text = Global.storage.jxh;
            txtDL1.Text = Global.storage.dlh1;
            txtDL2.Text = Global.storage.dlh2;
            txtDJ.Text = Global.storage.djh;

            cboDq.SelectedIndex = Global.storage.dqSelectedIndex;

            cboDq.SelectedIndex = 0;

            if (Global.storage.inFlag)
            {
                cboStatus.SelectedIndex = 0;
            }
            else
            {
                cboStatus.SelectedIndex = 1;
            }
        }

        public void scanner_ScanCompleteEvent(string text)
        {
            string barcode = text;

            bool bval = false;

            if (barcode.Length !=7) bval=true;

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

            addCL(barcode);

        }

        private void addCL(string text)
        {
            string dlType = text.Substring(2, 1);
            if (dlType == "L")
            {
                if (txtDL1.Text == "")
                {
                    txtDL1.Text = text;
                }
                else if(text!=txtDL1.Text)
                {
                    txtDL2.Text = text;
                }
            }
            else if(dlType=="J")
            {
                txtDJ.Text = text;
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            frmMessage frmMessage = new frmMessage();
            bool bval = (cboDq.SelectedIndex == 0 && txtKjh.Text.Length == 4) || (cboDq.SelectedIndex > 0 && txtKjh.Text.Length == 6);
            if (bval == false)
            {
                frmMessage.ShowDialog("请输入正确的车号！","提示","确定");
                frmMessage.Dispose();
                return;
            }

            
            frmMessage.ShowDialog("是否需要发送入库消帐电文？", "提示", "是", "否");
            bool ret = frmMessage.ret;
            frmMessage.Dispose();

            if (ret)
            {
                Global.storage.kjh = "";
                Global.storage.dqSelectedIndex = cboDq.SelectedIndex;

                Global.storage.jxh = txtJxh.Text;
                Global.storage.dlh1 = txtDL1.Text;
                Global.storage.dlh2 = txtDL2.Text;
                Global.storage.djh = txtDJ.Text;

                btnSend.Enabled = false;

                foreach (string gram in grams)
                {
                    string data = DateTime.Now.ToString("yyyyMMddHHmmss") + ";";
                    data += Global.sBb + ";";
                    data += Global.sZyq + ";";
                    data += Global.sKb + ";";
                    data += Global.sUserId + ";";
                    data += txtJxh.Text + ";";
                    data += txtDL1.Text + ";";
                    data += txtDL2.Text + ";";
                    data += txtDJ.Text + ";";
                    data += cboDq.Text + txtKjh.Text + ";";
                    if (cboStatus.SelectedIndex == 1)
                    {
                        data += "N;";
                    }
                    else
                    {
                        data += "Y;";
                    }
                    data += gram;

                    if (Global.sDebug == "False")
                    {
                        Business.SendText(Business.msg.Package("ZDWX55", data));
                    }
                }

                Business.InvokeMethod(this.Owner, "InStore", new object[] { });


                Global.frmCurrent = this.Owner;
                this.Owner.Show();
                this.Owner = null;
                this.Close();
            }
        }

        private void frmSend_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Global.frmCurrent = this.Owner;
                this.Owner.Show();
                this.Owner = null;
                this.Close();
            }
        }

        private void txtKjh_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode >= Keys.A && e.KeyCode <= Keys.Z)
            {
                txtKjh.Text = txtKjh.Text.ToUpper();
                txtKjh.SelectionStart = txtKjh.Text.Length;
            }
        }

        private void txtDL1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode >= Keys.A && e.KeyCode <= Keys.Z)
            {
                txtDL1.Text = txtDL1.Text.ToUpper();
                txtDL1.SelectionStart = txtDL1.Text.Length;
            }
        }

        private void txtDL2_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode >= Keys.A && e.KeyCode <= Keys.Z)
            {
                txtDL2.Text = txtDL2.Text.ToUpper();
                txtDL2.SelectionStart = txtDL2.Text.Length;
            }
        }

        private void txtDJ_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode >= Keys.A && e.KeyCode <= Keys.Z)
            {
                txtDJ.Text = txtDJ.Text.ToUpper();
                txtDJ.SelectionStart = txtDJ.Text.Length;
            }
        }

    }
}