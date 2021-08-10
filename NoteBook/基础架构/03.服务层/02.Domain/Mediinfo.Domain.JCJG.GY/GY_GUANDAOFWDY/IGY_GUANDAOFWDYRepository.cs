using System;
using System.Collections.Generic;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Infrastructure.Core.Entity;
using Mediinfo.Infrastructure.Core.DBEntity;
namespace Mediinfo.Domain.JCJG.GY
{
	 public interface IGY_GUANDAOFWDYRepository : IRepository<GY_GUANDAOFWDY>, IDependency
	{
        List<GY_GUANDAOFWDY> GetList(string guandaoid);

    }
}
