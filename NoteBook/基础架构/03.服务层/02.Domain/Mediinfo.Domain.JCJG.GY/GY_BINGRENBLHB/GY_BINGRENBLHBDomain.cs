using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public partial class GY_BINGRENBLHB
	{
        /// <summary>
        /// ɾ��
        /// </summary>
        public GY_BINGRENBLHB Delete()
        {
            IRepositoyBase.RegisterDelete<GY_BINGRENBLHB>(this);
            return this;
        }
    }
}
