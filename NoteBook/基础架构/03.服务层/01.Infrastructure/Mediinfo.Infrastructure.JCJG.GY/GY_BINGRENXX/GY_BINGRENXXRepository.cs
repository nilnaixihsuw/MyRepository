using Mediinfo.Domain.JCJG.GY;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.DBEntity;
using Mediinfo.Infrastructure.Core.Repository;

using System.Collections.Generic;
using System.Linq;

namespace Mediinfo.Infrastructure.JCJG.GY
{
    public class GY_BINGRENXXRepository : RepositoryBase<GY_BINGRENXX>, IGY_BINGRENXXRepository
    {
        public GY_BINGRENXXRepository(IRepositoryContext context, ServiceContext sContext) : base(context, sContext) { }

        public List<GY_BINGRENXX> GetList(string binRenID)
        {
            var list = this.Set<GY_BINGRENXX>().Where(p => p.BINGRENID == binRenID).ToList().WithContext(this, ServiceContext);
            return list;
        }

        /// <summary>
        /// ���ݹ���֤�ţ����￨�ţ�ҽ�����ţ����˱������һ����ѯ���˲�����Ϣ
        /// </summary>
        /// <param name="gongFeiZH"></param>
        /// <param name="jiuZHenKH"></param>
        /// <param name="yiBaoKH"></param>
        /// <param name="geRenBH"></param>
        /// <returns></returns>
        public List<GY_BINGRENXX> GetList(string gongFeiZH, string jiuZHenKH, string yiBaoKH, string geRenBH)
        {
            if (string.IsNullOrWhiteSpace(gongFeiZH))
            {
                gongFeiZH = "-1";
            }
            if (string.IsNullOrWhiteSpace(jiuZHenKH))
            {
                jiuZHenKH = "-1";
            }
            if (string.IsNullOrWhiteSpace(yiBaoKH))
            {
                yiBaoKH = "-1";
            }
            if (string.IsNullOrWhiteSpace(geRenBH))
            {
                geRenBH = "-1";
            }

            var list = this.Set<GY_BINGRENXX>().Where(p => p.JIUZHENKH == jiuZHenKH ||
           p.GONGFEIZH == gongFeiZH || p.YIBAOKH == yiBaoKH || p.GERENBH == geRenBH).ToList().WithContext(this, ServiceContext);

            //var list = this.Set<GY_BINGRENXX>().Where(p => p.JIUZHENKH == jiuZHenKH || p.YIBAOKH == yiBaoKH || p.GERENBH == geRenBH).ToList().WithContext(this, ServiceContext);

            return list;
        }

       //public List<GY_BINGRENXX> GetYingErBRXX(string BingRenZYID)
       // {
       //     var list = (from gyBingRenXX in this.Set<GY_BINGRENXX>()
       //                 join zyBingRenXX in this.Set<ZY_BINGRENXX>()
       //                 on gyBingRenXX.BINGRENZYID equals zyBingRenXX.BINGRENZYID
       //                 where zyBingRenXX.MUQINZYID == BingRenZYID &&
       //                 zyBingRenXX.YINGERBZ == 1
       //                 select gyBingRenXX).ToList();
       //     return list;


       // }

        public GY_BINGRENXX GetBingRenXX(string jiuZhenKH)
        {
            return this.Set<GY_BINGRENXX>().Where(p => p.JIUZHENKH == jiuZhenKH).FirstOrDefault().WithContext(this, ServiceContext);
        }

        public List<GY_BINGRENXX> GetYingErBRXX(string BingRenZYID)
        {
            throw new System.NotImplementedException();
        }
    }
}
