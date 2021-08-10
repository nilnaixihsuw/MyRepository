using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
using Mediinfo.Infrastructure.Core.DBEntity;

namespace Mediinfo.Infrastructure.JCJG.GY
{
	public class GY_CHUANGKOUZY_NEWRepository : RepositoryBase<GY_CHUANGKOUZY_NEW>, IGY_CHUANGKOUZY_NEWRepository
	{
		public GY_CHUANGKOUZY_NEWRepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}

        /// <summary>
        /// 获取控件的布局信息
        /// </summary>
        /// <param name="nameSpace">命名空间</param>
        /// <param name="formName">窗口名</param>
        /// <param name="controlName">控件名</param>
        /// <returns></returns>
        public List<GY_CHUANGKOUZY_NEW> GetList(string nameSpace, string formName, string controlName)
        {
            var list = this.Set<GY_CHUANGKOUZY_NEW>().Where(o => o.NAMESPACE == nameSpace && o.FORMNAME==formName && o.CONTROLNAME==controlName).ToList().WithContext(this, ServiceContext);
            return list;
        }

        /// <summary>
        /// 获取制定窗口下所有控件的布局信息
        /// </summary>
        /// <param name="nameSpace">命名空间</param>
        /// <param name="formName">窗口名</param>
        /// <returns></returns>
        public List<GY_CHUANGKOUZY_NEW> GetList(string nameSpace, string formName)
        {
            var list = this.Set<GY_CHUANGKOUZY_NEW>().Where(o => o.NAMESPACE == nameSpace && o.FORMNAME == formName ).ToList().WithContext(this, ServiceContext);
            return list;
        } 

    }
}
