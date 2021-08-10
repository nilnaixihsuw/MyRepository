using System;
using System.Collections.Generic;
using Mediinfo.DTO.HIS.GY;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Infrastructure.Core.Entity;

namespace Mediinfo.Domain.JCJG.GY
{
    public interface IGY_CANSHURepository : IRepository<GY_CANSHU>, IDependency
    {
        string GetCanShu(string yingYongId, string canShuId, string defaultValue);

        List<GY_CANSHU> GetList(string canShuID);

        GY_CANSHU GetParamByKey(string yingYongID, string canShuID);
    }
}

