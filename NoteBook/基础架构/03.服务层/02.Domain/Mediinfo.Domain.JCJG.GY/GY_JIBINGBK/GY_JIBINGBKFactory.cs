using Mediinfo.DTO.HIS.GY;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Domain;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public static class GY_JIBINGBKFactory
	{
        /*
		 
		public static GY_JIBINGBK CreateIfNotExists(IGY_JIBINGBKRepository irep, ServiceContext sContext, E_GY_JIBINGBK dto)
		{
			var entity =  irep.GetByKey("1");
			if(entity == null)
			{
				    entity = new GY_JIBINGBK();
			}  
			return entity;
		}
		*/

        /*
		public static GY_JIBINGBK Create(IGY_JIBINGBKRepository irep,ServiceContext sContext,E_GY_JIBINGBK dto )
		{
			GY_JIBINGBK entity = new GY_JIBINGBK();
			return entity;
		}
		 
		*/

        public static GY_JIBINGBK Create(IGY_JIBINGBKRepository irep, ServiceContext sContext, E_GY_JIBINGBK EJiBing)
        {
            var entity = EJiBing.EToDB<E_GY_JIBINGBK, GY_JIBINGBK>();
            entity.BAOKAID = irep.GetOrder("GY_JIBINGBK", EJiBing.BAOKAID)[0].ToString();
            entity.XITONGSJ= irep.GetSYSTime();
            entity.YINGYONGID = sContext.YINGYONGID;
            entity.YUANQUID = sContext.YUANQUID;
            irep.RegisterAdd(entity);
            return entity;
        }

    }
}
