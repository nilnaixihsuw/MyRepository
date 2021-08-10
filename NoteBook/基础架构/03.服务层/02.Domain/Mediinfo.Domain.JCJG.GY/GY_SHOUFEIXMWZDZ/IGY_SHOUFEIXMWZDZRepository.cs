using System;
using System.Collections.Generic;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Infrastructure.Core.Entity;
using  Mediinfo.Infrastructure.Core.DBEntity;
namespace Mediinfo.Domain.JCJG.GY
{
	 public interface IGY_SHOUFEIXMWZDZRepository : IRepository<GY_SHOUFEIXMWZDZ>, IDependency
	{
        List<GY_SHOUFEIXMWZDZ> GetList(string jiaGeID);

        List<GY_SHOUFEIXMWZDZ> GetList();
    }
}
