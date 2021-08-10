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
        /// ���ݴ���ID,ȡ���ƹ����Ϣ
        /// </summary>
        /// <param name="daGuiGeID"></param>
        /// <returns></returns>
        List<GY_YAOPINMCGG2> GetList(string daGuiGeID);
        /// <summary>
        /// ����С���ID,ȡ���ƹ����Ϣ
        /// </summary>
        /// <param name="xiaoGuiGeID"></param>
        /// <returns></returns>
        List<GY_YAOPINMCGG2> GetXiaoGuiGeList(string xiaoGuiGeID);
        /// <summary>
        /// ͨ��ҩƷID��ȡ���ƹ����Ϣ
        /// </summary>
        /// <param name="yaoPinID"></param>
        /// <param name="zuoFeiBz"></param>
        /// <returns></returns>
        List<GY_YAOPINMCGG2> GetListByYaoPinID(string yaoPinID, int zuoFeiBz = 0);
        List<GY_YAOPINMCGG2> GetGuiGeList(string GuiGeID);
        List<GY_YAOPINMCGG2> QueryGuiGeList(string GuiGeID);

        /// <summary>
        /// ��ȡ���д�����Ϣ
        /// </summary>
        /// <param name="daGuiGeID"></param>
        /// <returns></returns>
        List<GY_YAOPINMCGG2> GetAllDaGuiList();

        List<GY_YAOPINMCGG2> GetZhongJianGGList(string daGuiGeID, string xiaoGuiGeID);

        List<GY_YAOPINMCGG2> GetListByGuiGeID(List<string> guiGeIDList);
    }
}
