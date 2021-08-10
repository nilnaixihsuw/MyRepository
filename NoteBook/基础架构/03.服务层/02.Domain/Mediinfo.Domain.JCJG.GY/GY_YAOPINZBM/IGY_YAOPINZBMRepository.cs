using System;
using System.Collections.Generic;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Infrastructure.Core.Entity;
using  Mediinfo.Infrastructure.Core.DBEntity;
namespace Mediinfo.Domain.JCJG.GY
{
	 public interface IGY_YAOPINZBMRepository : IRepository<GY_YAOPINZBM>, IDependency
	{
        List<GY_YAOPINZBM> GetListByYaoPinID(string yaoPinID);
        List<GY_YAOPINZBM> GetListByGuiGeID(string guiGeID);
        List<GY_YAOPINZBM> GetListByJiaGeID(string jiaGeID);

        /// <summary>
        /// 获取全部主别名
        /// </summary>
        /// <returns></returns>
        List<GY_YAOPINZBM> GetList();
        List<GY_YAOPINZBM> GetYaoPinZBM(Dictionary<string, string> YaoPinXXDictionary);

    }
}
