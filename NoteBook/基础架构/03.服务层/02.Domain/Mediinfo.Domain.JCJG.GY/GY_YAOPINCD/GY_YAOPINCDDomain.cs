using Mediinfo.DTO.HIS.GY;
using Mediinfo.Infrastructure.Core.Domain;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public partial class GY_YAOPINCD
	{
        /// <summary>
        /// 删除 公用_药品产地
        /// </summary>
        public void Delete()
        {
            IRepositoyBase.RegisterDelete<GY_YAOPINCD>(this);
        }
        /// <summary>
        /// 更新 公用_药品产地
        /// </summary>
        /// <param name="eYaoPinCD"></param>
        public GY_YAOPINCD Update(E_GY_YAOPINCD eYaoPinCD)
        {
            this.MargeDTO<GY_YAOPINCD, E_GY_YAOPINCD>(eYaoPinCD);
            this.XIUGAIREN = ServiceContext.USERID;
            this.XIUGAISJ = this.GetSYSTime();
            return this;
        }
    }
}
