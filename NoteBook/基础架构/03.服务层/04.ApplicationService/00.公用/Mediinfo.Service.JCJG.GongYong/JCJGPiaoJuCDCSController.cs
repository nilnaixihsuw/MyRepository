using Mediinfo.Domain.JCJG.GY;
using Mediinfo.DTO.Core;
using Mediinfo.DTO.HIS.GY;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core;
using Mediinfo.Service.JCJG.GongYong.Route;

using System.Collections.Generic;
using System.Web.Http;

namespace Mediinfo.Service.JCJG.GongYong
{
    /// <summary>
    /// 票据重打次数
    /// </summary>
    [ServiceRoutePrefix]
    [Route("JCJGPiaoJuCDCS/{action}")]
    public class JCJGPiaoJuCDCSController : Mediinfo.Infrastructure.JCJG.Controller.HISController
    {
        #region 查询

        /// <summary>
        /// 获取票据重打次数
        /// </summary>
        /// <param name="piaoJuYWID">票据业务ID</param>
        /// <param name="piaoJuLXID">票据类型ID</param>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<List<E_GY_PIAOJUCDCS>> GetPiaoJuCDCS(string piaoJuYWID, string piaoJuLXID)
        {
            QueryService query = new QueryService(UnitOfWork);

            // 查询职工信息
            E_GY_PIAOJUCDCS e_GY_PIAOJUCDCS = new E_GY_PIAOJUCDCS();
            e_GY_PIAOJUCDCS.Where(" where PIAOJUYWID=:piaoJuYWID and PIAOJULXID=:piaoJuLXID", piaoJuYWID, piaoJuLXID);
            var piaoJuCDCSlist = query.Get<E_GY_PIAOJUCDCS>(e_GY_PIAOJUCDCS);
            return ServiceContent(piaoJuCDCSlist);
        }

        #endregion

        #region 业务处理

        /// <summary>
        /// 保存票据重打次数
        /// </summary>
        /// <param name="piaoJuCDCSlist">票据重打次数</param>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<bool> BaoCunPiaoJuChongDaXX(List<E_GY_PIAOJUCDCS> piaoJuCDCSlist)
        {
            UnitOfWork.BeginTransaction();
            var piaoJuCDCSRep = this.GetRepository<IGY_PIAOJUCDCSRepository>(UnitOfWork);

            // 新增票据重打次数
            if (piaoJuCDCSlist.GetNews().Count > 0)
            {
                piaoJuCDCSlist.GetNews().ForEach(o =>
                {
                    var piaojucdcs = GY_PIAOJUCDCSFactory.Create(piaoJuCDCSRep, ServiceContext, o);
                });
            }

            // 更新票据重打次数
            if (piaoJuCDCSlist.GetUpdates().Count > 0)
            {
                piaoJuCDCSlist.GetUpdates().ForEach(o =>
                {

                    var piaojuxx = piaoJuCDCSRep.GetList(o.PIAOJUYWID, o.PIAOJULXID);
                    if (piaojuxx != null)
                    {
                        piaojuxx[0].Update(o);
                    }
                });
            }

            // 删除票据重打次数
            if (piaoJuCDCSlist.GetDeletes().Count > 0)
            {
                piaoJuCDCSlist.GetDeletes().ForEach(o =>
                {
                    var piaojuxx = piaoJuCDCSRep.GetList(o.PIAOJUYWID, o.PIAOJULXID);
                    if (piaojuxx != null)
                    {
                        piaojuxx[0].Delete();
                    }
                });
            }

            UnitOfWork.SaveChanges();
            UnitOfWork.Commit();
            return ServiceContent(true);
        }

        /// <summary>
        /// 根据票据业务ID和票据类型id,获取票据重打次数记录
        /// </summary>
        /// <param name="piaoJuYWID">票据业务ID</param>
        /// <param name="piaoJuLXID">票据类型ID</param>
        /// <returns></returns>
        public ServiceResult<List<E_GY_PIAOJUCDCS>> GetPiaoJuCDCSByYeWuID(string piaoJuYWID, string piaoJuLXID)
        {
            E_GY_PIAOJUCDCS piaoJuCDCS = new E_GY_PIAOJUCDCS();
            piaoJuCDCS.Where("PiaoJuYWID = :a And PiaoJuLXID = :b ", piaoJuYWID, piaoJuLXID);
            var ret = new QueryService(UnitOfWork).Get<E_GY_PIAOJUCDCS>(piaoJuCDCS); ;
            return ServiceContent(ret);
        }

        #endregion
    }
}
