using System;
using System.Collections.Generic;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Infrastructure.Core.Entity;
using  Mediinfo.Infrastructure.Core.DBEntity;
namespace Mediinfo.Domain.JCJG.GY
{
	 public interface IGY_YONGHUYYRepository : IRepository<GY_YONGHUYY>, IDependency
	{
        GY_YONGHUYY GetByID(string yonghuid, string yingyongid);

    }
}
