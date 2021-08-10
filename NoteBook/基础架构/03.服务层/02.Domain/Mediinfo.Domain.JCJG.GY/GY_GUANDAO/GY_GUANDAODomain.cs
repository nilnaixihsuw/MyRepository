using Mediinfo.DTO.HIS.GY;
using Mediinfo.Infrastructure.Core.Domain;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public partial class GY_GUANDAO
    {
        public GY_GUANDAO Update(E_GY_GUANDAO_EX dto)
        {
            this.MargeDTO<GY_GUANDAO, E_GY_GUANDAO_EX>(dto);
            this.XIUGAIREN = ServiceContext.USERID;
            this.XIUGAISJ = GetSYSTime();
            return this.RegisterUpdate<GY_GUANDAO>(this);
        }
        public GY_GUANDAO ZuoFei()
        {
            this.ZUOFEIBZ = 1;
            this.XIUGAIREN = ServiceContext.USERID;
            this.XIUGAISJ = GetSYSTime();
            return this.RegisterUpdate<GY_GUANDAO>(this);
        }
    }
}
