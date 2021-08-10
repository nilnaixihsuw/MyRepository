using Autofac.Core;
using Mediinfo.Enterprise;
using Mediinfo.Enterprise.Log;
using Mediinfo.Infrastructure.Core;
using Mediinfo.Infrastructure.Core.MessageQueue;
using Mediinfo.Infrastructure.Core.MessageQueue.BindingsEntity;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Infrastructure.Core.UnitOfWork;
using Mediinfo.Infrastructure.JCJG.Filter;
using Mediinfo.Utility.Compress;
using Mediinfo.Utility.Util;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediinfo.Infrastructure.JCJG.Controller
{
    /// <summary>
    /// HIS控制器基类
    /// </summary>
    [HISActionFilterAttribute]
    public class HISController : ApiBaseController
    {
        public HISController()
        {
            try
            {

                // 如果消息队列未配置
                if (Messager.PeiZhiZt == PeiZhiZt.WeiPeiZhi)
                {
                    lock (Messager._lock)
                    {
                        if (Messager.PeiZhiZt == PeiZhiZt.WeiPeiZhi)
                        {
                            lock (Messager._lock)
                            {
                                //using (HISDBContext dbContext = HISDBContextFactory.Create())
                                //{

                                //// 查询消息配置
                                //var faSongPz = dbContext.Database.SqlQuery<int>("SELECT a.zhi FROM gy_xiaoxipz a where a.leixing = '消息发送'").FirstOrDefault();

                                //// 默认sql为查询对象允许值
                                //string sql = @"SELECT b.zhi FROM gy_xiaoxidx b where b.duixianglx='数据库表'";

                                //// 如果消息发送配置为1，代表全部发送，这时候要查询过滤值
                                //if (faSongPz == 1)
                                //{
                                //    sql += " and b.YUNXUGLBZ=1";
                                //}
                                //else
                                //{
                                //    sql += " and b.YUNXUGLBZ=0";
                                //}

                                //var xiaoXiDxList = dbContext.Database.SqlQuery<string>(sql).ToList();

                                // 设置配置状态为已配置
                                Messager.PeiZhiZt = PeiZhiZt.YiPeiZhi;
                                // 设置发送配置标识
                                Messager.FaSongPz = 0;
                                try
                                {
                                    using (var client = MessageQueueClientFactory.CreateClient())
                                    {
                                        List<BindingsModel> bindingsList = client.GetBindingsList();
                                        // 设置允许或过滤的消息对象
                                        Messager.DuiXingList = bindingsList.SelectMany(m => m.arguments.Keys).ToList();

                                        //获取HL7-SHUJUTX队列所监控的实体名称
                                        foreach (var bindModel in bindingsList.Where(b => b.destination == "HL7-SHUJUTX"))
                                        {
                                            foreach (var item in bindModel.arguments.Keys)
                                            {
                                                if (!Messager.DaiJianCeSTList.Contains(item))
                                                    Messager.DaiJianCeSTList.Add(item);
                                            }
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {

                                    LogHelper.Intance.Error("消息队列", "服务启动时，初始化消息发送配置时，发生错误！", JsonUtil.SerializeObject(ex));
                                }

                                //}
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                LogHelper.Intance.Error("系统日志", "消息发送配置失败", ex.ToString() + "\nJson日志：" + JsonUtil.SerializeObject(ex));
            }
        }

        private Pagination _pagination = null;
        /// <summary>
        /// 分页信息
        /// </summary>
        protected Pagination Pagination
        {
            get
            {
                if (_pagination == null)
                {
                    Dictionary<string, string> postData = QueryUrl.GetData(this.Request.Content.ReadAsStringAsync().Result);
                    try
                    {
                        _pagination = (Pagination)postData["Pagination"].Decompress(typeof(Pagination));
                    }
                    catch
                    {
                        _pagination = new Pagination();
                    }
                }

                return _pagination;
            }
        }

        /// <summary>
        /// 重写默认UnitOfWork
        /// </summary>
        protected override IUnitOfWork UnitOfWork
        {
            get
            {
                return GetUnitOfWork<IHISUnitOfWork>();
            }
        }
        public static readonly object _lock = new object();
        public override IUnitOfWork TreadUnitOfWork
        {
            get
            {
                lock (_lock)
                {
                    var uw = GetUnitOfWork<IHISUnitOfWork>();
                    if (UnitOfWork.CurrentDbTransaction != null)
                    {
                        uw.UseTransaction(UnitOfWork.CurrentDbTransaction);
                    }
                    return uw;
                }
                
            }
        }

        /// <summary>
        /// 获取仓储接口
        /// </summary>
        /// <typeparam name="T">仓储类型</typeparam>
        /// <returns></returns>
        protected override T GetRepository<T>()
        {
            var list = new List<ResolvedParameter>();

            list.Add(new ResolvedParameter((pi, ctx) => pi.ParameterType == typeof(IRepositoryContext),
                                           (pi, ctx) => (IRepositoryContext)UnitOfWork));

            list.Add(new ResolvedParameter((pi, ctx) => pi.ParameterType == typeof(ServiceContext),
                                                     (pi, ctx) => (ServiceContext)ServiceContext));

            return ServiceLocator.Instance.GetService<T>(list);
        }
    }
}
