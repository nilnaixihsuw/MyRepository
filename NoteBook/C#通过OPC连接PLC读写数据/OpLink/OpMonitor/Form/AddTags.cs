using OpcClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OpMonitor
{
    public partial class AddTags : Form
    {
        private IOpcClient client;
        private List<string> branches;

        private string groupName;
        private string blockName;

        public AddTags()
        {
            InitializeComponent();
        }

        public AddTags(IOpcClient iOpc, string groupName, string blockName)
        {
            InitializeComponent();
            this.client = iOpc;
            this.groupName = groupName;
            this.blockName = blockName;
            lblGroup.Text = this.groupName;
            lblBlock.Text = this.blockName;
            client.ShowBranchesByTree(treeViewBranches.Nodes);
        }

        private void AddTags_Load(object sender, EventArgs e)
        {
            //client = new DaOpc();
            //client.Connect("KEPware.KEPServerEx.V4");

            //client.ShowBranchesTree(treeViewBranches.Nodes);

            //branches=client.ShowBranches();
            //listGroups.Items.Clear();
            //foreach (string item in branches)
            //{
            //    listGroups.Items.Add(item);
            //}
        }

        #region 事件
        //显示items
        private void treeViewBranches_AfterSelect(object sender, TreeViewEventArgs e)
        {
            string branch = treeViewBranches.SelectedNode.FullPath.ToString().Replace("\\", ".");
            List<string> leafs = client.ShowLeafs(branch);
            //List<string> leafs = client.ShowLeafs("Channel_3.Device_4");
            listItems.Items.Clear();
            foreach (string item in leafs)
            {
                listItems.Items.Add(item);

            }
            tsslItemsNum.Text = "Item数量:" + leafs.Count.ToString() + "    ";
        }
        //新增tag
        private void btnAddTags_Click(object sender, EventArgs e)
        {
            List<Tag> tags = new List<Tag>();
            foreach (string item in listItems.CheckedItems)
            {
                tags.Add(new Tag() { OpcTagName = item, TagName = item.Remove(0, item.LastIndexOf(".") + 1) });
            }
            Tag t = new Tag();

            //判断是否在在重复项
            for (int i = 0; i < dataGridTags.Rows.Count; i++)
            {
                Tag dfd;
                string TagName = dataGridTags.Rows[i].Cells["TagName"].Value.ToString();
                int count = tags.Where(p => p.TagName == TagName).Count();
                if (count != 0)
                {
                    MessageBox.Show("在在重复项：item = " + OpcTagName, "提示", MessageBoxButtons.OK);
                    return;
                }
            }

            //将勾选的tags添加进griview
            for (int i = 0; i < tags.Count; i++)
            {
                int index = dataGridTags.Rows.Add();
                dataGridTags.Rows[dataGridTags.RowCount - 1].Cells["OpcTagName"].Value = (tags[i]).OpcTagName;
                dataGridTags.Rows[dataGridTags.RowCount - 1].Cells["TagName"].Value = (tags[i]).TagName;
                dataGridTags.Rows[dataGridTags.RowCount - 1].Cells["DataType"].Value = (tags[i]).DataType;
                dataGridTags.Rows[dataGridTags.RowCount - 1].Cells["Value"].Value = (tags[i]).Value;
                dataGridTags.Rows[dataGridTags.RowCount - 1].Cells["Group"].Value = lblGroup.Text;

                //dataGridView1.Rows[i].Cells["Qualities"].Value = (tags[i]).Qualities;
                //dataGridView1.Rows[i].Cells["TimeStamps"].Value = (tags[i]).TimeStamps;
            }
            tsslTagsNum.Text = "Tag数量:" + dataGridTags.Rows.Count.ToString();
        }
        //全选/反选
        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < listItems.Items.Count; i++)
            {
                listItems.SetItemChecked(i, chkAll.Checked);
            }
        }
        //列表中删除
        private void toolStripTagDel_Click(object sender, EventArgs e)
        {
            dataGridTags.Rows.Remove(dataGridTags.CurrentRow);
            tsslTagsNum.Text = "Tag数量:" + dataGridTags.Rows.Count.ToString();
        }
        //校验重复性
        private void btnCheckRepeat_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridTags.Rows)
            {
                //Xml文件中在在此名称，修改dataGridTags的显示背景色
                if (TagConfig.ExistTag(row.Cells["TagName"].Value.ToString()))
                {
                    row.DefaultCellStyle.BackColor = Color.Yellow;
                }
                else
                {
                    row.DefaultCellStyle.BackColor = Color.White;
                }

            }
        }
        //保存提交的数据
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (dataGridTags.Rows.Count == 0)
            {
                MessageBox.Show("不存在需要提交的数据集！");
                return;
            }
            List<Tag> tags = new List<Tag>();
            foreach (DataGridViewRow row in dataGridTags.Rows)
            {
                Tag tag = new Tag() { TagName = row.Cells["TagName"].Value.ToString(), OpcTagName = row.Cells["OpcTagName"].Value.ToString() };
                tags.Add(tag);
            }
            if (TagConfig.CreateTag(groupName, blockName, tags))
            {
                this.DialogResult = DialogResult.OK;
                //this.Close();
                this.Dispose();
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
        #endregion      
    }
}
