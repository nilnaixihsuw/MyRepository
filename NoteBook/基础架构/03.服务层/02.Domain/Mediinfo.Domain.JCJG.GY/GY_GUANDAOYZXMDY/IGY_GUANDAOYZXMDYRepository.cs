using System;
using System.Collections.Generic;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Infrastructure.Core.Entity;
using Mediinfo.Infrastructure.Core.DBEntity;
namespace Mediinfo.Domain.JCJG.GY
{
	 public interface IGY_GUANDAOYZXMDYRepository : IRepository<GY_GUANDAOYZXMDY>, IDependency
	{
        List<GY_GUANDAOYZXMDY> GetList();
        List<GY_GUANDAOYZXMDY> GetList(string guandaoid);

    }
}
