using System;
using System.Collections.Generic;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Infrastructure.Core.Entity;
using  Mediinfo.Infrastructure.Core.DBEntity;
namespace Mediinfo.Domain.JCJG.GY
{
	 public interface IGY_XUNIYFDYRepository : IRepository<GY_XUNIYFDY>, IDependency
	{
        List<GY_XUNIYFDY> GetList(string yingYongID);

        List<GY_YINGYONG> GetYingYongMC(string jiaGeID, int kuCunSL, string yingYongID, string yingYongID1);

    }
}
