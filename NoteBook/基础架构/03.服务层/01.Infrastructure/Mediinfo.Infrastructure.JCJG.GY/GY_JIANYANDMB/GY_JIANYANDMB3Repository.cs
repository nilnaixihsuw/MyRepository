using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
namespace Mediinfo.Infrastructure.JCJG.GY
{
	public class GY_JIANYANDMB3Repository : RepositoryBase<GY_JIANYANDMB3>, IGY_JIANYANDMB3Repository
	{
		public GY_JIANYANDMB3Repository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}
	}
}
