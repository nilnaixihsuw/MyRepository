using Mediinfo.Domain.JCJG.GY;
using Mediinfo.DTO.Core;
using Mediinfo.DTO.HIS.GY;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core;
using Mediinfo.Service.JCJG.GongYong.Route;

using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Mediinfo.Service.JCJG.GongYong
{
    [ServiceRoutePrefix]
    [Route("JCJGDanJuXX/{action}")]
    public class JCJGDanJuXXController : Mediinfo.Infrastructure.JCJG.Controller.HISController
    {

        /// <summary>
        /// 获取单据信息根据单据ID
        /// </summary>
        /// <param name="danJuID">单据ID</param>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<List<E_GY_DANJUXX>> GetDanJuByDanJuID(string danJuID)
        {
            E_GY_DANJUXX dtoDanJu = new E_GY_DANJUXX();
            dtoDanJu.Where("WHERE danjuid = :DANJUID", danJuID);
            var queryService = new QueryService(UnitOfWork);
            var list = queryService.Get<E_GY_DANJUXX>(dtoDanJu);
            return new ServiceResult<List<E_GY_DANJUXX>>(list);
        }

        /// <summary>
        /// 根据单据名称获取单据信息
        /// </summary>
        /// <param name="danJuMC">单据名称</param>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<List<E_GY_DANJUXX>> GetDanJuByDanJuMC(string danJuMC)
        {
            E_GY_DANJUXX dtoDanJu = new E_GY_DANJUXX();
            dtoDanJu.Where("WHERE DANJUMC = :DANJUMC", danJuMC);
            var list = new QueryService(UnitOfWork).Get<E_GY_DANJUXX>(dtoDanJu);
            return new ServiceResult<List<E_GY_DANJUXX>>(list);
        }

        /// <summary>
        /// 获取所有单据
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<List<E_GY_DANJUXX>> GetAll()
        {
            E_GY_DANJUXX canShu = new E_GY_DANJUXX();
            var list = new QueryService(UnitOfWork).Get<E_GY_DANJUXX>(canShu);
            return new ServiceResult<List<E_GY_DANJUXX>>(list);
        }

        /// <summary>
        /// 获取除单据内容外的单据信息，单据内容比较大查询慢
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<List<E_GY_DANJUXX>> GetALLNotDanJuNR()
        {
            E_GY_DANJUXX dANJUXX = new E_GY_DANJUXX();
            List<string> columns = new List<string>();
            foreach (var item in dANJUXX.GetType().GetProperties())
            {
                if (item.Name != "DANJUNR")
                {
                    columns.Add(item.Name);
                }
            }
            dANJUXX.EnableSelectColumn();
            dANJUXX.SelectByColumnName(columns.ToArray());
            var list = new QueryService(UnitOfWork).Get<E_GY_DANJUXX>(dANJUXX);
            return new ServiceResult<List<E_GY_DANJUXX>>(list);
        }


        [HttpPost]
        public ServiceResult<E_GY_DANJUXX> SaveDanJuXX(List<E_GY_DANJUXX> eDanJu)
        {
            E_GY_DANJUXX refDanJuXX = new E_GY_DANJUXX();

            var repDanJuXX = this.GetRepository<IGY_DANJUXXRepository>(UnitOfWork);
            UnitOfWork.BeginTransaction();
            //新增单据
            if (eDanJu.GetNews().Count > 0)
            {
                eDanJu.GetNews().ForEach(o =>
                {
                    var danju = GY_DANJUXXFactory.Create(repDanJuXX, ServiceContext, o);
                    refDanJuXX.DANJUID = danju.DANJUID;
                    refDanJuXX.XIUGAISJ = danju.XIUGAISJ;
                });
            }
            //更新单据
            if (eDanJu.GetUpdates().Count > 0)
            {
                eDanJu.GetUpdates().ForEach(o =>
                {
                    var danju = repDanJuXX.GetByKey(o.DANJUID);
                    danju.Update(o);
                    refDanJuXX.DANJUID = danju.DANJUID;
                    refDanJuXX.XIUGAISJ = danju.XIUGAISJ;
                });
            }
            //删除单据
            if (eDanJu.GetDeletes().Count > 0)
            {
                eDanJu.GetDeletes().ForEach(o =>
                {
                    var danju = repDanJuXX.GetByKey(o.DANJUID);
                    danju.Delete();
                    refDanJuXX.DANJUID = danju.DANJUID;
                    refDanJuXX.XIUGAISJ = danju.XIUGAISJ;
                });
            }
            UnitOfWork.SaveChanges();
            UnitOfWork.Commit();
            return new ServiceResult<E_GY_DANJUXX>(refDanJuXX);
        }

        [HttpPost]
        public ServiceResult<E_GY_YONGHUXX> CheckLogIn(string UserName, string Pwd)
        {
            E_GY_YONGHUXX e_GY_YONGHUXX = new E_GY_YONGHUXX();
            e_GY_YONGHUXX.Where("WHERE YONGHUID=:YONGHUID AND MIMA=:MIMA", UserName, Pwd);
            var entity = new QueryService(UnitOfWork).Get<E_GY_YONGHUXX>(e_GY_YONGHUXX).FirstOrDefault();
            return new ServiceResult<E_GY_YONGHUXX>(entity);
        }
    }
}
