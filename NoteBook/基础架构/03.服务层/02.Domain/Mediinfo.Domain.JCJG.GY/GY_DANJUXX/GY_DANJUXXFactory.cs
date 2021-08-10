using System;
using Mediinfo.DTO.HIS.GY;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Domain;
using Mediinfo.Utility;

namespace Mediinfo.Domain.JCJG.GY
{
	public static class GY_DANJUXXFactory
	{
        /*
		 
		public static GY_DANJUXX CreateIfNotExists(IGY_DANJUXXRepository irep, ServiceContext sContext, E_GY_DANJUXX dto)
		{
			var entity =  irep.GetByKey("1");
			if(entity == null)
			{
				    entity = new GY_DANJUXX();
			}  
			return entity;
		}
		*/

     
        public static GY_DANJUXX Create(IGY_DANJUXXRepository irep, ServiceContext sContext, E_GY_DANJUXX dto)
        {
            var danJuXX = dto.EToDB<E_GY_DANJUXX, GY_DANJUXX>();
            if (string.IsNullOrEmpty(danJuXX.DANJUID))
            {
                //danJuXX.DANJUID = irep.GetOrder("GY_DANJUXX", sContext.YUANQUID)[0];
                //单据有可能在现场做和上传，所以不使用序列，单据id用 应用id前两位+yyyyMMddHHmmss
                danJuXX.DANJUID = danJuXX.YINGYONGID.Substring(0, 2) + irep.GetSYSTime().ToString("yyyyMMddHHmmss");
            }
            danJuXX.DANJUMC = danJuXX.DANJUMC;
            danJuXX.XIUGAIREN = sContext.USERID;
            danJuXX.XIUGAISJ = irep.GetSYSTime();
            danJuXX.DANJUZT = 0;
            string SHURUMA1 = "";
            string SHURUMA2 = "";
            string SHURUMA3 = "";
            ShuRuMaHelper.GetShuRuMa(danJuXX.DANJUMC, out SHURUMA1, out SHURUMA2, out SHURUMA3);
            danJuXX.SHURUMA1 = SHURUMA1;
            danJuXX.SHURUMA2 = SHURUMA2;
            danJuXX.SHURUMA3 = SHURUMA3;
            danJuXX.Initialize(irep, sContext);
            irep.RegisterAdd(danJuXX);
            return danJuXX;
        }



    }
}
