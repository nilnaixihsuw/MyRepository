using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
namespace Mediinfo.Infrastructure.JCJG.GY
{
	public class GY_JIBINGRepository : RepositoryBase<GY_JIBING>, IGY_JIBINGRepository
	{
		public GY_JIBINGRepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}
	}
}
