using Mediinfo.Enterprise.Config;
using Mediinfo.HIS.Core;
using Mediinfo.Infrastructure.Core.MessageQueue;
using Mediinfo.Utility.Extensions;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mediinfo.Infrastructure.Oracle.Core
{
    public class DataSheetMonitoring
    {
        private static OracleDependency dep;
        private static OracleConnection conn;

        /// <summary>
        /// 启动数据表监控
        /// </summary>
        /// <param name="strCon">连接字符串</param>
        /// <param name="sql">sql查询语句</param>
        /// <param name="dependencyPort">数据库监控端口</param>
        /// <param name="socketPort">通信端口</param>
        //public static bool StartOracleMonitoring(string strCon, string sql, int dependencyPort)
        //{
        //    bool bluess = false;
        //    if (dep == null || !dep.IsEnabled)
        //    {
        //        if (string.IsNullOrEmpty(strCon))
        //        {
        //            strCon = MediinfoConfig.GetValue("DbConfig.xml", "HIS6");
        //        }
        //        //strCon = strCon.Replace("SERVICE_NAME", "SID");
        //        //设置App的监听端口，即使用哪个端口接收Change Notification。
        //        OracleDependency.Port = GetFirstAvailablePort(dependencyPort);
        //        conn = new OracleConnection(strCon);
        //        conn.Open();

        //        OracleCommand cmd = new OracleCommand(sql, conn);
        //        //绑定OracleDependency实例与OracleCommand实例
        //        dep = new OracleDependency(cmd);
        //        //指定Notification是object-based还是query-based，前者表示表（本例中为tab_cn）中任意数据变化时都会发出Notification；后者提供更细粒度的Notification，例如可以在前面的sql语句中加上where子句，从而指定Notification只针对查询结果里的数据，而不是全表。
        //        dep.QueryBasedNotification = false;
        //        //是否在Notification中包含变化数据对应的RowId
        //        dep.RowidInfo = OracleRowidInfo.Include;
        //        //指定收到Notification后的事件处理方法
        //        dep.OnChange += Dep_OnChange;
        //        cmd.AddRowid = true;
        //        //是否在一次Notification后立即移除此次注册
        //        cmd.Notification.IsNotifiedOnce = false;
        //        //此次注册的超时时间（秒），超过此时间，注册将被自动移除。0表示不超时。
        //        cmd.Notification.Timeout = 0;
        //        //False表示Notification将被存于内存中，True表示存于数据库中，选择True可以保证即便数据库重启之后，消息仍然不会丢失
        //        cmd.Notification.IsPersistent = true;
        //        DataTable t = new DataTable();
        //        OracleDataAdapter adapter = new OracleDataAdapter(cmd);
        //        adapter.Fill(t);
        //        //cmd.ExecuteReader();    
        //        if (dep != null && dep.IsEnabled)
        //        {
        //            bluess = true;
        //        }
        //    }
        //    else
        //    {
        //        bluess = true;
        //    }
        //    return bluess;
        //}

        /// <summary>
        /// 获取第一个可用端口
        /// </summary>
        /// <param name="port"></param>
        /// <returns></returns>
        private static int GetFirstAvailablePort(int port)
        {
            if (PortIsAvailable(port))
            {
                return port;
            }
            else
            {
                const int Max_Port = 65535;//系统TCP/UDP端口最大输
                const int Begin_Port = 5001;//默认开始端口
                if (port == 5000)
                {
                    for (int i = Begin_Port; i < Max_Port; i++)
                    {
                        if (PortIsAvailable(i))
                        {
                            return i;
                        }
                    }
                }
                else
                {
                    for (int i = 6000; i < Max_Port; i++)
                    {
                        if (PortIsAvailable(i))
                        {
                            return i;
                        }
                    }
                }
                return -1;
            }
        }

        /// <summary>
        /// 获取系统已用端口号
        /// </summary>
        /// <returns></returns>
        private static IList PortIsUsed()
        {
            //获取本地计算机网络连接和通讯统计数据的信息
            IPGlobalProperties iPGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();
            //返回本地计算机上的所有TCP监听
            IPEndPoint[] iPTcpPoints = iPGlobalProperties.GetActiveTcpListeners();
            //返回本地计算机上的所有UDP监听
            IPEndPoint[] iPUdpPoints = iPGlobalProperties.GetActiveUdpListeners();
            //返回本地计算机上的Internet协议版本4(IPC4传输控制协议TCP连接的信息)
            TcpConnectionInformation[] tcpConnections = iPGlobalProperties.GetActiveTcpConnections();

            IList allPort = new ArrayList();
            foreach (IPEndPoint point in iPTcpPoints)
            {
                allPort.Add(point.Port);
            }
            foreach (IPEndPoint point in iPUdpPoints)
            {
                allPort.Add(point.Port);
            }
            foreach (TcpConnectionInformation point in tcpConnections)
            {
                allPort.Add(point.LocalEndPoint.Port);
                allPort.Add(point.RemoteEndPoint.Port);
            }
            return allPort;
        }

        /// <summary>
        /// 判断当前端口是否已使用
        /// </summary>
        /// <param name="port"></param>
        /// <returns></returns>
        private static bool PortIsAvailable(int port)
        {
            bool IsAvailable = true;
            IList portUsed = PortIsUsed();
            foreach (object p in portUsed)
            {
                if (!string.IsNullOrEmpty(p.ToStringEx()))
                {
                    if (p.ToStringEx().ToInt() == port)
                    {
                        IsAvailable = false;
                        break;
                    }
                }
            }
            return IsAvailable;
        }

        /// <summary>
        /// 监控处理事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        private static void Dep_OnChange(object sender, OracleNotificationEventArgs eventArgs)
        {
            DataTable dt = eventArgs.Details;
            string rowID = dt.Rows[0]["rowid"].ToStringEx();
            // 获取在线用户
            string sql = "select zhuangtaiid from xt_zaixianzt a where a.jieshusj>GETDATE()";
            OracleCommand cmd = new OracleCommand(sql, conn);
            DataTable table = new DataTable();
            OracleDataAdapter adapter = new OracleDataAdapter(cmd);
            adapter.Fill(table);
            IEnumerable<string> receivers = table.AsEnumerable().Select(d => d.Field<string>("zhuangtaiid")).ToArray();
            HISMessageBody messageBody = new HISMessageBody();
            messageBody.Receivers = receivers.ToArray();
            messageBody.XiaoXiNR = rowID;
            messageBody.XiaoXiBM = "yz_biandongxx";

            using (var client = MessageQueueClientFactory.CreateUserClient())
            {
                client.PublishUserMsg(receivers, messageBody);
            }
        }

        /// <summary>
        /// 获取本机地址IP
        /// </summary>
        /// <returns></returns>
        private static string IpAddressXX()
        {
            IPHostEntry IpEntry = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress item in IpEntry.AddressList)
            {
                if (item.AddressFamily == AddressFamily.InterNetwork)
                {
                    return item.ToString();
                }
            }
            return "";
        }

        /// <summary>
        /// 注销监控注册
        /// </summary>
        public static void EndOracleMonitoring()
        {
            if (dep != null)
            {
                if (dep.IsEnabled)
                {
                    //注销
                    dep.RemoveRegistration(conn);
                }
            }
        }
    }
}
