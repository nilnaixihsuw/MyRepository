namespace Mediinfo.WinForm.HIS.Controls
{
    partial class MediDataNavigator
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MediDataNavigator));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.allCount = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.pageSize = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel4 = new System.Windows.Forms.ToolStripLabel();
            this.simpleButtonEnd = new System.Windows.Forms.ToolStripButton();
            this.simpleButtonNext = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel5 = new System.Windows.Forms.ToolStripLabel();
            this.textEditAllPageCount = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabel7 = new System.Windows.Forms.ToolStripLabel();
            this.textEditCurPage = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel8 = new System.Windows.Forms.ToolStripLabel();
            this.simpleButtonPre = new System.Windows.Forms.ToolStripButton();
            this.simpleButtonFirst = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.allCount,
            this.toolStripLabel3,
            this.pageSize,
            this.toolStripLabel4,
            this.simpleButtonEnd,
            this.simpleButtonNext,
            this.toolStripLabel5,
            this.textEditAllPageCount,
            this.toolStripLabel7,
            this.textEditCurPage,
            this.toolStripLabel8,
            this.simpleButtonPre,
            this.simpleButtonFirst});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.MaximumSize = new System.Drawing.Size(0, 52);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.toolStrip1.ShowItemToolTips = false;
            this.toolStrip1.Size = new System.Drawing.Size(684, 36);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Margin = new System.Windows.Forms.Padding(8, 1, 0, 2);
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(20, 33);
            this.toolStripLabel1.Text = "共";
            // 
            // allCount
            // 
            this.allCount.Name = "allCount";
            this.allCount.Size = new System.Drawing.Size(23, 33);
            this.allCount.Text = " n ";
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(85, 33);
            this.toolStripLabel3.Text = "条 / 每页显示 ";
            // 
            // pageSize
            // 
            this.pageSize.Margin = new System.Windows.Forms.Padding(1, 1, 1, 2);
            this.pageSize.Name = "pageSize";
            this.pageSize.Size = new System.Drawing.Size(43, 33);
            this.pageSize.KeyDown += new System.Windows.Forms.KeyEventHandler(this.pageSize_KeyDown);
            // 
            // toolStripLabel4
            // 
            this.toolStripLabel4.Name = "toolStripLabel4";
            this.toolStripLabel4.Size = new System.Drawing.Size(20, 33);
            this.toolStripLabel4.Text = "条";
            // 
            // simpleButtonEnd
            // 
            this.simpleButtonEnd.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.simpleButtonEnd.AutoToolTip = false;
            this.simpleButtonEnd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.simpleButtonEnd.ForeColor = System.Drawing.Color.Transparent;
            this.simpleButtonEnd.Image = ((System.Drawing.Image)(resources.GetObject("simpleButtonEnd.Image")));
            this.simpleButtonEnd.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.simpleButtonEnd.ImageTransparentColor = System.Drawing.Color.Transparent;
            this.simpleButtonEnd.Margin = new System.Windows.Forms.Padding(0, 1, 8, 2);
            this.simpleButtonEnd.Name = "simpleButtonEnd";
            this.simpleButtonEnd.Size = new System.Drawing.Size(38, 33);
            this.simpleButtonEnd.Text = "toolStripButton1";
            this.simpleButtonEnd.Click += new System.EventHandler(this.simpleButtonEnd_Click);
            this.simpleButtonEnd.EnabledChanged += new System.EventHandler(this.simpleButtonEnd_EnabledChanged);
            this.simpleButtonEnd.MouseLeave += new System.EventHandler(this.simpleButtonEnd_MouseLeave);
            this.simpleButtonEnd.MouseHover += new System.EventHandler(this.simpleButtonEnd_MouseHover);
            // 
            // simpleButtonNext
            // 
            this.simpleButtonNext.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.simpleButtonNext.AutoToolTip = false;
            this.simpleButtonNext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.simpleButtonNext.ForeColor = System.Drawing.Color.Transparent;
            this.simpleButtonNext.Image = ((System.Drawing.Image)(resources.GetObject("simpleButtonNext.Image")));
            this.simpleButtonNext.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.simpleButtonNext.ImageTransparentColor = System.Drawing.Color.Transparent;
            this.simpleButtonNext.Margin = new System.Windows.Forms.Padding(8, 1, 8, 2);
            this.simpleButtonNext.Name = "simpleButtonNext";
            this.simpleButtonNext.Size = new System.Drawing.Size(38, 33);
            this.simpleButtonNext.Text = "toolStripButton2";
            this.simpleButtonNext.Click += new System.EventHandler(this.simpleButtonNext_Click);
            this.simpleButtonNext.EnabledChanged += new System.EventHandler(this.simpleButtonNext_EnabledChanged);
            this.simpleButtonNext.MouseLeave += new System.EventHandler(this.simpleButtonNext_MouseLeave);
            this.simpleButtonNext.MouseHover += new System.EventHandler(this.simpleButtonNext_MouseHover);
            // 
            // toolStripLabel5
            // 
            this.toolStripLabel5.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripLabel5.Name = "toolStripLabel5";
            this.toolStripLabel5.Size = new System.Drawing.Size(20, 33);
            this.toolStripLabel5.Text = "页";
            // 
            // textEditAllPageCount
            // 
            this.textEditAllPageCount.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.textEditAllPageCount.Name = "textEditAllPageCount";
            this.textEditAllPageCount.Size = new System.Drawing.Size(23, 33);
            this.textEditAllPageCount.Text = " k ";
            // 
            // toolStripLabel7
            // 
            this.toolStripLabel7.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripLabel7.Name = "toolStripLabel7";
            this.toolStripLabel7.Size = new System.Drawing.Size(53, 33);
            this.toolStripLabel7.Text = " 页 / 共 ";
            // 
            // textEditCurPage
            // 
            this.textEditCurPage.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.textEditCurPage.Margin = new System.Windows.Forms.Padding(1, 1, 1, 2);
            this.textEditCurPage.Name = "textEditCurPage";
            this.textEditCurPage.Size = new System.Drawing.Size(43, 33);
            this.textEditCurPage.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textEditCurPage_KeyDown);
            // 
            // toolStripLabel8
            // 
            this.toolStripLabel8.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripLabel8.Name = "toolStripLabel8";
            this.toolStripLabel8.Size = new System.Drawing.Size(20, 33);
            this.toolStripLabel8.Text = "第";
            // 
            // simpleButtonPre
            // 
            this.simpleButtonPre.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.simpleButtonPre.AutoToolTip = false;
            this.simpleButtonPre.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.simpleButtonPre.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.simpleButtonPre.ForeColor = System.Drawing.Color.Transparent;
            this.simpleButtonPre.Image = ((System.Drawing.Image)(resources.GetObject("simpleButtonPre.Image")));
            this.simpleButtonPre.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.simpleButtonPre.ImageTransparentColor = System.Drawing.Color.Transparent;
            this.simpleButtonPre.Margin = new System.Windows.Forms.Padding(8, 1, 8, 2);
            this.simpleButtonPre.Name = "simpleButtonPre";
            this.simpleButtonPre.Size = new System.Drawing.Size(38, 33);
            this.simpleButtonPre.Text = "toolStripButton3";
            this.simpleButtonPre.Click += new System.EventHandler(this.simpleButtonPre_Click);
            this.simpleButtonPre.EnabledChanged += new System.EventHandler(this.simpleButtonPre_EnabledChanged);
            this.simpleButtonPre.MouseLeave += new System.EventHandler(this.simpleButtonPre_MouseLeave);
            this.simpleButtonPre.MouseHover += new System.EventHandler(this.simpleButtonPre_MouseHover);
            // 
            // simpleButtonFirst
            // 
            this.simpleButtonFirst.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.simpleButtonFirst.AutoToolTip = false;
            this.simpleButtonFirst.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.simpleButtonFirst.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.simpleButtonFirst.ForeColor = System.Drawing.Color.White;
            this.simpleButtonFirst.Image = ((System.Drawing.Image)(resources.GetObject("simpleButtonFirst.Image")));
            this.simpleButtonFirst.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.simpleButtonFirst.ImageTransparentColor = System.Drawing.Color.Transparent;
            this.simpleButtonFirst.Name = "simpleButtonFirst";
            this.simpleButtonFirst.Size = new System.Drawing.Size(38, 33);
            this.simpleButtonFirst.Text = "toolStripButton4";
            this.simpleButtonFirst.Click += new System.EventHandler(this.simpleButtonFirst_Click);
            this.simpleButtonFirst.EnabledChanged += new System.EventHandler(this.simpleButtonFirst_EnabledChanged);
            this.simpleButtonFirst.MouseLeave += new System.EventHandler(this.simpleButtonFirst_MouseLeave);
            this.simpleButtonFirst.MouseHover += new System.EventHandler(this.simpleButtonFirst_MouseHover);
            // 
            // MediDataNavigator
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.Controls.Add(this.toolStrip1);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.MaximumSize = new System.Drawing.Size(0, 42);
            this.MinimumSize = new System.Drawing.Size(600, 0);
            this.Name = "MediDataNavigator";
            this.Size = new System.Drawing.Size(684, 36);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripLabel allCount;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private System.Windows.Forms.ToolStripTextBox pageSize;
        private System.Windows.Forms.ToolStripLabel toolStripLabel4;
        private System.Windows.Forms.ToolStripButton simpleButtonEnd;
        private System.Windows.Forms.ToolStripButton simpleButtonNext;
        private System.Windows.Forms.ToolStripLabel toolStripLabel5;
        private System.Windows.Forms.ToolStripLabel textEditAllPageCount;
        private System.Windows.Forms.ToolStripLabel toolStripLabel7;
        private System.Windows.Forms.ToolStripTextBox textEditCurPage;
        private System.Windows.Forms.ToolStripLabel toolStripLabel8;
        private System.Windows.Forms.ToolStripButton simpleButtonPre;
        private System.Windows.Forms.ToolStripButton simpleButtonFirst;
    }
}
