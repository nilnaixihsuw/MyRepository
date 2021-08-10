using System;
using System.Collections.Generic;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Infrastructure.Core.Entity;
using  Mediinfo.Infrastructure.Core.DBEntity;
namespace Mediinfo.Domain.JCJG.ZY
{
	 public interface IZY_BINGRENXXRepository : IRepository<ZY_BINGRENXX>, IDependency
    { 
        List<ZY_BINGRENXX> GetBingRenXXByBQ(string dangQianBQ);
        List<ZY_BINGRENXX> GetBingRenXX(string BingRenZYID);
        List<ZY_BINGRENXX> GetMuQinYEList(string BingRenZYID);

        List<ZY_BINGRENXX> GetMuQinYEListByBingRenZYID(string BingRenZYID);
        List<ZY_BINGRENXX> GetListByGYFeiYongLB(string BingRenZYID);

        List<ZY_BINGRENXX> QueryGetBingRenXX(string BingRenZYID);
        List<ZY_BINGRENXX> GetYingErXXByMQZYID(string BingRenZYID);

        /// <summary>
        /// ��סԺ��ȡ��Ժ������Ϣ
        /// </summary>
        /// <param name="zhuYuanHao"></param>
        /// <returns></returns>
        List<ZY_BINGRENXX> GetChuYuanBRXX(string zhuYuanHao);

        /// <summary>
        /// ȡסԺ������Ϣ����Ӥ����DesignBy Xieyz 2019-6-10
        /// </summary>
        /// <returns></returns>
        List<ZY_BINGRENXX> GetYingErBRXX();
        /// <summary>
        /// ��סԺ��ȡ��Ժ������Ϣ
        /// </summary>
        /// <param name="zhuYuanHao"></param>
        /// <returns></returns>
        List<ZY_BINGRENXX> GetZaiYuanBRXX(string zhuYuanHao);
         /// <summary>
         /// ��������ȡ��Ժ������Ϣ
         /// </summary>
         /// <param name="bingAnHao"></param>
         /// <returns></returns>
        List<ZY_BINGRENXX> GetZaiYuanBR(string bingAnHao);

        /// <summary>
        /// ��סԺ�Ų�ѯ������Ϣ
        /// </summary>
        /// <param name="zhuYuanHao"></param>
        /// <returns></returns>
        List<ZY_BINGRENXX> GetList(string zhuYuanHao);
        List<ZY_BINGRENXX> GetBingRenIDList(string bingRenZYID);
         List<ZY_BINGRENXX> GetList();
        List<ZY_BINGRENXX> Get_BINGRENXXs(string keShiID,string bingQuID,DateTime beginTime);
        List<ZY_BINGRENXX> GetBingRenIDList(List<string> bingRenZYIDlst);
        List<ZY_BINGRENXX> GetLvSeTDBRXX();
        List<ZY_BINGRENXX> GetBingRenXXByKS(string dangQianKS);
        List<ZY_BINGRENXX> GetYingErXXByZYIDList(List<string> bingRenZYIDList);
        /// <summary>
        /// ҽ��վ��ܻ�ȡ������Ϣ
        /// </summary>
        /// <param name="bingRenZyID"></param>
        /// <returns></returns>
        ZY_BINGRENXX GetKJBingRenXX(string bingRenZyID);
        /// <summary>
        /// ҽ����ܻ�Ӥ����Ϣ
        /// </summary>
        /// <param name="bingRenZyID"></param>
        /// <returns></returns>
        ZY_YINGERXX GetKJYingErXX(string bingRenZyID);
        /// <summary>
        /// ��ȡ������Ϣ����
        /// </summary>
        /// <param name="bingRenID"></param>
        /// <returns></returns>
        List<ZY_BINGRENXX> GetAllZYBRXX(List<string> bingRenID);
    }
}
