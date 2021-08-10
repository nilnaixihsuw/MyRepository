using System;
using System.Collections.Generic;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Infrastructure.Core.Entity;
using Mediinfo.Infrastructure.Core.DBEntity;
namespace Mediinfo.Domain.JCJG.XT
{
    public interface IXT_XIAOXI_NEWRepository : IRepository<XT_XIAOXI_NEW>, IDependency
    {
        List<XT_XIAOXI_NEW> GetWeiGuoQi(DateTime faSongSJ);
        List<XT_XIAOXI_NEW> GetXiaoXiByID(List<long?> xiaoXiIDList);
        XT_XIAOXI_NEW GetXiaoXiByXXBMAndXXLY(string xiaoxiBM, string xiaoxiLY);
    }
}
