using Microsoft.Owin;
using System;
using System.Threading.Tasks;

namespace Mediinfo.Cloud.Service.SelfHost.Starter.Middleware
{
    /// <summary>
    /// 最大请求大小
    /// </summary>
    public class RequestSizeLimitingMiddleware : OwinMiddleware
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="next"></param>
        /// <param name="maxRequestSizeInBytes"></param>
        public RequestSizeLimitingMiddleware(OwinMiddleware next, long maxRequestSizeInBytes)
            : base(next)
        {
            this.MaxRequestSizeInBytes = maxRequestSizeInBytes;
        }

        /// <summary>
        /// 最大请求大小
        /// </summary>
        public long MaxRequestSizeInBytes { get; private set; }

        /// <summary>
        /// 处理请求
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task Invoke(IOwinContext context)
        {
            IOwinRequest request = context.Request;
            
            if (request != null)
            {
                
                string[] values = null;
                if (request.Headers.TryGetValue("Content-Length", out values))
                {
                    if (Convert.ToInt64(values[0]) > MaxRequestSizeInBytes)
                    {
                        throw new InvalidOperationException(string.Format("Request size exceeds the allowed maximum size of {0} bytes", MaxRequestSizeInBytes));
                    }
                }
                if (request.Headers.TryGetValue("Transfer-Encoding", out values)
                    && values[0] == "chunked")
                {
                    RequestSizeLimitingStream wrappingStream = new RequestSizeLimitingStream(request.Body, MaxRequestSizeInBytes);

                    request.Body = wrappingStream;
                }
            }

            await Next.Invoke(context);
        }
    }
}
