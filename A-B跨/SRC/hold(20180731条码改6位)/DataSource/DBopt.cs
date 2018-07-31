using System;

using System.Collections.Generic;
using System.Text;

namespace hStore
{
    class DBopt
    {
        #region ----------字符串处理----------
        /// <summary>
        /// 转定义符
        /// </summary>
        public static char charTurnDefine = (char)0x00;
        /// <summary>
        /// 分隔符
        /// </summary>
        public static char charSeparate = '|';

        /// <summary>
        /// 组合结果
        /// </summary>
        /// <param name="whatData"></param>
        /// <returns></returns>
        public static string CombineString(object[] whatData)
        {
            string stringTurnDefine = "" + charTurnDefine;
            if (charTurnDefine != (char)0x00)
                stringTurnDefine = "" + charTurnDefine + charTurnDefine;
            string stringSeparate = "" + charSeparate;
            if (charTurnDefine != (char)0x00)
                stringSeparate = "" + charTurnDefine + charSeparate;
            string stringRes = "";

            for (int inta = 0; inta < whatData.Length; inta++)
                stringRes = stringRes + whatData[inta].ToString().Replace("" + charTurnDefine, stringTurnDefine).Replace("" + charSeparate, stringSeparate) + charSeparate;
            return stringRes;
        }

        /// <summary>
        /// 拆分结果
        /// </summary>
        /// <param name="whatData"></param>
        /// <returns></returns>
        public static string[] DecomposeString(string whatData)
        {
            if (whatData == null)
                whatData = "";

            string[] stringArrRes = new string[0];

            if (charTurnDefine == (char)0x00)
                stringArrRes = whatData.Split(new char[] { charSeparate });
            else
            {
                string stringTurnDefine = "" + charTurnDefine + charTurnDefine;
                string stringSeparate = "" + charTurnDefine + charSeparate;

                List<string> listRes = new List<string>();
                string stringNew = "";
                while (whatData.Length > 0)
                {
                    //找第一个分隔符
                    int intFirstSeparate = whatData.IndexOf(charSeparate);
                    if (intFirstSeparate < 0)
                    {//没有分隔符
                        stringNew = stringNew + whatData;
                        listRes.Add(stringNew.Replace(stringSeparate, "" + charSeparate).Replace(stringTurnDefine, "" + charTurnDefine));
                        break;
                    }
                    else
                    {//有分隔符
                        string stringNewSetion = whatData.Substring(0, intFirstSeparate + 1);
                        whatData = whatData.Substring(intFirstSeparate + 1);
                        //检查分隔符前转定义符的数量
                        int intTurnNum = 0;
                        for (int inta = 0; inta < stringNewSetion.Length - 1; inta++)
                        {
                            if (stringNewSetion[stringNewSetion.Length - 2 - inta] == charTurnDefine)
                                intTurnNum = intTurnNum + 1;
                            else
                                break;
                        }
                        //双数为真分隔，单数为转定义分隔
                        if (intTurnNum % 2 == 0)
                        {
                            stringNew = stringNew + stringNewSetion.Substring(0, stringNewSetion.Length - 1);
                            listRes.Add(stringNew.Replace(stringSeparate, "" + charSeparate).Replace(stringTurnDefine, "" + charTurnDefine));
                            stringNew = "";
                        }
                        else
                            stringNew = stringNew + stringNewSetion;
                    }
                }
                stringArrRes = listRes.ToArray();
            }

            return stringArrRes;
        }
        #endregion
    }
}
