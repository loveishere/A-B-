#define InterfaceTest

using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;

namespace hStore
{
    public class TSocketClientBase<TSession> : IDisposable, ISessionEvent
    where TSession : TSessionBase, new()
    {
        #region  member fields

        private string m_serverIp;
        private int m_serverPort;
        private byte[] m_headDelimiter;
        private byte[] m_tailDelimiter;

        private Socket m_clientSocket;
        private bool m_clientClosed = true;
        private int m_connectServerTimeInterval = 1000;      // 重连服务器论询时间间隔(ms)
        private int m_checkSessionTableTimeInterval = 500;   // 清理Timer的时间间隔(ms)
        private int m_checkDatagramQueueTimeInterval = 500;  // 检查数据包队列时间休息间隔(ms)

        private int m_sessionSequenceNo = 0;  // sessionID 流水号
        private int m_sessionCount;
        private int m_receivedDatagramCount;
        private int m_errorDatagramCount;
        private int m_datagramQueueLength;

        private int m_clientExceptCount;
        private int m_sessionExceptionCount;


        private int m_maxSessionCount = 5;
        private int m_receiveBufferSize = 5 * 1024;  // 5 K
        private int m_sendBufferSize = 5 * 1024;

        private int m_maxDatagramSize = 2048;    // 2 K
        private int m_maxSessionTimeout = -1;    // 长链接，-1:不检查会话超时

        private Dictionary<int, TSession> m_sessionTable;

        private bool m_disposed = false;

        private ManualResetEvent m_checkConnectServerResetEvent;
        private ManualResetEvent m_checkSessionTableResetEvent;
        private ManualResetEvent m_checkDatagramQueueResetEvent;

        private BufferManager m_bufferManager;

        #endregion

        #region  public properties

        public bool Closed
        {
            get { return m_clientClosed; }
        }

        public bool Connected
        {
            get
            {
                bool bval = false;
                try
                {
                    if (m_sessionCount > 0)
                    {
                        TSession session = null;
                        lock (m_sessionTable)
                        {
                            session = (TSession)m_sessionTable[m_sessionSequenceNo];
                        }
                        if (session != null)
                        {
                            if (session.State == TSessionState.Active)
                            {
                                bval = true;
                            }
                        }
                    }
                }
                catch { }

                return bval;
            }
        }


        public int ServerPort
        {
            get { return m_serverPort; }
            set { m_serverPort = value; }
        }

        public string ServerIP
        {
            get { return m_serverIp; }
            set { m_serverIp = value; }

        }

        public byte[] HeadDelimiter
        {
            get { return m_headDelimiter; }
            set { m_headDelimiter = value; }
        }

        public string HeadDelimiterStr
        {
            get
            {
                return Encoding.Default.GetString(m_headDelimiter, 0, m_headDelimiter.Length);
            }
        }

        public byte[] TailDelimiter
        {
            get { return m_tailDelimiter; }
            set { m_tailDelimiter = value; }
        }

        public string TailDelimiterStr
        {
            get
            {
                return Encoding.Default.GetString(m_tailDelimiter, 0, m_tailDelimiter.Length);
            }
        }

        public int ClientExceptionCount
        {
            get { return m_clientExceptCount; }
        }

        public int SessionExceptionCount
        {
            get { return m_sessionExceptionCount; }
        }

        public int SessionCount
        {
            get { return m_sessionCount; }
        }

        public int ReceivedDatagramCount
        {
            get { return m_receivedDatagramCount; }
        }

        public int ErrorDatagramCount
        {
            get { return m_errorDatagramCount; }
        }

        public int DatagramQueueLength
        {
            get { return m_datagramQueueLength; }
        }

        public int ConnectServerTimeInterval
        {
            get { return m_connectServerTimeInterval; }
            set
            {
                if (value < 0)
                {
                    m_connectServerTimeInterval = 10;
                }
                else
                {
                    m_connectServerTimeInterval = value;
                }
            }
        }

        public int CheckSessionTableTimeInterval
        {
            get { return m_checkSessionTableTimeInterval; }
            set
            {
                if (value < 10)
                {
                    m_checkSessionTableTimeInterval = 10;
                }
                else
                {
                    m_checkSessionTableTimeInterval = value;
                }
            }
        }

        public int CheckDatagramQueueTimeInterval
        {
            get { return m_checkDatagramQueueTimeInterval; }
            set
            {
                if (value < 10)
                {
                    m_checkDatagramQueueTimeInterval = 10;
                }
                else
                {
                    m_checkDatagramQueueTimeInterval = value;
                }
            }
        }

        public int MaxSessionCount
        {
            get { return m_maxSessionCount; }
        }

        public int ReceiveBufferSize
        {
            get { return m_receiveBufferSize; }
        }

        public int SendBufferSize
        {
            get { return m_sendBufferSize; }
        }

        public int MaxDatagramSize
        {
            get { return m_maxDatagramSize; }
            set
            {
                if (value < 1024)
                {
                    m_maxDatagramSize = 1024;
                }
                else
                {
                    m_maxDatagramSize = value;
                }
            }
        }

        public int MaxSessionTimeout
        {
            get { return m_maxSessionTimeout; }
            set
            {
                m_maxSessionTimeout = value;
            }
        }

        public Collection<TSessionCoreInfo> SessionCoreInfoCollection
        {
            get
            {
                Collection<TSessionCoreInfo> sessionCollection = new Collection<TSessionCoreInfo>();
                lock (m_sessionTable)
                {
                    foreach (TSession session in m_sessionTable.Values)
                    {
                        sessionCollection.Add((TSessionCoreInfo)session);
                    }
                }
                return sessionCollection;
            }
        }

        #endregion

        #region  class events

        public event EventHandler ClientConnected;
        public event EventHandler ClientClosed;
        public event EventHandler<TExceptionEventArgs> ClientException;

        public event EventHandler SessionRejected;
        public event EventHandler<TSessionEventArgs> SessionConnected;
        public event EventHandler<TSessionEventArgs> SessionDisconnected;
        public event EventHandler<TSessionEventArgs> SessionTimeout;

        public event EventHandler<TSessionEventArgs> DatagramDelimiterError;
        public event EventHandler<TSessionEventArgs> DatagramOversizeError;
        public event EventHandler<TSessionExceptionEventArgs> SessionReceiveException;
        public event EventHandler<TSessionExceptionEventArgs> SessionSendException;
        public event EventHandler<TSessionEventArgs> DatagramAccepted;
        public event EventHandler<TSessionEventArgs> DatagramError;
        public event EventHandler<TSessionEventArgs> DatagramHandled;

        public event EventHandler<TMessageEventArgs> ShowDebugMessage;

        #endregion

        #region  class constructor

        public TSocketClientBase()
        {
            this.Initiate(m_maxSessionCount, m_receiveBufferSize, m_sendBufferSize);
        }

        public TSocketClientBase(int maxSessionCount, int receiveBufferSize, int sendBufferSize)
        {
            this.Initiate(maxSessionCount, receiveBufferSize, sendBufferSize);
        }

        public TSocketClientBase(string serverIP,int serverPort)
        {
            this.m_serverIp = serverIP;
            this.m_serverPort = serverPort;
            this.Initiate(m_maxSessionCount, m_receiveBufferSize, m_sendBufferSize);
        }

        private void Initiate(int maxSessionCount, int receiveBufferSize, int sendBufferSize)
        {
            m_maxSessionCount = maxSessionCount;
            m_receiveBufferSize = receiveBufferSize;
            m_sendBufferSize = sendBufferSize;

            m_bufferManager = new BufferManager(maxSessionCount, receiveBufferSize, sendBufferSize);
            m_sessionTable = new Dictionary<int, TSession>();

            m_checkConnectServerResetEvent = new ManualResetEvent(true);
            m_checkSessionTableResetEvent = new ManualResetEvent(true);
            m_checkDatagramQueueResetEvent = new ManualResetEvent(true);

        }


        ~TSocketClientBase()
        {
            this.Dispose(false);
        }

        public void Dispose()
        {
            if (!m_disposed)
            {
                m_disposed = true;
                this.Close();
                this.Dispose(true);
                GC.SuppressFinalize(this);  // Finalize 不会第二次执行
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)  // 对象正在被显示释放, 不是执行 Finalize()
            {
                m_sessionTable = null;  // 释放托管资源
            }


            if (m_checkConnectServerResetEvent != null)
            {
                m_checkConnectServerResetEvent.Close();  // 释放非托管资源
            }

            if (m_checkSessionTableResetEvent != null)
            {
                m_checkSessionTableResetEvent.Close();
            }

            if (m_checkDatagramQueueResetEvent != null)
            {
                m_checkDatagramQueueResetEvent.Close();
            }

            if (m_bufferManager != null)
            {
                m_bufferManager.Clear();
            }
        }

        #endregion

        #region  public methods

        public bool Start()
        {
            if (!m_clientClosed)
            {
                return true;
            }

            m_clientClosed = true;  // 在其它方法中要判断该字段

            this.Close();
            this.ClearCountValues();

            try
            {
                if (!ThreadPool.QueueUserWorkItem(this.CheckDatagramQueue)) return false;
                if (!ThreadPool.QueueUserWorkItem(this.StartClientConnect)) return false;
                if (!ThreadPool.QueueUserWorkItem(this.CheckSessionTable)) return false;

                m_clientClosed = false;

                this.OnClientConnected();
            }
            catch (Exception err)
            {
                this.OnClientException(err);
            }
            return !m_clientClosed;
        }

        public void Stop()
        {
            this.Close();
        }

        public void CloseSession(int sessionId)
        {
            TSession session = null;
            lock (m_sessionTable)
            {
                if (m_sessionTable.ContainsKey(sessionId))  // 包含该会话 ID
                {
                    session = (TSession)m_sessionTable[sessionId];
                }
            }

            if (session != null)
            {
                session.SetInactive();
            }
        }

        public void CloseAllSessions()
        {
            lock (m_sessionTable)
            {
                foreach (TSession session in m_sessionTable.Values)
                {
                    session.SetInactive();
                }
            }
        }

        public void SendToSession(int sessionId, string datagramText)
        {
            TSession session = null;
            lock (m_sessionTable)
            {
                session = (TSession)m_sessionTable[sessionId];
            }

            if (session != null)
            {
                session.SendDatagram(datagramText);
            }
        }

        public void SendToSession(string datagramText)
        {
            TSession session = null;
            lock (m_sessionTable)
            {
                session = (TSession)m_sessionTable[m_sessionSequenceNo];
            }

            if (session != null)
            {
                session.SendDatagram(datagramText);
            }
        }

        public void SendToAllSessions(string datagramText)
        {
            lock (m_sessionTable)
            {
                foreach (TSession session in m_sessionTable.Values)
                {
                    session.SendDatagram(datagramText);
                }
            }
        }

        #endregion

        #region  private methods
        /// <summary>
        /// 客户端连接请求
        /// </summary>
        private void StartClientConnect(object state)
        {
            m_checkConnectServerResetEvent.Reset();

            while (!m_clientClosed)
            {
                if (m_sessionTable.Count == 0)//一个客户端只允许一个会话
                {
                    try
                    {
                        System.Diagnostics.Debug.WriteLine("Conncet " + m_serverIp + " " + m_serverPort.ToString());
                        m_clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                        IPEndPoint iep = new IPEndPoint(IPAddress.Parse(m_serverIp), m_serverPort);
                        m_clientSocket.Connect(iep);

                        if (m_clientSocket != null && m_clientSocket.Connected)
                        {
                            this.AddSession(m_clientSocket, m_headDelimiter, m_tailDelimiter);  // 添加到队列中, 并调用异步接收方法
                        }
                        else
                        {
                            this.CloseClientSocket(m_clientSocket);
                        }

                    }
                    catch(Exception err)
                    {
                        System.Diagnostics.Debug.WriteLine(err.Message);
                        this.CloseClientSocket(m_clientSocket);
                        this.OnClientException(err);
                    }
                }
                Thread.Sleep(m_connectServerTimeInterval);
            }

            m_checkConnectServerResetEvent.Set();
        }

        /// <summary>
        /// 资源清理线程, 分若干步完成
        /// </summary>
        private void CheckSessionTable(object state)
        {
            m_checkSessionTableResetEvent.Reset();

            while (!m_clientClosed)
            {
                lock (m_sessionTable)
                {
                    List<int> sessionIDList = new List<int>();

                    foreach (TSession session in m_sessionTable.Values)
                    {
                        if (m_clientClosed)
                        {
                            break;
                        }

                        if (session.State == TSessionState.Inactive)  // 分三步清除一个 Session
                        {
                            session.Shutdown();  // 第一步: shutdown, 结束异步事件
                        }
                        else if (session.State == TSessionState.Shutdown)
                        {
                            session.Close();  // 第二步: Close
                        }
                        else if (session.State == TSessionState.Closed)
                        {
                            sessionIDList.Add(session.ID);
                            this.DisconnectSession(session);

                        }
                        else if(session.State==TSessionState.Active) // 正常的会话 Active
                        {
                            session.CheckTimeout(m_maxSessionTimeout); // 判超时，若是则标记
                        }
                    }

                    foreach (int id in sessionIDList)  // 统一清除
                    {
                        m_sessionTable.Remove(id);
                    }

                    sessionIDList.Clear();
                }

                Thread.Sleep(m_checkSessionTableTimeInterval);
            }

            m_checkSessionTableResetEvent.Set();
        }


        /// <summary>
        /// 数据包处理线程
        /// </summary>
        private void CheckDatagramQueue(object state)
        {
            m_checkDatagramQueueResetEvent.Reset();

            while (!m_clientClosed)
            {
                lock (m_sessionTable)
                {
                    foreach (TSession session in m_sessionTable.Values)
                    {
                        if (m_clientClosed)
                        {
                            break;
                        }

                        session.HandleDatagram();
                    }
                }
                Thread.Sleep(m_checkDatagramQueueTimeInterval);
            }

            m_checkDatagramQueueResetEvent.Set();
        }

        private void DisconnectSession(TSession session)
        {
            if (session.DisconnectType == TDisconnectType.Normal)
            {
                this.OnSessionDisconnected(session);
            }
            else if (session.DisconnectType == TDisconnectType.Timeout)
            {
                this.OnSessionTimeout(session);
            }
        }

        /// <summary>
        /// 输出调试信息
        /// </summary>
        private void OnShowDebugMessage(string message)
        {
            if (this.ShowDebugMessage != null)
            {
                TMessageEventArgs e = new TMessageEventArgs(message);
                this.ShowDebugMessage(this, e);
            }
        }

        /// <summary>
        /// 强制关闭客户端请求时的 Socket
        /// </summary>
        private void CloseClientSocket(Socket socket)
        {
            if (socket != null)
            {
                try
                {
                    socket.Shutdown(SocketShutdown.Both);
                    socket.Close();
                }
                catch (Exception) { }  // 强制关闭, 忽略错误
            }
        }

        private void CloseClientSocket()
        {
            if (m_clientSocket == null)
            {
                return;
            }

            try
            {
                lock (m_sessionTable)
                {
                    if (m_sessionTable != null && m_sessionTable.Count > 0)
                    {
                        // 可能结束服务器端的 AcceptClientConnect 的 Poll
                        //                        m_serverSocket.Shutdown(SocketShutdown.Both);  // 有连接才关
                    }
                }
                m_clientSocket.Close();
            }
            catch (Exception err)
            {
                this.OnClientException(err);
            }
            finally
            {
                m_clientSocket = null;
            }
        }

        private void ClearCountValues()
        {
            m_sessionCount = 0;
            m_receivedDatagramCount = 0;
            m_errorDatagramCount = 0;
            m_datagramQueueLength = 0;

            m_clientExceptCount = 0;
            m_sessionExceptionCount = 0;
        }

        private void Close()
        {
            if (m_clientClosed)
            {
                return;
            }

            m_clientClosed = true;

            m_checkConnectServerResetEvent.WaitOne();  // 等待3个线程
            m_checkSessionTableResetEvent.WaitOne();
            m_checkDatagramQueueResetEvent.WaitOne();

            if (m_sessionTable != null)
            {
                lock (m_sessionTable)
                {
                    foreach (TSession session in m_sessionTable.Values)
                    {
                        session.Close();
                    }
                }
            }

            this.CloseClientSocket();

            if (m_sessionTable != null)  // 清空会话列表
            {
                lock (m_sessionTable)
                {
                    m_sessionTable.Clear();
                }
            }

            this.OnClientClosed();
        }

        /// <summary>
        /// 增加一个会话对象
        /// </summary>
        private void AddSession(Socket clientSocket, byte[] headDelimiter, byte[] tailDelimiter)
        {
            Interlocked.Increment(ref m_sessionSequenceNo);

            TSession session = new TSession();
            session.Initiate(m_maxDatagramSize, m_sessionSequenceNo, clientSocket, m_bufferManager, Encoding.Default, headDelimiter, tailDelimiter);
            session.DatagramDelimiterError += new EventHandler<TSessionEventArgs>(this.OnDatagramDelimiterError);
            session.DatagramOversizeError += new EventHandler<TSessionEventArgs>(this.OnDatagramOversizeError);
            session.DatagramError += new EventHandler<TSessionEventArgs>(this.OnDatagramError);
            session.DatagramAccepted += new EventHandler<TSessionEventArgs>(this.OnDatagramAccepted);
            session.DatagramHandled += new EventHandler<TSessionEventArgs>(this.OnDatagramHandled);
            session.SessionReceiveException += new EventHandler<TSessionExceptionEventArgs>(this.OnSessionReceiveException);
            session.SessionSendException += new EventHandler<TSessionExceptionEventArgs>(this.OnSessionSendException);

            session.ShowDebugMessage += new EventHandler<TMessageEventArgs>(this.ShowDebugMessage);

            lock (m_sessionTable)
            {
                m_sessionTable.Add(session.ID, session);
            }
            session.ReceiveDatagram();

            this.OnSessionConnected(session);
        }

        #endregion

        #region  protected virtual methods
        protected virtual void OnSessionRejected()
        {
            EventHandler handler = this.SessionRejected;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        protected virtual void OnSessionConnected(TSession session)
        {
            Interlocked.Increment(ref m_sessionCount);

            EventHandler<TSessionEventArgs> handler = this.SessionConnected;
            if (handler != null)
            {
                TSessionEventArgs e = new TSessionEventArgs(session);
                handler(this, e);
            }
        }

        protected virtual void OnSessionDisconnected(TSession session)
        {
            Interlocked.Decrement(ref m_sessionCount);

            EventHandler<TSessionEventArgs> handler = this.SessionDisconnected;
            if (handler != null)
            {
                TSessionEventArgs e = new TSessionEventArgs(session);
                handler(this, e);
            }
        }

        protected virtual void OnSessionTimeout(TSession session)
        {
            Interlocked.Decrement(ref m_sessionCount);

            EventHandler<TSessionEventArgs> handler = this.SessionTimeout;
            if (handler != null)
            {
                TSessionEventArgs e = new TSessionEventArgs(session);
                handler(this, e);
            }
        }

        protected virtual void OnSessionReceiveException(object sender, TSessionExceptionEventArgs e)
        {
            Interlocked.Decrement(ref m_sessionCount);
            Interlocked.Increment(ref m_sessionExceptionCount);

            EventHandler<TSessionExceptionEventArgs> handler = this.SessionReceiveException;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnSessionSendException(object sender, TSessionExceptionEventArgs e)
        {
            Interlocked.Decrement(ref m_sessionCount);
            Interlocked.Increment(ref m_sessionExceptionCount);

            EventHandler<TSessionExceptionEventArgs> handler = this.SessionSendException;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnClientException(Exception err)
        {
            Interlocked.Increment(ref m_clientExceptCount);

            EventHandler<TExceptionEventArgs> handler = this.ClientException;
            if (handler != null)
            {
                TExceptionEventArgs e = new TExceptionEventArgs(err);
                handler(this, e);
            }
        }

        protected virtual void OnClientConnected()
        {
            EventHandler handler = this.ClientConnected;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        protected virtual void OnClientClosed()
        {
            EventHandler handler = this.ClientClosed;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        protected virtual void OnDatagramDelimiterError(object sender, TSessionEventArgs e)
        {
            Interlocked.Increment(ref m_errorDatagramCount);

            EventHandler<TSessionEventArgs> handler = this.DatagramDelimiterError;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnDatagramOversizeError(object sender, TSessionEventArgs e)
        {
            Interlocked.Increment(ref m_errorDatagramCount);

            EventHandler<TSessionEventArgs> handler = this.DatagramOversizeError;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnDatagramAccepted(object sender, TSessionEventArgs e)
        {
            Interlocked.Increment(ref m_receivedDatagramCount);
            Interlocked.Increment(ref m_datagramQueueLength);

            EventHandler<TSessionEventArgs> handler = this.DatagramAccepted;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnDatagramError(object sender, TSessionEventArgs e)
        {
            Interlocked.Increment(ref m_errorDatagramCount);
            Interlocked.Decrement(ref m_datagramQueueLength);

            EventHandler<TSessionEventArgs> handler = this.DatagramError;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnDatagramHandled(object sender, TSessionEventArgs e)
        {
            Interlocked.Decrement(ref m_datagramQueueLength);

            EventHandler<TSessionEventArgs> handler = this.DatagramHandled;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        #endregion
    }


    /// <summary>
    /// 会话类核心成员
    /// </summary>
    public class TSessionCoreInfo
    {
        #region  member fields

        private int m_id;
        private string m_ip = string.Empty;
        private string m_name = string.Empty;
        private TSessionState m_state = TSessionState.Active;
        private TDisconnectType m_disconnectType = TDisconnectType.Normal;

        private DateTime m_loginTime;
        private DateTime m_lastSessionTime;

        #endregion

        #region  public properties

        public int ID
        {
            get { return m_id; }
            protected set { m_id = value; }
        }

        public string IP
        {
            get { return m_ip; }
            protected set { m_ip = value; }
        }

        /// <summary>
        /// 数据包发送者的名称/编号
        /// </summary>
        public string Name
        {
            get { return m_name; }
            protected set { m_name = value; }
        }

        public DateTime LoginTime
        {
            get { return m_loginTime; }
            protected set
            {
                m_loginTime = value;
                m_lastSessionTime = value;
            }
        }

        public DateTime LastSessionTime
        {
            get { return m_lastSessionTime; }
            protected set { m_lastSessionTime = value; }
        }

        public TSessionState State
        {
            get { return m_state; }
            protected set
            {
                lock (this)
                {
                    m_state = value;
                }
            }
        }

        public TDisconnectType DisconnectType
        {
            get { return m_disconnectType; }
            protected set
            {
                lock (this)
                {
                    m_disconnectType = value;
                }
            }
        }

        #endregion
    }

    /// <summary>
    /// 会话基类(抽象类, 必须实现其 AnalyzeDatagram 方法)
    /// </summary>
    public abstract class TSessionBase : TSessionCoreInfo, ISessionEvent
    {
        #region  member fields

        private Socket m_socket;
        private int m_maxDatagramSize;

        private BufferManager m_bufferManager;

        private int m_bufferBlockIndex;
        private byte[] m_receiveBuffer;
        private byte[] m_sendBuffer;

        private byte[] m_datagramBuffer;
        private byte[] m_headDelimiter;
        private byte[] m_tailDelimiter;
        private Encoding m_coder;

        private Queue<byte[]> m_datagramQueue;

        #endregion

        #region class events

        public event EventHandler<TSessionExceptionEventArgs> SessionReceiveException;
        public event EventHandler<TSessionExceptionEventArgs> SessionSendException;
        public event EventHandler<TSessionEventArgs> DatagramDelimiterError;
        public event EventHandler<TSessionEventArgs> DatagramOversizeError;
        public event EventHandler<TSessionEventArgs> DatagramAccepted;
        public event EventHandler<TSessionEventArgs> DatagramError;
        public event EventHandler<TSessionEventArgs> DatagramHandled;

        public event EventHandler<TMessageEventArgs> ShowDebugMessage;

        #endregion

        #region  class constructor
        /// <summary>
        /// 作泛型参数类型时, 必须有无参构造函数
        /// </summary>
        protected TSessionBase() { }

        /// <summary>
        /// 替构造函数初始化对象
        /// </summary>
        public virtual void Initiate(int maxDatagramsize, int id, Socket socket, BufferManager bufferManager, Encoding coder, byte[] headDelimiter, byte[] tailDelimiter)
        {
            base.ID = id;
            base.LoginTime = DateTime.Now;

            m_bufferManager = bufferManager;
            m_bufferBlockIndex = bufferManager.GetBufferBlockIndex();
            m_coder = coder;
            m_headDelimiter = headDelimiter;
            m_tailDelimiter = tailDelimiter;

            if (m_bufferBlockIndex == -1)  // 没有空块, 新建
            {
                m_receiveBuffer = new byte[m_bufferManager.ReceiveBufferSize];
                m_sendBuffer = new byte[m_bufferManager.SendBufferSize];
            }
            else
            {
                m_receiveBuffer = m_bufferManager.ReceiveBuffer;
                m_sendBuffer = m_bufferManager.SendBuffer;
            }

            m_maxDatagramSize = maxDatagramsize;

            m_socket = socket;

            m_datagramQueue = new Queue<byte[]>();

            if (m_socket != null)
            {
                IPEndPoint iep = m_socket.RemoteEndPoint as IPEndPoint;
                if (iep != null)
                {
                    base.IP = iep.Address.ToString();
                }
            }
        }

        #endregion

        #region  public methods

        public void Shutdown()
        {
            lock (this)
            {
                if (this.State != TSessionState.Inactive || m_socket == null)  // Inactive 状态才能 Shutdown
                {
                    return;
                }

                this.State = TSessionState.Shutdown;
                try
                {
                    m_socket.Shutdown(SocketShutdown.Both);  // 目的：结束异步事件
                }
                catch (Exception) { }
            }
        }

        public void Close()
        {
            lock (this)
            {
                if (this.State != TSessionState.Shutdown || m_socket == null)  // Shutdown 状态才能 Close
                {
                    return;
                }

                m_datagramBuffer = null;

                if (m_datagramQueue != null)
                {
                    while (m_datagramQueue.Count > 0)
                    {

                        m_datagramQueue.Dequeue();
                    }
                    m_datagramQueue.Clear();
                }

                m_bufferManager.FreeBufferBlockIndex(m_bufferBlockIndex);

                try
                {
                    this.State = TSessionState.Closed;
                    m_socket.Close();
                }
                catch (Exception) { }
            }
        }

        public void SetInactive()
        {
            lock (this)
            {
                if (this.State == TSessionState.Active)
                {
                    this.State = TSessionState.Inactive;
                    this.DisconnectType = TDisconnectType.Normal;
                }
            }
        }

        public void HandleDatagram()
        {
            lock (this)
            {
                if (this.State != TSessionState.Active || m_datagramQueue.Count == 0)
                {
                    return;
                }

                byte[] datagramBytes = m_datagramQueue.Dequeue();
                this.AnalyzeDatagram(datagramBytes);
            }
        }

        public void ReceiveDatagram()
        {
            lock (this)
            {
                if (this.State != TSessionState.Active)
                {
                    return;
                }

                try  // 一个客户端连续做连接 或连接后立即断开，容易在该处产生错误，系统不认为是错误
                {
                    // 开始接受来自该客户端的数据
                    int bufferOffset = m_bufferManager.GetReceivevBufferOffset(m_bufferBlockIndex);
                    m_socket.BeginReceive(m_receiveBuffer, bufferOffset, m_bufferManager.ReceiveBufferSize, SocketFlags.None, this.EndReceiveDatagram, this);
                }
                catch (Exception err)  // 读 Socket 异常，准备关闭该会话
                {
                    this.DisconnectType = TDisconnectType.Exception;
                    this.State = TSessionState.Inactive;

                    this.OnSessionReceiveException(err);
                }
            }
        }

        public void SendDatagram(string datagramText)
        {
            lock (this)
            {
                if (this.State != TSessionState.Active)
                {
                    return;
                }

                try
                {
                    //int byteLength = m_coder.GetBytes(datagramText).Length;
                    //if (byteLength <= m_bufferManager.SendBufferSize)
                    //{
                    //    int bufferOffset = m_bufferManager.GetSendBufferOffset(m_bufferBlockIndex);
                    //    m_coder.GetBytes(datagramText, 0, datagramText.Length, m_sendBuffer, bufferOffset);
                    //    m_socket.BeginSend(m_sendBuffer, bufferOffset, byteLength, SocketFlags.None, this.EndSendDatagram, this);
                    //}
                    //else
                    //{
                        byte[] data = m_coder.GetBytes(datagramText);  // 获得数据字节数组
                        m_socket.BeginSend(data, 0, data.Length, SocketFlags.None, this.EndSendDatagram, this);
                    //}
                    System.Diagnostics.Debug.WriteLine(DateTime.Now.ToString("hh:mm:ss")+" Send:"+datagramText);
                }
                catch (Exception err)  // 写 socket 异常，准备关闭该会话
                {
                    System.Diagnostics.Debug.WriteLine("Socket Error:" + err.Message);
                    this.DisconnectType = TDisconnectType.Exception;
                    this.State = TSessionState.Inactive;

                    this.OnSessionSendException(err);
                }
            }
        }

        public void CheckTimeout(int maxSessionTimeout)
        {
            if (maxSessionTimeout > 0)
            {
                TimeSpan ts = DateTime.Now.Subtract(this.LastSessionTime);
                int elapsedSecond = Math.Abs((int)ts.TotalSeconds);

#if InterfaceTest
#else
                if (elapsedSecond > maxSessionTimeout)  // 超时，则准备断开连接
                {
                    System.Diagnostics.Debug.WriteLine("超时："+ elapsedSecond.ToString()+";" + DateTime.Now.ToString("hh:mm:ss") +";" + this.LastSessionTime.ToString("hh:mm:ss"));
                    this.DisconnectType = TDisconnectType.Timeout;
                    this.SetInactive();  // 标记为将关闭、准备断开
                }
#endif
            }
        }

        public bool CheckTimeout120()
        {
            TimeSpan ts = DateTime.Now.Subtract(this.LastSessionTime);
            int elapsedSecond = Math.Abs((int)ts.TotalSeconds);
            bool bTimeOut = false;

            if (elapsedSecond > 120)  // 超时，则准备断开连接
            {
                bTimeOut = true;
                //System.Diagnostics.Debug.WriteLine("超时：" + elapsedSecond.ToString() + ";" + DateTime.Now.ToString("hh:mm:ss") + ";" + this.LastSessionTime.ToString("hh:mm:ss"));
                //this.DisconnectType = TDisconnectType.Timeout;
                //this.SetInactive();  // 标记为将关闭、准备断开
            }
            
            return bTimeOut;
        }

        #endregion

        #region  private methods

        /// <summary>
        /// 发送数据完成处理函数, iar 为目标客户端 Session
        /// </summary>
        private void EndSendDatagram(IAsyncResult iar)
        {
            lock (this)
            {
                if (this.State != TSessionState.Active)
                {
                    return;
                }

                if (!m_socket.Connected)
                {
                    this.SetInactive();
                    return;
                }

                try
                {
                    m_socket.EndSend(iar);
                    iar.AsyncWaitHandle.Close();
                }
                catch (Exception err)  // 写 socket 异常，准备关闭该会话
                {
                    this.DisconnectType = TDisconnectType.Exception;
                    this.State = TSessionState.Inactive;

                    this.OnSessionSendException(err);
                }
            }
        }

        private void EndReceiveDatagram(IAsyncResult iar)
        {
            lock (this)
            {
                if (this.State != TSessionState.Active)
                {
                    return;
                }

                if (!m_socket.Connected)
                {
                    this.SetInactive();
                    return;
                }

                try
                {
                    // Shutdown 时将调用 ReceiveData，此时也可能收到 0 长数据包
                    int readBytesLength = m_socket.EndReceive(iar);
                    iar.AsyncWaitHandle.Close();

                    if (readBytesLength == 0)
                    {
                        this.DisconnectType = TDisconnectType.Normal;
                        this.State = TSessionState.Inactive;
                    }
                    else  // 正常数据包
                    {
                        this.LastSessionTime = DateTime.Now;
                        // 合并报文，按报文头、尾字符标志抽取报文，将包交给数据处理器
                        //this.OnDatagramAccepted();
                        this.ResolveSessionBuffer(readBytesLength);
                        this.ReceiveDatagram();  // 继续接收
                    }
                }
                catch (Exception err)  // 读 socket 异常，关闭该会话，系统不认为是错误（这种错误可能太多）
                {
                    if (this.State == TSessionState.Active)
                    {
                        this.DisconnectType = TDisconnectType.Exception;
                        this.State = TSessionState.Inactive;

                        this.OnSessionReceiveException(err);
                    }
                }
            }
        }

        /// <summary>
        /// 拷贝接收缓冲区的数据到数据缓冲区（即多次读一个包文）
        /// </summary>
        private void CopyToDatagramBuffer(int start, int length)
        {
            byte[] datagramBytes;
            //int datagramLength = 0;
            //if (m_datagramBuffer != null)
            //{
            //    datagramLength = m_datagramBuffer.Length;
            //}

            //Array.Resize(ref m_datagramBuffer, datagramLength + length);  // 调整长度（m_datagramBuffer 为 null 不出错）
            //Array.Copy(m_receiveBuffer, start, m_datagramBuffer, datagramLength, length);  // 拷贝到数据包缓冲区


            if (m_datagramBuffer != null)
            {

                datagramBytes = new byte[m_datagramBuffer.Length];
                Array.Copy(m_datagramBuffer, 0, datagramBytes, 0, m_datagramBuffer.Length);  // 先拷贝 Session 的数据缓冲区的数据
                m_datagramBuffer = new byte[datagramBytes.Length + length];
                Array.Copy(datagramBytes, 0, m_datagramBuffer, 0, datagramBytes.Length);

                Array.Copy(m_receiveBuffer, start, m_datagramBuffer, datagramBytes.Length, length);  // 再拷贝 Session 的接收缓冲区的数据
            }
            else
            {
                m_datagramBuffer = new byte[length];
                Array.Copy(m_receiveBuffer, start, m_datagramBuffer, 0, length);  // 再拷贝 Session 的接收缓冲区的数据
            }
        }

        #endregion

        #region protected methods

        /// <summary>
        /// 提取包时与包规则紧密相关，根据实际规则重定义
        /// </summary>
        protected virtual void ResolveSessionBuffer(int readBytesLength)
        {

            // 上次留下包文非空, 必然含包头
            bool hasHeadDelimiter = (m_datagramBuffer != null);

            int headDelimiter = m_headDelimiter.Length;
            int tailDelimiter = m_tailDelimiter.Length;

            int bufferOffset = m_bufferManager.GetReceivevBufferOffset(m_bufferBlockIndex);
            int start = bufferOffset;   // m_receiveBuffer 缓冲区中包开始位置
            int length = 0;  // 已经搜索的接收缓冲区长度

            int subIndex = bufferOffset;  // 缓冲区下标
            while (subIndex < readBytesLength + bufferOffset)
            {
                if (SearchHeadDelimiter(readBytesLength, bufferOffset, subIndex))  // 数据包开始字符<，前面包文作废
                {
                    if (hasHeadDelimiter || length > 0)  // 如果 包头 前面有数据，则认为错误包
                    {
                        this.OnDatagramDelimiterError();
                    }

                    m_datagramBuffer = null;  // 清空包缓冲区，开始一个新的包

                    start = subIndex;         // 新包起点，即<所在位置
                    length = headDelimiter;   // 新包的长度
                    hasHeadDelimiter = true;  // 新包有开始字符
                    subIndex += headDelimiter;
                }
                else if (SearchTailDelimiter(readBytesLength, bufferOffset, subIndex))
                {
                    if (hasHeadDelimiter)  // 两个缓冲区中有包头
                    {
                        length += tailDelimiter;  // 长度包括包尾

                        this.GetDatagramFromBuffer(start, length); // >前面的为正确格式的包

                        start = subIndex + tailDelimiter;  // 新包起点（一般一次处理将结束循环）
                        length = 0;  // 新包长度
                        hasHeadDelimiter = false;
                    }
                    else  // >前面没有开始字符，此时认为结束字符>为一般字符，待后续的错误包处理
                    {
                        length++;  //  hasHeadDelimiter = false;
                    }
                    subIndex += tailDelimiter;
                }
                else  // 即非 < 也非 >， 是一般字符，长度 + 1
                {
                    length++;
                    ++subIndex;
                }

            }

            if (length > 0)  // 剩下的待处理串，分两种情况
            {
                int mergedLength = length;
                if (m_datagramBuffer != null)
                {
                    mergedLength += m_datagramBuffer.Length;
                }

                // 剩下的包文含包头且不超长，转存到包文缓冲区中，待下次处理
                if (hasHeadDelimiter && mergedLength <= m_maxDatagramSize)
                {
                    this.CopyToDatagramBuffer(start, length);
                }
                else  // 不含首字符或超长
                {
                    this.OnDatagramOversizeError();
                    m_datagramBuffer = null;  // 丢弃全部数据
                }
            }
        }

        /// <summary>
        /// Session重写入口, 基本功能: 
        /// 1) 判断包有效性与包类型(注意：包带起止符号); 
        /// 2) 分解包中的各字段数据
        /// 3) 校验包及其数据有效性
        /// 4) 发送确认消息给客户端(调用方法 SendDatagram())
        /// 5) 存储包数据到数据库中
        /// 6) 存储包原文到数据库中(可选)
        /// 7) 补充字段m_name, 表示数据包发送者的名称/编号
        /// 8) 其它相关方法
        /// </summary>
        protected abstract void AnalyzeDatagram(byte[] datagramBytes);

        protected virtual bool SearchHeadDelimiter(int readBytesLength, int bufferOffset, int start)
        {
            if (start + m_headDelimiter.Length > readBytesLength + bufferOffset) return false;

            bool bVal = true;
            for (int i = 0; i < m_headDelimiter.Length; i++)
            {
                if (m_headDelimiter[i] != m_receiveBuffer[start + i])
                {
                    bVal = false;
                    break;
                }
            }

            return bVal;
        }

        protected virtual bool SearchTailDelimiter(int readBytesLength, int bufferOffset, int start)
        {
            if (start + m_tailDelimiter.Length > readBytesLength + bufferOffset) return false;

            bool bVal = true;
            for (int i = 0; i < m_tailDelimiter.Length; i++)
            {
                if (m_tailDelimiter[i] != m_receiveBuffer[start + i])
                {
                    bVal = false;
                    break;
                }
            }

            return bVal;
        }

        protected virtual void GetDatagramFromBuffer(int startPos, int len)
        {
            byte[] datagramBytes;
            if (m_datagramBuffer != null)
            {
                datagramBytes = new byte[len + m_datagramBuffer.Length];
                Array.Copy(m_datagramBuffer, 0, datagramBytes, 0, m_datagramBuffer.Length);  // 先拷贝 Session 的数据缓冲区的数据
                Array.Copy(m_receiveBuffer, startPos, datagramBytes, m_datagramBuffer.Length, len);  // 再拷贝 Session 的接收缓冲区的数据
            }
            else
            {
                datagramBytes = new byte[len];
                Array.Copy(m_receiveBuffer, startPos, datagramBytes, 0, len);  // 再拷贝 Session 的接收缓冲区的数据
            }

            if (m_datagramBuffer != null)
            {
                m_datagramBuffer = null;
            }

            System.Diagnostics.Debug.WriteLine(DateTime.Now.ToString("HH:mm:ss") + " " + Encoding.Default.GetString(datagramBytes, 0, datagramBytes.Length) + "\r\n");

            m_datagramQueue.Enqueue(datagramBytes);
        }

        protected virtual void OnDatagramDelimiterError()
        {
            EventHandler<TSessionEventArgs> handler = this.DatagramDelimiterError;
            if (handler != null)
            {
                TSessionEventArgs e = new TSessionEventArgs(this);
                handler(this, e);
            }
        }

        protected virtual void OnDatagramOversizeError()
        {
            EventHandler<TSessionEventArgs> handler = this.DatagramOversizeError;
            if (handler != null)
            {
                TSessionEventArgs e = new TSessionEventArgs(this);
                handler(this, e);
            }
        }

        protected virtual void OnDatagramAccepted()
        {
            EventHandler<TSessionEventArgs> handler = this.DatagramAccepted;
            if (handler != null)
            {
                TSessionEventArgs e = new TSessionEventArgs(this);
                handler(this, e);
            }
        }

        protected virtual void OnDatagramError()
        {
            EventHandler<TSessionEventArgs> handler = this.DatagramError;
            if (handler != null)
            {
                TSessionEventArgs e = new TSessionEventArgs(this);
                handler(this, e);
            }
        }

        protected virtual void OnDatagramHandled()
        {
            EventHandler<TSessionEventArgs> handler = this.DatagramHandled;
            if (handler != null)
            {
                TSessionEventArgs e = new TSessionEventArgs(this);
                handler(this, e);
            }
        }

        protected virtual void OnSessionReceiveException(Exception err)
        {
            EventHandler<TSessionExceptionEventArgs> handler = this.SessionReceiveException;
            if (handler != null)
            {
                TSessionExceptionEventArgs e = new TSessionExceptionEventArgs(err, this);
                handler(this, e);
            }
        }

        protected virtual void OnSessionSendException(Exception err)
        {
            EventHandler<TSessionExceptionEventArgs> handler = this.SessionSendException;
            if (handler != null)
            {
                TSessionExceptionEventArgs e = new TSessionExceptionEventArgs(err, this);
                handler(this, e);
            }
        }

        protected void OnShowDebugMessage(string message)
        {
            if (this.ShowDebugMessage != null)
            {
                TMessageEventArgs e = new TMessageEventArgs(message);
                this.ShowDebugMessage(this, e);
            }
        }

        #endregion
    }

    /// <summary>
    /// 发送和接收公共缓冲区
    /// </summary>
    public sealed class BufferManager
    {
        private byte[] m_receiveBuffer;
        private byte[] m_sendBuffer;

        private int m_maxSessionCount;
        private int m_receiveBufferSize;
        private int m_sendBufferSize;

        private int m_bufferBlockIndex;
        private Stack<int> m_bufferBlockIndexStack;

        public BufferManager(int maxSessionCount, int receivevBufferSize, int sendBufferSize)
        {
            m_maxSessionCount = maxSessionCount;
            m_receiveBufferSize = receivevBufferSize;
            m_sendBufferSize = sendBufferSize;

            m_bufferBlockIndex = 0;
            m_bufferBlockIndexStack = new Stack<int>();

            m_receiveBuffer = new byte[m_receiveBufferSize * m_maxSessionCount];
            m_sendBuffer = new byte[m_sendBufferSize * m_maxSessionCount];
        }

        public int ReceiveBufferSize
        {
            get { return m_receiveBufferSize; }
        }

        public int SendBufferSize
        {
            get { return m_sendBufferSize; }
        }

        public byte[] ReceiveBuffer
        {
            get { return m_receiveBuffer; }
        }

        public byte[] SendBuffer
        {
            get { return m_sendBuffer; }
        }

        public void FreeBufferBlockIndex(int bufferBlockIndex)
        {
            if (bufferBlockIndex == -1)
            {
                return;
            }

            lock (this)
            {
                m_bufferBlockIndexStack.Push(bufferBlockIndex);
            }
        }

        public int GetBufferBlockIndex()
        {
            lock (this)
            {
                int blockIndex = -1;

                if (m_bufferBlockIndexStack.Count > 0)  // 有用过释放的缓冲块
                {
                    blockIndex = m_bufferBlockIndexStack.Pop();
                }
                else
                {
                    if (m_bufferBlockIndex < m_maxSessionCount)  // 有未用缓冲区块
                    {
                        blockIndex = m_bufferBlockIndex++;
                    }
                }

                return blockIndex;
            }
        }

        public int GetReceivevBufferOffset(int bufferBlockIndex)
        {
            if (bufferBlockIndex == -1)  // 没有使用共享块
            {
                return 0;
            }

            return bufferBlockIndex * m_receiveBufferSize;
        }

        public int GetSendBufferOffset(int bufferBlockIndex)
        {
            if (bufferBlockIndex == -1)  // 没有使用共享块
            {
                return 0;
            }

            return bufferBlockIndex * m_sendBufferSize;
        }

        public void Clear()
        {
            lock (this)
            {
                m_bufferBlockIndexStack.Clear(); 
                m_receiveBuffer = null;
                m_sendBuffer = null;
            }
        }
    }

    public interface ISessionEvent
    {
        event EventHandler<TSessionExceptionEventArgs> SessionReceiveException;
        event EventHandler<TSessionExceptionEventArgs> SessionSendException;
        event EventHandler<TSessionEventArgs> DatagramDelimiterError;
        event EventHandler<TSessionEventArgs> DatagramOversizeError;
        event EventHandler<TSessionEventArgs> DatagramAccepted;
        event EventHandler<TSessionEventArgs> DatagramError;
        event EventHandler<TSessionEventArgs> DatagramHandled;
        event EventHandler<TMessageEventArgs> ShowDebugMessage;
    }

    public class TMessageEventArgs : EventArgs
    {
        private string m_Message;

        public TMessageEventArgs(string message)
        {
            m_Message = message;
        }

        public string Message
        {
            get { return m_Message; }
        }

    }

    public class TExceptionEventArgs : EventArgs
    {
        private string m_exceptionMessage;

        public TExceptionEventArgs(Exception exception)
        {
            m_exceptionMessage = exception.Message;
        }

        public string ExceptionMessage
        {
            get { return m_exceptionMessage; }
        }

    }

    public class TSessionEventArgs : EventArgs
    {
        TSessionCoreInfo m_sessionBaseInfo;

        public TSessionEventArgs(TSessionCoreInfo sessionCoreInfo)
        {
            m_sessionBaseInfo = sessionCoreInfo;
        }

        public TSessionCoreInfo SessionBaseInfo
        {
            get { return m_sessionBaseInfo; }
        }
    }

    public class TSessionExceptionEventArgs : TSessionEventArgs
    {
        private string m_exceptionMessage;

        public TSessionExceptionEventArgs(Exception exception, TSessionCoreInfo sessionCoreInfo)
            : base(sessionCoreInfo)
        {
            m_exceptionMessage = exception.Message;
        }

        public string ExceptionMessage
        {
            get { return m_exceptionMessage; }
        }
    }

    public enum TDisconnectType
    {
        Normal,     // disconnect normally
        Timeout,    // disconnect because of timeout
        Exception   // disconnect because of exception
    }

    public enum TSessionState
    {
        Active,    // state is active
        Inactive,  // session is inactive and will be closed
        Shutdown,  // session is shutdownling
        Closed     // session is closed
    }
}