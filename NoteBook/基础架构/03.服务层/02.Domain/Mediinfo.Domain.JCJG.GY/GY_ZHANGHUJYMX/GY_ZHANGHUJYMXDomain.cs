using Mediinfo.DTO.HIS.GY;
using Mediinfo.Infrastructure.Core.Domain;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public partial class GY_ZHANGHUJYMX
	{
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="eZhangHuJYMX"></param>
        /// <returns></returns>
        public GY_ZHANGHUJYMX Update(E_GY_ZHANGHUJYMX eZhangHuJYMX)
        {

            this.MargeDTO<GY_ZHANGHUJYMX, E_GY_ZHANGHUJYMX>(eZhangHuJYMX);
            return this;
        }
        /// <summary>
        /// 删除
        /// </summary>
        public GY_ZHANGHUJYMX Delete()
        {
            IRepositoyBase.RegisterDelete<GY_ZHANGHUJYMX>(this);
            return this;
        }

        /// <summary>
        /// 取消日报
        /// </summary>
        /// <param name="quXiaoRQ"></param>
        /// <returns></returns>
        public GY_ZHANGHUJYMX QuXiaoRB()
        {
            this.RIBAOID = null;
            this.RIBAORQ = null;
            this.RegisterUpdate<GY_ZHANGHUJYMX>(this);
            return this;
        }
    }
}
