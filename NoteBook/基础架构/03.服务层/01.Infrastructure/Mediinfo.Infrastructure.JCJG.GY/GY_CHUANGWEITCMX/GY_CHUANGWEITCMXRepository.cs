using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
namespace Mediinfo.Infrastructure.JCJG.GY
{
	public class GY_CHUANGWEITCMXRepository : RepositoryBase<GY_CHUANGWEITCMX>, IGY_CHUANGWEITCMXRepository
	{
		public GY_CHUANGWEITCMXRepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}
	}
}
