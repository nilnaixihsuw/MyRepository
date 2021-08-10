using Mediinfo.Domain.JCJG.GY;
using Mediinfo.Domain.JCJG.XT;
using Mediinfo.DTO.HIS.XT;
using Mediinfo.Enterprise;
using Mediinfo.Enterprise.Exceptions;
using Mediinfo.Infrastructure.Core;
using Mediinfo.Infrastructure.Core.Domain;
using Mediinfo.Service.JCJG.GongYong.Route;

using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Http;

namespace Mediinfo.Service.JCJG.GongYong
{
    [ServiceRoutePrefix]
    [Route("JCJGShuJuZD/{action}")]
    public class JCJGShuJuZDController : Mediinfo.Infrastructure.JCJG.Controller.HISController
    {
        /// <summary>
        /// 获取数据字典
        /// </summary>
        /// <param name="shuJuZDIdList">数据字典列表</param>
        /// <returns></returns>
        [HttpPost]
        [HttpGet]
        public ServiceResult<DataSet> GetShuJuZDList(List<string> shuJuZDIdList)
        {
            QueryService query = new QueryService(UnitOfWork);

            var selectSql3Rep = this.GetRepository<IXT_SELECTSQL3Repository>(UnitOfWork);

            DataSet ds = new DataSet();
            foreach (var item in shuJuZDIdList)
            {
                XT_SELECTSQL3 sql3 = selectSql3Rep.GetByKey(item);

                if (null == sql3)
                {
                    throw new ServiceException(string.Format("{0}不存在", item));
                }

                DataTable result = query.Get(new Parm<string>(sql3.SQL), false);

                result.TableName = item;

                if (!ds.Tables.Contains(item))
                    ds.Tables.Add(result);
            }
            return ServiceContent(ds);
        }

        /// <summary>
        /// 获取所有数据字典
        /// </summary>
        /// <param name="yingYongId"></param>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<List<E_XT_SELECTSQL3>> GetAllShuJuZD()
        {
            E_XT_SELECTSQL3 xT_SELECTSQL3 = new E_XT_SELECTSQL3();
            var result = new QueryService(UnitOfWork).Get(xT_SELECTSQL3);
            return ServiceContent(result);
        }

        /// <summary>
        /// 获取应用ID所有数据字典
        /// </summary>
        /// <param name="yingYongId"></param>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<List<E_XT_SELECTSQL3>> GetShuJuZDByYingYong(string yingYongId)
        {
            IXT_SHUJUZDHCRepository xT_SHUJUZDHCRepository = GetRepository<IXT_SHUJUZDHCRepository>();
            var zdhcList = xT_SHUJUZDHCRepository.GetList(yingYongId);

            IXT_SELECTSQL3Repository xT_SELECTSQL3Repository = GetRepository<IXT_SELECTSQL3Repository>();
            var result = xT_SELECTSQL3Repository.GetList(zdhcList.Select(m => m.SQLID).ToList());

            return ServiceContent(result.DBToE<XT_SELECTSQL3, E_XT_SELECTSQL3>());
        }

        /// <summary>
        /// 保存数据字典缓存
        /// </summary>
        /// <param name="yingYongID"></param>
        /// <param name="sqlIDList"></param>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<bool> SaveShuJuZDHC(string yingYongID, List<string> sqlIDList)
        {
            UnitOfWork.BeginTransaction();

            IXT_SHUJUZDHCRepository xT_SHUJUZDHCRepository = GetRepository<IXT_SHUJUZDHCRepository>();

            // 清楚历史数据
            var hcList = xT_SHUJUZDHCRepository.GetList(yingYongID);
            foreach (var item in hcList)
            {
                xT_SHUJUZDHCRepository.RegisterDelete(item);
            }
            UnitOfWork.BulkSaveChanges();

            // 重新保存
            foreach (var sqlID in sqlIDList)
            {
                var ziDianHc = XT_SHUJUZDHCFactory.Create(xT_SHUJUZDHCRepository, ServiceContext, yingYongID, sqlID);
                xT_SHUJUZDHCRepository.RegisterAdd(ziDianHc);
            }
            UnitOfWork.BulkSaveChanges();
            UnitOfWork.Commit();

            return ServiceContent(true);
        }

        /// <summary>
        /// 通过应用Id获取所有的数据字典（一般用于系统初始化缓存使用）
        /// </summary>
        /// <param name="yingYongId">应用Id</param>
        /// <returns></returns>
        [HttpPost]
        [HttpGet]
        public ServiceResult<JsonDataSet> GetByYingYongId(string yingYongId)
        {
            JsonDataSet jsonDataSet = null;
            DataSet ds = new DataSet();

            QueryService query = new QueryService(UnitOfWork);
            var selectSql3Rep = this.GetRepository<IXT_SELECTSQL3Repository>(UnitOfWork);

            IXT_SHUJUZDHCRepository xTShujuzdhcRepository = GetRepository<IXT_SHUJUZDHCRepository>();
            List<XT_SHUJUZDHC> ziDianHc = xTShujuzdhcRepository.GetList(yingYongId);
            var sqlIDs = ziDianHc.Select(m => m.SQLID).ToList();

            var list = sqlIDs.Count > 0 ? selectSql3Rep.GetList(sqlIDs) : selectSql3Rep.GetList();

            foreach (var item in list)
            {
                var result = query.Get(new Parm<string>(item.SQL), false);
                result.TableName = item.SQLID;

                if (!ds.Tables.Contains(item.SQLID))
                    ds.Tables.Add(result);
            }
            jsonDataSet = new JsonDataSet(ds);
            return ServiceContent(jsonDataSet);
        }
    }
}
