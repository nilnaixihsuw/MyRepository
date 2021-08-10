using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mediinfo.Infrastructure.Core.Entity;
using Mediinfo.Infrastructure.Core.Repository;

namespace Mediinfo.Domain.JCJG.GY.GY_YKFCACHE
{
    public interface IGY_YKFCACHERepository : IDependency
    {
        /// <summary>
        /// 获取库存缓存
        /// </summary>
        /// <param name="jiaGeID"></param>
        /// <param name="yingYongID"></param>
        /// <returns></returns>
        GY_KUCUNCACHE GetKuCunCache(string jiaGeID, string yingYongID);
        /// <summary>
        /// 获取库存缓存列表
        /// </summary>
        /// <param name="guiGeID"></param>
        /// <param name="yingYongID"></param>
        /// <returns></returns>
        List<GY_KUCUNCACHE> GetKuCunListCache(string guiGeID, string yingYongID);
        /// <summary>
        /// 获取药品字典缓存
        /// </summary>
        /// <param name="jiaGeID"></param>
        /// <param name="yingYongID"></param>
        /// <returns></returns>
        GY_YAOPINZDCACHE GetYaoPinZdCache(string jiaGeID, string yingYongID);
        /// <summary>
        /// 获取药品字典缓存列表
        /// </summary>
        /// <param name="guiGeID"></param>
        /// <param name="yingYongID"></param>
        /// <returns></returns>
        List<GY_YAOPINZDCACHE> GetYaoPinZdListCache(string guiGeID, string yingYongID);
        /// <summary>
        /// 获取库存缓存
        /// </summary>
        /// <param name="JiaGeID"></param>
        /// <returns></returns>
        GY_KUCUNCACHE GetKuCun(string jiaGeID, string yingYongID);
        GY_KUCUNCACHE GetKuCunXH(string jiaGeID, string yingYongID, string guiGeID);
        /// <summary>
        /// 获取库存缓存列表，根据规格ID
        /// </summary>
        /// <param name="guiGeID"></param>
        /// <param name="yingYongID"></param>
        /// <returns></returns>
        List<GY_KUCUNCACHE> GetKuCunList(string guiGeID, string yingYongID);

        /// <summary>
        /// 获取药品字典缓存
        /// </summary>
        /// <param name="jiaGeID"></param>
        /// <param name="yingYongID"></param>
        /// <returns></returns>
        GY_YAOPINZDCACHE GetYaoPinZd(string jiaGeID, string yingYongID);

        /// <summary>
        /// 获取药品字典缓存列表，根据规格ID
        /// </summary>
        /// <param name="jguiGeIDiaGeID"></param>
        /// <param name="yingYongID"></param>
        /// <returns></returns>
        List<GY_YAOPINZDCACHE> GetYaoPinZdList(string guiGeID, string yingYongID);

        /// <summary>
        /// 刷新所有库存缓存
        /// </summary>
        void KuCunCacheRefreshAll();

        /// <summary>
        /// 刷新库存缓存
        /// </summary>
        /// <param name="jiaGeID"></param>
        void KuCunCacheRefresh(string jiaGeID);

        /// <summary>
        /// 刷新药品字典缓存
        /// </summary>
        void YaoPinZdCacheRefreshAll();

        /// <summary>
        /// 刷新药品字典缓存
        /// </summary>
        /// <param name="jiaGeID"></param>
        void YaoPinZdCacheRefresh(string jiaGeID);
    }
}
