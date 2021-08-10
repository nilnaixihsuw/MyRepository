using Mediinfo.DTO.HIS.GY;
using Mediinfo.Infrastructure.Core.Domain;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public partial class GY_ZIJINZHDR
	{
        public GY_ZIJINZHDR Update(E_GY_ZIJINZHDR eGYZiJinZHDRDTO)
        {
            this.MargeDTO<GY_ZIJINZHDR, E_GY_ZIJINZHDR>(eGYZiJinZHDRDTO);
            this.RegisterUpdate(this);
            return this;
        }
    }
}
