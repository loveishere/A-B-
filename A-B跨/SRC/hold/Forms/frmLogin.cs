using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlServerCe;
using System.Threading;
using System.IO;
using System.Reflection;

namespace hStore.Forms
{
    public partial class frmLogin : Form
    {
        private string sVersionR = "";

        private string sVersionL = "";

        private string sFileNameL = "";

        private string sFilePathL = "";

        private string sFilePathLu = "";

        private bool bChkVersion = false;

        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnQd_Click(object sender, EventArgs e)
        {
            if (Global.sUserId != "")
            {
                frmMain frmMain = new frmMain();
                Global.frmCurrent = frmMain;
                frmMain.Owner = this;
                frmMain.Show();
                this.Hide();
                return;
            }

            if (txtGh.Text == "")
            {
                new frmMessage().ShowDialog("请输入工号！", "提示");
                return;
            }

            if (txtGh.Text != "BS")
            {
                if (txtMm.Text == "")
                {
                    new frmMessage().ShowDialog("请输入密码！", "提示");
                    return;
                }
            }

            if (cboBb.SelectedIndex == -1)
            {
                new frmMessage().ShowDialog("请选择班别！", "提示");
                return;
            }
            if (cboZyq.SelectedIndex == -1)
            {
                new frmMessage().ShowDialog("请选择作业区！", "提示");
                return;
            }

            string sGh = txtGh.Text;
            string sMm = txtMm.Text;
            string sBb = "";
            if (cboBb.Text == "白")
            {
                sBb = "22";
            }
            else if (cboBb.Text == "夜")
            {
                sBb = "21";
            }
            string sZyq = cboZyq.Text;

            if (cboZyq.Text == "甲")
            {
                sZyq = "1";
            }
            else if (cboZyq.Text == "乙")
            {
                sZyq = "2";
            }
            else if (cboZyq.Text == "丙")
            {
                sZyq = "3";
            }
            else if (cboZyq.Text == "丁")
            {
                sZyq = "4";
            }

            if (sGh != "BS")
            {
                string sql = "select PWD from UserInf where UserID='" + sGh + "'";
                DataTable dt = SqlCe.ExecuteQuery(sql);
                if(dt.Rows.Count>0)
                {
                    if (dt.Rows[0]["PWD"].ToString() == sMm)
                    {
                        Global.sUserId = sGh;
                        Global.sPassword = sMm;
                        Global.sBb = sBb;
                        Global.sZyq = sZyq;
                    }
                }
                dt.Dispose();

                if (Global.sUserId != "")//登陆成功
                {
                    sql = "insert into UserLogin (UserID,Class,StartTime) values (";
                    sql += "'" + sGh + "',";
                    sql += "'" + sBb + "',";
                    //sql += "'" + sHch + "',";
                    sql += "'" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'";
                    sql += ")";
                    SqlCe.ExecuteNonQuery(sql);

                    //Business.SaveConfig(Global.appPath + @"\config.xml", sHch);
                    //Global.sHch = sHch;
                }
                else
                {
                    new frmMessage().ShowDialog("工号或者密码错误！", "提示");
                }

                if (Global.sUserId != "")
                {
                    if (Global.sDebug == "False")
                    {
                        Business.SendText(Business.msg.Package("ZDUA09", System.DateTime.Today.ToString("yyyyMMdd") + ";" + sBb + ";" + sGh + ";" + sZyq + ";" + System.DateTime.Now.ToString("yyyyMMddHHmmss")));
                    }

                    frmMain frmMain = new frmMain();
                    Global.frmCurrent = frmMain;
                    frmMain.Owner = this;
                    frmMain.Show();
                    this.Hide();
                }
            }
            else
            {
                Global.sUserId = sGh;
                Global.sPassword = sMm;
                Global.sBb = sBb;
                Global.sZyq = sZyq;

                frmMain frmMain = new frmMain();
                Global.frmCurrent = frmMain;
                frmMain.Owner = this;
                frmMain.Show();
                this.Hide();
            }

        }

        private void btnQx_Click(object sender, EventArgs e)
        {
            txtGh.Text = "";
            txtMm.Text = "";
            Global.sBb = "";
            Global.sUserId = "";
            Global.sPassword = "";
            cboBb.SelectedIndex = -1;
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            showConnect();
            cboBb.SelectedIndex = -1;
            //txtHch.Text = Global.sHch;

            sFilePathL = Assembly.GetExecutingAssembly().ManifestModule.FullyQualifiedName;
            //   \Flash Disk\Store.exe
            sFileNameL = Path.GetFileName(sFilePathL);
            //   Store.exe
            sFilePathLu = Path.GetDirectoryName(sFilePathL) + @"\Update\";
            //   \Flash Disk\Update\

            sVersionL = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            lblVersion.Text = sVersionL;

            this.lblMsg.Text = Business.info;
        }

        public void showConnect()
        {
            Bitmap bitmap = null;
            if (Global.wwanConnected)
            {
                bitmap = new Bitmap(Global.appPath + @"\img\connect.bmp");
            }
            else
            {
                bitmap = new Bitmap(Global.appPath + @"\img\disconnect.bmp");
            }
            Global.UIThread(picCon, delegate { picCon.Image = Image.FromHbitmap(bitmap.GetHbitmap()); });
        }

        public void showMessage(string msg)
        {
            Global.UIThread(this, delegate { lblMsg.Text = msg; });
        }

        private void frmLogin_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                aliveTimer.Enabled = false;
                recvTimer.Enabled = false;
                sendFailTimer.Enabled = false;
                sendTimer.Enabled = false;
                Global.Exit();
                Application.Exit();
            }
            if (e.KeyCode == Keys.F3)
            {
                getUserInf();
            }
            if (e.KeyCode == Keys.F4)
            {
                frmMessage frmMessage = new frmMessage();
                frmMessage.ShowDialog("是否恢复系统文件？", "提示", "确定", "取消");
                if (frmMessage.ret == true)
                {
                    FileInfo iFile = new FileInfo(Global.appPath + @"\s.sdf");
                    iFile.CopyTo(Global.appPath + @"\update\store.sdf", true);

                    PsionTeklogix.Power.PowerManagement.WarmBoot();
                }
                frmMessage.Dispose();
            }
        }

        //private void txtKb_KeyUp(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode >= Keys.A && e.KeyCode <= Keys.Z)
        //    {
        //        txtHch.Text = txtHch.Text.ToUpper();
        //        txtHch.SelectionStart = txtHch.Text.Length;
        //    }
        //}


        private void recvTimer_Tick(object sender, EventArgs e)
        {
            recvTimer.Enabled = false;
            string sql = "select * from RecvMsg where Lock=0 and Done=0 order by RecvTime";
            DataTable dt = SqlCe.ExecuteQuery(sql);
            string DataGram = "";


            //##0109UAZD02S0301;B232;20;43900;1500;N;2;;;;43900;7800;2400;1700;780;C9;;;;43900;10000;2400;1700;780;C99999@@

            string id = "";

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataGram = dt.Rows[i]["Msg"].ToString();
                id = dt.Rows[i]["ID"].ToString();
                Global.RecvData(DataGram);
                SqlCe.ExecuteNonQuery("delete RecvMsg where ID='" + id + "'");
            }
            dt.Dispose();
            recvTimer.Enabled = true;
        }

        private void sendTimer_Tick(object sender, EventArgs e)
        {
            sendTimer.Enabled = false;

            if (Global.socketClient.Connected)
            {
                DataTable dt = SqlCe.ExecuteQuery("select * from SendMsg where Lock=0 and Done=0 order by SendTime ");
                if (dt.Rows.Count > 0)
                {
                    string datagram = dt.Rows[0]["Msg"].ToString();
                    string id = dt.Rows[0]["ID"].ToString();
                    //if (datagram.IndexOf("ZDUA11") > 0)//清盘库信息更新
                    //{
                    //    if (Business.msg.UnPackage(datagram))
                    //    {
                    //        SqlCe.ExecuteNonQuery("update SendMsg set Lock=1,GramTime='" + Business.msg.ItemValue("fssj") + "',GramID='" + Business.msg.MessageID + "' where ID='" + id + "'");
                    //        Global.socketClient.SendToSession(datagram);
                    //    }
                    //    else
                    //    {
                    //        SqlCe.ExecuteNonQuery("delete from SendMsg where ID='" + id + "'");
                    //    }

                    //}
                    //else
                    //{
                        Global.socketClient.SendToSession(datagram);
                        SqlCe.ExecuteNonQuery("delete from SendMsg where ID='" + id + "'");
                    //}

                }
                dt.Dispose();
            }

            sendTimer.Enabled = true;
        }

        private void sendFailTimer_Tick(object sender, EventArgs e)
        {
            sendFailTimer.Enabled = false;

            SqlCe.ExecuteNonQuery("update SendMsg set Lock=0 where Lock=1 and GramID<>'' and GramTime<>''");
            sendFailTimer.Enabled = true;
        }

        private void aliveTimer_Tick(object sender, EventArgs e)
        {
            aliveTimer.Enabled = false;
            WebService.Service ws = new WebService.Service();

            AsyncCallback HelloWorldCallBack = new AsyncCallback(HelloWorldResult);
            ws.BeginHelloWorld(HelloWorldCallBack, ws);

            if (Global.socketClient.Connected)
            {
                 Global.socketClient.SendToSession("##0024ZDUA00alive;9999@@");
            }
            Business.InvokeMethod(Global.frmCurrent, "showConnect", new object[] { });

            aliveTimer.Enabled = true;
        }

        private void pic_Click(object sender, EventArgs e)
        {
            frmDebug frmDebug = new frmDebug();
            frmDebug.Owner = this;
            frmDebug.Show();
            this.Hide();
        }

        public void HelloWorldResult(IAsyncResult ar)
        {
            bool bval = false;

            WebService.Service ws = (WebService.Service)ar.AsyncState;
            try
            {
                string ip = ws.EndHelloWorld(ar);
                if (ip.IndexOf('.') > 0)
                {
                    bval = true;

                    if (bChkVersion == false)
                    {
                        WebService.Service wss = new hStore.WebService.Service();
                        AsyncCallback GetCurrentTimeCallBack = new AsyncCallback(GetCurrentTimeResult);
                        wss.BeginGetCurrentTime(GetCurrentTimeCallBack, wss);

                        //Thread td = new Thread(new ThreadStart(GetRemoteVersion));
                        //td.Start();
                        bChkVersion = true;
                    }
                }

                Global.wwanConnected = bval;

            }
            catch (Exception e)
             {
                System.Diagnostics.Debug.WriteLine("HelloWorldResult Exception:" + e.Message);
            }
            finally
            {
                ws.Dispose();
            }
        }


        public void GetCurrentTimeResult(IAsyncResult ar)
        {

            WebService.Service ws = (WebService.Service)ar.AsyncState;
            try
            {
                DateTime dt= ws.EndGetCurrentTime(ar);

                Business.SetCurrentTime(dt);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("GetCurrentTimeResult Exception:" + e.Message);
            }
            finally
            {
                ws.Dispose();
            }
        }

        //private void GetRemoteVersion()
        //{
        //    WebService.Service ws = new WebService.Service();
        //    AsyncCallback GetVersionInfoNoneCallBack = new AsyncCallback(GetVersionInfoNoneResult);
        //    ws.BeginGetVersionInfoNone(sFileNameL, GetVersionInfoNoneCallBack, ws);
        //}

        private void getUserInf()
        {
            frmMessage frmMessage = new frmMessage();
            frmMessage.ShowDialog("是否更新用户资料库？", "提示", "确定", "取消");
            if (frmMessage.ret==true)
            {
                WebService.Service ws = new WebService.Service();
                AsyncCallback GetUserInfCallBack = new AsyncCallback(GetUserInfResult);
                ws.BegingetUserInf("3", GetUserInfCallBack, ws);
            }


        }

        public void GetUserInfResult(IAsyncResult ar)
        {
            WebService.Service ws = (WebService.Service)ar.AsyncState;
            try
            {
                string sql;
                string userid;
                string pwd;

                DataTable dt = new DataTable();
                try
                {
                    dt = ws.EndgetUserInf(ar);

                    sql = "delete from userinf";
                    SqlCe.ExecuteNonQuery(sql);

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        userid = dt.Rows[i]["USERID"].ToString();
                        pwd = dt.Rows[i]["PWD"].ToString();

                        sql = "insert into userinf (USERID,PWD) values('" + userid + "','" + pwd + "')";
                        SqlCe.ExecuteNonQuery(sql);
                    }

                    frmMessage frmMessage = new frmMessage();
                    frmMessage.ShowDialog("用户资料库已更新！", "提示", "确定");
                    frmMessage.Dispose();

                }
                catch(Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e.Message);
                }
                finally
                {
                    dt.Dispose();
                }
            }
            catch { }
            finally
            {
                ws.Dispose();
            }
        }

        public void GetVersionInfoNoneResult(IAsyncResult ar)
        {
            WebService.Service ws = (WebService.Service)ar.AsyncState;
            try
            {
                DataTable dt = new DataTable();
                bool bUpdate = false;

                try
                {
                    dt = ws.EndGetVersionInfoNone(ar);

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["FILENAME"].ToString().ToUpper() == sFileNameL.ToUpper())
                        {
                            sVersionR = dt.Rows[i]["VERSIONNO"].ToString();
                            break;
                        }
                    }
                }
                catch
                { }
                finally
                {
                    dt.Dispose();
                }


                bUpdate = CheckVersion(sVersionL, sVersionR);

                if (bUpdate)
                {
                    WebService.Service ws2 = new WebService.Service();
                    AsyncCallback GetVersionInfo2CallBack = new AsyncCallback(GetVersionInfo2Result);
                    ws.BeginGetVersionInfo2(sFileNameL, sVersionL, GetVersionInfo2CallBack, ws2);
                }

            }
            catch
            {
            }
            finally
            {
                ws.Dispose();
            }
        }

        public void GetVersionInfo2Result(IAsyncResult ar)
        {
            WebService.Service ws = (WebService.Service)ar.AsyncState;
            try
            {

                DataTable dtRaw = new DataTable();
                try
                {
                    dtRaw = ws.EndGetVersionInfo2(ar);
                    string fname = "";

                    for (int i = 0; i < dtRaw.Rows.Count; i++)
                    {
                        if (dtRaw.Rows[i]["FILEINFO"] != DBNull.Value)
                        {
                            fname = dtRaw.Rows[i]["FILENAME"].ToString();
                            if (File.Exists(sFilePathLu + fname))
                            {
                                File.Delete(sFilePathLu + fname);

                            }
                            byte[] tmpFile = (byte[])dtRaw.Rows[i]["FILEINFO"];
                            FileStream fs = new FileStream(sFilePathLu + fname, FileMode.CreateNew);
                            BinaryWriter bw = new BinaryWriter(fs);
                            bw.Write(tmpFile, 0, tmpFile.Length);
                            bw.Close();
                            fs.Close();
                        }
                    }

                    SetVersion(sVersionL + "+");

                    Business.SaveConfig(Global.appPath + @"\config.xml", sVersionL, sVersionR, sFileNameL);
                }
                catch
                {
                }
                finally
                {
                    dtRaw.Dispose();
                }
            }
            catch
            {
            }
            finally
            {
                ws.Dispose();
            }
        }

        private bool CheckVersion(string LVersion, string RVersion)
        {
            bool proUpdate = false;

            if (RVersion != "")
            {
                if (LVersion == "")
                {
                    proUpdate = true;
                }
                else
                {
                    string[] dVer = RVersion.Split('.');
                    string[] lVer = LVersion.Split('.');

                    if (Convert.ToInt32(dVer[0]) > Convert.ToInt32(lVer[0]))
                    {
                        proUpdate = true;
                    }
                    else
                    {
                        if (Convert.ToInt32(dVer[1]) > Convert.ToInt32(lVer[1]))
                        {
                            proUpdate = true;
                        }
                        else
                        {
                            if (Convert.ToInt32(dVer[2]) > Convert.ToInt32(lVer[2]))
                            {
                                proUpdate = true;
                            }
                            else
                            {
                                if (Convert.ToInt32(dVer[3]) > Convert.ToInt32(lVer[3]))
                                {
                                    proUpdate = true;
                                }
                            }
                        }
                    }

                }
            }

            return proUpdate;
        }

        private void SetVersion(string version)
        {
            Global.UIThread(lblVersion, delegate { lblVersion.Text = version; });
        }

        private void txtGh_KeyUp(object sender, KeyEventArgs e)
        {
            txtGh.Text = txtGh.Text.ToUpper();
            txtGh.SelectionStart = txtGh.Text.Length;
        }

        private void txtMm_KeyUp(object sender, KeyEventArgs e)
        {
            txtMm.Text = txtMm.Text.ToUpper();
            txtMm.SelectionStart = txtMm.Text.Length;
        }
    }
}