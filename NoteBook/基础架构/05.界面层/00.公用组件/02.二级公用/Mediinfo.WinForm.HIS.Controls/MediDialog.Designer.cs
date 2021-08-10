namespace Mediinfo.WinForm.HIS.Controls
{
    partial class MediDialog
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
            // MediDialog
            // 
            this.Appearance.Options.UseBackColor = true;
            this.ClientSize = new System.Drawing.Size(1008, 601);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.LookAndFeel.SkinName = "mediskindevexpressstyle";
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MediDialog";
            this.Padding = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "对话框";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MediDialog_FormClosed);
            this.Shown += new System.EventHandler(this.MediDialog_Shown);
            this.ResumeLayout(false);

        }

        #endregion
    }
}