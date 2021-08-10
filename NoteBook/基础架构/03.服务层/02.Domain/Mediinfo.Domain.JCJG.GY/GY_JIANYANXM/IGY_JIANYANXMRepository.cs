using System;
using System.Collections.Generic;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Infrastructure.Core.Entity;
using Mediinfo.Infrastructure.Core.DBEntity;
namespace Mediinfo.Domain.JCJG.GY
{
	 public interface IGY_JIANYANXMRepository : IRepository<GY_JIANYANXM>, IDependency
	{
        /// <summary>
        /// 根据检验项目id取导医数据
        /// </summary>
        /// <param name="jianYanXMID"></param>
        /// <returns></returns>
        List<GY_JIANYANXM> GetJianYanXM(string jianYanXMID);

    }
}
