using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
using Mediinfo.Infrastructure.Core.DBEntity;

namespace Mediinfo.Infrastructure.JCJG.GY
{
	public class GY_SHOUFEIXMRepository : RepositoryBase<GY_SHOUFEIXM>, IGY_SHOUFEIXMRepository
	{
		public GY_SHOUFEIXMRepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}

        /// <summary>
        /// 取不上传医保的收费项目
        /// </summary>
        /// <returns></returns>
        public List<GY_SHOUFEIXM> GetBuShangCYBXMList()
        {
            var list = this.Set<GY_SHOUFEIXM>().Where(o => o.XINGZHISX != null && o.XINGZHISX.Substring(4,1)=="1").ToList().WithContext(this, ServiceContext);
            return list;
        }

        /// <summary>
        /// 获取收费项目信息
        /// </summary>
        /// <param name="jiaGeID"></param>
        /// <returns></returns>
        public List<GY_SHOUFEIXM> GetShouFeiXM(string jiaGeID)
        {
            var list = (from sfxm in this.Set<GY_SHOUFEIXM>()
                        join wzdz in this.Set<GY_SHOUFEIXMWZDZ>()
                        on sfxm.SHOUFEIXMID equals wzdz.SHOUFEIXMID
                        where wzdz.JIAGEID == jiaGeID
                        && wzdz.ZUOFEIBZ == 0
                        select sfxm).ToList();
            return list;
        }

        public List<object> GetShouFeiTCXM(string shouFeiXMID)
        {
            var objList = new List<object>();
            var list = (from f in
                           (from sfxm in this.Set<GY_SHOUFEIXM>()
                            join sftc in this.Set<GY_SHOUFEITCMX>()
                            on sfxm.SHOUFEIXMID equals sftc.XIANGMUID
                            where sftc.SHOUFEITCID == shouFeiXMID
                            select new { sftc.SHULIANG, sfxm.DANJIA1, sfxm.DANJIA2, sfxm.DANJIA3, sfxm.DANJIA4, sfxm.DANJIA5 })
                        select new { f.DANJIA1, f.DANJIA2, f.DANJIA3, f.DANJIA4, f.DANJIA5, f.SHULIANG }).ToList();
            list.ForEach(o => objList.Add(o));

            return objList;
        }

	    public List<GY_SHOUFEIXM> GetList(string shouFeiXM)
	    {
            return (from o in Set<GY_SHOUFEIXM>()
                   where o.SHOUFEIXMID == shouFeiXM
                   select o).ToList().WithContext(this, ServiceContext);
	    }
        public List<GY_SHOUFEIXM> GetList(List<string> shouFeiXM)
        {
            return this.Set<GY_SHOUFEIXM>().Where(w=>shouFeiXM.Contains(w.SHOUFEIXMID)).ToList().WithContext(this, ServiceContext);
        }

        /// <summary>
        /// 兴和接口获取收费项目信息
        /// </summary>
        /// <param name="shoufeixmid"></param>
        /// <param name="xiangmulx"></param>
        /// <returns></returns>
        public GY_SHOUFEIXM GetshoufeixmList(string shoufeixmid, string xiangmulx)
        {
            var shouFeiXM = this.Set<GY_SHOUFEIXM>().Where(o => o.SHOUFEIXMID== shoufeixmid && o.XIANGMULX== xiangmulx).FirstOrDefaultWithContext(this, ServiceContext);
            return shouFeiXM;
        }

    }
}
