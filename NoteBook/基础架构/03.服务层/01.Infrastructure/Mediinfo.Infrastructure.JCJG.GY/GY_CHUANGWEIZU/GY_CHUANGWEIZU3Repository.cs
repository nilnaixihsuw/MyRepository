using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
namespace Mediinfo.Infrastructure.JCJG.GY
{
	public class GY_CHUANGWEIZU3Repository : RepositoryBase<GY_CHUANGWEIZU3>, IGY_CHUANGWEIZU3Repository
	{
		public GY_CHUANGWEIZU3Repository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}
	}
}
