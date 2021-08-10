using System;
using System.Collections.Generic;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Infrastructure.Core.Entity;
using  Mediinfo.Infrastructure.Core.DBEntity;
namespace Mediinfo.Domain.JCJG.GY
{
	 public interface IGY_SHOUFEIXMZXKSRepository : IRepository<GY_SHOUFEIXMZXKS>, IDependency
	{
        List<GY_SHOUFEIXMZXKS> GetList(string shouFeiXMID);
        GY_SHOUFEIXMZXKS Get(string shouFeiXMID);
    }
}
