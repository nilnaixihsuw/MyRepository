namespace Mediinfo.WinForm.HIS.Controls
{
    partial class MediPromptBox
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
            this.mediPanelControl1 = new Mediinfo.WinForm.MediPanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.mediPanelControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // mediPanelControl1
            // 
            this.mediPanelControl1.Appearance.BackColor = System.Drawing.Color.Black;
            this.mediPanelControl1.Appearance.Options.UseBackColor = true;
            this.mediPanelControl1.BorderSize = 1;
            this.mediPanelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.mediPanelControl1.CustomBorderColor = System.Drawing.Color.White;
            this.mediPanelControl1.developerHelper = null;
            this.mediPanelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mediPanelControl1.IsDoubleBuffer = false;
            this.mediPanelControl1.IsHiddedTopBorder = false;
            this.mediPanelControl1.IsShowBorderColor = false;
            this.mediPanelControl1.Location = new System.Drawing.Point(0, 0);
            this.mediPanelControl1.Name = "mediPanelControl1";
            this.mediPanelControl1.Size = new System.Drawing.Size(333, 86);
            this.mediPanelControl1.TabIndex = 0;
            this.mediPanelControl1.Paint += new System.Windows.Forms.PaintEventHandler(this.mediPanelControl1_Paint);
            // 
            // MediPromptBox
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(333, 86);
            this.Controls.Add(this.mediPanelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MediPromptBox";
            this.Opacity = 0.85D;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MediPromptBox";
            this.TransparencyKey = System.Drawing.Color.White;
            this.Load += new System.EventHandler(this.MediPromptBox_Load);
            this.Shown += new System.EventHandler(this.MediPromptBox_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.mediPanelControl1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private MediPanelControl mediPanelControl1;
    }
}