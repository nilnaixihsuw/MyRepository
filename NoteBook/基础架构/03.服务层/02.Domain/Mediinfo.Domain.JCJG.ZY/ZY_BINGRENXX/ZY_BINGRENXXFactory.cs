using Mediinfo.DTO.HIS.ZY;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Domain;
using Mediinfo.Utility.Extensions;
using System;
namespace Mediinfo.Domain.JCJG.ZY
{
	public static class ZY_BINGRENXXFactory
	{
        /*
		 
		public static ZY_BINGRENXX CreateIfNotExists(IZY_BINGRENXXRepository irep, ServiceContext sContext, E_ZY_BINGRENXX dto)
		{
			var entity =  irep.GetByKey("1");
			if(entity == null)
			{
				    entity = new ZY_BINGRENXX();
			}  
			return entity;
		}
		*/

        /*
		public static ZY_BINGRENXX Create(IZY_BINGRENXXRepository irep,ServiceContext sContext,E_ZY_BINGRENXX dto )
		{
			ZY_BINGRENXX entity = new ZY_BINGRENXX();
			return entity;
		}
		 
		*/

        public static ZY_BINGRENXX Create(IZY_BINGRENXXRepository irep, ServiceContext sContext, E_ZY_BINGRENXX_EX zyBingRenXXDTO)
        {
    
            zyBingRenXXDTO.XIUGAISJ = irep.GetSYSTime();
            var zyBingRenXX = zyBingRenXXDTO.EToDB<E_ZY_BINGRENXX_EX, ZY_BINGRENXX>();
            zyBingRenXX.Initialize(irep, sContext);
            irep.RegisterAdd(zyBingRenXX);
            return zyBingRenXX;
        }

        public static ZY_BINGRENXX Create(IZY_BINGRENXXRepository irep, ServiceContext sContext, E_ZY_BINGRENXX zyBingRenXXDTO)
        {

            zyBingRenXXDTO.XIUGAISJ = irep.GetSYSTime();
            var zyBingRenXX = zyBingRenXXDTO.EToDB<E_ZY_BINGRENXX, ZY_BINGRENXX>();
            if (zyBingRenXX.BINGRENZYID.IsNullOrWhiteSpace())
            {
                zyBingRenXX.BINGRENZYID = irep.GetOrder("ZY_BINGRENXX")[0];
            }
            zyBingRenXX.Initialize(irep, sContext);
            irep.RegisterAdd(zyBingRenXX);
            return zyBingRenXX;
        }


    }
}
