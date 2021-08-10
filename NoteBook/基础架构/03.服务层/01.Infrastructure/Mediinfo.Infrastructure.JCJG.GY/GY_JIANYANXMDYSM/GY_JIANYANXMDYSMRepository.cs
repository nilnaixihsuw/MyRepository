using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
namespace Mediinfo.Infrastructure.JCJG.GY
{
	public class GY_JIANYANXMDYSMRepository : RepositoryBase<GY_JIANYANXMDYSM>, IGY_JIANYANXMDYSMRepository
	{
		public GY_JIANYANXMDYSMRepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}
	}
}
