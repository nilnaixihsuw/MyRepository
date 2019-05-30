using OPCAutomation;
using System;
using System.Collections.Generic;

namespace opc
{
    public class OPC
    {
        private static OPC opcSingle;
        private static object locker = new object();
        private OPC() { }
        public static OPC GetSingle
        {
            get
            {
                if (opcSingle == null)
                {
                    lock (locker)
                    {
                        if (opcSingle == null)
                        {
                            opcSingle = new OPC();
                        }
                    }
                }
                return opcSingle;
            }
        }
        private OPCServer opcServer;
        private OPCGroups opcGroups;
        private bool isConnection = false;
        /// <summary>
        /// 连接opc
        /// </summary>
        public void ConnectOPC(string ProgID,object Node)
        {
            if (!isConnection)
            {
                try
                {
                    opcServer = new OPCServer();
                    //opcServer.Connect("KEPware.KEPServerEx.V6", "127.0.0.1");
                    opcServer.Connect(ProgID,Node);
                    opcGroups = opcServer.OPCGroups;
                    opcServer.OPCGroups.DefaultGroupIsActive = true;//激活组。
                    opcServer.OPCGroups.DefaultGroupDeadband = 0;// 死区值，设为0时，服务器端该组内任何数据变化都通知组。
                    opcServer.OPCGroups.DefaultGroupUpdateRate = 200;//默认组群的刷新频率为200ms
                    isConnection = true;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        /// <summary>
        /// 断开opc
        /// </summary>
        public void DisConnectOPC()
        {
            if (isConnection)
            {
                try
                {
                    opcServer.Disconnect();
                    isConnection = false;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        /// <summary>
        /// 添加组
        /// </summary>
        /// <param name="groupName">组名</param>
        /// <param name="updataRate">刷新速率</param>
        /// <param name="isSubscribed">是否订阅</param>
        /// <param name="AsyncReadEvent">异步读取成功触发事件</param>
        /// <param name="AsyncWriteEvent">异步写入成功触发事件</param>
        /// <param name="DataChangeEvent">数据改变触发事件</param>
        /// <returns></returns>
        public OPCGroup AddGroup(string groupName,
            int updataRate,
            bool isSubscribed,
            DIOPCGroupEvent_AsyncReadCompleteEventHandler AsyncReadEvent,
            DIOPCGroupEvent_AsyncWriteCompleteEventHandler AsyncWriteEvent,
            DIOPCGroupEvent_DataChangeEventHandler DataChangeEvent)
        {
            OPCGroup group = opcGroups.Add(groupName);
            group.UpdateRate = updataRate;//刷新频率为1秒。
            group.IsSubscribed = isSubscribed;//使用订阅功能，即可以异步，默认false
            group.AsyncReadComplete += AsyncReadEvent;
            group.AsyncWriteComplete += AsyncWriteEvent;
            group.DataChange += DataChangeEvent;
            return group;
        }

        /// <summary>
        /// 异步读触发事件原型
        /// </summary>
        /// <param name="TransactionID"></param>
        /// <param name="NumItems"></param>
        /// <param name="ClientHandles"></param>
        /// <param name="ItemValues"></param>
        /// <param name="Qualities"></param>
        /// <param name="TimeStamps"></param>
        /// <param name="Errors"></param>
        private void GroupAsyncReadComplete(int TransactionID,
            int NumItems,
            ref Array ClientHandles,
            ref Array ItemValues,
            ref Array Qualities,
            ref Array TimeStamps,
            ref Array Errors)
        {

        }
        /// <summary>
        /// 异步写触发事件原型
        /// </summary>
        /// <param name="TransactionID"></param>
        /// <param name="NumItems"></param>
        /// <param name="ClientHandles"></param>
        /// <param name="Errors"></param>
        private void GroupAsyncWriteComplete(int TransactionID, int NumItems, ref Array ClientHandles, ref Array Errors)
        {

        }
        /// <summary>
        /// 数据改变触发事件原型
        /// </summary>
        /// <param name="TransactionID"></param>
        /// <param name="NumItems"></param>
        /// <param name="ClientHandles"></param>
        /// <param name="ItemValues"></param>
        /// <param name="Qualities"></param>
        /// <param name="TimeStamps"></param>
        private void GroupDataChange(int TransactionID,
            int NumItems,
            ref Array ClientHandles,
            ref Array ItemValues,
            ref Array Qualities,
            ref Array TimeStamps)
        {

        }


    }
    public static class OPCGroupExtend
    {
        /// <summary>
        /// 添加项
        /// </summary>
        /// <param name="group"></param>
        /// <param name="itemID"></param>
        /// <param name="clientHandle"></param>
        public static OPCItem AddItem(this OPCGroup group, string itemID, int clientHandle)
        {
            OPCItem item = group.OPCItems.AddItem(itemID, clientHandle);
            return item;
        }
        /// <summary>
        /// 获取所有项的服务句柄
        /// </summary>
        /// <param name="group"></param>
        /// <param name="oPCItems">组下所有opcItem泛型集合</param>
        /// <returns></returns>
        public static Array GetServerhandles(this OPCGroup group, List<OPCItem> oPCItems)
        {
            List<int> serverhandles = new List<int>() { 0 };
            oPCItems.ForEach(item =>
            {
                serverhandles.Add(item.ServerHandle);
            });
            return serverhandles.ToArray();
        }
    }
}
