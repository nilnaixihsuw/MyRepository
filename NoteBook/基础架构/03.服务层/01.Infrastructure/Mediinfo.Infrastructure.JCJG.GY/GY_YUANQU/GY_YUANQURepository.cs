using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
using Mediinfo.Infrastructure.Core.DBEntity;

namespace Mediinfo.Infrastructure.JCJG.GY
{
	public class GY_YUANQURepository : RepositoryBase<GY_YUANQU>, IGY_YUANQURepository
	{
		public GY_YUANQURepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}

        void SetCache()
        {
            var yaunQuList = GetFromCache<List<GY_YUANQU>>("YUANQU");
            if (yaunQuList == null)
            {
                yaunQuList = this.Set<GY_YUANQU>().ToList();
                AddToCache<List<GY_YUANQU>>("YUANQU", yaunQuList);
            }
        }
        public List<GY_YUANQU> GetList()
        {
            var list = this.Set<GY_YUANQU>().ToList().WithContext(this, ServiceContext);
            return list;
        }
        /// <summary>
        /// ��������ǰ׺
        /// </summary>
        /// <param name="yuanQuID"></param>
        /// <param name="menZhenZYBZ"></param>
        /// <returns></returns>
        public string TiaoMaQZ(string yuanQuID,int menZhenZYBZ)
        {
            //����������12λ��ǰ3λ����������� + ��4λ��ʾ0���1סԺ��2���� + ��8λ˳��ţ�
            string qianZui="";
            var yuanQu =this.GetByKey(yuanQuID);

            if (yuanQu is null)
            { return qianZui; }
            if (string.IsNullOrEmpty(yuanQu.JIANYANJGJM))
            {
                qianZui = menZhenZYBZ.ToString();
            }
            else
            {
                qianZui = yuanQu.JIANYANJGJM+menZhenZYBZ.ToString();
            }
            //���û��ά���������룬��ô��ԭ��һ��ȡ'1'
            if (qianZui == "0")
            { qianZui = "1"; }
           
            return qianZui;
        }

        /// <summary>
        /// ����Ժ��idȡԺ������
        /// </summary>
        /// <param name="yuanQuID"></param>
        /// <returns></returns>
        public List<GY_YUANQU> GetList(string yuanQuID)
        {
            var YuanQuList = this.Set<GY_YUANQU>().Where(o => o.YUANQUID == yuanQuID).ToList().WithContext(this, ServiceContext);
            return YuanQuList;
        }
    }
}
