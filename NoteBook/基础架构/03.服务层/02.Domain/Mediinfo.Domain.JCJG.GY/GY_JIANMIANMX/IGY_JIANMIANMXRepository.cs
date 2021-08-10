using System;
using System.Collections.Generic;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Infrastructure.Core.Entity;
using Mediinfo.Infrastructure.Core.DBEntity;
namespace Mediinfo.Domain.JCJG.GY
{
	 public interface IGY_JIANMIANMXRepository : IRepository<GY_JIANMIANMX>, IDependency
	{
        List<GY_JIANMIANMX> GetList(string jieSuanID);

    }
}
