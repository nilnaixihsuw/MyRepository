using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.HIS.GY;
using Mediinfo.DTO.HIS.GY;

namespace Mediinfo.Infrastructure.HIS.GY
{
	public class GY_DAIMARepository : RepositoryBase<GY_DAIMA>, IGY_DAIMARepository
	{
		public GY_DAIMARepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}
 
        public  List<GY_DAIMA> GetList(string daiMaLB ,string daiMaId)
        {
            var list = this.Set<GY_DAIMA>().Where(o => o.DAIMALB == daiMaLB && o.DAIMAID == daiMaId).ToList();
            return list;
        }

        /// <summary>
        /// 根据代码类别获取代码信息
        /// </summary>
        /// <param name="daiMaLB">代码类别</param>
        /// <returns></returns>
        public List<GY_DAIMA> GetList(string daiMaLB)
        {
            var list = this.Set<GY_DAIMA>().Where(o => o.DAIMALB == daiMaLB).ToList();
            return list;
        }
        /// <summary>
        /// 公用控件取代码数据
        /// </summary>
        /// <returns></returns>
        public List<GY_DAIMA> GetGYList(string DAIMALB, int MENZHENSY, int ZHUYUANSY, int ZUOFEIBZ)
        {
            var list = this.Set<GY_DAIMA>().Where(o => o.DAIMALB == DAIMALB && o.MENZHENSY == MENZHENSY && o.ZHUYUANSY == ZHUYUANSY && o.ZUOFEIBZ == ZUOFEIBZ).ToList();
            return list;
        }
        /// <summary>
        /// 获取公用频次类型
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public int? GetPinCiLX(string key)
        {
            int? pinCiLX=null;
            var list = this.Set<GY_DAIMA>().Where(o => o.DAIMAID==key).ToList();

            if (list!=null)
            {
                pinCiLX = list.FirstOrDefault().BIAOZHIWEI2;
            }
            return pinCiLX;
        }
    }
}
