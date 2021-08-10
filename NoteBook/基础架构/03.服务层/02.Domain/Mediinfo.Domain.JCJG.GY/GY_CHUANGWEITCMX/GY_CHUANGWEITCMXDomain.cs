using Mediinfo.DTO.HIS.GY;
using Mediinfo.Infrastructure.Core.Domain;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public partial class GY_CHUANGWEITCMX
	{
        /// <summary>
        /// ¸üÐÂ´²Î»Ì×²ÍÃ÷Ï¸
        /// </summary>
        /// <param name="eChuangWeiTCMX"></param>
        /// <returns></returns>
        public GY_CHUANGWEITCMX Update(E_GY_CHUANGWEITCMX_EX eChuangWeiTCMX)
        {
            var dto = eChuangWeiTCMX.EToE<E_GY_CHUANGWEITCMX_EX, E_GY_CHUANGWEITCMX>();
            this.MargeDTO<GY_CHUANGWEITCMX, E_GY_CHUANGWEITCMX>(dto);
            this.XINGZHISX = string.Format("{0}{1}{2}{3}{4}", eChuangWeiTCMX.PUTONG, eChuangWeiTCMX.BAOCHUANG, eChuangWeiTCMX.BAOFANG, eChuangWeiTCMX.YINGER, "000000");
            this.XIUGAISJ = GetSYSTime();
            this.XIUGAIREN = ServiceContext.USERID;
            return this;
        }

        /// <summary>
        /// É¾³ý´²Î»Ì×²ÍÃ÷Ï¸
        /// </summary>
        public void Delete()
        {
            IRepositoyBase.RegisterDelete<GY_CHUANGWEITCMX>(this);
        }
    }
}
