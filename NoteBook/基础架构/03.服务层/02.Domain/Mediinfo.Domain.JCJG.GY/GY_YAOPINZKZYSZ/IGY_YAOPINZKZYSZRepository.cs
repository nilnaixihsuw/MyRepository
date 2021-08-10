using System;
using System.Collections.Generic;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Infrastructure.Core.Entity;
using  Mediinfo.Infrastructure.Core.DBEntity;
namespace Mediinfo.Domain.JCJG.GY
{
	 public interface IGY_YAOPINZKZYSZRepository : IRepository<GY_YAOPINZKZYSZ>, IDependency
	{
        /// <summary>
        /// 获取药品专科专用设置
        /// </summary>
        /// <param name="jiaGeID"></param>
        /// <returns></returns>
        List<GY_YAOPINZKZYSZ> GetListByJiaGeID(string jiaGeID);

        List<GY_YAOPINZKZYSZ> GetListByKeShiID(string jiaGeID, string keShiID, int menZhenZYBZ);

        List<GY_YAOPINZKZYSZ> GetListByZhiGongID(string jiaGeID, string zhiGongID, int menZhenZYBZ);

        List<GY_YAOPINZKZYSZ> GetListByKeShiID(string keShiID, int menZhenZYBZ);

        List<GY_YAOPINZKZYSZ> GetListByZhiGongID(string zhiGongID, int menZhenZYBZ);
    }
}
