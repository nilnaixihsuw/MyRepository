using Mediinfo.DTO.HIS.GY;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Domain;
using Mediinfo.Utility;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
    public static class GY_CHUANGWEIZU1Factory
    {
        /*
		 
		public static GY_CHUANGWEIZU1 CreateIfNotExists(IGY_CHUANGWEIZU1Repository irep, ServiceContext sContext, E_GY_CHUANGWEIZU1 dto)
		{
			var entity =  irep.GetByKey("1");
			if(entity == null)
			{
				    entity = new GY_CHUANGWEIZU1();
			}  
			return entity;
		}
		*/

        /*
		public static GY_CHUANGWEIZU1 Create(IGY_CHUANGWEIZU1Repository irep,ServiceContext sContext,E_GY_CHUANGWEIZU1 dto )
		{
			GY_CHUANGWEIZU1 entity = new GY_CHUANGWEIZU1();
			return entity;
		}
		 
		*/

        public static GY_CHUANGWEIZU1 Create(IGY_CHUANGWEIZU1Repository irep, ServiceContext sContext, E_GY_CHUANGWEIZU1 dto)
        {
            GY_CHUANGWEIZU1 entity = dto.EToDB<E_GY_CHUANGWEIZU1, GY_CHUANGWEIZU1>();
            entity.Initialize(irep, sContext);

            entity.CHUANGWEIZUID = irep.GetOrder("GY_CHUANGWEIZU1", sContext.YUANQUID)[0].ToString();
            //jieSuan2Entity.JIESUANMXID = irep.GetOrder("ZY_JIESUAN2", sContext.YUANQUID)[0].ToString();
            string shuRuMa1 = string.Empty;
            string shuRuMa2 = string.Empty;
            string shuRuMa3 = string.Empty;
            ShuRuMaHelper.GetShuRuMa(entity.CHUANGWEIZUMC, out shuRuMa1, out shuRuMa2, out shuRuMa3);
            entity.SHURUMA1 = shuRuMa1;
            entity.SHURUMA2 = shuRuMa2;
            entity.SHURUMA3 = shuRuMa3;
            entity.XIUGAISJ = irep.GetSYSTime();
            entity.XIUGAIREN = sContext.USERID;

            irep.RegisterAdd(entity);
            return entity;
        }

    }
}
