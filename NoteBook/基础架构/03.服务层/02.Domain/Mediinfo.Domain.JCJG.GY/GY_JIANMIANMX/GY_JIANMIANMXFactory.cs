using Mediinfo.Enterprise;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public static class GY_JIANMIANMXFactory
	{
        /*
		 
		public static GY_JIANMIANMX CreateIfNotExists(IGY_JIANMIANMXRepository irep, ServiceContext sContext, E_GY_JIANMIANMX dto)
		{
			var entity =  irep.GetByKey("1");
			if(entity == null)
			{
				    entity = new GY_JIANMIANMX();
			}  
			return entity;
		}
		*/

        /*
		public static GY_JIANMIANMX Create(IGY_JIANMIANMXRepository irep,ServiceContext sContext,E_GY_JIANMIANMX dto )
		{
			GY_JIANMIANMX entity = new GY_JIANMIANMX();
			return entity;
		}
		 
		*/

        public static GY_JIANMIANMX Create(IGY_JIANMIANMXRepository irep, ServiceContext sContext, 
            string jieSuanID, string shouFeiID,  int menZhenZYBZ, string bingRenZYID,string bingRenID,
            string xiangMuID,string xiangMuLX,decimal zongJinE,decimal ziLiJE,decimal ziFeiJE,decimal tongChouJE,
           decimal jianMianBL, decimal ziLiZFJM, decimal tongChouJM, string heSuanXM,string feiYongMXID,string feiYongLY)
        {
            GY_JIANMIANMX entity = new GY_JIANMIANMX();
            entity.Initialize(irep, sContext);
            entity.JIANMIANMXID = irep.GetOrder("GY_JIANMIANMX", sContext.YUANQUID)[0].ToString();
            entity.JIESUANID = jieSuanID;
            entity.SHOUFEIID = shouFeiID;
            entity.MENZHENZYBZ = menZhenZYBZ;
            entity.BINGRENZYID = bingRenZYID;
            entity.BINGRENID = bingRenID;
            entity.XIANGMUID = xiangMuID;
            entity.XIANGMULX = xiangMuLX;
            entity.ZONGJINE = zongJinE;
            entity.ZILIJE = ziLiJE;
            entity.ZIFEIJE = ziFeiJE;
            entity.TONGCHOUJE = tongChouJE;
            entity.JIANMIANBL = jianMianBL;
            entity.ZILIZFJM = ziLiZFJM;
            entity.TONGCHOUJM = tongChouJM;
            entity.HESUANXM = heSuanXM;
            entity.FEIYONGMXID = feiYongMXID;
            entity.FEIYONGLY = feiYongLY;
            irep.RegisterAdd(entity);
            return entity;
        }

    }
}
