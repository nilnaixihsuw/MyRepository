using Mediinfo.DTO.HIS.ZY;
using Mediinfo.Infrastructure.Core.Domain;
using System;
namespace Mediinfo.Domain.JCJG.ZY
{
	public partial class ZY_YINGERXX
	{

        public ZY_YINGERXX UpdateZhuYuanBRXX(E_ZY_YINGERXX ebingrenxx)
        {
            this.MargeDTO<ZY_YINGERXX, E_ZY_YINGERXX>(ebingrenxx);
            return this;
        }

        /// <summary>
        /// 更新zy_yingerxx
        /// </summary>
        /// <returns></returns>
        public ZY_YINGERXX Update()
        {
            this.RegisterUpdate(this);
            return this;
        }

        public ZY_YINGERXX UpdateBingRenID(string bingRenID)
        {

            this.BINGRENID = bingRenID;
            IRepositoyBase.RegisterUpdate(this);
            return this;
        }

        public ZY_YINGERXX Delete()
        {
            IRepositoyBase.RegisterDelete<ZY_YINGERXX>(this); //将本身在仓储中登记为删除
            return this;
        }

    }
}
