using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
namespace Mediinfo.Infrastructure.JCJG.GY
{
	public class GY_ZIJINZHDRRepository : RepositoryBase<GY_ZIJINZHDR>, IGY_ZIJINZHDRRepository
	{
		public GY_ZIJINZHDRRepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}
	}
}
