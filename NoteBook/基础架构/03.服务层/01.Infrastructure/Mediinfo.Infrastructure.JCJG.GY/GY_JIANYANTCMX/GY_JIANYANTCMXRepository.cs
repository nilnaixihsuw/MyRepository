using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
namespace Mediinfo.Infrastructure.JCJG.GY
{
	public class GY_JIANYANTCMXRepository : RepositoryBase<GY_JIANYANTCMX>, IGY_JIANYANTCMXRepository
	{
		public GY_JIANYANTCMXRepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}
	}
}
