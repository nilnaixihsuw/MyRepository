using System;
using System.Collections.Generic;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Infrastructure.Core.Entity;
using Mediinfo.Infrastructure.Core.DBEntity;
namespace Mediinfo.Domain.JCJG.GY
{
	 public interface IGY_GUANDAOBWDYRepository : IRepository<GY_GUANDAOBWDY>, IDependency
	{
        List<GY_GUANDAOBWDY> GetList(string guandaoid);

    }
}
