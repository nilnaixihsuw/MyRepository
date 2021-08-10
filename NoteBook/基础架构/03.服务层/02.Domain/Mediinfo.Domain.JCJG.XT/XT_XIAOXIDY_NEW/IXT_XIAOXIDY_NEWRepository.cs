using System;
using System.Collections.Generic;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Infrastructure.Core.Entity;
using Mediinfo.Infrastructure.Core.DBEntity;
namespace Mediinfo.Domain.JCJG.XT
{
    public interface IXT_XIAOXIDY_NEWRepository : IRepository<XT_XIAOXIDY_NEW>, IDependency
    {
        List<XT_XIAOXIDY_NEW> QueryDingYueList(string xiaoXiBM);

        List<XT_XIAOXIDY_NEW> QueryDingYueLists(List<string> xiaoXiBM);
    }
}
