using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
using Mediinfo.Infrastructure.Core.DBEntity;

namespace Mediinfo.Infrastructure.JCJG.GY
{
	public class GY_HUIZHENDANRepository : RepositoryBase<GY_HUIZHENDAN>, IGY_HUIZHENDANRepository
	{
		public GY_HUIZHENDANRepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}

        public List<GY_HUIZHENDAN> GetList(string yiZhuID)
        {
            var HuiZhenList = this.Set<GY_HUIZHENDAN>().Where(o => o.YIZHUID == yiZhuID).ToList().WithContext(this, ServiceContext);
            return HuiZhenList;
        }

    }
}
