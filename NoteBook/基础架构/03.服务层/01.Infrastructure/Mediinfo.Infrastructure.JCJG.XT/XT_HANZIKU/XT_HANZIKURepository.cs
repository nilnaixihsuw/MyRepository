using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.XT;
namespace Mediinfo.Infrastructure.JCJG.XT
{
	public class XT_HANZIKURepository : RepositoryBase<XT_HANZIKU>, IXT_HANZIKURepository
	{
		public XT_HANZIKURepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}
	}
}
