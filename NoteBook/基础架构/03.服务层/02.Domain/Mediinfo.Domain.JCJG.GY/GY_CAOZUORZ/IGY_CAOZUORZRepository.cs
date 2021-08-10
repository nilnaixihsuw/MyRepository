using System;
using System.Collections.Generic;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Infrastructure.Core.Entity;
using  Mediinfo.Infrastructure.Core.DBEntity;
namespace Mediinfo.Domain.JCJG.GY
{
	 public interface IGY_CAOZUORZRepository : IRepository<GY_CAOZUORZ>, IDependency
	{
        GY_CAOZUORZ GetLoginLog();
        GY_CAOZUORZ GetByID(string ip, string yingyongid);
    }
}
