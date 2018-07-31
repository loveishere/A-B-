using System;
using System.Net.Sockets;
using System.Diagnostics;

namespace hStore.GPRS
{
    /// <summary> 
    /// 客户端与服务器之间的会话类 
    /// 
    /// 说明: 
    /// 会话类包含远程通讯端的状态,这些状态包括Socket,报文内容, 
    /// 客户端退出的类型(正常关闭,强制退出两种类型) 
    /// </summary> 
    public class Session : ICloneable
    {
        #region 字段

        // 会话ID 
        private SessionId _id;

        // 客户端发送到服务器的报文 
        // 注意:在有些情况下报文可能只是报文的片断而不完整 
        private string _datagram;

        // 客户端的Socket 
        private Socket _cliSock;

        // 客户端的退出类型 
        private ExitType _exitType;

        // 错误消息 
        private Exception _ex;

        // 退出类型枚举 
        public enum ExitType
        {
            NormalExit,
            ExceptionExit
        };

        #endregion

        #region 属性

        public string LocalPort
        {
            get
            {
                return _cliSock.LocalEndPoint.ToString().Split(':')[1];
            }
        }

        public string RemotePort
        {
            get
            {
                return _cliSock.RemoteEndPoint.ToString().Split(':')[1];
            }
        }

        // 返回会话的ID 
        public SessionId ID
        {
            get
            {
                return _id;
            }
        }

        // 存取会话的报文 
        public string Datagram
        {
            get
            {
                return _datagram;
            }
            set
            {
                _datagram = value;
            }
        }

        // 存取错误消息
        public Exception Exception
        {
            get
            {
                return _ex;
            }
            set
            {
                _ex = value;
            }
        }

        // 获得与客户端会话关联的Socket对象 
        public Socket ClientSocket
        {
            get
            {
                return _cliSock;
            }
            set
            {
                _cliSock = value;
                _id = new SessionId((int)_cliSock.Handle);
            }

        }

        // 存取客户端的退出方式 
        public ExitType TypeOfExit
        {
            get
            {
                return _exitType;
            }

            set
            {
                _exitType = value;
            }
        }

        #endregion

        #region 方法

        // 使用Socket对象的Handle值作为HashCode,它具有良好的线性特征. 
        public override int GetHashCode()
        {
            return (int)_cliSock.Handle;
        }

        // 返回两个Session是否代表同一个客户端 
        public override bool Equals(object obj)
        {
            Session rightObj = (Session)obj;

            return (int)_cliSock.Handle == (int)rightObj.ClientSocket.Handle;

        }

        // 重载ToString()方法,返回Session对象的特征 
        public override string ToString()
        {
            string result = string.Format("Session:{0},IP:{1}", _id, _cliSock.LocalEndPoint.ToString());

            return result;
        }

        // 构造函数 
        public Session(Socket cliSock)
        {
            if (cliSock != null)
            {
                _cliSock = cliSock;

                _id = new SessionId((int)cliSock.Handle);
            }
        }

        public Session()
        {
            _cliSock = null;
            _id = new SessionId(0);
        }

        // 关闭会话 
        public void Close()
        {
            if (_cliSock != null)
            {

                //关闭数据的接受和发送 
                _cliSock.Shutdown(SocketShutdown.Both);

                //清理资源 
                _cliSock.Close();

                _cliSock = null;
            }
        }

        #endregion

        #region ICloneable 成员

        object System.ICloneable.Clone()
        {
            Session newSession = new Session(_cliSock);
            newSession.Datagram = _datagram;
            newSession.TypeOfExit = _exitType;
            return newSession;
        }

        #endregion
    }


    /// <summary> 
    /// 唯一的标志一个Session,辅助Session对象在Hash表中完成特定功能 
    /// </summary> 
    public class SessionId
    {
        /// <summary> 
        /// 与Session对象的Socket对象的Handle值相同,必须用这个值来初始化它 
        /// </summary> 
        private int _id;

        /// <summary> 
        /// 返回ID值 
        /// </summary> 
        public int ID
        {
            get
            {
                return _id;
            }
        }

        /// <summary> 
        /// 构造函数 
        /// </summary> 
        /// <param name="id">Socket的Handle值</param> 
        public SessionId(int id)
        {
            _id = id;
        }

        /// <summary> 
        /// 重载.为了符合Hashtable键值特征 
        /// </summary> 
        /// <param name="obj"></param> 
        /// <returns></returns> 
        public override bool Equals(object obj)
        {
            if (obj != null)
            {
                SessionId right = (SessionId)obj;

                return _id == right._id;
            }
            else if (this == null)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        /// <summary> 
        /// 重载.为了符合Hashtable键值特征 
        /// </summary> 
        /// <returns></returns> 
        public override int GetHashCode()
        {
            return _id;
        }

        /// <summary> 
        /// 重载,为了方便显示输出 
        /// </summary> 
        /// <returns></returns> 
        public override string ToString()
        {
            return _id.ToString();
        }

    }

}
