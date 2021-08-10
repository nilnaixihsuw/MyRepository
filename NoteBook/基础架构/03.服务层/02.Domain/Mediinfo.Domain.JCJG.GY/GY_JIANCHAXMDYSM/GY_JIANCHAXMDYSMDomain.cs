using Mediinfo.DTO.HIS.GY;
using Mediinfo.Infrastructure.Core.Domain;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public partial class GY_JIANCHAXMDYSM
	{
        public void Update(E_GY_JIANCHAXMDYSM jianChaXMDTO)
        {
            this.MargeDTO<GY_JIANCHAXMDYSM, E_GY_JIANCHAXMDYSM>(jianChaXMDTO);
        }
    }
}
