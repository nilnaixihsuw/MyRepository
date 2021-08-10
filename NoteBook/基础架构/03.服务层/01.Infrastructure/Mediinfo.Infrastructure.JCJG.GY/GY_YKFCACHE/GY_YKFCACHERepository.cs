using Mediinfo.Domain.JCJG.GY;
using Mediinfo.Domain.JCJG.GY.GY_YKFCACHE;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Redis;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Utility.Extensions;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mediinfo.Infrastructure.JCJG.GY
{
    public class GY_YKFCACHERepository : RepositoryBase<GY_CANSHU>, IGY_YKFCACHERepository
    {
        public GY_YKFCACHERepository(IRepositoryContext context, ServiceContext sContext) : base(context, sContext) { }
        /// <summary>
        /// 获取库存缓存
        /// </summary>
        /// <param name="jiaGeID"></param>
        /// <param name="yingYongID"></param>
        /// <returns></returns>
        public GY_KUCUNCACHE GetKuCunCache(string jiaGeID, string yingYongID)
        {
            GY_KUCUNCACHE result = RedisClient.Clinent.Get<GY_KUCUNCACHE>("GY_KUCUN:JIAGEID:" + yingYongID + ":" + jiaGeID);
            if (result != null)
            {
                return result;
            }

            result = GetKuCun(jiaGeID, yingYongID);

            //刷新缓存
            Task.Run(() =>
            {
                KuCunCacheRefresh(jiaGeID);
            });

            return result;
        }
        /// <summary>
        /// 获取库存列表缓存
        /// </summary>
        /// <param name="guiGeID"></param>
        /// <param name="yingYongID"></param>
        /// <returns></returns>
        public List<GY_KUCUNCACHE> GetKuCunListCache(string guiGeID, string yingYongID)
        {
            List<GY_KUCUNCACHE> result = RedisClient.Clinent.Get<List<GY_KUCUNCACHE>>("GY_KUCUN:GUIGEID:" + yingYongID + ":" + guiGeID);
            if (result != null)
            {
                return result;
            }

            result = GetKuCunList(guiGeID, yingYongID);

            //刷新缓存
            Task.Run(() =>
            {
                List<string> jiaGeIDList = this.QuerySet<GY_YAOPINCDJG2>().Where(m => m.GUIGEID == guiGeID && m.ZUOFEIBZ == 0).Select(m => m.JIAGEID).ToList();
                foreach (var jiaGeID in jiaGeIDList)
                {
                    KuCunCacheRefresh(jiaGeID);
                }
            });

            return result;
        }
        /// <summary>
        /// 获取药品字典缓存
        /// </summary>
        /// <param name="jiaGeID"></param>
        /// <param name="yingYongID"></param>
        /// <returns></returns>
        public GY_YAOPINZDCACHE GetYaoPinZdCache(string jiaGeID, string yingYongID)
        {
            GY_YAOPINZDCACHE result = RedisClient.Clinent.Get<GY_YAOPINZDCACHE>("GY_YAOPINZD:JIAGEID:" + yingYongID + ":" + jiaGeID);
            if (result != null)
            {
                return result;
            }

            result = GetYaoPinZd(jiaGeID, yingYongID);

            //刷新缓存
            Task.Run(() =>
            {
                YaoPinZdCacheRefresh(jiaGeID);
            });

            return result;
        }
        /// <summary>
        /// 获取药品字典列表缓存
        /// </summary>
        /// <param name="guiGeID"></param>
        /// <param name="yingYongID"></param>
        /// <returns></returns>
        public List<GY_YAOPINZDCACHE> GetYaoPinZdListCache(string guiGeID, string yingYongID)
        {
            List<GY_YAOPINZDCACHE> result = RedisClient.Clinent.Get<List<GY_YAOPINZDCACHE>>("GY_YAOPINZD:GUIGEID:" + yingYongID + ":" + guiGeID);
            if (result != null)
            {
                return result;
            }

            result = GetYaoPinZdList(guiGeID, yingYongID);

            //刷新缓存
            Task.Run(() =>
            {
                List<string> jiaGeIDList = this.QuerySet<GY_YAOPINCDJG2>().Where(m => m.GUIGEID == guiGeID && m.ZUOFEIBZ == 0).Select(m => m.JIAGEID).ToList();
                foreach (var jiaGeID in jiaGeIDList)
                {
                    YaoPinZdCacheRefresh(jiaGeID);
                }
            });

            return result;
        }
        /// <summary>
        /// 获取库存缓存
        /// </summary>
        /// <param name="JiaGeID"></param>
        /// <returns></returns>
        public GY_KUCUNCACHE GetKuCun(string jiaGeID, string yingYongID)
        {
            GY_KUCUNCACHE ykfCACHE = new GY_KUCUNCACHE();
            var yaoPinCDJG2 = this.QuerySet<GY_YAOPINCDJG2>().Where(o => o.JIAGEID == jiaGeID).FirstOrDefault();
            var baoZhuangLiang = yaoPinCDJG2.BAOZHUANGLIANG.GetDecimal();
            var yaoPinID = yaoPinCDJG2.YAOPINID;
            var guiGeID = yaoPinCDJG2.GUIGEID;
            var yaoPinMCGG2 = this.QuerySet<GY_YAOPINMCGG2>().Where(o => o.GUIGEID == guiGeID).FirstOrDefault();
            var maxJiaGeID = this.QuerySet<GY_YAOPINCDJG2>().Where(o => o.GUIGEID == yaoPinCDJG2.DAGUIGID && o.CHANDI == yaoPinCDJG2.CHANDI).FirstOrDefault()?.JIAGEID;
            var minJiaGeID = this.QuerySet<GY_YAOPINCDJG2>().Where(o => o.GUIGEID == yaoPinCDJG2.XIAOGUIGID && o.CHANDI == yaoPinCDJG2.CHANDI).FirstOrDefault()?.JIAGEID;
            if (minJiaGeID.IsNullOrWhiteSpace())
            {
                minJiaGeID = maxJiaGeID;
            }
            decimal? kuCunSL = 0;
            decimal? yishouWFSL = 0;
            //系统ID
            var xiTongID = yingYongID.Substring(0, 2);

            //药品库存字段
            if (xiTongID == "05")
            {
                //kuCunSL = this.QuerySet<MY_KUCUN1>().Where(o => o.JIAGEID == minJiaGeID && o.YINGYONGID == yingYongID).FirstOrDefault()?.KUCUNSL.GetDecimal();
                //yishouWFSL = this.QuerySet<MY_KUCUN1>().Where(o => o.JIAGEID == minJiaGeID && o.YINGYONGID == yingYongID).FirstOrDefault()?.YISHOUWFSL.GetDecimal();
            }
            else if (xiTongID == "06")
            {
                //kuCunSL = this.QuerySet<YK_KUCUN1>().Where(o => o.JIAGEID == minJiaGeID && o.YINGYONGID == yingYongID).FirstOrDefault()?.KUCUNSL.GetDecimal();
                yishouWFSL = 0;
            }
            //数量转换
            if (jiaGeID != minJiaGeID)
            {
                kuCunSL = kuCunSL * baoZhuangLiang;
                yishouWFSL = yishouWFSL * baoZhuangLiang;
            }

            //药品存量控制字段
            ykfCACHE.CACHEID = yingYongID + ":" + jiaGeID;
            ykfCACHE.YINGYONGID = yingYongID;
            ykfCACHE.MINJIAGEID = minJiaGeID;
            ykfCACHE.JIAGEID = jiaGeID;
            ykfCACHE.GUIGEID = guiGeID;
            ykfCACHE.KUCUNSL = kuCunSL;
            ykfCACHE.YISHOUWFSL = yishouWFSL;


            return ykfCACHE;
        }
        /// <summary>
        /// 满足HIS1的取库存数量
        /// </summary>
        /// <param name="jiaGeID"></param>
        /// <param name="yingYongID"></param>
        /// <returns></returns>
        public GY_KUCUNCACHE GetKuCunXH(string jiaGeID, string yingYongID, string guiGeID)
        {
            GY_KUCUNCACHE ykfCACHE = new GY_KUCUNCACHE();
            //取药品本身的信息
            var yaoPinCDJG2 = this.QuerySet<GY_YAOPINCDJG2>().FirstOrDefault(o => o.JIAGEID == jiaGeID);
            if (yaoPinCDJG2 != null)
            {
                decimal baoZhuangLiang = yaoPinCDJG2.BAOZHUANGLIANG.GetDecimal();
                var guiGeIDS = yaoPinCDJG2.GUIGEID;
                var fzfl = yaoPinCDJG2.FZFL.ToInt();
                var yaoPinLX = yaoPinCDJG2.YAOPINLX;
                var qiTaSX = yaoPinCDJG2.QITASX;

                if (yaoPinLX == "1")
                {
                    if (qiTaSX.Substring(89, 2) == "11")
                        baoZhuangLiang = 1;
                }
                else if (yaoPinLX == "2")
                {
                    if (qiTaSX.Substring(92, 2) == "11")
                        baoZhuangLiang = 1;
                }
                else if (yaoPinLX == "3")
                {
                    if (qiTaSX.Substring(95, 2) == "11")
                        baoZhuangLiang = 1;
                }

                if (fzfl > 0)
                {
                    if (baoZhuangLiang != 1)
                    {
                        guiGeIDS = fzfl.ToStringEx();
                    }
                }

                var guiGeID_Int = guiGeIDS.ToInt();

                decimal? kuCunSL = 0;
                decimal? yishouWFSL = 0;

                //药品存量控制字段
                ykfCACHE.CACHEID = yingYongID + ":" + jiaGeID;
                ykfCACHE.YINGYONGID = yingYongID;
                ykfCACHE.MINJIAGEID = "";
                ykfCACHE.JIAGEID = jiaGeID;
                ykfCACHE.GUIGEID = guiGeID;
                ykfCACHE.KUCUNSL = kuCunSL;
                ykfCACHE.YISHOUWFSL = yishouWFSL;
                ykfCACHE.GUIGEIDS = guiGeIDS;
                ykfCACHE.FZFL = guiGeID_Int;
            }

            return ykfCACHE;
        }

        /// <summary>
        /// 获取库存缓存列表，根据规格ID
        /// </summary>
        /// <param name="guiGeID"></param>
        /// <param name="yingYongID"></param>
        /// <returns></returns>
        public List<GY_KUCUNCACHE> GetKuCunList(string guiGeID, string yingYongID)
        {
            List<GY_YAOPINCDJG2> gy_YAOPINCDJG2List = this.QuerySet<GY_YAOPINCDJG2>().Where(o => o.GUIGEID == guiGeID && o.ZUOFEIBZ == 0).ToList();
            List<GY_KUCUNCACHE> result = new List<GY_KUCUNCACHE>();
            foreach (var item in gy_YAOPINCDJG2List)
            {
                var ykfCACHE = GetKuCun(item.JIAGEID, yingYongID);
                result.Add(ykfCACHE);

            }

            return result;
        }

        /// <summary>
        /// 获取药品字典缓存
        /// </summary>
        /// <param name="jiaGeID"></param>
        /// <param name="yingYongID"></param>
        /// <returns></returns>
        public GY_YAOPINZDCACHE GetYaoPinZd(string jiaGeID, string yingYongID)
        {
            GY_YAOPINZDCACHE ykfCACHE = new GY_YAOPINZDCACHE();
            var yaoPinCDJG2 = this.QuerySet<GY_YAOPINCDJG2>().FirstOrDefault(o => o.JIAGEID == jiaGeID);
            var baoZhuangLiang = yaoPinCDJG2.BAOZHUANGLIANG.GetDecimal();
            var yaoPinID = yaoPinCDJG2.YAOPINID;
            var guiGeID = yaoPinCDJG2.GUIGEID;
            var yaoPinMCGG2 = this.QuerySet<GY_YAOPINMCGG2>().FirstOrDefault(o => o.GUIGEID == guiGeID);
            var maxJiaGeID = this.QuerySet<GY_YAOPINCDJG2>().FirstOrDefault(o => o.GUIGEID == yaoPinCDJG2.DAGUIGID && o.CHANDI == yaoPinCDJG2.CHANDI)?.JIAGEID;
            var minJiaGeID = this.QuerySet<GY_YAOPINCDJG2>().FirstOrDefault(o => o.GUIGEID == yaoPinCDJG2.XIAOGUIGID && o.CHANDI == yaoPinCDJG2.CHANDI)?.JIAGEID;

            string yaoPinBM = string.Empty;
            var canShuDomain = this.QuerySet<GY_CANSHU>().FirstOrDefault(m => m.CANSHUID == "药品字典_别名跟主名还是跟规格");
            //药品别名
            switch (canShuDomain.CANSHUZHI)
            {
                case "1"://别名跟主名
                    yaoPinBM = this.QuerySet<GY_YAOPINZBM>().FirstOrDefault(o => o.ZHUMINGID == null && o.ZUOFEIBZ == 0 && o.YAOPINID == yaoPinID)?.YAOPINMC;
                    break;
                case "2"://别名跟规格
                    yaoPinBM = this.QuerySet<GY_YAOPINZBM>().FirstOrDefault(o => o.ZHUMINGID != null && o.ZUOFEIBZ == 0 && o.GUIGEID == guiGeID)?.YAOPINMC;
                    break;
                case "3"://别名跟产地
                    yaoPinBM = this.QuerySet<GY_YAOPINZBM>().FirstOrDefault(o => o.ZHUMINGID != null && o.ZUOFEIBZ == 0 && o.JIAGEID == jiaGeID)?.YAOPINMC;
                    break;
            }

            //药品摆放位置
            var yaoPinBFWZ = this.QuerySet<GY_YAOPINBFWZ>().FirstOrDefault(o => o.JIAGEID == maxJiaGeID && o.XIANSHIBZ == 0 && o.WEIZHISM != null && o.GUANLILB == "3")?.WEIZHISM ??
                             (this.QuerySet<GY_YAOPINBFWZ>().FirstOrDefault(o => o.GUIGEID == maxJiaGeID && o.XIANSHIBZ == 0 && o.WEIZHISM != null && o.GUANLILB == "2")?.WEIZHISM ??
                              this.QuerySet<GY_YAOPINBFWZ>().FirstOrDefault(o => o.YAOPINID == yaoPinID && o.XIANSHIBZ == 0 && o.WEIZHISM != null && o.GUANLILB == "2")?.WEIZHISM);

            string shiYongPL = string.Empty;
            decimal? yiShouWFSLYZ = 0;
            decimal? yuKouKCSL = 0;
            decimal? jinJia = 0;
            decimal? lingShouJia = 0;

            //数量转换
            if (jiaGeID != minJiaGeID)
            {

                yiShouWFSLYZ = yiShouWFSLYZ * baoZhuangLiang;
                yuKouKCSL = yuKouKCSL * baoZhuangLiang;

                jinJia = this.QuerySet<GY_YAOPINJGDZ>().FirstOrDefault(o => o.JIAGEID2 == minJiaGeID && o.JIAGE2 == jinJia && o.JIAGELX == 1 && o.JIAGEID1 == jiaGeID)?.JIAGE1.GetDecimal();
                if (jinJia == 0)
                {
                    jinJia = jinJia * baoZhuangLiang;
                }
                lingShouJia = this.QuerySet<GY_YAOPINJGDZ>().FirstOrDefault(o => o.JIAGEID2 == minJiaGeID && o.JIAGE2 == lingShouJia && o.JIAGELX == 3 && o.JIAGEID1 == jiaGeID)?.JIAGE1.GetDecimal();
                if (lingShouJia == 0)
                {
                    lingShouJia = lingShouJia * baoZhuangLiang;
                }
            }
            //药品存量控制，库存上限，库存下级，采购固定数量，请领固定数量 
            decimal kuCunSX = 0;
            decimal kuCunXX = 0;
            decimal qingLingGDSL = 0;
            decimal caiGouGDSL = 0;
            var yaoPinCLKZ = this.QuerySet<GY_YAOPINCLKZ>().FirstOrDefault(o => o.YINGYONGID == yingYongID && o.JIAGEID == maxJiaGeID) ??
                             (this.QuerySet<GY_YAOPINCLKZ>().FirstOrDefault(o => o.YINGYONGID == yingYongID && o.GUIGEID == guiGeID) ??
                              this.QuerySet<GY_YAOPINCLKZ>().FirstOrDefault(o => o.YINGYONGID == yingYongID && o.YAOPINID == yaoPinID));
            if (yaoPinCLKZ != null)
            {
                kuCunSX = yaoPinCLKZ.KUCUNSX.GetDecimal();
                kuCunXX = yaoPinCLKZ.KUCUNXX.GetDecimal();
                qingLingGDSL = yaoPinCLKZ.QINGLINGGDSL.GetDecimal();
                caiGouGDSL = yaoPinCLKZ.GetDecimal();
            }
            if (jiaGeID != minJiaGeID)
            {
                kuCunSX = kuCunSX * baoZhuangLiang;
                kuCunXX = kuCunXX * baoZhuangLiang;
                qingLingGDSL = qingLingGDSL * baoZhuangLiang;
                caiGouGDSL = caiGouGDSL * baoZhuangLiang;
            }
            //药品中药配方颗粒
            decimal pfZhuanHuanSL = 0;
            var zhongYaoPFKLZD = this.QuerySet<GY_ZHONGYAOPFKLZD>().FirstOrDefault(o => o.ZHONGYAOPFKLID == yaoPinCDJG2.ZHONGYAOPFKLID);
            if (zhongYaoPFKLZD != null)
            {
                pfZhuanHuanSL = zhongYaoPFKLZD.ZHUANHUANSL.GetDecimal();
            }
            //药品存量控制字段
            ykfCACHE.CACHEID = yingYongID + ":" + jiaGeID;
            ykfCACHE.YINGYONGID = yingYongID;
            ykfCACHE.MINJIAGEID = minJiaGeID;
            ykfCACHE.JIAGEID = jiaGeID;
            ykfCACHE.GUIGEID = guiGeID;
            ykfCACHE.DULIFL = yaoPinCDJG2?.DULIFL;
            ykfCACHE.JIAZHIFL = yaoPinCDJG2?.JIAZHIFL;
            ykfCACHE.ZHANGBULB = yaoPinCDJG2?.ZHANGBULB;
            ykfCACHE.YONGYAOJYSM = yaoPinMCGG2?.YONGYAOJYSM;
            ykfCACHE.JINGSHIYS = yaoPinMCGG2?.JINGSHIYS;
            ykfCACHE.TINGYONGBZ = yaoPinCDJG2?.TINGYONGBZ;
            ykfCACHE.ZUOFEIBZ = yaoPinCDJG2?.ZUOFEIBZ;
            ykfCACHE.YAOPINBM = yaoPinBM;
            ykfCACHE.BAIFANGWZ = yaoPinBFWZ;
            ykfCACHE.YISHOUWFSLYZ = yiShouWFSLYZ;
            ykfCACHE.YUKOUKCSL = yuKouKCSL;
            ykfCACHE.JINJIA = jinJia;
            ykfCACHE.LINGSHOUJIA = lingShouJia;
            ykfCACHE.SHIYONGPL = shiYongPL;
            ykfCACHE.KUCUNSX = kuCunSX;
            ykfCACHE.KUCUNXX = kuCunXX;
            ykfCACHE.QINGLINGGDSL = qingLingGDSL;
            ykfCACHE.CAIGOUGDSL = caiGouGDSL;
            ykfCACHE.ZHONGYAOPFKLZHSL = pfZhuanHuanSL;
            return ykfCACHE;
        }

        /// <summary>
        /// 获取药品字典缓存列表，根据规格ID
        /// </summary>
        /// <param name="guiGeID"></param>
        /// <param name="yingYongID"></param>
        /// <returns></returns>
        public List<GY_YAOPINZDCACHE> GetYaoPinZdList(string guiGeID, string yingYongID)
        {
            List<GY_YAOPINCDJG2> gy_YAOPINCDJG2List = this.QuerySet<GY_YAOPINCDJG2>().Where(o => o.GUIGEID == guiGeID && o.ZUOFEIBZ == 0).ToList();

            return gy_YAOPINCDJG2List.Select(item => GetYaoPinZd(item.JIAGEID, yingYongID)).ToList();
        }


        /// <summary>
        /// 刷新所有库存缓存
        /// </summary>
        public void KuCunCacheRefreshAll()
        {
            List<GY_YINGYONG> yingYongList = this.QuerySet<GY_YINGYONG>().Where(o => o.XITONGID == "05" || o.XITONGID == "06").ToList();
            List<string> jiaGeIDList = this.QuerySet<GY_YAOPINCDJG2>().Where(m => m.ZUOFEIBZ == 0).Select(m => m.JIAGEID).ToList();
            foreach (var jiaGeID in jiaGeIDList)
            {
                foreach (var yingYong in yingYongList)
                {
                    RedisClient.Clinent.Set("GY_KUCUN:JIAGEID:" + yingYong.YINGYONGID + ":" + jiaGeID, GetKuCun(jiaGeID, yingYong.YINGYONGID), 2880);
                }
            }

            List<string> guiGeIDList = this.QuerySet<GY_YAOPINMCGG2>().Where(m => m.ZUOFEIBZ == 0).Select(m => m.GUIGEID).ToList();
            foreach (var guiGeID in guiGeIDList)
            {
                foreach (var yingYong in yingYongList)
                {
                    RedisClient.Clinent.Set("GY_KUCUN:GUIGEID:" + yingYong.YINGYONGID + ":" + guiGeID, GetKuCunList(guiGeID, yingYong.YINGYONGID), 2880);
                }
            }
        }

        /// <summary>
        /// 刷新库存缓存
        /// </summary>
        /// <param name="jiaGeID"></param>
        public void KuCunCacheRefresh(string jiaGeID)
        {
            List<GY_YINGYONG> yingYongList = this.QuerySet<GY_YINGYONG>().Where(o => o.XITONGID == "05" || o.XITONGID == "06").ToList();
            foreach (var yingYong in yingYongList)
            {
                RedisClient.Clinent.Set("GY_KUCUN:JIAGEID:" + yingYong.YINGYONGID + ":" + jiaGeID, GetKuCun(jiaGeID, yingYong.YINGYONGID), 2880);
            }

            var guiGeID = this.QuerySet<GY_YAOPINCDJG2>().FirstOrDefault(m => m.JIAGEID == jiaGeID)?.GUIGEID;
            foreach (var yingYong in yingYongList)
            {
                RedisClient.Clinent.Set("GY_KUCUN:GUIGEID:" + yingYong.YINGYONGID + ":" + guiGeID, GetKuCunList(guiGeID, yingYong.YINGYONGID), 2880);
            }
        }

        /// <summary>
        /// 刷新药品字典缓存
        /// </summary>
        public void YaoPinZdCacheRefreshAll()
        {
            List<GY_YINGYONG> yingYongList = this.QuerySet<GY_YINGYONG>().Where(o => o.XITONGID == "05" || o.XITONGID == "06").ToList();
            List<string> jiaGeIDList = this.QuerySet<GY_YAOPINCDJG2>().Where(m => m.ZUOFEIBZ == 0).Select(m => m.JIAGEID).ToList();
            foreach (var jiaGeID in jiaGeIDList)
            {
                foreach (var yingYong in yingYongList)
                {
                    RedisClient.Clinent.Set("GY_YAOPINZD:JIAGEID:" + yingYong.YINGYONGID + ":" + jiaGeID, GetYaoPinZd(jiaGeID, yingYong.YINGYONGID), 2880);
                }
            }

            List<string> guiGeIDList = this.QuerySet<GY_YAOPINMCGG2>().Where(m => m.ZUOFEIBZ == 0).Select(m => m.GUIGEID).ToList();
            foreach (var guiGeID in guiGeIDList)
            {
                foreach (var yingYong in yingYongList)
                {
                    RedisClient.Clinent.Set("GY_YAOPINZD:GUIGEID:" + yingYong.YINGYONGID + ":" + guiGeID, GetYaoPinZdList(guiGeID, yingYong.YINGYONGID), 2880);
                }
            }
        }

        /// <summary>
        /// 刷新药品字典缓存
        /// </summary>
        /// <param name="jiaGeID"></param>
        public void YaoPinZdCacheRefresh(string jiaGeID)
        {
            List<GY_YINGYONG> yingYongList = this.QuerySet<GY_YINGYONG>().Where(o => o.XITONGID == "05" || o.XITONGID == "06").ToList();

            foreach (var yingYong in yingYongList)
            {
                RedisClient.Clinent.Set("GY_YAOPINZD:JIAGEID:" + yingYong.YINGYONGID + ":" + jiaGeID, GetYaoPinZd(jiaGeID, yingYong.YINGYONGID), 2880);
            }

            var guiGeID = this.QuerySet<GY_YAOPINCDJG2>().FirstOrDefault(m => m.JIAGEID == jiaGeID)?.GUIGEID;

            foreach (var yingYong in yingYongList)
            {
                RedisClient.Clinent.Set("GY_YAOPINZD:GUIGEID:" + yingYong.YINGYONGID + ":" + guiGeID, GetYaoPinZdList(guiGeID, yingYong.YINGYONGID), 2880);
            }
        }
    }
}
