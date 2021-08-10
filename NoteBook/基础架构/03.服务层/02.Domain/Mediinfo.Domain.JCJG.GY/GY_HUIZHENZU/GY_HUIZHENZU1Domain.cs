using Mediinfo.DTO.HIS.GY;
using Mediinfo.Infrastructure.Core.Domain;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public partial class GY_HUIZHENZU1
	{
        public GY_HUIZHENZU1 Update(E_HL_HZXZ_XZXX entity)
        {
            this.HUIZHENZM = entity.HUIZHENZM;
            this.XIUGAISJ = GetSYSTime();
            this.XIUGAIREN = ServiceContext.USERID;
            IRepositoyBase.RegisterUpdate(this);
            return this;
        }
    }
}
