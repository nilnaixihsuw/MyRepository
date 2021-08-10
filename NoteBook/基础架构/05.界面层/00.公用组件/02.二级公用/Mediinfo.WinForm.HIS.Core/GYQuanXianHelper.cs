using Mediinfo.DTO.HIS.GY;
using Mediinfo.Enterprise;
using Mediinfo.HIS.Core;
using Mediinfo.ServiceProxy.JCJG.GongYong;
using Mediinfo.Utility.Extensions;

using System.Collections.Generic;
using System.Linq;

namespace Mediinfo.WinForm.HIS.Core
{
    public static class GYQuanXianHelper
    {
        private static JCJGYingYongCDService quanXian = null;
        private static JCJGQuanXianService gYQuanXianService = null;
        private static JCJGJueSeQXService gYJueSeQXService = null;
        private static JCJGZhiGongService xTGLZhiGongService = null;
        private static readonly object syncObject = new object();
        private static List<E_GY_YONGHUQX> JueSeYHQXList = null;
        private static List<E_GY_JUESECKQX_NEW> JueSeYHCKQXList = null;
        private static List<E_GY_JUESEYH_EX> JueSeYHList = null;

        static GYQuanXianHelper()
        {
            // 单例模式
            if (quanXian == null)
            {
                lock (syncObject)
                {
                    if (quanXian == null)
                    {
                        quanXian = new JCJGYingYongCDService();
                        gYJueSeQXService = new JCJGJueSeQXService();
                        gYQuanXianService = new JCJGQuanXianService();
                        xTGLZhiGongService = new JCJGZhiGongService();
                    }
                }
            }
        }

        public static void InitializeCache()
        {
            string userid = string.IsNullOrEmpty(HISClientHelper.USERID) ? "DBA" : HISClientHelper.USERID;
            
            var ret = quanXian.GetYongHuQX(userid);
            var jsckret = quanXian.GetYongHuJSCKQX_NEW(HISClientHelper.YINGYONGID); //quanXian.GetYongHuJSCKQX(HISClientHelper.USERID); 

            if (jsckret.ReturnCode == Enterprise.ReturnCode.SUCCESS)
            {
                JueSeYHCKQXList = jsckret.Return.ToList();
            }
            if (ret.ReturnCode == Enterprise.ReturnCode.SUCCESS)
            {
                JueSeYHQXList = ret.Return;
            }
            var juesehy = xTGLZhiGongService.GetJueSeYHEXByYongHuID(userid);
            if (juesehy.ReturnCode == Enterprise.ReturnCode.SUCCESS)
            {
                JueSeYHList = juesehy.Return;
            }
        }

        /// <summary>
        /// 获取当前用户的所有权限（不包括停用的权限）
        /// </summary>
        /// <returns></returns>
        public static List<E_GY_YONGHUQX> GetQuanXian()
        {
            return JueSeYHQXList;
        }

        /// <summary>
        /// 判断该用户是否有权限
        /// </summary>
        /// <param name="quanXianID">权限名称</param>
        /// <param name="message">输出信息</param>
        /// <returns></returns>
        public static int GetQuanXian(string quanXianID, ref string message)
        {
            message = "";
            var ret = gYQuanXianService.GetQuanXianByQXID(quanXianID);
            if (ret.ReturnCode != ReturnCode.SUCCESS)
            {
                message = "根据权限名称获取权限信息失败！";
                return -1;
            }
            else
            {
                if (ret.Return.Count <= 0)
                {
                    // 插入一条数据到数据库中
                    var ret2 = gYQuanXianService.XinJianQX(quanXianID);
                    if (ret2.ReturnCode != ReturnCode.SUCCESS)
                    {
                        message = "数据库中没有该权限数据，新建权限信息失败！";
                        return -2;
                    }
                    else
                    {
                        message = "数据库中没有该权限数据！";
                        return -3;
                    }
                }
                else
                {
                    // 先判断启用标志，启用标志为0直接返回0有权限，启用标志为1再判断角色
                    string qiYongBZ = ret.Return[0].QIYONGBZ.ToStringEx();
                    if (qiYongBZ == "0")
                    {
                        return 0;
                    }
                    else
                    {
                        var ret1 = gYJueSeQXService.GetListByQXID(quanXianID);
                        if (ret1.ReturnCode != ReturnCode.SUCCESS)
                        {
                            message = "根据权限ID获取角色权限信息失败！";
                            return -4;
                        }
                        else
                        {
                            if (ret1.Return.Count <= 0)
                            {
                                message = "数据库中没有赋予该权限任何角色！";
                                return -5;
                            }
                        }
                    }

                }
            }
            return 0;
        }

        /// <summary>
        /// 获取当前用户的所有窗口权限（不包括停用的权限）
        /// </summary>
        /// <returns></returns>
        public static List<E_GY_JUESECKQX_NEW> GetJueSeYHQX()
        {
            List<E_GY_JUESECKQX_NEW> results = new List<E_GY_JUESECKQX_NEW>();
            if (JueSeYHList != null&& JueSeYHCKQXList!=null)
            {
                foreach (var item in JueSeYHList)
                {
                    JueSeYHCKQXList.ForEach(o => {
                        if (o.JUESEID == item.JUESEID)
                            results.Add(o);
                    }
                    );
                }
            }
            return results;
        }

        /// <summary>
        /// 获取用户权限 （不包括停用的权限）
        /// </summary>
        /// <param name="quanXianList">需要获取的权限列表</param>
        /// <param name="yongHuId">不传默认是当前用户</param>
        /// <returns></returns>
        public static Dictionary<string, bool> GetQuanXian(List<string> quanXianList, string yongHuId = "")
        {
            if (string.IsNullOrWhiteSpace(yongHuId))
                yongHuId = HISClientHelper.USERID;

            Dictionary<string, bool> quanXian = new Dictionary<string, bool>();

            foreach (var item in quanXianList)
            {
                if (JueSeYHList != null)// add by xuyi 2021/6/29
                {
                    List<string> jueseids = JueSeYHList.Select(o => o.JUESEID).ToList();
                    if (JueSeYHCKQXList != null && JueSeYHCKQXList.Where(c => jueseids.Contains(c.JUESEID) && c.QUANXIANID == item).Any())
                    {
                        quanXian.Add(item, true);
                    }
                }

            }
            return quanXian;
        }
    }
}
