using System;
using System.Collections.Generic;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Infrastructure.Core.Entity;
using  Mediinfo.Infrastructure.Core.DBEntity;
namespace Mediinfo.Domain.JCJG.GY
{
	 public interface IGY_DANJUDXXXRepository : IRepository<GY_DANJUDXXX>, IDependency
	{
        /// <summary>
        /// 根据单据对象名称及应用ID 获取单据对象
        /// </summary>
        /// <param name="danJuDXMC">单据对象名称</param>
        /// <param name="yingYongID">应用ID</param>
        /// <returns></returns>
        List<GY_DANJUDXXX> GetList(string danJuDXMC,string yingYongID);
    }
}
