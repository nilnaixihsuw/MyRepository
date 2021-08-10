using Mediinfo.DTO.HIS.GY;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Domain;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public static class GY_GUANDAOFactory
	{
        /*
		 
		public static GY_GUANDAO CreateIfNotExists(IGY_GUANDAORepository irep, ServiceContext sContext, E_GY_GUANDAO dto)
		{
			var entity =  irep.GetByKey("1");
			if(entity == null)
			{
				    entity = new GY_GUANDAO();
			}  
			return entity;
		}
		*/

        /*
		public static GY_GUANDAO Create(IGY_GUANDAORepository irep,ServiceContext sContext,E_GY_GUANDAO dto )
		{
			GY_GUANDAO entity = new GY_GUANDAO();
			return entity;
		}
		 
		*/


        public static GY_GUANDAO Create(IGY_GUANDAORepository irep, ServiceContext sContext, E_GY_GUANDAO_EX dto)
        {
            GY_GUANDAO entity = new GY_GUANDAO();
            entity = dto.EToDB<E_GY_GUANDAO_EX, GY_GUANDAO>();
            if (string.IsNullOrWhiteSpace(entity.GUANDAOID ))
            {
                entity.GUANDAOID = irep.GetOrder("GY_GUANDAO", sContext.YUANQUID)[0];
            }
            entity.XIUGAIREN = sContext.USERID;
            entity.XIUGAISJ = irep.GetSYSTime();
            entity.Initialize(irep, sContext);
            irep.RegisterAdd(entity);
            return entity;
        }
    }
}
