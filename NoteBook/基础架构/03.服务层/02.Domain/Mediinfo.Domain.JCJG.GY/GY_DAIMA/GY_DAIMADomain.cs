using Mediinfo.DTO.HIS.GY;
using Mediinfo.Infrastructure.Core.Domain;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public partial class GY_DAIMA
	{
        public void Update(E_GY_DAIMA daiMaDTO)
        {
            this.MargeDTO<GY_DAIMA, E_GY_DAIMA>(daiMaDTO);
            this.XIUGAISJ = GetSYSTime();
            this.XIUGAIREN = ServiceContext.USERID;
        } 
    }
}
