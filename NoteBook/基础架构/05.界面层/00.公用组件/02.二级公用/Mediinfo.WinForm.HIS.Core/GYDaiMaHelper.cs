using Mediinfo.DTO.HIS.GY;
using Mediinfo.Enterprise;
using Mediinfo.ServiceProxy.JCJG.GongYong;

using System.Collections.Generic;

namespace Mediinfo.WinForm.HIS.Core
{
    /// <summary>
    /// 公用控件获取公用代码数据
    /// </summary>
    public static class GYDaiMaHelper
    {
        private static JCJGDaiMaService service;
        private static List<E_GY_DAIMA> daiMaList;

        static GYDaiMaHelper()
        {
            service = new JCJGDaiMaService();
        }

        /// <summary>
        /// 缓存初始化
        /// </summary>
        public static void InitializeCache()
        {
            //数据量过大!
            //var ret = service.GetGYList();

            //if (ret.ReturnCode == ReturnCode.SUCCESS)
            //{
            //    daiMaList = ret.Return;
            //}
            //else
            //{
            //    daiMaList = new List<E_GY_DAIMA>();
            //}
        }

        /// <summary>
        /// 公用控件获取公用代码数据
        /// </summary>
        /// <param name="daimalb">代码类型ID</param>
        /// <param name="menzhensy">门诊使用</param>
        /// <param name="zhuyuansy">住院使用</param>
        /// <param name="zuofeibz">作废标志</param>
        /// <returns></returns>
        public static List<E_GY_DAIMA> GteGYList(string daimalb, int? menzhensy,int? zhuyuansy,int zuofeibz)
        {
            var ret = service.GetGYList(daimalb,menzhensy,zhuyuansy,zuofeibz);

            if (ret.ReturnCode == ReturnCode.SUCCESS)
            {
                daiMaList = ret.Return;
            }
            else
            {
                daiMaList = new List<E_GY_DAIMA>();
            }
            return daiMaList;
        }
    }
}
