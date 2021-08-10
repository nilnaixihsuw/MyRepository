using Mediinfo.Enterprise;
using Mediinfo.Utility.Compress;
using System;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Mediinfo.ServiceProxy.Core
{
    
    public enum HttpVerbNew
    {
        GET,            //method  常用的就这几样，可以添加其他的   get：获取    post：修改    put：写入    delete：删除
        POST,
        PUT,
        DELETE
    }
    /// <summary>
    /// Restful客户端
    /// </summary>
    public class RestfulClient
    {

        public string EndPoint { get; set; }    //请求的url地址  
        public HttpVerbNew Method { get; set; }    //请求的方法
        public string ContentType { get; set; } //格式类型
        public string PostData { get; set; }    //传送的数据


        public RestfulClient(string endpoint, HttpVerbNew method, string contentType, string postData)
        {
            EndPoint = endpoint;
            Method = method;
            ContentType = contentType;
            PostData = postData;
        }

        /// <summary>
        /// 请求方式
        /// </summary>
        /// <returns></returns>
        public string MakeRequest()
        {
            return MakeRequest("");
        }


        public string MakeRequest(string parameters)
        {
            // 添加https
            if (EndPoint.Substring(0, 8) == "https://")
            {
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)0XC00;
            }
            var request = (HttpWebRequest)WebRequest.Create(EndPoint + parameters);

            // end添加https
            request.Method = Method.ToString();
            request.ContentLength = 0;
            request.ContentType = ContentType;

            if (!string.IsNullOrEmpty(PostData) && Method == HttpVerbNew.POST)//如果传送的数据不为空，并且方法是post
            {
                var encoding = new UTF8Encoding();
                var bytes = Encoding.GetEncoding("utf-8").GetBytes(PostData);//编码方式按自己需求进行更改，我在项目中使用的是UTF-8
                request.ContentLength = bytes.Length;
                using (var writeStream = request.GetRequestStream())
                {
                    writeStream.Write(bytes, 0, bytes.Length);
                }
            }

            if (!string.IsNullOrEmpty(PostData) && Method == HttpVerbNew.PUT)//如果传送的数据不为空，并且方法是put
            {
                var encoding = new UTF8Encoding();
                var bytes = Encoding.GetEncoding("utf-8").GetBytes(PostData);//编码方式按自己需求进行更改，我在项目中使用的是UTF-8
                request.ContentLength = bytes.Length;
                using (var writeStream = request.GetRequestStream())
                {
                    writeStream.Write(bytes, 0, bytes.Length);
                }
            }
            using (var response = (HttpWebResponse)request.GetResponse())
            {
                var responseValue = string.Empty;
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    var message = String.Format("Request failed. Received HTTP {0}", response.StatusCode);
                    throw new ApplicationException(message);
                }

                // grab the response
                using (var responseStream = response.GetResponseStream())
                {
                    if (responseStream != null)
                        using (var reader = new StreamReader(responseStream))
                        {
                            responseValue = reader.ReadToEnd();
                        }
                }

                return responseValue;
            }
        }

        /// <summary>
        /// 执行URL方法
        /// </summary>
        /// <param name="token"></param>
        /// <param name="serviceUrl"></param>
        /// <param name="method"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public static string Invoke(string token, string serviceUrl, string method, params ServiceParm[] parms)
        {
            using (var client = new WebClient())
            {
                var nameContent = new NameValueCollection();
                foreach (var item in parms)
                {
                    nameContent.Add(item.ParmName, item.ParmValue.ToCompress());
                }

                client.Headers.Add("token", token);
                var response = client.UploadValues(serviceUrl + "/api/" + method, nameContent);
                return Encoding.UTF8.GetString(response).Decompress<string>();
            }
        }

        /// <summary>
        /// 获取资源
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static ServiceResult Get(string url, string data)
        {
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.Method = "GET";
            request.ContentType = "application/json";

            byte[] byteData = UTF8Encoding.UTF8.GetBytes(data.ToString());
            request.ContentLength = byteData.Length;

            using (Stream postStream = request.GetRequestStream())
            {
                postStream.Write(byteData, 0, byteData.Length);
            }

            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                StreamReader reader = new StreamReader(response.GetResponseStream());
                return new ServiceResult(reader.ReadToEnd());
            }
        }

        /// <summary>
        /// 获取url并返回结果
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string Get(string url, int timeOut = 600000)
        {
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.Method = "GET";
            request.ServicePoint.Expect100Continue = false;
            request.ServicePoint.UseNagleAlgorithm = false;
            request.ServicePoint.ConnectionLimit = 65500;
            request.AllowWriteStreamBuffering = false;
            request.Proxy = null;
            request.Timeout = timeOut;
            ServicePointManager.DefaultConnectionLimit = 50;

            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                StreamReader reader = new StreamReader(response.GetResponseStream());
                string bodyResult = reader.ReadToEnd();
                if (request != null)
                {
                    request.Abort();
                    request = null;
                }

                return bodyResult;
            }
        }

        /// <summary>
        /// 异步获取url并返回结果
        /// </summary>
        /// <param name="url">Url地址</param>
        /// <param name="timeOut">超时时长</param>
        /// <returns></returns>
        public static async Task<string> GetAsync(string url, int timeOut = 600000)
        {
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.Method = "GET";
            request.ServicePoint.Expect100Continue = false;
            request.ServicePoint.UseNagleAlgorithm = false;
            request.ServicePoint.ConnectionLimit = 65500;
            request.AllowWriteStreamBuffering = false;
            request.Proxy = null;
            request.Timeout = timeOut;
            ServicePointManager.DefaultConnectionLimit = 512;

            using (HttpWebResponse response = await request.GetResponseAsync() as HttpWebResponse)
            {
                StreamReader reader = new StreamReader(response.GetResponseStream());
                string bodyResult = await reader.ReadToEndAsync();
                if (request != null)
                {
                    request.Abort();
                    request = null;
                }

                return bodyResult;
            }
        }

        /// <summary>
        /// 获取Url请求并返回结果
        /// </summary>
        /// <param name="url">Url地址</param>
        /// <param name="timeOut">超时时长</param>
        /// <returns></returns>
        public static string Post(string url, int timeOut = 600000)
        {
            string result = "";
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "POST";
            req.ContentLength = 0;
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            Stream stream = resp.GetResponseStream();
            // 获取内容
            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            {
                result = reader.ReadToEnd();
            }
            return result;
        }

        ///// <summary>
        ///// 新建或更新资源
        ///// </summary>
        ///// <param name="url"></param>
        ///// <param name="data"></param>
        ///// <returns></returns>
        //public static ServiceResult Post(string url, string data)
        //{
        //    using (HttpClient httpClient = new HttpClient())
        //    {
        //        httpClient.BaseAddress = new Uri(url);
        //        httpClient.DefaultRequestHeaders
        //              .Accept
        //              .Add(new MediaTypeWithQualityHeaderValue("application/json"));

        //        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "");
        //        request.Content = new StringContent(data);

        //        var responseTask = httpClient.SendAsync(request);

        //        return new ServiceResult(responseTask.Result.Content.ReadAsStringAsync().Result);

        //    }
        //}

        /// <summary>
        /// 更新资源
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static ServiceResult Put(string url, string data)
        {
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.Method = "PUT";
            request.ContentType = "application/json";

            byte[] byteData = UTF8Encoding.UTF8.GetBytes(data.ToString());
            request.ContentLength = byteData.Length;

            using (Stream postStream = request.GetRequestStream())
            {
                postStream.Write(byteData, 0, byteData.Length);
            }

            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                StreamReader reader = new StreamReader(response.GetResponseStream());
                return new ServiceResult(reader.ReadToEnd());
            }
        }

        /// <summary>
        /// 删除资源
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static ServiceResult Delete(string url, string data)
        {
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.Method = "DELETE";
            request.ContentType = "application/json";

            byte[] byteData = UTF8Encoding.UTF8.GetBytes(data.ToString());
            request.ContentLength = byteData.Length;

            using (Stream postStream = request.GetRequestStream())
            {
                postStream.Write(byteData, 0, byteData.Length);
            }

            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                StreamReader reader = new StreamReader(response.GetResponseStream());
                return new ServiceResult(reader.ReadToEnd());
            }
        }
    }
}
