using Mediinfo.DTO.HIS.GY;
using Mediinfo.Enterprise;
using Mediinfo.ServiceProxy.JCJG.GongYong;

using System.Collections.Generic;

namespace Mediinfo.WinForm.HIS.Core
{
    /// <summary>
    /// 临床工作台首页配置信息
    /// </summary>
    public static class GYGongZuoTDYHelper
    {
        public static JCJGGongZuoTDYService gYGongZuoTDYService;
        public static List<E_GY_GONGZUOTDY> gongZuoTdyList;

        static GYGongZuoTDYHelper()
        {
            gYGongZuoTDYService = new JCJGGongZuoTDYService();
        }

        /// <summary>
        /// 缓存初始化
        /// </summary>
        public static void InitializeCache()
        {
            var ret = gYGongZuoTDYService.GetList();

            if (ret.ReturnCode == ReturnCode.SUCCESS)
            {
                gongZuoTdyList = ret.Return;
            }
            else
            {
                gongZuoTdyList = new List<E_GY_GONGZUOTDY>();
            }
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns></returns>
        public static List<E_GY_GONGZUOTDY> GetGongZuoTDY()
        {
            if (gongZuoTdyList == null)
            {
                var ret = gYGongZuoTDYService.GetList();

                if (ret.ReturnCode == ReturnCode.SUCCESS)
                {
                    gongZuoTdyList = ret.Return;
                }
            }
            return gongZuoTdyList;
        }
    }
}
