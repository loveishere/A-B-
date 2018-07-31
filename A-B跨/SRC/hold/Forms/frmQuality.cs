using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace hStore.Forms
{
    public partial class frmQuality : Form
    {
        private string clh;
        private string qa;
        private CoilPoint p;

        public frmQuality()
        {
            InitializeComponent();
        }

        public void Show(string clh,string qa,CoilPoint p)
        {
            this.clh = clh;
            this.qa = qa;
            this.p = p;
            this.Show();
        }

        private void frmQuality_Load(object sender, EventArgs e)
        {
            lblPdt.Text = "材料号："+ clh +"\r\n";

            DataTable dt = Storage.GetCLDetail(clh, Global.curFrame.KZ == "重");
            if (dt.Rows.Count > 0)
            {
                lblPdt.Text += "品名："+dt.Rows[0]["PM"].ToString();
            }

            lstQa.Items.Clear();
            dt = Storage.GetBasicQuality();
            ListViewItem item;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                item = new ListViewItem(new string[]{ dt.Rows[i]["name"].ToString(),dt.Rows[i]["id"].ToString() });
                item.BackColor = Color.PaleTurquoise;
                lstQa.Items.Add(item);
            }
            dt.Dispose();

            for (int i = 0; i < lstQa.Items.Count; i++)
            {
                if (lstQa.Items[i].SubItems[1].Text == qa)
                {
                    lstQa.Items[i].Selected = true;
                    lstQa.Items[i].Checked = true;
                    break;
                }
            }
        }

        private void frmQuality_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Escape)
            {
                Global.frmCurrent = this.Owner;
                this.Owner.Show();
                this.Owner = null;
                this.Close();
            }
        }

        private void lstQa_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (lstQa.SelectedIndices.Count > 0)
                {
                    ListViewItem item = lstQa.Items[lstQa.SelectedIndices[0]];
                    string qa=item.SubItems[1].Text;
                    if (qa != "0")
                    {
                        Global.coils[p].qa = qa;
                        Storage.SetCLQuality(clh, qa, Global.curFrame.KZ == "重");
                    }
                    else
                    {
                        Global.coils[p].qa = "";
                        Storage.ClearCLQuality(clh, Global.curFrame.KZ == "重");
                    }
                    Business.InvokeMethod(this.Owner, "ShowCoils", new object[] { });
                    Global.frmCurrent = this.Owner;
                    this.Owner.Show();
                    this.Owner = null;
                    this.Close();
                }
            }
        }

        private void lstQa_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < lstQa.Items.Count; i++)
            {
                lstQa.Items[i].Checked = lstQa.Items[i].Selected;
            }
        }

    }
}