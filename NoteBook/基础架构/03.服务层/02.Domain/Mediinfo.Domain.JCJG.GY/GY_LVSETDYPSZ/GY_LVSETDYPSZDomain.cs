using Mediinfo.DTO.HIS.GY;
using Mediinfo.Infrastructure.Core.Domain;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public partial class GY_LVSETDYPSZ
	{
        /// <summary>
        /// 更新 公用_绿色通道药品设置
        /// </summary>
        /// <param name="eLvSeTDYPSZ_EX"></param>
        /// <returns></returns>
        public GY_LVSETDYPSZ Update(E_GY_LVSETDYPSZ_EX eLvSeTDYPSZ_EX)
        {
            this.MargeDTO<GY_LVSETDYPSZ, E_GY_LVSETDYPSZ_EX>(eLvSeTDYPSZ_EX);
            this.XIUGAIREN = ServiceContext.USERID;
            this.XIUGAISJ = GetSYSTime();
            return this;
        }
        /// <summary>
        /// 更新 公用_绿色通道药品设置
        /// </summary>
        /// <param name="eLvSeTDYPSZ"></param>
        /// <returns></returns>
        public GY_LVSETDYPSZ Update(E_GY_LVSETDYPSZ eLvSeTDYPSZ)
        {
            this.MargeDTO<GY_LVSETDYPSZ, E_GY_LVSETDYPSZ>(eLvSeTDYPSZ);
            this.XIUGAIREN = ServiceContext.USERID;
            this.XIUGAISJ = GetSYSTime();
            return this;
        }
        /// <summary>
        /// 删除 更新 公用_绿色通道药品设置
        /// </summary>
        public void Delete()
        {
            IRepositoyBase.RegisterDelete<GY_LVSETDYPSZ>(this);
        }
    }
}
