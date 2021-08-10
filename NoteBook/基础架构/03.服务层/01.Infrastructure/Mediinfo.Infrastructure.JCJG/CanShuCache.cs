using Mediinfo.HIS.Core;
using Mediinfo.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace Mediinfo.Infrastructure.JCJG
{
    /// <summary>
    /// 参数缓存
    /// </summary>
    public class CanShuCache : IDisposable
    {
        private MemoryCache cache = null;
        private bool disposed = false;

        public CanShuCache()
        {
            cache = new MemoryCache("CanShuCache");
        }

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
        /// <param name="cacheKey"></param>
        /// <returns></returns>
        public virtual string GetValue(string cacheKey)
        {
            if (null == this.cache)
                return null;

            return this.cache.Get(cacheKey).ToStringEx();
        }

        /// <summary>
        /// 新增缓存
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <param name="cacheValue"></param>
        /// <returns></returns>
        public virtual bool Add(string cacheKey, string cacheValue)
        {
            if (null == this.cache)
                return false;

            var policy = new CacheItemPolicy() { AbsoluteExpiration = DateTime.Now.AddMinutes(5)};//设置缓存有效时间为5分钟
            return this.cache.Add(cacheKey, cacheValue, policy);
        }

        /// <summary>
        /// 删除缓存
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <returns></returns>
        public virtual bool Remove(string cacheKey)
        {
            if (null == this.cache)
                return true;

            return this.cache.Remove(cacheKey) != null;
        }

        /// <summary>
        /// 更新缓存
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <param name="cacheValue"></param>
        /// <returns></returns>
        public virtual void Update(string cacheKey, object cacheValue)
        {
            if (null == this.cache)
                return;
            var policy = new CacheItemPolicy() { AbsoluteExpiration = DateTime.Now.AddMinutes(5)};//设置缓存有效时间为5分钟
            this.cache.Set(cacheKey, cacheValue, policy);
        }

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <returns></returns>
        public virtual bool Exist(string cacheKey)
        {
            if (null == this.cache)
                return false;

            return this.cache.Where(m => m.Key == cacheKey).Count() > 0;
        }

        protected virtual void Dispose(bool dispose)
        {
            if (!this.disposed)
            {
                if (dispose)
                {
                    if (null != cache)
                    {
                        cache.Dispose();
                    }
                }
            }
            this.disposed = true;
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
