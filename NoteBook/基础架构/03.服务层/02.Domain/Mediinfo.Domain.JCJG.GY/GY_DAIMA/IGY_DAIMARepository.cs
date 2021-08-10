using System;
using System.Collections.Generic;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Infrastructure.Core.Entity;
using  Mediinfo.Infrastructure.Core.DBEntity;
namespace Mediinfo.Domain.JCJG.GY
{
	 public interface IGY_DAIMARepository : IRepository<GY_DAIMA>, IDependency
	{
        List<GY_DAIMA> GetList(string daiMaLB, string daiMaId);

        /// <summary>
        /// ���ݴ�������ȡ������Ϣ
        /// </summary>
        /// <param name="daiMaLB"></param>
        /// <returns></returns>
        List<GY_DAIMA> GetList(string daiMaLB);

        /// <summary>
        /// ��ȡ����Ƶ������
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        int? GetPinCiLX(string key);


        /// <summary>
        /// ���ÿؼ�ȡ��������
        /// </summary>
        /// <returns></returns>
        List<GY_DAIMA> GetGYList(string DAIMALB, int MENZHENSY, int ZHUYUANSY, int ZUOFEIBZ);

	    string GetGuaHaoLB(string leiBieGL);

	    int GetZiFu1(string S_GUAHAOLB);

        /// <summary>
        /// ȡ����ҽ����Ƶ������
        /// </summary>
        /// <param name="pinCiLX"></param>
        /// <returns></returns>
        List<string> GetYiZhuLXList(int pinCiLX);

        /// <summary>
        /// ��Ƶ��IDȡƵ����Ϣ
        /// </summary>
        /// <param name="pinCiLX"></param>
        /// <returns></returns>
        List<GY_DAIMA> GetPinCiList(string pinCiID);

        /// <summary>
        /// ���ݴ�������ȡδ���ϴ�����Ϣ --xieyz 2019-05-31
        /// </summary>
        /// <param name="daiMaLB">�������</param>
        /// <returns></returns>
        List<GY_DAIMA> GetListBydaiMaLB(string daiMaLB);

        /// <summary>
        /// ��ȡ��������
        /// </summary>
        /// <returns></returns>
         List<GY_DAIMA> GetShuHouDMList();

        int GetShunXuHao(string daimalb);

        bool CheckDaiMaID(string daimaid, string daimalb);
    }
}
