using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
namespace Mediinfo.Infrastructure.JCJG.GY
{
	public class GY_ZHANGHULBRepository : RepositoryBase<GY_ZHANGHULB>, IGY_ZHANGHULBRepository
	{
		public GY_ZHANGHULBRepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}

        public List<GY_ZHANGHULB> getQiYongZH()
        {
            var List = this.Set<GY_ZHANGHULB>().Where(w => w.QIYONGBZ == 1).ToList();
            return List;
        }
    }
}
