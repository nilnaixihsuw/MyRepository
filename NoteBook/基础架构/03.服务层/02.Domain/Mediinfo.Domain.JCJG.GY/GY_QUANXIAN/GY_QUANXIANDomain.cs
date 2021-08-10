using System;
using System.Collections.Generic;
using Mediinfo.DTO.HIS.GY;
using Mediinfo.Infrastructure.Core.Domain;

namespace Mediinfo.Domain.JCJG.GY
{
	public partial class GY_QUANXIAN
	{

        /// <summary>
        /// 停用权限
        /// </summary>
        public GY_QUANXIAN Disable()
        {
            this.XIUGAIREN = ServiceContext.USERID;
            this.XIUGAISJ = GetSYSTime();
            this.QIYONGBZ = 0;
            return this;
        }

        /// <summary>
        /// 启用权限
        /// </summary>
        public GY_QUANXIAN Enable()
        {
            this.XIUGAIREN = ServiceContext.USERID;
            this.XIUGAISJ = GetSYSTime();
            this.QIYONGBZ = 1;
            return this;
        }
        /// <summary>
        /// 更新权限信息
        /// </summary>
        /// <param name="eQuanXian"></param>
        public GY_QUANXIAN Update(E_GY_QUANXIAN eQuanXian)
        {
            this.MargeDTO<GY_QUANXIAN, E_GY_QUANXIAN>(eQuanXian);
            this.XIUGAIREN = ServiceContext.USERID;
            this.XIUGAISJ = GetSYSTime();
            return this;
        }
        /// <summary>
        /// 删除权限信息
        /// </summary>
        public void Delete()
        {
            IRepositoyBase.RegisterDelete<GY_QUANXIAN>(this);
        }

      
    }
}
