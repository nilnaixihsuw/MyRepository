using Mediinfo.DTO.HIS.GY;
using Mediinfo.Enterprise;
using Mediinfo.Enterprise.Config;
using Mediinfo.Enterprise.Log;
using Mediinfo.HIS.Core;
using Mediinfo.ServiceProxy.JCJG.GongYong;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mediinfo.WinForm.HIS.Core
{
    /// <summary>
    /// 界面层获取参数
    /// </summary>
    public static class GYCanShuHelper
    {
        private static JCJGCanSuService service;
        private static List<E_GY_CANSHU> canShuList;
        private static List<string> importantCSList = new List<string>();//重要的参数，一般只第三方接口参数，实时去取数据库，避免第三方接口蹦了后要重启系统
        static GYCanShuHelper()
        {
            service = new JCJGCanSuService();
        }

        /// <summary>
        /// 初始化参数缓存
        /// </summary>
        public static void InitializeCache()
        {
            var ret = service.GetByYingYongId(HISClientHelper.YINGYONGID);

            canShuList = ret.ReturnCode == Enterprise.ReturnCode.SUCCESS ? ret.Return : new List<E_GY_CANSHU>();

            canShuList.ForEach(c =>
            {
                HISCacheManager.AddCanShu(c.YINGYONGID, c.CANSHUID, c.CANSHUZHI);
            });
            importantCSList = GetList();

        }

        /// <summary>
        /// 获取参数（批量获取，优先从缓存中获取）
        /// </summary>
        /// <param name="canShuList"></param>
        /// <returns></returns>
        public static Dictionary<string, string> GetCanShu(List<E_GY_CANSHU_QUZHI> canShuList)
        {
            if (canShuList.Count <= 0)
                return new Dictionary<string, string>();

            Dictionary<string, string> canShuDict = new Dictionary<string, string>();
            List<E_GY_CANSHU_QUZHI> reqList = new List<E_GY_CANSHU_QUZHI>();

            string canShuZhi = string.Empty;

            foreach (var item in canShuList)
            {
                if (item == null) continue;

                if (string.IsNullOrWhiteSpace(item.YingYongID))
                    item.YingYongID = HISClientHelper.YINGYONGID;

                // 从缓存中找先
                bool result = HISCacheManager.GetCanShu(item.YingYongID, item.CanShuID, ref canShuZhi);
                if (!result)
                {
                    // 如果缓存中找不到则取 04
                    if (item.YingYongID != null && item.YingYongID.Length > 1)
                    {
                        result = HISCacheManager.GetCanShu(item.YingYongID.Substring(0, 2), item.CanShuID, ref canShuZhi);
                        if (!result)
                        {
                            // 如果 04 取不到则取 00
                            result = HISCacheManager.GetCanShu("00", item.CanShuID, ref canShuZhi);
                        }
                    }
                }

                if (result && !importantCSList.Contains(item.CanShuID))//modify by zhukp HR6-2113(541159)
                {
                    if (!canShuDict.ContainsKey(item.CanShuID))
                        canShuDict.Add(item.CanShuID, canShuZhi);
                    else
                        canShuDict[item.CanShuID] = canShuZhi;   // 如果包含则更新为最后一次的值
                }
                else
                {
                    reqList.Add(item);
                }
            }

            // 如果参数都缓存了，则直接返回
            if (reqList.Count <= 0)
            {
                return canShuDict;
            }

            if (!ApplicationHelper.IsDesignMode())
            {
                var result = service.GetCanShuZhi(reqList);

                if (result.ReturnCode == Enterprise.ReturnCode.SUCCESS)
                {
                    foreach (var item in result.Return)
                    {
                        HISCacheManager.AddCanShu(item.YingYongID, item.CanShuID, item.CanShuZhi);

                        if (!canShuDict.ContainsKey(item.CanShuID))
                            canShuDict.Add(item.CanShuID, item.CanShuZhi);
                        else
                            canShuDict[item.CanShuID] = item.CanShuZhi;     // 如果包含则更新为最后一次的值
                    }
                }
                else
                {
                    // 如果失败，则调用service部分的参数全部取默认值
                    foreach (var item in reqList)
                    {
                        if (!canShuDict.ContainsKey(item.CanShuID))
                            canShuDict.Add(item.CanShuID, item.CanShuZhi);
                        else
                            canShuDict[item.CanShuID] = item.CanShuZhi;     // 如果包含则更新为最后一次的值
                    }
                }
            }

            return canShuDict;
        }

        /// <summary>
        /// 获取本应用参数
        /// </summary>
        /// <param name="canShuId">参数名</param>
        /// <param name="defaultValue">默认值</param>
        public static string GetCanShu(string canShuId, string defaultValue)
        {
            List<E_GY_CANSHU_QUZHI> list = new List<E_GY_CANSHU_QUZHI>();
            list.Add(new E_GY_CANSHU_QUZHI(HISClientHelper.YINGYONGID, canShuId, defaultValue));

            //if (!ApplicationHelper.IsDesignMode())
            //{
            var canShu = GetCanShu(list);
            return canShu[canShuId];
            //}
            //else
            //{
            //    return string.Empty;
            //}
        }

        /// <summary>
        /// 获取参数
        /// </summary>
        /// <param name="yingYongId">应用ID</param>
        /// <param name="canShuId">参数名</param>
        /// <param name="defaultValue">默认值</param>
        public static string GetCanShu(string yingYongId, string canShuId, string defaultValue)
        {
            if (ApplicationHelper.IsDesignMode())
            {
                return null;
            }
            List<E_GY_CANSHU_QUZHI> list = new List<E_GY_CANSHU_QUZHI>();
            list.Add(new E_GY_CANSHU_QUZHI(yingYongId, canShuId, defaultValue));

            var canShu = GetCanShu(list);

            return canShu[canShuId];
        }
        private static List<string> GetList()
        {
            try
            {
                var paramValue = MediinfoConfig.GetValue("ThirdInterfaceParam.xml", "param");
                var array = paramValue.Split(',');
                return array.ToList();
            }
            catch (Exception ex)
            {
                LogHelper.Intance.Error(" 线程异常", "线程异常", "加载ThirdInterfaceParam.xml异常：" + ex.Message);
                return null;
            }
        }
    }
}
