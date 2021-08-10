namespace Mediinfo.WinForm.HIS.Main
{
    partial class CustomDownLoadFileFrm
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
            Mediinfo.WinForm.SystemInfoHelper systemInfoHelper2 = new Mediinfo.WinForm.SystemInfoHelper();
            Mediinfo.WinForm.SystemInfoHelper systemInfoHelper1 = new Mediinfo.WinForm.SystemInfoHelper();
            Mediinfo.WinForm.SystemInfoHelper systemInfoHelper5 = new Mediinfo.WinForm.SystemInfoHelper();
            Mediinfo.WinForm.SystemInfoHelper systemInfoHelper3 = new Mediinfo.WinForm.SystemInfoHelper();
            Mediinfo.WinForm.SystemInfoHelper systemInfoHelper4 = new Mediinfo.WinForm.SystemInfoHelper();
            this.mediPanelControl1 = new Mediinfo.WinForm.MediPanelControl();
            this.updateFiletreeList = new Mediinfo.WinForm.MediTreeList();
            this.gcDirectoryName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.mediPanelControl2 = new Mediinfo.WinForm.MediPanelControl();
            this.medisbtnCancel = new Mediinfo.WinForm.MediButton();
            this.medisbtnOK = new Mediinfo.WinForm.MediButton();
            ((System.ComponentModel.ISupportInitialize)(this.mediPanelControl1)).BeginInit();
            this.mediPanelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.updateFiletreeList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mediPanelControl2)).BeginInit();
            this.mediPanelControl2.SuspendLayout();
            this.SuspendLayout();
            // 
            // mediPanelControl1
            // 
            this.mediPanelControl1.Controls.Add(this.updateFiletreeList);
            systemInfoHelper2.ControlAssemblyName = null;
            systemInfoHelper2.ControlClassName = null;
            systemInfoHelper2.ControlForFormName = null;
            systemInfoHelper2.ControlFormAssemblyName = null;
            systemInfoHelper2.ControlFormClassName = null;
            systemInfoHelper2.ControlFormDYCS = null;
            systemInfoHelper2.ControlFormGongNengID = null;
            systemInfoHelper2.ControlFormNameSpace = null;
            systemInfoHelper2.ControlFromYingYongID = null;
            systemInfoHelper2.ControlName = null;
            systemInfoHelper2.ControlNameSpace = null;
            systemInfoHelper2.CurrentControlParentFrm = null;
            systemInfoHelper2.CurrentSystemDBConnStr = null;
            this.mediPanelControl1.developerHelper = systemInfoHelper2;
            this.mediPanelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mediPanelControl1.IsDoubleBuffer = false;
            this.mediPanelControl1.Location = new System.Drawing.Point(0, 0);
            this.mediPanelControl1.Name = "mediPanelControl1";
            this.mediPanelControl1.Size = new System.Drawing.Size(501, 301);
            this.mediPanelControl1.TabIndex = 0;
            // 
            // updateFiletreeList
            // 
            this.updateFiletreeList.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(163)))), ((int)(((byte)(229)))));
            this.updateFiletreeList.Appearance.FocusedRow.ForeColor = System.Drawing.Color.White;
            this.updateFiletreeList.Appearance.FocusedRow.Options.UseBackColor = true;
            this.updateFiletreeList.Appearance.FocusedRow.Options.UseForeColor = true;
            this.updateFiletreeList.Appearance.SelectedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(163)))), ((int)(((byte)(229)))));
            this.updateFiletreeList.Appearance.SelectedRow.ForeColor = System.Drawing.Color.White;
            this.updateFiletreeList.Appearance.SelectedRow.Options.UseBackColor = true;
            this.updateFiletreeList.Appearance.SelectedRow.Options.UseForeColor = true;
            this.updateFiletreeList.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.gcDirectoryName});
            this.updateFiletreeList.DataSource = null;
            systemInfoHelper1.ControlAssemblyName = null;
            systemInfoHelper1.ControlClassName = null;
            systemInfoHelper1.ControlForFormName = null;
            systemInfoHelper1.ControlFormAssemblyName = null;
            systemInfoHelper1.ControlFormClassName = null;
            systemInfoHelper1.ControlFormDYCS = null;
            systemInfoHelper1.ControlFormGongNengID = null;
            systemInfoHelper1.ControlFormNameSpace = null;
            systemInfoHelper1.ControlFromYingYongID = null;
            systemInfoHelper1.ControlName = null;
            systemInfoHelper1.ControlNameSpace = null;
            systemInfoHelper1.CurrentControlParentFrm = null;
            systemInfoHelper1.CurrentSystemDBConnStr = null;
            this.updateFiletreeList.developerHelper = systemInfoHelper1;
            this.updateFiletreeList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.updateFiletreeList.Location = new System.Drawing.Point(2, 2);
            this.updateFiletreeList.Name = "updateFiletreeList";
            this.updateFiletreeList.OptionsBehavior.AutoNodeHeight = false;
            this.updateFiletreeList.OptionsBehavior.Editable = false;
            this.updateFiletreeList.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.updateFiletreeList.OptionsView.AutoWidth = false;
            this.updateFiletreeList.OptionsView.FocusRectStyle = DevExpress.XtraTreeList.DrawFocusRectStyle.None;
            this.updateFiletreeList.OptionsView.ShowCheckBoxes = true;
            this.updateFiletreeList.OptionsView.ShowHorzLines = false;
            this.updateFiletreeList.OptionsView.ShowIndicator = false;
            this.updateFiletreeList.OptionsView.ShowVertLines = false;
            this.updateFiletreeList.RowSpace = 4;
            this.updateFiletreeList.Size = new System.Drawing.Size(497, 297);
            this.updateFiletreeList.TabIndex = 0;
            // 
            // gcDirectoryName
            // 
            this.gcDirectoryName.AppearanceCell.Options.UseTextOptions = true;
            this.gcDirectoryName.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcDirectoryName.AppearanceHeader.Options.UseTextOptions = true;
            this.gcDirectoryName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcDirectoryName.Caption = "文件夹";
            this.gcDirectoryName.FieldName = "DirectoryName";
            this.gcDirectoryName.MinWidth = 34;
            this.gcDirectoryName.Name = "gcDirectoryName";
            this.gcDirectoryName.Visible = true;
            this.gcDirectoryName.VisibleIndex = 0;
            this.gcDirectoryName.Width = 495;
            // 
            // mediPanelControl2
            // 
            this.mediPanelControl2.Controls.Add(this.medisbtnCancel);
            this.mediPanelControl2.Controls.Add(this.medisbtnOK);
            systemInfoHelper5.ControlAssemblyName = null;
            systemInfoHelper5.ControlClassName = null;
            systemInfoHelper5.ControlForFormName = null;
            systemInfoHelper5.ControlFormAssemblyName = null;
            systemInfoHelper5.ControlFormClassName = null;
            systemInfoHelper5.ControlFormDYCS = null;
            systemInfoHelper5.ControlFormGongNengID = null;
            systemInfoHelper5.ControlFormNameSpace = null;
            systemInfoHelper5.ControlFromYingYongID = null;
            systemInfoHelper5.ControlName = null;
            systemInfoHelper5.ControlNameSpace = null;
            systemInfoHelper5.CurrentControlParentFrm = null;
            systemInfoHelper5.CurrentSystemDBConnStr = null;
            this.mediPanelControl2.developerHelper = systemInfoHelper5;
            this.mediPanelControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.mediPanelControl2.IsDoubleBuffer = false;
            this.mediPanelControl2.Location = new System.Drawing.Point(0, 301);
            this.mediPanelControl2.Name = "mediPanelControl2";
            this.mediPanelControl2.Size = new System.Drawing.Size(501, 38);
            this.mediPanelControl2.TabIndex = 1;
            // 
            // medisbtnCancel
            // 
            this.medisbtnCancel.Appearance.Options.UseTextOptions = true;
            this.medisbtnCancel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            systemInfoHelper3.ControlAssemblyName = null;
            systemInfoHelper3.ControlClassName = null;
            systemInfoHelper3.ControlForFormName = null;
            systemInfoHelper3.ControlFormAssemblyName = null;
            systemInfoHelper3.ControlFormClassName = null;
            systemInfoHelper3.ControlFormDYCS = null;
            systemInfoHelper3.ControlFormGongNengID = null;
            systemInfoHelper3.ControlFormNameSpace = null;
            systemInfoHelper3.ControlFromYingYongID = null;
            systemInfoHelper3.ControlName = null;
            systemInfoHelper3.ControlNameSpace = null;
            systemInfoHelper3.CurrentControlParentFrm = null;
            systemInfoHelper3.CurrentSystemDBConnStr = null;
            this.medisbtnCancel.developerHelper = systemInfoHelper3;
            this.medisbtnCancel.Location = new System.Drawing.Point(421, 7);
            this.medisbtnCancel.Name = "medisbtnCancel";
            this.medisbtnCancel.Size = new System.Drawing.Size(75, 26);
            this.medisbtnCancel.TabIndex = 1;
            this.medisbtnCancel.Text = "取消";
            this.medisbtnCancel.UnboundExpression = null;
            this.medisbtnCancel.Click += new System.EventHandler(this.medisbtnCancel_Click);
            // 
            // medisbtnOK
            // 
            this.medisbtnOK.Appearance.Options.UseTextOptions = true;
            this.medisbtnOK.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            systemInfoHelper4.ControlAssemblyName = null;
            systemInfoHelper4.ControlClassName = null;
            systemInfoHelper4.ControlForFormName = null;
            systemInfoHelper4.ControlFormAssemblyName = null;
            systemInfoHelper4.ControlFormClassName = null;
            systemInfoHelper4.ControlFormDYCS = null;
            systemInfoHelper4.ControlFormGongNengID = null;
            systemInfoHelper4.ControlFormNameSpace = null;
            systemInfoHelper4.ControlFromYingYongID = null;
            systemInfoHelper4.ControlName = null;
            systemInfoHelper4.ControlNameSpace = null;
            systemInfoHelper4.CurrentControlParentFrm = null;
            systemInfoHelper4.CurrentSystemDBConnStr = null;
            this.medisbtnOK.developerHelper = systemInfoHelper4;
            this.medisbtnOK.Location = new System.Drawing.Point(340, 7);
            this.medisbtnOK.Name = "medisbtnOK";
            this.medisbtnOK.Size = new System.Drawing.Size(75, 26);
            this.medisbtnOK.TabIndex = 0;
            this.medisbtnOK.Text = "保存";
            this.medisbtnOK.UnboundExpression = null;
            this.medisbtnOK.Click += new System.EventHandler(this.medisbtnOK_Click);
            // 
            // CustomDownLoadFileFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(501, 339);
            this.Controls.Add(this.mediPanelControl1);
            this.Controls.Add(this.mediPanelControl2);
            this.Name = "CustomDownLoadFileFrm";
            this.Text = "选择更新的文件夹";
            this.Load += new System.EventHandler(this.CustomDownLoadFileFrm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.mediPanelControl1)).EndInit();
            this.mediPanelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.updateFiletreeList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mediPanelControl2)).EndInit();
            this.mediPanelControl2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private MediPanelControl mediPanelControl1;
        private MediPanelControl mediPanelControl2;
        private MediButton medisbtnCancel;
        private MediButton medisbtnOK;
        private MediTreeList updateFiletreeList;
        private DevExpress.XtraTreeList.Columns.TreeListColumn gcDirectoryName;
    }
}