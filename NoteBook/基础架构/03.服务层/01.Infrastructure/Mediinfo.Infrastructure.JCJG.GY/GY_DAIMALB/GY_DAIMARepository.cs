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
        /// ���ݴ�������ȡ������Ϣ
        /// </summary>
        /// <param name="daiMaLB">�������</param>
        /// <returns></returns>
        public List<GY_DAIMA> GetList(string daiMaLB)
        {
            var list = this.Set<GY_DAIMA>().Where(o => o.DAIMALB == daiMaLB).ToList();
            return list;
        }
        /// <summary>
        /// ���ÿؼ�ȡ��������
        /// </summary>
        /// <returns></returns>
        public List<GY_DAIMA> GetGYList(string DAIMALB, int MENZHENSY, int ZHUYUANSY, int ZUOFEIBZ)
        {
            var list = this.Set<GY_DAIMA>().Where(o => o.DAIMALB == DAIMALB && o.MENZHENSY == MENZHENSY && o.ZHUYUANSY == ZHUYUANSY && o.ZUOFEIBZ == ZUOFEIBZ).ToList();
            return list;
        }
        /// <summary>
        /// ��ȡ����Ƶ������
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
