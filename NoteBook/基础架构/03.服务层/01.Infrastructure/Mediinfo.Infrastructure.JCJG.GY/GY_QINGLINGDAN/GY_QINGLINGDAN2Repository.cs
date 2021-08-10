using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
using Mediinfo.Infrastructure.Core.DBEntity;

namespace Mediinfo.Infrastructure.JCJG.GY
{
	public class GY_QINGLINGDAN2Repository : RepositoryBase<GY_QINGLINGDAN2>, IGY_QINGLINGDAN2Repository
	{
		public GY_QINGLINGDAN2Repository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}

        /// <summary>
        /// 联合请领单1查询请购药品
        /// </summary>
        /// <param name="qingLingDID"></param>
        /// <param name="shengQingKSSJ"></param>
        /// <param name="shengQingKSJS"></param>
        /// <returns></returns>
        public List<GY_QINGLINGDAN2> GetList(string qingLingDID, DateTime shengQingKSSJ, DateTime shengQingKSJS)
        {
            var list = (from qingling2 in this.Set<GY_QINGLINGDAN2>()
                        join qingling1 in this.Set<GY_QINGLINGDAN1>() on qingling2.QINGLINGDID equals qingling1.QINGLINGDID
                        where qingling1.BEIQINGLYYID == qingLingDID && qingling2.SHENQINGRQ >= shengQingKSSJ && qingling2.SHENQINGRQ <= shengQingKSJS
                                                         && qingling2.QINGLINGZT == "2" && qingling1.QINGLINGLX == "3"
                        select qingling2).ToList().WithContext(this, ServiceContext);
            return list;

        }

        /// <summary>
        /// 联合请领单1和请领状态查询请购药品
        /// </summary>
        /// <param name="jiaGeID"></param>
        /// <param name="beiQingLDYYID"></param>
        /// <param name="shengQingKSSJ"></param>
        /// <param name="shengQingKSJS"></param>
        /// <param name="qingLingZT"></param>
        /// <returns></returns>
        public List<GY_QINGLINGDAN2> GetList(string jiaGeID, string beiQingLDYYID, DateTime shengQingKSSJ, DateTime shengQingKSJS,string qingLingZT)
        {
            var list = (from qinglingD1 in this.Set<GY_QINGLINGDAN1>()
                        join qinglingD2 in this.Set<GY_QINGLINGDAN2>() on qinglingD1.QINGLINGDID equals qinglingD2.QINGLINGDID
                        where qinglingD1.BEIQINGLYYID == beiQingLDYYID && qinglingD2.SHENQINGRQ >= shengQingKSSJ && qinglingD2.SHENQINGRQ <= shengQingKSJS
                        && qinglingD2.QINGLINGZT == qingLingZT && new string[] { "0", "1", "2" }.Contains(qinglingD1.QINGLINGLX)
                        select qinglingD2).ToList().WithContext(this, ServiceContext);
            return list;
        }

        /// <summary>
        /// 根据价格ID获取请领单明细
        /// </summary>
        /// <param name="jiaGeID"></param>
        /// <returns></returns>
        public List<GY_QINGLINGDAN2> GetList(string jiaGeID)
        {
            var list = this.Set<GY_QINGLINGDAN2>().Where(o => o.WUPINID == jiaGeID).ToList().WithContext(this, ServiceContext);
            return list;
        }
    }
}
