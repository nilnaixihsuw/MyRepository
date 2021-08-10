using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
namespace Mediinfo.Infrastructure.JCJG.GY
{
	public class GY_ZHANGHUXXMXRepository : RepositoryBase<GY_ZHANGHUXXMX>, IGY_ZHANGHUXXMXRepository
	{
		public GY_ZHANGHUXXMXRepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}
	}
}
