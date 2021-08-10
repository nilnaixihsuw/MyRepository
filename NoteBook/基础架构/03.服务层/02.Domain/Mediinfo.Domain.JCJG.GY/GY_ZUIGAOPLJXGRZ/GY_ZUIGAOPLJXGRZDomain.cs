using Mediinfo.DTO.HIS.GY;
using Mediinfo.Infrastructure.Core.Domain;

namespace Mediinfo.Domain.JCJG.GY
{
    public partial class GY_ZUIGAOPLJXGRZ
	{
        /// <summary>
        /// 删除 公用_最高批零价修改日志
        /// </summary>
        public void Delete()
        {
            IRepositoyBase.RegisterDelete<GY_ZUIGAOPLJXGRZ>(this);
        }
        /// <summary>
        /// 更新 公用_最高批零价修改日志
        /// </summary>
        /// <param name="eYZuiGaoPLJXGRZ"></param>
        /// <returns></returns>
        public GY_ZUIGAOPLJXGRZ Update(E_GY_ZUIGAOPLJXGRZ eYZuiGaoPLJXGRZ)
        {
            this.MargeDTO<GY_ZUIGAOPLJXGRZ, E_GY_ZUIGAOPLJXGRZ>(eYZuiGaoPLJXGRZ);
            this.XIUGAISJ = GetSYSTime();
            this.XIUGAIREN = ServiceContext.USERID;
            return this;
        }
    }
}
