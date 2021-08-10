using System;
using System.Collections.Generic;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Infrastructure.Core.Entity;
using  Mediinfo.Infrastructure.Core.DBEntity;
namespace Mediinfo.Domain.JCJG.GY
{
	 public interface IGY_ZHONGYAOPFKLZDRepository : IRepository<GY_ZHONGYAOPFKLZD>, IDependency
	{
        List<GY_ZHONGYAOPFKLZD> GetList(string ZHONGYAOPFKLID);
    }
}
