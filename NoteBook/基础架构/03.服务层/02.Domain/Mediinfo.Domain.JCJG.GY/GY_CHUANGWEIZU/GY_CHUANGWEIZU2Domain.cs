using Mediinfo.DTO.HIS.GY;
using Mediinfo.Infrastructure.Core.Domain;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public partial class GY_CHUANGWEIZU2
	{
        /// <summary>
        /// 更新床位组2表数据
        /// </summary>
        /// <param name="eChuangWeiZu2"></param>
        /// <returns></returns>
        public GY_CHUANGWEIZU2 Update(E_GY_CHUANGWEIZU2 eChuangWeiZu2)
        {
            this.MargeDTO<GY_CHUANGWEIZU2, E_GY_CHUANGWEIZU2>(eChuangWeiZu2);
            this.XIUGAISJ = GetSYSTime();
            this.XIUGAIREN = ServiceContext.USERID;
            return this;
        }

        /// <summary>
        /// 删除床位组2表数据
        /// </summary>
        public void Delete()
        {
            IRepositoyBase.RegisterDelete<GY_CHUANGWEIZU2>(this);
        }
    }
}
