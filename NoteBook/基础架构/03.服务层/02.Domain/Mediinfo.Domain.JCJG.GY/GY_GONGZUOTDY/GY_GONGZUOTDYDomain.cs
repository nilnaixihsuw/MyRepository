using Mediinfo.DTO.HIS.GY;
using Mediinfo.Infrastructure.Core.Domain;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public partial class GY_GONGZUOTDY
	{
        public GY_GONGZUOTDY Update(E_GY_GONGZUOTDY dto)
        {
            this.MargeDTO<GY_GONGZUOTDY, E_GY_GONGZUOTDY>(dto);
            this.XIUGAIREN = ServiceContext.USERID;
            this.XIUGAISJ = GetSYSTime();
            return this.RegisterUpdate<GY_GONGZUOTDY>(this);
        }
       
        public GY_GONGZUOTDY Delete()
        {
            return this.RegisterDelete<GY_GONGZUOTDY>(this);
        }
        public GY_GONGZUOTDY ZuoFei(E_GY_GONGZUOTDY dto)
        {
            this.MargeDTO<GY_GONGZUOTDY, E_GY_GONGZUOTDY>(dto);
            this.ZUOFEIBZ = 1;   
            this.XIUGAIREN = ServiceContext.USERID;
            this.XIUGAISJ = GetSYSTime();
            return this.RegisterUpdate<GY_GONGZUOTDY>(this);
        }
    }
}
