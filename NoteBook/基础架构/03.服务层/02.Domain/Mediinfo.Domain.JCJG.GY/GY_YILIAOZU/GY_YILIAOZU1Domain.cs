using Mediinfo.DTO.HIS.GY;
using Mediinfo.Infrastructure.Core.Domain;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public partial class GY_YILIAOZU1
	{
        /// <summary>
        /// 更新 公用_医疗组1
        /// </summary>
        /// <param name="eYiLiaoZu"></param>
        /// <returns></returns>
        public GY_YILIAOZU1 Update(E_GY_YILIAOZU1 eYiLiaoZu)
        {
            this.MargeDTO<GY_YILIAOZU1, E_GY_YILIAOZU1>(eYiLiaoZu);
            this.XIUGAISJ = GetSYSTime();
            this.XIUGAIREN = ServiceContext.USERID;
            return this;
        }
        /// <summary>
        /// 删除 公用_医疗组1
        /// </summary>
        public void Delete()
        {
            IRepositoyBase.RegisterDelete<GY_YILIAOZU1>(this);
        }
    }
}
