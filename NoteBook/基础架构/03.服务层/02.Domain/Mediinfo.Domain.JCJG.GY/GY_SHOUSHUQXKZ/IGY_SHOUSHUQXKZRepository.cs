using System;
using System.Collections.Generic;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Infrastructure.Core.Entity;
using Mediinfo.Infrastructure.Core.DBEntity;
namespace Mediinfo.Domain.JCJG.GY
{
    public interface IGY_SHOUSHUQXKZRepository : IRepository<GY_SHOUSHUQXKZ>, IDependency
    {
        /// <summary>
        /// 取职工手术权限
        /// </summary>
        /// <returns></returns>
        List<GY_SHOUSHUQXKZ> GetShouShuQX(string prmShouShuMCID, string prmZhiGongID);
        /// <summary>
        /// 根据职工ID,手术级别判断是否有权限
        /// </summary>
        /// <param name="prmZhiGongID"></param>
        /// <param name="prmShouShuJB"></param>
        /// <returns></returns>
        bool IsShouShuQXKZAny(string prmZhiGongID, string prmShouShuJB);
    }
}
