using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mediinfo.WinForm.HIS.Core
{
    public class SelfSignRequest
    {
        [JsonProperty("head")]
        public SelfSignRequestHead head { get; set; }
        [JsonProperty("body")]
        public SelfSignRequestBody body { get; set; }
    }

    public class SelfSignRequestBody
    {
        public string openId { get; set; }
        public string sysTag { get; set; }
    }

    public class SelfSignRequestHead
    {
        public string clientId { get; set; }
        public string clientSecret { get; set; }
    }
}
