using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
namespace Mediinfo.Infrastructure.JCJG.GY
{
	public class GY_YAOPINCDRepository : RepositoryBase<GY_YAOPINCD>, IGY_YAOPINCDRepository
	{
		public GY_YAOPINCDRepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}

        public List<GY_YAOPINCD> GetList(string chanDiLB) {
          return  this.Set<GY_YAOPINCD>().Where(o => o.CHANDILB == chanDiLB).ToList();
        }
	}
}
