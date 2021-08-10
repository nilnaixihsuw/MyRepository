using System.Collections.Generic;
using System.Web.Http;
using Mediinfo.Domain.JCJG.GY;
using Mediinfo.DTO.HIS.GY;
using Mediinfo.Enterprise;
using Mediinfo.Enterprise.Exceptions;
using Mediinfo.Infrastructure.Core;
using Mediinfo.Infrastructure.Core.Domain;
using Mediinfo.Service.JCJG.GongYong.Route;

namespace Mediinfo.Service.JCJG.GongYong
{
    [ServiceRoutePrefix]
    [Route("JCJGCaiDan/{action}")]
    public class JCJGCaiDanController : Mediinfo.Infrastructure.JCJG.Controller.HISController
    {
        /// <summary>
        /// 获取菜单信息
        /// </summary>
        /// <param name="caiDanID"></param>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<List<E_GY_CAIDAN_NEW>> GetCaiDanNew(string caiDanID)
        {
            E_GY_CAIDAN_NEW entity = new E_GY_CAIDAN_NEW();

            entity.Where(" where yingyongid=:yingyongid", ServiceContext.KUCUNYYID);
            entity.WhereAppend(" and caidanid=:caidanid", caiDanID);

            var list = new QueryService(UnitOfWork).Get<E_GY_CAIDAN_NEW>(entity);
            return ServiceContent(list);
        }

        /// <summary>
        /// 获取当前应用下的默认打开页面
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<List<E_GY_CAIDAN_NEW>> GetYingYongCD()
        {
            E_GY_CAIDAN_NEW entity = new E_GY_CAIDAN_NEW();

            entity.Where(" where yingyongid=:yingyongid", ServiceContext.KUCUNYYID);
            entity.WhereAppend(" and isopen=:isopen", 1);
            entity.WhereAppend(" and (substr(shangjicdid,'0','1')='1' or shangjicdid='1')");
            entity.WhereAppend(" and caidanid<>1");

            var list = new QueryService(UnitOfWork).Get<E_GY_CAIDAN_NEW>(entity);
            return ServiceContent(list);
        }

        /// <summary>
        /// 修改菜单信息
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<bool> EditCaiDan(string caiDanID, int isOpen)
        {
            bool bluess = false;
            if (string.IsNullOrEmpty(caiDanID))
                throw new ServiceException("入参不能为空！");
            UnitOfWork.BeginTransaction();

            var repCaiDan = this.GetRepository<IGY_CAIDAN_NEWRepository>(UnitOfWork);

            List<GY_CAIDAN_NEW> caiDan = repCaiDan.GetList(caiDanID);
            if (caiDan != null && caiDan.Count > 0)
            {
                E_GY_CAIDAN_NEW cdDto = caiDan[0].DBToE<GY_CAIDAN_NEW, E_GY_CAIDAN_NEW>();
                caiDan[0].UpdateIsOpen(isOpen);
                bluess = true;
            }


            UnitOfWork.SaveChanges();
            UnitOfWork.Commit();
            return ServiceContent(bluess);
        }



        /// <summary>
        /// 获取常用菜单列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<List<E_GY_CHANGYONGCAIDAN>> GetChangYongCaiDanList()
        {
            E_GY_CHANGYONGCAIDAN entity = new E_GY_CHANGYONGCAIDAN();

            entity.Where(" where YONGHUID = :YONGHUID ", ServiceContext.USERID);
            entity.WhereAppend(" and YINGYONGID = :YINGYONGID ", ServiceContext.YINGYONGID);
            entity.WhereAppend(" and ISCHANGYONG = 1 order by PAIXU desc");

            var list = new QueryService(UnitOfWork).Get<E_GY_CHANGYONGCAIDAN>(entity);
            return ServiceContent(list);
        }

        /// <summary>
        /// 获取全局常用菜单列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<List<E_GY_CHANGYONGCAIDAN>> GetALLChangYongCaiDanList()
        {
            E_GY_CHANGYONGCAIDAN entity = new E_GY_CHANGYONGCAIDAN();
            //entity.Where(" where (isquanjvcy = 1 and ischangyong = 0) or(YONGHUID = :YONGHUID and ischangyong = 1)", ServiceContext.USERID);
            //entity.WhereAppend(" and YINGYONGID = :YINGYONGID ", ServiceContext.YINGYONGID);
            //entity.WhereAppend(" order by ischangyong ");

            entity.WhereAppend(" where (YONGHUID = :YONGHUID and ISCHANGYONG = 1 and YINGYONGID = :YINGYONGID)", ServiceContext.USERID,ServiceContext.YINGYONGID);
            entity.WhereAppend(" or (YONGHUID = 'ALL' and isquanjvcy=1 and YINGYONGID = :YINGYONGID)", ServiceContext.YINGYONGID);
            entity.WhereAppend(" order by isquanjvcy desc,PAIXU desc");

            var list = new QueryService(UnitOfWork).Get<E_GY_CHANGYONGCAIDAN>(entity);
            return ServiceContent(list);
        }

        /// <summary>
        /// 根据菜单ID获取常用菜单
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<List<E_GY_CHANGYONGCAIDAN>> GetChangYongCaiDanByCaiDanID(string caiDanID)
        {
            E_GY_CHANGYONGCAIDAN entity = new E_GY_CHANGYONGCAIDAN();

            entity.Where(" where YONGHUID = :YONGHUID ", ServiceContext.USERID);
            entity.WhereAppend(" and YINGYONGID = :YINGYONGID ", ServiceContext.YINGYONGID);
            entity.WhereAppend(" and CAIDANID = :CAIDANID ", caiDanID);

            var list = new QueryService(UnitOfWork).Get<E_GY_CHANGYONGCAIDAN>(entity);
            return ServiceContent(list);
        }

        /// <summary>
        /// 根据菜单ID获取全局常用菜单
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<List<E_GY_CHANGYONGCAIDAN>> GetALLChangYongCaiDanByCaiDanID(string caiDanID)
        {
            E_GY_CHANGYONGCAIDAN entity = new E_GY_CHANGYONGCAIDAN();
            entity.Where(" where YONGHUID = 'ALL' ");
            entity.WhereAppend(" and YINGYONGID = :YINGYONGID ", ServiceContext.YINGYONGID);
            entity.WhereAppend(" and CAIDANID = :CAIDANID  ", caiDanID);

            var list = new QueryService(UnitOfWork).Get<E_GY_CHANGYONGCAIDAN>(entity);
            return ServiceContent(list);
        }
        /// <summary>
        /// 根据菜单ID获取常用菜单
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<List<E_GY_CHANGYONGCAIDAN>> GetALLQJChangYongCaiDanByCaiDanID(string caiDanID)
        {
            E_GY_CHANGYONGCAIDAN entity = new E_GY_CHANGYONGCAIDAN();

            entity.Where(" where YONGHUID = 'ALL' ");
            entity.WhereAppend(" and YINGYONGID = :YINGYONGID ", ServiceContext.YINGYONGID);
            entity.WhereAppend(" and CAIDANID = :CAIDANID  and isquanjvcy=1", caiDanID);

            var list = new QueryService(UnitOfWork).Get<E_GY_CHANGYONGCAIDAN>(entity);
            return ServiceContent(list);
        }



        /// <summary>
        /// 更新常用菜单
        /// </summary>
        /// <param name="caiDanID">菜单ID</param>
        /// <param name="isFavorite">是否为常用菜单</param>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<bool> EditChangYongCaiDan(string caiDanID, int isFavorite,int xuhao)
        {
            bool bluess = false;
            if (string.IsNullOrEmpty(caiDanID))
                throw new ServiceException("入参不能为空！");
            UnitOfWork.BeginTransaction();

            var changYongCaiDan = this.GetRepository<IGY_CHANGYONGCAIDANRepository>(UnitOfWork);

            List<GY_CHANGYONGCAIDAN> list = changYongCaiDan.GetChangYongCaiDanList(caiDanID);
            if (list != null && list.Count > 0)
            {
                E_GY_CHANGYONGCAIDAN cdDTO = list[0].DBToE<GY_CHANGYONGCAIDAN, E_GY_CHANGYONGCAIDAN>();
                list[0].UpdateChangYongCaiDan(isFavorite,xuhao);
                bluess = true;
            }

            UnitOfWork.SaveChanges();
            UnitOfWork.Commit();
            return ServiceContent(bluess);
        }

        /// <summary>
        /// 更新全局常用菜单
        /// </summary>
        /// <param name="caiDanID">菜单ID</param>
        /// <param name="isALLFavorite">是否为全局常用菜单</param>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<bool> EditALLChangYongCaiDan(string caiDanID, int isALLFavorite,int xuhao)
        {
            bool bluess = false;
            if (string.IsNullOrEmpty(caiDanID))
                throw new ServiceException("入参不能为空！");
            UnitOfWork.BeginTransaction();

            var changYongCaiDan = this.GetRepository<IGY_CHANGYONGCAIDANRepository>(UnitOfWork);

            List<GY_CHANGYONGCAIDAN> list = changYongCaiDan.GetALLChangYongCaiDanList(caiDanID);
            if (list != null && list.Count > 0)
            {
                E_GY_CHANGYONGCAIDAN cdDTO = list[0].DBToE<GY_CHANGYONGCAIDAN, E_GY_CHANGYONGCAIDAN>();
                
                list[0].UpdateALLChangYongCaiDan(isALLFavorite,xuhao);
                bluess = true;
            }

            UnitOfWork.SaveChanges();
            UnitOfWork.Commit();
            return ServiceContent(bluess);
        }
        /// <summary>
        /// 获取常用最大序号
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<int> getChangYongCDXH()
        {
            IGY_CHANGYONGCAIDANRepository changYongCaiDan = this.GetRepository<IGY_CHANGYONGCAIDANRepository>();
            int xuhao = changYongCaiDan.GetXuHao();
            return ServiceContent(xuhao);
        }
        /// <summary>
        /// 获取全局菜单最大序号
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<int> getQJChangYongCDXH()
        {
            IGY_CHANGYONGCAIDANRepository changYongCaiDan = this.GetRepository<IGY_CHANGYONGCAIDANRepository>();
            int xuhao = changYongCaiDan.GetQJXuHao();
            return ServiceContent(xuhao);
        }
        /// <summary>
        /// 添加常用菜单
        /// </summary>
        /// <param name="caiDanID">菜单ID</param>
        /// <param name="caiDanMC">菜单名称</param>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<bool> AddChangYongCaiDan(string caiDanID, string caiDanMC,int xuhao)
        {
            UnitOfWork.BeginTransaction();

            var changYongCaiDan = this.GetRepository<IGY_CHANGYONGCAIDANRepository>(UnitOfWork);
            E_GY_CHANGYONGCAIDAN dto = new E_GY_CHANGYONGCAIDAN();
            dto.YONGHUID = ServiceContext.USERID;
            dto.YINGYONGID = ServiceContext.YINGYONGID;
            dto.CAIDANID = caiDanID;
            dto.CAIDANMC = caiDanMC;
            dto.ISCHANGYONG = 1;
            dto.PAIXU = xuhao;

            GY_CHANGYONGCAIDANFactory.Create(changYongCaiDan, ServiceContext, dto);

            UnitOfWork.SaveChanges();
            UnitOfWork.Commit();
            return ServiceContent(true);
        }
        /// <summary>
        /// 添加全局常用菜单
        /// </summary>
        /// <param name="caiDanID">菜单ID</param>
        /// <param name="caiDanMC">菜单名称</param>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<bool> AddALLChangYongCaiDan(string caiDanID, string caiDanMC,int xuhao)
        {
            UnitOfWork.BeginTransaction();

            var changYongCaiDan = this.GetRepository<IGY_CHANGYONGCAIDANRepository>(UnitOfWork);
            E_GY_CHANGYONGCAIDAN dto = new E_GY_CHANGYONGCAIDAN();
            dto.YONGHUID = "ALL";
            dto.YINGYONGID = ServiceContext.YINGYONGID;
            dto.CAIDANID = caiDanID;
            dto.CAIDANMC = caiDanMC;
            dto.ISQUANJVCY = 1;
            dto.PAIXU = xuhao;

            GY_CHANGYONGCAIDANFactory.Create(changYongCaiDan, ServiceContext, dto);

            UnitOfWork.SaveChanges();
            UnitOfWork.Commit();
            return ServiceContent(true);
        }
    }
}
