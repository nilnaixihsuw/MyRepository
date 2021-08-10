using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
using Mediinfo.Infrastructure.Core.DBEntity;

namespace Mediinfo.Infrastructure.JCJG.GY
{
	public class GY_JIEZHIRepository : RepositoryBase<GY_JIEZHI>, IGY_JIEZHIRepository
	{
		public GY_JIEZHIRepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}
        /// <summary>
        /// ���ݲ���IDȡδ���Ͻ�����Ϣ
        /// </summary>
        /// <param name="bingRenID">����ID</param> 
        /// <returns></returns>
        public GY_JIEZHI GetJieZhiXX(string bingRenID ,int zuoFeiBZ)
        {
            var list = this.Set<GY_JIEZHI>().Where(w => w.BINGRENID == bingRenID&&w.ZUOFEIBZ== zuoFeiBZ).ToList().FirstOrDefault().WithContext(this, ServiceContext);
            return list;
        }
        /// <summary>
        ///  ���ݲ���ID�����ʺŻ�ȡδ���Ͻ�����Ϣ
        /// </summary>
        /// <param name="bingRenID">����ID</param>
        /// <param name="jieZhiHao">����ID</param>
        /// <returns></returns>
        public GY_JIEZHI GetJieZhiXX(string bingRenID, string jieZhiHao)
        {
            var list = this.Set<GY_JIEZHI>().Where(w => w.BINGRENID == bingRenID && w.JIEZHIHAO == jieZhiHao).ToList().FirstOrDefault().WithContext(this, ServiceContext);
            return list;
        }
        /// <summary>
        /// ���ݲ���idȡ����id�ͽ��ʺ���ͬ������
        /// </summary>
        /// <param name="bingRenID"></param>
        /// <returns></returns>
        public GY_JIEZHI GetJieZhiXXWithBRIDISJZH(string bingRenID)
        {
            var list = this.Set<GY_JIEZHI>().Where(w => w.BINGRENID == bingRenID && w.BINGRENID == w.JIEZHIHAO && w.ZUOFEIBZ == 0).ToList().FirstOrDefault().WithContext(this, ServiceContext);
            return list;
        }

        /// <summary>
        /// ���ݽ��ʺŻ�ȡ��¼
        /// </summary>
        /// <param name="jieZhiHao">���ʺ�</param> 
        /// <returns></returns>
        public GY_JIEZHI GetJieZhiXX(string jieZhiHao)
        {
            var list = this.Set<GY_JIEZHI>().Where(w => w.JIEZHIHAO == jieZhiHao).ToList().FirstOrDefault().WithContext(this, ServiceContext);
            return list;
        }
        /// <summary>
        /// ���ݲ���ID��ȡ�ϲ����Ľ�����Ϣ
        /// </summary>
        /// <param name="bingRenID">����id</param> 
        /// <returns></returns>
        public List<GY_JIEZHI> GetHeBingXX(string bingRenID)
        {
            var list = this.Set<GY_JIEZHI>().Where(w => w.BINGRENID == bingRenID && w.HEBINGBZ == "1").ToList().WithContext(this, ServiceContext);
            return list;
        }
    }
}
