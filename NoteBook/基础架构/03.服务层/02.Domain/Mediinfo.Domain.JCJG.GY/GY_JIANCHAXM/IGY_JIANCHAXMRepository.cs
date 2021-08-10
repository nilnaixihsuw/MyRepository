using System;
using System.Collections.Generic;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Infrastructure.Core.Entity;
using Mediinfo.Infrastructure.Core.DBEntity;
namespace Mediinfo.Domain.JCJG.GY
{
	 public interface IGY_JIANCHAXMRepository : IRepository<GY_JIANCHAXM>, IDependency
	{
        List<GY_JIANCHAXM> GetList(string jianChaXMID);

        /// <summary>
        /// 根据检查项目idS取检查项目数据
        /// </summary>
        /// <param name="jianChaXMIDS"></param>
        /// <returns></returns>
        List<GY_JIANCHAXM> GetListS(string[] jianChaXMIDS, string[] jianchalxLXs);

    }
}
