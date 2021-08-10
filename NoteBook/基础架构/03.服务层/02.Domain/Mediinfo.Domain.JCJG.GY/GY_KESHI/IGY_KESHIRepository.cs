using System;
using System.Collections.Generic;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Infrastructure.Core.Entity;
using  Mediinfo.Infrastructure.Core.DBEntity;
namespace Mediinfo.Domain.JCJG.GY
{
	 public interface IGY_KESHIRepository : IRepository<GY_KESHI>, IDependency
	 {
	     string GetShangJiYWKS(string jiuZhenKS);

	     List<string> GetKeShiID(string s_ShangJiYWKS);

	     string GetKeShiMC(string keShiID);

         List<GY_KESHI> GetKeShiXX(string zuoFeiBZ);

        List<GY_KESHI> GetYouXiaoKS();

        List<GY_KESHI> GetKeShi(string keShiID);

        List<GY_KESHI> GetKeShiS(string[] keShiIDS);
        List<GY_KESHI> QueryKeShi(string keShiID);
        List<string> GetZhuYuanKS();
    }
}
