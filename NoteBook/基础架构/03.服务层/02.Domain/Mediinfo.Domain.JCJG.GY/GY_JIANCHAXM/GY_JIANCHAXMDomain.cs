using Mediinfo.DTO.HIS.GY;
using Mediinfo.Infrastructure.Core.Domain;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public partial class GY_JIANCHAXM
	{
        public void Update(E_GY_JIANCHAXM jianChaXMDTO)
        {
            this.MargeDTO<GY_JIANCHAXM, E_GY_JIANCHAXM>(jianChaXMDTO);
        }
    }
}
