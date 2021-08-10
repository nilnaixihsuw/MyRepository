using Microsoft.Owin;
using System;
using System.Threading.Tasks;

namespace Mediinfo.Cloud.Service.SelfHost.Starter.Middleware
{
    /// <summary>
    /// 清理中间件
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
        /// 最大次数
        /// </summary>
        public long MaxRequestTimesToClear { get; private set; }

        /// <summary>
        /// 处理事件
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task Invoke(IOwinContext context)
        {
            IOwinRequest request = context.Request;

            if (request != null)
            {
                if(Program.CurrentRequestTimes++ > MaxRequestTimesToClear)
                {
                    Program.CurrentRequestTimes = 0;
                    GC.Collect();
                }
            }

            await Next.Invoke(context);
        }
    }
}
