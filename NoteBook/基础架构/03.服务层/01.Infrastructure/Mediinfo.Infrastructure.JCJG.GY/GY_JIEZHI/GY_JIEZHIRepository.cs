using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
using Mediinfo.Infrastructure.Core.DBEntity;

namespace Mediinfo.Infrastructure.JCJG.GY
{
	public class GY_JIEZHIRepository : RepositoryBase<GY_JIEZHI>, IGY_JIEZHIRepository
	{
		public GY_JIEZHIRepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}
        /// <summary>
        /// 根据病人ID取未作废介质信息
        /// </summary>
        /// <param name="bingRenID">病人ID</param> 
        /// <returns></returns>
        public GY_JIEZHI GetJieZhiXX(string bingRenID ,int zuoFeiBZ)
        {
            var list = this.Set<GY_JIEZHI>().Where(w => w.BINGRENID == bingRenID&&w.ZUOFEIBZ== zuoFeiBZ).ToList().FirstOrDefault().WithContext(this, ServiceContext);
            return list;
        }
        /// <summary>
        ///  根据病人ID、介质号获取未作废介质信息
        /// </summary>
        /// <param name="bingRenID">病人ID</param>
        /// <param name="jieZhiHao">介质ID</param>
        /// <returns></returns>
        public GY_JIEZHI GetJieZhiXX(string bingRenID, string jieZhiHao)
        {
            var list = this.Set<GY_JIEZHI>().Where(w => w.BINGRENID == bingRenID && w.JIEZHIHAO == jieZhiHao).ToList().FirstOrDefault().WithContext(this, ServiceContext);
            return list;
        }
        /// <summary>
        /// 根据病人id取病人id和介质号相同的数据
        /// </summary>
        /// <param name="bingRenID"></param>
        /// <returns></returns>
        public GY_JIEZHI GetJieZhiXXWithBRIDISJZH(string bingRenID)
        {
            var list = this.Set<GY_JIEZHI>().Where(w => w.BINGRENID == bingRenID && w.BINGRENID == w.JIEZHIHAO && w.ZUOFEIBZ == 0).ToList().FirstOrDefault().WithContext(this, ServiceContext);
            return list;
        }

        /// <summary>
        /// 根据介质号获取记录
        /// </summary>
        /// <param name="jieZhiHao">介质号</param> 
        /// <returns></returns>
        public GY_JIEZHI GetJieZhiXX(string jieZhiHao)
        {
            var list = this.Set<GY_JIEZHI>().Where(w => w.JIEZHIHAO == jieZhiHao).ToList().FirstOrDefault().WithContext(this, ServiceContext);
            return list;
        }
        /// <summary>
        /// 根据病人ID获取合并过的介质信息
        /// </summary>
        /// <param name="bingRenID">病人id</param> 
        /// <returns></returns>
        public List<GY_JIEZHI> GetHeBingXX(string bingRenID)
        {
            var list = this.Set<GY_JIEZHI>().Where(w => w.BINGRENID == bingRenID && w.HEBINGBZ == "1").ToList().WithContext(this, ServiceContext);
            return list;
        }
    }
}
