using System;
using Mediinfo.DTO.HIS.GY;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Domain;

namespace Mediinfo.Domain.JCJG.GY
{
	public static class GY_XIANGMUSPFactory
	{
        /*
		 
		public static GY_XIANGMUSP CreateIfNotExists(IGY_XIANGMUSPRepository irep, ServiceContext sContext, E_GY_XIANGMUSP dto)
		{
			var entity =  irep.GetByKey("1");
			if(entity == null)
			{
				    entity = new GY_XIANGMUSP();
			}  
			return entity;
		}
		*/


        public static GY_XIANGMUSP Create(IGY_XIANGMUSPRepository irep, ServiceContext sContext, E_GY_XIANGMUSP dto)
        {
            GY_XIANGMUSP entity = dto.EToDB<E_GY_XIANGMUSP, GY_XIANGMUSP>();
            entity.Initialize(irep, sContext);
            entity.SHENPIID = irep.GetOrder("GY_XIANGMUSP")[0];
            entity.XIUGAIREN = sContext.USERID;
            entity.XIUGAISJ = irep.GetSYSTime();
            irep.RegisterAdd(entity);
            return entity;
        }



    }
}
