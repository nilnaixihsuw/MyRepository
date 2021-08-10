using Mediinfo.Infrastructure.Core.Entity;
using Mediinfo.Infrastructure.Core.Repository;

using System.Collections.Generic;

namespace Mediinfo.Domain.JCJG.GY
{
    public interface IGY_GONGZUOZHANRepository : IRepository<GY_GONGZUOZHAN>, IDependency
    {
        /// <summary>
        /// ��ȡ����վ�б�
        /// </summary>
        /// <param name="ip">IP��ַ</param>
        /// <param name="moJiBz">ĩ����־</param>
        /// <returns></returns>
        List<GY_GONGZUOZHAN> GetList(string ip, int moJiBz);
    }
}
