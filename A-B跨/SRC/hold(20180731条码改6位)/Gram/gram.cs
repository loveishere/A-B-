using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlServerCe;
using System.Collections;
using System.Data;
using System.Text.RegularExpressions;
using System.Security.Cryptography;  

namespace hStore.Gram
{
    class Message
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


        public int LoopNum
        {
            get
            {
                return _loopnum;
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
                SetDataModel(_messageid);
            }
        }

        public Message()
        {
            _messageid = "";
        }

        public Message(string MessageID)
        {
            _messageid = MessageID;

            SetDataModel(_messageid);

        }

        public string ItemValue(string ItemName)
        {
            string val = "";

            for (int i = 0; i < _fields.Length; i++)
            {
                if (_fields[i].FieldName == ItemName)
                {
                    val= _content[i];
                    break;
                }
            }
            return val;
        }

        private void SetDataModel(string MessageID)
        {

            switch (MessageID)
            {
                //货品代码
                case "WXZD01":
                    DataModel("BasicPdInf","", "ProductID,C;ProductName,C");
                    break;

                //装卸点代码
                case "WXZD02":
                    DataModel("BasicZXDInf","", "DDDM,C;DDMC,C");
                    break;

                //用户信息
                case "WXZD04":
                    DataModel("UserInf", "","oper;UserID,N;PWD,C");
                    break;

                //生产信息公告
                case "WXZD05":
                    DataModel("MsgInf","", "MsgID,N;MsgTime,C;Msg,C");
                    break;

                //区域基础信息
                case "WXZD06":
                    DataModel("BasicErInf","","oper;ID,N;Name,C");
                    break;

                //单车运输计划
                case "WXZD10":
                    DataModel("PlanInf", "PlanOrderInf", "oper;SeqID,Npk;TYH,C;SXS,N;YL,N;JS,N;KJLX,C;CC,N;YQWGTime,C;GTCH,C;loop;JHBH,C;CM,C;ZF,C;BW,C;ZL,N;JS,N;YQWGTime,C");
                    break;

                //框架作业指令
                case "WXZD11":
                    DataModel("FrameCarryOrder","","oper;SeqID,N;FrameID,C;Status,N;JHBH,C;ZD,C;XD,C");
                    break;

                //框架分布状态信息
                case "WXZD12":
                    DataModel("","","qy;kjs;loop;kjh;kzzt;ydch");
                    break;
                
                case "WXZD13":
                    DataModel("", "", "kjh,kb1,kb2,cm,js");
                    break;

                //车辆故障代码
                case "WXZD14":
                    DataModel("CarExceptionInfo", "", "oper;ID,C;Part,C;Des,C");
                    break;

                //托运基准信息
                case "WXZD15":
                    DataModel("BasicTYInf", "", "oper;TYH,C;HPDM,C;ZDDM,C;XDDM,C;GLS,N;GBBZ,N");
                    break;

                //区域信息变更
                case "WXZD16":
                    DataModel("", "", "qy");
                    break;

                //共同作业实绩更新
                case "WXZD17":
                    DataModel("PlanInf", "", "SeqID,N;TYH,C;SXS,N;DYL,N;DJS,N;WGTime,C");
                    break;

                //货名代码请求
                case "ZDWX01":
                    DataModel("", "", "ProductID");
                    break;

                //装卸点代码请求
                case "ZDWX02":
                    DataModel("", "", "DDDM");
                    break;

                //车辆GPS信息
                case "ZDWX04":
                    DataModel("", "", "jd;wd;sd");
                    break;

                //单车计划确认
                case "ZDWX10":
                    DataModel("", "", "SeqID;tyh;qrbz;qrtime");
                    break;

                //框架材料申请反馈
                case "WXZD03":
                    DataModel("FrameDetail", "", "KJH,C;loop;CLH,C;WZ,C;JHDD,C;TDH,C;GDH,C;ZFH,C;JZ,N;MZ,N;Long,C;Width,C;Height,C;KW,C;CH,C;FDH,C");
                    break;

                //框架作业信息
                case "ZDWX11":
                    DataModel("PlanDetail", "", "KJH,C;OperaTime,C;Opera,N;QY,C;XDDM,C;SeqID,N");
                    break;

                //外购料卸船
                case "WXZD30":
                    DataModel("ShipInfo","UnloadShipDetail","CPH,Cpk;CMDM,C;CMZW,Cpk;JHKBTIME,D;JHKGTIME,D;JHWGTIME,D;JHLBTIME,D;ZJS,N;ZZL,N;loop;CLH,C;PM,C;MZ,C;Long,C;Width,C;Height,C;RKB,C");
                    break;

                //产成品装船船批下提单信息
                case "WXZD31":
                    DataModel("ShipInfo", "LoadShipInfo", "CPH,Cpk;CMDM,Cpk;CMZW,Cpk;JHKBTIME,D;JHKGTIME,D;JHWGTIME,D;JHLBTIME,D;loop;TDH,C;GDH,C;JHDD,C;SHDW,C;ZJS,N;ZMZ,N;ZJZ,N;YB,N");
                    break;

                //产成品装船提单下准发信息
                case "WXZD32":
                    DataModel("", "LoadShipOrder", "TDH,Cpk;loop;PM,C;HTH,C;ZFH,C;LH,C;ZJS,N;ZMZ,N;ZJZ,N");
                    break;

                //产成品装船准发下材料信息
                case "WXZD33":
                    DataModel("", "LoadShipAcceptOrder", "ZFH,Cpk;LH,Cpk;loop;CLH,C;FDH,C;Long,N;Width,N;Height,N;JZ,N;MZ,N");
                    break;

                //船批销账件数查询反馈
                case "WXZD36":
                    DataModel("", "", "CPH;CMZW;WCJS;WCYL");
                    break;

                //加固材料变更
                case "WXZD37":
                    DataModel("JGCLInf", "", "oper;CLDM,C;CLMC,C;CLGG,C");
                    break;

                //当班入库计划信息
                case "WXZD51":
                    DataModel("", "ImpPlan", "loop;RKB,C;JHH,C;YHSJ,D;JS,N;MZ,N;JZ,N");
                    break;

                //当班入库计划下准发信息
                case "WXZD52":
                    DataModel("", "ImpOrder", "JHH,Cpk;loop;PMDM,C;PM,C;HTH,C;ZFH,C;JS,N;MZ,N;JZ,N");
                    break;

                //当班入库计准发下材料信息
                case "WXZD53":
                    DataModel("", "ImpAccOrder", "ZFH,Cpk;loop;CLH,C;LH,C;Long,C;Width,C;Height,C;JZ,N;MZ,N;KW,C;FDH,C");
                    break;

                //当班出库计划信息
                case "WXZD54":
                    DataModel("", "ExpPlan", "loop;CKB,C;CPH,C;CM,C;JHH,C;GDH,C;JHDD,C;SHDW,C;JS,N;MZ,N;JZ,N;YB,N;YSFS,C");
                    break;

                //当班出库计划下准发信息
                case "WXZD55":
                    DataModel("", "ExpOrder", "JHH,Cpk;loop;PMDM,C;PM,C;HTH,C;ZFH,C;YHSJ,D;JS,N;MZ,N;JZ,N");
                    break;

                //当班出库准发下材料信息
                case "WXZD56":
                    DataModel("", "ExpAccOrder", "ZFH,Cpk;loop;CLH,C;LH,C;ZPH,C;Long,C;Width,C;Height,C;JZ,N;MZ,N;KW,C;FDH,C");
                    break;

                //当班铁路出厂组批材料信息
                case "WXZD57":
                    DataModel("", "Troop", "ZPH,Cpk;JS;YHSJ,Dpk;loop;CLH,C;ZFH,C");
                    break;
            }
        }

        public bool UnPackage(string DataGram)
        {

            if (DataGram.Length <= 14) return false;

            if (!IsNumberic(DataGram.Substring(0, 4))) return false;

            //4位长度
            _length = int.Parse(DataGram.Substring(0, 4));

            if (DataGram.Length != _length + 14) return false;

            if (DataGram.Substring(4, 4) != "WXZD") return false;

            //6位电文号
            this.MessageID = DataGram.Substring(4, 6).Trim();


            //电文内容，以；作为分割符
            _content = DataGram.Substring(10, _length).Split(';');

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

            if (_oper >= 0)
            {
                _operation=_content[_oper];
            }

            //最后4位验证码
            _validatecode = DataGram.Substring(10 + _length, 4);

            return true;
        }

        public static string Package(string MessageID,string data)
        {
            return zeroString(data.Length+6,4)+MessageID + data + ValidateCode(data);

        }

        private bool SaveMainTbl()
        {
            SqlCeCommand cmd = new SqlCeCommand();
            cmd.Connection = App.con;

            string sql = "insert into " + _mtablename + "(";

            int fldCnt=(_loopindex>=0)? _loopindex : _fields.Length;

            for (int i = 0; i <fldCnt ; i++)
            {
                if (_fields[i].FieldName != "oper")
                {
                    sql += _fields[i].FieldName + ",";
                }
            }

            sql = sql.Substring(0, sql.Length - 1);
            sql += ") values (";


            for (int j = 0; j < fldCnt; j++)
            {
                switch (_fields[j].FieldType)
                {
                    case "C":
                    case "Cpk":
                        sql += "'" + _content[j] + "',";
                        break;
                    case "N":
                    case "Npk":
                        sql += (_content[j] == "") ? "null," : _content[j] + ",";
                        break;
                    case "D":
                    case "Dpk":
                        sql += (_content[j] == "") ? "null," : dateString(_content[j]) + ",";
                        break;
                    default:
                        break;
                }
            }

            if (_fields.Length > 0) sql = sql.Substring(0, sql.Length - 1);
            sql += ")";

            cmd.CommandText = sql;
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                App.WriteLog("sql="+sql);
                App.WriteLog("Error:"+ex.Message);
            }

            cmd.Dispose();
            return true;
            
        }

        private bool SaveDetailTbl()
        {
            SqlCeCommand cmd = new SqlCeCommand();
            cmd.Connection = App.con;

            string sql = "insert into " + _dtablename + "(";
            for (int i = 0; i < _loopindex; i++)
            {
                if(_fields[i].FieldType=="Npk" || _fields[i].FieldType=="Cpk")
                {
                    sql += _fields[i].FieldName + ",";
                }
            }

            for (int i = _loopindex + 1; i < _fields.Length; i++)
            {
                sql += _fields[i].FieldName + ",";
            }

            sql = sql.Substring(0, sql.Length - 1);
            sql += ") ";

            int looplength = _fields.Length - _loopindex - 1;

            for (int i = 0; i < _loopnum; i++)
            {
                string sqlValue = "values (";

                for (int j = 0; j < _loopindex; j++)
                {
                    switch (_fields[j].FieldType)
                    {
                        case "Npk":
                            sqlValue += (_content[j] == "") ? "null," : _content[j] + ",";
                            break;
                        case "Cpk":
                            sqlValue += "'" + _content[j] + "',";
                            break;
                        case "Dpk":
                            sqlValue += (_content[j] == "") ? "null," : dateString(_content[j]) + ",";
                            break;
                        default:
                            break;
                    }
                }

                for (int j = _loopindex + 1; j < _fields.Length; j++)
                {
                    switch (_fields[j].FieldType)
                    {
                        case "C":
                            sqlValue += "'" + _content[j + i * looplength] + "',";
                            break;
                        case "N":
                            sqlValue += (_content[j + i * looplength] == "") ? "null," : _content[j + i * looplength] + ",";
                            break;
                        case "D":
                            sqlValue += (_content[j + i * looplength] == "") ? "null," : dateString(_content[j + i * looplength]) + ",";
                            break;
                        default:
                            break;
                    }

                }
                if (_fields.Length > _loopindex+1) sqlValue = sqlValue.Substring(0, sqlValue.Length - 1);
                sqlValue += ")";

                cmd.CommandText = sql+sqlValue;
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch(Exception ex)
                {
                    App.WriteLog("sql="+cmd.CommandText);
                    App.WriteLog("Error:" + ex.Message);
                }
            }

            cmd.Dispose();
            return true;
        }

        public bool Save()
        {
            if (_messageid == "") return false;

            if (_mtablename != "") SaveMainTbl();

            if (_loopindex >= 0 && _dtablename != "") SaveDetailTbl();

            return true;
        }

        private void DataModel(string mTableName,string dTableName, string Fields)
        {
            _mtablename = mTableName;
            _dtablename = dTableName;

            _loopindex = -1;
            _oper = -1;

            string[] arr = Fields.Split(';');
            _fields = new FieldDesc[arr.Length];

            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i].Equals("oper")) _oper = i;
                if (arr[i].Equals("loop")) _loopindex = i;
                _fields[i] = new FieldDesc(arr[i]);
            }

        }

        private static string ValidateCode(string sInputString)
        {
            return "9999";
        }

        private static string zeroString(int var, int len)
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
                return "'"+sInputString.Substring(0, 4)+"-"+sInputString.Substring(4,2)+"-" + sInputString.Substring(6,2) + " " + sInputString.Substring(8,2)+ ":" + sInputString.Substring(10,2)+":" + sInputString.Substring(12,2) + "'" ;
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
        public FieldDesc(string field)
        {
            if (field.IndexOf(',') >= 0)
            {
                _fieldname = field.Split(',')[0];
                _fieldtype = field.Split(',')[1];
            }
            else
            {
                _fieldname = field;
                _fieldtype = "";
            }
        } 
    }



}
