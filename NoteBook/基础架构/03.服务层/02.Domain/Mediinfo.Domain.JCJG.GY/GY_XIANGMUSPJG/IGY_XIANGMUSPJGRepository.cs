using System;
using System.Collections.Generic;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Infrastructure.Core.Entity;
using Mediinfo.Infrastructure.Core.DBEntity;
namespace Mediinfo.Domain.JCJG.GY
{
	 public interface IGY_XIANGMUSPJGRepository : IRepository<GY_XIANGMUSPJG>, IDependency
	{
        List<GY_XIANGMUSPJG> QueryList(string bingRenID, string shenPiID, DateTime shiYongRQ, int menZhenZYBZ);
        List<GY_XIANGMUSPJG> QueryList(string bingRenID, List<string> shenPiID);
    }
}
