using Mediinfo.DTO.HIS.XT;
using Mediinfo.Enterprise;
using Mediinfo.ServiceProxy.JCJG.GongYong;

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Caching;

namespace Mediinfo.WinForm.HIS.Core
{
    /// <summary>
    /// 缓存帮助类
    /// </summary>
    public static class HISCacheManager
    {
        /// <summary>
        /// 
        /// </summary>
        public static MemoryCache CanShuCache = new MemoryCache("CanShu");

        /// <summary>
        /// 
        /// </summary>
        public static MemoryCache ShuJuZDCache = new MemoryCache("ShuJuZD");

        /// <summary>
        /// 
        /// </summary>
        public static MemoryCache FangAnPZCache = new MemoryCache("FangAnPZ");

        /// <summary>
        /// 方案查询结果缓存
        /// </summary>
        private static MemoryCache FangAnCXCache = new MemoryCache("FangAnCX");

        /// <summary>
        /// 
        /// </summary>
        private static readonly CacheItemPolicy policy = new CacheItemPolicy();

        static HISCacheManager()
        {
            policy.Priority = CacheItemPriority.NotRemovable;
        }

        /// <summary>
        /// 从缓存中获取参数
        /// </summary>
        /// <param name="yingYongId"></param>
        /// <param name="canShuId"></param>
        /// <param name="canShuZhi"></param>
        /// <returns></returns>
        public static bool GetCanShu(string yingYongId, string canShuId, ref string canShuZhi)
        {
            string key = string.Format("{0}-{1}", yingYongId, canShuId);

            var o = CanShuCache.Get(key);

            if (o == null)
            {
                return false;
            }

            canShuZhi = o.ToString();
            return true;
        }

        /// <summary>
        /// 添加参数进缓存中
        /// </summary>
        /// <param name="yingYongId"></param>
        /// <param name="canShuId"></param>
        /// <param name="canShuZhi"></param>
        public static void AddCanShu(string yingYongId, string canShuId, string canShuZhi)
        {
            string key = string.Format("{0}-{1}", yingYongId, canShuId);
            CanShuCache.Set(key, canShuZhi, policy);
        }      

        /// <summary>
        /// 获取所有缓存键
        /// </summary>
        /// <returns></returns>
        public static List<string> GetCacheKeys(MemoryCache cache)
        {
            return cache.Select(t => t.Key).ToList();
        }

        /// <summary>
        /// 获取数据字典缓存（拷贝）
        /// </summary>
        /// <param name="shuJuZDId"></param>
        /// <returns>如果不存则返回空</returns>
        public static DataTable GetShuJuZD(string shuJuZDId)
        {
            if (string.IsNullOrWhiteSpace(shuJuZDId))
                return null;

            Object o = ShuJuZDCache.Get(shuJuZDId);
            if (null == o)
                return null;

            DataTable dt = (o as DataTable);

            return dt.Copy();
        }

        /// <summary>
        /// 添加数据字典缓存
        /// </summary>
        /// <param name="shuJuZDId"></param>
        /// <param name="dt"></param>
        public static void AddShuJuZD(string shuJuZDId, DataTable dt)
        {
            if (string.IsNullOrWhiteSpace(shuJuZDId) || null == dt)
                return;

            ShuJuZDCache.Add(shuJuZDId, dt.Copy(), policy);
        }

        /// <summary>
        /// 从缓存中获取方案配置信息
        /// </summary>
        /// <param name="xiangMuMC">项目名</param>
        /// <param name="fangAnMC">方案名</param>
        /// <param name="fangAn">方案配置</param>
        /// <returns></returns>
        public static bool GetFangAN(string xiangMuMC, string fangAnMC, ref E_XT_SELECTSQL2_EX fangAn)
        {
            string key = string.Format("{0}-{1}", xiangMuMC, fangAnMC);

            var o = FangAnPZCache.Get(key);
            if (o == null)
            {
                return false;
            }
            else
            {
                fangAn = (o as E_XT_SELECTSQL2_EX);
                return true;
            }
        }

        /// <summary>
        /// 添加方案配置缓存
        /// </summary>
        /// <param name="key">key值</param>
        /// <param name="fangAn">方案配置信息</param>
        public static void AddFangAnPZ(string key, E_XT_SELECTSQL2_EX fangAn)
        {
            if (string.IsNullOrWhiteSpace(key) || null == fangAn)
                return;

            FangAnPZCache.Add(key, fangAn, policy);
        }

        //20200806 add by zhengcj for HR6-4430 begin
        /// <summary>
        /// 获取方案查询缓存
        /// </summary>
        /// <param name="fangAnSQL"></param>
        public static DataTable GetFangAnCX(string fangAnSQL)
        {
            if (string.IsNullOrWhiteSpace(fangAnSQL))
                return null;

            Object o = FangAnCXCache.Get(fangAnSQL);
            if (null == o)
                return null;

            DataTable dt = (o as DataTable);

            return dt.Copy();
        }

        /// <summary>
        /// 是否存在方案查询缓存
        /// </summary>
        /// <param name="fangAnSQL"></param>
        public static bool ContainsFangAnCX(string fangAnSQL)
        {
            if (string.IsNullOrWhiteSpace(fangAnSQL))
                return false;

            return FangAnCXCache.Contains(fangAnSQL);
        }

        /// <summary>
        /// 添加方案查询缓存
        /// </summary>
        /// <param name="fangAnSQL"></param>
        /// <param name="dt"></param>
        public static void SetFangAnCX(string fangAnSQL, DataTable dt)
        {
            if (string.IsNullOrWhiteSpace(fangAnSQL) || null == dt)
                return;

            FangAnCXCache.Set(fangAnSQL, dt.Copy(), policy);
        }

        /// <summary>
        /// 移除方案查询缓存
        /// </summary>
        /// <param name="fangAnSQL"></param>
        public static void RemoveFangAnCX(string fangAnSQL)
        {
            if (string.IsNullOrWhiteSpace(fangAnSQL) || !ShuJuZDCache.Contains(fangAnSQL))
                return;

            FangAnCXCache.Remove(fangAnSQL);
        }

        /// <summary>
        /// 清空缓存
        /// </summary>
        public static void ClearFangAnCX()
        {
            var cache = FangAnCXCache;
            FangAnCXCache = new MemoryCache("FangAnCX");
            cache.Dispose();
        }
        //20200806 add by zhengcj for HR6-4430 end
    }
}
