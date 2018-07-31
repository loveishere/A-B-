using System;

using System.Collections.Generic;
using System.Text;
using System.Data;

namespace hStore
{
    public struct scancl
    {
        public string cl;
        public string kw;
        public string scantime;
        public int mz;
        public string qa;

        public scancl(string cl, string scantime, int mz,string kw)
        {
            this.cl = cl;
            this.scantime = scantime;
            this.mz = mz;
            this.qa = "";
            this.kw = kw;
        }
    }

    class Storage
    {
        public string make;//制造单元
        public string jhh;//计划号
        public string sjfs;//委托单实绩方式
        public string jhdd;//到港/到站
        public string shdw;//收货单位
        public string gdh;//关单号
        public int cltype;
        public string zfh;//准发号
        public string pm;//品名
        public string lh;//炉号
        public string hth;//合同号
        public string zph;//轧批号
        public string kjh;//框架号/车号
        public int dqSelectedIndex;//车号前缀
        public string jxh;//港机号
        public string dlh1;//吊梁号1
        public string dlh2;//吊梁号2
        public string djh;//吊具号
        public bool pzbj;//拼装标记
        public bool inFlag;//入库标记
        public string cph;//船批号
        public string cm;
        public string expflag;

        public Storage()
        {
            make = "";
            jhh = "";
            sjfs = "";
            jhdd = "";
            shdw = "";
            gdh = "";
            cltype = -1;
            zfh = "";
            pm = "";
            lh = "";
            hth = "";
            zph = "";
            kjh = "";
            dqSelectedIndex = -1;
            jxh = "";
            dlh1 = "";
            dlh2 = "";
            djh = "";
            pzbj = false;
            inFlag = true;
            cph = "";
            cm = "";
            expflag = "";
        }

        public static DataTable GetImpPlan(string kb,string jhh)
        {
            string sql = "select JHH,Make,";
            sql += "JS,WCJS,JS-(case isnull(WCJS) when 1 then 0 else WCJS end) SYJS, ";
            sql += "JZ,WCJZ,JZ-(case isnull(WCJZ) when 1 then 0 else WCJZ end) SYJZ, ";
            sql += "MZ,WCMZ,MZ-(case isnull(WCJZ) when 1 then 0 else WCMZ end) SYMZ ";
            sql += "from ImportStoragePlan  where RKB='" + kb + "' ";
            if (jhh != "")
            {
                sql += "and JHH='" + jhh + "'";
            }

            return SqlCe.ExecuteQuery(sql);
        }

        public static DataTable GetExpShip(string kb)
        {
            string sql = "select CPH,CM,sum(JS)-sum(WCJS) SYJS,sum(WCJS) WCJS from ExportStoragePlan where CKB='" + kb + "' and JHH like '3%' group by CPH,CM";

            return SqlCe.ExecuteQuery(sql);
        }


        public static DataTable GetExpPlan(string kb, string jhh)
        {
            string sql = "select Make,JHH,Make,JHDD,SHDW,";
            sql += "JS,WCJS,JS-(case isnull(WCJS) when 1 then 0 else WCJS end) SYJS, ";
            sql += "JZ,WCJZ,JZ-(case isnull(WCJZ) when 1 then 0 else WCJZ end) SYJZ, ";
            sql += "MZ,WCMZ,MZ-(case isnull(WCJZ) when 1 then 0 else WCMZ end) SYMZ ";
            sql += "from ExportStoragePlan where CKB='" + kb + "' ";
            if (jhh != "")
            {
                sql += "and JHH='" + jhh + "'";
            }

            return SqlCe.ExecuteQuery(sql);
        }

        public static DataTable GetExpPlanF(string kb, string jhh, string flag)
        {
            string sql = "select JHH,Make,JHDD,SHDW,";
            sql += "JS,WCJS,JS-(case isnull(WCJS) when 1 then 0 else WCJS end) SYJS, ";
            sql += "JZ,WCJZ,JZ-(case isnull(WCJZ) when 1 then 0 else WCJZ end) SYJZ, ";
            sql += "MZ,WCMZ,MZ-(case isnull(WCJZ) when 1 then 0 else WCMZ end) SYMZ ";
            sql += "from ExportStoragePlan where CKB='" + kb + "' ";
            if (flag == "zt")
            {
                sql += "and JHH like '1%' ";
            }
            else if (flag == "tl")
            {
                sql += "and JHH like '2%' ";
            }
            else if (flag == "zk")
            {
                sql += "and (JHH like '7%' or JHH like '8%' or JHH like '9%') ";
            }
            if (jhh != "")
            {
                sql += "and JHH='" + jhh + "'";
            }

            return SqlCe.ExecuteQuery(sql);
        }

        public static DataTable GetExpPlanZC(string kb,string cph,string cm,string gdh)
        {
            string sql = "select JHH,Make,JHDD,SHDW,GDH,";
            sql += "JS,WCJS,JS-(case isnull(WCJS) when 1 then 0 else WCJS end) SYJS, ";
            sql += "JZ,WCJZ,JZ-(case isnull(WCJZ) when 1 then 0 else WCJZ end) SYJZ, ";
            sql += "MZ,WCMZ,MZ-(case isnull(WCJZ) when 1 then 0 else WCMZ end) SYMZ ";
            sql += "from ExportStoragePlan where CKB='" + kb + "' and JHH like '3%' ";
            sql += " and CM='" + cm + "' and CPH='" + cph + "' ";
            if (gdh != "") sql += "and GDH='" + gdh + "'";

            return SqlCe.ExecuteQuery(sql);
        }

        public static void DeleteImpPlan(string jhh,string make)
        {
            string sql = "delete from ImportStorageAcceptOrder where JHH='" + jhh + "' and Make='"+ make +"'";
            SqlCe.ExecuteNonQuery(sql);

            //sql = "delete from ImportStorageOrder where JHH='" + jhh + "' and Make='"+ make +"'";
            //SqlCe.ExecuteNonQuery(sql);

            sql = "delete from ImportStoragePlan where JHH='" + jhh + "' and Make='"+ make +"'";
            SqlCe.ExecuteNonQuery(sql);
        }

        public static void DeleteExpPlan(string jhh,string make)
        {
            string sql = "delete from ExportStorageAcceptOrder where JHH='" + jhh + "' and Make='"+ make +"'";
            SqlCe.ExecuteNonQuery(sql);

            //sql = "delete from ExportStorageOrder where JHH='" + jhh + "' and Make='" + make + "'";
            //SqlCe.ExecuteNonQuery(sql);

            sql = "delete from ExportStoragePlan where JHH='" + jhh + "' and Make='" + make + "'";
            SqlCe.ExecuteNonQuery(sql);
        }

        public static DataTable GetImpZF(string jhh,string make)
        {
            //string sql = "select A.ZFH,B.KW,sum(case B.WCFlag when 2 then 0 else 1 end) SYJS, sum(case B.WCFlag when 2 then 1 else 0 end) WCJS, count(*) JS, ";
            //sql += "sum(case B.WCFlag when 2 then 0 else B.MZ end) SYMZ, sum(case B.WCFlag when 2 then B.MZ else 0 end) WCMZ, sum(B.MZ) MZ,";
            //sql += "sum(case B.WCFlag when 2 then 0 else B.JZ end) SYJZ, sum(case B.WCFlag when 2 then B.JZ else 0 end) WCJZ, sum(B.JZ) JZ,";
            //sql += "max(A.HTH) HTH,max(A.PM) PM ";
            //sql += "from ImportStorageOrder A,ImportStorageAcceptOrder B ";
            //sql += "where A.ZFH=B.ZFH and A.JHH=B.JHH and A.Make=B.Make ";
            //sql += "and A.JHH='" + jhh + "' and A.make='"+ make +"'";
            //sql += "group by A.ZFH,B.KW ";


            string sql="select ZFH,sum(case WCFlag when 2 then 0 else 1 end) SYJS, sum(case WCFlag when 2 then 1 else 0 end) WCJS, count(*) JS,";
            sql+="sum(case WCFlag when 2 then 0 else MZ end) SYMZ, sum(case WCFlag when 2 then MZ else 0 end) WCMZ, sum(MZ) MZ,";
            sql+="sum(case WCFlag when 2 then 0 else JZ end) SYJZ, sum(case WCFlag when 2 then JZ else 0 end) WCJZ, sum(JZ) JZ,";
            sql+="max(HTH) HTH,max(PM) PM ";
            sql+="from ImportStorageAcceptOrder ";
            sql += "where JHH='" + jhh + "' and Make='" + make + "'";
            sql += "group by ZFH ";

            return SqlCe.ExecuteQuery(sql);
        }

        public static DataTable GetExpZF(string jhh,string make)
        {
            //string sql = "select A.ZFH,substring(B.KW,4,8) KW,sum(case B.WCFlag when 2 then 0 else 1 end) SYJS, sum(case B.WCFlag when 2 then 1 else 0 end) WCJS, count(*) JS, ";
            //sql += "sum(case B.WCFlag when 2 then 0 else B.MZ end) SYMZ, sum(case B.WCFlag when 2 then B.MZ else 0 end) WCMZ, sum(B.MZ) MZ,";
            //sql += "sum(case B.WCFlag when 2 then 0 else B.JZ end) SYJZ, sum(case B.WCFlag when 2 then B.JZ else 0 end) WCJZ, sum(B.JZ) JZ,";
            //sql += "max(A.PM) PM,max(B.Height+'×'+B.Width+'×'+B.Long) GG ";
            //sql += "from ExportStorageOrder A,ExportStorageAcceptOrder B ";
            //sql += "where A.ZFH=B.ZFH and A.JHH=B.JHH and A.MAKE=B.MAKE ";
            //sql += "and A.JHH='" + jhh + "' and A.MAKE='" + make + "' ";
            //sql += "group by A.ZFH,B.KW ";


            string sql="select ZFH,sum(case WCFlag when 2 then 0 else 1 end) SYJS, sum(case WCFlag when 2 then 1 else 0 end) WCJS, count(*) JS,";
            sql+="sum(case WCFlag when 2 then 0 else MZ end) SYMZ, sum(case WCFlag when 2 then MZ else 0 end) WCMZ, sum(MZ) MZ,";
            sql+="sum(case WCFlag when 2 then 0 else JZ end) SYJZ, sum(case WCFlag when 2 then JZ else 0 end) WCJZ, sum(JZ) JZ,";
            sql+="max(PM) PM,max(Height+'×'+Width+'×'+Long) GG ";
            sql+="from ExportStorageAcceptOrder ";
            sql += "where JHH='" + jhh + "' and MAKE='" + make + "' ";
            sql += "group by ZFH ";

            return SqlCe.ExecuteQuery(sql);
        }

        public static DataTable GetExpZF(string jhh,string make,int cltype)
        {
            string sql = "sum(case WCFlag when 2 then 0 else 1 end) SYJS, sum(case WCFlag when 2 then 1 else 0 end) WCJS, count(*) JS, ";
            sql += "sum(case WCFlag when 2 then 0 else MZ end) SYMZ, sum(case WCFlag when 2 then MZ else 0 end) WCMZ, sum(MZ) MZ,";
            sql += "sum(case WCFlag when 2 then 0 else JZ end) SYJZ, sum(case WCFlag when 2 then JZ else 0 end) WCJZ, sum(JZ) JZ,";
            sql += "max(PM) PM,max(Height+'×'+Width+'×'+Long) GG ";
            sql += "from ExportStorageAcceptOrder ";
            sql += "where JHH='" + jhh + "' and MAKE='"+ make +"' ";

            switch (cltype)
            {
                case 0:
                    sql = "select ZFH," + sql + "group by ZFH ";
                    break;
                case 1:
                    sql = "select LH," + sql + "group by LH ";
                    break;
                case 2:
                    sql = "select HTH," + sql + "group by HTH ";
                    break;
                case 3:
                    sql = "select substring(CLH,1,6) ZPH," + sql + "group by substring(CLH,1,6) ";
                    break;
                default:
                    break;
            }

            return SqlCe.ExecuteQuery(sql);
        }

        public static DataTable GetExpZF(string jhh, string make, int cltype, string value)
        {
            string sql = "select count(*) JS,sum(MZ) MZ from ExportStorageAcceptOrder ";
            sql += "where JHH='" + jhh + "' and MAKE='" + make + "' and KW<>'' and (WCFlag is Null or WCFlag<>2) ";
            switch (cltype)
            {
                case 0:
                    sql += "and ZFH='" + value + "' ";
                    break;
                case 1:
                    sql += "and LH='" + value + "' ";
                    break;
                case 2:
                    sql += "and HTH='" + value + "' ";
                    break;
                case 3:
                    sql += "and substring(CLH,1,6)='" + value + "' ";
                    break;
                default:
                    break;
            }

            return SqlCe.ExecuteQuery(sql);
        }

        public static DataTable GetImpCL(string kb,string jhh,string make,string zfh)
        {
            string sql = "select A.JHH,A.Make,A.ZFH,A.CLH,A.CLH2,A.MZ,A.JZ,A.Height+'×'+A.Width+'×'+A.Long GG,A.WCFlag,A.SCANTIME,A.QA, ";
            sql += "case when B.CLH is not null then '1' else '0' end ExFlag ";
            sql += "from ImportStorageAcceptOrder A left join ExportStorageAcceptOrder B on A.CLH=B.CLH and A.Make=B.Make and B.CKB='" + kb + "' ";
            sql += "where A.ZFH='" + zfh + "' and A.JHH='" + jhh + "' and A.Make='"+ make +"'";

            return SqlCe.ExecuteQuery(sql);
        }

        public static DataTable GetExpCL(string jhh,string make, string zfh)
        {
            string sql = "select ZFH,CLH,MZ,JZ,";
            sql += "Height+'×'+Width+'×'+Long GG,";
            sql += "WCFlag,SCANTIME from ";
            sql += "ExportStorageAcceptOrder where (WCFlag is null or WCFlag<>2) ";
            sql += "and ZFH='" + zfh + "' and JHH='" + jhh + "' and Make='"+ make +"' ";

            return SqlCe.ExecuteQuery(sql);
        }

        public static DataTable GetExpCL(string jhh,string make,int cltype,string value)
        {
            string sql = "select JHH,Make,ZFH,CLH,CLH2,MZ,JZ,";
            sql += "Height+'×'+Width+'×'+Long GG,QA, ";
            sql += "WCFlag,SCANTIME from ";
            sql += "ExportStorageAcceptOrder where (WCFlag is null or WCFlag<>2) ";
            sql += "and JHH='"+ jhh +"' and Make='"+ make +"' ";

            switch (cltype)
            {
                case 0:
                    sql += "and ZFH='" + value + "'";
                    break;
                case 1:
                    sql += "and LH='" + value + "'";
                    break;
                case 2:
                    sql += "and HTH='" + value + "'";
                    break;
                case 3:
                    sql += "and substring(CLH,1,6)='" + value + "'";
                    break;
                default:
                    break;
            }

            sql += " order by CLH";

            return  SqlCe.ExecuteQuery(sql);
        }

        public static DataTable GetExpCLkb(string kb, string cph, string jhh, string make, int cltype, string value)
        {
            string sql = "select A.JHH,A.Make,A.ZFH,A.CLH,A.CLH2,A.MZ,A.JZ,substring(A.KW,4,8) KW,";
            sql += "A.Height+'×'+A.Width+'×'+A.Long GG,A.QA,";
            sql += "A.WCFlag,A.SCANTIME from ";
            sql += "ExportStorageAcceptOrder A, ExportStoragePlan B where A.JHH=B.JHH and A.Make=B.Make and (A.WCFlag is null or A.WCFlag<>2) and A.KW<>'' ";
            sql += "and A.CKB='" + kb + "' ";
            if (cph != "")
            {
                sql += "and B.CPH='" + cph + "' ";
            }
            if (jhh != "")
            {
                sql += "and A.JHH='" + jhh + "' and A.Make='" + make + "' ";
            }

            switch (cltype)
            {
                case 0:
                    sql += "and A.ZFH='" + value + "'";
                    break;
                case 1:
                    sql += "and A.LH='" + value + "'";
                    break;
                case 2:
                    sql += "and A.HTH='" + value + "'";
                    break;
                case 3:
                    sql += "and substring(A.CLH,1,6)='" + value + "'";
                    break;
                default:
                    break;
            }

            sql += " order by KW,CLH ";

            return SqlCe.ExecuteQuery(sql);
        }

        public static DataTable GetExpCLkb(string kb, string cph, string jhh, string make, int cltype, string value, string txt)
        {
            string sql = "select A.CLH,A.CLH2 from ExportStorageAcceptOrder A,ExportStoragePlan B where A.JHH=B.JHH and A.Make=B.Make and (A.WCFlag is null or A.WCFlag<>2) and A.KW<>'' ";
            sql += " and A.CLH2 like '%" + txt + "' and A.CKB='" + kb + "' ";
            if (cph != "")
            {
                sql += "and B.CPH='"+ cph +"' ";
            }
            if (jhh != "")
            {
                sql += "and A.JHH='" + jhh + "' and A.Make='" + make + "' ";
            }
            switch (cltype)
            {
                case 0:
                    sql += "and A.ZFH='" + value + "'";
                    break;
                case 1:
                    sql += "and A.LH='" + value + "'";
                    break;
                case 2:
                    sql += "and A.HTH='" + value + "'";
                    break;
                case 3:
                    sql += "and substring(A.CLH,1,6)='" + value + "'";
                    break;
                default:
                    break;
            }

            sql += " order by KW,CLH ";

            return SqlCe.ExecuteQuery(sql);
        }

        public static DataTable GetImpFrameCL(string kb, string kjh)
        {
            string sql = "select A.CLH,";
            sql += "case when B.CLH is not null then B.RKB else A.WZ end  WZ,";
            sql += "case when B.CLH is not null then case when len(B.KW)>3 then substring(B.KW,4,len(B.KW)-3) else '' end else case when len(A.KW)>3 then substring(A.KW,4,len(A.KW)-3) else '' end end KW,";
            sql += "case when B.CLH is not null then B.Height+'×'+B.Width+'×'+B.Long else A.Height+'×'+A.Width+'×'+A.Long end GG,";
            sql += "case when B.CLH is not null then B.MZ else A.MZ end MZ,";
            sql += "case when B.CLH is not null then B.WCFlag else -1 end WCFlag,";
            sql += "case when B.CLH is not null then B.SCANTIME else '' end SCANTIME,";
            sql += "case when B.CLH is not null then B.ZFH else A.ZFH end ZFH,";
            sql += "case when B.CLH is not null then B.JHH else A.TDH end TDH,";
            sql += "case when B.CLH is not null then B.Make else A.Make end Make,";
            sql += "case when B.CLH is not null then B.QA else A.QA end QA,";
            sql += "A.CH,";
            sql += "case when C.CLH is not null then '1' else '0' end ExFlag ";
            sql += "from FrameDetail A left join ImportStorageAcceptOrder B on A.CLH=B.CLH and A.Make=B.Make and (B.WCFlag is null or B.WCFlag<>2) and B.RKB='" + kb + "' ";
            sql += "left join ExportStorageAcceptOrder C on A.CLH=C.CLH and A.Make=C.Make and (C.WCFlag is null or C.WCFlag<>2) and C.CKB='" + kb + "' ";
            sql += "where A.KJH='" + kjh + "' order by A.CH desc,A.CLH asc ";

            return SqlCe.ExecuteQuery(sql);
        }

        public static DataTable GetCLDetail(string clh,bool inflag)
        {
            string sql = "";
            if (inflag)
            {
                sql = "select PM,Height+'×'+Width+'×'+Long GG from ImportStorageAcceptOrder where CLH='" + clh + "'";
            }
            else
            {
                sql = "select PM,Height+'×'+Width+'×'+Long GG from ExportStorageAcceptOrder where CLH='" + clh + "'";
            }
            return SqlCe.ExecuteQuery(sql);
        }

        public static DataTable GetBasicQuality()
        {
            string sql = "select id,name from BasicQuality";
            return SqlCe.ExecuteQuery(sql);
        }

        public static void ClearCLQuality(string clh, bool inflag)
        {
            string sql;

            if (inflag)
            {

                sql = "update ImportStorageAcceptOrder set QA='' where CLH='" + clh + "'";
                SqlCe.ExecuteNonQuery(sql);

            }
            else
            {
                sql = "update ExportStorageAcceptOrder set QA='' where CLH='" + clh + "'";
                SqlCe.ExecuteNonQuery(sql);
            }
        }

        public static void SetCLQuality(string clh, string qa, bool inflag)
        {
            string sql;

            if (inflag)
            {

                sql = "update ImportStorageAcceptOrder set QA='" + qa + "' where CLH='" + clh + "'";
                SqlCe.ExecuteNonQuery(sql);

            }
            else
            {
                sql = "update ExportStorageAcceptOrder set QA='" + qa + "' where CLH='" + clh + "'";
                SqlCe.ExecuteNonQuery(sql);
            }

        }

        public static int GetImpScanJs(string jhh,string make)
        {
            int js = 0;
            string sql = "select count(*) js from ImportStorageAcceptOrder where WCFlag=1 and RKB='"+ Global.sKb +"' ";
            if (jhh != "")
            {
                sql += "and JHH='" + jhh + "' and Make='" + make + "'";
            }

            DataTable dt = SqlCe.ExecuteQuery(sql);
            if (dt.Rows.Count > 0)
            {
                js = Convert.ToInt32(dt.Rows[0]["js"]);
            }

            return js;
        }

        //public static int GetImpScanJs(string jhh, string make,string kjh)
        //{
        //    int js = 0;
        //    string sql = "select count(*) js from ImportStorageAcceptOrder where WCFlag=1 and RKB='" + Global.sKb + "' ";
        //    if (jhh != "")
        //    {
        //        sql += "and JHH='" + jhh + "' and Make='" + make + "' ";
        //    }
        //    if (kjh != "")
        //    {
        //        sql += "and CH='"+ kjh +"'";
        //    }

        //    DataTable dt = SqlCe.ExecuteQuery(sql);
        //    if (dt.Rows.Count > 0)
        //    {
        //        js = Convert.ToInt32(dt.Rows[0]["js"]);
        //    }

        //    return js;
        //}

        public static int GetExpScanJs(string jhh)
        {
            int js = 0;
            string sql = "select count(*) js from ExportStorageAcceptOrder where WCFlag=1 and JHH='"+ jhh +"' and CKB='"+ Global.sKb +"' ";
            DataTable dt = SqlCe.ExecuteQuery(sql);
            if (dt.Rows.Count > 0)
            {
                js = Convert.ToInt32(dt.Rows[0]["js"]);
            }

            return js;
        }

        public static DataTable GetImpScanCL(string jhh,string make)
        {
            string sql = "select *  from ImportStorageAcceptOrder where WCFlag=1 ";
            if (jhh != "")
            {
                sql += "and JHH='" + jhh + "' and Make='"+ make +"' ";
            }
            return SqlCe.ExecuteQuery(sql);
        }

        public static DataTable GetExpScanCL(string cph,string jhh,string make,string zfh)
        {
            string sql = "select A.* from ExportStorageAcceptOrder A,ExportStoragePlan B where A.JHH=B.JHH and A.Make=B.Make and  A.WCFlag=1 and A.CKB='" + Global.sKb + "' ";
            if (cph != "")
            {
                sql += "and B.CPH='"+ cph +"' ";
            }
            if (jhh != "")
            {
                sql += "and A.JHH='" + jhh + "' and A.Make='" + make + "' ";
            }
            if (zfh != "")
            {
                sql += "and A.ZFH='"+ zfh +"'";
            }
            return SqlCe.ExecuteQuery(sql);
        }

        public static DataTable GetExpSacnClByZfh(string cph, string jhh, string make, string zfh)
        {
            string sql = "select A.* from ExportStorageAcceptOrder A,ExportStoragePlan B where A.JHH=B.JHH and A.Make=B.Make and  A.WCFlag=1  and A.CKB='" + Global.sKb + "' ";
            if (cph != "")
            {
                sql += "and B.CPH='" + cph + "' ";
            }
            if (jhh != "")
            {
                sql += "and A.JHH='" + jhh + "' and A.Make='" + make + "' ";
            }
            if (zfh != "")
            {
                sql += "and A.ZFH='" + zfh + "' ";
            }
            sql += "order by A.ZFH";


            return SqlCe.ExecuteQuery(sql);

        }

        public static DataTable GetWtd(string kb)
        {
            string sql = "select WTDH,HPMC,ZJS,ZZL,CRFLAG,SJFS,WCJS,WCZL from Wtd where KB='"+ kb +"'";
            return SqlCe.ExecuteQuery(sql);
        }

        public static DataTable GetWtdCl(string jhh)
        {
            string sql = "select CLH,substring(KW,4,8) KW,JS,ZL,QA,SCANTIME,WCFLAG,LBL,FLAG from wtddetail where WTDH='" + jhh + "'";
            return SqlCe.ExecuteQuery(sql);
        }

        public static int GetWtdScanJs(string jhh)
        {
            int js = 0;
            string sql = "select count(*) js from WtdDetail where WCFlag=1 ";
            if (jhh != "")
            {
                sql += "and Wtdh='" + jhh + "' ";
            }

            DataTable dt = SqlCe.ExecuteQuery(sql);
            if (dt.Rows.Count > 0)
            {
                js = Convert.ToInt32(dt.Rows[0]["js"]);
            }

            return js;
        }

        public static void FinishImpScanCL(string jhh,string make)
        {
            string sql = "update ImportStorageAcceptOrder set WCFlag=2 where WCFlag=1 and JHH='" + jhh + "' and Make='"+ make +"'";
            SqlCe.ExecuteNonQuery(sql);
        }

        public static void FinishExpScanCL(string cph,string jhh,string make,string zfh)
        {
            string sql = "update ExportStorageAcceptOrder set WCFlag=2 where WCFlag=1  and CKB='"+ Global.sKb +"' ";
            if (cph != "")
            {
                sql += "and exists(select * from ExportStoragePlan B where ExportStorageAcceptOrder.JHH=B.JHH and ExportStorageAcceptOrder.Make=B.Make and B.CPH='"+ cph +"') ";
            }
            if (jhh != "")
            {
                sql += "and JHH='" + jhh + "' and Make='" + make + "' ";
            }
            if (zfh != "")
            {
                sql += "and ZFH='" + zfh + "'";
            }
            SqlCe.ExecuteNonQuery(sql);
        }

        public static void ClearImpScanStatus(string jhh,string make,string zfh,string clh)
        {
            string sql = "update ImportStorageAcceptOrder set WCFlag=0,SCANTIME='',KW='',CH='' where CLH='" + clh + "' and ZFH='" + zfh + "' and JHH='" + jhh + "' and Make='"+ make +"'";
            SqlCe.ExecuteNonQuery(sql);
        }

        public static void ClearWtdInScanStatus(string jhh, string lbl,string flag)
        {
            string sql = "";

            if (flag == "")
            {
                ClearWtdOutScanStatus(jhh, lbl);
            }
            else
            {
                sql = "delete from wtddetail where WTDH='" + jhh + "' and LBL='" + lbl + "'";
                SqlCe.ExecuteNonQuery(sql);
            }
        }

        public static void ClearWtdOutScanStatus(string jhh, string lbl)
        {
            string sql = "update wtddetail set WCFlag=0,SCANTIME='',lbl=clh where LBL='" + lbl + "' and WTDH='" + jhh + "'";
            SqlCe.ExecuteNonQuery(sql);
        }

        public static void ClearExpScanStatus(string jhh,string make,string zfh,string clh)
        {
            string sql = "update ExportStorageAcceptOrder set WCFlag=0,SCANTIME='' where CLH='" + clh + "' and ZFH='" + zfh + "' and JHH='" + jhh + "' and Make='"+ make +"'";
            SqlCe.ExecuteNonQuery(sql);
        }

        public static void SetImpScanStatus(string jhh,string make,string zfh,string clh,string kw,string now)
        {
            string sql = "update ImportStorageAcceptOrder set WCFlag=1,SCANTIME='" + now + "',KW='" + kw + "' where ZFH='" + zfh + "' and CLH='" + clh + "' and JHH='" + jhh + "' and Make='"+ make +"'";
            SqlCe.ExecuteNonQuery(sql);
        }

        public static void SetImpScanStatus(string jhh, string make, string zfh, string clh, string kw, string kjh, string now)
        {
            string sql = "update ImportStorageAcceptOrder set WCFlag=1,SCANTIME='" + now + "',KW='" + kw + "',CH='"+ kjh +"' where ZFH='" + zfh + "' and CLH='" + clh + "' and JHH='" + jhh + "' and Make='" + make + "'";
            SqlCe.ExecuteNonQuery(sql);
        }

        public static void SetExpScanStatus(string jhh,string make,string zfh,string clh,string now)
        {
            string sql = "update ExportStorageAcceptOrder set WCFlag=1,SCANTIME='" + now + "' where CLH='" + clh + "' and ZFH='" + zfh + "' and JHH='" + jhh + "' and Make='"+ make +"'";
            SqlCe.ExecuteNonQuery(sql);
        }

        public static void SetWtdInScanStatus(string jhh, string clh, string kw,string js,string zl,string now,string lbl)
        {
            string sql = "select * from wtddetail where wtdh='" + jhh + "' and clh='" + clh + "'";
            DataTable dt = SqlCe.ExecuteQuery(sql);
            if (dt.Rows.Count > 0)
            {
                sql = "update wtddetail set js=" + js + ",zl="+ zl + ",kw='"+ kw +"',scantime='"+ now +"',wcflag=1,lbl='"+ lbl +"' where wtdh='"+ jhh +"' and clh='"+ clh +"'";
                SqlCe.ExecuteNonQuery(sql);
            }
            else
            {
                sql = "insert into wtddetail (wtdh,clh,js,zl,kw,scantime,wcflag,lbl,flag) values('" + jhh + "','" + clh + "'," + js + "," + zl + ",'" + kw + "','" + now + "',1,'" + lbl + "','A')";
                SqlCe.ExecuteNonQuery(sql);
            }
            dt.Dispose();
        }

        public static void SetWtdOutScanStatus(string jhh,string clh,string wjs,string wzl,string now)
        {
            string sql = "update wtddetail set WCFlag=1,SCANTIME='" + now + "',wjs="+ wjs +",wzl="+ wzl +" where CLH='" + clh + "' and WTDH='" + jhh + "'";
            SqlCe.ExecuteNonQuery(sql);
        }

        public static void UpdateImpKw(string jhh,string make,string zfh,string clh,string kw)
        {
            string sql = "update ImportStorageAcceptOrder set KW='" + kw + "' where ZFH='" + zfh + "' and JHH='" + jhh + "' and Make='"+ make +"' and CLH='" + clh + "'";
            SqlCe.ExecuteNonQuery(sql);
        }

        public static void UpdateWtdInKw(string jhh,string lbl, string kw,string js,string zl)
        {
            string sql = "update wtddetail set KW='" + kw + "',JS="+ js +",ZL="+ zl +" where WTDH='" + jhh + "' and LBL='" + lbl + "'";
            SqlCe.ExecuteNonQuery(sql);
        }

        public static string GetCurrentKw(string zfh,string jhh,string make)
        {
            string kw = "";
                        
            string sql = "select KW from ZF2KW where ZFH='" + zfh + "' and JHH='"+ jhh +"' and Make='"+ make +"'";
            DataTable dt = SqlCe.ExecuteQuery(sql);
            if (dt.Rows.Count > 0)
            {
                kw = dt.Rows[0]["KW"].ToString();
            }
            dt.Dispose();
            return kw;

        }

        //public static string GetCurrentKw(string jhh)
        //{
        //    string kw = "";

        //    string sql = "select KW from ZF2KW where JHH='" + jhh + "'";
        //    DataTable dt = SqlCe.ExecuteQuery(sql);
        //    if (dt.Rows.Count > 0)
        //    {
        //        kw = dt.Rows[0]["KW"].ToString();
        //    }
        //    dt.Dispose();
        //    return kw;

        //}

        public static void SaveCurrentKw(string zfh,string jhh,string make,string kw)
        {
            string sql = "select * from ZF2KW where JHH='"+ jhh +"' and Make='"+ make +"' and ZFH='"+ zfh +"'";
            DataTable dt = SqlCe.ExecuteQuery(sql);
            if (dt.Rows.Count > 0)
            {
                sql = "update ZF2KW set KW='" + kw + "' where JHH='" + jhh + "' and Make='" + make + "' and ZFH='" + zfh + "'";
            }
            else
            {
                sql = "insert into ZF2KW (ZFH,JHH,Make,KW) values ('" + zfh + "','" + jhh + "','" + make + "','" + kw + "')";
            }
            dt.Dispose();
            SqlCe.ExecuteNonQuery(sql);
        }

        public static void ClearZF2KW()
        {
            string sql = "delete from ZF2KW";
            SqlCe.ExecuteNonQuery(sql);
        }

        //public static void SaveCurrentKw(string jhh, string kw)
        //{
        //    string sql = "select * from ZF2KW where JHH='" + jhh + "'";
        //    DataTable dt = SqlCe.ExecuteQuery(sql);
        //    if (dt.Rows.Count > 0)
        //    {
        //        sql = "update ZF2KW set KW='"+ kw +"' where JHH='"+ jhh +"'";
        //    }
        //    else
        //    {
        //        sql = "insert into ZF2KW (JHH,KW) values ('" + jhh + "','" + kw + "')";
        //    }
        //    dt.Dispose();
        //    SqlCe.ExecuteNonQuery(sql);
        //}

        public static string GetRemainCls(string jhh,string make,string zfh)
        {
            string remain = "";

            string sql = "select count(*) remain from ImportStorageAcceptOrder where ZFH='" + zfh + "' and JHH='" + jhh + "' and Make='"+ make +"' and (WCFlag is null or WCFlag<>2) ";
            DataTable dt = SqlCe.ExecuteQuery(sql);
            if (dt.Rows.Count > 0)
            {
                remain = dt.Rows[0]["remain"].ToString();
            }
            return remain;
        }

        public static void UpdateImpWcjs()
        {

            //string sql = "select A.JHH,A.ZFH,count(*) WCJS,sum(B.MZ) WCMZ,sum(B.JZ) WCJZ from ImportStorageOrder A,ImportStorageAcceptOrder B ";
            //sql += "where A.ZFH=B.ZFH and A.JHH=B.JHH and B.WCFlag=2 group by A.JHH,A.ZFH";

            //DataTable dt = SqlCe.ExecuteQuery(sql);

            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    SqlCe.ExecuteNonQuery("update ImportStorageOrder set WCJS=" + dt.Rows[i]["WCJS"].ToString() + ",WCMZ=" + dt.Rows[i]["WCMZ"].ToString() + ",WCJZ=" + dt.Rows[i]["WCJZ"].ToString() + " where ZFH='" + dt.Rows[i]["ZFH"].ToString() + "' and JHH='" + dt.Rows[i]["JHH"].ToString() + "'");
            //}

            string sql = "select A.JHH,A.MAKE,count(*) WCJS,sum(B.MZ) WCMZ,sum(B.JZ) WCJZ  from ImportStoragePlan A,ImportStorageAcceptOrder B ";
            sql += "where A.JHH=B.JHH and A.Make=B.Make and B.WCFlag=2 group by A.JHH,A.Make";


            DataTable dt = SqlCe.ExecuteQuery(sql);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                SqlCe.ExecuteNonQuery("update ImportStoragePlan set WCJS=" + dt.Rows[i]["WCJS"].ToString() + ",WCMZ=" + dt.Rows[i]["WCMZ"].ToString() + ",WCJZ=" + dt.Rows[i]["WCJZ"].ToString() + " where JHH='" + dt.Rows[i]["JHH"].ToString() + "' and Make='"+ dt.Rows[i]["Make"].ToString() +"'");
            }

            dt.Dispose();

            sql = "delete from ImportStorageAcceptOrder where exists(select * from ImportStoragePlan where JS=WCJS and JHH=ImportStorageAcceptOrder.JHH)";
            SqlCe.ExecuteNonQuery(sql);
            //sql = "delete from ImportStorageOrder where exists(select * from ImportStoragePlan where JS=WCJS and JHH=ImportStorageOrder.JHH)";
            //SqlCe.ExecuteNonQuery(sql);
            sql = "delete from ImportStoragePlan where JS=WCJS";
            SqlCe.ExecuteNonQuery(sql);
        }


        public static void UpdateExpWcjs()
        {
            //string sql = "select A.JHH,A.ZFH,count(*) WCJS,sum(B.MZ) WCMZ,sum(B.JZ) WCJZ from ExportStorageOrder A,ExportStorageAcceptOrder B ";
            //sql += "where A.ZFH=B.ZFH and A.JHH=B.JHH and B.WCFlag=2 group by A.JHH,A.ZFH";

            //DataTable dt = SqlCe.ExecuteQuery(sql);

            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    SqlCe.ExecuteNonQuery("update ExportStorageOrder set WCJS=" + dt.Rows[i]["WCJS"].ToString() + ",WCMZ=" + dt.Rows[i]["WCMZ"].ToString() + ",WCJZ=" + dt.Rows[i]["WCJZ"].ToString() + " where ZFH='" + dt.Rows[i]["ZFH"].ToString() + "' and JHH='" + dt.Rows[i]["JHH"].ToString() + "'");
            //}

            //sql = "select A.JHH,sum(B.WCJS) WCJS,sum(B.WCMZ) WCMZ,sum(B.WCJZ) WCJZ  from ExportStoragePlan A,ExportStorageOrder B ";
            //sql += "where A.JHH=B.JHH group by A.JHH";


            string sql="select A.JHH,A.MAKE,count(*) WCJS,sum(B.MZ) WCMZ,sum(B.JZ) WCJZ  ";
    
            sql += "from ExportStoragePlan A,ExportStorageAcceptOrder B ";
            
            sql += "where A.JHH=B.JHH and A.Make=B.Make and B.WCFlag=2 group by A.JHH,A.Make";

            DataTable dt = SqlCe.ExecuteQuery(sql);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                SqlCe.ExecuteNonQuery("update ExportStoragePlan set WCJS=" + dt.Rows[i]["WCJS"].ToString() + ",WCMZ=" + dt.Rows[i]["WCMZ"].ToString() + ",WCJZ=" + dt.Rows[i]["WCJZ"].ToString() + " where JHH='" + dt.Rows[i]["JHH"].ToString() + "' and Make='"+ dt.Rows[i]["MAKE"].ToString() +"'" );
            }

            dt.Dispose();

            sql = "delete from ExportStorageAcceptOrder where exists(select * from ExportStoragePlan where JS=WCJS and JHH=ExportStorageAcceptOrder.JHH)";
            SqlCe.ExecuteNonQuery(sql);
            //sql = "delete from ExportStorageOrder where exists(select * from ExportStoragePlan where JS=WCJS and JHH=ExportStorageOrder.JHH)";
            //SqlCe.ExecuteNonQuery(sql);
            sql = "delete from ExportStoragePlan where JS=WCJS";
            SqlCe.ExecuteNonQuery(sql);
        }

        public static string GetExportFlag(string clh)
        {
            string sql = "select * from ExportStorageAcceptOrder where CLH='" + clh + "' and CKB='" + Global.sKb + "'";
            string ret = "0";
            DataTable dt = SqlCe.ExecuteQuery(sql);
            if (dt.Rows.Count > 0)
            {
                ret = "1";
            }
            dt.Dispose();
            return ret;
        }

        public static bool isSameCLH(string clh, string text)
        {
            if (text.Length == 9 || text.Length == 10)
            {
                return (clh.EndsWith(text));
            }
            else
            {
                return (clh == text);
            }
        }

        ////changeByYang20170205 板坯材料号保存
        //public static void SaveCurrentSlabMaterials(string jhh, string clh)
        //{
        //    string sql = "select * from ExpSlabPlan where JHH='" + jhh + "' and CLH='" + clh + "'";
        //    DataTable dt = SqlCe.ExecuteQuery(sql);
        //    if (dt.Rows.Count > 0)
        //    {
        //        sql = "update ExpSlabPlan set CLH='" + clh + "' where JHH='" + jhh + "'";
        //        SqlCe.ExecuteNonQuery(sql);
        //    }
        //    else
        //    {
        //        sql = "insert into ExpSlabPlan (JHH,CLH) values ('" + jhh + "','" + clh + "')";
        //        SqlCe.ExecuteNonQuery(sql);
        //    }
        //    dt.Dispose();
        //    //  SqlCe.ExecuteNonQuery(sql);
        //}
    }
}
