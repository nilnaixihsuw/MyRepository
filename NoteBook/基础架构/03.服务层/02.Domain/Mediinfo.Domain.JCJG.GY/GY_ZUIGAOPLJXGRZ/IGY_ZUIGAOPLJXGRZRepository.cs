using System;
using System.Collections.Generic;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Infrastructure.Core.Entity;
using  Mediinfo.Infrastructure.Core.DBEntity;
namespace Mediinfo.Domain.JCJG.GY
{
	 public interface IGY_ZUIGAOPLJXGRZRepository : IRepository<GY_ZUIGAOPLJXGRZ>, IDependency
	{
        GY_ZUIGAOPLJXGRZ GetByID(string yingyongid, string jiageid, DateTime xiugaisj);

    }
}
