using System;
using System.Collections.Generic;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Infrastructure.Core.Entity;
namespace Mediinfo.Domain.JCJG.GY
{
	 public interface IGY_QUANXIANRepository : IRepository<GY_QUANXIAN>, IDependency
	{
        //  List<GY_QUANXIAN> GetByID(string quanxianid);
        GY_QUANXIAN GetByID(string quanxianid);
    }
}
