using System;
using System.Collections.Generic;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Infrastructure.Core.Entity;
using  Mediinfo.Infrastructure.Core.DBEntity;
namespace Mediinfo.Domain.JCJG.GY
{
	 public interface IGY_CAIDAN_NEWRepository : IRepository<GY_CAIDAN_NEW>, IDependency
	{
        List<GY_CAIDAN_NEW> GetList(string caiDanID);
    }
}
