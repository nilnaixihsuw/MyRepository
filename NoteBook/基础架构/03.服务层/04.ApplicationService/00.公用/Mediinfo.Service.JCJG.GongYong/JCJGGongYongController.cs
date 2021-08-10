using Mediinfo.Domain.JCJG.GY;
using Mediinfo.DTO.Core;
using Mediinfo.DTO.HIS.GY;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core;
using Mediinfo.Service.JCJG.GongYong.Route;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Mediinfo.Service.JCJG.GongYong
{
    [ServiceRoutePrefix]
    [Route("JCJGGongYong/{action}")]
    public class JCJGGongYongController : Mediinfo.Infrastructure.JCJG.Controller.HISController
    {
        #region 查询类

        /// <summary>
        /// 获取公用代码BY代码类别
        /// </summary>
        /// <param name="daimalb"></param>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<List<E_GY_DAIMA>> GetGYDaiMa(string daimalb)
        {
            Check.NotEmpty(daimalb, "代码类别不能为空！");

            E_GY_DAIMA entry = new E_GY_DAIMA();
            entry.Where(" Where DAIMALB = :DAIMALB ", daimalb);
            var list = new QueryService(UnitOfWork).Get<E_GY_DAIMA>(entry);

            return ServiceContent(list);
        }

        /// <summary>
        /// 获取公用科室名称
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [HttpGet]
        public ServiceResult<string> GetKeShiMCByKeShiID(string keShiID)
        {
            if (string.IsNullOrWhiteSpace(keShiID)) return ServiceContent("");
            E_GY_KESHI entry = new E_GY_KESHI();
            entry.Where(" WHERE KESHIID = :KESHIID ", keShiID);
            var keShi = new QueryService(UnitOfWork).Get<E_GY_KESHI>(entry).FirstOrDefault();
            if (keShi == null)
            {
                return ServiceContent("");

            }
            return ServiceContent(keShi.KESHIMC);
        }

        /// <summary>
        /// 获取公用科室
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [HttpGet]
        public ServiceResult<List<E_GY_KESHI>> GetGongYongKS()
        {

            E_GY_KESHI entry = new E_GY_KESHI();

            var list = new QueryService(UnitOfWork).Get<E_GY_KESHI>(entry);

            return ServiceContent(list);
        }

        /// <summary>
        /// 获取收费项目
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<List<E_GY_SHOUFEIXM>> GetShouFeiXM()
        {
            E_GY_SHOUFEIXM entry = new E_GY_SHOUFEIXM();
            var list = new QueryService(UnitOfWork).Get<E_GY_SHOUFEIXM>(entry);
            return ServiceContent(list);
        }

        /// <summary>
        /// 获取挂号费项目
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<List<E_GY_SHOUFEIXM>> GetGuaHaoFXM()
        {
            E_GY_SHOUFEIXM entry = new E_GY_SHOUFEIXM();
            entry.Where(@" WHERE (Taocanbz = 0 And Mojibz = 1 And Zuofeibz = 0 And (Shoufeixmid In(Select Canshuzhi From Gy_Canshu Where Canshuid = '挂号_就诊卡成本收费项目')))
             OR (Taocanbz = 0 And Mojibz = 1 And Zuofeibz = 0 And (Shoufeixmid In (Select Canshuzhi From Gy_Canshu Where Canshuid = '挂号_病历本成本收费项目')) )
             OR (Taocanbz = 0 And Mojibz = 1 And Zuofeibz = 0 And (Hesuanxm In (Select Canshuzhi From Gy_Canshu Where Canshuid = '挂号_诊疗费核算项目')))
             OR (Taocanbz = 0 And Mojibz = 1 And Zuofeibz = 0 And (Hesuanxm In (Select Canshuzhi From Gy_Canshu Where Canshuid = '挂号_挂号费核算项目')) )");
            var list = new QueryService(UnitOfWork).Get<E_GY_SHOUFEIXM>(entry);
            return ServiceContent(list);
        }

        /// <summary>
        /// 获取病区
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<List<E_GY_BINGQU>> GetGYBingQu()
        {
            E_GY_BINGQU entry = new E_GY_BINGQU();
            // entry.Where(" WHERE ZUOFEIBZ = :ZUOFEIBZ ", 0);
            var list = new QueryService(UnitOfWork).Get<E_GY_BINGQU>(entry);
            return ServiceContent(list);
        }

        /// <summary>
        /// 根据科室id取对应病区
        /// </summary>
        /// <param name="keShiID">科室id</param>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<List<E_GY_KESHIBQ>> GetBingQuByKSID(string keShiID)
        {
            E_GY_KESHIBQ entry = new E_GY_KESHIBQ();
            entry.Where(" WHERE KESHIID = :KESHIID ", keShiID);
            var list = new QueryService(UnitOfWork).Get<E_GY_KESHIBQ>(entry);
            return ServiceContent(list);
        }

        /// <summary>
        /// 取系统时间
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<DateTime> GetSysDate()
        {
            var dateTime = new QueryService(UnitOfWork).GetSYSTime();
            return ServiceContent(dateTime);
        }

        /// <summary>
        /// 取院区
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<List<E_GY_YUANQU>> GetYuanQu()
        {
            E_GY_YUANQU entry = new E_GY_YUANQU();
            // entry.Where(" WHERE ZUOFEIBZ = :ZUOFEIBZ ", 0);
            var list = new QueryService(UnitOfWork).Get<E_GY_YUANQU>(entry);
            return ServiceContent(list);
        }

        /// <summary>
        /// 根据作废标志取院区
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<List<E_GY_YUANQU>> GetYuanQuByZuoFei(string zuoFeiBZ)
        {
            E_GY_YUANQU entry = new E_GY_YUANQU();
            if (zuoFeiBZ == "0")
            {
                entry.Where(" WHERE ZUOFEIBZ = :ZUOFEIBZ ", 0);
            }
            entry.WhereAppend(" ORDER BY YUANQUID ");
            var list = new QueryService(UnitOfWork).Get<E_GY_YUANQU>(entry);
            return ServiceContent(list);
        }

        /// <summary>
        /// 取应用
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<List<E_GY_YINGYONG>> GetYingYong()
        {
            E_GY_YINGYONG entry = new E_GY_YINGYONG();
            // entry.Where(" WHERE ZUOFEIBZ = :ZUOFEIBZ ", 0);
            var list = new QueryService(UnitOfWork).Get<E_GY_YINGYONG>(entry);
            return ServiceContent(list);
        }

        /// <summary>
        /// 取应用
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<List<E_GY_YINGYONG>> GetYingYongSJ(string jiaGeID)
        {
            E_GY_YINGYONG entry = new E_GY_YINGYONG();
            entry.Where(" where SubStr(YINGYONGID, 1, 2) IN ('05', '06', '07', '13', '33', '08')");
            var list = new QueryService(UnitOfWork).Get<E_GY_YINGYONG>(entry);
            //YKFGongYongDomainService ykfGY = new YKFGongYongDomainService(UnitOfWork, ServiceContext);
            //foreach (E_GY_YINGYONG yy in list)
            //{
            //    yy.KUCUNSL = ykfGY.GetKuCunSL(jiaGeID, yy.YINGYONGID, 1, 2, 4, null, null, null, null);
            //    yy.ZHANGMIANKCSL = ykfGY.GetKuCunSL(jiaGeID, yy.YINGYONGID, 1, 2, 6, null, null, null, null);
            //    yy.XUKUCUNSL = ykfGY.GetKuCunSL(jiaGeID, yy.YINGYONGID, 1, 2, 5, null, null, null, null);
            //}
            return ServiceContent(list);
        }

        /// <summary>
        /// 获取应用对照
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<List<E_GY_YINGYONG>> GetYingYongDZ()
        {
            E_GY_YINGYONG entry = new E_GY_YINGYONG();
            entry.Where(" where yingyongid in (select yingyongid2 from gy_churukdz dz where dz.yingyongid1 = :yingyongid1)", ServiceContext.YINGYONGID);
            var list = new QueryService(UnitOfWork).Get<E_GY_YINGYONG>(entry);
            return ServiceContent(list);
        }

        /// <summary>
        /// 取应用单元
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<List<E_GY_YINGYONGDY>> GetYingYongDY()
        {
            E_GY_YINGYONGDY entry = new E_GY_YINGYONGDY();
            var list = new QueryService(UnitOfWork).Get<E_GY_YINGYONGDY>(entry);
            return ServiceContent(list);
        }

        //[HttpPost]
        //public ServiceResult<List<E_GY_YINGYONGS>> GetKCFBData(string id)
        //{
        //    E_GY_YINGYONGS entry = new E_GY_YINGYONGS();
        //    entry.AddParameter("ID", id);
        //    entry.Where(" WHERE FUN_GY_ISYAOKUFANG(YINGYONGID) = 1");
        //    var list = new QueryService(UnitOfWork).Get<E_GY_YINGYONGS>(entry);
        //    return ServiceContent(list);
        //}

        /// <summary>
        /// 获取请领入库方式
        /// </summary>
        /// <returns></returns>
        //[HttpPost]
        //public ServiceResult<List<E_GY_QINGLINGRKFS>> GetQingLingRKFS()
        //{
        //    E_GY_QINGLINGRKFS entry = new E_GY_QINGLINGRKFS();
        //    var list = new QueryService(UnitOfWork).Get<E_GY_QINGLINGRKFS>(entry);
        //    return ServiceContent(list);
        //}

        /// <summary>
        /// 按病区id取科室病区
        /// </summary>
        /// <param name="bingQuID"></param>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<List<E_GY_KESHIBQ>> GetKeShiBQ(string bingQuID)
        {
            E_GY_KESHIBQ entry = new E_GY_KESHIBQ();
            entry.Where("WHERE BINGQUID = :BingQuID", bingQuID);
            var list = new QueryService(UnitOfWork).Get<E_GY_KESHIBQ>(entry);
            return ServiceContent(list);
        }

        /// <summary>
        /// 按keshiid取病区
        /// </summary>
        /// <param name="keShiID"></param>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<List<E_GY_KESHIBQ>> GetBingQukS(string keShiID)
        {
            E_GY_KESHIBQ entry = new E_GY_KESHIBQ();
            entry.Where("WHERE KESHIID = :keShiID", keShiID);
            var list = new QueryService(UnitOfWork).Get<E_GY_KESHIBQ>(entry);
            return ServiceContent(list);
        }

        /// <summary>
        /// 获取图片信息
        /// </summary>
        /// <param name="tuPianID">图片ID</param>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<List<E_GY_TUPIANXX>> GetTuPianXX(string tuPianID)
        {
            Check.NotEmpty(tuPianID, "图片ID不允许为空");
            E_GY_TUPIANXX entry = new E_GY_TUPIANXX();
            entry.Where(" WHERE TUPIANID =:TUPIANID", tuPianID);
            var list = new QueryService(UnitOfWork).Get<E_GY_TUPIANXX>(entry);
            return ServiceContent(list);
        }

        #endregion

        #region 业务类

        /// <summary>
        /// 取序号
        /// </summary>
        /// <param name="xuHaoMC"></param>
        /// <param name="yuanQuID"></param>
        /// <param name="Count"></param>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<List<string>> GetOrder(string xuHaoMC, string yuanQuID, int Count = 1)
        {
            var xuHaoRep = this.GetRepository<IGY_XUHAORepository>();
            var list = xuHaoRep.GetOrder(xuHaoMC, yuanQuID, Count);
            return ServiceContent(list);
        }

        /// <summary>
        /// 取序号列表
        /// </summary>
        /// <param name="dicXuHao">键值对：序号名称 - 数量</param>
        /// <param name="yuanQuID">院区ID</param>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<Dictionary<string, List<string>>> GetOrderList(
            Dictionary<string, int> dicXuHao,
            string yuanQuID)
        {
            // 定义序号键值对，返回格式：序号名称 - 序号列表
            Dictionary<string, List<string>> dic = new Dictionary<string, List<string>>();
            var xuHaoRep = this.GetRepository<IGY_XUHAORepository>();

            foreach (string xuHao in dicXuHao.Keys)
            {
                // 要取的序号数量
                int count = dicXuHao[xuHao];
                // 取序号列表
                List<string> list = xuHaoRep.GetOrder(xuHao, yuanQuID, count);
                dic.Add(xuHao, list);
            }
            return ServiceContent(dic);
        }

        /// <summary>
        /// 保存院区信息
        /// </summary>
        /// <param name="listYuanQu"></param>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<bool> SaveYuanQuXX(List<E_GY_YUANQU> listYuanQu)
        {
            UnitOfWork.BeginTransaction();

            var yuanQuRep = this.GetRepository<IGY_YUANQURepository>(UnitOfWork);

            //新增院区
            if (listYuanQu.GetNews().Count > 0)
            {
                listYuanQu.GetNews().ForEach(p =>
                {
                    var yuanQu = GY_YUANQUFactory.Create(yuanQuRep, ServiceContext, p);
                });
            }

            //更新院区
            if (listYuanQu.GetUpdates().Count > 0)
            {
                listYuanQu.GetUpdates().ForEach(p =>
                {
                    var yuanQu = yuanQuRep.GetByKey(p.YUANQUID);
                    yuanQu.UpdateYuanQu(p);
                });
            }

            if (listYuanQu.GetDeletes().Count > 0)
            {
                listYuanQu.GetDeletes().ForEach(p =>
                {
                    var yuanQu = yuanQuRep.GetByKey(p.YUANQUID);
                    yuanQuRep.RegisterDelete(yuanQu);
                });
            }
            UnitOfWork.SaveChanges();
            //提交
            UnitOfWork.Commit();
            return ServiceContent(true);
        }

        #endregion
    }
}
