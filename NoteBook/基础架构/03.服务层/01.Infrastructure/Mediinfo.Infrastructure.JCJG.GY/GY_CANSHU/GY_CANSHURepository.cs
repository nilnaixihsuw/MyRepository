using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
using Mediinfo.Enterprise.Exceptions;
using Mediinfo.Infrastructure.Core.Domain;

namespace Mediinfo.Infrastructure.JCJG.GY
{
	public class GY_CANSHURepository : RepositoryBase<GY_CANSHU>, IGY_CANSHURepository
	{
		public GY_CANSHURepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}

        ///// <summary>
        ///// ��ȡ����������Ӧ��ID->ϵͳID->00��˳���ȡ��
        ///// </summary>
        ///// <param name="Context">DBContext</param>
        ///// <param name="yingYongId">Ӧ��ID�����Ϊ����ȡ��00���Ĳ���</param>
        ///// <param name="canShuId">����ID</param>
        ///// <param name="defaultValue">����Ĭ��ֵ</param>
        ///// <returns>����ֵ</returns>
        //public   string GetCanShu(  string yingYongId, string canShuId, string defaultValue)
        //{
        //    if (string.IsNullOrWhiteSpace(canShuId))
        //        throw new DBException("����IDΪ�գ�");

        //    if (string.IsNullOrWhiteSpace(yingYongId))
        //        yingYongId = "00";

        //    //�ȴӻ�������ȡ
        //    string canShuZhi = "";

        //    //if (HISCacheManager.GetCanShu(yingYongId, canShuId, ref canShuZhi))
        //    //    return canShuZhi;

        //    //ȡ����Ӧ�õĲ���
        //    var canShuList = this.Set<GY_CANSHU>() 
        //                        .Where(c => c.CANSHUID == canShuId)
        //                        .Select(c => (new
        //                        {
        //                            c.CANSHUID,
        //                            c.CANSHUZHI,
        //                            c.YINGYONGID
        //                        })).ToList(); 

        //    var canShu = canShuList.Where(c => c.YINGYONGID == yingYongId).FirstOrDefault();

        //    if (null != canShu)
        //    {
        //        return canShu.CANSHUZHI;
        //    }
        //    else if (yingYongId == "00") //���Ӧ��IDΪ�գ����ʾӦ��IDû�д���
        //    {
        //        return defaultValue;
        //    } 

        //    canShu = canShuList.Where(c => c.YINGYONGID == yingYongId.Substring(0, 2)).FirstOrDefault();

        //    if (null != canShu)
        //    {
        //        return canShu.CANSHUZHI;
        //    }

        //    canShu = canShuList.Where(c => c.YINGYONGID == "00").FirstOrDefault();

        //    if (null != canShu)
        //    {
        //        return canShu.CANSHUZHI;
        //    }

        //    return defaultValue;
        //}

        /// <summary>
        /// ���ݲ���ID ȡ����Ӧ�õĲ�����Ϣ
        /// </summary>
        /// <param name="canShuID"></param>
        /// <returns></returns>
        public List<GY_CANSHU> GetList(string canShuID)
        {
            var list = this.Set<GY_CANSHU>().Where(o => o.CANSHUID == canShuID).ToList();
            return list;
        }

        public GY_CANSHU GetParamByKey(string yingYongID, string canShuID)
        {
            var entity = this.Set<GY_CANSHU>()
                .FirstOrDefault(o => o.YINGYONGID == yingYongID && o.CANSHUID == canShuID);
            return entity;
        }
    }
}
