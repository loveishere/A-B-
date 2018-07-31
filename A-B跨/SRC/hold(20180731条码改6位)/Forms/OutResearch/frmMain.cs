using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace hStore.Forms.OutResearch
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void listView1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (listView1.SelectedIndices.Count > 0)
                {
                    if (listView1.SelectedIndices[0] == 0)
                    {
                        hStore.Forms.OutResearch.zt.frmOutPlan frm = new hStore.Forms.OutResearch.zt.frmOutPlan();
                        Global.frmCurrent = frm;
                        Global.storage.expflag = "zt";
                        frm.Owner = this;
                        frm.Show();
                        this.Hide();
                    }
                    if (listView1.SelectedIndices[0] == 1)
                    {
                        hStore.Forms.OutResearch.zk.frmOutPlan frm = new hStore.Forms.OutResearch.zk.frmOutPlan();
                        Global.frmCurrent = frm;
                        Global.storage.expflag = "zk";
                        frm.Owner = this;
                        frm.Show();
                        this.Hide();
                    }
                    if (listView1.SelectedIndices[0] == 2)
                    {
                        hStore.Forms.OutResearch.zc.frmOutCP frm = new hStore.Forms.OutResearch.zc.frmOutCP();
                        Global.frmCurrent = frm;
                        Global.storage.expflag = "zc";
                        frm.Owner = this;
                        frm.Show();
                        this.Hide();
                    }
                    else if (listView1.SelectedIndices[0] == 3)
                    {
                        hStore.Forms.OutResearch.tl.frmOutPlan frm = new hStore.Forms.OutResearch.tl.frmOutPlan();
                        Global.frmCurrent = frm;
                        Global.storage.expflag = "tl";
                        frm.Owner = this;
                        frm.Show();
                        this.Hide();

                    }
                    else if (listView1.SelectedIndices[0] == 4)
                    {
                        frmOutPlan frm = new frmOutPlan();
                        Global.frmCurrent = frm;
                        frm.Owner = this;
                        frm.Show();
                        this.Hide();
                    }
                }
            }
            else if (e.KeyCode == Keys.Escape)
            {
                Global.frmCurrent = this.Owner;
                Global.storage.expflag = "";
                this.Owner.Show();
                this.Owner = null;
                this.imgList.Images.Clear();
                this.imgList.Dispose();
                this.Close();
                this.Dispose();
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            for (int i = 1; i <= 5; i++)
            {
                Bitmap bitmap = new Bitmap(Global.appPath +@"\img\m" + i.ToString() + ".bmp");
                imgList.Images.Add(Image.FromHbitmap(bitmap.GetHbitmap()));
                listView1.Items[i - 1].ImageIndex = i - 1;
                bitmap.Dispose();
                bitmap = null;
            }
        }
    }
}