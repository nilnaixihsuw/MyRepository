using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
namespace Mediinfo.Infrastructure.JCJG.GY
{
	public class GY_YILIAOZU1Repository : RepositoryBase<GY_YILIAOZU1>, IGY_YILIAOZU1Repository
	{
		public GY_YILIAOZU1Repository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}
	}
}
