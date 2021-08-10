using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
namespace Mediinfo.Infrastructure.JCJG.GY
{
	public class GY_JIANYANDMB1Repository : RepositoryBase<GY_JIANYANDMB1>, IGY_JIANYANDMB1Repository
	{
		public GY_JIANYANDMB1Repository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}
	}
}
