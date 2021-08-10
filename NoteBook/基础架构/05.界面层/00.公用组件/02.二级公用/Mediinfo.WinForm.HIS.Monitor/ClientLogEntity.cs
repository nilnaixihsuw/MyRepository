using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mediinfo.WinForm.HIS.Monitor
{
    public class ClientLogEntity
    {
        /// <summary>
        /// 进程id
        /// </summary>
        public object ProcessID { get; set; }
        /// <summary>
        /// 进程名称
        /// </summary>
        public object ProcessName { get; set; }

        /// <summary>
        /// 进程数量
        /// </summary>
        public object ProcessThreads { get; set; }
        /// <summary>
        /// 内存大小
        /// </summary>
        public object ProcessMemory { get; set; }
        /// <summary>
        /// 机器ip地址
        /// </summary>
        public object MachineIP { get; set; }
        /// <summary>
        /// 系统ID
        /// </summary>
        public object XiTongId { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public object CreateTime { get; set; }
    }
}

