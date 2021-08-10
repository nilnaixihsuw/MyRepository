using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mediinfo.WinForm.HIS.Core
{
    public class DoctorSearchRequest
    {

        [JsonProperty("head")]
        public DoctorSearchHead Head { get; set; }
        [JsonProperty("body")]
        public DoctorSearchBody Body { get; set; }
    }

    public class DoctorSearchHead
    {
        [JsonProperty("clientId")]
        public string ClientId { get; set; }
    }

    public class DoctorSearchBody
    {
        [JsonProperty("openId")]
        public string OpenId { get; set; }
        [JsonProperty("phone")]
        public string phone { get; set; }
        [JsonProperty("userIdcardNum")]
        public string userIdcardNum { get; set; }
        [JsonProperty("employeeNumber")]
        public string employeeNumber { get; set; }
    }
}
