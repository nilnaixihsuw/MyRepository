using System;
using System.Collections.Generic;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Infrastructure.Core.Entity;
namespace Mediinfo.Domain.JCJG.GY
{
	 public interface IGY_YAOPINCDJG2Repository : IRepository<GY_YAOPINCDJG2>, IDependency
	{
        void SetCache();
        /// <summary>
        /// ���ݹ��ID������ȡ���ؼ۸���Ϣ
        /// </summary>
        /// <param name="guiGeID"></param>
        /// <param name="chanDi"></param>
        /// <returns></returns>
        List<GY_YAOPINCDJG2> GetList(string guiGeID,string chanDi);

        /// <summary>
        /// ���ݼ۸�ID��ȡ����
        /// </summary>
        /// <param name="jIAGEID"></param>
        /// <returns></returns>
        List<GY_YAOPINCDJG2> GetList(string jIAGEID);
        List<GY_YAOPINCDJG2> GetList(List<string> jIAGEID);


        List<GY_YAOPINCDJG2> GetChanDi(string chanDi);

        List<GY_YAOPINCDJG2> GetList(int zfBZ);

        /// <summary>
        /// ҩƷ��Ϣ
        /// </summary>
        /// <returns></returns>
        List<GY_YAOPINCDJG2> GetList();

        List<GY_YAOPINCDJG2> GetList(string xtGGID, string cd, int zfBZ = 0, int tyBZ = 0);

        /// <summary>
        /// ���ݹ��ID������ȡ���ؼ۸���Ϣ(ͨ������)
        /// </summary>
        /// <param name="guiGeID"></param>
        /// <param name="chanDi"></param>
        /// <returns></r
        List<GY_YAOPINCDJG2> GetListFromCache(string guiGeID, string chanDi);

        /// <summary>
        /// ��ȡͬһ����ͬһ���ص�����ҩƷ
        /// </summary>
        /// <param name="daGuiGeID"></param>
        /// <param name="chanDi"></param>
        /// <returns></returns>
        List<GY_YAOPINCDJG2> GetXiangTongYPList(string daGuiGeID, string chanDi);

        /// <summary>
        /// ��ȡͬһ��������ҩƷ
        /// </summary>
        /// <param name="daGuiGeID"></param>
        /// <returns></returns>

        List<GY_YAOPINCDJG2> GetListByDaGuiGeID(string daGuiGeID);
        /// <summary>
        /// ͨ�������ȡҩƷ��Ϣ
        /// </summary>
        /// <param name="jiaGeID"></param>
        /// <returns></returns>
        GY_YAOPINCDJG2 GetByKeyFromCache(string jiaGeID);

        /// <summary>
        /// ͨ������ȡҩƷ������Ϣ
        /// </summary>
        /// <param name="jiaGeID"></param>
        /// <returns></returns>
        List<GY_YAOPINCDJG2> GetListByGuiGeID(string guiGeID);

        List<GY_YAOPINCDJG2> GetTongGuiGeList(string jiaGeID);

        /// <summary>
        /// �������۱�־��ȡ���ؼ۸�2��Ϣ
        /// </summary>
        /// <param name="lingChaJBZ"></param>
        /// <returns></returns>
        List<GY_YAOPINCDJG2> GetListByLingChaJBZ(List<int?> lingChaJBZ);


        /// <summary>
        /// ���ݲ��ؼ����id��ȡ���ؼ۸�2��Ϣ
        /// </summary>
        /// <param name="chanDi">����</param>
        /// <param name="guiGeIDList">���ID�б�</param>
        /// <returns></returns>
        List<GY_YAOPINCDJG2> GetListByChanDiGGID(string chanDi, List<string> guiGeIDList);



        /// <summary>
        ///  ����YK_CHUKUDAN1��YK_CHUKUDAN2��GY_YAOPINMC��GY_YAOPINFL��ѯҩƷ���ؼ۸��
        /// </summary>
        /// <param name="yingYongID"></param>
        /// <param name="kaiShiSJ"></param>
        /// <param name="jieShuSJ"></param>
        /// <param name="danJuZT"></param>
        /// <param name="gongHuoBZ"></param>
        /// <param name="zuoFeiBZ"></param>
        /// <param name="tingYongBZ"></param>
        /// <returns></returns>
        List<GY_YAOPINCDJG2> GetList(string yingYongID, DateTime kaiShiSJ, DateTime jieShuSJ, string danJuZT, int gongHuoBZ, int zuoFeiBZ, int tingYongBZ);

        /// <summary>
        /// ����YK_CHUKUDAN1��YK_CHUKUDAN2��ѯҩƷ���ؼ۸��
        /// </summary>
        /// <param name="yingYongID"></param>
        /// <param name="kaiShiSJ"></param>
        /// <param name="jieShuSJ"></param>
        /// <param name="danJuZT"></param>
        /// <returns></returns>
        List<GY_YAOPINCDJG2> GetList(string yingYongID, DateTime kaiShiSJ, DateTime jieShuSJ, string danJuZT);

        /// <summary>
        /// ����GY_QINGLINGDAN1��GY_QINGLINGDAN2���������Ͳ�ѯҩƷ���ؼ۸��
        /// </summary>
        /// <param name="yingYongID"></param>
        /// <param name="kaiShiSJ"></param>
        /// <param name="jieShuSJ"></param>
        /// <param name="qingLingZT"></param>
        /// <param name="qingLingLX"></param>
        /// <returns></returns>
        List<GY_YAOPINCDJG2> GetList(string yingYongID, DateTime kaiShiSJ, DateTime jieShuSJ, string qingLingZT, string qingLingLX);

        /// <summary>
        /// ����YK_KUCUN1��GY_YAOPINMC��GY_YAOPINFL��ѯҩƷ���ؼ۸��
        /// </summary>
        /// <param name="yingYongID"></param>
        /// <param name="gongHuoBZ"></param>
        /// <param name="tingYongBZ"></param>
        /// <param name="zuoFeiBZ"></param>
        /// <returns></returns>
        List<GY_YAOPINCDJG2> GetList(string yingYongID, int gongHuoBZ, int tingYongBZ, int zuoFeiBZ);

        /// <summary>
        /// ����GY_QINGLINGDAN1��GY_QINGLINGDAN2��ѯҩƷ���ؼ۸��
        /// </summary>
        /// <param name="yingYongID"></param>
        /// <param name="kaiShiSJ"></param>
        /// <param name="jieShuSJ"></param>
        /// <param name="qingLingZT"></param>
        /// <returns></returns>
        List<GY_YAOPINCDJG2> GetYaoPinCDJG2ByQL(string yingYongID, DateTime kaiShiSJ, DateTime jieShuSJ, string qingLingZT);

        /// <summary>
        /// ���ݼ۸�id�б��ȡ���ؼ۸�2��Ϣ
        /// </summary>
        /// <param name="jiaGeIDList"></param>
        /// <returns></returns>
        List<GY_YAOPINCDJG2> GetList(HashSet<string> jiaGeIDList);

        /// <summary>
        /// ����ҩƷ���ͺ�ID��ȡ���ؼ۸�2��Ϣ
        /// </summary>
        /// <param name="weiShu"></param>
        /// <param name="yaoPinLX"></param>
        /// <param name="yaoPinID"></param>
        /// <returns></returns>
        List<GY_YAOPINCDJG2> GetList(int weiShu, List<string> yaoPinLX, List<string> yaoPinID);

        List<GY_YAOPINCDJG2> GetYaoPinLst(List<string> lstguiGeID);

        List<GY_YAOPINCDJG2> GetListByGuiGeIDQ(string guiGeID);
    }
}
