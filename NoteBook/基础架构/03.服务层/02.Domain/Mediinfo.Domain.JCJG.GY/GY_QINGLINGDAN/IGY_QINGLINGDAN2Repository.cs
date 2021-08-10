using System;
using System.Collections.Generic;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Infrastructure.Core.Entity;
using  Mediinfo.Infrastructure.Core.DBEntity;
namespace Mediinfo.Domain.JCJG.GY
{
	 public interface IGY_QINGLINGDAN2Repository : IRepository<GY_QINGLINGDAN2>, IDependency
	{
        /// <summary>
        /// �������쵥1��ѯ�빺ҩƷ
        /// </summary>
        /// <param name="qingLingDID"></param>
        /// <param name="shengQingKSSJ"></param>
        /// <param name="shengQingKSJS"></param>
        /// <returns></returns>
        List<GY_QINGLINGDAN2> GetList(string qingLingDID, DateTime shengQingKSSJ, DateTime shengQingKSJS);

        /// <summary>
        /// �������쵥1������״̬��ѯ�빺ҩƷ
        /// </summary>
        /// <param name="jiaGeID"></param>
        /// <param name="beiQingLDYYID"></param>
        /// <param name="shengQingKSSJ"></param>
        /// <param name="shengQingKSJS"></param>
        /// <param name="qingLingZT"></param>
        /// <returns></returns>
        List<GY_QINGLINGDAN2> GetList(string jiaGeID,string beiQingLDYYID, DateTime shengQingKSSJ, DateTime shengQingKSJS, string qingLingZT);

        /// <summary>
        /// ���ݼ۸�ID��ȡ���쵥��ϸ
        /// </summary>
        /// <param name="jiaGeID"></param>
        /// <returns></returns>
        List<GY_QINGLINGDAN2> GetList(string jiaGeID);
    }
}
