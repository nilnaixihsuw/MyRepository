using Mediinfo.DTO.HIS.GY;
using Mediinfo.Enterprise;
using Mediinfo.Enterprise.Token;
using Mediinfo.Infrastructure.Core;
using Mediinfo.Infrastructure.Core.Cache;
using Mediinfo.Infrastructure.Core.Job;
using Mediinfo.Infrastructure.Core.UnitOfWork;
using Mediinfo.Infrastructure.JCJG;
using Mediinfo.Service.JCJG.GongYong.Route;
using Mediinfo.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Http;
using Mediinfo.Utility.Compress;
using Mediinfo.Utility.Util;

namespace Mediinfo.Service.JCJG.GongYong
{
    [ServiceRoutePrefix]
    [Route("OpenInterface/{action}")]
    public class OpenInterfaceController : ApiController
    {
        /// <summary>
        /// 检验服务层，并抛出异常
        /// </summary>
        public ICheck Check => new ServiceCheck();

        /// <summary>
        /// 转发服务，第三方调用
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public object Path()
        {
            string str = "";
            try
            {
                var keyValuePairs = Request.Properties["MS_QueryNameValuePairs"];//获取传统context
                if (keyValuePairs is KeyValuePair<string, string>[] kvDic)
                {
                    var service = kvDic[0].Value;
                    var controller = kvDic[1].Value;
                    var method = kvDic[2].Value;

                    using (var client = new WebClientEx(1800000))
                    {
                        var nameContent = new NameValueCollection();
                        for (int i = 3; i < kvDic.Length; i++)
                        {
                            nameContent.Add(kvDic[i].Key, kvDic[i].Value.ToCompress());
                        }

                        var tokens = this.Request.Headers.Where(n => n.Key == "token").ToList();
                        string token = null;
                        if (tokens.Any())
                        {
                            token = tokens.FirstOrDefault().Value.FirstOrDefault();
                        }

                        client.Headers.Add("token", token);

                        var response = client.UploadValues($"http://127.0.0.1:1024/{service}/{controller}/{method}", nameContent);
                        string resStr = Encoding.UTF8.GetString(response);
                        ServiceResult serviceResult = JsonConvert.DeserializeObject<ServiceResult>(resStr);
                        var result = new Result<object>(serviceResult.ReturnCode.Decompress<ReturnCode>(), serviceResult.ReturnMessage.Decompress<string>(), serviceResult.Content.Decompress<object>());
                        return JsonConvert.SerializeObject(result);
                    }
                }
                return JsonConvert.SerializeObject("转发服务异常！url地址不正确");
            }
            catch (WebException ex)
            {
                string errMsg = "转发服务异常！详细信息：" + $"请求地址:{str}" + Environment.NewLine + ex;
                if (ex.Response is HttpWebResponse mResponse)
                {
                    var responseStream = mResponse.GetResponseStream();
                    string msg = "";
                    if (responseStream != null)
                    {
                        var streamReader = new StreamReader(responseStream, Encoding.UTF8);
                        //获取返回的信息
                        msg = streamReader.ReadToEnd();
                        streamReader.Close();
                        responseStream.Close();
                    }
                    errMsg += (Environment.NewLine + "返回数据:" + msg);
                }

                return JsonConvert.SerializeObject(errMsg);
            }
            catch (Exception ex)
            {
                string errMsg = "转发服务异常！详细信息：" + ex + Environment.NewLine + $"请求地址:{str}";

                return JsonConvert.SerializeObject(errMsg);
            }
        }

        public IUnitOfWork GetUnitOfWork<T>() where T : IUnitOfWork
        {
            return ServiceLocator.Instance.GetService<T>(new ContextCache());
        }

        /// <summary>
        /// 第三方登录
        /// </summary>
        /// <param name="encode"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult ThirdpartyLogin(string encode)
        {
            // 获取入参
            var keyValuePairs = Request.Properties["MS_QueryNameValuePairs"];
            if (keyValuePairs is KeyValuePair<string, string>[] kvDic && kvDic.Length == 1)
            {
                encode = kvDic[0].Value;
            }

            // 生成JWT Token
            var mediToken = new MediToken("HALO", "HALO", encode, DateTime.Now.AddHours(1), new AuthInfo { UserID = encode });
            var token = mediToken.CreateToken();

            return Ok(token);
        }

        /// <summary>
        /// 重定向
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <param name="encode"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult Redirect(string returnUrl, string encode)
        {
            // 获取入参
            var keyValuePairs = Request.Properties["MS_QueryNameValuePairs"];
            if (keyValuePairs is KeyValuePair<string, string>[] kvDic && kvDic.Length == 2)
            {
                returnUrl = kvDic[0].Value;
                encode = kvDic[1].Value;
            }

            // 生成JWT Token
            var mediToken = new MediToken("HALO", "HALO", encode, DateTime.Now.AddHours(12), new AuthInfo { UserID = encode });
            var token = mediToken.CreateToken();

            // 判断url中是否原来就存在参数
            if (returnUrl.Contains("?"))
            {
                return Redirect($"{returnUrl}&token={token}");
            }
            return Redirect($"{returnUrl}?token={token}");
        }

        /// <summary>
        /// 验证Token
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult VerifyToken(string token)
        {
            // 获取入参
            var keyValuePairs = Request.Properties["MS_QueryNameValuePairs"];
            if (keyValuePairs is KeyValuePair<string, string>[] kvDic && kvDic.Length == 1)
            {
                token = kvDic[0].Value;
            }

            var payload = MediToken.GetTokenPayLoad(token);
            var encode = payload.AuthInfo.UserID;

            string userId;
            string password;
            try
            {
                // Base64解密
                var userInfo = Base64Util.Base64Decode(encode);
                var spilt = userInfo.Split('&');
                userId = spilt[0];
                password = spilt[1];
            }
            catch (Exception)
            {
                return BadRequest("Token不合法!");
            }

            // 创建查询实例
            var query = new QueryService(GetUnitOfWork<IHISUnitOfWork>());
            // 获取用户
            var yongHu = new E_GY_YONGHUXX();
            yongHu.Where("where yonghuid=:id and tingyongbz = 0", userId);
            var user = query.Get(yongHu).FirstOrDefault();

            // 错误入参
            if (user == null) return BadRequest("Token不合法!");

            // 获取是否加密的参数
            var canshu = new E_GY_CANSHU();
            canshu.Where("where canshuid = '公用_用户密码是否加密'");
            var value = query.Get(canshu).FirstOrDefault()?.CANSHUZHI;
            var isEncrypt = false;

            // 参数为空默认false
            if (!string.IsNullOrWhiteSpace(value)) isEncrypt = value == "1";

            // 加密参数需要加密后对比
            if (isEncrypt)
            {
                if (user.MIMA != SHA256.Encrypt(password)) return BadRequest("Token不合法!");
            }
            else
            {
                if (user.MIMA != password) return BadRequest("Token不合法!");
            }
            
            return Ok("认证通过");
        }
    }
}
