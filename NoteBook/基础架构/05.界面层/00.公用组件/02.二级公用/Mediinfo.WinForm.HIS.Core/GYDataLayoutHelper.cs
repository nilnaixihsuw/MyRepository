using Mediinfo.DTO.HIS.GY;
using Mediinfo.Enterprise;
using Mediinfo.HIS.Core;
using Mediinfo.ServiceProxy.JCJG.GongYong;

using System.Collections.Generic;
using System.Linq;

namespace Mediinfo.WinForm.HIS.Core
{
    public static class GYDataLayoutHelper
    {
        private static JCJGDataLayoutService service;
        private static List<E_GY_DATALAYOUTDTO> dataLayoutCache;

        static GYDataLayoutHelper()
        {
            service = new JCJGDataLayoutService();
            dataLayoutCache = new List<E_GY_DATALAYOUTDTO>();
        }

        /// <summary>
        /// 缓存初始化
        /// </summary>
        public static void InitializeCache()
        {
            if (dataLayoutCache != null)
            {
                var ret = service.GetDataLayoutInfoByYingYongId(HISClientHelper.YINGYONGID);

                if (ret.ReturnCode == ReturnCode.SUCCESS)
                {
                    dataLayoutCache = ret.Return;
                }
                else    // 直接初始化，防止以后判断起来麻烦
                {
                    dataLayoutCache = new List<E_GY_DATALAYOUTDTO>();
                }
            }
        }
        
        /// <summary>
        /// 从缓存中获取布局信息
        /// </summary>
        /// <param name="controlName"></param>
        /// <param name="formName"></param>
        /// <param name="nameSpace"></param>
        /// <param name="yingYongId"></param>
        public static E_GY_DATALAYOUTDTO GetDataLayoutInfo(string controlName, string formName, string nameSpace, string yingYongId)
        {
            if (string.IsNullOrWhiteSpace(controlName) || string.IsNullOrWhiteSpace(formName) || string.IsNullOrWhiteSpace(nameSpace))
                return null;

            if (null == dataLayoutCache)
                return null;

            if (string.IsNullOrWhiteSpace(yingYongId))
                yingYongId = HISClientHelper.YINGYONGID;

            // 取应用级
            var list = dataLayoutCache.Where(c => c.DataLayout1.CONTROLNAME == controlName
                                              && c.DataLayout1.FORMNAME == formName
                                              && c.DataLayout1.NAMESPACE == nameSpace
                                              && c.DataLayout1.YINGYONGID == yingYongId).ToList();

            if (list.Count > 0)
            {
                return (E_GY_DATALAYOUTDTO)list[0].Clone();
            }

            // 取系统级
            list = dataLayoutCache.Where(c => c.DataLayout1.CONTROLNAME == controlName
                                            && c.DataLayout1.FORMNAME == formName
                                            && c.DataLayout1.NAMESPACE == nameSpace
                                            && c.DataLayout1.YINGYONGID == yingYongId.Substring(0, 2)).ToList();
            if (list.Count > 0)
            {
                return (E_GY_DATALAYOUTDTO)list[0].Clone();
            }

            // 取全局
            list = dataLayoutCache.Where(c => c.DataLayout1.CONTROLNAME == controlName
                                            && c.DataLayout1.FORMNAME == formName
                                            && c.DataLayout1.NAMESPACE == nameSpace
                                            && c.DataLayout1.YINGYONGID == "00").ToList();
            if (list.Count > 0)
            {
                return (E_GY_DATALAYOUTDTO)list[0].Clone();
            }

            return null;
        }
        
        /// <summary>
        /// 清空某个DataLayout的信息，用于本地自定义界面的时候可以刷新缓存
        /// </summary>
        /// <param name="dataLayoutId"></param>
        public static void RefreshDataLayoutInfo(string controlName, string formName, string nameSpace, string yingYongId = "")
        {
            if (string.IsNullOrWhiteSpace(controlName) || string.IsNullOrWhiteSpace(formName) || string.IsNullOrWhiteSpace(nameSpace))
                return;

            if (string.IsNullOrWhiteSpace(yingYongId))
                yingYongId = HISClientHelper.YINGYONGID;
            if (dataLayoutCache != null)
            {
                if (dataLayoutCache.RemoveAll(c => c.DataLayout1.CONTROLNAME == controlName
                                       && c.DataLayout1.FORMNAME == formName
                                       && c.DataLayout1.NAMESPACE == nameSpace
                                       && c.DataLayout1.YINGYONGID == yingYongId) <= 0)
                {
                    if (dataLayoutCache.RemoveAll(c => c.DataLayout1.CONTROLNAME == controlName
                                            && c.DataLayout1.FORMNAME == formName
                                            && c.DataLayout1.NAMESPACE == nameSpace
                                            && c.DataLayout1.YINGYONGID == yingYongId.Substring(0, 2)) <= 0)
                    {
                        dataLayoutCache.RemoveAll(c => c.DataLayout1.CONTROLNAME == controlName
                                            && c.DataLayout1.FORMNAME == formName
                                            && c.DataLayout1.NAMESPACE == nameSpace
                                            && c.DataLayout1.YINGYONGID == "00");
                    }
                }
            }
            var ret = service.GetDataLayoutInfo(controlName, formName, nameSpace, yingYongId);
            if (ret.ReturnCode == ReturnCode.SUCCESS)
            {


                if (ret.Return.DataLayout1 == null && dataLayoutCache == null)
                {
                    dataLayoutCache = null;
                    return;
                }
                else if (ret.Return.DataLayout1 == null)
                {
                    return;
                }
                dataLayoutCache.Add((E_GY_DATALAYOUTDTO)ret.Return.Clone());
            }
        }
    }
}
