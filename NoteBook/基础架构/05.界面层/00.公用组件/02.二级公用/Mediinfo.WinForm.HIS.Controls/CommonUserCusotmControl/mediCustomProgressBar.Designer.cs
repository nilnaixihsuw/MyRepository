namespace Mediinfo.WinForm.HIS.Controls
{
    partial class mediCustomProgressBar
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.circleProgressControl1 = new Mediinfo.WinForm.CircleProgressControl();
            this.SuspendLayout();
            // 
            // circleProgressControl1
            // 
            this.circleProgressControl1.AnimationToTextDistance = 45;
            this.circleProgressControl1.Description = "医院目录正在上传中...";
            this.circleProgressControl1.FrameInterval = 100;
            this.circleProgressControl1.Location = new System.Drawing.Point(0, 0);
            this.circleProgressControl1.MaximumSize = new System.Drawing.Size(280, 79);
            this.circleProgressControl1.MinimumSize = new System.Drawing.Size(280, 79);
            this.circleProgressControl1.Name = "circleProgressControl1";
            this.circleProgressControl1.Size = new System.Drawing.Size(280, 79);
            this.circleProgressControl1.TabIndex = 1;
            this.circleProgressControl1.Text = "circleProgressControl1";
            // 
            // mediCustomProgressBar
            // 
            this.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.Controls.Add(this.circleProgressControl1);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximumSize = new System.Drawing.Size(280, 79);
            this.MinimumSize = new System.Drawing.Size(280, 79);
            this.Name = "mediCustomProgressBar";
            this.Size = new System.Drawing.Size(280, 79);
            this.Load += new System.EventHandler(this.mediCustomProgressBar_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private CircleProgressControl circleProgressControl1;
    }
}
