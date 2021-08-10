namespace Mediinfo.WinForm.HIS.Main
{
    partial class Form4
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
            this.mediLayoutControl1 = new Mediinfo.WinForm.Common.MediLayoutControl();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.mediTextBox1 = new Mediinfo.WinForm.MediTextBox();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.mediTextBox2 = new Mediinfo.WinForm.MediTextBox();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.mediLayoutControl1)).BeginInit();
            this.mediLayoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mediTextBox1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mediTextBox2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            this.SuspendLayout();
            // 
            // mediLayoutControl1
            // 
            this.mediLayoutControl1.Controls.Add(this.mediTextBox2);
            this.mediLayoutControl1.Controls.Add(this.mediTextBox1);
            this.mediLayoutControl1.developerHelper = null;
            this.mediLayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mediLayoutControl1.Location = new System.Drawing.Point(0, 0);
            this.mediLayoutControl1.LookAndFeel.SkinName = "MediSkinDevExpressStyle";
            this.mediLayoutControl1.Name = "mediLayoutControl1";
            this.mediLayoutControl1.OptionsFocus.EnableAutoTabOrder = false;
            this.mediLayoutControl1.Root = this.layoutControlGroup1;
            this.mediLayoutControl1.Size = new System.Drawing.Size(800, 450);
            this.mediLayoutControl1.TabIndex = 0;
            this.mediLayoutControl1.Text = "mediLayoutControl1";
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.emptySpaceItem1,
            this.layoutControlItem2});
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(800, 450);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // mediTextBox1
            // 
            this.mediTextBox1.developerHelper = null;
            this.mediTextBox1.EnterMoveNextControl = true;
            this.mediTextBox1.IsOpenEnterNext = false;
            this.mediTextBox1.Location = new System.Drawing.Point(548, 12);
            this.mediTextBox1.MinimumSize = new System.Drawing.Size(0, 26);
            this.mediTextBox1.Name = "mediTextBox1";
            this.mediTextBox1.Properties.AllowMouseWheel = false;
            this.mediTextBox1.Properties.developerHelper = null;
            this.mediTextBox1.Properties.UnboundExpression = null;
            this.mediTextBox1.Size = new System.Drawing.Size(240, 26);
            this.mediTextBox1.StyleController = this.mediLayoutControl1;
            this.mediTextBox1.TabIndex = 4;
            this.mediTextBox1.UnboundExpression = null;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.mediTextBox1;
            this.layoutControlItem1.Location = new System.Drawing.Point(390, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(390, 30);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(142, 20);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 30);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(780, 400);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // mediTextBox2
            // 
            this.mediTextBox2.developerHelper = null;
            this.mediTextBox2.EnterMoveNextControl = true;
            this.mediTextBox2.IsOpenEnterNext = false;
            this.mediTextBox2.Location = new System.Drawing.Point(158, 12);
            this.mediTextBox2.MinimumSize = new System.Drawing.Size(0, 26);
            this.mediTextBox2.Name = "mediTextBox2";
            this.mediTextBox2.Properties.AllowMouseWheel = false;
            this.mediTextBox2.Properties.developerHelper = null;
            this.mediTextBox2.Properties.UnboundExpression = null;
            this.mediTextBox2.Size = new System.Drawing.Size(240, 26);
            this.mediTextBox2.StyleController = this.mediLayoutControl1;
            this.mediTextBox2.TabIndex = 5;
            this.mediTextBox2.UnboundExpression = null;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.mediTextBox2;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(390, 30);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(142, 20);
            // 
            // Form4
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.mediLayoutControl1);
            this.Name = "Form4";
            this.Text = "Form4";
            ((System.ComponentModel.ISupportInitialize)(this.mediLayoutControl1)).EndInit();
            this.mediLayoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mediTextBox1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mediTextBox2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Common.MediLayoutControl mediLayoutControl1;
        private MediTextBox mediTextBox2;
        private MediTextBox mediTextBox1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
    }
}