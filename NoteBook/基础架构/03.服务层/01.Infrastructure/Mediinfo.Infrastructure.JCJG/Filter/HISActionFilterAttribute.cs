using Mediinfo.Enterprise;
using Mediinfo.Enterprise.Exceptions;
using Mediinfo.Utility.Compress;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Mediinfo.Infrastructure.JCJG.Filter
{
    /// <summary>
    /// HIS的token验证
    /// </summary>
    public class HISActionFilterAttribute: ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext filterContext)
        {
            var tokens = filterContext.Request.Headers.Where(n => n.Key == "token");
            string token = null;
            if (tokens.Count() > 0)
            {
                token = tokens.FirstOrDefault().Value.FirstOrDefault();
            }

            // 如果token是空的，则验证当前的url是否运行免验证（一般是登录之前的服务）
            if ( string.IsNullOrEmpty(token))
            {
                string pathAndQuery = filterContext.ControllerContext.Request.RequestUri.PathAndQuery;
                string moKuaiMc = pathAndQuery.Split('/')[1];
                string yeWuMc = pathAndQuery.Split('/')[2];
                string caoZuoMc = pathAndQuery.Split('/')[3];
                // 如果访问的不是登录业务
                if(moKuaiMc != "JCJG-GongYong")
                {
                    throw new UnauthorizedException("请求的服务：" + moKuaiMc + "/" + yeWuMc + "/" + caoZuoMc + " 需求Token授权！");
                }

            }

            // 获取header中的context
            var contexts = filterContext.Request.Headers.Where(n => n.Key == "context");
            string context = null;
            if (contexts.Count() > 0)
            {
                context = contexts.FirstOrDefault().Value.FirstOrDefault();
            }

            // 设置ServiceContext
            if (context != null)
            {
                var controller = filterContext.ControllerContext.Controller;
                var serviceContextProperty = controller.GetType().BaseType.GetProperty("ServiceContext", System.Reflection.BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
                serviceContextProperty.SetValue(controller, context.Decompress<ServiceContext>());
            }

            base.OnActionExecuting(filterContext);
        }
    }
}



