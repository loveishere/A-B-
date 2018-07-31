#define InterfaceTest

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
    public partial class FrameIn : Form
    {
        private string sGroove = "";
        private string sZZ = "";
        private int iMaxRow = 0;
        private int iSpace = 0;
        public static string getKJHtext;

        public FrameIn()
        {
            InitializeComponent();
            
        }
        //将框架号值传到清盘入库界面
        public string Text3
        {
            get { return this.textKJH.Text; }
            //set { this.textBox1.Text = value; }
        }
        //public FrameIn(string text)
        //    : this()
        //{
        //    textKJH.Text = text;
        //}
        public string getKJHText() {
            string kjh = getKJHtext;
            return kjh;
        }
        private void FrameIn_Load(object sender, EventArgs e)
        {
            textTCH.Text = "";
            cboQY.SelectedIndex = 0;
            textKJH.Text = "";
            cboKZ.SelectedIndex = -1;
            //changeByyangting 20170518
            Category.SelectedIndex = 0;
            textZZ.Text = "";

            bool bval = false;
            if (Global.curFrame == null)
            {
                bval = true;
            }
            else
            {
                if (Global.curFrame.status == FrameStatus.PT_Initial)
                {
                    bval = true;
                }
            }
            if (bval)
            {
                //btnRW.Visible = true;
                //btnScan.Visible = false;
            }
            else
            {
                //btnRW.Visible = false;
                //btnScan.Visible = true;
            }


        }

        private void btnRW_Click(object sender, EventArgs e)
        {
            frmMessage frmMessage = new frmMessage();

#if InterfaceTest
            if (textTCH.Text.Length < 3)
#else
            if (textTCH.Text.Length != 4)
#endif
            {
                frmMessage.ShowDialog("请输入正确的停车位编号！", "提示", "确定");
                frmMessage.Dispose();
                return;
            }

            bool bva = (cboQY.SelectedIndex == 0 && textKJH.Text.Length == 4 || cboQY.SelectedIndex > 0 && textKJH.Text.Length == 6);
            if (bva == false)
            {
                frmMessage.ShowDialog("请输入正确的车号！","提示", "确定");
                frmMessage.Dispose();
                return;
            }

            if (cboFX.SelectedIndex == -1)
            {
                frmMessage.ShowDialog("请选择框架的方向！", "提示", "确定");
                frmMessage.Dispose();
                return;
            }

            if (cboKZ.SelectedIndex==-1)
            {
                frmMessage.ShowDialog("请选择框架的空重状态！", "提示", "确定");
                frmMessage.Dispose();
                return;
            }
            //changeByyangting20170518
            //if (Category.SelectedIndex == -1)
            //{
            //    frmMessage.ShowDialog("请选择材料种类！", "提示", "确定");
            //    frmMessage.Dispose();
            //    return;
            //}

            if (Global.IsNumberic(textZZ.Text)==false)
            {
                frmMessage.ShowDialog("请输入载重！", "提示", "确定");
                frmMessage.Dispose();
                return;
            }

            string sDirection = "";
            if (cboFX.SelectedIndex == 0)
            {
                sDirection = "N";//朝北
            }
            else if (cboFX.SelectedIndex == 1)
            {
                sDirection = "S";
            }
            #region changeByyang2170518

            string whatCategory = "";
            if (Category.SelectedIndex == 0)
            {
                whatCategory = "J";//钢卷
            }
            else if (Category.SelectedIndex == 1)
            {
                whatCategory = "B";//板坯
            }

            #endregion
            if (cboQY.Text != "")
            {
                //changeByyangting20170518
                Global.curFrame = new Frame(textTCH.Text, cboQY.Text, textKJH.Text, "0000", cboKZ.Text, sDirection, textZZ.Text, iMaxRow, iSpace, textTDH.Text, whatCategory);
            }
            else
            {
                //changeByyangting20170518
                Global.curFrame = new Frame(textTCH.Text, cboQY.Text, textKJH.Text, textKJH.Text, cboKZ.Text, sDirection, textZZ.Text, iMaxRow, iSpace, textTDH.Text, whatCategory);
            }

            Global.curFrame.status = FrameStatus.PT_CarArrive;

            string data = Global.sUserId + AllCode.stringInterfaceChar;
            data += System.DateTime.Now.ToString("yyyyMMddHHmmss") + AllCode.stringInterfaceChar;
            data += Global.sBb + AllCode.stringInterfaceChar;
            data += Global.sZyq + AllCode.stringInterfaceChar;
            data += textTCH.Text + AllCode.stringInterfaceChar;
            data += cboQY.Text + textKJH.Text + AllCode.stringInterfaceChar;
            if (cboKZ.SelectedIndex == 0)
            {
                data += "0" + AllCode.stringInterfaceChar;//空
            }
            else if (cboKZ.SelectedIndex == 1)
            {
                data += "1" + AllCode.stringInterfaceChar;//重
            }
            else
            {
                data += "9" + AllCode.stringInterfaceChar;//拼
            }


            data += sDirection + AllCode.stringInterfaceChar;
            data += textZZ.Text + AllCode.stringInterfaceChar;

            if (textTDH.Text != "")
            {
                data += textTDH.Text;//提单号
            }
            data += AllCode.stringInterfaceChar;

            #region  changeByyangting20170518

            data += whatCategory + AllCode.stringInterfaceChar;
            #endregion

            if (Global.sDebug == "False")
            {
                frmMessage.ShowDialog("是否进行"+ Global.curFrame.KZ +"框架入位操作？", "选择", "确定", "取消");
                if (frmMessage.ret == true)
                {
                    Business.SendText(Business.msg.Package("ZDUA01", data));//发送入位电文
                }
                frmMessage.Dispose();
            }

            //btnRW.Visible = false;
            //btnScan.Visible = true;
            sGroove = "";
        }

        public void showDialogMsg(string msg)
        {
            Global.UIThread(this, delegate
            {
                frmMessage dialog = new frmMessage();
                dialog.ShowDialog(msg, "提示", "确定");
                dialog.Dispose();
            });
        }

        public void showMessage(string msg)
        {
            Global.UIThread(this, delegate { lblMsg.Text = msg; });
        }

        private void FrameIn_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {

                Global.frmCurrent = this.Owner;
                this.Owner.Show();
                this.Owner = null;
                this.Close();
            }
            else if (e.KeyCode == Keys.Tab)
            {
                if (Global.curFrame != null)
                {
                    if (Global.curFrame.CarArrived)
                    {
                        if (Global.curFrame.KZ == "重")
                        {
                            
                                to_frmDgv();
                          
                           
                        }
                        else if (Global.curFrame.KZ == "空" || Global.curFrame.KZ == "拼")
                        {
                            to_frmSxChange();
                        }
                    }
                }
            }
            else if (e.KeyCode == Keys.Enter)
            {
                if (Global.curFrame.KZ == "重")
                {
                    to_frmDgv2();
                }

            }
            else if (e.KeyCode == Keys.F5)//F1键
            {
                if (Global.sDebug == "False")
                {
                    string data = Global.sUserId + ";";
                    data += System.DateTime.Now.ToString("yyyyMMddHHmmss") + ";";
                    data += Global.sBb + ";";
                    data += Global.sZyq + ";";
                    data += "15" + ";";//15号行车
                    data += System.DateTime.Now.ToString("yyyyMMddHHmmss");

                    Business.SendText(Business.msg.Package("ZDUA06", data));//控制15号行车紧停
                }
            }
            else if (e.KeyCode == Keys.F3)//F3键
            {
                if (Global.sDebug == "False")
                {
                    string data = Global.sUserId + ";";
                    data += System.DateTime.Now.ToString("yyyyMMddHHmmss") + ";";
                    data += Global.sBb + ";";
                    data += Global.sZyq + ";";
                    data += "17" + ";";//17号行车
                    data += System.DateTime.Now.ToString("yyyyMMddHHmmss");

                    Business.SendText(Business.msg.Package("ZDUA06", data));//控制17号行车紧停
                }
            }
            else if (e.KeyCode == Keys.F4)//F4键
            {
                if (Global.sDebug == "False")
                {
                    string data = Global.sUserId + ";";
                    data += System.DateTime.Now.ToString("yyyyMMddHHmmss") + ";";
                    data += Global.sBb + ";";
                    data += Global.sZyq + ";";
                    data += "21" + ";";//21号行车
                    data += System.DateTime.Now.ToString("yyyyMMddHHmmss");

                    Business.SendText(Business.msg.Package("ZDUA06", data));//控制21号行车紧停
                }
            }
            else if (e.KeyCode == Keys.F2)//F2键
            {
                if (Global.sDebug == "False")
                {
                    string data = Global.sUserId + ";";
                    data += System.DateTime.Now.ToString("yyyyMMddHHmmss") + ";";
                    data += Global.sBb + ";";
                    data += Global.sZyq + ";";
                    data += "22" + ";";//22号行车
                    data += System.DateTime.Now.ToString("yyyyMMddHHmmss");

                    Business.SendText(Business.msg.Package("ZDUA06", data));//控制22号行车紧停
                }
            }
        }

        public void scanner_ScanCompleteEvent(string text)
        {
            string barcode = text;

            bool bval = false;

            if (barcode.Length != 7  && barcode.Length != 3) bval = true;

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
            //changeByYang20170525 停车号T15=0105

            addResult(barcode);

        }

        private void addResult(string text)
        {
            if (text.Length == 7)
            {
                sGroove = text;
                //getDirection(textTCH.Text, sGroove);
                getDirection(sGroove);
                textKJH.Text = text.Substring(0, 4);
                getKJInfo(textKJH.Text);
                textZZ.Text = sZZ;
            }
            else if (text.Length == 3)
            {
                 textTCH.Text = text;
                //changeByYang20170525 停车号T15=0105
                //textTCH.Text = "T" + text[1].ToString() + text[3].ToString(); 

                //getDirection(textTCH.Text, sGroove);
            }
        }

        private void btnScan_Click(object sender, EventArgs e)
        {
            string data = Global.sUserId + ";";
            data += System.DateTime.Now.ToString("yyyyMMddHHmmss") + ";";
            data += Global.sBb + ";";
            data += Global.sZyq + ";";
            data += textTCH.Text + ";";
            data += textKJH.Text;

            if (Global.sDebug == "False")
            {
                frmMessage frmMessage = new frmMessage();
                frmMessage.ShowDialog("是否进行启动激光扫描操作？", "选择", "确定", "取消");
                if (frmMessage.ret == true)
                {
                    Business.SendText(Business.msg.Package("ZDUA02", data));//启动扫描
                }
                frmMessage.Dispose();

                //string gram = "##0378UAZD020402;2001;20;N;4;;;;2200;3000;1100;1900;69;;;;;3150;5400;1100;1900;70;;;;;3600;7800;1100;1900;71;;;;;1650;7800;1100;1900;72;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;9999@@";
                //string sql = "insert into RecvMsg(Msg,Done,Lock,RecvTime,ID) values('" + gram.Replace("'", "''") + "',0,0,getDate(),newID())";
                //SqlCe.ExecuteNonQuery(sql);

                Global.curFrame.status = FrameStatus.PT_LaserStart;
            }
        }
    
        private void to_frmDgv()
        {
            frmDgv frmDgv = new frmDgv();
            Global.frmCurrent = frmDgv;
            frmDgv.Owner = this;
            frmDgv.Show();
            this.Hide();
        }
        //yt
        private void to_frmDgv2()
        {
            frmDgv2 frmDgv2 = new frmDgv2();
           // frmSlabIn2 frmDgv2 = new frmSlabIn2();
            Global.frmCurrent = frmDgv2;
            frmDgv2.Owner = this;
            frmDgv2.Show();
            this.Hide();
        }
  
        private void to_frmSxChange()
        {
            frmSxChange frmSxChange = new frmSxChange();
            Global.frmCurrent = frmSxChange;
            frmSxChange.Owner = this;
            frmSxChange.Show();
            this.Hide();
        }

        private void textTCH_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                textTCH.Text = textTCH.Text.ToUpper(); 
                textTCH.SelectionStart = textTCH.Text.Length;
                //if (textTCH.Text.Length == 4)
                //{
                //    getDirection(textTCH.Text, sGroove);
                //}
            }
            catch { }
        }

        private void textKJH_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                textKJH.Text = textKJH.Text.ToUpper();
                textKJH.SelectionStart = textKJH.Text.Length;
                 getKJHtext = textTCH.Text;

                if (cboQY.SelectedIndex > 0 && textKJH.Text.Length == 6)
                {
                    cboFX.SelectedIndex = 0;//规定自提车辆的行驶路线为南进北出；

                    sZZ = "90";
                    iMaxRow = 10;
                    iSpace = 1200;
                    textZZ.Text = sZZ;
                }

                if (cboQY.SelectedIndex == 0 && textKJH.Text.Length == 4)
                {
                    getKJInfo(textKJH.Text);
                    textZZ.Text = sZZ;
                }
            }
            catch { }
        }

        //private void getDirection(string tch,string groove)
        //{
        //    if(tch.Length!=4) return;
        //    if (groove.Length != 7) return;
        //    int iTch = Int32.Parse(tch.Substring(2, 2));
        //    int iGroove = Convert.ToInt32(groove.Substring(4,2));
        //    if (iTch <= 4)//北跨
        //    {
        //        if (iGroove > 4)
        //        {
        //            cboFX.SelectedIndex = 1;//南
        //        }
        //        else
        //        {
        //            cboFX.SelectedIndex = 0;//北
        //        }
        //    }
        //    else if (iTch > 4)//南跨
        //    {
        //        if (iGroove > 4)
        //        {
        //            cboFX.SelectedIndex = 0;//北
        //        }
        //        else
        //        {
        //            cboFX.SelectedIndex = 1;//南
        //        }
        //    }
        //}

        private void getDirection(string groove)
        {
            if (groove.Length != 7) return;
            int iGroove = Convert.ToInt32(groove.Substring(4, 2));
            if (iGroove > 4)
            {
                cboFX.SelectedIndex = 1;//南
            }
            else
            {
                cboFX.SelectedIndex = 0;//北
            }
        }


        private void getKJInfo(string kjh)
        {
            string sql = "select * from Frame where FrameID='"+ kjh +"'";
            DataTable dt = SqlCe.ExecuteQuery(sql);
            if (dt.Rows.Count == 0)
            {
                sZZ = "";
                iMaxRow = 0;
                iSpace = 0;
                frmMessage frmMessage = new frmMessage();
                frmMessage.ShowDialog("未找到框架号为：" + kjh + "的框架信息！", "提示", "确定");
                frmMessage.Dispose();
            }
            else
            {
                sZZ = dt.Rows[0]["Weight"].ToString();
                if (dt.Rows[0]["FrameCount"] != DBNull.Value)
                {
                    iMaxRow = Convert.ToInt32(dt.Rows[0]["FrameCount"]);
                }
                else
                {
                    iMaxRow = 0;
                }

                if (dt.Rows[0]["Space"] != DBNull.Value)
                {
                    iSpace = Convert.ToInt32(dt.Rows[0]["Space"]);
                }
                else
                {
                    iSpace = 0;
                }
            }

        }

        private void FrameIn_Activated(object sender, EventArgs e)
        {
            if (ScanCodeRemapping.NormalTable.Contains(0x003A))
            {
                ScanCodeRemapping.NormalTable.Remove(0x003A);
            }
        }

        private void btnScan_Click_1(object sender, EventArgs e)
        {

        }

    }
}