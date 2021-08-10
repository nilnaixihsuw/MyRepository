using System;
using Mediinfo.DTO.HIS.GY;
using Mediinfo.Infrastructure.Core.Domain;

namespace Mediinfo.Domain.JCJG.GY
{
	public partial class GY_BAOBIAOQX
	{

        public GY_BAOBIAOQX Update(E_GY_BAOBIAOQX quanXianDTO)
        {
            this.MargeDTO<GY_BAOBIAOQX, E_GY_BAOBIAOQX>(quanXianDTO);
            return this;
        }
    }
}
