namespace Mediinfo.WinForm.HIS.Main
{
    partial class Form1
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
            this.mediButton1 = new Mediinfo.WinForm.MediButton();
            this.SuspendLayout();
            // 
            // mediButton1
            // 
            this.mediButton1.Appearance.Options.UseTextOptions = true;
            this.mediButton1.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.mediButton1.developerHelper = null;
            this.mediButton1.Location = new System.Drawing.Point(119, 92);
            this.mediButton1.LookAndFeel.SkinName = "MediSkinDevExpressStyle";
            this.mediButton1.Name = "mediButton1";
            this.mediButton1.Size = new System.Drawing.Size(70, 26);
            this.mediButton1.TabIndex = 0;
            this.mediButton1.Text = "mediButton1";
            this.mediButton1.UnboundExpression = null;
            this.mediButton1.Click += new System.EventHandler(this.mediButton1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(520, 411);
            this.Controls.Add(this.mediButton1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private MediButton mediButton1;
    }
}