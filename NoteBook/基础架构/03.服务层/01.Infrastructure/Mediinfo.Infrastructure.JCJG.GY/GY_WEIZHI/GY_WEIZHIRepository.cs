using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
namespace Mediinfo.Infrastructure.JCJG.GY
{
	public class GY_WEIZHIRepository : RepositoryBase<GY_WEIZHI>, IGY_WEIZHIRepository
	{
		public GY_WEIZHIRepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}
	}
}
