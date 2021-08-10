using Mediinfo.DTO.HIS.GY;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Domain;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public static class GY_JIBINGFactory
	{
        /*
		 
		public static GY_JIBING CreateIfNotExists(IGY_JIBINGRepository irep, ServiceContext sContext, E_GY_JIBING dto)
		{
			var entity =  irep.GetByKey("1");
			if(entity == null)
			{
				    entity = new GY_JIBING();
			}  
			return entity;
		}
		*/

        public static GY_JIBING Create(IGY_JIBINGRepository irep, ServiceContext sContext, E_GY_JIBING EJiBing)
        {
            var entity = EJiBing.EToDB<E_GY_JIBING, GY_JIBING>();
            entity.JIBINGID = irep.GetOrder("GY_JIBINGDM", EJiBing.JIBINGID)[0].ToString();
            entity.XIUGAISJ = irep.GetSYSTime();
            entity.XIUGAIREN = sContext.USERID;
            irep.RegisterAdd(entity);
            return entity;
        }

    }
}
