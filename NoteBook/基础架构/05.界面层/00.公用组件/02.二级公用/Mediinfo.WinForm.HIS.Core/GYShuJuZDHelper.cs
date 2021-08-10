using Mediinfo.Enterprise;
using Mediinfo.HIS.Core;
using Mediinfo.ServiceProxy.JCJG.GongYong;

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Mediinfo.WinForm.HIS.Core
{
    /// <summary>
    /// 通用数据字典（用于将界面部分ID转换成名称）
    /// </summary>
    public static class GYShuJuZDHelper
    {
        private static JCJGShuJuZDService service;

        static GYShuJuZDHelper()
        {
            service = new JCJGShuJuZDService();
        }

        /// <summary>
        /// 获取所有
        /// </summary>
        public static void InitializeCache()
        {
            var ret = service.GetByYingYongId(HISClientHelper.YINGYONGID);
            if (ret.ReturnCode == ReturnCode.SUCCESS)
            {
                foreach (DataTable dt in ret.Return.DataSetContent.Tables)
                {
                    HISCacheManager.AddShuJuZD(dt.TableName, dt.Copy());
                }
            }
        }

        /// <summary>
        /// 批量获取数据字典（DataSet中的表名就是sqlId）
        /// </summary>
        /// <param name="">数据字典的ID列表</param>
        /// <returns>返回批量获取数据字典的数据集</returns>
        public static DataSet GetShuJuZDList(List<String> sqlIdList)
        {
            DataSet dsReturn = new DataSet();
            DataTable dtCache = null;
            List<string> sqlIdReqList = new List<string>();

            foreach (var item in sqlIdList)
            {
                dtCache = HISCacheManager.GetShuJuZD(item);
                if (null != dtCache)
                {
                    dsReturn.Tables.Add(dtCache);
                }
                else
                {
                    sqlIdReqList.Add(item);
                }
            }

            if (sqlIdReqList.Count <= 0)
                return dsReturn;

            // 从服务端取
            var ret = service.GetShuJuZDList(sqlIdReqList);

            if (ret.ReturnCode == ReturnCode.SUCCESS)
            {
                foreach (DataTable dt in ret.Return.Tables)
                {
                    dsReturn.Tables.Add(dt.Copy());
                    HISCacheManager.AddShuJuZD(dt.TableName, dt.Copy());
                }
            }
            else
            {
                foreach (var item in sqlIdReqList)
                {
                    dsReturn.Tables.Add(new DataTable(item));
                }
            }

            return dsReturn;
        }

        /// <summary>
        /// 获取数据字典（DataTable的名字就是SqlId）
        /// </summary>
        /// <param name="sqlId">数据字典的ID</param>
        /// <returns>返回数据字典的DataTable</returns>
        public static DataTable GetShuJuZD(string sqlId)
        {
            List<string> list = new List<string>();
            list.Add(sqlId);

            DataSet ds = GetShuJuZDList(list);

            if (ds.Tables.Count > 0)
                return ds.Tables[0];
            else
                return null;
        }

        /// <summary>
        /// 根据ID获取Name,支持不同类型
        /// </summary>
        /// <param name="leiXing">支持类型:职工、科室、病区、费用性质、费用类别  </param>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static string GetName(string leiXing, String ID)
        {
            string returnString;
            DataTable ret = new DataTable();
            switch (leiXing)
            {
                case "职工":
                    ret = GetShuJuZD("公用职工");
                    if (ret != null)
                    {
                        var dt = ret.Select("ZHIGONGID = '" + ID + "'");
                        if (dt.Any())
                        {
                            returnString = dt[0]["ZHIGONGXM"] as string;
                            return returnString;
                        }
                    }
                    break;
                case "科室":
                    ret = GetShuJuZD("公用科室");
                    if (ret != null)
                    {
                        var dt = ret.Select("KESHIID = '" + ID + "'");
                        if (dt.Any())
                        {
                            returnString = dt[0]["KESHIMC"] as string;
                            return returnString;
                        }
                    }
                    break;
                case "病区":
                    ret = GetShuJuZD("公用病区");
                    if (ret != null)
                    {
                        var dt = ret.Select("BINGQUID = '" + ID + "'");
                        if (dt.Any())
                        {
                            returnString = dt[0]["BINGQUMC"] as string;
                            return returnString;
                        }
                    }
                    break;
                case "费用类别":
                    ret = GetShuJuZD("公用费用类别");
                    if (ret != null)
                    {
                        var dt = ret.Select("LEIBIEID = '" + ID + "'");
                        if (dt.Any())
                        {
                            returnString = dt[0]["LEIBIEMC"] as string;
                            return returnString;
                        }
                    }
                    break;
                case "费用性质":
                    ret = GetShuJuZD("公用费用性质");
                    if (ret != null)
                    {
                        var dt = ret.Select("XINGZHIID = '" + ID + "'");
                        if (dt.Any())
                        {
                            returnString = dt[0]["XINGZHIMC"] as string;
                            return returnString;
                        }
                    }
                    break;
                case "医疗组":
                    ret = GetShuJuZD("公用医疗组");
                    if (ret != null)
                    {
                        var dt = ret.Select("YILIAOZID='" + ID + "'");
                        if (dt.Any())
                        {
                            returnString = dt[0]["YILIAOZM"] as string;
                            return returnString;
                        }
                    }
                    break;
                case "给药方式":
                    ret = GetShuJuZD("公用给药方式");
                    if (ret != null)
                    {
                        var dt = ret.Select("GEIYAOFSID='" + ID + "'");
                        if (dt.Any())
                        {
                            returnString = dt[0]["GEIYAOFSMC"] as string;
                            return returnString;
                        }
                    }
                    break;
                case "应用":
                    ret = GetShuJuZD("公用应用");
                    if (ret != null)
                    {
                        var dt = ret.Select("YINGYONGID='" + ID + "'");
                        if (dt.Any())
                        {
                            returnString = dt[0]["YINGYONGMC"] as string;
                            return returnString;
                        }
                    }
                    break;
                case "离院去向":
                    ret = GetShuJuZD("公用离院去向");
                    if (ret != null)
                    {
                        var dt = ret.Select("DAIMAID='" + ID + "'");
                        if (dt.Any())
                        {
                            returnString = dt[0]["DAIMAMC"] as string;
                            return returnString;
                        }
                    }
                    break;
                default:
                    return "";
            }
            return "";
        }
    }
}
