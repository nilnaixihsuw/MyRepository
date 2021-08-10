using Mediinfo.DTO.HIS.GY;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Domain;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public static class GY_YAOPINZBMFactory
	{
        /*
		 
		public static GY_YAOPINZBM CreateIfNotExists(IGY_YAOPINZBMRepository irep, ServiceContext sContext, E_GY_YAOPINZBM dto)
		{
			var entity =  irep.GetByKey("1");
			if(entity == null)
			{
				    entity = new GY_YAOPINZBM();
			}  
			return entity;
		}
		*/

        /*
		public static GY_YAOPINZBM Create(IGY_YAOPINZBMRepository irep,ServiceContext sContext,E_GY_YAOPINZBM dto )
		{
			GY_YAOPINZBM entity = new GY_YAOPINZBM();
			return entity;
		}
		 
		*/

        public static GY_YAOPINZBM Create(IGY_YAOPINZBMRepository irep, ServiceContext sContext, E_GY_YAOPINZBM EYaoPinZBM)
        { 
            var yaoPinZBM = EYaoPinZBM.EToDB<E_GY_YAOPINZBM, GY_YAOPINZBM>();
            
            yaoPinZBM.XIUGAISJ = irep.GetSYSTime();
            yaoPinZBM.XIUGAIREN = sContext.USERID;
            yaoPinZBM.ZUOFEIBZ = 0;
            yaoPinZBM.BIEMINGID = irep.GetOrder("GY_YAOPINZBM", sContext.YUANQUID, 1)[0]; 
            irep.RegisterAdd(yaoPinZBM,true);
            return yaoPinZBM;

        } 

        public static GY_YAOPINZBM Create(IGY_YAOPINZBMRepository irep, ServiceContext sContext, string yaoPinMC, string shuRuMa1, string shuRuMa2, string shuRuMa3, string yaoPinID, string guiGeID, string jiaGeID)
        { 
            GY_YAOPINZBM yaoPinZBM = new GY_YAOPINZBM();
            yaoPinZBM.Initialize(irep, sContext); 
            yaoPinZBM.XIUGAISJ = irep.GetSYSTime();
            yaoPinZBM.XIUGAIREN = sContext.USERID;
            yaoPinZBM.ZUOFEIBZ = 0;
            yaoPinZBM.BIEMINGID = irep.GetOrder("GY_YAOPINZBM", sContext.YUANQUID, 1)[0]; 
            yaoPinZBM.YAOPINMC = yaoPinMC;
            yaoPinZBM.SHURUMA1 = shuRuMa1;
            yaoPinZBM.SHURUMA2 = shuRuMa2;
            yaoPinZBM.SHURUMA3 = shuRuMa3;
            yaoPinZBM.YAOPINID = yaoPinID;
            yaoPinZBM.GUIGEID = guiGeID;
            yaoPinZBM.JIAGEID = jiaGeID;
            irep.RegisterAdd(yaoPinZBM,true);
            return yaoPinZBM;
        }
    }
}
