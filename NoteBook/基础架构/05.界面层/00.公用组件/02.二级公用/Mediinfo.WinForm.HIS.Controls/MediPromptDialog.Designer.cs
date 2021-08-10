namespace Mediinfo.WinForm.HIS.Controls
{
    partial class MediPromptDialog
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
            Mediinfo.WinForm.SystemInfoHelper systemInfoHelper4 = new Mediinfo.WinForm.SystemInfoHelper();
            Mediinfo.WinForm.SystemInfoHelper systemInfoHelper1 = new Mediinfo.WinForm.SystemInfoHelper();
            Mediinfo.WinForm.SystemInfoHelper systemInfoHelper2 = new Mediinfo.WinForm.SystemInfoHelper();
            Mediinfo.WinForm.SystemInfoHelper systemInfoHelper3 = new Mediinfo.WinForm.SystemInfoHelper();
            this.mediPanelControl1 = new Mediinfo.WinForm.MediPanelControl(this.components);
            this.mediLabel2 = new Mediinfo.WinForm.MediLabel();
            this.mediLabel1 = new Mediinfo.WinForm.MediLabel();
            this.mediPictureEdit1 = new Mediinfo.WinForm.MediPictureEdit();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.mediPanelControl2 = new Mediinfo.WinForm.MediPanelControl(this.components);
            this.mediPanelControl3 = new Mediinfo.WinForm.MediPanelControl(this.components);
            this.mediButton3 = new Mediinfo.WinForm.MediButton();
            this.mediPanelControl4 = new Mediinfo.WinForm.MediPanelControl(this.components);
            this.mediButton2 = new Mediinfo.WinForm.MediButton();
            this.mediPanelControl5 = new Mediinfo.WinForm.MediPanelControl(this.components);
            this.mediButton1 = new Mediinfo.WinForm.MediButton();
            ((System.ComponentModel.ISupportInitialize)(this.mediPanelControl1)).BeginInit();
            this.mediPanelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mediPictureEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mediPanelControl2)).BeginInit();
            this.mediPanelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mediPanelControl3)).BeginInit();
            this.mediPanelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mediPanelControl4)).BeginInit();
            this.mediPanelControl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mediPanelControl5)).BeginInit();
            this.mediPanelControl5.SuspendLayout();
            this.SuspendLayout();
            // 
            // mediPanelControl1
            // 
            this.mediPanelControl1.BorderSize = 1;
            this.mediPanelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.mediPanelControl1.Controls.Add(this.mediLabel2);
            this.mediPanelControl1.Controls.Add(this.mediLabel1);
            this.mediPanelControl1.Controls.Add(this.mediPictureEdit1);
            this.mediPanelControl1.Controls.Add(this.panelControl3);
            this.mediPanelControl1.CustomBorderColor = System.Drawing.Color.White;
            this.mediPanelControl1.developerHelper = null;
            this.mediPanelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mediPanelControl1.IsDoubleBuffer = false;
            this.mediPanelControl1.IsHiddedTopBorder = false;
            this.mediPanelControl1.IsShowBorderColor = false;
            this.mediPanelControl1.Location = new System.Drawing.Point(1, 1);
            this.mediPanelControl1.Name = "mediPanelControl1";
            this.mediPanelControl1.Size = new System.Drawing.Size(328, 138);
            this.mediPanelControl1.TabIndex = 0;
            this.mediPanelControl1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mediPanelControl1_MouseDown);
            // 
            // mediLabel2
            // 
            this.mediLabel2.Appearance.Font = new System.Drawing.Font("微软雅黑", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.mediLabel2.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.mediLabel2.Appearance.Options.UseFont = true;
            this.mediLabel2.Appearance.Options.UseForeColor = true;
            this.mediLabel2.developerHelper = null;
            this.mediLabel2.Location = new System.Drawing.Point(61, 59);
            this.mediLabel2.Name = "mediLabel2";
            this.mediLabel2.Size = new System.Drawing.Size(138, 20);
            this.mediLabel2.TabIndex = 13;
            this.mediLabel2.Text = "已成功导出20条数据";
            // 
            // mediLabel1
            // 
            this.mediLabel1.Appearance.Font = new System.Drawing.Font("微软雅黑", 11.25F);
            this.mediLabel1.Appearance.Options.UseFont = true;
            this.mediLabel1.developerHelper = null;
            this.mediLabel1.Location = new System.Drawing.Point(61, 33);
            this.mediLabel1.Name = "mediLabel1";
            this.mediLabel1.Size = new System.Drawing.Size(135, 20);
            this.mediLabel1.TabIndex = 12;
            this.mediLabel1.Text = "是否打印电子病历？";
            // 
            // mediPictureEdit1
            // 
            this.mediPictureEdit1.developerHelper = null;
            this.mediPictureEdit1.EditValue = global::Mediinfo.WinForm.HIS.Controls.Properties.Resources.tixingicon;
            this.mediPictureEdit1.Location = new System.Drawing.Point(28, 33);
            this.mediPictureEdit1.Name = "mediPictureEdit1";
            this.mediPictureEdit1.Properties.AllowFocused = false;
            this.mediPictureEdit1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.mediPictureEdit1.Properties.Appearance.Options.UseBackColor = true;
            this.mediPictureEdit1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.mediPictureEdit1.Properties.developerHelper = null;
            this.mediPictureEdit1.Properties.NullText = " ";
            this.mediPictureEdit1.Properties.RelativeImagePath = "";
            this.mediPictureEdit1.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.mediPictureEdit1.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            this.mediPictureEdit1.Properties.UnboundExpression = null;
            this.mediPictureEdit1.RelativeImagePath = "";
            this.mediPictureEdit1.Size = new System.Drawing.Size(24, 24);
            this.mediPictureEdit1.TabIndex = 11;
            // 
            // panelControl3
            // 
            this.panelControl3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl3.Controls.Add(this.mediPanelControl2);
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl3.Location = new System.Drawing.Point(0, 92);
            this.panelControl3.Margin = new System.Windows.Forms.Padding(4);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(328, 46);
            this.panelControl3.TabIndex = 10;
            // 
            // mediPanelControl2
            // 
            this.mediPanelControl2.Appearance.BackColor = System.Drawing.Color.White;
            this.mediPanelControl2.Appearance.Options.UseBackColor = true;
            this.mediPanelControl2.BorderSize = 1;
            this.mediPanelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.mediPanelControl2.Controls.Add(this.mediPanelControl3);
            this.mediPanelControl2.Controls.Add(this.mediPanelControl4);
            this.mediPanelControl2.Controls.Add(this.mediPanelControl5);
            this.mediPanelControl2.CustomBorderColor = System.Drawing.Color.White;
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
            this.mediPanelControl2.developerHelper = systemInfoHelper4;
            this.mediPanelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mediPanelControl2.IsDoubleBuffer = false;
            this.mediPanelControl2.IsHiddedTopBorder = false;
            this.mediPanelControl2.IsShowBorderColor = false;
            this.mediPanelControl2.Location = new System.Drawing.Point(0, 0);
            this.mediPanelControl2.Margin = new System.Windows.Forms.Padding(4);
            this.mediPanelControl2.Name = "mediPanelControl2";
            this.mediPanelControl2.Padding = new System.Windows.Forms.Padding(0, 0, 5, 0);
            this.mediPanelControl2.Size = new System.Drawing.Size(328, 46);
            this.mediPanelControl2.TabIndex = 11;
            // 
            // mediPanelControl3
            // 
            this.mediPanelControl3.BorderSize = 1;
            this.mediPanelControl3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.mediPanelControl3.Controls.Add(this.mediButton3);
            this.mediPanelControl3.CustomBorderColor = System.Drawing.Color.White;
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
            this.mediPanelControl3.developerHelper = systemInfoHelper1;
            this.mediPanelControl3.Dock = System.Windows.Forms.DockStyle.Right;
            this.mediPanelControl3.IsDoubleBuffer = false;
            this.mediPanelControl3.IsHiddedTopBorder = false;
            this.mediPanelControl3.IsShowBorderColor = false;
            this.mediPanelControl3.Location = new System.Drawing.Point(108, 0);
            this.mediPanelControl3.Margin = new System.Windows.Forms.Padding(4);
            this.mediPanelControl3.Name = "mediPanelControl3";
            this.mediPanelControl3.Size = new System.Drawing.Size(69, 46);
            this.mediPanelControl3.TabIndex = 12;
            // 
            // mediButton3
            // 
            this.mediButton3.Appearance.ForeColor = System.Drawing.Color.Black;
            this.mediButton3.Appearance.Options.UseForeColor = true;
            this.mediButton3.AppearancePressed.BackColor = System.Drawing.Color.White;
            this.mediButton3.AppearancePressed.Options.UseBackColor = true;
            this.mediButton3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.mediButton3.Location = new System.Drawing.Point(4, 10);
            this.mediButton3.Margin = new System.Windows.Forms.Padding(4);
            this.mediButton3.Name = "mediButton3";
            this.mediButton3.Size = new System.Drawing.Size(60, 26);
            this.mediButton3.TabIndex = 8;
            this.mediButton3.Text = "待定";
            this.mediButton3.UnboundExpression = null;
            this.mediButton3.Click += new System.EventHandler(this.mediButton3_Click);
            // 
            // mediPanelControl4
            // 
            this.mediPanelControl4.BorderSize = 1;
            this.mediPanelControl4.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.mediPanelControl4.Controls.Add(this.mediButton2);
            this.mediPanelControl4.CustomBorderColor = System.Drawing.Color.White;
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
            this.mediPanelControl4.developerHelper = systemInfoHelper2;
            this.mediPanelControl4.Dock = System.Windows.Forms.DockStyle.Right;
            this.mediPanelControl4.IsDoubleBuffer = false;
            this.mediPanelControl4.IsHiddedTopBorder = false;
            this.mediPanelControl4.IsShowBorderColor = false;
            this.mediPanelControl4.Location = new System.Drawing.Point(177, 0);
            this.mediPanelControl4.Margin = new System.Windows.Forms.Padding(4);
            this.mediPanelControl4.Name = "mediPanelControl4";
            this.mediPanelControl4.Size = new System.Drawing.Size(69, 46);
            this.mediPanelControl4.TabIndex = 11;
            // 
            // mediButton2
            // 
            this.mediButton2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.mediButton2.Location = new System.Drawing.Point(5, 10);
            this.mediButton2.Margin = new System.Windows.Forms.Padding(4);
            this.mediButton2.Name = "mediButton2";
            this.mediButton2.Size = new System.Drawing.Size(60, 26);
            this.mediButton2.TabIndex = 10;
            this.mediButton2.Text = "待定";
            this.mediButton2.UnboundExpression = null;
            this.mediButton2.Click += new System.EventHandler(this.mediButton2_Click);
            // 
            // mediPanelControl5
            // 
            this.mediPanelControl5.BorderSize = 1;
            this.mediPanelControl5.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.mediPanelControl5.Controls.Add(this.mediButton1);
            this.mediPanelControl5.CustomBorderColor = System.Drawing.Color.White;
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
            this.mediPanelControl5.developerHelper = systemInfoHelper3;
            this.mediPanelControl5.Dock = System.Windows.Forms.DockStyle.Right;
            this.mediPanelControl5.IsDoubleBuffer = false;
            this.mediPanelControl5.IsHiddedTopBorder = false;
            this.mediPanelControl5.IsShowBorderColor = false;
            this.mediPanelControl5.Location = new System.Drawing.Point(246, 0);
            this.mediPanelControl5.Margin = new System.Windows.Forms.Padding(4);
            this.mediPanelControl5.Name = "mediPanelControl5";
            this.mediPanelControl5.Size = new System.Drawing.Size(77, 46);
            this.mediPanelControl5.TabIndex = 12;
            // 
            // mediButton1
            // 
            this.mediButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.mediButton1.Location = new System.Drawing.Point(4, 10);
            this.mediButton1.Margin = new System.Windows.Forms.Padding(4);
            this.mediButton1.MediButtonStyle = Mediinfo.WinForm.ButtonType.BlueButton;
            this.mediButton1.Name = "mediButton1";
            this.mediButton1.Size = new System.Drawing.Size(60, 26);
            this.mediButton1.TabIndex = 9;
            this.mediButton1.Text = "待定";
            this.mediButton1.UnboundExpression = null;
            this.mediButton1.Click += new System.EventHandler(this.mediButton1_Click);
            // 
            // MediPromptDialog
            // 
            this.Appearance.BackColor = System.Drawing.Color.White;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(330, 140);
            this.Controls.Add(this.mediPanelControl1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.LookAndFeel.SkinName = "mediskindevexpressstyle";
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MediPromptDialog";
            this.Padding = new System.Windows.Forms.Padding(1);
            this.ShowInTaskbar = true;
            this.Text = "MediPromptDialog";
            ((System.ComponentModel.ISupportInitialize)(this.mediPanelControl1)).EndInit();
            this.mediPanelControl1.ResumeLayout(false);
            this.mediPanelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mediPictureEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mediPanelControl2)).EndInit();
            this.mediPanelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mediPanelControl3)).EndInit();
            this.mediPanelControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mediPanelControl4)).EndInit();
            this.mediPanelControl4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mediPanelControl5)).EndInit();
            this.mediPanelControl5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private MediPanelControl mediPanelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private MediPanelControl mediPanelControl2;
        private MediPanelControl mediPanelControl3;
        private MediButton mediButton3;
        private MediPanelControl mediPanelControl5;
        private MediButton mediButton1;
        private MediPanelControl mediPanelControl4;
        private MediButton mediButton2;
        private MediPictureEdit mediPictureEdit1;
        private MediLabel mediLabel1;
        private MediLabel mediLabel2;
    }
}