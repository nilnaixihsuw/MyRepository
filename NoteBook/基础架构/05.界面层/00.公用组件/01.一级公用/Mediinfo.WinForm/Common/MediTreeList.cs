using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Mediinfo.WinForm
{
    /// <summary>
    /// 将数据显示为多列树视图。 可以在绑定或非绑定模式下使用，完全支持数据编辑，验证，摘要等
    /// </summary>
    [ToolboxItem(true)]
    public partial class MediTreeList : TreeList
    {
        /// <summary>
        /// 开发者帮助接口
        /// </summary>
        [Browsable(false)]
        public IDeveloperHelper developerHelper { get; set; }

        public MediTreeList()
        {
            InitializeComponent();

            if (!ControlCommonHelper.IsDesignMode())
            {
                if (developerHelper == null)
                {
                    developerHelper = new SystemInfoHelper();
                }
                developerHelper.DealRelativeControl(this);
            }
            this.MouseDown -= MediTreeList_MouseDown;
            this.MouseDown += MediTreeList_MouseDown;
        }

        /// <summary>
        /// 是否启用右键聚焦
        /// </summary>
        [Browsable(true), DefaultValue(false), Description("是否启用右键聚焦")]
        public bool FocusedByRight { get; set; }

        private void MediTreeList_MouseDown(object sender, MouseEventArgs e)
        {
            if (FocusedByRight)
            {
                if (e.Button == MouseButtons.Right)
                {
                    TreeList treeList = sender as TreeList;
                    TreeListHitInfo info = treeList.CalcHitInfo(e.Location);
                    if (info.Node != null)
                    {
                        this.FocusedNode = info.Node;
                    }
                }
            }
        }

        private void InitializeComponent()
        {
            // 
            // MediTreeList
            // 
            this.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(163)))), ((int)(((byte)(229)))));
            this.Appearance.FocusedRow.ForeColor = System.Drawing.Color.White;
            this.Appearance.FocusedRow.Options.UseBackColor = true;
            this.Appearance.FocusedRow.Options.UseForeColor = true;
            this.Appearance.SelectedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(163)))), ((int)(((byte)(229)))));
            this.Appearance.SelectedRow.ForeColor = System.Drawing.Color.White;
            this.Appearance.SelectedRow.Options.UseBackColor = true;
            this.Appearance.SelectedRow.Options.UseForeColor = true;
            this.OptionsBehavior.Editable = false;
            this.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.OptionsView.FocusRectStyle = DevExpress.XtraTreeList.DrawFocusRectStyle.None;
            this.OptionsView.ShowColumns = false;
            this.OptionsView.ShowHorzLines = false;
            this.OptionsView.ShowIndicator = false;
            this.OptionsView.ShowVertLines = false;
            this.Cursor = System.Windows.Forms.Cursors.Hand;

        }

        public MediTreeList(IContainer container)
        {
            container.Add(this);
            InitializeComponent();
            if (developerHelper == null)
            {
                developerHelper = new SystemInfoHelper();
            }
            developerHelper.DealRelativeControl(this);
            this.MouseDown -= MediTreeList_MouseDown;
            this.MouseDown += MediTreeList_MouseDown;
        }

        /// <summary>
        /// 加载图片
        /// </summary>
        public void LoadTreeListItemsIco()
        {
            this.BeginUpdate();
            try
            {
                //  this.GetSelectImage += MediTreeList_GetSelectImage;
                ImageList imageList2 = new ImageList();
                string sPath = AppDomain.CurrentDomain.BaseDirectory;
                Image img1 = ((System.Drawing.Image)(ResourceImage.ico_folderClose));// Image.FromFile(sPath + @"image\folderClose.png");
                Image img2 = ((System.Drawing.Image)(ResourceImage.ico_folderOpen));// Image.FromFile(sPath + @"image\folderOpen.png");
                Image img3 = ((System.Drawing.Image)(ResourceImage.ico_document));// Image.FromFile(sPath + @"image\document.png");
                imageList2.Images.Add("folderClose.png", img1);
                imageList2.Images.Add("folderOpen.png", img2);
                imageList2.Images.Add("document.png", img3);
                this.SelectImageList = imageList2;
                imageList2.TransparentColor = System.Drawing.Color.Transparent;
                imageList2.Images.SetKeyName(0, "folderClose.png");
                imageList2.Images.SetKeyName(1, "folderOpen.png");
                imageList2.Images.SetKeyName(2, "document.png");
            }
            finally
            {
                this.EndUpdate();
            }
        }

        protected override void RaiseGetSelectImage(TreeListNode node, bool focused, ref int nodeImageIndex)
        {
            if (node.HasChildren)
                nodeImageIndex = node.Expanded ? 1 : 0;
            else
                nodeImageIndex = 2;
            base.RaiseGetSelectImage(node, focused, ref nodeImageIndex);
        }

        /// <summary>
        /// 选择状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MediTreeList_GetSelectImage(object sender, DevExpress.XtraTreeList.GetSelectImageEventArgs e)
        {
            this.BeginUpdate();
            try
            {
                if (e.Node.HasChildren)
                    e.NodeImageIndex = e.Node.Expanded ? 1 : 0;
                else
                    e.NodeImageIndex = 2;
            }
            finally
            {
                this.EndUpdate();
            }
        }

        /// <summary>
        /// 允许您将状态图像分配给节点。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MediTreeList_GetStateImage(object sender, DevExpress.XtraTreeList.GetStateImageEventArgs e)
        {
            this.BeginUpdate();
            try
            {
                // 获取或设置一个值，指示一个节点是否有子节点。
                if (e.Node.HasChildren)
                    e.NodeImageIndex = e.Node.Expanded ? 1 : 0;
                else e.NodeImageIndex = 2;
            }
            finally
            {
                this.EndUpdate();
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="node"></param>
        /// <param name="nodeHeight"></param>
        protected override void RaiseCalcNodeHeight(TreeListNode node, ref int nodeHeight)
        {
            nodeHeight += rowSpace;
            base.RaiseCalcNodeHeight(node, ref nodeHeight);
        }

        private void MediTreeList_CalcNodeHeight(object sender, CalcNodeHeightEventArgs e)
        {
            this.BeginUpdate();
            try
            {
                if (rowSpace > 0)
                    e.NodeHeight += rowSpace;
            }
            finally
            {
                this.EndUpdate();
            }
        }

        private int rowSpace = 4;

        [Browsable(true)]
        [Category("自定义")]
        [Description("获取或设置行与行之间的间距，间距大于0的时候AutoNodeHeight自动设置为false")]
        public int RowSpace
        {
            get { return rowSpace; }
            set { if (rowSpace > 0) this.OptionsBehavior.AutoNodeHeight = false; rowSpace = value; }
        }
    }
}