using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.XT;
namespace Mediinfo.Infrastructure.JCJG.XT
{
	public class XT_SHUJUZDHCRepository : RepositoryBase<XT_SHUJUZDHC>, IXT_SHUJUZDHCRepository
	{
		public XT_SHUJUZDHCRepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}

        public List<XT_SHUJUZDHC> GetList(string yingYongId)
        {
            return this.Set<XT_SHUJUZDHC>().Where(m => m.YINGYONGID == yingYongId).ToList();
        }
    }
}
