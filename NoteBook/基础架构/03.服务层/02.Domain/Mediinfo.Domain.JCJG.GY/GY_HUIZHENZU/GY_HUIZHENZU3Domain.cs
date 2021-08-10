using Mediinfo.DTO.HIS.GY;
using Mediinfo.Infrastructure.Core.Domain;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public partial class GY_HUIZHENZU3
	{
        public GY_HUIZHENZU3 Update(E_HL_HZXZ_HuiZhenLB entity)
        {
            this.MargeDTO(entity);
            this.XIUGAISJ = GetSYSTime();
            this.XIUGAIREN = ServiceContext.USERID;
            IRepositoyBase.RegisterUpdate(this);
            return this;
        }
    }
}
