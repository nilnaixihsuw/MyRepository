using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.XT;
namespace Mediinfo.Infrastructure.JCJG.XT
{
	public class XT_XIAOXIDY_NEWRepository : RepositoryBase<XT_XIAOXIDY_NEW>, IXT_XIAOXIDY_NEWRepository
	{
		public XT_XIAOXIDY_NEWRepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}

        public List<XT_XIAOXIDY_NEW> QueryDingYueList(string xiaoXiBM)
        {
            return this.QuerySet<XT_XIAOXIDY_NEW>().Where(m => m.XIAOXIBM == xiaoXiBM).ToList();
        }

        public List<XT_XIAOXIDY_NEW> QueryDingYueLists(List<string> xiaoXiBM)
        {
            return this.QuerySet<XT_XIAOXIDY_NEW>().Where(m => xiaoXiBM.Contains(m.XIAOXIBM)).ToList();
        }
    }
}
