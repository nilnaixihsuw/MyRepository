using Mediinfo.DTO.HIS.GY;
using Mediinfo.Enterprise;
using Mediinfo.Enterprise.Exceptions;
using Mediinfo.Infrastructure.Core;
using Mediinfo.Service.JCJG.GongYong.Route;

using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Mediinfo.Service.JCJG.GongYong
{
    [ServiceRoutePrefix]
    [Route("JCJGKeShi/{action}")]
    public class JCJGKeShiController : Mediinfo.Infrastructure.JCJG.Controller.HISController
    {
        [HttpPost]
        public ServiceResult<List<string>> Get()
        {
            return ServiceContent(new List<string>());
        }

        /// <summary>
        /// 获取科室信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<List<E_GY_KESHI>> GetKeShi()
        {
            E_GY_KESHI dto = new E_GY_KESHI();
            var list = new QueryService(UnitOfWork).Get<E_GY_KESHI>(dto);
            return ServiceContent(list);
        }

        /// <summary>
        /// 获取科室信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<List<E_GY_KESHI>> GetKeShiXX(string keShiID)
        {
            E_GY_KESHI dto = new E_GY_KESHI();
            if (string.IsNullOrWhiteSpace(keShiID))
            {
                throw new ServiceException("科室ID不能为空！");
            }
            dto.Where("WHERE KESHIID =:KESHIID", keShiID);
            var list = new QueryService(UnitOfWork).Get<E_GY_KESHI>(dto);
            return ServiceContent(list);
        }

        /// <summary>
        /// 获取科室处方限额
        /// </summary>
        /// <param name="yuanQuID"></param>
        /// <param name="keShiID"></param>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<List<E_GY_KESHI>> GetKeShiCFXE(string yuanQuID, string keShiID)
        {
            if (string.IsNullOrWhiteSpace(yuanQuID))
            {
                throw new ServiceException("院区ID不能为空！");
            }
            if (string.IsNullOrWhiteSpace(keShiID))
            {
                throw new ServiceException("科室ID不能为空！");
            }
            E_GY_KESHI dto = new E_GY_KESHI();
            dto.Where(" WHERE KESHIID =:KESHIID OR (YUANQUID =:YUANQUID AND ZUOFEIBZ = 0) ", keShiID, yuanQuID);
            var list = new QueryService(UnitOfWork).Get<E_GY_KESHI>(dto);
            return ServiceContent(list);
        }

        /// <summary>
        /// 获取科室关联的病区列表
        /// </summary>
        /// <param name="yuanQuID"></param>
        /// <param name="keShiID"></param>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<List<E_GY_BINGQUGUANLIANKS>> GetGuanLianBQList(string yuanQuID, string keShiID)
        {
            if (string.IsNullOrWhiteSpace(yuanQuID))
            {
                throw new ServiceException("院区ID不能为空！");
            }
            if (string.IsNullOrWhiteSpace(keShiID))
            {
                throw new ServiceException("科室ID不能为空！");
            }
            E_GY_BINGQUGUANLIANKS dto = new E_GY_BINGQUGUANLIANKS();
            dto.Where(" WHERE YUANQUID=:YUANQUID AND KESHIID=:KESHIID AND ZHUYUANBZ=1 AND ZUOFEIBZ=0 AND ZUOFEIBZ2=0", yuanQuID, keShiID);
            var list = new QueryService(UnitOfWork).Get<E_GY_BINGQUGUANLIANKS>(dto);
            return ServiceContent(list);
        }
      
        /// <summary>
        /// 获取科室树数据
        /// </summary>
        /// <param name="onlyNotCancel">默认仅查询未作废数据</param>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<List<E_GY_KESHI>> GetKeiShiTree(bool onlyNotCancel = true)
        {
            QueryService query = new QueryService(UnitOfWork);

            E_GY_YUANQU yuanQuDto = new E_GY_YUANQU();
            E_GY_KESHI keShiDto = new E_GY_KESHI();
            if (onlyNotCancel)
            {
                yuanQuDto.Where("where zuofeibz = 0");
                keShiDto.Where("where zuofeibz = 0");
            }
            List<E_GY_YUANQU> yiYuanList = query.Get(yuanQuDto);
            List<E_GY_KESHI> keShiList = query.Get(keShiDto);

            Check.IsTrue(keShiList.Count > 0, "获取科室信息失败!");

            foreach (E_GY_KESHI keShi in keShiList)
            {
                if (string.IsNullOrWhiteSpace(keShi.SHANGJIYWKS))
                {
                    keShi.SHANGJIYWKS = "y_" + keShi.YUANQUID;
                }
            }
            foreach (E_GY_YUANQU yiYuan in yiYuanList)
            {
                if (keShiList.Any(k => k.YUANQUID == yiYuan.YUANQUID))
                {
                    keShiList.Insert(0, new E_GY_KESHI
                    {
                        KESHIMC = yiYuan.YUANQUMC,
                        KESHIID = "y_" + yiYuan.YUANQUID,
                        SHANGJIYWKS = "root",
                        ZUOFEIBZ = (int?)yiYuan.ZUOFEIBZ
                    });
                }
            }

            return ServiceContent(keShiList.OrderBy(k => k.KESHIID).ToList());
        }
     
        /// <summary>
        /// 获取护理单元树
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<List<E_GY_KESHI>> GetHuLiDYTree()
        {
            QueryService query = new QueryService(UnitOfWork);

            E_GY_YUANQU yuanQuDto = new E_GY_YUANQU();
            yuanQuDto.Where("where zuofeibz = 0");
            E_GY_KESHI keShiDto = new E_GY_KESHI();
            keShiDto.Where("where zuofeibz = 0 and substr(xingzhisx, 28, 1) = '1'");

            List<E_GY_YUANQU> yiYuanList = query.Get(yuanQuDto);
            List<E_GY_KESHI> keShiList = query.Get(keShiDto);

            Check.IsTrue(keShiList.Count > 0, "获取科室信息失败!");

            foreach (E_GY_KESHI keShi in keShiList)
            {
                if (string.IsNullOrWhiteSpace(keShi.SHANGJIYWKS))
                {
                    keShi.SHANGJIYWKS = "y_" + keShi.YUANQUID;
                }
            }
            foreach (E_GY_YUANQU yiYuan in yiYuanList)
            {
                if (keShiList.Any(k => k.YUANQUID == yiYuan.YUANQUID))
                {
                    keShiList.Insert(0, new E_GY_KESHI
                    {
                        KESHIMC = yiYuan.YUANQUMC,
                        KESHIID = "y_" + yiYuan.YUANQUID,
                        SHANGJIYWKS = "root",
                        ZUOFEIBZ = (int?)yiYuan.ZUOFEIBZ
                    });
                }
            }

            return ServiceContent(keShiList.OrderBy(k => k.KESHIID).ToList());
        }

        /// <summary>
        /// 获取病案接口的科室列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<List<E_JK_GY_KESHI>> GetKeShiLB_BingAnJK()
        {
            E_JK_GY_KESHI dto = new E_JK_GY_KESHI();
            var list = new QueryService(UnitOfWork).Get<E_JK_GY_KESHI>(dto);
            return ServiceContent(list);
        }
    }
}
