using Mediinfo.DTO.HIS.GY;
using Mediinfo.Infrastructure.Core.Domain;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public partial class GY_CAIDAN_NEW
	{
        /// <summary>
        /// 更新应用菜单
        /// </summary>
        /// <param name="eCaiDanNew"></param>
        /// <returns></returns>
        public GY_CAIDAN_NEW Update(E_GY_CAIDAN_NEW eCaiDanNew)
        {
            this.MargeDTO<GY_CAIDAN_NEW, E_GY_CAIDAN_NEW>(eCaiDanNew);
            this.XIUGAIREN = ServiceContext.USERID;
            this.XIUGAISJ = GetSYSTime();
            this.RegisterUpdate(this);
            //IRepositoyBase.RegisterUpdate(this);
            return this;
        }

        /// <summary>
        /// 更新应用菜单
        /// </summary>
        /// <param name="eCaiDanNew"></param>
        /// <returns></returns>
        public GY_CAIDAN_NEW UpdateIsOpen(int isOpen)
        {
            //this.MargeDTO<GY_CAIDAN_NEW, E_GY_CAIDAN_NEW>(eCaiDanNew);
            this.ISOPEN = isOpen;
            this.XIUGAIREN = ServiceContext.USERID;
            this.XIUGAISJ = GetSYSTime();
            this.RegisterUpdate(this);
            //IRepositoyBase.RegisterUpdate(this);
            return this;
        }
        /// <summary>
        /// 删除应用菜单
        /// </summary>
        /// <returns></returns>
        public GY_CAIDAN_NEW Delete()
        {
            IRepositoyBase.RegisterDelete<GY_CAIDAN_NEW>(this);
            return this;
        }
    }
}
