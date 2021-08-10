using System;
using System.Collections.Generic;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Infrastructure.Core.Entity;
using  Mediinfo.Infrastructure.Core.DBEntity;
namespace Mediinfo.Domain.JCJG.GY
{
	 public interface IGY_YAOPINMCRepository : IRepository<GY_YAOPINMC>, IDependency
	{
        List<GY_YAOPINMC> GetList(string yaoPinFL);

    }
}
