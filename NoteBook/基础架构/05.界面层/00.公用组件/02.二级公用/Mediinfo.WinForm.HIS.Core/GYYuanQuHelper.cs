using Mediinfo.DTO.HIS.GY;
using Mediinfo.Enterprise;
using Mediinfo.ServiceProxy.JCJG.GongYong;

using System.Collections.Generic;

namespace Mediinfo.WinForm.HIS.Core
{
    /// <summary>
    /// 界面院区数据获取
    /// </summary>
    public static class GYYuanQuHelper
    {
        private static JCJGYuanQuService service;
        private static List<E_GY_YUANQU> yuanQnList;

        static GYYuanQuHelper()
        {
            service = new JCJGYuanQuService();
        }

        /// <summary>
        /// 缓存初始化
        /// </summary>
        public static void InitializeCache()
        {
            var ret = service.GetGYYuanQu();

            if (ret.ReturnCode == ReturnCode.SUCCESS)
            {
                yuanQnList = ret.Return;
            }
            else 
            {
                yuanQnList = new List<E_GY_YUANQU>();
            }
        }

        /// <summary>
        /// 获取所有院区
        /// </summary>
        /// <returns></returns>
        public static List<E_GY_YUANQU> GetYuanQuList()
        {
            return yuanQnList;
        }
    }
}
