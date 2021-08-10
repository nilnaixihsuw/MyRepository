using Mediinfo.DTO.HIS.GY;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core;
using Mediinfo.Service.JCJG.GongYong.Route;

using System.Collections.Generic;
using System.Web.Http;

namespace Mediinfo.Service.JCJG.GongYong
{
    [ServiceRoutePrefix]
    [Route("JCJGYingYong/{action}")]
    public class JCJGYingYongController : Mediinfo.Infrastructure.JCJG.Controller.HISController
    {
        [HttpPost]
        public ServiceResult<List<E_GY_YINGYONG>> GetAll()
        {
            E_GY_YINGYONG yy = new E_GY_YINGYONG();
            yy.Where("WHERE xitongid in ('05','06')");  // 门诊药房、门诊中药房、草药库
            var list = new QueryService(UnitOfWork).Get<E_GY_YINGYONG>(yy);
            return ServiceContent(list);
        }

        /// <summary>
        /// 获取应用科室
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<List<E_GY_YINGYONGKS>> GetYingYongKS()
        {
            E_GY_YINGYONGKS entry = new E_GY_YINGYONGKS();
            var list = new QueryService(UnitOfWork).Get<E_GY_YINGYONGKS>(entry);
            return ServiceContent(list);
        }

        /// <summary>
        /// 获取挂号收费应用
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<List<E_GY_YINGYONG>> GetGuaHaoSFYY()
        {
            E_GY_YINGYONG yy = new E_GY_YINGYONG();
            yy.Where("WHERE xitongid = '23'");
            var list = new QueryService(UnitOfWork).Get<E_GY_YINGYONG>(yy);
            return ServiceContent(list);
        }

        /// <summary>
        /// 查询出入库应用
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<List<E_GY_YINGYONG>> GetChuRuKuYY(string chuRukBZ)
        {
            E_GY_YINGYONG yy = new E_GY_YINGYONG();
            yy.Where(" where XITONGID in ('06','05','13','33','08','12','18','26','27','37','93')");
            yy.WhereAppend(" and YINGYONGID in (select YINGYONGID from gy_churukfs where churukbz = :churukbz)", chuRukBZ);
            var list = new QueryService(UnitOfWork).Get<E_GY_YINGYONG>(yy);
            return ServiceContent(list);
        }

        /// <summary>
        /// 获取应用列表
        /// </summary>
        /// <param name="yingYongID">应用ID</param>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<List<E_GY_YINGYONG>> GetListByYYID(string yingYongID)
        {
            E_GY_YINGYONG yy = new E_GY_YINGYONG();
            yy.Where("WHERE YINGYONGID = :YINGYONGID", yingYongID);
            var list = new QueryService(UnitOfWork).Get<E_GY_YINGYONG>(yy);
            return ServiceContent(list);
        }

        /// <summary>
        /// 公用控件取应用数据
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<List<E_GY_YINGYONG>> GetGYList()
        {
            E_GY_YINGYONG yingYong = new E_GY_YINGYONG();
            var list = new QueryService(UnitOfWork).Get<E_GY_YINGYONG>(yingYong);
            return ServiceContent(list);
        }

        /// <summary>
        /// 获取启用临床框架系统
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<List<E_GY_YINGYONG>> GetLinChuangKJList()
        {
            E_GY_YINGYONG yy = new E_GY_YINGYONG();
            yy.Where(" WHERE linchuangkjqybz = 1");
            var list = new QueryService(UnitOfWork).Get<E_GY_YINGYONG>(yy);
            return ServiceContent(list);
        }

        /// <summary>
        /// 获取启用临床框架系统2GetLinChuangKJ2List
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<List<E_GY_YINGYONG>> GetLinChuangKJ2List()
        {
            E_GY_YINGYONG yy = new E_GY_YINGYONG();
            yy.Where(" WHERE linchuangkjqybz = 2");
            var list = new QueryService(UnitOfWork).Get<E_GY_YINGYONG>(yy);
            return ServiceContent(list);
        }
    }
}
