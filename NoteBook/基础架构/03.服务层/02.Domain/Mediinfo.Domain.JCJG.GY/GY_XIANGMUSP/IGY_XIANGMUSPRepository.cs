using System;
using System.Collections.Generic;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Infrastructure.Core.Entity;
using Mediinfo.Infrastructure.Core.DBEntity;
namespace Mediinfo.Domain.JCJG.GY
{
	 public interface IGY_XIANGMUSPRepository : IRepository<GY_XIANGMUSP>, IDependency
	{
        List<GY_XIANGMUSP> QueryList(string xiangMuID,string feiYongKZID,int menZhenZYBZ,DateTime shiYongRQ);
        List<GY_XIANGMUSP> QueryList(List<string> xiangMuID,int yaoPinZLBZ);

    }
}
