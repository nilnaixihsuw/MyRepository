using Mediinfo.DTO.HIS.GY;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Domain;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public static class GY_HUIZHENZU2Factory
	{
        /*
		 
		public static GY_HUIZHENZU2 CreateIfNotExists(IGY_HUIZHENZU2Repository irep, ServiceContext sContext, E_GY_HUIZHENZU2 dto)
		{
			var entity =  irep.GetByKey("1");
			if(entity == null)
			{
				    entity = new GY_HUIZHENZU2();
			}  
			return entity;
		}
		*/

        /*
		public static GY_HUIZHENZU2 Create(IGY_HUIZHENZU2Repository irep,ServiceContext sContext,E_GY_HUIZHENZU2 dto )
		{
			GY_HUIZHENZU2 entity = new GY_HUIZHENZU2();
			return entity;
		}
		 
		*/
        public static GY_HUIZHENZU2 Create(IGY_HUIZHENZU2Repository irep, ServiceContext sContext, E_HL_HZXZ_ZhiGongXX dto)
        {
            GY_HUIZHENZU2 entity = dto.EToDB<E_HL_HZXZ_ZhiGongXX, GY_HUIZHENZU2>();
            entity.Initialize(irep, sContext);
            if (string.IsNullOrWhiteSpace(entity.ZHIGONGID))
            {
                entity.ZHIGONGID = irep.GetOrder("GY_ZHIGONGID", sContext.YUANQUID)[0].ToString();
            }
            entity.XIUGAIREN = sContext.USERID;
            entity.XIUGAISJ = irep.GetSYSTime();
            irep.RegisterAdd(entity);
            return entity;
        }
    }
}
