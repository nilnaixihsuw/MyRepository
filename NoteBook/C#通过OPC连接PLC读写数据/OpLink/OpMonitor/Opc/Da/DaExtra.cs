using OPCAutomation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpMonitor
{
    public class DaExtra
    {
        public DaExtra()
        {
            ItmHandleClient = default(int);
            ItmHandleServer = default(int);
        }
    
        /// <summary>
        /// 唯一标识（获取变化量时使用）
        /// </summary>
        public int ItmHandleClient { get; set; }
        /// <summary>
        /// 唯一标识（删除时使用）
        /// </summary>
        public int ItmHandleServer { get; set; }

        public OPCItem item { get; set; }

    }
}
