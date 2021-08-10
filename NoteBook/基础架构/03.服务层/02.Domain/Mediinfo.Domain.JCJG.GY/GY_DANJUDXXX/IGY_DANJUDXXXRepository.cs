using System;
using System.Collections.Generic;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Infrastructure.Core.Entity;
using  Mediinfo.Infrastructure.Core.DBEntity;
namespace Mediinfo.Domain.JCJG.GY
{
	 public interface IGY_DANJUDXXXRepository : IRepository<GY_DANJUDXXX>, IDependency
	{
        /// <summary>
        /// ���ݵ��ݶ������Ƽ�Ӧ��ID ��ȡ���ݶ���
        /// </summary>
        /// <param name="danJuDXMC">���ݶ�������</param>
        /// <param name="yingYongID">Ӧ��ID</param>
        /// <returns></returns>
        List<GY_DANJUDXXX> GetList(string danJuDXMC,string yingYongID);
    }
}
