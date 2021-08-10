using System;
using System.Collections.Generic;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Infrastructure.Core.Entity;
using  Mediinfo.Infrastructure.Core.DBEntity;
namespace Mediinfo.Domain.JCJG.GY
{
	 public interface IGY_ZIJINZHRepository : IRepository<GY_ZIJINZH>, IDependency
	{
        GY_ZIJINZH GetZiJinZH(string bingRenID,string jieZhiHao);
        decimal? GetQiMoJE(string bingRenID);
    }
}
