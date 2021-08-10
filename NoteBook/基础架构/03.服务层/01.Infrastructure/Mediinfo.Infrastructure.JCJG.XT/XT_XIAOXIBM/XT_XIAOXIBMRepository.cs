using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.XT;
namespace Mediinfo.Infrastructure.JCJG.XT
{
	public class XT_XIAOXIBMRepository : RepositoryBase<XT_XIAOXIBM>, IXT_XIAOXIBMRepository
	{
		public XT_XIAOXIBMRepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}
    }
}
