using Mediinfo.Domain.JCJG.GY;
using Mediinfo.DTO.Core;
using Mediinfo.DTO.HIS.GY;
using Mediinfo.Enterprise;
using Mediinfo.Enterprise.Exceptions;
using Mediinfo.HIS.Core;
using Mediinfo.Infrastructure.Core;
using Mediinfo.Service.JCJG.GongYong.Route;
using Mediinfo.Utility.Extensions;

using System.Collections.Generic;
using System.Web.Http;

namespace Mediinfo.Service.JCJG.GongYong
{
    [ServiceRoutePrefix]
    [Route("JCJGYongHuGRXX/{action}")]
    public class JCJGYongHuGRXXController : Mediinfo.Infrastructure.JCJG.Controller.HISController
    {
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="yongHuID"></param>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<List<E_GY_YONGHUXX>> GetYongHuXXByID(string yongHuID)
        {
            if (yongHuID.IsNullOrWhiteSpace())
                throw new ServiceException("入参:用户ID不能为空！");

            E_GY_YONGHUXX eYongHuXX = new E_GY_YONGHUXX();
            eYongHuXX.Where("Where YONGHUID=:YONGHUID AND TINGYONGBZ=:TINGYONGBZ", yongHuID, 0);

            var list = new QueryService(UnitOfWork).Get<E_GY_YONGHUXX>(eYongHuXX);

            return ServiceContent(list);
        }


        /// <summary>
        /// 根据职工工号取用户信息
        /// </summary>
        /// <param name="zhiGongGH">职工工号</param>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<List<E_GY_YONGHUXX_EX>> GetYongHuXXByZhiGongGH(string zhiGongGH)
        {
            if (zhiGongGH.IsNullOrWhiteSpace())
                throw new ServiceException("入参:用户ID不能为空！");

            E_GY_YONGHUXX_EX eYongHuXX = new E_GY_YONGHUXX_EX();
            eYongHuXX.Where("Where ZHIGONGGH=:ZHIGONGGH AND DANGQIANZT=:DANGQIANZT", zhiGongGH, "1");

            var list = new QueryService(UnitOfWork).Get<E_GY_YONGHUXX_EX>(eYongHuXX);

            return ServiceContent(list);
        }

        /// <summary>
        /// 根据用户ID获取用户皮肤信息
        /// </summary>
        /// <param name="yongHuID"></param>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<List<E_GY_YONGHUPFXX>> GetYongHuPFXXByID(string yongHuID)
        {
            if (yongHuID.IsNullOrWhiteSpace())
                throw new ServiceException("入参:用户ID不能为空！");

            E_GY_YONGHUPFXX eYongHuPFXX = new E_GY_YONGHUPFXX();
            eYongHuPFXX.Where("Where YONGHUID=:YONGHUID AND TINGYONGBZ=:TINGYONGBZ", yongHuID, 0);

            var list = new QueryService(UnitOfWork).Get<E_GY_YONGHUPFXX>(eYongHuPFXX);

            return ServiceContent(list);
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="eYongHuPFXX"></param>
        /// <param name="eYongHuXX"></param>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<bool> SaveYongHuGRXX(E_GY_YONGHUPFXX eYongHuPFXX, E_GY_YONGHUXX eYongHuXX)
        {
            UnitOfWork.BeginTransaction();

            var yongHuGRXXRep = this.GetRepository<IGY_YONGHUPFXXRepository>(UnitOfWork);
            var yongHuXXRep = this.GetRepository<IGY_YONGHUXXRepository>(UnitOfWork);
            //先处理皮肤信息
            string yongHuID = string.Empty;
            switch (eYongHuPFXX.GetState())
            {
                case DTOState.Update:
                    yongHuID = eYongHuPFXX.YONGHUID;
                    var yongHuPFXXUpdate = yongHuGRXXRep.GetByKey(yongHuID);
                    if (yongHuPFXXUpdate != null)
                    {
                        yongHuPFXXUpdate.Update(eYongHuPFXX);
                    }                   
                    break;
                case DTOState.New:
                    var yongHuPFXXNew = GY_YONGHUPFXXFactory.Create(yongHuGRXXRep, ServiceContext, eYongHuPFXX);
                    break;
                case DTOState.Delete:
                    yongHuID = eYongHuPFXX.YONGHUID;
                    var yongHuPFXX = yongHuGRXXRep.GetByKey(yongHuID);
                    if (yongHuPFXX != null)
                    {
                        yongHuPFXX.Delete();
                    }
                    break;
            }

            //更新用户信息的输入码
            yongHuID = eYongHuXX.YONGHUID;
            var isHis1 = yongHuXXRep.GetCanShu(HISClientHelper.YINGYONGID, "公用_是否对接其他HIS", "0");
            if (isHis1 == "1")
            {
                yongHuXXRep.ModifyShuRuMaByYongHuID(yongHuID, eYongHuXX.SHURUMA);
            }
            else
            {
                var yongHuXX = yongHuXXRep.GetByKey(yongHuID);
                if (yongHuXX !=null)
                {
                    yongHuXX.Update(eYongHuXX);                
                }      
            }
            
            UnitOfWork.SaveChanges();
            UnitOfWork.Commit();
            return ServiceContent(true);
        }
    }
}
