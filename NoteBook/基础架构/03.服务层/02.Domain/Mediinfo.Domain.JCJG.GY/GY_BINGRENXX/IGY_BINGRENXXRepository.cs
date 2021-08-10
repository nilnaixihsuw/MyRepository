using Mediinfo.Infrastructure.Core.Entity;
using Mediinfo.Infrastructure.Core.Repository;
using System.Collections.Generic;

namespace Mediinfo.Domain.JCJG.GY
{
    public interface IGY_BINGRENXXRepository : IRepository<GY_BINGRENXX>, IDependency
	{
        List<GY_BINGRENXX> GetList(string binRenID);

        List<GY_BINGRENXX> GetList(string gongFeiZH, string jiuZHenKH, string yiBaoKH, string geRenBH);
        
        /// <summary>
        /// ���ݲ���סԺiDȡӤ���б���Ϣ����zy_bingrenxx
        /// </summary>
        /// <param name="BingRenZYID"></param>
        /// <returns></returns>
        List<GY_BINGRENXX> GetYingErBRXX(string BingRenZYID);

        GY_BINGRENXX GetBingRenXX(string jiuZhenKH);
    }
}
