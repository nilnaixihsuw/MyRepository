using System;
using System.Collections.Generic;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Infrastructure.Core.Entity;
using Mediinfo.Infrastructure.Core.DBEntity;
namespace Mediinfo.Domain.JCJG.GY
{
	 public interface IGY_JIANYANXMSFRepository : IRepository<GY_JIANYANXMSF>, IDependency
	{
        /// <summary>
        /// ȡ������Ŀ�շ���Ϣ�����ݼ�����ĿID
        /// </summary>
        /// <param name="jianYanXMID">������ĿID</param>
        /// <returns></returns>
        List<GY_JIANYANXMSF> GetListByJYXMID(string jianYanXMID);
	}
}
