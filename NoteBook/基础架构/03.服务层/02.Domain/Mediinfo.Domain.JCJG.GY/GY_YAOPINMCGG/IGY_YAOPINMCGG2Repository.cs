using System;
using System.Collections.Generic;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Infrastructure.Core.Entity;
using Mediinfo.Infrastructure.Core.DBEntity;
namespace Mediinfo.Domain.JCJG.GY
{
    public interface IGY_YAOPINMCGG2Repository : IRepository<GY_YAOPINMCGG2>, IDependency
    {
        /// <summary>
        /// 根据大规格ID,取名称规格信息
        /// </summary>
        /// <param name="daGuiGeID"></param>
        /// <returns></returns>
        List<GY_YAOPINMCGG2> GetList(string daGuiGeID);
        /// <summary>
        /// 根据小规格ID,取名称规格信息
        /// </summary>
        /// <param name="xiaoGuiGeID"></param>
        /// <returns></returns>
        List<GY_YAOPINMCGG2> GetXiaoGuiGeList(string xiaoGuiGeID);
        /// <summary>
        /// 通过药品ID获取名称规格信息
        /// </summary>
        /// <param name="yaoPinID"></param>
        /// <param name="zuoFeiBz"></param>
        /// <returns></returns>
        List<GY_YAOPINMCGG2> GetListByYaoPinID(string yaoPinID, int zuoFeiBz = 0);
        List<GY_YAOPINMCGG2> GetGuiGeList(string GuiGeID);
        List<GY_YAOPINMCGG2> QueryGuiGeList(string GuiGeID);

        /// <summary>
        /// 获取所有大规格信息
        /// </summary>
        /// <param name="daGuiGeID"></param>
        /// <returns></returns>
        List<GY_YAOPINMCGG2> GetAllDaGuiList();

        List<GY_YAOPINMCGG2> GetZhongJianGGList(string daGuiGeID, string xiaoGuiGeID);

        List<GY_YAOPINMCGG2> GetListByGuiGeID(List<string> guiGeIDList);
    }
}
