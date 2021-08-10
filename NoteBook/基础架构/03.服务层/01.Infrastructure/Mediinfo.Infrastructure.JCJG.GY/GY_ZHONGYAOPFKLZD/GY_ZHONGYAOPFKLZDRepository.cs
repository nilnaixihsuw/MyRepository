using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
using Mediinfo.Infrastructure.Core.DBEntity;

namespace Mediinfo.Infrastructure.JCJG.GY
{
	public class GY_ZHONGYAOPFKLZDRepository : RepositoryBase<GY_ZHONGYAOPFKLZD>, IGY_ZHONGYAOPFKLZDRepository
	{
		public GY_ZHONGYAOPFKLZDRepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}

        public List<GY_ZHONGYAOPFKLZD> GetList(string ZHONGYAOPFKLID)
        {
            var list = this.Set<GY_ZHONGYAOPFKLZD>().Where(o => o.ZHONGYAOPFKLID == ZHONGYAOPFKLID).ToList().WithContext(this, ServiceContext);
            return list;
        }
    }
}
