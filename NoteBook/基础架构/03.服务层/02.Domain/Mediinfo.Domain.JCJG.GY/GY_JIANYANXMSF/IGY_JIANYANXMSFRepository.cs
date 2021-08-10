using System;
using System.Collections.Generic;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Infrastructure.Core.Entity;
using Mediinfo.Infrastructure.Core.DBEntity;
namespace Mediinfo.Domain.JCJG.GY
{
	 public interface IGY_JIANYANXMSFRepository : IRepository<GY_JIANYANXMSF>, IDependency
	{
        /// <summary>
        /// 取检验项目收费信息，根据检验项目ID
        /// </summary>
        /// <param name="jianYanXMID">检验项目ID</param>
        /// <returns></returns>
        List<GY_JIANYANXMSF> GetListByJYXMID(string jianYanXMID);
	}
}
