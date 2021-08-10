using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.ZY;
using Mediinfo.Infrastructure.Core.DBEntity;
using Mediinfo.Domain.JCJG.GY;

namespace Mediinfo.Infrastructure.JCJG.ZY
{
	public class ZY_BINGRENXXRepository : RepositoryBase<ZY_BINGRENXX>, IZY_BINGRENXXRepository
	{  
		public ZY_BINGRENXXRepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}

        public List<ZY_BINGRENXX> GetBingRenXXByBQ(string dangQianBQ)
        {
            var list = this.Set<ZY_BINGRENXX>().Where(o=>o.DANGQIANBQ == dangQianBQ&&o.ZAIYUANZT == "0").ToList().WithContext(this, ServiceContext);
            return list;
        }
        /// <summary>
        /// 取住院病人信息by病人住院ID
        /// </summary>
        /// <param name="BingRenZYID">病人住院ID</param>
        /// <returns></returns>

        public List<ZY_BINGRENXX> GetBingRenXX(string BingRenZYID)
        {
            var list = this.Set<ZY_BINGRENXX>().Where(o =>o.BINGRENZYID == BingRenZYID ).ToList().WithContext(this, ServiceContext);
            return list;
        }

	    public List<ZY_BINGRENXX> QueryGetBingRenXX(string BingRenZYID)
	    {
	        var list = this.QuerySet<ZY_BINGRENXX>().Where(o => o.BINGRENZYID == BingRenZYID).ToList().WithContext(this, ServiceContext);
	        return list;
	    }


        /// <summary>
        /// 取住院病人信息包含婴儿的DesignBy Xieyz 2019-6-10
        /// </summary>
        /// <returns></returns>
        public List<ZY_BINGRENXX> GetYingErBRXX()
        {
            var list = this.Set<ZY_BINGRENXX>().Where(o => o.YINGERBZ == 1).ToList().WithContext(this, ServiceContext);
            return list;
        }


        /// <summary>
        /// 取出院病人信息by 住院号
        /// </summary>
        /// <param name="zhuYuanHao"></param>
        /// <returns></returns>
        public List<ZY_BINGRENXX> GetChuYuanBRXX(string zhuYuanHao)
        {
            var list = this.Set<ZY_BINGRENXX>().Where(o => o.ZHUYUANHAO == zhuYuanHao
            && o.ZAIYUANZT=="2").ToList().WithContext(this, ServiceContext);
            return list;
        }

        /// <summary>
        /// 取在院病人信息by住院号
        /// </summary>
        /// <param name="zhuYuanHao"></param>
        /// <returns></returns>
        public List<ZY_BINGRENXX> GetZaiYuanBRXX(string zhuYuanHao)
        {
            var list = this.Set<ZY_BINGRENXX>().Where(o => o.ZHUYUANHAO == zhuYuanHao
            && (o.ZAIYUANZT == "1"||o.ZAIYUANZT=="0")).ToList().WithContext(this, ServiceContext);
            return list;
        }

        /// <summary>
        /// 按住院号查询病人信息
        /// </summary>
        /// <param name="zhuYuanHao"></param>
        /// <returns></returns>
        public List<ZY_BINGRENXX> GetList(string zhuYuanHao)
        {
            var list = this.Set<ZY_BINGRENXX>().Where(o => o.ZHUYUANHAO == zhuYuanHao).ToList().WithContext(this, ServiceContext);
            return list;
        }


        /// <summary>
        /// 取在院病人信息by住院号
        /// </summary>
        /// <param name="bingAnHao"></param>
        /// <returns></returns>
        public List<ZY_BINGRENXX> GetZaiYuanBR(string bingAnHao)
        {
            var list = this.Set<ZY_BINGRENXX>().Where(o => o.BINGANHAO == bingAnHao
            && (o.ZAIYUANZT == "1" || o.ZAIYUANZT == "0")).ToList().WithContext(this, ServiceContext);
            return list;
        }

        /// <summary>
        /// 取在院婴儿病人信息by母亲住院iD
        /// </summary>
        /// <param name="BingRenZYID"></param>
        /// <returns></returns>
        public List<ZY_BINGRENXX> GetYingErXXByMQZYID(string BingRenZYID)
        {
            var list = this.Set<ZY_BINGRENXX>().Where(o => o.MUQINZYID == BingRenZYID && o.YINGERBZ==1 && o.ZAIYUANZT=="0").ToList().WithContext(this, ServiceContext);
            return list;
        }

        /// <summary>
        ///  取母婴信息by母亲的病人住院ID
        /// </summary>
        /// <param name="BingRenZYID">母亲的病人住院ID</param>
        /// <returns></returns>
        public List<ZY_BINGRENXX> GetMuQinYEList(string BingRenZYID)
        {
            var list = this.Set<ZY_BINGRENXX>().Where(o => o.BINGRENZYID == BingRenZYID || o.MUQINZYID == BingRenZYID).ToList().WithContext(this, ServiceContext);
            return list;
        }
        /// <summary>
        ///  取母婴信息by母亲的病人住院ID
        /// </summary>
        /// <param name="BingRenZYID">母亲的病人住院ID</param>
        /// <returns></returns>
        public List<ZY_BINGRENXX> GetMuQinYEListByBingRenZYID(string BingRenZYID)
        {
            var list = this.QuerySet<ZY_BINGRENXX>().Where(o => (o.BINGRENZYID == BingRenZYID && o.YINGERBZ == 0) || (o.MUQINZYID == BingRenZYID && o.YINGERBZ == 1)).ToList();
            return list;
        }
        /// <summary>
        /// 关联公用费用类别表
        /// </summary>
        /// <param name="BingRenZYID"></param>
        /// <returns></returns>
        public List<ZY_BINGRENXX> GetListByGYFeiYongLB(string BingRenZYID)
        {
            var list = (from zyBingRenXX in this.Set<ZY_BINGRENXX>()
                        join gyfeiyonglb in this.Set<GY_FEIYONGLB>()
                        on zyBingRenXX.FEIYONGLB equals gyfeiyonglb.LEIBIEID
                        where zyBingRenXX.BINGRENZYID == BingRenZYID && gyfeiyonglb.LEIBIESX.Substring(9, 1) == "1"
                        select zyBingRenXX).ToList();
            return list;


        }

        public List<ZY_BINGRENXX> GetBingRenIDList(string bingRenZYID)
        {
            var list = this.Set<ZY_BINGRENXX>().Where(o => o.MUQINZYID==bingRenZYID&&o.YINGERBZ==1&&(o.YINGERCWFJZZT==null||o.YINGERCWFJZZT==0)).ToList().WithContext(this, ServiceContext);
            return list;
        }

        /// <summary>
        /// 按照病人zyidlst 查询病人住院信息
        /// </summary>
        /// <param name="bingRenZYIDlst"></param>
        /// <returns></returns>
        public List<ZY_BINGRENXX> GetBingRenIDList(List<string> bingRenZYIDlst)
        {
            var list = this.QuerySet<ZY_BINGRENXX>().Where(o => bingRenZYIDlst.Contains(o.BINGRENZYID)).ToList();
            return list;
        }



        /// <summary>
        /// 取在院和预出院病人列表
        /// </summary>
        /// <returns></returns>
        public List<ZY_BINGRENXX> GetList()
        {
            var list = this.Set<ZY_BINGRENXX>().Where(o => o.ZAIYUANZT == "0"  || o.ZAIYUANZT == "1").ToList().WithContext(this, ServiceContext);
            return list;
        }

        public List<ZY_BINGRENXX> Get_BINGRENXXs(string keShiID, string bingQuID, DateTime beginTime)
        {
            //return this.Set<ZY_BINGRENXX>().Where(o => o.DANGQIANKS == keShiID && o.JIANDANGRQ < beginTime
            //&& (o.YUCHUYRQ == null || (o.YUCHUYRQ > beginTime && new string[] { "1", "2" }.Contains(o.ZAIYUANZT)))
            //&& o.MUQINZYID == null && o.DANGQIANBQ == bingQuID
            //                                    ).ToList().WithContext(this, ServiceContext);
            string sql = string.Format(@"Select Zy_Bingrenxx.*
        From Zy_Bingrenxx
       Where Dangqianks = {0}
         And Jiandangrq < {1}
         And ((Yuchuyrq > {1} And Zaiyuanzt In ('1', '2')) Or
             Yuchuyrq Is Null)
         And Muqinzyid Is Null
         and dangqianbq = {2}
         and bingrenzyid not in (select bingrenzyid from zy_yingerxx)", keShiID, beginTime, bingQuID);
            return this.SqlQuery<ZY_BINGRENXX>(sql).ToList().WithContext(this, ServiceContext);
        }
        public List<ZY_BINGRENXX> GetLvSeTDBRXX()
        {
            DateTime YDate = GetSYSTime().AddDays(-1);
            var list = this.Set<ZY_BINGRENXX>().Where(o =>o.LVSETDBZ == 1 && o.LVSETDKQRQ < YDate).ToList().WithContext(this,ServiceContext);
            return list;
        }

        public List<ZY_BINGRENXX> GetBingRenXXByKS(string dangQianKS)
        {
            var list = this.Set<ZY_BINGRENXX>().Where(o => o.DANGQIANKS == dangQianKS && o.ZAIYUANZT == "0").ToList().WithContext(this, ServiceContext);
            return list;
        }

        public List<ZY_BINGRENXX> GetYingErXXByZYIDList(List<string> bingRenZYIDList)
        {
            var list = this.QuerySet<ZY_BINGRENXX>().Where(o => bingRenZYIDList.Contains(o.MUQINZYID)&&o.YINGERBZ==1).ToList();
            return list;
        }
        /// <summary>
        /// 医生站框架获取病人信息
        /// </summary>
        /// <param name="bingRenZyID"></param>
        /// <returns></returns>
        public ZY_BINGRENXX GetKJBingRenXX(string bingRenZyID)
        {
            return this.QuerySet<ZY_BINGRENXX>().FirstOrDefault(o => o.BINGRENZYID == bingRenZyID);
        }
       
        /// <summary>
        /// 医生框架获婴儿信息
        /// </summary>
        /// <param name="bingRenZyID"></param>
        /// <returns></returns>
        public ZY_YINGERXX GetKJYingErXX(string bingRenZyID)
        {
            return this.QuerySet<ZY_YINGERXX>().FirstOrDefault(o => o.BINGRENZYID == bingRenZyID);
        }

        public List<ZY_BINGRENXX> GetAllZYBRXX(List<string> bingRenID)
        {
            var list = this.Set<ZY_BINGRENXX>().Where(o => bingRenID.Contains(o.BINGRENZYID)).ToList().WithContext(this, ServiceContext);
            return list;
        }
    }
}
