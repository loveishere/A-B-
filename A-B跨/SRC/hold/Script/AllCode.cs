using System;

using System.Collections.Generic;
using System.Text;

namespace hStore
{
    public class AllCode
    {
        public const string stringInterfaceChar = ",";
        #region 本地文件
        /// <summary>
        /// 根目录
        /// </summary>
        public static string stringBootDir = "\\Flash Disk";
        #endregion
        /// <summary>
        /// 
        /// </summary>
        public static int intDataLength = 1000;


        /// <summary>
        /// 消息框
        /// </summary>
        public static DDSkyDll.Net20.MessageBox mbParent = null;

        #region webService 连接设置
        /// <summary>
        /// webService
        /// </summary>
        /// 
        public static SlabWebservice.ServiceSlabYard serviceSlabYard = new hStore.SlabWebservice.ServiceSlabYard();
        /// <summary>
        /// 板坯数据
        /// </summary>
        /// 
        public static SlabWebservice.PileData[] pileDataW= new hStore.SlabWebservice.PileData[0];
        public static List<SlabWebservice.PileData> mlistpileData_Check = new List<hStore.SlabWebservice.PileData>();

       
        

        #endregion
    }
}
