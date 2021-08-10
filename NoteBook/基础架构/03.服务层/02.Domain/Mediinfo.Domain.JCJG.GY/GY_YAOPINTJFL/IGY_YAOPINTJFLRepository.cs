using System;
using System.Collections.Generic;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Infrastructure.Core.Entity;
using  Mediinfo.Infrastructure.Core.DBEntity;
namespace Mediinfo.Domain.JCJG.GY
{
	 public interface IGY_YAOPINTJFLRepository : IRepository<GY_YAOPINTJFL>, IDependency
	{
        GY_YAOPINTJFL GetByID(string yingyongid, string tongjiflid);

    }
}
