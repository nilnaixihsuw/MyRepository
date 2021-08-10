using System;
using Mediinfo.Enterprise;

namespace Mediinfo.Domain.JCJG.XT
{
	public static class XT_XIAOXISJX_NEWFactory
	{
        /*
		 
		public static XT_XIAOXISJX_NEW CreateIfNotExists(IXT_XIAOXISJX_NEWRepository irep, ServiceContext sContext, E_XT_XIAOXISJX_NEW dto)
		{
			var entity =  irep.GetByKey("1");
			if(entity == null)
			{
				    entity = new XT_XIAOXISJX_NEW();
			}  
			return entity;
		}
		*/


        public static XT_XIAOXISJX_NEW ChuShiHua(IXT_XIAOXISJX_NEWRepository irep, ServiceContext sContext, 
            long xiaoXiID, DateTime? tiXingXQ,int yiCiXBZ)
        {
            XT_XIAOXISJX_NEW entity = new XT_XIAOXISJX_NEW();
            entity.XIAOXIID = xiaoXiID;
            entity.SHOUJIANRID = "U-" + sContext.USERID;
            entity.SHOUJIANRXM = sContext.USERNAME;
            if (yiCiXBZ == 2)
            {
                entity.YUEDUSJ = DateTime.Now;
                entity.YUEDUBZ = 1;
            }
            else
            {
                entity.YUEDUBZ = 0;
            }
            entity.SHANCHUBZ = 0;
            entity.HUIFUBZ = 0;
            entity.HOUXUBZ = 0;
            entity.ZHUANGTAI = 0;
            entity.TIXINGXQ = tiXingXQ;
            return entity;
        }



    }
}
