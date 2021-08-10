using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.ZY;
using Mediinfo.Infrastructure.Core.DBEntity;

namespace Mediinfo.Infrastructure.JCJG.ZY
{
	public class ZY_YINGERXXRepository : RepositoryBase<ZY_YINGERXX>, IZY_YINGERXXRepository
	{
		public ZY_YINGERXXRepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}

        /// <summary>
        /// ȡӤ��סԺ��Ϣ��by Ӥ����ԺZYID
        /// </summary>
        /// <param name="yingErRYZYID"></param>
        /// <returns></returns>
        public List<ZY_YINGERXX> GetYingERZYXX(string yingErRYZYID)
        {
            var list = this.Set<ZY_YINGERXX>().Where(o => o.YINGERRYZYID== yingErRYZYID).ToList().WithContext(this, ServiceContext);
            return list;
        }

        /// <summary>
        /// ��ĸ��סԺ�ɣ�ȡӤ����Ϣ
        /// </summary>
        /// <param name="MuQiNZYID"></param>
        /// <returns></returns>
        public List<ZY_YINGERXX> GetList(string MuQiNZYID)
        {
            var list = this.Set<ZY_YINGERXX>().Where(o => o.MUQINZYID == MuQiNZYID).ToList().WithContext(this, ServiceContext);
            return list;
        }

        public List<ZY_YINGERXX> GetListByBingRenZYID(string bingRenZYID)
        {
            var list = this.Set<ZY_YINGERXX>().Where(o => o.BINGRENZYID==bingRenZYID).ToList().WithContext(this, ServiceContext);
            return list;
        }

    }
}
