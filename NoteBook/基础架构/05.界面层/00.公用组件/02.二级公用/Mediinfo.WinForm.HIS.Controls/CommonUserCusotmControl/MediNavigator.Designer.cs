namespace Mediinfo.WinForm.HIS.Controls
{
    partial class MediNavigator
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
            this.toolStripEx1 = new Mediinfo.WinForm.HIS.Controls.ToolStripEx();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.allCount = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.pageSize = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabel5 = new System.Windows.Forms.ToolStripLabel();
            this.simpleButtonEnd = new System.Windows.Forms.ToolStripButton();
            this.simpleButtonNext = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel19 = new System.Windows.Forms.ToolStripLabel();
            this.textEditAllPageCount = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabel16 = new System.Windows.Forms.ToolStripLabel();
            this.textEditCurPage = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabel14 = new System.Windows.Forms.ToolStripLabel();
            this.simpleButtonPre = new System.Windows.Forms.ToolStripButton();
            this.simpleButtonFirst = new System.Windows.Forms.ToolStripButton();
            this.toolStripEx1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripEx1
            // 
            this.toolStripEx1.AutoSize = false;
            this.toolStripEx1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripEx1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.allCount,
            this.toolStripLabel3,
            this.pageSize,
            this.toolStripLabel2,
            this.toolStripLabel5,
            this.simpleButtonEnd,
            this.simpleButtonNext,
            this.toolStripLabel19,
            this.textEditAllPageCount,
            this.toolStripLabel16,
            this.textEditCurPage,
            this.toolStripLabel14,
            this.simpleButtonPre,
            this.simpleButtonFirst});
            this.toolStripEx1.Location = new System.Drawing.Point(0, 0);
            this.toolStripEx1.MaximumSize = new System.Drawing.Size(0, 60);
            this.toolStripEx1.MinimumSize = new System.Drawing.Size(407, 0);
            this.toolStripEx1.Name = "toolStripEx1";
            this.toolStripEx1.Size = new System.Drawing.Size(439, 30);
            this.toolStripEx1.TabIndex = 0;
            this.toolStripEx1.Text = "toolStripEx1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(20, 27);
            this.toolStripLabel1.Text = "共";
            // 
            // allCount
            // 
            this.allCount.Name = "allCount";
            this.allCount.Size = new System.Drawing.Size(15, 27);
            this.allCount.Text = "n";
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(85, 27);
            this.toolStripLabel3.Text = "条 / 每页显示 ";
            // 
            // pageSize
            // 
            this.pageSize.Name = "pageSize";
            this.pageSize.Size = new System.Drawing.Size(19, 27);
            this.pageSize.Text = "m";
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(20, 27);
            this.toolStripLabel2.Text = "条";
            // 
            // toolStripLabel5
            // 
            this.toolStripLabel5.Name = "toolStripLabel5";
            this.toolStripLabel5.Size = new System.Drawing.Size(0, 27);
            // 
            // simpleButtonEnd
            // 
            this.simpleButtonEnd.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.simpleButtonEnd.AutoSize = false;
            this.simpleButtonEnd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.simpleButtonEnd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.simpleButtonEnd.Image = global::Mediinfo.WinForm.HIS.Controls.Properties.Resources.pagination_last;
            this.simpleButtonEnd.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.simpleButtonEnd.ImageTransparentColor = System.Drawing.Color.Transparent;
            this.simpleButtonEnd.Name = "simpleButtonEnd";
            this.simpleButtonEnd.Size = new System.Drawing.Size(34, 29);
            this.simpleButtonEnd.Text = "尾页";
            this.simpleButtonEnd.Click += new System.EventHandler(this.simpleButtonEnd_Click);
            this.simpleButtonEnd.EnabledChanged += new System.EventHandler(this.simpleButtonEnd_EnabledChanged);
            this.simpleButtonEnd.MouseLeave += new System.EventHandler(this.simpleButtonEnd_MouseLeave);
            this.simpleButtonEnd.MouseHover += new System.EventHandler(this.simpleButtonEnd_MouseHover);
            // 
            // simpleButtonNext
            // 
            this.simpleButtonNext.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.simpleButtonNext.AutoSize = false;
            this.simpleButtonNext.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.simpleButtonNext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.simpleButtonNext.Image = global::Mediinfo.WinForm.HIS.Controls.Properties.Resources.pagination_next;
            this.simpleButtonNext.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.simpleButtonNext.ImageTransparentColor = System.Drawing.Color.Transparent;
            this.simpleButtonNext.Name = "simpleButtonNext";
            this.simpleButtonNext.Size = new System.Drawing.Size(34, 29);
            this.simpleButtonNext.Text = "下一页";
            this.simpleButtonNext.Click += new System.EventHandler(this.simpleButtonNext_Click);
            this.simpleButtonNext.EnabledChanged += new System.EventHandler(this.simpleButtonNext_EnabledChanged);
            this.simpleButtonNext.MouseLeave += new System.EventHandler(this.simpleButtonNext_MouseLeave);
            this.simpleButtonNext.MouseHover += new System.EventHandler(this.simpleButtonNext_MouseHover);
            // 
            // toolStripLabel19
            // 
            this.toolStripLabel19.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripLabel19.Name = "toolStripLabel19";
            this.toolStripLabel19.Size = new System.Drawing.Size(20, 27);
            this.toolStripLabel19.Text = "页";
            // 
            // textEditAllPageCount
            // 
            this.textEditAllPageCount.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.textEditAllPageCount.AutoSize = false;
            this.textEditAllPageCount.Name = "textEditAllPageCount";
            this.textEditAllPageCount.Size = new System.Drawing.Size(22, 22);
            this.textEditAllPageCount.Text = "k";
            // 
            // toolStripLabel16
            // 
            this.toolStripLabel16.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripLabel16.Name = "toolStripLabel16";
            this.toolStripLabel16.Size = new System.Drawing.Size(37, 27);
            this.toolStripLabel16.Text = "页/共";
            // 
            // textEditCurPage
            // 
            this.textEditCurPage.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.textEditCurPage.Name = "textEditCurPage";
            this.textEditCurPage.Size = new System.Drawing.Size(11, 27);
            this.textEditCurPage.Text = "j";
            // 
            // toolStripLabel14
            // 
            this.toolStripLabel14.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripLabel14.Name = "toolStripLabel14";
            this.toolStripLabel14.Size = new System.Drawing.Size(20, 27);
            this.toolStripLabel14.Text = "第";
            // 
            // simpleButtonPre
            // 
            this.simpleButtonPre.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.simpleButtonPre.AutoSize = false;
            this.simpleButtonPre.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.simpleButtonPre.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.simpleButtonPre.Enabled = false;
            this.simpleButtonPre.Image = global::Mediinfo.WinForm.HIS.Controls.Properties.Resources.pagination_previous;
            this.simpleButtonPre.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.simpleButtonPre.ImageTransparentColor = System.Drawing.Color.Transparent;
            this.simpleButtonPre.Name = "simpleButtonPre";
            this.simpleButtonPre.Size = new System.Drawing.Size(34, 29);
            this.simpleButtonPre.Text = "上一页";
            this.simpleButtonPre.Click += new System.EventHandler(this.simpleButtonPre_Click);
            this.simpleButtonPre.EnabledChanged += new System.EventHandler(this.simpleButtonPre_EnabledChanged);
            this.simpleButtonPre.MouseLeave += new System.EventHandler(this.simpleButtonPre_MouseLeave);
            this.simpleButtonPre.MouseHover += new System.EventHandler(this.simpleButtonPre_MouseHover);
            // 
            // simpleButtonFirst
            // 
            this.simpleButtonFirst.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.simpleButtonFirst.AutoSize = false;
            this.simpleButtonFirst.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.simpleButtonFirst.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.simpleButtonFirst.Enabled = false;
            this.simpleButtonFirst.Image = global::Mediinfo.WinForm.HIS.Controls.Properties.Resources.pagination_first;
            this.simpleButtonFirst.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.simpleButtonFirst.ImageTransparentColor = System.Drawing.Color.Transparent;
            this.simpleButtonFirst.Name = "simpleButtonFirst";
            this.simpleButtonFirst.Size = new System.Drawing.Size(34, 29);
            this.simpleButtonFirst.Text = "首页";
            this.simpleButtonFirst.Click += new System.EventHandler(this.simpleButtonFirst_Click);
            this.simpleButtonFirst.EnabledChanged += new System.EventHandler(this.simpleButtonFirst_EnabledChanged);
            this.simpleButtonFirst.MouseLeave += new System.EventHandler(this.simpleButtonFirst_MouseLeave);
            this.simpleButtonFirst.MouseHover += new System.EventHandler(this.simpleButtonFirst_MouseHover);
            // 
            // MediNavigator
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.Controls.Add(this.toolStripEx1);
            this.MaximumSize = new System.Drawing.Size(0, 35);
            this.Name = "MediNavigator";
            this.Size = new System.Drawing.Size(439, 30);
            this.toolStripEx1.ResumeLayout(false);
            this.toolStripEx1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ToolStripEx toolStripEx1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripLabel allCount;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripLabel textEditAllPageCount;
        private System.Windows.Forms.ToolStripLabel toolStripLabel19;
        private System.Windows.Forms.ToolStripLabel toolStripLabel16;
        private System.Windows.Forms.ToolStripLabel toolStripLabel14;
        private System.Windows.Forms.ToolStripButton simpleButtonPre;
        private System.Windows.Forms.ToolStripButton simpleButtonFirst;
        private System.Windows.Forms.ToolStripLabel toolStripLabel5;
        private System.Windows.Forms.ToolStripButton simpleButtonNext;
        private System.Windows.Forms.ToolStripButton simpleButtonEnd;
        private System.Windows.Forms.ToolStripLabel pageSize;
        private System.Windows.Forms.ToolStripLabel textEditCurPage;
    }
}
