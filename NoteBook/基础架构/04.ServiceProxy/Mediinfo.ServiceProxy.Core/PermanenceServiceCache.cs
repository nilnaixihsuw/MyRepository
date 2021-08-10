using Mediinfo.Utility;
using Mediinfo.Utility.Util;

using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.Caching;
using System.Text;

namespace Mediinfo.ServiceProxy.Core
{
    /// <summary>
    /// 上下文缓存
    /// </summary>
    public class PermanenceServiceCache : IDisposable
    {
        private MemoryCache cache = null;
        private CacheItemPolicy cp = null;
        private string filePath = Path.Combine(Environment.CurrentDirectory, "gateway.txt");    // 本地保存网关信息文件路径

        /// <summary>
        /// 构造函数
        /// </summary>
        private PermanenceServiceCache()
        {
            cache = new MemoryCache("PermanenceServiceCache");
            cp = new CacheItemPolicy();
        }

        public static PermanenceServiceCache Instance { get; } = new PermanenceServiceCache();

        #region 缓存

        /// <summary>
        /// 清空
        /// </summary>
        public virtual void Clear()
        {
            var list = cache.ToList();
            foreach (var item in list)
            {
                cache.Remove(item.Key);
            }
        }

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cacheKey"></param>
        /// <returns></returns>
        public virtual T GetFromCache<T>(string cacheKey) where T : class
        {
            if (null == this.cache)
                return default(T);

            return (T)this.cache.Get(cacheKey);
        }

        /// <summary>
        /// 新增缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cacheKey"></param>
        /// <param name="cacheObject"></param>
        /// <returns></returns>
        public virtual bool AddToCache<T>(string cacheKey, object cacheObject) where T : class
        {
            if (null == this.cache)
                return false;

            return this.cache.Add(cacheKey, cacheObject, cp);
        }

        /// <summary>
        /// 新增缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cacheKey"></param>
        /// <param name="cacheObject"></param>
        public virtual void PutToCache<T>(string cacheKey, object cacheObject) where T : class
        {
            if (null == this.cache)
                return;

            this.cache.Add(cacheKey, cacheObject, cp);
        }

        /// <summary>
        /// 更新缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cacheKey"></param>
        /// <param name="cacheObject"></param>
        /// <returns></returns>
        public virtual T UpdateToCache<T>(string cacheKey, object cacheObject) where T : class
        {
            if (null == this.cache)
                return default(T);

            cache.Remove(cacheKey);


            bool o = this.cache.Add(cacheKey, cacheObject, cp);
            if (!o)
                return default(T);

            return (T)cacheObject;
        }

        /// <summary>
        /// 添加或更新缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cacheKey"></param>
        /// <param name="cacheObject"></param>
        /// <returns></returns>
        public bool AddOrUpdateCache(string cacheKey, object cacheObject)
        {
            if (null == this.cache)
                return false;

            cache.Remove(cacheKey);


            bool o = this.cache.Add(cacheKey, cacheObject, cp);
            if (!o)
                return false;

            return true;
        }

        /// <summary>
        /// 删除缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cacheKey"></param>
        /// <returns></returns>
        public virtual bool RemoveFromCache<T>(string cacheKey) where T : class
        {
            if (null == this.cache)
                return true;

            return this.cache.Remove(cacheKey) != null;
        }

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cacheKey"></param>
        /// <returns></returns>
        public virtual bool ExistInCache<T>(string cacheKey) where T : class
        {
            if (null == this.cache)
                return false;

            return this.cache.Where(m => m.Key == cacheKey).Count() > 0;
        }

        private bool disposed = false;

        /// <summary>
        /// 析构
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    if (null != cache)
                    {
                        cache.Dispose();
                    }
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        /// <summary>
        /// 保存API网关信息
        /// </summary>
        /// <param name="content"></param>
        public void SaveGatewayInfo(string content)
        {
            // 获取配置文件中客户端网关缓存刷新时间，如果刷新缓存时间0时则表示为永久缓存
            double apiCacheRefreshTime = 0;
            if (ConfigurationManager.AppSettings["APICacheRefreshTime"] != null)
                apiCacheRefreshTime = Convert.ToDouble(ConfigurationManager.AppSettings["APICacheRefreshTime"]);

            Dictionary<string, string> serviceList = JsonUtil.DeserializeToObject<Dictionary<string, string>>(content);
            foreach (var service in serviceList)
            {
                // 如果缓存时间不为0则代表临时缓存
                if (apiCacheRefreshTime != 0)
                    CacheHelper.Cache.SetCache(service.Key, service.Value, apiCacheRefreshTime * 60);

                AddOrUpdateCache(service.Key, service.Value);
            }

            File.WriteAllText(filePath, content, Encoding.UTF8);
        }

        /// <summary>
        /// 获取本地API网关信息
        /// </summary>
        public void GetGatewayInfo()
        {
            if (!File.Exists(filePath)) return;

            FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            StreamReader sr = new StreamReader(fs, Encoding.Default);
            StringBuilder content = new StringBuilder();
            while (!sr.EndOfStream)
            {
                content.Append(sr.ReadLine());
            }
            sr.Close();
            fs.Close();

            Dictionary<string, string> serviceList = JsonUtil.DeserializeToObject<Dictionary<string, string>>(content.ToString());
            foreach (var service in serviceList)
            {
                AddOrUpdateCache(service.Key, service.Value);
            }
        }
    }
}
