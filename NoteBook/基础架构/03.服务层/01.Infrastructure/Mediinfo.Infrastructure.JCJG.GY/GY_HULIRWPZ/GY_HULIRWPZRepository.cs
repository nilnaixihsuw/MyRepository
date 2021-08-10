using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
namespace Mediinfo.Infrastructure.JCJG.GY
{
	public class GY_HULIRWPZRepository : RepositoryBase<GY_HULIRWPZ>, IGY_HULIRWPZRepository
	{
		public GY_HULIRWPZRepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}


    }
}
