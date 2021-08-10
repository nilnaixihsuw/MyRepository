using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Mediinfo.DTO.HIS.XT;
using Mediinfo.HIS.Core;
using Mediinfo.ServiceProxy.JCJG.GongYong;
using Mediinfo.Utility.Extensions;
using Mediinfo.WinForm.HIS.Core;

namespace Mediinfo.WinForm.HIS.Controls
{
    /// <summary>
    /// 
    /// </summary>
    public partial class W_Msg_ChooseReceiver : MediFormWithQX
    {
        /// <summary>
        /// 已选收件人
        /// </summary>
        public List<E_XT_SHOUJIANREN_NEW> shouJianRenList = new List<E_XT_SHOUJIANREN_NEW>();


        /// <summary>
        /// 
        /// </summary>
        public W_Msg_ChooseReceiver()
        {
            InitializeComponent();

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        public W_Msg_ChooseReceiver(List<E_XT_SHOUJIANREN_NEW> list)
        {
            InitializeComponent();
            shouJianRenList = list;
            this.rightBindingSource.DataSource = shouJianRenList;
        }

        private void mediComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string xiangMu = mediComboBox1.SelectedText;
            if (string.IsNullOrWhiteSpace(xiangMu)) return;
            string shouZiMu = xiangMu.Substring(0, 1);
            if (xiangMu.Equals("全部"))
            {
                shouZiMu = "全部";
            }

            var ret = new JCJGXiaoXiService().GetShouJianRen(shouZiMu);
            if (ret.ReturnCode != Enterprise.ReturnCode.SUCCESS)
            {
                MediMsgBox.Warn("获取收件人信息失败");
            }
            else
            {
                var list = ret.Return;
                if (this.rightBindingSource.DataSource is List<E_XT_SHOUJIANREN_NEW> listRight && listRight.Count != 0)
                {
                    list.RemoveAll(o => listRight.Select(p => p.SHOUJIANRID).ToList().Contains(o.SHOUJIANRID));
                    leftBindingSource.DataSource = list;
                }
                else
                {
                    leftBindingSource.DataSource = list;
                }
            }

        }

        private void mediButtonCancle_Click(object sender, EventArgs e)
        {
            this.Close();
            DialogResult = DialogResult.Cancel;
        }

        /// <summary>
        /// 全部移动到右侧
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mediBlueButton_RightAll_Click(object sender, EventArgs e)
        {
            if (this.leftBindingSource.DataSource is List<E_XT_SHOUJIANREN_NEW> listLeft && listLeft.Count != 0)
            {
                var list = listLeft.Where(item => item != null).ToList();

                if (this.rightBindingSource.DataSource is List<E_XT_SHOUJIANREN_NEW> listRight && listRight.Count != 0)
                {
                    var unionList = list.Union(listRight).ToList();
                    this.rightBindingSource.DataSource = unionList;
                }
                else
                {
                    this.rightBindingSource.DataSource = list;
                }

                this.leftBindingSource.DataSource = null;

                this.mediGridView1.RefreshData();
                this.mediGridView2.RefreshData();
            }
        }

        /// <summary>
        /// 选中行移动到右侧
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mediBlueButton_Right_Click(object sender, EventArgs e)
        {
            var listRight = this.rightBindingSource.DataSource as List<E_XT_SHOUJIANREN_NEW> ??
                            new List<E_XT_SHOUJIANREN_NEW>();

            var indexList = new List<int>();
            if (this.mediGridView1.GetSelectedRows() != null)
                indexList = this.mediGridView1.GetSelectedRows().ToList();
            else
                indexList.Add(this.mediGridView1.FocusedRowHandle);
            if (indexList.Count == 0)
            {
                MediMsgBox.Show("请选择一行数据！");
                return;
            }
            foreach (var t in indexList)
            {
                if (this.mediGridView1.GetRow(t) is E_XT_SHOUJIANREN_NEW jb)
                {
                    if (listRight.Any(p => p.SHOUJIANRID == jb.SHOUJIANRID))
                    {
                        MediMsgBox.Show("该收件人已添加，不能重复添加！");
                        return;
                    }
                    listRight.Add(jb);
                }
            }
            this.rightBindingSource.DataSource = listRight;
            this.mediGridView1.DeleteSelectedRows();

            this.mediGridView1.RefreshData();
            this.mediGridView2.RefreshData();
        }

        /// <summary>
        /// 全部移动到左侧
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mediBlueButton_LeftAll_Click(object sender, EventArgs e)
        {
            if (this.rightBindingSource.DataSource is List<E_XT_SHOUJIANREN_NEW> listRight && listRight.Count != 0)
            {
                var list = listRight.Where(item => item != null).ToList();
                if (this.leftBindingSource.DataSource is List<E_XT_SHOUJIANREN_NEW> listLeft && listLeft.Count != 0)
                {
                    var unionList = list.Union(listLeft).ToList();
                    this.leftBindingSource.DataSource = unionList;
                }
                else
                {
                    this.leftBindingSource.DataSource = list;
                }
                this.rightBindingSource.DataSource = null;
                this.mediGridView2.RefreshData();
                this.mediGridView1.RefreshData();
            }
        }

        private void mediBlueButton_Left_Click(object sender, EventArgs e)
        {
            List<int> index = new List<int>();
            if (this.mediGridView2.GetSelectedRows() != null)
                index = this.mediGridView2.GetSelectedRows().ToList();
            else
                index.Add(this.mediGridView2.FocusedRowHandle);
            if (index.Count == 0)
            {
                MediMsgBox.Show("请选择一行数据！");
                return;
            }

            if (leftBindingSource.DataSource == null)
                leftBindingSource.DataSource = new List<E_XT_SHOUJIANREN_NEW>();

            foreach (var t in index)
            {
                if (this.mediGridView2.GetRow(t) is E_XT_SHOUJIANREN_NEW jb)
                {
                    if (!this.leftBindingSource.Contains(jb))
                        this.leftBindingSource.Add(jb);
                }
            }
            this.mediGridView2.DeleteSelectedRows();
            this.mediGridView2.RefreshData();
            this.mediGridView1.RefreshData();
        }

        private void mediBlueButtonSave_Click(object sender, EventArgs e)
        {
            if (this.rightBindingSource.DataSource is List<E_XT_SHOUJIANREN_NEW> listRight)
            {
                var max = GYCanShuHelper.GetCanShu(HISClientHelper.YINGYONGID, "公用_发送消息最多选择收件人数量", "10").ToInt(10);
                if (listRight.Count > max)
                {
                    MediMsgBox.FloatMsg(this, "", $"最多允许选择{max}个收件人/科室/病区", "3");
                    return;
                }
                shouJianRenList = listRight;
                DialogResult = DialogResult.OK;
            }
            else
            {
                MediMsgBox.Warn("没有选择收件人，请选择收件人");
            }
        }

        private static List<E_XT_SHOUJIANREN_NEW> MatchInputText(List<E_XT_SHOUJIANREN_NEW> list, string inputText)
        {
            var matchList = new List<E_XT_SHOUJIANREN_NEW>();
            for (int i = list.Count - 1; i >= 0; i--)
            {
                if (list[i].SHURUMA1 != null && list[i].SHURUMA1.StartsWith(inputText))
                {
                    matchList.Add(list[i]);
                    list.RemoveAt(i);
                    continue;
                }

                if (list[i].ZHIGONGGH != null && list[i].ZHIGONGGH.StartsWith(inputText))
                {
                    matchList.Add(list[i]);
                    list.RemoveAt(i);
                    continue;
                }
            }
            var dataList = matchList.Union(list).ToList();
            return dataList;
        }

        /// <summary>
        /// 过滤
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mediSearchControl1_TextChanged(object sender, EventArgs e)
        {
            var inputText = mediSearchControl1.Text.ToUpper();//输入文本
            if (string.IsNullOrEmpty(inputText))
                return;

            if (this.leftBindingSource.DataSource is List<E_XT_SHOUJIANREN_NEW> list)
            {
                var dataList = MatchInputText(list, inputText);
                this.leftBindingSource.DataSource = null;
                this.leftBindingSource.DataSource = dataList;
            }
        }

        private void mediSearchControl2_TextChanged(object sender, EventArgs e)
        {
            var inputText = mediSearchControl2.Text.ToUpper();//输入文本
            if (string.IsNullOrEmpty(inputText))
                return;

            if (this.rightBindingSource.DataSource is List<E_XT_SHOUJIANREN_NEW> list)
            {
                var dataList = MatchInputText(list, inputText);
                this.rightBindingSource.DataSource = null;
                this.rightBindingSource.DataSource = dataList;
            }
        }
    }

}
