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
        /// ���ݹܵ���¼id��ȡ�ܵ���Ϣ
        /// </summary>
        /// <param name="guandaojlID"></param>
        /// <returns></returns>
          List<GY_GUANDAO> GetList(string guandaojlID);
        /// <summary>
        /// ��ȡ���йܵ���Ϣ
        /// </summary>
        /// <returns></returns>
        List<GY_GUANDAO> GetList();
        int GetShunXuHao();

    }
}
