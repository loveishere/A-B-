using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace hStore
{
    public partial class frmReceive : Form
    {
        //private delegate void setImage();
        //private delegate void setText();
        //private delegate void setStatus(string status);

        public frmReceive()
        {
            InitializeComponent();
        }

        private void txtJhh_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode >= Keys.A && e.KeyCode <= Keys.Z)
            {
                txtJhh.Text = txtJhh.Text.ToUpper();
                txtJhh.SelectionStart = txtJhh.Text.Length;
            }
        }

        public void showConnect()
        {
            Bitmap bitmap = null;
            if (Global.wwanConnected)
            {
                bitmap = new Bitmap(Global.appPath +@"\img\connect.bmp");
            }
            else
            {
                bitmap = new Bitmap(Global.appPath +@"\img\disconnect.bmp");
            }
            Global.UIThread(picCon, delegate { picCon.Image = Image.FromHbitmap(bitmap.GetHbitmap()); });
            //if (picCon.InvokeRequired)
            //{
            //    picCon.Invoke(new setImage(showConnect), new object[] { });
            //}
            //else
            //{
            //    picCon.Image = Image.FromHbitmap(bitmap.GetHbitmap());
            //}
        }

        private void frmReceive_Load(object sender, EventArgs e)
        {
            lblTitle.Text = "FMA计划下载";
            showConnect();
        }
        
        public void showMessage(string status)
        {
            showRJhs();
            showRCls();

            showCJhs();
            showCCls();
            showStatus(status);
        }

        public void showStatus(string status)
        {
            Global.UIThread(lblZt, delegate { lblZt.Text = status; });
            //if (lblZt.InvokeRequired)
            //{
            //    lblZt.Invoke(new setStatus(showStatus), new object[] { status });
            //}
            //else
            //{
            //    lblZt.Text = status;
            //}
        }

        private void showRJhs()
        {
            Global.UIThread(lblRJhs, delegate { lblRJhs.Text = Business.rjhs.ToString() + "/" + Business.ralljhs.ToString(); });
            //if (lblRJhs.InvokeRequired)
            //{
            //    lblRJhs.Invoke(new setText(showRJhs), new object[] { });
            //}
            //else
            //{

            //    lblRJhs.Text = Business.rjhs.ToString() + "/" + Business.ralljhs.ToString();
            //}
        }


        private void showCJhs()
        {
            Global.UIThread(lblCJhs, delegate { lblCJhs.Text = Business.cjhs.ToString() + "/" + Business.calljhs.ToString(); });
            //if (lblCJhs.InvokeRequired)
            //{
            //    lblCJhs.Invoke(new setText(showCJhs), new object[] { });
            //}
            //else
            //{
            //    lblCJhs.Text = Business.cjhs.ToString() + "/" + Business.calljhs.ToString();

            //}
        }

        private void showRCls()
        {
            Global.UIThread(lblRCls, delegate { lblRCls.Text = Business.rcls.ToString() + "/" + Business.rallcls.ToString(); });
            //if (lblRCls.InvokeRequired)
            //{
            //    lblRCls.Invoke(new setText(showRCls), new object[] { });
            //}
            //else
            //{
            //    lblRCls.Text = Business.rcls.ToString() + "/" + Business.rallcls.ToString();
            //}
        }

        private void showCCls()
        {
            Global.UIThread(lblCCls, delegate { lblCCls.Text = Business.ccls.ToString() + "/" + Business.callcls.ToString(); });
            //if (lblCCls.InvokeRequired)
            //{
            //    lblCCls.Invoke(new setText(showCCls), new object[] { });
            //}
            //else
            //{
            //    lblCCls.Text = Business.ccls.ToString() + "/" + Business.callcls.ToString();
            //}
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            if (txtJhh.Text != "")
            {
                //string gram = cboMake.Text + ";" + txtJhh.Text + ";" + Global.sKb;//调试
                string gram = txtJhh.Text + ";" + Global.sKb;
                //Business.SendText(Business.msg.Package("ZDWX51", gram));
                Business.SendText(Business.msg.Package("ZDUA10", gram));
            }
            else
            {
                SqlCe.ExecuteNonQuery("delete ImportStoragePlan");
                SqlCe.ExecuteNonQuery("delete ImportStorageAcceptOrder");
                SqlCe.ExecuteNonQuery("delete ExportStoragePlan");
                SqlCe.ExecuteNonQuery("delete ExportStorageAcceptOrder");
                SqlCe.ExecuteNonQuery("delete FrameDetail");

                //string gram =  cboMake.Text +";All;" + Global.sKb;//调试
                string gram = "ALL;" + Global.sKb;
                //Business.SendText(Business.msg.Package("ZDWX51", gram));
                Business.SendText(Business.msg.Package("ZDUA10", gram));

                //gram = "##0063UAZD04;0;0;117;919;S;20141022173429;20141022173429;4F35@@";
                //string sql = "insert into RecvMsg(Msg,Done,Lock,RecvTime,ID) values('" + gram.Replace("'", "''") + "',0,0,getDate(),newID())";
                //SqlCe.ExecuteNonQuery(sql);

            }
            btnDown.Enabled = false;
            Business.callcls = 0;
            Business.calljhs = 0;
            Business.ccls = 0;
            Business.cjhs = 0;
            Business.rallcls = 0;
            Business.ralljhs = 0;
            Business.rcls = 0;
            Business.rjhs = 0;
            showMessage("等待UACS反馈");
        }

        private void frmReceive_KeyUp(object sender, KeyEventArgs e)
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