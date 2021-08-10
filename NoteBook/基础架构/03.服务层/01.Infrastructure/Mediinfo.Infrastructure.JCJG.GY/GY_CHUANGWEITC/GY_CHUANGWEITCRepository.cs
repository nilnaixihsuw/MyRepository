using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
namespace Mediinfo.Infrastructure.JCJG.GY
{
	public class GY_CHUANGWEITCRepository : RepositoryBase<GY_CHUANGWEITC>, IGY_CHUANGWEITCRepository
	{
		public GY_CHUANGWEITCRepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}
	}
}
