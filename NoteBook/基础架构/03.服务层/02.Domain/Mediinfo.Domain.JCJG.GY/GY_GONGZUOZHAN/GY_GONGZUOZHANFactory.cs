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
        /// 通过网卡信息获取工作站信息（如果不存在则创建）
        /// </summary>
        /// <param name="db"></param>
        /// <param name="sConText"></param>
        /// <param name="networkList">网卡地址列表</param>
        /// <returns></returns>
        public static GY_GONGZUOZHAN CreateIfNotExists(IGY_GONGZUOZHANRepository irep, string userId, ServiceContext sContext, List<NetworkConfig> networkList)
        {
            // 定义客户端局域网IP
            string ip = String.Empty;

            // 去网卡类型是以太网的类型(包含网线、虚拟网络)，如果网卡名称是以太网则默认使用的是网线
            var model = networkList.Where(p => p.NetworkInterfaceType == NetworkInterfaceType.Ethernet && p.Name == "以太网").FirstOrDefault();
            if (model != null)
            {
                ip = model.Ip;
            }

            // 如果网卡地址为空则默认使用第一个能用的电脑IP
            if (String.IsNullOrEmpty(ip))
            {
                ip = networkList.FirstOrDefault(p => !String.IsNullOrEmpty(p.Ip)).Ip;
            }

            // 根据修改时间默认取第一个
            var gongZuoZhan = irep.GetList(ip, 1).OrderByDescending(c => c.XIUGAISJ).FirstOrDefault();
            if (null == gongZuoZhan) // 如果不存在则自动注册
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
                throw new DomainException(string.Format("工作站{0}已经被禁用！", gongZuoZhan.GONGZUOZID));
            }
            else
            {
                // 如果找到的话，需要将当前注册的网卡地址、计算机名 更新成最新的
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
