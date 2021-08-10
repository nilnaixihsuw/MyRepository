using Mediinfo.DTO.HIS.GY;
using Mediinfo.ServiceProxy.JCJG.GongYong;

using System.Collections.Generic;
using System.Linq;

namespace Mediinfo.WinForm.HIS.Core
{
    /// <summary>
    /// 窗口控件辅助类（按钮权限、文字等）
    /// </summary>
    public static class GYChuangKouZYHelper
    {
        private static JCJGChuangKouZYService chuangKouZYService;
        private static List<E_GY_CHUANGKOUZY_NEW> chuangKouZYList;

        private static readonly object syncObject = new object();
        static GYChuangKouZYHelper()
        {
            if (chuangKouZYService == null)
            {
                lock (syncObject)
                {
                    if (chuangKouZYService == null)
                    {
                        chuangKouZYService = new JCJGChuangKouZYService();
                    }
                }
            }
            InitlizeCache();
        }

        /// <summary>
        /// 初始化缓存
        /// </summary>
        public static void InitlizeCache()
        {
            chuangKouZYList = chuangKouZYService.GetAll().Return;
        }

        /// <summary>
        /// 通过窗口ID获取控件信息
        /// </summary>
        /// <returns></returns>
        public static List<E_GY_CHUANGKOUZY_NEW> GetByForm(string nameSpace, string formName)
        {
            if(chuangKouZYList == null )
            {
                return new List<E_GY_CHUANGKOUZY_NEW>();
            }
            var list = chuangKouZYList.Where(c => c.NAMESPACE == nameSpace && c.FORMNAME == formName).ToList();

            var list2 = new List<E_GY_CHUANGKOUZY_NEW>();
            list.ForEach(c =>
            {
                list2.Add((E_GY_CHUANGKOUZY_NEW)c.Clone());
            });

            return list2;
        }

        /// <summary>
        /// 刷新缓存
        /// </summary>
        /// <param name="nameSpace"></param>
        /// <param name="formName"></param>
        public static void RefreshChuangKouInfo(string nameSpace, string formName)
        {
            chuangKouZYList.RemoveAll(o => o.NAMESPACE.ToUpper().Equals(nameSpace.ToUpper()) && o.FORMNAME.ToUpper().Equals(formName.ToUpper()));
            var chuangkouResult = chuangKouZYService.GetByFromName(nameSpace, formName);
            if (chuangkouResult.ReturnCode== Enterprise.ReturnCode.SUCCESS)
            {
                if (chuangkouResult.Return!=null)
                {
                    //chuangKouZYList.Where(o => o.NAMESPACE.ToUpper().Equals(nameSpace.ToUpper()) && o.FORMNAME.ToUpper().Equals(formName.ToUpper()));
                    chuangKouZYList.AddRange(chuangkouResult.Return);
                }
            }
        }

        ///// <summary>
        ///// 保存信息
        ///// </summary>
        ///// <param name="chuangKouZY"></param>
        ///// <returns></returns>
        //public static Result<bool> SaveChuangKouZY(List<E_GY_CHUANGKOUZY_NEW> chuangKouZY)
        //{
        //    //return JMService.SaveChuangKouZY(chuangKouZY);
        //    return null;
        //}
    }
}

