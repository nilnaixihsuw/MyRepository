using Mediinfo.DTO.HIS.GY;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Domain;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public static class GY_JIXINGDZFactory
	{
        /*
		 
		public static GY_JIXINGDZ CreateIfNotExists(IGY_JIXINGDZRepository irep, ServiceContext sContext, E_GY_JIXINGDZ dto)
		{
			var entity =  irep.GetByKey("1");
			if(entity == null)
			{
				    entity = new GY_JIXINGDZ();
			}  
			return entity;
		}
		*/

 
        public static GY_JIXINGDZ Create(IGY_JIXINGDZRepository irep,ServiceContext sContext,E_GY_JIXINGDZ EJiXingDZ)
        {  
            var entity = EJiXingDZ.EToDB<E_GY_JIXINGDZ, GY_JIXINGDZ>();
            entity.JIXINGDZID = irep.GetOrder("GY_JIXINGDZ")[0];
            entity.XIUGAISJ = irep.GetSYSTime();
            entity.XIUGAIREN = sContext.USERID;
            irep.RegisterAdd(entity);
            return entity;
        }


}
}
