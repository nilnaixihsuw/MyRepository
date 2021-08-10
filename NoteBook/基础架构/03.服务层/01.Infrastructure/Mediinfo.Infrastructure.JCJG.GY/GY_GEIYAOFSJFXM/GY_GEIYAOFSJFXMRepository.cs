using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
namespace Mediinfo.Infrastructure.JCJG.GY
{
	public class GY_GEIYAOFSJFXMRepository : RepositoryBase<GY_GEIYAOFSJFXM>, IGY_GEIYAOFSJFXMRepository
	{
		public GY_GEIYAOFSJFXMRepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}
	}
}
