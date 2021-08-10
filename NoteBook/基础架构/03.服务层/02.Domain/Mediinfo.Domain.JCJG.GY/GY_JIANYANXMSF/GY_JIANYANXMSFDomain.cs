using Mediinfo.DTO.HIS.GY;
using Mediinfo.Infrastructure.Core.Domain;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public partial class GY_JIANYANXMSF
	{
        public void Update(E_GY_JIANYANXMSFXX jianYanXMSFDTO)
        {
            this.MargeDTO<GY_JIANYANXMSF, E_GY_JIANYANXMSFXX>(jianYanXMSFDTO);
        }
    }
}
