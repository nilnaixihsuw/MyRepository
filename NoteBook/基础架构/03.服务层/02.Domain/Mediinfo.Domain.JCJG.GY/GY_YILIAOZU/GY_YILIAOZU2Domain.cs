using Mediinfo.DTO.HIS.GY;
using Mediinfo.Infrastructure.Core.Domain;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public partial class GY_YILIAOZU2
	{
        /// <summary>
        /// 更新 公用_医疗组2
        /// </summary>
        /// <param name="eYiLiaoZu2"></param>
        /// <returns></returns>
        public GY_YILIAOZU2 Update(E_GY_YILIAOZU2 eYiLiaoZu2)
        {
            this.MargeDTO<GY_YILIAOZU2, E_GY_YILIAOZU2>(eYiLiaoZu2);
            this.XIUGAISJ = GetSYSTime();
            this.XIUGAIREN = ServiceContext.USERID;
            return this;
        }
        /// <summary>
        /// 删除 公用_医疗组2
        /// </summary>
        public void Delete()
        {
            IRepositoyBase.RegisterDelete<GY_YILIAOZU2>(this);
        }
    }
}
