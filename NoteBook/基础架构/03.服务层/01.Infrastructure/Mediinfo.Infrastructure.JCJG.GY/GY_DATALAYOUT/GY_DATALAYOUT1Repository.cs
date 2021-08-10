using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY; 
using Mediinfo.Infrastructure.Core.DBEntity;

namespace Mediinfo.Infrastructure.JCJG.GY
{
	public class GY_DATALAYOUT1Repository : RepositoryBase<GY_DATALAYOUT1>, IGY_DATALAYOUT1Repository
	{
		public GY_DATALAYOUT1Repository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}
       
        /// <summary>
        /// 获取指定控件的布局信息
        /// </summary>
        /// <param name="context"></param>
        /// <param name="sContext"></param>
        /// <param name="nameSpace"></param>
        /// <param name="formName"></param>
        /// <param name="yingYongId"></param>
        /// <param name="controlName"></param>
        /// <returns></returns>
        public GY_DATALAYOUT1 GetByName(  string nameSpace, string formName,
                                                    string yingYongId, string controlName)
        {
            if (string.IsNullOrWhiteSpace(nameSpace) || string.IsNullOrWhiteSpace(formName)
                || string.IsNullOrWhiteSpace(yingYongId) || string.IsNullOrWhiteSpace(controlName))
            {
                return null;
            }

            var entity =this.Set<GY_DATALAYOUT1>().Where(c => c.CONTROLNAME == controlName && c.NAMESPACE == nameSpace && c.FORMNAME == formName && c.YINGYONGID == yingYongId).FirstOrDefault().WithContext(this,ServiceContext);

            if (null == entity)
                return null; 
          
            return entity;
        }

    }
}
