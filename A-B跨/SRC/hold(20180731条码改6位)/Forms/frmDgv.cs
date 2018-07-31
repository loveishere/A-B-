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
    public partial class frmDgv : Form
    {
        public frmDgv()
        {
            InitializeComponent();
        }

        private CoilPoint curPoint;
        //private int maxCol=3;
        private Coil curcoil;

        DataTable dtFrame;

        private void frmDgv_Load(object sender, EventArgs e)
        {

            LoadData();
         }

        private void ShowCoils()
        {
            //foreach (KeyValuePair<CoilPoint, Coil> kv in Global.coils)
            //{
            //    Coil c = kv.Value;
            //    CoilPoint p = kv.Key;

            //    if (c.clh.Length > 8)
            //    {
            //        dtFrame.Rows[p.row][p.col] = c.clh.Substring(c.clh.Length - 8, 8);
            //    }
            //    else
            //    {
            //        dtFrame.Rows[p.row][p.col] = c.clh;
            //    }

            //}
            dataGrid1.DataSource = dtFrame;
        }

        private void LoadData()
        {
            Global.coils.Clear();

#if InterfaceTest
            newFrameLayout(10);
#else
            newFrameLayout(Global.curFrame.MaxRow);
#endif

            if (Global.curFrame.KZ == "重")//入库作业
            {
                //loadFrameLayout10(Global.curFrame.KJH);
            }

            loadFrameLayout20(Global.curFrame.KJH,Global.curFrame.KZ);

            curPoint = new CoilPoint(Global.curFrame.KJH, -1, -1);

            //PrintField();

            this.dataGrid1.DataSource = dtFrame;

            CheckCellEventHandler cellEvent = new CheckCellEventHandler(CheckCellColor);

            DataGridTableStyle tablestyle = new DataGridTableStyle();
            tablestyle.GridColumnStyles.Add(new ColumnStyle(0, "X", "X", 28, "", cellEvent));
            tablestyle.GridColumnStyles.Add(new ColumnStyle(1, "L", "L",103, "", cellEvent));
            tablestyle.GridColumnStyles.Add(new ColumnStyle(2, "R", "R", 103, "", cellEvent));

            this.dataGrid1.TableStyles.Add(tablestyle);

            if (Global.curFrame.MaxRow == 8)
            {
                SetGridRowHeight(dataGrid1, 0, 38);
                SetGridRowHeight(dataGrid1, 1, 38);
                SetGridRowHeight(dataGrid1, 2, 38);
                SetGridRowHeight(dataGrid1, 3, 38);
                SetGridRowHeight(dataGrid1, 4, 38);
                SetGridRowHeight(dataGrid1, 5, 38);
                SetGridRowHeight(dataGrid1, 6, 38);
                SetGridRowHeight(dataGrid1, 7, 38);
                dataGrid1.Invalidate();
            }
            else if (Global.curFrame.MaxRow == 9)
            {
                SetGridRowHeight(dataGrid1, 0, 34);
                SetGridRowHeight(dataGrid1, 1, 33);
                SetGridRowHeight(dataGrid1, 2, 34);
                SetGridRowHeight(dataGrid1, 3, 33);
                SetGridRowHeight(dataGrid1, 4, 34);
                SetGridRowHeight(dataGrid1, 5, 33);
                SetGridRowHeight(dataGrid1, 6, 34);
                SetGridRowHeight(dataGrid1, 7, 34);
                SetGridRowHeight(dataGrid1, 8, 34);
                dataGrid1.Invalidate();
            }
            else if (Global.curFrame.MaxRow == 10)
            {
                SetGridRowHeight(dataGrid1, 0, 31);
                SetGridRowHeight(dataGrid1, 1, 30);
                SetGridRowHeight(dataGrid1, 2, 31);
                SetGridRowHeight(dataGrid1, 3, 30);
                SetGridRowHeight(dataGrid1, 4, 30);
                SetGridRowHeight(dataGrid1, 5, 30);
                SetGridRowHeight(dataGrid1, 6, 30);
                SetGridRowHeight(dataGrid1, 7, 30);
                SetGridRowHeight(dataGrid1, 8, 30);
                SetGridRowHeight(dataGrid1, 9, 30);
                dataGrid1.Invalidate();
            }
            else if (Global.curFrame.MaxRow == 12)
            {
                SetGridRowHeight(dataGrid1, 0, 25);
                SetGridRowHeight(dataGrid1, 1, 25);
                SetGridRowHeight(dataGrid1, 2, 25);
                SetGridRowHeight(dataGrid1, 3, 25);
                SetGridRowHeight(dataGrid1, 4, 25);
                SetGridRowHeight(dataGrid1, 5, 25);
                SetGridRowHeight(dataGrid1, 6, 25);
                SetGridRowHeight(dataGrid1, 7, 25);
                SetGridRowHeight(dataGrid1, 8, 25);
                SetGridRowHeight(dataGrid1, 9, 25);
                SetGridRowHeight(dataGrid1, 10, 25);
                SetGridRowHeight(dataGrid1, 11, 25);
                dataGrid1.Invalidate();
            }
        }

        private void newFrameLayout(int maxRow)
        {
            dtFrame = new DataTable();

            DataColumn col1 = new DataColumn("X", typeof(string));
            DataColumn col2 = new DataColumn("L", typeof(string));
            DataColumn col3 = new DataColumn("R", typeof(string));

            dtFrame.Columns.Add(col1);
            dtFrame.Columns.Add(col2);
            dtFrame.Columns.Add(col3);

            for (int i = 0; i < maxRow; i++)
            {
                DataRow row = dtFrame.NewRow();

                int count = i + 1;
                row["X"] = count;
                row["L"] = "";
                row["R"] = "";
                dtFrame.Rows.Add(row);
            }
        }

        //加载末端库框架配载图
        private void loadFrameLayout10(string sFrameID)
        {
            string sql = "select Location,CLH,RKB,SteelDiameter from FrameLayout where FrameID='" + sFrameID + "' and Mold='10'";
            DataTable dt = SqlCe.ExecuteQuery(sql);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["Location"].ToString().Length==7)
                {
                    string clh = dt.Rows[i]["CLH"].ToString();
                    if (!clh.Equals(""))
                    {
                        CoilPoint p = new CoilPoint(sFrameID, dt.Rows[i]["Location"].ToString());
                        if (clh.Length > 8)
                        {
                            dtFrame.Rows[p.row][p.col] = clh.Substring(clh.Length - 8, 8);
                        }
                        else
                        {
                            dtFrame.Rows[p.row][p.col] = clh;
                        }
                        Coil coil = new Coil(p, clh);
                        coil.kb = dt.Rows[i]["RKB"].ToString();
                        coil.diameter = dt.Rows[i]["SteelDiameter"].ToString();
                        Global.coils.Add(p, coil);
                    }
                }
            }
            dt.Dispose();
        }

        //加载激光扫描框架配载图
        private void loadFrameLayout20(string sFrameID,string kz)
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
                    string kb=dt.Rows[i]["CKB"].ToString();
                    CoilPoint p = new CoilPoint(sFrameID,Location);
                    if (clh.Length > 8)
                    {
                        dtFrame.Rows[p.row][p.col] = clh.Substring(clh.Length-8,8);
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
                    if(Global.coils.ContainsKey(p)==false)
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


        private void clearCoils()
        {
            Global.coils.Clear();
            curcoil = null;

            dtFrame = new DataTable();
            for (int i = 0; i < Global.curFrame.MaxRow; i++)
            {
                DataRow row = dtFrame.NewRow();

                int count = i + 1;
                row["X"] = count;
                row["L"] = "";
                row["R"] = "";
                dtFrame.Rows.Add(row);
            }
            this.dataGrid1.DataSource = dtFrame;
            dataGrid1.Invalidate();
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

        private void dataGrid1_Click(object sender, EventArgs e)
        {
            
            if (dataGrid1.CurrentCell.Equals(null)) return;

            if (dataGrid1.CurrentCell.ColumnNumber > 0)
            {
                curPoint = new CoilPoint(Global.curFrame.KJH, dataGrid1.CurrentCell.RowNumber, dataGrid1.CurrentCell.ColumnNumber);
                if (dtFrame.Rows[curPoint.row][curPoint.col].ToString() != "")
                {
                    curcoil = Global.coils[curPoint];
                }
                else
                {
                    curcoil = null;
                }
            }
            dataGrid1.Invalidate();

        }
         
        private void deleteCoil(CoilPoint p)
        {
            Global.coils.Remove(p);
            
            dtFrame.Rows[p.row][p.col] = "";
            dataGrid1.DataSource = dtFrame;
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
                            //frmMessage.ShowDialog("当前位置已经存在钢卷！", "提示", "确定");
                            frmMessage.ShowDialog("是否取消当前位置上的钢卷？", "选择", "是", "否");
                            if (frmMessage.ret == true)
                            {
                                deleteCoil(p);
                            }
                        }
                        else
                        {
                            frmMessage.ShowDialog("当前位置已经存在钢卷！", "提示", "确定");
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

        private void doScanSesult(string kz,CoilPoint p,string text,string scantime)
        {
            DataTable dt=new DataTable();

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
                dtFrame.Rows[p.row][p.col] = text.Substring(text.Length-8,8);
            }
            else
            {
                dtFrame.Rows[p.row][p.col] = text;
            }
            dataGrid1.DataSource = dtFrame;
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
            dataGrid1.Invalidate();
            if (scanflag == 1)
            {
                PsionTeklogix.Sound.Beeper.Beeper.PlayTone(5000, 500, 100);
                string sql = "update exportstorageacceptorder set scantime='" + scantime + "', wcflag=1 where clh2='" + text + "'";
                SqlCe.ExecuteNonQuery(sql);
            }

        }

        private void setCurrentPoint(CoilPoint p)
        {
            curPoint = p;
            dataGrid1.Invalidate();
        }


        public void scanner_ScanCompleteEvent(string text)
        {
            string barcode = text;

            if (barcode.StartsWith("L")) barcode = barcode.Substring(1, barcode.Length - 1);

            if (barcode.StartsWith("S")) barcode = barcode.Substring(1, barcode.Length - 1);//新日铁热卷

            barcode = barcode.Replace("-", "");//湛江冷卷

            bool bval = false;

            if (text.Length < 7) bval = true;

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

            

            addResult(barcode,text);

        }

        private void addResult(string barcode, string text)
        {
            if (text.Length == 7)//扫描凹槽条码
            {
                curPoint = new CoilPoint(Global.curFrame.KJH, text);
                dataGrid1.Invalidate();
            }
            else//扫描材料号
            {
                bool bval = false;

                foreach (KeyValuePair<CoilPoint, Coil> kv in Global.coils)
                {
                    if (kv.Value.clh == barcode)
                    {
                        if (kv.Key.Equals(curPoint)==false)
                        {
                            bval = true;
                            curPoint = kv.Key;
                            dataGrid1.Invalidate();
                        }
                    }
                }
                if (bval == false)
                {
                    addCoil(curPoint, barcode);
                }
            }
        }

        public static void SetGridDefaultRowHeight(DataGrid dg, int cy)
        {
            FieldInfo fi = dg.GetType().GetField("m_cyRow",
                            BindingFlags.NonPublic |
                            BindingFlags.Static |
                            BindingFlags.Instance);
            fi.SetValue(dg, cy);
            dg.GetType().GetMethod("_DataRebind",
                             BindingFlags.NonPublic |
                             BindingFlags.Static |
                             BindingFlags.Instance).Invoke(dg, new object[] { });
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

        public void PrintField()
        {
            System.Reflection.FieldInfo[] fis = typeof(DataGrid).GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
            string str = "";
            foreach (var fi in fis)
            {
                str+=fi.Name + ":" + fi.FieldType.ToString()+"\r\n";
                System.Diagnostics.Debug.WriteLine(fi.Name + ":" + fi.FieldType.ToString() + "\r\n");
            }            
            //MessageBox.Show(str);           
        }

        private void dataGrid1_KeyUp(object sender, KeyEventArgs e)
        {
            /*
            if (e.KeyCode == Keys.Up)    //上移
            {
                if (curcoil.Equals(null)) return;
                else
                {
                    if (curPoint.row > 0)
                    {
                        CoilPoint p = new CoilPoint(curPoint.frameid,curPoint.row - 1, curPoint.col);


                        if (dtFrame.Rows[p.row][p.col].ToString() == "")
                        {
                            moveCoil(p);

                        }
                    }
                }
            }
            else if (e.KeyCode == Keys.Down)   //下移
            {
                if (curcoil.Equals(null)) return;
                else
                {
                    if (curPoint.row < Global.curFrame.MaxRow - 1)
                    {
                        CoilPoint p = new CoilPoint(curPoint.frameid, curPoint.row + 1, curPoint.col);
                        if (dtFrame.Rows[p.row][p.col].ToString() == "")
                        {
                            moveCoil(p);
                        }
                    }
                }
            }
            else if (e.KeyCode == Keys.Left)   //左移
            {
                if (curcoil.Equals(null)) return;
                else
                {
                    if (curPoint.col > 1)
                    {
                        CoilPoint p = new CoilPoint(curPoint.frameid, curPoint.row, curPoint.col - 1);

                        if (dtFrame.Rows[p.row][p.col].ToString() == "")
                        {
                            moveCoil(p);
                        }

                    }
                }
            }
            else if (e.KeyCode == Keys.Right)    //右移
            {
                if (curcoil.Equals(null)) return;
                else
                {
                    if (curPoint.col < maxCol-1)
                    {
                        CoilPoint p = new CoilPoint(curPoint.frameid, curPoint.row, curPoint.col + 1);

                        if (dtFrame.Rows[p.row][p.col].ToString() == "")
                        {
                            moveCoil(p);
                        }                       
                    }
                }
            }
            */ 
        }

        /*
        private void moveCoil(CoilPoint p)
        {
            if (curcoil == null) return;

            if (isFramePosBusi(p)) return;
            
            deleteCoil(curcoil.p);
            curcoil.move(p);

            addCoil(p, curcoil.clh);
        }

        //判断当前位置是否有卷
        private bool isFramePosBusi(CoilPoint p)
        {
            return Global.coils.ContainsKey(p);
        }
        */

        private void frmDgv_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F21)//环形键
            {
                if (Global.curFrame.KZ == "重")//入库作业
                {
                    string data = Global.sUserId + AllCode.stringInterfaceChar;
                    data += System.DateTime.Now.ToString("yyyyMMddHHmmss") + AllCode.stringInterfaceChar;
                    data += Global.sBb + AllCode.stringInterfaceChar;
                    data += Global.sZyq + AllCode.stringInterfaceChar;
                    data += Global.curFrame.TCH + AllCode.stringInterfaceChar;
                    data += Global.curFrame.QY + Global.curFrame.CH + AllCode.stringInterfaceChar;

                    int cnt = 0;
                    string msg = "";

                    foreach (KeyValuePair<CoilPoint, Coil> kv in Global.coils)
                    {
                        Coil c = kv.Value;
                        if (c.clh != "")
                        {
                            msg += c.p.barcode + AllCode.stringInterfaceChar;                  //逻辑位置
                            msg += c.zzdy + AllCode.stringInterfaceChar;                        //制造单元
                            msg += c.clh + AllCode.stringInterfaceChar;                        //材料号
                            msg += c.scantime + AllCode.stringInterfaceChar;                  //扫描时间
                            msg += c.qa + AllCode.stringInterfaceChar;                         //质量代码
                            msg += c.scanflag.ToString() + AllCode.stringInterfaceChar;       //处理标志
                            msg += c.wide + AllCode.stringInterfaceChar;                   //钢卷宽度
                            msg += c.diameter + AllCode.stringInterfaceChar;                   //钢卷内径

                            cnt++;
                        }

                    }

                    if (cnt > 0)
                    {
                        data += cnt.ToString() + AllCode.stringInterfaceChar;
                        if (msg.Length > 0) msg = msg.Substring(0, msg.Length - 1);
                        data += msg;
                        data += AllCode.stringInterfaceChar;

                        if (Global.sDebug == "False")
                        {
                            frmMessage frmMessage = new frmMessage();
                            frmMessage.ShowDialog("是否上传入库手持扫描结果？", "选择", "确定", "取消");
                            if (frmMessage.ret == true)
                            {
                                Business.SendText(Business.msg.Package("ZDUA03", data));//入库手持扫描完成
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
                if (curPoint.row>=0)
                {
                    if(Global.coils.ContainsKey(curPoint))
                    {
                        Coil coil = Global.coils[curPoint];
                        string clh=coil.clh;
                        string qa= coil.qa;
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

        private void frmDgv_Activated(object sender, EventArgs e)
        {
            if (ScanCodeRemapping.NormalTable.Contains(0x003A))
            {
                ScanCodeRemapping.NormalTable.Remove(0x003A);
            }
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
                if (txtClh.Text.Length>=9)
                {
                    addResult(txtClh.Text,"");
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
                    sql = "SELECT CLH2 FROM ImportStorageAcceptOrder WHERE (CLH2 LIKE '%" + txtClh.Text + "') ";
                }
                else
                {
                    sql = "SELECT CLH2 FROM ExportStorageAcceptOrder WHERE (CLH2 LIKE '%" + txtClh.Text + "') ";
                }

                DataTable dt = SqlCe.ExecuteQuery(sql);
                if (dt.Rows.Count == 1)
                {
                    txtClh.Text = dt.Rows[0]["CLH2"].ToString();
                    txtClh.SelectionStart = txtClh.Text.Length;
                }
                else if (dt.Rows.Count > 1)
                {
                    lstClh.Items.Clear();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        lstClh.Items.Add(dt.Rows[i]["CLH2"].ToString());
                    }
                    lstClh.Visible = true;
                }
                dt.Dispose();
            }
        }

        private void lstClh_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtClh.Text = lstClh.Items[lstClh.SelectedIndex].ToString();
            txtClh.SelectionStart = txtClh.Text.Length;
            txtClh.Focus();
            lstClh.Visible = false;
        }

        private void dataGrid1_DoubleClick(object sender, EventArgs e)
        {
            txtClh.Visible = true;
            lblClh.Visible = true;
            txtClh.Focus();
        }

    }
}