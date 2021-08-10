using Mediinfo.Domain.JCJG.GY;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.DBEntity;
using Mediinfo.Infrastructure.Core.Repository;

using System;
using System.Collections.Generic;
using System.Linq;

namespace Mediinfo.Infrastructure.JCJG.GY
{
    public class GY_YAOPINCDJG2Repository : RepositoryBase<GY_YAOPINCDJG2>, IGY_YAOPINCDJG2Repository
	{
		public GY_YAOPINCDJG2Repository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}

       
        public void SetCache()
        {
            var yaoPinCDJG2List = GetFromCache<List<GY_YAOPINCDJG2>>("YPCDJGLIST");
            if (yaoPinCDJG2List == null)
            {
              yaoPinCDJG2List = this.QuerySet<GY_YAOPINCDJG2>().Where(o => o.ZUOFEIBZ == 0 && o.TINGYONGBZ == 0).ToList().WithContext(this, ServiceContext);
                AddToCache<List<GY_YAOPINCDJG2>>("YPCDJGLIST", yaoPinCDJG2List);
            }
        }

        /// <summary>
        /// 根据规格ID及产地取产地价格信息
        /// </summary>
        /// <param name="guiGeID"></param>
        /// <param name="chanDi"></param>
        /// <returns></returns>0
        public List<GY_YAOPINCDJG2> GetList(string guiGeID, string chanDi)
        { 
           var  yaoPinCDJG2 = this.Set<GY_YAOPINCDJG2>().Where(o => o.GUIGEID == guiGeID && o.CHANDI == chanDi).ToList().WithContext(this, ServiceContext); 
               
            return yaoPinCDJG2;
        }

        public List<GY_YAOPINCDJG2> GetList(string xtGGID, string cd, int zfBZ = 0, int tyBZ = 0)
        {
            var yaoPinCDJG2 = this.Set<GY_YAOPINCDJG2>().Where(o => o.XIAOGUIGID == xtGGID && o.ZUOFEIBZ == zfBZ && o.TINGYONGBZ == tyBZ && o.CHANDI == cd).ToList().WithContext(this, ServiceContext);
            return yaoPinCDJG2;
        }

        /// <summary>
        /// 根据规格ID及产地取产地价格信息 (通过缓存)
        /// </summary>
        /// <param name="guiGeID"></param>
        /// <param name="chanDi"></param>
        /// <returns></returns>0
        public List<GY_YAOPINCDJG2> GetListFromCache(string guiGeID, string chanDi)
        {
            //var yaoPinCDJG2 = GetFromCache<List<GY_YAOPINCDJG2>>("YPCDGG" + guiGeID + "|" + chanDi);
            //if (yaoPinCDJG2 == null)
            //{
            //    yaoPinCDJG2 = this.Set<GY_YAOPINCDJG2>().Where(o => o.GUIGEID == guiGeID && o.ZUOFEIBZ == 0 && o.TINGYONGBZ == 0 && o.CHANDI == chanDi).ToList();
            //    AddToCache<List<GY_YAOPINCDJG2>>("YPCDGG" + guiGeID + "|" + chanDi, yaoPinCDJG2);
            //}


            //var yaoPinCDJG2List = GetFromCache<List<GY_YAOPINCDJG2>>("YPCDJGLIST");
            //if(yaoPinCDJG2List == null)
            //{
            //     SetCache();
            //    yaoPinCDJG2List = GetFromCache<List<GY_YAOPINCDJG2>>("YPCDJGLIST");
            //}


            var yaoPinCDJG2 = new List<GY_YAOPINCDJG2>();

            var yaoPinCDJG2List = GetFromCache<List<GY_YAOPINCDJG2>>("YPCDJGLIST");

            if (yaoPinCDJG2List == null)
            { 
                  yaoPinCDJG2  = this.Set<GY_YAOPINCDJG2>().Where(o => o.GUIGEID == guiGeID && o.ZUOFEIBZ == 0 && o.TINGYONGBZ == 0 && o.CHANDI == chanDi).ToList();
            }
            else
            {
                yaoPinCDJG2 = yaoPinCDJG2List.Where(o => o.GUIGEID == guiGeID && o.ZUOFEIBZ == 0 && o.TINGYONGBZ == 0 && o.CHANDI == chanDi).ToList();

            }


            return yaoPinCDJG2;
            
        }

        /// <summary>
        /// 根据价格ID获取信息
        /// </summary>
        /// <param name="JIAGEID"></param>
        /// <returns></returns>
        public List<GY_YAOPINCDJG2> GetList(string JIAGEID)
        {
            var list = this.Set<GY_YAOPINCDJG2>().Where(o => o.JIAGEID == JIAGEID).ToList().WithContext(this, ServiceContext);
            return list;
        }
        /// <summary>
        /// 根据价格ID获取信息
        /// </summary>
        /// <param name="JIAGEID"></param>
        /// <returns></returns>
        public List<GY_YAOPINCDJG2> GetList(List<string> jiaGeID)
        {
            var list = this.Set<GY_YAOPINCDJG2>().Where(o => jiaGeID.Contains(o.JIAGEID)).ToList().WithContext(this, ServiceContext);
            return list;
        }
        /// <summary>
        /// 获取信息
        /// </summary>
        /// <param name="zfBZ"></param>
        /// <param name="lingCJBZ"></param>
        /// <returns></returns>
        public List<GY_YAOPINCDJG2> GetList(int zfBZ)
        {
            var list = this.Set<GY_YAOPINCDJG2>().Where(o => o.ZUOFEIBZ == zfBZ && (o.LINGCHAJBZ == 1 || o.LINGCHAJBZ == 2)).ToList().WithContext(this, ServiceContext);
            return list;
        }

        /// <summary>
        /// 获取信息
        /// </summary>
        /// <param name="zfBZ"></param>
        /// <param name="lingCJBZ"></param>
        /// <returns></returns>
        public List<GY_YAOPINCDJG2> GetList()
        {
            var list = this.Set<GY_YAOPINCDJG2>().Where(o => o.ZUOFEIBZ == 0 ).ToList().WithContext(this, ServiceContext);
            return list;
        }

        public List<GY_YAOPINCDJG2> GetChanDi(string chanDi)
        {
            var list = this.Set<GY_YAOPINCDJG2>().Where(o => o.CHANDI == chanDi).ToList().WithContext(this, ServiceContext);
            return list;
        }

        /// <summary>
        /// 获取同一大规格，同一产地的所有药品
        /// </summary>
        /// <param name="daGuiGeID"></param>
        /// <param name="chanDi"></param>
        /// <returns></returns>
        public List<GY_YAOPINCDJG2> GetXiangTongYPList(string daGuiGeID, string chanDi)
        {
            var list = this.Set<GY_YAOPINCDJG2>().Where(o => o.DAGUIGID == daGuiGeID && o.CHANDI == chanDi).ToList().WithContext(this, ServiceContext);
            return list;
        }

        /// <summary>
        /// 获取同一大规格所有药品
        /// </summary>
        /// <param name="daGuiGeID"></param>
        /// <param name="chanDi"></param>
        /// <returns></returns>
        public List<GY_YAOPINCDJG2> GetListByDaGuiGeID(string daGuiGeID)
        {
            var list = this.Set<GY_YAOPINCDJG2>().Where(o => o.DAGUIGID == daGuiGeID ).ToList().WithContext(this, ServiceContext);
            return list;
        }


        /// <summary>
        /// 通过缓存获取药品信息
        /// </summary>
        /// <param name="jiaGeID"></param>
        /// <returns></returns>
        public GY_YAOPINCDJG2 GetByKeyFromCache(string jiaGeID)
        {
            var yaoPinCDJG2 = new GY_YAOPINCDJG2();
            var yaoPinCDJG2List = GetFromCache<List<GY_YAOPINCDJG2>>("YPCDJGLIST");
            if (yaoPinCDJG2List == null)
            {
                yaoPinCDJG2 = this.Set<GY_YAOPINCDJG2>().Where(o => o.JIAGEID == jiaGeID).FirstOrDefault();

            }
            else
            {
                yaoPinCDJG2 = yaoPinCDJG2List.Where(o => o.JIAGEID == jiaGeID).FirstOrDefault();
            }

            return yaoPinCDJG2;
        }

        /// <summary>
        /// 通过规格获取药品产地信息
        /// </summary>
        /// <param name="jiaGeID"></param>
        /// <returns></returns>
        public List<GY_YAOPINCDJG2> GetListByGuiGeID(string guiGeID)
        {  
             var  yaoPinCDJG2 = this.Set<GY_YAOPINCDJG2>().Where(c => c.GUIGEID == guiGeID).ToList().WithContext(this, ServiceContext);
            
            return yaoPinCDJG2; 
        }
        /// <summary>
        /// 通过规格获取药品产地信息
        /// </summary>
        /// <param name="jiaGeID"></param>
        /// <returns></returns>
        public List<GY_YAOPINCDJG2> GetListByGuiGeIDQ(string guiGeID)
        {
            var yaoPinCDJG2 = this.QuerySet<GY_YAOPINCDJG2>().Where(m => m.GUIGEID == guiGeID && m.ZUOFEIBZ == 0).ToList();
             
            return yaoPinCDJG2;
        }

        /// <summary>
        /// 获取与当前价格ID相同产地和大规格的药品
        /// </summary>
        /// <param name="jiaGeID"></param>
        /// <returns></returns>
        public List<GY_YAOPINCDJG2> GetTongGuiGeList(string jiaGeID)
        { 

            var yaoPinCDJGList = (from yaoPinJG in this.QuerySet<GY_YAOPINCDJG2>().Where(o => o.JIAGEID == jiaGeID)
                                  join yaoPinJG2 in this.QuerySet<GY_YAOPINCDJG2>()
                                        on new
                                        {
                                            DAGUIGID = yaoPinJG.DAGUIGID,
                                            CHANDI = yaoPinJG.CHANDI
                                        }
                                        equals
                                          new
                                          {
                                              DAGUIGID = yaoPinJG2.DAGUIGID,
                                              CHANDI = yaoPinJG2.CHANDI
                                          }
                                  select yaoPinJG2).ToList().WithContext(this, ServiceContext);

            return yaoPinCDJGList;

        }


        /// <summary>
        /// 根据零差价标志获取产地价格2信息
        /// </summary>
        /// <param name="lingChaJBZ"></param>
        /// <returns></returns>
        public List<GY_YAOPINCDJG2> GetListByLingChaJBZ(List<int?> lingChaJBZ)
        {
            var yaoPinCDJG2 = this.Set<GY_YAOPINCDJG2>().Where(c => c.ZUOFEIBZ == 0 && lingChaJBZ.Contains(c.LINGCHAJBZ)).ToList().WithContext(this, ServiceContext);
            return yaoPinCDJG2;
        }

        /// <summary>
        /// 根据产地及规格id获取产地价格2信息
        /// </summary>
        /// <param name="chanDi">产地</param>
        /// <param name="guiGeIDList">规格ID列表</param>
        /// <returns></returns>
        public List<GY_YAOPINCDJG2> GetListByChanDiGGID(string chanDi,List<string> guiGeIDList)
        {
            var yaoPinCDJG2 = this.Set<GY_YAOPINCDJG2>().Where(c => c.CHANDI == chanDi && guiGeIDList.Contains(c.GUIGEID) &&c.ZUOFEIBZ==0).ToList().WithContext(this, ServiceContext);
            return yaoPinCDJG2;
        }

        /// <summary>
        ///  联合YK_CHUKUDAN1、YK_CHUKUDAN2、GY_YAOPINMC和GY_YAOPINFL查询药品产地价格表
        /// </summary>
        /// <param name="yingYongID"></param>
        /// <param name="kaiShiSJ"></param>
        /// <param name="jieShuSJ"></param>
        /// <param name="danJuZT"></param>
        /// <param name="gongHuoBZ"></param>
        /// <param name="zuoFeiBZ"></param>
        /// <param name="tingYongBZ"></param>
        /// <returns></returns>
        //public List<GY_YAOPINCDJG2> GetList(string yingYongID, DateTime kaiShiSJ, DateTime jieShuSJ, string danJuZT, int gongHuoBZ, int zuoFeiBZ, int tingYongBZ)
        //{
        //    var list = (from chuKuD1 in this.Set<YK_CHUKUDAN1>()
        //                join chuKuD2 in this.Set<YK_CHUKUDAN2>() on chuKuD1.CHUKUDID equals chuKuD2.CHUKUDID
        //                join yaoPinCDJG2 in this.Set<GY_YAOPINCDJG2>() on chuKuD2.JIAGEID equals yaoPinCDJG2.JIAGEID
        //                join yaoPinMC in this.Set<GY_YAOPINMC>() on yaoPinCDJG2.YAOPINID equals yaoPinMC.YAOPINID
        //                join yaoPinFL in this.Set<GY_YAOPINFL>() on yaoPinMC.YAOPINFL equals yaoPinFL.YAOPINFLID
        //                where chuKuD1.YINGYONGID == yingYongID && chuKuD1.JIZHANGRQ >= kaiShiSJ && chuKuD1.JIZHANGRQ <= jieShuSJ
        //                && chuKuD1.DANJUZT==danJuZT && yaoPinCDJG2.GOUHUOBZ== gongHuoBZ && yaoPinCDJG2.ZUOFEIBZ==zuoFeiBZ
        //                && yaoPinCDJG2.TINGYONGBZ==tingYongBZ 
        //                select yaoPinCDJG2).ToList().WithContext(this, ServiceContext);
        //    return list;
        //}

        /// <summary>
        /// 联合YK_CHUKUDAN1、YK_CHUKUDAN2查询药品产地价格表
        /// </summary>
        /// <param name="yingYongID"></param>
        /// <param name="kaiShiSJ"></param>
        /// <param name="jieShuSJ"></param>
        /// <param name="danJuZT"></param>
        /// <returns></returns>
        //public List<GY_YAOPINCDJG2> GetList(string yingYongID, DateTime kaiShiSJ, DateTime jieShuSJ, string danJuZT)
        //{
        //    var list = (from chuKuD1 in this.Set<YK_CHUKUDAN1>()
        //                join chuKuD2 in this.Set<YK_CHUKUDAN2>() on chuKuD1.CHUKUDID equals chuKuD2.CHUKUDID
        //                join yaoPinCDJG2 in this.Set<GY_YAOPINCDJG2>() on chuKuD2.JIAGEID equals yaoPinCDJG2.JIAGEID
        //                where chuKuD1.YINGYONGID == yingYongID && chuKuD1.JIZHANGRQ >= kaiShiSJ && chuKuD1.JIZHANGRQ <= jieShuSJ && chuKuD1.DANJUZT == danJuZT
        //                select yaoPinCDJG2).ToList().WithContext(this, ServiceContext);
        //    return list;
        //}

        /// <summary>
        /// 联合GY_QINGLINGDAN1和GY_QINGLINGDAN2查询药品产地价格表
        /// </summary>
        /// <param name="yingYongID"></param>
        /// <param name="kaiShiSJ"></param>
        /// <param name="jieShuSJ"></param>
        /// <param name="qingLingZT"></param>
        /// <param name="qingLingLX"></param>
        /// <returns></returns>
        public List<GY_YAOPINCDJG2> GetList(string yingYongID, DateTime kaiShiSJ, DateTime jieShuSJ, string qingLingZT,string qingLingLX)
        {
            var list = (from qingLingD1 in this.Set<GY_QINGLINGDAN1>()
                        join qingLingD2 in this.Set<GY_QINGLINGDAN2>() on qingLingD1.QINGLINGDID equals qingLingD2.QINGLINGDID
                        join yaoPinCDJG2 in this.Set<GY_YAOPINCDJG2>() on qingLingD2.WUPINID equals yaoPinCDJG2.JIAGEID
                        where qingLingD1.BEIQINGLYYID == yingYongID && qingLingD2.SHENQINGRQ >= kaiShiSJ && qingLingD2.SHENQINGRQ <= jieShuSJ && qingLingD2.QINGLINGZT == qingLingZT && qingLingD1.QINGLINGLX == qingLingLX
                        select yaoPinCDJG2).ToList().WithContext(this, ServiceContext);
            return list;
        }

        /// <summary>
        /// 联合GY_QINGLINGDAN1、GY_QINGLINGDAN2查询药品产地价格表
        /// </summary>
        /// <param name="yingYongID"></param>
        /// <param name="kaiShiSJ"></param>
        /// <param name="jieShuSJ"></param>
        /// <param name="qingLingZT"></param>
        /// <returns></returns>
        public List<GY_YAOPINCDJG2> GetYaoPinCDJG2ByQL(string yingYongID, DateTime kaiShiSJ, DateTime jieShuSJ, string qingLingZT)
        {
            var list = (from qingLingD1 in this.Set<GY_QINGLINGDAN1>()
                        join qingLingD2 in this.Set<GY_QINGLINGDAN2>() on qingLingD1.QINGLINGDID equals qingLingD2.QINGLINGDID
                        join yaoPinCDJG2 in this.Set<GY_YAOPINCDJG2>() on qingLingD2.WUPINID equals yaoPinCDJG2.JIAGEID
                        where qingLingD1.BEIQINGLYYID == yingYongID && qingLingD2.SHENQINGRQ >= kaiShiSJ && qingLingD2.SHENQINGRQ <= jieShuSJ && qingLingD2.QINGLINGZT == qingLingZT && new string[] { "0", "1", "2" }.Contains(qingLingD1.QINGLINGLX)
                        select yaoPinCDJG2).ToList().WithContext(this, ServiceContext);
            return list;
        }

        /// <summary>
        /// 联合YK_KUCUN1、GY_YAOPINMC和GY_YAOPINFL查询药品产地价格表
        /// </summary>
        /// <param name="yingYongID"></param>
        /// <param name="gongHuoBZ"></param>
        /// <param name="tingYongBZ"></param>
        /// <param name="zuoFeiBZ"></param>
        /// <returns></returns>
        //public List<GY_YAOPINCDJG2> GetList(string yingYongID,int gongHuoBZ, int tingYongBZ, int zuoFeiBZ)
        //{
        //    var list = (from yaoPinCDJG2 in this.Set<GY_YAOPINCDJG2>()
        //                join kunCun1 in this.Set<YK_KUCUN1>() on yaoPinCDJG2.JIAGEID equals kunCun1.JIAGEID
        //                join yaoPinMC in this.Set<GY_YAOPINMC>() on yaoPinCDJG2.YAOPINID equals yaoPinMC.YAOPINID
        //                join yaoPinFL in this.Set<GY_YAOPINFL>() on yaoPinMC.YAOPINFL equals yaoPinFL.YAOPINFLID
        //                where kunCun1.YINGYONGID==yingYongID && yaoPinCDJG2.TINGYONGBZ == tingYongBZ && yaoPinCDJG2.ZUOFEIBZ ==zuoFeiBZ &&yaoPinCDJG2.GOUHUOBZ == gongHuoBZ
        //                select yaoPinCDJG2).ToList().WithContext(this,ServiceContext);
        //    return list;
        //}

        /// <summary>
        /// 根据价格id列表获取产地价格2信息
        /// </summary>
        /// <param name="jiaGeIDList"></param>
        /// <returns></returns>
        public List<GY_YAOPINCDJG2> GetList(HashSet<string> jiaGeIDList)
        {
            var yaoPinCDJG2 = this.Set<GY_YAOPINCDJG2>().Where(c => jiaGeIDList.Contains(c.JIAGEID)).ToList().WithContext(this, ServiceContext);
            return yaoPinCDJG2;
        }

        public List<GY_YAOPINCDJG2> GetList(int weiShu, List<string> yaoPinLX, List<string> yaoPinID)
        {
            //这个方法是批量调价获取字典表
            return null;
        }

        public List<GY_YAOPINCDJG2> GetYaoPinLst(List<string> lstguiGeID)
        {
            var list = this.Set<GY_YAOPINCDJG2>().Where(o => lstguiGeID.Contains(o.GUIGEID)).ToList().WithContext(this, ServiceContext);
            return list;
        }

        public List<GY_YAOPINCDJG2> GetList(string yingYongID, DateTime kaiShiSJ, DateTime jieShuSJ, string danJuZT, int gongHuoBZ, int zuoFeiBZ, int tingYongBZ)
        {
            throw new NotImplementedException();
        }

        public List<GY_YAOPINCDJG2> GetList(string yingYongID, DateTime kaiShiSJ, DateTime jieShuSJ, string danJuZT)
        {
            throw new NotImplementedException();
        }

        public List<GY_YAOPINCDJG2> GetList(string yingYongID, int gongHuoBZ, int tingYongBZ, int zuoFeiBZ)
        {
            throw new NotImplementedException();
        }
    }
}
