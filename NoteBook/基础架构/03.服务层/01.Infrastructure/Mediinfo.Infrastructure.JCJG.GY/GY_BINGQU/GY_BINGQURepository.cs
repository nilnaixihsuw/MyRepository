using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
namespace Mediinfo.Infrastructure.JCJG.GY
{
	public class GY_BINGQURepository : RepositoryBase<GY_BINGQU>, IGY_BINGQURepository
	{
		public GY_BINGQURepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}

        public List<GY_BINGQU> GetBingQu(string bingQuID)
        {
            return this.Set<GY_BINGQU>().Where(o => o.ZUOFEIBZ == 0 && o.BINGQUID == bingQuID).ToList();
        }

        public string GetBingQuMC(string bingQuID)
        {
            return Set<GY_BINGQU>().FirstOrDefault(o => o.BINGQUID == bingQuID)?.BINGQUMC;
        }

        public List<GY_BINGQU> GetYouXiaoBingQu()
        {
            return this.Set<GY_BINGQU>().Where(o => o.ZUOFEIBZ == 0).ToList();
        }
    }
}
