using Mediinfo.DTO.HIS.ZY;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Domain;
using System;
namespace Mediinfo.Domain.JCJG.ZY
{
	public static class ZY_YINGERXXFactory
	{
        /*
		 
		public static ZY_YINGERXX CreateIfNotExists(IZY_YINGERXXRepository irep, ServiceContext sContext, E_ZY_YINGERXX dto)
		{
			var entity =  irep.GetByKey("1");
			if(entity == null)
			{
				    entity = new ZY_YINGERXX();
			}  
			return entity;
		}
		*/

        /*
		public static ZY_YINGERXX Create(IZY_YINGERXXRepository irep,ServiceContext sContext,E_ZY_YINGERXX dto )
		{
			ZY_YINGERXX entity = new ZY_YINGERXX();
			return entity;
		}
		 
		*/


        public static ZY_YINGERXX Create(IZY_YINGERXXRepository irep, ServiceContext sContext, E_ZY_YINGERXX DTO)
        {
            DTO.BINGRENZYID = irep.GetOrder("ZY_BINGRENXX", sContext.YUANQUID)[0].ToString();
            DTO.DENGJIRQ = irep.GetSYSTime();
            var zyBingRenXX = DTO.EToDB<E_ZY_YINGERXX, ZY_YINGERXX>();
            zyBingRenXX.Initialize(irep, sContext);
            irep.RegisterAdd(zyBingRenXX);
            return zyBingRenXX;
        }


    }
}
