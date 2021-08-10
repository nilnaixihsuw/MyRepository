using Mediinfo.Infrastructure.Core;

using System.IO;
using System.IO.Compression;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Mediinfo.Cloud.Service.DevHost.Starter.MessageHandlers
{
    /// <summary>
    /// 压缩处理器
    /// </summary>
    public class DecompressionHandler : DelegatingHandler
    {
        /// <summary>
        /// 拦截发送数据，并压缩
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request.Method == HttpMethod.Post)
            {
                Stream decompressedStream = new MemoryStream();

                using (var gzipStream = new GZipStream(await request.Content.ReadAsStreamAsync(), CompressionMode.Decompress))
                {
                    await gzipStream.CopyToAsync(decompressedStream);
                }
                decompressedStream.Seek(0, SeekOrigin.Begin);
                var originContent = request.Content;
                request.Content = new StreamContent(decompressedStream);
                foreach (var header in originContent.Headers)
                {
                    request.Content.Headers.Add(header.Key, header.Value);
                }
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
