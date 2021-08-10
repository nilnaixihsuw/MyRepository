using System;
using System.Collections.Generic;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Infrastructure.Core.Entity;
using  Mediinfo.Infrastructure.Core.DBEntity;
namespace Mediinfo.Domain.JCJG.GY
{
	public interface IGY_BILIYHRepository : IRepository<GY_BILIYH>, IDependency
	{
        /// <summary>
        /// ͨ��������ȡ�����ڸ���������Ҫ���أ�
        /// </summary>
        /// <param name="youHuiLB">�Ż����</param>
        /// <param name="xiangMuId">��ĿId</param>
        /// <param name="xiangMuLX">��Ŀ����</param>
        /// <returns></returns>
        GY_BILIYH GetByKey(string youHuiLB, string xiangMuId, string xiangMuLX);
        List<GY_BILIYH> GetList();
	}
}
