using System;
using System.Collections.Generic;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Infrastructure.Core.Entity;
using  Mediinfo.Infrastructure.Core.DBEntity;
namespace Mediinfo.Domain.JCJG.GY
{
	 public interface IGY_DANJUXXRepository : IRepository<GY_DANJUXX>, IDependency
	{
        ///// <summary>
        ///// ���ݵ��ݶ������ƻ�ȡ������Ϣ
        ///// </summary>
        ///// <param name="danJuDXMC"></param>
        ///// <returns></returns>
        //List<GY_DANJUXX> GetList(string danJuDXMC);
    }
}
