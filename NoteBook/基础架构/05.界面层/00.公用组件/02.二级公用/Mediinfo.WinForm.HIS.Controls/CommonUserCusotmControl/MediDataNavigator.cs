using Mediinfo.WinForm.HIS.Controls.Properties;

using System;
using System.Drawing;
using System.Windows.Forms;

namespace Mediinfo.WinForm.HIS.Controls
{
    public partial class MediDataNavigator : MediUserControl
    {
        public MediDataNavigator()
        {
            InitializeComponent();
        }

        private int curPage = 1;
        public delegate void MyPagerEvents(int curPage, int pageSize);

        public event MyPagerEvents myPagerEvents;

        /// <summary>
        /// 页大小
        /// </summary>
        public int PageSize
        {
            get
            {
                int a = 0;
                return int.TryParse(this.pageSize.Text, out a) ? Convert.ToInt32(this.pageSize.Text) : 0;
            }
            set => pageSize.Text = value.ToString();
        }

        /// <summary>
        /// 所有页
        /// </summary>
        public int AllCount { get; set; }

        /// <summary>
        /// 当前页
        /// </summary>
        public int CurrentPage
        {
            get => int.TryParse(this.textEditCurPage.Text, out _) ? Convert.ToInt32(this.textEditCurPage.Text) : 0;
            set => textEditCurPage.Text = value.ToString();
        }

        /// <summary>
        /// 刷新pagebar
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="allCount"></param>
        /// <param name="curPage"></param>
        public void RefreshPagerBar(int pageSize, int allCount, int curPage)
        {
            this.allCount.Text = allCount.ToString();
            this.pageSize.Text = pageSize.ToString();
            this.curPage = curPage;
            PageSize = pageSize;
            AllCount = allCount;
            this.textEditAllPageCount.Text = GetPageCount().ToString();

            textEditCurPage.Text = curPage.ToString();

            CurrentPage = curPage;
            // 如果没有数据直接返回
            if (allCount <= 0)
                return;
            if (curPage == 0 && GetPageCount() > 0)
            {
                curPage = 1;
                myPagerEvents?.Invoke(curPage, pageSize);
            }
            if (curPage > GetPageCount())
            {
                curPage = GetPageCount();
                myPagerEvents?.Invoke(curPage, pageSize);
            }
        }

        /// <summary>
        /// 获取总条数
        /// </summary>
        /// <returns></returns>
        public int GetAllCount()        {            return AllCount;        }

        /// <summary>
        /// 获得当前页编号，从1开始
        /// </summary>
        /// <returns></returns>
        public int GetCurrentPage()        {            return curPage;        }

        /// <summary>
        /// 获得总页数
        /// </summary>
        /// <returns></returns>
        public int GetPageCount()        {            int count = 0;            if (AllCount % PageSize == 0)            {                count = AllCount / PageSize;            }            else                count = AllCount / PageSize + 1;            return count;        }        private void simpleButtonNext_Click(object sender, EventArgs e)        {            if (myPagerEvents != null)
            {
                if (curPage < GetPageCount())
                    curPage += 1;
                myPagerEvents?.Invoke(curPage, PageSize);
            }        }        private void simpleButtonEnd_Click(object sender, EventArgs e)        {            if (myPagerEvents != null)
            {
                curPage = GetPageCount();
                myPagerEvents?.Invoke(curPage, PageSize);
            }        }        private void simpleButtonPre_Click(object sender, EventArgs e)        {            if (myPagerEvents != null)            {                if (curPage > 1)                    curPage -= 1;                myPagerEvents(curPage, PageSize);            }        }        private void simpleButtonFirst_Click(object sender, EventArgs e)        {            if (myPagerEvents != null)            {                curPage = 1;                myPagerEvents(curPage, PageSize);            }        }

        /// <summary>
        /// 跳转页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textEditCurPage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue != 13) return;
            int selPage = Convert.ToInt32(textEditCurPage.Text);
            if (myPagerEvents != null)
            {
                if ((selPage >= 1) && (selPage <= GetPageCount()))
                    curPage = selPage;
                myPagerEvents?.Invoke(curPage, PageSize);
            }
        }

        /// <summary>
        /// 每页显示条数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pageSize_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue != 13) return;
            if (myPagerEvents != null)            {                curPage = 1;
                PageSize = Convert.ToInt32(pageSize.Text);                myPagerEvents(curPage, PageSize);            }
        }

        private void simpleButtonFirst_MouseHover(object sender, EventArgs e)
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Resources));
            this.simpleButtonFirst.Image = ((System.Drawing.Image)(resources.GetObject("pagination_first_hover")));
        }

        private void simpleButtonFirst_EnabledChanged(object sender, EventArgs e)
        {
            if (simpleButtonFirst.Enabled)
            {
                System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Resources));
                this.simpleButtonFirst.Image = ((System.Drawing.Image)(resources.GetObject("pagination_first_disabled")));
            }
        }
        private void simpleButtonPre_EnabledChanged(object sender, EventArgs e)
        {
            if (simpleButtonPre.Enabled)
            {
                System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Resources));
                this.simpleButtonPre.Image = ((System.Drawing.Image)(resources.GetObject("pagination_previous_disabled")));
            }
        }

        private void simpleButtonPre_MouseHover(object sender, EventArgs e)
        {
            this.simpleButtonPre.ForeColor = Color.Transparent;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Resources));
            this.simpleButtonPre.Image = ((System.Drawing.Image)(resources.GetObject("pagination_previous_hover")));
        }

        private void simpleButtonNext_EnabledChanged(object sender, EventArgs e)
        {
            if (simpleButtonNext.Enabled)
            {
                System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Resources));
                this.simpleButtonNext.Image = ((System.Drawing.Image)(resources.GetObject("pagination_next_disabled")));
            }
        }

        private void simpleButtonNext_MouseHover(object sender, EventArgs e)
        {
            this.simpleButtonNext.ForeColor = Color.Transparent;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Resources));
            this.simpleButtonNext.Image = ((System.Drawing.Image)(resources.GetObject("pagination_next_hover")));
        }

        private void simpleButtonEnd_EnabledChanged(object sender, EventArgs e)
        {
            if (simpleButtonFirst.Enabled)
            {
                System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Resources));
                this.simpleButtonEnd.Image = ((System.Drawing.Image)(resources.GetObject("pagination_last_disabled")));
            }
        }

        private void simpleButtonEnd_MouseHover(object sender, EventArgs e)
        {
            this.simpleButtonEnd.ForeColor = Color.Transparent;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Resources));
            this.simpleButtonEnd.Image = ((System.Drawing.Image)(resources.GetObject("pagination_last_hover")));
        }

        private void simpleButtonFirst_MouseLeave(object sender, EventArgs e)
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Resources));
            this.simpleButtonFirst.Image = ((System.Drawing.Image)(resources.GetObject("pagination_first")));
        }

        private void simpleButtonPre_MouseLeave(object sender, EventArgs e)
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Resources));
            this.simpleButtonPre.Image = ((System.Drawing.Image)(resources.GetObject("pagination_previous")));
        }

        private void simpleButtonNext_MouseLeave(object sender, EventArgs e)
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Resources));
            this.simpleButtonNext.Image = ((System.Drawing.Image)(resources.GetObject("pagination_next")));
        }

        private void simpleButtonEnd_MouseLeave(object sender, EventArgs e)
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Resources));
            this.simpleButtonEnd.Image = ((System.Drawing.Image)(resources.GetObject("pagination_last")));
        }
        
    }
}