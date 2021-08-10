using System;
using System.Collections.Generic;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Infrastructure.Core.Entity;
using  Mediinfo.Infrastructure.Core.DBEntity;
namespace Mediinfo.Domain.JCJG.GY
{
	 public interface IGY_YILIAOZU4Repository : IRepository<GY_YILIAOZU4>, IDependency
	{
        GY_YILIAOZU4 GetByID(string yiliaozid, string keshiid);

    }
}
