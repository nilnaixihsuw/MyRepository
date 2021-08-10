using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
namespace Mediinfo.Infrastructure.JCJG.GY
{
	public class GY_ZIFUBLRepository : RepositoryBase<GY_ZIFUBL>, IGY_ZIFUBLRepository
	{
		public GY_ZIFUBLRepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}

        /// <summary>
        /// ��ȡ�Ը�����
        /// </summary>
        /// <param name="feiYongKZID">���ÿ���Id</param>
        /// <param name="xiangMuID">��ĿID</param>
        /// <param name="xiangMuLX">��Ŀ����</param>
        /// <param name="faShengRQ">���õķ�������</param>
        /// <param name="menZhenZYBZ">����סԺ��־��0�����1��סԺ��</param>
        /// <returns>true����ʾȡ�����Ը�������false����ʾδȡ���Ը�����</returns>
        public bool GetZiFuBL(string feiYongKZID, string xiangMuID, string xiangMuLX, 
                              DateTime faShengRQ, int menZhenZYBZ,out decimal ziFuBL)
        {
            ziFuBL = 1.0m;

            var query = this.QuerySet<GY_ZIFUBL>()
                            .Where(c => c.FEIYONGKZID == feiYongKZID
                                 && c.XIANGMUID == xiangMuID
                                 && c.XIANGMULX == xiangMuLX);

            if (menZhenZYBZ == 0)
                query = query.Where(c => c.MENZHENSY == 1);
            else
                query = query.Where(c => c.ZHUYUANSY == 1);

            //���ݿ�ʼ���������ڽ����ж�
            DateTime dtBegin = faShengRQ.AddDays(-1);
            DateTime dtEnd = faShengRQ.AddDays(1);
            query = query.Where(c =>( c.KAISHIRQ.HasValue ? c.KAISHIRQ : dtBegin) <= faShengRQ 
                                  &&( c.JIESHURQ.HasValue ? c.JIESHURQ : dtEnd) >= faShengRQ);

            var ziFuBLEntity = query.OrderByDescending(c => c.XIUGAISJ).Select(c => c.ZIFUBL).FirstOrDefault();

            if (null == ziFuBLEntity)
            {
                return false;
            }
            else
            {
                //���Ϊ�գ��򷵻��Ը�����Ϊ1
                ziFuBL = ziFuBLEntity.HasValue ? ziFuBLEntity.Value : 1.0m;
                return true;
            }
        }

        public bool GetZiFuBL(string xiangMuID,string feiYongXZ, out decimal ziFuBL)
        {
            ziFuBL = 1.0m;
            var ziFuBLEntity = this.QuerySet<GY_ZIFUBL>().FirstOrDefault(c => c.FEIYONGKZID == feiYongXZ
                                && c.XIANGMUID == xiangMuID);
             
            if (null == ziFuBLEntity)
            {
                return false;
            }
            else
            {
                //���Ϊ�գ��򷵻��Ը�����Ϊ1
                ziFuBL = ziFuBLEntity.ZIFUBL?? 1.0m;
                return true;
            }
        }

        public List<GY_ZIFUBL> GetList(List<string> xiangMuIDS, string feiYongXZ)
        {
            return this.QuerySet<GY_ZIFUBL>().Where(w => xiangMuIDS.Contains(w.XIANGMUID) && w.FEIYONGKZID == feiYongXZ).ToList();
        }
    }
}
