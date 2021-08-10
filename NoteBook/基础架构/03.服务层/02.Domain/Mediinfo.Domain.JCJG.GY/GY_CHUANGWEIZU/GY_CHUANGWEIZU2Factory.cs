using Mediinfo.DTO.HIS.GY;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Domain;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public static class GY_CHUANGWEIZU2Factory
	{
        /*
		 
		public static GY_CHUANGWEIZU2 CreateIfNotExists(IGY_CHUANGWEIZU2Repository irep, ServiceContext sContext, E_GY_CHUANGWEIZU2 dto)
		{
			var entity =  irep.GetByKey("1");
			if(entity == null)
			{
				    entity = new GY_CHUANGWEIZU2();
			}  
			return entity;
		}
		*/

        /*
		public static GY_CHUANGWEIZU2 Create(IGY_CHUANGWEIZU2Repository irep,ServiceContext sContext,E_GY_CHUANGWEIZU2 dto )
		{
			GY_CHUANGWEIZU2 entity = new GY_CHUANGWEIZU2();
			return entity;
		}
		 
		*/

        public static GY_CHUANGWEIZU2 Create(IGY_CHUANGWEIZU2Repository irep, ServiceContext sContext, E_GY_CHUANGWEIZU2 dto)
        {
            GY_CHUANGWEIZU2 entity = dto.EToDB<E_GY_CHUANGWEIZU2, GY_CHUANGWEIZU2>();
            entity.Initialize(irep, sContext);

            //entity.CHUANGWEIZUID = irep.GetOrder("GY_CHUANGWEIZU1", sContext.YUANQUID)[0].ToString();
            //jieSuan2Entity.JIESUANMXID = irep.GetOrder("ZY_JIESUAN2", sContext.YUANQUID)[0].ToString();

            entity.XIUGAISJ = irep.GetSYSTime();
            entity.XIUGAIREN = sContext.USERID;

            irep.RegisterAdd(entity);
            return entity;
        }

        public static GY_CHUANGWEIZU3 Create(IGY_CHUANGWEIZU3Repository irep, ServiceContext sContext, E_GY_CHUANGWEIZU3 dto)
        {
            GY_CHUANGWEIZU3 entity = dto.EToDB<E_GY_CHUANGWEIZU3, GY_CHUANGWEIZU3>();
            entity.Initialize(irep, sContext);

            //entity.CHUANGWEIZUID = irep.GetOrder("GY_CHUANGWEIZU1", sContext.YUANQUID)[0].ToString();
            //jieSuan2Entity.JIESUANMXID = irep.GetOrder("ZY_JIESUAN2", sContext.YUANQUID)[0].ToString();

            entity.XIUGAISJ = irep.GetSYSTime();
            entity.XIUGAIREN = sContext.USERID;

            irep.RegisterAdd(entity);
            return entity;
        }

    }
}
