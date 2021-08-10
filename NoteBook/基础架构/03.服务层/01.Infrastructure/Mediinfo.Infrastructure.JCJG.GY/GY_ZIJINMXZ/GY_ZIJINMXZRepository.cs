using Mediinfo.Domain.JCJG.GY;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.DBEntity;
using Mediinfo.Infrastructure.Core.Repository;

using System.Collections.Generic;
using System.Linq;

namespace Mediinfo.Infrastructure.JCJG.GY
{
    public class GY_ZIJINMXZRepository : RepositoryBase<GY_ZIJINMXZ>, IGY_ZIJINMXZRepository
	{
		public GY_ZIJINMXZRepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}
        /// <summary>
        /// �����˻�idȡ�ʽ���ϸ����Ϣ
        /// </summary>
        /// <param name="zhangHuID">�˻�id</param>
        /// <returns></returns>
        public List<GY_ZIJINMXZ> GetList(string zhangHuID)
        {
            var ziJinMXZJEList = this.Set<GY_ZIJINMXZ>().Where(o => o.ZHANGHUID == zhangHuID).ToList().WithContext(this, ServiceContext);
            return ziJinMXZJEList;
        }

        /// <summary>
        /// �����˻�idȡδ��ת�ʽ���ϸ����Ϣ
        /// </summary>
        /// <param name="zhangHuID"></param>
        /// <returns></returns>
        public List<GY_ZIJINMXZ> GetWeiJieZMXZList(string zhangHuID)
        {
            var list = this.Set<GY_ZIJINMXZ>().Where(o => o.ZHANGHUID == zhangHuID && string.IsNullOrWhiteSpace(o.JIESUANID)).ToList().WithContext(this, ServiceContext);
            return list;
        }

        public decimal GetFaShengJE(string zhangHuID)
        {
            var faShengJE = this.Set<GY_ZIJINMXZ>().Where(o => o.ZHANGHUID == zhangHuID).ToList().WithContext(this, ServiceContext).Sum(p => p.FASHENGJE) ?? 0; 
            return faShengJE;
        }


        /// <summary>
        /// �����˻�idȡ����Ԥ������Ϣ��JIAOYIFS=="3"������Ԥ����   9��Ƿ�ѹ���
        /// </summary>
        /// <param name="zhangHuID">�˻�id</param>
        /// <returns></returns>
        public decimal GetMZYuJiaoKJE(string zhangHuID)
        {
            var faShengJE = this.Set<GY_ZIJINMXZ>().Where(o => o.ZHANGHUID == zhangHuID && (o.JIAOYIFS=="3" ||o.JIAOYIFS=="9" )).ToList().WithContext(this, ServiceContext).Sum(p => p.FASHENGJE) ?? 0; ;
            return faShengJE;
        }

        /// <summary>
        /// �����˻�id Ƿ�ѱ�־ ȡ��ֵ��ϸ��
        /// </summary>
        /// <param name="zhangHuID"></param>
        /// <param name="qianFeiBZ"></param>
        /// <returns></returns>
        public List<GY_ZIJINMXZ> GetChongZhiMXZ(string zhangHuID,int qianFeiBZ)
        {
            var list = (
                   from GY_ZIJINMXZ in this.Set<GY_ZIJINMXZ>()
                   where
                   GY_ZIJINMXZ.ZHIFUFS != null &&
                   GY_ZIJINMXZ.ZHANGHUID == zhangHuID &&
                   GY_ZIJINMXZ.YEWULX == "1" &&
                   (GY_ZIJINMXZ.FASHENGJE - ((System.Decimal?)GY_ZIJINMXZ.SHIYONGJE ?? (System.Decimal?)0)) >= 0 &&
                   qianFeiBZ == 0
                   select GY_ZIJINMXZ
               ).Concat
               (
                   from GY_ZIJINMXZ in this.Set<GY_ZIJINMXZ>()
                   where
                   GY_ZIJINMXZ.ZHANGHUID == zhangHuID &&
                   GY_ZIJINMXZ.YEWULX == "12" &&
                   (GY_ZIJINMXZ.FASHENGJE - ((System.Decimal?)GY_ZIJINMXZ.SHIYONGJE ?? (System.Decimal?)0)) >= 0 &&
                   qianFeiBZ == 1
                   select GY_ZIJINMXZ
               ).Concat
               (
                   from GY_ZIJINMXZ in this.Set<GY_ZIJINMXZ>()
                   where
                   GY_ZIJINMXZ.ZHANGHUID == zhangHuID &&
                   GY_ZIJINMXZ.YEWULX == "13" &&
                   (GY_ZIJINMXZ.FASHENGJE - ((System.Decimal?)GY_ZIJINMXZ.SHIYONGJE ?? (System.Decimal?)0)) >= 0 &&
                   qianFeiBZ == 0
                   select GY_ZIJINMXZ
               ).OrderByDescending(o => o.CAOZUORQ).ToList().WithContext(this, ServiceContext);

           
        return list;
        }
        /// <summary>
        /// �����˻�id ֧����ʽ��ȡ�ֱ�־��Ƿ�ѱ�־ ȡ��ֵ��ϸ��
        /// </summary>
        /// <param name="zhangHuID"></param>
        /// <param name="zhiFuFS"></param>
        /// <param name="quXianBZ"></param>
        /// <param name="qianFeiBZ"></param>
        /// <returns></returns>
        public List<GY_ZIJINMXZ> GetChongZhiMXZ(string zhangHuID, string zhiFuFS,int quXianBZ,int qianFeiBZ)
        {
            var list = (
                   from GY_ZIJINMXZ in this.Set<GY_ZIJINMXZ>()
                   where
                   GY_ZIJINMXZ.ZHIFUFS != null &&
                   GY_ZIJINMXZ.ZHANGHUID == zhangHuID &&
                   GY_ZIJINMXZ.YEWULX == "1" && //-- ��ֵ
                   ((GY_ZIJINMXZ.ZHIFUFS == zhiFuFS && quXianBZ == 1) || quXianBZ == 2) &&
                   qianFeiBZ ==  0
                   select GY_ZIJINMXZ
               ).Concat
               (
                   from GY_ZIJINMXZ in this.Set<GY_ZIJINMXZ>()
                   where
                   GY_ZIJINMXZ.ZHANGHUID == zhangHuID &&
                   GY_ZIJINMXZ.YEWULX == "12" && //--Ƿ�ѳ�ֵ
                   qianFeiBZ == 1
                   select GY_ZIJINMXZ
               ).Concat
               (
                   from GY_ZIJINMXZ in this.Set<GY_ZIJINMXZ>()
                   where
                   GY_ZIJINMXZ.ZHANGHUID == zhangHuID &&
                   GY_ZIJINMXZ.YEWULX == "13" && //-- ��������ֵ                 
                   qianFeiBZ == 0
                   select GY_ZIJINMXZ
               ).OrderByDescending(o=>o.CAOZUORQ).ToList().WithContext(this, ServiceContext);


            return list;
        }

        public List<GY_ZIJINMXZ> GetTuiFeiZJMXZ(string zhangHuID, string jieSuanID, int qianFeiZT)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// �����˻�ID ����ID,Ƿ�ѱ�־��ȡҪ�˷ѵ� �ʽ���ϸ��
        /// </summary>
        /// <param name="zhangHuID"></param>
        /// <param name="jieSuanID"></param>
        /// <param name="qianFeiZT"></param>
        /// <returns></returns>
        //public List<GY_ZIJINMXZ> GetTuiFeiZJMXZ(string zhangHuID, string jieSuanID, int qianFeiZT)
        //{
        //    var dbZhiFu = this.Set<MZ_ZHIFU>();
        //    var dbZiJinMXZ = this.Set<GY_ZIJINMXZ>();
        //    var list = (from GY_ZIJINMXZ in dbZiJinMXZ
        //                where
        //                  GY_ZIJINMXZ.ZHANGHUID == zhangHuID &&
        //                    (from MZ_ZHIFU in dbZhiFu
        //                     where MZ_ZHIFU.DANGQIANJSID == jieSuanID
        //                     select new
        //                     {
        //                         MZ_ZHIFU.JIESUANID
        //                     }).Contains(new { JIESUANID = GY_ZIJINMXZ.JIESUANID }) &&
        //                  (qianFeiZT == 0 || (GY_ZIJINMXZ.YEWULX == "9" && qianFeiZT == 1))
        //                orderby
        //                  GY_ZIJINMXZ.SHUNXUHAO descending
        //                select GY_ZIJINMXZ).ToList();

        //    return list;
        //}
    }
}
