using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
namespace Mediinfo.Infrastructure.JCJG.GY
{
	public class GY_YONGHUPFXXRepository : RepositoryBase<GY_YONGHUPFXX>, IGY_YONGHUPFXXRepository
	{
		public GY_YONGHUPFXXRepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}
	}
}
