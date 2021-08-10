using System;
using System.Collections.Generic;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Infrastructure.Core.Entity;
using  Mediinfo.Infrastructure.Core.DBEntity;
namespace Mediinfo.Domain.JCJG.GY
{
	 public interface IGY_DANWEIRepository : IRepository<GY_DANWEI>, IDependency
	{
        /// <summary>
        /// ���ݵ�λID��Ӧ��ID��ȡ��λ����
        /// </summary>
        /// <param name="yyid"></param>
        /// <param name="dwid"></param>
        /// <returns></returns>
        List<GY_DANWEI> GetList(string yingYongID, string danWeiID);
    }
}
