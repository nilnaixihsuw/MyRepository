namespace Mediinfo.WinForm.HIS.Main
{
    partial class MediMainForm
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
            this.SuspendLayout();
            // 
            // MediMainForm
            // 
            this.Appearance.Options.UseBackColor = true;
            this.Appearance.Options.UseFont = true;
            this.ClientSize = new System.Drawing.Size(1008, 696);
            this.LookAndFeel.SkinName = "mediskindevexpressstyle";
            this.Name = "MediMainForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MediMainForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MediMainForm_FormClosed);
            this.Shown += new System.EventHandler(this.MediMainForm_Shown);
            this.SizeChanged += new System.EventHandler(this.MediMainForm_SizeChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }
}