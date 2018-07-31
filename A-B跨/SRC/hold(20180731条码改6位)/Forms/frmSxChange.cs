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
    public partial class frmSxChange : Form
    {
        private DataTable dtJhzy;
        private int curRow;
        
        public frmSxChange()
        {
            InitializeComponent();
        }

        //bool key = false;

        //int pageSize = 0;     //每页显示行数
        //int nMax = 0;         //总记录数
        //int pageCount = 0;    //页数=总记录数/每页显示行数
        //int pageCurrent = 0;  //当前页号
        //int nCurrent = 0;     //当前记录行

        private void LoadPlan()
        {
            string sql = "select SX,HCH,KJH,CLH,MDD from TaskOrder ";
            if (Global.curFrame != null)
            {
                if (Global.curFrame.HCH != "")
                {
                    sql += "where HCH='" + Global.curFrame.HCH + "'";
                }
                else
                {
                    sql += "where 1=2 ";
                }
            }
            dtJhzy = SqlCe.ExecuteQuery(sql);
            dgSxChange.DataSource = dtJhzy;


            dgSxChange.TableStyles.Clear();

            DataGridTableStyle SxchangeStyle = new DataGridTableStyle();
            SxchangeStyle.MappingName = dtJhzy.TableName;
            CheckCellEventHandler cellEvent = new CheckCellEventHandler(CheckCellColor);

            SxchangeStyle.GridColumnStyles.Add(new ColumnStyle(0, "SX", "顺序", 50, "", cellEvent));
            SxchangeStyle.GridColumnStyles.Add(new ColumnStyle(1, "KJH", "框架号", 60, "", cellEvent));
            SxchangeStyle.GridColumnStyles.Add(new ColumnStyle(2, "CLH", "材料号", 120, "", cellEvent));
            SxchangeStyle.GridColumnStyles.Add(new ColumnStyle(3, "MDD", "目的地", 120, "", cellEvent));

            dgSxChange.TableStyles.Add(SxchangeStyle);

            if (Global.curFrame.HCH != "")
            {
                lblTitle.Text = Global.curFrame.HCH + "#行车作业指令";
            }

            curRow = -1;
        }

        private void frmSxChange_Load(object sender, EventArgs e)
        {
            LoadPlan();
        }

        private void frmSxChange_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Global.frmCurrent = this.Owner;
                this.Owner.Show();
                this.Owner = null;
                this.Close();
            }
            //else if (e.KeyCode == Keys.Right)//下一页
            //{
            //    if (pageCurrent < pageCount)
            //    {
            //        pageCurrent++;

            //        nCurrent = pageSize * (pageCurrent - 1);
            //        LoadData();
            //    }
            //}
            //else if(e.KeyCode==Keys.Left)//上一页
            //{
            //    if (pageCurrent > 1)
            //    {
            //        pageCurrent--;

            //        nCurrent = pageSize * (pageCurrent - 1);
            //        LoadData();
            //    }
            //}
            else if (e.KeyValue == 239)//环形键
            {
                string data = Global.sUserId + ";";
                data += System.DateTime.Now.ToString("yyyyMMddHHmmss") + ";";
                data += Global.sBb + ";";
                data += Global.sZyq + ";";
                data += Global.curFrame.TCH + ";";
                data += Global.curFrame.CH;

                if (Global.sDebug == "False")
                {
                    frmMessage frmMessage = new frmMessage();
                    frmMessage.ShowDialog("是否确认启动行车作业？", "选择", "确定", "取消");
                    if (frmMessage.ret == true)
                    {
                        Business.SendText(Business.msg.Package("ZDUA04", data));//单车作业开始
                    }
                    frmMessage.Dispose();
                }

            }
            else if (e.KeyCode == Keys.Back)//DEL键
            {
                if(Global.sDebug == "False")
                {
                    string data = Global.sUserId + ";";
                    data += System.DateTime.Now.ToString("yyyyMMddHHmmss") + ";";
                    data += Global.sBb + ";";
                    data += Global.sZyq + ";";
                    data += Global.curFrame.TCH + ";";
                    data += Global.curFrame.CH + ";";
                    data += Global.curFrame.HCH;//行车号

                    frmMessage frmMessage = new frmMessage();
                    frmMessage.ShowDialog("是否确认暂停行车作业？", "选择", "确定", "取消");
                    if (frmMessage.ret == true)
                    {
                        Business.SendText(Business.msg.Package("ZDUA05", data));//单车作业暂停
                    }
                    frmMessage.Dispose();

                    
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
            else if (e.KeyCode == Keys.Tab)
            {
                if (Global.curFrame.KZ == "空" || Global.curFrame.KZ == "拼")
                {
                    frmDgv frmDgv = new frmDgv();
                    Global.frmCurrent = frmDgv;
                    frmDgv.Owner = this;
                    frmDgv.Show();
                    this.Hide();
                }
            }
        }

        //private void InitDataSet()
        //{
        //    pageSize = 8;                 //设置页面行数
        //    nMax = dtJhzy.Rows.Count;          //总记录数
        //    pageCount = (nMax / pageSize);     //计算出总页数

        //    if ((nMax % pageSize) > 0) pageCount++;
        //    pageCurrent = 1;               //当前页号从1开始
        //    nCurrent = 0;                  //当前记录行从0开始

        //    LoadData();

        //}

        //private void LoadData()
        //{
        //    int nStartPos = 0;            //当前页面开始记录行
        //    int nEndPos = 0;              //当前页面结束记录行

        //    DataTable dtview = dtJhzy.Clone();

        //    if (pageCount == 0)
        //    {
        //        nEndPos = 0;
        //    }
        //    else if (pageCurrent == pageCount)
        //    {
        //        nEndPos = nMax;
        //    }
        //    else
        //    {
        //        nEndPos = pageSize * pageCurrent;

        //    }
        //    nStartPos = nCurrent;

        //    for (int i = nStartPos; i < nEndPos; i++)
        //    {
        //        dtview.ImportRow(dtJhzy.Rows[i]);
        //    }
        //    dgSxChange.DataSource = dtview;
        //}

        private void dgSxChange_CurrentCellChanged(object sender, EventArgs e)
        {
            curRow = dgSxChange.CurrentCell.RowNumber;
            dgSxChange.Select(curRow);
        }

        public void CheckCellColor(object sender, DataGridEnableEventArgs e)
        {
            if (dgSxChange.CurrentCell.RowNumber == e.Row && curRow != -1)
                e.MeetsCriteria = false;
            else
            {
                if (dtJhzy.Rows[e.Row]["KJH"].ToString() == Global.curFrame.CH)
                {
                    e.MeetsCriteria = true;
                    e.BackColor = Color.Blue;
                    e.ForeColor = SystemColors.WindowText;
                }
                else
                {
                    e.MeetsCriteria = true;
                    e.BackColor = Color.Cornsilk;
                    e.ForeColor = SystemColors.WindowText;
                }
            }
        }

        private void btnLeave_Click(object sender, EventArgs e)
        {
            if (Global.sDebug == "False")
            {
                frmMessage frmMessage = new frmMessage();
                frmMessage.ShowDialog(Global.curFrame.QY + Global.curFrame.CH +"是否已经离开停车位？", "选择", "确定", "取消");
                if (frmMessage.ret == true)
                {
                    string data = Global.sUserId + ";";
                    data += System.DateTime.Now.ToString("yyyyMMddHHmmss") + ";";
                    data += Global.sBb + ";";
                    data += Global.sZyq + ";";
                    data += Global.curFrame.TCH;
                    Business.SendText(Business.msg.Package("ZDUA08", data));//单车作业开始
                }
                frmMessage.Dispose();
            }
        }

        private void frmSxChange_Activated(object sender, EventArgs e)
        {
            if (!ScanCodeRemapping.NormalTable.Contains(0x003A))
            {
                ScanCodeRemapping.NormalTable.Add(0x003A, new RemappingTable.Remapping(VirtualKey.VK_F24, Function.SendCode));
            }
        }


    }
}