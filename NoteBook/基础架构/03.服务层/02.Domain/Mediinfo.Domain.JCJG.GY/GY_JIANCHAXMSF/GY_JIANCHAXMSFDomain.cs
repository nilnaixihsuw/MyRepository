using Mediinfo.DTO.HIS.GY;
using Mediinfo.Infrastructure.Core.Domain;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public partial class GY_JIANCHAXMSF
	{
        public void Update(E_GY_JIANCHAXMSF jianChaXMDTO)
        {
            this.MargeDTO<GY_JIANCHAXMSF, E_GY_JIANCHAXMSF>(jianChaXMDTO);
        }
    }
}
