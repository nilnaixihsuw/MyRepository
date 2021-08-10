using System;
using System.Collections.Generic;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Infrastructure.Core.Entity;
using  Mediinfo.Infrastructure.Core.DBEntity;
namespace Mediinfo.Domain.JCJG.GY
{
	 public interface IGY_QUANXIAN2_NEWRepository : IRepository<GY_QUANXIAN2_NEW>, IDependency
	{
        GY_QUANXIAN2_NEW GetByID(string quanxianid);
    }
}
