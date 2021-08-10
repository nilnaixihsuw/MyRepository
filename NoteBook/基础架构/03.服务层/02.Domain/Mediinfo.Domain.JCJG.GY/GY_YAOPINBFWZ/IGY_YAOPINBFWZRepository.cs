using System;
using System.Collections.Generic;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Infrastructure.Core.Entity;
using  Mediinfo.Infrastructure.Core.DBEntity;
namespace Mediinfo.Domain.JCJG.GY
{
	 public interface IGY_YAOPINBFWZRepository : IRepository<GY_YAOPINBFWZ>, IDependency
	{
        void SetCache(string yingYongID);
        List<GY_YAOPINBFWZ> GetListByJiaGeID(string yingYongID, string guanLiLB, string jiaGeID);
        List<GY_YAOPINBFWZ> GetListByJiaGeIDFromCache(string yingYongID, string guanLiLB, string jiaGeID);

        List<GY_YAOPINBFWZ> GetListByGuiGeID(string yingYongID, string guanLiLB, string guiGeID);
        List<GY_YAOPINBFWZ> GetListByGuiGeIDFromCache(string yingYongID, string guanLiLB, string guiGeID);

        List<GY_YAOPINBFWZ> GetListByYaoPinID(string yingYongID, string guanLiLB, string yaoPinID);

        List<GY_YAOPINBFWZ> GetListByYaoPinIDFromCache(string yingYongID, string guanLiLB, string yaoPinID);
        List<GY_YAOPINBFWZ> GetListByJiaGeID(string yingYongID, string guanLiLB, string jiaGeID, int menZhenZYBZ);
        List<GY_YAOPINBFWZ> GetListByGuiGeID(string yingYongID, string guanLiLB, string guiGeID, int menZhenZYBZ);

        List<GY_YAOPINBFWZ> GetListByYaoPinID(string yingYongID, string guanLiLB, string yaoPinID, int menZhenZYBZ);


    }
}
