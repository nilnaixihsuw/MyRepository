using System;
using System.Collections.Generic;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Infrastructure.Core.Entity;
using  Mediinfo.Infrastructure.Core.DBEntity;
namespace Mediinfo.Domain.JCJG.GY
{
	 public interface IGY_DUOJICTLXXMFLDZRepository : IRepository<GY_DUOJICTLXXMFLDZ>, IDependency
	{
        List<GY_DUOJICTLXXMFLDZ> GetList(string xiangMuID, string xiangMuLX);
        List<GY_DUOJICTLXXMFLDZ> GetList(string fenLeiID, decimal? shouFeiJC);
        List<dynamic> GetShuLList(string bingRenZYID, decimal? shouFeiJC);
    }
   
}
