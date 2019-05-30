using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OPCAutomation;
using System.Net;
using System.Windows.Forms;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Collections.Specialized;
using OpMonitor;
using System.Threading;

namespace OpMonitor
{
    /// <summary>
    /// 采用Da Opc连接方式
    /// </summary>
    public class DaOpc : IOpcClient
    {
        #region 私有变量
        //*****************OPC服务器变量**************
        /// <summary>
        /// OPCServer Object
        /// </summary>
        public OPCServer KepServer;
        /// <summary>
        /// 主机名称
        /// </summary>
        private string strHostName = "";
        /// <summary>
        /// IP
        /// </summary>
        private string ip = "";
        /// <summary>
        /// OPCserver节点浏览器，可遍历其中的分组名称与变量名称
        /// </summary>
        private OPCBrowser oPCBrowser;
        /// <summary>
        /// OPC连接状态
        /// </summary>
        private bool opc_connected = false;
        /// <summary>
        /// OPCserver分组列表
        /// </summary>
        private List<string> branches;
        /// <summary>
        /// OPC变量列表
        /// </summary>
        private List<string> leafs;
        /// <summary>
        /// OPCGroup字典
        /// </summary>
        private readonly Dictionary<string, DaGroup> groupDictionary = new Dictionary<string, DaGroup>();
        object syncLock = new object();
        #endregion
        /// <summary>
        /// OPC服务端运行开始时间
        /// </summary>
        public string ServerStartTime { get; private set; }
        /// <summary>
        ///  OPC服务端版本号
        /// </summary>
        public string ServerVersion { get; private set; }
        /// <summary>
        /// OPC服务端状态描述
        /// </summary>
        public string ServerStateDesc { get; private set; }
        /// <summary>
        /// OPCServer列表
        /// </summary>
        /// <returns></returns>
        public List<string> ServerList { get; private set; }
        /// <summary>
        /// 服务端断开通知
        /// </summary>
        public Action<string> DisConnectedHandle { get; set; }

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public DaOpc()
            : this("127.0.0.1")
        {

        }
        /// <summary>
        /// 构造函数
        /// 1、初始化OPCServer
        /// 2、遍历OPCServer列表
        /// </summary>
        public DaOpc(string ip)
        {
            //通过IP来获取计算机名称，可用在局域网内 
            IPHostEntry ipHostEntry = Dns.GetHostEntry(IPAddress.Parse(ip));
            strHostName = ipHostEntry.HostName.ToString();
            this.ip = ip;
            //获取本地计算机上的OPCServerName
            if (KepServer != null)
            {
                throw new Exception("KepServer已打开，请误再次打开");
            }
            else
            {
                try
                {
                    KepServer = new OPCServer();
                    KepServer.ServerShutDown += new DIOPCServerEvent_ServerShutDownEventHandler(KepServer_DisConnected);
                    KepServer.OPCGroups.DefaultGroupIsActive = true;
                    KepServer.OPCGroups.DefaultGroupDeadband = 0;
                    //server列表
                    ServerList = new List<string>();

                    //object list = ((Array)KepServer.GetOPCServers(strHostName));// .net3.5用法
                    //遍历指定IP下的OPCServer列表
                    var list = ((object)KepServer.GetOPCServers(strHostName));// .net4.0用法
                    foreach (string turn in (Array)list)
                    {
                        ServerList.Add(turn);
                    }
                }
                catch (Exception err)
                {
                    throw new Exception("枚举本地OPC服务器出错," + err.Message);
                }
            }
        }
        #endregion
        #region IOpcClient 成员
        /// <summary>
        /// 连接OPCServer服务
        /// 1、初始化daOpcItems集合
        /// 2、创建浏览器对象
        /// 3、OPCServer信息获取
        /// </summary>
        /// <param name="ServerName"></param>
        /// <returns></returns>
        public void Connect(string ServerName)
        {
            try
            {
                //尝试连接OPC服务
                ConnectRemoteServer(ip, ServerName);
                //创建浏览器对象，由于服务器端的菜单是树形结构，可以通过创建浏览器对象，一步步浏览菜单，寻找需要浏览的Item
                oPCBrowser = KepServer.CreateBrowser();                
                ServerStartTime = KepServer.StartTime.ToString() + "   ";
                ServerVersion = KepServer.MajorVersion.ToString() + "." + KepServer.MinorVersion.ToString() + "." + KepServer.BuildNumber.ToString() + "   ";
                opc_connected = true;

            }
            catch (Exception ex)
            {
                //保存连接状态
                opc_connected = false;
                throw;
            }
        } 

        /// <summary>
        /// 断开与OPC服务器的连接(使用 KepServer.CreateBrowser,导致无法正常使用)
        /// 释放OPC中的所有Group成员及解除事件监听
        /// </summary>
        /// <returns></returns>
        public void DisConnect()
        {
            if (!opc_connected)
            {
                return;
            }
            KepServer.Disconnect();
            opc_connected = false;
        }
        /// <summary>
        /// OPCBrowser遍历组下所有点，并构建Tree节点
        /// </summary>
        /// <param name="nodes"></param>
        public void ShowBranchesByTree(TreeNodeCollection nodes)
        {
            TreeNode treeNode;
            oPCBrowser.ShowBranches();
            foreach (object branch in oPCBrowser)
            {
                if (branch.ToString().Equals("_Hints") || branch.ToString().Equals("_System"))//排除这两项
                {

                }
                else
                {
                    treeNode = nodes.Add(branch.ToString());
                    oPCBrowser.MoveDown(branch.ToString());
                    ShowBranchesByTree(treeNode.Nodes);
                    oPCBrowser.MoveUp();
                }
            }
        }
        /// <summary>
        /// 列出OPC服务端下的组名称
        /// </summary>
        /// <returns></returns>
        public List<string> ShowBranches()
        {
            branches = new List<string>();
            try
            {
                oPCBrowser.ShowBranches();
                foreach (object turn in oPCBrowser)
                {
                    if (turn.ToString().Contains("_Hints") || turn.ToString().Contains("_System"))//排除这两项
                    {

                    }
                    else
                    {
                        branches.Add(turn.ToString());
                    }
                }
                return branches;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 列出OPC服务端分组下所有标签名称
        /// </summary>
        /// <param name="groupName"></param>
        public List<string> ShowLeafs(string groupName)
        {
            try
            {
                leafs = new List<string>();
                oPCBrowser.MoveDown(groupName);
                //oPCBrowser.ShowBranches();
                //true 显示父节点
                oPCBrowser.ShowLeafs(true);
                foreach (object turn in oPCBrowser)
                {
                    if (turn.ToString().Contains("_Hints") || turn.ToString().Contains("_System"))//排除这两项
                    {

                    }
                    else
                    {
                        leafs.Add(turn.ToString());
                    }
                }
                oPCBrowser.MoveUp();
                return leafs;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 创建变量组 GroupTrigger GroupData
        /// </summary>
        /// <param name="groupName"></param>
        /// <returns></returns>
        public IGroup CreateGroup(string groupName)
        {
            try
            {
                var kepGroup = KepServer.OPCGroups.Add(groupName);
                kepGroup.UpdateRate = 250;
                kepGroup.IsActive = true;
                kepGroup.IsSubscribed = true;
                var group = new DaGroup(kepGroup);
                groupDictionary.Add(groupName, group);
                return group;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 删除组
        /// </summary>
        /// <param name="groupName"></param>
        /// <returns></returns>
        public bool RemoveGroupByGroupName(string groupName)
        {
            try
            {
                KepServer.OPCGroups.Remove(groupName);
                groupDictionary.Remove(groupName);

            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 清空删除所有组(当前有问题，多次调用时会报错)
        /// </summary>
        /// <returns></returns>
        public bool RemoveGroupsAll()
        {
            try
            {
                //解除事件监听
                //foreach (OPCGroup opcGroup in KepServer.OPCGroups)
                //{
                //    if (opcGroup.Name == "GroupTrigger")
                //    {
                //        opcGroup.DataChange -= new DIOPCGroupEvent_DataChangeEventHandler(KepGroupTrigger_DataChange);
                //        KepServer.OPCGroups.Remove(opcGroup);
                //        daTagsTrigger.Clear();
                //    }
                //    else if (opcGroup.Name == "GroupData")
                //    {
                //        opcGroup.DataChange -= new DIOPCGroupEvent_DataChangeEventHandler(KepGroupData_DataChange);
                //        KepServer.OPCGroups.Remove(opcGroup);
                //        daTagsData.Clear();
                //    }      
                //}
                ////释放掉OPC中的所有Group成员
                //KepServer.OPCGroups.RemoveAll();
            }
            catch (NotImplementedException)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 获取Tag值
        /// </summary>
        /// <param name="biList"></param>
        /// <param name="grouName"></param>
        /// <returns></returns>
        public List<Tag> GetTagValuesFromGroup(ref List<Tag> biList, string grouName)
        {
            biList = groupDictionary[grouName].Tags.ToList();
            return biList;
        }

        // 摘要: 
        //     根据组名获取组
        //     获取或设置与指定的键相关联的值。
        //
        // 参数: 
        //   key:
        //     要获取或设置的值的键。
        public DaGroup this[string groupName] { get { return this.groupDictionary[groupName]; } }
        #endregion
        #region 自定义
        /// <summary>
        /// 连接OPC服务器
        /// </summary>
        /// <param name="remoteServerIP">OPCServerIP</param>
        /// <param name="remoteServerName">OPCServer名称</param>
        private void ConnectRemoteServer(string remoteServerIP, string remoteServerName)
        {
            try
            {
                KepServer.Connect(remoteServerName, remoteServerIP);
                if (KepServer.ServerState == (int)OPCServerState.OPCRunning)
                {
                    ServerStateDesc = "已连接到-" + KepServer.ServerName + "   ";
                }
                else
                {
                    //这里你可以根据返回的状态来自定义显示信息，请查看自动化接口API文档
                    ServerStateDesc = "状态：" + KepServer.ServerState.ToString() + "   ";
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("连接远程服务器出现错误：" + ex.Message, "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                throw;
            }
        }
        #endregion
        #region 事件
        /// <summary>
        /// 写入TAG值时执行的事件（异步写入）
        /// </summary>
        /// <param name="TransactionID"></param>
        /// <param name="NumItems"></param>
        /// <param name="ClientHandles"></param>
        /// <param name="Errors"></param>
        void KepGroup_AsyncWriteComplete(int TransactionID, int NumItems, ref Array ClientHandles, ref Array Errors)
        {
            //lblState.Text = "";
            //for (int i = 1; i <= NumItems; i++)
            //{
            //    lblState.Text += "Tran:" + TransactionID.ToString() + "   CH:" + ClientHandles.GetValue(i).ToString() + "   Error:" + Errors.GetValue(i).ToString();
            //}
        }
        /// <summary>
        /// 服务器断开通知
        /// </summary>
        /// <param name="msg"></param>
        void KepServer_DisConnected(string msg)
        {
            if (DisConnectedHandle != null)
            {
                DisConnectedHandle(msg);
                this.Dispose();
            }
        }
        #endregion
        #region IDisposable
        /// <summary>
        /// 获取是否已释放
        /// </summary>
        public bool IsDisposed { get; private set; }
        /// <summary>
        /// 关闭和释放所有相关资源
        /// </summary>
        public void Dispose()
        {
            // 如果资源未释放 这个判断主要用了防止对象被多次释放
            if (this.IsDisposed == false)
            {
                this.Dispose(true);
                GC.SuppressFinalize(this);
            }
            this.IsDisposed = true;
        }
        /// <summary>
        /// 析构函数
        /// </summary>
        ~DaOpc()
        {
            this.Dispose(false);
        }
        /// <summary>
        /// 释放资源
        ///参数为true表示释放所有资源，只能由使用者调用
        //参数为false表示释放非托管资源，只能由垃圾回收器自动调用
        //如果子类有自己的非托管资源，可以重载这个函数，添加自己的非托管资源的释放
        //但是要记住，重载此函数必须保证调用基类的版本，以保证基类的资源正常释放
        /// </summary>
        /// <param name="disposing">是否也释放托管资源</param>
        protected virtual void Dispose(bool disposing)
        {
            //释放非托管资源
            this.KepServer = null;   
            opc_connected = false;
            foreach (var item in groupDictionary)
            {
                item.Value.Dispose();
            }
            groupDictionary.Clear();


            // 释放托管资源(一般用不到，不需要手动释放，依赖垃圾回收器自动回收)
            if (disposing)
            {
                //GC.Collect();
            }
        }
        #endregion
    }
}
