using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OPCAutomation;
using System.Diagnostics;

namespace OPC_Client
{
    public partial class Form1 : Form
    {
        OPCServer objServer;
        OPCGroups objGroups;
        OPCGroup objGroup;
        OPCItems objItems;
        Array strItemIDs;
        Array lClientHandles;
        Array lserverhandles;
        Array lErrors;
      //  int ltransID_Rd = 1;
       // int lCancelID_Rd;
       object RequestedDataTypes = null;
        object AccessPaths = null;
     //   Array lerrors_Rd;
        Array lErrors_Wt;
        int lTransID_Wt = 2;
        int lCancelID_Wt;
        public Form1()
        {
            InitializeComponent();
        }


        //连接opc server
        private void button1_Click(object sender, EventArgs e)
        {
            //(1)创建opc server对象
            objServer = new OPCServer();
            //连接opc server
            objServer.Connect("KEPware.KEPServerEx.V6", null);
            //(2)建立一个opc组集合
            objGroups = objServer.OPCGroups;
            //(3)建立一个opc组
            objGroup = objGroups.Add(null); //Group组名字可有可无
            //(4)添加opc标签
            objGroup.IsActive = true; //设置该组为活动状态，连接PLC时，设置为非活动状态也一样
            objGroup.IsSubscribed = true; //设置异步通知
            objGroup.UpdateRate = 250;
            objServer.OPCGroups.DefaultGroupDeadband = 0;
            objGroup.DataChange += new DIOPCGroupEvent_DataChangeEventHandler(KepGroup_DataChange);
           // objGroup.AsyncReadComplete += new DIOPCGroupEvent_AsyncReadCompleteEventHandler(AsyncReadComplete);
            objGroup.AsyncWriteComplete += new DIOPCGroupEvent_AsyncWriteCompleteEventHandler(AsyncWriteComplete);
            objItems = objGroup.OPCItems; //建立opc标签集合
            string[] tmpIDs = new string[7];
            int[] tmpCHandles = new int[7];
            for (int i = 1; i < 7; i++)
            {
                tmpCHandles[i] = i;
            }
            tmpIDs[1] = "西门子S7-300.PLC.系统启动开关";
            tmpIDs[2] = "西门子S7-300.PLC.机械手启动开关";
            tmpIDs[3] = "西门子S7-300.PLC.M1电动机";
            tmpIDs[4] = "西门子S7-300.PLC.机械手";
            tmpIDs[5] = "西门子S7-300.PLC.温度";
            tmpIDs[6] = "西门子S7-300.PLC.湿度";
            strItemIDs = (Array)tmpIDs;//必须转成Array型，否则不能调用AddItems方法
            lClientHandles = (Array)tmpCHandles;
            // 添加opc标签
            objItems.AddItems(6, ref strItemIDs, ref lClientHandles, out lserverhandles, out lErrors, RequestedDataTypes, AccessPaths);
        }


        //结束并断开opc server
        private void button4_Click(object sender, EventArgs e)
        {
            objServer.Disconnect();
            //关闭kepserver进程，这个跟OPC操作无关
            /*
            foreach ( Process oneProcess in Process.GetProcesses())
            {
            if (oneProcess.ProcessName == "ServerMain")
            oneProcess.Kill();
            }
            */
        }

        //每当项数据有变化时执行的事件,采用订阅方式


        void KepGroup_DataChange(int TransactionID, int NumItems, ref Array ClientHandles, ref Array ItemValues, ref Array Qualities, ref Array TimeStamps)
        {
            //为了测试，所以加了控制台的输出，来查看事物ID号
            //Console.WriteLine("********"+TransactionID.ToString()+"*********");

            for (int i = 0; i < NumItems; i++)
            {


                if (Convert.ToInt32(ClientHandles.GetValue(i + 1)) == 1)
                {
                    if (ItemValues.GetValue(i + 1) != null)
                    {
                        this.Data1.Text = ItemValues.GetValue(i + 1).ToString();
                    }
                }
                if (Convert.ToInt32(ClientHandles.GetValue(i + 1)) == 2)
                {
                    if (ItemValues.GetValue(i + 1) != null)
                    {
                        this.Data2.Text = ItemValues.GetValue(i + 1).ToString();
                    }
                }
                if (Convert.ToInt32(ClientHandles.GetValue(i + 1)) == 3)
                {
                    if (ItemValues.GetValue(i + 1) != null)
                    {
                        this.Data3.Text = ItemValues.GetValue(i + 1).ToString();
                    }
                }
                if (Convert.ToInt32(ClientHandles.GetValue(i + 1)) == 4)
                {
                    if (ItemValues.GetValue(i + 1) != null)
                    {
                        this.Data4.Text = ItemValues.GetValue(i + 1).ToString();
                    }
                }
                if (Convert.ToInt32(ClientHandles.GetValue(i + 1)) == 5)
                {
                    if (ItemValues.GetValue(i + 1) != null)
                    {
                        this.Data5.Text = ItemValues.GetValue(i + 1).ToString();
                    }
                }
                if (Convert.ToInt32(ClientHandles.GetValue(i + 1)) == 6)
                {
                    if (ItemValues.GetValue(i + 1) != null)
                    {
                        this.Data6.Text = ItemValues.GetValue(i + 1).ToString();
                    }
                }
            }


        }

       

           
         
        //发送异步写数据指令
        private void button3_Click(object sender, EventArgs e)
        {
            Array AsyncValue_Wt;
            Array SerHandles;
            object[] tmpWtData = new object[3];//写入的数据必须是object型的，否则会报错
            int[] tmpSerHdles = new int[3];
            //将输入数据赋给数组，然后再转成Array型送给AsyncValue_Wt
            tmpWtData[1] = (object)textBox1.Text;
            tmpWtData[2] = (object)textBox2.Text;
            AsyncValue_Wt = (Array)tmpWtData;
            //将输入数据送给的Item对应服务器句柄赋给数组，然后再转成Array型送给SerHandles
            tmpSerHdles[1] = Convert.ToInt32(lserverhandles.GetValue(1));
            tmpSerHdles[2] = Convert.ToInt32(lserverhandles.GetValue(2));
            SerHandles = (Array)tmpSerHdles;
            objGroup.AsyncWrite(2, ref SerHandles, ref AsyncValue_Wt, out lErrors_Wt, lTransID_Wt, out lCancelID_Wt);

        }
        //异步写入成功
        private void AsyncWriteComplete(int TransactionID, int NumItems, ref Array ClientHandles, ref Array Errors)
        {
            MessageBox.Show("数据写入成功！");
        }
    }
}
