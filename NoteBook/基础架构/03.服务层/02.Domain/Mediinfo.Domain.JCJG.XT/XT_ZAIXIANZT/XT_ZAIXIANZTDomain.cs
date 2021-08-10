using Mediinfo.HIS.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mediinfo.Domain.JCJG.XT
{
    /// <summary>
    /// 在线状态
    /// </summary>
	public partial class XT_ZAIXIANZT
	{
        /// <summary>
        /// 当前的角色权限
        /// </summary>
        /// <returns></returns>
        public List<string> DangQianJsQxIDList()
        {
            if(string.IsNullOrWhiteSpace(this.JUESEQX))
            {
                return new List<string>();
            }
            else
            {
                return this.JUESEQX.Split('|').ToList();
            }
        }

        /// <summary>
        /// 续租
        /// </summary>
        public XT_ZAIXIANZT XuZu(XT_ZAIXIANZT xT_ZAIXIANZT)
        {
            this.BINGQUID = xT_ZAIXIANZT.BINGQUID;
            this.KESHIID = xT_ZAIXIANZT.KESHIID;
            this.JIESHUSJ = DateTime.Now.AddMinutes(30);
            return this;
        }

        /// <summary>
        /// 是否在线
        /// </summary>
        /// <returns></returns>
        public bool ZaiXian()
        {
            return this.JIESHUSJ >= DateTime.Now;
        }
    }
}
