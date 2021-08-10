using System;
using System.Collections.Generic;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Infrastructure.Core.Entity;
using Mediinfo.Infrastructure.Core.DBEntity;
namespace Mediinfo.Domain.JCJG.XT
{
    public interface IXT_XIAOXISJX_NEWRepository : IRepository<XT_XIAOXISJX_NEW>, IDependency
    {
        List<XT_XIAOXISJX_NEW> GetList(long xiaoXiID);
        List<XT_XIAOXISJX_NEW> GetAllXiaoXiSJ(List<long?> xiaoXiID, string zhiGongID);
        List<XT_XIAOXISJX_NEW> GetAllXiaoXiSJ(List<long?> xiaoXiID);
    }
}
