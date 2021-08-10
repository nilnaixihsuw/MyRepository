using System;
using System.Collections.Generic;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Infrastructure.Core.Entity;
using Mediinfo.Infrastructure.Core.DBEntity;
namespace Mediinfo.Domain.JCJG.GY
{
	 public interface IGY_MAZUIQXKZRepository : IRepository<GY_MAZUIQXKZ>, IDependency
	{
        string GetZhiChengID(string prmZhiGongID, int prmMenZhenZYBZ);
        int? GetShenHeTZ(string prmShouShuMCID, string prmZhiGongID);
        bool IsAny(string prmShouShuJB, string prmZhiGongID);
    }
}
