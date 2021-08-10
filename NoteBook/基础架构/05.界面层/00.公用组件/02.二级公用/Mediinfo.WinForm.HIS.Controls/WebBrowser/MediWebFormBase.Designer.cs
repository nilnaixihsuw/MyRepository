namespace Mediinfo.WinForm.HIS.Controls
{
    partial class MediWebFormBase
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
            this.mediProgressPanel1 = new Mediinfo.WinForm.MediProgressPanel();
            ((System.ComponentModel.ISupportInitialize)(this.mediPanelControl1)).BeginInit();
            this.mediPanelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mediPanelControl1
            // 
            this.mediPanelControl1.Controls.Add(this.mediProgressPanel1);
            this.mediPanelControl1.developerHelper = null;
            this.mediPanelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mediPanelControl1.IsDoubleBuffer = false;
            this.mediPanelControl1.IsHiddedTopBorder = false;
            this.mediPanelControl1.Location = new System.Drawing.Point(0, 0);
            this.mediPanelControl1.Name = "mediPanelControl1";
            this.mediPanelControl1.Size = new System.Drawing.Size(800, 450);
            this.mediPanelControl1.TabIndex = 0;
            // 
            // mediProgressPanel1
            // 
            this.mediProgressPanel1.Appearance.BackColor = System.Drawing.Color.White;
            this.mediProgressPanel1.Appearance.Options.UseBackColor = true;
            this.mediProgressPanel1.AutoHeight = true;
            this.mediProgressPanel1.AutoWidth = true;
            this.mediProgressPanel1.BarAnimationElementThickness = 2;
            this.mediProgressPanel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.mediProgressPanel1.Caption = "正在初始化资源";
            this.mediProgressPanel1.Description = "请等待 ...";
            this.mediProgressPanel1.Location = new System.Drawing.Point(322, 183);
            this.mediProgressPanel1.Name = "mediProgressPanel1";
            this.mediProgressPanel1.ShowDescription = false;
            this.mediProgressPanel1.Size = new System.Drawing.Size(129, 43);
            this.mediProgressPanel1.TabIndex = 0;
            this.mediProgressPanel1.Text = "mediProgressPanel1";
            this.mediProgressPanel1.UseWaitCursor = true;
            this.mediProgressPanel1.WaitAnimationType = DevExpress.Utils.Animation.WaitingAnimatorType.Line;
            // 
            // MediWebFormBase
            // 
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.mediPanelControl1);
            this.LookAndFeel.SkinName = "mediskindevexpressstyle";
            this.Name = "MediWebFormBase";
            this.Text = "MediWebFormBase";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MediWebFormBase_Load);
            ((System.ComponentModel.ISupportInitialize)(this.mediPanelControl1)).EndInit();
            this.mediPanelControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private MediPanelControl mediPanelControl1;
        private MediProgressPanel mediProgressPanel1;
    }
}