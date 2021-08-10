using Mediinfo.Domain.JCJG.GY;
using Mediinfo.DTO.HIS.XT;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core;
using Mediinfo.Service.JCJG.GongYong.Route;

using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Mediinfo.Service.JCJG.GongYong
{
    [ServiceRoutePrefix]
    [Route("JCJGFangAnPZ/{action}")]
    public class JCJGFangAnPZController : Mediinfo.Infrastructure.JCJG.Controller.HISController
    {
        /// <summary>
        /// 取所有方案数据
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<List<E_XT_SELECTSQL2_EX>> GetAllFangAn()
        {
            var canshurep = this.GetRepository<IGY_CANSHURepository>(UnitOfWork);
            var canshu = canshurep.GetCanShu(ServiceContext.YINGYONGID, "公用_是否启用新方案", "0");
            E_XT_SELECTSQL2_EX xT_SELECTSQL2 = new E_XT_SELECTSQL2_EX();
            var result = new QueryService(UnitOfWork).Get(xT_SELECTSQL2);
            //如果启用参数"公用_是否启用新方案",那么会优先取XT_SELECTSQL1_NEW和XT_SELECTSQL2_NEW内的方案
            //因为对接HIS3的时候HIS3的方案和HALO的不同，需要HALO启用_NEW表的方案（即公用的方案都取原先的表，差异的方案HALO取_NEW表中数据）
            if (canshu == "1")
            {
                E_XT_SELECTSQL2_NEW_EX xT_SELECTSQL2_NEW = new E_XT_SELECTSQL2_NEW_EX();
                //先搜索出_NEW的方案
                var result_new = new QueryService(UnitOfWork).Get(xT_SELECTSQL2_NEW);
                if (result_new.Count > 0)
                {
                    foreach (var item in result_new)
                    {
                        //把_new方案内的属性全部赋值到不_NEW的DTO里
                        E_XT_SELECTSQL2_EX entity = new E_XT_SELECTSQL2_EX();
                        foreach (var o in item?.GetType()?.GetProperties())
                        {
                            entity?.GetType()?.GetProperty(o.Name)?.SetValue(entity, o?.GetValue(item, null));
                        }
                        //判断原来的方案内是否有和_NEW方案相同的方案，没有就直接加，有就先移除再添加
                        E_XT_SELECTSQL2_EX yichufa = result.FirstOrDefault(o => entity?.SQLID == o?.SQLID && entity?.FANGANMC == o?.FANGANMC && !string.IsNullOrEmpty(entity.SQLID) && !string.IsNullOrEmpty(entity.FANGANMC));
                        if (yichufa == null)
                            result.Add(entity);
                        else
                        {
                            result.Remove(yichufa);
                            result.Add(entity);
                        }
                    }
                }
            }
            return ServiceContent(result);
        }

        /// <summary>
        /// 取项目名和方案名的方案信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<List<E_XT_SELECTSQL2_EX>> GetFangAn(string xiangMu, string fangAnMC)
        {
            var canshurep = this.GetRepository<IGY_CANSHURepository>(UnitOfWork);
            var canshu = canshurep.GetCanShu(ServiceContext.YINGYONGID, "公用_是否启用新方案", "0");
            E_XT_SELECTSQL2_EX xT_SELECTSQL2 = new E_XT_SELECTSQL2_EX();
            xT_SELECTSQL2.Where(" WHERE ( SQLID=:SQLID AND FANGANMC=:FANGANMC ) OR ( SQLID=:SQLID1 AND FANGANMC=:FANGANMC1 ) OR ( SQLID=:SQLID2 AND FANGANMC=:FANGANMC2 )  ", xiangMu, fangAnMC, xiangMu, fangAnMC + "@", xiangMu + "@", fangAnMC);
            var result = new QueryService(UnitOfWork).Get(xT_SELECTSQL2);
            //如果启用参数"公用_是否启用新方案",那么会优先取XT_SELECTSQL1_NEW和XT_SELECTSQL2_NEW内的方案
            //因为对接HIS3的时候HIS3的方案和HALO的不同，需要HALO启用_NEW表的方案（即公用的方案都取原先的表，差异的方案HALO取_NEW表中数据）
            if (canshu == "1")
            {
                E_XT_SELECTSQL2_NEW_EX xT_SELECTSQL2_NEW = new E_XT_SELECTSQL2_NEW_EX();
                xT_SELECTSQL2_NEW.Where(" WHERE ( SQLID=:SQLID AND FANGANMC=:FANGANMC ) OR ( SQLID=:SQLID1 AND FANGANMC=:FANGANMC1 ) OR ( SQLID=:SQLID2 AND FANGANMC=:FANGANMC2 )  ", xiangMu, fangAnMC, xiangMu, fangAnMC + "@", xiangMu + "@", fangAnMC);
                //先搜索出_NEW的方案
                var result_new = new QueryService(UnitOfWork).Get(xT_SELECTSQL2_NEW);
                if (result_new.Count > 0)
                {
                    foreach (var item in result_new)
                    {
                        //把_new方案内的属性全部赋值到不_NEW的DTO里
                        E_XT_SELECTSQL2_EX entity = new E_XT_SELECTSQL2_EX();
                        foreach (var o in item?.GetType()?.GetProperties())
                        {
                            entity?.GetType()?.GetProperty(o.Name)?.SetValue(entity, o?.GetValue(item, null));
                        }
                        //判断原来的方案内是否有和_NEW方案相同的方案，没有就直接加，有就先移除再添加
                        E_XT_SELECTSQL2_EX yichufa = result.FirstOrDefault(o => entity?.SQLID == o?.SQLID && entity?.FANGANMC == o?.FANGANMC && !string.IsNullOrEmpty(entity.SQLID) && !string.IsNullOrEmpty(entity.FANGANMC));
                        if (yichufa == null)
                            result.Add(entity);
                        else
                        {
                            result.Remove(yichufa);
                            result.Add(entity);
                        }
                    }
                }
            }
            return ServiceContent(result);
        }
    }
}