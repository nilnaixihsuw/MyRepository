using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public partial class GY_CAOZUORZ
	{
        /// <summary>
        /// ”√ªß«©ÕÀ
        /// </summary>
        public GY_CAOZUORZ Logout( )
        {
            this.CAOZUORQ2 = this.GetSYSTime();
            IRepositoyBase.RegisterUpdate(this);
            return this;
        }
    }
}
