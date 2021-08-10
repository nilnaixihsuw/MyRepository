using System;
using System.Collections.Generic;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Infrastructure.Core.Entity;
using Mediinfo.Infrastructure.Core.DBEntity;
namespace Mediinfo.Domain.JCJG.GY
{
	 public interface IGY_GUANDAORepository : IRepository<GY_GUANDAO>, IDependency
	{
        /// <summary>
        /// 根据管道记录id获取管道信息
        /// </summary>
        /// <param name="guandaojlID"></param>
        /// <returns></returns>
          List<GY_GUANDAO> GetList(string guandaojlID);
        /// <summary>
        /// 获取所有管道信息
        /// </summary>
        /// <returns></returns>
        List<GY_GUANDAO> GetList();
        int GetShunXuHao();

    }
}
