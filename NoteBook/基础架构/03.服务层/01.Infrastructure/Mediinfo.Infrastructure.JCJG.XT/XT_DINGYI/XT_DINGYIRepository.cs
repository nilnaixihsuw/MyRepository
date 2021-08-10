using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.XT;
namespace Mediinfo.Infrastructure.JCJG.XT
{
	public class XT_DINGYIRepository : RepositoryBase<XT_DINGYI>, IXT_DINGYIRepository
	{
		public XT_DINGYIRepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}
	}
}
