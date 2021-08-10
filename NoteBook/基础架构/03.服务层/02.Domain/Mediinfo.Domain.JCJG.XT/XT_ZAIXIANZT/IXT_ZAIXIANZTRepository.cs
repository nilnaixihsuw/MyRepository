using System;
using System.Collections.Generic;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Infrastructure.Core.Entity;
using Mediinfo.Infrastructure.Core.DBEntity;
namespace Mediinfo.Domain.JCJG.XT
{
    public interface IXT_ZAIXIANZTRepository : IRepository<XT_ZAIXIANZT>, IDependency
    {
        /// <summary>
        /// ����ְ��ID��ȡ����״̬
        /// </summary>
        /// <param name="zhiGongID"></param>
        /// <returns></returns>
        XT_ZAIXIANZT GetZaiXianZT(string zhiGongID);
        List<XT_ZAIXIANZT> QueryZaiXianYh();
    }
}
