using Mediinfo.DTO.HIS.GY;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Domain;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public static class GY_ZIJINMXZFactory
	{
        
        /*
		 
		public static GY_ZIJINMXZ CreateIfNotExists(IGY_ZIJINMXZRepository irep, ServiceContext sContext, E_GY_ZIJINMXZ dto)
		{
			var entity =  irep.GetByKey("1");
			if(entity == null)
			{
				    entity = new GY_ZIJINMXZ();
			}  
			return entity;
		}
		*/


        public static GY_ZIJINMXZ Create(IGY_ZIJINMXZRepository irep, ServiceContext sContext, E_GY_ZIJINMXZ dto)
        {
            var ziJinMXZ = dto.EToDB<E_GY_ZIJINMXZ, GY_ZIJINMXZ>();

            ziJinMXZ.Initialize(irep, sContext);
            if (string.IsNullOrEmpty(ziJinMXZ.ZIJINMXZID))
            {
                ziJinMXZ.ZIJINMXZID = irep.GetOrder("GY_ZIJINMXZ", sContext.YUANQUID)[0];//ȡ�������ֵ����һ�����������һ����ֵ�����ֵ��
                ziJinMXZ.YUANZIJMXZID = ziJinMXZ.ZIJINMXZID;
            }

            ziJinMXZ.CHONGXIAOBZ = 0;
            //����ط�����д����Ҫ����ҵ�����
            //ziJinMXZ.JIAOYILX = 1;//������
            //ziJinMXZ.JIAOYIFS = "3";//����Ԥ����

            ziJinMXZ.CAOZUOYUAN = sContext.USERID;
            ziJinMXZ.YINGYONGID = sContext.YINGYONGID;
            ziJinMXZ.CAOZUORQ = irep.GetSYSTime();
            ziJinMXZ.YUANQUID = sContext.YUANQUID;
            irep.RegisterAdd(ziJinMXZ);
            return ziJinMXZ;
        }



    }
}
