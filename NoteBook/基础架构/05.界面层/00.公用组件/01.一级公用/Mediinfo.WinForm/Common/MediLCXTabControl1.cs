using DevExpress.Skins;
using DevExpress.XtraTab;
using DevExpress.XtraTab.Registrator;
using DevExpress.XtraTab.ViewInfo;
using Mediinfo.WinForm;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace WindowsFormsApplication4
{
    [ToolboxItem(true)]
    public class MediLCXTabControl : XtraTabControl
    {
        private int pageGroupDistance;
        private List<XtraTabPage> newGroupPages = new List<XtraTabPage>();
        private Skin customSkin = TabSkins.GetSkin(DevExpress.LookAndFeel.UserLookAndFeel.Default.ActiveLookAndFeel);
        private SkinElement element;
        private List<DevExpress.XtraTab.XtraTabPage> groupPages = new List<DevExpress.XtraTab.XtraTabPage>();

        /// <summary>
        /// 临床控件
        /// </summary>
        public MediLCXTabControl() : base()
        {
            //SetStyle(ControlStyles.DoubleBuffer |
            //        ControlStyles.UserPaint |
            //        ControlStyles.AllPaintingInWmPaint |
            //        ControlStyles.ResizeRedraw |
            //        ControlStyles.StandardDoubleClick |
            //        ControlStyles.UserMouse |
            //        ControlStyles.StandardClick |
            //        ControlStyles.SupportsTransparentBackColor,
            //        true);
            this.Size = new System.Drawing.Size(200, 200);
            this.ClosePageButtonShowMode = DevExpress.XtraTab.ClosePageButtonShowMode.InActiveTabPageHeader;
            element = customSkin[TabSkins.SkinTabHeader];
            element.Properties["AllowTouch"] = false;
            element.ContentMargins.Top = 14;
            element.ContentMargins.Bottom = 13;
        }

        protected Rectangle MoveRectangle(Rectangle rect, int amount)
        {
            return new Rectangle(rect.Left, rect.Top + amount, rect.Width, rect.Height);
        }

        public override Rectangle DisplayRectangle
        {
            get
            {
                Rectangle rect = base.DisplayRectangle;
                return new Rectangle(0, rect.Top + 0, rect.Width + 6, rect.Height + 5);
            }
        }

        protected override void OnCloseButtonClick(object sender, EventArgs e)
        {
            ClosePageButtonEventArgs arg = e as ClosePageButtonEventArgs;
            (arg.Page as XtraTabPage).PageVisible = false;
            base.OnCloseButtonClick(sender, e);
        }

        [DefaultValue(50)]
        public int PageGroupDistance
        {
            get { return pageGroupDistance; }
            set
            {
                if (pageGroupDistance != value)
                {
                    pageGroupDistance = value > 0 ? value : 0;

                    LayoutChanged();
                }
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<XtraTabPage> NewGroupPages
        {
            get
            {
                return newGroupPages;
            }
            set
            {
                if (newGroupPages != value)
                {
                    newGroupPages = value;
                    LayoutChanged();
                }
            }
        }

        protected override void CheckInfo()
        {
            FieldInfo fi = typeof(XtraTabControl).GetField("view", BindingFlags.Instance | BindingFlags.NonPublic);
            fi.SetValue(this, new MySkinViewInfoRegistrator());
            CreateView();
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
            //Graphics g = e.Graphics;
            //g.FillRectangle(new SolidBrush(Color.DodgerBlue), 0, 0, Width, Height);
        }

        private Hashtable ht = new Hashtable();

        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);
            if (!ControlCommonHelper.IsDesignMode())
            {
                AutoSelectedHeight();
            }
          
        }

        private void AutoSelectedHeight()
        {
            int index = 0;
            foreach (BaseTabPageViewInfo btpvi in ViewInfo.HeaderInfo.AllPages)
            {
                try
                {
                    if (ht.Contains(btpvi)) continue;
                    if (index == SelectedTabPageIndex)
                    {
                        ht.Add(btpvi, btpvi);
                        btpvi.Bounds = MoveRectangle(btpvi.Bounds, 2);
                        btpvi.Clip = MoveRectangle(btpvi.Clip, 2);
                        btpvi.Content = MoveRectangle(btpvi.Content, 2);
                        btpvi.Focus = MoveRectangle(btpvi.Focus, 2);
                        btpvi.Image = MoveRectangle(btpvi.Image, 2);
                        btpvi.Text = MoveRectangle(btpvi.Text, 2);
                    }
                }
                finally
                {
                    index++;
                }
            }
        }

        public class MySkinTabHeaderViewInfo : SkinTabHeaderViewInfo
        {
            public MySkinTabHeaderViewInfo(BaseTabControlViewInfo viewInfo) : base(viewInfo)
            {
            } // ctor

            private int PageGroupDistance { get { return ((MediLCXTabControl)TabControl).PageGroupDistance; } }

            private bool IsPageInNewGroupPages(XtraTabPage page)
            {
                List<XtraTabPage> NewGroupPages = ((MediLCXTabControl)TabControl).NewGroupPages;
                return NewGroupPages.Contains(page);
            }

            protected override void CalcPageViewInfo(BaseTabRowViewInfo row, BaseTabPageViewInfo info, ref Point topLeft)
            {
                if (IsPageInNewGroupPages((XtraTabPage)info.Page))
                {
                    if (HeaderLocation == TabHeaderLocation.Bottom
                        || HeaderLocation == TabHeaderLocation.Top)
                        topLeft = new Point(topLeft.X + PageGroupDistance, topLeft.Y);
                    else
                        topLeft = new Point(topLeft.X, topLeft.Y + PageGroupDistance);

                    base.CalcPageViewInfo(row, info, ref topLeft);
                }
                else
                {
                    base.CalcPageViewInfo(row, info, ref topLeft);
                }

                return;
            }
        }

        public class MySkinViewInfoRegistrator : SkinViewInfoRegistrator
        {
            public MySkinViewInfoRegistrator() : base()
            {
            }

            public override BaseTabHeaderViewInfo CreateHeaderViewInfo(BaseTabControlViewInfo viewInfo)
            {
                return new MySkinTabHeaderViewInfo(viewInfo);
            }
        }
    }
}