using System;
using System.Collections.Generic;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Infrastructure.Core.Entity;
using Mediinfo.Infrastructure.Core.DBEntity;
namespace Mediinfo.Domain.JCJG.GY
{
    public interface IGY_TAOCANMXRepository : IRepository<GY_TAOCANMX>, IDependency
    {
        /// <summary>
        /// �����ײ�ID,��ĿID,��������ȡ�����ײ���ϸ
        /// </summary>
        /// <param name="TaoCanID">�ײ�ID</param>
        /// <param name="XiangMuID">��ĿID</param>
        /// <param name="FeiYongLB">�������</param>
        /// <returns></returns>
        GY_TAOCANMX GetTaoCanMX(string TaoCanID, string XiangMuID, int? FeiYongLB);
    }
}
