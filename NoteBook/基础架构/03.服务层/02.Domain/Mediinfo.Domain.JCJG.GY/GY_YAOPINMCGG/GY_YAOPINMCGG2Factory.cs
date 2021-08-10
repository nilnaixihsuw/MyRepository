using Mediinfo.DTO.HIS.GY;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Domain;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public static class GY_YAOPINMCGG2Factory
	{
        /*
		 
		public static GY_YAOPINMCGG2 CreateIfNotExists(IGY_YAOPINMCGG2Repository irep, ServiceContext sContext, E_GY_YAOPINMCGG2 dto)
		{
			var entity =  irep.GetByKey("1");
			if(entity == null)
			{
				    entity = new GY_YAOPINMCGG2();
			}  
			return entity;
		}
		*/ 


        /// <summary>
        /// 创建药品规格
        /// </summary>
        /// <param name="DBContext"></param>
        /// <param name="sContext"></param>
        /// <param name="EYaoPinGG"></param>
        /// <returns></returns>
        public static GY_YAOPINMCGG2 Create(IGY_YAOPINMCGG2Repository irep, ServiceContext sContext, E_GY_YAOPINMCGG2_EX EYaoPinGG)
        { 
            var yaoPinMCGG = EYaoPinGG.EToDB<E_GY_YAOPINMCGG2_EX, GY_YAOPINMCGG2>();
            
            yaoPinMCGG.XIUGAISJ = irep.GetSYSTime();
            yaoPinMCGG.XIUGAIREN = sContext.USERID;
            string guiGeID = irep.GetOrder("GY_YAOPINMCGG2", sContext.YUANQUID, 1)[0];
            yaoPinMCGG.GUIGEID = guiGeID;
            yaoPinMCGG.DAGUIGID = guiGeID;

            irep.RegisterAdd(yaoPinMCGG);
            return yaoPinMCGG;
        }

    }
}
