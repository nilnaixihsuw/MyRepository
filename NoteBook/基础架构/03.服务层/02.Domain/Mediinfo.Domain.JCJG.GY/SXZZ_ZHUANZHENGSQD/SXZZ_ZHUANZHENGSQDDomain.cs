using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public partial class SXZZ_ZHUANZHENGSQD
	{

        public SXZZ_ZHUANZHENGSQD updateZhunZhenDH(string zhuZhenSQDH)
        {
            this.ZHUANZHENDH = zhuZhenSQDH;
            //this.RegisterUpdate<SXZZ_ZHUANZHENGSQD>(this);
            return this;
        }
    }
}
