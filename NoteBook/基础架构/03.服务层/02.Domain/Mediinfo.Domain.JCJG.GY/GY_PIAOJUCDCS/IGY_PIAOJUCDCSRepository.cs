using System;
using System.Collections.Generic;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Infrastructure.Core.Entity;
using  Mediinfo.Infrastructure.Core.DBEntity;
namespace Mediinfo.Domain.JCJG.GY
{
    public interface IGY_PIAOJUCDCSRepository : IRepository<GY_PIAOJUCDCS>, IDependency
    {
        /// <summary>
        /// 根据票据业务id，及票据类型id获取票据打印次数信息
        /// </summary>
        /// <param name="piaoJuYWID"></param>
        /// <param name="piaoJuLXID"></param>
        /// <returns></returns>
        List<GY_PIAOJUCDCS> GetList(string piaoJuYWID, string piaoJuLXID);

    }
}
