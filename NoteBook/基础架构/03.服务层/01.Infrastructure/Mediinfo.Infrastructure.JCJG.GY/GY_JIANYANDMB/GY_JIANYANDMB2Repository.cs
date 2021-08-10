using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
namespace Mediinfo.Infrastructure.JCJG.GY
{
	public class GY_JIANYANDMB2Repository : RepositoryBase<GY_JIANYANDMB2>, IGY_JIANYANDMB2Repository
	{
		public GY_JIANYANDMB2Repository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}
	}
}
