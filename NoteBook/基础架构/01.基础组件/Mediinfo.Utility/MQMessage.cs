using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediinfo.Utility
{
    public class MQMessage
    {
        public string MessageType { get; set; }
        public string Channel { get; set; }
        public string Target { get; set; }
        public string HospitalID { get; set; }
        public string JiuZhenKH { get; set; }
        /// <summary>
        /// 触发的数据
        /// </summary>
        public object Messages { get; set; }
    }
}
