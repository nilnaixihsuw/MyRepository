using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.Specialized;
using OpcClient;
using System.Windows.Forms;

namespace OpcClient
{
    public interface IOpcClient : IDisposable
    {
        /// <summary>
        /// 初始化设置
        /// 1、初始化OPCServer
        /// 2、遍历OPCServer列表
        /// </summary>
        /// <param name="ip"></param>
        void Init(string ip);
        /// <summary>
        /// 连接OPC
        /// </summary>
        /// <returns></returns>
        bool Connect(string ServerName);
        /// <summary>
        /// 断开OPC
        /// </summary>
        /// <returns></returns>
        bool Disconnect();
        /// <summary>
        /// OPC连接状态
        /// </summary>
        bool OpcStatus { get; }
        /// <summary>
        /// Opc服务端开始时间
        /// </summary>
        /// <returns></returns>
        string ServerStartTime { get; }
        string ServerVersion { get; }
        string ServerStateDesc { get;}
        /// <summary>
        /// 获取OPCServer列表
        /// </summary>
        List<string> ServerList { get;}
        /// <summary>
        /// 遍历OPCBrowser,并以Tree节点方式构造所有nodes
        /// </summary>
        /// <param name="nodes"></param>
        void ShowBranchesByTree(TreeNodeCollection nodes);
        /// <summary>
        /// 列出OPC服务器下的组名称
        /// </summary>
        /// <returns></returns>
        List<string> ShowBranches();
        /// <summary>
        /// 取出指定组下的所有Items信息（不包括value）
        /// </summary>
        /// <returns></returns>
        List<string> ShowLeafs(string groupName);    
        /// <summary>
        /// 创建变量组 GroupTrigger GroupData
        /// </summary>
        /// <param name="groupName"></param>
        /// <returns></returns>
        IGroup CreateGroup(string groupName);
        /// <summary>
        /// 删除过程数据组
        /// </summary>
        /// <returns></returns>
        //bool RemoveGroupDATA();
        ///// <summary>
        ///// 删除信号组
        ///// </summary>
        ///// <returns></returns>
        //bool RemoveGroupTRIGGER();
        /// <summary>
        /// 删除组
        /// </summary>
        /// <param name="groupName"></param>
        /// <returns></returns>
        bool RemoveGroupByGroupName(string groupName);
        /// <summary>
        /// 删除所有组
        /// </summary>
        /// <returns></returns>
        void RemoveGroupsAll();
        /// <summary>
        /// 获取Tag值
        /// </summary>
        /// <param name="biList"></param>
        /// <param name="grouName"></param>
        /// <returns></returns>
        List<Tag> GetTagValuesFromGroup(ref List<Tag> biList, string grouName);
        /// <summary>
        /// 服务端断开通知
        /// </summary>
        Action<IOpcClient> DisConnectedHandle { get; set; }
        DaGroup this[string groupName] { get; }
    }
}
