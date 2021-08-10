using Mediinfo.DTO.HIS.GY;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Domain;

namespace Mediinfo.Domain.JCJG.GY
{
    public static class GY_BINGRENGMSFactory
	{
        /*
		 
		public static GY_BINGRENGMS CreateIfNotExists(IGY_BINGRENGMSRepository irep, ServiceContext sContext, E_GY_BINGRENGMS dto)
		{
			var entity =  irep.GetByKey("1");
			if(entity == null)
			{
				    entity = new GY_BINGRENGMS();
			}  
			return entity;
		}
		*/


        public static GY_BINGRENGMS Create(IGY_BINGRENGMSRepository irep, ServiceContext sContext, E_GY_BINGRENGMS dto)
        {
            var gYBingRenGMSDomain =dto.EToDB<E_GY_BINGRENGMS, GY_BINGRENGMS>();

            gYBingRenGMSDomain.Initialize(irep, sContext);
            if (string.IsNullOrEmpty(gYBingRenGMSDomain.GUOMINSID))
            {
                gYBingRenGMSDomain.GUOMINSID = irep.GetOrder("GY_BINGRENGMS", sContext.YUANQUID)[0];//取表的主键值，给一个表的主键的一个新值（最大值）      
            }
            gYBingRenGMSDomain.GUOMINSID = gYBingRenGMSDomain.GUOMINSID;
            //gYBingRenGMSDomain.YINGYONGID = dto.YINGYONGID;
            //gYBingRenGMSDomain.YUANQUID = dto.YUANQUID;
            //gYBingRenGMSDomain.BINGRENID = dto.BINGRENID;
            //gYBingRenGMSDomain.LAIYUANID = dto.SHENQINGDID;
            //gYBingRenGMSDomain.JILULY = "1";
            //gYBingRenGMSDomain.CHULIYJ = dto.CHULIYJ;
            //gYBingRenGMSDomain.PISHIJG = dto.PISHIJG;
            //gYBingRenGMSDomain.JIAGEID = dto.PISHIJGID;
            //gYBingRenGMSDomain.YAOPINMC = dto.PISHIYP;
            //gYBingRenGMSDomain.ZHIXINGSJ = dto.ZHIXINGSJ.ToString();
            //gYBingRenGMSDomain.ZHIXINGREN = dto.ZHIXINGREN;
            //gYBingRenGMSDomain.ZHIXINGRXM = dto.ZHIXINGRXM;
            //gYBingRenGMSDomain.GUOMINLX = "1";

            irep.RegisterAdd(gYBingRenGMSDomain);
            return gYBingRenGMSDomain;
        }

        //public static GY_BINGRENGMS BingRenGMS(IGY_BINGRENGMSRepository irep, ServiceContext sContext,E_YZ_BINGRENYZ dto, string prmYingyongid, string prmCaozuoyuan, string zhigongxm,string yaopinid)
        //{
        //    GY_BINGRENGMS gYBingRenGMSDomain = new GY_BINGRENGMS();
        //    //var gYBingRenGMSDomain = dto.EToDB<E_YZ_BINGRENYZ, YZ_BINGRENYZ>();

        //    gYBingRenGMSDomain.Initialize(irep, sContext);
        //    if (string.IsNullOrEmpty(gYBingRenGMSDomain.GUOMINSID))
        //    {
        //        gYBingRenGMSDomain.GUOMINSID = irep.GetOrder("GY_BINGRENGMS", sContext.YUANQUID)[0];//取表的主键值，给一个表的主键的一个新值（最大值）      
        //    }
        //    gYBingRenGMSDomain.GUOMINSID = gYBingRenGMSDomain.GUOMINSID;
        //    gYBingRenGMSDomain.YINGYONGID = prmYingyongid;
        //    gYBingRenGMSDomain.YUANQUID = dto.YUANQUID;
        //    gYBingRenGMSDomain.BINGRENID = dto.BINGRENID;
        //    gYBingRenGMSDomain.LAIYUANID = dto.YIZHUID;
        //    gYBingRenGMSDomain.JILULY = "2";
        //    gYBingRenGMSDomain.JIAGEID = dto.JIAGEID;
        //    gYBingRenGMSDomain.YAOPINMC = dto.YAOPINMC;
        //    gYBingRenGMSDomain.CHULIYJ = dto.CHULIYJ;
        //    gYBingRenGMSDomain.PISHIJG = DateTime.Now.ToString("yyyy-MM-dd");
        //    gYBingRenGMSDomain.ZHIXINGSJ = dto.ZHIXINGSJ.Value.ToString("yyyy-MM-dd HH:mm:ss");
        //    gYBingRenGMSDomain.ZHIXINGREN = prmCaozuoyuan;
        //    gYBingRenGMSDomain.ZHIXINGRXM= zhigongxm;
        //    gYBingRenGMSDomain.GUOMINLX = "1";
        //    gYBingRenGMSDomain.YAOPINID = yaopinid;

        //    irep.RegisterAdd(gYBingRenGMSDomain);
        //    return gYBingRenGMSDomain;
        //}

    }
}
