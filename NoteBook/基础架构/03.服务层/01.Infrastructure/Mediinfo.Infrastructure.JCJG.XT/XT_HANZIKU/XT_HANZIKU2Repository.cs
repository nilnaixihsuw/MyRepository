using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.XT;
namespace Mediinfo.Infrastructure.JCJG.XT
{
	public class XT_HANZIKU2Repository : RepositoryBase<XT_HANZIKU2>, IXT_HANZIKU2Repository
	{
		public XT_HANZIKU2Repository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}
	}
}
