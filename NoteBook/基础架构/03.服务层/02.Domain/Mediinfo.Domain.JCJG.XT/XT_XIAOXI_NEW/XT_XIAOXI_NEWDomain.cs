using System;
namespace Mediinfo.Domain.JCJG.XT
{
	public partial class XT_XIAOXI_NEW
	{
        /// <summary>
        /// 处理一次性标志
        /// </summary>
        /// <returns></returns>
        public XT_XIAOXI_NEW SetYiCiXBZ()
        {
            this.YICIXBZ = 2;
            return this;
        }
    }
}
