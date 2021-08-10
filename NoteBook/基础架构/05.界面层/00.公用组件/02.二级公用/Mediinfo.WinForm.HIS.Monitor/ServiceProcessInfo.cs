using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mediinfo.WinForm.HIS.Monitor
{
    public class ServiceProcessInfo
    {
        /// <summary>
        /// 进程id
        /// </summary>
        public int ProcessID { get; set; }
        /// <summary>
        /// 进程名称
        /// </summary>
        public string ProcessName { get; set; }

        /// <summary>
        /// 进程数量
        /// </summary>
        public int ProcessThreads { get; set; }
        /// <summary>
        /// 内存大小
        /// </summary>
        public double ProcessMemory { get; set; }
        /// <summary>
        /// 包id
        /// </summary>
        public string BaoID { get; set; }
        /// <summary>
        /// 基线id
        /// </summary>
        public string JiXianID { get; set; }
        /// <summary>
        /// 机器ip地址
        /// </summary>
        public string MachineIP { get; set; }
    }
}
