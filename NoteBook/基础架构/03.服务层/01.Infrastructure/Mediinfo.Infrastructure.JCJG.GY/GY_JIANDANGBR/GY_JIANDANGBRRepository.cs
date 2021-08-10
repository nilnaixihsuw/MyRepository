using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
namespace Mediinfo.Infrastructure.JCJG.GY
{
	public class GY_JIANDANGBRRepository : RepositoryBase<GY_JIANDANGBR>, IGY_JIANDANGBRRepository
	{
		public GY_JIANDANGBRRepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}
	}
}
