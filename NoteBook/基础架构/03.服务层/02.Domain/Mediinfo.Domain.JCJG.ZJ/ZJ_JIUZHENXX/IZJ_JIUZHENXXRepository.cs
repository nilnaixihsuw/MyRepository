using System;
using System.Collections.Generic;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Infrastructure.Core.Entity;
using  Mediinfo.Infrastructure.Core.DBEntity;
namespace Mediinfo.Domain.JCJG.ZJ
{
	 public interface IZJ_JIUZHENXXRepository : IRepository<ZJ_JIUZHENXX>, IDependency
	{
        List<ZJ_JIUZHENXX> GetList(List<string> jiuZhengID);
        ZJ_JIUZHENXX GetByJiuZhenID(string jiuZhenID);
        ZJ_JIUZHENXX Get(string guaHaoID);
        /// <summary>
        /// ��ȡ���˵�����Ҿ����¼����
        /// </summary>
        /// <param name="bingRenID"></param>
        /// <param name="jiuZhenKS"></param>
        /// <returns></returns>
	    int GetBingRenDTKSJZJLCount(string bingRenID, string jiuZhenKS);
        /// <summary>
        /// ��ȡ���������ϼ����Ҿ����¼����
        /// </summary>
        /// <param name="bingRenID"></param>
        /// <param name="list"></param>
        /// <returns></returns>
	    int GetBingRenSYSJKSJZJLCount(string bingRenID, List<string> list);
        /// <summary>
        /// �����ϴξ�����Ϣ
        /// </summary>
        /// <param name="bingRenID"></param>
        /// <param name="jiuZhenID"></param>
        /// <param name="keShiID"></param>
        /// <returns></returns>
        ZJ_JIUZHENXX GetShangJiJZXX(string bingRenID, string jiuZhenID, string keShiID);

        List<ZJ_JIUZHENXX> GetList(List<string> bingRenID, List<string> jiuZhenID, List<string> keShiID);
        /// <summary>
        /// ��ȡʡҽ�����úϼƽ��
        /// </summary>
        /// <param name="sfsb"></param>
        /// <returns></returns>
        string GetShengYiBaoFeiYongHeJiJinE(string sfsb);
        /// <summary>
        /// ���ݲ���ID��ȡ����
        /// </summary>
        /// <param name="bingRenID"></param>
        /// <returns></returns>
        List<ZJ_JIUZHENXX> GetAllZJJZXX(List<string> bingRenID);
    }
}
