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
    [Route("JCJGDataLayout/{action}")]
    public class JCJGDataLayoutController : Mediinfo.Infrastructure.JCJG.Controller.HISController
    {
        /// <summary>
        /// 获取控件布局（DataLayout1，DataLayout2）的信息（按照应用ID->系统ID的顺序->"00"）
        /// </summary>
        /// <param name="controlName"></param>
        /// <param name="formName"></param>
        /// <param name="nameSpace"></param>
        /// <param name="yingYongID"></param>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<E_GY_DATALAYOUTDTO> GetDataLayoutInfo(string controlName, string formName, string nameSpace, string yingYongID)
        {
            if (string.IsNullOrWhiteSpace(controlName) || string.IsNullOrWhiteSpace(formName)
                || string.IsNullOrWhiteSpace(nameSpace) || string.IsNullOrWhiteSpace(yingYongID))
            {
                throw new ServiceException("入参：布局参数不能为空！");
            }

            var queryService = new QueryService(UnitOfWork);

            E_GY_DATALAYOUT1 eDataLayout1 = new E_GY_DATALAYOUT1();

            //先按照应用ID取
            eDataLayout1.Where(" WHERE FORMNAME=:FORMNAME AND CONTROLNAME=:CONTROLNAME AND YINGYONGID=:YINGYONGID AND NAMESPACE=:NAMESPACE", formName, controlName, yingYongID, nameSpace);

            var list = queryService.Get<E_GY_DATALAYOUT1>(eDataLayout1);


            //没有的话按照系统ID取
            if (null == list || list.Count <= 0)
            {
                eDataLayout1.Where(" WHERE FORMNAME=:FORMNAME AND CONTROLNAME=:CONTROLNAME AND YINGYONGID=:YINGYONGID AND NAMESPACE=:NAMESPACE", formName, controlName, yingYongID.Substring(0, 2), nameSpace);
                list = queryService.Get<E_GY_DATALAYOUT1>(eDataLayout1);

                if (list == null || list.Count <= 0)
                {
                    eDataLayout1.Where(" WHERE FORMNAME=:FORMNAME AND CONTROLNAME=:CONTROLNAME AND YINGYONGID=:YINGYONGID AND NAMESPACE=:NAMESPACE", formName, controlName, "00", nameSpace);
                    list = queryService.Get<E_GY_DATALAYOUT1>(eDataLayout1);

                    if (list == null || list.Count <= 0)
                    {
                        return ServiceContent(new E_GY_DATALAYOUTDTO()
                        {
                            DataLayout1 = null,
                            DataLayout2 = null
                        });
                    }
                }
            }

            //取DataLayt2
            E_GY_DATALAYOUT2 layout2 = new E_GY_DATALAYOUT2();
            layout2.Where(" where DataLayoutID=:dataLayoutid", list[0].DATALAYOUTID);

            var list2 = queryService.Get(layout2);

            return ServiceContent(new E_GY_DATALAYOUTDTO()
            {
                DataLayout1 = list[0],
                DataLayout2 = list2
            });
        }
        
        ///// <summary>
        ///// 获取整个应用的布局（DataLayout1，DataLayout2）的信息（取本应用、本系统及00）
        ///// </summary>
        ///// <param name="yingYongId">应用ID</param>
        ///// <returns></returns>
        [HttpPost]
        public ServiceResult<List<E_GY_DATALAYOUTDTO>> GetDataLayoutInfoByYingYongId(string yingYongId)
        {
            if (string.IsNullOrWhiteSpace(yingYongId))
            {
                throw new ServiceException("应用ID不能为空");
            }

            if (yingYongId.Length < 4)
            {
                throw new ServiceException("无效的应用ID");
            }

            var queryService = new QueryService(UnitOfWork);

            //DataLayout1
            E_GY_DATALAYOUT1 layout1 = new E_GY_DATALAYOUT1();
            layout1.Where(" where yingyongid=:yingyongid or yingyongid=:xitongid or yingyongid='00'", yingYongId, yingYongId.Substring(0, 2));
            var layout1List = queryService.Get(layout1);

            //DataLayout2
            E_GY_DATALAYOUT2 layout2 = new E_GY_DATALAYOUT2();
            layout2.Where("  where exists (select datalayoutid from gy_datalayout1 where datalayoutid = datalayoutid and ( yingyongid = :yingyongid or yingyongid=:xitongid or yingyongid='00'))", yingYongId, yingYongId.Substring(0, 2));
            var layout2List = queryService.Get(layout2);

            List<E_GY_DATALAYOUTDTO> list = new List<E_GY_DATALAYOUTDTO>();
            foreach (var item in layout1List)
            {
                list.Add(new E_GY_DATALAYOUTDTO()
                {
                    DataLayout1 = item,
                    DataLayout2 = layout2List.Where(c => c.DATALAYOUTID == item.DATALAYOUTID).ToList()
                });
            }

            return ServiceContent(list);
        }

        /// <summary>
        /// 获取DataLayout1布局信息
        /// </summary>
        /// <param name="controlName">控件名称</param>
        /// <param name="formName">窗体名称</param>
        /// <param name="yingYongID">应用ID：两位为系统级别，四位为应用级别</param>
        /// <param name="nameSpace">命名控件</param>
        /// <returns>返回数据集</returns>
        [HttpPost]
        public ServiceResult<List<E_GY_DATALAYOUT1>> GetDataLayout1ByPara(string controlName, string formName, string yingYongID, string nameSpace)
        {
            if (string.IsNullOrWhiteSpace(controlName) || string.IsNullOrWhiteSpace(formName) || string.IsNullOrWhiteSpace(yingYongID) || string.IsNullOrWhiteSpace(nameSpace))
                throw new ServiceException("入参：布局参数不能为空！");

            E_GY_DATALAYOUT1 eDataLayout1 = new E_GY_DATALAYOUT1();

            eDataLayout1.Where(" WHERE FORMNAME=:FORMNAME AND CONTROLNAME=:CONTROLNAME AND YINGYONGID=:YINGYONGID AND NAMESPACE=:NAMESPACE", formName, controlName, yingYongID, nameSpace);

            var list = new QueryService(UnitOfWork).Get<E_GY_DATALAYOUT1>(eDataLayout1);

            return ServiceContent(list);
        }

        /// <summary>
        /// 根据ID获取列设置详情
        /// </summary>
        /// <param name="dataLayoutID">布局ID</param>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<List<E_GY_DATALAYOUT2>> GetDataLayout2ByID(string dataLayoutID)
        {
            if (string.IsNullOrWhiteSpace(dataLayoutID))
                throw new ServiceException("入参:布局参数ID不能为空！");

            E_GY_DATALAYOUT2 eDataLayout2 = new E_GY_DATALAYOUT2();
            eDataLayout2.Where(" Where DATALAYOUTID=:DATALAYOUTID", dataLayoutID);

            var list = new QueryService(UnitOfWork).Get(eDataLayout2);

            return ServiceContent(list);

        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="eDataLayout1"></param>
        /// <param name="eDataLayout2"></param>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<bool> SaveDataLayoutInfo(E_GY_DATALAYOUT1 eDataLayout1, List<E_GY_DATALAYOUT2> eDataLayout2)
        {
            UnitOfWork.BeginTransaction();

            var dataLayouRep = this.GetRepository<IGY_DATALAYOUT1Repository>(UnitOfWork);
            //先保存主表
            switch (eDataLayout1.GetState())
            {
                case DTOState.New:
                    var dataLayoutEntity = GY_DATALAYOUT1Factory.Create(dataLayouRep, ServiceContext, eDataLayout1);

                    foreach (var item in eDataLayout2)
                    {
                        dataLayoutEntity.AddDataLayout2(item);
                    }

                    break;
                case DTOState.Update:
                case DTOState.UnChange:

                    dataLayoutEntity = dataLayouRep.GetByKey(eDataLayout1.DATALAYOUTID);
                    if (null == dataLayoutEntity)
                        throw new ServiceException($"DataLayoutid为:{eDataLayout1.DATALAYOUTID}的数据不存在");

                    if (eDataLayout1.GetState() == DTOState.Update)
                    {
                        dataLayoutEntity.Update(eDataLayout1);
                    }

                    eDataLayout2.ForEach(o =>
                    {
                        switch (o.GetState())
                        {
                            case DTOState.New:
                                dataLayoutEntity.AddDataLayout2(o);
                                break;
                            case DTOState.Update:
                                dataLayoutEntity.UpdateDataLayout2(o);
                                break;
                            case DTOState.Delete:
                                dataLayoutEntity.RemoveDataLayout2(o.DATALAYOUTID);
                                break;
                        }
                    });
                    break;
                case DTOState.Delete:

                    dataLayoutEntity = dataLayouRep.GetByKey(eDataLayout1.DATALAYOUTID);
                    if (null == dataLayoutEntity)
                        throw new ServiceException($"DataLayoutid为:{eDataLayout1.DATALAYOUTID}的数据不存在");

                    dataLayoutEntity.Delete();
                    break;

            }

            UnitOfWork.BulkSaveChanges();
            UnitOfWork.Commit();
            return ServiceContent(true);
        }
    }
}
