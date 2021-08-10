using System;
using System.Collections.Generic;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Infrastructure.Core.Entity;
using Mediinfo.Infrastructure.Core.DBEntity;
namespace Mediinfo.Domain.JCJG.ZY
{
	 public interface IZY_YINGERXXRepository : IRepository<ZY_YINGERXX>, IDependency
	{

        List<ZY_YINGERXX> GetYingERZYXX(string yingErRYZYID);

        /// <summary>
        /// 取婴儿信息by母亲住院ＩＤ
        /// </summary>
        /// <param name="MuQiNZYID"></param>
        /// <returns></returns>
        List<ZY_YINGERXX> GetList(string MuQiNZYID);
        List<ZY_YINGERXX> GetListByBingRenZYID(string bingRenZYID);

    }


}
