using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
namespace Mediinfo.Infrastructure.JCJG.GY
{
	public class GY_CUOWURZRepository : RepositoryBase<GY_CUOWURZ>, IGY_CUOWURZRepository
	{
		public GY_CUOWURZRepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}
	}
}
