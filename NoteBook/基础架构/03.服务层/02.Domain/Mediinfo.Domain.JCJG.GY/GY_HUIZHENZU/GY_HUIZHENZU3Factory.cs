using Mediinfo.DTO.HIS.GY;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Domain;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public static class GY_HUIZHENZU3Factory
	{
        /*
		 
		public static GY_HUIZHENZU3 CreateIfNotExists(IGY_HUIZHENZU3Repository irep, ServiceContext sContext, E_GY_HUIZHENZU3 dto)
		{
			var entity =  irep.GetByKey("1");
			if(entity == null)
			{
				    entity = new GY_HUIZHENZU3();
			}  
			return entity;
		}
		*/

        /*
		public static GY_HUIZHENZU3 Create(IGY_HUIZHENZU3Repository irep,ServiceContext sContext,E_GY_HUIZHENZU3 dto )
		{
			GY_HUIZHENZU3 entity = new GY_HUIZHENZU3();
			return entity;
		}
		 
		*/
        public static GY_HUIZHENZU3 Create(IGY_HUIZHENZU3Repository irep, ServiceContext sContext, E_HL_HZXZ_HuiZhenLB dto)
        {
            GY_HUIZHENZU3 entity = dto.EToDB<E_HL_HZXZ_HuiZhenLB, GY_HUIZHENZU3>();
            entity.Initialize(irep, sContext);
            if (string.IsNullOrWhiteSpace(entity.HUIZHENLBID))
            {
                entity.HUIZHENLBID = irep.GetOrder("GY_HUIZHENLBID", sContext.YUANQUID)[0].ToString();
            }
            entity.XIUGAIREN = sContext.USERID;
            entity.XIUGAISJ = irep.GetSYSTime();
            irep.RegisterAdd(entity);
            return entity;
        }
    }
}
