using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Xml;
using System.Collections;
using System.IO;
using OpMonitor;
using OpcClient;


namespace OpMonitor
{
    public partial class Setting : Form
    {
        private XmlDocument doc = new XmlDocument();
        private IOpcClient client;
        private string groupName = "";
        private string blockName = "";
        private string IP;
        private string ServerName;

        public Setting()
        {
            InitializeComponent();
        }

        private void Setting_Load(object sender, EventArgs e)
        {
            //读取配置文件
            IniRead();
            //加载xml
            XElement doc = XElement.Load(TagConfig.Path);//将加载完成的XML文件显示在TreeView控件中
            RecursionTreeControl(doc, treeTags.Nodes);
            treeTags.ExpandAll();//展开TreeView控件中的所有项 

            #region OpcClient初始化
            try
            {
                client = new DaOpc(IP);
                client.DisConnectedHandle = this.OpcServerDisConnected;
                client.Connect(ServerName);
                client.CreateGroup("GroupTrigger").ValueChangedHandle = TagValueChanged;
                client.CreateGroup("GroupData");
            }
            catch (Exception ex)
            {
                //日志
                throw;
            }
            #endregion
            tsslServerState.Text = client.ServerStateDesc;
            tsslServerStartTime.Text = client.ServerStartTime;
            tsslversion.Text = "版本号：" + client.ServerVersion;
            cmbInterval.SelectedIndex = 2;
            dataGridTags.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
        }
        //关闭
        private void Setting_FormClosing(object sender, FormClosingEventArgs e)
        {
            client.Disconnect();
        }

        #region 函数
        /// <summary>
        /// 读取配置文件
        /// </summary>
        private void IniRead()
        {
            IP = IniReader.Instance.G_IP;
            ServerName = IniReader.Instance.G_SERVERNAME;
            if (IP.Equals("") || ServerName.Equals(""))
            {
                IniSetting iniS = new IniSetting();
                if (iniS.ShowDialog() == DialogResult.OK)
                {
                    IP = IniReader.Instance.G_IP;
                    ServerName = IniReader.Instance.G_SERVERNAME;
                }
                else
                {
                    this.Close();
                }
            }
        }
        /// </summary>
        ///  RecursionTreeControl:表示将XML文件的内容显示在TreeView控件中
        /// </summary>
        /// <param name="xmlNode">将要加载的XML文件中的节点元素</param>
        /// <param name="nodes">将要加载的XML文件中的节点集合</param>
        /// <param name="lev">设定遍历的初始层级</param>
        /// <param name="levelMax">设定遍历的最大层级</param>
        private void RecursionTreeControl(XElement xmlList, TreeNodeCollection nodes, int lev = 0, int levelMax = 2)
        {
            int level = lev + 1;
            foreach (XElement node in xmlList.Elements())
            {
                if (level <= levelMax)
                {
                    TreeNode new_child = new TreeNode();//定义一个TreeNode节点对象

                    new_child.Name = node.Attribute("Name").Value;

                    new_child.Text = node.Attribute("Name").Value;

                    nodes.Add(new_child);//向当前TreeNodeCollection集合中添加当前节点
                    RecursionTreeControl(node, new_child.Nodes, level);//调用本方法进行递归
                }
                else
                {
                    return;
                }
            }
        }
        /// <summary>
        /// 重载dataGridTags的Tag定义
        /// </summary>
        /// <param name="groupName"></param>
        /// <param name="blockName"></param>
        private void QueryTags(string groupName, string blockName)
        {
            //显示block中的tag点明细
            List<Tag> tags = TagConfig.QueryTagsByBlockName<Tag>(groupName, blockName);

            dataGridTags.Rows.Clear();
            //添加进griview
            for (int i = 0; i < tags.Count; i++)
            {
                int index = dataGridTags.Rows.Add();
                dataGridTags.Rows[i].Cells["OpcTagName"].Value = (tags[i]).OpcTagName;
                dataGridTags.Rows[i].Cells["TagName"].Value = (tags[i]).TagName;
                dataGridTags.Rows[i].Cells["DataType"].Value = (tags[i]).DataType;
                dataGridTags.Rows[i].Cells["Value"].Value = (tags[i]).Value;
                //dataGridTags.Rows[i].Cells["Group"].Value = lblGroup.Text;

                dataGridTags.Rows[i].Cells["Qualities"].Value = (tags[i]).Qualities;
                dataGridTags.Rows[i].Cells["TimeStamps"].Value = (tags[i]).TimeStamps;
            }
        }
        /// <summary>
        /// 刷新dataGridTags的Tag数据
        /// </summary>
        /// <param name="groupName">groupName</param>
        private void QueryTagsRecord(string groupName)
        {
            if (groupName =="")
            {
                return;
            }
            List<Tag> tags = new List<Tag>();
            foreach (DataGridViewRow row in dataGridTags.Rows)
            {
                Tag bi = new Tag();
                bi.OpcTagName = row.Cells["OpcTagName"].Value.ToString();
                bi.TagName = row.Cells["TagName"].Value.ToString();
                bi.TimeStamps = DateTime.Now;
                bi.Value = "";
                bi.Qualities = "";
                bi.Message = "";
                tags.Add(bi);
            }

            client.GetTagValuesFromGroup(ref tags, groupName);
            dataGridTags.Rows.Clear();
            for (int i = 0; i < tags.Count; i++)
            {
                int index = dataGridTags.Rows.Add();
                dataGridTags.Rows[i].Cells["OpcTagName"].Value = (tags[i]).OpcTagName;
                dataGridTags.Rows[i].Cells["TagName"].Value = (tags[i]).TagName;
                dataGridTags.Rows[i].Cells["DataType"].Value = (tags[i]).DataType;
                dataGridTags.Rows[i].Cells["Value"].Value = (tags[i]).Value;
                dataGridTags.Rows[i].Cells["Qualities"].Value = (tags[i]).Qualities;
                dataGridTags.Rows[i].Cells["TimeStamps"].Value = (tags[i]).TimeStamps;
                dataGridTags.Rows[i].Cells["Message"].Value = (tags[i]).Message;
            }
        }
        /// <summary>
        /// 重载dataGridTags的Tag数据
        /// </summary> 
        private void IniTagsRecord(string groupName)
        {
            try
            {
                if (groupName.Equals(null) || groupName.Equals(""))
                {
                    return;
                }
                List<Tag> listIn = new List<Tag>();
                //将需要重载的点压入集合
                foreach (DataGridViewRow row in dataGridTags.Rows)
                {
                    Tag bi = new Tag();
                    bi.OpcTagName = row.Cells["OpcTagName"].Value.ToString();
                    bi.TagName = row.Cells["TagName"].Value.ToString();
                    bi.TimeStamps = DateTime.Now;
                    bi.Value = "";
                    bi.Qualities = "";
                    bi.Message = "";
                    listIn.Add(bi);
                }

                //删除分组下所有tag
                client[groupName].RemoveItemsAll();
                //opc中重新加入tag
                client[groupName].AddItems(listIn);
            }
            catch (Exception)
            {
                throw;
            }
        }
        private void TagValueChanged(Tag tag)
        {
            Console.WriteLine("TagName={0}, Value={1}, DataType={2}", tag.TagName, tag.Value, tag.DataType);
        }
        /// <summary>
        /// 服务端断开通知
        /// </summary>
        /// <param name="msg"></param>
        private void OpcServerDisConnected(IOpcClient client)
        {
            Console.WriteLine("服务端断开：" + client.ServerStateDesc);
            client = null;
            System.Environment.Exit(0);
        }
        #endregion

        #region 事件
        //双击显示gridTags
        private void treeTags_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //右键点击，弹出对应的菜单
            if (e.Button == MouseButtons.Left)
            {
                //确定右键的位置  
                Point clickPoint = new Point(e.X, e.Y);
                //在确定后的位置上面定义一个节点  
                TreeNode treeNode = treeTags.GetNodeAt(clickPoint);
                if (treeNode != null)
                {

                    if (treeNode.Level == 0)
                    {
                        //不处理
                    }
                    else if (treeNode.Level == 1)
                    {
                        groupName = treeNode.Parent.Text.ToString();
                        blockName = treeNode.Text.ToString();

                        QueryTags(groupName, blockName);//在gridview中加载需要监视的tag点
                        IniTagsRecord(groupName);//OPC初始化监视的tag点

                        lblGroup.Text = groupName;
                        lblBlock.Text = blockName;
                    }
                    //使treeTags的SelectedNode为当前新建的节点，也就是右键后的节点被选中  
                    treeTags.SelectedNode = treeNode;
                }
            }
        }
        //treeview点击事件，根据层级不同弹出不同的ContextMenuStrip
        private void treeTags_MouseDown(object sender, MouseEventArgs e)
        {
            //右键点击，弹出对应的菜单
            if (e.Button == MouseButtons.Right)
            {
                //确定右键的位置  
                Point clickPoint = new Point(e.X, e.Y);
                //在确定后的位置上面定义一个节点  
                TreeNode treeNode = treeTags.GetNodeAt(clickPoint);
                if (treeNode != null)
                {

                    if (treeNode.Level == 0)
                    {
                        //第一层关联菜单
                        treeNode.ContextMenuStrip = contextMenuGroup;
                    }
                    else if (treeNode.Level == 1)
                    {
                        //第二层关联菜单
                        treeNode.ContextMenuStrip = contextMenuBlock;
                    }
                }
                //使treeTags的SelectedNode为当前新建的节点，也就是右键后的节点被选中  
                treeTags.SelectedNode = treeNode;
            }
        }
        //新增block
        private void toolBlockAdd_Click(object sender, EventArgs e)
        {
            AddBlock ab = new AddBlock(treeTags.SelectedNode.Text);
            if (ab.ShowDialog() == DialogResult.OK)
            {
                //清空后并重新加载
                treeTags.Nodes.Clear();
                XElement doc = XElement.Load(TagConfig.Path);//将加载完成的XML文件显示在TreeView控件中
                RecursionTreeControl(doc, treeTags.Nodes);
                treeTags.ExpandAll();//展开TreeView控件中的所有项               
            }
        }
        //新增tag
        private void toolTagAdd_Click(object sender, EventArgs e)
        {
            chkRefresh.Checked = false;
            //设置当前选中的节点信息
            groupName = treeTags.SelectedNode.Parent.Text;
            blockName = treeTags.SelectedNode.Text;
            lblGroup.Text = groupName;
            lblBlock.Text = blockName;

            AddTags at = new AddTags(this.client, groupName, blockName);
            if (at.ShowDialog() == DialogResult.OK)
            {
                //显示block中的tag点明细
                List<Tag> tags = TagConfig.QueryTagsByBlockName<Tag>(groupName, blockName);

                dataGridTags.Rows.Clear();
                //添加进griview
                for (int i = 0; i < tags.Count; i++)
                {
                    int index = dataGridTags.Rows.Add();
                    dataGridTags.Rows[dataGridTags.RowCount - 1].Cells["OpcTagName"].Value = (tags[i]).OpcTagName;
                    dataGridTags.Rows[dataGridTags.RowCount - 1].Cells["TagName"].Value = (tags[i]).TagName;
                    dataGridTags.Rows[dataGridTags.RowCount - 1].Cells["DataType"].Value = (tags[i]).DataType;
                    dataGridTags.Rows[dataGridTags.RowCount - 1].Cells["Value"].Value = (tags[i]).Value;
                    //dataGridTags.Rows[dataGridTags.RowCount - 1].Cells["Group"].Value = lblGroup.Text;

                    dataGridTags.Rows[i].Cells["Qualities"].Value = (tags[i]).Qualities;
                    dataGridTags.Rows[i].Cells["TimeStamps"].Value = (tags[i]).TimeStamps;
                    dataGridTags.Rows[i].Cells["Message"].Value = (tags[i]).Message;
                }
                IniTagsRecord(groupName);//OPC初始化监视的tag点
            }
        }
        //删除block
        private void toolBlockDel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否删除", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                if (!TagConfig.DelBlock(treeTags.SelectedNode.Text))
                {
                    MessageBox.Show("删除失败，不在在节点");
                }
                else
                {
                    //成功删除后刷新
                    treeTags.Nodes.Clear();
                    //加载treeview 
                    XElement doc = XElement.Load(TagConfig.Path);//将加载完成的XML文件显示在TreeView控件中
                    RecursionTreeControl(doc, treeTags.Nodes);
                    dataGridTags.Rows.Clear();
                    treeTags.ExpandAll();//展开TreeView控件中的所有项

                    groupName = "";
                    blockName = ""; ;
                    lblGroup.Text = groupName;
                    lblBlock.Text = blockName;
                }
            }
        }
        //删除tag
        private void toolTagDel_Click(object sender, EventArgs e)
        {
            string tagName = dataGridTags.Rows[dataGridTags.CurrentRow.Index].Cells["TagName"].Value.ToString();
            Tag tag = new Tag() { TagName = tagName };
            if (TagConfig.DelTag(groupName, blockName, tag))
            {
                dataGridTags.Rows.RemoveAt(dataGridTags.CurrentRow.Index);
            }
            IniTagsRecord(groupName);//OPC初始化监视的tag点
        }
        //treeview刷新
        private void toolGroupsRefresh_Click(object sender, EventArgs e)
        {
            //清空并重新加载treeview
            treeTags.Nodes.Clear();
            //加载treeview 
            XElement doc = XElement.Load(TagConfig.Path);//将加载完成的XML文件显示在TreeView控件中
            RecursionTreeControl(doc, treeTags.Nodes);
            treeTags.ExpandAll();//展开TreeView控件中的所有项
        }
        //gridview刷新
        private void toolTagsRefresh_Click(object sender, EventArgs e)
        {
            QueryTags(groupName, blockName);
        }
        //gridview刷新
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            if (client != null)
            {
                QueryTagsRecord(groupName);
            }
           
        }

        //gridview弹出ContextMenuStrip
        private void dataGridTags_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    //若行已是选中状态就不再进行设置
                    if (dataGridTags.Rows[e.RowIndex].Selected == false)
                    {
                        dataGridTags.ClearSelection();
                        dataGridTags.Rows[e.RowIndex].Selected = true;
                    }
                    //只选中一行时设置活动单元格
                    if (dataGridTags.SelectedRows.Count == 1)
                    {
                        dataGridTags.CurrentCell = dataGridTags.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    }
                    //弹出操作菜单
                    contextMenuTags.Show(MousePosition.X, MousePosition.Y);
                }
            }
        }
        //是否自动获取数据
        private void chkRefresh_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRefresh.Checked == false)
            {
                timerRefresh.Stop();
                treeTags.Enabled = true;
                dataGridTags.Enabled = true;
                groupBoxWatch.Enabled = false;
            }
            else if (chkRefresh.Checked == true)
            {
                treeTags.Enabled = false;
                dataGridTags.Enabled = false;
                groupBoxWatch.Enabled = true;
                timerRefresh.Interval = Convert.ToInt32(cmbInterval.Text);
                timerRefresh.Start();
                IniTagsRecord(groupName);
            }
        }
        //刷新数据
        private void timerRefresh_Tick(object sender, EventArgs e)
        {
            if (chkRefresh.Checked == true)
            {
                if (client!=null)
                {
                    QueryTagsRecord(groupName);
                    dataGridTags.ClearSelection();
                }              
            }
        }
        //改变刷新周期
        private void cmbInterval_SelectedIndexChanged(object sender, EventArgs e)
        {
            timerRefresh.Interval = Convert.ToInt32(cmbInterval.Text);
        }
        //IP设置
        private void btnSet_Click(object sender, EventArgs e)
        {
            IniSetting iniS = new IniSetting();
            if (iniS.ShowDialog() == DialogResult.OK)
            {
                IP = IniReader.Instance.G_IP;
                ServerName = IniReader.Instance.G_SERVERNAME;
                MessageBox.Show("重启后配置生效~");
            }
        }
        #endregion
    }
}


////对于生成树的部分其实还有一种Linq的实现方式如下
//private void SaveToXml()

//        {

//            XDeclaration dec = new XDeclaration("1.0", "utf-8", "yes");

//            XDocument xml = new XDocument(dec);



//            XElement root = new XElement("Tree");



//            foreach (TreeNode node in treeTags.Nodes)

//            {

//                XElement e = CreateElements(node);

//                root.Add(e);

//            }

//            xml.Add(root);

//            xml.Save("TreeXml.xml");

//        }



//private XElement CreateElements(TreeNode node)

//{

//    XElement root = CreateElement(node);



//    foreach (TreeNode n in node.Nodes)

//    {

//        XElement e = CreateElements(n);

//        root.Add(e);

//    }

//    return root;

//}



//private XElement CreateElement(TreeNode node)

//{

//    return new XElement("Node",

//        new XAttribute("Name", node.Name),

//        new XAttribute("Text", node.Text)

//        );

//}
//    }
//}
