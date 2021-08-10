using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
using Mediinfo.Infrastructure.Core.DBEntity;

namespace Mediinfo.Infrastructure.JCJG.GY
{
	public class GY_SHOUSHUQXKZRepository : RepositoryBase<GY_SHOUSHUQXKZ>, IGY_SHOUSHUQXKZRepository
	{
		public GY_SHOUSHUQXKZRepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}

        /// <summary>
        /// 取职工手术权限
        /// </summary>
        /// <returns></returns>
        public List<GY_SHOUSHUQXKZ> GetShouShuQX(string prmShouShuMCID,string prmZhiGongID)
        {
            var list = this.Set<GY_SHOUSHUQXKZ>().Where(o => o.SHOUSHUMCID == prmShouShuMCID && o.ZHIGONGID == prmZhiGongID).ToList().WithContext(this, ServiceContext);
            return list;
        }

        /// <summary>
        /// 根据职工ID,手术级别判断是否有权限
        /// </summary>
        /// <param name="prmZhiGongID"></param>
        /// <param name="prmShouShuJB"></param>
        /// <returns></returns>
        public bool IsShouShuQXKZAny(string prmZhiGongID,string prmShouShuJB)
        {
            return this.Set<GY_SHOUSHUQXKZ>().Where(o => o.ZHIGONGID == prmZhiGongID && o.SHOUSHUJB == prmShouShuJB && o.ZUOFEIBZ == 0 && o.SHENHETZ == 1).Any();
        }
    }
}
