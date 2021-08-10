using Mediinfo.Domain.JCJG.GY;
using Mediinfo.DTO.HIS.GY;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Domain;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public static class GY_HUIZHENZU1Factory
	{
        /*
		 
		public static GY_HUIZHENZU1 CreateIfNotExists(IGY_HUIZHENZU1Repository irep, ServiceContext sContext, E_GY_HUIZHENZU1 dto)
		{
			var entity =  irep.GetByKey("1");
			if(entity == null)
			{
				    entity = new GY_HUIZHENZU1();
			}  
			return entity;
		}
		*/

        /*
		public static GY_HUIZHENZU1 Create(IGY_HUIZHENZU1Repository irep,ServiceContext sContext,E_GY_HUIZHENZU1 dto )
		{
			GY_HUIZHENZU1 entity = new GY_HUIZHENZU1();
			return entity;
		}
		 
		*/
        public static GY_HUIZHENZU1 Create(IGY_HUIZHENZU1Repository irep, ServiceContext sContext, E_HL_HZXZ_XZXX dto)
        {
            GY_HUIZHENZU1 entity = dto.EToDB<E_HL_HZXZ_XZXX, GY_HUIZHENZU1>();
            entity.Initialize(irep, sContext);
            if (string.IsNullOrWhiteSpace(entity.HUIZHENZID))
            {
                entity.HUIZHENZID = irep.GetOrder("GY_HUIZHENZID", sContext.YUANQUID)[0].ToString();
            }
            entity.XIUGAIREN = sContext.USERID;
            entity.XIUGAISJ = irep.GetSYSTime();
            irep.RegisterAdd(entity);
            return entity;
        }
    }
}
