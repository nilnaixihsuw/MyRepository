namespace Mediinfo.WinForm.HIS.Controls
{
    partial class CustomProgressBarFrm
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
            this.mediCustomProgressBar1 = new Mediinfo.WinForm.HIS.Controls.mediCustomProgressBar();
            this.SuspendLayout();
            // 
            // mediCustomProgressBar1
            // 
            this.mediCustomProgressBar1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mediCustomProgressBar1.IsVisiblePercent = true;
            this.mediCustomProgressBar1.Location = new System.Drawing.Point(0, 0);
            this.mediCustomProgressBar1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.mediCustomProgressBar1.MaximumSize = new System.Drawing.Size(280, 79);
            this.mediCustomProgressBar1.MinimumSize = new System.Drawing.Size(280, 79);
            this.mediCustomProgressBar1.Name = "mediCustomProgressBar1";
            this.mediCustomProgressBar1.Size = new System.Drawing.Size(280, 79);
            this.mediCustomProgressBar1.TabIndex = 0;
            this.mediCustomProgressBar1.Totaluploadedcount = 0;
            this.mediCustomProgressBar1.UploadCountDes = "医院目录正在上传中...";
            this.mediCustomProgressBar1.UploadedFilePercentProcess = "";
            this.mediCustomProgressBar1.UpLoadFileInfo = "请稍后";
            // 
            // CustomProgressBarFrm
            // 
          
            this.ClientSize = new System.Drawing.Size(280, 79);
            this.Controls.Add(this.mediCustomProgressBar1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximumSize = new System.Drawing.Size(280, 79);
            this.MinimumSize = new System.Drawing.Size(280, 79);
            this.Name = "CustomProgressBarFrm";
            this.Text = "CustomProgressBarFrm";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.CustomProgressBarFrm_Paint);
            this.ResumeLayout(false);

        }

        #endregion

        private mediCustomProgressBar mediCustomProgressBar1;
    }
}