using System;
using System.Collections.Generic;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Infrastructure.Core.Entity;
using  Mediinfo.Infrastructure.Core.DBEntity;
namespace Mediinfo.Domain.JCJG.GY
{
	 public interface IGY_SHOUFEIXMRepository : IRepository<GY_SHOUFEIXM>, IDependency
	{
        List<GY_SHOUFEIXM> GetBuShangCYBXMList();
        List<GY_SHOUFEIXM> GetShouFeiXM(string jiaGeID);
        List<object> GetShouFeiTCXM(string shouFeiXMID);

	    List<GY_SHOUFEIXM> GetList(string shouFeiXM);
        List<GY_SHOUFEIXM> GetList(List<string> shouFeiXM);
        /// <summary>
        /// 兴和接口获取收费项目信息
        /// </summary>
        /// <param name="shouFeiXM"></param>
        /// <returns></returns>
        GY_SHOUFEIXM GetshoufeixmList(string shoufeixmid, string xiangmulx);
    }
}
