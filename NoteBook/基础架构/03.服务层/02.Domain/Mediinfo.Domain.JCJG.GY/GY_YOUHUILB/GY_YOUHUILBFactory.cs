using System;
using Mediinfo.DTO.HIS.GY;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Domain;

namespace Mediinfo.Domain.JCJG.GY
{
	public static class GY_YOUHUILBFactory
	{
        /*
		 
		public static GY_YOUHUILB CreateIfNotExists(IGY_YOUHUILBRepository irep, ServiceContext sContext, E_GY_YOUHUILB dto)
		{
			var entity =  irep.GetByKey("1");
			if(entity == null)
			{
				    entity = new GY_YOUHUILB();
			}  
			return entity;
		}
		*/


        public static GY_YOUHUILB Create(IGY_YOUHUILBRepository irep, ServiceContext sContext, E_GY_YOUHUILB dto)
        {
            GY_YOUHUILB entity = dto.EToDB<E_GY_YOUHUILB, GY_YOUHUILB>();
            entity.Initialize(irep, sContext);
            entity.XIUGAIREN = sContext.USERID;
            entity.XIUGAISJ = irep.GetSYSTime();
            irep.RegisterAdd(entity);
            return entity;
        }



    }
}
