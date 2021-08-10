using Mediinfo.DTO.HIS.GY;
using Mediinfo.Enterprise;
using Mediinfo.HIS.Core;
using Mediinfo.ServiceProxy.Core;
using Mediinfo.ServiceProxy.JCJG.GongYong;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mediinfo.WinForm.HIS.Core
{
    public class CAFactory
    {

        private static dynamic _tuple;
        private static dynamic tuple
        {
            get
            {
                if (_tuple == null)
                {
                    _tuple = GetYWXPZXX();
                }
                return _tuple;
            }
        }

        public SelfSignRequest selfSignRequest { get; set; }
        public DoctorSearchRequest doctorSearchRequest { get; set; }

        public static GetResult resultGet { get; set; }

        public static ResultErWm resultErWm { get; set; }

        public string Url { get; set; }

        public static string SFZH { get; set; }

        public static string ZGGH { get; set; }

        public static string DHHM { get; set; }

        public CAFactory() { }

        public CAFactory(string funciton, string dhhm, string sfzh, string zggh)
        {
            //var tuple = GetYWXPZXX();
            switch (funciton)
            {
                case "2.3.2同步医师结果查询接口":
                    doctorSearchRequest = new DoctorSearchRequest();
                    doctorSearchRequest.Head = new DoctorSearchHead();
                    //doctorSearchRequest.Head.ClientId = tuple.Item1;
                    doctorSearchRequest.Body = new DoctorSearchBody();
                    doctorSearchRequest.Body.OpenId = "";
                    doctorSearchRequest.Body.phone = dhhm;
                    doctorSearchRequest.Body.userIdcardNum = sfzh;
                    doctorSearchRequest.Body.employeeNumber = "";
                    //json = JsonConvert.SerializeObject(doctorSearchRequest);
                    //string action1 = "gateway/doctor/synDoctorSearch";
                    //Url = tuple.Item3 + action1;
                    break;
                case "4.3.2自动签名授权-获取授权结果接口":
                    Enterprise.Log.ClientLogHelper.Intance.WriteLog("开始4.3.2自动签名授权-获取授权结果接口");
                    selfSignRequest = new SelfSignRequest();
                    selfSignRequest.head = new SelfSignRequestHead();
                    selfSignRequest.body = new SelfSignRequestBody();
                    selfSignRequest.body.openId = GetOpenId();// "c23fa15a6b7f9b2bq3244w2f98ya1fb1a2d"; //GetOpenId();//"c23fa15a6b7f9b2bq3244w2f98ya1fb1a2d";
                    selfSignRequest.body.sysTag = "his";
                    selfSignRequest.head.clientId = tuple.Item1;
                    selfSignRequest.head.clientSecret = tuple.Item2;
                    string action2 = "gateway/selfSign/getResult";
                    Url = tuple.Item3 + action2;
                    var json = JsonConvert.SerializeObject(selfSignRequest);
                    Enterprise.Log.ClientLogHelper.Intance.WriteLog("4.3.2自动签名授权-获取授权结果接口入参：" + json.ToString());
                    RestfulClient client1 = new RestfulClient(Url, ServiceProxy.Core.HttpVerbNew.POST, "application/json", json);
                    string result = string.Empty;
                    result = client1.MakeRequest();
                    resultGet = JsonConvert.DeserializeObject<GetResult>(result);
                    break;
                case "4.3.1自动签名授权-请求自动签名授权接口":
                    Enterprise.Log.ClientLogHelper.Intance.WriteLog("开始获取二维码4.3.1自动签名授权-请求自动签名授权接口");
                    selfSignRequest = new SelfSignRequest();
                    selfSignRequest.head = new SelfSignRequestHead();
                    selfSignRequest.head.clientId = tuple.Item1;
                    selfSignRequest.head.clientSecret = tuple.Item2;
                    selfSignRequest.body = new SelfSignRequestBody();
                    selfSignRequest.body.openId = GetOpenId(); /*"c23fa15a6b7f9b2bq3244w2f98ya1fb1a2d"*/;//;//"c23fa15a6b7f9b2bq3244w2f98ya1fb1a2d";//"c23fa15a6b7f9b2bq3244w2f98ya1fb1a2d";//OpenID;
                    Enterprise.Log.ClientLogHelper.Intance.WriteLog("开始获取二维码4.3.1Openid：" + selfSignRequest.body.openId);
                    selfSignRequest.body.sysTag = "his";
                    string action3 = "gateway/selfSign/request";
                    Url = tuple.Item3 + action3;
                    json = JsonConvert.SerializeObject(selfSignRequest);
                    Enterprise.Log.ClientLogHelper.Intance.WriteLog("4.3.1自动签名授权 - 请求自动签名授权接口入参：" + json.ToString());
                    ServiceProxy.Core.RestfulClient client2 = new ServiceProxy.Core.RestfulClient(Url, ServiceProxy.Core.HttpVerbNew.POST, "application/json", json);
                    string result2 = string.Empty;
                    result2 = client2.MakeRequest();
                    resultErWm = JsonConvert.DeserializeObject<ResultErWm>(result2);
                    break;
                case "4.3.3自动签名授权 - 退出授权接口":
                    Enterprise.Log.ClientLogHelper.Intance.WriteLog("开始获取二维码4.3.3自动签名授权 - 退出授权接口");
                    selfSignRequest = new SelfSignRequest();
                    selfSignRequest.head = new SelfSignRequestHead();
                    selfSignRequest.head.clientId = tuple.Item1;
                    selfSignRequest.head.clientSecret = tuple.Item2;
                    selfSignRequest.body = new SelfSignRequestBody();
                    selfSignRequest.body.openId = GetOpenId(); /*"c23fa15a6b7f9b2bq3244w2f98ya1fb1a2d"*/;//;//"c23fa15a6b7f9b2bq3244w2f98ya1fb1a2d";//"c23fa15a6b7f9b2bq3244w2f98ya1fb1a2d";//OpenID;
                    Enterprise.Log.ClientLogHelper.Intance.WriteLog("开始获取二维码4.3.3Openid：" + selfSignRequest.body.openId);
                    selfSignRequest.body.sysTag = "his";
                    string action4 = "gateway/selfSign/quit";
                    Url = tuple.Item3 + action4;
                    json = JsonConvert.SerializeObject(selfSignRequest);
                    Enterprise.Log.ClientLogHelper.Intance.WriteLog("4.3.3自动签名授权 - 退出授权接口入参：" + json.ToString());
                    ServiceProxy.Core.RestfulClient client3 = new ServiceProxy.Core.RestfulClient(Url, ServiceProxy.Core.HttpVerbNew.POST, "application/json", json);
                    string result3 = string.Empty;
                    result3 = client3.MakeRequest();
                    Enterprise.Log.ClientLogHelper.Intance.WriteLog("4.3.3自动签名授权 - 退出授权接口回参：" + result3);
                    resultErWm = JsonConvert.DeserializeObject<ResultErWm>(result3);
                    break;

            }
        }

        /// <summary>
        /// CA退出授权接口
        /// </summary>
        /// <returns></returns>
        public static bool CALoginOut()
        {
            try
            {
                CAFactory cAFactory = new CAFactory("4.3.3自动签名授权 - 退出授权接口", "", "", "");
                if (resultErWm.message == "success")
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                Enterprise.Log.ClientLogHelper.Intance.WriteLog("4.3.3自动签名授权 - 退出授权接口异常：" + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 医网信配置信息 item1=clientid,item2 = ClientSecret,item3 = url
        /// </summary>
        public static Tuple<string, string, string> GetYWXPZXX()
        {
            var canShuZhi = GetCanshuById("电子病历_北京CA签名接口(医网信)配置信息");
            var dic = JsonConvert.DeserializeObject<Dictionary<string, string>>(canShuZhi);
            if (dic == null)
            {
                return new Tuple<string, string, string>("", "", ""); ;
            }
            string ClientID = dic.ContainsKey("clientId") ? dic["clientId"] : "";//"2017070411003376";//;
            string ClientSecret = dic.ContainsKey("clientSecret") ? dic["clientSecret"] : ""; //"2017070411003360";//dic.ContainsKey("clientSecret") ? dic["clientSecret"] : "";
            string YXWUrl = dic.ContainsKey("Url") ? dic["Url"] : ""; //"https://test.51trust.com/";//
            var tuple = new Tuple<string, string, string>(ClientID, ClientSecret, YXWUrl);
            return tuple;
        }

        /// <summary>
        /// 登录前获取参数方法
        /// </summary>
        /// <param name="canshuid"></param>
        /// <returns></returns>
        public static string GetCanshuById(string canshuid)
        {
            var result = new JCJGCanSuService().GetCanShu(canshuid.Trim());// jCJGCanSu.GetCanShu("公用_是否强制扫码登录");
            if (result.ReturnCode == ReturnCode.SUCCESS && result.Return.Count > 0)
            {
                return result.Return[0].CANSHUZHI;
            }
            return "";
        }

        /// <summary>
        /// 获取扫码登录前入参（openid）
        /// </summary>
        /// <returns></returns>
        public static string GetOpenId()
        {
            try
            {
                Enterprise.Log.ClientLogHelper.Intance.WriteLog("开始2.3.2同步医师结果查询接口");
                if (string.IsNullOrWhiteSpace(DHHM) || string.IsNullOrWhiteSpace(SFZH) || string.IsNullOrWhiteSpace(ZGGH))
                {
                    DHHM = HISClientHelper.ClientSetting.GetConfigItemValue("CASM", "DHHM");
                    SFZH = HISClientHelper.ClientSetting.GetConfigItemValue("CASM", "SFZH");
                    ZGGH = HISClientHelper.ClientSetting.GetConfigItemValue("CASM", "ZGGH");
                }
                CAFactory cAFactory = new CAFactory("2.3.2同步医师结果查询接口", DHHM, SFZH, ZGGH);
                cAFactory.doctorSearchRequest.Head.ClientId = tuple.Item1;
                string action1 = "gateway/doctor/synDoctorSearch";
                cAFactory.Url = tuple.Item3 + action1;
                var json = JsonConvert.SerializeObject(cAFactory.doctorSearchRequest);
                Enterprise.Log.ClientLogHelper.Intance.WriteLog("2.3.2同步医师结果查询接口入参：" + json.ToString());
                ServiceProxy.Core.RestfulClient client = new ServiceProxy.Core.RestfulClient(cAFactory.Url, ServiceProxy.Core.HttpVerbNew.POST, "application/json", json);

                string result = string.Empty;
                GetDoctorResult getDoctorResult = new GetDoctorResult();
                result = client.MakeRequest();
                getDoctorResult = JsonConvert.DeserializeObject<GetDoctorResult>(result);
                if (getDoctorResult.message == "success")
                {
                    Enterprise.Log.ClientLogHelper.Intance.WriteLog("2.3.2同步医师结果查询接口成功：" + getDoctorResult.data.openId);
                    return getDoctorResult.data.openId;

                }
                return "";
            }
            catch (Exception ex)
            {
                Enterprise.Log.ClientLogHelper.Intance.WriteLog("2.3.2同步医师结果查询接口异常：" + ex.Message);
            }
            return "";
        }
    }
}
