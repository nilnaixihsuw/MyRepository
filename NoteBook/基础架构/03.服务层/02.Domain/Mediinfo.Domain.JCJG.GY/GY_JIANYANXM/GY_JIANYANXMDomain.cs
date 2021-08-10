using Mediinfo.DTO.HIS.GY;
using Mediinfo.Infrastructure.Core.Domain;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public partial class GY_JIANYANXM
	{
        public void Update(E_GY_JIANYANXM jianYanXMDTO)
        {
            this.MargeDTO<GY_JIANYANXM, E_GY_JIANYANXM>(jianYanXMDTO);
        }
    }
}
