using Mediinfo.Domain.JCJG.GY;
using Mediinfo.Enterprise;
using Mediinfo.Service.JCJG.GongYong.Route;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Mediinfo.Service.JCJG.GongYong
{
    [ServiceRoutePrefix]
    [Route("JCJGBuJuQX/{action}")]
    public class JCJGBuJuQXController : Mediinfo.Infrastructure.JCJG.Controller.HISController
    {
        /// <summary>
        /// 判断是否可以获取布局权限
        /// </summary>
        /// <param name="yonghuID"></param>
        /// <returns></returns>
        [HttpPost]
        [HttpGet]
        public ServiceResult<int> IsBuJuQX(string yonghuID)
        {
            var jueseqxRep = this.GetRepository<IGY_BUJUQXRepository>(UnitOfWork);
            var listNum= jueseqxRep.GetBuJuQX(yonghuID);
            return ServiceContent(listNum);
        }
    }
}
