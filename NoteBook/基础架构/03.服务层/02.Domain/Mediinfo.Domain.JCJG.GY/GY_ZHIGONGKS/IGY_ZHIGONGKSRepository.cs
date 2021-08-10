using System;
using System.Collections.Generic;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Infrastructure.Core.Entity;
using  Mediinfo.Infrastructure.Core.DBEntity;
namespace Mediinfo.Domain.JCJG.GY
{
	 public interface IGY_ZHIGONGKSRepository : IRepository<GY_ZHIGONGKS>, IDependency
	{
        GY_ZHIGONGKS GetByID(string zhigongid, string keshiqbid, int biaozhi);

    }
}
