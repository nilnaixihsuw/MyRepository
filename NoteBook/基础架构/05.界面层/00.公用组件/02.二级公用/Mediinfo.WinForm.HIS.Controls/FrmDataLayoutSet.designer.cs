using DevExpress.XtraBars.Navigation;

namespace Mediinfo.WinForm.HIS.Controls
{
    partial class FrmDataLayoutSet
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDataLayoutSet));
            this.simpleButtomUp = new Mediinfo.WinForm.MediButton();
            this.simpleButtonDown = new Mediinfo.WinForm.MediButton();
            this.btnEditSearch = new DevExpress.XtraEditors.ButtonEdit();
            this.propertyGridLieShuX = new System.Windows.Forms.PropertyGrid();
            this.propertyGridHangShuX = new System.Windows.Forms.PropertyGrid();
            this.propertyGridQiTaSX = new System.Windows.Forms.PropertyGrid();
            this.radioGroupLevel = new Mediinfo.WinForm.Common.MediRadioGroup();
            this.labelControl1 = new Mediinfo.WinForm.MediLabel();
            this.simpleButtonBack = new Mediinfo.WinForm.MediButton();
            this.SimpleButtomReset = new Mediinfo.WinForm.MediButton();
            this.SimpleButtonSave = new Mediinfo.WinForm.MediButton();
            this.mediPanelControl1 = new Mediinfo.WinForm.MediPanelControl(this.components);
            this.mediSplitContainerControl1 = new Mediinfo.WinForm.MediSplitContainerControl(this.components);
            this.mediPanelControl5 = new Mediinfo.WinForm.MediPanelControl(this.components);
            this.ListBoxControlColumn = new Mediinfo.WinForm.MediListBox();
            this.mediPanelControl4 = new Mediinfo.WinForm.MediPanelControl(this.components);
            this.mediTabControl1 = new Mediinfo.WinForm.MediTabControl();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.mediGridControlSource = new Mediinfo.WinForm.HIS.Controls.MediGridControl();
            this.mediGridView1 = new Mediinfo.WinForm.HIS.Controls.MediGridView();
            this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
            this.xtraTabPage3 = new DevExpress.XtraTab.XtraTabPage();
            this.mediPanelControl2 = new Mediinfo.WinForm.MediPanelControl(this.components);
            this.labelName = new Mediinfo.WinForm.MediLabel();
            this.mediLabel1 = new Mediinfo.WinForm.MediLabel();
            this.mediPanelControl3 = new Mediinfo.WinForm.MediPanelControl(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.btnEditSearch.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroupLevel.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mediPanelControl1)).BeginInit();
            this.mediPanelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mediSplitContainerControl1)).BeginInit();
            this.mediSplitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mediPanelControl5)).BeginInit();
            this.mediPanelControl5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ListBoxControlColumn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mediPanelControl4)).BeginInit();
            this.mediPanelControl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mediTabControl1)).BeginInit();
            this.mediTabControl1.SuspendLayout();
            this.xtraTabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mediGridControlSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mediGridView1)).BeginInit();
            this.xtraTabPage2.SuspendLayout();
            this.xtraTabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mediPanelControl2)).BeginInit();
            this.mediPanelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mediPanelControl3)).BeginInit();
            this.mediPanelControl3.SuspendLayout();
            this.SuspendLayout();
            // 
            // simpleButtomUp
            // 
            this.simpleButtomUp.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.simpleButtomUp.Location = new System.Drawing.Point(7, 5);
            this.simpleButtomUp.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.simpleButtomUp.Name = "simpleButtomUp";
            this.simpleButtomUp.Size = new System.Drawing.Size(84, 26);
            this.simpleButtomUp.TabIndex = 0;
            this.simpleButtomUp.Text = "↑";
            this.simpleButtomUp.UnboundExpression = null;
            this.simpleButtomUp.Click += new System.EventHandler(this.btnUp_Click);
            // 
            // simpleButtonDown
            // 
            this.simpleButtonDown.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.simpleButtonDown.Location = new System.Drawing.Point(97, 5);
            this.simpleButtonDown.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.simpleButtonDown.Name = "simpleButtonDown";
            this.simpleButtonDown.Size = new System.Drawing.Size(84, 26);
            this.simpleButtonDown.TabIndex = 0;
            this.simpleButtonDown.Text = "↓";
            this.simpleButtonDown.UnboundExpression = null;
            this.simpleButtonDown.Click += new System.EventHandler(this.btnDown_Click);
            // 
            // btnEditSearch
            // 
            this.btnEditSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnEditSearch.Location = new System.Drawing.Point(2, 2);
            this.btnEditSearch.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.btnEditSearch.Name = "btnEditSearch";
            this.btnEditSearch.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.btnEditSearch.Properties.Appearance.Options.UseBackColor = true;
            this.btnEditSearch.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.btnEditSearch.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Search)});
            this.btnEditSearch.Size = new System.Drawing.Size(192, 20);
            this.btnEditSearch.TabIndex = 2;
            this.btnEditSearch.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.btnEditSearch_ButtonClick);
            this.btnEditSearch.TextChanged += new System.EventHandler(this.btnEditSearch_TextChanged);
            // 
            // propertyGridLieShuX
            // 
            this.propertyGridLieShuX.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGridLieShuX.Location = new System.Drawing.Point(0, 0);
            this.propertyGridLieShuX.Margin = new System.Windows.Forms.Padding(0);
            this.propertyGridLieShuX.Name = "propertyGridLieShuX";
            this.propertyGridLieShuX.PropertySort = System.Windows.Forms.PropertySort.Categorized;
            this.propertyGridLieShuX.Size = new System.Drawing.Size(531, 394);
            this.propertyGridLieShuX.TabIndex = 0;
            this.propertyGridLieShuX.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.propGridCommon_PropertyValueChanged);
            // 
            // propertyGridHangShuX
            // 
            this.propertyGridHangShuX.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGridHangShuX.Location = new System.Drawing.Point(0, 0);
            this.propertyGridHangShuX.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.propertyGridHangShuX.Name = "propertyGridHangShuX";
            this.propertyGridHangShuX.Size = new System.Drawing.Size(531, 394);
            this.propertyGridHangShuX.TabIndex = 0;
            this.propertyGridHangShuX.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.propGridRelue_PropertyValueChanged);
            // 
            // propertyGridQiTaSX
            // 
            this.propertyGridQiTaSX.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGridQiTaSX.Location = new System.Drawing.Point(0, 0);
            this.propertyGridQiTaSX.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.propertyGridQiTaSX.Name = "propertyGridQiTaSX";
            this.propertyGridQiTaSX.Size = new System.Drawing.Size(531, 394);
            this.propertyGridQiTaSX.TabIndex = 0;
            this.propertyGridQiTaSX.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.propGridOrther_PropertyValueChanged);
            // 
            // radioGroupLevel
            // 
            this.radioGroupLevel.EditValue = "2";
            this.radioGroupLevel.Location = new System.Drawing.Point(72, 5);
            this.radioGroupLevel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.radioGroupLevel.Name = "radioGroupLevel";
            this.radioGroupLevel.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.radioGroupLevel.Properties.Appearance.Options.UseBackColor = true;
            this.radioGroupLevel.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.radioGroupLevel.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem("0", "应用"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("1", "系统"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("2", "全局")});
            this.radioGroupLevel.Size = new System.Drawing.Size(160, 26);
            this.radioGroupLevel.TabIndex = 2;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.labelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.labelControl1.Location = new System.Drawing.Point(7, 10);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(48, 14);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "控制级别";
            // 
            // simpleButtonBack
            // 
            this.simpleButtonBack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleButtonBack.Location = new System.Drawing.Point(655, 5);
            this.simpleButtonBack.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.simpleButtonBack.Name = "simpleButtonBack";
            this.simpleButtonBack.Size = new System.Drawing.Size(78, 26);
            this.simpleButtonBack.TabIndex = 0;
            this.simpleButtonBack.Text = "关闭(&X)";
            this.simpleButtonBack.UnboundExpression = null;
            this.simpleButtonBack.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // SimpleButtomReset
            // 
            this.SimpleButtomReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.SimpleButtomReset.Location = new System.Drawing.Point(487, 5);
            this.SimpleButtomReset.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.SimpleButtomReset.Name = "SimpleButtomReset";
            this.SimpleButtomReset.Size = new System.Drawing.Size(78, 26);
            this.SimpleButtomReset.TabIndex = 0;
            this.SimpleButtomReset.Text = "复位(&R)";
            this.SimpleButtomReset.UnboundExpression = null;
            this.SimpleButtomReset.Click += new System.EventHandler(this.SimpleButtomReset_Click);
            // 
            // SimpleButtonSave
            // 
            this.SimpleButtonSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.SimpleButtonSave.Location = new System.Drawing.Point(571, 5);
            this.SimpleButtonSave.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.SimpleButtonSave.Name = "SimpleButtonSave";
            this.SimpleButtonSave.Size = new System.Drawing.Size(78, 26);
            this.SimpleButtonSave.TabIndex = 0;
            this.SimpleButtonSave.Text = "保存(&S)";
            this.SimpleButtonSave.UnboundExpression = null;
            this.SimpleButtonSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // mediPanelControl1
            // 
            this.mediPanelControl1.Controls.Add(this.mediSplitContainerControl1);
            this.mediPanelControl1.Controls.Add(this.mediPanelControl2);
            this.mediPanelControl1.Controls.Add(this.mediPanelControl3);
            this.mediPanelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mediPanelControl1.Location = new System.Drawing.Point(5, 4);
            this.mediPanelControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.mediPanelControl1.Name = "mediPanelControl1";
            this.mediPanelControl1.Size = new System.Drawing.Size(742, 492);
            this.mediPanelControl1.TabIndex = 1;
            // 
            // mediSplitContainerControl1
            // 
            this.mediSplitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mediSplitContainerControl1.Location = new System.Drawing.Point(2, 32);
            this.mediSplitContainerControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.mediSplitContainerControl1.Name = "mediSplitContainerControl1";
            this.mediSplitContainerControl1.Panel1.Controls.Add(this.mediPanelControl5);
            this.mediSplitContainerControl1.Panel1.Text = "Panel1";
            this.mediSplitContainerControl1.Panel2.Controls.Add(this.mediTabControl1);
            this.mediSplitContainerControl1.Panel2.Text = "Panel2";
            this.mediSplitContainerControl1.Size = new System.Drawing.Size(738, 423);
            this.mediSplitContainerControl1.SplitterPosition = 196;
            this.mediSplitContainerControl1.TabIndex = 0;
            this.mediSplitContainerControl1.Text = "mediSplitContainerControl1";
            // 
            // mediPanelControl5
            // 
            this.mediPanelControl5.Controls.Add(this.ListBoxControlColumn);
            this.mediPanelControl5.Controls.Add(this.btnEditSearch);
            this.mediPanelControl5.Controls.Add(this.mediPanelControl4);
            this.mediPanelControl5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mediPanelControl5.Location = new System.Drawing.Point(0, 0);
            this.mediPanelControl5.Name = "mediPanelControl5";
            this.mediPanelControl5.Size = new System.Drawing.Size(196, 423);
            this.mediPanelControl5.TabIndex = 5;
            // 
            // ListBoxControlColumn
            // 
            this.ListBoxControlColumn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ListBoxControlColumn.Location = new System.Drawing.Point(2, 22);
            this.ListBoxControlColumn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ListBoxControlColumn.Name = "ListBoxControlColumn";
            this.ListBoxControlColumn.Size = new System.Drawing.Size(192, 364);
            this.ListBoxControlColumn.TabIndex = 4;
            this.ListBoxControlColumn.SelectedIndexChanged += new System.EventHandler(this.lstBoxColumn_SelectedIndexChanged);
            this.ListBoxControlColumn.DrawItem += new DevExpress.XtraEditors.ListBoxDrawItemEventHandler(this.lstBoxColumn_DrawItem);
            // 
            // mediPanelControl4
            // 
            this.mediPanelControl4.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.mediPanelControl4.Controls.Add(this.simpleButtonDown);
            this.mediPanelControl4.Controls.Add(this.simpleButtomUp);
            this.mediPanelControl4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.mediPanelControl4.Location = new System.Drawing.Point(2, 386);
            this.mediPanelControl4.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.mediPanelControl4.Name = "mediPanelControl4";
            this.mediPanelControl4.Size = new System.Drawing.Size(192, 35);
            this.mediPanelControl4.TabIndex = 3;
            // 
            // mediTabControl1
            // 
            this.mediTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mediTabControl1.Location = new System.Drawing.Point(0, 0);
            this.mediTabControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.mediTabControl1.Name = "mediTabControl1";
            this.mediTabControl1.SelectedTabPage = this.xtraTabPage1;
            this.mediTabControl1.Size = new System.Drawing.Size(537, 423);
            this.mediTabControl1.TabIndex = 0;
            this.mediTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1,
            this.xtraTabPage2,
            this.xtraTabPage3});
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Controls.Add(this.mediGridControlSource);
            this.xtraTabPage1.Controls.Add(this.propertyGridLieShuX);
            this.xtraTabPage1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(531, 394);
            this.xtraTabPage1.Text = "列属性";
            // 
            // mediGridControlSource
            // 
            this.mediGridControlSource.Location = new System.Drawing.Point(86, 68);
            this.mediGridControlSource.MainView = this.mediGridView1;
            this.mediGridControlSource.Name = "mediGridControlSource";
            this.mediGridControlSource.Size = new System.Drawing.Size(400, 200);
            this.mediGridControlSource.TabIndex = 1;
            this.mediGridControlSource.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.mediGridView1});
            this.mediGridControlSource.Visible = false;
            // 
            // mediGridView1
            // 
            this.mediGridView1.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.mediGridView1.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.mediGridView1.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.mediGridView1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.mediGridView1.ColumnPanelRowHeight = 24;
            this.mediGridView1.EditableState = true;
            this.mediGridView1.GetDataRowInfo = null;
            this.mediGridView1.GetDataRowList = null;
            this.mediGridView1.GetList = null;
            this.mediGridView1.GridControl = this.mediGridControlSource;
            this.mediGridView1.GridviewColumnDefaultValuedt = null;
            this.mediGridView1.GridviewOtherTypeExpressiondt = null;
            this.mediGridView1.GridviewTabIndexdt = null;
            this.mediGridView1.Name = "mediGridView1";
            this.mediGridView1.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDown;
            this.mediGridView1.OptionsDetail.EnableMasterViewMode = false;
            this.mediGridView1.OptionsNavigation.AutoFocusNewRow = true;
            this.mediGridView1.OptionsNavigation.EnterMoveNextColumn = true;
            this.mediGridView1.OptionsSelection.MultiSelect = true;
            this.mediGridView1.OptionsView.ColumnAutoWidth = false;
            this.mediGridView1.OptionsView.EnableAppearanceEvenRow = true;
            this.mediGridView1.OptionsView.EnableAppearanceOddRow = true;
            this.mediGridView1.OptionsView.ShowFooter = true;
            this.mediGridView1.OptionsView.ShowGroupPanel = false;
            this.mediGridView1.OptionsView.ShowHorizontalLines = DevExpress.Utils.DefaultBoolean.False;
            this.mediGridView1.OptionsView.ShowIndicator = false;
            this.mediGridView1.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.False;
            this.mediGridView1.RequiredFieldItem = "";
            // 
            // xtraTabPage2
            // 
            this.xtraTabPage2.Controls.Add(this.propertyGridHangShuX);
            this.xtraTabPage2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.xtraTabPage2.Name = "xtraTabPage2";
            this.xtraTabPage2.Size = new System.Drawing.Size(531, 394);
            this.xtraTabPage2.Text = "行属性";
            // 
            // xtraTabPage3
            // 
            this.xtraTabPage3.Controls.Add(this.propertyGridQiTaSX);
            this.xtraTabPage3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.xtraTabPage3.Name = "xtraTabPage3";
            this.xtraTabPage3.Size = new System.Drawing.Size(531, 394);
            this.xtraTabPage3.Text = "其他属性";
            // 
            // mediPanelControl2
            // 
            this.mediPanelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.mediPanelControl2.Controls.Add(this.labelName);
            this.mediPanelControl2.Controls.Add(this.mediLabel1);
            this.mediPanelControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.mediPanelControl2.Location = new System.Drawing.Point(2, 2);
            this.mediPanelControl2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.mediPanelControl2.Name = "mediPanelControl2";
            this.mediPanelControl2.Size = new System.Drawing.Size(738, 30);
            this.mediPanelControl2.TabIndex = 1;
            // 
            // labelName
            // 
            this.labelName.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.labelName.Location = new System.Drawing.Point(42, 6);
            this.labelName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(61, 14);
            this.labelName.TabIndex = 2;
            this.labelName.Text = "labelName";
            // 
            // mediLabel1
            // 
            this.mediLabel1.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.mediLabel1.Location = new System.Drawing.Point(7, 6);
            this.mediLabel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.mediLabel1.Name = "mediLabel1";
            this.mediLabel1.Size = new System.Drawing.Size(39, 14);
            this.mediLabel1.TabIndex = 2;
            this.mediLabel1.Text = "名称：";
            // 
            // mediPanelControl3
            // 
            this.mediPanelControl3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.mediPanelControl3.Controls.Add(this.simpleButtonBack);
            this.mediPanelControl3.Controls.Add(this.SimpleButtomReset);
            this.mediPanelControl3.Controls.Add(this.labelControl1);
            this.mediPanelControl3.Controls.Add(this.radioGroupLevel);
            this.mediPanelControl3.Controls.Add(this.SimpleButtonSave);
            this.mediPanelControl3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.mediPanelControl3.Location = new System.Drawing.Point(2, 455);
            this.mediPanelControl3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.mediPanelControl3.Name = "mediPanelControl3";
            this.mediPanelControl3.Size = new System.Drawing.Size(738, 35);
            this.mediPanelControl3.TabIndex = 1;
            // 
            // FrmDataLayoutSet
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(752, 500);
            this.Controls.Add(this.mediPanelControl1);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "FrmDataLayoutSet";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "自定义数据显示设置";
            this.Load += new System.EventHandler(this.FrmDataLayoutSet_Load);
            ((System.ComponentModel.ISupportInitialize)(this.btnEditSearch.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroupLevel.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mediPanelControl1)).EndInit();
            this.mediPanelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mediSplitContainerControl1)).EndInit();
            this.mediSplitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mediPanelControl5)).EndInit();
            this.mediPanelControl5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ListBoxControlColumn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mediPanelControl4)).EndInit();
            this.mediPanelControl4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mediTabControl1)).EndInit();
            this.mediTabControl1.ResumeLayout(false);
            this.xtraTabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mediGridControlSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mediGridView1)).EndInit();
            this.xtraTabPage2.ResumeLayout(false);
            this.xtraTabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mediPanelControl2)).EndInit();
            this.mediPanelControl2.ResumeLayout(false);
            this.mediPanelControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mediPanelControl3)).EndInit();
            this.mediPanelControl3.ResumeLayout(false);
            this.mediPanelControl3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private WinForm.MediButton simpleButtonBack;
        private WinForm.MediButton SimpleButtonSave;
        private System.Windows.Forms.PropertyGrid propertyGridLieShuX;
        private WinForm.MediButton simpleButtomUp;
        private WinForm.MediButton simpleButtonDown;
        private System.Windows.Forms.PropertyGrid propertyGridQiTaSX;
        private DevExpress.XtraEditors.ButtonEdit btnEditSearch;
        private WinForm.MediButton SimpleButtomReset;
        private WinForm.Common.MediRadioGroup radioGroupLevel;
        private WinForm.MediLabel labelControl1;
        private System.Windows.Forms.PropertyGrid propertyGridHangShuX;
        private WinForm.MediPanelControl mediPanelControl1;
        private WinForm.MediPanelControl mediPanelControl3;
        private WinForm.MediPanelControl mediPanelControl2;
        private WinForm.MediSplitContainerControl mediSplitContainerControl1;
        private WinForm.MediTabControl mediTabControl1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage2;
        private WinForm.MediPanelControl mediPanelControl4;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage3;
        private WinForm.MediLabel labelName;
        private WinForm.MediLabel mediLabel1;
        private WinForm.MediListBox ListBoxControlColumn;
        private WinForm.MediPanelControl mediPanelControl5;
        private MediGridControl mediGridControlSource;
        private MediGridView mediGridView1;
    }
}