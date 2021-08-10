using System;
using System.Collections.Generic;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Infrastructure.Core.Entity;
using  Mediinfo.Infrastructure.Core.DBEntity;
namespace Mediinfo.Domain.JCJG.GY
{
	 public interface IGY_JUESEQXRepository : IRepository<GY_JUESEQX>, IDependency
	{
        GY_JUESEQX GetByID(string jueseid, string quanxianid);
        /// <summary>
        ///added by xyz for   HR3-45056(390395) �Ƿ���й���ԱȨ��
        /// </summary>
        /// <param name="zhiGongId">ְ��id</param>
        /// <param name="quanXianID">Ȩ��id</param>
        /// <returns></returns>
        int GetByZhiGongID(string zhiGongId, string quanXianID);
    }
}
