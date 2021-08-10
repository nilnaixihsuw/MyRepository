using Mediinfo.DTO.HIS.GY;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Domain;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public static class GY_ZIJINZHFactory
	{
        /*
		 
		public static GY_ZIJINZH CreateIfNotExists(IGY_ZIJINZHRepository irep, ServiceContext sContext, E_GY_ZIJINZH dto)
		{
			var entity =  irep.GetByKey("1");
			if(entity == null)
			{
				    entity = new GY_ZIJINZH();
			}  
			return entity;
		}
		*/


        public static GY_ZIJINZH Create(IGY_ZIJINZHRepository irep, ServiceContext sContext, E_GY_ZIJINZH dto)
        {
            var gYZiJinZH = dto.EToDB<E_GY_ZIJINZH, GY_ZIJINZH>();

            gYZiJinZH.Initialize(irep, sContext);
            if (string.IsNullOrEmpty(gYZiJinZH.ZHANGHUID))
            {
                gYZiJinZH.ZHANGHUID = irep.GetOrder("GY_ZIJINZH", sContext.YUANQUID)[0];//取表的主键值，给一个表的主键的一个新值（最大值）
            }

            gYZiJinZH.QICHUJE = 0;
            gYZiJinZH.ZENGJIAJE = 0;
            gYZiJinZH.JIANSHAOJE = 0;
            gYZiJinZH.QIMOJE = 0;

            //gYZiJinZH.GERENDWBZ = 1;
            //gYZiJinZH.XIANJINQBZ = 0;
            //gYZiJinZH.ZHANGHUDJ = "1";
            gYZiJinZH.ZHANGHUZT = 0;
            gYZiJinZH.QIANFEIZT = 0;

            irep.RegisterAdd(gYZiJinZH);
            return gYZiJinZH;
        }



    }
}
