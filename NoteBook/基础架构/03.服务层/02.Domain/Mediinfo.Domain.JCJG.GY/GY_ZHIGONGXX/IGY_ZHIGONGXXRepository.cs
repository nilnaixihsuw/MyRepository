using System;
using System.Collections.Generic;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Infrastructure.Core.Entity;
using Mediinfo.DTO.HIS.GY;

namespace Mediinfo.Domain.JCJG.GY
{
	 public interface IGY_ZHIGONGXXRepository : IRepository<GY_ZHIGONGXX>, IDependency
	{
        List<GY_ZHIGONGXX> GetList(string zhiGongID);

        List<GY_ZHIGONGXX> GetListByZhiGongGH(string zhiGonggh);

        string GetZhiGongxx(string zhigongid);

        List<GY_ZHIGONGXX> GetZhiGongXXS(string[] zhiGongIDS);

    }
}
