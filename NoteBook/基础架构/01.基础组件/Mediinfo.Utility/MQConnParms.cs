using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediinfo.Utility
{
    public class MQConnParms
    {
        public string MessageType { get; set; }
        public string MQExchange { get; set; }
        public string MQRoutingKey { get; set; }
    }
}
