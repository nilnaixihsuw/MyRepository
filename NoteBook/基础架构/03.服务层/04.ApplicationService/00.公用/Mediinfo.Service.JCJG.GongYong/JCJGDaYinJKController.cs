using Mediinfo.Domain.JCJG.GY;
using Mediinfo.DTO.Core;
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
    [Route("JCJGDaYinJK/{action}")]
    public class JCJGDaYinJKController : Mediinfo.Infrastructure.JCJG.Controller.HISController
    {
        [HttpPost]
        public ServiceResult<List<string>> Get()
        {
            return ServiceContent(new List<string>());
        }

        /// <summary>
        /// 获取打印接口
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<List<E_GY_DAYINJK>> GetDaYinJK()
        {
            E_GY_DAYINJK dto = new E_GY_DAYINJK ();
            var list = new QueryService(UnitOfWork).Get<E_GY_DAYINJK>(dto);
            return ServiceContent(list);
        }

        /// <summary>
        /// 保存医疗证明
        /// </summary>
        /// <param name="chuFang1s"></param>
        /// <param name="chuFang2s"></param>
        /// <param name="yiJi1s"></param>
        /// <param name="yiJi2s"></param>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<bool> SaveDaYinJK(List<E_GY_DAYINJK> lstDaYinJK)
        {
            Check.IsTrue(lstDaYinJK.Count > 0, "医疗证明列表为空");

            var daYinRep = this.GetRepository<IGY_DAYINJKRepository>(UnitOfWork);
            UnitOfWork.BeginTransaction();

            foreach (var item in lstDaYinJK)
            {
                if (item.GetState() == DTOState.New)
                {
                    List<GY_DAYINJK> lstdyjk = daYinRep.GetMZList(item.JILULY??0, item.JIUZHENID, item.LAIYUANID);
                    if (lstdyjk != null && lstdyjk.Any())
                    {
                        foreach(var dyjk in lstdyjk)
                        {
                            daYinRep.RegisterDelete(dyjk);
                        }
                    }

                    GY_DAYINJKFactory.Create(daYinRep, ServiceContext, item);
                }
                else if (item.GetState() == DTOState.Update)
                {
                    var domain = daYinRep.GetByKey(item.JILUID);
                    domain.Update(item);
                }
                //else if (item.GetState() == DTOState.Delete)
                //{
                //    var domain = daYinRep.GetByKey(item.JILUID);
                //    domain.Delete();

                //}
            }

            UnitOfWork.BulkSaveChanges();
            UnitOfWork.Commit();

            return ServiceContent(true);
        }


    }
}
