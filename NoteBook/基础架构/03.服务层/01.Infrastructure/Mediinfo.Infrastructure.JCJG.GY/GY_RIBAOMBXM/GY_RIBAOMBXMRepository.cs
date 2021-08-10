using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
namespace Mediinfo.Infrastructure.JCJG.GY
{
	public class GY_RIBAOMBXMRepository : RepositoryBase<GY_RIBAOMBXM>, IGY_RIBAOMBXMRepository
	{
		public GY_RIBAOMBXMRepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}

        public List<GY_RIBAOMBXM> GetList()
        {
            return this.Set<GY_RIBAOMBXM>().ToList();
        }
    }
}
