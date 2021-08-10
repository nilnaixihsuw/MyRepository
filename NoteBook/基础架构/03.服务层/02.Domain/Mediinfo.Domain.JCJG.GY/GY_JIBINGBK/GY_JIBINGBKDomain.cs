using Mediinfo.DTO.HIS.GY;
using Mediinfo.Infrastructure.Core.Domain;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public partial class GY_JIBINGBK
	{
        public void Update(E_GY_JIBINGBK daiMaDTO)
        {
            this.MargeDTO<GY_JIBINGBK, E_GY_JIBINGBK>(daiMaDTO);
            this.XITONGSJ = GetSYSTime();
            IRepositoyBase.RegisterUpdate(this);
        }
    }
}
