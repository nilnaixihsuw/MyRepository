using Mediinfo.Domain.JCJG.GY;
using Mediinfo.DTO.HIS.GY;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Domain;
using Mediinfo.Service.JCJG.GongYong.Route;

using System.Collections.Generic;
using System.Web.Http;

namespace Mediinfo.Service.JCJG.GongYong
{
    [ServiceRoutePrefix]
    [Route("JCJGGongZuoTDY/{action}")]
    public class JCJGGongZuoTDYController : Mediinfo.Infrastructure.JCJG.Controller.HISController
    {
        [HttpPost]
        public ServiceResult<List<string>> Get()
        {
            return ServiceContent(new List<string>());
        }

        [HttpPost]
        public ServiceResult<List<E_GY_GONGZUOTDY>> GetList()
        {
            IGY_GONGZUOTDYRepository gongzuotdyRep = this.GetRepository<IGY_GONGZUOTDYRepository>();
            var list = gongzuotdyRep.GetList();
            var result = list.DBToE<GY_GONGZUOTDY, E_GY_GONGZUOTDY>();
            return ServiceContent(result);
        }
    }
}
