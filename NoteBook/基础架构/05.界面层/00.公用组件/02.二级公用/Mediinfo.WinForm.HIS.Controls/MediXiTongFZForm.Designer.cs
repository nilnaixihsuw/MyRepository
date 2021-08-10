namespace Mediinfo.WinForm.HIS.Controls
{
    partial class MediXiTongFZForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MediXiTongFZForm));
            this.mediPanelControl2 = new Mediinfo.WinForm.MediPanelControl(this.components);
            this.mediGridControl1 = new Mediinfo.WinForm.HIS.Controls.MediGridControl();
            this.layoutView1 = new DevExpress.XtraGrid.Views.Layout.LayoutView();
            this.layoutViewCard1 = new DevExpress.XtraGrid.Views.Layout.LayoutViewCard();
            this.mediPanelControl1 = new Mediinfo.WinForm.MediPanelControl(this.components);
            this.medibtnClose = new Mediinfo.WinForm.MediButton();
            this.medibtnControlCustom = new Mediinfo.WinForm.MediButton();
            this.medibtnSecondRootProjectManager = new Mediinfo.WinForm.MediButton();
            ((System.ComponentModel.ISupportInitialize)(this.mediPanelControl2)).BeginInit();
            this.mediPanelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mediGridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewCard1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mediPanelControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // mediPanelControl2
            // 
            this.mediPanelControl2.Controls.Add(this.mediGridControl1);
            this.mediPanelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mediPanelControl2.Location = new System.Drawing.Point(5, 130);
            this.mediPanelControl2.Name = "mediPanelControl2";
            this.mediPanelControl2.Size = new System.Drawing.Size(335, 250);
            this.mediPanelControl2.TabIndex = 1;
            // 
            // mediGridControl1
            // 
            this.mediGridControl1.Location = new System.Drawing.Point(5, 45);
            this.mediGridControl1.MainView = this.layoutView1;
            this.mediGridControl1.Name = "mediGridControl1";
            this.mediGridControl1.Size = new System.Drawing.Size(400, 200);
            this.mediGridControl1.TabIndex = 0;
            this.mediGridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.layoutView1});
            // 
            // layoutView1
            // 
            this.layoutView1.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.layoutView1.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.layoutView1.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.layoutView1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.layoutView1.GridControl = this.mediGridControl1;
            this.layoutView1.Name = "layoutView1";
            this.layoutView1.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDown;
            this.layoutView1.OptionsSelection.MultiSelect = true;
            this.layoutView1.TemplateCard = this.layoutViewCard1;
            // 
            // layoutViewCard1
            // 
            this.layoutViewCard1.HeaderButtonsLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            this.layoutViewCard1.Name = "layoutViewCard1";
            // 
            // mediPanelControl1
            // 
            this.mediPanelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.mediPanelControl1.Location = new System.Drawing.Point(5, 5);
            this.mediPanelControl1.Name = "mediPanelControl1";
            this.mediPanelControl1.Size = new System.Drawing.Size(335, 125);
            this.mediPanelControl1.TabIndex = 0;
            // 
            // medibtnClose
            // 
            this.medibtnClose.Appearance.Options.UseTextOptions = true;
            this.medibtnClose.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.medibtnClose.Location = new System.Drawing.Point(181, 244);
            this.medibtnClose.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.medibtnClose.Name = "medibtnClose";
            this.medibtnClose.Size = new System.Drawing.Size(75, 32);
            this.medibtnClose.TabIndex = 2;
            this.medibtnClose.Text = "关闭";
            this.medibtnClose.UnboundExpression = null;
            this.medibtnClose.Click += new System.EventHandler(this.medibtnCancel_Click);
            // 
            // medibtnControlCustom
            // 
            this.medibtnControlCustom.AccessibleDescription = "";
            this.medibtnControlCustom.Appearance.Options.UseTextOptions = true;
            this.medibtnControlCustom.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.medibtnControlCustom.Location = new System.Drawing.Point(77, 157);
            this.medibtnControlCustom.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.medibtnControlCustom.Name = "medibtnControlCustom";
            this.medibtnControlCustom.Size = new System.Drawing.Size(179, 49);
            this.medibtnControlCustom.TabIndex = 1;
            this.medibtnControlCustom.Text = "控件项目自定义";
            this.medibtnControlCustom.UnboundExpression = null;
            this.medibtnControlCustom.Click += new System.EventHandler(this.medibtnControlCustom_Click);
            // 
            // medibtnSecondRootProjectManager
            // 
            this.medibtnSecondRootProjectManager.Appearance.Options.UseTextOptions = true;
            this.medibtnSecondRootProjectManager.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.medibtnSecondRootProjectManager.Location = new System.Drawing.Point(77, 63);
            this.medibtnSecondRootProjectManager.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.medibtnSecondRootProjectManager.Name = "medibtnSecondRootProjectManager";
            this.medibtnSecondRootProjectManager.Size = new System.Drawing.Size(179, 49);
            this.medibtnSecondRootProjectManager.TabIndex = 0;
            this.medibtnSecondRootProjectManager.Text = "二级权限项目管理";
            this.medibtnSecondRootProjectManager.UnboundExpression = null;
            this.medibtnSecondRootProjectManager.Click += new System.EventHandler(this.medibtnSecondRootProjectManager_Click);
            // 
            // MediXiTongFZForm
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(345, 385);
            this.Controls.Add(this.mediPanelControl2);
            this.Controls.Add(this.mediPanelControl1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MediXiTongFZForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "系统辅助功能选择";
            ((System.ComponentModel.ISupportInitialize)(this.mediPanelControl2)).EndInit();
            this.mediPanelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mediGridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewCard1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mediPanelControl1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private MediPanelControl mediPanelControl1;
        private MediPanelControl mediPanelControl2;
        private MediButton medibtnClose;
        private MediButton medibtnControlCustom;
        private MediButton medibtnSecondRootProjectManager;
        private MediGridControl mediGridControl1;
        private DevExpress.XtraGrid.Views.Layout.LayoutView layoutView1;
        private DevExpress.XtraGrid.Views.Layout.LayoutViewCard layoutViewCard1;
    }
}