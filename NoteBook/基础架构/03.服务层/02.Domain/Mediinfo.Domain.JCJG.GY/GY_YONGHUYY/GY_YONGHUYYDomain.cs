using Mediinfo.DTO.HIS.GY;
using Mediinfo.Infrastructure.Core.Domain;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public partial class GY_YONGHUYY
	{
        /// <summary>
        /// 删除 公用_用户应用
        /// </summary>
        public void Delete()
        {
            IRepositoyBase.RegisterDelete<GY_YONGHUYY>(this);
        }
        /// <summary>
        /// 更新 公用_用户应用
        /// </summary>
        /// <param name="eYongHuXX"></param>
        public GY_YONGHUYY Update(E_GY_YONGHUYY eYongHuYY)
        {
            this.MargeDTO<GY_YONGHUYY, E_GY_YONGHUYY>(eYongHuYY);
            this.XIUGAISJ = GetSYSTime();
            this.XIUGAIREN = ServiceContext.USERID;
            return this;
        }
    }
}
