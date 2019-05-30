using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Collections;
using OPCAutomation;
using OpLink.test;

namespace OpLink
{
    public partial class FormServerRun : Form
    {
        public FormServerRun()
        {
            InitializeComponent();
        }

        #region 私有变量
        /// <summary>
        /// OPCServer Object
        /// </summary>
        OPCServer KepServer;
        /// <summary>
        /// OPCGroups Object
        /// </summary>
        OPCGroups KepGroups;
        /// <summary>
        /// OPCGroup Object
        /// </summary>
        OPCGroup KepGroup;
        /// <summary>
        /// OPCItems Object
        /// </summary>
        OPCItems KepItems;
        /// <summary>
        /// OPCItem Object
        /// </summary>
        OPCItem KepItem;
        /// <summary>
        /// 主机IP
        /// </summary>
        string strHostIP = "";
        /// <summary>
        /// 主机名称
        /// </summary>
        string strHostName = "";
        /// <summary>
        /// 连接状态
        /// </summary>
        bool opc_connected = false;
        /// <summary>
        /// 客户端句柄
        /// </summary>
        int itmHandleClient = 1;
        /// <summary>
        /// 服务端句柄
        /// </summary>
        int itmHandleServer = 0;

        private List<Tag<DaExtra>> daOpcItems;
        //ITEMS集合
        ArrayList itemsList = new ArrayList();

        enum  CanonicalDataTypes:short
        {
		CanonDtByte = 17,
		CanonDtChar = 16,
		CanonDtWord = 18,
		CanonDtShort = 2,
		CanonDtDWord = 19,
		CanonDtLong = 3,
		CanonDtFloat = 4,
		CanonDtDouble = 5,
		CanonDtBool = 11,
		CanonDtString = 8,
        }
        #endregion

        #region 方法
        /// <summary>
        /// 枚举本地OPC服务器
        /// </summary>
        private void GetLocalServer()
        {
            //OpcClient opc = new DaOpc();
            //List<DaOpcItem> s = new List<DaOpcItem>();
            //opc.GetItemValues(s);


            //获取本地计算机IP,计算机名称
            IPHostEntry IPHost = Dns.Resolve(Environment.MachineName);
            if (IPHost.AddressList.Length > 0)
            {
                strHostIP = IPHost.AddressList[0].ToString();
            }
            else
            {
                return;
            }
            //通过IP来获取计算机名称，可用在局域网内
            IPHostEntry ipHostEntry = Dns.GetHostByAddress(strHostIP);
            strHostName = ipHostEntry.HostName.ToString();

            //获取本地计算机上的OPCServerName
            try
            {
                KepServer = new OPCServer();
                object serverList = KepServer.GetOPCServers(strHostName);

                foreach (string turn in (Array)serverList)
                {
                    cmbServerName.Items.Add(turn);
                }

                cmbServerName.SelectedIndex = 0;
                btnConnServer.Enabled = true;
            }
            catch (Exception err)
            {
                MessageBox.Show("枚举本地OPC服务器出错：" + err.Message, "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }
        /// <summary>
        /// 创建组
        /// </summary>
        private bool CreateGroup()
        {
            try
            {
                KepGroups = KepServer.OPCGroups;
                KepGroup = KepGroups.Add("OPCDOTNETGROUP");
                SetGroupProperty();
                KepGroup.DataChange += new DIOPCGroupEvent_DataChangeEventHandler(KepGroup_DataChange);
                KepGroup.AsyncWriteComplete += new DIOPCGroupEvent_AsyncWriteCompleteEventHandler(KepGroup_AsyncWriteComplete);
                KepItems = KepGroup.OPCItems;
            }
            catch (Exception err)
            {
                MessageBox.Show("创建组出现错误：" + err.Message, "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
        /// <summary>
        /// 设置组属性
        /// </summary>
        private void SetGroupProperty()
        {
            KepServer.OPCGroups.DefaultGroupIsActive = Convert.ToBoolean(txtGroupIsActive.Text);
            KepServer.OPCGroups.DefaultGroupDeadband = Convert.ToInt32(txtGroupDeadband.Text);
            KepGroup.UpdateRate = Convert.ToInt32(txtUpdateRate.Text);
            KepGroup.IsActive = Convert.ToBoolean(txtIsActive.Text);
            KepGroup.IsSubscribed = Convert.ToBoolean(txtIsSubscribed.Text);
        }

        /// <summary>
        /// 列出OPC服务器中所有组
        /// </summary>
        /// <param name="oPCBrowser"></param>
        private void RecurBrowseGroups(OPCBrowser oPCBrowser)
        {
           //string branch;
           //oPCBrowser.ShowBranches();
           //oPCBrowser.ShowLeafs(true);
           //foreach (object turn in oPCBrowser) 
           // {                  Console.WriteLine("服务端浏览——————：" + turn.ToString());             } 
           // Console.WriteLine("请需要浏览的分支："); 
           // branch = Console.ReadLine();
           // oPCBrowser.MoveDown("Channel_0_User_Defined");
           // oPCBrowser.ShowBranches();
           // oPCBrowser.ShowLeafs(true);
           // foreach (object turn in oPCBrowser)        
           // {         
           //     Console.WriteLine("服务端：" + turn.ToString());     
           // }

            //展开分支
            oPCBrowser.ShowBranches();
            foreach (object turn in oPCBrowser)
            {
                if (!turn.ToString().Substring(0, 1).Equals("_"))
                {
                    listBoxGroups.Items.Add(turn.ToString());
                }              
            }
        }

        /// <summary>
        /// 列出OPC组下所有点
        /// </summary>
        /// <param name="oPCBrowser"></param>
        private void RecurBrowseItems(OPCBrowser oPCBrowser, string groupName)
        {
            //int len = groupName.Length;
            //ArrayList al = new ArrayList();
            ////展开分支
            //oPCBrowser.ShowBranches();
            ////展开叶子
            //oPCBrowser.ShowLeafs(true);
            //listBoxItems.Items.Clear();
            //listView1.Items.Clear();
            //foreach (object turn in oPCBrowser)
            //{
            //    if (turn.ToString().Contains(groupName))
            //    {
            //        al.Add(turn);
            //    }  
            //}
            //foreach (var item in al)
            //{
            //    if (!item.ToString().Contains("._"))
            //    {
            //        listBoxItems.Items.Add(item.ToString());
            //        listView1.Items.Add(item.ToString());
            //    }
            //}

            listView1.Items.Clear();            
            oPCBrowser.MoveDown(groupName);
            oPCBrowser.ShowBranches();
            oPCBrowser.ShowLeafs(true);
            listBoxItems.Items.Clear();
            listView1.Items.Clear();
            foreach (object turn in oPCBrowser)
            {
                listView1.Items.Add(turn.ToString());
                listBoxItems.Items.Add(turn.ToString());
            }
            oPCBrowser.MoveUp();
        }

        /// <summary>
        /// 列出OPC服务器中所有节点
        /// </summary>
        /// <param name="oPCBrowser"></param>
        private void RecurBrowse(OPCBrowser oPCBrowser)
        {
            //展开分支
            oPCBrowser.ShowBranches();
            //展开叶子
            oPCBrowser.ShowLeafs(true);
            
            foreach (object turn in oPCBrowser)
            {
                listBoxItems.Items.Add(turn.ToString());
            }
        }
        /// <summary>
        /// 获取服务器信息，并显示在窗体状态栏上
        /// </summary>
        private void GetServerInfo()
        {
            tsslServerStartTime.Text = "开始时间:" + KepServer.StartTime.ToString() + "    ";
            tsslversion.Text = "版本:" + KepServer.MajorVersion.ToString() + "." + KepServer.MinorVersion.ToString() + "." + KepServer.BuildNumber.ToString();
        }
        /// <summary>
        /// 连接OPC服务器
        /// </summary>
        /// <param name="remoteServerIP">OPCServerIP</param>
        /// <param name="remoteServerName">OPCServer名称</param>
        private bool ConnectRemoteServer(string remoteServerIP, string remoteServerName)
        {
            try
            {
                KepServer.Connect(remoteServerName, remoteServerIP);

                if (KepServer.ServerState == (int)OPCServerState.OPCRunning)
                {
                    tsslServerState.Text = "已连接到-" + KepServer.ServerName + "   ";
                }
                else
                {
                    //这里你可以根据返回的状态来自定义显示信息，请查看自动化接口API文档
                    tsslServerState.Text = "状态：" + KepServer.ServerState.ToString() + "   ";
                }
            }
            catch (Exception err)
            {
                MessageBox.Show("连接远程服务器出现错误：" + err.Message, "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
        #endregion

        #region 事件
        /// <summary>
        /// 写入TAG值时执行的事件
        /// </summary>
        /// <param name="TransactionID"></param>
        /// <param name="NumItems"></param>
        /// <param name="ClientHandles"></param>
        /// <param name="Errors"></param>
        void KepGroup_AsyncWriteComplete(int TransactionID, int NumItems, ref Array ClientHandles, ref Array Errors)
        {
            lblState.Text = "";
            for (int i = 1; i <= NumItems; i++)
            {
                lblState.Text += "Tran:" + TransactionID.ToString() + "   CH:" + ClientHandles.GetValue(i).ToString() + "   Error:" + Errors.GetValue(i).ToString();
            }
        }
        /// <summary>
        /// 每当项数据有变化时执行的事件
        /// </summary>
        /// <param name="TransactionID">处理ID</param>
        /// <param name="NumItems">项个数</param>
        /// <param name="ClientHandles">项客户端句柄</param>
        /// <param name="ItemValues">TAG值</param>
        /// <param name="Qualities">品质</param>
        /// <param name="TimeStamps">时间戳</param>
        void KepGroup_DataChange(int TransactionID, int NumItems, ref Array ClientHandles, ref Array ItemValues, ref Array Qualities, ref Array TimeStamps)
        {
            //为了测试，所以加了控制台的输出，来查看事物ID号
            Console.WriteLine("********"+TransactionID.ToString()+"*********");         

            for (int i = 1; i <= NumItems; i++)
            {
                int index = Convert.ToInt32(ClientHandles.GetValue(i));
                daOpcItems[index - 1].Value = ItemValues.GetValue(i).ToString();
                //标签时间戳
                daOpcItems[index - 1].TimeStamps = TimeStamps.GetValue(i).ToString();
                //标签质量
                switch (Convert.ToInt32(Qualities.GetValue(i)))
                {
                    case (int)(OPCQuality.OPCQualityGood):
                        daOpcItems[index - 1].Qualities = "Good";
                        break;
                    case (int)(OPCQuality.OPCQualityUncertain):
                        daOpcItems[index - 1].Qualities = "Uncertain";
                        break;
                    case (int)(OPCQuality.OPCQualityBad):
                        daOpcItems[index - 1].Qualities = "Bad";
                        break;
                    default:
                        break;
                }
            }
        }
        /// <summary>
        /// 选择列表项时处理的事情
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {
                if (itmHandleClient != 0)
                {
                    this.txtTagValue.Text = "";
                    this.txtQualities.Text = "";
                    this.txtTimeStamps.Text = "";
                    this.txtDataType.Text = "";
                    int sd = KepItems.Count;
                    Array Errors;
                    OPCItem bItem = KepItems.GetOPCItem(itmHandleServer);                   
                    //注：OPC中以1为数组的基数
                    int[] temp = new int[2] { 0, bItem.ServerHandle };
                    Array serverHandle = (Array)temp;
                    //移除上一次选择的项
                    KepItems.Remove(KepItems.Count, ref serverHandle, out Errors);
                }
                itmHandleClient = 0;
                //for (int i = 0; i < 5; i++)
                //{
                //    KepItem = KepItems.AddItem(listBox1.Items[i].ToString(), itmHandleClient);
                //    itmHandleClient += 1;
                //}
                //KepItem = KepItems.AddItem(listBoxItems.SelectedItem.ToString(), itmHandleClient);
                KepItem = KepItems.AddItem("Channel_0_User_Defined.User.User1", itmHandleClient);
                KepItems.AddItem("Channel_0_User_Defined.User.User2", 1235);
                //数据类型
                txtDataType.Text = Enum.GetName(typeof(CanonicalDataTypes), KepItem.CanonicalDataType);
                itmHandleServer = KepItem.ServerHandle;
            }
            catch (Exception err)
            {
                //没有任何权限的项，都是OPC服务器保留的系统项，此处可不做处理。
                itmHandleClient = 0;
                txtTagValue.Text = "Error ox";
                txtQualities.Text = "Error ox";
                txtTimeStamps.Text = "Error ox";
                MessageBox.Show("此项为系统保留项:" + err.Message, "提示信息");
            }
        }
        /// <summary>
        /// 载入窗体时处理的事情
        /// </summary>
        private void FormServerRun_Load(object sender, EventArgs e)
        {
            GetLocalServer();
            daOpcItems = new List<Tag<DaExtra>>();
        }
        /// <summary>
        /// 关闭窗体时处理的事情
        /// </summary>
        private void FormServerRun_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!opc_connected)
            {
                return;
            }

            if (KepGroup != null)
            {
                KepGroup.DataChange -= new DIOPCGroupEvent_DataChangeEventHandler(KepGroup_DataChange);
            }

            if (KepServer != null)
            {
                KepServer.Disconnect();
                KepServer = null;
            }

            opc_connected = false;
        }
        /// <summary>
        /// 【按钮】设置
        /// </summary>
        private void btnSetGroupPro_Click(object sender, EventArgs e)
        {
            SetGroupProperty();
        }
        /// <summary>
        /// 【按钮】连接ＯＰＣ服务器
        /// </summary>
        private void btnConnLocalServer_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ConnectRemoteServer(txtRemoteServerIP.Text, cmbServerName.Text))
                {
                    return;
                }

                btnSetGroupPro.Enabled = true;

                opc_connected = true;

                btnConnServer.Enabled = false;
                btnDisconnServer.Enabled = true;

                listBoxGroups.Items.Clear();
                listBoxItems.Items.Clear();

                GetServerInfo();

                RecurBrowseGroups(KepServer.CreateBrowser());

                if (!CreateGroup())
                {
                    return;
                }
            }
            catch (Exception err)
            {
                MessageBox.Show("初始化出错：" + err.Message, "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// 【按钮】断开ＯＰＣ服务器
        /// </summary>
        private void btnDisconnServer_Click(object sender, EventArgs e)
        {
            if (!opc_connected)
            {
                return;
            }

            if (KepGroup != null)
            {
                KepGroup.DataChange -= new DIOPCGroupEvent_DataChangeEventHandler(KepGroup_DataChange);
            }

            if (KepServer != null)
            {                
                KepServer.Disconnect();
            }

            opc_connected = false;
            btnConnServer.Enabled = true;
            btnDisconnServer.Enabled = false;
        }

        /// <summary>
        /// 【按钮】写入
        /// </summary>
        private void btnWrite_Click(object sender, EventArgs e)
        {
            OPCItem bItem = KepItems.GetOPCItem(itmHandleServer);
            int[] temp = new int[2] { 0, bItem.ServerHandle };
            Array serverHandles = (Array)temp;
            object[] valueTemp = new object[2] { "", txtTagValue.Text };
            Array values = (Array)valueTemp;
            Array Errors;
            int cancelID;
            KepGroup.AsyncWrite(1, ref serverHandles, ref values, out Errors, 2009, out cancelID);
            //KepItem.Write(txtWriteTagValue.Text);//这句也可以写入，但并不触发写入事件
            GC.Collect();
        }
        #endregion

        private void button2_Click(object sender, EventArgs e)
        {
            //****将勾选的OPC点压入集合中**********
            
            foreach (ListViewItem item in listView1.CheckedItems)
            {
                OPCItem ki;

                ki=KepItems.AddItem(item.Text, itmHandleClient);
                Tag<DaExtra> bi = new Tag<DaExtra>();
                bi.OpcTagName=item.Text;
                bi.TagName=item.Text;
                bi.DataType = Enum.GetName(typeof(CanonicalDataTypes), ki.CanonicalDataType);
                bi.TagExtra.ItmHandleClient = itmHandleClient;
                bi.TimeStamps="";
                bi.Value="";
                bi.Qualities="";                
                itmHandleClient += 1;
                itemsList.Add(bi);
                daOpcItems.Add(bi);         
            }
            //******将数据集合载入gridview**************
            //1.清空旧数据  
            dataGridView1.Rows.Clear();
            //2.赋值新数据  
            for (int i = 0; i < itemsList.Count; i++)
            {
                int index = dataGridView1.Rows.Add();
                dataGridView1.Rows[i].Cells["OpcTagName"].Value = ((Tag<DaExtra>)itemsList[i]).OpcTagName;
                dataGridView1.Rows[i].Cells["TagName"].Value = ((Tag<DaExtra>)itemsList[i]).TagName;
                dataGridView1.Rows[i].Cells["DataType"].Value = ((Tag<DaExtra>)itemsList[i]).DataType;
                dataGridView1.Rows[i].Cells["Value"].Value = ((Tag<DaExtra>)itemsList[i]).Value;
                dataGridView1.Rows[i].Cells["Qualities"].Value = ((Tag<DaExtra>)itemsList[i]).Qualities;
                dataGridView1.Rows[i].Cells["TimeStamps"].Value = ((Tag<DaExtra>)itemsList[i]).TimeStamps;
            } 
            //this.listView1.BeginUpdate();   //数据更新，UI暂时挂起，直到EndUpdate绘制控件，可以有效避免闪烁并大大提高加载速度 

            //for (int i = 0; i < itemsList.Count; i++)   //添加10行数据 
            //{
            //    ListViewItem lvi = new ListViewItem();

            //    lvi.ImageIndex = i;     //通过与imageList绑定，显示imageList中第i项图标 

            //    lvi.Text ="subitem" + i; 

            //    lvi.SubItems.Add(((DaOpcItem)itemsList[i]).OpcTagName);
            //    lvi.SubItems.Add(((DaOpcItem)itemsList[i]).TagName);
            //    lvi.SubItems.Add(((DaOpcItem)itemsList[i]).DataType);
            //    lvi.SubItems.Add(((DaOpcItem)itemsList[i]).Value);
            //    lvi.SubItems.Add(((DaOpcItem)itemsList[i]).Qualities);
            //    lvi.SubItems.Add(((DaOpcItem)itemsList[i]).TimeStamps);
            //    //lvi.SubItems.Add("第2列,第" + i + "行");

            //    //lvi.SubItems.Add("第3列,第" + i + "行");

            //    this.listView1.Items.Add(lvi);
            //}

            //this.listView1.EndUpdate();

            //KepItems.AddItem(listBoxItems.SelectedItem.ToString(), itmHandleClient);
        }

        private void listBoxGroups_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxGroups.Items.Count != 0)
            {
                RecurBrowseItems(KepServer.CreateBrowser(), listBoxGroups.SelectedItem.ToString());
            }           
        }

        private void gridRresh()
        {
            //1.清空旧数据  
            dataGridView1.Rows.Clear();
            //2.赋值新数据  
            for (int i = 0; i < itemsList.Count; i++)
            {
                int index = dataGridView1.Rows.Add();
                dataGridView1.Rows[i].Cells["OpcTagName"].Value = ((Tag<DaExtra>)daOpcItems[i]).OpcTagName;
                dataGridView1.Rows[i].Cells["TagName"].Value = ((Tag<DaExtra>)daOpcItems[i]).TagName;
                dataGridView1.Rows[i].Cells["DataType"].Value = ((Tag<DaExtra>)daOpcItems[i]).DataType;
                dataGridView1.Rows[i].Cells["Value"].Value = ((Tag<DaExtra>)daOpcItems[i]).Value;
                dataGridView1.Rows[i].Cells["Qualities"].Value = ((Tag<DaExtra>)daOpcItems[i]).Qualities;
                dataGridView1.Rows[i].Cells["TimeStamps"].Value = ((Tag<DaExtra>)daOpcItems[i]).TimeStamps;
            } 
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            gridRresh();
        }
    }
}
