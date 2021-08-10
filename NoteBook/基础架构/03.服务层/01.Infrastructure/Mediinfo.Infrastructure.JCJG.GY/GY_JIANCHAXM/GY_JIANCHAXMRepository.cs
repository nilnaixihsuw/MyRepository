using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
using Mediinfo.Infrastructure.Core.DBEntity;

namespace Mediinfo.Infrastructure.JCJG.GY
{
	public class GY_JIANCHAXMRepository : RepositoryBase<GY_JIANCHAXM>, IGY_JIANCHAXMRepository
	{
		public GY_JIANCHAXMRepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}

        /// <summary>
        /// 根据检查项目id取检查项目数据
        /// </summary>
        /// <param name="shenQingDID"></param>
        /// <returns></returns>
        public List<GY_JIANCHAXM> GetList(string jianChaXMID)
        {
            var JianChaXMList = this.Set<GY_JIANCHAXM>().Where(o => o.JIANCHAXMID == jianChaXMID).ToList().WithContext(this, ServiceContext);
            return JianChaXMList;
        }

        /// <summary>
        /// 根据检查项目idS取检查项目数据
        /// </summary>
        /// <param name="jianChaXMIDS"></param>
        /// <returns></returns>
        public List<GY_JIANCHAXM> GetListS(string[] jianChaXMIDS,string[]jianchalxLXs)
        {


            //var JIANCHAXMID= jianChaXMIDS.Aggregate("", (current, t) => current + ("'" + t + "',")).TrimEnd(',');
            //var ckbqdz = this.SqlQuery<GY_JIANCHAXM>("select * from GY_JIANCHAXM where JIANCHAXMID in ("+ JIANCHAXMID + ") ").ToList().WithContext(this, ServiceContext);
            //return ckbqdz;

             

            var temp=this.Set<GY_JIANCHAXM>().Where(o => jianChaXMIDS.Contains(o.JIANCHAXMID)).ToString();
            var JianChaXMList = this.Set<GY_JIANCHAXM>().Where(o => jianChaXMIDS.Contains(o.JIANCHAXMID)&&jianchalxLXs.Contains(o.JIANCHALX) && o.MOJIBZ == 0).ToList().WithContext(this, ServiceContext);
            return JianChaXMList;
        }
    }
}
