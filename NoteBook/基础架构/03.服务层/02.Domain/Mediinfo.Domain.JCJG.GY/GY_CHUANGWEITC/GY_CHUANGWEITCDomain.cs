using Mediinfo.DTO.HIS.GY;
using Mediinfo.Infrastructure.Core.Domain;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public partial class GY_CHUANGWEITC
	{
        /// <summary>
        /// ×÷·Ï´²Î»Ì×²Í
        /// </summary>
        /// <returns></returns>
        public GY_CHUANGWEITC ZuoFeiCWTC()
        {
            this.ZUOFEIBZ = 1;
            this.XIUGAIREN = ServiceContext.USERID;
            this.XIUGAISJ = DateTime.Now;
            IRepositoyBase.RegisterUpdate(this);
            return this;
        }

        /// <summary>
        /// »Ö¸´´²Î»Ì×²Í
        /// </summary>
        /// <returns></returns>
        public GY_CHUANGWEITC HuiFuCWTC()
        {
            this.ZUOFEIBZ = 0;
            this.XIUGAIREN = ServiceContext.USERID;
            this.XIUGAISJ = DateTime.Now;
            IRepositoyBase.RegisterUpdate(this);
            return this;
        }



        /// <summary>
        /// ¸üÐÂ´²Î»Ì×²Í±í
        /// </summary>
        /// <param name="eChuangWeiTC"></param>
        /// <returns></returns>
        public GY_CHUANGWEITC Update(E_GY_CHUANGWEITC eChuangWeiTC)
        {
            this.MargeDTO<GY_CHUANGWEITC, E_GY_CHUANGWEITC>(eChuangWeiTC);
            this.XIUGAISJ = GetSYSTime();
            this.XIUGAIREN = ServiceContext.USERID;
            return this;
        }
    }
}
