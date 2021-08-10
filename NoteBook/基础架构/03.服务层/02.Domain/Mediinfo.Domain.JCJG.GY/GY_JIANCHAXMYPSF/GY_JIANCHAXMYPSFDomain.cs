using Mediinfo.DTO.HIS.GY;
using Mediinfo.Infrastructure.Core.Domain;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public partial class GY_JIANCHAXMYPSF
	{
        public void Update(E_GY_JIANCHAXMYPSF jianChaXMDTO)
        {
            this.MargeDTO<GY_JIANCHAXMYPSF, E_GY_JIANCHAXMYPSF>(jianChaXMDTO);
        }
    }
}
