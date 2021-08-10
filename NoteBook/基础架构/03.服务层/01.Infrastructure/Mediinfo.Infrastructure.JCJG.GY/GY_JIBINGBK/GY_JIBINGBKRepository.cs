using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
namespace Mediinfo.Infrastructure.JCJG.GY
{
	public class GY_JIBINGBKRepository : RepositoryBase<GY_JIBINGBK>, IGY_JIBINGBKRepository
	{
		public GY_JIBINGBKRepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}
	}
}
