using Mediinfo.Domain.JCJG.GY;
using Mediinfo.DTO.Core;
using Mediinfo.DTO.HIS.GY;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core;
using Mediinfo.Service.JCJG.GongYong.Route;

using System;
using System.Collections.Generic;
using System.Web.Http;

namespace Mediinfo.Service.JCJG.GongYong
{
    [ServiceRoutePrefix]
    [Route("JCJGDaiMa/{action}")] 
    public class JCJGDaiMaController : Mediinfo.Infrastructure.JCJG.Controller.HISController
    {
        #region 查询

        /// <summary>
        /// 取代码类型
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<List<E_GY_DAIMA>> GETDaiMa()
        {
            E_GY_DAIMA dtoDaiMa = new E_GY_DAIMA();
            var list = new QueryService(UnitOfWork).Get<E_GY_DAIMA>(dtoDaiMa);
            return ServiceContent(list);
        }


        /// <summary>
        /// 通过代码类别取代码 
        /// </summary>
        /// <param name="daiMaLB">代码类别</param>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<List<E_GY_DAIMA>> GETDaiMaByLB(string daiMaLB)
        {
            E_GY_DAIMA dtoDaiMa = new E_GY_DAIMA();
            dtoDaiMa.Where("WHERE DAIMALB = :DAIMALB", daiMaLB);
            var list = new  QueryService(UnitOfWork).Get<E_GY_DAIMA>(dtoDaiMa);
            return ServiceContent(list);
        }
        /// <summary>
        /// 检查部位专用，通过ZiFu3(jijiabw)取代码 
        /// </summary>
        /// <param name="daiMaLB">代码类别</param>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<List<E_GY_DAIMA>> GETDaiMaByJiJiaBW(string daiMaLB, string ZiFu3)
        {
            E_GY_DAIMA dtoDaiMa = new E_GY_DAIMA();
            dtoDaiMa.Where("WHERE DAIMALB = :DAIMALB AND (ZiFu3=:ZiFu3 or zifu3 is null)", daiMaLB, ZiFu3);
            var list = new QueryService(UnitOfWork).Get<E_GY_DAIMA>(dtoDaiMa);
            return ServiceContent(list);
        }
        /// <summary>
        /// 查询指定代码类别的公用代码
        /// </summary>
        /// <param name="daiMaLB"></param>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<List<E_GY_DAIMA>> GetDaiMaByLBS(List<string> daiMaLB)
        {
            if (daiMaLB == null || daiMaLB.Count == 0)
            {
                throw new ApplicationException("参数daiMaLB不能为空！");
            }

            E_GY_DAIMA dtoDaiMa = new E_GY_DAIMA();
            dtoDaiMa.Where("Where daimalb = :daimalb", daiMaLB[0]);
            daiMaLB.RemoveAt(0);
            daiMaLB.ForEach(key => {
                dtoDaiMa.WhereAppend(" or daimalb = :daimalb" + key, key);
            });

            return ServiceContent(new QueryService(UnitOfWork).Get(dtoDaiMa));
        }
        /// <summary>
        /// 通过代码类别取代码 
        /// </summary>
        /// <param name="daiMaLB">代码类别</param>
        /// <param name="zifu1">字符1</param>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<List<E_GY_DAIMA>> GETDaiMaByLBZF1(string daiMaLB,string zifu1)
        {
            E_GY_DAIMA dtoDaiMa = new E_GY_DAIMA();
            dtoDaiMa.Where("WHERE DAIMALB = :DAIMALB AND ZIFU1 = :ZIFU1", daiMaLB, zifu1);
            var list = new QueryService(UnitOfWork).Get<E_GY_DAIMA>(dtoDaiMa);
            return ServiceContent(list);
        }

        /// <summary>
        /// 通过代码类别和代码ID获得指定的代码 
        /// </summary>
        /// <param name="daiMaLB">代码类别</param>
        /// <param name="daiMaID">代码ID</param>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<List<E_GY_DAIMA>> GETDaiMaByDaiMaID(string daiMaLB,string daiMaID)
        {
            E_GY_DAIMA dtoDaiMa = new E_GY_DAIMA();
            dtoDaiMa.Where("WHERE DAIMALB = :DAIMALB AND DAIMAID = :DAIMAID", daiMaLB,daiMaID);
            var list = new QueryService(UnitOfWork).Get<E_GY_DAIMA>(dtoDaiMa);
            return ServiceContent(list);
        }
        /// <summary>
        /// 获取所有代码类别
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<List<E_GY_DAIMALB>> GETDaiMaLB()
        {
            E_GY_DAIMALB dtoDaiMaLB = new E_GY_DAIMALB();
            var list = new QueryService(UnitOfWork).Get<E_GY_DAIMALB>(dtoDaiMaLB);
            return ServiceContent(list);
        }

        /// <summary>
        ///  通过给药方式ID获取给药方式对应的附加收费
        /// </summary>
        /// <param name="geiYaoFSID">给药方式ID</param>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<List<E_GY_GEIYAOFSJFXM>> GETGeiYaoFSJFXM(string geiYaoFSID)
        {
            E_GY_GEIYAOFSJFXM dtoGeiYaoFSJFXM = new E_GY_GEIYAOFSJFXM();
            dtoGeiYaoFSJFXM.Where("WHERE GEIYAOFSID = :GEIYAOFSID", geiYaoFSID);
            var list = new  QueryService(UnitOfWork).Get<E_GY_GEIYAOFSJFXM>(dtoGeiYaoFSJFXM);
            return ServiceContent(list);
        }

        /// <summary>
        ///  通过给药方式ID获取给药方式对应
        /// </summary>

        /// <returns></returns>
        [HttpPost]
        public ServiceResult<List<E_GY_GEIYAOFSDZ>> GETGeiYaoFSDZ(string geiYaoFSID)
        {
            E_GY_GEIYAOFSDZ dtoGeiYaoFSJDZ = new E_GY_GEIYAOFSDZ();
            dtoGeiYaoFSJDZ.Where("WHERE GEIYAOFSID = :GEIYAOFSID", geiYaoFSID);
            var list = new  QueryService(UnitOfWork).Get<E_GY_GEIYAOFSDZ>(dtoGeiYaoFSJDZ);
            return ServiceContent(list);
        }


        /// <summary>
        /// 通过给药方式ID获取给药方式对应
        /// </summary>
        ///    <param name="jiXingID">剂型ID</param>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<List<E_GY_JIXINGDZ>> GETJiXingDz(string jiXingID)
        {
            E_GY_JIXINGDZ dtoJingXingDZ = new E_GY_JIXINGDZ();
            dtoJingXingDZ.Where("WHERE JIXINGID = :JIXINGID", jiXingID);
            var list = new  QueryService(UnitOfWork).Get<E_GY_JIXINGDZ>(dtoJingXingDZ);
            return  ServiceContent(list);
        }

        /// <summary>
        ///获取挂号诊疗收费项目
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<List<E_GY_V_SHOUFEIXM_GUAHAO>> GetGuaHaoZLSFXM()
        {
            E_GY_V_SHOUFEIXM_GUAHAO dtoShouFeiXM = new E_GY_V_SHOUFEIXM_GUAHAO();
            var list = new QueryService(UnitOfWork).Get<E_GY_V_SHOUFEIXM_GUAHAO>(dtoShouFeiXM);
            return ServiceContent(list);
        }

        /// <summary>
        /// 查询医院信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<List<E_GY_YUANQU>> GetYuanQu()
        {
            E_GY_YUANQU entry = new E_GY_YUANQU();
            entry.Where(" Where ZUOFEIBZ = :ZUOFEIBZ order by YUANQUID", 0);
            var list = new  QueryService(UnitOfWork).Get<E_GY_YUANQU>(entry);
            return ServiceContent(list);
        }
        /// <summary>
        /// 查询优惠类别
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<List<E_GY_YOUHUILB>> GetYouHuiLB()
        {
            E_GY_YOUHUILB entry = new E_GY_YOUHUILB();
            entry.Where(" Where ZUOFEIBZ = :ZUOFEIBZ ORDER BY SHUNXUHAO", 0);
            var list = new  QueryService(UnitOfWork).Get<E_GY_YOUHUILB>(entry);
            return  ServiceContent(list);
        }

        /// <summary>
        /// 查询费用性质
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<List<E_GY_FEIYONGXZ>> GetFeiYongXZ()
        {
            E_GY_FEIYONGXZ entry = new E_GY_FEIYONGXZ();
            entry.Where(" Where ZUOFEIBZ = :ZUOFEIBZ ORDER BY SHUNXUHAO", 0);
            var list = new QueryService(UnitOfWork).Get<E_GY_FEIYONGXZ>(entry);
            return ServiceContent(list);
        }

        [HttpPost]
        public ServiceResult<DateTime?> GetSysTime()
        {
            var daiMaRep = this.GetRepository<IGY_DAIMARepository>(UnitOfWork);
            DateTime? date = daiMaRep.GetSYSTime();
            return ServiceContent(date)  ;
        }
        #endregion

        /// <summary>
        /// 保存代码类别信息
        /// </summary>
        /// <param name="eDaiMaLB">代码类别DTO</param>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<bool> BaoCunDaiMaLBXX(List<E_GY_DAIMALB> eDaiMaLB)
        {

            //using (var trans = DBContext.Database.BeginTransaction())
            //{
            UnitOfWork.BeginTransaction();
            var daiMaLBRep = this.GetRepository<IGY_DAIMALBRepository>(UnitOfWork);

            //新增代码类别
            if (eDaiMaLB.GetNews().Count > 0)
            {
                eDaiMaLB.GetNews().ForEach(o =>
                {
                    var daiMaLB = GY_DAIMALBFactory.Create(daiMaLBRep, ServiceContext, o);
                });
            }
            //更新代码类别
            if (eDaiMaLB.GetUpdates().Count > 0)
            {
                eDaiMaLB.GetUpdates().ForEach(o =>
                {
                    var daiMaLB = daiMaLBRep.GetByKey(o.LEIBIEID);
                    daiMaLB.UpdateDaiMaLB(o);
                });
            }
            //删除代码类别
            if (eDaiMaLB.GetDeletes().Count > 0)
            {
                eDaiMaLB.GetDeletes().ForEach(o =>
                {
                    var daiMaLB = daiMaLBRep.GetByKey(o.LEIBIEID);
                    daiMaLBRep.RegisterDelete(daiMaLB);
                });
            }
            UnitOfWork.SaveChanges();
            //提交
            UnitOfWork.Commit();
            // }
            return ServiceContent(true);
        }
        /// <summary>
        /// 保存代码信息
        /// </summary>
        /// <param name="eDaiMa">代码DTO</param>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<bool> BaoCunDaiMaXX(List<E_GY_DAIMA> eDaiMa)
        {

            UnitOfWork.BeginTransaction();

            var daiMaRep = this.GetRepository<IGY_DAIMARepository>(UnitOfWork);
            //新增代码类别
            if (eDaiMa.GetNews().Count > 0)
            {
                eDaiMa.GetNews().ForEach(o =>
                {
                    var daiMa = GY_DAIMAFactory.Create(daiMaRep, ServiceContext, o);
                });
            }
            //更新代码类别
            if (eDaiMa.GetUpdates().Count > 0)
            {
                eDaiMa.GetUpdates().ForEach(o =>
                {
                    var daiMa = daiMaRep.GetByKey(o.DAIMAID,o.DAIMALB);
                    daiMa.Update(o);
                });
            }
            //删除代码类别
            if (eDaiMa.GetDeletes().Count > 0)
            {
                eDaiMa.GetDeletes().ForEach(o =>
                {
                    var daiMa = daiMaRep.GetByKey(o.DAIMAID, o.DAIMALB);
                    daiMaRep.RegisterDelete(daiMa);
                });
            }
            UnitOfWork.SaveChanges();
            //提交
            UnitOfWork.Commit();
            
            return ServiceContent(true);
        }

        #region 0020 剂型对应给药方式维护保存
        /// <summary>
        /// 保存剂型对应的给药方式
        /// </summary>
        /// <param name="eJiXingDZ"></param>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<bool> BaoCunJiXingDZ(List<E_GY_JIXINGDZ> eJiXingDZ)
        {
            UnitOfWork.BeginTransaction();
            var jiXingDZRep = this.GetRepository<IGY_JIXINGDZRepository>(UnitOfWork);
            if (eJiXingDZ != null && eJiXingDZ.Count > 0)
            {
                eJiXingDZ.GetNews().ForEach(o =>
                {
                    if (!string.IsNullOrEmpty(o.GEIYAOFSID))
                    {
                        var jiXingDZ = GY_JIXINGDZFactory.Create(jiXingDZRep, ServiceContext, o);
                    }
                });

                eJiXingDZ.GetUpdates().ForEach(o =>
                {
                    var jiXingDZ = jiXingDZRep.GetByKey(o.JIXINGDZID);
                    jiXingDZ.Update(o);
                });
                eJiXingDZ.GetDeletes().ForEach(o =>
                {
                    var jiXingDZ = jiXingDZRep.GetByKey(o.JIXINGDZID);
                    jiXingDZRep.RegisterDelete(jiXingDZ);
                });
            }
            UnitOfWork.SaveChanges();
            UnitOfWork.Commit();
            
            return  ServiceContent(true);
        }
        #endregion


        /// <summary>
        /// 公用控件取代码数据
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<List<E_GY_DAIMA>> GetGYList(string DAIMALB, int? MENZHENSY, int? ZHUYUANSY, int ZUOFEIBZ)
        {
            E_GY_DAIMA daiMa = new E_GY_DAIMA();
            daiMa.Where(" WHERE DAIMALB =:DAIMALB AND  ZUOFEIBZ=:ZUOFEIBZ", DAIMALB, ZUOFEIBZ);
            if (MENZHENSY != null)
            {
                daiMa.WhereAppend(" AND MENZHENSY=:MENZHENSY", MENZHENSY);
            }
            if (ZHUYUANSY != null)
            {
                daiMa.WhereAppend(" AND ZHUYUANSY=:ZHUYUANSY ", ZHUYUANSY);
            }
            var list = new QueryService(UnitOfWork).Get<E_GY_DAIMA>(daiMa);
            return ServiceContent(list);
        }
        /// <summary>
        /// 根据代码类别、作废标志、末级标志取
        /// </summary>
        /// <param name="DAIMALB"></param>
        /// <param name="ZUOFEIBZ"></param>
        /// <param name="MOJIBZ"></param>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<List<E_GY_DAIMA>> GetDaiMaList(string DAIMALB, int ZUOFEIBZ, int MOJIBZ)
        {
            E_GY_DAIMA daiMa = new E_GY_DAIMA();
            daiMa.Where(string.Format("WHERE DAIMALB='{0}'", DAIMALB));
            daiMa.WhereAppend(string.Format(" AND ZUOFEIBZ={0}", ZUOFEIBZ));
            daiMa.WhereAppend(string.Format(" AND MOJIBZ={0}", MOJIBZ));
            daiMa.WhereAppend(" order by  ShunXuHao");
            var list = new QueryService(UnitOfWork).Get<E_GY_DAIMA>(daiMa);
            return ServiceContent(list);
        }

        /// <summary>
        /// 通过代码类别取代码 
        /// </summary>
        /// <param name="daiMaLB">代码类别</param>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<List<E_GY_V_JIANCHAXMSF>> GETJianChaXMSFDMByID(string daiMaID)
        {
            E_GY_V_JIANCHAXMSF dtoDaiMa = new E_GY_V_JIANCHAXMSF();
            dtoDaiMa.Where("WHERE SHOUFEIDYBW = :SHOUFEIDYBW OR JIAOPIANFDYBW=:JIAOPIANFDYBW", daiMaID, daiMaID);
            var list = new QueryService(UnitOfWork).Get<E_GY_V_JIANCHAXMSF>(dtoDaiMa);
            return ServiceContent(list);
        }
        /// <summary>
        /// 保存代码信息
        /// </summary>
        /// <param name="eDaiMa">代码DTO</param>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<bool> SaveDaiMaJCBW(E_GY_DAIMA eDaiMa)
        {

            UnitOfWork.BeginTransaction();

            var daiMaRep = this.GetRepository<IGY_DAIMARepository>(UnitOfWork);
            //新增代码类别
            if (eDaiMa.GetState() == DTOState.New)
            {
                var daiMa = GY_DAIMAFactory.Create(daiMaRep, ServiceContext, eDaiMa);
            }
            //更新代码类别
            if (eDaiMa.GetState() == DTOState.Update)
            {
                var daiMa = daiMaRep.GetByKey(eDaiMa.DAIMAID, eDaiMa.DAIMALB);
                daiMa.Update(eDaiMa);
            }
            UnitOfWork.SaveChanges();
            //提交
            UnitOfWork.Commit();

            return ServiceContent(true);
        }
    }
}
