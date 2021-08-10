using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
namespace Mediinfo.Infrastructure.JCJG.GY
{
	public class GY_JIXINGDZRepository : RepositoryBase<GY_JIXINGDZ>, IGY_JIXINGDZRepository
	{
		public GY_JIXINGDZRepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}
	}
}
