using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Scrolling;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Layout;
using DevExpress.XtraGrid.Views.Layout.Modes;
using DevExpress.XtraGrid.Views.Layout.ViewInfo;
using DevExpress.XtraLayout;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Mediinfo.WinForm.HIS.Controls
{
    public class MediLayoutView : DevExpress.XtraGrid.Views.Layout.LayoutView
    {
        #region constructor

        /// <summary>
        /// 开发者帮助接口
        /// </summary>
        [Browsable(false)]
        public IDeveloperHelper developerHelper { get; set; }

        protected override string ViewName => "MediLayoutView";
        public MediLayoutView() : base()
        {
            this.OptionsView.AllowBorderColorBlending = true;
            this.CardVertInterval = 20;
            RegisterEvents();
            if (!SkinCat.Instance.IsDesignMode)
            {
                if (developerHelper == null)
                {
                    developerHelper = new SystemInfoHelper();
                }
                developerHelper.DealRelativeControl(this);
                ele = currentSkin[skinElementName];
            }
        }

        public MediLayoutView(GridControl ownerGrid) : base(ownerGrid)
        {
            this.OptionsView.AllowBorderColorBlending = true;
            RegisterEvents();
            if (!SkinCat.Instance.IsDesignMode)
            {
                if (developerHelper == null)
                {
                    developerHelper = new SystemInfoHelper();
                }
                developerHelper.DealRelativeControl(this);
                ele = currentSkin[skinElementName];
            }
        }

        #endregion

        #region fields

        private string skinElementName = "TextBox";
        private DevExpress.Skins.SkinElement ele;
        private Color activeBackColor = Color.White;
        //背景色
        private Color nativeBackColor = Color.White;
        private Color borderColor = Color.FromArgb(221, 221, 221);
        private Color activeBorderColor = Color.FromArgb(79, 149, 179);
        private bool showCarBorder = true;
        private bool locked = false;
        private bool forward = false;
        private int visibleIndex = -1;

        //从皮肤中颜色信息
        DevExpress.Skins.Skin currentSkin = ControlCommonHelper.GetSkinInstance();

        #endregion

        #region properties

        /// <summary>
        /// 选中时卡片背景色
        /// </summary>
        public Color ActiveBackColor
        {
            get { return activeBackColor; }
            set
            {
                if (activeBackColor != value)
                {
                    activeBackColor = value;
                }
            }
        }
        /// <summary>
        /// 选中时卡片边框颜色
        /// </summary>
        public Color ActiveBorderBackColor
        {
            get { return activeBorderColor; }
            set
            {
                if (activeBorderColor != value)
                {
                    activeBorderColor = value;
                }
            }
        }
        /// <summary>
        /// 全部卡片背景色
        /// </summary>
        public Color NativeBackColor
        {
            get { return nativeBackColor; }
            set
            {
                if (nativeBackColor != value)
                {
                    nativeBackColor = value;
                }
            }
        }

        /// <summary>
        /// 是否显示卡片边框
        /// </summary>
        [Browsable(true)]
        [DefaultValue(true)]
        [Description("是否显示卡片边框")]
        public bool ShowCarBorder
        {
            get { return showCarBorder; }
            set { showCarBorder = value; }
        }

        #endregion

        #region methods

        private void RegisterEvents()
        {
            this.CustomCardStyle -= MediLayoutView_CustomCardStyle;
            this.CustomCardStyle += MediLayoutView_CustomCardStyle;
            this.CustomDrawCardBackground += MediLayoutView_CustomDrawCardBackground;
            this.MouseWheel -= MediLayoutView_MouseWheel;
            this.MouseWheel += MediLayoutView_MouseWheel;
            this.VisibleRecordIndexChanged += MediLayoutView_VisibleRecordIndexChanged;
        }

        #endregion

        #region events

        private void MediLayoutView_CustomCardStyle(object sender, DevExpress.XtraGrid.Views.Layout.Events.LayoutViewCardStyleEventArgs e)
        {
            if (e.RowHandle == (sender as LayoutView).FocusedRowHandle)
            {
                if (ele != null && ele.Properties.GetColor("FocusedBorderColor") != null)
                {
                    e.Appearance.BorderColor = ele.Properties.GetColor("FocusedBorderColor");
                }
            }
        }
        private void MediLayoutView_MouseWheel(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            LockAutoPanByFocusedCard();
        }

        private void MediLayoutView_CustomDrawCardBackground(object sender, DevExpress.XtraGrid.Views.Layout.Events.LayoutViewCustomDrawCardBackgroundEventArgs e)
        {
            // 执行默认图
            e.DefaultDraw();

            if (e.IsFocused)
            {
                using (Brush brush = new SolidBrush(activeBackColor))
                {
                    if (showCarBorder)
                    {

                        // 卡片默认边框
                        Rectangle rect = e.Bounds;
                        int xPos = rect.X;
                        int yPos = rect.Y;
                        int width = rect.Width - 1;
                        int height = rect.Height - 1;

                        Pen pen = new Pen(activeBorderColor, 1F);
                        e.Graphics.DrawLine(pen, xPos, yPos, xPos, yPos + height);
                        e.Graphics.DrawLine(pen, xPos, yPos, xPos + width, yPos);
                        e.Graphics.DrawLine(pen, xPos + width, yPos, xPos + width, yPos + height);
                        e.Graphics.DrawLine(pen, xPos, yPos + height, xPos + width, yPos + height);
                        // 卡片颜色填充
                        e.Cache.FillRectangle(brush, Rectangle.Inflate(e.Bounds, -1, -1));
                    }
                    else
                    {
                        // 卡片颜色填充
                        e.Cache.FillRectangle(brush, e.Bounds);
                    }
                    return;
                }
            }
            else
            {
                // 如果设置了背景图片则不填充白色背景
                if (this.TemplateCard.CaptionImageOptions.Image != null)
                    return;

                using (Brush brush = new SolidBrush(nativeBackColor))
                {
                    if (showCarBorder)
                    {
                        // 卡片颜色填充
                        e.Cache.FillRectangle(brush, Rectangle.Inflate(e.Bounds, -1, -1));

                        // 卡片默认边框
                        Rectangle rect = e.Bounds;
                        int xPos = rect.X;
                        int yPos = rect.Y;
                        int width = rect.Width - 1;
                        int height = rect.Height - 1;

                        Pen pen = new Pen(borderColor, 1F);
                        e.Graphics.DrawLine(pen, xPos, yPos, xPos, yPos + height);
                        e.Graphics.DrawLine(pen, xPos, yPos, xPos + width, yPos);
                        e.Graphics.DrawLine(pen, xPos + width, yPos, xPos + width, yPos + height);
                        e.Graphics.DrawLine(pen, xPos, yPos + height, xPos + width, yPos + height);
                    }
                    else
                    {
                        // 卡片颜色填充
                        e.Cache.FillRectangle(brush, e.Bounds);
                    }
                }
            }
        }

        private void MediLayoutView_VisibleRecordIndexChanged(object sender, DevExpress.XtraGrid.Views.Layout.Events.LayoutViewVisibleRecordIndexChangedEventArgs e)
        {
            if (OptionsView.ViewMode == LayoutViewMode.MultiRow &&
                OptionsMultiRecordMode.MultiRowScrollBarOrientation == ScrollBarOrientation.Vertical)
            {
                int visibleCards = ViewInfo.VisibleCards.Count;
                if (visibleCards > 0)
                {
                    // 方式一：卡片数计算
                    //int firstRow = ViewInfo.VisibleCards[0].VisibleRow;
                    //int lastRow = ViewInfo.VisibleCards[visibleCards - 1].VisibleRow;
                    //int rowCount = lastRow - firstRow + 1;
                    //int itemsInRow = visibleCards / rowCount;

                    // 方式二：卡片宽度计算
                    LayoutViewCard card = ViewInfo.VisibleCards[0];
                    int cardWidth = card.Bounds.Width + CardHorzInterval;
                    if (cardWidth < 0) return;
                    int layoutViewWidth = ViewInfo.ClientBounds.Width;
                    int itemsInRow = layoutViewWidth / cardWidth;

                    if (locked) return;
                    locked = true;
                    forward = visibleIndex < VisibleRecordIndex ? true : false;
                    VisibleRecordIndex = e.PrevVisibleRecordIndex;
                    if (forward)
                        VisibleRecordIndex += itemsInRow;
                    else
                        VisibleRecordIndex -= itemsInRow;
                    locked = false;
                    visibleIndex = VisibleRecordIndex;
                }
            }
        }

        #endregion

        #region override

        protected override ScrollInfo CreateScrollInfo()
        {
            return new MediLayoutViewScrollInfo(this);
        }

        #endregion
    }

    public class MediLayoutViewScrollInfo: LayoutViewScrollInfo
    {
        public MediLayoutViewScrollInfo(BaseView view) : base(view) { }

       

        public override int VScrollSize { get { return SystemInformation.VerticalScrollBarWidth - 5; } }

        public override int HScrollSize { get { return SystemInformation.HorizontalScrollBarHeight; } }

        protected override void CalcRects()
        {
            base.CalcRects();

            var prop = typeof(ScrollInfo).GetField("vscrollRect", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

            var prop1 = typeof(ScrollInfo).GetField("hscrollRect", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            prop.SetValue(this as ScrollInfo, new Rectangle(new Point(VScrollRect.Location.X, VScrollRect.Location.Y), new Size(12, VScrollRect.Height)));

            prop1.SetValue(this as ScrollInfo, new Rectangle(new Point(HScrollRect.Location.X, HScrollRect.Location.Y), new Size(HScrollRect.Width, 12)));
        }
    }
    public class MediLayoutViewInfo : LayoutViewInfo
    {
        public MediLayoutViewInfo(LayoutView view) : base(view)
        {
            var prop = typeof(LayoutView).GetField("scrollInfo", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            scrollInfo = prop.GetValue(this.View as LayoutView) as MediGridviewScrollInfo;
        }

        MediGridviewScrollInfo scrollInfo = null;

        protected override Rectangle CalcTabClientRect()
        {
            var clientRect = base.CalcTabClientRect();
            clientRect.Height = clientRect.Height + 5;
            return clientRect;
        }


        protected override LayoutViewMultiRowMode CreateLayoutViewMultiRowMode()
        {
            return new MediLayoutViewMultiRowMode(this);
        }


    }
    public class MediLayoutViewMultiRowMode : LayoutViewMultiRowMode
    {

        public MediLayoutViewMultiRowMode(ILayoutViewInfo ownerView) : base(ownerView) { }

        protected override bool CalcCardIsPartiallyVisible(Rectangle rect, LayoutViewCard card)
        {
            if (ArrangeCache.Rows.Count == 1)
                return base.CalcCardIsPartiallyVisible(rect, card);
            return !rect.Contains(new Rectangle(card.Location, card.Size));
        }
        protected override void CollectAllCardsWhichMayBeVisible(int currentRecordIndex)
        {
            CollectAnyCards(currentRecordIndex);
        }
        protected override LayoutViewCard TryInsertCardInEmptySpaceAndCache(int recordIndex)
        {
            LayoutViewCard card = ViewInfo.InitializeCard(recordIndex);
            if (card != null)
            {
                List<LayoutViewCard> cards = MakeListToArrange(recordIndex, card);
                bool fCanInsertCard = (cards.Count == TryArrangeList(cards)) && CheckMaxCardRows();
                if (fCanInsertCard)
                {
                    ArrangeCache.Cache(card, recordIndex);
                    return card;
                }
                else AddCardToCache(card);
            }
            return null;
        }
        protected override int TryArrangeList(List<LayoutViewCard> list)
        {
            ArrangeCache.PreArrangeReset();
            if (list.Count == 0) return 0;
            int iArrangedCount = 0;
            int iRowCount = 0;
            Size viewSize = ViewInfo.ViewRects.CardsRect.Size;
            Size currentRowSize = Size.Empty;
            if (list.Count == 1)
            {
                (typeof(LayoutViewCard).GetProperty("IsPartiallyVisible", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance)).SetValue(list[0], false, null);
                DoPreArrangeRowCard(list[0], 0, 0, list[0].Size);
                return 1;
            }
            bool allCardsInView = AllCardsInView(list);
            foreach (LayoutViewCard card in list)
            {
                bool isBoundCard = IsBoundCard(card, allCardsInView);
                (typeof(LayoutViewCard).GetProperty("IsPartiallyVisible", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance)).SetValue(card, false, null);
                if (!card.Expanded) card.Width = Math.Max(card.MinSize.Width, card.Width);
                Size cardCellSize = card.Size + new Size(MinArrangeHSpacing + SeparatorWidth, MinArrangeVSpacing + SeparatorWidth);
                if (!card.Expanded)
                {
                    Size defSize = GetCollapsedCardSizeDefault();
                    cardCellSize = new Size(card.Size.Width, defSize.Height) + new Size(MinArrangeHSpacing, MinArrangeVSpacing);
                }
                Size availSize = new Size(viewSize.Width - currentRowSize.Width, viewSize.Height);
                bool fCanInsertInCurrentRow = CanInsertCardInRow(card, isBoundCard, iRowCount, cardCellSize, availSize);
                if (fCanInsertInCurrentRow && CheckMaxCardCountInRow(iRowCount))
                {
                    currentRowSize.Width += cardCellSize.Width;
                    currentRowSize.Height = Math.Max(currentRowSize.Height, cardCellSize.Height);
                    (typeof(LayoutViewCard).GetProperty("IsPartiallyVisible", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance)).SetValue(card, viewSize.Width < currentRowSize.Width, null);
                    DoPreArrangeRowCard(card, iRowCount, iArrangedCount++, currentRowSize);
                }
                else
                {
                    Size nextRowSize = new Size(viewSize.Width, viewSize.Height - currentRowSize.Height);
                    bool fCanInsertInNextRow = CanInsertCardInRow(card, isBoundCard, iRowCount + 1, cardCellSize, nextRowSize);
                    if (fCanInsertInNextRow)
                    {
                        iRowCount++;
                        currentRowSize = cardCellSize;
                        (typeof(LayoutViewCard).GetProperty("IsPartiallyVisible", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance)).SetValue(card, false, null);
                        DoPreArrangeRowCard(card, iRowCount, iArrangedCount++, currentRowSize);
                        viewSize.Height = nextRowSize.Height;
                    }
                }

            }

            return iArrangedCount;
        }

        private void DoPreArrangeRowCard(LayoutViewCard card, int iRowCount, int index, Size curRowSize)
        {
            if (iRowCount == ArrangeCache.Rows.Count) ArrangeCache.Rows.Add(new RowInfo(curRowSize));
            ArrangeCache.Rows[iRowCount].Cards.Add(card);
            ArrangeCache.Rows[iRowCount].RowSize = curRowSize;
            ArrangeCache.Rows[iRowCount].iMaxCardHeight = Math.Max(ArrangeCache.Rows[iRowCount].iMaxCardHeight, card.Size.Height);
        }
        private bool CanInsertCardInRow(LayoutViewCard card, bool isBoundCard, int iRow, Size cardCellSize, Size availRowSize)
        {
            bool canInsert = false;

            int spacing = MinArrangeHSpacing + SeparatorWidth;
            if (UseWholeCards)
            {
                canInsert = CheckDim(cardCellSize.Width - spacing, availRowSize.Width, spacing);
                if (iRow != 0) canInsert &= (availRowSize.Height - cardCellSize.Width - spacing > 0);
            }
            else
            {
                canInsert = availRowSize.Width - cardCellSize.Width > 0;
                if (iRow != 0) canInsert &= (availRowSize.Height - MinVisibleCardThickness > 0);
            }
            return canInsert;
        }

        protected override bool CheckMaxCardCountInRow(int currentRow)
        {
            bool result = true;
            if (MaxCardColumns > 0)
            {
                if (ArrangeCache.Rows.Count > currentRow)
                {
                    result &= MaxCardColumns > ArrangeCache.Rows[currentRow].Cards.Count;
                }
            }
            return result;
        }
    }
}