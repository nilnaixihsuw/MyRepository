using Mediinfo.DTO.HIS.GY;
using Mediinfo.Infrastructure.Core.Domain;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public partial class GY_JIANCHAXMZXKS
	{
        public void Update(E_GY_JIANCHAXMZXKS jianChaXMDTO)
        {
            this.MargeDTO<GY_JIANCHAXMZXKS, E_GY_JIANCHAXMZXKS>(jianChaXMDTO);
        }
    }
}
