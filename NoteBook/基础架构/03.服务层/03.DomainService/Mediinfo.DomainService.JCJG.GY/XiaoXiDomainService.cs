using Mediinfo.Domain.JCJG.GY;
using Mediinfo.Domain.JCJG.XT;
using Mediinfo.Domain.JCJG.ZJ;
using Mediinfo.Domain.JCJG.ZY;
using Mediinfo.Enterprise;
using Mediinfo.HIS.Core;
using Mediinfo.Infrastructure.JCJG;
using Mediinfo.Infrastructure.Core.DomainService;
using Mediinfo.Infrastructure.Core.UnitOfWork;
using Mediinfo.Utility.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise.Log;

namespace Mediinfo.DomainService.JCJG.GY
{
    /// <summary>
    /// 系统消息
    /// </summary>
    public class XiaoXiDomainService : DomainServiceBase
    {
        IXT_XIAOXIBMRepository xT_XIAOXIBMRepository = null;
        IXT_SHOUJIANREN_NEWRepository xT_SHOUJIANREN_NEWRepository = null;
        IXT_XIAOXIDY_NEWRepository xT_XIAOXIDY_NEWRepository = null;
        IGY_ZHIGONGXXRepository gY_ZHIGONGXXRepository = null;
        IGY_ZHIGONGKSRepository gY_ZHIGONGKSRepository = null;
        IXT_ZAIXIANZTRepository xT_ZAIXIANZTRepository = null;
        IZJ_JIUZHENXXRepository zJ_JIUZHENXXRepository = null;
        IZY_BINGRENXXRepository zY_BINGRENXXRepository = null;
        IXT_XIAOXISJX_NEWRepository xT_XIAOXISJX_NEWRepository = null;
        IXT_XIAOXI_NEWRepository xT_XIAOXI_NEWRepository = null;
        IXT_XIAOXIFJRepository xT_XIAOXIFJRepository = null;
        IXT_XIAOXIFJNRRepository xT_XIAOXIFJNRRepository = null;
        public XiaoXiDomainService(IUnitOfWork unitOfWork, ServiceContext serviceContext) : base(unitOfWork, serviceContext)
        {
            xT_XIAOXIBMRepository = this.GetRepository<IXT_XIAOXIBMRepository>();
            xT_SHOUJIANREN_NEWRepository = this.GetRepository<IXT_SHOUJIANREN_NEWRepository>();
            xT_XIAOXIDY_NEWRepository = this.GetRepository<IXT_XIAOXIDY_NEWRepository>();
            gY_ZHIGONGXXRepository = this.GetRepository<IGY_ZHIGONGXXRepository>();
            gY_ZHIGONGKSRepository = this.GetRepository<IGY_ZHIGONGKSRepository>();
            xT_ZAIXIANZTRepository = this.GetRepository<IXT_ZAIXIANZTRepository>();
            zJ_JIUZHENXXRepository = this.GetRepository<IZJ_JIUZHENXXRepository>();
            zY_BINGRENXXRepository = this.GetRepository<IZY_BINGRENXXRepository>();

            xT_XIAOXISJX_NEWRepository = GetRepository<IXT_XIAOXISJX_NEWRepository>();
            xT_XIAOXI_NEWRepository = this.GetRepository<IXT_XIAOXI_NEWRepository>();
            xT_XIAOXIFJRepository = this.GetRepository<IXT_XIAOXIFJRepository>();
            xT_XIAOXIFJNRRepository = this.GetRepository<IXT_XIAOXIFJNRRepository>();
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="xiaoXiBM">消息编码</param>
        /// <param name="bingRenID">病人标识(门诊时为jiuzhenid,住院时为bingrenzyid)</param>
        /// <param name="menZhenZyBz">门诊住院标识（(0,门诊 1住院)）</param>
        /// <param name="dingYueList">订阅列表(通过XT_XIAOXIDY_NEWFactory创建)</param>
        /// <param name="xiaoXiBT">消息标题</param>
        /// <param name="xiaoXiZY">消息摘要</param>
        /// <param name="xiaoXiNR">消息内容</param>
        /// <param name="fuJianMc">附件名称</param>
        /// <param name="fuJianNR">附件内容</param>
        /// <param name="yiCiXBZ">一次性标志</param>
        /// <param name="xiaoXiTXLX">消息提示类型</param>
        /// <returns></returns>
        public bool FaSongXX(string xiaoXiBM, string bingRenID, int menZhenZyBz,
            List<XT_XIAOXIDY_NEW> dingYueList,
            string xiaoXiBT, string xiaoXiZY, object xiaoXiNR, Dictionary<string, string> XiaoXiFJ, string xiaoXiLY = "", int yiCiXBZ = 0, string xiaoXiTXLX = "1")
        {
            Check.NotEmpty(xiaoXiBT, "xiaoXiBT不能为空");
            Check.NotEmpty(xiaoXiZY, "xiaoXiZY不能为空");

            // 从消息编码中
            XT_XIAOXIBM xT_XIAOXIBM = xT_XIAOXIBMRepository.GetByKey(xiaoXiBM);
            if (xT_XIAOXIBM == null)
            {
                return false;
            }

            // 病人主治医生
            string bingRenZzYs = string.Empty;
            // 病人医疗组
            string bingRenYlz = string.Empty;
            // 病人科室
            string bingRenKs = string.Empty;
            // 病人病区
            string bingRenBq = string.Empty;
            // 病人院区
            string bingRenYq = string.Empty;

            // 如果是门诊病人
            if (!string.IsNullOrWhiteSpace(bingRenID) && menZhenZyBz == 0)
            {
                ZJ_JIUZHENXX jiuZhenXx = zJ_JIUZHENXXRepository.GetByKey(bingRenID);
                bingRenZzYs = jiuZhenXx.JIUZHENYS;
                bingRenKs = jiuZhenXx.JIUZHENKS;
                bingRenYq = jiuZhenXx.YUANQUID;
            }

            // 如果是住院病人
            if (!string.IsNullOrWhiteSpace(bingRenID) && menZhenZyBz == 1)
            {
                ZY_BINGRENXX zyBingRenXx = zY_BINGRENXXRepository.GetByKey(bingRenID);
                if (zyBingRenXx != null)
                {
                    bingRenZzYs = zyBingRenXx.ZHUZHIYS;
                    bingRenYlz = zyBingRenXx.YILIAOZID;
                    bingRenKs = zyBingRenXx.DANGQIANKS;
                    bingRenBq = zyBingRenXx.DANGQIANBQ;
                    bingRenYq = zyBingRenXx.YUANQUID;
                }
            }

            // 获取在线用户
            List<XT_ZAIXIANZT> zaiXianList = xT_ZAIXIANZTRepository.QueryZaiXianYh();

            // 获取订阅列表并合并用户自定义订阅列表
            if (dingYueList != null && dingYueList.Count > 0)
            {
                dingYueList = dingYueList.Union(xT_XIAOXIDY_NEWRepository.QueryDingYueList(xiaoXiBM)).ToList();
            }
            else
            {
                dingYueList = xT_XIAOXIDY_NEWRepository.QueryDingYueList(xiaoXiBM);
            }

            // 初始化待发送列表
            List<XT_ZAIXIANZT> daiFaSongList = new List<XT_ZAIXIANZT>();

            // 订阅列表和在线用户合并产生待发送列表
            foreach (XT_XIAOXIDY_NEW item in dingYueList)
            {

                // 初始化收件人列表
                List<XT_ZAIXIANZT> shouJianRenList = new List<XT_ZAIXIANZT>();
                switch (item.GetDingYueRLx())
                {
                    case DingYueRLx.用户:
                        shouJianRenList = zaiXianList.Where(m => m.ZHIGONGID == item.GetShouJianZuID()).ToList();
                        break;
                    case DingYueRLx.系统:
                        shouJianRenList = zaiXianList.Where(m => m.XITONGID == item.GetShouJianZuID()).ToList();
                        break;
                    case DingYueRLx.应用:
                        shouJianRenList = zaiXianList.Where(m => m.YINGYONGID == item.GetShouJianZuID()).ToList();
                        break;
                    case DingYueRLx.病区:
                        shouJianRenList = zaiXianList.Where(m => m.BINGQUID == item.GetShouJianZuID()).ToList();
                        break;
                    case DingYueRLx.科室:
                        shouJianRenList = zaiXianList.Where(m => m.KESHIID == item.GetShouJianZuID()).ToList();
                        break;
                    case DingYueRLx.医疗组:
                        shouJianRenList = zaiXianList.Where(m => m.YILIAOZID == item.GetShouJianZuID()).ToList();
                        break;
                    case DingYueRLx.院区:
                        shouJianRenList = zaiXianList.Where(m => m.YUANQUID == item.GetShouJianZuID()).ToList();
                        break;
                    case DingYueRLx.全院:
                        shouJianRenList = zaiXianList.ToList();
                        break;
                    case DingYueRLx.病人主治医生:
                        shouJianRenList = zaiXianList.Where(m => m.ZHIGONGID == bingRenZzYs).ToList();
                        break;
                    case DingYueRLx.病人当前医疗组:
                        shouJianRenList = zaiXianList.Where(m => m.YILIAOZID == bingRenYlz).ToList();
                        break;
                    case DingYueRLx.病人当前病区:
                        shouJianRenList = zaiXianList.Where(m => m.BINGQUID == bingRenBq).ToList();
                        break;
                    case DingYueRLx.病人当前科室:
                        shouJianRenList = zaiXianList.Where(m => m.KESHIID == bingRenKs).ToList();
                        break;
                    case DingYueRLx.病人当前院区:
                        shouJianRenList = zaiXianList.Where(m => m.YUANQUID == bingRenYq).ToList();
                        break;
                    default:
                        break;
                }

                // 收件人列表进行角色权限、应用过滤
                foreach (var shouJianRen in shouJianRenList)
                {
                    if (item.AllowJueSeQx(shouJianRen.DangQianJsQxIDList()) &&
                    item.AllowYingYongQx(shouJianRen.YINGYONGID))
                    {
                        daiFaSongList.Add(shouJianRen);
                    }
                }

            }

            bool fuJianBz = XiaoXiFJ != null && XiaoXiFJ.Count > 0;

            // 消息入库
            XT_XIAOXI_NEW xiaoXi = XT_XIAOXI_NEWFactory.Create(xT_XIAOXI_NEWRepository, ServiceContext,
                xiaoXiBM, xT_XIAOXIBM.XIAOXIMC, xT_XIAOXIBM.XIAOXIJC, bingRenID, menZhenZyBz, dingYueList, xiaoXiBT, xiaoXiZY, xiaoXiNR,
                xiaoXiTXLX, xT_XIAOXIBM.YOUXIAOSJ, yiCiXBZ, xT_XIAOXIBM.BAOMIXXBZ,
                xT_XIAOXIBM.YOUXIANJB, xT_XIAOXIBM.HUIZHIBZ, fuJianBz ? 1 : 0, "2", xiaoXiLY);
            xT_XIAOXI_NEWRepository.RegisterAdd(xiaoXi);

            if (fuJianBz)
            {
                int i = 1;
                foreach (var item in XiaoXiFJ)
                {
                    XT_XIAOXIFJ fuJian = XT_XIAOXIFJFactory.Create(xT_XIAOXIFJRepository, (long)xiaoXi.XIAOXIID, item.Key);
                    xT_XIAOXIFJRepository.RegisterAdd(fuJian);

                    XT_XIAOXIFJNR fuJianNr = XT_XIAOXIFJNRFactory.Create(xT_XIAOXIFJNRRepository, fuJian.FUJIANID, i++, item.Value);
                    xT_XIAOXIFJNRRepository.RegisterAdd(fuJianNr);
                }
            }

            UnitOfWork.BulkSaveChanges();

            bool sendResult = HISMessage.SendMessage(daiFaSongList.Distinct().Select(m => m.ZHUANGTAIID),
                xiaoXi.XIAOXIID.ToString(), xiaoXiBM, xT_XIAOXIBM.XIAOXIMC, xT_XIAOXIBM.XIAOXIJC, bingRenID, menZhenZyBz, xiaoXiBT, xiaoXiZY, xiaoXiNR, (DateTime)xiaoXi.FASONGSJ,
                (string.IsNullOrEmpty(xiaoXi.FAJIANRXM) ? xiaoXi.FAJIANRID : xiaoXi.FAJIANRXM),
                xiaoXiTXLX, xT_XIAOXIBM.YOUXIAOSJ, yiCiXBZ, xT_XIAOXIBM.BAOMIXXBZ,
                xT_XIAOXIBM.YOUXIANJB, xT_XIAOXIBM.HUIZHIBZ, fuJianBz ? 1 : 0, xiaoXiLY);

            return sendResult;


        }

        /// <summary>
        /// 获取已读消息
        /// </summary>
        /// <param name="zaiXianZTID"></param>
        /// <param name="faSongSJ"></param>
        /// <returns></returns>
        public List<HISMessageBody> GetYiDuXX(string zaiXianZTID, DateTime faSongSJ)
        {
            Check.NotEmpty(zaiXianZTID, "zaiXianZTID不能为空");
            List<HISMessageBody> result = new List<HISMessageBody>();
            XT_ZAIXIANZT zaiXian = xT_ZAIXIANZTRepository.GetByKey(zaiXianZTID);
            if (zaiXian == null)
                return result;

            List<XT_XIAOXI_NEW> xiaoXiList = xT_XIAOXI_NEWRepository.GetWeiGuoQi(faSongSJ);
            List<long?> strXXID = xiaoXiList.Select(p => p.XIAOXIID).ToList();
            //List<string> bingRenID = xiaoXiList.Select(p => p.BINGRENID).ToList();
            //List<string> xiaoXiBM = xiaoXiList.Select(p => p.XIAOXIBM).ToList();
            //过滤已读消息
            //var yiDuXiaoXi = xT_XIAOXISJX_NEWRepository.GetAllXiaoXiSJ(strXXID, "U-" + zaiXian.ZHIGONGID);
            var yiDuXiaoXi = xT_XIAOXISJX_NEWRepository.GetAllXiaoXiSJ(strXXID);

            #region 临时注释

            ////获取病人就诊信息
            //var jiuZhenXxList = zJ_JIUZHENXXRepository.GetAllZJJZXX(bingRenID);
            ////获取住院病人信息
            //var zyBingRenXxList = zY_BINGRENXXRepository.GetAllZYBRXX(bingRenID);
            ////获取权限
            //var dingYue = xT_XIAOXIDY_NEWRepository.QueryDingYueLists(xiaoXiBM);
            //foreach (var xiaoXi in xiaoXiList)
            //{
            //    ////过滤已读消息
            //    //var shouJianXiang = xT_XIAOXISJX_NEWRepository.GetByKey(xiaoXi.XIAOXIID, "U-" + zaiXian.ZHIGONGID);
            //    var shouJianXiang = yiDuXiaoXi.Where(p => p.XIAOXIID == xiaoXi.XIAOXIID).FirstOrDefault();

            //    if (shouJianXiang != null && shouJianXiang.YiDu())
            //    {
            //        // 病人主治医生
            //        string bingRenZzYs = string.Empty;
            //        // 病人医疗组
            //        string bingRenYlz = string.Empty;
            //        // 病人科室
            //        string bingRenKs = string.Empty;
            //        // 病人病区
            //        string bingRenBq = string.Empty;
            //        // 病人院区
            //        string bingRenYq = string.Empty;

            //        // 如果是门诊病人
            //        if (!string.IsNullOrWhiteSpace(xiaoXi.BINGRENID) && xiaoXi.MENZHENZYBZ == 0)
            //        {
            //            var jiuZhenXx = jiuZhenXxList.Where(p => p.JIUZHENID == xiaoXi.BINGRENID).FirstOrDefault();
            //            if (jiuZhenXx != null)
            //            {
            //                bingRenZzYs = jiuZhenXx.JIUZHENYS;
            //                bingRenKs = jiuZhenXx.JIUZHENKS;
            //                bingRenYq = jiuZhenXx.YUANQUID;
            //            }
            //        }

            //        // 如果是住院病人
            //        if (!string.IsNullOrWhiteSpace(xiaoXi.BINGRENID) && (xiaoXi.MENZHENZYBZ == 1 || xiaoXi.MENZHENZYBZ == 2))
            //        {
            //            var zyBingRenXx = zyBingRenXxList.Where(p => p.BINGRENZYID == xiaoXi.BINGRENID).FirstOrDefault();
            //            if (zyBingRenXx != null)
            //            {
            //                bingRenZzYs = zyBingRenXx.ZHUZHIYS;
            //                bingRenYlz = zyBingRenXx.YILIAOZID;
            //                bingRenKs = zyBingRenXx.DANGQIANKS;
            //                bingRenBq = zyBingRenXx.DANGQIANBQ;
            //                bingRenYq = zyBingRenXx.YUANQUID;
            //            }
            //        }

            //        var dingYueList = JsonUtil.DeserializeToObject<List<XT_XIAOXIDY_NEW>>(xiaoXi.SHOUJIANREN);
            //        // 获取订阅列表并合并用户自定义订阅列表
            //        if (dingYueList != null && dingYueList.Count > 0)
            //        {
            //            //dingYueList = dingYueList.Union(xT_XIAOXIDY_NEWRepository.QueryDingYueList(xiaoXi.XIAOXIBM)).ToList();
            //            dingYueList = dingYueList.Union(dingYue.Where(p => p.XIAOXIBM == xiaoXi.XIAOXIBM)).ToList();
            //        }
            //        else
            //        {
            //            dingYueList = dingYue.Where(p => p.XIAOXIBM == xiaoXi.XIAOXIBM).ToList();
            //            //dingYueList = xT_XIAOXIDY_NEWRepository.QueryDingYueList(xiaoXi.XIAOXIBM);
            //        }

            //        bool FaSong = false;

            //        // 订阅列表和在线用户合并产生待发送列表
            //        foreach (XT_XIAOXIDY_NEW item in dingYueList)
            //        {
            //            switch (item.GetDingYueRLx())
            //            {
            //                case DingYueRLx.用户:
            //                    FaSong = zaiXian.ZHIGONGID == item.GetShouJianZuID();
            //                    break;
            //                case DingYueRLx.系统:
            //                    FaSong = zaiXian.XITONGID == item.GetShouJianZuID();
            //                    break;
            //                case DingYueRLx.应用:
            //                    FaSong = zaiXian.YINGYONGID == item.GetShouJianZuID();
            //                    break;
            //                case DingYueRLx.病区:
            //                    FaSong = zaiXian.BINGQUID == item.GetShouJianZuID();
            //                    break;
            //                case DingYueRLx.科室:
            //                    FaSong = zaiXian.KESHIID == item.GetShouJianZuID();
            //                    break;
            //                case DingYueRLx.医疗组:
            //                    FaSong = zaiXian.YILIAOZID == item.GetShouJianZuID();
            //                    break;
            //                case DingYueRLx.院区:
            //                    FaSong = zaiXian.YUANQUID == item.GetShouJianZuID();
            //                    break;
            //                case DingYueRLx.全院:
            //                    FaSong = true;
            //                    break;
            //                case DingYueRLx.病人主治医生:
            //                    FaSong = zaiXian.ZHIGONGID == bingRenZzYs;
            //                    break;
            //                case DingYueRLx.病人当前医疗组:
            //                    FaSong = zaiXian.YILIAOZID == bingRenYlz;
            //                    break;
            //                case DingYueRLx.病人当前病区:
            //                    FaSong = zaiXian.BINGQUID == bingRenBq;
            //                    break;
            //                case DingYueRLx.病人当前科室:
            //                    FaSong = zaiXian.KESHIID == bingRenKs;
            //                    break;
            //                case DingYueRLx.病人当前院区:
            //                    FaSong = zaiXian.YUANQUID == bingRenYq;
            //                    break;
            //                default:
            //                    break;
            //            }

            //            if (!item.AllowJueSeQx(zaiXian.DangQianJsQxIDList()) ||
            //            !item.AllowYingYongQx(zaiXian.YINGYONGID))
            //            {
            //                FaSong = false;
            //            }

            //            if (FaSong)
            //            {
            //                break;
            //            }
            //        }
            //        if (FaSong)
            //        {
            //            HISMessageBody hISMessageBody = new HISMessageBody();
            //            hISMessageBody.Receivers = new string[] { zaiXian.ZHIGONGID };
            //            hISMessageBody.BaoMiXxBz = xiaoXi.BAOMIXXBZ;
            //            hISMessageBody.BingRenID = xiaoXi.BINGRENID;
            //            hISMessageBody.HuiZhiBz = xiaoXi.HUIZHIBZ;
            //            hISMessageBody.MenZhenZyBz = xiaoXi.MENZHENZYBZ == null ? 1 : (int)xiaoXi.MENZHENZYBZ;
            //            hISMessageBody.TiXingLx = xiaoXi.XIAOXITXLX;
            //            hISMessageBody.XiaoXiBM = xiaoXi.XIAOXIBM;
            //            hISMessageBody.XiaoXiBT = xiaoXi.XIAOXIZT;
            //            hISMessageBody.XiaoXiID = xiaoXi.XIAOXIID.ToString();
            //            hISMessageBody.XiaoXiNR = xiaoXi.XIAOXINR;
            //            hISMessageBody.XiaoXiZY = xiaoXi.XIAOXIZY;
            //            hISMessageBody.YiCiXBZ = xiaoXi.YICIXBZ;
            //            hISMessageBody.YouXianJi = xiaoXi.YOUXIANJB;
            //            hISMessageBody.YouXiaoSj = xiaoXi.YOUXIAOQI;
            //            hISMessageBody.FuJianBz = (int)xiaoXi.FUJIANBZ;
            //            hISMessageBody.XiaoXiMc = xiaoXi.XIAOXIMC;
            //            hISMessageBody.XiaoXiJc = xiaoXi.XIAOXIJC;
            //            hISMessageBody.FaSongSj = (DateTime)xiaoXi.FASONGSJ;
            //            hISMessageBody.XiaoXiLY = xiaoXi.XIAOXILY;
            //            hISMessageBody.ShouJianRen = xiaoXi.SHOUJIANRXM;
            //            hISMessageBody.XiaoXiLY = xiaoXi.XIAOXILY;
            //            hISMessageBody.FaSongRen = (string.IsNullOrEmpty(xiaoXi.FAJIANRXM) ? xiaoXi.FAJIANRID : xiaoXi.FAJIANRXM);
            //            if (shouJianXiang != null)
            //            {
            //                hISMessageBody.JieShouBZ = shouJianXiang.JIESHOUBZ;
            //            }
            //            hISMessageBody.XiaoXiLY = xiaoXi.XIAOXILY;
            //            result.Add(hISMessageBody);
            //        }
            //    }
            //}

            #endregion

            foreach (var xiaoXi in xiaoXiList)
            {
                XT_XIAOXISJX_NEW shouJianXiang = new XT_XIAOXISJX_NEW();
                if (!string.IsNullOrEmpty(ServiceContext.BINGQUID))
                    shouJianXiang = yiDuXiaoXi.Where(p => (p.SHOUJIANRID.Contains(ServiceContext.KESHIID) || p.SHOUJIANRID.Contains(ServiceContext.USERID) || p.SHOUJIANRID.Contains(ServiceContext.BINGQUID)) && p.XIAOXIID == xiaoXi.XIAOXIID).FirstOrDefault();
                else
                    shouJianXiang = yiDuXiaoXi.Where(p => (p.SHOUJIANRID.Contains(ServiceContext.KESHIID) || p.SHOUJIANRID.Contains(ServiceContext.USERID)) && p.XIAOXIID == xiaoXi.XIAOXIID).FirstOrDefault();
                if (shouJianXiang != null && shouJianXiang.YiDu())
                {
                    HISMessageBody hISMessageBody = new HISMessageBody();
                    hISMessageBody.Receivers = new string[] { zaiXian.ZHIGONGID };
                    hISMessageBody.BaoMiXxBz = xiaoXi.BAOMIXXBZ;
                    hISMessageBody.BingRenID = xiaoXi.BINGRENID;
                    hISMessageBody.HuiZhiBz = xiaoXi.HUIZHIBZ;
                    hISMessageBody.MenZhenZyBz = xiaoXi.MENZHENZYBZ == null ? 1 : (int)xiaoXi.MENZHENZYBZ;
                    hISMessageBody.TiXingLx = xiaoXi.XIAOXITXLX;
                    hISMessageBody.XiaoXiBM = xiaoXi.XIAOXIBM;
                    hISMessageBody.XiaoXiBT = xiaoXi.XIAOXIZT;
                    hISMessageBody.XiaoXiID = xiaoXi.XIAOXIID.ToString();
                    hISMessageBody.XiaoXiNR = xiaoXi.XIAOXINR;
                    hISMessageBody.XiaoXiZY = xiaoXi.XIAOXIZY;
                    hISMessageBody.YiCiXBZ = xiaoXi.YICIXBZ;
                    hISMessageBody.YouXianJi = xiaoXi.YOUXIANJB;
                    hISMessageBody.YouXiaoSj = xiaoXi.YOUXIAOQI;
                    hISMessageBody.FuJianBz = (int)xiaoXi.FUJIANBZ;
                    hISMessageBody.XiaoXiMc = xiaoXi.XIAOXIMC;
                    hISMessageBody.XiaoXiJc = xiaoXi.XIAOXIJC;
                    hISMessageBody.FaSongSj = (DateTime)xiaoXi.FASONGSJ;
                    hISMessageBody.XiaoXiLY = xiaoXi.XIAOXILY;
                    hISMessageBody.ShouJianRen = xiaoXi.SHOUJIANRXM;
                    hISMessageBody.XiaoXiLY = xiaoXi.XIAOXILY;
                    hISMessageBody.FaSongRen = (string.IsNullOrEmpty(xiaoXi.FAJIANRXM) ? xiaoXi.FAJIANRID : xiaoXi.FAJIANRXM);
                    hISMessageBody.JieShouBZ = shouJianXiang.JIESHOUBZ;
                    hISMessageBody.XiaoXiLY = xiaoXi.XIAOXILY;
                    result.Add(hISMessageBody);
                }
            }
            return result;
        }

        /// <summary>
        /// 获取离线消息
        /// </summary>
        /// <param name="zaiXianZTID">在线状态ID</param>
        /// <returns></returns>
        public List<HISMessageBody> GetLiXianXX(string zaiXianZTID, DateTime faSongSJ)
        {
            Check.NotEmpty(zaiXianZTID, "zaiXianZTID不能为空");

            XT_ZAIXIANZT zaiXian = xT_ZAIXIANZTRepository.GetByKey(zaiXianZTID);
            List<HISMessageBody> result = new List<HISMessageBody>();

            //判断在线状态为空
            if (zaiXian == null)
                return result;

            List<XT_XIAOXI_NEW> xiaoXiList = xT_XIAOXI_NEWRepository.GetWeiGuoQi(faSongSJ);
            List<long?> strXXID = xiaoXiList.Select(p => p.XIAOXIID).ToList();
            List<string> bingRenID = xiaoXiList.Select(p => p.BINGRENID).ToList();
            List<string> xiaoXiBM = xiaoXiList.Select(p => p.XIAOXIBM).ToList();
            //过滤已读消息
            var yiDuXiaoXi = xT_XIAOXISJX_NEWRepository.GetAllXiaoXiSJ(strXXID, "U-" + zaiXian.ZHIGONGID);
            //获取病人就诊信息
            var jiuZhenXxList = zJ_JIUZHENXXRepository.GetAllZJJZXX(bingRenID);
            //获取住院病人信息
            var zyBingRenXxList = zY_BINGRENXXRepository.GetAllZYBRXX(bingRenID);
            //获取权限
            var dingYue = xT_XIAOXIDY_NEWRepository.QueryDingYueLists(xiaoXiBM);
            foreach (var xiaoXi in xiaoXiList)
            {
                ////过滤已读消息
                //var shouJianXiang = xT_XIAOXISJX_NEWRepository.GetByKey(xiaoXi.XIAOXIID, "U-" + zaiXian.ZHIGONGID);
                var shouJianXiang = yiDuXiaoXi.Where(p => p.XIAOXIID == xiaoXi.XIAOXIID).FirstOrDefault();
                if (shouJianXiang != null && shouJianXiang.YiDu())
                {
                    continue;
                }

                // 病人主治医生
                string bingRenZzYs = string.Empty;
                // 病人医疗组
                string bingRenYlz = string.Empty;
                // 病人科室
                string bingRenKs = string.Empty;
                // 病人病区
                string bingRenBq = string.Empty;
                // 病人院区
                string bingRenYq = string.Empty;

                // 如果是门诊病人
                if (!string.IsNullOrWhiteSpace(xiaoXi.BINGRENID) && xiaoXi.MENZHENZYBZ == 0)
                {
                    var jiuZhenXx = jiuZhenXxList.Where(p => p.JIUZHENID == xiaoXi.BINGRENID).FirstOrDefault();
                    if (jiuZhenXx != null)
                    {
                        bingRenZzYs = jiuZhenXx.JIUZHENYS;
                        bingRenKs = jiuZhenXx.JIUZHENKS;
                        bingRenYq = jiuZhenXx.YUANQUID;
                    }
                }

                // 如果是住院病人
                if (!string.IsNullOrWhiteSpace(xiaoXi.BINGRENID) && xiaoXi.MENZHENZYBZ == 1)
                {
                    var zyBingRenXx = zyBingRenXxList.Where(p => p.BINGRENZYID == xiaoXi.BINGRENID).FirstOrDefault();
                    if (zyBingRenXx != null)
                    {
                        if(!string.IsNullOrEmpty(zyBingRenXx.ZAIYUANZT)&& zyBingRenXx.ZAIYUANZT!="0")
                        {
                            continue;
                        }
                        bingRenZzYs = zyBingRenXx.ZHUZHIYS;
                        bingRenYlz = zyBingRenXx.YILIAOZID;
                        bingRenKs = zyBingRenXx.DANGQIANKS;
                        bingRenBq = zyBingRenXx.DANGQIANBQ;
                        bingRenYq = zyBingRenXx.YUANQUID;
                    }
                }

                var dingYueList = JsonUtil.DeserializeToObject<List<XT_XIAOXIDY_NEW>>(xiaoXi.SHOUJIANREN);
                // 获取订阅列表并合并用户自定义订阅列表
                if (dingYueList != null && dingYueList.Count > 0)
                {
                    //dingYueList = dingYueList.Union(xT_XIAOXIDY_NEWRepository.QueryDingYueList(xiaoXi.XIAOXIBM)).ToList();
                    dingYueList = dingYueList.Union(dingYue.Where(p => p.XIAOXIBM == xiaoXi.XIAOXIBM)).ToList();
                }
                else
                {
                    dingYueList = dingYue.Where(p => p.XIAOXIBM == xiaoXi.XIAOXIBM).ToList();
                    //dingYueList = xT_XIAOXIDY_NEWRepository.QueryDingYueList(xiaoXi.XIAOXIBM);
                }

                bool FaSong = false;

                // 订阅列表和在线用户合并产生待发送列表
                foreach (XT_XIAOXIDY_NEW item in dingYueList)
                {
                    switch (item.GetDingYueRLx())
                    {
                        case DingYueRLx.用户:
                            FaSong = zaiXian.ZHIGONGID == item.GetShouJianZuID();
                            break;
                        case DingYueRLx.系统:
                            FaSong = zaiXian.XITONGID == item.GetShouJianZuID();
                            break;
                        case DingYueRLx.应用:
                            FaSong = zaiXian.YINGYONGID == item.GetShouJianZuID();
                            break;
                        case DingYueRLx.病区:
                            FaSong = zaiXian.BINGQUID == item.GetShouJianZuID();
                            break;
                        case DingYueRLx.科室:
                            FaSong = zaiXian.KESHIID == item.GetShouJianZuID();
                            break;
                        case DingYueRLx.医疗组:
                            FaSong = zaiXian.YILIAOZID == item.GetShouJianZuID();
                            break;
                        case DingYueRLx.院区:
                            FaSong = zaiXian.YUANQUID == item.GetShouJianZuID();
                            break;
                        case DingYueRLx.全院:
                            FaSong = true;
                            break;
                        case DingYueRLx.病人主治医生:
                            FaSong = zaiXian.ZHIGONGID == bingRenZzYs;
                            break;
                        case DingYueRLx.病人当前医疗组:
                            FaSong = zaiXian.YILIAOZID == bingRenYlz;
                            break;
                        case DingYueRLx.病人当前病区:
                            FaSong = zaiXian.BINGQUID == bingRenBq;
                            break;
                        case DingYueRLx.病人当前科室:
                            FaSong = zaiXian.KESHIID == bingRenKs;
                            break;
                        case DingYueRLx.病人当前院区:
                            FaSong = zaiXian.YUANQUID == bingRenYq;
                            break;
                        default:
                            break;
                    }


                    if (!item.AllowJueSeQx(zaiXian.DangQianJsQxIDList()) ||
                    !item.AllowYingYongQx(zaiXian.YINGYONGID))
                    {
                        FaSong = false;
                    }

                    if (FaSong)
                    {
                        break;
                    }

                }

                if (FaSong)
                {
                    HISMessageBody hISMessageBody = new HISMessageBody();
                    hISMessageBody.Receivers = new string[] { zaiXian.ZHIGONGID };
                    hISMessageBody.BaoMiXxBz = xiaoXi.BAOMIXXBZ;
                    hISMessageBody.BingRenID = xiaoXi.BINGRENID;
                    hISMessageBody.HuiZhiBz = xiaoXi.HUIZHIBZ;
                    hISMessageBody.MenZhenZyBz = xiaoXi.MENZHENZYBZ == null ? 1 : (int)xiaoXi.MENZHENZYBZ;
                    hISMessageBody.TiXingLx = xiaoXi.XIAOXITXLX;
                    hISMessageBody.XiaoXiBM = xiaoXi.XIAOXIBM;
                    hISMessageBody.XiaoXiBT = xiaoXi.XIAOXIZT;
                    hISMessageBody.XiaoXiID = xiaoXi.XIAOXIID.ToString();
                    hISMessageBody.XiaoXiNR = xiaoXi.XIAOXINR;
                    hISMessageBody.XiaoXiZY = xiaoXi.XIAOXIZY;
                    hISMessageBody.YiCiXBZ = xiaoXi.YICIXBZ;
                    hISMessageBody.YouXianJi = xiaoXi.YOUXIANJB;
                    hISMessageBody.YouXiaoSj = xiaoXi.YOUXIAOQI;
                    hISMessageBody.FuJianBz = (int)xiaoXi.FUJIANBZ;
                    hISMessageBody.XiaoXiMc = xiaoXi.XIAOXIMC;
                    hISMessageBody.XiaoXiJc = xiaoXi.XIAOXIJC;
                    hISMessageBody.FaSongSj = (DateTime)xiaoXi.FASONGSJ;
                    hISMessageBody.FaSongRen = (string.IsNullOrEmpty(xiaoXi.FAJIANRXM) ? xiaoXi.FAJIANRID : xiaoXi.FAJIANRXM);
                    if (shouJianXiang != null)
                    {
                        hISMessageBody.JieShouBZ = shouJianXiang.JIESHOUBZ;
                    }
                    hISMessageBody.XiaoXiLY = xiaoXi.XIAOXILY;
                    result.Add(hISMessageBody);
                }

            }

            return result;
        }

        /// <summary>
        /// 获取全部消息
        /// </summary>
        /// <param name="zaiXianZTID"></param>
        /// <returns></returns>
        public List<HISMessageBody> GetAllXX(string zaiXianZTID, DateTime faSongSJ)
        {
            Check.NotEmpty(zaiXianZTID, "zaiXianZTID不能为空");
            string value = HISClientHelper.IP + "|" + HISClientHelper.MAC;
            List<HISMessageBody> result = new List<HISMessageBody>();
            XT_ZAIXIANZT zaiXian = xT_ZAIXIANZTRepository.GetByKey(zaiXianZTID);
            if (zaiXian == null)
                return result;

            List<XT_XIAOXI_NEW> xiaoXiList = xT_XIAOXI_NEWRepository.GetWeiGuoQi(faSongSJ);
            List<long?> strXXID = xiaoXiList.Select(p => p.XIAOXIID).ToList();
            List<string> bingRenID = xiaoXiList.Select(p => p.BINGRENID).ToList();
            //过滤已读消息
            var yiDuXiaoXi = xT_XIAOXISJX_NEWRepository.GetAllXiaoXiSJ(strXXID, "U-" + zaiXian.ZHIGONGID);
            //获取病人就诊信息
            var jiuZhenXxList = zJ_JIUZHENXXRepository.GetAllZJJZXX(bingRenID);
            //获取住院病人信息
            var zyBingRenXxList = zY_BINGRENXXRepository.GetAllZYBRXX(bingRenID);
            foreach (var xiaoXi in xiaoXiList)
            {
                ////过滤已读消息
                //var shouJianXiang = xT_XIAOXISJX_NEWRepository.GetByKey(xiaoXi.XIAOXIID, "U-" + zaiXian.ZHIGONGID);
                var shouJianXiang = yiDuXiaoXi.Where(p => p.XIAOXIID == xiaoXi.XIAOXIID).FirstOrDefault();

                //if (shouJianXiang != null && shouJianXiang.YiDu())
                {
                    // 病人主治医生
                    string bingRenZzYs = string.Empty;
                    // 病人医疗组
                    string bingRenYlz = string.Empty;
                    // 病人科室
                    string bingRenKs = string.Empty;
                    // 病人病区
                    string bingRenBq = string.Empty;
                    // 病人院区
                    string bingRenYq = string.Empty;

                    // 如果是门诊病人
                    if (!string.IsNullOrWhiteSpace(xiaoXi.BINGRENID) && xiaoXi.MENZHENZYBZ == 0)
                    {
                        var jiuZhenXx = jiuZhenXxList.Where(p => p.JIUZHENID == xiaoXi.BINGRENID).FirstOrDefault();
                        if (jiuZhenXx != null)
                        {
                            bingRenZzYs = jiuZhenXx.JIUZHENYS;
                            bingRenKs = jiuZhenXx.JIUZHENKS;
                            bingRenYq = jiuZhenXx.YUANQUID;
                        }
                    }

                    // 如果是住院病人
                    if (!string.IsNullOrWhiteSpace(xiaoXi.BINGRENID) && (xiaoXi.MENZHENZYBZ == 1 || xiaoXi.MENZHENZYBZ == 2))
                    {
                        var zyBingRenXx = zyBingRenXxList.Where(p => p.BINGRENZYID == xiaoXi.BINGRENID).FirstOrDefault();
                        if (zyBingRenXx != null)
                        {
                            bingRenZzYs = zyBingRenXx.ZHUZHIYS;
                            bingRenYlz = zyBingRenXx.YILIAOZID;
                            bingRenKs = zyBingRenXx.DANGQIANKS;
                            bingRenBq = zyBingRenXx.DANGQIANBQ;
                            bingRenYq = zyBingRenXx.YUANQUID;
                        }
                    }

                    var dingYueList = JsonUtil.DeserializeToObject<List<XT_XIAOXIDY_NEW>>(xiaoXi.SHOUJIANREN);
                    // 获取订阅列表并合并用户自定义订阅列表
                    if (dingYueList != null && dingYueList.Count > 0)
                    {
                        dingYueList = dingYueList.Union(xT_XIAOXIDY_NEWRepository.QueryDingYueList(xiaoXi.XIAOXIBM)).ToList();
                    }
                    else
                    {
                        dingYueList = xT_XIAOXIDY_NEWRepository.QueryDingYueList(xiaoXi.XIAOXIBM);
                    }

                    bool FaSong = false;

                    // 订阅列表和在线用户合并产生待发送列表
                    foreach (XT_XIAOXIDY_NEW item in dingYueList)
                    {
                        switch (item.GetDingYueRLx())
                        {
                            case DingYueRLx.用户:
                                FaSong = zaiXian.ZHIGONGID == item.GetShouJianZuID();
                                break;
                            case DingYueRLx.系统:
                                FaSong = zaiXian.XITONGID == item.GetShouJianZuID();
                                break;
                            case DingYueRLx.应用:
                                FaSong = zaiXian.YINGYONGID == item.GetShouJianZuID();
                                break;
                            case DingYueRLx.病区:
                                FaSong = zaiXian.BINGQUID == item.GetShouJianZuID();
                                break;
                            case DingYueRLx.科室:
                                FaSong = zaiXian.KESHIID == item.GetShouJianZuID();
                                break;
                            case DingYueRLx.医疗组:
                                FaSong = zaiXian.YILIAOZID == item.GetShouJianZuID();
                                break;
                            case DingYueRLx.院区:
                                FaSong = zaiXian.YUANQUID == item.GetShouJianZuID();
                                break;
                            case DingYueRLx.全院:
                                FaSong = true;
                                break;
                            case DingYueRLx.病人主治医生:
                                FaSong = zaiXian.ZHIGONGID == bingRenZzYs;
                                break;
                            case DingYueRLx.病人当前医疗组:
                                FaSong = zaiXian.YILIAOZID == bingRenYlz;
                                break;
                            case DingYueRLx.病人当前病区:
                                FaSong = zaiXian.BINGQUID == bingRenBq;
                                break;
                            case DingYueRLx.病人当前科室:
                                FaSong = zaiXian.KESHIID == bingRenKs;
                                break;
                            case DingYueRLx.病人当前院区:
                                FaSong = zaiXian.YUANQUID == bingRenYq;
                                break;
                            default:
                                break;
                        }

                        if (!item.AllowJueSeQx(zaiXian.DangQianJsQxIDList()) ||
                        !item.AllowYingYongQx(zaiXian.YINGYONGID))
                        {
                            LogHelper.Intance.Info("查询已读消息方法", "查询已读消息方法" + value, "权限不在此范围内！;");
                            FaSong = false;
                        }

                        if (FaSong)
                        {
                            LogHelper.Intance.Info("查询已读消息方法", "查询已读消息方法" + value, "已获取到接收权限！;");
                            break;
                        }
                        LogHelper.Intance.Info("查询已读消息方法", "查询已读消息方法" + item, "遍历循环日志处理！;");
                    }
                    if (FaSong)
                    {
                        LogHelper.Intance.Info("查询已读消息方法", "查询已读消息方法" + value, "获取已读信息赋值！;");
                        HISMessageBody hISMessageBody = new HISMessageBody();
                        hISMessageBody.Receivers = new string[] { zaiXian.ZHIGONGID };
                        hISMessageBody.BaoMiXxBz = xiaoXi.BAOMIXXBZ;
                        hISMessageBody.BingRenID = xiaoXi.BINGRENID;
                        hISMessageBody.HuiZhiBz = xiaoXi.HUIZHIBZ;
                        hISMessageBody.MenZhenZyBz = xiaoXi.MENZHENZYBZ == null ? 1 : (int)xiaoXi.MENZHENZYBZ;
                        hISMessageBody.TiXingLx = xiaoXi.XIAOXITXLX;
                        hISMessageBody.XiaoXiBM = xiaoXi.XIAOXIBM;
                        hISMessageBody.XiaoXiBT = xiaoXi.XIAOXIZT;
                        hISMessageBody.XiaoXiID = xiaoXi.XIAOXIID.ToString();
                        hISMessageBody.XiaoXiNR = xiaoXi.XIAOXINR;
                        hISMessageBody.XiaoXiZY = xiaoXi.XIAOXIZY;
                        hISMessageBody.YiCiXBZ = xiaoXi.YICIXBZ;
                        hISMessageBody.YouXianJi = xiaoXi.YOUXIANJB;
                        hISMessageBody.YouXiaoSj = xiaoXi.YOUXIAOQI;
                        hISMessageBody.FuJianBz = (int)xiaoXi.FUJIANBZ;
                        hISMessageBody.XiaoXiMc = xiaoXi.XIAOXIMC;
                        hISMessageBody.XiaoXiJc = xiaoXi.XIAOXIJC;
                        hISMessageBody.FaSongSj = (DateTime)xiaoXi.FASONGSJ;
                        hISMessageBody.XiaoXiLY = xiaoXi.XIAOXILY;
                        hISMessageBody.ShouJianRen = xiaoXi.SHOUJIANRXM;
                        hISMessageBody.XiaoXiLY = xiaoXi.XIAOXILY;
                        hISMessageBody.FaSongRen = (string.IsNullOrEmpty(xiaoXi.FAJIANRXM) ? xiaoXi.FAJIANRID : xiaoXi.FAJIANRXM);
                        if (shouJianXiang != null)
                        {
                            hISMessageBody.JieShouBZ = shouJianXiang.JIESHOUBZ;
                        }
                        hISMessageBody.XiaoXiLY = xiaoXi.XIAOXILY;
                        result.Add(hISMessageBody);
                    }
                }
            }
            LogHelper.Intance.Info("查询已读消息方法", "查询已读消息方法" + value, "获取已读信息赋值，总行数" + result.Count + "！;");
            return result;
        }

        /// <summary>
        /// 获取已读消息
        /// </summary>
        /// <param name="zaiXianZTID"></param>
        /// <param name="messAgeBodyList"></param>
        /// <returns></returns>
        public List<HISMessageBody> GetYiDuXX_YH(string zaiXianZTID, List<HISMessageBody> messAgeBodyList)
        {
            Check.NotNull(messAgeBodyList, "messAgeBodyList不能为空");
            List<HISMessageBody> result = new List<HISMessageBody>();
            XT_ZAIXIANZT zaiXian = xT_ZAIXIANZTRepository.GetByKey(zaiXianZTID);
            if (zaiXian == null)
                return result;

            List<string> strXXID = messAgeBodyList.Select(p => p.XiaoXiID).ToList();
            List<long?> longXXID = new List<long?>();
            foreach (string xxid in strXXID)
            { longXXID.Add(Convert.ToInt32(xxid)); }
            List<XT_XIAOXI_NEW> xiaoXiList = xT_XIAOXI_NEWRepository.GetXiaoXiByID(longXXID);
            var yiDuXiaoXi = xT_XIAOXISJX_NEWRepository.GetAllXiaoXiSJ(longXXID);

            foreach (var xiaoXi in xiaoXiList)
            {
                XT_XIAOXISJX_NEW shouJianXiang = new XT_XIAOXISJX_NEW();
                if (!string.IsNullOrEmpty(ServiceContext.BINGQUID))
                    shouJianXiang = yiDuXiaoXi.Where(p => (p.SHOUJIANRID.Contains(ServiceContext.KESHIID) || p.SHOUJIANRID.Contains(ServiceContext.USERID) || p.SHOUJIANRID.Contains(ServiceContext.BINGQUID)) && p.XIAOXIID == xiaoXi.XIAOXIID).FirstOrDefault();
                else
                    shouJianXiang = yiDuXiaoXi.Where(p => (p.SHOUJIANRID.Contains(ServiceContext.KESHIID) || p.SHOUJIANRID.Contains(ServiceContext.USERID)) && p.XIAOXIID == xiaoXi.XIAOXIID).FirstOrDefault();
                if (shouJianXiang != null && shouJianXiang.YiDu())
                {
                    HISMessageBody hISMessageBody = new HISMessageBody();
                    hISMessageBody.Receivers = new string[] { zaiXian.ZHIGONGID };
                    hISMessageBody.BaoMiXxBz = xiaoXi.BAOMIXXBZ;
                    hISMessageBody.BingRenID = xiaoXi.BINGRENID;
                    hISMessageBody.HuiZhiBz = xiaoXi.HUIZHIBZ;
                    hISMessageBody.MenZhenZyBz = xiaoXi.MENZHENZYBZ == null ? 1 : (int)xiaoXi.MENZHENZYBZ;
                    hISMessageBody.TiXingLx = xiaoXi.XIAOXITXLX;
                    hISMessageBody.XiaoXiBM = xiaoXi.XIAOXIBM;
                    hISMessageBody.XiaoXiBT = xiaoXi.XIAOXIZT;
                    hISMessageBody.XiaoXiID = xiaoXi.XIAOXIID.ToString();
                    hISMessageBody.XiaoXiNR = xiaoXi.XIAOXINR;
                    hISMessageBody.XiaoXiZY = xiaoXi.XIAOXIZY;
                    hISMessageBody.YiCiXBZ = xiaoXi.YICIXBZ;
                    hISMessageBody.YouXianJi = xiaoXi.YOUXIANJB;
                    hISMessageBody.YouXiaoSj = xiaoXi.YOUXIAOQI;
                    hISMessageBody.FuJianBz = (int)xiaoXi.FUJIANBZ;
                    hISMessageBody.XiaoXiMc = xiaoXi.XIAOXIMC;
                    hISMessageBody.XiaoXiJc = xiaoXi.XIAOXIJC;
                    hISMessageBody.FaSongSj = (DateTime)xiaoXi.FASONGSJ;
                    hISMessageBody.XiaoXiLY = xiaoXi.XIAOXILY;
                    hISMessageBody.ShouJianRen = xiaoXi.SHOUJIANRXM;
                    hISMessageBody.XiaoXiLY = xiaoXi.XIAOXILY;
                    hISMessageBody.FaSongRen = (string.IsNullOrEmpty(xiaoXi.FAJIANRXM) ? xiaoXi.FAJIANRID : xiaoXi.FAJIANRXM);
                    hISMessageBody.JieShouBZ = shouJianXiang.JIESHOUBZ;
                    hISMessageBody.XiaoXiLY = xiaoXi.XIAOXILY;
                    //hISMessageBody.ShouJianRenID = xiaoXi.SHOUJIANRID;
                    result.Add(hISMessageBody);
                }
            }
            return result;
        }
    }
}
