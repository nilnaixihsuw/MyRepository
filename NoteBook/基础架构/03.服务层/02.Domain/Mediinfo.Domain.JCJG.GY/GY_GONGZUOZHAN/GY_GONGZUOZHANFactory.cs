using Mediinfo.Enterprise;
using Mediinfo.Enterprise.Exceptions;
using Mediinfo.Utility;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;

namespace Mediinfo.Domain.JCJG.GY
{
    public static class GY_GONGZUOZHANFactory
    {
        /// <summary>
        /// ͨ��������Ϣ��ȡ����վ��Ϣ������������򴴽���
        /// </summary>
        /// <param name="db"></param>
        /// <param name="sConText"></param>
        /// <param name="networkList">������ַ�б�</param>
        /// <returns></returns>
        public static GY_GONGZUOZHAN CreateIfNotExists(IGY_GONGZUOZHANRepository irep, string userId, ServiceContext sContext, List<NetworkConfig> networkList)
        {
            // ����ͻ��˾�����IP
            string ip = String.Empty;

            // ȥ������������̫��������(�������ߡ���������)�����������������̫����Ĭ��ʹ�õ�������
            var model = networkList.Where(p => p.NetworkInterfaceType == NetworkInterfaceType.Ethernet && p.Name == "��̫��").FirstOrDefault();
            if (model != null)
            {
                ip = model.Ip;
            }

            // ���������ַΪ����Ĭ��ʹ�õ�һ�����õĵ���IP
            if (String.IsNullOrEmpty(ip))
            {
                ip = networkList.FirstOrDefault(p => !String.IsNullOrEmpty(p.Ip)).Ip;
            }

            // �����޸�ʱ��Ĭ��ȡ��һ��
            var gongZuoZhan = irep.GetList(ip, 1).OrderByDescending(c => c.XIUGAISJ).FirstOrDefault();
            if (null == gongZuoZhan) // ������������Զ�ע��
            {
                gongZuoZhan = new GY_GONGZUOZHAN
                {
                    GONGZUOZID = irep.GetOrder("GY_GONGZUOZHAN", sContext.YUANQUID)[0],
                    GONGZUOZM = networkList[0].ComputerName,
                    JISUANJM = networkList[0].ComputerName,
                    IP = networkList[0].Ip,
                    WANGKADZ = networkList[0].PhysicalAddress,
                    ZUOFEIBZ = 0,
                    XIUGAIREN = userId,
                    XIUGAISJ = irep.GetSYSTime(),
                    MOJIBZ = 1
                };

                irep.RegisterAdd(gongZuoZhan);
            }
            else if (gongZuoZhan.ZUOFEIBZ.HasValue && gongZuoZhan.ZUOFEIBZ.Value == 1)
            {
                throw new DomainException(string.Format("����վ{0}�Ѿ������ã�", gongZuoZhan.GONGZUOZID));
            }
            else
            {
                // ����ҵ��Ļ�����Ҫ����ǰע���������ַ��������� ���³����µ�
                gongZuoZhan.JISUANJM = networkList[0].ComputerName;
                gongZuoZhan.WANGKADZ = networkList[0].PhysicalAddress;
                gongZuoZhan.XIUGAIREN = userId;
                gongZuoZhan.XIUGAISJ = irep.GetSYSTime();
            }

            return gongZuoZhan;
        }

        /*
		public static GY_GONGZUOZHAN Create(IGY_GONGZUOZHANRepository irep,ServiceContext sContext,E_GY_GONGZUOZHAN dto )
		{
			GY_GONGZUOZHAN entity = new GY_GONGZUOZHAN();
			return entity;
		}
		 
		*/

    }
}
