using Mediinfo.DTO.HIS.GY;
using Mediinfo.Infrastructure.Core.Domain;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public partial class GY_YONGHUPFXX
	{
        public GY_YONGHUPFXX Update(E_GY_YONGHUPFXX eYongHuPFXX)
        {
            this.MargeDTO<GY_YONGHUPFXX, E_GY_YONGHUPFXX>(eYongHuPFXX);
            this.XIUGAIREN = ServiceContext.USERID;
            this.XIUGAISJ = GetSYSTime();
            return this;
        }
      
        public void Delete()
        {
            IRepositoyBase.RegisterDelete<GY_YONGHUPFXX>(this);
        }
    }
}
