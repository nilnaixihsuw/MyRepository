namespace Mediinfo.WinForm.HIS.Controls
{
    partial class SwitchSystemForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.mediGridControl1 = new Mediinfo.WinForm.HIS.Controls.MediGridControl();
            this.eGYYONGHUYYEXBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.mediLayoutView1 = new Mediinfo.WinForm.HIS.Controls.MediLayoutView();
            this.layoutViewColumn1 = new DevExpress.XtraGrid.Columns.LayoutViewColumn();
            this.repositoryItemMediPictureEdit1 = new Mediinfo.WinForm.RepositoryItemMediPictureEdit();
            this.layoutViewField_layoutViewColumn1 = new DevExpress.XtraGrid.Views.Layout.LayoutViewField();
            this.layoutViewColumn2 = new DevExpress.XtraGrid.Columns.LayoutViewColumn();
            this.layoutViewField_layoutViewColumn2 = new DevExpress.XtraGrid.Views.Layout.LayoutViewField();
            this.layoutViewColumn3 = new DevExpress.XtraGrid.Columns.LayoutViewColumn();
            this.layoutViewField_layoutViewColumn3 = new DevExpress.XtraGrid.Views.Layout.LayoutViewField();
            this.layoutViewCard1 = new DevExpress.XtraGrid.Views.Layout.LayoutViewCard();
            this.toolTipController1 = new DevExpress.Utils.ToolTipController(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.mediGridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eGYYONGHUYYEXBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mediLayoutView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMediPictureEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField_layoutViewColumn1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField_layoutViewColumn2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField_layoutViewColumn3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewCard1)).BeginInit();
            this.SuspendLayout();
            // 
            // mediGridControl1
            // 
            this.mediGridControl1.DataSource = this.eGYYONGHUYYEXBindingSource;
            this.mediGridControl1.developerHelper = null;
            this.mediGridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mediGridControl1.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.mediGridControl1.Location = new System.Drawing.Point(0, 0);
            this.mediGridControl1.MainView = this.mediLayoutView1;
            this.mediGridControl1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.mediGridControl1.Name = "mediGridControl1";
            this.mediGridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemMediPictureEdit1});
            this.mediGridControl1.Size = new System.Drawing.Size(459, 211);
            this.mediGridControl1.TabIndex = 0;
            this.mediGridControl1.ToolTipController = this.toolTipController1;
            this.mediGridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.mediLayoutView1});
            // 
            // eGYYONGHUYYEXBindingSource
            // 
            this.eGYYONGHUYYEXBindingSource.DataSource = typeof(Mediinfo.DTO.HIS.GY.E_GY_YONGHUYY_EX);
            // 
            // mediLayoutView1
            // 
            this.mediLayoutView1.ActiveBackColor = System.Drawing.Color.White;
            this.mediLayoutView1.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.mediLayoutView1.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.mediLayoutView1.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.mediLayoutView1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.mediLayoutView1.CardHorzInterval = 0;
            this.mediLayoutView1.CardMinSize = new System.Drawing.Size(114, 90);
            this.mediLayoutView1.CardVertInterval = 0;
            this.mediLayoutView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.LayoutViewColumn[] {
            this.layoutViewColumn1,
            this.layoutViewColumn2,
            this.layoutViewColumn3});
            this.mediLayoutView1.developerHelper = null;
            this.mediLayoutView1.GridControl = this.mediGridControl1;
            this.mediLayoutView1.HiddenItems.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutViewField_layoutViewColumn3});
            this.mediLayoutView1.Name = "mediLayoutView1";
            this.mediLayoutView1.NativeBackColor = System.Drawing.Color.White;
            this.mediLayoutView1.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseUp;
            this.mediLayoutView1.OptionsBehavior.ImmediateUpdateRowPosition = false;
            this.mediLayoutView1.OptionsBehavior.ScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Auto;
            this.mediLayoutView1.OptionsCustomization.AllowFilter = false;
            this.mediLayoutView1.OptionsCustomization.AllowSort = false;
            this.mediLayoutView1.OptionsItemText.TextToControlDistance = 0;
            this.mediLayoutView1.OptionsMultiRecordMode.MultiRowScrollBarOrientation = DevExpress.XtraGrid.Views.Layout.ScrollBarOrientation.Vertical;
            this.mediLayoutView1.OptionsView.AllowHotTrackFields = false;
            this.mediLayoutView1.OptionsView.CardsAlignment = DevExpress.XtraGrid.Views.Layout.CardsAlignment.Near;
            this.mediLayoutView1.OptionsView.ContentAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.mediLayoutView1.OptionsView.FocusRectStyle = DevExpress.XtraGrid.Views.Layout.FocusRectStyle.None;
            this.mediLayoutView1.OptionsView.ShowCardCaption = false;
            this.mediLayoutView1.OptionsView.ShowCardExpandButton = false;
            this.mediLayoutView1.OptionsView.ShowCardLines = false;
            this.mediLayoutView1.OptionsView.ShowFieldHints = false;
            this.mediLayoutView1.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            this.mediLayoutView1.OptionsView.ShowHeaderPanel = false;
            this.mediLayoutView1.OptionsView.ViewMode = DevExpress.XtraGrid.Views.Layout.LayoutViewMode.MultiRow;
            this.mediLayoutView1.TemplateCard = this.layoutViewCard1;
            this.mediLayoutView1.CardClick += new DevExpress.XtraGrid.Views.Layout.Events.CardClickEventHandler(this.mediLayoutView1_CardClick);
            this.mediLayoutView1.CustomUnboundColumnData += new DevExpress.XtraGrid.Views.Base.CustomColumnDataEventHandler(this.mediLayoutView1_CustomUnboundColumnData);
            // 
            // layoutViewColumn1
            // 
            this.layoutViewColumn1.Caption = "layoutViewColumn1";
            this.layoutViewColumn1.ColumnEdit = this.repositoryItemMediPictureEdit1;
            this.layoutViewColumn1.FieldName = "Image";
            this.layoutViewColumn1.LayoutViewField = this.layoutViewField_layoutViewColumn1;
            this.layoutViewColumn1.Name = "layoutViewColumn1";
            this.layoutViewColumn1.OptionsColumn.AllowEdit = false;
            this.layoutViewColumn1.UnboundType = DevExpress.Data.UnboundColumnType.Object;
            // 
            // repositoryItemMediPictureEdit1
            // 
            this.repositoryItemMediPictureEdit1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.repositoryItemMediPictureEdit1.developerHelper = null;
            this.repositoryItemMediPictureEdit1.Name = "repositoryItemMediPictureEdit1";
            this.repositoryItemMediPictureEdit1.NullText = " ";
            this.repositoryItemMediPictureEdit1.RelativeImagePath = "";
            this.repositoryItemMediPictureEdit1.UnboundExpression = null;
            // 
            // layoutViewField_layoutViewColumn1
            // 
            this.layoutViewField_layoutViewColumn1.EditorPreferredWidth = 114;
            this.layoutViewField_layoutViewColumn1.Location = new System.Drawing.Point(0, 0);
            this.layoutViewField_layoutViewColumn1.Name = "layoutViewField_layoutViewColumn1";
            this.layoutViewField_layoutViewColumn1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutViewField_layoutViewColumn1.Size = new System.Drawing.Size(114, 64);
            this.layoutViewField_layoutViewColumn1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutViewField_layoutViewColumn1.TextVisible = false;
            // 
            // layoutViewColumn2
            // 
            this.layoutViewColumn2.AppearanceCell.Options.UseTextOptions = true;
            this.layoutViewColumn2.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.layoutViewColumn2.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Bottom;
            this.layoutViewColumn2.Caption = "layoutViewColumn2";
            this.layoutViewColumn2.FieldName = "YINGYONGMC";
            this.layoutViewColumn2.LayoutViewField = this.layoutViewField_layoutViewColumn2;
            this.layoutViewColumn2.Name = "layoutViewColumn2";
            this.layoutViewColumn2.OptionsColumn.AllowEdit = false;
            // 
            // layoutViewField_layoutViewColumn2
            // 
            this.layoutViewField_layoutViewColumn2.EditorPreferredWidth = 114;
            this.layoutViewField_layoutViewColumn2.Location = new System.Drawing.Point(0, 64);
            this.layoutViewField_layoutViewColumn2.Name = "layoutViewField_layoutViewColumn2";
            this.layoutViewField_layoutViewColumn2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutViewField_layoutViewColumn2.Size = new System.Drawing.Size(114, 26);
            this.layoutViewField_layoutViewColumn2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutViewField_layoutViewColumn2.TextVisible = false;
            // 
            // layoutViewColumn3
            // 
            this.layoutViewColumn3.Caption = "layoutViewColumn3";
            this.layoutViewColumn3.FieldName = "YINGYONGID";
            this.layoutViewColumn3.LayoutViewField = this.layoutViewField_layoutViewColumn3;
            this.layoutViewColumn3.Name = "layoutViewColumn3";
            // 
            // layoutViewField_layoutViewColumn3
            // 
            this.layoutViewField_layoutViewColumn3.EditorPreferredWidth = 15;
            this.layoutViewField_layoutViewColumn3.Location = new System.Drawing.Point(0, 0);
            this.layoutViewField_layoutViewColumn3.Name = "layoutViewField_layoutViewColumn3";
            this.layoutViewField_layoutViewColumn3.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutViewField_layoutViewColumn3.Size = new System.Drawing.Size(110, 90);
            this.layoutViewField_layoutViewColumn3.TextSize = new System.Drawing.Size(224, 33);
            // 
            // layoutViewCard1
            // 
            this.layoutViewCard1.CustomizationFormText = "TemplateCard";
            this.layoutViewCard1.GroupBordersVisible = false;
            this.layoutViewCard1.HeaderButtonsLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            this.layoutViewCard1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutViewField_layoutViewColumn1,
            this.layoutViewField_layoutViewColumn2});
            this.layoutViewCard1.Name = "layoutViewCard1";
            this.layoutViewCard1.OptionsItemText.TextToControlDistance = 0;
            this.layoutViewCard1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutViewCard1.Text = "TemplateCard";
            // 
            // toolTipController1
            // 
            this.toolTipController1.GetActiveObjectInfo += new DevExpress.Utils.ToolTipControllerGetActiveObjectInfoEventHandler(this.toolTipController1_GetActiveObjectInfo);
            // 
            // SwitchSystemForm
            // 
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(459, 211);
            this.Controls.Add(this.mediGridControl1);
            this.LookAndFeel.SkinName = "mediskindevexpressstyle";
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SwitchSystemForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "切换系统";
            this.Load += new System.EventHandler(this.SwitchSystemForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.mediGridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eGYYONGHUYYEXBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mediLayoutView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMediPictureEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField_layoutViewColumn1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField_layoutViewColumn2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField_layoutViewColumn3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewCard1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.MediGridControl mediGridControl1;
        private Controls.MediLayoutView mediLayoutView1;
        private DevExpress.XtraGrid.Columns.LayoutViewColumn layoutViewColumn1;
        private RepositoryItemMediPictureEdit repositoryItemMediPictureEdit1;
        private DevExpress.XtraGrid.Columns.LayoutViewColumn layoutViewColumn2;
        private System.Windows.Forms.BindingSource eGYYONGHUYYEXBindingSource;
        private DevExpress.XtraGrid.Columns.LayoutViewColumn layoutViewColumn3;
        private DevExpress.Utils.ToolTipController toolTipController1;
        private DevExpress.XtraGrid.Views.Layout.LayoutViewField layoutViewField_layoutViewColumn1;
        private DevExpress.XtraGrid.Views.Layout.LayoutViewField layoutViewField_layoutViewColumn2;
        private DevExpress.XtraGrid.Views.Layout.LayoutViewField layoutViewField_layoutViewColumn3;
        private DevExpress.XtraGrid.Views.Layout.LayoutViewCard layoutViewCard1;
    }
}