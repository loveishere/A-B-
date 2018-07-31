using System;
using System.Windows.Forms;
using System.Data;
using System.Reflection;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Threading;
using System.Xml;
using hStore.Forms;

namespace hStore
{
    class Business
    {
        public static WebService.Service service;
        public static GramMessage msg;

        public static int cjhs;
        public static int ccls;
        public static int calljhs;
        public static int callcls;

        public static int rjhs;
        public static int rcls;
        public static int ralljhs;
        public static int rallcls;

        public static string info;

        public static bool bDownSucc;

        public static string rstamp;
        public static string cstamp;

        public static Thread td=null;

        #region p/invoke

        [DllImport("coredll.dll", EntryPoint = "CreateEvent", SetLastError = true)]
        private static extern IntPtr CreateEvent(IntPtr lpEventAttributes, bool bManualReset, bool bInitialState, string spName);

        private const UInt32 INFINITE = 0xFFFFFFFF;

        [DllImport("coredll.dll", EntryPoint = "WaitForSingleObject", SetLastError = true)]
        private static extern int WaitForSingleObject(IntPtr hHandle, UInt32 dwMilliseconds);

        [DllImport("coredll.dll", EntryPoint = "WaitForMultipleObjects", SetLastError = true)]
        static extern uint WaitForMultipleObjects(uint nCount, IntPtr[] lpHandles, bool bWaitAll, uint dwMilliseconds);

        private const int WAIT_OBJECT_0 = 0;

        [DllImport("coredll.dll", EntryPoint = "CloseHandle", SetLastError = true)]
        private static extern bool CloseHandle(IntPtr hHandle);

        private const int NOTIFICATION_EVENT_NONE = 0;
        private const int NOTIFICATION_EVENT_TIME_CHANGE = 1;
        private const int NOTIFICATION_EVENT_SYNC_END = 2;
        private const int NOTIFICATION_EVENT_ON_AC_POWER = 3;
        private const int NOTIFICATION_EVENT_OFF_AC_POWER = 4;
        private const int NOTIFICATION_EVENT_NET_CONNECT = 5;
        private const int NOTIFICATION_EVENT_NET_DISCONNECT = 6;
        private const int NOTIFICATION_EVENT_DEVICE_CHANGE = 7;
        private const int NOTIFICATION_EVENT_IR_DISCOVERED = 8;
        private const int NOTIFICATION_EVENT_RS232_DETECTED = 9;
        private const int NOTIFICATION_EVENT_RESTORE_END = 10;
        private const int NOTIFICATION_EVENT_WAKEUP = 11;
        private const int NOTIFICATION_EVENT_TZ_CHANGE = 12;
        private const int NOTIFICATION_EVENT_MACHINE_NAME_CHANGE = 13;

        /*
         * 
         * Microsoft Windows CE .NET 4.2
         * CeRunAppAtEvent
         * This function starts running an application or launches a named event when a specified event occurs.
         *
         * BOOL CeRunAppAtEvent(
         *      TCHAR* pwszAppName, 
         *      LONG lWhichEvent 
         *      ); Parameters
         *
         * pwszAppName 
         * -----------
         * [in] Pointer to a null-terminated string that specifies the name of the application to be started. 
         * You can also use this string to support a named event instead of launching an application. 
         * The following code example shows the format that you should use for the string pointed to by pwszAppName. 
         * 
         * "\\\\.\\Notifications\\NamedEvents\\Event Name"Event Name represents the application-defined name of the 
         * event to signal. When you use this format for pwszAppName, the event is opened and signaled. Named events 
         * are supported in Microsoft?Windows?CE .NET 4.0 and later. 
         * 
         * lWhichEvent 
         * -----------
         * [in] Long integer that specifies the event at which the application is to be started. It is one of the 
         * following values. 
         * 
         * Value                               Description 
         * NOTIFICATION_EVENT_DEVICE_CHANGE    A PC Card device changed. 
         * NOTIFICATION_EVENT_IR_DISCOVERED    The device discovered a server by using infrared communications. 
         * NOTIFICATION_EVENT_NET_CONNECT      The device connected to a network. 
         * NOTIFICATION_EVENT_NET_DISCONNECT   The device disconnected from a network. 
         * NOTIFICATION_EVENT_NONE             No events occurred. Remove all event registrations for this application. 
         * NOTIFICATION_EVENT_OFF_AC_POWER     The user turned the alternating current (AC) power off. 
         * NOTIFICATION_EVENT_ON_AC_POWER      The user turned the AC power on. 
         * NOTIFICATION_EVENT_RESTORE_END      A full device data restore completed. 
         * NOTIFICATION_EVENT_RS232_DETECTED   An RS232 connection was made. 
         * NOTIFICATION_EVENT_SYNC_END         Data synchronization finished. 
         * NOTIFICATION_EVENT_TIME_CHANGE      The system time changed. 
         * NOTIFICATION_EVENT_TZ_CHANGE        The time zone changed. 
         * NOTIFICATION_EVENT_WAKEUP           The device woke up. 
         * 
         * Return Values
         * -------------
         * TRUE indicates success. FALSE indicates failure.
         * 
         */

        [DllImport("coredll.dll", EntryPoint = "CeRunAppAtEvent", SetLastError = true)]
        private static extern bool CeRunAppAtEvent(string pszAppName, long lwichEvent);

        // Note: SetEvent, ResetEvent, and PulseEvent are all just macros
        //       that call EventModify. EventModify is in coredll.dll. 

        private enum EventAction : uint
        {
            EVENT_PULSE = 1,
            EVENT_RESET = 2,
            EVENT_SET = 3
        }

        [DllImport("coredll.dll", SetLastError = true)]
        public static extern bool EventModify(IntPtr hEvent, [In, MarshalAs(UnmanagedType.U4)] int dEvent);

        [DllImport("coredll")]
        public static extern void SetLocalTime(SystemTime st);

        #endregion p/invoke

        public static void InvokeMethod(Form frm, string methodName, object[] args)
        {
            if (frm != null)
            {
                Type t = frm.GetType();
                BindingFlags flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static;

                MethodInfo m = t.GetMethod(methodName, flags);
                if (m != null)
                {
                    m.Invoke(frm, args);
                }
            }
        }

        private static string dateString(string sInputString)
        {
            if (sInputString.Length == 14)
            {
                return "'" + sInputString.Substring(0, 4) + "-" + sInputString.Substring(4, 2) + "-" + sInputString.Substring(6, 2) + " " + sInputString.Substring(8, 2) + ":" + sInputString.Substring(10, 2) + ":" + sInputString.Substring(12, 2) + "'";
            }
            else
            {
                return "null";
            }
        }

        private static string valuesql(string type, string content)
        {
            string sql = "";

            switch (type)
            {
                case "C":
                    sql = "'" + content + "',";
                    break;
                case "N":
                    if (content == "")
                    {
                        sql = "null,";
                    }
                    else
                    {
                        sql = content + ",";
                    }
                    break;
                case "D":
                    if (content == "")
                    {
                        sql = "null,";
                    }
                    else
                    {
                        sql = dateString(content) + ",";
                    }
                    break;
                default:
                    break;
            }

            return sql;

        }

        public static void GetImpPlanByPageResults(IAsyncResult ar)
        {
            ManualResetEvent ent;
            ent = (ManualResetEvent)ar.AsyncState;
            try
            {
                DataTable dt = service.EndStore_GetImpPlanByPage(ar);

                bool bval = dt.Rows.Count > 0;
                if (bval)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {


                        string jhh = dt.Rows[i]["PLAN_NO"].ToString();
                        string make = dt.Rows[i]["MANUFACT_UNIT_CODE"].ToString();
                        string jz = dt.Rows[i]["NET_WEIGHT"].ToString();
                        string mz = dt.Rows[i]["GROSS_WEIGHT"].ToString();
                        string js = dt.Rows[i]["PIECES"].ToString();
                        string wcjs = "0";
                        string wcjz = "0";
                        string yhsj = dt.Rows[i]["PLAN_TIME"].ToString();
                        string wcmz = "0";
                        string rkb = dt.Rows[i]["INSTORE_ID"].ToString();
                        string sql = "delete from ImportStorageAcceptOrder where jhh='" + jhh + "' and make='" + make + "'";
                        SqlCe.ExecuteNonQuery(sql);
                        sql = "delete from ImportStoragePlan where jhh='" + jhh + "' and make='"+ make +"'";
                        SqlCe.ExecuteNonQuery(sql);

                        sql = "insert into ImportStoragePlan(JHH,JZ,MZ,JS,WCJS,WCJZ,YHSJ,WCMZ,RKB,Make) ";
                        sql += "values(" + valuesql("C", jhh) + valuesql("N", jz) + valuesql("N", mz) + valuesql("N", js) + valuesql("N", wcjs);
                        sql += valuesql("N", wcjz) + valuesql("D", yhsj) + valuesql("N", wcmz) + valuesql("C", rkb) + valuesql("C", make);
                        sql = sql.Substring(0, sql.Length - 1);
                        sql += ")";

                        SqlCe.ExecuteNonQuery(sql);

                    }
                    rjhs += dt.Rows.Count;

                    bDownSucc = true;
                }

                ent.Set();
            }
            catch
            {
            }
        }

        public static void GetImpAccOrderByPageResults(IAsyncResult ar)
        {
            ManualResetEvent ent;
            ent = (ManualResetEvent)ar.AsyncState;
            try
            {
                DataTable dt = service.EndStore_GetImpAccOrderByPage(ar);

                bool bval = dt.Rows.Count > 0;
                if (bval)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string jhh = dt.Rows[i]["PLAN_NO"].ToString();
                        string make = dt.Rows[i]["MANUFACT_UNIT_CODE"].ToString();
                        string rkb = dt.Rows[i]["INSTORE_ID"].ToString();
                        string zfh = dt.Rows[i]["CONFIRM_NO"].ToString();
                        string clh = dt.Rows[i]["MAT_NO"].ToString();
                        string jz = dt.Rows[i]["NET_WEIGHT"].ToString();
                        string mz = dt.Rows[i]["GROSS_WEIGHT"].ToString();
                        //string kw = dt.Rows[i]["KW"].ToString();
                        string kw = "";
                        string ilong = dt.Rows[i]["LENGTH"].ToString();
                        string iwidth = dt.Rows[i]["WIDTH"].ToString();
                        string iheight = dt.Rows[i]["THICK"].ToString();
                        string wcflag = "0";
                        //string fdh = dt.Rows[i]["FDH"].ToString();
                        string fdh = "";
                        string scantime = "";
                        string lh = dt.Rows[i]["PONO"].ToString();
                        string clh2 = dt.Rows[i]["MAT_NO2"].ToString();
                        string pmdm = dt.Rows[i]["GOODS_CODE"].ToString();
                        string pm = dt.Rows[i]["GOODS_NAME"].ToString();
                        string hth = dt.Rows[i]["CONTRACT_ID"].ToString();


                        string sql = "insert into ImportStorageAcceptOrder (JHH,RKB,ZFH,CLH,JZ,MZ,KW,Long,Width,Height,WCFlag,FDH,SCANTIME,LH,CLH2,Make,PMDM,PM,HTH) ";
                        sql += "values(" + valuesql("C", jhh) + valuesql("C", rkb) + valuesql("C", zfh) + valuesql("C", clh) + valuesql("N", jz) + valuesql("N", mz) + valuesql("C", kw);
                        sql += valuesql("C", ilong) + valuesql("C", iwidth) + valuesql("C", iheight) + valuesql("N", wcflag) + valuesql("C", fdh) + valuesql("C", scantime) + valuesql("C", lh);
                        sql += valuesql("C", clh2) + valuesql("C", make) + valuesql("C", pmdm) + valuesql("C", pm) + valuesql("C", hth);
                        sql = sql.Substring(0, sql.Length - 1);
                        sql += ")";
                        SqlCe.ExecuteNonQuery(sql);
                    }
                    rcls += dt.Rows.Count;
                    bDownSucc = true;
                }

                ent.Set();
            }
            catch
            {
            }
        }

        public static void GetExpPlanByPageResults(IAsyncResult ar)
        {
            ManualResetEvent ent;
            ent = (ManualResetEvent)ar.AsyncState;
            try
            {
                DataTable dt = service.EndStore_GetExpPlanByPage(ar);

                bool bval = dt.Rows.Count > 0;
                if (bval)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string jhh = dt.Rows[i]["PLAN_NO"].ToString();
                        string make = dt.Rows[i]["MANUFACT_UNIT_CODE"].ToString();
                        string gdh = dt.Rows[i]["SO_NO"].ToString();
                        string cm = dt.Rows[i]["SHIP_CNAME"].ToString();
                        string shdw = dt.Rows[i]["RECEIVE_UNIT"].ToString();
                        string jhdd = dt.Rows[i]["DELIVERY_ADDRESS"].ToString();
                        string yb = dt.Rows[i]["ClothShelter"].ToString();
                        string jz = dt.Rows[i]["NET_WEIGHT"].ToString();
                        string mz = dt.Rows[i]["GROSS_WEIGHT"].ToString();
                        string js = dt.Rows[i]["PIECES"].ToString();
                        string ysfs = dt.Rows[i]["TRANS_TYPE"].ToString();
                        string wcjs = "0";
                        string wcjz = "0";
                        string cph = dt.Rows[i]["SHIP_LOT_NUM"].ToString();
                        string wcmz = "0";
                        string ckb = dt.Rows[i]["OUTSTORE_ID"].ToString();

                        string sql = "delete from ExportStorageAcceptOrder where jhh='" + jhh + "' and make='" + make + "'";
                        SqlCe.ExecuteNonQuery(sql);
                        sql = "delete from ExportStoragePlan where jhh='" + jhh + "' and make='" + make + "'";
                        SqlCe.ExecuteNonQuery(sql);

                        sql = "insert into ExportStoragePlan(JHH,GDH,CM,SHDW,JHDD,YB,JZ,MZ,JS,YSFS,WCJS,WCMZ,CPH,WCJZ,CKB,Make) ";
                        sql += "values(" + valuesql("C", jhh) + valuesql("C", gdh) + valuesql("C", cm) + valuesql("C", shdw) + valuesql("C", jhdd) + valuesql("C", yb);
                        sql += valuesql("N", jz) + valuesql("N", mz) + valuesql("N", js) + valuesql("C", ysfs) + valuesql("N", wcjs);
                        sql += valuesql("N", wcmz) + valuesql("C", cph) + valuesql("N", wcjz) + valuesql("C", ckb) + valuesql("C", make);
                        sql = sql.Substring(0, sql.Length - 1);
                        sql += ")";

                        SqlCe.ExecuteNonQuery(sql);


                    }
                    cjhs += dt.Rows.Count;
                    bDownSucc = true;
                }
                ent.Set();
            }
            catch
            {
            }
        }

        public static void GetExpAccOrderByPageResults(IAsyncResult ar)
        {
            ManualResetEvent ent;
            ent = (ManualResetEvent)ar.AsyncState;
            try
            {
                DataTable dt = service.EndStore_GetExpAccOrderByPage(ar);

                bool bval = dt.Rows.Count > 0;
                if (bval)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string jhh = dt.Rows[i]["PLAN_NO"].ToString();
                        string make = dt.Rows[i]["MANUFACT_UNIT_CODE"].ToString();
                        string ckb = dt.Rows[i]["OUTSTORE_ID"].ToString();
                        string zfh = dt.Rows[i]["CONFIRM_NO"].ToString();
                        string clh = dt.Rows[i]["MAT_NO"].ToString();
                        string jz = dt.Rows[i]["NET_WEIGHT"].ToString();
                        string mz = dt.Rows[i]["GROSS_WEIGHT"].ToString();
                        //string kw = dt.Rows[i]["KW"].ToString();
                        string kw = "";
                        string wcflag = "0";
                        string ilong = dt.Rows[i]["LENGTH"].ToString();
                        string iwidth = dt.Rows[i]["WIDTH"].ToString();
                        string iheight = dt.Rows[i]["THICK"].ToString();
                        //string fdh = dt.Rows[i]["FDH"].ToString();
                        string fdh = "";
                        string scantime = "";
                        //string zph = dt.Rows[i]["ZPH"].ToString();
                        string zph = "";
                        string lh = dt.Rows[i]["PONO"].ToString().Trim();
                        string clh2 = dt.Rows[i]["MAT_NO2"].ToString();
                        string pmdm = dt.Rows[i]["GOODS_CODE"].ToString();
                        string pm = dt.Rows[i]["GOODS_NAME"].ToString();
                        string hth = dt.Rows[i]["CONTRACT_ID"].ToString();
                        string yhsj = dt.Rows[i]["REQUIRE_TIME"].ToString();

                        string sql = "insert into ExportStorageAcceptOrder (JHH,CKB,ZFH,CLH,JZ,MZ,KW,WCFlag,Long,Width,Height,FDH,SCANTIME,ZPH,LH,CLH2,Make,PMDM,PM,HTH,YHSJ) ";
                        sql += "values(" + valuesql("C", jhh) + valuesql("C", ckb) + valuesql("C", zfh) + valuesql("C", clh) + valuesql("N", jz) + valuesql("N", mz) + valuesql("C", kw) + valuesql("N", wcflag);
                        sql += valuesql("C", ilong) + valuesql("C", iwidth) + valuesql("C", iheight) + valuesql("C", fdh) + valuesql("C", scantime) + valuesql("C", zph) + valuesql("C", lh);
                        sql += valuesql("C", clh2) + valuesql("C", make) + valuesql("C", pmdm) + valuesql("C", pm) + valuesql("C", hth) + valuesql("D", yhsj);
                        sql = sql.Substring(0, sql.Length - 1);
                        sql += ")";
                        SqlCe.ExecuteNonQuery(sql);

                    }
                    ccls += dt.Rows.Count;
                    bDownSucc = true;
                }

                ent.Set();
            }
            catch
            {
            }
        }

        public static void GetImpPlanByPage(string stamp,string beginno,string endno)
        {
            bDownSucc = false;
            string pram = "<root><p>" + stamp + "</p></root>";
            AsyncCallback callBack = new AsyncCallback(GetImpPlanByPageResults);
            ManualResetEvent ent = new ManualResetEvent(false);
            service.BeginStore_GetImpPlanByPage(pram,beginno,endno, callBack, ent);
            ent.WaitOne();
        }

        public static void GetImpAccOrderByPage(string stamp,string beginno,string endno)
        {
            bDownSucc = false;
            string pram = "<root><p>" + stamp + "</p></root>";
            AsyncCallback callBack = new AsyncCallback(GetImpAccOrderByPageResults);
            ManualResetEvent ent = new ManualResetEvent(false);
            service.BeginStore_GetImpAccOrderByPage(pram,beginno,endno, callBack, ent);
            ent.WaitOne();
        }

        public static void GetExpPlanByPage(string stamp,string beginno,string endno)
        {
            bDownSucc = false;
            string pram = "<root><p>" + stamp + "</p></root>";
            AsyncCallback callBack = new AsyncCallback(GetExpPlanByPageResults);
            ManualResetEvent ent = new ManualResetEvent(false);
            service.BeginStore_GetExpPlanByPage(pram, beginno, endno, callBack, ent);
            ent.WaitOne();
        }

        public static void GetExpAccOrderByPage(string stamp, string beginno, string endno)
        {
            bDownSucc = false;
            string pram = "<root><p>" + stamp + "</p></root>";
            AsyncCallback callBack = new AsyncCallback(GetExpAccOrderByPageResults);
            ManualResetEvent ent = new ManualResetEvent(false);
            service.BeginStore_GetExpAccOrderByPage(pram,beginno,endno, callBack, ent);
            ent.WaitOne();
        }

        public static void DownLoadPlan()
        {
            InvokeMethod(Global.frmCurrent, "showMessage", new object[] { "开始下载" });
            Application.DoEvents();

            bDownSucc = true;

            //获取入库计划信息
            int step = ralljhs / 200;
            if (ralljhs % 200 > 0) step++;

            for (int i = 0; i < step; i++)
            {
                if (bDownSucc == false) break;

                int beginno = i * 200;
                int endno = beginno + 200;


                if (endno > ralljhs) endno = ralljhs;

                GetImpPlanByPage(rstamp, beginno.ToString(), endno.ToString());
                InvokeMethod(Global.frmCurrent, "showMessage", new object[] { "下载中" });
                Application.DoEvents();
                Thread.Sleep(1000);

            }


            //获取入库材料信息
            step = rallcls / 200;
            if (rallcls % 200 > 0) step++;

            for (int i = 0; i < step; i++)
            {
                if (bDownSucc == false) break;

                int beginno = i * 200;
                int endno = beginno + 200;


                if (endno > rallcls) endno = rallcls;

                GetImpAccOrderByPage(rstamp, beginno.ToString(), endno.ToString());
                InvokeMethod(Global.frmCurrent, "showMessage", new object[] { "下载中" });
                Application.DoEvents();
                Thread.Sleep(1000);

            }

            //获取出库计划信息
            step = calljhs / 200;
            if (calljhs % 200 > 0) step++;

            for (int i = 0; i < step; i++)
            {
                if (bDownSucc == false) break;

                int beginno = i * 200;
                int endno = beginno + 200;


                if (endno > calljhs) endno = calljhs;

                GetExpPlanByPage(cstamp, beginno.ToString(), endno.ToString());
                InvokeMethod(Global.frmCurrent, "showMessage", new object[] { "下载中" });
                Application.DoEvents();
                Thread.Sleep(1000);

            }


            //获取出库材料信息
            step = callcls / 200;
            if (callcls % 200 > 0) step++;

            for (int i = 0; i < step; i++)
            {
                if (bDownSucc == false) break;

                int beginno = i * 200;
                int endno = beginno + 200;


                if (endno > callcls) endno = callcls;

                GetExpAccOrderByPage(cstamp, beginno.ToString(), endno.ToString());
                InvokeMethod(Global.frmCurrent, "showMessage", new object[] { "下载中" });
                Application.DoEvents();
                Thread.Sleep(1000);

            }

            if (bDownSucc == false)
            {
                InvokeMethod(Global.frmCurrent, "showMessage", new object[] { "下载失败" });
                Application.DoEvents();
            }
            else
            {
                InvokeMethod(Global.frmCurrent, "showMessage", new object[] { "下载完成" });
                Application.DoEvents();

            }


        }

        private static void SetCurFrameStatus(FrameStatus status)
        {
            if (Global.curFrame != null)
            {
                Global.curFrame.status = status;
            }
        }

        public static void SetCurrentTime(DateTime dt)
        {
            string dtRemote = dt.ToString("yyyyMMddHHmmss");
            if (dtRemote != "")
            {
                SystemTime st = new SystemTime();
                st.wYear = ushort.Parse(dtRemote.Substring(0, 4));
                st.wMonth = ushort.Parse(dtRemote.Substring(4, 2));
                st.wDay = ushort.Parse(dtRemote.Substring(6, 2));
                st.Whour = ushort.Parse(dtRemote.Substring(8, 2));
                st.wMinute = ushort.Parse(dtRemote.Substring(10, 2));
                st.wSecond = ushort.Parse(dtRemote.Substring(12, 2));
                SetLocalTime(st);
            }
        }

        public static void RecvData(string Datagram)
        {

            if (msg.UnPackage(Datagram))
            {
                switch (msg.MessageID)
                {
                    //case "WXZD03":
                    //    SqlCe.ExecuteNonQuery("delete from FrameDetail where kjh='" + msg.ItemValue("KJH") + "' and stamp!='" + msg.ItemValue("stamp") + "' ");
                    //    break;
                    case "UAZD02":
                        SqlCe.ExecuteNonQuery("delete from FrameLayout where FrameID='"+ msg.ItemValue("FrameID") +"' and ParkID='"+ msg.ItemValue("ParkID") +"' and Mold='"+ msg.ItemValue("Mold") +"'");
                        break;

                    case "UAZD03":
                        SqlCe.ExecuteNonQuery("delete from FrameLayoutExp where FrameID='" + msg.ItemValue("FrameID") + "' and ParkID='" + msg.ItemValue("ParkID") + "'");
                        break;

                    case "UAZD05":
                        if (Global.curFrame != null)
                        {
                            Global.curFrame.HCH = msg.ItemValue("HCH");
                        }
                        SqlCe.ExecuteNonQuery("delete from TaskOrder where HCH='" + msg.ItemValue("HCH") + "'");
                        break;

                    default:
                        break;
                }

                msg.Save();

                switch (msg.MessageID)
                {
                    //case "WXZD03":
                    //    InvokeMethod(Global.frmCurrent, "RefreshFrameInfo", new object[] { });
                    //    break;

                    //case "WXZD1B":
                    //    Global.sZdh = msg.ItemValue("ch");
                    //    string dtRemote = msg.ItemValue("sj");
                    //    if (dtRemote != "")
                    //    {
                    //        SystemTime st = new SystemTime();
                    //        st.wYear = ushort.Parse(dtRemote.Substring(0, 4));
                    //        st.wMonth = ushort.Parse(dtRemote.Substring(4, 2));
                    //        st.wDay = ushort.Parse(dtRemote.Substring(6, 2));
                    //        st.Whour = ushort.Parse(dtRemote.Substring(8, 2));
                    //        st.wMinute = ushort.Parse(dtRemote.Substring(10, 2));
                    //        st.wSecond = ushort.Parse(dtRemote.Substring(12, 2));
                    //        SetLocalTime(st);
                    //    }
                    //    break;

                    //case "WXZD1E":
                    //    SqlCe.ExecuteNonQuery("delete from SendMsg where GramID='" + msg.ItemValue("dwh") + "' and GramTime='" + msg.ItemValue("sj") + "'");
                    //    break;

                    case "UAZD01"://UACS反馈信息
                        string sMsg = "";
                        switch (msg.ItemValue("Message_No"))
                        {
                            case "PT_CarArrive":
                                sMsg = "框架入位";
                                SetCurFrameStatus(FrameStatus.PT_CarArrive);
                                if (Global.curFrame != null)
                                {
                                    Global.curFrame.CarArrived = true;
                                }
                                InvokeMethod(Global.frmCurrent, "showDialogMsg", new object[] { "停车位" + msg.ItemValue("Parking_No") + "框架已经入位！" });
                                break;
                            case "PT_LaserStart":
                                sMsg = "扫描启动";
                                SetCurFrameStatus(FrameStatus.PT_LaserStart);
                                break;
                            case "PT_LaserEnd":
                                SetCurFrameStatus(FrameStatus.PT_LaserEnd);
                                sMsg = "扫描结束";
                                break;
                            case "PS_In_PortableEnd":
                                sMsg = "入库上传";
                                SetCurFrameStatus(FrameStatus.PS_In_PortableEnd);
                                InvokeMethod(Global.frmCurrent, "showDialogMsg", new object[] { "入库框架材料布局信息已经上传！" });
                                break;
                            case "PT_CranePlanCreated":
                                SetCurFrameStatus(FrameStatus.PT_CranePlanCreated);
                                sMsg = "生成计划";
                                break;
                            case "PS_OperateStart":
                                sMsg = "作业开始";
                                SetCurFrameStatus(FrameStatus.PS_OperateStart);
                                break;
                            case "PS_OperatePause":
                                sMsg = "作业暂停";
                                SetCurFrameStatus(FrameStatus.PS_OperatePause);
                                break;
                            case "PS_Out_PortableEnd":
                                sMsg = "出库上传";
                                SetCurFrameStatus(FrameStatus.PS_Out_PortableEnd);
                                break;
                            case "PS_CarLeave":
                                sMsg = "框架离位";
                                SetCurFrameStatus(FrameStatus.PS_CarLeave);
                                if (Global.curFrame != null)
                                {
                                    //Global.curFrame.CarArrived = false;
                                    if (Global.curFrame.KZ == "空" || Global.curFrame.KZ == "拼")
                                    {
                                        ClearExpFrameLayout(msg.ItemValue("Parking_No"));
                                    }
                                }
                                break;
                            case "PT_OperateFini":
                                sMsg = "作业完成";
                                SetCurFrameStatus(FrameStatus.PT_OperateFini);
                                ClearTaskOrder(msg.ItemValue("Parking_No"));
                                if (Global.curFrame != null)
                                {
                                    if (Global.curFrame.KZ == "重")
                                    {
                                        ClearImpFrameLayout(msg.ItemValue("Parking_No"));
                                    }
                                }
                                break;

                            case "FeedBack4CheckBank":
                                sMsg = "盘库信息已经接收";
                                InvokeMethod(Global.frmCurrent, "showDialogMsg", new object[] { sMsg });
                                break;
                        }

                        if (sMsg != "")
                        {
                            if (msg.ItemValue("Ret") == "-1")
                            {
                                sMsg += "异常";
                                sMsg += msg.ItemValue("Message");
                            }
                            else if (msg.ItemValue("Ret") == "1")
                            {
                                sMsg += "正常";
                            }
                            
                            InvokeMethod(Global.frmCurrent, "showMessage", new object[] { sMsg });
                        }



                        break;

                    case "UAZD02"://入库配载图
                        string mold = msg.ItemValue("Mold");
                        
                        if(mold=="20")//激光扫描结果
                        {
                            if (Global.curFrame != null)
                            {
                                Global.curFrame.status = FrameStatus.PT_LaserEnd;
                                if (Global.frmCurrent.Name == "FrameIn")
                                {
                                    if (Global.curFrame.KZ == "重")
                                    {
                                        InvokeMethod(Global.frmCurrent, "to_frmDgv", new object[] { });
                                    }
                                }
                                else if (Global.frmCurrent.Name == "frmDgv")
                                {
                                    if (Global.curFrame.KZ == "重")
                                    {
                                        //暂时屏蔽激光扫描功能
                                        //InvokeMethod(Global.frmCurrent, "LoadData", new object[] { });
                                    }
                                }
                            }
                        }
                        break;

                    case"UAZD03"://出库配载图
                        if (Global.curFrame != null)
                        {
                            if (Global.curFrame.KZ == "空" || Global.curFrame.KZ == "拼")
                            {
                                InvokeMethod(Global.frmCurrent, "to_frmSxChange", new object[] { });
                            }
                        }
                        break;

                    case"UAZD04":
                        rstamp = msg.ItemValue("stamp");
                        ralljhs = int.Parse(msg.ItemValue("RJHS"));
                        rallcls = int.Parse(msg.ItemValue("RCLS"));
                        rjhs = 0;
                        rcls = 0;

                        cstamp = msg.ItemValue("stamp");
                        calljhs = int.Parse(msg.ItemValue("CJHS"));
                        callcls = int.Parse(msg.ItemValue("CCLS"));
                        cjhs = 0;
                        ccls = 0;

                        ThreadStart ts = new ThreadStart(DownLoadPlan);

                        td = new Thread(ts);
                        td.Start();
                        break;

                    case "UAZD05":
                        InvokeMethod(Global.frmCurrent, "LoadPlan", new object[] { });
                        break;

                    default:
                        break;
                }
            }


        }

        public static void ClearTaskOrder(string tch)
        {
            string sql = "delete from TaskOrder where TCH='" + tch + "'";
            SqlCe.ExecuteNonQuery(sql);

        }

        public static void ClearImpFrameLayout(string tch)
        {
            string sql = "delete from FrameLayerOut where ParkID='"+ tch +"'";
            SqlCe.ExecuteNonQuery(sql);
        }

        public static void ClearExpFrameLayout(string tch)
        {
            string sql = "delete from FrameLayerOutExp where ParkID='" + tch + "'";
            SqlCe.ExecuteNonQuery(sql);
        }

        public static void SendText(string datagram)
        {
            string data = datagram.Replace("'", "''");
            SqlCe.ExecuteNonQuery("insert into SendMsg(Msg,ID,SendTime,Lock,Done) values('" + "##" + data + "@@" + "',newID(),getDate(),0,0)");
        }

        public static void SaveConfig(string filename, string kb)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(filename);

            doc.DocumentElement.SelectSingleNode("//add[@key='Storage']").Attributes["value"].Value = kb;

            doc.Save(filename);

            doc = null;
        }


        public static void SaveConfig(string filename, string versionL, string versionR,string appName)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(filename);

            doc.DocumentElement.SelectSingleNode("//add[@key='VersionL']").Attributes["value"].Value = versionL;

            doc.DocumentElement.SelectSingleNode("//add[@key='VersionR']").Attributes["value"].Value = versionR;

            doc.DocumentElement.SelectSingleNode("//add[@key='Application Name']").Attributes["value"].Value = appName;

            doc.Save(filename);

            doc = null;
        }

        public static void SaveConfig(string filename, string ip,string port,string kb,string device,string debug)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(filename);

            doc.DocumentElement.SelectSingleNode("//add[@key='Server IP']").Attributes["value"].Value = ip;

            doc.DocumentElement.SelectSingleNode("//add[@key='Store Server Port']").Attributes["value"].Value = port;

            doc.DocumentElement.SelectSingleNode("//add[@key='Storage']").Attributes["value"].Value = kb;

            doc.DocumentElement.SelectSingleNode("//add[@key='Device']").Attributes["value"].Value = device;

            doc.DocumentElement.SelectSingleNode("//add[@key='Debug']").Attributes["value"].Value = debug;

            doc.Save(filename);

            doc = null;
        }
    }
}
