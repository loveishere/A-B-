using System;
using System.Net;
using System.Collections.Generic;
using System.Windows.Forms;
using PsionTeklogix.Barcode;
using PsionTeklogix.Barcode.ScannerServices;
using PsionTeklogix.Keyboard;
using PsionTeklogix.Keyboard.NoChording;
using System.Data;
using System.Data.SqlServerCe;
using System.Reflection;
using System.Text.RegularExpressions;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Xml;
//using DDSkyDll.Net20;

namespace hStore
{
    //Psion 7545XA
    static class Global
    {
        public static string sUserId;
        public static string sPassword;
        public static string sBb;
        public static string sKb="FMA";
        public static string sDevice;
        public static string sDebug;
        public static string sZyq;
        public static string sZdh;
        public static string sKw;
        public static string sJs = "";
        public static string sZl = "";
        public static string sHeight="";
        public static string sWidth="";
        public static string sLong = "";
        //public static string HCH = "";
        //Frame结构
        //curFrame
        //lstFrame
        public static Frame curFrame;
        public static string appPath;
        public static Form frmCurrent;
        public static Storage storage;
        public static bool wwanConnected = false;

        //public static List<Coil> coils=new List<Coil>();

        public static Dictionary<CoilPoint, Coil> coils = new Dictionary<CoilPoint, Coil>();
        public static List<Frame> lstframe = new List<Frame>();

        public static string ip;
        public static int port;
        public static TSocketClientBase<TSession> socketClient;
        public static Queue<string> socketExceptionQueue;
        public static Queue<string> socketDataGramQueue;

        private static Scanner scanner = new Scanner();
        private static ScannerServicesDriver scannerServicesDriver = new ScannerServicesDriver();

        public delegate void MethodInvoker();

        [MTAThread]
        static void Main()
        {
            appPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().ManifestModule.FullyQualifiedName);
            string conStr = "Data Source=" + appPath + "\\store.sdf;password=baosight;";
            SqlCe.con = new System.Data.SqlServerCe.SqlCeConnection(conStr);
            SqlCe.con.Open();

            //string connStr = "Provider=Microsoft.Jet.OLEDB.4.0;" + "data source=E:\\work\\work\\8.19\\Frame.xls;Extended Properties='Excel 8.0 Xml; HDR=YES; IMEX=1'";
            //SqlCe.con = new System.Data.SqlServerCe.SqlCeConnection(connStr);
            //SqlCe.con.Open();
            //string sql = "select A1 from [Sheet1$]";
            //DataTable dt_insert = new DataTable();
            //dt_insert = SqlCe.ExecuteQuery(sql);

            //for (int i = 0; i < dt_insert.Rows.Count; i++)
            //{
            //    sql = dt_insert.Rows[i][0].ToString();
            //    SqlCe.ExecuteNonQuery(sql);
            //}

            ReadConfigFile();

            ScanCodeRemapping.NormalTable.Clear();

           // //新机器
           // if (sDevice == "WORKABOUTPRO3")
           // {
           //     ScanCodeRemapping.NormalTable.Add(0x000F, new RemappingTable.Remapping(VirtualKey.VK_F2, Function.SendCode));//ALT->F2
           //     ScanCodeRemapping.NormalTable.Add(0x0007, new RemappingTable.Remapping(VirtualKey.VK_F5, Function.SendCode));//CTRL->F5
           // }
           // else
           // {
           //     ScanCodeRemapping.NormalTable.Add(0x000D, new RemappingTable.Remapping(VirtualKey.VK_F2, Function.SendCode));
           //     ScanCodeRemapping.NormalTable.Add(0x000F, new RemappingTable.Remapping(VirtualKey.VK_F5, Function.SendCode));
           // }
             

           // //中 环形键
           // ScanCodeRemapping.NormalTable.Add(0x0001, new RemappingTable.Remapping(VirtualKey.VK_F21, Function.SendCode));
           // //左 手持机左臂
           // ScanCodeRemapping.NormalTable.Add(0x0038, new RemappingTable.Remapping(VirtualKey.VK_F22, Function.SendCode));
           // //右 手持机右臂
           // ScanCodeRemapping.NormalTable.Add(0x0039, new RemappingTable.Remapping(VirtualKey.VK_F23, Function.SendCode));
           // //下 扫描键
           //// ScanCodeRemapping.NormalTable.Add(0x003A, new RemappingTable.Remapping(VirtualKey.VK_F24, Function.SendCode));
            if (sDevice == "7545")
            {
                //中
                ScanCodeRemapping.NormalTable.Add(0x0049, new RemappingTable.Remapping(VirtualKey.VK_F21, Function.SendCode));
                //左
                ScanCodeRemapping.NormalTable.Add(0x0053, new RemappingTable.Remapping(VirtualKey.VK_F22, Function.SendCode));
                //右
                ScanCodeRemapping.NormalTable.Add(0x0056, new RemappingTable.Remapping(VirtualKey.VK_F23, Function.SendCode));

                //下
                //ScanCodeRemapping.NormalTable.Add(0x0057, new RemappingTable.Remapping(VirtualKey.VK_F24, Function.SendCode));
            }
            else if (sDevice == "WORKABOUTPRO3")
            {
                ScanCodeRemapping.NormalTable.Add(0x000F, new RemappingTable.Remapping(VirtualKey.VK_F2, Function.SendCode));//ALT->F2
                ScanCodeRemapping.NormalTable.Add(0x0007, new RemappingTable.Remapping(VirtualKey.VK_F5, Function.SendCode));//CTRL->F5

                //中
                ScanCodeRemapping.NormalTable.Add(0x0001, new RemappingTable.Remapping(VirtualKey.VK_F21, Function.SendCode));
                //左
                ScanCodeRemapping.NormalTable.Add(0x0038, new RemappingTable.Remapping(VirtualKey.VK_F22, Function.SendCode));
                //右
                ScanCodeRemapping.NormalTable.Add(0x0039, new RemappingTable.Remapping(VirtualKey.VK_F23, Function.SendCode));

                //下
                //ScanCodeRemapping.NormalTable.Add(0x003A, new RemappingTable.Remapping(VirtualKey.VK_F24, Function.SendCode));
            }

            scanner.Driver = scannerServicesDriver;
            scanner.ScanCompleteEvent += new PsionTeklogix.Barcode.ScanCompleteEventHandler(scanner_ScanCompleteEvent);

            sUserId = "";
            sPassword = "";
            sBb = "";
            sZyq = "";
            sZdh = "";
            Business.info = "";
            sKw = "";
            storage = new Storage();

            Business.msg = new GramMessage();
            Business.service = new WebService.Service();

            socketClient = new TSocketClientBase<TSession>(ip, port);
            socketClient.MaxSessionTimeout = 50;

            AttachClientEvent();
            socketClient.HeadDelimiter = new byte[] { 35, 35 };//##
            socketClient.TailDelimiter = new byte[] { 64, 64 };//@@
            socketExceptionQueue = new Queue<string>();
            socketDataGramQueue = new Queue<string>();

            //////// for test
            if (Global.socketClient.Closed) Global.socketClient.Start();
            ///////

            frmCurrent = new hStore.Forms.frmLogin();
            //frmCurrent = new hStore.Forms.FormTEST();
            Application.Run(frmCurrent);



        }

        private static void AttachClientEvent()
        {
            socketClient.SessionConnected += new EventHandler<TSessionEventArgs>(socketClient_SessionConnected);
            socketClient.SessionDisconnected += new EventHandler<TSessionEventArgs>(socketClient_SessionDisconnected);
            socketClient.ClientException += new EventHandler<TExceptionEventArgs>(socketClient_ClientException);
            socketClient.ShowDebugMessage += new EventHandler<TMessageEventArgs>(socketClient_ShowDebugMessage);
            socketClient.SessionSendException += new EventHandler<TSessionExceptionEventArgs>(socketClient_SessionSendException);
            socketClient.SessionReceiveException += new EventHandler<TSessionExceptionEventArgs>(socketClient_SessionReceiveException);
        }

        static void socketClient_SessionDisconnected(object sender, TSessionEventArgs e)
        {
            //Business.InvokeMethod(frmCurrent, "showConnect", new object[] { });
        }

        static void socketClient_SessionConnected(object sender, TSessionEventArgs e)
        {
            //Business.InvokeMethod(frmCurrent, "showConnect", new object[] { });
        }


        static void socketClient_SessionReceiveException(object sender, TSessionExceptionEventArgs e)
        {
            if (socketExceptionQueue.Count < 10)
            {
                socketExceptionQueue.Enqueue("[" + DateTime.Now.ToString("HH:mm:ss") + "] Recv:" + e.ExceptionMessage);
            }
        }

        static void socketClient_SessionSendException(object sender, TSessionExceptionEventArgs e)
        {
            if (socketExceptionQueue.Count < 10)
            {
                socketExceptionQueue.Enqueue("[" + DateTime.Now.ToString("HH:mm:ss") + "] Send:" + e.ExceptionMessage);
            }
        }

        static void socketClient_ShowDebugMessage(object sender, TMessageEventArgs e)
        {
            if (socketDataGramQueue.Count < 10)
            {
                socketDataGramQueue.Enqueue(e.Message);
            }
        }

        private static void socketClient_ClientException(object sender, TExceptionEventArgs e)
        {

            if (socketExceptionQueue.Count < 10)
            {
                socketExceptionQueue.Enqueue("[" + DateTime.Now.ToString("HH:mm:ss") + "] Client:" + e.ExceptionMessage);
            }

        }

        private static void ReadConfigFile()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(appPath + @"\config.xml");

            XmlNode node = doc.DocumentElement.SelectSingleNode("//add[@key='Server IP']");
            ip = node.Attributes["value"].Value;

            node = doc.DocumentElement.SelectSingleNode("//add[@key='Store Server Port']");
            port = int.Parse(node.Attributes["value"].Value);

            node = doc.DocumentElement.SelectSingleNode("//add[@key='Storage']");
            sKb = node.Attributes["value"].Value;

            node = doc.DocumentElement.SelectSingleNode("//add[@key='Device']");
            sDevice = node.Attributes["value"].Value;

            node = doc.DocumentElement.SelectSingleNode("//add[@key='Debug']");
            sDebug = node.Attributes["value"].Value;

            node = null;
            doc = null;

        }

        private static void scanner_ScanCompleteEvent(object sender, PsionTeklogix.Barcode.ScanCompleteEventArgs e)
        {
            InvokeMethod(frmCurrent, "scanner_ScanCompleteEvent", new object[] { e.Text });
        }

        public static void Exit()
        {
            SqlCe.con.Close();
            Business.service.Dispose();

            if (Business.td != null)
            {
                Business.td.Abort();
                Business.td = null;
            }
            if (socketClient != null)
            {
                socketClient.Stop();
            }
        }

        public static void RecvData(string Datagram)
        {
            Business.RecvData(Datagram);
        }

        public static void InvokeMethod(Form frm, string methodName, object[] args)
        {
            Type t = frm.GetType();
            BindingFlags flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static;

            MethodInfo m = t.GetMethod(methodName, flags);
            if (m != null)
            {
                m.Invoke(frm, args);
            }
        }

        public static bool IsNumberic(String strNumber)
        {

            Regex objFloat = new Regex(@"^(-?\d+)(\.\d+)?$");

            return objFloat.IsMatch(strNumber);

        }

        public static void UIThread(Control control, MethodInvoker code)
        {
            if (control.InvokeRequired) control.BeginInvoke(code);
            else control.Invoke(code);
        }
    }

    public class SystemTime
    {
        public ushort wYear;
        public ushort wMonth;
        public ushort wDayOfWeek;
        public ushort wDay;
        public ushort Whour;
        public ushort wMinute;
        public ushort wSecond;
        public ushort wMilliseconds;
    }

    public class SqlCe
    {
        public static System.Data.SqlServerCe.SqlCeConnection con;

        public static void ExecuteNonQuery(string sql)
        {
            try
            {
                if (con.State == ConnectionState.Open)
                {
                    SqlCeCommand cmd = new SqlCeCommand(sql, con);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    GC.Collect();
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message + "|" + sql);
            }
        }

        public static DataTable ExecuteQuery(string sql)
        {
            DataTable dt = new DataTable();
            try
            {
                if (con.State == ConnectionState.Open)
                {
                    SqlCeDataAdapter da = new SqlCeDataAdapter(sql, con);

                    da.Fill(dt);
                    da.Dispose();
                    da = null;
                    GC.Collect();
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message + "|" + sql);
            }
            return dt;
        }

    }

    public class TSession : TSessionBase
    {
        protected override void AnalyzeDatagram(byte[] datagramBytes)
        {
            if (datagramBytes.Length > 0)
            {
                string gram = System.Text.Encoding.Default.GetString(datagramBytes, 0, datagramBytes.Length);
                if (gram != "##0025UAZD00alive;9999@@")
                {
                    string sql = "insert into RecvMsg(Msg,Done,Lock,RecvTime,ID) values('" + gram.Replace("'", "''") + "',0,0,getDate(),newID())";
                    SqlCe.ExecuteNonQuery(sql);
                }
                base.OnShowDebugMessage(gram);
                //base.OnDatagramAccepted();
            }
        }
    }


    public struct CoilPoint
    {
        public int row;
        public int col;
        public string barcode;//扫描标签号
        public string frameid;//框架号

        public CoilPoint(string frameid, int row, int col)
        {
            this.row = row;
            this.col = col;
            this.frameid = frameid;

            int szRow = row + 1;
            int szCol = 0;
            if (col == 2)
            {
                szCol = 3;
            }
            else if (col == 1)
            {
                szCol = 1;
            }
            else
            {
                szCol = 0;
            }
            this.barcode = frameid + szRow.ToString("00") + szCol.ToString();
            //yt 20170507
            //this.barcode =szRow.ToString("00") ;//车上板坯层号
        }

        public CoilPoint(string frameid, string barcode)
        {
            int szRow = Convert.ToInt32(barcode.Substring(4, 2));
            int szCol = Convert.ToInt32(barcode.Substring(6, 1));
            this.row = szRow - 1;
            if (szCol == 3)
            {
                this.col = 2;
            }
            else if (szCol == 1)
            {
                this.col = 1;
            }
            else
            {
                this.col = -1;
            }
            this.frameid = frameid;
            this.barcode = barcode;
        }
    }

    public class Coil
    {
        public CoilPoint p;
        public int X;           //相对框架的X坐标
        public int Y;           //相对框架的Y坐标
        public string clh;
        public string zzdy;     //制造单元
        public string kb;       //入库别/出库别
        public string scantime; //比对时间
        public int scanflag;    //比对结果，初始值-1；比对失败0；比对成功1
        public string qa;       //质量代码
        public string wide;     //宽度（激光扫描获得）
        public string diameter; //内径（上游L3提供）

        public Coil(CoilPoint p, string clh)
        {
            this.p = p;
            this.clh = clh;
            this.zzdy = "";
            this.kb = "";
            this.X = 0;
            this.Y = 0;
            this.scantime = "";
            this.scanflag = -1;
            this.qa = "";
            this.wide = "";
            this.diameter = "";
        }

        public void move(CoilPoint p)
        {
            this.p = p;
        }
    }

    public enum FrameStatus
    {
        PT_Initial = 0,
        PT_CarArrive = 1,
        PT_LaserStart = 2,
        PT_LaserEnd = 3,
        PS_In_PortableEnd = 4,
        PS_Out_PortableEnd = 5,
        PT_CranePlanCreated = 6,
        PS_OperateStart = 7,
        PS_OperatePause = 8,
        PT_OperateFini = 9,
        PS_CarLeave = 10
    }
    

    public class Frame
    {
        //入位画面
        public string TCH;//停车号
        public string QY;//区域
        public string CH;//车号
        public string KJH;//框架号
        public string KZ;//空重
        public string FX;//方向
        public string ZZ;//载重
        public int MaxRow;//档位
        public int Space;//间隔   单位毫米
        public string TDH;//提单号
        public FrameStatus status;//作业状态
        public string HCH;//行车号
        public bool CarArrived;//True:框架入位  False:框架离位
        //changeByyangting20170518
        public string ZL;//种类

        public Frame(string TCH,string QY,string CH,string KJH,string KZ,string FX,string ZZ,int MaxRow, int Space, string TDH, string ZL)
        {
            this.TCH = TCH;
            this.QY = QY;
            this.CH = CH;
            this.KJH = KJH;
            this.KZ = KZ;
        
            this.FX = FX;
            this.ZZ = ZZ;
          
            this.MaxRow = MaxRow;
            this.Space = Space;
            this.TDH = TDH;
            //changeByyangting20170518
            this.ZL = ZL;

            this.status = FrameStatus.PT_Initial;
            this.HCH = "";
            this.CarArrived = true;
        }

       
    }

}