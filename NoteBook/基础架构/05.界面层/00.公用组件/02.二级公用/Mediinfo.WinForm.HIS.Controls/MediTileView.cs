using DevExpress.XtraGrid;
using System;
using System.ComponentModel;

namespace Mediinfo.WinForm.HIS.Controls
{
    [Obsolete("该组件已过时,请使用MediTileView")]
    public class MediTitleView : DevExpress.XtraGrid.Views.Tile.TileView
    {
        protected override string ViewName => "MediTitleView";
        /// <summary>
        /// 开发者帮助接口
        /// </summary>
        [Browsable(false)]
        public IDeveloperHelper developerHelper { get; set; }
        public MediTitleView(GridControl ownerGrid) : base(ownerGrid)
        {
            if (!SkinCat.Instance.IsDesignMode)
            {
                if (developerHelper == null)
                {
                    developerHelper = new SystemInfoHelper();
                }
                developerHelper.DealRelativeControl(this);
            }
        }
    }
    public class MediTileView : DevExpress.XtraGrid.Views.Tile.TileView
    {
        public MediTileView(){}
        protected override string ViewName => "MediTileView";
        /// <summary>
        /// 开发者帮助接口
        /// </summary>
        [Browsable(false)]
        public IDeveloperHelper developerHelper { get; set; }
        public MediTileView(GridControl ownerGrid) : base(ownerGrid)
        {
            if (!SkinCat.Instance.IsDesignMode)
            {
                if (developerHelper == null)
                {
                    developerHelper = new SystemInfoHelper();
                }
                developerHelper.DealRelativeControl(this);
            }
        }
    }
}