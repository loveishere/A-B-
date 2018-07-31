#define InterfaceTest

using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Collections;
using PsionTeklogix.Keyboard;
using PsionTeklogix.Keyboard.NoChording;

namespace hStore.Forms
{
    public partial class frmDgv2 : Form
    {
        public frmDgv2()
        {
           //板柸入库
            InitializeComponent();
            //changebyyang 20170225 下载文件存放位置
            AllCode.mbParent = new DDSkyDll.Net20.MessageBox();
            if (System.IO.Directory.Exists("\\Flash Disk"))
            {
                AllCode.stringBootDir = "\\Flash Disk";
            }
        }
        private CoilPoint curPoint;
        //private int maxCol=3;
        private Coil curcoil;

        DataTable dtFrame;
        private void frmDgv2_Load(object sender, EventArgs e)
        {
            FrameIn framein = new FrameIn(); ;

            // textBoxKJH.Text = framein.Text3;
            //string xx = framein.getKJHText();
            textBoxKJH.Text = Global.curFrame.KJH;

            LoadData();
        }
        private void LoadData()
        {
            Global.coils.Clear();

#if InterfaceTest
            newFrameLayout(6);
#else
            newFrameLayout(Global.curFrame.MaxRow);
#endif

            if (Global.curFrame.KZ == "重")//入库作业
            {
                //loadFrameLayout10(Global.curFrame.KJH);
            }

            loadFrameLayout20(Global.curFrame.KJH, Global.curFrame.KZ);

            curPoint = new CoilPoint(Global.curFrame.KJH, -1, -1);

            //PrintField();

            this.dataGrid2.DataSource = dtFrame;

            CheckCellEventHandler cellEvent = new CheckCellEventHandler(CheckCellColor);

            DataGridTableStyle tablestyle = new DataGridTableStyle();
            tablestyle.GridColumnStyles.Add(new ColumnStyle(0, "X", "X", 28, "", cellEvent));
            tablestyle.GridColumnStyles.Add(new ColumnStyle(1, "L", "L", 206, "", cellEvent));
            //tablestyle.GridColumnStyles.Add(new ColumnStyle(2, "R", "R", 103, "", cellEvent));

            this.dataGrid2.TableStyles.Add(tablestyle);

            if (Global.curFrame.MaxRow == 6)
            {
                SetGridRowHeight(dataGrid2, 0, 45);
                SetGridRowHeight(dataGrid2, 1, 45);
                SetGridRowHeight(dataGrid2, 2, 45);
                SetGridRowHeight(dataGrid2, 3, 40);
                SetGridRowHeight(dataGrid2, 4, 40);
                SetGridRowHeight(dataGrid2, 5, 40);
                SetGridRowHeight(dataGrid2, 6, 38);
                SetGridRowHeight(dataGrid2, 7, 38);
                dataGrid2.Invalidate();
            }
          
        }
        private void newFrameLayout(int maxRow)
        {
            dtFrame = new DataTable();

            DataColumn col1 = new DataColumn("X", typeof(string));
            DataColumn col2 = new DataColumn("L", typeof(string));
            //DataColumn col3 = new DataColumn("R", typeof(string));

            dtFrame.Columns.Add(col1);
            dtFrame.Columns.Add(col2);
            //dtFrame.Columns.Add(col3);

            for (int i = 0; i < maxRow; i++)
            {  
                DataRow row = dtFrame.NewRow();

                int count = i + 1;
              
                 
                row["X"] = count;
                row["L"] = "";
                //row["R"] = "";
                  
                dtFrame.Rows.Add(row); 
            }
        }
        private void CheckCellColor(object sender, DataGridEnableEventArgs e)
        {
            if (curPoint.row == e.Row && curPoint.col == e.Column && curPoint.row != -1)
            {
                e.MeetsCriteria = true;
                e.BackColor = Color.Orange;
                e.ForeColor = SystemColors.WindowText;
            }
            else
            {
                string clh = dtFrame.Rows[e.Row][e.Column].ToString();
                CoilPoint p = new CoilPoint(Global.curFrame.KJH, e.Row, e.Column);
                if (Global.coils.ContainsKey(p))//有占位信息
                {
                    if (Global.coils[p].scantime != "")//材料经过比对
                    {
                        if (Global.coils[p].qa != "")
                        {
                            e.MeetsCriteria = true;
                            e.BackColor = Color.BlueViolet;
                            e.ForeColor = SystemColors.WindowText;
                        }
                        else
                        {
                            if (Global.coils[p].scanflag == 1)//材料比对成功
                            {
                                e.MeetsCriteria = true;
                                e.BackColor = Color.PaleGreen;
                                e.ForeColor = SystemColors.WindowText;
                            }
                            else if (Global.coils[p].scanflag == 0)//材料比对失败
                            {
                                e.MeetsCriteria = true;
                                e.BackColor = Color.LightSalmon;
                                e.ForeColor = SystemColors.WindowText;
                            }
                        }
                    }
                    else
                    {
                        e.MeetsCriteria = true;
                        e.BackColor = Color.SkyBlue;
                        e.ForeColor = SystemColors.WindowText;
                    }
                }
                else
                {
                    e.MeetsCriteria = true;
                    e.BackColor = Color.Cornsilk;
                    if (e.Column == 0)
                    {
                        e.ForeColor = Color.Red;
                    }
                    else
                    {
                        e.ForeColor = SystemColors.WindowText;
                    }
                }
            }

        }

        private void addResult(string barcode, string text)//barcode条码材料号
        {
            if (text.Length == 7)//扫描凹槽条码
            {
                curPoint = new CoilPoint(Global.curFrame.KJH, text);
                dataGrid2.Invalidate();
            }
            else//扫描材料号
            {
                bool bval = false;

                foreach (KeyValuePair<CoilPoint, Coil> kv in Global.coils)//CoilPoint键，Coil值
                {
                    if (kv.Value.clh == barcode)
                    {
                        if (kv.Key.Equals(curPoint) == false)
                        {
                            bval = true;
                            curPoint = kv.Key;
                            dataGrid2.Invalidate();
                        }
                    }
                }
                if (bval == false)
                {
                    addCoil(curPoint, barcode);
                }
            }
        }


        private void loadFrameLayout20(string sFrameID, string kz)
        {

            string sql = "";
            if (kz == "重")
            {
                sql = "select Location,CLH,Relative_X,Relative_Y,CLH,RKB,SteelWide,SteelDiameter from FrameLayout where FrameID='" + sFrameID + "' and Mold='20' order by Relative_X";
            }
            else
            {
                sql = "select Location,CLH,Relative_X,Relative_Y,CLH,CKB,SteelWide,SteelDiameter from FrameLayoutExp where FrameID='" + sFrameID + "' order by Relative_X";
            }

            DataTable dt = SqlCe.ExecuteQuery(sql);
            if (kz == "重")
            {
                int[] colIndex = new int[Global.curFrame.MaxRow];
                for (int i = 0; i < Global.curFrame.MaxRow; i++)
                {
                    colIndex[i] = 0;
                }

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    int X = Convert.ToInt32(dt.Rows[i]["Relative_X"]);
                    int Y = Convert.ToInt32(dt.Rows[i]["Relative_Y"]);
                    int row = Y / Global.curFrame.Space;
                    int col = colIndex[row] + 1;
                    if (col <= 2)
                    {
                        CoilPoint p = new CoilPoint(sFrameID, row, col);
                        if (Global.coils.ContainsKey(p))
                        {
                            Global.coils[p].X = X;
                            Global.coils[p].Y = Y;
                            Global.coils[p].diameter = dt.Rows[i]["SteelDiameter"].ToString();
                            Global.coils[p].wide = dt.Rows[i]["SteelWide"].ToString();
                        }
                        else
                        {
                            Coil coil = new Coil(p, "");
                            coil.X = X;
                            coil.Y = Y;
                            coil.diameter = dt.Rows[i]["SteelDiameter"].ToString();
                            coil.wide = dt.Rows[i]["SteelWide"].ToString();
                            Global.coils.Add(p, coil);
                        }
                        colIndex[row] = col;
                    }


                }
            }
            else if (kz == "空" || kz == "拼")
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    int X = Convert.ToInt32(dt.Rows[i]["Relative_X"]);
                    int Y = Convert.ToInt32(dt.Rows[i]["Relative_Y"]);
                    string Location = dt.Rows[i]["Location"].ToString();
                    string clh = dt.Rows[i]["CLH"].ToString();
                    string kb = dt.Rows[i]["CKB"].ToString();
                    CoilPoint p = new CoilPoint(sFrameID, Location);
                    if (clh.Length > 8)
                    {
                        dtFrame.Rows[p.row][p.col] = clh.Substring(clh.Length - 8, 8);
                    }
                    else
                    {
                        dtFrame.Rows[p.row][p.col] = clh;
                    }
                    //if (Global.coils.ContainsKey(p))
                    //{
                    //    Global.coils[p].X = X;
                    //    Global.coils[p].Y = Y;
                    //    Global.coils[p].clh = clh;
                    //    Global.coils[p].kb = kb;
                    //    Global.coils[p].diameter = dt.Rows[i]["SteelDiameter"].ToString();
                    //    Global.coils[p].wide = dt.Rows[i]["SteelWide"].ToString();
                    //}
                    //else
                    if (Global.coils.ContainsKey(p) == false)
                    {
                        Coil coil = new Coil(p, "");
                        coil.X = X;
                        coil.Y = Y;
                        coil.clh = clh;
                        coil.kb = kb;
                        coil.diameter = dt.Rows[i]["SteelDiameter"].ToString();
                        coil.wide = dt.Rows[i]["SteelWide"].ToString();
                        Global.coils.Add(p, coil);
                    }
                }
            }
            dt.Dispose();
        }


        public static void SetGridRowHeight(DataGrid dg, int nRow, int cy)
        {
            ArrayList arrRows = (ArrayList)dg.GetType().GetField("m_rlrow",
                            BindingFlags.NonPublic |
                            BindingFlags.Static |
                            BindingFlags.Instance).GetValue(dg);
            object row = arrRows[nRow];
            row.GetType().GetField("m_cy",
                             BindingFlags.NonPublic |
                             BindingFlags.Static |
                             BindingFlags.Instance).SetValue(row, cy);
        }

        private void txtClh_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                lblClh.Visible = false;
                txtClh.Visible = false;
                return;
            }

            if (e.KeyCode >= Keys.A && e.KeyCode <= Keys.Z)
            {
                txtClh.Text = txtClh.Text.ToUpper();
                txtClh.SelectionStart = txtClh.Text.Length;
            }

            if (e.KeyCode == Keys.Enter)
            {
                if (txtClh.Text.Length >= 9)
                {
                    addResult(txtClh.Text, "");
                    //changeByYang20170526
                }
                txtClh.Text = "";
                lblClh.Visible = false;
                txtClh.Visible = false;
            }

            if (txtClh.Text.Length >= 5)
            {
                string sql = "";

                if (Global.curFrame.KZ == "重")//入库作业
                {
                   //匹配后5位
                    sql = "SELECT CLH FROM ExpSlabPlan WHERE (CLH LIKE '%" + txtClh.Text + "') ";
                }
                else
                {
                    sql = "SELECT CLH FROM ExpSlabPlan WHERE (CLH LIKE '%" + txtClh.Text + "') ";
                }

                DataTable dt = SqlCe.ExecuteQuery(sql);
                if (dt.Rows.Count == 1)
                {
                    txtClh.Text = dt.Rows[0]["CLH"].ToString();
                    txtClh.SelectionStart = txtClh.Text.Length;
                }
                else if (dt.Rows.Count > 1)
                {
                    lstClh.Items.Clear();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        lstClh.Items.Add(dt.Rows[i]["CLH"].ToString());
                    }
                    lstClh.Visible = true;
                    //yt20170526
                 
                }
                dt.Dispose();
            }
        }


        private void deleteCoil(CoilPoint p)
        {
            Global.coils.Remove(p);

            dtFrame.Rows[p.row][p.col] = "";
            dataGrid2.DataSource = dtFrame;
        }

        private void addCoil(CoilPoint p, string text)
        {
            if (p.row >= 0)
            {
                if (Global.coils.ContainsKey(p))
                {
                    if (Global.coils[p].clh != "")
                    {
                        frmMessage frmMessage = new frmMessage();
                        if (Global.coils[p].clh != text)
                        {
                            frmMessage.ShowDialog("当前位置已经存在板坯是否取消？", "提示", "确定");
                            if (frmMessage.ret == true)
                            {
                                deleteCoil(p);
                            }
                            doScanSesult(Global.curFrame.KZ, p, text, DateTime.Now.ToString("yyyyMMddHHmmss"));
                        }
                        else
                        {
                            frmMessage.ShowDialog("是否取消当前位置上的板坯？", "选择", "是", "否");
                            if (frmMessage.ret == true)
                            {
                                deleteCoil(p);
                            }
                        }
                        frmMessage.Dispose();
                    }
                    else
                    {
                        doScanSesult(Global.curFrame.KZ, p, text, DateTime.Now.ToString("yyyyMMddHHmmss"));
                    }
                }
                else
                {
                    doScanSesult(Global.curFrame.KZ, p, text, DateTime.Now.ToString("yyyyMMddHHmmss"));

                }
            }
        }

        private void doScanSesult(string kz, CoilPoint p, string text, string scantime)
        {
            DataTable dt = new DataTable();

            if (Global.curFrame.KZ == "重")
            {

                dt = SearchImpCoil(text);
                ScanImpCoilResult(p, text, scantime, dt);
            }
            else if (Global.curFrame.KZ == "空" || Global.curFrame.KZ == "拼")
            {
                dt = SearchExpCoil(text);
                ScanExpCoilResult(p, text, scantime, dt);
            }

            dt.Dispose();
        }

        private DataTable SearchImpCoil(string text)
        {
            DataTable dt = new DataTable();

            string sql = "select CLH,Make,QA from ImportStorageAcceptOrder where (WCFlag is null or WCFlag<>2) and RKB='" + Global.sKb + "' ";
            if (text.Length == 9 || text.Length == 10)
            {
                sql += "and (CLH2='" + text + "' or ( len(CLH2)=11 and charindex('" + text + "',CLH2)>0 ) ) ";
            }
            else
            {
                sql += "and CLH2='" + text + "'";
            }

            dt = SqlCe.ExecuteQuery(sql);

            return dt;

        }

        private DataTable SearchExpCoil(string text)
        {
            DataTable dt = new DataTable();

            string sql = "select CLH,Make,QA from ExportStorageAcceptOrder where (WCFlag is null or WCFlag<>2) and CKB='" + Global.sKb + "' ";
            if (text.Length == 9 || text.Length == 10)
            {
                sql += "and (CLH2='" + text + "' or ( len(CLH2)=11 and charindex('" + text + "',CLH2)>0 ) ) ";
            }
            else
            {
                sql += "and CLH2='" + text + "'";
            }

            dt = SqlCe.ExecuteQuery(sql);

            return dt;

        }

        private void ScanImpCoilResult(CoilPoint p, string text, string scantime, DataTable dt)
        {
            curcoil = new Coil(p, text);
            int scanflag = 0;
            string zzdy = "";

            if (dt.Rows.Count > 0)
            {
                scanflag = 1;
                zzdy = dt.Rows[0]["Make"].ToString();

            }

            if (Global.coils.ContainsKey(p))
            {
                Global.coils[p].scanflag = scanflag;
                Global.coils[p].scantime = scantime;
                Global.coils[p].clh = text;
                Global.coils[p].zzdy = zzdy;
            }
            else
            {
                curcoil.scanflag = scanflag;
                curcoil.scantime = scantime;
                curcoil.zzdy = zzdy;
                Global.coils.Add(p, curcoil);
            }
            if (text.Length > 8)
            {
                //材料号只显示8位
                //dtFrame.Rows[p.row][p.col] = text.Substring(text.Length - 8, 8);
                dtFrame.Rows[p.row][p.col] = text;

            }
            else
            {
                dtFrame.Rows[p.row][p.col] = text;
            }
            dataGrid2.DataSource = dtFrame;
            if (scanflag == 1)
            {
                PsionTeklogix.Sound.Beeper.Beeper.PlayTone(5000, 500, 100);
                string sql = "update importstorageacceptorder set scantime='" + scantime + "', wcflag=1 where clh2='" + text + "'";
                SqlCe.ExecuteNonQuery(sql);
            }

        }

        private void ScanExpCoilResult(CoilPoint p, string text, string scantime, DataTable dt)
        {
            //curcoil = new Coil(p, text);

            int scanflag = 0;
            string zzdy = ""; //制造单元

            if (dt.Rows.Count > 0)
            {
                scanflag = 1;
                zzdy = dt.Rows[0]["Make"].ToString();

            }

            if (Global.coils.ContainsKey(p))
            {
                Global.coils[p].scanflag = scanflag;
                Global.coils[p].scantime = scantime;
                //Global.coils[p].clh = text;
                Global.coils[p].zzdy = zzdy; 
            }
            //else
            //{
            //    curcoil.scanflag = scanflag;
            //    curcoil.scantime = scantime;
            //    curcoil.zzdy = zzdy;
            //    Global.coils.Add(p, curcoil);
            //}
            //if (text.Length > 8)
            //{
            //    dtFrame.Rows[p.row][p.col] = text.Substring(text.Length - 8, 8);
            //}
            //else
            //{
            //    dtFrame.Rows[p.row][p.col] = text;
            //}
            //dataGrid1.DataSource = dtFrame;
            dataGrid2.Invalidate();
            if (scanflag == 1)
            {
                PsionTeklogix.Sound.Beeper.Beeper.PlayTone(5000, 500, 100);
                string sql = "update exportstorageacceptorder set scantime='" + scantime + "', wcflag=1 where clh2='" + text + "'";
                SqlCe.ExecuteNonQuery(sql);
            }

        }

        private void dataGrid2_DoubleClick(object sender, EventArgs e)
        {
            //changebyYang 20170711 
            int rowNum = this.dataGrid2.CurrentCell .RowNumber ;
            string rowText = this.dataGrid2[rowNum, 1].ToString ();
            txtClh.Visible = true;
            lblClh.Visible = true;
            if (rowText.CompareTo ("")!=0)
            {
                txtClh.Text = rowText;
            }
            txtClh.Focus();
          
        }

        private void dataGrid2_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void dataGrid2_Click(object sender, EventArgs e)
        {
            if (dataGrid2.CurrentCell.Equals(null)) return;

            if (dataGrid2.CurrentCell.ColumnNumber > 0)
            {
                curPoint = new CoilPoint(Global.curFrame.KJH, dataGrid2.CurrentCell.RowNumber, dataGrid2.CurrentCell.ColumnNumber);
                if (dtFrame.Rows[curPoint.row][curPoint.col].ToString() != "")
                {
                    curcoil = Global.coils[curPoint];
                }
                else
                {
                    curcoil = null;
                }
            }
            dataGrid2.Invalidate();
        }

        //private void dataGrid2_DoubleClick(object sender, EventArgs e)
        //{
        //    txtClh.Visible = true;
        //    lblClh.Visible = true;
        //    txtClh.Focus();
        //}

        private void lstClh_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtClh.Text = lstClh.Items[lstClh.SelectedIndex].ToString();
            txtClh.SelectionStart = txtClh.Text.Length;
            txtClh.Focus();
            lstClh.Visible = false;
        }

        private void frmDgv2_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F21)//环形键
            {
                if (Global.curFrame.KZ == "重")//入库作业
                {
                    string data = Global.sUserId + AllCode.stringInterfaceChar;
                    data += System.DateTime.Now.ToString("yyyyMMddHHmmss") + AllCode.stringInterfaceChar;
                    data += Global.sBb + AllCode.stringInterfaceChar;//班别
                    data += Global.sZyq + AllCode.stringInterfaceChar;//作业区
                    data += Global.curFrame.TCH + AllCode.stringInterfaceChar;//停车号
                    data += Global.curFrame.QY + Global.curFrame.CH + AllCode.stringInterfaceChar;//车号

                    int cnt = 0;
                    string msg = "";

                    foreach (KeyValuePair<CoilPoint, Coil> kv in Global.coils)
                    {
                        Coil c = kv.Value;
                        if (c.clh != "")
                        {
                            msg += c.p.barcode.Substring(c.p.barcode.Length - 3, 2) + AllCode.stringInterfaceChar;                  //层号
                           // msg += c.p.barcode + AllCode.stringInterfaceChar;                  //层号
                            //msg += c.zzdy + AllCode.stringInterfaceChar;                        //制造单元
                            msg += c.clh + AllCode.stringInterfaceChar;                        //材料号
                            //msg += c.scantime + AllCode.stringInterfaceChar;                  //扫描时间
                           // msg += c.qa + AllCode.stringInterfaceChar;                         //质量代码
                           // msg += c.scanflag.ToString() + AllCode.stringInterfaceChar;       //处理标志
                           // msg += c.wide + AllCode.stringInterfaceChar;                   //钢卷宽度
                            //msg += c.diameter + AllCode.stringInterfaceChar;                   //钢卷内径

                            cnt++;
                        }

                    }

                    if (cnt > 0)
                    {
                        data += cnt.ToString() + AllCode.stringInterfaceChar;//材料数量
                        if (msg.Length > 0) msg = msg.Substring(0, msg.Length - 1);
                        data += msg;
                        data += AllCode.stringInterfaceChar;

                        if (Global.sDebug == "False")
                        {
                            frmMessage frmMessage = new frmMessage();
                            frmMessage.ShowDialog("是否上传入库手持扫描结果？", "选择", "确定", "取消");
                            if (frmMessage.ret == true)
                            {
                                Business.SendText(Business.msg.Package("ZDUA17", data));//入库手持扫描完成
                            }
                            frmMessage.Dispose();
                        }

                    }
                }
                else//出库作业
                {
                    string data = Global.sUserId + ";";
                    data += System.DateTime.Now.ToString("yyyyMMddHHmmss") + ";";
                    data += Global.sBb + ";";
                    data += Global.sZyq + ";";
                    data += Global.curFrame.TCH + ";";
                    data += Global.curFrame.QY + Global.curFrame.CH + ";";

                    int cnt = 0;
                    string msg = "";

                    foreach (KeyValuePair<CoilPoint, Coil> kv in Global.coils)
                    {
                        Coil c = kv.Value;
                        if (c.clh != "")
                        {
                            msg += c.p.barcode + ";";                   //逻辑位置
                            msg += c.zzdy + ";";                        //制造单元
                            msg += c.clh + ";";                         //材料号
                            msg += c.scantime + ";";                    //扫描时间
                            msg += c.qa + ";";                          //质量代码
                            msg += c.wide + ";";                        //钢卷宽度
                            msg += c.diameter + ";";                    //钢卷内径

                            cnt++;
                        }
                    }

                    if (cnt > 0)
                    {
                        data += cnt.ToString() + ";";
                        if (msg.Length > 0) msg = msg.Substring(0, msg.Length - 1);
                        data += msg;

                        if (Global.sDebug == "False")
                        {
                            frmMessage frmMessage = new frmMessage();
                            frmMessage.ShowDialog("是否上传出库手持扫描结果？", "选择", "确定", "取消");
                            if (frmMessage.ret == true)
                            {
                                Business.SendText(Business.msg.Package("ZDUA07", data));//出库手持扫描完成
                            }
                            frmMessage.Dispose();
                        }


                    }
                }

                if (Global.curFrame.KZ == "重")
                {
                    frmSxChange frmSxChange = new frmSxChange();
                    Global.frmCurrent = frmSxChange;
                    frmSxChange.Owner = this;
                    frmSxChange.Show();
                    this.Hide();
                }
            }
            else if (e.KeyCode == Keys.Escape)
            {
                Global.frmCurrent = this.Owner;
                this.Owner.Show();
                this.Owner = null;
                this.Close();
            }
            else if (e.KeyCode == Keys.Tab)
            {
                if (Global.curFrame.KZ == "重")
                {
                    frmSxChange frmSxChange = new frmSxChange();
                    Global.frmCurrent = frmSxChange;
                    frmSxChange.Owner = this;
                    frmSxChange.Show();
                    this.Hide();
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
            else if (e.KeyCode == Keys.F22 || e.KeyCode == Keys.F23)
            {
                if (curPoint.row >= 0)
                {
                    if (Global.coils.ContainsKey(curPoint))
                    {
                        Coil coil = Global.coils[curPoint];
                        string clh = coil.clh;
                        string qa = coil.qa;
                        if (clh != "")
                        {
                            frmQuality frmQuality = new frmQuality();
                            Global.frmCurrent = frmQuality;
                            frmQuality.Owner = this;
                            this.Hide();
                            frmQuality.Show(clh, qa, curPoint);
                        }

                        coil = null;

                    }
                }

            }
        }

        private void frmDgv2_Activated(object sender, EventArgs e)
        {
            if (ScanCodeRemapping.NormalTable.Contains(0x003A))
            {
                ScanCodeRemapping.NormalTable.Remove(0x003A);
            }
        }

        //计划号下载
        string stringSlabPlanDownLoadDataFile = "";
        private void downLoad_Click(object sender, EventArgs e)
        {
           // string strPlanNum = "20201";

            //txtJhh.Text = txtJhh.Text.ToUpper();
            //txtJhh.SelectionStart = txtJhh.Text.Length;
            string strPlanNum = txtJhh.Text;
            if (strPlanNum.CompareTo("") == 0 || strPlanNum.Length!=5)
            {
                frmMessage frmMessage = new frmMessage();
                frmMessage.ShowDialog("请输入计划号后5位！", "提示", "确定");
                frmMessage.Dispose();
            }
            else
            {
              

                try
                {
                    //if (strPlanNum.Length!=5)
                    //{
                    //    AllCode.mbParent.Show(string.Format("输入计划号后5位"), "错误", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);

                    //}
                    string[] ServiceSlabYardData = AllCode.serviceSlabYard.GetPlanMaterial(strPlanNum);

                    // string[] ServiceSlabPlanData={"1234566666","11111166666","2222266666"};
                    string[] strArrSlabPlanData = ServiceSlabYardData;
                    if (strArrSlabPlanData.Length == 0)
                    {
                        frmMessage frmMessage = new frmMessage();
                        frmMessage.ShowDialog("该计划无材料", "提示", "确定");
                        frmMessage.Dispose();
                    }
                    else 
                    {
                        frmMessage frmMessage = new frmMessage();
                        frmMessage.ShowDialog("下载成功", "提示", "确定");
                        frmMessage.Dispose();

                    }
                    for (int inta = 0; inta < strArrSlabPlanData.Length; inta++)
                    {
                        //changeByYang20170205 板坯材料号保存
                        SaveCurrentSlabMaterials(strPlanNum, strArrSlabPlanData[inta]);

                    }
                }
                catch (Exception e1)
                {
                    System.Diagnostics.Debug.WriteLine(e1.Message);
                    frmMessage frmMessage = new frmMessage();
                    frmMessage.ShowDialog(e1.Message, "提示", "确定");
                    frmMessage.Dispose();
                }
            }
         
            ///////////////////////////
           
            //List<string[]> test = new List<string[]>();
            ////缓存web查询信息
            //test.Clear();
            //String[] str1 = { "1", "23123123", "abc" };
            //String[] str2 = { "2", "23456789", "abc" };
            //String[] str3 = { "3", "11111111", "abc" };
            //test.Add(str1);
            //test.Add(str2);
            //test.Add(str3);
            //int countX = test.Count;
            //label4.Text = "6";
            //label6.Text = countX.ToString();
            //for (int i = 0; i < countX; i++)
            //{
            //    int j = Convert.ToInt32(test[i][0]);
            //    curPoint = new CoilPoint(Global.curFrame.KJH, j, 1);
            //    addCoil(curPoint, test[i][1]);
            //}
               
          
        }


        //changeByYang20170205 板坯材料号保存
        public static void SaveCurrentSlabMaterials(string jhh, string clh)
        {
            string sql2 = "select * from ExpSlabPlan where CLH= '" + clh + "' and   JHH= '" + jhh + "' ";
            DataTable dt = SqlCe.ExecuteQuery(sql2);
            if (dt.Rows.Count == 0)
            { 
                    string sql = "insert into ExpSlabPlan (JHH,CLH) values ('" + jhh + "','" + clh + "')";
                    SqlCe.ExecuteNonQuery(sql);
            }
            
            //string sql2 = "select CLH from ExpSlabPlan  where CLH= '" + clh + "'";
            //DataTable dt = SqlCe.ExecuteQuery(sql2);
            dt.Dispose();
           
        }

        //计划号
        private void txtJhh_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode >= Keys.A && e.KeyCode <= Keys.Z)
            {
                txtJhh.Text = txtJhh.Text.ToUpper();
                txtJhh.SelectionStart = txtJhh.Text.Length;
            }
        }


       
    }
}