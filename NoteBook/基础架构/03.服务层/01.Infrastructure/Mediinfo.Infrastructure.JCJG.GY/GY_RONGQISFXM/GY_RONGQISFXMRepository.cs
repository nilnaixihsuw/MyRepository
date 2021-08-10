using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
namespace Mediinfo.Infrastructure.JCJG.GY
{
	public class GY_RONGQISFXMRepository : RepositoryBase<GY_RONGQISFXM>, IGY_RONGQISFXMRepository
	{
		public GY_RONGQISFXMRepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}
	}
}
