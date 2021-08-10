using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
using Mediinfo.Infrastructure.Core.DBEntity;

namespace Mediinfo.Infrastructure.JCJG.GY
{
	public class GY_CHUANGWEIRepository : RepositoryBase<GY_CHUANGWEI>, IGY_CHUANGWEIRepository
    {
		public GY_CHUANGWEIRepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}


        
        /// <summary>
        /// ȡ��λ��Ϣ(��λid�Ͳ���id����������)
        /// </summary>
        /// <param name="ChuangWeiID">��λID</param>
        /// <param name="BingQuID">����ID</param>
        /// <returns></returns>
        public GY_CHUANGWEI GetByKey(string chuangWeiID, string bingQuID)
        {
            var gyChuangWei = this.Set<GY_CHUANGWEI>().Where(o => o.CHUANGWEIID == chuangWeiID && o.BINGQUID == bingQuID).FirstOrDefault().WithContext(this, ServiceContext);
            return gyChuangWei;
        }



        /// <summary>
        /// ȡһ����������δ����δ��ʹ�õĴ�λ��Ϣ
        /// </summary>
        /// <param name="FangJianID">����ID</param>
        /// <returns></returns>
        public List<GY_CHUANGWEI> GetWeiZuoFFJCW(string FangJianID)
        {
            var list = this.Set<GY_CHUANGWEI>().Where(o => o.FANGJIANID == FangJianID && o.CHUANGWEIZT != "0" && o.ZUOFEIBZ==0).ToList().WithContext(this, ServiceContext);
            return list;
        }

        /// <summary>
        /// ȡͬ���䴲λ
        /// </summary>
        /// <param name="FangJianID"></param>
        /// <param name="ChuangWeiID"></param>
        /// <returns></returns>
        public List<GY_CHUANGWEI> GetTongFangJianCW(string FangJianID, string ChuangWeiID)
        {
            var list = this.Set<GY_CHUANGWEI>().Where(o => o.FANGJIANID == FangJianID && o.CHUANGWEIID!= ChuangWeiID).ToList().WithContext(this, ServiceContext);
            return list;
        }


        /// <summary>
        /// ȡ���˰���
        /// </summary>
        /// <param name="BingrenZYID"></param>
        /// <param name="ChuangWeiID"></param>
        /// <returns></returns>
        public List<GY_CHUANGWEI> GetBingRenBCList(string BingrenZYID, string ChuangWeiID)
        {
            var list = this.Set<GY_CHUANGWEI>().Where(o => o.BINGRENZYID == BingrenZYID && o.CHUANGWEIID != ChuangWeiID && o.CHUANGWEIZT!="9").ToList().WithContext(this, ServiceContext);
            return list;
        }
        /// <summary>
        /// ���ݲ���סԺiDȡ���˴�λ��Ϣ
        /// </summary>
        /// <param name="BingrenZYID"></param>
        /// <returns></returns>
        public List<GY_CHUANGWEI> GetBingRenCWXX(string BingrenZYID)
        {
            var list = this.Set<GY_CHUANGWEI>().Where(o => o.BINGRENZYID == BingrenZYID).ToList().WithContext(this, ServiceContext);
            return list;
        }

        /// <summary>
        /// ���ݷ���ID��ȡ�÷������д�λ
        /// </summary>
        /// <param name="fangJianID">����ID</param>
        /// <returns></returns>
        public List<GY_CHUANGWEI> GetFangJianCW(string fangJianID)
        {
            var list = this.Set<GY_CHUANGWEI>().Where(O => O.FANGJIANID == fangJianID).ToList().WithContext(this, ServiceContext);
            return list;
        }
        public List<GY_CHUANGWEI> GetChuangWeiIDList(string fangJianID,string chuangWeiID)
        {
            var list = this.Set<GY_CHUANGWEI>().Where(o => o.FANGJIANID == fangJianID&&o.CHUANGWEIID!=chuangWeiID).OrderBy(o=>o.CHUANGWEIID).ToList().WithContext(this, ServiceContext);
            return list;
        }

        public int JianChanCW(string keShiID,ref string message)
        {
            var chuangWei = this.Set<GY_CHUANGWEI>();
            var keShiBQ = this.Set<GY_KESHIBQ>();
            var bingQu = this.Set<GY_BINGQU>();
            int num = 1;

            int count = (from gychuangWei in chuangWei
                         join gykeShiBQ in keShiBQ
                         on gychuangWei.BINGQUID equals gykeShiBQ.BINGQUID
                         where gykeShiBQ.KESHIID==keShiID
                         && gychuangWei.CHUANGWEIZT == "0"
                         && gychuangWei.ZUOFEIBZ ==0
                         select gychuangWei).ToList().Count;
            //�ж����������Ƿ��д�λ
            if (count<=0)
            {
                count = (from gychuangWei in chuangWei
                         join gykeShiBQ in keShiBQ
                         on gychuangWei.BINGQUID equals gykeShiBQ.BINGQUID
                         where gykeShiBQ.KESHIID != keShiID
                         && gychuangWei.CHUANGWEIZT == "0"
                         && gychuangWei.ZUOFEIBZ == 0
                         select gychuangWei).ToList().Count;

                if(count>0)
                {
                    var bingQuMC= (from gychuangWei in chuangWei
                                   join gykeShiBQ in keShiBQ
                                   on gychuangWei.BINGQUID equals gykeShiBQ.BINGQUID
                                   join gybingQu in bingQu
                                   on gychuangWei.BINGQUID equals gybingQu.BINGQUID
                                   where gykeShiBQ.KESHIID != keShiID
                                   && gychuangWei.CHUANGWEIZT == "0"
                                   && gychuangWei.ZUOFEIBZ == 0
                                   select gybingQu.BINGQUMC).ToList().FirstOrDefault();
                    num = -2;
                    message = "�����Ҷ�Ӧ����û�д�λ����"+bingQuMC+ "���д�λ���Ƿ����תסԺ?";
                }
                else
                {
                    num = -1;
                    message = "Ŀǰ���в�����û�д�λ���Ƿ����תסԺ?";
                }
            }
            return num;
        }

        public List<GY_CHUANGWEI> GetChuangWeis(string keShiID, string bingQuID)
        {
            return this.Set<GY_CHUANGWEI>().Where(o=>o.KESHIID==keShiID&&o.BINGQUID==bingQuID).ToList().WithContext(this, ServiceContext);
        }
    }
}
