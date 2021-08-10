using Mediinfo.Domain.JCJG.GY;
using Mediinfo.DTO.HIS.GY;
using Mediinfo.Enterprise;
using Mediinfo.Enterprise.Exceptions;
using Mediinfo.Infrastructure.Core;
using Mediinfo.Infrastructure.Core.Domain;
using Mediinfo.Service.JCJG.GongYong.Route;
using Mediinfo.Utility.Extensions;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Mediinfo.Service.JCJG.GongYong
{
    /// <summary>
    /// 公用参数处理类
    /// </summary>
    [ServiceRoutePrefix]
    [Route("JCJGCanSu/{action}")]
    public class JCJGCanSuController : Mediinfo.Infrastructure.JCJG.Controller.HISController
    {
        /// <summary>
        /// 获取所有参数（同名参数可能存在重复）
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [HttpGet]
        public ServiceResult<List<E_GY_CANSHU>> GetAll()
        {
            E_GY_CANSHU canShu = new E_GY_CANSHU();

            var list = new QueryService(UnitOfWork).Get<E_GY_CANSHU>(canShu);

            return new ServiceResult<List<E_GY_CANSHU>>(list);

        }

        /// <summary>
        /// 通过应用ID批量获取参数（同名参数可能存在重复）
        /// </summary>
        /// <param name="yingYongId"></param>
        /// <returns></returns>
        [HttpPost]
        [HttpGet]
        public ServiceResult<List<E_GY_CANSHU>> GetByYingYongId(string yingYongId)
        {
            if (string.IsNullOrWhiteSpace(yingYongId))
            {
                throw new ServiceException("应用ID不能为空");
            }

            if (yingYongId.Length < 4)
            {
                throw new ServiceException("无效的应用ID,长度不能小于4位");
            }


            E_GY_CANSHU canShu = new E_GY_CANSHU();
            canShu.Where("where yingyongid=:yingyongId or yingyongid = :xitongId or yingyongid='00'", yingYongId, yingYongId.Substring(0, 2));
            var list = new QueryService(UnitOfWork).Get<E_GY_CANSHU>(canShu);

            return new ServiceResult<List<E_GY_CANSHU>>(list);


        }

        /// <summary>
        /// 根据参数ID获取参数（同名参数可能存在重复）
        /// </summary>
        /// <param name="canShuId">参数ID</param>
        /// <returns></returns>
        [HttpPost]
        [HttpGet]
        public ServiceResult<List<E_GY_CANSHU>> GetCanShu(string canShuId)
        {
            if (string.IsNullOrWhiteSpace(canShuId))
                throw new ServiceException("参数ID未传入");

            E_GY_CANSHU canShu = new E_GY_CANSHU();

            canShu.Where(" where canshuid=:a1", canShuId);

            var list = new QueryService(UnitOfWork).Get<E_GY_CANSHU>(canShu);

            return ServiceContent(list);
        }

        /// <summary>
        /// 获取参数值（按照应用ID->系统ID->"00")的顺序获取
        /// </summary>
        /// <param name="canShuList"></param>
        /// <returns></returns>

        [HttpPost]
        [HttpGet]
        public ServiceResult<List<E_GY_CANSHU_QUZHI>> GetCanShuZhi(List<E_GY_CANSHU_QUZHI> canShuList)
        {
            List<E_GY_CANSHU_QUZHI> list = new List<E_GY_CANSHU_QUZHI>();
            var canShuRep = this.GetRepository<IGY_CANSHURepository>(UnitOfWork);

            foreach (var item in canShuList)
            {
                list.Add(new E_GY_CANSHU_QUZHI
                {
                    CanShuID = item.CanShuID,
                    YingYongID = item.YingYongID,
                    DefaultValue = item.DefaultValue,
                    CanShuZhi = canShuRep.GetCanShu(item.YingYongID, item.CanShuID, item.DefaultValue)
                });
            }
            return ServiceContent(list);
        }
        /// <summary>
        /// 查询并插入00的参数
        /// </summary>
        /// <param name="canShuList"></param>
        /// <returns></returns>
        [HttpPost]
        [HttpGet]
        public ServiceResult<List<E_GY_CANSHU_QUZHI>> GetAndSetCanShuZhi(List<E_GY_CANSHU_QUZHI> canShuList)
        {
            List<E_GY_CANSHU_QUZHI> list = new List<E_GY_CANSHU_QUZHI>();


            var canShuRep = this.GetRepository<IGY_CANSHURepository>(UnitOfWork);
            foreach (var item in canShuList)
            {
                list.Add(new E_GY_CANSHU_QUZHI
                {
                    CanShuID = item.CanShuID,
                    YingYongID = item.YingYongID,
                    DefaultValue = item.DefaultValue,
                    CanShuZhi = canShuRep.GetCanShu(item.YingYongID, item.CanShuID, item.DefaultValue)
                });
            }

            foreach (var item in canShuList)
            {
                var canShuList1 = canShuRep.GetList(item.CanShuID).ToList();
                if (canShuList1.Find(w => w.YINGYONGID == "00") == null)
                {
                    E_GY_CANSHU canShu = new E_GY_CANSHU();
                    canShu.YINGYONGID = "00";
                    canShu.CANSHUID = item.CanShuID;
                    canShu.CANSHUMS = canShuList1.Find(f => f.YINGYONGID != "00") == null ? "数据库里无该参数，系统自动生成插入" : canShuList1.Find(f => f.YINGYONGID != "00").CANSHUMS;
                    canShu.XIANGGUANSM = "数据库里无该参数，系统自动生成插入";
                    canShu.CANSHUZHI = string.IsNullOrWhiteSpace(item.DefaultValue) ? "1" : item.DefaultValue;
                    canShu.QUESHENGZHI = string.IsNullOrWhiteSpace(item.DefaultValue) ? "1" : item.DefaultValue;
                    canShu.JIAZAIFS = "1";
                    canShu.XITONGBZ = 1;
                    canShu.XIUGAIREN = ServiceContext.USERID;
                    canShu.XIUGAISJ = canShuRep.GetSYSTime();
                    GY_CANSHUFactory.Create(canShuRep, ServiceContext, canShu);

                }
            }
            UnitOfWork.BulkSaveChanges();
            return ServiceContent(list);
        }

        [HttpPost]
        [HttpGet]
        public ServiceResult<List<E_GY_CANSHU_QUZHI>> GetChuangKouCanShuZhi(string chuangKouMc, List<E_GY_CANSHU_QUZHI> canShuList)
        {
            List<E_GY_CANSHU_QUZHI> list = new List<E_GY_CANSHU_QUZHI>();


            var canShuRep = this.GetRepository<IGY_CANSHURepository>(UnitOfWork);
            var chuangKouCsRep = this.GetRepository<IGY_CHUANGKOUCSDYJLRepository>(UnitOfWork);

            List<GY_CHUANGKOUCSDYJL> jlList = new List<GY_CHUANGKOUCSDYJL>();

            foreach (var item in canShuList)
            {
                var csz = canShuRep.GetCanShu(item.YingYongID, item.CanShuID, item.DefaultValue);
                list.Add(new E_GY_CANSHU_QUZHI
                {
                    CanShuID = item.CanShuID,
                    YingYongID = item.YingYongID,
                    DefaultValue = item.DefaultValue,
                    CanShuZhi = csz
                });

                jlList.Add(new GY_CHUANGKOUCSDYJL()
                {
                    JILUID = Guid.NewGuid().ToString(),
                    CANSHUID = item.CanShuID,
                    CANSHUZHI = csz,
                    CHUANGKOUMC = chuangKouMc,
                    DEFAULTVALUE = item.DefaultValue,
                    YINGYONGID = item.YingYongID
                });
            }

            // 更新或插入窗口调用记录
            chuangKouCsRep.InsertOrUpdate(jlList);

            return ServiceContent(list);

        }

        [HttpPost]
        public ServiceResult<List<E_GY_CANSHU>> GetParamsByKey(List<string> paramList)
        {
            List<E_GY_CANSHU> list = new List<E_GY_CANSHU>();
            var canShuRep = this.GetRepository<IGY_CANSHURepository>(UnitOfWork);

            foreach (var t in paramList)
            {
                if (t.IsNullOrWhiteSpace()) continue;
                var item = t.Split('-');
                if (item.Length == 2)
                {
                    var yingYongID = item[0];
                    var canShuID = item[1];
                    list.Add(canShuRep.GetParamByKey(yingYongID, canShuID).DBToE<GY_CANSHU, E_GY_CANSHU>());
                }

            }
            return ServiceContent(list);
        }

        [HttpPost]
        public ServiceResult<List<E_GY_CANSHU_ZUOYONGYU>> GetWindowParamByZuoYongYu(List<string> zuoYongYuList)
        {
            E_GY_CANSHU_ZUOYONGYU entity = new E_GY_CANSHU_ZUOYONGYU();
            string zuoYongYu = zuoYongYuList.Aggregate("", (current, t) => current + ",'" + t + "',").Trim(',');

            entity.Where($" where zuoyongyu in ({zuoYongYu})");

            List<E_GY_CANSHU_ZUOYONGYU> list = new QueryService(UnitOfWork).Get(entity);

            return ServiceContent(list);
        }
    }
}
