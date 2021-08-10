using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
namespace Mediinfo.Infrastructure.JCJG.GY
{
	public class GY_JUESEBBRepository : RepositoryBase<GY_JUESEBB>, IGY_JUESEBBRepository
	{
		public GY_JUESEBBRepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}
	}
}
