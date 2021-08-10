using System;
using System.Collections.Generic;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Infrastructure.Core.Entity;
using  Mediinfo.Infrastructure.Core.DBEntity;
namespace Mediinfo.Domain.JCJG.GY
{
	 public interface IGY_ZIJINMXZRepository : IRepository<GY_ZIJINMXZ>, IDependency
	{
        /// <summary>
        /// �����˻�idȡ�ʽ���ϸ����Ϣ
        /// </summary>
        /// <param name="zhangHuID">�˻�id</param>
        /// <returns>�ʽ���ϸ��list</returns>
        List<GY_ZIJINMXZ> GetList(string zhangHuID);

        /// <summary>
        /// �����˻�idȡδ��ת�ʽ���ϸ����Ϣ
        /// </summary>
        /// <param name="zhangHuID"></param>
        /// <returns></returns>
        List<GY_ZIJINMXZ> GetWeiJieZMXZList(string zhangHuID);
        /// <summary>
        /// �����˻�idȡ���еķ�������ܺ�
        /// </summary>
        /// <param name="zhangHuID">�˻�id</param>
        /// <returns>��������ܺ�</returns>
        decimal GetFaShengJE(string zhangHuID);


        /// <summary>
        /// �����˻�idȡ����Ԥ����ķ�������ܺͣ�JIAOYIFS=="3"������Ԥ����   9��Ƿ�ѹ���
        /// </summary>
        /// <param name="zhangHuID">�˻�id</param>
        /// <returns>����Ԥ����ķ�������ܺ�</returns>
        decimal GetMZYuJiaoKJE(string zhangHuID);

        /// <summary>
        /// �����˻�id Ƿ�ѱ�־ ȡ��ֵ��ϸ��
        /// </summary>
        /// <param name="zhangHuID"></param>
        /// <param name="qianFeiBZ"></param>
        /// <returns></returns>
        List<GY_ZIJINMXZ> GetChongZhiMXZ(string zhangHuID, int qianFeiBZ);

        /// <summary>
        /// �����˻�id ֧����ʽ��ȡ�ֱ�־��Ƿ�ѱ�־ ȡ��ֵ��ϸ��
        /// </summary>
        /// <param name="zhangHuID"></param>
        /// <param name="zhiFuFS"></param>
        /// <param name="quXianBZ"></param>
        /// <param name="qianFeiBZ"></param>
        /// <returns></returns>
        List<GY_ZIJINMXZ> GetChongZhiMXZ(string zhangHuID, string zhiFuFS, int quXianBZ, int qianFeiBZ);

        /// <summary>
        /// �����˻�ID ����ID,Ƿ�ѱ�־��ȡҪ�˷ѵ� �ʽ���ϸ��
        /// </summary>
        /// <param name="zhangHuID"></param>
        /// <param name="jieSuanID"></param>
        /// <param name="qianFeiZT"></param>
        /// <returns></returns>
        List<GY_ZIJINMXZ> GetTuiFeiZJMXZ(string zhangHuID, string jieSuanID, int qianFeiZT);
    }
}
