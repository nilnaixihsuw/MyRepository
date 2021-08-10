using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
using Mediinfo.Infrastructure.Core.DBEntity;

namespace Mediinfo.Infrastructure.JCJG.GY
{
    public class GY_CHANGYONGCAIDANRepository : RepositoryBase<GY_CHANGYONGCAIDAN>, IGY_CHANGYONGCAIDANRepository
    {
        public GY_CHANGYONGCAIDANRepository(IRepositoryContext context, ServiceContext sContext) : base(context, sContext) { }

        /// <summary>
        /// 获取常用菜单
        /// </summary>
        /// <param name="caiDanID">菜单ID</param>
        /// <returns></returns>
        public List<GY_CHANGYONGCAIDAN> GetChangYongCaiDanList(string caiDanID)
        {
            var list = this.Set<GY_CHANGYONGCAIDAN>().Where(p => p.YONGHUID == ServiceContext.USERID && p.YINGYONGID == ServiceContext.YINGYONGID && p.CAIDANID == caiDanID).ToList().WithContext(this, ServiceContext);
            return list;
        }

        /// <summary>
        /// 获取全局常用菜单
        /// </summary>
        /// <param name="caiDanID">菜单ID</param>
        /// <returns></returns>
        public List<GY_CHANGYONGCAIDAN> GetALLChangYongCaiDanList(string caiDanID)
        {
            var list = this.Set<GY_CHANGYONGCAIDAN>().Where(p => p.YONGHUID == "ALL" && p.YINGYONGID == ServiceContext.YINGYONGID && p.CAIDANID == caiDanID).ToList().WithContext(this, ServiceContext);
            return list;
        }
        /// <summary>
        /// 获取全局最大排序号
        /// </summary>
        /// <returns></returns>
        public int GetQJXuHao()
        {
            var list = this.Set<GY_CHANGYONGCAIDAN>().Where(p => p.YONGHUID == "ALL" && p.YINGYONGID == ServiceContext.YINGYONGID && p.ISQUANJVCY ==1).ToList().WithContext(this, ServiceContext);
            int? xuhao = list.Max(o => o.PAIXU);
            if (xuhao != null && xuhao > 0)
            {
                return Convert.ToInt32(xuhao) + 1;
            }
            else
            {
                return 1;
            }
        }

        /// <summary>
        /// 获取最大排序号
        /// </summary>
        /// <returns></returns>
        public int GetXuHao()
        {
            var list = this.Set<GY_CHANGYONGCAIDAN>().Where(p => p.YONGHUID == ServiceContext.USERID && p.YONGHUID == null && p.YINGYONGID == ServiceContext.YINGYONGID && p.ISQUANJVCY == 1).ToList().WithContext(this, ServiceContext);
            int? xuhao = list.Max(o => o.PAIXU);
            if (xuhao != null && xuhao > 0)
            {
                return Convert.ToInt32(xuhao) + 1;
            }
            else
            {
                return 1;
            }
        }
    }
}
