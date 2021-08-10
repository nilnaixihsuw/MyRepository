namespace Mediinfo.WinForm.HIS.Controls
{
    partial class MediFormWithQX
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // MediFormWithQX
            // 
            this.Appearance.Options.UseBackColor = true;
            this.ClientSize = new System.Drawing.Size(1008, 601);
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "MediFormWithQX";
            this.Padding = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MediFormWithQX_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MediFormWithQX_FormClosed);
            this.SizeChanged += new System.EventHandler(this.MediFormWithQX_SizeChanged);
            this.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.Ctr_MouseDoubleClick);
            this.ResumeLayout(false);

        }





        #endregion
    }
}
