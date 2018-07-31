using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using PsionTeklogix.Keyboard;
using PsionTeklogix.Keyboard.NoChording;


namespace hStore.Forms.OutResearch.zc
{
    public partial class frmSend : Form
    {
        public List<string> grams;

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

            DataTable dt = Storage.GetExpScanCL(Global.storage.cph,Global.storage.jhh,Global.storage.make,Global.storage.zfh);
            js = dt.Rows.Count;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["MZ"] != DBNull.Value)
                {
                    mz += Convert.ToInt32(dt.Rows[i]["MZ"]);
                }
                
                gram += dt.Rows[i]["Make"].ToString() + ";" + dt.Rows[i]["JHH"].ToString() + ";" + dt.Rows[i]["CLH"].ToString() + ";" + dt.Rows[i]["SCANTIME"].ToString() + ";" + dt.Rows[i]["QA"].ToString() + ";";
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
            cboDq.SelectedIndex = Global.storage.dqSelectedIndex;
            txtKjh.Text = Global.storage.kjh;
            if (Global.storage.pzbj)//装车完成
            {
                txtKjh.Text = "";
            }
            else//装车未完成
            {
                txtKjh.Text = Global.storage.kjh;
            }
            txtJxh.Text = Global.storage.jxh;
            txtDL1.Text = Global.storage.dlh1;
            txtDL2.Text = Global.storage.dlh2;
            txtDJ.Text = Global.storage.djh;
            
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
            else if (dlType == "J")
            {
                txtDJ.Text = text;
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            frmMessage frmMessage = new frmMessage();
            frmMessage.ShowDialog("是否需要发送出库消帐电文？", "提示", "是", "否");
            bool ret = frmMessage.ret;
            frmMessage.Dispose();

            if (ret)
            {
                btnSend.Enabled = false;
                bool bval = false;

                foreach (string gram in grams)
                {
                    Global.storage.kjh = txtKjh.Text;
                    Global.storage.dqSelectedIndex = cboDq.SelectedIndex;
                    Global.storage.jxh = txtJxh.Text;
                    Global.storage.dlh1 = txtDL1.Text;
                    Global.storage.dlh2 = txtDL2.Text;
                    Global.storage.djh = txtDJ.Text;

                    Global.storage.pzbj = (cboPcbj.SelectedIndex == 0);

                    string data = ";" + Global.sBb + ";";
                    data += Global.sKb + ";";
                    data += Global.sUserId + ";";
                    data += txtJxh.Text + ";";
                    data += txtDL1.Text + ";";
                    data += txtDL2.Text + ";";
                    data += txtDJ.Text + ";";
                    data += cboDq.Text + txtKjh.Text + ";";
                    data += ";";
                    data += ";";
                    if (Global.storage.pzbj)
                    {
                        if (bval == false)
                        {
                            //确保第一条电文装车完成字段Y,剩下的电文都是N
                            data += "Y;";
                            bval = true;
                        }
                        else
                        {
                            data += "N;";
                        }
                    }
                    else
                    {
                        data += "N;";
                    }
   
                    data += Global.sZyq + ";";
                    data += ";";//直装标志 0,1
                    data += gram;

                    if (Global.sDebug == "False")
                    {
                        Business.SendText(Business.msg.Package("ZDWX56", DateTime.Now.ToString("yyyyMMddHHmmss") + data));
                    }
                }

                Storage.FinishExpScanCL(Global.storage.cph,Global.storage.jhh,Global.storage.make,Global.storage.zfh);

                Business.InvokeMethod(this.Owner, "LoadData", new object[] { });

                Storage.UpdateExpWcjs();

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
                Business.InvokeMethod(Global.frmCurrent, "LoadData", new object[] { });
                this.Close();
            }
        }

        private void txtKjh_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.Z)
            {
                txtKjh.Text = txtKjh.Text.ToUpper();
                txtKjh.SelectionStart = txtKjh.Text.Length;

                if (Global.storage.expflag == "tl")
                {
                    lstWagon.Items.Clear();
                    string sql = "select * from wagon where wagon like '" + txtKjh.Text + "%' and finish='K' and isused='Y' and kb='" + Global.sKb + "'";
                    DataTable dt = SqlCe.ExecuteQuery(sql);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        lstWagon.Items.Add(dt.Rows[i]["wagon"].ToString());
                    }
                    if (dt.Rows.Count > 0) lstWagon.Visible = true;
                    dt.Dispose();
                }
            }

        }

        private void lstWagon_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtKjh.Text = lstWagon.Items[lstWagon.SelectedIndex].ToString();
            txtKjh.SelectionStart = txtKjh.Text.Length;
            lstWagon.Visible = false;
        }

        private void txtDL1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.Z)
            {
                txtDL1.Text = txtDL1.Text.ToUpper();
                txtDL1.SelectionStart = txtDL1.Text.Length;
            }
        }

        private void txtDL2_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.Z)
            {
                txtDL2.Text = txtDL2.Text.ToUpper();
                txtDL2.SelectionStart = txtDL2.Text.Length;
            }
        }

        private void txtDJ_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.Z)
            {
                txtDJ.Text = txtDJ.Text.ToUpper();
                txtDJ.SelectionStart = txtDJ.Text.Length;
            }
        }

    }
}