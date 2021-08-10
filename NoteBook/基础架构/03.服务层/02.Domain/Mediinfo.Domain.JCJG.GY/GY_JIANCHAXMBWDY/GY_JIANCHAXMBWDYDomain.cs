using Mediinfo.DTO.HIS.GY;
using Mediinfo.Infrastructure.Core.Domain;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public partial class GY_JIANCHAXMBWDY
	{
        public void Update(E_GY_JIANCHAXMBWDY jianChaXMDTO)
        {
            this.MargeDTO<GY_JIANCHAXMBWDY, E_GY_JIANCHAXMBWDY>(jianChaXMDTO);
        }
    }
}
