using System;
using Mediinfo.DTO.HIS.GY;
using Mediinfo.Infrastructure.Core.Domain;

namespace Mediinfo.Domain.JCJG.GY
{
	public partial class GY_JUESEBB
	{
        public GY_JUESEBB Update(E_GY_JUESEBB jueSeDTO)
        {
            jueSeDTO.XIUGAIREN = ServiceContext.USERID;
            jueSeDTO.XIUGAISJ = GetSYSTime();
            this.MargeDTO<GY_JUESEBB, E_GY_JUESEBB>(jueSeDTO);
            return this;
        }
    }
}
