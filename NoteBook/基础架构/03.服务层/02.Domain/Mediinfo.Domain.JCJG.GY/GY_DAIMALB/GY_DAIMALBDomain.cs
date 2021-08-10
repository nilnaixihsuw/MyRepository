using Mediinfo.DTO.HIS.GY;
using Mediinfo.Infrastructure.Core.Domain;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public partial class GY_DAIMALB
	{ 
        public void UpdateDaiMaLB(E_GY_DAIMALB EDaiMaLB)
        {
            this.MargeDTO<GY_DAIMALB, E_GY_DAIMALB>(EDaiMaLB);
            this.XIUGAISJ = GetSYSTime();
            this.XIUGAIREN = ServiceContext.USERID;
        }
    }
}
