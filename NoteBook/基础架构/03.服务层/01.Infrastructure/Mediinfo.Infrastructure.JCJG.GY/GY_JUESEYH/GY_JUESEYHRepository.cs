using Mediinfo.Domain.JCJG.GY;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.DBEntity;
using Mediinfo.Infrastructure.Core.Repository;

using System.Collections.Generic;
using System.Linq;

namespace Mediinfo.Infrastructure.JCJG.GY
{
    public class GY_JUESEYHRepository : RepositoryBase<GY_JUESEYH>, IGY_JUESEYHRepository
    {
        public GY_JUESEYHRepository(IRepositoryContext context, ServiceContext sContext) : base(context, sContext) { }
        
        /// <summary>
        /// 根据角色ID和用户ID查询用户角色
        /// </summary>
        /// <param name="jueSeID">角色ID</param>
        /// <param name="yongHuID">用户ID</param>
        /// <returns></returns>
        public GY_JUESEYH GetByID(string jueSeID, string yongHuID)
        {
            var dto = this.Set<GY_JUESEYH>().Where(p => p.JUESEID == jueSeID && p.YONGHUID == yongHuID).ToList().FirstOrDefault<GY_JUESEYH>().WithContext(this, ServiceContext);
            return dto;
        }

        /// <summary>
        /// 根据用户id得到角色id
        /// </summary>
        /// <param name="zhiGongID">职工ID</param>
        /// <returns></returns>
        public List<GY_JUESEYH> GetJueSeID(string zhiGongID)
        {
            var list = this.Set<GY_JUESEYH>().Where(p => p.YONGHUID == zhiGongID).ToList().WithContext(this, ServiceContext);
            return list;
        }
    }
}
