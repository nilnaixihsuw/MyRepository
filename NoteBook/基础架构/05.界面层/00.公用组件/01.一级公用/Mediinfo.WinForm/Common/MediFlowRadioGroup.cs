using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;

namespace Mediinfo.WinForm
{
    /// <summary>
    /// 横向单选按钮(多行)
    /// </summary>
    public class MediFlowRadioGroup : PanelControl
    {
        #region 构造函数

        public MediFlowRadioGroup()
        {
            this.DoubleBuffered = true;
            this.ResizeRedraw = true;

            this.BorderStyle = BorderStyles.NoBorder;
        }

        #endregion

        #region 变量

        private List<CheckEdit> items = new List<CheckEdit>();
        private int horzSpace = 80;
        private int vertSpace = 10;

        public event EventHandler SelectIndexChangeed;

        #endregion

        #region 属性

        /// <summary>
        /// 获取或设置单选按钮
        /// </summary>
        [Browsable(false)]
        public List<CheckEdit> Items
        {
            get { return items; }
        }

        /// <summary>
        /// 单选按钮横向间距
        /// </summary>
        [Browsable(true)]
        [DefaultValue(80)]
        [Description("单选按钮横向间距。")]
        public int HorzSpace
        {
            get { return horzSpace; }
            set { horzSpace = value; }
        }

        /// <summary>
        /// 单选按钮纵向间距
        /// </summary>
        [Browsable(true)]
        [DefaultValue(10)]
        [Description("单选按钮纵向间距。")]
        public int VertSpace
        {
            get { return vertSpace; }
            set { vertSpace = value; }
        }

        #endregion

        #region 方法

        /// <summary>
        /// 添加单选按钮
        /// </summary>
        /// <param name="text">显示文字</param>
        /// <param name="tag">Tag值</param>
        /// <param name="isChecked">是否选中</param>
        public void AddItem(string text, object tag = null, bool isChecked = false)
        {
            CheckEdit checkEdit = new CheckEdit();
            checkEdit.Properties.AutoWidth = true;
            checkEdit.Properties.Caption = text;
            checkEdit.Properties.CheckStyle = CheckStyles.Radio;
            checkEdit.Properties.RadioGroupIndex = 1;
            checkEdit.Properties.AllowFocused = false;
            checkEdit.Font = this.Font;
            checkEdit.TabIndex = items.Count;
            checkEdit.Tag = tag;
            checkEdit.Checked = isChecked;
            checkEdit.CheckedChanged += CheckEdit_CheckedChanged;

            items.Add(checkEdit);
        }

        /// <summary>
        /// 显示单选按钮(多行)
        /// </summary>
        public new void Show()
        {
            // 获取最大单选按钮长度
            int maxWidth = GetRadioMaxWidth();
            // 设置单选按钮的位置
            SetRadioLocation(maxWidth);
            // 重新绘制页面
            this.Invalidate();
        }

        /// <summary>
        /// 清除单选按钮
        /// </summary>
        public void ClearItem()
        {
            // 清除单选按钮
            items.Clear();
            // 清空控件
            this.Controls.Clear();
            // 重新绘制页面
            this.Invalidate();
        }

        /// <summary>
        /// 获取最大单选按钮长度
        /// </summary>
        /// <returns></returns>
        private int GetRadioMaxWidth()
        {
            // 如果按钮数量为0直接返回0
            if (items.Count <= 0)
                return 0;
            // 取最大单选按钮的长度
            int maxWidth = items[0].Width;
            for (int i = 1; i < items.Count; i++)
            {
                if (items[i].Width > maxWidth)
                {
                    maxWidth = items[i].Width;
                }
            }
            return maxWidth;
        }

        /// <summary>
        /// 设置单选按钮的位置
        /// </summary>
        /// <param name="maxWidth">最大单选按钮长度</param>
        /// <param name="radioSpace">按钮间距(横向)</param>
        private void SetRadioLocation(int maxWidth)
        {
            // 如果按钮数量为0或按钮长度大约控件长度则直接返回
            if (items.Count <= 0 || maxWidth > this.Width)
                return;

            // 按钮初始坐标
            int xPos = 0;
            int yPos = 0;

            for (int i = 0; i < items.Count; i++)
            {
                CheckEdit radioButton = items[i];
                radioButton.Location = new Point(xPos, yPos);
                this.Controls.Add(radioButton);

                if (xPos + 2 * maxWidth + horzSpace <= this.Width)
                {
                    xPos += maxWidth + horzSpace;
                }
                else
                {
                    xPos = 0;
                    yPos = yPos + radioButton.Height + vertSpace;
                }
            }

            // 设置控件高度
            this.Height = yPos + items[0].Height * 2;
        }

        #endregion

        #region 事件

        private void CheckEdit_CheckedChanged(object sender, EventArgs e)
        {
            SelectIndexChangeed?.Invoke(sender, e);
        }

        #endregion
    }
}