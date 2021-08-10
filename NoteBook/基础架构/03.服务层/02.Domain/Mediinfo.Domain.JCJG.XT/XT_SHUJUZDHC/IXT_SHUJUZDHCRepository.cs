using System;
using System.Collections.Generic;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Infrastructure.Core.Entity;
using Mediinfo.Infrastructure.Core.DBEntity;
namespace Mediinfo.Domain.JCJG.XT
{
    public interface IXT_SHUJUZDHCRepository : IRepository<XT_SHUJUZDHC>, IDependency
    {
        List<XT_SHUJUZDHC> GetList(string yingYongId);
    }
}
