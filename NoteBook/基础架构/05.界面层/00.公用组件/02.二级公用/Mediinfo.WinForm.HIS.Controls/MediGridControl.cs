using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Registrator;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Base.ViewInfo;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Layout;
using DevExpress.XtraGrid.Views.Layout.ViewInfo;
using System.ComponentModel;

namespace Mediinfo.WinForm.HIS.Controls
{
    [ToolboxItem(true)]
    public class MediGridControl : GridControl
    {
        /// <summary>
        /// 开发者帮助接口
        /// </summary>
        [Browsable(false)]
        public IDeveloperHelper developerHelper { get; set; }

        /// <summary>
        /// 控件的构造函数
        /// </summary>
        public MediGridControl()
        {
            InitControls();
            if (!SkinCat.Instance.IsDesignMode)
            {
                if (developerHelper == null)
                {
                    developerHelper = new SystemInfoHelper();
                }
                developerHelper.DealRelativeControl(this);
            }


        }

        private bool isLockException = false;

        [Browsable(true), DefaultValue(false)]
        public bool IsLockException
        {
            get
            {
                return isLockException;
            }
            set
            {
                isLockException = value;
                if (isLockException)
                    this.EditorHelper.LockHideException();
                else
                    this.EditorHelper.UnlockHideException();
            }
        }

        /// <summary>
        /// 初始化GridControl控件
        /// </summary>
        private void InitControls()
        {
            //this.Dock = DockStyle.Fill;

            //获取或设置嵌入的数据导航器是否可见。页脚
            this.UseEmbeddedNavigator = false;

            //this.MainView.BorderStyle = BorderStyles.NoBorder;

            //this.DataSourceChanged += MediGridControl_DataSourceChanged;
        }

        ///// <summary>
        ///// 数据源重写
        ///// </summary>
        //private object dataSource = null;
        //public override object DataSource
        //{
        //    get
        //    {
        //        return dataSource;
        //    }
        //    set
        //    {
        //        BeginUpdate();
        //        dataSource = value;
        //        EndUpdate();
        //    }
        //}

        #region 重写事件

        /// <summary>
        /// 重写创建默认视图
        /// </summary>
        /// <returns></returns>
        protected override BaseView CreateDefaultView()
        {
            return CreateView("MediGridView");
        }

        /// <summary>
        /// 注册有效的视图
        /// </summary>
        /// <param name="collection"></param>
        protected override void RegisterAvailableViewsCore(InfoCollection collection)
        {
            base.RegisterAvailableViewsCore(collection);
            //for (int i = 0; i < collection.Count; i++)
            //{
            //    if (!collection[i].ViewName.Contains("Medi"))
            //        collection.Remove(collection[i]);
            //}

            collection.Add(new MediGridViewInfoRegistrator());
            collection.Add(new MediAdvBandedGridViewRegistrator());
            collection.Add(new MediLayoutViewRegistrator());
            collection.Add(new MediTileViewRegistrator());
        }
        
        #endregion 重写事件
    }

    /// <summary>
    /// 注册视图
    /// </summary>
    public class MediGridViewInfoRegistrator : GridInfoRegistrator
    {
        public MediGridViewInfoRegistrator() : base()
        {

        }

        public override string ViewName => "MediGridView";
        
        public override BaseView CreateView(GridControl grid)
        {
            return new MediGridView(grid);
        }
        
        public override BaseViewInfo CreateViewInfo(BaseView view)
        {
            return new MediGridViewInfo(view as GridView);
        }
    }

    /// <summary>
    /// 注册AdvBanded视图
    /// </summary>
    public class MediAdvBandedGridViewRegistrator : AdvBandedGridInfoRegistrator
    {
        public override string ViewName => "MediAdvBandedGridView";

        public override BaseView CreateView(GridControl grid)
        {
            return new MediAdvBandedGridView(grid);
        }
    }

    /// <summary>
    /// 注册MediLayOutView视图
    /// </summary>
    public class MediLayoutViewRegistrator : LayoutViewInfoRegistrator
    {
        public MediLayoutViewRegistrator():base()
        {

        }
        public override string ViewName => "MediLayoutView";

        public override BaseView CreateView(GridControl grid)
        {
            return new MediLayoutView(grid);
        }
        
        public override BaseViewInfo CreateViewInfo(BaseView view)
        {
            return new MediLayoutViewInfo(view as LayoutView);
        }

        public override BaseViewPainter CreatePainter(BaseView view)
        {
            return new MediLayoutViewPainter(view);
        }
    }

    public class MediLayoutViewPainter: DevExpress.XtraGrid.Views.Layout.Drawing.LayoutViewPainter
    {
        public MediLayoutViewPainter(BaseView view) : base(view) { }

        protected override void DrawCard(LayoutViewDrawArgs e, LayoutViewCard card)
        {
            
            base.DrawCard(e, card);
           
        }
    }
    /// <summary>
    /// 注册TileView视图
    /// </summary>
    public class MediTileViewRegistrator : TileViewInfoRegistrator
    {
        public MediTileViewRegistrator():base()
        {

        }
        public override string ViewName => "MediTileView";

        public override BaseView CreateView(GridControl grid)
        {
            return new MediTileView(grid);
        }
    }
}