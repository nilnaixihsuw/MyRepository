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
    /// <summary>
    /// 界面按钮控件项目管理服务
    /// </summary>
    [ServiceRoutePrefix]
    [Route("JCJGChuangKouZY/{action}")]
    public class JCJGChuangKouZYController : Mediinfo.Infrastructure.JCJG.Controller.HISController
    {
        [HttpPost]
        public ServiceResult<List<E_GY_CHUANGKOUZY_NEW>> GetAll()
        {
            var list = new QueryService(UnitOfWork).Get<E_GY_CHUANGKOUZY_NEW>();
            return ServiceContent(list);
        }

        /// <summary>
        /// 通过主键ID获取数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<List<E_GY_CHUANGKOUZY_NEW>> GeByID(string id)
        {
            E_GY_CHUANGKOUZY_NEW eChuangKouZY = new E_GY_CHUANGKOUZY_NEW();
            eChuangKouZY.Where("WHERE ID=:ID", id);
            var list = new QueryService(UnitOfWork).Get<E_GY_CHUANGKOUZY_NEW>(eChuangKouZY);

            return ServiceContent(list);
        }

        /// <summary>
        /// 通过数据窗口获取数据
        /// </summary>
        /// <param name="chuangKouId"></param>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<List<E_GY_CHUANGKOUZY_NEW>> GetByFromName(string nameSpace, string formName)
        {
            E_GY_CHUANGKOUZY_NEW eChuangKouZY = new E_GY_CHUANGKOUZY_NEW();

            eChuangKouZY.Where("WHERE formname=:formname AND namespace=:nameSpace", formName, nameSpace);
            var list = new QueryService(UnitOfWork).Get<E_GY_CHUANGKOUZY_NEW>(eChuangKouZY);

            return ServiceContent(list);
        }

        /// <summary>
        /// 重置某个窗口的全部个性化设置
        /// </summary>
        /// <param name="nameSpace">命名空间</param>
        /// <param name="formName">窗口名称</param>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<bool> Reset(string nameSpace, string formName)
        {
            var quanXianRep = this.GetRepository<IGY_QUANXIANRepository>(UnitOfWork);

            var chuangKouZYRep = this.GetRepository<IGY_CHUANGKOUZY_NEWRepository>(UnitOfWork);
            var list = chuangKouZYRep.GetList(nameSpace, formName);

            UnitOfWork.BeginTransaction();
            string quanXianId = string.Empty;

            foreach (var item in list)
            {
                if (item.NeedQianXian())
                {
                    var quanXianEntity = quanXianRep.GetByKey(item.GetFullName());

                    // 将权限禁用
                    if (null != quanXianEntity) quanXianEntity.Disable();
                }

                chuangKouZYRep.RegisterDelete(item);
            }

            UnitOfWork.BulkSaveChanges();
            UnitOfWork.Commit();

            return ServiceContent(true);
        }

        /// <summary>
        /// 保存窗口的个性化设置
        /// </summary>
        /// <param name="chuangKouZYList"></param>
        /// <returns></returns>
        //[HttpPost]
        //public ServiceResult<bool> Save(List<E_GY_CHUANGKOUZY_NEW> chuangKouZYList, List<E_GY_JUESECKQX> jueSeQXList)
        //{
        //    var yonghuQXRep = this.GetRepository<IGY_YONGHUQX2Repository>(UnitOfWork);
        //    var quanXianRep = this.GetRepository<IGY_QUANXIAN2_NEWRepository>(UnitOfWork);

        //    var juesequanXianRep = this.GetRepository<IGY_JUESEQXRepository>(UnitOfWork);

        //    var chuangKouZYRep = this.GetRepository<IGY_CHUANGKOUZY_NEWRepository>(UnitOfWork);

        //    UnitOfWork.BeginTransaction();

        //    // 先保存窗口按钮信息
        //    if (chuangKouZYList.GetNews().Count > 0)
        //    {
        //        chuangKouZYList.GetNews().ForEach(o =>
        //        {
        //            var xiTongGN = GY_CHUANGKOUZY_NEWFactory.Create(chuangKouZYRep, ServiceContext, o);
        //        });
        //        UnitOfWork.SaveChanges();
        //    }

        //    // 修改
        //    if (chuangKouZYList.GetUpdates().Count > 0)
        //    {
        //        chuangKouZYList.GetUpdates().ForEach(o =>
        //        {
        //            var chuangkou = chuangKouZYRep.GetByKey(o.CHUANGKOUZYID);
        //            chuangkou.Update(o);

        //        });
        //        UnitOfWork.SaveChanges();
        //    }

        //    // 删除窗口按钮信息
        //    if (chuangKouZYList.GetDeletes().Count > 0)
        //    {
        //        chuangKouZYList.GetDeletes().ForEach(o =>
        //        {
        //            var chuangkou = chuangKouZYRep.GetByKey(o.CHUANGKOUZYID);
        //            chuangkou.Delete();
        //        });
        //        UnitOfWork.SaveChanges();
        //    }

        //    if (jueSeQXList != null && jueSeQXList.GetDeletes().Count > 0)
        //    {
        //        jueSeQXList.GetDeletes().ForEach(o =>
        //        {
        //            var quixian2new = quanXianRep.GetByKey(o.QUANXIANID);
        //            quixian2new.Delete();
        //            var jueseyhqx = juesequanXianRep.GetByKey(o.JUESEID, o.QUANXIANID);
        //            jueseyhqx.Delete();
        //            var yonghuqx = yonghuQXRep.GetByKey(o.YONGHUID, o.QUANXIANID);
        //            yonghuqx.Delete();
        //        });
        //    }
        //    else
        //    {
        //        foreach (var item in chuangKouZYList)
        //        {
        //            //获取窗口资源
        //            var chuangKouZY = chuangKouZYRep.GetList(item.NAMESPACE, item.FORMNAME, item.CONTROLNAME).FirstOrDefault();
        //            if (null == chuangKouZY)
        //            {
        //                chuangKouZY = GY_CHUANGKOUZY_NEWFactory.Create(chuangKouZYRep, ServiceContext, item);
        //            }
        //            else
        //            {
        //                chuangKouZY.Update(item);
        //            }

        //            if (chuangKouZY.NeedQianXian())
        //            {

        //                if (jueSeQXList != null)
        //                {
        //                    foreach (var jueseqx in jueSeQXList)
        //                    {

        //                        if (item.CONTROLNAME != jueseqx.CONTROLNAME)
        //                            continue;
        //                        // 获取相关的权限
        //                        var quanXian = quanXianRep.GetByKey(jueSeQXList.FirstOrDefault().QUANXIANID);
        //                        if (null == quanXian)
        //                        {
        //                            var e = new E_GY_QUANXIAN2_NEW();
        //                            e.SetTraceChange(true);
        //                            e.QUANXIANID = jueSeQXList.FirstOrDefault().QUANXIANID;

        //                            e.QUANXIANMC = chuangKouZY.QUANXIANMC();
        //                            e.MINGMINGKJ = item.NAMESPACE;
        //                            e.CHUANGKOUMC = item.FORMNAME;
        //                            e.KONGJIANMC = item.CONTROLNAME;
        //                            e.YINGYONGID = jueSeQXList.FirstOrDefault().YINGYONGID;
        //                            e.GONGNENGID = jueSeQXList.FirstOrDefault().GONGNENGID;

        //                            quanXian = GY_QUANXIAN2_NEWFactory.CreateIfNotExists(quanXianRep, ServiceContext, e);
        //                        }
        //                        // 获取角色权限
        //                        var jueSeQuanXian = juesequanXianRep.GetByKey(jueseqx.JUESEID, jueseqx.QUANXIANID);
        //                        if (null == jueSeQuanXian)
        //                        {
        //                            var e = new E_GY_JUESEQX();
        //                            e.SetTraceChange(true);
        //                            e.QUANXIANID = jueseqx.QUANXIANID;
        //                            e.JUESEID = jueseqx.JUESEID;
        //                            jueSeQuanXian = GY_JUESEQXFactory.CreateIfNotExists(juesequanXianRep, ServiceContext, e);
        //                        }

        //                        // 获取用户权限
        //                        var yonghuQuanXian = yonghuQXRep.GetByKey(jueseqx.QUANXIANID, jueseqx.YONGHUID);
        //                        if (null == yonghuQuanXian)
        //                        {
        //                            var e = new E_GY_YONGHUQX2();

        //                            e.QUANXIANID = jueseqx.QUANXIANID;
        //                            e.YONGHUID = jueseqx.YONGHUID;

        //                            yonghuQuanXian = GY_YONGHUQX2Factory.CreateIfNotExists(yonghuQXRep, ServiceContext, e);
        //                        }
        //                    }

        //                }

        //                //启用，停用相关权限
        //                //if (item.QUANXIANKZ == 1)
        //                //quanXian.Enable();
        //                // else
        //                // quanXian.Disable();
        //            }
        //        }
        //    }

        //    UnitOfWork.SaveChanges();
        //    UnitOfWork.Commit();

        //    return ServiceContent(true);
        //}
        [HttpPost]
        public ServiceResult<bool> Save(List<E_GY_CHUANGKOUZY_NEW> chuangKouZYList, List<E_GY_JUESECKQX_NEW> jueSeQXList)
        {

            var quanXianRep = this.GetRepository<IGY_QUANXIAN2_NEWRepository>(UnitOfWork);

            var juesequanXianRep = this.GetRepository<IGY_JUESEQXRepository>(UnitOfWork);

            var chuangKouZYRep = this.GetRepository<IGY_CHUANGKOUZY_NEWRepository>(UnitOfWork);

            UnitOfWork.BeginTransaction();

            // 先保存窗口按钮信息
            if (chuangKouZYList.GetNews().Count > 0)
            {
                chuangKouZYList.GetNews().ForEach(o =>
                {
                    var xiTongGN = GY_CHUANGKOUZY_NEWFactory.Create(chuangKouZYRep, ServiceContext, o);
                });
                UnitOfWork.SaveChanges();
            }

            // 修改
            if (chuangKouZYList.GetUpdates().Count > 0)
            {
                chuangKouZYList.GetUpdates().ForEach(o =>
                {
                    var chuangkou = chuangKouZYRep.GetByKey(o.CHUANGKOUZYID);
                    chuangkou.Update(o);

                });
                UnitOfWork.SaveChanges();
            }

            // 删除窗口按钮信息
            if (chuangKouZYList.GetDeletes().Count > 0)
            {
                chuangKouZYList.GetDeletes().ForEach(o =>
                {
                    var chuangkou = chuangKouZYRep.GetByKey(o.CHUANGKOUZYID);
                    if (chuangkou != null)
                        chuangkou.Delete();
                });
                UnitOfWork.SaveChanges();
            }

            if (jueSeQXList != null && jueSeQXList.GetDeletes().Count > 0)
            {
                jueSeQXList.GetDeletes().ForEach(o =>
                {
                    //var quixian2new = quanXianRep.GetByKey(o.QUANXIANID);
                    //quixian2new.Delete();
                    var jueseyhqx = juesequanXianRep.GetByKey(o.JUESEID, o.QUANXIANID);
                    if (jueseyhqx != null)
                        jueseyhqx.Delete();
                });
            }
            else
            {
                foreach (var item in chuangKouZYList)
                {
                    //获取窗口资源
                    var chuangKouZY = chuangKouZYRep.GetList(item.NAMESPACE, item.FORMNAME, item.CONTROLNAME).FirstOrDefault();
                    if (null == chuangKouZY)
                    {
                        chuangKouZY = GY_CHUANGKOUZY_NEWFactory.Create(chuangKouZYRep, ServiceContext, item);
                    }
                    else
                    {
                        chuangKouZY.Update(item);
                    }

                    if (chuangKouZY.NeedQianXian())
                    {

                        if (jueSeQXList != null)
                        {
                            foreach (var jueseqx in jueSeQXList)
                            {

                                if (item.CONTROLNAME != jueseqx.CONTROLNAME)
                                    continue;
                                // 获取相关的权限
                                var quanXian = quanXianRep.GetByKey(jueSeQXList.FirstOrDefault().QUANXIANID);
                                if (null == quanXian)
                                {
                                    var e = new E_GY_QUANXIAN2_NEW();
                                    e.SetTraceChange(true);
                                    e.QUANXIANID = jueSeQXList.FirstOrDefault().QUANXIANID;

                                    e.QUANXIANMC = chuangKouZY.QUANXIANMC();
                                    e.MINGMINGKJ = item.NAMESPACE;
                                    e.CHUANGKOUMC = item.FORMNAME;
                                    e.KONGJIANMC = item.CONTROLNAME;
                                    e.YINGYONGID = jueSeQXList.FirstOrDefault().YINGYONGID;
                                    e.GONGNENGID = jueSeQXList.FirstOrDefault().GONGNENGID;

                                    quanXian = GY_QUANXIAN2_NEWFactory.CreateIfNotExists(quanXianRep, ServiceContext, e);
                                }
                                // 获取角色权限
                                var jueSeQuanXian = juesequanXianRep.GetByKey(jueseqx.JUESEID, jueseqx.QUANXIANID);
                                if (null == jueSeQuanXian)
                                {
                                    var e = new E_GY_JUESEQX_NEW();
                                    e.SetTraceChange(true);
                                    e.QUANXIANID = jueseqx.QUANXIANID;
                                    e.JUESEID = jueseqx.JUESEID;
                                    jueSeQuanXian = GY_JUESEQXFactory.CreateNewIfNotExists(juesequanXianRep, ServiceContext, e);
                                }

                            }

                        }

                    }
                }
            }

            UnitOfWork.SaveChanges();
            UnitOfWork.Commit();
            return ServiceContent(true);
        }

    }
}
