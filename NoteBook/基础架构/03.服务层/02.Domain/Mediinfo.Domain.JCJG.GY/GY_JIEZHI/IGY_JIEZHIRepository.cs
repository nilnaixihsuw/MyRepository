using System;
using System.Collections.Generic;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Infrastructure.Core.Entity;
using  Mediinfo.Infrastructure.Core.DBEntity;
namespace Mediinfo.Domain.JCJG.GY
{
	 public interface IGY_JIEZHIRepository : IRepository<GY_JIEZHI>, IDependency
	{ 
        GY_JIEZHI GetJieZhiXX(string bingRenID,int zuoFeiBZ);
        GY_JIEZHI GetJieZhiXX(string bingRenID, string jieZhiHao);
        GY_JIEZHI GetJieZhiXX(string jieZhiHao);
        GY_JIEZHI GetJieZhiXXWithBRIDISJZH(string bingRenID);

       List<GY_JIEZHI> GetHeBingXX(string bingRenID);

    }
}
