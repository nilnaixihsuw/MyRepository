using System;
using System.Collections.Generic;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Infrastructure.Core.Entity;
using  Mediinfo.Infrastructure.Core.DBEntity;
namespace Mediinfo.Domain.JCJG.GY
{
    public interface IGY_YAOPINCLKZRepository : IRepository<GY_YAOPINCLKZ>, IDependency
    {
        void SetCache();
        List<GY_YAOPINCLKZ> GetByAll();
        GY_YAOPINCLKZ GetByID(string yingYongID, string jiaGeID, string guiGeID);
        List<GY_YAOPINCLKZ> GetListByJiaGeID(string jiaGeID, string yingYongID);


        List<GY_YAOPINCLKZ> GetListByGuiGeID(string guiGeID, string yingYongID);

        List<GY_YAOPINCLKZ> GetListByYaoPinID(string yaoPinID, string yingYongID);
    } 
}
