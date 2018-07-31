using System;
using System.Net.Sockets;
using System.Net;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Runtime.InteropServices;

namespace hStore.GPRS
{
    /// <summary> 
    /// 提供Tcp网络连接服务的客户端类 
    /// 
    /// 原理: 
    /// 1.使用异步Socket通讯与服务器按照一定的通讯格式通讯,请注意与服务器的通 
    /// 讯格式一定要一致,否则可能造成服务器程序崩溃,整个问题没有克服,怎么从byte[] 
    /// 判断它的编码格式 
    /// 2.支持带标记的数据报文格式的识别,以完成大数据报文的传输和适应恶劣的网 
    /// 络环境. 
    /// </summary> 
    public class TcpCli
    {
        #region 字段

        private string _ip;
        private int _port;
        private Timer connectTimer;

        private string _msg = "";

        /// <summary> 
        /// 客户端与服务器之间的会话类 
        /// </summary> 
        private Session _session;

        /// <summary> 
        /// 客户端是否已经连接服务器 
        /// </summary> 
        private bool _isConnected = false;

        private bool _isLogout = false;

        /// <summary> 
        /// 接收数据缓冲区大小64K 
        /// </summary> 
        //public const int DefaultBufferSize = 4 * 1024 * 1024;
        public const int DefaultBufferSize = 64 * 1024;

        /// <summary> 
        /// 报文解析器 
        /// </summary> 
        private DatagramResolver _resolver;

        /// <summary> 
        /// 通讯格式编码解码器 
        /// </summary> 
        private Coder _coder;

        /// <summary> 
        /// 接收数据缓冲区 
        /// </summary> 
        private byte[] _recvDataBuffer = new byte[DefaultBufferSize];

        /// <summary>
        /// 客户端文件保存路径
        /// </summary>
        private string _filePath;

        #endregion

        #region 事件定义

        //需要订阅事件才能收到事件的通知，如果订阅者退出，必须取消订阅 

        /// <summary> 
        /// 已经连接服务器事件 
        /// </summary> 
        public event NetEvent ConnectedServer;

        /// <summary> 
        /// 接收到数据报文事件 
        /// </summary> 
        public event NetEvent ReceivedDatagram;

        /// <summary> 
        /// 连接断开事件 
        /// </summary> 
        public event NetEvent DisConnectedServer;


        public event NetEvent ThrowException;

        #endregion

        #region 属性

        /// <summary> 
        /// 返回客户端与服务器之间的会话对象 
        /// </summary> 
        public Session ClientSession
        {
            get
            {
                return _session;
            }
        }

        /// <summary> 
        /// 返回客户端与服务器之间的连接状态 
        /// </summary> 
        public bool IsConnected
        {
            get
            {
                return _isConnected;
            }
            set
            {
                _isConnected = value;
            }
        }

        /// <summary> 
        /// 数据报文分析器 
        /// </summary> 
        public DatagramResolver Resovlver
        {
            get
            {
                return _resolver;
            }
            set
            {
                _resolver = value;
            }
        }

        /// <summary> 
        /// 编码解码器 
        /// </summary> 
        public Coder ServerCoder
        {
            get
            {
                return _coder;
            }
        }
        /// <summary> 
        /// 客户端文件保存路径 
        /// </summary> 
        public string FilePath
        {
            get
            {
                return _filePath;
            }

        }
        #endregion

        #region 公有方法

        /// <summary> 
        /// 默认构造函数,使用默认的编码格式 
        /// </summary> 
        public TcpCli(string saveFilePath)
        {
            _coder = new Coder(Coder.EncodingMothord.Default);
            _filePath = saveFilePath;
        }

        /// <summary> 
        /// 构造函数,使用一个特定的编码器来初始化 
        /// </summary> 
        /// <param name="_coder">报文编码器</param> 
        public TcpCli(Coder coder, string saveFilePath)
        {
            _coder = coder;
            _filePath = saveFilePath;
        }

        public virtual void BeginConnectListener(int interval, string ip, int port)
        {
            _ip = ip;
            _port = port;

            connectTimer = new Timer(new TimerCallback(this.ConnectListener), null, 0, interval);
        }

        private void ConnectListener(object state)
        {

            if (_session != null)
            {
                if (_session.ClientSocket != null)
                {
                    if (_msg != "")
                    {
                        SendText(_msg);
                    }
                    else
                    {
                        SendText("alive"+Resovlver.EndTag);
                    }
                }
            }
            if (!_isConnected)
            {
                Connect(_ip, _port);
            }
        }

        /// <summary> 
        /// 连接服务器 
        /// </summary> 
        /// <param name="ip">服务器IP地址</param> 
        /// <param name="port">服务器端口</param> 
        /// 
        public virtual void Connect(string ip, int port)
        {
            if (_isConnected) return;

            _ip = ip;
            _port = port;
            _session = new Session();

            try
            {
                Socket newsocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPEndPoint iep = new IPEndPoint(IPAddress.Parse(_ip), _port);
                newsocket.BeginConnect(iep, new AsyncCallback(Connected), newsocket);
            }
            catch (SocketException se)
            {
                _session.Exception = se;
                ThrowException(this, new NetEventArgs(_session));
                _isConnected = false;
            }

        }

        /// <summary> 
        /// 发送数据报文 
        /// </summary> 
        /// <param name="datagram"></param> 
        public virtual void SendText(string datagram)
        {
            if (datagram.Length == 0)
            {
                return;
            }

            if (!_isConnected)
            {
                //throw (new ApplicationException("没有连接服务器，不能发送数据"));
                return;
            }

            //获得报文的编码字节 
            byte[] data = _coder.GetTextBytes(datagram);

            try
            {
                _session.ClientSocket.BeginSend(data, 0, data.Length, SocketFlags.None, new AsyncCallback(SendDataEnd), _session.ClientSocket);
            }
            catch (IOException ioe)
            {
                _session.Exception = ioe;
                ThrowException(this, new NetEventArgs(_session));
                _isConnected = false;
            }
            catch (SocketException se)
            {
                if (se.ErrorCode == 10060)
                {
                    //System.Collections.Queue msg = new System.Collections.Queue();
                    //msg.Enqueue(datagram);
                    _msg = datagram;
                }
                _session.Exception = se;
                ThrowException(this, new NetEventArgs(_session));
                _isConnected = false;
            }



        }
        public virtual void SendFile(string FilePath)
        {
            if (FilePath.Length == 0)
            {
                return;
            }

            if (!_isConnected)
            {
                throw (new ApplicationException("没有连接服务器，不能发送数据"));
            }

            if (File.Exists(FilePath))
            {
                byte[] data = _coder.GetFileBytes(FilePath);

                _session.ClientSocket.BeginSend(data, 0, data.Length, SocketFlags.None,
                    new AsyncCallback(SendDataEnd), _session.ClientSocket);
            }
            else
            {
                throw new Exception("文件不存在");
            }
        }

        //public virtual void Dispose()
        //{
        //    _session = null;
        //}

        /// <summary> 
        /// 关闭连接 
        /// </summary> 
        public virtual void Close()
        {
            if (connectTimer != null)
            {
                connectTimer.Change(Timeout.Infinite, 0);
                connectTimer.Dispose();
                connectTimer = null;
            }

            if (!_isConnected) return;

            _session.TypeOfExit = Session.ExitType.NormalExit;

            if (DisConnectedServer != null)
            {
                DisConnectedServer(this, new NetEventArgs(_session));
            }

            _session.Close();

            _session = null;

            _isLogout = true;

            _isConnected = false;
        }

        #endregion

        #region 受保护方法

        /// <summary> 
        /// 数据发送完成处理函数 
        /// </summary> 
        /// <param name="iar"></param> 
        protected virtual void SendDataEnd(IAsyncResult iar)
        {
            try
            {
                Socket remote = (Socket)iar.AsyncState;
                int sent = remote.EndSend(iar);
                _msg = "";
            }
            catch (IOException ioe)
            {
                _session.Exception = ioe;
                ThrowException(this, new NetEventArgs(_session));
                _isConnected = false;
            }
            catch (SocketException se)
            {
                _session.Exception = se;
                ThrowException(this, new NetEventArgs(_session));
                _isConnected = false;
            }

        }

        /// <summary> 
        /// 建立Tcp连接后处理过程 
        /// </summary> 
        /// <param name="iar">异步Socket</param> 
        protected virtual void Connected(IAsyncResult iar)
        {
            Socket socket = null;
            try
            {
                socket = (Socket)iar.AsyncState;
                socket.EndConnect(iar);

                //创建新的会话 
                _session = new Session(socket);

                _isConnected = true;

                _isLogout = false;


                //触发连接建立事件 
                if (ConnectedServer != null)
                {
                    ConnectedServer(this, new NetEventArgs(_session));
                }

            }
            catch (SocketException ex)
            {
                _isConnected = false;
                _session.Exception = ex;
                ThrowException(this, new NetEventArgs(_session));
            }

            if (_isConnected)
            {
                try
                {
                    //建立连接后应该立即接收数据 
                    if (socket.Connected)
                    {
                        _session.ClientSocket.BeginReceive(_recvDataBuffer, 0, DefaultBufferSize, SocketFlags.None, new AsyncCallback(RecvData), socket);
                    }
                }
                catch (SocketException se)
                {
                    _isConnected = false;
                    _session.Exception = se;
                    ThrowException(this, new NetEventArgs(_session));
                }
            }
        }

        /// <summary> 
        /// 数据接收处理函数 
        /// </summary> 
        /// <param name="iar">异步Socket</param> 
        protected virtual void RecvData(IAsyncResult iar)
        {
            if (_isLogout == true) return;

            Socket remote = (Socket)iar.AsyncState;
            int recv = 0;

            try
            {
                recv = remote.EndReceive(iar);
            }
            catch (SocketException ex)
            {
                _isConnected = false;
                _session.Exception = ex;
                ThrowException(this, new NetEventArgs(_session));

                //客户端退出 
                if (ex.ErrorCode == 10054)
                {
                    //服务器强制的关闭连接，强制退出 
                    _session.TypeOfExit = Session.ExitType.ExceptionExit;

                    if (DisConnectedServer != null)
                    {
                        DisConnectedServer(this, new NetEventArgs(_session));
                    }
                }
                return;
            }
            catch (ObjectDisposedException ex)
            {
                _isConnected = false;
                _session.Exception = ex;
                ThrowException(this, new NetEventArgs(_session));
                return;
            }

            //正常的退出 
            if (recv == 0)
            {
                _session.TypeOfExit = Session.ExitType.ExceptionExit;

                if (DisConnectedServer != null)
                {
                    DisConnectedServer(this, new NetEventArgs(_session));
                }
                _isConnected = false;
                return;
            }
            string receivedData = "";

            if (_recvDataBuffer[0] == 0x23 && _recvDataBuffer[1] == 0x23)
            {
                receivedData = _coder.GetEncodingString(_recvDataBuffer, 0, recv);
                App.WriteLog(receivedData);
            }
            /*
            else
            {
                string fileName = this._coder.GetEncodingString(_recvDataBuffer,6,_recvDataBuffer[1]-1);
                _coder.SaveFile(_filePath + fileName, _recvDataBuffer);
                receivedData = "Receive File:" + fileName + "##";
            }
            */
            //通过事件发布收到的报文 
            if (ReceivedDatagram != null)
            {
                if (_session.Datagram != null && _session.Datagram.Length != 0)
                {
                    //加上最后一次通讯剩余的报文片断 
                    receivedData = _session.Datagram + receivedData;
                }

                string[] recvDatagrams = _resolver.Resolve(ref receivedData);


                foreach (string newDatagram in recvDatagrams)
                {
                    //Need Deep Copy.因为需要保证多个不同报文独立存在 
                    ICloneable copySession = (ICloneable)_session;

                    Session clientSession = (Session)copySession.Clone();

                    clientSession.Datagram = newDatagram;

                    //发布一个报文消息 
                    ReceivedDatagram(this, new NetEventArgs(clientSession));
                }

                //剩余的代码片断,下次接收的时候使用 
                _session.Datagram = receivedData;


            }

            //继续接收数据 
            try
            {
                _session.ClientSocket.BeginReceive(_recvDataBuffer, 0, DefaultBufferSize, SocketFlags.None, new AsyncCallback(RecvData), _session.ClientSocket);
            }
            catch (SocketException se)
            {
                _isConnected = false;
                _session.Exception = se;
                ThrowException(this, new NetEventArgs(_session));
            }
        }

        #endregion


    }
}
