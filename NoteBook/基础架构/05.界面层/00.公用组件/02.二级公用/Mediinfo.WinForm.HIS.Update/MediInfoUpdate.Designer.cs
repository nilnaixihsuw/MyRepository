
namespace Mediinfo.WinForm.HIS.Update
{
    partial class MediInfoUpdate
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.mediWaitCircleControl1 = new Mediinfo.WinForm.HIS.Update.MediWaitCircleControl();
            this.SuspendLayout();
            // 
            // mediWaitCircleControl1
            // 
            this.mediWaitCircleControl1.Activate = true;
            this.mediWaitCircleControl1.Description = "正在更新启动程序并重启，请稍后. . .";
            this.mediWaitCircleControl1.DescriptionFont = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mediWaitCircleControl1.DescriptionFontColor = System.Drawing.SystemColors.MenuHighlight;
            this.mediWaitCircleControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mediWaitCircleControl1.EndCap = System.Drawing.Drawing2D.LineCap.Flat;
            this.mediWaitCircleControl1.HotSpokeColor = System.Drawing.Color.Gray;
            this.mediWaitCircleControl1.InnerRadius = 10.5F;
            this.mediWaitCircleControl1.Location = new System.Drawing.Point(0, 0);
            this.mediWaitCircleControl1.Name = "mediWaitCircleControl1";
            this.mediWaitCircleControl1.NumberOfSpokes = 8;
            this.mediWaitCircleControl1.Size = new System.Drawing.Size(557, 52);
            this.mediWaitCircleControl1.SpokeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.mediWaitCircleControl1.StartCap = System.Drawing.Drawing2D.LineCap.Flat;
            this.mediWaitCircleControl1.TabIndex = 0;
            this.mediWaitCircleControl1.Text = "mediWaitCircleControl1";
            this.mediWaitCircleControl1.Thickness = 3F;
            // 
            // MediInfoUpdate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(557, 52);
            this.Controls.Add(this.mediWaitCircleControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MediInfoUpdate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MediInfoUpdate";
            this.Load += new System.EventHandler(this.MediInfoUpdate_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private MediWaitCircleControl mediWaitCircleControl1;
    }
}

