using DevExpress.Utils;
using Mediinfo.WinForm;

namespace Mediinfo.WinForm.HIS.Controls
{
    partial class MediZiDianTYForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MediZiDianTYForm));
            this.splitControl = new Mediinfo.WinForm.MediSplitContainerControl(this.components);
            this.panelLeft = new Mediinfo.WinForm.MediPanelControl(this.components);
            this.titleBarType = new Mediinfo.WinForm.MediTitleBar();
            this.panelMain = new Mediinfo.WinForm.MediPanelControl(this.components);
            this.titleZiDianXx = new Mediinfo.WinForm.MediTitleBar();
            this.mediPanelControl1 = new Mediinfo.WinForm.MediPanelControl(this.components);
            this.textBoxSearch = new Mediinfo.WinForm.MediSearchControl();
            this.cbXianShiZF = new Mediinfo.WinForm.MediCheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitControl)).BeginInit();
            this.splitControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelLeft)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.titleBarType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.titleZiDianXx)).BeginInit();
            this.titleZiDianXx.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mediPanelControl1)).BeginInit();
            this.mediPanelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbXianShiZF.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // splitControl
            // 
            this.splitControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitControl.Location = new System.Drawing.Point(5, 5);
            this.splitControl.Margin = new System.Windows.Forms.Padding(0);
            this.splitControl.Name = "splitControl";
            this.splitControl.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.splitControl.Panel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
            this.splitControl.Panel1.Controls.Add(this.panelLeft);
            this.splitControl.Panel1.Controls.Add(this.titleBarType);
            this.splitControl.Panel1.MinSize = 25;
            this.splitControl.Panel1.Text = "Panel1";
            this.splitControl.Panel2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
            this.splitControl.Panel2.Controls.Add(this.panelMain);
            this.splitControl.Panel2.Controls.Add(this.titleZiDianXx);
            this.splitControl.Panel2.Text = "Panel2";
            this.splitControl.Size = new System.Drawing.Size(791, 439);
            this.splitControl.SplitterPosition = 181;
            this.splitControl.TabIndex = 31;
            this.splitControl.Text = "splitContainerControl1";
            // 
            // panelLeft
            // 
            this.panelLeft.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelLeft.Location = new System.Drawing.Point(0, 25);
            this.panelLeft.Margin = new System.Windows.Forms.Padding(0);
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.Size = new System.Drawing.Size(177, 400);
            this.panelLeft.TabIndex = 33;
            // 
            // titleBarType
            // 
            this.titleBarType.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.titleBarType.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.titleBarType.Dock = System.Windows.Forms.DockStyle.Top;
            this.titleBarType.LabeBackColor = System.Drawing.Color.Empty;
            this.titleBarType.LabeForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(31)))), ((int)(((byte)(53)))));
            this.titleBarType.LabelEnabled = true;
            this.titleBarType.LabelFont = new System.Drawing.Font("微软雅黑", 9F);
            this.titleBarType.LabelSize = new System.Drawing.Size(36, 17);
            this.titleBarType.LabelText = "分类树";
            this.titleBarType.LabelVisible = true;
            this.titleBarType.LabePadding = new System.Windows.Forms.Padding(0);
            this.titleBarType.Location = new System.Drawing.Point(0, 0);
            this.titleBarType.Margin = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.titleBarType.Name = "titleBarType";
            this.titleBarType.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.titleBarType.Size = new System.Drawing.Size(177, 25);
            this.titleBarType.TabIndex = 32;
            this.titleBarType.UnboundExpression = null;
            // 
            // panelMain
            // 
            this.panelMain.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 25);
            this.panelMain.Margin = new System.Windows.Forms.Padding(0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(593, 400);
            this.panelMain.TabIndex = 32;
            // 
            // titleZiDianXx
            // 
            this.titleZiDianXx.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.titleZiDianXx.Controls.Add(this.mediPanelControl1);
            this.titleZiDianXx.Dock = System.Windows.Forms.DockStyle.Top;
            this.titleZiDianXx.LabeBackColor = System.Drawing.Color.Empty;
            this.titleZiDianXx.LabeForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(31)))), ((int)(((byte)(53)))));
            this.titleZiDianXx.LabelEnabled = true;
            this.titleZiDianXx.LabelFont = new System.Drawing.Font("微软雅黑", 9F);
            this.titleZiDianXx.LabelSize = new System.Drawing.Size(48, 17);
            this.titleZiDianXx.LabelText = "字典信息";
            this.titleZiDianXx.LabelVisible = true;
            this.titleZiDianXx.LabePadding = new System.Windows.Forms.Padding(0);
            this.titleZiDianXx.Location = new System.Drawing.Point(0, 0);
            this.titleZiDianXx.Margin = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.titleZiDianXx.Name = "titleZiDianXx";
            this.titleZiDianXx.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.titleZiDianXx.Size = new System.Drawing.Size(593, 25);
            this.titleZiDianXx.TabIndex = 30;
            this.titleZiDianXx.UnboundExpression = null;
            // 
            // mediPanelControl1
            // 
            this.mediPanelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.mediPanelControl1.Controls.Add(this.textBoxSearch);
            this.mediPanelControl1.Controls.Add(this.cbXianShiZF);
            this.mediPanelControl1.Dock = System.Windows.Forms.DockStyle.Right;
            this.mediPanelControl1.Location = new System.Drawing.Point(298, 0);
            this.mediPanelControl1.Name = "mediPanelControl1";
            this.mediPanelControl1.Padding = new System.Windows.Forms.Padding(0, 1, 2, 1);
            this.mediPanelControl1.Size = new System.Drawing.Size(295, 25);
            this.mediPanelControl1.TabIndex = 0;
            // 
            // textBoxSearch
            // 
            this.textBoxSearch.Dock = System.Windows.Forms.DockStyle.Right;
            this.textBoxSearch.Location = new System.Drawing.Point(97, 1);
            this.textBoxSearch.Margin = new System.Windows.Forms.Padding(0);
            this.textBoxSearch.Name = "textBoxSearch";
            this.textBoxSearch.Size = new System.Drawing.Size(196, 20);
            this.textBoxSearch.TabIndex = 1;
            // 
            // cbXianShiZF
            // 
            this.cbXianShiZF.Dock = System.Windows.Forms.DockStyle.Left;
            this.cbXianShiZF.Location = new System.Drawing.Point(0, 1);
            this.cbXianShiZF.Margin = new System.Windows.Forms.Padding(0);
            this.cbXianShiZF.Name = "cbXianShiZF";
            this.cbXianShiZF.Properties.Appearance.Options.UseFont = true;
            this.cbXianShiZF.Properties.AutoHeight = false;
            this.cbXianShiZF.Properties.AutoWidth = true;
            this.cbXianShiZF.Properties.Caption = "显示已作废";
            this.cbXianShiZF.Size = new System.Drawing.Size(82, 23);
            this.cbXianShiZF.TabIndex = 2;
            // 
            // MediZiDianTYForm
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(801, 449);
            this.Controls.Add(this.splitControl);
            this.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.Name = "MediZiDianTYForm";
            this.Text = "MedZiDuanForm";
            ((System.ComponentModel.ISupportInitialize)(this.splitControl)).EndInit();
            this.splitControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelLeft)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.titleBarType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.titleZiDianXx)).EndInit();
            this.titleZiDianXx.ResumeLayout(false);
            this.titleZiDianXx.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mediPanelControl1)).EndInit();
            this.mediPanelControl1.ResumeLayout(false);
            this.mediPanelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbXianShiZF.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        public MediSplitContainerControl splitControl;
        public MediTitleBar titleZiDianXx;
        public MediTitleBar titleBarType;
        public MediPanelControl panelMain;
        public MediPanelControl panelLeft;
        public MediSearchControl textBoxSearch;
        public MediCheckBox cbXianShiZF;
        private MediPanelControl mediPanelControl1;
    }
}