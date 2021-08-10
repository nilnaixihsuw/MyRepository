using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
namespace Mediinfo.Infrastructure.JCJG.GY
{
	public class GY_CHUANGWEIZU1Repository : RepositoryBase<GY_CHUANGWEIZU1>, IGY_CHUANGWEIZU1Repository
	{
		public GY_CHUANGWEIZU1Repository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}
	}
}
