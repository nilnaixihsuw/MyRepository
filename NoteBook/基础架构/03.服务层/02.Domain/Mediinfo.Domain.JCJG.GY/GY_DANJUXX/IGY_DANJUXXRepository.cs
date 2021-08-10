using System;
using System.Collections.Generic;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Infrastructure.Core.Entity;
using  Mediinfo.Infrastructure.Core.DBEntity;
namespace Mediinfo.Domain.JCJG.GY
{
	 public interface IGY_DANJUXXRepository : IRepository<GY_DANJUXX>, IDependency
	{
        ///// <summary>
        ///// 根据单据对象名称获取单据信息
        ///// </summary>
        ///// <param name="danJuDXMC"></param>
        ///// <returns></returns>
        //List<GY_DANJUXX> GetList(string danJuDXMC);
    }
}
