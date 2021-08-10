namespace Mediinfo.WinForm.HIS.Controls
{
    partial class UserControl_DiZhi
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
            this.mediTextBoxDiZhi = new Mediinfo.WinForm.MediTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.mediTextBoxDiZhi.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // mediTextBoxDiZhi
            // 
            this.mediTextBoxDiZhi.developerHelper = null;
            this.mediTextBoxDiZhi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mediTextBoxDiZhi.EnterMoveNextControl = true;
            this.mediTextBoxDiZhi.IsOpenEnterNext = false;
            this.mediTextBoxDiZhi.Location = new System.Drawing.Point(0, 0);
            this.mediTextBoxDiZhi.MinimumSize = new System.Drawing.Size(0, 26);
            this.mediTextBoxDiZhi.Name = "mediTextBoxDiZhi";
            this.mediTextBoxDiZhi.Properties.AllowMouseWheel = false;
            this.mediTextBoxDiZhi.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.mediTextBoxDiZhi.Properties.Appearance.Options.UseBackColor = true;
            this.mediTextBoxDiZhi.Properties.developerHelper = null;
            this.mediTextBoxDiZhi.Properties.NullValuePrompt = "请选择省/市/县";
            this.mediTextBoxDiZhi.Properties.NullValuePromptShowForEmptyValue = true;
            this.mediTextBoxDiZhi.Properties.UnboundExpression = null;
            this.mediTextBoxDiZhi.Size = new System.Drawing.Size(513, 26);
            this.mediTextBoxDiZhi.TabIndex = 0;
            this.mediTextBoxDiZhi.UnboundExpression = null;
            this.mediTextBoxDiZhi.EditValueChanged += new System.EventHandler(this.mediTextBoxDiZhi_EditValueChanged);
            this.mediTextBoxDiZhi.Click += new System.EventHandler(this.mediTextBoxDiZhi_Click);
            this.mediTextBoxDiZhi.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.mediTextBoxDiZhi_KeyPress);
            // 
            // UserControl_DiZhi
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.Controls.Add(this.mediTextBoxDiZhi);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximumSize = new System.Drawing.Size(650, 26);
            this.MinimumSize = new System.Drawing.Size(53, 26);
            this.Name = "UserControl_DiZhi";
            this.Size = new System.Drawing.Size(513, 26);
            this.Load += new System.EventHandler(this.UserControl_DiZhi_Load);
            ((System.ComponentModel.ISupportInitialize)(this.mediTextBoxDiZhi.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private MediTextBox mediTextBoxDiZhi;
    }
}
