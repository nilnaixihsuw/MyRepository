using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.XT;
using Mediinfo.Infrastructure.Core.DBEntity;

namespace Mediinfo.Infrastructure.JCJG.XT
{
	public class XT_GONGNENG_NEWRepository : RepositoryBase<XT_GONGNENG_NEW>, IXT_GONGNENG_NEWRepository
	{
		public XT_GONGNENG_NEWRepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}
        /// <summary>
        /// 取系统功能ID
        /// </summary>
        /// <param name="shangjignid"></param>
        /// <param name="xitongid"></param>
        /// <returns></returns>
        public string GetGongNengID(int jiBie,string shangjignid, string xitongid)
        {
            string max = "";
            if (jiBie == 1) //一级功能
            {
                max = (from gongnengxx in this.Set<XT_GONGNENG_NEW>()
                       where gongnengxx.GONGNENGID.Length == 8
                       && gongnengxx.XITONGID == xitongid && gongnengxx.SHANGJIGNID == "-"
                       orderby gongnengxx.GONGNENGID
                       select gongnengxx.GONGNENGID.Substring(2, 6)).Distinct().Max();
            }
            else if (jiBie == 2) //多级功能
            {

                max = (from gongnengxx in this.Set<XT_GONGNENG_NEW>()
                       where gongnengxx.XITONGID == xitongid //&& gongnengxx.SHANGJIGNID == shangjignid
                       orderby gongnengxx.GONGNENGID
                       select gongnengxx.GONGNENGID.Substring(2, 6)).Distinct().Max();
            }
            return max;
        }
        
        public XT_GONGNENG_NEW GetByID(string id)
        {
            var dto = (from xtgn in this.Set<XT_GONGNENG_NEW>()
                       where xtgn.GONGNENGROWID == id
                       select xtgn).FirstOrDefault().WithContext(this, ServiceContext); ;
            return dto;
        }
        public XT_GONGNENG_NEW GetByGongNengID(string gongNengID)
        {
            var dto = (from xtgn in this.Set<XT_GONGNENG_NEW>()
                       where xtgn.GONGNENGID == gongNengID
                       select xtgn).FirstOrDefault();
            return dto;
        }
    }
}
