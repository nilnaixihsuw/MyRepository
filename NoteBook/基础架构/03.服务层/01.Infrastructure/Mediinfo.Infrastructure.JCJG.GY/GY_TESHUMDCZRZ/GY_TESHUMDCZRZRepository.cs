using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
namespace Mediinfo.Infrastructure.JCJG.GY
{
	public class GY_TESHUMDCZRZRepository : RepositoryBase<GY_TESHUMDCZRZ>, IGY_TESHUMDCZRZRepository
	{
		public GY_TESHUMDCZRZRepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}
	}
}
