using System;
using System.Collections.Generic;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Infrastructure.Core.Entity;
using  Mediinfo.Infrastructure.Core.DBEntity;
namespace Mediinfo.Domain.JCJG.GY
{
	 public interface IGY_CAIDANGJL_NEWRepository : IRepository<GY_CAIDANGJL_NEW>, IDependency
	{
        GY_CAIDANGJL_NEW GetByID(string yingyongid, string gongnengid, string caidanid);

    }
}
