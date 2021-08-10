using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.XT;
using Mediinfo.Infrastructure.Core.DBEntity;

namespace Mediinfo.Infrastructure.JCJG.XT
{
	public class XT_ZAIXIANZTRepository : RepositoryBase<XT_ZAIXIANZT>, IXT_ZAIXIANZTRepository
	{
		public XT_ZAIXIANZTRepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}

        public List<XT_ZAIXIANZT> QueryZaiXianYh()
        {
            return this.QuerySet<XT_ZAIXIANZT>().Where(m => m.JIESHUSJ > DateTime.Now).ToList();
        }

        public XT_ZAIXIANZT GetZaiXianZT(string zhiGongID)
        {
            return this.Set<XT_ZAIXIANZT>().Where(m => m.ZHIGONGID == zhiGongID).OrderByDescending(m=>m.JIESHUSJ).FirstOrDefault();
        }
    }
}
