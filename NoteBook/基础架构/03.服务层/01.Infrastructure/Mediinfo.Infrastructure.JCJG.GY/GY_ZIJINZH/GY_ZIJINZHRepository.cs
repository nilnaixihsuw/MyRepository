using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
using Mediinfo.Infrastructure.Core.DBEntity;

namespace Mediinfo.Infrastructure.JCJG.GY
{
	public class GY_ZIJINZHRepository : RepositoryBase<GY_ZIJINZH>, IGY_ZIJINZHRepository
	{
		public GY_ZIJINZHRepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}


        /// <summary>
        /// ���ݲ���id�ͽ��ʺ�ȡ�����ʽ��˻���Ϣ
        /// </summary>
        /// <param name="bingRenID">����id</param>
        /// <param name="jieZhiHao">���ʺ�</param>
        /// <returns></returns>
        public GY_ZIJINZH GetZiJinZH(string bingRenID, string jieZhiHao)
        {
            var ziJinZH = this.Set<GY_ZIJINZH>().Where(w=> w.BINGRENID==bingRenID&&w.JIEZHIHAO ==jieZhiHao).FirstOrDefaultWithContext(this, ServiceContext);
            return ziJinZH;
        } 

        public decimal? GetQiMoJE(string bingRenID)
        {
            var qiMoJE = this.Set<GY_ZIJINZH>().Where(w => w.BINGRENID == bingRenID && w.ZHANGHUZT == 0 && (w.QIANFEIZT == 0 || w.QIANFEIZT == 1 || w.QIANFEIZT == null)).ToList().Select(w => w.QIMOJE)?.FirstOrDefault();
            return qiMoJE;
        }
    }
}
