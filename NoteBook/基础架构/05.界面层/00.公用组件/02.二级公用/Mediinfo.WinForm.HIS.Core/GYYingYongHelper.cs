using Mediinfo.DTO.HIS.GY;
using Mediinfo.Enterprise;
using Mediinfo.ServiceProxy.JCJG.GongYong;

using System.Collections.Generic;

namespace Mediinfo.WinForm.HIS.Core
{
    /// <summary>
    /// 公用控件取应用数据
    /// </summary>
    public static class GYYingYongHelper
    {
        private static JCJGYingYongService service;
        private static List<E_GY_YINGYONG> yingYongList;

        static GYYingYongHelper()
        {
            service = new JCJGYingYongService();
        }

        /// <summary>
        /// 缓存初始化
        /// </summary>
        public static void InitializeCache()
        {
            var ret = service.GetGYList();

            if (ret.ReturnCode == ReturnCode.SUCCESS)
            {
                yingYongList = ret.Return;
            }
            else
            {
                yingYongList = new List<E_GY_YINGYONG>();
            }
        }

        /// <summary>
        /// 公用控件取应用数据
        /// </summary>
        /// <returns></returns>
        public static List<E_GY_YINGYONG> GetYingYong()
        {
            return yingYongList;
        }
    }
}
