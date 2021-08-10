using System;
using System.Web;
using System.Web.Caching;

namespace Mediinfo.Utility
{
    /// <summary>
    /// 缓存管理
    public class CacheHelper
    {
        #region constructor

        /// <summary>
        /// 缓存管理
        /// </summary>
        private CacheHelper()
        {

        }

        #endregion

        #region properties

        /// <summary>
        /// 缓存数据
        /// </summary>
        public static CacheHelper Cache { get; } = new CacheHelper();

        #endregion

        #region public methods

        /// <summary>
        /// 获取缓存数据
        /// </summary>
        /// <param name="cacheKey">键</param>
        /// <returns></returns>
        public T GetCache<T>(string cacheKey)
        {
            return (T)HttpRuntime.Cache.Get(cacheKey);
        }

        /// <summary>
        /// 设置缓存数据
        /// </summary>
        /// <param name="cacheKey">键</param>
        /// <param name="objObject">需要缓存的数据</param>
        public void SetCache(string cacheKey, object objObject)
        {
            var objCache = HttpRuntime.Cache;
            // 如果发现缓存数据已存在则先删除再添加，保证缓存数据为最新的
            objCache.Remove(cacheKey);
            objCache.Insert(cacheKey, objObject);
        }

        /// <summary>
        /// 设置缓存数据
        /// </summary>
        /// <param name="cacheKey">键</param>
        /// <param name="objObject">需要缓存的数据</param>
        /// <param name="timeout">缓存时间(秒)</param>
        public void SetCache(string cacheKey, object objObject, double timeout = 7200)
        {
            var objCache = HttpRuntime.Cache;
            // 如果发现缓存数据已存在则先删除再添加，保证缓存数据为最新的
            objCache.Remove(cacheKey);
            // 绝对过期时间  
            objCache.Insert(cacheKey, objObject, null, DateTime.Now.AddSeconds(timeout), TimeSpan.Zero, CacheItemPriority.High, null);
        }

        /// <summary>
        /// 移除缓存数据
        /// </summary>
        /// <param name="cacheKey">键</param>
        public void RemoveAllCache(string cacheKey)
        {
            var cache = HttpRuntime.Cache;
            cache.Remove(cacheKey);
        }

        /// <summary>
        /// 移除全部缓存
        /// </summary>
        public void RemoveAllCache()
        {
            var cache = HttpRuntime.Cache;
            var cacheEnum = cache.GetEnumerator();
            while (cacheEnum.MoveNext())
            {
                cache.Remove(cacheEnum.Key.ToString());
            }
        }

        #endregion
    }
}
