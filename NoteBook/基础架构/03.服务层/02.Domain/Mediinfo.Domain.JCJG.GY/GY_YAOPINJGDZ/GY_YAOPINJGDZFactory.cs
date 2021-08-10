using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Domain;

namespace Mediinfo.Domain.JCJG.GY
{
    public static class GY_YAOPINJGDZFactory
	{
        /*
		 
		public static GY_YAOPINJGDZ CreateIfNotExists(IGY_YAOPINJGDZRepository irep, ServiceContext sContext, E_GY_YAOPINJGDZ dto)
		{
			var entity =  irep.GetByKey("1");
			if(entity == null)
			{
				    entity = new GY_YAOPINJGDZ();
			}  
			return entity;
		}
		*/

 

        //public static GY_YAOPINJGDZ Create(IGY_YAOPINJGDZRepository irep, ServiceContext sContext, E_GY_YAOPINJGDZ dto)
        //{   
             
        //    var yaoPinJGDZ = dto.EToDB<E_GY_YAOPINJGDZ, GY_YAOPINJGDZ>();
        //    irep.RegisterAdd(yaoPinJGDZ);
        //    return yaoPinJGDZ;
        //} 

        public static GY_YAOPINJGDZ Create(IGY_YAOPINJGDZRepository irep, ServiceContext sContext, string  jiaGeID1,  decimal jiaGe1, string jiaGeID2, decimal jiaGe2,int jiaGeLX)
        {
            var entity = new GY_YAOPINJGDZ();
            entity.Initialize(irep, sContext);
            entity.JIAGEID1 = jiaGeID1;
            entity.JIAGE1 = jiaGe1;
            entity.JIAGEID2 = jiaGeID2;
            entity.JIAGE2 = jiaGe2;
            entity.JIAGELX = jiaGeLX;
            irep.RegisterAdd(entity);
             
           
            return entity;
        }

        public static GY_YAOPINJGDZ CreateIfNotExists(IGY_YAOPINJGDZRepository irep, ServiceContext sContext, string jiaGeID1, decimal jiaGe1, string jiaGeID2, decimal jiaGe2, int jiaGeLX)
        {
            var entity = irep.GetByKey(jiaGeID1, jiaGe1, jiaGeID2, jiaGe2, jiaGeLX);
            if (entity == null)
            {
                entity = new GY_YAOPINJGDZ();
                entity.Initialize(irep, sContext);
                entity.JIAGEID1 = jiaGeID1;
                entity.JIAGE1 = jiaGe1;
                entity.JIAGEID2 = jiaGeID2;
                entity.JIAGE2 = jiaGe2;
                entity.JIAGELX = jiaGeLX;
                irep.RegisterAdd(entity);
            }
            return entity;
        }


    }
}
