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
                        //hStore.Forms.InResearch.frmInPlan frm = new hStore.Forms.InResearch.frmInPlan();//入库计划查询
                        frmSlabClear frm = new frmSlabClear();//板坯清盘库
                        Global.frmCurrent = frm;
                        Global.storage.inFlag = true;
                        frm.Owner = this;
                        frm.Show();
                        this.Hide();
                    }
                    //else if (listView1.SelectedIndices[0] == 1)
                    //{
                    //    hStore.Forms.OutResearch.frmMain frm = new hStore.Forms.OutResearch.frmMain();//出库
                    //    Global.frmCurrent = frm;
                    //    Global.storage.inFlag = false;
                    //    frm.Owner = this;
                    //    frm.Show();                        
                    //    this.Hide();
                    //}
                    else if (listView1.SelectedIndices[0] == 1)
                    {
                        //frmClear frm = new frmClear();
                        frmClearA frm = new frmClearA();//清盘库
                        Global.frmCurrent = frm;
                        frm.Owner = this;
                        frm.Show();
                        this.Hide();
                    }
                    //else if (listView1.SelectedIndices[0] == 2)
                    //{
                    //    frmReceive frm = new frmReceive();//当班计划下载
                    //    Global.frmCurrent = frm;
                    //    frm.Owner = this;
                    //    frm.Show();
                    //    this.Hide();
                    //}
                    else if (listView1.SelectedIndices[0] == 2)
                    {
                        frmWriteOff frm = new frmWriteOff();//销账
                        Global.frmCurrent = frm;
                        frm.Owner = this;
                        frm.Show();
                        this.Hide();
                    }
                    else if (listView1.SelectedIndices[0] == 3)
                    {
                        FrameIn frm = new FrameIn();//自动化作业
                        Global.frmCurrent = frm;
                        frm.Owner = this;
                        frm.Show();
                        this.Hide();
                    }
                    else if (listView1.SelectedIndices[0] == 4)
                    {
                        frmSetup frm = new frmSetup();//系统设置
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
                this.Owner.Show();
                this.Owner = null;
                this.imgList.Images.Clear();
                this.imgList.Dispose();
                this.Close();
                this.Dispose();
                //this.Dispose();
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

        private void frmMain_Activated(object sender, EventArgs e)
        {
            if (!ScanCodeRemapping.NormalTable.Contains(0x003A))
            {
                ScanCodeRemapping.NormalTable.Add(0x003A, new RemappingTable.Remapping(VirtualKey.VK_F24, Function.SendCode));
            }
        }
    }
}