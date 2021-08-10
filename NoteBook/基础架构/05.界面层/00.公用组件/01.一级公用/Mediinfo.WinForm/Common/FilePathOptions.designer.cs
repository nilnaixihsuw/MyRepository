namespace Mediinfo.WinForm
{
    partial class FilePathOptions
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
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.btePicPath = new DevExpress.XtraEditors.ButtonEdit();
            this.bteStartPath = new DevExpress.XtraEditors.ButtonEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.sbtnSave = new DevExpress.XtraEditors.SimpleButton();
            this.sbtnClose = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.btePicPath.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bteStartPath.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(34, 68);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(75, 17);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "图片文件路径:";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(34, 123);
            this.labelControl2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(75, 17);
            this.labelControl2.TabIndex = 1;
            this.labelControl2.Text = "启动程序目录:";
            // 
            // btePicPath
            // 
            this.btePicPath.Location = new System.Drawing.Point(137, 68);
            this.btePicPath.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btePicPath.Name = "btePicPath";
            this.btePicPath.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.btePicPath.Properties.Click += new System.EventHandler(this.btePicPath_Properties_Click);
            this.btePicPath.Size = new System.Drawing.Size(177, 24);
            this.btePicPath.TabIndex = 2;
            // 
            // bteStartPath
            // 
            this.bteStartPath.Location = new System.Drawing.Point(137, 119);
            this.bteStartPath.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.bteStartPath.Name = "bteStartPath";
            this.bteStartPath.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.bteStartPath.Properties.Click += new System.EventHandler(this.bteStartPath_Properties_Click);
            this.bteStartPath.Size = new System.Drawing.Size(177, 24);
            this.bteStartPath.TabIndex = 3;
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.ForeColor = System.Drawing.Color.Red;
            this.labelControl3.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl3.Location = new System.Drawing.Point(34, 159);
            this.labelControl3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(280, 33);
            this.labelControl3.TabIndex = 4;
            this.labelControl3.Text = "注意: 图片文件路径和启动程序根路径保持一致";
            // 
            // sbtnSave
            // 
            this.sbtnSave.Location = new System.Drawing.Point(34, 221);
            this.sbtnSave.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.sbtnSave.Name = "sbtnSave";
            this.sbtnSave.Size = new System.Drawing.Size(75, 28);
            this.sbtnSave.TabIndex = 5;
            this.sbtnSave.Text = "保存(&S)";
            this.sbtnSave.Click += new System.EventHandler(this.sbtnSave_Click);
            // 
            // sbtnClose
            // 
            this.sbtnClose.Location = new System.Drawing.Point(239, 221);
            this.sbtnClose.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.sbtnClose.Name = "sbtnClose";
            this.sbtnClose.Size = new System.Drawing.Size(75, 28);
            this.sbtnClose.TabIndex = 6;
            this.sbtnClose.Text = "关闭(&C)";
            this.sbtnClose.Click += new System.EventHandler(this.sbtnClose_Click);
            // 
            // FilePathOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(350, 264);
            this.Controls.Add(this.sbtnClose);
            this.Controls.Add(this.sbtnSave);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.bteStartPath);
            this.Controls.Add(this.btePicPath);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "FilePathOptions";
            this.Text = "文件选项";
          
            ((System.ComponentModel.ISupportInitialize)(this.btePicPath.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bteStartPath.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.ButtonEdit btePicPath;
        private DevExpress.XtraEditors.ButtonEdit bteStartPath;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.SimpleButton sbtnSave;
        private DevExpress.XtraEditors.SimpleButton sbtnClose;
    }
}