using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
namespace Mediinfo.Infrastructure.JCJG.GY
{
	public class GY_JUESERepository : RepositoryBase<GY_JUESE>, IGY_JUESERepository
	{
		public GY_JUESERepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}
	}
}
