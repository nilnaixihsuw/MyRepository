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
        /// ��ȡ���ò˵�
        /// </summary>
        /// <param name="caiDanID">�˵�ID</param>
        /// <returns></returns>
        List<GY_CHANGYONGCAIDAN> GetChangYongCaiDanList(string caiDanID);

        /// <summary>
        /// ��ȡȫ�ֳ��ò˵�
        /// </summary>
        /// <param name="caiDanID">�˵�ID</param>
        /// <returns></returns>
        List<GY_CHANGYONGCAIDAN> GetALLChangYongCaiDanList(string caiDanID);

        int GetQJXuHao();

        int GetXuHao();
    }
}
