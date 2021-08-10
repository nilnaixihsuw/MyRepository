using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
using Mediinfo.Infrastructure.Core.DBEntity;

namespace Mediinfo.Infrastructure.JCJG.GY
{
	public class GY_ZHIGONGKSRepository : RepositoryBase<GY_ZHIGONGKS>, IGY_ZHIGONGKSRepository
	{
		public GY_ZHIGONGKSRepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}

        public GY_ZHIGONGKS GetByID(string zhigongid, string keshiqbid,int biaozhi)
        {
            var dto = (from zgks in this.Set<GY_ZHIGONGKS>()
                       where zgks.ZHIGONGID == zhigongid && zgks.KESHIBQID == keshiqbid && zgks.KESHIBQBZ == biaozhi
                       select zgks).FirstOrDefault().WithContext(this, ServiceContext);
            return dto;
        }
    }
}
