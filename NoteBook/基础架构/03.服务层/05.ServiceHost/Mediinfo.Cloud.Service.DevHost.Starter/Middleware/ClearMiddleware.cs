using Microsoft.Owin;
using System;
using System.Threading.Tasks;

namespace Mediinfo.Cloud.Service.DevHost.Starter.Middleware
{
    /// <summary>
    /// GC回收中间件
    /// </summary>
    public class ClearMiddleware : OwinMiddleware
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="next"></param>
        /// <param name="maxRequestTimesToClear"></param>
        public ClearMiddleware(OwinMiddleware next, long maxRequestTimesToClear)
            : base(next)
        {
            this.MaxRequestTimesToClear = maxRequestTimesToClear;
        }

        /// <summary>
        /// 最长时间
        /// </summary>
        public long MaxRequestTimesToClear { get; private set; }

        /// <summary>
        /// 拦截处理
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task Invoke(IOwinContext context)
        {
            IOwinRequest request = context.Request;

            if (request != null)
            {
                if(MainForm.CurrentRequestTimes++ > MaxRequestTimesToClear)
                {
                    MainForm.CurrentRequestTimes = 0;
                    GC.Collect();
                }
            }

            await Next.Invoke(context);
        }
    }
}
