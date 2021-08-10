using System;
using System.Collections.Generic;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Infrastructure.Core.Entity;
using Mediinfo.Infrastructure.Core.DBEntity;
namespace Mediinfo.Domain.JCJG.GY
{
    public interface IGY_CHANGYONGCAIDANRepository : IRepository<GY_CHANGYONGCAIDAN>, IDependency
    {
        /// <summary>
        /// 获取常用菜单
        /// </summary>
        /// <param name="caiDanID">菜单ID</param>
        /// <returns></returns>
        List<GY_CHANGYONGCAIDAN> GetChangYongCaiDanList(string caiDanID);

        /// <summary>
        /// 获取全局常用菜单
        /// </summary>
        /// <param name="caiDanID">菜单ID</param>
        /// <returns></returns>
        List<GY_CHANGYONGCAIDAN> GetALLChangYongCaiDanList(string caiDanID);

        int GetQJXuHao();

        int GetXuHao();
    }
}
