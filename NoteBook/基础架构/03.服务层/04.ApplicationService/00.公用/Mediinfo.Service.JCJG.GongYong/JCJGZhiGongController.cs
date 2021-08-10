using Mediinfo.Domain.JCJG.GY;
using Mediinfo.DTO.Core;
using Mediinfo.DTO.HIS.GY;
using Mediinfo.Enterprise;
using Mediinfo.Enterprise.Exceptions;
using Mediinfo.Infrastructure.Core;
using Mediinfo.Service.JCJG.GongYong.Route;
using Mediinfo.Utility.Extensions;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Mediinfo.Service.JCJG.GongYong
{
    [ServiceRoutePrefix]
    [Route("JCJGZhiGong/{action}")]
    public class JCJGZhiGongController : Mediinfo.Infrastructure.JCJG.Controller.HISController
    {
        #region 查询类
        /// <summary>
        /// 获取职工信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<List<E_GY_ZHIGONGXX>> GetZhiGongXX()
        {
            E_GY_ZHIGONGXX entry = new E_GY_ZHIGONGXX();
            entry.Where(" ORDER BY ZHIGONGID ");
            var list = new QueryService(UnitOfWork).Get<E_GY_ZHIGONGXX>(entry);
            return ServiceContent(list);
        }
        /// <summary>
        /// 获取职工信息by 职工类别
        /// </summary>
        /// <param name="zhiGongLB"></param>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<List<E_GY_ZHIGONGXX>> GetZhiGongXXByZhiGongLB(string zhiGongLB)
        {
            Check.NotEmpty(zhiGongLB, "职工类别不能为空!");
            E_GY_ZHIGONGXX entry = new E_GY_ZHIGONGXX();
            entry.Where(" WHERE ZHIGONGLB =:ZHIGONGLB ", zhiGongLB);
            entry.WhereAppend(" ORDER BY ZHIGONGID ");
            var list = new QueryService(UnitOfWork).Get<E_GY_ZHIGONGXX>(entry);
            return ServiceContent(list);
        }

        ///// <summary>
        ///// 获取门诊诊间登录 职工科室列表
        ///// </summary>
        ///// <param name="yuanQuID"></param>
        ///// <param name="zhiGongID"></param>
        ///// <returns></returns>
        //[HttpPost]
        //public ServiceResult<List<E_GY_ZHIGONGKS>> GetMenZhenZJDLZGKSList(string yuanQuID,int keShiBQBZ,string zhiGongID)
        //{
        //    if (string.IsNullOrWhiteSpace(yuanQuID))
        //    {
        //        throw new ServiceException("院区ID不能为空！");
        //    }
        //    if (string.IsNullOrWhiteSpace(zhiGongID))
        //    {
        //        throw new ServiceException("职工ID不能为空！");
        //    }

        //    E_GY_ZHIGONGKS entry = new E_GY_ZHIGONGKS();
        //    entry.Where(" WHERE KESHIBQBZ = :KESHIBQBZ ", keShiBQBZ);
        //    entry.WhereAppend(" AND ZHIGONGID = :ZHIGONGID AND KESHIBQID IN(SELECT KESHIID FROM GY_KESHI WHERE YUANQUID =:YUANQUID AND  ZUOFEIBZ <> 1 ) ", zhiGongID, yuanQuID);
        //    var list = new QueryService(UnitOfWork).Get<E_GY_ZHIGONGKS>(entry);
        //    return ServiceContent(list);
        //}

  

        /// <summary>
        /// 获取职工处方限额
        /// </summary>
        /// <param name="keShiID"></param>
        /// <param name="zhiGongID"></param>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<List<E_GY_ZHIGONGCFED>> GetZhiGongCFXE(string keShiID, string zhiGongID)
        {
            if (string.IsNullOrWhiteSpace(keShiID))
            {
                throw new ServiceException("科室ID不能为空！");
            }
            if (string.IsNullOrWhiteSpace(zhiGongID))
            {
                throw new ServiceException("职工ID不能为空！");
            }
            E_GY_ZHIGONGCFED entry = new E_GY_ZHIGONGCFED();
            entry.Where(" WHERE KESHIID =:KESHIID AND ZHIGONGID =:ZHIGONGID ", keShiID,zhiGongID);            
            var list = new QueryService(UnitOfWork).Get<E_GY_ZHIGONGCFED>(entry);
            return ServiceContent(list);
        }
        /// <summary>
        /// 获取职工输液限量
        /// </summary>
        /// <param name="keShiID"></param>
        /// <param name="zhiGongID"></param>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<List<E_GY_ZHIGONGSYXL>> GetZhiGongSYXL(string keShiID, string zhiGongID)
        {
            if (string.IsNullOrWhiteSpace(keShiID))
            {
                throw new ServiceException("科室ID不能为空！");
            }
            if (string.IsNullOrWhiteSpace(zhiGongID))
            {
                throw new ServiceException("职工ID不能为空！");
            }
            E_GY_ZHIGONGSYXL entry = new E_GY_ZHIGONGSYXL();
            entry.Where(" WHERE  KESHIID =:KESHIID AND ZHIGONGID =:ZHIGONGID ", keShiID, zhiGongID);
            var list = new QueryService(UnitOfWork).Get<E_GY_ZHIGONGSYXL>(entry);
            return ServiceContent(list);
        }
        /// <summary>
        /// 获取职工信息BY职工ID
        /// </summary>
        /// <param name="zhigongid"></param>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<List<E_GY_ZHIGONGXX>> GetZhiGongXXByZhiGongID(string zhiGongID)
        {

            E_GY_ZHIGONGXX entry = new E_GY_ZHIGONGXX();
            if (!string.IsNullOrWhiteSpace(zhiGongID))
            {
                entry.Where(" Where ZHIGONGID =:ZHIGONGID ", zhiGongID);
            }            
            var list = new QueryService(UnitOfWork).Get<E_GY_ZHIGONGXX>(entry);
            return  ServiceContent(list);
        }

        /// <summary>
        /// 获取职工信息BY职工ID
        /// </summary>
        /// <param name="zhigongid"></param>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<List<E_GY_ZHIGONGXX>> GetKangJunZGXXByZGID(string zhiGongID)
        {

            E_GY_ZHIGONGXX entry = new E_GY_ZHIGONGXX();
            if (!string.IsNullOrWhiteSpace(zhiGongID))
            {
                entry.Where(" Where substr(quanxian,28,1)='1' AND  ZHIGONGID =:ZHIGONGID ", zhiGongID);
            }
            var list = new QueryService(UnitOfWork).Get<E_GY_ZHIGONGXX>(entry);
            return ServiceContent(list);
        }

        /// <summary>
        /// 获取科室护士长
        /// </summary>
        /// <param name="zhigongid"></param>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<List<E_GY_ZHIGONGXX>> GetKeShiHSZByKSID(string keShiID)
        {
            Check.NotEmpty(keShiID, "科室ID不能为空!");

            E_GY_ZHIGONGXX entry = new E_GY_ZHIGONGXX();

            entry.Where(" Where KESHIID =:KESHIID AND ZHIWU='15' ", keShiID);

            var list = new QueryService(UnitOfWork).Get<E_GY_ZHIGONGXX>(entry);
            return ServiceContent(list);
        }

        /// <summary>
        /// 获取职工科室信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<List<E_GY_ZHIGONGKS>> GetZhiGongKS()
        {
            E_GY_ZHIGONGKS entry = new E_GY_ZHIGONGKS();
            var list = new QueryService(UnitOfWork).Get<E_GY_ZHIGONGKS>(entry);
            return ServiceContent(list);

        }
        /// <summary>
        /// 获取职工科室信息BY职工ID
        /// </summary>
        /// <param name="ZhiGongID"></param>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<List<E_GY_ZHIGONGKS>> GetZhiGongKSByZhiGongID(string zhiGongID)
        {
            E_GY_ZHIGONGKS entry = new E_GY_ZHIGONGKS();
            if (!string.IsNullOrWhiteSpace(zhiGongID))
            {
                entry.Where(" Where ZHIGONGID = :ZHIGONGID ", zhiGongID);
            }                
            var list = new QueryService(UnitOfWork).Get<E_GY_ZHIGONGKS>(entry);
            return ServiceContent(list);
        }
        /// <summary>
        /// 获取医疗组信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<List<E_GY_YILIAOZU1>> GetYiLiaoZu1()
        {
            E_GY_YILIAOZU1 entry = new E_GY_YILIAOZU1();
            var list = new QueryService(UnitOfWork).Get<E_GY_YILIAOZU1>(entry);
            return ServiceContent(list);
        }
        /// <summary>
        /// 获取医疗组1信息  BY 医疗组ID
        /// </summary>
        /// <param name="yiliaozid"></param>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<List<E_GY_YILIAOZU1>> GetYiLiaoZu1ByYiLiaoZID(string yiLiaoZID)
        {

            E_GY_YILIAOZU1 entry = new E_GY_YILIAOZU1();
            if (!string.IsNullOrWhiteSpace(yiLiaoZID))
            {
                entry.Where(" WHERE YILIAOZID = :YILIAOZID", yiLiaoZID);
            }                
            var list = new QueryService(UnitOfWork).Get<E_GY_YILIAOZU1>(entry);
            return  ServiceContent(list);
        }
        /// <summary>
        /// 获取医疗组信息2BY职工ID
        /// </summary>
        /// <param name="ZhiGongID"></param>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<List<E_GY_YILIAOZU2>> GetYiLiaoZu2ByZhiGongID(string zhiGongID)
        {
            E_GY_YILIAOZU2 entry = new E_GY_YILIAOZU2();
            if (!string.IsNullOrWhiteSpace(zhiGongID))
            {
                entry.Where(" Where ZHIGONGID = :ZHIGONGID ", zhiGongID);
            }                
            var list = new QueryService(UnitOfWork).Get<E_GY_YILIAOZU2>(entry);
            return ServiceContent(list);
        }
        /// <summary>
        /// 获取医疗组2信息 BY 医疗组ID
        /// </summary>
        /// <param name="yiliaozid"></param>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<List<E_GY_YILIAOZU2>> GetYiLiaoZu2ByYiLiaoZID(string yiLiaoZID)
        {
            E_GY_YILIAOZU2 entry = new E_GY_YILIAOZU2();
            if (!string.IsNullOrWhiteSpace(yiLiaoZID))
            {
                entry.Where(" Where YILIAOZID = :YILIAOZID ", yiLiaoZID);
            }                
            var list = new QueryService(UnitOfWork).Get<E_GY_YILIAOZU2>(entry);
            return ServiceContent(list);
        }
        /// <summary>
        /// 获取医疗组信息3BY职工ID
        /// </summary>
        /// <param name="ZhiGongID"></param>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<List<E_GY_YILIAOZU3>> GetYiLiaoZu3ByZhiGongID(string zhiGongID)
        {
            E_GY_YILIAOZU3 entry = new E_GY_YILIAOZU3();
            if (!string.IsNullOrWhiteSpace(zhiGongID))
            {
                entry.Where(" Where ZHIGONGID = :ZHIGONGID ", zhiGongID);
            }                
            var list = new QueryService(UnitOfWork).Get<E_GY_YILIAOZU3>(entry);
            return ServiceContent(list);
        }
        /// <summary>
        /// 获取医疗组4信息 BY 医疗组ID
        /// </summary>
        /// <param name="yiliaozid"></param>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<List<E_GY_YILIAOZU4>> GetYiLiaoZu4ByYiLiaoZID(string yiLiaoZID)
        {
            E_GY_YILIAOZU4 entry = new E_GY_YILIAOZU4();
            if (!string.IsNullOrWhiteSpace(yiLiaoZID))
            {
                entry.Where(" Where YILIAOZID = :YILIAOZID ", yiLiaoZID);
            }                
            var list = new QueryService(UnitOfWork).Get<E_GY_YILIAOZU4>(entry);
            return ServiceContent(list);
        }

        /// <summary>
        /// 获取会诊组BY职工ID
        /// </summary>
        /// <param name="ZhiGongID"></param>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<List<E_GY_HUIZHENZU>> GetHuZhenZuByZGID(string zhiGongID)
        {
            E_GY_HUIZHENZU entry = new E_GY_HUIZHENZU();
            if (!string.IsNullOrWhiteSpace(zhiGongID))
            {
                entry.Where(" Where ZHIGONGID = :ZHIGONGID ", zhiGongID);
            }
            var list = new QueryService(UnitOfWork).Get<E_GY_HUIZHENZU>(entry);
            return ServiceContent(list);
        }
        /// <summary>
        /// 获取职工核算科室预设BY职工ID
        /// </summary>
        /// <param name="zhigongid"></param>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<List<E_GY_ZHIGONGHSKSYS>> GetZhiGongHSKSYSByZhiGongID(string zhiGongID)
        {
            E_GY_ZHIGONGHSKSYS entry = new E_GY_ZHIGONGHSKSYS();
            if (!string.IsNullOrWhiteSpace(zhiGongID))
            {
                entry.Where(" Where ZHIGONGID = :ZHIGONGID ", zhiGongID);
            }                
            var list = new QueryService(UnitOfWork).Get<E_GY_ZHIGONGHSKSYS>(entry);
            return  ServiceContent(list);
        }
        /// <summary>
        /// 获取角色信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<List<E_GY_JUESE>> GetJueSe()
        {
            E_GY_JUESE entry = new E_GY_JUESE();
            //entry.Where(" Where ZUOFEIBZ = :ZUOFEIBZ", 0);
            var list = new QueryService(UnitOfWork).Get<E_GY_JUESE>(entry);
            return ServiceContent(list);
        }


        /// <summary>
        /// 获取角色窗口权限信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<List<E_GY_JUESECKQX>> GetJueSeCKQX()
        {
            E_GY_JUESECKQX entry = new E_GY_JUESECKQX();
           
            var list = new QueryService(UnitOfWork).Get<E_GY_JUESECKQX>(entry);
            return ServiceContent(list);
        }


        /// <summary>
        /// 获取角色窗口权限信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<List<E_GY_JUESECKQX>> GetJueSeCKQXByID(string jueSeID)
        {
            if (string.IsNullOrWhiteSpace(jueSeID))
            {
                throw new ServiceException("角色ID不能为空!");
            }
            E_GY_JUESECKQX entry = new E_GY_JUESECKQX();
            entry.Where(" Where JUESEID = :JUESEID", jueSeID);
            var list = new QueryService(UnitOfWork).Get<E_GY_JUESECKQX>(entry);
            return ServiceContent(list);
        }

        /// <summary>
        /// 获取用户窗口权限信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<List<E_GY_JUESECKQX>> GetYongHuCKQXByID(string quanXianID)
        {
            if (string.IsNullOrWhiteSpace(quanXianID))
            {
                throw new ServiceException("权限ID不能为空!");
            }
            E_GY_JUESECKQX entry = new E_GY_JUESECKQX();
            entry.Where(" Where QUANXIANID = :QUANXIANID", quanXianID);
            var list = new QueryService(UnitOfWork).Get<E_GY_JUESECKQX>(entry);
            return ServiceContent(list);
        }


        /// <summary>
        /// 获取角色信息By用户ID
        /// </summary>
        /// <param name="yonghuid"></param>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<List<E_GY_JUESEYH>> GetJueSeYHByYongHuID(string yongHuID)
        {
            E_GY_JUESEYH entry = new E_GY_JUESEYH();
            if (!string.IsNullOrWhiteSpace(yongHuID))
            {
                entry.Where(" Where YONGHUID = :YONGHUID", yongHuID);
            }            
            var list = new QueryService(UnitOfWork).Get<E_GY_JUESEYH>(entry);
            return ServiceContent(list);
        }
        /// <summary>
        /// 获取角色用户信息 BY 用户ID
        /// </summary>
        /// <param name="yonghuid"></param>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<List<E_GY_JUESEYH_EX>> GetJueSeYHEXByYongHuID(string yongHuID)
        {
            E_GY_JUESEYH_EX entry = new E_GY_JUESEYH_EX();
            if (!string.IsNullOrWhiteSpace(yongHuID))
            {
                entry.Where(" Where YONGHUID = :YONGHUID", yongHuID);
            }                
            var list = new QueryService(UnitOfWork).Get<E_GY_JUESEYH_EX>(entry);
            return ServiceContent(list);
        }
        /// <summary>
        /// 获取角色窗口权限信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<List<E_GY_JUESECKQX_NEW>> GetYongHuCKQXByIDNEW(string quanXianID)
        {
            if (string.IsNullOrWhiteSpace(quanXianID))
            {
                throw new ServiceException("权限ID不能为空!");
            }
            E_GY_JUESECKQX_NEW entry = new E_GY_JUESECKQX_NEW();
            entry.Where(" Where QUANXIANID = :QUANXIANID", quanXianID);
            var list = new QueryService(UnitOfWork).Get<E_GY_JUESECKQX_NEW>(entry);
            return ServiceContent(list);
        }

        /// <summary>
        /// 获取职工信息(包含权限)
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<List<E_GY_ZHIGONGYHQX>> GetZhiGongUserRootAllXX()
        {
            E_GY_ZHIGONGYHQX entry = new E_GY_ZHIGONGYHQX();
            var list = new QueryService(UnitOfWork).Get<E_GY_ZHIGONGYHQX>(entry);
            return ServiceContent(list);
        }


        /// <summary>
        /// 根据权限ID获取职工信息
        /// </summary>
        /// <param name="quanxianid"></param>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<List<E_GY_ZHIGONGYHQX>> GetZhiGongUserRoot(string quanxianid)
        {
            E_GY_ZHIGONGYHQX entry = new E_GY_ZHIGONGYHQX();
            if (!string.IsNullOrWhiteSpace(quanxianid))
            {
                entry.Where(" WHERE QUANXIANID = :QUANXIANID", quanxianid);
            }
            var list = new QueryService(UnitOfWork).Get<E_GY_ZHIGONGYHQX>(entry);
            return ServiceContent(list);
        }




        /// <summary>
        /// 更新用户表
        /// </summary>
        /// <param name="eYingYongCDList"></param>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<bool> BaoCunYongHuXinXi(List<E_GY_ZHIGONGYHQX> yonghuList)
        {
            UnitOfWork.BeginTransaction();
            var yonghuRep = this.GetRepository<IGY_YONGHUQX2Repository>(UnitOfWork);
            ///1先保存用户信息
            if (yonghuList.GetNews().Count > 0)
            {
                yonghuList.GetNews().ForEach(o =>
                {
                    var xiTongGN = GY_YONGHUQX2Factory.Create(yonghuRep, ServiceContext, o);
                });
            }
            //修改
            if (yonghuList.GetUpdates().Count > 0)
            {
                yonghuList.GetUpdates().ForEach(o =>
                {
                    var yonghu = yonghuRep.GetByKey(o.YONGHUID,o.QUANXIANID);
                   
                    yonghu.Update(o);

                });
            }
            ////删除应用菜单
            if (yonghuList.GetDeletes().Count > 0)
            {
                yonghuList.GetDeletes().ForEach(o =>
                {
                    var yonghu = yonghuRep.GetByKey( o.YONGHUID, o.QUANXIANID);
                    yonghu.Delete();

                });
            }

            UnitOfWork.SaveChanges();
            //提交
            UnitOfWork.Commit();
            return ServiceContent(true);
        }
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<List<E_GY_YONGHUXX>> GetYongHuXX()
        {
            E_GY_YONGHUXX entry = new E_GY_YONGHUXX();
            var list = new QueryService(UnitOfWork).Get<E_GY_YONGHUXX>(entry);
            return ServiceContent(list);
        }
        /// <summary>
        /// 获取用户信息By用户ID
        /// </summary>
        /// <param name="yonghuid"></param>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<List<E_GY_YONGHUXX>> GetYongHuXXByYongHuID(string yonghuid)
        {
            E_GY_YONGHUXX entry = new E_GY_YONGHUXX();
            if (!string.IsNullOrWhiteSpace(yonghuid))
            {
                entry.Where(" Where YONGHUID = :YONGHUID", yonghuid);
            }                
            var list = new QueryService(UnitOfWork).Get<E_GY_YONGHUXX>(entry);
            return ServiceContent(list);
        }
        /// <summary>
        /// 获取用户应用信息 By 用户ID
        /// </summary>
        /// <param name="yonghuid"></param>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<List<E_GY_YONGHUYY_EX>> GetYongHuYYEXByYongHuID(string yonghuid)
        {
            E_GY_YONGHUYY_EX entry = new E_GY_YONGHUYY_EX();
            if (!string.IsNullOrWhiteSpace(yonghuid))
            {
                entry.Where(" Where YONGHUID = :YONGHUID", yonghuid);
            }            
            var list = new QueryService(UnitOfWork).Get<E_GY_YONGHUYY_EX>(entry);
            return  ServiceContent(list);
        }
        /// <summary>
        /// 获取角色信息 By 角色ID
        /// </summary>
        /// <param name="jueseid"></param>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<List<E_GY_JUESE>> GetJueSeByID(string jueseid)
        {
            E_GY_JUESE entry = new E_GY_JUESE();
            if (!string.IsNullOrWhiteSpace(jueseid))
            {
                entry.Where(" Where JUESEID = :JUESEID", jueseid);
            }
            else
            {
                entry.Where(" ORDER BY JUESEID ASC");
            }                
            var list = new QueryService(UnitOfWork).Get<E_GY_JUESE>(entry);
            return ServiceContent(list);
        }
        /// <summary>
        /// 获取角色用户信息信息 By 角色ID
        /// </summary>
        /// <param name="jueseid"></param>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<List<E_GY_JUESEYH_EX>> GetJueSeYHEXByID(string jueseid)
        {
            E_GY_JUESEYH_EX entry = new E_GY_JUESEYH_EX();
            if (!string.IsNullOrWhiteSpace(jueseid))
            {
                entry.Where(" Where JUESEID = :JUESEID", jueseid);
            }
            else
            {
                entry.Where(" ORDER BY JUESEID ASC");
            }
            var list = new QueryService(UnitOfWork).Get<E_GY_JUESEYH_EX>(entry);
            return ServiceContent(list);
        }
        //权限相关
        /// <summary>
        /// 获取权限列表信息 By 权限ID
        /// </summary>
        /// <param name="quanxianid"></param>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<List<E_GY_QUANXIAN>> GetQuanXian(string quanxianid)
        {
            E_GY_QUANXIAN entry = new E_GY_QUANXIAN();
            if (!string.IsNullOrWhiteSpace(quanxianid))
            {
                entry.Where(" Where QUANXIANID = :QUANXIANID", quanxianid);
            }
            else
            {
                entry.Where(" ORDER BY QUANXIANID ASC");
            }
            var list = new QueryService(UnitOfWork).Get<E_GY_QUANXIAN>(entry);
            return ServiceContent(list);
        }
        /// <summary>
        /// 获取角色权限信息 BY 角色ID
        /// </summary>
        /// <param name="jueseid"></param>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<List<E_GY_JUESEQX>> GetJueSeQXByJueSeID(string jueseid)
        {
            E_GY_JUESEQX entry = new E_GY_JUESEQX();
            if (!string.IsNullOrWhiteSpace(jueseid))
            {
                entry.Where(" Where JUESEID = :JUESEID", jueseid);
            }
            else
            {
                entry.Where(" ORDER BY JUESEID ASC");
            }
            var list = new QueryService(UnitOfWork).Get<E_GY_JUESEQX>(entry);
            return ServiceContent(list);
        }



        /// <summary>
        /// 获取角色权限信息 BY 权限ID
        /// </summary>
        /// <param name="quanxianid"></param>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<List<E_GY_JUESEQX>> GetJueSeQXByQuanXianID(string quanxianid)
        {
            E_GY_JUESEQX entry = new E_GY_JUESEQX();
            if (!string.IsNullOrWhiteSpace(quanxianid))
            {
                entry.Where(" Where QUANXIANID = :QUANXIANID", quanxianid);
            }
            else
            {
                entry.Where(" ORDER BY QUANXIANID ASC");
            }
            var list = new QueryService(UnitOfWork).Get<E_GY_JUESEQX>(entry);
            return ServiceContent(list);
        }
        /// <summary>
        /// 通过用户ID和权限ID获得用户对应的权限
        /// </summary>
        /// <param name="quanxianid"></param>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<bool> GetJueSeYHQXBYQuanXianID(string quanxianid)
        {
            bool returnValue = true;
            var quanXianRep = this.GetRepository<IGY_QUANXIANRepository>(UnitOfWork);
            var quanXian = quanXianRep.GetByKey(quanxianid);

            if(quanXian == null)
            {
                E_GY_QUANXIAN quanXianDTO = new E_GY_QUANXIAN();
                quanXianDTO.QUANXIANID = quanxianid;
                quanXianDTO.QUANXIANMC = quanxianid + "(A)";
                quanXianDTO.GONGNENGID = "*";
                quanXianDTO.QUANXIANJB = 2;
                quanXianDTO.XIUGAIREN = ServiceContext.USERID;
                quanXianDTO.QUANXIANMS = "";
                quanXianDTO.QIYONGBZ = 0;   //默认使用启用标志为0
                var quanXianEntity = GY_QUANXIANFactory.Create(quanXianRep, ServiceContext, quanXianDTO);
                quanXianRep.RegisterAdd(quanXianEntity);
                UnitOfWork.SaveChanges();
                returnValue = true;
            } 
            else
            {
                if(quanXian.QIYONGBZ != 0 )
                { 
                    E_GY_JUESEYHQX entry = new E_GY_JUESEYHQX();

                    entry.Where(" Where QUANXIANID = :QUANXIANID AND YONGHUID = :YONGHUID ", quanxianid, ServiceContext.USERID);

                    var list = new QueryService(UnitOfWork).Get<E_GY_JUESEYHQX>(entry).FirstOrDefault();
                    
                    if(list == null)
                    {
                        returnValue = false;
                    }
                    else
                    {
                        returnValue = true;
                    }
                }  
            } 

            return ServiceContent(returnValue);
        }

        #endregion

        #region 业务类
        /// <summary>
        /// 用户重置密码
        /// </summary>
        /// <param name="newPassword">用户输入的新密码</param>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<bool> ResetPassword(string zhiGongID, string newPassword)
        {
            UnitOfWork.BeginTransaction();
            var yongHuXXRep = this.GetRepository<IGY_YONGHUXXRepository>(UnitOfWork);
            var yonghu = yongHuXXRep.GetByKey(zhiGongID);
            if (yonghu != null)
            {
                yonghu.ResetPassword(newPassword);
            }    
            UnitOfWork.SaveChanges();
            UnitOfWork.Commit();
          
            //  return new Result<bool>(ReturnCode.SUCCESS, "密码重置成功");
            return ServiceContent(true);
        }
        /// <summary>
        /// 保存职工信息 同时插入用户信息
        /// </summary>
        /// <param name="eZhiGongXXList"></param>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<bool> BaoCunZhiGongXX(List<E_GY_ZHIGONGXX> eZhiGongXXList)
        {
            UnitOfWork.BeginTransaction();
            var zhigongxxRep = this.GetRepository<IGY_ZHIGONGXXRepository>(UnitOfWork);
            ///1先保存职工信息
            if (eZhiGongXXList.GetNews().Count > 0)
            {
                eZhiGongXXList.GetNews().ForEach(o =>
                {
                    var zhigongxx = GY_ZHIGONGXXFactory.Create(zhigongxxRep, ServiceContext, o);

                    //新增职工信息的同时 也是要新增用户信息
                    NewYongHuXX(o);
                });
            }
            //更新职工信息
            if (eZhiGongXXList.GetUpdates().Count > 0)
            {
                eZhiGongXXList.GetUpdates().ForEach(o =>
                {
                    string zhigongid = o.ZHIGONGID;
                    var zhigongxx = zhigongxxRep.GetByKey(zhigongid);
                    if (zhigongxx != null)
                    {
                        zhigongxx.Update(o);
                    }    
                });
            }
            //删除职工信息
            if (eZhiGongXXList.GetDeletes().Count > 0)
            {
                eZhiGongXXList.GetDeletes().ForEach(o =>
                {
                    string zhigongid = o.ZHIGONGID;
                    var zhigongxx = zhigongxxRep.GetByKey(zhigongid);
                    if (zhigongxx != null)
                    {
                        zhigongxx.Delete();
                    }                        
                });
            }


            UnitOfWork.SaveChanges();
            UnitOfWork.Commit();            
            return ServiceContent(true);
        }
        /// <summary>
        /// 临时插入用户信息 方法
        /// </summary>
        /// <param name="eZhiGongXX"></param>
        private void NewYongHuXX(E_GY_ZHIGONGXX eZhiGongXX)
        {
            var yonghuxxRep = this.GetRepository<IGY_YONGHUXXRepository>(UnitOfWork);
            E_GY_YONGHUXX eYongHuXX = new E_GY_YONGHUXX();
            eYongHuXX.YONGHUID = eZhiGongXX.ZHIGONGID;
            eYongHuXX.YONGHUXM = eZhiGongXX.ZHIGONGXM;
            eYongHuXX.TINGYONGBZ = 0;
            eYongHuXX.MIMA = null;
            eYongHuXX.SHURUMA = "SHURUMA1";
            eYongHuXX.XIUGAIREN = ServiceContext.USERID;
            eYongHuXX.XIUGAISJ = yonghuxxRep.GetSYSTime();
            eYongHuXX.SetState( DTOState.New);
            var yonghuxx = GY_YONGHUXXFactory.Create(yonghuxxRep, ServiceContext, eYongHuXX); 
        }
        /// <summary>
        /// 保存职工信息 职工科室信息
        /// </summary>
        /// <param name="eZhiGongXX"></param>
        /// <param name="eZhiGongKSList"></param>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<bool> BaoCunZhiGongXX2(E_GY_ZHIGONGXX eZhiGongXX, List<E_GY_ZHIGONGKS> eZhiGongKSList)
        {
            UnitOfWork.BeginTransaction();

            var zhigongxxRep = this.GetRepository<IGY_ZHIGONGXXRepository>(UnitOfWork);

            var zhigongksRep = this.GetRepository<IGY_ZHIGONGKSRepository>(UnitOfWork);
            ///1先保存职工信息
            if (eZhiGongXX.GetState() == DTOState.New)
            {
                var zhigongxx = GY_ZHIGONGXXFactory.Create(zhigongxxRep, ServiceContext, eZhiGongXX);

                //新增职工信息的同时 也是要新增用户信息
                NewYongHuXX(eZhiGongXX);
            }
            //更新职工信息
            if (eZhiGongXX.GetState() == DTOState.Update)
            {
                string zhigongid = eZhiGongXX.ZHIGONGID;
                var zhigongxx = zhigongxxRep.GetByKey(zhigongid);
                if (zhigongxx != null)
                {
                    zhigongxx.Update(eZhiGongXX);
                }                
            }

            //2再保存职工科室信息
            if (eZhiGongKSList.GetNews().Count > 0)
            {
                eZhiGongKSList.GetNews().ForEach(o =>
                {
                    var zhigongks = GY_ZHIGONGKSFactory.Create(zhigongksRep, ServiceContext, o);
                });
            }
            //删除职工科室
            if (eZhiGongKSList.GetDeletes().Count > 0)
            {
                eZhiGongKSList.GetDeletes().ForEach(o =>
                {
                    string zhigongid = o.ZHIGONGID;
                    string keshiid = o.KESHIBQID;
                    int biaozhi = o.KESHIBQBZ ?? 0;
                    var zhigongks = zhigongksRep.GetByID(zhigongid, keshiid, biaozhi);
                    if (zhigongks != null)
                    {
                        zhigongks.Delete();
                    }
                });
            }

            //更新职工科室
            if (eZhiGongKSList.GetUpdates().Count > 0)
            {
                eZhiGongKSList.GetUpdates().ForEach(o =>
                {
                    string zhigongid = o.ZHIGONGID;
                    string keshiid = o.KESHIBQID;
                    int biaozhi = o.KESHIBQBZ ?? 0;
                    var zhigongks = zhigongksRep.GetByID(zhigongid, keshiid, biaozhi);
                    if (zhigongks != null)
                    {
                        zhigongks.Update(o);
                    }
                });
            }

            UnitOfWork.SaveChanges();
            UnitOfWork.Commit();
            return ServiceContent(true);
        }
        /// <summary>
        /// 保存职工信息 角色用户信息 职工科室信息
        /// </summary>
        /// <param name="eZhiGongXX"></param>
        /// <param name="eJueSeYHList"></param>
        /// <param name="eZhiGongKSList"></param>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<bool> BaoCunZhiGongXX3(E_GY_ZHIGONGXX eZhiGongXX, List<E_GY_JUESEYH> eJueSeYHList, List<E_GY_ZHIGONGKS> eZhiGongKSList)
        {
            UnitOfWork.BeginTransaction();

            var zhigongxxRep = this.GetRepository<IGY_ZHIGONGXXRepository>(UnitOfWork);
            var jueseyhRep = this.GetRepository<IGY_JUESEYHRepository>(UnitOfWork);
            var zhigongksRep = this.GetRepository<IGY_ZHIGONGKSRepository>(UnitOfWork);
            ///1先保存职工信息                
            if (eZhiGongXX.GetState() == DTOState.New)
            {
                var zhigongxx = GY_ZHIGONGXXFactory.Create(zhigongxxRep, ServiceContext, eZhiGongXX);

                //新增职工信息的同时 也是要新增用户信息
                NewYongHuXX(eZhiGongXX);
            }
            //更新职工信息
            if (eZhiGongXX.GetState() == DTOState.Update)
            {
                string zhigongid = eZhiGongXX.ZHIGONGID;
                var zhigongxx = zhigongxxRep.GetByKey(zhigongid);
                if (zhigongxx != null)
                {
                    zhigongxx.Update(eZhiGongXX);
                }
            }
            //2 保存角色用户
            if (eJueSeYHList.GetNews().Count > 0)
            {
                eJueSeYHList.GetNews().ForEach(o =>
                {
                    var jueseyh = GY_JUESEYHFactory.Create(jueseyhRep, ServiceContext, o);
                   
                });
            }
            //删除
            if (eJueSeYHList.GetDeletes().Count > 0)
            {
                eJueSeYHList.GetDeletes().ForEach(o =>
                {
                    string jueseid = o.JUESEID;
                    string yonghuid = o.YONGHUID;
                    var jueseyh = jueseyhRep.GetByID(jueseid, yonghuid);
                    if (jueseyh != null)
                    {
                        jueseyh.Delete();
                    }                    
                });
            }
            //更新
            if (eJueSeYHList.GetUpdates().Count > 0)
            {
                eJueSeYHList.GetUpdates().ForEach(o =>
                {
                    string jueseid = o.JUESEID;
                    if (o.OriginalValues["JUESEID"] != null)
                    {
                        jueseid = o.OriginalValues.FirstOrDefault(t => t.Key == "JUESEID").Value.ToStringEx();
                    }
                    string yonghuid = o.YONGHUID;
                    if (o.OriginalValues["YONGHUID"] != null)
                    {
                        yonghuid = o.OriginalValues.FirstOrDefault(t => t.Key == "YONGHUID").Value.ToStringEx();
                    }
                    var jueseyh = jueseyhRep.GetByID(jueseid, yonghuid);
                    if (jueseyh != null)
                    {
                        jueseyh.Delete();
                    }
                    var jueseyhXX = GY_JUESEYHFactory.Create(jueseyhRep, ServiceContext, o);
                });
            }

            //3再保存职工科室信息
            if (eZhiGongKSList.GetNews().Count > 0)
            {
                eZhiGongKSList.GetNews().ForEach(o =>
                {
                    var zhigongks = GY_ZHIGONGKSFactory.Create(zhigongksRep, ServiceContext, o);
                    //zhigongks.Insert();
                });
            }
            //删除职工科室
            if (eZhiGongKSList.GetDeletes().Count > 0)
            {
                eZhiGongKSList.GetDeletes().ForEach(o =>
                {
                    string zhigongid = o.ZHIGONGID;
                    string keshiid = o.KESHIBQID;
                    int biaozhi = o.KESHIBQBZ ?? 0;
                    var zhigongks = zhigongksRep.GetByID(zhigongid, keshiid, biaozhi);
                    if (zhigongks != null)
                    {
                        zhigongks.Delete();
                    }                    
                });
            }

            //更新职工科室
            if (eZhiGongKSList.GetUpdates().Count > 0)
            {
                eZhiGongKSList.GetUpdates().ForEach(o =>
                {
                    
                    string zhigongid = o.ZHIGONGID;
                    if (o.OriginalValues["ZHIGONGID"] != null)
                    {
                        zhigongid = o.OriginalValues.FirstOrDefault(t => t.Key == "ZHIGONGID").Value.ToStringEx();
                    }
                    string keshiid = o.KESHIBQID;
                    if (o.OriginalValues["KESHIBQID"] != null)
                    {
                        keshiid = o.OriginalValues.FirstOrDefault(t => t.Key == "KESHIBQID").Value.ToStringEx();
                    }
                    int biaozhi = o.KESHIBQBZ ?? 0;
                    if (o.OriginalValues["KESHIBQBZ"] != null)
                    {
                        biaozhi = Convert.ToInt32(o.OriginalValues.FirstOrDefault(t => t.Key == "KESHIBQBZ").Value);
                    }
                    var zhigongks = zhigongksRep.GetByID(zhigongid, keshiid, biaozhi);
                    if (zhigongks != null)
                    {
                        zhigongks.Delete();
                    }
                    var zhigongksXX = GY_ZHIGONGKSFactory.Create(zhigongksRep, ServiceContext, o);
                });
            }
            UnitOfWork.SaveChanges();
            UnitOfWork.Commit();
            return ServiceContent(true);
        }
        /// <summary>
        /// 保存医疗组1信息
        /// </summary>
        /// <param name="eYiLiaoZu1List"></param>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<bool> BaoCunYiLiaoZuXX(List<E_GY_YILIAOZU1> eYiLiaoZu1List)
        {
            UnitOfWork.BeginTransaction();

            var yiliaozu1Rep = this.GetRepository<IGY_YILIAOZU1Repository>(UnitOfWork);
            ///1先保存医疗组信息
            if (eYiLiaoZu1List.GetNews().Count > 0)
            {
                eYiLiaoZu1List.GetNews().ForEach(o =>
                {
                    var yiliaozu1 = GY_YILIAOZU1Factory.Create(yiliaozu1Rep, ServiceContext, o);                   
                });
            }
            //更新医疗组信息
            if (eYiLiaoZu1List.GetUpdates().Count > 0)
            {
                eYiLiaoZu1List.GetUpdates().ForEach(o =>
                {
                    var yiliaozu1 = yiliaozu1Rep.GetByKey(o.YILIAOZID);
                    if (yiliaozu1 != null)
                    {
                        yiliaozu1.Update(o);
                    }                    
                });
            }
            //删除医疗组信息
            if (eYiLiaoZu1List.GetDeletes().Count > 0)
            {
                eYiLiaoZu1List.GetDeletes().ForEach(o =>
                {
                    var yiliaozu1 = yiliaozu1Rep.GetByKey(o.YILIAOZID);
                    if (yiliaozu1 != null)
                    {
                        yiliaozu1.Delete();
                    }
                });
            }

            UnitOfWork.SaveChanges();
            UnitOfWork.Commit();
            return ServiceContent(true);
        }

        public ServiceResult<bool> BaoCunYiLiaoZuXX2(List<E_GY_YILIAOZU1> eYiLiaoZu1List, List<E_GY_YILIAOZU2> eYiLiaoZu2List, List<E_GY_YILIAOZU4> eYiLiaoZu4List)
        {
            UnitOfWork.BeginTransaction();

            var yiliaozu1Rep = this.GetRepository<IGY_YILIAOZU1Repository>(UnitOfWork);

            var yiliaozu2Rep = this.GetRepository<IGY_YILIAOZU2Repository>(UnitOfWork);

            var yiliaozu4Rep = this.GetRepository<IGY_YILIAOZU4Repository>(UnitOfWork);
            ///1先保存医疗组信息
            if (eYiLiaoZu1List.GetNews().Count > 0)
            {
                eYiLiaoZu1List.GetNews().ForEach(o =>
                {
                    var yiliaozu1 = GY_YILIAOZU1Factory.Create(yiliaozu1Rep, ServiceContext, o);
                });
            }
            //更新医疗组信息
            if (eYiLiaoZu1List.GetUpdates().Count > 0)
            {
                eYiLiaoZu1List.GetUpdates().ForEach(o =>
                {
                    var yiliaozu1 = yiliaozu1Rep.GetByKey(o.YILIAOZID);
                    if (yiliaozu1 != null)
                    {
                        yiliaozu1.Update(o);
                    }
                });
            }
            //删除医疗组信息
            if (eYiLiaoZu1List.GetDeletes().Count > 0)
            {
                eYiLiaoZu1List.GetDeletes().ForEach(o =>
                {
                    var yiliaozu1 = yiliaozu1Rep.GetByKey(o.YILIAOZID);
                    if (yiliaozu1 != null)
                    {
                        yiliaozu1.Delete();
                    }
                });
            }


            //2先保存医疗组2信息
            if (eYiLiaoZu2List.GetNews().Count > 0)
            {
                eYiLiaoZu2List.GetNews().ForEach(o =>
                {
                    var yiliaozu2 = GY_YILIAOZU2Factory.Create(yiliaozu2Rep, ServiceContext, o);
                    //yiliaozu2.Insert();
                });
            }
            //更新医疗组2信息
            if (eYiLiaoZu2List.GetUpdates().Count > 0)
            {
                eYiLiaoZu2List.GetUpdates().ForEach(o =>
                {
                    string yiliaozid = o.YILIAOZID;
                    string zhigongid = o.ZHIGONGID;
                    var yiliaozu2 = yiliaozu2Rep.GetByID(yiliaozid, zhigongid);
                    if (yiliaozu2 != null)
                    {
                        yiliaozu2.Update(o);
                    }
                });
            }
            //删除医疗组2信息
            if (eYiLiaoZu2List.GetDeletes().Count > 0)
            {
                eYiLiaoZu2List.GetDeletes().ForEach(o =>
                {
                    string yiliaozid = o.YILIAOZID;
                    string zhigongid = o.ZHIGONGID;
                    var yiliaozu2 = yiliaozu2Rep.GetByID(yiliaozid, zhigongid);
                    if (yiliaozu2 != null)
                    {
                        yiliaozu2.Delete();
                    }
                });
            }
            ///3先保存医疗组4信息
            if (eYiLiaoZu4List.GetNews().Count > 0)
            {
                eYiLiaoZu4List.GetNews().ForEach(o =>
                {
                    var yiliaozu4 = GY_YILIAOZU4Factory.Create(yiliaozu4Rep, ServiceContext, o);
                    //yiliaozu4.Insert();
                });
            }
            //更新医疗组4信息
            if (eYiLiaoZu4List.GetUpdates().Count > 0)
            {
                eYiLiaoZu4List.GetUpdates().ForEach(o =>
                {
                    string yiliaozid = o.YILIAOZID;
                    string keshiid = o.KESHIID;
                    var yiliaozu4 = yiliaozu4Rep.GetByID(yiliaozid, keshiid);
                    if (true)
                    {
                        yiliaozu4.Update(o);
                    }                    
                });
            }
            //删除医疗组4信息
            if (eYiLiaoZu4List.GetDeletes().Count > 0)
            {
                eYiLiaoZu4List.GetDeletes().ForEach(o =>
                {
                    string yiliaozid = o.YILIAOZID;
                    string keshiid = o.KESHIID;
                    var yiliaozu4 = yiliaozu4Rep.GetByID(yiliaozid, keshiid);
                    if (true)
                    {
                        yiliaozu4.Delete();
                    }
                });
            }
            UnitOfWork.SaveChanges();
            UnitOfWork.Commit();
            return ServiceContent(true);
        }
        /// <summary>
        /// 保存用户信息
        /// </summary>
        /// <param name="eYongHuXX"></param>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<bool> BaoCunYongHuXX(E_GY_YONGHUXX eYongHuXX)
        {
            UnitOfWork.BeginTransaction();

            var yonghuxxRep = this.GetRepository<IGY_YONGHUXXRepository>(UnitOfWork);
            string yonghuid = eYongHuXX.YONGHUID;
            if (eYongHuXX.GetState() == DTOState.New)
            {                
                var yonghuxx = GY_YONGHUXXFactory.Create(yonghuxxRep, ServiceContext, eYongHuXX);
            }
            if (eYongHuXX.GetState() == DTOState.Update)
            {               
                var yonghuxx = yonghuxxRep.GetByKey(yonghuid);
                if (yonghuxx != null)
                {
                    yonghuxx.Update(eYongHuXX);
                }
            }
            if (eYongHuXX.GetState() == DTOState.Delete)
            {
                var yonghuxx = yonghuxxRep.GetByKey(yonghuid);
                if (yonghuxx != null)
                {
                    yonghuxx.Delete();
                }                
            }

            UnitOfWork.SaveChanges();
            UnitOfWork.Commit();
            return ServiceContent(true);
        }
        /// <summary>
        /// 保存用户信息列表
        /// </summary>
        /// <param name="eYongHuXXList"></param>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<bool> BaoCunYongHuXXList(List<E_GY_YONGHUXX> eYongHuXXList)
        {
            UnitOfWork.BeginTransaction();
            var yonghuxxRep = this.GetRepository<IGY_YONGHUXXRepository>(UnitOfWork);

            if (eYongHuXXList.GetNews().Count > 0)
            {
                eYongHuXXList.GetNews().ForEach(o =>
                {
                    var yonghuxx = GY_YONGHUXXFactory.Create(yonghuxxRep, ServiceContext, o);
                });
            }
            if (eYongHuXXList.GetUpdates().Count > 0)
            {
                eYongHuXXList.GetUpdates().ForEach(o =>
                {
                    var yonghuxx = yonghuxxRep.GetByKey(o.YONGHUID);
                    if (yonghuxx != null)
                    {
                        yonghuxx.Update(o);
                    } 
                });
            }
            if (eYongHuXXList.GetDeletes().Count > 0)
            {
                eYongHuXXList.GetDeletes().ForEach(o =>
                {
                    var yonghuxx = yonghuxxRep.GetByKey(o.YONGHUID);
                    if (yonghuxx != null)
                    {
                        yonghuxx.Delete();
                    }                        
                });
            }


            UnitOfWork.SaveChanges();
            UnitOfWork.Commit();
            return ServiceContent(true);
        }
        /// <summary>
        /// 保存用户信息，职工信息
        /// </summary>
        /// <param name="eYongHuXX"></param>
        /// <param name="eZhiGongXX"></param>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<bool> BaoCunYongHuZGXX(E_GY_YONGHUXX eYongHuXX, E_GY_ZHIGONGXX eZhiGongXX)
        {
            UnitOfWork.BeginTransaction();
            var yonghuxxRep = this.GetRepository<IGY_YONGHUXXRepository>(UnitOfWork);

            var zhigongxxRep = this.GetRepository<IGY_ZHIGONGXXRepository>(UnitOfWork);

            //用户信息
            if (eYongHuXX.GetState() == DTOState.New)
            {
                var yonghuxx = GY_YONGHUXXFactory.Create(yonghuxxRep, ServiceContext, eYongHuXX);
            }
            if (eYongHuXX.GetState() == DTOState.Update)
            {
                var yonghuxx = yonghuxxRep.GetByKey(eYongHuXX.YONGHUID);
                if (yonghuxx != null)
                {
                    yonghuxx.Update(eYongHuXX);
                }
            }
            if (eYongHuXX.GetState() == DTOState.Delete)
            {
                var yonghuxx = yonghuxxRep.GetByKey(eYongHuXX.YONGHUID);
                if (yonghuxx != null)
                    yonghuxx.Delete();
            }

            //职工信息
            if (eZhiGongXX.GetState() == DTOState.New)
            {
                var zhigongxx = GY_ZHIGONGXXFactory.Create(zhigongxxRep, ServiceContext, eZhiGongXX);
            }
            if (eZhiGongXX.GetState() == DTOState.Update)
            {
                string zhigongid = eZhiGongXX.ZHIGONGID;
                var zhigongxx = zhigongxxRep.GetByKey(zhigongid);
                if (zhigongxx!= null)
                {
                    zhigongxx.Update(eZhiGongXX);
                }               
            }
            if (eZhiGongXX.GetState() == DTOState.Delete)
            {
                string zhigongid = eZhiGongXX.ZHIGONGID;
                var zhigongxx = zhigongxxRep.GetByKey(zhigongid);
                if (zhigongxx != null)
                    zhigongxx.Delete();
            }

            UnitOfWork.SaveChanges();
            UnitOfWork.Commit();
            return ServiceContent(true);
        }
        /// <summary>
        /// 保存角色信息
        /// </summary>
        /// <param name="eJueSe"></param>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<bool> BaoCunJueSe(E_GY_JUESE eJueSe)
        {
            UnitOfWork.BeginTransaction();
            var jueseRep = this.GetRepository<IGY_JUESERepository>(UnitOfWork);

            if (eJueSe.GetState() == DTOState.New)
            {
                var juese = GY_JUESEFactory.Create(jueseRep, ServiceContext, eJueSe);
            }
            if (eJueSe.GetState() == DTOState.Update)
            {
                var juese = jueseRep.GetByKey(eJueSe.JUESEID);
                if (juese != null)
                {
                    juese.Update(eJueSe);
                }
            }
            if (eJueSe.GetState() == DTOState.Delete)
            {
                var juese = jueseRep.GetByKey(eJueSe.JUESEID);
                if (juese != null)
                {
                    juese.Delete();
                }              
            }
            UnitOfWork.SaveChanges();
            UnitOfWork.Commit();
            return ServiceContent(true);
        }
        /// <summary>
        /// 保存角色信息列表
        /// </summary>
        /// <param name="eJueSeList"></param>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<bool> BaoCunJueSeList(List<E_GY_JUESE> eJueSeList)
        {
            UnitOfWork.BeginTransaction();

            var jueseRep = this.GetRepository<IGY_JUESERepository>(UnitOfWork);

            if (eJueSeList.GetNews().Count > 0)
            {
                eJueSeList.GetNews().ForEach(o =>
                {
                    var juese = GY_JUESEFactory.Create(jueseRep, ServiceContext, o);                   
                });
            }
            if (eJueSeList.GetUpdates().Count > 0)
            {
                eJueSeList.GetUpdates().ForEach(o =>
                {
                    var juese = jueseRep.GetByKey(o.JUESEID);
                    if (juese != null)
                    {
                        juese.Update(o);
                    }                    
                });
            }
            if (eJueSeList.GetDeletes().Count > 0)
            {
                eJueSeList.GetDeletes().ForEach(o =>
                {
                    var juese = jueseRep.GetByKey(o.JUESEID);
                    if (juese != null)
                    {
                        juese.Delete();
                    }                    
                });
            }

            UnitOfWork.SaveChanges();
            UnitOfWork.Commit();
            return ServiceContent(true);
        }
        /// <summary>
        /// 保存角色用户信息
        /// </summary>
        /// <param name="eJueSeYHEXList"></param>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<bool> BaoCunJueSeYH(List<E_GY_JUESEYH_EX> eJueSeYHEXList)
        {
            UnitOfWork.BeginTransaction();

            var jueseyhRep = this.GetRepository<IGY_JUESEYHRepository>(UnitOfWork);
            //用户角色
            if (eJueSeYHEXList.GetNews().Count > 0)
            {
                eJueSeYHEXList.GetNews().ForEach(o =>
                {
                    E_GY_JUESEYH ejueseyh = o.MapTo<E_GY_JUESEYH>();
                    var jueseyh = GY_JUESEYHFactory.Create(jueseyhRep, ServiceContext, ejueseyh);
                });
            }
            if (eJueSeYHEXList.GetUpdates().Count > 0)
            {
                eJueSeYHEXList.GetUpdates().ForEach(o =>
                {
                    E_GY_JUESEYH ejueseyh = o.MapTo<E_GY_JUESEYH>();//o.EToE<E_GY_JUESEYH_EX, E_GY_JUESEYH>();
                    string jueseid = ejueseyh.JUESEID;
                    string yonghuid = ejueseyh.YONGHUID;
                    var jueseyh = jueseyhRep.GetByID(jueseid, yonghuid);
                    if (jueseyh != null)
                    {
                        jueseyh.Update(ejueseyh);
                    }                    
                });
            }
            if (eJueSeYHEXList.GetDeletes().Count > 0)
            {
                eJueSeYHEXList.GetDeletes().ForEach(o =>
                {
                    E_GY_JUESEYH ejueseyh = o.MapTo<E_GY_JUESEYH>();// o.EToE<E_GY_JUESEYH_EX, E_GY_JUESEYH>();
                    string jueseid = ejueseyh.JUESEID;
                    string yonghuid = ejueseyh.YONGHUID;
                    var jueseyh = jueseyhRep.GetByID(jueseid, yonghuid);
                    if (jueseyh != null)
                    {
                        jueseyh.Delete();
                    }
                        
                });
            }

            UnitOfWork.SaveChanges();
            UnitOfWork.Commit();
            return ServiceContent(true);
        }
        /// <summary>
        /// 保存 用户角色 应用
        /// </summary>
        /// <param name="eJueSeYHEXList"></param>
        /// <param name="eYongHuYYEXList"></param>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<bool> BaoCunYongHuJSYY(List<E_GY_JUESEYH_EX> eJueSeYHEXList, List<E_GY_YONGHUYY_EX> eYongHuYYEXList)
        {
            UnitOfWork.BeginTransaction();

            var jueseyhRep = this.GetRepository<IGY_JUESEYHRepository>(UnitOfWork);            
            //用户角色
            if (eJueSeYHEXList.GetNews().Count > 0)
            {
                eJueSeYHEXList.GetNews().ForEach(o =>
                {
                    E_GY_JUESEYH ejueseyh = o.MapTo<E_GY_JUESEYH>(); //o.EToE<E_GY_JUESEYH_EX, E_GY_JUESEYH>();
                    var jueseyh = GY_JUESEYHFactory.Create(jueseyhRep, ServiceContext, ejueseyh);
                });
            }
            if (eJueSeYHEXList.GetUpdates().Count > 0)
            {
                eJueSeYHEXList.GetUpdates().ForEach(o =>
                {
                    E_GY_JUESEYH ejueseyh = o.MapTo<E_GY_JUESEYH>(); //o.EToE<E_GY_JUESEYH_EX, E_GY_JUESEYH>();
                    string jueseid = ejueseyh.JUESEID;
                    string yonghuid = ejueseyh.YONGHUID;
                    var jueseyh = jueseyhRep.GetByID(jueseid, yonghuid);
                    if (jueseyh != null)
                    {
                        jueseyh.Update(ejueseyh);
                    }                    
                });
            }
            if (eJueSeYHEXList.GetDeletes().Count > 0)
            {
                eJueSeYHEXList.GetDeletes().ForEach(o =>
                {
                    E_GY_JUESEYH ejueseyh = o.MapTo<E_GY_JUESEYH>(); //o.EToE<E_GY_JUESEYH_EX, E_GY_JUESEYH>();
                    string jueseid = ejueseyh.JUESEID;
                    string yonghuid = ejueseyh.YONGHUID;
                    var jueseyh = jueseyhRep.GetByID(jueseid, yonghuid);
                    if (jueseyh != null)
                    {
                        jueseyh.Delete();
                    }                        
                });
            }

            var yonghuyyRep = this.GetRepository<IGY_YONGHUYYRepository>(UnitOfWork);
            //用户应用
            if (eYongHuYYEXList.GetNews().Count > 0)
            {
                eYongHuYYEXList.GetNews().ForEach(o =>
                {
                    E_GY_YONGHUYY eyonghuyy = o.MapTo<E_GY_YONGHUYY>(); //o.EToE<E_GY_YONGHUYY_EX, E_GY_YONGHUYY>();
                    var yonghuyy = GY_YONGHUYYFactory.Create(yonghuyyRep, ServiceContext, eyonghuyy);                   
                });
            }
            if (eYongHuYYEXList.GetUpdates().Count > 0)
            {
                eYongHuYYEXList.GetUpdates().ForEach(o =>
                {
                    E_GY_YONGHUYY eyonghuyy = o.MapTo<E_GY_YONGHUYY>(); //o.EToE<E_GY_YONGHUYY_EX, E_GY_YONGHUYY>();
                    string yonghuid = eyonghuyy.YONGHUID;
                    string yingyongid = eyonghuyy.YINGYONGID;
                    var jueseyy = yonghuyyRep.GetByID(yonghuid, yingyongid);
                    if (jueseyy != null)
                    {
                        jueseyy.Update(eyonghuyy);
                    }       
                });
            }
            if (eYongHuYYEXList.GetDeletes().Count > 0)
            {
                eYongHuYYEXList.GetDeletes().ForEach(o =>
                {
                    E_GY_YONGHUYY eyonghuyy = o.MapTo<E_GY_YONGHUYY>(); //o.EToE<E_GY_YONGHUYY_EX, E_GY_YONGHUYY>();
                    string yonghuid = eyonghuyy.YONGHUID;
                    string yingyongid = eyonghuyy.YINGYONGID;
                    var jueseyy = yonghuyyRep.GetByID(yonghuid, yingyongid);
                    if (jueseyy != null)
                    {
                        jueseyy.Delete();
                    }                        
                });
            }            

            UnitOfWork.SaveChanges();
            UnitOfWork.Commit();
            return ServiceContent(true);
        }
        /// <summary>
        /// 保存 角色权限列表信息
        /// </summary>
        /// <param name="eJueSeQXList"></param>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<bool> BaoCunJueSeQX(List<E_GY_JUESEQX> eJueSeQXList)
        {
            UnitOfWork.BeginTransaction();
            var jueseqxRep = this.GetRepository<IGY_JUESEQXRepository>(UnitOfWork);

            if (eJueSeQXList.GetNews().Count > 0)
            {
                eJueSeQXList.GetNews().ForEach(o =>
                {
                    var jueseqx = GY_JUESEQXFactory.Create(jueseqxRep, ServiceContext, o);
                    //jueseqx.Insert();
                });
            }
            if (eJueSeQXList.GetUpdates().Count > 0)
            {
                eJueSeQXList.GetUpdates().ForEach(o =>
                {
                    string jueseid = o.JUESEID;
                    string quanxianid = o.QUANXIANID;
                    var jueseqx = jueseqxRep.GetByID(jueseid, quanxianid);
                    if (jueseqx != null)
                    {
                        jueseqx.Update(o);
                    }                    
                });
            }
            if (eJueSeQXList.GetDeletes().Count > 0)
            {
                eJueSeQXList.GetDeletes().ForEach(o =>
                {
                    string jueseid = o.JUESEID;
                    string quanxianid = o.QUANXIANID;
                    var jueseqx = jueseqxRep.GetByID(jueseid, quanxianid);
                    if (jueseqx != null)
                    {
                        jueseqx.Delete();
                    }
                });
            }
            
            UnitOfWork.SaveChanges();
            UnitOfWork.Commit();
            return ServiceContent(true);
        }




        //权限  相关的
        /// <summary>
        /// 保存权限信息
        /// </summary>
        /// <param name="eQuanXian"></param>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<bool> BaoCunQuanXian(E_GY_QUANXIAN eQuanXian)
        {
            UnitOfWork.BeginTransaction();
            var quanxianRep = this.GetRepository<IGY_QUANXIANRepository>(UnitOfWork);

            if (eQuanXian.GetState() == DTOState.New)
            {
                var quanxian = GY_QUANXIANFactory.Create(quanxianRep, ServiceContext, eQuanXian);
                //quanxian.Insert();
            }
            if (eQuanXian.GetState() == DTOState.Update)
            {
                string quanxianid = eQuanXian.QUANXIANID;
                var quanxian = quanxianRep.GetByKey(quanxianid);
                quanxian.Update(eQuanXian);
            }
            if (eQuanXian.GetState() == DTOState.Delete)
            {
                string quanxianid = eQuanXian.QUANXIANID;
                var quanxian = quanxianRep.GetByKey(quanxianid);
                quanxian.Delete();
            }                
           
            UnitOfWork.SaveChanges();
            UnitOfWork.Commit();
            return ServiceContent(true);
        }
        /// <summary>
        /// 保存权限列表
        /// </summary>
        /// <param name="eQuanXianList"></param>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<bool> BaoCunQuanXianList(List<E_GY_QUANXIAN> eQuanXianList)
        {
            UnitOfWork.BeginTransaction();
            
            eQuanXianList.ForEach(o =>
            {
                BaoCunQuanXian(o);
            });
            
            UnitOfWork.SaveChanges();
            UnitOfWork.Commit();
            return ServiceContent(true);
        }
        ///// <summary>
        ///// 保存职工信息
        ///// </summary>
        ///// <param name="eZhiGongXX"></param>
        ///// <returns></returns>
        //public Result<bool> SaveZhiGongXX(List<E_GY_ZHIGONGXX> eZhiGongXX)
        //{

        //    using (var trans = DBContext.Database.BeginTransaction())
        //    {
        //        ///1先保存职工信息
        //        if (eZhiGongXX.GetNews().Count > 0)
        //        {
        //            eZhiGongXX.GetNews().ForEach(o =>
        //            {
        //                var zhigongxx = GYZhiGongDomain.Create(DBContext, ServiceContext, o);
        //                //zhigongxx.Insert();

        //                //新增职工信息的同时 也是要新增用户信息
        //                NewYongHuXX(o);
        //            });
        //        }
        //        //更新职工信息
        //        if (eZhiGongXX.GetUpdates().Count > 0)
        //        {
        //            eZhiGongXX.GetUpdates().ForEach(o =>
        //            {
        //                var zhigongxx = GYZhiGongDomain.GetByID(DBContext, ServiceContext,o);
        //                zhigongxx.Update(o);
        //            });
        //        }
        //        //删除职工信息
        //        if (eZhiGongXX.GetDeletes().Count > 0)
        //        {
        //            eZhiGongXX.GetDeletes().ForEach(o =>
        //            {
        //                var zhigongxx = GYZhiGongDomain.GetByID(DBContext, ServiceContext, o);
        //                zhigongxx.Delete();
        //            });
        //        }
        //        DBContext.SaveChanges();
        //        //提交
        //        trans.Commit();
        //    }
        //    return new Result<bool>(ReturnCode.SUCCESS, "保存职工信息成功！");
        //}

        //private void NewYongHuXX(E_GY_ZHIGONGXX o)
        //{
        //    E_GY_YONGHUXX eYongHuXX = new E_GY_YONGHUXX();
        //    eYongHuXX.YONGHUID = o.ZHIGONGID;
        //    eYongHuXX.YONGHUXM = o.ZHIGONGXM;
        //    eYongHuXX.TINGYONGBZ = 0;
        //    eYongHuXX.MIMA = null;
        //    eYongHuXX.SHURUMA = "SHURUMA1";
        //    eYongHuXX.XIUGAIREN = ServiceContext.USERID;
        //    eYongHuXX.XIUGAISJ = new QueryService(DBContext).GetSYSDate();
        //    eYongHuXX.State = DTOState.New;
        //    var yonghuxx = GYYongHuXXDomain.Create(DBContext, ServiceContext, eYongHuXX);
        //    //yonghuxx.Insert();
        //}

        ///// <summary>
        ///// 保存职工信息, 职工科室信息
        ///// </summary>
        ///// <param name="eZhiGongXX"></param>
        ///// <param name="eZhiGongKS"></param>
        ///// <returns></returns>
        //public Result<bool> SaveZhiGongXX(E_GY_ZHIGONGXX eZhiGongXX, List<E_GY_ZHIGONGKS> eZhiGongKS)
        //{
        //    using (var trans = DBContext.Database.BeginTransaction())
        //    {
        //        ///1先保存职工信息
        //        if (eZhiGongXX.State == DTOState.New)
        //        {
        //            var zhigongxx = GYZhiGongDomain.Create(DBContext, ServiceContext, eZhiGongXX);
        //            //zhigongxx.Insert();

        //            //新增职工信息的同时 也是要新增用户信息
        //            NewYongHuXX(eZhiGongXX);
        //        }
        //        //更新职工信息
        //        if (eZhiGongXX.State == DTOState.Update)
        //        {
        //            var zhigongxx = GYZhiGongDomain.GetByID(DBContext, ServiceContext, eZhiGongXX);
        //            zhigongxx.Update(eZhiGongXX);
        //        }

        //        //2再保存职工科室信息
        //        if (eZhiGongKS.GetNews().Count > 0)
        //        {
        //            eZhiGongKS.GetNews().ForEach(o =>
        //            {
        //                var zhigongks = GYZhiGongKSDomain.Create(DBContext, ServiceContext, o);
        //                //zhigongks.Insert();
        //            });
        //        }
        //        //删除职工科室
        //        if (eZhiGongKS.GetDeletes().Count > 0)
        //        {
        //            eZhiGongKS.GetDeletes().ForEach(o =>
        //            {
        //                var zhigongks = GYZhiGongKSDomain.GetByID(DBContext, ServiceContext, o);
        //                zhigongks.Delete();
        //            });
        //        }

        //        //更新职工科室
        //        if (eZhiGongKS.GetUpdates().Count > 0)
        //        {
        //            eZhiGongKS.GetUpdates().ForEach(o =>
        //            {
        //                var zhigongks = GYZhiGongKSDomain.GetByID(DBContext, ServiceContext, o);
        //                zhigongks.Update(o);
        //            });
        //        }
        //        DBContext.SaveChanges();
        //        //提交
        //        trans.Commit();
        //    }
        //    return new Result<bool>(ReturnCode.SUCCESS, "保存职工相关信息成功！");
        //}
        ///// <summary>
        ///// 保存职工信息(用户信息)，角色，职工科室
        ///// </summary>
        ///// <param name = "eZhiGongXX" ></ param >
        ///// < param name="eJueSeYH"></param>
        ///// <param name = "eZhiGongKS" ></ param >
        ///// < returns ></ returns >
        //public Result<bool> SaveZhiGongXX(E_GY_ZHIGONGXX eZhiGongXX, List<E_GY_JUESEYH> eJueSeYH, List<E_GY_ZHIGONGKS> eZhiGongKS)
        //{
        //    using (var trans = DBContext.Database.BeginTransaction())
        //    {
        //        ///1先保存职工信息                
        //        if (eZhiGongXX.State == DTOState.New)
        //        {
        //            var zhigongxx = GYZhiGongDomain.Create(DBContext, ServiceContext, eZhiGongXX);
        //            //zhigongxx.Insert();

        //            //新增职工信息的同时 也是要新增用户信息
        //            NewYongHuXX(eZhiGongXX);
        //        }
        //        //更新职工信息
        //        if (eZhiGongXX.State == DTOState.Update)
        //        {
        //            var zhigongxx = GYZhiGongDomain.GetByID(DBContext, ServiceContext, eZhiGongXX);
        //            zhigongxx.Update(eZhiGongXX);
        //        }
        //        //2 保存角色用户
        //        if (eJueSeYH.GetNews().Count > 0)
        //        {
        //            eJueSeYH.GetNews().ForEach(o =>
        //            {
        //                var jueseyh = GYJueSeYHDomain.Create(DBContext, ServiceContext, o);
        //                //jueseyh.Insert();
        //            });
        //        }
        //        //删除
        //        if (eJueSeYH.GetDeletes().Count > 0)
        //        {
        //            eJueSeYH.GetDeletes().ForEach(o =>
        //            {
        //                var jueseyh = GYJueSeYHDomain.GetByID(DBContext, ServiceContext, o);
        //                jueseyh.Delete();
        //            });
        //        }
        //        //更新
        //        if (eJueSeYH.GetUpdates().Count > 0)
        //        {
        //            eJueSeYH.GetUpdates().ForEach(o =>
        //            {
        //                var jueseyh = GYJueSeYHDomain.GetByID(DBContext, ServiceContext, o);
        //                jueseyh.Update(o);
        //            });
        //        }

        //        //3再保存职工科室信息
        //        if (eZhiGongKS.GetNews().Count > 0)
        //        {
        //            eZhiGongKS.GetNews().ForEach(o =>
        //            {
        //                var zhigongks = GYZhiGongKSDomain.Create(DBContext, ServiceContext, o);
        //                //zhigongks.Insert();
        //            });
        //        }
        //        //删除职工科室
        //        if (eZhiGongKS.GetDeletes().Count > 0)
        //        {
        //            eZhiGongKS.GetDeletes().ForEach(o =>
        //            {
        //                var zhigongks = GYZhiGongKSDomain.GetByID(DBContext, ServiceContext, o);
        //                zhigongks.Delete();
        //            });
        //        }

        //        //更新职工科室
        //        if (eZhiGongKS.GetUpdates().Count > 0)
        //        {
        //            eZhiGongKS.GetUpdates().ForEach(o =>
        //            {
        //                var zhigongks = GYZhiGongKSDomain.GetByID(DBContext, ServiceContext, o);
        //                zhigongks.Update(o);
        //            });
        //        }

        //        DBContext.SaveChanges();
        //        //提交
        //        trans.Commit();
        //    }
        //    return new Result<bool>( ReturnCode.SUCCESS,"保存职工相关信息成功！");
        //}

        ///// <summary>
        ///// 保存医疗组信息
        ///// </summary>
        ///// <param name="eYiLiaoZu1"></param>
        ///// <returns></returns>
        //public Result<bool> SaveYiLiaoZuXX(List<E_GY_YILIAOZU1> eYiLiaoZu1)
        //{
        //    using (var trans = DBContext.Database.BeginTransaction())
        //    {
        //        ///1先保存职工信息
        //        if (eYiLiaoZu1.GetNews().Count > 0)
        //        {
        //            eYiLiaoZu1.GetNews().ForEach(o =>
        //            {
        //                var yiliaozu1 = GYYiLiaoZuDomain.Create(DBContext, ServiceContext, o);
        //                //yiliaozu1.Insert();
        //            });
        //        }
        //        //更新职工信息
        //        if (eYiLiaoZu1.GetUpdates().Count > 0)
        //        {
        //            eYiLiaoZu1.GetUpdates().ForEach(o =>
        //            {
        //                var yiliaozu1 = GYYiLiaoZuDomain.GetByID(DBContext, ServiceContext, o.YILIAOZID);
        //                yiliaozu1.Update(o);
        //            });
        //        }
        //        //删除职工信息
        //        if (eYiLiaoZu1.GetDeletes().Count > 0)
        //        {
        //            eYiLiaoZu1.GetDeletes().ForEach(o =>
        //            {
        //                var yiliaozu1 = GYYiLiaoZuDomain.GetByID(DBContext, ServiceContext, o.YILIAOZID);
        //                yiliaozu1.Delete();
        //            });
        //        }
        //        DBContext.SaveChanges();
        //        //提交
        //        trans.Commit();
        //    }
        //    return new Result<bool>(ReturnCode.SUCCESS, "保存医疗组信息成功！");
        //}
        ///// <summary>
        ///// 保存医疗组1信息，医疗组2信息，医疗组4信息
        ///// </summary>
        ///// <param name="eYiLiaoZu1"></param>
        ///// <param name="eYiLiaoZu2"></param>
        ///// <param name="eYiLiaoZu4"></param>
        ///// <returns></returns>       
        //public Result<bool> SaveYiLiaoZuXX(E_GY_YILIAOZU1 eYiLiaoZu1, List<E_GY_YILIAOZU2> eYiLiaoZu2, List<E_GY_YILIAOZU4> eYiLiaoZu4)
        //{
        //    using (var trans = DBContext.Database.BeginTransaction())
        //    {
        //        ///1先保存医疗组1信息
        //        if (eYiLiaoZu1.State == DTOState.New)
        //        {
        //           var yiliaozu1 = GYYiLiaoZuDomain.Create(DBContext, ServiceContext, eYiLiaoZu1);
        //           //yiliaozu1.Insert();                   
        //        }
        //        //更新医疗组1信息
        //        if (eYiLiaoZu1.State == DTOState.Update)
        //        {
        //            var yiliaozu1 = GYYiLiaoZuDomain.GetByID(DBContext, ServiceContext, eYiLiaoZu1.YILIAOZID);
        //            yiliaozu1.Update(eYiLiaoZu1);
        //        }
        //        //删除医疗组1信息
        //        if (eYiLiaoZu1.State == DTOState.Delete)
        //        {
        //            var yiliaozu1 = GYYiLiaoZuDomain.GetByID(DBContext, ServiceContext, eYiLiaoZu1.YILIAOZID);
        //            yiliaozu1.Delete();
        //        }
        //        //2先保存医疗组2信息
        //        if (eYiLiaoZu2.GetNews().Count > 0)
        //        {
        //            eYiLiaoZu2.GetNews().ForEach(o =>
        //            {
        //                var yiliaozu2 = GYYiLiaoZuYSDomain.Create(DBContext, ServiceContext, o);
        //                //yiliaozu2.Insert();
        //            });
        //        }
        //        //更新医疗组2信息
        //        if (eYiLiaoZu2.GetUpdates().Count > 0)
        //        {
        //            eYiLiaoZu2.GetUpdates().ForEach(o =>
        //            {
        //                var yiliaozu2 = GYYiLiaoZuYSDomain.GetByID(DBContext, ServiceContext, o);
        //                yiliaozu2.Update(o);
        //            });
        //        }
        //        //删除医疗组2信息
        //        if (eYiLiaoZu2.GetDeletes().Count > 0)
        //        {
        //            eYiLiaoZu2.GetDeletes().ForEach(o =>
        //            {
        //                var yiliaozu2 = GYYiLiaoZuYSDomain.GetByID(DBContext, ServiceContext, o);
        //                yiliaozu2.Delete();
        //            });
        //        }
        //        ///3先保存医疗组4信息
        //        if (eYiLiaoZu4.GetNews().Count > 0)
        //        {
        //            eYiLiaoZu4.GetNews().ForEach(o =>
        //            {
        //                var yiliaozu4 = GYYiLiaoZuKSDomain.Create(DBContext, ServiceContext, o);
        //                //yiliaozu4.Insert();
        //            });
        //        }
        //        //更新医疗组4信息
        //        if (eYiLiaoZu4.GetUpdates().Count > 0)
        //        {
        //            eYiLiaoZu4.GetUpdates().ForEach(o =>
        //            {
        //                var yiliaozu4 = GYYiLiaoZuKSDomain.GetByID(DBContext, ServiceContext, o);
        //                yiliaozu4.Update(o);
        //            });
        //        }
        //        //删除医疗组4信息
        //        if (eYiLiaoZu4.GetDeletes().Count > 0)
        //        {
        //            eYiLiaoZu4.GetDeletes().ForEach(o =>
        //            {
        //                var yiliaozu4 = GYYiLiaoZuKSDomain.GetByID(DBContext, ServiceContext, o);
        //                yiliaozu4.Delete();
        //            });
        //        }
        //        DBContext.SaveChanges();
        //        //提交
        //        trans.Commit();
        //    }
        //    return new Result<bool>(ReturnCode.SUCCESS, "保存医疗组信息成功！");
        //}
        //public Result<bool> SaveYiLiaoZuXX(List<E_GY_YILIAOZU1> eYiLiaoZu1, List<E_GY_YILIAOZU2> eYiLiaoZu2, List<E_GY_YILIAOZU4> eYiLiaoZu4)
        //{
        //    using (var trans = DBContext.Database.BeginTransaction())
        //    {
        //        ///1先保存医疗组1信息
        //        if (eYiLiaoZu1.GetNews().Count > 0)
        //        {
        //            eYiLiaoZu1.GetNews().ForEach(o =>
        //            {
        //                var yiliaozu1 = GYYiLiaoZuDomain.Create(DBContext, ServiceContext, o);
        //                //yiliaozu1.Insert();
        //            });
        //        }
        //        //更新医疗组1信息
        //        if (eYiLiaoZu1.GetUpdates().Count > 0)
        //        {
        //            eYiLiaoZu1.GetUpdates().ForEach(o =>
        //            {
        //                var yiliaozu1 = GYYiLiaoZuDomain.GetByID(DBContext, ServiceContext, o.YILIAOZID);
        //                yiliaozu1.Update(o);
        //            });
        //        }
        //        //删除医疗组1信息
        //        if (eYiLiaoZu1.GetDeletes().Count > 0)
        //        {
        //            eYiLiaoZu1.GetDeletes().ForEach(o =>
        //            {
        //                var yiliaozu1 = GYYiLiaoZuDomain.Create(DBContext, ServiceContext, o);
        //                yiliaozu1.Delete();
        //            });
        //        }
        //        //2先保存医疗组2信息
        //        if (eYiLiaoZu2.GetNews().Count > 0)
        //        {
        //            eYiLiaoZu2.GetNews().ForEach(o =>
        //            {
        //                var yiliaozu2 = GYYiLiaoZuYSDomain.Create(DBContext, ServiceContext, o);
        //                //yiliaozu2.Insert();
        //            });
        //        }
        //        //更新医疗组2信息
        //        if (eYiLiaoZu2.GetUpdates().Count > 0)
        //        {
        //            eYiLiaoZu2.GetUpdates().ForEach(o =>
        //            {
        //                var yiliaozu2 = GYYiLiaoZuYSDomain.GetByID(DBContext, ServiceContext, o);
        //                yiliaozu2.Update(o);
        //            });
        //        }
        //        //删除医疗组2信息
        //        if (eYiLiaoZu2.GetDeletes().Count > 0)
        //        {
        //            eYiLiaoZu2.GetDeletes().ForEach(o =>
        //            {
        //                var yiliaozu2 = GYYiLiaoZuYSDomain.GetByID(DBContext, ServiceContext, o);
        //                yiliaozu2.Delete();
        //            });
        //        }
        //        ///3先保存医疗组4信息
        //        if (eYiLiaoZu4.GetNews().Count > 0)
        //        {
        //            eYiLiaoZu4.GetNews().ForEach(o =>
        //            {
        //                var yiliaozu4 = GYYiLiaoZuKSDomain.Create(DBContext, ServiceContext, o);
        //                //yiliaozu4.Insert();
        //            });
        //        }
        //        //更新医疗组4信息
        //        if (eYiLiaoZu4.GetUpdates().Count > 0)
        //        {
        //            eYiLiaoZu4.GetUpdates().ForEach(o =>
        //            {
        //                var yiliaozu4 = GYYiLiaoZuKSDomain.GetByID(DBContext, ServiceContext, o);
        //                yiliaozu4.Update(o);
        //            });
        //        }
        //        //删除医疗组2信息
        //        if (eYiLiaoZu4.GetDeletes().Count > 0)
        //        {
        //            eYiLiaoZu4.GetDeletes().ForEach(o =>
        //            {
        //                var yiliaozu4 = GYYiLiaoZuKSDomain.GetByID(DBContext, ServiceContext, o);
        //                yiliaozu4.Delete();
        //            });
        //        }
        //        DBContext.SaveChanges();
        //        //提交
        //        trans.Commit();
        //    }
        //    return new Result<bool>(ReturnCode.SUCCESS, "保存医疗组信息成功！");
        //}
        ///// <summary>
        ///// 保存用户信息
        ///// </summary>
        ///// <param name="eYongHuXX"></param>
        ///// <returns></returns>
        //public Result<bool> SaveYongHuXX(E_GY_YONGHUXX eYongHuXX)
        //{
        //    using (var trans = DBContext.Database.BeginTransaction())
        //    {
        //        if (eYongHuXX.State == DTOState.New)
        //        {
        //            var yonghuxx = GYYongHuXXDomain.Create(DBContext, ServiceContext, eYongHuXX);
        //            //yonghuxx.Insert();
        //        }
        //        if (eYongHuXX.State == DTOState.Update)
        //        {
        //            var yonghuxx = GYYongHuXXDomain.GetByID(DBContext, ServiceContext, eYongHuXX);
        //            yonghuxx.Update(eYongHuXX);
        //        }
        //        if (eYongHuXX.State == DTOState.Delete)
        //        {
        //            var yonghuxx = GYYongHuXXDomain.GetByID(DBContext, ServiceContext, eYongHuXX);
        //            yonghuxx.Delete();
        //        }
        //        DBContext.SaveChanges();
        //        //提交
        //        trans.Commit();
        //    }           
        //    return new Result<bool>(ReturnCode.SUCCESS, "保存用户信息成功！");
        //}
        //public Result<bool> SaveYongHuXX(List<E_GY_YONGHUXX> eYongHuXXList)
        //{
        //    using (var trans = DBContext.Database.BeginTransaction())
        //    {
        //        if (eYongHuXXList.GetNews().Count > 0)
        //        {
        //            eYongHuXXList.GetNews().ForEach(o =>
        //            {
        //                var yonghuxx = GYYongHuXXDomain.Create(DBContext, ServiceContext, o);
        //                //yonghuxx.Insert();
        //            });                     
        //        }
        //        if (eYongHuXXList.GetUpdates().Count > 0)
        //        {
        //            eYongHuXXList.GetUpdates().ForEach(o =>
        //            {
        //                var yonghuxx = GYYongHuXXDomain.GetByID(DBContext, ServiceContext, o);
        //                yonghuxx.Update(o);
        //            });
        //        }
        //        if (eYongHuXXList.GetDeletes().Count > 0)
        //        {
        //            eYongHuXXList.GetDeletes().ForEach(o =>
        //            {
        //                var yonghuxx = GYYongHuXXDomain.GetByID(DBContext, ServiceContext, o);
        //                yonghuxx.Delete();
        //            });
        //        }
        //        DBContext.SaveChanges();
        //        //提交
        //        trans.Commit();
        //    }
        //    return new Result<bool>(ReturnCode.SUCCESS, "保存用户信息成功！");
        //}
        ///// <summary>
        ///// 保存 用户，职工信息
        ///// </summary>
        ///// <param name="eYongHuXX"></param>
        ///// <param name="eZhiGongXX"></param>
        ///// <returns></returns>
        //public Result<bool> SaveYongHuZGXX(E_GY_YONGHUXX eYongHuXX,E_GY_ZHIGONGXX eZhiGongXX)
        //{
        //    using (var trans = DBContext.Database.BeginTransaction())
        //    {
        //        //用户信息
        //        if (eYongHuXX.State == DTOState.New)
        //        {
        //            var yonghuxx = GYYongHuXXDomain.Create(DBContext, ServiceContext, eYongHuXX);
        //            //yonghuxx.Insert();
        //        }
        //        if (eYongHuXX.State == DTOState.Update)
        //        {
        //            var yonghuxx = GYYongHuXXDomain.GetByID(DBContext, ServiceContext, eYongHuXX);
        //            yonghuxx.Update(eYongHuXX);
        //        }
        //        if (eYongHuXX.State == DTOState.Delete)
        //        {
        //            var yonghuxx = GYYongHuXXDomain.GetByID(DBContext, ServiceContext, eYongHuXX);
        //            yonghuxx.Delete();
        //        }

        //        //职工信息
        //        if (eZhiGongXX.State == DTOState.New)
        //        {
        //            var zhigongxx = GYZhiGongDomain.Create(DBContext, ServiceContext, eZhiGongXX);
        //            //zhigongxx.Insert();
        //        }
        //        if (eZhiGongXX.State == DTOState.Update)
        //        {
        //            var zhigongxx = GYZhiGongDomain.GetByID(DBContext, ServiceContext, eZhiGongXX);
        //            zhigongxx.Update(eZhiGongXX);
        //        }
        //        if (eZhiGongXX.State == DTOState.Delete)
        //        {
        //            var zhigongxx = GYZhiGongDomain.GetByID(DBContext, ServiceContext, eZhiGongXX);
        //            zhigongxx.Delete();
        //        }
        //        DBContext.SaveChanges();
        //        //提交
        //        trans.Commit();
        //    }
        //    return new Result<bool>(ReturnCode.SUCCESS, "保存用户信息成功！");
        //}

        ////角色相关的
        //public Result<bool> SaveJueSe(E_GY_JUESE eJueSe)
        //{
        //    using (var trans = DBContext.Database.BeginTransaction())
        //    {
        //        if (eJueSe.State == DTOState.New)
        //        {
        //            var juese = GYJueSeDomain.Create(DBContext, ServiceContext, eJueSe);
        //            //juese.Insert();                   
        //        }
        //        if (eJueSe.State == DTOState.Update)
        //        {
        //           var juese = GYJueSeDomain.GetByID(DBContext, ServiceContext, eJueSe.JUESEID);
        //           juese.Update(eJueSe);                    
        //        }
        //        if (eJueSe.State == DTOState.Delete)
        //        {
        //            var juese = GYJueSeDomain.GetByID(DBContext, ServiceContext, eJueSe.JUESEID);
        //            juese.Delete();

        //        }
        //        DBContext.SaveChanges();
        //        //提交
        //        trans.Commit();
        //    }
        //    return new Result<bool>(ReturnCode.SUCCESS, "保存角色信息成功！");
        //}
        //public Result<bool> SaveJueSe(List<E_GY_JUESE> eJueSeList)
        //{
        //    using (var trans = DBContext.Database.BeginTransaction())
        //    {
        //        if (eJueSeList.GetNews().Count > 0)
        //        {
        //            eJueSeList.GetNews().ForEach(o =>
        //            {
        //                var juese = GYJueSeDomain.Create(DBContext, ServiceContext, o);
        //                //juese.Insert();
        //            });
        //        }
        //        if (eJueSeList.GetUpdates().Count > 0)
        //        {
        //            eJueSeList.GetUpdates().ForEach(o =>
        //            {
        //                var juese = GYJueSeDomain.GetByID(DBContext, ServiceContext, o.JUESEID);
        //                juese.Update(o);
        //            });
        //        }
        //        if (eJueSeList.GetDeletes().Count > 0)
        //        {
        //            eJueSeList.GetDeletes().ForEach(o =>
        //            {
        //                var juese = GYJueSeDomain.GetByID(DBContext, ServiceContext, o.JUESEID);
        //                juese.Delete();
        //            });
        //        }
        //        DBContext.SaveChanges();
        //        //提交
        //        trans.Commit();
        //    }
        //    return new Result<bool>(ReturnCode.SUCCESS, "保存角色信息成功！");
        //}
        //public Result<bool> SaveJueSeYH(List<E_GY_JUESEYH_EX> eJueSeYHEXList)
        //{
        //    using (var trans = DBContext.Database.BeginTransaction())
        //    {
        //        //用户角色
        //        if (eJueSeYHEXList.GetNews().Count > 0)
        //        {
        //            eJueSeYHEXList.GetNews().ForEach(o =>
        //            {
        //                E_GY_JUESEYH ejueseyh = o.MapTo<E_GY_JUESEYH>();//o.EToE<E_GY_JUESEYH_EX, E_GY_JUESEYH>();
        //                var jueseyh = GYJueSeYHDomain.Create(DBContext, ServiceContext, ejueseyh);
        //                //jueseyh.Insert();
        //            });
        //        }
        //        if (eJueSeYHEXList.GetUpdates().Count > 0)
        //        {
        //            eJueSeYHEXList.GetUpdates().ForEach(o =>
        //            {
        //                E_GY_JUESEYH ejueseyh = o.MapTo<E_GY_JUESEYH>();//o.EToE<E_GY_JUESEYH_EX, E_GY_JUESEYH>();
        //                var jueseyh = GYJueSeYHDomain.GetByID(DBContext, ServiceContext, ejueseyh);
        //                jueseyh.Update(ejueseyh);
        //            });
        //        }
        //        if (eJueSeYHEXList.GetDeletes().Count > 0)
        //        {
        //            eJueSeYHEXList.GetDeletes().ForEach(o =>
        //            {
        //                E_GY_JUESEYH ejueseyh = o.MapTo<E_GY_JUESEYH>();// o.EToE<E_GY_JUESEYH_EX, E_GY_JUESEYH>();
        //                var jueseyh = GYJueSeYHDomain.GetByID(DBContext, ServiceContext, ejueseyh);
        //                jueseyh.Delete();
        //            });
        //        }
        //        DBContext.SaveChanges();
        //        //提交
        //        trans.Commit();
        //    }
        //    return new Result<bool>(ReturnCode.SUCCESS, "保存角色用户信息成功！");
        //}

        /////用户角色 应用相关的
        //public Result<bool> SaveYongHuJSYY(List<E_GY_JUESEYH_EX> eJueSeYHEXList,List<E_GY_YONGHUYY_EX> eYongHuYYEXList)
        //{
        //    using (var trans = DBContext.Database.BeginTransaction())
        //    {
        //        //用户角色
        //        if (eJueSeYHEXList.GetNews().Count > 0)
        //        {
        //            eJueSeYHEXList.GetNews().ForEach(o =>
        //            {
        //                E_GY_JUESEYH ejueseyh = o.MapTo<E_GY_JUESEYH>(); //o.EToE<E_GY_JUESEYH_EX, E_GY_JUESEYH>();
        //                var jueseyh = GYJueSeYHDomain.Create(DBContext, ServiceContext, ejueseyh);
        //                //jueseyh.Insert();
        //            });
        //        }
        //        if (eJueSeYHEXList.GetUpdates().Count > 0)
        //        {
        //            eJueSeYHEXList.GetUpdates().ForEach(o =>
        //            {
        //                E_GY_JUESEYH ejueseyh = o.MapTo<E_GY_JUESEYH>(); //o.EToE<E_GY_JUESEYH_EX, E_GY_JUESEYH>();
        //                var jueseyh = GYJueSeYHDomain.GetByID(DBContext, ServiceContext, ejueseyh);
        //                jueseyh.Update(ejueseyh);
        //            });
        //        }
        //        if (eJueSeYHEXList.GetDeletes().Count > 0)
        //        {
        //            eJueSeYHEXList.GetDeletes().ForEach(o =>
        //            {
        //                E_GY_JUESEYH ejueseyh = o.MapTo<E_GY_JUESEYH>(); //o.EToE<E_GY_JUESEYH_EX, E_GY_JUESEYH>();
        //                var jueseyh = GYJueSeYHDomain.GetByID(DBContext, ServiceContext, ejueseyh);
        //                jueseyh.Delete();
        //            });
        //        }
        //        //用户应用
        //        if (eYongHuYYEXList.GetNews().Count > 0)
        //        {
        //            eYongHuYYEXList.GetNews().ForEach(o =>
        //            {
        //                E_GY_YONGHUYY eyonghuyy = o.MapTo<E_GY_YONGHUYY>(); //o.EToE<E_GY_YONGHUYY_EX, E_GY_YONGHUYY>();
        //                var yonghuyy = GYYongHuYYDomain.Create(DBContext, ServiceContext, eyonghuyy);
        //                //yonghuyy.Insert();
        //            });
        //        }
        //        if (eYongHuYYEXList.GetUpdates().Count > 0)
        //        {
        //            eYongHuYYEXList.GetUpdates().ForEach(o =>
        //            {
        //                E_GY_YONGHUYY eyonghuyy = o.MapTo<E_GY_YONGHUYY>(); //o.EToE<E_GY_YONGHUYY_EX, E_GY_YONGHUYY>();
        //                var yonghuyy = GYYongHuYYDomain.GetByID(DBContext, ServiceContext, eyonghuyy);
        //                yonghuyy.Update(eyonghuyy);
        //            });
        //        }
        //        if (eYongHuYYEXList.GetDeletes().Count > 0)
        //        {
        //            eYongHuYYEXList.GetDeletes().ForEach(o =>
        //            {
        //                E_GY_YONGHUYY eyonghuyy = o.MapTo<E_GY_YONGHUYY>(); //o.EToE<E_GY_YONGHUYY_EX, E_GY_YONGHUYY>();
        //                var yonghuyy = GYYongHuYYDomain.GetByID(DBContext, ServiceContext, eyonghuyy);
        //                yonghuyy.Delete();
        //            });
        //        }
        //        DBContext.SaveChanges();
        //        //提交
        //        trans.Commit();
        //    }
        //    return new Result<bool>(ReturnCode.SUCCESS, "保存用户的角色应用信息成功！");
        //}







        ///// <summary>
        ///// 修改密码
        ///// </summary>
        ///// <param name="zhiGongID">职工ID</param>
        ///// <param name="oldPassword">原始密码</param>
        ///// <param name="newPassword">新密码</param>
        ///// <returns></returns>
        //public Result<bool> UpdatePassword(string zhiGongID,string oldPassword,string newPassword)
        //{
        //    using (var trans = DBContext.Database.BeginTransaction())
        //    {
        //        var yonghu = GYYongHuXXDomain.GetByID(DBContext, ServiceContext, zhiGongID);

        //        yonghu.UpdatePassword(oldPassword, newPassword);
        //        DBContext.SaveChanges();
        //        trans.Commit();
        //    }
        //    return new Result<bool>(ReturnCode.SUCCESS, "密码修改成功");
        //}
        #endregion

        #region HIS1服务   
        /// <summary>
        /// 保存职工核算科室
        /// </summary>
        /// <param name="prmYeWuLX">1，新增；2，修改；3，删除；</param>
        /// <param name="prmZhiGongID"></param>
        /// <param name="prmHeSuanKS"></param>
        /// <param name="prmHeSuanKSMC"></param>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<bool> SaveZhiGongHSKS(int prmYeWuLX,string prmZhiGongID,string prmNewHeSuanKS, string prmNewHeSuanKSMC, string prmOLDHeSunKS)
        {
            UnitOfWork.BeginTransaction();
            //新增的
            if (prmYeWuLX == 1)
            {
                var zhiGongKSRep = this.GetRepository<IGY_ZHIGONGKSRepository>(UnitOfWork);
                if (zhiGongKSRep != null)
                {
                    E_GY_ZHIGONGKS eZhiGongKS = new E_GY_ZHIGONGKS();
                    eZhiGongKS.ZHIGONGID = prmZhiGongID;
                    eZhiGongKS.KESHIBQID = prmNewHeSuanKS;
                    eZhiGongKS.KESHIBQBZ = 3;
                    eZhiGongKS.KESHIBQMC = prmNewHeSuanKSMC;
                    eZhiGongKS.XIUGAIREN = ServiceContext.USERID;
                    eZhiGongKS.XIUGAISJ = zhiGongKSRep.GetSYSTime();
                    var zhiGongK = GY_ZHIGONGKSFactory.Create(zhiGongKSRep, ServiceContext, eZhiGongKS);
                }

            }
            else if (prmYeWuLX == 2) //修改
            {
                var zhiGongKSRep = this.GetRepository<IGY_ZHIGONGKSRepository>(UnitOfWork);
                if (zhiGongKSRep != null)
                {
                    var zhiGongK = zhiGongKSRep.GetByID(prmZhiGongID, prmOLDHeSunKS, 3);
                    if (zhiGongK != null)
                    {
                        zhiGongK.KESHIBQID = prmNewHeSuanKS;
                        zhiGongK.KESHIBQMC = prmNewHeSuanKSMC;
                        zhiGongK.XIUGAIREN = ServiceContext.USERID;
                        zhiGongK.XIUGAISJ = zhiGongKSRep.GetSYSTime();
                    }
                }
            }
            else if (prmYeWuLX == 3) //删除
            {
                var zhiGongKSRep = this.GetRepository<IGY_ZHIGONGKSRepository>(UnitOfWork);
                if (zhiGongKSRep != null)
                {
                    var zhiGongK = zhiGongKSRep.GetByID(prmZhiGongID, prmOLDHeSunKS, 3);
                    if (zhiGongK != null)
                    {
                        zhiGongK.Delete();
                    }
                }
            }
            UnitOfWork.BulkSaveChanges();
            UnitOfWork.Commit();
            return ServiceContent(true);
        }
        #endregion
    }
}
