using Mediinfo.DTO.HIS.GY;
using Mediinfo.Infrastructure.Core.Domain;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public partial class GY_JUESE
	{
        /// <summary>
        /// 删除角色信息
        /// </summary>
        public void Delete()
        {
            IRepositoyBase.RegisterDelete<GY_JUESE>(this);
        }   
        /// <summary>
        /// 更新角色信息
        /// </summary>
        /// <param name="eJueSe"></param>
        /// <returns></returns>
        public GY_JUESE Update(E_GY_JUESE eJueSe)
        {
            this.MargeDTO<GY_JUESE, E_GY_JUESE>(eJueSe);
            return this;
        }
    }
}
