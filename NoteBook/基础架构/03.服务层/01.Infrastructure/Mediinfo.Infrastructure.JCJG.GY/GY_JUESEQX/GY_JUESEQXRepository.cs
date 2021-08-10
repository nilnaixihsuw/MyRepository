using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
using Mediinfo.Infrastructure.Core.DBEntity;

namespace Mediinfo.Infrastructure.JCJG.GY
{
    public class GY_JUESEQXRepository : RepositoryBase<GY_JUESEQX>, IGY_JUESEQXRepository
    {
        public GY_JUESEQXRepository(IRepositoryContext context, ServiceContext sContext) : base(context, sContext) { }
        /// <summary>
        /// 根据主键获取角色权限信息
        /// </summary>
        /// <param name="jueSeID"></param>
        /// <param name="quanXianID"></param>
        /// <returns></returns>
        public GY_JUESEQX GetByID(string jueSeID, string quanXianID)
        {
            var dto = (from jsqx in this.Set<GY_JUESEQX>()
                       where jsqx.JUESEID == jueSeID && jsqx.QUANXIANID == quanXianID
                       select jsqx).FirstOrDefault()?.WithContext(this, ServiceContext);
            return dto;
        }

        /// <summary>
        ///added by xyz for   HR3-45056(390395) 获得管理员权限
        /// </summary>
        /// <param name="zhiGongId">职工id</param>
        /// <param name="quanXianID">权限id</param>
        /// <returns></returns>
        public int GetByZhiGongID(string zhiGongId, string quanXianID)
        {
            //Linq To Object
            //var dto = this.Set<GY_JUESEQX>().Join(this.Set<GY_JUESEYH>(), s => s.JUESEID, c => c.JUESEID,(s, c) => new { s.JUESEID }) as List<GY_JUESEQX>;
            #region Linq To Sql 实现
            //Linq To Sql
            var dtos = from s in this.Set<GY_JUESEQX>()
                       join c in this.Set<GY_JUESEYH>() on s.JUESEID equals c.JUESEID 
                       where  s.QUANXIANID == quanXianID &&  c.YONGHUID == zhiGongId
                       select new
                       {
                           s.QUANXIANID
                       };
            return dtos.Count();
            #endregion
        }
    }
}
