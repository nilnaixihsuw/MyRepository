using Mediinfo.Domain.JCJG.GY;
using Mediinfo.DTO.Core;
using Mediinfo.DTO.HIS.GY;
using Mediinfo.DTO.HIS.XT;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core;
using Mediinfo.Service.JCJG.GongYong.Route;

using System.Collections.Generic;
using System.Web.Http;

namespace Mediinfo.Service.JCJG.GongYong
{
    [ServiceRoutePrefix]
    [Route("JCJGYingYongCD/{action}")]
    public class JCJGYingYongCDController : Mediinfo.Infrastructure.JCJG.Controller.HISController
    {
        #region 查询
        /// <summary>
        /// 获取系统
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [HttpGet]
        public ServiceResult<List<E_XT_DINGYI>> GetXiTong()
        {
            E_XT_DINGYI entry = new E_XT_DINGYI();
            entry.Where(" WHERE QIYONGBZ = :QIYONGBZ", 1);
            var list = new QueryService(UnitOfWork).Get<E_XT_DINGYI>(entry);
            return ServiceContent(list);
        }
        /// <summary>
        /// 取应用菜单
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [HttpGet]
        public ServiceResult<List<E_GY_CAIDAN_NEW>> GetYingYongCD()
        {
            E_GY_CAIDAN_NEW entry = new E_GY_CAIDAN_NEW();
            //entry.Where(@" WHERE  ZUOFEIBZ = 0 AND  (YINGYONGID = :YINGYONGID or yingyongid = '1201') ", ServiceContext.YINGYONGID);
            entry.Where(@" WHERE  ZUOFEIBZ = 0 AND  YINGYONGID = :YINGYONGID ", ServiceContext.YINGYONGID);
            entry.WhereAppend(" ORDER BY CAIDANID");
            var list = new QueryService(UnitOfWork).Get<E_GY_CAIDAN_NEW>(entry);
            return ServiceContent(list);
        }


        /// <summary>
        /// 取应用菜单
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [HttpGet]
        public ServiceResult<List<E_GY_CAIDAN_NEW>> GetGongYongCD(string YINGYONGID)
        {
            Check.NotEmpty(YINGYONGID, "应用ID不能为空!");
            E_GY_CAIDAN_NEW entry = new E_GY_CAIDAN_NEW();
            //entry.Where(@" WHERE  ZUOFEIBZ = 0 AND  (YINGYONGID = :YINGYONGID or yingyongid = '1201') ", ServiceContext.YINGYONGID);
            entry.Where(@" WHERE  ZUOFEIBZ = 0 AND  YINGYONGID = :YINGYONGID ", YINGYONGID);
            entry.WhereAppend(" ORDER BY CAIDANID");
            var list = new QueryService(UnitOfWork).Get<E_GY_CAIDAN_NEW>(entry);
            return ServiceContent(list);
        }
        [HttpPost]
        [HttpGet]
        public ServiceResult<List<E_GY_YINGYONGCD>> GetyYongHuYYCD()
        {
            E_GY_YINGYONGCD entry = new E_GY_YINGYONGCD();
            entry.Where(@" WHERE YONGHUID = :YONGHUID", ServiceContext.USERID);
            entry.WhereAppend(@" AND YINGYONGID = :YINGYONGID", ServiceContext.YINGYONGID);                     
            var list = new QueryService(UnitOfWork).Get<E_GY_YINGYONGCD>(entry);

            return ServiceContent(list);
        }


        /// <summary>
        /// 获取系统功能NEW By 系统ID
        /// </summary>
        /// <param name="xitongid"></param>
        /// <returns></returns>
        [HttpPost]
        [HttpGet]
        public ServiceResult<List<E_XT_GONGNENG_NEW>> GetXiTongGNNewByXTID(string xitongid)
        {
            E_XT_GONGNENG_NEW entry = new E_XT_GONGNENG_NEW();
            entry.Where(" WHERE XITONGID = :XITONGID", xitongid);
            var list = new QueryService(UnitOfWork).Get<E_XT_GONGNENG_NEW>(entry);

            return ServiceContent(list);
        }
        [HttpPost]
        [HttpGet]
        public ServiceResult<List<E_XT_GONGNENG_NEW>> GetXiTongGNNewByGNID(string gongnengid)
        {
            E_XT_GONGNENG_NEW entry = new E_XT_GONGNENG_NEW();
            entry.Where(" WHERE GONGNENGID = :GONGNENGID", gongnengid);
            var list = new QueryService(UnitOfWork).Get<E_XT_GONGNENG_NEW>(entry);

            return ServiceContent(list);
        }
        /// <summary>
        /// 获取应用菜单NEW By 应用ID
        /// </summary>
        /// <param name="yingyongid"></param>
        /// <returns></returns>
        [HttpPost]
        [HttpGet]
        public ServiceResult<List<E_GY_CAIDAN_NEW>> GetYingYongCDNewByYYID(string yingyongid)
        {
            E_GY_CAIDAN_NEW entry = new E_GY_CAIDAN_NEW();
            entry.Where(" WHERE YINGYONGID = :YINGYONGID ", yingyongid);
            var list = new QueryService(UnitOfWork).Get<E_GY_CAIDAN_NEW>(entry);
            return ServiceContent(list);
        }
        /// <summary>
        /// 获取应用菜单NEW By 功能ID
        /// </summary>
        /// <param name="gongnengid"></param>
        /// <returns></returns>
        [HttpPost]
        [HttpGet]
        public ServiceResult<List<E_GY_CAIDAN_NEW>> GetYingYongCDNewByGNID(string gongnengid)
        {
            E_GY_CAIDAN_NEW entry = new E_GY_CAIDAN_NEW();
            entry.Where(" WHERE GONGNENGID = :GONGNENGID ", gongnengid);
            var list = new QueryService(UnitOfWork).Get<E_GY_CAIDAN_NEW>(entry);
            return ServiceContent(list);
        }
        /// <summary>
        /// 获取应用工具栏 By 应用ID
        /// </summary>
        /// <param name="yingyongid"></param>
        /// <returns></returns>
        [HttpPost]
        [HttpGet]
        public ServiceResult<List<E_GY_CAIDANGJL>> GetYingYongGJLByYYID(string yingyongid)
        {
            E_GY_CAIDANGJL entry = new E_GY_CAIDANGJL();
            entry.Where(" WHERE YINGYONGID = :YINGYONGID ", yingyongid);
            var list = new QueryService(UnitOfWork).Get<E_GY_CAIDANGJL>(entry);
            return ServiceContent(list);
        }
        /// <summary>
        /// 获取应用工具栏 By 应用ID NEW
        /// </summary>
        /// <param name="yingyongid"></param>
        /// <returns></returns>
        [HttpPost]
        [HttpGet]
        public ServiceResult<List<E_GY_CAIDANGJL_NEW>> GetYingYongGJLNewByYYID(string yingyongid)
        {
            E_GY_CAIDANGJL_NEW entry = new E_GY_CAIDANGJL_NEW();
            entry.Where(" WHERE YINGYONGID = :YINGYONGID ", yingyongid);
            var list = new QueryService(UnitOfWork).Get<E_GY_CAIDANGJL_NEW>(entry);
            return  ServiceContent(list);
        }

        /// <summary>
        /// 取用户权限
        /// </summary>
        /// <param name="yonghuid"></param>
        /// <returns></returns>
        [HttpPost]
        [HttpGet]
        public ServiceResult<List<E_GY_YONGHUQX>> GetYongHuQX(string yonghuid)
        {
            E_GY_YONGHUQX entry = new E_GY_YONGHUQX();

            if (!string.IsNullOrWhiteSpace(yonghuid))
            {
                entry.Where(" WHERE YONGHUID = :YONGHUID and qiyongbz = :qiyongbz", yonghuid,1);
            }

            var list = new QueryService(UnitOfWork).Get<E_GY_YONGHUQX>(entry);
            return  ServiceContent(list);
        }
        #endregion



        /// <summary>
        /// 取用户权窗口限
        /// </summary>
        /// <param name="yonghuid"></param>
        /// <returns></returns>
        [HttpPost]
        [HttpGet]
        public ServiceResult<List<E_GY_JUESECKQX>> GetYongHuJSCKQX(string yonghuid)
        {
            E_GY_JUESECKQX entry = new E_GY_JUESECKQX();

            if (!string.IsNullOrWhiteSpace(yonghuid))
            {
                entry.Where(" WHERE YONGHUID = :YONGHUID and QUANXIANKZ = :QUANXIANKZ and XIANSHIKZ = :XIANSHIKZ", yonghuid, 1,1);
            }

            var list = new QueryService(UnitOfWork).Get<E_GY_JUESECKQX>(entry);
            return ServiceContent(list);
        }

        /// <summary>
        /// 取用户角色窗口权限
        /// </summary>
        /// <param name="yonghuid"></param>
        /// <returns></returns>
        [HttpPost]
        [HttpGet]
        public ServiceResult<List<E_GY_JUESECKQX_NEW>> GetYongHuJSCKQX_NEW(string yingyongid)
        {
            E_GY_JUESECKQX_NEW entry = new E_GY_JUESECKQX_NEW();

            if (!string.IsNullOrWhiteSpace(yingyongid))
            {
                entry.Where(" WHERE YINGYONGID = :YINGYONGID and QUANXIANKZ = :QUANXIANKZ and XIANSHIKZ = :XIANSHIKZ", yingyongid, 1, 1);
            }

            var list = new QueryService(UnitOfWork).Get<E_GY_JUESECKQX_NEW>(entry);
            return ServiceContent(list);
        }

        /// <summary>
        /// 取用户表(新加zhoulele)
        /// </summary>
        /// <param name="yonghuid"></param>
        /// <returns></returns>
        [HttpPost]
        [HttpGet]
        public ServiceResult<List<E_GY_YONGHUQX2>> GetYongHuQX2(string yonghuid)
        {
            E_GY_YONGHUQX2 entry = new E_GY_YONGHUQX2();

            if (!string.IsNullOrWhiteSpace(yonghuid))
            {
                entry.Where(" WHERE YONGHUID = :YONGHUID ", yonghuid);
            }

            var list = new QueryService(UnitOfWork).Get<E_GY_YONGHUQX2>(entry);
            return ServiceContent(list);
        }

        //取二级联合用户权限表
        [HttpPost]
        [HttpGet]
        public ServiceResult<List<E_GY_ERJIYHQX>> GetSencondLevelRoot(string yonghuid)
        {
            E_GY_ERJIYHQX entry = new E_GY_ERJIYHQX();

            if (!string.IsNullOrWhiteSpace(yonghuid))
            {
                entry.Where(" WHERE YONGHUID = :YONGHUID", yonghuid);
            }

            var list = new QueryService(UnitOfWork).Get<E_GY_ERJIYHQX>(entry);
            return ServiceContent(list);
        }









        /// <summary>
        /// 更新二级权限用户信息表
        /// </summary>
        /// <param name="eYingYongCDList"></param>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<bool> BaoCunErJiQX(List<E_GY_ERJIYHQX> erjiquanxianList)
        {
            UnitOfWork.BeginTransaction();
            var erjiquanxianRep = this.GetRepository<IGY_QUANXIAN2_NEWRepository>(UnitOfWork);
            ///1先保存用户信息
            if (erjiquanxianList.GetNews().Count > 0)
            {
                erjiquanxianList.GetNews().ForEach(o =>
                {
                    var xiTongGN = GY_QUANXIAN2_NEWFactory.Create(erjiquanxianRep, ServiceContext, o);
                });
            }
            //修改
            if (erjiquanxianList.GetUpdates().Count > 0)
            {
                erjiquanxianList.GetUpdates().ForEach(o =>
                {
                    var erjiquanxian = erjiquanxianRep.GetByKey(o.QUANXIANID);
                    erjiquanxian.Update(o);

                });
            }
            ////删除应用菜单
            if (erjiquanxianList.GetDeletes().Count > 0)
            {
                erjiquanxianList.GetDeletes().ForEach(o =>
                {
                    var erjiquanxian = erjiquanxianRep.GetByKey(o.QUANXIANID);
                    erjiquanxian.Delete();

                });
            }

            UnitOfWork.SaveChanges();
            //提交
            UnitOfWork.Commit();
            return ServiceContent(true);
        }


        // #region 业务
        //[HttpPost]
        //[HttpGet]
        //public ServiceResult<bool> DeleteYingYongCDNew(E_GY_CAIDAN_NEW eYingYongCD)
        //{

        //    HISUnitOfWork.BeginTransaction();

        //    ////删除应用菜单
        //    if(eYingYongCD.GetState() == DTOState.Delete)
        //    {
        //        //var caidan = GYCaiDanNewDomain.GetByID(DBContext, ServiceContext, eYingYongCD.CAIDANROWID);
        //        //caidan.Delete();

        //    }
        //    HISUnitOfWork.SaveChanges();
        //    //提交
        //    HISUnitOfWork.Commit();

        //    return new Result<bool>(ReturnCode.SUCCESS, "删除应用菜单成功！");
        //}

        //public Result<bool> DeleteYingYongCDNew(List<E_GY_CAIDAN_NEW> eYingYongCD)
        //{

        //    using (var trans = DBContext.Database.BeginTransaction())
        //    {
        //        ////删除应用菜单
        //        if (eYingYongCD.GetDeletes().Count > 0)
        //        {
        //            eYingYongCD.GetDeletes().ForEach(o =>
        //            {
        //                var caidan = GYCaiDanNewDomain.GetByID(DBContext, ServiceContext, o.CAIDANROWID);
        //                caidan.Delete();
        //            });
        //        }
        //        DBContext.SaveChanges();
        //        //提交
        //        trans.Commit();
        //    }
        //    return new Result<bool>(ReturnCode.SUCCESS, "删除应用菜单成功！");
        //}
        //public Result<bool> SaveYingYongCDNew(E_GY_CAIDAN_NEW eYingYongCD)
        //{

        //    using (var trans = DBContext.Database.BeginTransaction())
        //    {
        //        //新增
        //        if (eYingYongCD.State == DTOState.New)
        //        {
        //            var caidan = GYCaiDanNewDomain.Create(DBContext, ServiceContext, eYingYongCD);

        //        }
        //        //修改
        //        if (eYingYongCD.State == DTOState.Update)
        //        {
        //            var caidan = GYCaiDanNewDomain.GetByID(DBContext, ServiceContext, eYingYongCD.CAIDANROWID);
        //            caidan.Update(eYingYongCD);

        //        }
        //        //删除
        //        if (eYingYongCD.State == DTOState.Cancel)
        //        {
        //            var caidan = GYCaiDanNewDomain.GetByID(DBContext, ServiceContext, eYingYongCD.CAIDANROWID);
        //            caidan.Delete();

        //        }
        //        DBContext.SaveChanges();
        //        //提交
        //        trans.Commit();
        //    }
        //    return new Result<bool>(ReturnCode.SUCCESS, "保存应用菜单成功！");
        //}
        //public Result<bool> SaveYingYongCDNew(List<E_GY_CAIDAN_NEW> eYingYongCD)
        //{

        //    using (var trans = DBContext.Database.BeginTransaction())
        //    {
        //        ///1先保存应用菜单
        //        if (eYingYongCD.GetNews().Count > 0)
        //        {
        //            eYingYongCD.GetNews().ForEach(o =>
        //            {
        //                var caidan = GYCaiDanNewDomain.Create(DBContext, ServiceContext, o);

        //            });
        //        }
        //        //修改
        //        if (eYingYongCD.GetUpdates().Count > 0)
        //        {
        //            eYingYongCD.GetUpdates().ForEach(o =>
        //            {
        //                var caidan = GYCaiDanNewDomain.GetByID(DBContext, ServiceContext, o.CAIDANROWID);
        //                caidan.Update(o);

        //            });
        //        }
        //        ////删除应用菜单
        //        if (eYingYongCD.GetDeletes().Count > 0)
        //        {
        //            eYingYongCD.GetDeletes().ForEach(o =>
        //            {
        //                var caidan = GYCaiDanNewDomain.GetByID(DBContext, ServiceContext, o.CAIDANROWID);
        //                caidan.Delete();

        //            });
        //        }
        //        DBContext.SaveChanges();
        //        //提交
        //        trans.Commit();
        //    }
        //    return new Result<bool>(ReturnCode.SUCCESS, "保存应用菜单成功！");
        //}


        ///// <summary>
        ///// 保存应用工具栏 NEW
        ///// </summary>
        ///// <param name="eYingYongGJL"></param>
        ///// <returns></returns>
        //public Result<bool> SaveYingYongGJLNew(List<E_GY_CAIDANGJL_NEW> eYingYongGJLNew)
        //{

        //    using (var trans = DBContext.Database.BeginTransaction())
        //    {
        //        ///1先保存应用菜单
        //        if (eYingYongGJLNew.GetNews().Count > 0)
        //        {
        //            eYingYongGJLNew.GetNews().ForEach(o =>
        //            {
        //                var caidangjl = GYCaiDanGJLNewDomain.Create(DBContext, ServiceContext, o);

        //            });
        //        }
        //        //修改
        //        if (eYingYongGJLNew.GetUpdates().Count > 0)
        //        {
        //            eYingYongGJLNew.GetUpdates().ForEach(o =>
        //            {
        //                var caidangjl = GYCaiDanGJLNewDomain.GetByID(DBContext, ServiceContext, o);
        //                caidangjl.Update(o);

        //            });
        //        }
        //        ////删除应用菜单
        //        if (eYingYongGJLNew.GetDeletes().Count > 0)
        //        {
        //            eYingYongGJLNew.GetDeletes().ForEach(o =>
        //            {
        //                var caidangjl = GYCaiDanGJLNewDomain.GetByID(DBContext, ServiceContext, o);
        //                caidangjl.Delete();

        //            });
        //        }
        //        DBContext.SaveChanges();
        //        //提交
        //        trans.Commit();
        //    }
        //    return new Result<bool>(ReturnCode.SUCCESS, "保存应用工具栏成功！");
        //}

        ///// <summary>
        ///// 保存功能
        ///// </summary>
        ///// <param name="eGongNengNew"></param>
        ///// <returns></returns>
        //public Result<bool> SaveXiTongGNNew(E_XT_GONGNENG_NEW eGongNengNew)
        //{
        //    using (var trans = DBContext.Database.BeginTransaction())
        //    {
        //        if (eGongNengNew.State == DTOState.New)
        //        {
        //            //新增功能
        //            string xitongid = eGongNengNew.XITONGID;
        //            string gongnengmc = eGongNengNew.GONGNENGMC;
        //            //获取功能id
        //            //var gongnengDomain = XTGongNengDomain.Create(DBContext, ServiceContext, eGongNengNew.SHANGJIGNID, xitongid);
        //            var gongnengid = XTGongNengDomain.GetGongNengID(DBContext, ServiceContext, eGongNengNew.SHANGJIGNID, xitongid);


        //            eGongNengNew.GONGNENGID = gongnengid;
        //            if (eGongNengNew.DIAOYONGCS.ToStringEx().IsNullOrWhiteSpace() )
        //            {
        //                eGongNengNew.DIAOYONGCS = eGongNengNew.RUKOUCK + "|" + gongnengid + "|" + eGongNengNew.DIAOYONGCS;
        //            }

        //            var gongneng = XTGongNengDomain.Create(DBContext, ServiceContext, eGongNengNew);


        //             //新增权限
        //             E_GY_QUANXIAN equanxian = new E_GY_QUANXIAN();
        //            string quanxianid = "Open[" + gongnengid + "]";
        //            string quanxianmc = "打开[" + gongnengmc + "]";
        //            equanxian.QUANXIANID = quanxianid;
        //            equanxian.QUANXIANMC = quanxianmc;
        //            equanxian.GONGNENGID = gongnengid;
        //            equanxian.QUANXIANJB = 1;
        //            equanxian.XIUGAIREN = ServiceContext.USERID;
        //            equanxian.XIUGAISJ = new QueryService(DBContext).GetSYSDate();
        //            equanxian.State = DTOState.New;

        //            var quanxian = GYQuanXianDomain.Create(DBContext, ServiceContext, equanxian);
        //            //quanxian.Insert();


        //            //角色权限
        //            E_GY_JUESEQX ejueseqx = new E_GY_JUESEQX();
        //            ejueseqx.JUESEID = "0";
        //            ejueseqx.QUANXIANID = quanxianid;
        //            ejueseqx.XIUGAIREN = ServiceContext.USERID;
        //            ejueseqx.XIUGAISJ = new QueryService(DBContext).GetSYSDate();
        //            //先删除
        //            var jueseqx = GYJueSeQXDomain.GetByID(DBContext, ServiceContext, ejueseqx);
        //            jueseqx.Delete();

        //            //再增加
        //            ejueseqx.State = DTOState.New;
        //            var jueseqx2 = GYJueSeQXDomain.Create(DBContext, ServiceContext, ejueseqx);
        //            //jueseqx2.Insert();

        //        }
        //        if (eGongNengNew.State == DTOState.Update)
        //        {
        //            var gongneng = XTGongNengDomain.GetByID(DBContext, ServiceContext, eGongNengNew.GONGNENGROWID);
        //            gongneng.Update(eGongNengNew);
        //            gongneng.DBContext.SaveChanges();
        //            //在判断是否存在权限如果不存在则插入
        //            string gongnengid = eGongNengNew.GONGNENGID;
        //            string gongnengmc = eGongNengNew.GONGNENGMC;
        //            string quanxianid = "Open[" + gongnengid + "]";
        //            string quanxianmc = "打开[" + gongnengmc + "]";
        //            var quanxianList = ServiceFactory.Create<GYZhiGongService>().GetQuanXian(quanxianid).Return;
        //            if (quanxianList == null || quanxianList.Count ==0)
        //            {
        //                //新增权限
        //                E_GY_QUANXIAN equanxian = new E_GY_QUANXIAN();               
        //                equanxian.QUANXIANID = quanxianid;
        //                equanxian.QUANXIANMC = quanxianmc;
        //                equanxian.GONGNENGID = gongnengid;
        //                equanxian.QUANXIANJB = 1;
        //                equanxian.XIUGAIREN = ServiceContext.USERID;
        //                equanxian.XIUGAISJ = new QueryService(DBContext).GetSYSDate();
        //                equanxian.State = DTOState.New;
        //                var quanxian = GYQuanXianDomain.Create(DBContext, ServiceContext, equanxian);
        //                //quanxian.Insert();

        //                //角色权限
        //                E_GY_JUESEQX ejueseqx = new E_GY_JUESEQX();
        //                ejueseqx.JUESEID = "0";
        //                ejueseqx.QUANXIANID = quanxianid;
        //                ejueseqx.XIUGAIREN = ServiceContext.USERID;
        //                ejueseqx.XIUGAISJ = new QueryService(DBContext).GetSYSDate();
        //                //先删除
        //                var jueseqx = GYJueSeQXDomain.GetByID(DBContext, ServiceContext, ejueseqx);
        //                jueseqx.Delete();

        //                //再增加
        //                ejueseqx.State = DTOState.New;
        //                var jueseqx2 = GYJueSeQXDomain.Create(DBContext, ServiceContext, ejueseqx);
        //                //jueseqx2.Insert();

        //            }

        //        }
        //        if (eGongNengNew.State == DTOState.Delete)
        //        {
        //            //先删除
        //            var gongneng = XTGongNengDomain.GetByID(DBContext, ServiceContext, eGongNengNew.GONGNENGROWID);
        //            gongneng.Delete();

        //            //在删除权限
        //            E_GY_QUANXIAN equanxian = new E_GY_QUANXIAN();
        //            string gongnengid = eGongNengNew.GONGNENGID;
        //            equanxian.GONGNENGID = gongnengid;
        //            equanxian.QUANXIANID = "Open[" + gongnengid + "]";
        //            equanxian.State = DTOState.Delete;
        //            var quanxian = GYQuanXianDomain.GetByID(DBContext, ServiceContext, equanxian.QUANXIANID);
        //            quanxian.Delete();

        //        }
        //        DBContext.SaveChanges();
        //        //提交
        //        trans.Commit();
        //    }
        //    return new Result<bool>(ReturnCode.SUCCESS, "保存系统功能成功！");
        //}

        ///// <summary>
        ///// 保存功能 权限 角色权限
        ///// </summary>
        ///// <param name="eGongNengNew"></param>
        ///// <param name="eQuanXian"></param>
        ///// <param name="eJueSeQX"></param>
        ///// <returns></returns>
        //public Result<bool> SaveXiTongGNNew(E_XT_GONGNENG_NEW eGongNengNew, E_GY_QUANXIAN eQuanXian, E_GY_JUESEQX eJueSeQX)
        //{
        //    using (var trans = DBContext.Database.BeginTransaction())
        //    {
        //        if (eGongNengNew.State == DTOState.New)
        //        {
        //            //新增功能
        //            string xitongid = eGongNengNew.XITONGID;
        //            string gongnengmc = eGongNengNew.GONGNENGMC;
        //            //获取功能id
        //            //var gongnengDomain = XTGongNengDomain.Create(DBContext, ServiceContext, eGongNengNew.SHANGJIGNID, xitongid);
        //            //var gongnengid = gongnengDomain.GetGongNengID();
        //            var gongnengid = XTGongNengDomain.GetGongNengID(DBContext, ServiceContext, eGongNengNew.SHANGJIGNID, xitongid);

        //            eGongNengNew.GONGNENGID = gongnengid;
        //            //if (eGongNengNew.SHANGJIGNID != "-" && eGongNengNew.DIAOYONGCS.ToStringEx().IsNullOrWhiteSpace())
        //            //{
        //            //    eGongNengNew.DIAOYONGCS = eGongNengNew.RUKOUCK + "|" + gongnengid + "|" + eGongNengNew.DIAOYONGCS + "|" + eGongNengNew.DAKAIFS;
        //            //}                                

        //            var gongneng = XTGongNengDomain.Create(DBContext, ServiceContext, eGongNengNew);
        //            //gongneng.Insert();

        //            //新增权限       
        //            string quanxianid = "Open[" + gongnengid + "]";
        //            eQuanXian.QUANXIANID = quanxianid;
        //            eQuanXian.GONGNENGID = gongnengid;
        //            var quanxian = GYQuanXianDomain.Create(DBContext, ServiceContext, eQuanXian);
        //            //quanxian.Insert();

        //            //角色权限                   
        //            //先删除
        //            eJueSeQX.QUANXIANID = quanxianid;
        //            try
        //            {
        //                var jueseqx = GYJueSeQXDomain.GetByID(DBContext, ServiceContext, eJueSeQX);
        //                jueseqx.Delete();
        //            }
        //            catch (Exception)
        //            {
        //                //说明没有这个角色权限   
        //                //再增加
        //                eJueSeQX.State = DTOState.New;
        //                var jueseqx2 = GYJueSeQXDomain.Create(DBContext, ServiceContext, eJueSeQX);
        //                //jueseqx2.Insert();
        //            }
        //        }
        //        if (eGongNengNew.State == DTOState.Update)
        //        {
        //            var gongneng = XTGongNengDomain.GetByID(DBContext, ServiceContext, eGongNengNew.GONGNENGROWID);
        //            gongneng.Update(eGongNengNew);

        //            //权限
        //            if (eQuanXian != null && eQuanXian.State == DTOState.New)
        //            {
        //                var quanxian = GYQuanXianDomain.Create(DBContext, ServiceContext, eQuanXian);
        //                //quanxian.Insert();

        //            }
        //            //角色权限
        //            if (eJueSeQX != null && eJueSeQX.State == DTOState.New)
        //            {
        //                //先删除
        //                try
        //                {
        //                    var jueseqx = GYJueSeQXDomain.GetByID(DBContext, ServiceContext, eJueSeQX);
        //                    jueseqx.Delete();
        //                }
        //                catch (Exception)
        //                {
        //                    //说明不存在 继续
        //                    //再增加
        //                    eJueSeQX.State = DTOState.New;
        //                    var jueseqx2 = GYJueSeQXDomain.Create(DBContext, ServiceContext, eJueSeQX);
        //                }                        
        //            }

        //        }
        //        if (eGongNengNew.State == DTOState.Delete)
        //        {
        //            //先删除
        //            var gongneng = XTGongNengDomain.GetByID(DBContext, ServiceContext, eGongNengNew.GONGNENGROWID);
        //            gongneng.Delete();

        //            if (eGongNengNew.GONGNENGID != null && eGongNengNew.GONGNENGID !="-")
        //            {
        //                //在删除权限  
        //                if (eQuanXian != null && eQuanXian.State == DTOState.Delete)
        //                {
        //                    var quanxian = GYQuanXianDomain.GetByID(DBContext, ServiceContext, eQuanXian.QUANXIANID);
        //                    quanxian.Delete();

        //                }
        //            }

        //        }
        //        DBContext.SaveChanges();
        //        //提交
        //        trans.Commit();
        //    }
        //    return new Result<bool>(ReturnCode.SUCCESS, "保存系统功能成功！");
        //}
        //#endregion
    }
}
