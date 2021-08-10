using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.XT;
namespace Mediinfo.Infrastructure.JCJG.XT
{
	public class XT_XIAOXIFJRepository : RepositoryBase<XT_XIAOXIFJ>, IXT_XIAOXIFJRepository
	{
		public XT_XIAOXIFJRepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}
	}
}
