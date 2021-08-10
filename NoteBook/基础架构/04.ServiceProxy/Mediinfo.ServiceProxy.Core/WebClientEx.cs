using Mediinfo.Enterprise.Log;
using Mediinfo.HIS.Core;
using Mediinfo.Utility.Extensions;

using Newtonsoft.Json;

using System;
using System.IO;
using System.Net;
using System.Text;

namespace Mediinfo.ServiceProxy.Core
{
    /// <summary>
    /// WebClient扩展
    /// </summary>
    public class WebClientEx : WebClient
    {
        /// <summary>
        /// 超时时间
        /// </summary>
        public int Timeout { get; set; }

        /// <summary>
        /// 请求是否出错
        /// </summary>
        public bool IsError = false;

        /// <summary>
        /// 错误详细消息
        /// </summary>
        public InternalServerErrorMessage ErrorMessage { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="timeout"></param>
        public WebClientEx(int timeout)
        {
            Timeout = timeout;
        }

        protected HttpWebRequest httpWebRequest;

        /// <summary>  
        /// 重写GetWebRequest,添加WebRequest对象超时时间  
        /// </summary>  
        /// <param name="address"></param>  
        /// <returns></returns>  
        protected override WebRequest GetWebRequest(Uri address)
        {
            HttpWebRequest request = (HttpWebRequest)base.GetWebRequest(address);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            request.Timeout = Timeout;
            request.ReadWriteTimeout = Timeout;

            httpWebRequest = request;
            return request;
        }

        /// <summary>
        /// 重写GetWebResponse，用于处理错误消息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        protected override WebResponse GetWebResponse(WebRequest request)
        {
            HttpWebResponse res;
            try
            {
                res = (HttpWebResponse)base.GetWebResponse(request);
            }
            catch (WebException ex)
            {
                // 处理超时异常
                if (ex.Status == WebExceptionStatus.Timeout)
                {
                    SysLogEntity logEntity = new SysLogEntity
                    {
                        RiZhiID = Guid.NewGuid().ToString(),
                        ChuangJianSj = DateTime.Now.ToInvariantString("yyyy/MM/dd HH:mm:ss"),
                        RiZhiBt = "[" + HISClientHelper.USERNAME + "]在使用[" + HISClientHelper.DANGQIANCKMC + "]界面时请求服务超时！。",
                        RiZhiNr = String.Format("请求 {0} 服务超时", request.RequestUri.ToString()),
                        FuWuMc = "",
                        QingQiuLy = HISClientHelper.DANGQIANCKMC,
                        // 日志类型：1.菜单打开，2.客户端异常，3.服务调用，4服务端异常，5.SQL日志，6.性能日志
                        RiZhiLx = 2,
                        YINGYONGID = HISClientHelper.YINGYONGID,
                        XITONGID = HISClientHelper.XITONGID,
                        YINGYONGMC = HISClientHelper.YINGYONGMC,
                        YINGYONGJC = HISClientHelper.YINGYONGJC,
                        VERSION = HISClientHelper.VERSION,
                        IP = HISClientHelper.IP,
                        MAC = HISClientHelper.MAC,
                        COMPUTERNAME = HISClientHelper.COMPUTERNAME,
                        USERNAME = HISClientHelper.USERNAME,
                        USERID = HISClientHelper.USERID,
                        KESHIID = HISClientHelper.KESHIID,
                        KESHIMC = HISClientHelper.KESHIMC,
                        BINGQUID = HISClientHelper.BINGQUID,
                        BINGQUMC = HISClientHelper.BINGQUMC,
                        JIUZHENKSID = HISClientHelper.JIUZHENKSID,
                        JIUZHENKSMC = HISClientHelper.JIUZHENKSMC,
                        YUANQUID = HISClientHelper.YUANQUID,
                        GONGZUOZID = HISClientHelper.GONGZUOZID
                    };
                    LogHelper.Intance.PutSysErrorLog(logEntity);

                    throw new Exception("请求 " + request.RequestUri.ToString() + " 超时");
                }

                IsError = true;

                res = (HttpWebResponse)ex.Response;
                StreamReader sr = new StreamReader(res.GetResponseStream(), Encoding.UTF8);
                string strHtml = sr.ReadToEnd();
                sr.BaseStream.Seek(0, SeekOrigin.Begin);
                ErrorMessage = JsonConvert.DeserializeObject<InternalServerErrorMessage>(strHtml);
                if (ErrorMessage == null)
                {
                    ErrorMessage = new InternalServerErrorMessage() { Message = "服务端发生错误，没有返回任何数据！", ExceptionMessage = "可能的原因有：1.请求的服务不存在；2.路由错误导致的404；" };
                }
            }

            return res;
        }
    }
}
