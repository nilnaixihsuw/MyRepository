using Mediinfo.DTO.HIS.GY;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Domain;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public static class GY_BINGRENBLHBFactory
	{
        /*
		 
		public static GY_BINGRENBLHB CreateIfNotExists(IGY_BINGRENBLHBRepository irep, ServiceContext sContext, E_GY_BINGRENBLHB dto)
		{
			var entity =  irep.GetByKey("1");
			if(entity == null)
			{
				    entity = new GY_BINGRENBLHB();
			}  
			return entity;
		}
		*/


        public static GY_BINGRENBLHB Create(IGY_BINGRENBLHBRepository irep, ServiceContext sContext, E_GY_BINGRENBLHBXX dto)
        {
            
            GY_BINGRENBLHB entity = dto.EToDB<E_GY_BINGRENBLHBXX, GY_BINGRENBLHB>();
            entity.Initialize(irep, sContext);
            if (string.IsNullOrWhiteSpace(entity.BINGLIHBID))
            {
                entity.BINGLIHBID = irep.GetOrder("GY_BINGRENBLHB", sContext.YUANQUID)[0].ToString();
            } 
            entity.XIUGAIREN = sContext.USERID;
            entity.XIUGAISJ = irep.GetSYSTime();
            irep.RegisterAdd(entity);
            return entity;

        }

        public static GY_BINGRENBLHB CreateZhuBingRen(IGY_BINGRENBLHBRepository irep, ServiceContext sContext,string zhuBingRenID)
        {

            GY_BINGRENBLHB entity = new GY_BINGRENBLHB();
            entity.BINGLIHBID = irep.GetOrder("GY_BINGRENBLHB", sContext.YUANQUID)[0].ToString();
            entity.ZHUBINGRBZ = 1;
            entity.BINGRENID = zhuBingRenID;
            entity.YUANBINGAH = zhuBingRenID;
            entity.XIUGAIREN = sContext.USERID;
            entity.XIUGAISJ = irep.GetSYSTime();
            irep.RegisterAdd(entity);
            return entity;

        }
        public static GY_BINGRENBLHB CreateFuBingRen(IGY_BINGRENBLHBRepository irep, ServiceContext sContext, string fuBingRenID,string zhuBingRID,string bingLiHBID)
        {

            GY_BINGRENBLHB entity = new GY_BINGRENBLHB(); 
            entity.BINGRENID = fuBingRenID;
            entity.BINGLIHBID = bingLiHBID;
            entity.ZHUBINGRBZ = 0;
            entity.YUANBINGAH = zhuBingRID; 
            entity.XIUGAIREN = sContext.USERID;
            entity.XIUGAISJ = irep.GetSYSTime();
            irep.RegisterAdd(entity);
            return entity;

        }

    }
}
