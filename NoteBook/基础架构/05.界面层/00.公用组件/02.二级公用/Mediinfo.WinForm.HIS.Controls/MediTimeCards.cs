using Mediinfo.WinForm.HIS.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Mediinfo.WinForm.HIS.Controls
{
    /// <summary>
    /// 时间卡片控件
    /// </summary>
    [ToolboxItem(true)]
    public class MediTimeCards : FlowLayoutPanel
    {
        #region constructor

        public MediTimeCards()
        {
            this.DoubleBuffered = true;
            this.ResizeRedraw = true;
        }

        #endregion

        #region fields

        private List<MediTimeCard> cards = null;    // 卡片集合
        private int firstClickTicks = -1;
        private List<int> selectIndex = new List<int>();
        private List<StartEndTime> enabledTimes = null;     // 禁用时间集合

        #endregion

        #region methods
        
        /// <summary>
        /// 设置显示卡片的时间段
        /// </summary>
        /// <param name="dt">显示时间段</param>
        public void SetTime(StartEndTime dt)
        {
            if (cards == null)
                cards = new List<MediTimeCard>();
            cards.Clear();
            this.Controls.Clear();

            // 计算时间差,获取总分钟数
            TimeSpan tsStart = new TimeSpan(dt.StartTime.Ticks);
            TimeSpan tsEnd = new TimeSpan(dt.EndTime.Ticks);
            TimeSpan ts = tsStart.Subtract(tsEnd).Duration();
            // 计算显示卡片数
            int cardCount = (int)(ts.TotalMinutes / 30);

            for (int i = 0; i < cardCount; i++)
            {
                MediTimeCard card = new MediTimeCard();
                card.CardID = i;
                card.StartTime = dt.StartTime.AddMinutes(i * 30);
                card.EndTime = dt.StartTime.AddMinutes((i + 1) * 30);
                card.MouseDown += Card_MouseDown;

                AddCard(card);
            }

            InitCard();
        }

        /// <summary>
        /// 设置禁用时间段
        /// </summary>
        /// <param name="list">禁用时间段集合</param>
        public void SetEnabledTime(List<StartEndTime> list)
        {
            enabledTimes = list;

            if (cards != null)
            {
                ResetCard();

                for (int i = 0; i < cards.Count; i++)
                {
                    MediTimeCard card = cards[i];
                    card.IsEnabled = IsEnabledCard(list, card.StartTime, card.EndTime);
                }
            }
        }

        /// <summary>
        /// 获取选中时间段
        /// </summary>
        /// <returns></returns>
        public StartEndTime GetTime()
        {
            StartEndTime dt = new StartEndTime();
            if (selectIndex.Count > 0)
            {
                int startCardID = selectIndex[0];
                int endCardID = selectIndex[selectIndex.Count - 1];
                if (cards != null)
                {
                    MediTimeCard startCard = cards.Find(p => p.CardID == startCardID);
                    if (startCard != null)
                        dt.StartTime = startCard.StartTime;

                    MediTimeCard endCard = cards.Find(p => p.CardID == endCardID);
                    if (endCard != null)
                        dt.EndTime = endCard.EndTime;
                }
            }
            return dt;
        }

        /// <summary>
        /// 添加事件卡片
        /// </summary>
        /// <param name="card"></param>
        private void AddCard(MediTimeCard card)
        {
            if (cards == null)
                cards = new List<MediTimeCard>();
            cards.Add(card);

            this.Controls.Add(card);
        }

        /// <summary>
        /// 初始化卡片
        /// </summary>
        private void InitCard()
        {
            if (cards != null)
            {
                for (int i = 0; i < cards.Count; i++)
                {
                    MediTimeCard card = cards[i];
                    card.VisibleBorders = AnchorStyles.None;

                    int columnCount = this.Width / card.Width;      // 每行显示卡片数

                    card.VisibleBorders = AnchorStyles.Right | AnchorStyles.Bottom;

                    if (i < columnCount)
                        card.VisibleBorders = AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Top;

                    if (i % columnCount == 0)
                    {
                        if (i == 0)
                            card.VisibleBorders = AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Top;
                        else
                            card.VisibleBorders = AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Left;
                    }
                }
            }
        }

        /// <summary>
        /// 判断卡片是否禁用
        /// </summary>
        /// <param name="list">禁用时间集合</param>
        /// <param name="dtStart">开始时间</param>
        /// <param name="dtEnd">结束时间</param>
        /// <returns></returns>
        private bool IsEnabledCard(List<StartEndTime> list, DateTime dtStart, DateTime dtEnd)
        {
            bool result = false;
            if (list != null)
            {
                foreach (StartEndTime dt in list)
                {
                    if (dtStart >= dt.StartTime && dtEnd <= dt.EndTime)
                        result = true;
                }
            }
            return result;
        }

        /// <summary>
        /// 选择更多
        /// </summary>
        /// <param name="selectIndex">卡片ID集合</param>
        /// <param name="list">禁止时间集合</param>
        private void CheckedMore(List<int> selectIndex, List<StartEndTime> list)
        {
            foreach (int cardID in selectIndex)
            {
                if (cards != null)
                {
                    MediTimeCard card = cards.Find(p => p.CardID == cardID);
                    if (card != null)
                    {
                        if (enabledTimes != null)
                        {
                            bool result = IsEnabledCard(enabledTimes, card.StartTime, card.EndTime);
                            if (result)
                            {
                                MediMsgBox.Warn("选择时间已占用");
                                ClearSelectExists(firstClickTicks);
                                selectIndex.Clear();
                                return;
                            }
                            else
                                card.IsChecked = true;
                        }
                        else
                            card.IsChecked = true;
                    }
                }
            }
        }

        /// <summary>
        /// 清楚选中项
        /// </summary>
        /// <param name="carID">排除项</param>
        private void ClearSelectExists(int carID)
        {
            if (cards != null)
            {
                for (int i = 0; i < cards.Count; i++)
                {
                    MediTimeCard card = cards[i];
                    if (card.CardID == carID)
                        card.IsChecked = true;
                    else
                        card.IsChecked = false;
                }
            }
        }

        /// <summary>
        /// 重置卡片
        /// </summary>
        private void ResetCard()
        {
            selectIndex.Clear();
            if (cards != null)
            {
                for (int i = 0; i < cards.Count; i++)
                {
                    MediTimeCard card = cards[i];
                    card.IsEnabled = false;
                    card.IsChecked = false;
                }
            }
        }

        #endregion

        #region override

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);

            InitCard();
        }

        #endregion

        #region events

        /// <summary>
        /// 卡片点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Card_MouseDown(object sender, MouseEventArgs e)
        {
            MediTimeCard card = sender as MediTimeCard;
            card.IsChecked = !card.IsChecked;

            selectIndex.Clear();
            if (firstClickTicks == -1)
            {
                firstClickTicks = card.CardID;
                ClearSelectExists(firstClickTicks);
                selectIndex.Add(card.CardID);
            }
            else
            {
                if (firstClickTicks != card.CardID)
                {
                    if (card.CardID > firstClickTicks)
                    {
                        for (int i = firstClickTicks; i <= card.CardID; i++)
                            selectIndex.Add(i);
                    }
                    else
                    {
                        for (int i = card.CardID; i <= firstClickTicks; i++)
                            selectIndex.Add(i);
                    }
                }
                firstClickTicks = -1;
            }

            CheckedMore(selectIndex, enabledTimes);
        }

        #endregion
    }

    /// <summary>
    /// 单个时间卡片
    /// </summary>
    public class MediTimeCard : Panel
    {
        #region constructor

        public MediTimeCard()
        {
            this.DoubleBuffered = true;
            this.ResizeRedraw = true;
            this.BorderStyle = BorderStyle.None;
            this.Margin = new Padding(0);
            this.Width = 75;
            this.Height = 50;
        }

        #endregion

        #region fields

        private int cardID = -1;
        private bool isEnabled = false;
        private bool isChecked = false;
        private int borderWidth = 1;
        private Color borderColor = Color.FromArgb(218, 235, 246);
        private Color enabledBackColor = Color.FromArgb(238, 238, 238);
        private Color activeBackColor = Color.FromArgb(215, 234, 246);
        private AnchorStyles visibleBorders = AnchorStyles.None;

        #endregion

        #region properties

        /// <summary>
        /// 卡片ID
        /// </summary>
        public int CardID
        {
            get { return this.cardID; }
            set { cardID = value; }
        }

        /// <summary>
        /// 卡片是否禁用
        /// </summary>
        public bool IsEnabled
        {
            get { return this.isEnabled; }
            set
            {
                if (this.isEnabled != value)
                {
                    this.isEnabled = value;
                    this.Invalidate();
                }
            }
        }

        /// <summary>
        /// 卡片是否被选中
        /// </summary>
        public bool IsChecked
        {
            get { return this.isChecked; }
            set
            {
                if (this.isChecked != value)
                {
                    isChecked = value;
                    this.Invalidate();
                }
            }
        }

        /// <summary>
        /// 卡片边框的大小
        /// </summary>
        public int BorderWidth
        {
            get { return this.borderWidth; }
            set
            {
                if (this.borderWidth != value)
                {
                    this.borderWidth = value;
                    this.Invalidate();
                }
            }
        }

        /// <summary>
        /// 卡片边框颜色
        /// </summary>
        public Color BorderColor
        {
            get { return this.borderColor; }
            set
            {
                if (this.borderColor != value)
                {
                    this.borderColor = value;
                    this.Invalidate();
                }
            }
        }

        /// <summary>
        /// 卡片被禁用时的背景色
        /// </summary>
        public Color EnabledBackColor
        {
            get { return this.enabledBackColor; }
            set
            {
                if (this.enabledBackColor != value)
                {
                    this.enabledBackColor = value;
                    this.Invalidate();
                }
            }
        }

        /// <summary>
        /// 卡片被选中时的背景色
        /// </summary>
        public Color ActiveBackColor
        {
            get { return this.activeBackColor; }
            set
            {
                if (this.activeBackColor != value)
                {
                    this.activeBackColor = value;
                    this.Invalidate();
                }
            }
        }

        /// <summary>
        /// 卡片边框显示
        /// </summary>
        public AnchorStyles VisibleBorders
        {
            get { return this.visibleBorders; }
            set
            {
                if (this.visibleBorders != value)
                {
                    this.visibleBorders = value;
                    this.Invalidate();
                }
            }
        }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }

        #endregion

        #region override

        protected override void OnPaint(PaintEventArgs e)
        {
            Rectangle rect = e.ClipRectangle;

            // 绘画边框
            if (this.borderWidth > 0 && this.visibleBorders != AnchorStyles.None)
            {
                Pen pen = new Pen(this.borderColor, (float)this.borderWidth);
                if ((this.visibleBorders & AnchorStyles.Left) == AnchorStyles.Left)
                {
                    e.Graphics.DrawLine(pen, 0, 0, 0, this.Height - 1);

                    rect.X += this.borderWidth;
                    rect.Width -= this.borderWidth;
                }
                if ((this.visibleBorders & AnchorStyles.Top) == AnchorStyles.Top)
                {
                    e.Graphics.DrawLine(pen, 0, 0, this.Width - 1, 0);

                    rect.Y += this.borderWidth;
                    rect.Height -= this.borderWidth;
                }
                if ((this.visibleBorders & AnchorStyles.Right) == AnchorStyles.Right)
                {
                    e.Graphics.DrawLine(pen, this.Width - 1, 0, this.Width - 1, this.Height - 1);

                    rect.Width -= this.borderWidth;
                }
                if ((this.visibleBorders & AnchorStyles.Bottom) == AnchorStyles.Bottom)
                {
                    e.Graphics.DrawLine(pen, 0, this.Height - 1, this.Width - 1, this.Height - 1);

                    rect.Height -= this.borderWidth;
                }
                pen.Dispose();
            }

            // 卡片被禁用时的背景色
            if (this.IsEnabled)
            {
                e.Graphics.FillRectangle(new SolidBrush(enabledBackColor), rect);
            }

            // 卡片被选中时的背景色
            if (this.IsChecked)
            {
                if (!this.IsEnabled)
                {
                    e.Graphics.FillRectangle(new SolidBrush(activeBackColor), rect);
                }
            }

            // 文字信息(开始时间、结束时间)
            using (Brush brush = new SolidBrush(Color.Black))
            {
                string txtStart = StartTime.ToString("HH:mm");
                e.Graphics.DrawString(txtStart, new Font("微软雅黑", 9.5F), brush, new Point(rect.X + 2, rect.Y + 2));

                string txtEnd = EndTime.ToString("HH:mm");
                Graphics graphics = e.Graphics;
                Font font = new Font("微软雅黑", 9F);
                SizeF size = graphics.MeasureString(txtEnd, font);
                e.Graphics.DrawString(txtEnd, font, brush, new Point(rect.Width - (int)size.Width - 1, rect.Height - (int)size.Height - 1));
            }

            base.OnPaint(e);
        }

        #endregion
    }

    /// <summary>
    /// 起始与结束时间
    /// </summary>
    public class StartEndTime
    {
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }
    }
}
