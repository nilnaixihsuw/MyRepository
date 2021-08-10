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
    [Route("JCJGDanJuDXXX/{action}")]
    public class JCJGDanJuDXXXController : Mediinfo.Infrastructure.JCJG.Controller.HISController
    {
        /// <summary>
        /// 获取单据对象信息根据单据对象ID
        /// </summary>
        /// <param name="DanJuID">单据对象ID</param>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<List<E_GY_DANJUDXXX>> GetDanJuDXByDanJuDXID(string danJuDXID)
        {
            E_GY_DANJUDXXX dtoDanJu = new E_GY_DANJUDXXX();
            dtoDanJu.Where("WHERE danjudxid = :DANJUDXID", danJuDXID);
            var queryService = new QueryService(UnitOfWork);
            var list = queryService.Get<E_GY_DANJUDXXX>(dtoDanJu);
            return new ServiceResult<List<E_GY_DANJUDXXX>>(list);
        }

        /// <summary>
        /// 根据单据对象明细获取单据对象
        /// </summary>
        /// <param name="danJuDXMC">单据对象名称</param>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<List<E_GY_DANJUDXXX>> GetDanJuDXByDanJuDXMC(string danJuDXMC)
        {
            E_GY_DANJUDXXX dtoDanJu = new E_GY_DANJUDXXX();
            dtoDanJu.Where("WHERE DANJUDXMC = :DanJuDXMC", danJuDXMC);
            var queryService = new QueryService(UnitOfWork);
            var list = queryService.Get<E_GY_DANJUDXXX>(dtoDanJu);
            return new ServiceResult<List<E_GY_DANJUDXXX>>(list);
        }

        /// <summary>
        /// 获取所有单据对象
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<List<E_GY_DANJUDXXX>> GetAll()
        {
            E_GY_DANJUDXXX canShu = new E_GY_DANJUDXXX();
            var queryService = new QueryService(UnitOfWork);
            var list = queryService.Get<E_GY_DANJUDXXX>(canShu);
            return new ServiceResult<List<E_GY_DANJUDXXX>>(list);
        }

        /// <summary>
        /// 保存单据对象信息
        /// </summary>
        /// <param name="eDanJuDX"></param>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<string> SaveDanJuDXXX(List<E_GY_DANJUDXXX> eDanJuDX)
        {
            string danJuID = "";
            var repDanJuDXXX = this.GetRepository<IGY_DANJUDXXXRepository>(UnitOfWork);
            UnitOfWork.BeginTransaction();

            //新增单据对象
            if (eDanJuDX.GetNews().Count > 0)
            {
                eDanJuDX.GetNews().ForEach(o =>
                {
                    //判断是否有重复的，同应用下，不允许对象名和应用一致的记录
                    var danJuDXXX = repDanJuDXXX.GetList(o.DANJUDXMC, o.YINGYONGID).Where(p=>p.ZUOFEIBZ==0).ToList();
                    if (danJuDXXX.Count > 0)
                    {
                        throw new ServiceException("当前应用下已存在单据对象名称为["+o.DANJUDXMC+"]且未停用的单据打印对象记录，请在原来的上记录上修改，或使用另外的名字。");
                    }
                    var danju = GY_DANJUDXXXFactory.Create(repDanJuDXXX, ServiceContext, o);
                    danJuID = danju.DANJUDXID;
                });
            }
            //更新单据对象
            if (eDanJuDX.GetUpdates().Count > 0)
            {
                eDanJuDX.GetUpdates().ForEach(o =>
                {
                    var danJuDXXX = repDanJuDXXX.GetByKey(o.DANJUDXID);
                    if (o.GetState() == DTOState.Update)
                    {
                        danJuDXXX.Update(o);
                    }
                    danJuID = o.DANJUDXID;
                });
            }
            //删除单据对象
            if (eDanJuDX.GetDeletes().Count > 0)
            {
                eDanJuDX.GetDeletes().ForEach(o =>
                {
                    var danJuDXXX = repDanJuDXXX.GetByKey(o.DANJUDXID);
                    danJuDXXX.Delete();
                    danJuID = o.DANJUDXID;
                });
            }
            UnitOfWork.SaveChanges();
            UnitOfWork.Commit();
            return new ServiceResult<string>(danJuID);
        }
    }
}
