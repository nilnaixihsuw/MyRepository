using Mediinfo.WinForm.HIS.Controls.Properties;

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Mediinfo.WinForm.HIS.Controls
{
    public partial class MediNavigator : MediUserControl
    {
        public MediNavigator()
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
            get
            {
                var a = 0;
                return int.TryParse(this.textEditCurPage.Text, out a) ? Convert.ToInt32(this.textEditCurPage.Text) : 0;
            }
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
            Debug.WriteLine("刷新分页，当前页为：" + curPage.ToString()+"总页数为："+allCount.ToString()); 
            this.allCount.Text = allCount.ToString();
            this.pageSize.Text = pageSize.ToString();
            this.curPage = curPage;
            PageSize = pageSize;
            AllCount = allCount;
            this.textEditAllPageCount.Text = GetPageCount().ToString();

            textEditCurPage.Text = curPage.ToString();
                 
            CurrentPage = curPage;
            // 如果没有数据直接返回
            if (allCount <= 0|| curPage == 0|| curPage > GetPageCount())
                return;
          
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
        public int GetPageCount()        {            int count = 0;            if (AllCount % PageSize == 0)            {                count = AllCount / PageSize;            }            else                count = AllCount / PageSize + 1;            return count;        }
        
        /// <summary>
        /// 下一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonNext_Click(object sender, EventArgs e)        {
            nextPage();        }        /// <summary>
        /// 尾页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>        private void simpleButtonEnd_Click(object sender, EventArgs e)        {            endPage();        }        /// <summary>
        /// 下一页数据
        /// </summary>        public void nextPage()
        {
            if (myPagerEvents != null)            {                if (curPage < (GetPageCount() - 1))
                {
                    curPage += 1;
                    myPagerEvents?.Invoke(curPage, PageSize);

                    this.simpleButtonFirst.Enabled = true;
                    this.simpleButtonPre.Enabled = true;
                }                else if (curPage == (GetPageCount() - 1))   // 当前页等于总页数时，下一页按钮不可以用
                {
                    curPage += 1;
                    myPagerEvents?.Invoke(curPage, PageSize);

                    simpleButtonNext.Enabled = false;
                    this.simpleButtonEnd.Enabled = false;
                }            }
        }        /// <summary>
        /// 上一页数据
        /// </summary>        public void prevPage()
        {
            if (myPagerEvents != null)            {                if (curPage > 2)
                {
                    curPage -= 1;
                    myPagerEvents(curPage, PageSize);
                    this.simpleButtonEnd.Enabled = true;
                    this.simpleButtonNext.Enabled = true;
                }                else if (curPage == 2)
                {
                    curPage -= 1;
                    myPagerEvents(curPage, PageSize);
                    this.simpleButtonFirst.Enabled = false;
                    this.simpleButtonPre.Enabled = false;
                }            }
        }        /// <summary>
        /// 首页
        /// </summary>        public void homePage()
        {
            if (myPagerEvents != null)            {                curPage = 1;                myPagerEvents(curPage, PageSize);
                this.simpleButtonFirst.Enabled = false;
                this.simpleButtonPre.Enabled = false;
                this.simpleButtonEnd.Enabled = true;
                this.simpleButtonNext.Enabled = true;            }
        }        /// <summary>
        /// 尾页
        /// </summary>        public void endPage()
        {
            if (myPagerEvents != null)            {
                curPage = GetPageCount();
                myPagerEvents?.Invoke(curPage, PageSize);

                this.simpleButtonEnd.Enabled = false;
                this.simpleButtonNext.Enabled = false;
                this.simpleButtonFirst.Enabled = true;
                this.simpleButtonPre.Enabled = true;
            }
        }        /// <summary>
        /// 前一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>        private void simpleButtonPre_Click(object sender, EventArgs e)        {            prevPage();        }        /// <summary>
        /// 首页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>        private void simpleButtonFirst_Click(object sender, EventArgs e)        {
            homePage();        }
        
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
            ComponentResourceManager resources = new ComponentResourceManager(typeof(Resources));
            this.simpleButtonFirst.Image = ((Image)(resources.GetObject("pagination_first_hover")));
        }

        private void simpleButtonFirst_EnabledChanged(object sender, EventArgs e)
        {
            if (simpleButtonFirst.Enabled)
            {
                ComponentResourceManager resources = new ComponentResourceManager(typeof(Resources));
                this.simpleButtonFirst.Image = ((Image)(resources.GetObject("pagination_first_disabled")));
            }
        }

        private void simpleButtonPre_EnabledChanged(object sender, EventArgs e)
        {
            if (simpleButtonPre.Enabled)
            {
                ComponentResourceManager resources = new ComponentResourceManager(typeof(Resources));
                this.simpleButtonPre.Image = ((Image)(resources.GetObject("pagination_previous_disabled")));
            }
        }

        private void simpleButtonPre_MouseHover(object sender, EventArgs e)
        {
            this.simpleButtonPre.ForeColor = Color.Transparent;
            ComponentResourceManager resources = new ComponentResourceManager(typeof(Resources));
            this.simpleButtonPre.Image = ((Image)(resources.GetObject("pagination_previous_hover")));
        }

        private void simpleButtonNext_EnabledChanged(object sender, EventArgs e)
        {
            if (simpleButtonNext.Enabled)
            {
                ComponentResourceManager resources = new ComponentResourceManager(typeof(Resources));
                this.simpleButtonNext.Image = ((Image)(resources.GetObject("pagination_next_disabled")));
            }
        }

        private void simpleButtonNext_MouseHover(object sender, EventArgs e)
        {
            this.simpleButtonNext.ForeColor = Color.Transparent;
            ComponentResourceManager resources = new ComponentResourceManager(typeof(Resources));
            this.simpleButtonNext.Image = ((Image)(resources.GetObject("pagination_next_hover")));
        }

        private void simpleButtonEnd_EnabledChanged(object sender, EventArgs e)
        {
            if (simpleButtonFirst.Enabled)
            {
                ComponentResourceManager resources = new ComponentResourceManager(typeof(Resources));
                this.simpleButtonEnd.Image = ((Image)(resources.GetObject("pagination_last_disabled")));
            }
        }

        private void simpleButtonEnd_MouseHover(object sender, EventArgs e)
        {
            this.simpleButtonEnd.ForeColor = Color.Transparent;
            ComponentResourceManager resources = new ComponentResourceManager(typeof(Resources));
            this.simpleButtonEnd.Image = ((Image)(resources.GetObject("pagination_last_hover")));
        }

        private void simpleButtonFirst_MouseLeave(object sender, EventArgs e)
        {
            ComponentResourceManager resources = new ComponentResourceManager(typeof(Resources));
            this.simpleButtonFirst.Image = ((Image)(resources.GetObject("pagination_first")));
        }

        private void simpleButtonPre_MouseLeave(object sender, EventArgs e)
        {
            ComponentResourceManager resources = new ComponentResourceManager(typeof(Resources));
            this.simpleButtonPre.Image = ((Image)(resources.GetObject("pagination_previous")));
        }

        private void simpleButtonNext_MouseLeave(object sender, EventArgs e)
        {
            ComponentResourceManager resources = new ComponentResourceManager(typeof(Resources));
            this.simpleButtonNext.Image = ((Image)(resources.GetObject("pagination_next")));
        }

        private void simpleButtonEnd_MouseLeave(object sender, EventArgs e)
        {
            ComponentResourceManager resources = new ComponentResourceManager(typeof(Resources));
            this.simpleButtonEnd.Image = ((Image)(resources.GetObject("pagination_last")));
        }

    }
    
    public class ToolStripEx : ToolStrip
    {
        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            const int WM_MOUSEACTIVATE = 0x21;

            if (m.Msg == WM_MOUSEACTIVATE && this.CanFocus && !this.Focused)
                this.Focus();

            base.WndProc(ref m);
        }
    }
}
