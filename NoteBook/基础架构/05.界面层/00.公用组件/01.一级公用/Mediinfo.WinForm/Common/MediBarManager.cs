using DevExpress.Utils.Drawing;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Painters;
using DevExpress.XtraBars.Styles;
using DevExpress.XtraBars.ViewInfo;

using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;

namespace Mediinfo.WinForm
{
    /// <summary>
    /// MediBarManager组件管理传统的条形图和弹出菜单。它是菜单、工具条，控件的容器。对于需要放导航栏和菜单的窗体，应添加BarManager组件。
    /// MediBarManager提供自定义窗口，允许您在运行时或设计时可视地自定义条。要在运行时激活自定义窗口，请点击右上角弹出窗中的Customize属性。除了创建和管理条形及其项目之外，BarManager组件还为用户提供了几个重要功能。 
    /// BarManager组件提供了用于保存和恢复工具条布局的工具。要保存布局，请使用SaveToRegistry方法和RestoreFromRegistry方法来恢复布局。在保存或加载之前，请确保指定了RegistryPath属性。
    /// </summary>
    [ToolboxItem(true)]
    public class MediBarManager : BarManager
    {
        /// <summary>
        /// 开发者帮助接口:主要用于开发者帮主窗体
        /// </summary>
        [Browsable(false)]
        public IDeveloperHelper developerHelper { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public MediBarManager()
        {
            RegisterEvents();

            if (!ControlCommonHelper.IsDesignMode())
            {
                if (developerHelper == null)
                {
                    developerHelper = new SystemInfoHelper();
                }
                developerHelper.DealRelativeControl(this);
            }
        }

        public MediBarManager(IContainer container) : base(container)
        {
            RegisterEvents();
        }

        #region methods

        /// <summary>
        /// 注册事件
        /// </summary>
        private void RegisterEvents()
        {
            this.HighlightedLinkChanged += MediBarManager_HighlightedLinkChanged;
        }

        #endregion

        #region events

        private void MediBarManager_HighlightedLinkChanged(object sender, HighlightedLinkChangedEventArgs e)
        {
            BarItemLink link = e.Link as BarItemLink;
            if (link != null)
            {
                link.Item.ItemInMenuAppearance.Normal.BackColor = Color.Empty;
                link.Item.ItemInMenuAppearance.Hovered.BackColor = Color.FromArgb(171, 214, 255);
            }
            BarItemLink prevLink = e.PrevLink as BarItemLink;
            if (prevLink != null)
            {
                prevLink.Item.ItemInMenuAppearance.Normal.BackColor = Color.Empty;
                prevLink.Item.ItemInMenuAppearance.Hovered.BackColor = Color.FromArgb(171, 214, 255);
            }
        }

        #endregion

        #region virtual

        protected virtual void UpdateBarItemInfo()
        {
            foreach (BarManagerPaintStyle ps in GetController().PaintStyles)
            {
                if (ps is SkinBarManagerPaintStyle)
                {

                    List<BarItemInfo> barlist = new List<BarItemInfo>();


                    foreach (BarItemInfo info in ps.ItemInfoCollection)
                        if (info.Name == "BarButtonItem")
                            barlist.Add(info);
                        else if (info.Name == "BarSubItem")
                            barlist.Add(info);
                    barlist.ForEach(o =>
                    {
                        ps.ItemInfoCollection.Remove(o);

                    });

                    ps.ItemInfoCollection.Add(new BarItemInfo("BarButtonItem", "Button", 0, typeof(BarButtonItem), typeof(BarButtonItemLink), typeof(MediBarButtonLinkViewInfo), new BarButtonLinkPainter(ps), true, true));
                    ps.ItemInfoCollection.Add(new BarItemInfo("BarSubItem", "Menu", 1, typeof(BarSubItem), typeof(BarSubItemLink), typeof(MediBarSubItemLinkViewInfo), new BarCustomContainerLinkPainter(ps), true, true));
                }
            }
        }

        #endregion

        #region override

        protected override void OnLoaded()
        {
            base.OnLoaded();
            UpdateBarItemInfo();
        }

        #endregion
    }


    public class MediBarSubItemLinkViewInfo : BarSubItemLinkViewInfo
    {
        public MediBarSubItemLinkViewInfo(BarDrawParameters parameters, BarItemLink link)
          : base(parameters, link)
        {

        }

        protected override void CalcInMenuViewInfoCore(GraphicsCache cache, object sourceObject, System.Drawing.Rectangle r)
        {
            base.CalcInMenuViewInfoCore(cache, sourceObject, r);

            Rectangle rect = Rects[BarLinkParts.Caption];
            rect.X = r.X + DrawParameters.Constants.SubMenuGlyphCaptionIndent;
            Rects[BarLinkParts.Caption] = rect;
            Rects[BarLinkParts.Glyph] = Rectangle.Empty;
        }
    }

    public class MediBarButtonLinkViewInfo : BarButtonLinkViewInfo
    {
        public MediBarButtonLinkViewInfo(BarDrawParameters parameters, BarItemLink link)
            : base(parameters, link)
        {

        }

        protected override void CalcInMenuViewInfoCore(GraphicsCache cache, object sourceObject, System.Drawing.Rectangle r)
        {
            base.CalcInMenuViewInfoCore(cache, sourceObject, r);

            Rectangle rect = Rects[BarLinkParts.Caption];
            rect.X = r.X + DrawParameters.Constants.SubMenuGlyphCaptionIndent;
            Rects[BarLinkParts.Caption] = rect;
            Rects[BarLinkParts.Glyph] = Rectangle.Empty;
        }
    }
}