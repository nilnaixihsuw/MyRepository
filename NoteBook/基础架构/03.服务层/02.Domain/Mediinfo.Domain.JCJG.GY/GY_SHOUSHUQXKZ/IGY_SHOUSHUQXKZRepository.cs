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
        /// ȡְ������Ȩ��
        /// </summary>
        /// <returns></returns>
        List<GY_SHOUSHUQXKZ> GetShouShuQX(string prmShouShuMCID, string prmZhiGongID);
        /// <summary>
        /// ����ְ��ID,���������ж��Ƿ���Ȩ��
        /// </summary>
        /// <param name="prmZhiGongID"></param>
        /// <param name="prmShouShuJB"></param>
        /// <returns></returns>
        bool IsShouShuQXKZAny(string prmZhiGongID, string prmShouShuJB);
    }
}
