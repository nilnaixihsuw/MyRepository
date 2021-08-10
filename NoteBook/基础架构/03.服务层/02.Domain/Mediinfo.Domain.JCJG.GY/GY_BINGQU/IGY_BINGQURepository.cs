using System;
using System.Collections.Generic;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Infrastructure.Core.Entity;
using  Mediinfo.Infrastructure.Core.DBEntity;
namespace Mediinfo.Domain.JCJG.GY
{
	 public interface IGY_BINGQURepository : IRepository<GY_BINGQU>, IDependency
	{
        string GetBingQuMC(string bingQuID);
        List<GY_BINGQU> GetYouXiaoBingQu();

        List<GY_BINGQU> GetBingQu(string bingQuID);
    }
}
