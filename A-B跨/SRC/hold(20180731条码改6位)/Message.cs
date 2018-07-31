using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Text.RegularExpressions;

namespace hStore
{
    public class GramMessage
    {
        private int _length;                       //电文长度
        private string _messageid;                 //电文号
        private string _validatecode;              //验证码
        private string[] _content;                 //电文内容
        private int _loopindex;                    //循环开始位置 没有循环：-1
        private int _loopnum;                      //循环次数
        private string _mtablename;                //主表名
        private string _dtablename;                //子表名
        private FieldDesc[] _fields;               //字段定义：字段名、字段类型
        private int _oper;                         //操作类型索引
        private string _operation;                 //操作类型 A新增  M修改 D删除 I初始化
        private bool _isFeedback;
        private string _name;


        public int LoopNum
        {
            get
            {
                return _loopnum;
            }
        }

        public bool IsFeedback
        {
            get
            {
                return _isFeedback;
            }
            set
            {
                _isFeedback = value;
            }
        }

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        public string MessageID
        {
            get
            {
                return _messageid;
            }
            set
            {
                _messageid = value;
                init();
                SetDataModel(_messageid);
            }
        }

        private void init()
        {
            _loopindex = -1;
            _loopnum = 0;
            _oper = -1;
            _operation = "";
            _isFeedback = false;
            _name = "";
            _mtablename = "";
            _dtablename = "";
            _validatecode = "";
        }

        public GramMessage()
        {
            _messageid = "";
            _length = 0;
            init();
        }

        public GramMessage(string MessageID)
        {
            _messageid = MessageID;
            init();

            SetDataModel(_messageid);

        }

        public string ItemValue(string ItemName)
        {
            string val = "";

            for (int i = 0; i < _fields.Length; i++)
            {
                if (_fields[i].FieldName == ItemName)
                {
                    val = _content[i];
                    break;
                }
            }
            return val;
        }

        public string[] ItemValues(string ItemName)
        {
            string[] result = new string[_loopnum];

            int looplength = _fields.Length - _loopindex - 1;

            int index = -1;

            for (int j = _loopindex + 1; j < _fields.Length; j++)
            {
                if (_fields[j].FieldName == ItemName)
                {
                    index = j;
                    break;
                }
            }

            if (index >= 0)
            {
                for (int i = 0; i < _loopnum; i++)
                {
                    result[i] = _content[index + i * looplength];
                }
            }

            return result;
        }

        private void SetDataModel(string MessageID)
        {
            string sql = "select * from gram where mid='"+ MessageID +"'";
            DataTable dt = SqlCe.ExecuteQuery(sql);
            if (dt.Rows.Count > 0)
            {
                _mtablename = dt.Rows[0]["mainTbl"].ToString();
                _dtablename = dt.Rows[0]["detailTbl"].ToString();
                _isFeedback = Convert.ToBoolean(dt.Rows[0]["isFeedback"]);
                _name = dt.Rows[0]["name"].ToString();
            }
            sql = "select * from gramfld where mid='" + MessageID + "' order by seq";
            dt = SqlCe.ExecuteQuery(sql);
            
            string fldname = "";

            if (dt.Rows.Count > 0)
            {
                _fields = new FieldDesc[dt.Rows.Count];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    _fields[i] = new FieldDesc();
                    fldname = dt.Rows[i]["name"].ToString();
                    if (fldname == "oper")
                    {
                        _oper = i;
                    }
                    else if(fldname=="loop")
                    {
                        _loopindex = i;
                    }
                    _fields[i].FieldName = fldname;
                    _fields[i].FieldType = dt.Rows[i]["type"].ToString();
                    _fields[i].IsPK = Convert.ToInt32(dt.Rows[i]["isPK"]);
                    _fields[i].IsLoop = Convert.ToBoolean(dt.Rows[i]["isLoop"]);
                    _fields[i].Value = "";
                }
            }
            dt.Dispose();
        }

        public bool UnPackage(string DataGram)
        {

            if (DataGram.Length < 18)
            {
                return false;
            }

            if (!IsNumberic(SubString(DataGram, 2, 4)))
            {
                return false;
            }

            //4位长度
            _length = int.Parse(DataGram.Substring(2, 4)) -1 ;

            if (Length(DataGram) != _length)
            {
                return false;
            }

            if (SubString(DataGram, 6, 4) != "UAZD" && SubString(DataGram, 6, 4) != "ZDUA")
            {
                return false;
            }

            //6位电文号
            this.MessageID = SubString(DataGram, 6, 6);


            //电文内容，以；作为分割符
            _content = SubString(DataGram, 12, _length - 18).Split(';');

            if (_loopindex >= 0)
            {
                if (IsNumberic(_content[_loopindex]))
                {
                    _loopnum = int.Parse(_content[_loopindex]);
                }
                else
                {
                    _loopnum = 0;
                }
            }
            else
            {
                _loopnum = 0;
            }

            if (_oper >= 0) _operation = _content[_oper];

            //最后4位验证码
            _validatecode = SubString(DataGram, _length - 6, 4);

            return true;
        }

        public string Package(string MessageID, string data)
        {
            int len = Length(data) + 18;
            return zeroString(len.ToString(), 4) + MessageID + data + ValidateCode(data);

        }

        private string namevaluesql(string type,string name,string content)
        {
            string sql = "";

            switch (type)
            {
                case "C":
                    sql = name + "=" + "'" + content + "'";
                    break;
                case "N":
                    sql = name + "=" + ((content == "") ? "null" : content);
                    break;
                case "D":
                    sql = name + "=" + ((content == "") ? "null" : dateString(content));
                    break;
                default:
                    break;
            }

            return sql;
        }

        private string valuesql(string type,string content)
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

        private bool SaveMainTbl()
        {
            string sql = "";
            string nvsql = "";

            int fldCnt = (_loopindex >= 0) ? _loopindex : _fields.Length;

            string whsql = " where 1=1 ";
            for (int i = 0; i < fldCnt; i++)
            {
                if (_fields[i].IsPK == 1)
                {
                    nvsql = namevaluesql(_fields[i].FieldType, _fields[i].FieldName, _content[i]);
                    if (nvsql != "")
                    {
                        whsql += "and " + nvsql + " ";
                    }
                }
            }

            if (_operation == "D")
            {
                sql = "delete from " + _mtablename + whsql;
                try
                {
                    SqlCe.ExecuteNonQuery(sql);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                    System.Diagnostics.Debug.WriteLine(sql);
                    SqlCe.ExecuteNonQuery("insert into error values('" + sql.Replace("'", "''") + "','" + ex.Message.Replace("'", "''") + "',getDate())");
                }
                return true;

            }

            DataTable dt = SqlCe.ExecuteQuery("select * from " + _mtablename + whsql);
            bool bExist = false;
            if (dt.Rows.Count > 0)
            {
                bExist = true;
            }
            dt.Dispose();

            if (bExist == false)
            {
                sql = "insert into " + _mtablename + "(";

                for (int i = 0; i < fldCnt; i++)
                {
                    if (_fields[i].FieldType != "")
                    {
                        sql += _fields[i].FieldName + ",";
                    }
                }

                sql = sql.Substring(0, sql.Length - 1);
                sql += ") values (";


                for (int j = 0; j < fldCnt; j++)
                {
                    sql += valuesql(_fields[j].FieldType, _content[j]);
                }

                sql = sql.Substring(0, sql.Length - 1);
                sql += ")";

                try
                {
                    SqlCe.ExecuteNonQuery(sql);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                    System.Diagnostics.Debug.WriteLine(sql);
                    SqlCe.ExecuteNonQuery("insert into error values('" + sql.Replace("'", "''") + "','" + ex.Message.Replace("'", "''") + "',getDate())");
                }
            }
            else
            {
                sql = "update " + _mtablename + " set ";
                for (int j = 0; j < fldCnt; j++)
                {
                    if (_fields[j].IsPK == 0 || _fields[j].IsPK == 2)
                    {
                        nvsql = namevaluesql(_fields[j].FieldType, _fields[j].FieldName, _content[j]);
                        if (nvsql != "")
                        {
                            sql += nvsql + ",";
                        }
                    }
                }
                sql = sql.Substring(0, sql.Length - 1);

                sql = sql + whsql;

                try
                {
                    SqlCe.ExecuteNonQuery(sql);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                    System.Diagnostics.Debug.WriteLine(sql);
                    SqlCe.ExecuteNonQuery("insert into error values('" + sql.Replace("'", "''") + "','" + ex.Message.Replace("'", "''") + "',getDate())");
                }

            }

            return true;

        }

        private bool SaveDetailTbl()
        {
            string sql = "";
            string nvsql = "";

            int looplength = _fields.Length - _loopindex - 1;
            string[] whsql = new string[_loopnum];

            sql = " where 1=1 ";
            for (int i = 0; i < _loopindex; i++)
            {
                if (_fields[i].IsPK == 1)
                {
                    nvsql = namevaluesql(_fields[i].FieldType, _fields[i].FieldName, _content[i]);
                    if (nvsql != "")
                    {
                        sql += "and " + nvsql + " ";
                    }
                }
            }

            for (int i = 0; i < _loopnum; i++)
            {
                whsql[i] = sql;

                for (int j = _loopindex + 1; j < _fields.Length; j++)
                {
                    if (_fields[j].IsPK == 1)
                    {
                        nvsql = namevaluesql(_fields[j].FieldType, _fields[j].FieldName, _content[j + i * looplength]);
                        if (nvsql != "")
                        {
                            whsql[i] += "and " + nvsql + " ";
                        }
                    }
                }

            }

            if (_operation == "D")
            {
                for (int i = 0; i < _loopnum; i++)
                {
                    sql = "delete from " + _dtablename + whsql[i];
                    try
                    {
                        SqlCe.ExecuteNonQuery(sql);
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine(ex.Message);
                        System.Diagnostics.Debug.WriteLine(sql);
                        SqlCe.ExecuteNonQuery("insert into error values('" + sql.Replace("'", "''") + "','" + ex.Message.Replace("'", "''") + "',getDate())");
                    }
                }
                return true;
            }

            for (int i = 0; i < _loopnum; i++)
            {
                bool bExist = false;
                DataTable dt = SqlCe.ExecuteQuery("select * from " + _dtablename + whsql[i]);
                if (dt.Rows.Count > 0)
                {
                    bExist = true;
                }
                dt.Dispose();
                if (bExist == false)
                {
                    sql = "insert into " + _dtablename + "(";
                    for (int j = 0; j < _loopindex; j++)
                    {
                        if ((_fields[j].IsPK==1 || _fields[j].IsPK==2) &&  _fields[j].FieldType != "")
                        {
                            sql += _fields[j].FieldName + ",";
                        }
                    }

                    for (int j = _loopindex + 1; j < _fields.Length; j++)
                    {
                        if (_fields[j].FieldType != "")
                        {
                            sql += _fields[j].FieldName + ",";
                        }
                    }

                    sql = sql.Substring(0, sql.Length - 1);
                    sql += ") values (";

                    for (int j = 0; j < _loopindex; j++)
                    {
                        if ((_fields[j].IsPK == 1 || _fields[j].IsPK == 2) && _fields[j].FieldType!="")
                        {
                            sql += valuesql(_fields[j].FieldType, _content[j]);
                        }
                    }

                    for (int j = _loopindex + 1; j < _fields.Length; j++)
                    {
                        if (_fields[j].FieldType != "")
                        {
                            sql += valuesql(_fields[j].FieldType, _content[j + i * looplength]);
                        }
                    }
                    sql = sql.Substring(0, sql.Length - 1);
                    sql += ")";


                    try
                    {
                        SqlCe.ExecuteNonQuery(sql);
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine(ex.Message);
                        System.Diagnostics.Debug.WriteLine(sql);
                        SqlCe.ExecuteNonQuery("insert into error values('" + sql.Replace("'", "''") + "','" + ex.Message.Replace("'", "''") + "',getDate())");
                    }
                }
                else
                {
                    sql = "update " + _dtablename + " set ";
                    for (int j = 0; j < _loopindex; j++)
                    {
                        if (_fields[j].IsPK == 2 && _fields[j].FieldType != "")
                        {
                            nvsql = namevaluesql(_fields[j].FieldType, _fields[j].FieldName, _content[j]);
                            if (nvsql != "")
                            {
                                sql += nvsql + ",";
                            }
                        }
                    }
                    for (int j = _loopindex + 1; j < _fields.Length; j++)
                    {
                        if (_fields[j].IsPK == 0 && _fields[j].FieldType != "")
                        {
                            nvsql = namevaluesql(_fields[j].FieldType, _fields[j].FieldName, _content[j + i * looplength]);
                            if (nvsql != "")
                            {
                                sql += nvsql + ",";
                            }
                        }
                    }
                    sql = sql.Substring(0, sql.Length - 1);
                    sql += whsql[i];

                    try
                    {
                        SqlCe.ExecuteNonQuery(sql);
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine(ex.Message);
                        System.Diagnostics.Debug.WriteLine(sql);
                        SqlCe.ExecuteNonQuery("insert into error values('" + sql.Replace("'", "''") + "','" + ex.Message.Replace("'", "''") + "',getDate())");
                    }
                }
            }

            return true;
        }

        //截字符串
        public string SubString(string toSub, int startIndex, int length)
        {
            string str = "";
            try
            {
                byte[] subbyte = System.Text.Encoding.Default.GetBytes(toSub);

                string sub = System.Text.Encoding.Default.GetString(subbyte, startIndex, length);

                str = sub;
            }
            catch
            {
            }
            return str;
        }

        //算字符串的长度
        public int Length(string str)
        {
            byte[] subbyte = System.Text.Encoding.Default.GetBytes(str);

            return subbyte.Length;
        }

        public bool Save()
        {
            if (_messageid == "") return false;

            if (_mtablename != "") SaveMainTbl();

            if (_loopindex >= 0 && _dtablename != "") SaveDetailTbl();

            return true;
        }

        private static string ValidateCode(string sInputString)
        {
            byte[] b = Encoding.Default.GetBytes(sInputString);

            CRC_Modbus crc= new CRC_Modbus();

            string crcString = crc.crc16(b).ToString("X");
            if (crcString.Length < 4)
            {
                crcString += new string('F',4-crcString.Length);
            }
            return crcString;
        }

        private static string zeroString(string var, int len)
        {
            string tmp = var.ToString();

            if (tmp.Length < len)
            {
                string zeroStr = new string('0', len - tmp.Length);

                tmp = zeroStr + tmp;
            }
            if (tmp.Length > len)
            {
                tmp = tmp.Substring(0, len);
            }

            return tmp;
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

        private bool IsNumberic(String strNumber)
        {
            Regex objPattern = new Regex("[^0-9]");

            return !objPattern.IsMatch(strNumber);
        }

    }

    class FieldDesc
    {
        private string _fieldtype;
        private string _fieldname;
        private int _isPK;
        private bool _isLoop;
        private string _value;

        public string FieldType
        {
            get
            {
                return _fieldtype;
            }
            set
            {
                _fieldtype = value;
            }
        }
        public string FieldName
        {
            get
            {
                return _fieldname;
            }
            set
            {
                _fieldname = value;
            }

        }

        public int IsPK
        {
            get
            {
                return _isPK;
            }
            set
            {
                _isPK = value;
            }
        }

        public bool IsLoop
        {
            get
            {
                return _isLoop;
            }
            set
            {
                _isLoop = value;
            }
        }

        public string Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
            }
        }
    }
}
