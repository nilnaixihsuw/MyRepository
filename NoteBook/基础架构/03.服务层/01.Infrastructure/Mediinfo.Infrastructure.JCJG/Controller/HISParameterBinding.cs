using Mediinfo.DTO.Core;
using Mediinfo.Infrastructure.Core;
using Mediinfo.Utility;
using Mediinfo.Utility.Compress;
using Mediinfo.Utility.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Metadata;

namespace Mediinfo.Infrastructure.JCJG.Controller
{
    /// <summary>
    /// Json参数转换
    /// </summary>
    public class HISParameterBinding : HttpParameterBinding
    {
        HttpParameterDescriptor _parameter;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="parameter"></param>
        public HISParameterBinding(HttpParameterDescriptor parameter)
        : base(parameter)
        {
            _parameter = parameter;
        }
        /// <summary>
        /// 重写绑定方法
        /// </summary>
        /// <param name="metadataProvider"></param>
        /// <param name="actionContext"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override Task ExecuteBindingAsync(ModelMetadataProvider metadataProvider, HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            var pName = _parameter.ParameterName;
            var pType = _parameter.ParameterType;

            try
            {
                //获取参数
                var data = QueryUrl.GetData(actionContext.Request.Content.ReadAsStringAsync().Result);

                if (data.ContainsKey(pName))
                {
                    string value = data[pName];

                    // 压缩模式
                    var postEncoding = actionContext.Request.Headers.Where(n => n.Key == "post-encoding").FirstOrDefault().Value;
                    string compressionMode = "GZIP";
                    if (postEncoding != null)
                        compressionMode = postEncoding.FirstOrDefault();
                    if (compressionMode.ToUpper() == "GZIP")
                        value = value.DecompressString();

                    //设置dto状态跟踪
                    var traceChange = actionContext.Request.Headers.Where(n => n.Key == "TraceChange").FirstOrDefault().Value;

                    if (traceChange != null && traceChange.FirstOrDefault().ToUpper() == "FALSE" && pType.IsClass)
                    {
                        actionContext.ActionArguments.Add(pName, JsonUtil.DeserializeToObject(value, pType, new DTOJsonConverter()));
                    }
                    else
                    {
                        actionContext.ActionArguments.Add(pName, JsonUtil.DeserializeToObject(value, pType));
                    }
                }
                else
                {
                    if (!_parameter.IsOptional)
                    {
                        string exceptionContext = "参数未传递。请求地址：" + actionContext.Request.RequestUri.ToString() + "，参数名：" + pName + ",参数类型：" + pType;
                        Enterprise.Log.LogHelper.Intance.Error("系统日志", "服务入参解析错误", exceptionContext);
                    }

                    actionContext.ActionArguments.Add(pName, _parameter.DefaultValue);
                }

            }
            catch (Exception ex)
            {
                Enterprise.Log.LogHelper.Intance.Error("系统日志", "服务入参解析错误", actionContext.Request.RequestUri.ToString() + "服务入参解析错误！参数名：" + pName + ",参数类型：" + pType + "。详细信息：" + ex.Message);
                //throw new Exception(actionContext.Request.RequestUri.ToString() + "服务入参解析错误！参数名：" + pName + ",参数类型：" + pType + "。详细信息：" +ex.Message);
            }

            var tsc = new TaskCompletionSource<object>();
            tsc.SetResult(null);
            return tsc.Task;

        }
    }
}
