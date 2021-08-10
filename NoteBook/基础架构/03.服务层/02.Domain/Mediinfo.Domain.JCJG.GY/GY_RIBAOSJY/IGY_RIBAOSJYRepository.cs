using Mediinfo.Infrastructure.Core.Entity;
using Mediinfo.Infrastructure.Core.Repository;
using System.Collections.Generic;

namespace Mediinfo.Domain.JCJG.GY
{
    public interface IGY_RIBAOSJYRepository : IRepository<GY_RIBAOSJY>, IDependency
	{
        //List<E_MZ_RIBAOXX> GetListBySQL(string SQL);
        List<dynamic> GetDYListBySQL(string SQL);
       int UpdateBySQL(string SQL);
    }
}
