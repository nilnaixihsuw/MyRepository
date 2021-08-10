namespace Mediinfo.WinForm.HIS.Controls.SecondaryFramework
{
    partial class W_BINGRENKJ_BASE
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
            this.ddOtherMenuBtn = new DevExpress.XtraEditors.DropDownButton();
            this.otherMenuPM = new Mediinfo.WinForm.MediPopupMenu();
            this.otherMenuBM = new Mediinfo.WinForm.MediBarManager(this.components);
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.mediPanelControl2 = new Mediinfo.WinForm.MediPanelControl(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.mediPanelControl1)).BeginInit();
            this.mediPanelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.otherMenuPM)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.otherMenuBM)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mediPanelControl2)).BeginInit();
            this.SuspendLayout();
            // 
            // mediPanelControl1
            // 
            this.mediPanelControl1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.mediPanelControl1.Appearance.Options.UseBackColor = true;
            this.mediPanelControl1.BorderSize = 1;
            this.mediPanelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.mediPanelControl1.Controls.Add(this.ddOtherMenuBtn);
            this.mediPanelControl1.CustomBorderColor = System.Drawing.Color.White;
            this.mediPanelControl1.developerHelper = null;
            this.mediPanelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.mediPanelControl1.IsDoubleBuffer = false;
            this.mediPanelControl1.IsHiddedTopBorder = false;
            this.mediPanelControl1.IsShowBorderColor = false;
            this.mediPanelControl1.Location = new System.Drawing.Point(0, 0);
            this.mediPanelControl1.Name = "mediPanelControl1";
            this.mediPanelControl1.Size = new System.Drawing.Size(1026, 67);
            this.mediPanelControl1.TabIndex = 1;
            // 
            // ddOtherMenuBtn
            // 
            this.ddOtherMenuBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ddOtherMenuBtn.Appearance.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.ddOtherMenuBtn.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(115)))), ((int)(((byte)(195)))));
            this.ddOtherMenuBtn.Appearance.Options.UseFont = true;
            this.ddOtherMenuBtn.Appearance.Options.UseForeColor = true;
            this.ddOtherMenuBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ddOtherMenuBtn.DropDownArrowStyle = DevExpress.XtraEditors.DropDownArrowStyle.Show;
            this.ddOtherMenuBtn.DropDownControl = this.otherMenuPM;
            this.ddOtherMenuBtn.Location = new System.Drawing.Point(911, 17);
            this.ddOtherMenuBtn.Name = "ddOtherMenuBtn";
            this.otherMenuBM.SetPopupContextMenu(this.ddOtherMenuBtn, this.otherMenuPM);
            this.ddOtherMenuBtn.Size = new System.Drawing.Size(96, 32);
            this.ddOtherMenuBtn.TabIndex = 0;
            this.ddOtherMenuBtn.Text = "其他操作";
            // 
            // otherMenuPM
            // 
            this.otherMenuPM.developerHelper = null;
            this.otherMenuPM.Manager = this.otherMenuBM;
            this.otherMenuPM.MenuAppearance.AppearanceMenu.Hovered.BackColor = System.Drawing.Color.White;
            this.otherMenuPM.MenuAppearance.AppearanceMenu.Hovered.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(115)))), ((int)(((byte)(195)))));
            this.otherMenuPM.MenuAppearance.AppearanceMenu.Hovered.Options.UseBackColor = true;
            this.otherMenuPM.MenuAppearance.AppearanceMenu.Hovered.Options.UseForeColor = true;
            this.otherMenuPM.MenuAppearance.AppearanceMenu.Normal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(240)))), ((int)(((byte)(249)))));
            this.otherMenuPM.MenuAppearance.AppearanceMenu.Normal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(115)))), ((int)(((byte)(195)))));
            this.otherMenuPM.MenuAppearance.AppearanceMenu.Normal.Options.UseBackColor = true;
            this.otherMenuPM.MenuAppearance.AppearanceMenu.Normal.Options.UseForeColor = true;
            this.otherMenuPM.MenuAppearance.AppearanceMenu.Pressed.BackColor = System.Drawing.Color.White;
            this.otherMenuPM.MenuAppearance.AppearanceMenu.Pressed.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(115)))), ((int)(((byte)(195)))));
            this.otherMenuPM.MenuAppearance.AppearanceMenu.Pressed.Options.UseBackColor = true;
            this.otherMenuPM.MenuAppearance.AppearanceMenu.Pressed.Options.UseForeColor = true;
            this.otherMenuPM.MenuAppearance.HeaderItemAppearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(240)))), ((int)(((byte)(249)))));
            this.otherMenuPM.MenuAppearance.HeaderItemAppearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(115)))), ((int)(((byte)(195)))));
            this.otherMenuPM.MenuAppearance.HeaderItemAppearance.Options.UseBackColor = true;
            this.otherMenuPM.MenuAppearance.HeaderItemAppearance.Options.UseForeColor = true;
            this.otherMenuPM.MenuAppearance.MenuBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(240)))), ((int)(((byte)(249)))));
            this.otherMenuPM.MenuAppearance.MenuBar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(115)))), ((int)(((byte)(195)))));
            this.otherMenuPM.MenuAppearance.MenuBar.Options.UseBackColor = true;
            this.otherMenuPM.MenuAppearance.MenuBar.Options.UseForeColor = true;
            this.otherMenuPM.MenuAppearance.MenuCaption.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(240)))), ((int)(((byte)(249)))));
            this.otherMenuPM.MenuAppearance.MenuCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(115)))), ((int)(((byte)(195)))));
            this.otherMenuPM.MenuAppearance.MenuCaption.Options.UseBackColor = true;
            this.otherMenuPM.MenuAppearance.MenuCaption.Options.UseForeColor = true;
            this.otherMenuPM.Name = "otherMenuPM";
            // 
            // otherMenuBM
            // 
            this.otherMenuBM.developerHelper = null;
            this.otherMenuBM.DockControls.Add(this.barDockControlTop);
            this.otherMenuBM.DockControls.Add(this.barDockControlBottom);
            this.otherMenuBM.DockControls.Add(this.barDockControlLeft);
            this.otherMenuBM.DockControls.Add(this.barDockControlRight);
            this.otherMenuBM.Form = this;
            this.otherMenuBM.MaxItemId = 0;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.otherMenuBM;
            this.barDockControlTop.Size = new System.Drawing.Size(1026, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 576);
            this.barDockControlBottom.Manager = this.otherMenuBM;
            this.barDockControlBottom.Size = new System.Drawing.Size(1026, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Manager = this.otherMenuBM;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 576);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1026, 0);
            this.barDockControlRight.Manager = this.otherMenuBM;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 576);
            // 
            // mediPanelControl2
            // 
            this.mediPanelControl2.Appearance.BackColor = System.Drawing.Color.White;
            this.mediPanelControl2.Appearance.Options.UseBackColor = true;
            this.mediPanelControl2.BorderSize = 1;
            this.mediPanelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.mediPanelControl2.CustomBorderColor = System.Drawing.Color.White;
            this.mediPanelControl2.developerHelper = null;
            this.mediPanelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mediPanelControl2.IsDoubleBuffer = false;
            this.mediPanelControl2.IsHiddedTopBorder = false;
            this.mediPanelControl2.IsShowBorderColor = false;
            this.mediPanelControl2.Location = new System.Drawing.Point(0, 67);
            this.mediPanelControl2.Name = "mediPanelControl2";
            this.mediPanelControl2.Size = new System.Drawing.Size(1026, 509);
            this.mediPanelControl2.TabIndex = 6;
            // 
            // W_BINGRENKJ_BASE
            // 
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(1026, 576);
            this.Controls.Add(this.mediPanelControl2);
            this.Controls.Add(this.mediPanelControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.LookAndFeel.SkinName = "mediskindevexpressstyle";
            this.Name = "W_BINGRENKJ_BASE";
            this.Padding = new System.Windows.Forms.Padding(0);
            this.Text = "W_BINGREN_BASE";
            this.Load += new System.EventHandler(this.W_BINGREN_BASE_Load);
            ((System.ComponentModel.ISupportInitialize)(this.mediPanelControl1)).EndInit();
            this.mediPanelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.otherMenuPM)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.otherMenuBM)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mediPanelControl2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private MediPopupMenu otherMenuPM;
        private MediBarManager otherMenuBM;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private MediPanelControl mediPanelControl2;
        public MediPanelControl mediPanelControl1;
        public DevExpress.XtraEditors.DropDownButton ddOtherMenuBtn;
    }
}