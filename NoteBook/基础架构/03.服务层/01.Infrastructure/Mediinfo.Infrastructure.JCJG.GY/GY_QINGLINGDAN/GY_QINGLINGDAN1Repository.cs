using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
namespace Mediinfo.Infrastructure.JCJG.GY
{
	public class GY_QINGLINGDAN1Repository : RepositoryBase<GY_QINGLINGDAN1>, IGY_QINGLINGDAN1Repository
	{
		public GY_QINGLINGDAN1Repository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}
	}
}
