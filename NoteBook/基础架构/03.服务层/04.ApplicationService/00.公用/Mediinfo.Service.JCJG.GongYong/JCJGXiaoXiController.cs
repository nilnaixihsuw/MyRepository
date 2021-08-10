using Mediinfo.Domain.JCJG.GY;
using Mediinfo.Domain.JCJG.XT;
using Mediinfo.DomainService.JCJG.GY;
using Mediinfo.DTO.HIS.GY;
using Mediinfo.DTO.HIS.XT;
using Mediinfo.DTO.HIS.ZJ;
using Mediinfo.DTO.HIS.ZY;
using Mediinfo.Enterprise;
using Mediinfo.HIS.Core;
using Mediinfo.Infrastructure.Core;
using Mediinfo.Service.JCJG.GongYong.Route;
using Mediinfo.Utility.Util;

using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web.Http;
using Mediinfo.Infrastructure.Core.Domain;
using Mediinfo.Utility.Extensions;

namespace Mediinfo.Service.JCJG.GongYong
{
    [ServiceRoutePrefix]
    [Route("JCJGXiaoXi/{action}")]
    public class JCJGXiaoXiController : Mediinfo.Infrastructure.JCJG.Controller.HISController
    {
        /// <summary>
        /// 触发消息（初始化消息收件箱）
        /// </summary>
        /// <param name="xiaoXiID"></param>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<bool> ChuFaXiaoXi(long xiaoXiID, DateTime? youXiaoSj, int yiCiXBZ)
        {
            IXT_XIAOXISJX_NEWRepository xT_XIAOXISJX_NEWRepository = GetRepository<IXT_XIAOXISJX_NEWRepository>();

            if (xT_XIAOXISJX_NEWRepository.GetByKey(xiaoXiID, "U-" + ServiceContext.USERID) == null)
            {
                // 初始化消息收件箱
                XT_XIAOXISJX_NEW shouJianXiang = XT_XIAOXISJX_NEWFactory.ChuShiHua(xT_XIAOXISJX_NEWRepository, ServiceContext,
                    xiaoXiID, youXiaoSj, yiCiXBZ);
                xT_XIAOXISJX_NEWRepository.RegisterAdd(shouJianXiang);
            }

            try
            {
                UnitOfWork.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                Mediinfo.Enterprise.Log.LogHelper.Intance.Warn("内部消息", "生成消息收件人时，发生错误", ex.ToString() + "\r\nJson:" + JsonUtil.SerializeObject(ex));
            }

            return ServiceContent(true);
        }


        /// <summary>
        /// 阅读消息
        /// </summary>
        /// <param name="xiaoXiID"></param>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<bool> YueDuXiaoXi(long xiaoXiID, string userID)
        {
            IXT_XIAOXISJX_NEWRepository xT_XIAOXISJX_NEWRepository = GetRepository<IXT_XIAOXISJX_NEWRepository>();

            var shouJianXiang = xT_XIAOXISJX_NEWRepository.GetByKey(xiaoXiID, "U-" + userID);
            shouJianXiang.YueDu();

            //处理消息一次性标志，当其中一个人查看消息后，其他人不需要再提示该消息
            IXT_XIAOXI_NEWRepository xT_XIAOXI_NEWRepository = GetRepository<IXT_XIAOXI_NEWRepository>();
            var faJianXiang = xT_XIAOXI_NEWRepository.GetByKey(xiaoXiID);
            //获取当前登录用户的职工类型
            IGY_ZHIGONGXXRepository gY_ZHIGONGXXRepository = GetRepository<IGY_ZHIGONGXXRepository>();
            var thisZGXX = gY_ZHIGONGXXRepository.GetByKey(userID);
            if (!string.IsNullOrEmpty(faJianXiang.XIAOXILY))
            {
                if (faJianXiang.YICIXBZ == 1)
                {
                    var yiCiXingCL = xT_XIAOXISJX_NEWRepository.GetList(xiaoXiID);
                    foreach (XT_XIAOXISJX_NEW xiaoXiJS in yiCiXingCL)
                    {
                        if (thisZGXX.ZHIGONGLB == "2")
                        {
                            var zhiGongXX = gY_ZHIGONGXXRepository.GetByKey(xiaoXiJS.SHOUJIANRID.Split('-')[1]);
                            if (zhiGongXX.ZHIGONGLB == thisZGXX.ZHIGONGLB)
                            {
                                var jieShouJSCL = xT_XIAOXISJX_NEWRepository.GetByKey(xiaoXiID, xiaoXiJS.SHOUJIANRID);
                                jieShouJSCL.YueDu();
                            }
                        }
                        else
                        {
                            var jieShouJSCL = xT_XIAOXISJX_NEWRepository.GetByKey(xiaoXiID, xiaoXiJS.SHOUJIANRID);
                            jieShouJSCL.YueDu();
                        }
                    }
                }
                else if (faJianXiang.YICIXBZ == 2)
                {
                    //根据当前登录用户判断，修改跟当前职工用户一样的消息已读标志
                    var yiCiXingCL = xT_XIAOXISJX_NEWRepository.GetList(xiaoXiID);
                    foreach (XT_XIAOXISJX_NEW xiaoXiJS in yiCiXingCL)
                    {
                        var zhiGongXX = gY_ZHIGONGXXRepository.GetByKey(xiaoXiJS.SHOUJIANRID.Split('-')[1]);
                        if (zhiGongXX.ZHIGONGLB == thisZGXX.ZHIGONGLB)
                        {
                            var jieShouJSCL = xT_XIAOXISJX_NEWRepository.GetByKey(xiaoXiID, xiaoXiJS.SHOUJIANRID);
                            jieShouJSCL.YueDu();
                        }
                    }
                }
                //faJianXiang.SetYiCiXBZ();
            }

            UnitOfWork.SaveChanges();
            return ServiceContent(true);
        }

        /// <summary>
        /// 接收消息
        /// </summary>
        /// <param name="xiaoXiID"></param>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<bool> JieShouXiaoXi(long xiaoXiID)
        {
            IXT_XIAOXISJX_NEWRepository xT_XIAOXISJX_NEWRepository = GetRepository<IXT_XIAOXISJX_NEWRepository>();

            var shouJianXiang = xT_XIAOXISJX_NEWRepository.GetByKey(xiaoXiID, "U-" + ServiceContext.USERID);
            shouJianXiang.JieShou();

            UnitOfWork.SaveChanges();
            return ServiceContent(true);
        }

        /// <summary>
        /// 获取离线消息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<List<HISMessageBody>> GetLiXianXX(string zaiXianZTID, DateTime faSongSJ)
        {
            Check.NotEmpty(zaiXianZTID, "在线状态ID不能为空！");

            XiaoXiDomainService xiaoXiDomainService = new XiaoXiDomainService(UnitOfWork, ServiceContext);
            var result = xiaoXiDomainService.GetLiXianXX(zaiXianZTID, faSongSJ);
            if (result.Count > 0 && result != null)
            {
                string menZhenXXCanShu = xiaoXiDomainService.GetCanShu("公用_门诊消息编码", "");
                string zhuYuanXXCanShu = xiaoXiDomainService.GetCanShu("公用_住院消息编码", "");
                List<string> MZcanshuXXBM = menZhenXXCanShu.Split(',').ToList();//此处在gy_canshu里面配置消息编码，配置成两个（一个门诊，一个住院），逗号隔开每个消息编码。
                List<string> ZYcanshuXXBM = zhuYuanXXCanShu.Split(',').ToList();
                if (ServiceContext.XITONGID == "10" || ServiceContext.XITONGID == "12")
                {
                    if(!string.IsNullOrEmpty(zhuYuanXXCanShu))
                    {
                        result = result?.Where(o=> ZYcanshuXXBM.Contains(o.XiaoXiBM))?.ToList().OrderByDescending(p => p.FaSongSj).ToList();
                    }
                    else
                    {
                        result = result.OrderByDescending(p => p.FaSongSj).ToList();
                    }
                }
                else if (ServiceContext.XITONGID == "04")
                {
                    if (!string.IsNullOrEmpty(menZhenXXCanShu))
                    {
                        result = result?.Where(o => MZcanshuXXBM.Contains(o.XiaoXiBM))?.ToList().OrderByDescending(p => p.FaSongSj).ToList();
                    }
                    else
                    {
                        result = result.OrderByDescending(p => p.FaSongSj).ToList();
                    }
                }
                else
                {
                    result = result.OrderByDescending(p => p.FaSongSj).ToList();
                }
            }
            return ServiceContent(result);
        }

        /// <summary>
        /// 获取已读消息
        /// </summary>
        /// <param name="zaiXianZTID"></param>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<List<HISMessageBody>> GetYiDuXX(string zaiXianZTID, DateTime faSongSJ)
        {
            Check.NotEmpty(zaiXianZTID, "在线状态ID不能为空！");

            XiaoXiDomainService xiaoXiDomainService = new XiaoXiDomainService(UnitOfWork, ServiceContext);
            var result = xiaoXiDomainService.GetYiDuXX(zaiXianZTID, faSongSJ);
            string menZhenXXCanShu = xiaoXiDomainService.GetCanShu("公用_门诊消息编码", "");
            string zhuYuanXXCanShu = xiaoXiDomainService.GetCanShu("公用_住院消息编码", "");
            List<string> MZcanshuXXBM = menZhenXXCanShu.Split(',').ToList();//此处在gy_canshu里面配置消息编码，配置成两个（一个门诊，一个住院），逗号隔开每个消息编码。
            List<string> ZYcanshuXXBM = zhuYuanXXCanShu.Split(',').ToList();
            if (ServiceContext.XITONGID == "10" || ServiceContext.XITONGID == "12")
            {
                if (!string.IsNullOrEmpty(zhuYuanXXCanShu))
                {
                    result = result?.Where(o=> ZYcanshuXXBM.Contains(o.XiaoXiBM))?.ToList().OrderByDescending(p => p.FaSongSj).ToList();
                }
                else
                {
                    result= result.OrderByDescending(p => p.FaSongSj).ToList();
                }
            }
            else if (ServiceContext.XITONGID == "04")
            {
                if (!string.IsNullOrEmpty(menZhenXXCanShu))
                {
                    result = result?.Where(o => MZcanshuXXBM.Contains(o.XiaoXiBM))?.ToList().OrderByDescending(p => p.FaSongSj).ToList();
                }
                else
                {
                    result = result.OrderByDescending(p => p.FaSongSj).ToList();
                }
            }
            else
            {
                result = result.OrderByDescending(p => p.FaSongSj).ToList();
            }
            return ServiceContent(result);
        }

        /// <summary>
        /// 获取消息处理窗口对照
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<Dictionary<string, string>> GetXiaoXiChuLiCKDZ()
        {
            E_XT_XIAOXIBM xiaoXiBM = new E_XT_XIAOXIBM();

            var list = new QueryService(UnitOfWork).Get<E_XT_XIAOXIBM>(xiaoXiBM);

            Dictionary<string, string> result = new Dictionary<string, string>();
            foreach (var item in list)
            {
                result.Add(item.XIAOXIBM, item.XIAOXICLCK + "|" + item.ISDUZHAN);
            }

            return ServiceContent(result);
        }

        /// <summary>
        /// 获取自定义消息编码
        /// </summary>
        [HttpPost]
        public ServiceResult<List<E_XT_XIAOXIBM>> GetZiDingYiXXBM()
        {

            E_XT_XIAOXIBM entity = new E_XT_XIAOXIBM();
            entity.Where(" WHERE ZIDINGYXXBZ = 1");
            var list = new QueryService(UnitOfWork).Get<E_XT_XIAOXIBM>(entity);

            return ServiceContent(list);
        }

        /// <summary>
        /// 获取病人姓名
        /// </summary>
        /// <param name="menZhenZyBz">门诊住院标识（0,门诊 1住院,2其他)</param>
        /// <param name="bingRenID">病人住院ID</param>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<string> GetBingRenXMByID(int menZhenZyBz, string bingRenID)
        {
            Check.NotEmpty(bingRenID, "病人ID不能为空！");

            string xm = "";
            if (menZhenZyBz == 0)
            {
                E_ZJ_JIUZHENXX entity = new E_ZJ_JIUZHENXX();
                entity.Where($" WHERE JIUZHENID = '{bingRenID}'");
                var item = new QueryService(UnitOfWork).Get<E_ZJ_JIUZHENXX>(entity).FirstOrDefault();
                if (item != null)
                    xm = item.XINGMING;
            }
            else if (menZhenZyBz == 1)
            {
                E_ZY_BINGRENXX entity = new E_ZY_BINGRENXX();
                entity.Where($" WHERE BINGRENZYID = '{bingRenID}'");
                var item = new QueryService(UnitOfWork).Get<E_ZY_BINGRENXX>(entity).FirstOrDefault();
                if (item != null)
                    xm = item.XINGMING;
            }
            else
            {
                E_GY_BINGRENXX entity = new E_GY_BINGRENXX();
                entity.Where($" WHERE BINGRENID = '{bingRenID}'");
                var item = new QueryService(UnitOfWork).Get<E_GY_BINGRENXX>(entity).FirstOrDefault();
                if (item != null)
                    xm = item.XINGMING;
            }
            return ServiceContent(xm);
        }

        /// <summary>
        /// 获取收件人
        /// </summary>
        /// <param name="shouJianRenID">收件人id</param>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<List<E_XT_SHOUJIANREN_NEW>> GetShouJianRen(string shouJianRenID)
        {
            Check.NotEmpty(shouJianRenID, "shouJianRenID");

            E_XT_SHOUJIANREN_NEW dto = new E_XT_SHOUJIANREN_NEW();
            if (shouJianRenID.Equals("全部"))
            {
                dto.Where(" order by shoujianrid");
            }
            else
            {
                dto.Where(" WHERE shoujianrid like  '" + shouJianRenID + "%' order by shoujianrid");
            }

            var list = new QueryService(UnitOfWork).Get<E_XT_SHOUJIANREN_NEW>(dto);
            return ServiceContent(list);
        }

        [HttpPost]
        public ServiceResult<bool> FaSongXX(string xiaoXiBT, string xiaoXiZY, string xiaoXiNR, List<E_XT_XIAOXIDY_NEW> xiaoXiList, Dictionary<string, string> XiaoXiFJ)
        {
            Check.NotEmpty(xiaoXiBT, "消息标题不可为空");
            Check.NotEmpty(xiaoXiZY, "消息摘要不可为空");
            Check.NotEmpty(xiaoXiNR, "消息内容不可为空");
            UnitOfWork.BeginTransaction();
            var entityList = xiaoXiList.EToDB<E_XT_XIAOXIDY_NEW, XT_XIAOXIDY_NEW>();
            XiaoXiDomainService xiaoXiDomainService = new XiaoXiDomainService(UnitOfWork, ServiceContext);

            xiaoXiDomainService.FaSongXX("04010", "", 0, entityList, xiaoXiBT, xiaoXiZY, xiaoXiNR, XiaoXiFJ, "0");
            
            UnitOfWork.BulkSaveChanges();
            UnitOfWork.Commit();
            return ServiceContent(true);
        }
        /// <summary>
        /// 获取已读消息
        /// </summary>
        /// <param name="zaiXianZTID"></param>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<List<HISMessageBody>> GetYiDuXX_YH(string zaiXianZTID, List<HISMessageBody> messAgeBodyList, List<string> wjzBM)
        {
            Check.NotEmpty(zaiXianZTID, "在线状态ID不能为空！");

            XiaoXiDomainService xiaoXiDomainService = new XiaoXiDomainService(UnitOfWork, ServiceContext);
            var result = xiaoXiDomainService.GetYiDuXX_YH(zaiXianZTID, messAgeBodyList);
            if (result != null && result.Count > 0)
            {
                string menZhenXXCanShu = xiaoXiDomainService.GetCanShu("公用_门诊消息编码", "");
                string zhuYuanXXCanShu = xiaoXiDomainService.GetCanShu("公用_住院消息编码", "");
                List<string> MZcanshuXXBM = menZhenXXCanShu.Split(',').ToList();//此处在gy_canshu里面配置消息编码，配置成两个（一个门诊，一个住院），逗号隔开每个消息编码。
                List<string> ZYcanshuXXBM = zhuYuanXXCanShu.Split(',').ToList();
                if (ServiceContext.XITONGID == "10" || ServiceContext.XITONGID == "12")
                {
                    if (!string.IsNullOrEmpty(zhuYuanXXCanShu))
                    {
                        result = result.Where(p => ZYcanshuXXBM.Contains(p.XiaoXiBM) && p.XiaoXiBM.SubString(0, 2) == ServiceContext.XITONGID || !wjzBM.Contains(p.XiaoXiBM)).OrderByDescending(p => p.FaSongSj).ToList();
                    }
                    else
                    {
                        result = result.Where(p => p.XiaoXiBM.SubString(0, 2) == ServiceContext.XITONGID || !wjzBM.Contains(p.XiaoXiBM)).OrderByDescending(p => p.FaSongSj).ToList();
                    }
                }
                else if(ServiceContext.XITONGID == "04")
                {
                    if (!string.IsNullOrEmpty(menZhenXXCanShu))
                    {
                        result = result.Where(p => MZcanshuXXBM.Contains(p.XiaoXiBM) && p.XiaoXiBM.SubString(0, 2) == ServiceContext.XITONGID || !wjzBM.Contains(p.XiaoXiBM)).OrderByDescending(p => p.FaSongSj).ToList();
                    }
                    else
                    {
                        result = result.Where(p => p.XiaoXiBM.SubString(0, 2) == ServiceContext.XITONGID || !wjzBM.Contains(p.XiaoXiBM)).OrderByDescending(p => p.FaSongSj).ToList();
                    }
                }
                else
                {
                    //现在根据系统id匹配就行,这里处理一下特殊编码消息
                    //add by meiyuan @2020.8.6 for 
                    result = result.Where(p => p.XiaoXiBM.SubString(0, 2) == ServiceContext.XITONGID || !wjzBM.Contains(p.XiaoXiBM)).OrderByDescending(p => p.FaSongSj).ToList();
                }
            }
            return ServiceContent(result);
        }

        /// <summary>
        /// 查询消息内容
        /// </summary>
        /// <param name="xiaoXiID"></param>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<List<E_XT_XIAOXI_NEW>> GetXiaoXiSJ(string xiaoXiID)
        {
            Check.NotEmpty(xiaoXiID, "消息ID不能为空！");

            E_XT_XIAOXI_NEW entry = new E_XT_XIAOXI_NEW();
            entry.Where(" Where XIAOXIID = :XIAOXIID ", xiaoXiID);
            var list = new QueryService(UnitOfWork).Get<E_XT_XIAOXI_NEW>(entry);
            return ServiceContent(list);
        }
    }
}
