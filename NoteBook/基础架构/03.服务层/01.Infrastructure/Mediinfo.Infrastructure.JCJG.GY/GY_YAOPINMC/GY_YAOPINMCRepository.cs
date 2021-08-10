using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
namespace Mediinfo.Infrastructure.JCJG.GY
{
	public class GY_YAOPINMCRepository : RepositoryBase<GY_YAOPINMC>, IGY_YAOPINMCRepository
	{
		public GY_YAOPINMCRepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}

        public List<GY_YAOPINMC> GetList(string yaoPinFL)
        {
            var list = this.Set<GY_YAOPINMC>().Where(o => o.YAOPINFL == yaoPinFL).ToList();
            return list;
        }
    }
}
 