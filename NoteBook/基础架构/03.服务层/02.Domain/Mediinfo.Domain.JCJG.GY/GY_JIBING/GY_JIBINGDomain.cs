using Mediinfo.DTO.HIS.GY;
using Mediinfo.Infrastructure.Core.Domain;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public partial class GY_JIBING
	{
        public void Update(E_GY_JIBING daiMaDTO)
        {
            this.MargeDTO<GY_JIBING, E_GY_JIBING>(daiMaDTO);
            this.XIUGAISJ = GetSYSTime();
            this.XIUGAIREN = ServiceContext.USERID;
            IRepositoyBase.RegisterUpdate(this);
        }
    }
}
