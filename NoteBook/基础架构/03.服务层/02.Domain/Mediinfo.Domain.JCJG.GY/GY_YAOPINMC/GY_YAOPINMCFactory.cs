using Mediinfo.DTO.HIS.GY;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Domain;

namespace Mediinfo.Domain.JCJG.GY
{
    public static class GY_YAOPINMCFactory
	{ 

        /*
		 
		public static GY_YAOPINMC CreateIfNotExists(IGY_YAOPINMCRepository irep, ServiceContext sContext, E_GY_YAOPINMC dto)
		{
			var entity =  irep.GetByKey("1");
			if(entity == null)
			{
				    entity = new GY_YAOPINMC();
			}  
			return entity;
		}
		*/


        public static GY_YAOPINMC Create(IGY_YAOPINMCRepository irep, ServiceContext sContext, E_GY_YAOPINMC_EX yaoPinMCDto)
        {
            GY_YAOPINMC entity = new GY_YAOPINMC();

            entity = yaoPinMCDto.EToDB<E_GY_YAOPINMC_EX, GY_YAOPINMC>();

            entity.XIUGAISJ = irep.GetSYSTime();
            entity.XIUGAIREN = sContext.USERID;
            entity.YAOPINID = irep.GetOrder("GY_YAOPINMC", sContext.YUANQUID, 1)[0];

            irep.RegisterAdd(entity,true); 

            return entity;
        } 
    }
}
