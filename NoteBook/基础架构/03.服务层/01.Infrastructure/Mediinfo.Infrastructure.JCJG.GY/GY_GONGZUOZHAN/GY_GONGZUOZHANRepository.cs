using Mediinfo.Domain.JCJG.GY;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.DBEntity;
using Mediinfo.Infrastructure.Core.Repository;

using System.Collections.Generic;
using System.Linq;

namespace Mediinfo.Infrastructure.JCJG.GY
{
    public class GY_GONGZUOZHANRepository : RepositoryBase<GY_GONGZUOZHAN>, IGY_GONGZUOZHANRepository
    {
        public GY_GONGZUOZHANRepository(IRepositoryContext context, ServiceContext sContext)
            : base(context, sContext)
        {

        }

        /// <summary>
        /// ��ȡ����վ�б�
        /// </summary>
        /// <param name="ip">IP��ַ</param>
        /// <param name="moJiBz">ĩ����־</param>
        /// <returns></returns>
        public List<GY_GONGZUOZHAN> GetList(string ip, int moJiBz)
        {
            var list = this.Set<GY_GONGZUOZHAN>().Where(c => c.IP == ip && c.MOJIBZ == moJiBz).ToList().WithContext(this, ServiceContext);
            return list;
        }
    }
}
