using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.HIS.GY;
namespace Mediinfo.Infrastructure.HIS.GY
{
	public class GY_ZIJINZHDRRepository : RepositoryBase<GY_ZIJINZHDR>, IGY_ZIJINZHDRRepository
	{
		public GY_ZIJINZHDRRepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}
	}
}
