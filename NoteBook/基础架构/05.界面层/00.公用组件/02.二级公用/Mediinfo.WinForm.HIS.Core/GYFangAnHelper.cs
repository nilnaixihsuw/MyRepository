using Mediinfo.DTO.HIS.XT;
using Mediinfo.Enterprise;
using Mediinfo.ServiceProxy.JCJG.GongYong;

using System.Collections.Generic;

namespace Mediinfo.WinForm.HIS.Core
{
    /// <summary>
    /// 界面获取方案信息
    /// </summary>
    public static class GYFangAnHelper
    {
        private static JCJGFangAnPZService service;

        static GYFangAnHelper()
        {
            service = new JCJGFangAnPZService();
        }

        public static void InitializeCache()
        {
            var ret = service.GetAllFangAn();
            var fangAnList = ret.Return;

            if (ret.ReturnCode == ReturnCode.SUCCESS)
            {
                foreach (var item in fangAnList)
                {
                    string xiangMMC = item.SQLID.ToString();
                    string fangAnMC = item.FANGANMC.ToString();
                    HISCacheManager.AddFangAnPZ(xiangMMC + "-" + fangAnMC, item);
                }
            }
        }

        /// <summary>
        /// 根据项目名和方案名获取方案信息
        /// </summary>
        /// <param name="xiangMu">项目名</param>
        /// <param name="fangAnMing">方案名</param>
        /// <returns></returns>
        public static Dictionary<string, E_XT_SELECTSQL2_EX> GetFangAnPZ(string xiangMu, string fangAnMing)
        {
            if (string.IsNullOrWhiteSpace(xiangMu) || string.IsNullOrWhiteSpace(fangAnMing))
                return new Dictionary<string, E_XT_SELECTSQL2_EX>();

            Dictionary<string, E_XT_SELECTSQL2_EX> canShuDict = new Dictionary<string, E_XT_SELECTSQL2_EX>();
            List<E_XT_SELECTSQL2_EX> reqList = new List<E_XT_SELECTSQL2_EX>();
            //DataTable dataTable = new DataTable();
            //DataRow fangan = dataTable.NewRow();

            E_XT_SELECTSQL2_EX fangan = new E_XT_SELECTSQL2_EX();

            // 从缓存中找先

            // 找方案名不带@的方案
            if (HISCacheManager.GetFangAN(xiangMu, fangAnMing, ref fangan))
            {
                if (!canShuDict.ContainsKey(xiangMu + "-" + fangAnMing))
                    canShuDict.Add(xiangMu + "-" + fangAnMing, fangan);
                else
                    canShuDict[xiangMu + "-" + fangAnMing] = fangan;
            }
            else
            {
                reqList.Add(fangan);
            }

            // 找方案名带@的方案
            if (HISCacheManager.GetFangAN(xiangMu, fangAnMing + "@", ref fangan))
            {
                if (!canShuDict.ContainsKey(xiangMu + "-" + fangAnMing + "@"))
                    canShuDict.Add(xiangMu + "-" + fangAnMing + "@", fangan);
                else
                    canShuDict[xiangMu + "-" + fangAnMing + "@"] = fangan;
            }
            
            // 找项目名带@的方案
            if (HISCacheManager.GetFangAN(xiangMu + "@", fangAnMing, ref fangan))
            {
                if (!canShuDict.ContainsKey(xiangMu + "@" + "-" + fangAnMing))
                    canShuDict.Add(xiangMu + "@" + "-" + fangAnMing, fangan);
                else
                    canShuDict[xiangMu + "@" + "-" + fangAnMing] = fangan;
            }
            
            // 如果参数缓存了，则直接返回
            if (reqList.Count <= 0)
            {
                return canShuDict;
            }

            // 查找方案名不带@方案信息
            var ret = service.GetFangAn(xiangMu, fangAnMing);
            var fangAnList = ret.Return;
            if (ret.ReturnCode != Enterprise.ReturnCode.SUCCESS)
            {
                return null;
            }
            
            foreach (var item in fangAnList)
            {
                if (!canShuDict.ContainsKey(item.SQLID + "-" + item.FANGANMC))
                    canShuDict.Add(item.SQLID + "-" + item.FANGANMC, item);
                else
                    canShuDict[item.SQLID + "-" + item.FANGANMC] = item;
            }

            //if (fangAnList != null && fangAnList.Count > 0)
            //{
            //    if (!canShuDict.ContainsKey(xiangMu + "-" + fangAnMing))
            //        canShuDict.Add(xiangMu + "-" + fangAnMing, fangAnList[0]);
            //    else
            //        canShuDict[xiangMu + "-" + fangAnMing] = fangAnList[0];
            //}


            //if (fangAnList1 != null && fangAnList1.Count > 0)
            //{
            //    if (!canShuDict.ContainsKey(xiangMu + "-" + fangAnMing + "@"))
            //        canShuDict.Add(xiangMu + "-" + fangAnMing + "@", fangAnList1[0]);
            //    else
            //        canShuDict[xiangMu + "-" + fangAnMing + "@"] = fangAnList1[0];
            //}


            //if (fangAnList2 != null && fangAnList2.Count > 0)
            //{
            //    if (!canShuDict.ContainsKey(xiangMu + "@"+ "-" + fangAnMing ))
            //        canShuDict.Add(xiangMu+ "@" + "-" + fangAnMing , fangAnList2[0]);
            //    else
            //        canShuDict[xiangMu+ "@" + "-" + fangAnMing ] = fangAnList2[0];

            //}
            
            //if (fangAnList != null && fangAnList.Count > 0)
            //{
            //    if (!fangAnList[0].FANGANMC.Contains("@"))
            //    {
            //        if (!canShuDict.ContainsKey(xiangMu + "-" + fangAnMing))
            //            canShuDict.Add(xiangMu + "-" + fangAnMing, fangAnList[0]);
            //        else
            //            canShuDict[xiangMu + "-" + fangAnMing] = fangAnList[0];

            //        //找方案名中带@
            //        if (fangAnList.Count > 1)
            //        {
            //            if (!canShuDict.ContainsKey(xiangMu + "-" + fangAnMing + "@"))
            //                canShuDict.Add(xiangMu + "-" + fangAnMing + "@", fangAnList[1]);
            //            else
            //                canShuDict[xiangMu + "-" + fangAnMing + "@"] = fangAnList[1];
            //        }
            //    }
            //    else
            //    {
            //        if (!canShuDict.ContainsKey(xiangMu + "-" + fangAnMing + "@"))
            //            canShuDict.Add(xiangMu + "-" + fangAnMing + "@", fangAnList[0]);
            //        else
            //            canShuDict[xiangMu + "-" + fangAnMing + "@"] = fangAnList[0];

            //        //找方案名中带@
            //        if (fangAnList.Count > 1)
            //        {
            //            if (!canShuDict.ContainsKey(xiangMu + "-" + fangAnMing))
            //                canShuDict.Add(xiangMu + "-" + fangAnMing, fangAnList[1]);
            //            else
            //                canShuDict[xiangMu + "-" + fangAnMing] = fangAnList[1];
            //        }
            //    }
            //}

            return canShuDict;
        }
    }
}
