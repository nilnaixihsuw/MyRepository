namespace Mediinfo.WinForm.HIS.Controls
{
    partial class ShiShiDialog
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
            this.mediPanelControl1 = new Mediinfo.WinForm.MediPanelControl(this.components);
            this.mediGridControl1 = new Mediinfo.WinForm.HIS.Controls.MediGridControl();
            this.fanHuiXxBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.mediGridView1 = new Mediinfo.WinForm.HIS.Controls.MediGridView();
            this.colJUECEFS = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colXIAOXINR = new DevExpress.XtraGrid.Columns.GridColumn();
            this.mediPanelControl2 = new Mediinfo.WinForm.MediPanelControl(this.components);
            this.l_chushengrq = new Mediinfo.WinForm.MediLabel();
            this.mediLabel4 = new Mediinfo.WinForm.MediLabel();
            this.l_xingbie = new Mediinfo.WinForm.MediLabel();
            this.mediLabel3 = new Mediinfo.WinForm.MediLabel();
            this.l_xingming = new Mediinfo.WinForm.MediLabel();
            this.mediLabel2 = new Mediinfo.WinForm.MediLabel();
            this.l_jiankanghao = new Mediinfo.WinForm.MediLabel();
            this.mediLabel1 = new Mediinfo.WinForm.MediLabel();
            this.mediScrollablePanelControl1 = new Mediinfo.WinForm.Common.MediScrollablePanelControl();
            this.ok = new Mediinfo.WinForm.MediBlueButton();
            this.cancel = new Mediinfo.WinForm.MediButton();
            ((System.ComponentModel.ISupportInitialize)(this.mediPanelControl1)).BeginInit();
            this.mediPanelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mediGridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fanHuiXxBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mediGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mediPanelControl2)).BeginInit();
            this.mediPanelControl2.SuspendLayout();
            this.mediScrollablePanelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mediPanelControl1
            // 
            this.mediPanelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.mediPanelControl1.Controls.Add(this.mediGridControl1);
            this.mediPanelControl1.Controls.Add(this.mediPanelControl2);
            this.mediPanelControl1.developerHelper = null;
            this.mediPanelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mediPanelControl1.IsDoubleBuffer = false;
            this.mediPanelControl1.IsHiddedTopBorder = false;
            this.mediPanelControl1.Location = new System.Drawing.Point(0, 0);
            this.mediPanelControl1.Name = "mediPanelControl1";
            this.mediPanelControl1.Size = new System.Drawing.Size(709, 393);
            this.mediPanelControl1.TabIndex = 0;
            // 
            // mediGridControl1
            // 
            this.mediGridControl1.DataSource = this.fanHuiXxBindingSource;
            this.mediGridControl1.developerHelper = null;
            this.mediGridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mediGridControl1.Location = new System.Drawing.Point(0, 39);
            this.mediGridControl1.MainView = this.mediGridView1;
            this.mediGridControl1.Name = "mediGridControl1";
            this.mediGridControl1.Size = new System.Drawing.Size(709, 354);
            this.mediGridControl1.TabIndex = 1;
            this.mediGridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.mediGridView1});
            // 
            // fanHuiXxBindingSource
            // 
            this.fanHuiXxBindingSource.DataSource = typeof(Mediinfo.WinForm.HIS.Core.RuleEntity.FanHuiXx);
            // 
            // mediGridView1
            // 
            this.mediGridView1.Appearance.HeaderPanel.BackColor = System.Drawing.Color.White;
            this.mediGridView1.Appearance.HeaderPanel.Options.UseBackColor = true;
            this.mediGridView1.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.mediGridView1.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.mediGridView1.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.mediGridView1.Appearance.Row.Options.UseTextOptions = true;
            this.mediGridView1.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.mediGridView1.Appearance.Row.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.mediGridView1.AppearancePrint.HeaderPanel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.mediGridView1.AppearancePrint.HeaderPanel.Options.UseBorderColor = true;
            this.mediGridView1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.mediGridView1.ColumnPanelRowHeight = 24;
            this.mediGridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colJUECEFS,
            this.colXIAOXINR});
            this.mediGridView1.DataLayoutCustomValue = null;
            this.mediGridView1.DataLayoutDefaultValue = null;
            this.mediGridView1.DetailTabHeaderLocation = DevExpress.XtraTab.TabHeaderLocation.Left;
            this.mediGridView1.developerHelper = null;
            this.mediGridView1.EditableState = false;
            this.mediGridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.None;
            this.mediGridView1.GetDataRowInfo = null;
            this.mediGridView1.GetDataRowList = null;
            this.mediGridView1.GetList = null;
            this.mediGridView1.GridControl = this.mediGridControl1;
            this.mediGridView1.GridviewColumnDefaultValuedt = null;
            this.mediGridView1.GridviewOtherTypeExpressiondt = null;
            this.mediGridView1.GridviewTabIndexdt = null;
            this.mediGridView1.IsShowLineNumber = false;
            this.mediGridView1.Name = "mediGridView1";
            this.mediGridView1.OptionsBehavior.Editable = false;
            this.mediGridView1.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseUp;
            this.mediGridView1.OptionsBehavior.ImmediateUpdateRowPosition = false;
            this.mediGridView1.OptionsCustomization.AllowFilter = false;
            this.mediGridView1.OptionsDetail.EnableMasterViewMode = false;
            this.mediGridView1.OptionsMenu.EnableColumnMenu = false;
            this.mediGridView1.OptionsNavigation.AutoFocusNewRow = true;
            this.mediGridView1.OptionsNavigation.EnterMoveNextColumn = true;
            this.mediGridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.mediGridView1.OptionsSelection.EnableAppearanceFocusedRow = false;
            this.mediGridView1.OptionsSelection.EnableAppearanceHideSelection = false;
            this.mediGridView1.OptionsView.EnableAppearanceEvenRow = true;
            this.mediGridView1.OptionsView.EnableAppearanceOddRow = true;
            this.mediGridView1.OptionsView.ShowGroupPanel = false;
            this.mediGridView1.OptionsView.ShowHorizontalLines = DevExpress.Utils.DefaultBoolean.False;
            this.mediGridView1.OptionsView.ShowIndicator = false;
            this.mediGridView1.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.False;
            this.mediGridView1.RequiredFieldItem = "";
            this.mediGridView1.RowHeight = 30;
            this.mediGridView1.RowSpace = 8;
            this.mediGridView1.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.mediGridView1_RowCellStyle);
            // 
            // colJUECEFS
            // 
            this.colJUECEFS.Caption = "警告级别";
            this.colJUECEFS.FieldName = "JUECEFS";
            this.colJUECEFS.Name = "colJUECEFS";
            this.colJUECEFS.Visible = true;
            this.colJUECEFS.VisibleIndex = 0;
            this.colJUECEFS.Width = 109;
            // 
            // colXIAOXINR
            // 
            this.colXIAOXINR.Caption = "决策结果";
            this.colXIAOXINR.FieldName = "XIAOXINR";
            this.colXIAOXINR.Name = "colXIAOXINR";
            this.colXIAOXINR.Visible = true;
            this.colXIAOXINR.VisibleIndex = 1;
            this.colXIAOXINR.Width = 596;
            // 
            // mediPanelControl2
            // 
            this.mediPanelControl2.Appearance.BackColor = System.Drawing.Color.White;
            this.mediPanelControl2.Appearance.Options.UseBackColor = true;
            this.mediPanelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.mediPanelControl2.Controls.Add(this.l_chushengrq);
            this.mediPanelControl2.Controls.Add(this.mediLabel4);
            this.mediPanelControl2.Controls.Add(this.l_xingbie);
            this.mediPanelControl2.Controls.Add(this.mediLabel3);
            this.mediPanelControl2.Controls.Add(this.l_xingming);
            this.mediPanelControl2.Controls.Add(this.mediLabel2);
            this.mediPanelControl2.Controls.Add(this.l_jiankanghao);
            this.mediPanelControl2.Controls.Add(this.mediLabel1);
            this.mediPanelControl2.developerHelper = null;
            this.mediPanelControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.mediPanelControl2.IsDoubleBuffer = false;
            this.mediPanelControl2.IsHiddedTopBorder = false;
            this.mediPanelControl2.Location = new System.Drawing.Point(0, 0);
            this.mediPanelControl2.Name = "mediPanelControl2";
            this.mediPanelControl2.Size = new System.Drawing.Size(709, 39);
            this.mediPanelControl2.TabIndex = 0;
            // 
            // l_chushengrq
            // 
            this.l_chushengrq.developerHelper = null;
            this.l_chushengrq.Location = new System.Drawing.Point(579, 10);
            this.l_chushengrq.Name = "l_chushengrq";
            this.l_chushengrq.Size = new System.Drawing.Size(30, 20);
            this.l_chushengrq.TabIndex = 7;
            this.l_chushengrq.Text = "未知";
            // 
            // mediLabel4
            // 
            this.mediLabel4.developerHelper = null;
            this.mediLabel4.Location = new System.Drawing.Point(506, 10);
            this.mediLabel4.Name = "mediLabel4";
            this.mediLabel4.Size = new System.Drawing.Size(75, 20);
            this.mediLabel4.TabIndex = 6;
            this.mediLabel4.Text = "出生日期：";
            // 
            // l_xingbie
            // 
            this.l_xingbie.developerHelper = null;
            this.l_xingbie.Location = new System.Drawing.Point(408, 10);
            this.l_xingbie.Name = "l_xingbie";
            this.l_xingbie.Size = new System.Drawing.Size(30, 20);
            this.l_xingbie.TabIndex = 5;
            this.l_xingbie.Text = "未知";
            // 
            // mediLabel3
            // 
            this.mediLabel3.developerHelper = null;
            this.mediLabel3.Location = new System.Drawing.Point(365, 10);
            this.mediLabel3.Name = "mediLabel3";
            this.mediLabel3.Size = new System.Drawing.Size(45, 20);
            this.mediLabel3.TabIndex = 4;
            this.mediLabel3.Text = "性别：";
            // 
            // l_xingming
            // 
            this.l_xingming.developerHelper = null;
            this.l_xingming.Location = new System.Drawing.Point(256, 11);
            this.l_xingming.Name = "l_xingming";
            this.l_xingming.Size = new System.Drawing.Size(30, 20);
            this.l_xingming.TabIndex = 3;
            this.l_xingming.Text = "未知";
            // 
            // mediLabel2
            // 
            this.mediLabel2.developerHelper = null;
            this.mediLabel2.Location = new System.Drawing.Point(183, 10);
            this.mediLabel2.Name = "mediLabel2";
            this.mediLabel2.Size = new System.Drawing.Size(75, 20);
            this.mediLabel2.TabIndex = 2;
            this.mediLabel2.Text = "患者姓名：";
            // 
            // l_jiankanghao
            // 
            this.l_jiankanghao.developerHelper = null;
            this.l_jiankanghao.Location = new System.Drawing.Point(68, 11);
            this.l_jiankanghao.Name = "l_jiankanghao";
            this.l_jiankanghao.Size = new System.Drawing.Size(30, 20);
            this.l_jiankanghao.TabIndex = 1;
            this.l_jiankanghao.Text = "未知";
            // 
            // mediLabel1
            // 
            this.mediLabel1.developerHelper = null;
            this.mediLabel1.Location = new System.Drawing.Point(11, 11);
            this.mediLabel1.Name = "mediLabel1";
            this.mediLabel1.Size = new System.Drawing.Size(60, 20);
            this.mediLabel1.TabIndex = 0;
            this.mediLabel1.Text = "健康号：";
            // 
            // mediScrollablePanelControl1
            // 
            this.mediScrollablePanelControl1.Appearance.BackColor = System.Drawing.Color.White;
            this.mediScrollablePanelControl1.Appearance.Options.UseBackColor = true;
            this.mediScrollablePanelControl1.Controls.Add(this.ok);
            this.mediScrollablePanelControl1.Controls.Add(this.cancel);
            this.mediScrollablePanelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.mediScrollablePanelControl1.Location = new System.Drawing.Point(0, 393);
            this.mediScrollablePanelControl1.Name = "mediScrollablePanelControl1";
            this.mediScrollablePanelControl1.Size = new System.Drawing.Size(709, 64);
            this.mediScrollablePanelControl1.TabIndex = 1;
            // 
            // ok
            // 
            this.ok.developerHelper = null;
            this.ok.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.ok.Location = new System.Drawing.Point(627, 19);
            this.ok.LookAndFeel.SkinName = "MediSkinDevExpressStyle";
            this.ok.Name = "ok";
            this.ok.Size = new System.Drawing.Size(70, 26);
            this.ok.TabIndex = 1;
            this.ok.Text = "继续";
            this.ok.UnboundExpression = null;
            this.ok.Click += new System.EventHandler(this.ok_Click);
            // 
            // cancel
            // 
            this.cancel.developerHelper = null;
            this.cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancel.Location = new System.Drawing.Point(551, 19);
            this.cancel.LookAndFeel.SkinName = "MediSkinDevExpressStyle";
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(70, 26);
            this.cancel.TabIndex = 0;
            this.cancel.Text = "取消";
            this.cancel.UnboundExpression = null;
            this.cancel.Click += new System.EventHandler(this.cancel_Click);
            // 
            // ShiShiDialog
            // 
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(709, 457);
            this.Controls.Add(this.mediPanelControl1);
            this.Controls.Add(this.mediScrollablePanelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.LookAndFeel.SkinName = "mediskindevexpressstyle";
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ShiShiDialog";
            this.Padding = new System.Windows.Forms.Padding(0);
            this.Text = "医生决策 - 规则引擎";
            this.Load += new System.EventHandler(this.ShiShiDialog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.mediPanelControl1)).EndInit();
            this.mediPanelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mediGridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fanHuiXxBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mediGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mediPanelControl2)).EndInit();
            this.mediPanelControl2.ResumeLayout(false);
            this.mediPanelControl2.PerformLayout();
            this.mediScrollablePanelControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private MediPanelControl mediPanelControl1;
        private MediGridControl mediGridControl1;
        private MediGridView mediGridView1;
        private MediPanelControl mediPanelControl2;
        private Common.MediScrollablePanelControl mediScrollablePanelControl1;
        private System.Windows.Forms.BindingSource fanHuiXxBindingSource;
        private DevExpress.XtraGrid.Columns.GridColumn colJUECEFS;
        private DevExpress.XtraGrid.Columns.GridColumn colXIAOXINR;
        private MediLabel mediLabel1;
        private MediLabel l_jiankanghao;
        private MediLabel mediLabel2;
        private MediLabel l_xingming;
        private MediLabel mediLabel3;
        private MediLabel l_xingbie;
        private MediLabel l_chushengrq;
        private MediLabel mediLabel4;
        private MediBlueButton ok;
        private MediButton cancel;
    }
}