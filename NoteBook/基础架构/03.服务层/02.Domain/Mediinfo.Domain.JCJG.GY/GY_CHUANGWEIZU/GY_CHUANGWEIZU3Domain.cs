using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public partial class GY_CHUANGWEIZU3
	{
        /// <summary>
        /// 删除床位组3表数据
        /// </summary>
        public void Delete()
        {
            IRepositoyBase.RegisterDelete<GY_CHUANGWEIZU3>(this);
        }
    }
}
