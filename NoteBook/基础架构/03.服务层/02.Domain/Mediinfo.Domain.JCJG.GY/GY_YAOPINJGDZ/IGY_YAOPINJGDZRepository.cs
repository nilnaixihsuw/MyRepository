using System;
using System.Collections.Generic;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Infrastructure.Core.Entity;
using  Mediinfo.Infrastructure.Core.DBEntity;
namespace Mediinfo.Domain.JCJG.GY
{
	 public interface IGY_YAOPINJGDZRepository : IRepository<GY_YAOPINJGDZ>, IDependency
	{
        void SetCache(); 
        void UpdateCache(GY_YAOPINJGDZ yaoPinJGDZ);

        List<GY_YAOPINJGDZ> GetListByJiaGeID2(string jiaGeID2, decimal jiaGe2, int jiaGeLX);
        List<GY_YAOPINJGDZ> GetListByJiaGeID1(string jiaGeID1, decimal jiaGe1, int jiaGeLX);

        List<GY_YAOPINJGDZ> GetListByJiaGeID1FromCache(string jiaGeID1, decimal jiaGe1, int jiaGeLX);

       List<GY_YAOPINJGDZ> GetList(string jiaGeID1, string jiaGeID2, decimal jiaGe2, int jiaGeLX);

        List<GY_YAOPINJGDZ> GetListFromCache(string jiaGeID1, string jiaGeID2, decimal jiaGe2, int jiaGeLX);

    }
}
