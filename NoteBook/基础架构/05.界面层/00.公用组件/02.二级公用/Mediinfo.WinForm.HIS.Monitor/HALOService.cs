using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.ServiceProcess;
using System.Text;

namespace Mediinfo.WinForm.HIS.Monitor
{
    public partial class HALOService : ServiceBase
    {
        private System.Timers.Timer readTimer;
        private System.Timers.Timer writeTimer;
        public HALOService()
        {
            InitializeComponent();
            readTimer = new System.Timers.Timer();
            readTimer.Elapsed += ReadTimer_Elapsed;
            writeTimer = new System.Timers.Timer();
            writeTimer.Elapsed += WriteTimer_Elapsed;
        }

        protected override void OnStart(string[] args)
        {
            var writetime = YamlConfigManager.GetWriteInterval();
            var readtime = YamlConfigManager.GetReadInterval();
            readTimer.Interval = readtime;
            readTimer.Start();
            writeTimer.Interval = writetime;
            writeTimer.Start();
        }

        protected override void OnStop()
        {
            readTimer.Elapsed -= ReadTimer_Elapsed;
            writeTimer.Elapsed -= WriteTimer_Elapsed;
        }

        private void WriteTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            writeinfotimer();
        }

        private void ReadTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            readinfotimer();
        }

        /// <summary>
        /// 定时写日志
        /// </summary>
        private void writeinfotimer()
        {
            string sqlitename = AppDomain.CurrentDomain.BaseDirectory + "HaloMonitor.db";
            if (File.Exists(sqlitename))
            {
                string connStr = @"Data Source=" + @"" + sqlitename + ";Initial Catalog=sqlite;Integrated Security=True;Max Pool Size=10";
                string applicationMode = YamlConfigManager.GetApplicationMode();
                if (applicationMode == "Server")
                {
                    foreach (ServiceProcessInfo serviceProcessInfo in GetServiceInfo())
                    {
                        string insertsql = "INSERT INTO ServerProcessInfoTable (ProcessId,ServiceName,ThreadCount,MemorySize,BaoId,JiXianId,ComputerIp) VALUES (@ProcessId,@ServiceName,@ThreadCount,@MemorySize,@BaoId,@JiXianId,@ComputerIp)";
                        SQLiteHelper.ExecuteNonQuery(connStr, insertsql, new SQLiteParameter[]{
                        new SQLiteParameter("@ProcessId",serviceProcessInfo.ProcessID),
                        new SQLiteParameter("@ServiceName",serviceProcessInfo.ProcessName),
                        new SQLiteParameter("@ThreadCount",serviceProcessInfo.ProcessThreads),
                        new SQLiteParameter("@MemorySize",serviceProcessInfo.ProcessMemory),
                        new SQLiteParameter("@BaoId",serviceProcessInfo.BaoID),
                        new SQLiteParameter("@JiXianId",serviceProcessInfo.JiXianID),
                        new SQLiteParameter("@ComputerIp",serviceProcessInfo.MachineIP)
                    });
                    }
                }
                if (applicationMode == "Cleint")
                {
                    foreach (ClientProcessInfo clientProcessInfo in GetClientInfo())
                    {
                        string insertsql = "INSERT INTO ClientProcessInfoTable (ProcessId,ProcessName,ThreadCount,MemorySize,ComputerIp,XiTongId) VALUES (@ProcessId,@ProcessName, @ThreadCount,@MemorySize,@ComputerIp,@XiTongId)";
                        SQLiteHelper.ExecuteNonQuery(connStr, insertsql, new SQLiteParameter[]{
                        new SQLiteParameter("@ProcessId",clientProcessInfo.ProcessID),
                        new SQLiteParameter("@ProcessName",clientProcessInfo.ProcessName),
                        new SQLiteParameter("@ThreadCount",clientProcessInfo.ProcessThreads),
                        new SQLiteParameter("@MemorySize",clientProcessInfo.ProcessMemory),
                        new SQLiteParameter("@ComputerIp",clientProcessInfo.MachineIP),
                        new SQLiteParameter("@XiTongId",clientProcessInfo.XiTongId)
                    });
                    }

                }

            }

        }
        /// <summary>
        /// 定时读取日志
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void readinfotimer()
        {
            string sqlitename = AppDomain.CurrentDomain.BaseDirectory + "HaloMonitor.db";
            if (File.Exists(sqlitename))
            {
                string applicationMode = YamlConfigManager.GetApplicationMode();
                if (applicationMode == "Server")
                {
                    string connStr = @"Data Source=" + @"" + sqlitename + ";Initial Catalog=sqlite;Integrated Security=True;Max Pool Size=10";
                    string querysql = "select * from ServerProcessInfoTable limit @Count";
                    DataSet serverDataTable = SQLiteHelper.ExecuteDataSet(connStr, querysql, new SQLiteParameter[] { new SQLiteParameter("@Count", 100) });
                    foreach (DataRow dataRow in serverDataTable.Tables[0].Rows)
                    {
                        //string content = "{\r\n\"进程ID\":\""+dataRow["ProcessId"] + "\",\r\n\"进程名称\":\"" + dataRow["ServiceName"] + "\",\r\n\"线程数\":\"" + dataRow["ThreadCount"] + "\",\r\n\"内存大小\":\"" + dataRow["MemorySize"] + "\",\r\n\"包ID\":\"" + dataRow["BaoId"] + "\",\r\n\"基线ID\":\"" + dataRow["JiXianId"] + "\",\r\n\"电脑IP\":\"" + dataRow["ComputerIp"] + "\",\r\n\"创建时间\":\"" + dataRow["CreateTime"] + "\"\r\n}";
                        ServerLogEntity serverLogEntity = new ServerLogEntity();
                        serverLogEntity.ProcessID = dataRow["ProcessId"];
                        serverLogEntity.ProcessName = dataRow["ServiceName"];
                        serverLogEntity.ProcessThreads = dataRow["ThreadCount"];
                        serverLogEntity.ProcessMemory = dataRow["MemorySize"];
                        serverLogEntity.BaoID = dataRow["BaoId"];
                        serverLogEntity.JiXianID = dataRow["JiXianId"];
                        serverLogEntity.MachineIP = dataRow["ComputerIp"];
                        serverLogEntity.CreateTime = dataRow["CreateTime"];
                        PushLog(LogLevel.Info, serverLogEntity);
                        string deleteStr = "delete from ServerProcessInfoTable where Id = @Id";
                        SQLiteHelper.ExecuteNonQuery(connStr, deleteStr, new SQLiteParameter[]
                        {
                            new SQLiteParameter("@Id",dataRow["Id"])
                        });
                    }
                }
                if (applicationMode == "Cleint")
                {
                    string connStr = @"Data Source=" + @"" + sqlitename + ";Initial Catalog=sqlite;Integrated Security=True;Max Pool Size=10";
                    string querysql = "select * from clientProcessInfoTable limit @Count";
                    DataSet clientDataTable = SQLiteHelper.ExecuteDataSet(connStr, querysql, new SQLiteParameter[] { new SQLiteParameter("@Count", 100) });
                    foreach (DataRow dataRow in clientDataTable.Tables[0].Rows)
                    {

                        ClientLogEntity clientLogEntity = new ClientLogEntity();
                        clientLogEntity.ProcessID = dataRow["ProcessId"];
                        clientLogEntity.ProcessName = dataRow["ProcessName"];
                        clientLogEntity.ProcessThreads = dataRow["ThreadCount"];
                        clientLogEntity.ProcessMemory = dataRow["MemorySize"];
                        clientLogEntity.XiTongId = dataRow["XiTongId"];
                        clientLogEntity.MachineIP = dataRow["ComputerIp"];
                        clientLogEntity.CreateTime = dataRow["CreateTime"];
                        //string content = "{\r\n\"进程ID\":\"" + dataRow["ProcessId"] + "\",\r\n\"进程名称\":\"" + dataRow["ProcessName"] + "\r\n\",\"线程数\":\"" + dataRow["ThreadCount"] + "\",\r\n\"内存大小\":\"" + dataRow["MemorySize"] + "\",\r\n\"系统ID\":\"" + dataRow["XiTongId"] + "\",\r\n\"电脑IP\":\"" + dataRow["ComputerIp"] + "\r\n\",\"创建时间\":\"" + dataRow["CreateTime"] + "\"\r\n}";
                        PushLog(LogLevel.Info, clientLogEntity);
                        string deleteStr = "delete from ClientProcessInfoTable where Id = @Id";
                        SQLiteHelper.ExecuteNonQuery(connStr, deleteStr, new SQLiteParameter[]
                        {
                            new SQLiteParameter("@Id",dataRow["Id"])
                        });
                    }
                }
            }


        }
        /// <summary>
        /// 获取服务端监控信息
        /// </summary>
        /// <returns></returns>
        public List<ServiceProcessInfo> GetServiceInfo()
        {
            List<ServiceProcessInfo> list = new List<ServiceProcessInfo>();
            // 获取服务器上所有在运行的进程列表
            Process[] processes = Process.GetProcesses();
            foreach (Process item in processes)
            {
                // 判断服务名是否为集群模式的服务启动名
                if (YamlConfigManager.GetProcessNames().Contains(item.ProcessName))
                {
                    ServiceProcessInfo serviceProcessInfo = new ServiceProcessInfo
                    {
                        ProcessID = item.Id,
                        ProcessName = item.ProcessName,
                        ProcessThreads = item.Threads.Count
                    };
                    double memoryCount = GetPrcessMemory(item.Id);
                    serviceProcessInfo.ProcessMemory = memoryCount;
                    // 获取服务的完整路径，因为路径中包含当前服务的一个基线ID，包ID
                    string fileName = GetMainModuleFilepath(item.Id);
                    if (!String.IsNullOrWhiteSpace(fileName))
                    {
                        string[] arrayList = fileName.Split('\\');
                        if (arrayList.Length > 2)
                        {
                            serviceProcessInfo.BaoID = arrayList[arrayList.Length - 2];
                            serviceProcessInfo.JiXianID = arrayList[arrayList.Length - 3];
                        }
                    }
                    // 从配置文件中获取当前服务器的IP地址
                    string address = YamlConfigManager.GetHostIP();
                    if (!String.IsNullOrWhiteSpace(address))
                    {
                        serviceProcessInfo.MachineIP = address;
                    }
                    list.Add(serviceProcessInfo);
                }
            }
            return list;
        }
        /// <summary>
        /// 获取客户端监控信息
        /// </summary>
        /// <returns></returns>
        public List<ClientProcessInfo> GetClientInfo()
        {
            List<ClientProcessInfo> list = new List<ClientProcessInfo>();
            // 获取服务器上所有在运行的进程列表
            Process[] processes = Process.GetProcesses();
            foreach (Process item in processes)
            {
                // 判断服务名是否为集群模式的服务启动名
                if (YamlConfigManager.GetProcessNames().Contains(item.ProcessName))
                {
                    ClientProcessInfo clientProcessInfo = new ClientProcessInfo
                    {
                        ProcessID = item.Id,
                        ProcessName = item.ProcessName,
                        ProcessThreads = item.Threads.Count
                    };
                    double memoryCount = GetPrcessMemory(item.Id);
                    string fileName = GetMainModuleFilepath(item.Id);
                    if (!String.IsNullOrWhiteSpace(fileName) && fileName.Contains("Mediinfo.WinForm.HIS.Main.exe"))
                    {
                        string[] arrayList = fileName.Split('\\');
                        if (arrayList.Length > 2)
                        {
                            clientProcessInfo.XiTongId = arrayList[arrayList.Length - 2];
                        }
                    }
                    clientProcessInfo.ProcessMemory = memoryCount;
                    // 从配置文件中获取当前服务器的IP地址
                    string address = YamlConfigManager.GetHostIP();
                    if (!String.IsNullOrWhiteSpace(address))
                    {
                        clientProcessInfo.MachineIP = address;
                    }
                    list.Add(clientProcessInfo);
                }
            }
            return list;
        }

        /// <summary>
        /// 通过进程ID获取执行文件路径
        /// </summary>
        /// <param name="processId">进程ID</param>
        /// <returns>当前进程的可执行文件路径</returns>
        private string GetMainModuleFilepath(int processId)
        {
            //return Process.GetProcessById(processId).Modules[0].FileName;
            string wmiQueryString = "SELECT ProcessId, ExecutablePath FROM Win32_Process WHERE ProcessId = " + processId;
            using (var searcher = new ManagementObjectSearcher(wmiQueryString))
            {
                using (var results = searcher.Get())
                {
                    ManagementObject mo = results.Cast<ManagementObject>().FirstOrDefault();
                    if (mo != null)
                    {
                        return (string)mo["ExecutablePath"];
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// 根据进程ID查询内存大小
        /// </summary>
        /// <param name="processId">进程ID</param>
        /// <returns>当前进程占用内存大小</returns>
        private double GetPrcessMemory(int processId)
        {
            double totalMemory = 0;
            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2",
                                "SELECT * FROM Win32_PerfFormattedData_PerfProc_Process WHERE IDProcess = " + processId))
            {
                foreach (ManagementObject obj in searcher.Get())
                {
                    double memory = Convert.ToDouble(obj["WorkingSet"]) / 1024.0;
                    totalMemory += memory;
                }
            }

            return totalMemory;
        }
        /// <summary>
        /// 推送日志
        /// </summary>
        /// <param name="logLevel"></param>
        /// <param name="content"></param>
        public void PushLog(LogLevel logLevel, object content)
        {
            string configfile = AppDomain.CurrentDomain.BaseDirectory + @"\log4net.config";
            log4net.Config.XmlConfigurator.Configure(new FileInfo(configfile));
            var repository = log4net.LogManager.GetLogger("HALOMonitor");
            switch (logLevel)
            {
                case LogLevel.Info:
                    repository.Info(content);
                    break;
                case LogLevel.Warn:
                    repository.Warn(content);
                    break;
                case LogLevel.Error:
                    repository.Error(content);
                    break;
                default:
                    break;
            }
        }
    }
    /// <summary>
    /// 日志等级
    /// </summary>
    public enum LogLevel
    {
        Info = 0,//信息
        Warn = 1,//警告
        Error = 2 //错误
    }
}
