using Mediinfo.DTO.HIS.GY;
using Mediinfo.Infrastructure.Core.Domain;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public partial class GY_JIXINGDZ
	{
        public void Update(E_GY_JIXINGDZ jiXingDZDTO)
        {
            this.MargeDTO<GY_JIXINGDZ, E_GY_JIXINGDZ>(jiXingDZDTO);
            this.XIUGAISJ = GetSYSTime();
            this.XIUGAIREN = ServiceContext.USERID;
        }
    }
}
