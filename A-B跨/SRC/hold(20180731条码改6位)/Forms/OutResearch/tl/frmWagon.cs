using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace hStore.Forms.OutResearch.tl
{
    public partial class frmWagon : Form
    {
        //private delegate void getWagonList();

        public frmWagon()
        {
            InitializeComponent();
        }

        private void frmWagon_Load(object sender, EventArgs e)
        {
            showWagon();
        }

        public void showWagon()
        {
            Global.UIThread(lstWagon, delegate {
                lstWagon.Items.Clear();
                string sql = "select * from wagon where kb='" + Global.sKb + "'";
                DataTable dt = SqlCe.ExecuteQuery(sql);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["isused"].ToString() == "N")
                    {
                        lstWagon.Items.Add(dt.Rows[i]["wagon"].ToString() + "      色牌");
                    }
                    else if (dt.Rows[i]["finish"].ToString() == "Z")
                    {
                        lstWagon.Items.Add(dt.Rows[i]["wagon"].ToString() + "      重车");
                    }
                    else
                    {
                        lstWagon.Items.Add(dt.Rows[i]["wagon"].ToString() + "      空车");
                    }
                }
                dt.Dispose();            
            });
        }

        private void btnWagon_Click(object sender, EventArgs e)
        {
            string sql = "delete from wagon";
            SqlCe.ExecuteNonQuery(sql);
            showWagon();

            Business.SendText(Business.msg.Package("ZDWX59", Global.sKb));
            btnWagon.Enabled = false;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            hStore.Forms.OutResearch.tl.frmOutPlan frm = new hStore.Forms.OutResearch.tl.frmOutPlan();
            Global.frmCurrent = frm;
            frm.Owner = this;
            frm.Show();
            this.Hide();
        }

        private void frmWagon_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Global.frmCurrent = this.Owner;
                this.Owner.Show();
                this.Owner = null;
                this.Close();
            }
        }
    }
}