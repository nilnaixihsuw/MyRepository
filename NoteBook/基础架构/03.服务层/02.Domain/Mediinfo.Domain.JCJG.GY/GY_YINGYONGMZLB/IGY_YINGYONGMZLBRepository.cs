using System;
using System.Collections.Generic;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Infrastructure.Core.Entity;
using Mediinfo.Infrastructure.Core.DBEntity;
namespace Mediinfo.Domain.JCJG.GY
{
	 public interface IGY_YINGYONGMZLBRepository : IRepository<GY_YINGYONGMZLB>, IDependency
	 {
	     int GetShiFouWSDDPB(string S_YINGYONGMZLBID);

	     void Get(string S_YINGYONGMZLBID, int shangXiaWBZ, out string S_GUAHAOLB, out string S_GUAHAOSFXM, out string S_ZHENLIAOSFXM);


	 }
}
