using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using PsionTeklogix.Keyboard;
using PsionTeklogix.Keyboard.NoChording;


namespace hStore.Forms.OutResearch.zk
{
    public partial class frmSend : Form
    {
        public string mz;
        public string js;
        public string clhs;

        public frmSend()
        {
            InitializeComponent();
        }

        private void frmSend_Load(object sender, EventArgs e)
        {
            ScanCodeRemapping.NormalTable.Add(0x003A, new RemappingTable.Remapping(VirtualKey.VK_F24, Function.SendCode));
            cboDq.SelectedIndex = Global.sDq;
            txtKjh.Text = Global.sKjh;
            txtJs.Text = js;
            txtMz.Text = mz;
            txtKb.Text = Global.sKb;
            cboPcbj.SelectedIndex = 0;
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            frmMessage frmMessage = new frmMessage();
            frmMessage.ShowDialog("是否需要发送出库消帐电文？", "提示", "是", "否");
            bool ret = frmMessage.ret;
            frmMessage.Dispose();

            if (ret)
            {

                lblInfo.Text = "发送中...";
                btnSend.Enabled = false;
                Global.sKjh = txtKjh.Text;
                Global.sDq = cboDq.SelectedIndex;

                string data = System.DateTime.Now.ToString("yyyyMMddHHmmss") + ";";
                data += Global.sBb + ";";
                data += Global.sKb + ";";
                data += Global.sUserId + ";";
                data += txtJxh.Text + ";";
                data += cboDq.Text + txtKjh.Text + ";";
                data += ";";
                data += ";";
                if (cboPcbj.SelectedIndex == 0)
                {
                    data += "N;";
                }
                else
                {
                    data += "Y;";
                }
                data += Global.sZyq + ";";
                data += ";";//直装标志 0,1

                string sql = "select * from ExportStorageAcceptOrder where CLH in (" + clhs + ") order by ZFH ";
                DataTable dt = SqlCe.ExecuteQuery(sql);
                string gram = "";
                string zfh = "";
                if (dt.Rows.Count > 0) zfh = dt.Rows[0]["ZFH"].ToString();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (zfh != dt.Rows[i]["ZFH"].ToString())
                    {
                        gram = gram.Substring(0, gram.Length - 1);
                        Business.SendText(Business.msg.Package("ZDWX56", data + gram));
                        gram = "";
                        zfh = dt.Rows[i]["ZFH"].ToString();
                    }
                    gram += dt.Rows[i]["CLH"].ToString() + ";" + dt.Rows[i]["SCANTIME"].ToString() + ";";

                }

                if (dt.Rows.Count > 0)
                {
                    gram = gram.Substring(0, gram.Length - 1);
                    Business.SendText(Business.msg.Package("ZDWX56", data + gram));
                }
                dt.Dispose();

                Business.InvokeMethod(this.Owner, "OutStore", new object[] { });
                lblInfo.Text = "发送成功！";

                Global.frmCurrent = this.Owner;
                this.Owner.Show();
                this.Owner = null;
                this.Close();
            }

        }

        private void frmSend_Closed(object sender, EventArgs e)
        {
            ScanCodeRemapping.NormalTable.Remove(0x003A);
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
            if (e.KeyCode >= Keys.A && e.KeyCode <= Keys.Z)
            {
                txtKjh.Text = txtKjh.Text.ToUpper();
                txtKjh.SelectionStart = txtKjh.Text.Length;
            }
        }

    }
}