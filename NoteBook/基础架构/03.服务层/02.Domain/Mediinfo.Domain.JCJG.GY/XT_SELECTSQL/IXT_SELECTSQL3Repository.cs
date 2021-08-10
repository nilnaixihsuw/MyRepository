using System;
using System.Collections.Generic;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Infrastructure.Core.Entity;
namespace Mediinfo.Domain.JCJG.GY
{
	 public interface IXT_SELECTSQL3Repository : IRepository<XT_SELECTSQL3>, IDependency
	{
        List<XT_SELECTSQL3> GetList();
        List<XT_SELECTSQL3> GetList(List<string> sqlIDs);
    }
}
