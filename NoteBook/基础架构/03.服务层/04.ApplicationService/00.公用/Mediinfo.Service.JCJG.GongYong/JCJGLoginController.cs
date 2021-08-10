using Mediinfo.Domain.JCJG.GY;
using Mediinfo.Domain.JCJG.XT;
using Mediinfo.DTO.HIS.GY;
using Mediinfo.DTO.HIS.XT;
using Mediinfo.Enterprise;
using Mediinfo.Enterprise.Exceptions;
using Mediinfo.Infrastructure.Core;
using Mediinfo.Infrastructure.Core.Domain;
using Mediinfo.Service.JCJG.GongYong.Route;
using Mediinfo.Utility;

using System;
using System.Collections.Generic;
using System.Web.Http;

namespace Mediinfo.Service.JCJG.GongYong
{
    /// <summary>
    /// 登陆服务
    /// </summary>
    [ServiceRoutePrefix]
    [Route("JCJGLogin/{action}")]
    public class JCJGLoginController : Mediinfo.Infrastructure.JCJG.Controller.HISController
    {
        /// <summary>
        /// 根据工号获取职工信息及可登陆的工作站列表
        /// </summary>
        /// <param name="gongHao"></param>
        /// <param name="networkList">网卡地址列表</param>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<LoginDTO> GetYongHuXByGH(string gongHao, List<NetworkConfig> networkList)
        {
            if (string.IsNullOrWhiteSpace(gongHao))
            {
                throw new ServiceException("工号不能为空");
            }

            if (networkList == null || networkList.Count <= 0)
            {
                throw new ServiceException("网卡地址未传入");
                //return new Result<LoginDTO>(ReturnCode.SERVICEERROR, "网卡地址未传入", null);
            }

            try
            {
                QueryService query = new QueryService(UnitOfWork);

                //查询职工信息
                E_GY_ZHIGONGXX zhiGong = new E_GY_ZHIGONGXX();
                zhiGong.Where(" where zhigonggh=:zhigonggh and dangqianzt=:dangqianzt", gongHao, "1");
                var zhiGonglist = query.Get<E_GY_ZHIGONGXX>(zhiGong);
                if (zhiGonglist.Count <= 0)
                {
                    throw new ServiceException("您输入的工号不存在或已被注销，请重新输入！");
                    //return new Result<LoginDTO>(ReturnCode.SERVICEERROR, "工号不存在或已被注销", null);
                }
                else if (zhiGonglist.Count > 1)
                {
                    throw new ServiceException("系统中存在多个同工号的人员");
                    // return new Result<LoginDTO>(ReturnCode.SERVICEERROR, "系统中存在多个同工号的人员", null);
                }

                //查询用户信息
                E_GY_YONGHUXX yongHu = new E_GY_YONGHUXX();
                yongHu.Where("where yonghuid=:id and tingyongbz = :tingyongbz", zhiGonglist[0].ZHIGONGID, 0);
                var yongHuList = query.Get<E_GY_YONGHUXX>(yongHu);
                if (yongHuList.Count <= 0)
                {
                    throw new ServiceException("当前用户非系统用户");
                    //return new Result<LoginDTO>(ReturnCode.SERVICEERROR, "当前用户非系统用户", null);
                }

                //查询可用的应用列表
                E_GY_YONGHUYY_EX yingYong = new E_GY_YONGHUYY_EX();
                yingYong.Where(" where yonghuid=:yonghuid and tingyongbz=0 order by yingyongid", yongHuList[0].YONGHUID);
                var yingYongList = query.Get<E_GY_YONGHUYY_EX>(yingYong);

                LoginDTO loginDTO = new LoginDTO();
                loginDTO.YongHuXX = yongHuList[0];
                loginDTO.ZhiGongXX = zhiGonglist[0];
                loginDTO.YingYongList = yingYongList;

                return ServiceContent(loginDTO);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 根据职工ID获取用户信息，仅包括用户姓名，应用列表
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<LoginDTO> GetUserInfoByID(string userID)
        {
            if (string.IsNullOrWhiteSpace(userID))
            {
                throw new ServiceException("用户名不能为空");
            }

            QueryService query = new QueryService(UnitOfWork);

            //查询职工信息
            E_GY_ZHIGONGXX zhiGong = new E_GY_ZHIGONGXX();
            zhiGong.Where(" where zhigongid=:zhigongid and dangqianzt=:dangqianzt", userID, "1");
            var zhiGonglist = query.Get(zhiGong);
            if (zhiGonglist.Count <= 0)
            {
                throw new ServiceException("工号不存在或已被注销");
            }
            else if (zhiGonglist.Count > 1)
            {
                throw new ServiceException("系统中存在多个同工号的人员");
            }

            //查询用户信息
            E_GY_YONGHUXX yongHu = new E_GY_YONGHUXX();
            yongHu.Where("where yonghuid=:id and tingyongbz = :tingyongbz", zhiGonglist[0].ZHIGONGID, 0);
            var yongHuList = query.Get(yongHu);
            if (yongHuList.Count <= 0)
            {
                throw new ServiceException("当前用户非系统用户");
            }

            //查询可用的应用列表
            E_GY_YONGHUYY_EX yingYong = new E_GY_YONGHUYY_EX();
            yingYong.Where(" where yonghuid=:yonghuid order by yingyongid", yongHuList[0].YONGHUID);
            var yingYongList = query.Get<E_GY_YONGHUYY_EX>(yingYong);

            LoginDTO loginDTO = new LoginDTO();
            loginDTO.YongHuXX = yongHuList[0];
            loginDTO.ZhiGongXX = zhiGonglist[0];
            loginDTO.YingYongList = yingYongList;

            return ServiceContent(loginDTO);
        }

        /// <summary>
        /// 用户登陆
        /// </summary>
        /// <param name="userId">用户名</param>
        /// <param name="password">密码（加密后）</param>
        /// <param name="yingYongId">应用ID</param>
        /// <param name="networkList">网络地址信息</param>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<E_GY_GONGZUOZHAN> Login(string userId, string password, string yingYongId, List<NetworkConfig> networkList)
        {
            if (string.IsNullOrWhiteSpace(yingYongId))
            {
                return new ServiceResult<E_GY_GONGZUOZHAN>(Convert.ToInt32(ReturnCode.SERVICEERROR).ToString(), "工号不能为空", null);
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                return new ServiceResult<E_GY_GONGZUOZHAN>(Convert.ToInt32(ReturnCode.SERVICEERROR).ToString(), "密码不能为空", null);
            }

            if (networkList == null || networkList.Count <= 0)
            {
                return new ServiceResult<E_GY_GONGZUOZHAN>(Convert.ToInt32(ReturnCode.SERVICEERROR).ToString(), "网卡地址未传入", null);
            }

            QueryService query = new QueryService(UnitOfWork);

            // 再次验证用户信息
            E_GY_YONGHUXX yongHu = new E_GY_YONGHUXX();

            yongHu.EnableSelectColumn();

            yongHu.Select(yongHu.MIMA);
            yongHu.Where("where yonghuid=:id and tingyongbz = :tingyongbz", userId, 0);

            var yongHuList = query.Get<E_GY_YONGHUXX>(yongHu);

            if (yongHuList.Count <= 0)
            {
                return new ServiceResult<E_GY_GONGZUOZHAN>(Convert.ToInt32(ReturnCode.SERVICEERROR).ToString(), "当前用户非系统用户", null);
            }

            string miMa = password;
            var gongZuoZhanRep = this.GetRepository<IGY_GONGZUOZHANRepository>(UnitOfWork);
            var caoZuoRZRep = this.GetRepository<IGY_CAOZUORZRepository>(UnitOfWork);
            var gongZuoZhanEntity = GY_GONGZUOZHANFactory.CreateIfNotExists(gongZuoZhanRep, userId, ServiceContext, networkList);
            if (caoZuoRZRep.GetCanShu("00", "公用_用户密码是否加密", "0") != "0") miMa = SHA256.Encrypt(miMa);
            if (yongHuList[0].MIMA != miMa)
            {
                return new ServiceResult<E_GY_GONGZUOZHAN>(Convert.ToInt32(ReturnCode.SERVICEERROR).ToString(), "密码不正确", null);
            }
            // 查询工作站注册的信息,如果不存在则自动注册
            UnitOfWork.BeginTransaction();
            // 在登陆的时候，工作站信息没有传入，所以需要在此处给ServiceContext赋值
            ServiceContext.GONGZUOZID = gongZuoZhanEntity.GONGZUOZID;   // 工作站ID
            ServiceContext.IP = gongZuoZhanEntity.IP;                   // IP地址
            ServiceContext.COMPUTERNAME = gongZuoZhanEntity.JISUANJM;   // 计算机名
            ServiceContext.MAC = gongZuoZhanEntity.WANGKADZ;            // 网卡地址
            // 记录登陆日志
            GY_CAOZUORZFactory.Create(caoZuoRZRep, ServiceContext);

            E_GY_GONGZUOZHAN egongZuoZhan = gongZuoZhanEntity.DBToE<GY_GONGZUOZHAN, E_GY_GONGZUOZHAN>();

            UnitOfWork.SaveChanges();
            UnitOfWork.Commit();

            // 返回工作站信息
            return ServiceContent(egongZuoZhan);
        }

        /// <summary>
        /// 续租在线状态
        /// </summary>
        /// <param name="ZhiGongID"></param>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<E_XT_ZAIXIANZT> ZaiXianZTXZ(E_XT_ZAIXIANZT e_XT_ZAIXIANZT)
        {
            var zAIXIANZTRepository = this.GetRepository<IXT_ZAIXIANZTRepository>();

            // 获取当前的在线状态
            XT_ZAIXIANZT xT_ZAIXIANZT = zAIXIANZTRepository.GetByKey(e_XT_ZAIXIANZT.ZHUANGTAIID);

            // 如果在线状态不存在，或者不是在线状态，就新增在线状态
            if (xT_ZAIXIANZT == null)
            {
                xT_ZAIXIANZT = XT_ZAIXIANZTFactory.Create(zAIXIANZTRepository, e_XT_ZAIXIANZT);
                zAIXIANZTRepository.RegisterAdd(xT_ZAIXIANZT);
            }
            else if (!xT_ZAIXIANZT.ZaiXian())
            {
                // 续租
                xT_ZAIXIANZT.XuZu(xT_ZAIXIANZT);
            }

            UnitOfWork.SaveChanges();

            return ServiceContent(xT_ZAIXIANZT.DBToE<XT_ZAIXIANZT, E_XT_ZAIXIANZT>());
        }

        /// <summary>
        /// 系统推出
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="gongZuoZhanID">工作站ID</param>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<bool> Logout(string userId, string gongZuoZhanID)
        {
            UnitOfWork.BeginTransaction();
            var caoZuoRZRep = this.GetRepository<IGY_CAOZUORZRepository>(UnitOfWork);

            var caoZuoRZEntity = caoZuoRZRep.GetLoginLog();

            caoZuoRZEntity.Logout();

            UnitOfWork.SaveChanges();

            UnitOfWork.Commit();

            return ServiceContent(true);
        }
        /// <summary>
        /// 更新版本号
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="yingYongId"></param>
        /// <param name="banBenHao"></param>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<bool> LoginBanBenHao(string ip,string yingYongId, string banBenHao)
        {
            UnitOfWork.BeginTransaction();
            var caoZuoRZRep = this.GetRepository<IGY_CAOZUORZRepository>(UnitOfWork);
            // 记录登陆日志
            if (caoZuoRZRep != null)
            {
                var caozuorz = caoZuoRZRep.GetByID(ip, yingYongId);
                if (caozuorz != null)
                {
                    caozuorz.BANBENHAO = banBenHao;
                }
            }
            UnitOfWork.SaveChanges();
            UnitOfWork.Commit();
            return ServiceContent(true);
        }


        #region HRP登录业务

        /// <summary>
        /// 根据工号获取用户信息，仅包括用户姓名，应用列表
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<LoginDTO> GetUserInfo(string userID)
        {
            if (string.IsNullOrWhiteSpace(userID))
            {
                throw new ServiceException("用户名不能为空");
            }

            QueryService query = new QueryService(UnitOfWork);

            //查询职工信息
            E_GY_ZHIGONGXX zhiGong = new E_GY_ZHIGONGXX();
            zhiGong.Where(" where zhigonggh=:zhigonggh and dangqianzt=:dangqianzt", userID, "1");
            var zhiGonglist = query.Get(zhiGong);
            if (zhiGonglist.Count <= 0)
            {
                throw new ServiceException("工号不存在或已被注销");
            }
            else if (zhiGonglist.Count > 1)
            {
                throw new ServiceException("系统中存在多个同工号的人员");
            }

            //查询用户信息
            E_GY_YONGHUXX yongHu = new E_GY_YONGHUXX();
            yongHu.EnableSelectColumn();
            yongHu.Select(yongHu.YONGHUID, yongHu.YONGHUXM);
            yongHu.Where("where yonghuid=:id and tingyongbz = :tingyongbz", zhiGonglist[0].ZHIGONGID, 0);
            var yongHuList = query.Get(yongHu);
            if (yongHuList.Count <= 0)
            {
                throw new ServiceException("当前用户非系统用户");
            }

            //查询可用的应用列表
            E_GY_YONGHUYY_EX yingYong = new E_GY_YONGHUYY_EX();
            yingYong.EnableSelectColumn();
            yingYong.Select(yingYong.YINGYONGID, yingYong.YINGYONGJC, yingYong.YINGYONGMC);
            yingYong.Where(" where yonghuid=:yonghuid and xitongid in ('80','81','82') order by yingyongid", yongHuList[0].YONGHUID);
            var yingYongList = query.Get(yingYong);

            LoginDTO loginDTO = new LoginDTO();
            loginDTO.YongHuXX = yongHuList[0];
            loginDTO.YingYongList = yingYongList;

            return ServiceContent(loginDTO);
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="password"></param>
        /// <param name="yingYongId"></param>
        /// <returns></returns>
        public ServiceResult<E_GY_YONGHUXX> HRPLogin(string userID, string password)
        {
            if (string.IsNullOrWhiteSpace(userID))
            {
                throw new ServiceException("用户名不能为空");
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ServiceException("密码不能为空");
            }

            QueryService query = new QueryService(UnitOfWork);

            //再次验证用户信息
            E_GY_YONGHUXX yongHu = new E_GY_YONGHUXX();
            yongHu.EnableSelectColumn();
            yongHu.Select(yongHu.MIMA, yongHu.YONGHUID);
            yongHu.Where("where yonghuid=:id and tingyongbz = :tingyongbz", userID, 0);
            var yongHuList = query.Get(yongHu);
            if (yongHuList.Count <= 0)
            {
                throw new ServiceException("当前用户非系统用户");
            }
            string miMa = password;
            var caoZuoRZRep = this.GetRepository<IGY_CANSHURepository>(UnitOfWork);
            if (caoZuoRZRep.GetCanShu("00", "公用_用户密码是否加密", "0") != "0")
            {
                miMa = SHA256.Encrypt(miMa);
            }
            if (yongHuList[0].MIMA != miMa)
            {
                throw new ServiceException("密码不正确");
            }

            yongHu.YONGHUID = yongHuList[0].YONGHUID;

            return ServiceContent(yongHu);
        }

        #endregion

        ///// <summary>
        ///// 登陆移动护理
        ///// HR6-355(468160) add by xyq@20190425
        ///// </summary>
        ///// <param name="gongHao">工号</param>
        ///// <param name="password">密码</param>
        ///// <param name="bingQuID">病区ID</param>
        ///// <returns></returns>
        //[HttpPost]
        //public ServiceResult<DengLuFH> LoginYiDongHL(string gongHao, string password, string bingQuID)
        //{
        //    Check.NotEmpty(gongHao, "工号不能为空");
        //    Check.NotEmpty(password, "密码不能为空");
        //    Check.NotEmpty(bingQuID, "病区ID不能为空");


        //    QueryService query = new QueryService(UnitOfWork);

        //    //查询职工信息
        //    E_GY_ZHIGONGXX zhiGong = new E_GY_ZHIGONGXX();
        //    zhiGong.Where(" where zhigonggh=:zhigonggh and dangqianzt=:dangqianzt", gongHao, "1");
        //    var zhiGonglist = query.Get<E_GY_ZHIGONGXX>(zhiGong);

        //    Check.IsTrue(zhiGonglist.Count > 0, "您输入的工号不存在或已被注销，请重新输入！");
        //    Check.IsTrue(zhiGonglist.Count == 1, "系统中存在多个同工号的人员");
        //    //return ServiceContent(zhiGonglist[0]);
        //    var caoZuoRZRep = this.GetRepository<IGY_CANSHURepository>(UnitOfWork);
        //    string miMa = password;
        //    if (caoZuoRZRep.GetCanShu("00", "公用_用户密码是否加密", "0") != "0")
        //    {
        //        miMa = SHA256.Encrypt(miMa);
        //    }

        //    var userId = zhiGonglist[0].ZHIGONGID;

        //    //再次验证用户信息
        //    E_GY_YONGHUXX yongHu = new E_GY_YONGHUXX();
        //    yongHu.Where("where yonghuid=:id and tingyongbz = :tingyongbz", userId, 0);
        //    var yongHuList = new QueryService(UnitOfWork).Get<E_GY_YONGHUXX>(yongHu);
        //    Check.IsTrue(yongHuList.Count > 0, "当前用户非系统用户");
        //    Check.IsTrue(yongHuList[0].MIMA == miMa, "密码不正确");

        //    //验证护士登录病区
        //    E_GY_ZHIGONGKS zhiGongKS = new E_GY_ZHIGONGKS();
        //    zhiGongKS.Where(" where KeShiBQID =:KeShiBQID and KeShiBQBZ=2 and ZhiGongID=:ZhiGongID", bingQuID, userId);
        //    var huShiBQ = new QueryService(UnitOfWork).Get<E_GY_ZHIGONGKS>(zhiGongKS);
        //    Check.IsTrue(huShiBQ.Count > 0, "护士没有登录该病区的权限");


        //    //using (var trans = DBContext.Database.BeginTransaction())
        //    //{
        //    //    查询工作站注册的信息,如果不存在则自动注册

        //    //UnitOfWork.BeginTransaction();
        //    //    var gongZuoZhanRep = this.GetRepository<IGY_GONGZUOZHANRepository>(UnitOfWork);
        //    //    var caoZuoRZRep = this.GetRepository<IGY_CAOZUORZRepository>(UnitOfWork);

        //    //    var gongZuoZhanEntity = GY_GONGZUOZHANFactory.CreateIfNotExists(gongZuoZhanRep, userId, ServiceContext, networkList);

        //    //    //在登陆的时候，工作站信息没有传入，所以需要在此处给ServiceContext赋值
        //    //    ServiceContext.GONGZUOZID = gongZuoZhanEntity.GONGZUOZID; //工作站ID
        //    //    ServiceContext.IP = gongZuoZhanEntity.IP;         //IP地址
        //    //    ServiceContext.COMPUTERNAME = gongZuoZhanEntity.JISUANJM;   //计算机名
        //    //    ServiceContext.MAC = gongZuoZhanEntity.WANGKADZ;   //网卡地址

        //    //    //记录登陆日志
        //    //    GY_CAOZUORZFactory.Create(caoZuoRZRep, ServiceContext);

        //    //    E_GY_GONGZUOZHAN egongZuoZhan = gongZuoZhanEntity.DBToE<GY_GONGZUOZHAN, E_GY_GONGZUOZHAN>();


        //    //    UnitOfWork.SaveChanges();

        //    //    UnitOfWork.Commit();


        //    // 生成JWT Token(一般情况下生成Token建议在服务端生成)
        //    MediToken mediToken = new MediToken("HIS", "HIS", userId, new AuthInfo() { UserID = userId });

        //    //取手术标志
        //    E_GY_SHOUSHUSBZ shouShuBZ = new E_GY_SHOUSHUSBZ();
        //    shouShuBZ.Where("where YINGYONGID LIKE '18%' and BINGQUID=:BINGQUID", bingQuID);
        //    var shouShuBZList = new QueryService(UnitOfWork).Get<E_GY_SHOUSHUSBZ>(shouShuBZ);

        //    DengLuFH fanHui = new DengLuFH();
        //    //返回工作站信息
        //    fanHui.TOKEN = mediToken.CreateToken();
        //    fanHui.ZHIGONGID = userId;
        //    if (shouShuBZList.Count > 0)
        //    {
        //        fanHui.SHOUSHUSBZ = "1";
        //    }
        //    else
        //    {
        //        fanHui.SHOUSHUSBZ = "0";
        //    }
        //    return ServiceContent(fanHui);
        //    // }
        //}

        ///// <summary>
        ///// 登陆移动查房
        ///// HR6-692(487103) add by xyq@20190709
        ///// </summary>
        ///// <param name="gongHao">工号</param>
        ///// <param name="password">密码</param>
        ///// <returns></returns>
        //[HttpPost]
        //public ServiceResult<DengLuFH> LoginYiDongCF(string gongHao, string password)
        //{
        //    Check.NotEmpty(gongHao, "工号不能为空");
        //    Check.NotEmpty(password, "密码不能为空");


        //    QueryService query = new QueryService(UnitOfWork);

        //    //查询职工信息
        //    E_GY_ZHIGONGXX zhiGong = new E_GY_ZHIGONGXX();
        //    zhiGong.Where(" where zhigonggh=:zhigonggh and dangqianzt=:dangqianzt", gongHao, "1");
        //    var zhiGonglist = query.Get<E_GY_ZHIGONGXX>(zhiGong);

        //    Check.IsTrue(zhiGonglist.Count > 0, "您输入的工号不存在或已被注销，请重新输入！");
        //    Check.IsTrue(zhiGonglist.Count == 1, "系统中存在多个同工号的人员");
        //    //return ServiceContent(zhiGonglist[0]);
        //    var caoZuoRZRep = this.GetRepository<IGY_CANSHURepository>(UnitOfWork);
        //    string miMa = password;
        //    if (caoZuoRZRep.GetCanShu("00", "公用_用户密码是否加密", "0") != "0")
        //    {
        //        miMa = SHA256.Encrypt(miMa);
        //    }

        //    var userId = zhiGonglist[0].ZHIGONGID;

        //    //再次验证用户信息
        //    E_GY_YONGHUXX yongHu = new E_GY_YONGHUXX();
        //    yongHu.Where("where yonghuid=:id and tingyongbz = :tingyongbz", userId, 0);
        //    var yongHuList = new QueryService(UnitOfWork).Get<E_GY_YONGHUXX>(yongHu);
        //    Check.IsTrue(yongHuList.Count > 0, "当前用户非系统用户");
        //    Check.IsTrue(yongHuList[0].MIMA == miMa, "密码不正确");

        //    ////验证护士登录病区
        //    //E_GY_ZHIGONGKS zhiGongKS = new E_GY_ZHIGONGKS();
        //    //zhiGongKS.Where(" where KeShiBQID =:KeShiBQID and KeShiBQBZ=2 and ZhiGongID=:ZhiGongID", bingQuID, userId);
        //    //var huShiBQ = new QueryService(UnitOfWork).Get<E_GY_ZHIGONGKS>(zhiGongKS);
        //    //Check.IsTrue(huShiBQ.Count > 0, "护士没有登录该病区的权限");


        //    //using (var trans = DBContext.Database.BeginTransaction())
        //    //{
        //    //    查询工作站注册的信息,如果不存在则自动注册

        //    //UnitOfWork.BeginTransaction();
        //    //    var gongZuoZhanRep = this.GetRepository<IGY_GONGZUOZHANRepository>(UnitOfWork);
        //    //    var caoZuoRZRep = this.GetRepository<IGY_CAOZUORZRepository>(UnitOfWork);

        //    //    var gongZuoZhanEntity = GY_GONGZUOZHANFactory.CreateIfNotExists(gongZuoZhanRep, userId, ServiceContext, networkList);

        //    //    //在登陆的时候，工作站信息没有传入，所以需要在此处给ServiceContext赋值
        //    //    ServiceContext.GONGZUOZID = gongZuoZhanEntity.GONGZUOZID; //工作站ID
        //    //    ServiceContext.IP = gongZuoZhanEntity.IP;         //IP地址
        //    //    ServiceContext.COMPUTERNAME = gongZuoZhanEntity.JISUANJM;   //计算机名
        //    //    ServiceContext.MAC = gongZuoZhanEntity.WANGKADZ;   //网卡地址

        //    //    //记录登陆日志
        //    //    GY_CAOZUORZFactory.Create(caoZuoRZRep, ServiceContext);

        //    //    E_GY_GONGZUOZHAN egongZuoZhan = gongZuoZhanEntity.DBToE<GY_GONGZUOZHAN, E_GY_GONGZUOZHAN>();


        //    //    UnitOfWork.SaveChanges();

        //    //    UnitOfWork.Commit();


        //    // 生成JWT Token(一般情况下生成Token建议在服务端生成)
        //    MediToken mediToken = new MediToken("HIS", "HIS", userId, new AuthInfo() { UserID = userId });


        //    DengLuFH fanHui = new DengLuFH();
        //    ;            //返回工作站信息
        //    fanHui.TOKEN = mediToken.CreateToken();
        //    fanHui.ZHIGONGID = userId;
        //    return ServiceContent(fanHui);
        //    // }
        //}


        /// <summary>
        /// 移动护理用户密码校验
        /// HR6-355(468160) add by xyq@20190425
        /// </summary>
        /// <param name="gongHao">工号</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<bool> MiMaJY(string gongHao, string password)
        {
            Check.NotEmpty(gongHao, "工号不能为空");
            Check.NotEmpty(password, "密码不能为空");

            QueryService query = new QueryService(UnitOfWork);

            // 查询职工信息
            E_GY_ZHIGONGXX zhiGong = new E_GY_ZHIGONGXX();
            zhiGong.Where(" where zhigonggh=:zhigonggh and dangqianzt=:dangqianzt", gongHao, "1");
            var zhiGonglist = query.Get<E_GY_ZHIGONGXX>(zhiGong);

            Check.IsTrue(zhiGonglist.Count > 0, "您输入的工号不存在或已被注销，请重新输入！");
            Check.IsTrue(zhiGonglist.Count == 1, "系统中存在多个同工号的人员");
            //return ServiceContent(zhiGonglist[0]);
            var caoZuoRZRep = this.GetRepository<IGY_CANSHURepository>(UnitOfWork);
            string miMa = password;
            if (caoZuoRZRep.GetCanShu("00", "公用_用户密码是否加密", "0") != "0")
            {
                miMa = SHA256.Encrypt(miMa);
            }

            var userId = zhiGonglist[0].ZHIGONGID;

            // 再次验证用户信息
            E_GY_YONGHUXX yongHu = new E_GY_YONGHUXX();
            yongHu.Where("where yonghuid=:id and tingyongbz = :tingyongbz", userId, 0);
            var yongHuList = new QueryService(UnitOfWork).Get<E_GY_YONGHUXX>(yongHu);
            Check.IsTrue(yongHuList.Count > 0, "当前用户非系统用户");
            Check.IsTrue(yongHuList[0].MIMA == miMa, "密码不正确");

            return ServiceContent(true);
        }

        /// <summary>
        /// 解锁校验(适用于根据用户名判断密码是否正确)
        /// </summary>
        /// <param name="userId">用户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        [HttpPost]
        public ServiceResult<bool> JieSuoJY(string userId, string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ServiceException("密码不能为空");
            }

            QueryService query = new QueryService(UnitOfWork);

            // 再次验证用户信息
            E_GY_YONGHUXX yongHu = new E_GY_YONGHUXX();

            yongHu.EnableSelectColumn();

            yongHu.Select(yongHu.MIMA);
            yongHu.Where("where yonghuid=:id and tingyongbz = :tingyongbz", userId, 0);

            var yongHuList = query.Get<E_GY_YONGHUXX>(yongHu);

            if (yongHuList.Count <= 0)
            {
                throw new ServiceException("当前用户非系统用户");
            }

            if (yongHuList[0].MIMA != password && password != SHA256.Encrypt(yongHuList[0].MIMA))
            {
                // 密码错误返回false
                return ServiceContent(false);
            }

            return ServiceContent(true);
        }

        ///// <summary>
        ///// 其他HIS调用服务(HTTP)
        ///// </summary>
        ///// <param name="gongHao"></param>
        ///// <param name="password"></param>
        ///// <param name="bingQuID"></param>
        ///// <returns></returns>
        //[HttpPost]
        //public ServiceResult<DengLuFH> LoginHisDL(string gongHao, string password)
        //{
        //    Check.NotEmpty(gongHao, "工号不能为空");
        //    Check.NotEmpty(password, "密码不能为空");


        //    QueryService query = new QueryService(UnitOfWork);

        //    //查询职工信息
        //    E_GY_ZHIGONGXX zhiGong = new E_GY_ZHIGONGXX();
        //    zhiGong.Where(" where zhigonggh=:zhigonggh and dangqianzt=:dangqianzt", gongHao, "1");
        //    var zhiGonglist = query.Get<E_GY_ZHIGONGXX>(zhiGong);

        //    Check.IsTrue(zhiGonglist.Count > 0, "您输入的工号不存在或已被注销，请重新输入！");
        //    Check.IsTrue(zhiGonglist.Count == 1, "系统中存在多个同工号的人员");
        //    //return ServiceContent(zhiGonglist[0]);
        //    var caoZuoRZRep = this.GetRepository<IGY_CANSHURepository>(UnitOfWork);
        //    string miMa = password;
        //    if (caoZuoRZRep.GetCanShu("00", "公用_用户密码是否加密", "0") != "0")
        //    {
        //        miMa = SHA256.Encrypt(miMa);
        //    }

        //    var userId = zhiGonglist[0].ZHIGONGID;

        //     //再次验证用户信息
        //     E_GY_YONGHUXX yongHu = new E_GY_YONGHUXX();
        //    yongHu.Where("where yonghuid=:id and tingyongbz = :tingyongbz", userId, 0);
        //    var yongHuList = new QueryService(UnitOfWork).Get<E_GY_YONGHUXX>(yongHu);
        //    Check.IsTrue(yongHuList.Count > 0, "当前用户非系统用户");
        //    Check.IsTrue(yongHuList[0].MIMA == miMa, "密码不正确");

        //    ////验证护士登录病区
        //    //E_GY_ZHIGONGKS zhiGongKS = new E_GY_ZHIGONGKS();
        //    //zhiGongKS.Where(" where KeShiBQID =:KeShiBQID and KeShiBQBZ=2 and ZhiGongID=:ZhiGongID", bingQuID, userId);
        //    //var huShiBQ = new QueryService(UnitOfWork).Get<E_GY_ZHIGONGKS>(zhiGongKS);
        //    //Check.IsTrue(huShiBQ.Count > 0, "护士没有登录该病区的权限");


        //    //using (var trans = DBContext.Database.BeginTransaction())
        //    //{
        //    //    查询工作站注册的信息,如果不存在则自动注册

        //    //UnitOfWork.BeginTransaction();
        //    //    var gongZuoZhanRep = this.GetRepository<IGY_GONGZUOZHANRepository>(UnitOfWork);
        //    //    var caoZuoRZRep = this.GetRepository<IGY_CAOZUORZRepository>(UnitOfWork);

        //    //    var gongZuoZhanEntity = GY_GONGZUOZHANFactory.CreateIfNotExists(gongZuoZhanRep, userId, ServiceContext, networkList);

        //    //    //在登陆的时候，工作站信息没有传入，所以需要在此处给ServiceContext赋值
        //    //    ServiceContext.GONGZUOZID = gongZuoZhanEntity.GONGZUOZID; //工作站ID
        //    //    ServiceContext.IP = gongZuoZhanEntity.IP;         //IP地址
        //    //    ServiceContext.COMPUTERNAME = gongZuoZhanEntity.JISUANJM;   //计算机名
        //    //    ServiceContext.MAC = gongZuoZhanEntity.WANGKADZ;   //网卡地址

        //    //    //记录登陆日志
        //    //    GY_CAOZUORZFactory.Create(caoZuoRZRep, ServiceContext);

        //    //    E_GY_GONGZUOZHAN egongZuoZhan = gongZuoZhanEntity.DBToE<GY_GONGZUOZHAN, E_GY_GONGZUOZHAN>();


        //    //    UnitOfWork.SaveChanges();

        //    //    UnitOfWork.Commit();


        //    // 生成JWT Token(一般情况下生成Token建议在服务端生成)
        //    MediToken mediToken = new MediToken("HIS", "HIS", userId, new AuthInfo() { UserID = userId });

        //    DengLuFH fanHui = new DengLuFH();
        //    ;            //返回工作站信息
        //    fanHui.TOKEN = mediToken.CreateToken();
        //    fanHui.ZHIGONGID = userId;
        //    return ServiceContent(fanHui);
        //    // }
        //}
    }
}
