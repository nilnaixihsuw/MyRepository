namespace OPC_Client
{
    partial class Form1
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Data1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.Data2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.Data3 = new System.Windows.Forms.Label();
            this.Data4 = new System.Windows.Forms.Label();
            this.Data5 = new System.Windows.Forms.Label();
            this.Data6 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.label15 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.button4 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(80, 26);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(192, 34);
            this.button1.TabIndex = 0;
            this.button1.Text = "连接OPC服务器";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label1.Location = new System.Drawing.Point(12, 112);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "输入";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(66, 112);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "系统启动开关：";
            // 
            // Data1
            // 
            this.Data1.AutoSize = true;
            this.Data1.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.Data1.Location = new System.Drawing.Point(161, 112);
            this.Data1.Name = "Data1";
            this.Data1.Size = new System.Drawing.Size(35, 12);
            this.Data1.TabIndex = 4;
            this.Data1.Text = "Data1";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(54, 137);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 12);
            this.label4.TabIndex = 5;
            this.label4.Text = "机械手启动开关：";
            // 
            // Data2
            // 
            this.Data2.AutoSize = true;
            this.Data2.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.Data2.Location = new System.Drawing.Point(161, 137);
            this.Data2.Name = "Data2";
            this.Data2.Size = new System.Drawing.Size(35, 12);
            this.Data2.TabIndex = 6;
            this.Data2.Text = "Data2";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label6.Location = new System.Drawing.Point(12, 163);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 12);
            this.label6.TabIndex = 7;
            this.label6.Text = "输出";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(90, 163);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 8;
            this.label7.Text = "M1电动机：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(219, 163);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 9;
            this.label8.Text = "机械手：";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(114, 193);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(41, 12);
            this.label9.TabIndex = 10;
            this.label9.Text = "温度：";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(231, 193);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(41, 12);
            this.label10.TabIndex = 11;
            this.label10.Text = "湿度：";
            // 
            // Data3
            // 
            this.Data3.AutoSize = true;
            this.Data3.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.Data3.Location = new System.Drawing.Point(161, 163);
            this.Data3.Name = "Data3";
            this.Data3.Size = new System.Drawing.Size(35, 12);
            this.Data3.TabIndex = 12;
            this.Data3.Text = "Data3";
            // 
            // Data4
            // 
            this.Data4.AutoSize = true;
            this.Data4.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.Data4.Location = new System.Drawing.Point(278, 163);
            this.Data4.Name = "Data4";
            this.Data4.Size = new System.Drawing.Size(35, 12);
            this.Data4.TabIndex = 13;
            this.Data4.Text = "Data4";
            // 
            // Data5
            // 
            this.Data5.AutoSize = true;
            this.Data5.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.Data5.Location = new System.Drawing.Point(161, 193);
            this.Data5.Name = "Data5";
            this.Data5.Size = new System.Drawing.Size(35, 12);
            this.Data5.TabIndex = 14;
            this.Data5.Text = "Data5";
            // 
            // Data6
            // 
            this.Data6.AutoSize = true;
            this.Data6.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.Data6.Location = new System.Drawing.Point(278, 193);
            this.Data6.Name = "Data6";
            this.Data6.Size = new System.Drawing.Size(35, 12);
            this.Data6.TabIndex = 15;
            this.Data6.Text = "Data6";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(80, 219);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(192, 30);
            this.button3.TabIndex = 16;
            this.button3.Text = "写入数据";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(66, 271);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(89, 12);
            this.label15.TabIndex = 17;
            this.label15.Text = "系统启动开关：";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(161, 262);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(47, 21);
            this.textBox1.TabIndex = 18;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(54, 304);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(101, 12);
            this.label16.TabIndex = 19;
            this.label16.Text = "机械手启动开关：";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(160, 295);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(48, 21);
            this.textBox2.TabIndex = 20;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(80, 336);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(192, 32);
            this.button4.TabIndex = 21;
            this.button4.Text = "断开连接";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(353, 378);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.Data6);
            this.Controls.Add(this.Data5);
            this.Controls.Add(this.Data4);
            this.Controls.Add(this.Data3);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.Data2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.Data1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label Data1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label Data2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label Data3;
        private System.Windows.Forms.Label Data4;
        private System.Windows.Forms.Label Data5;
        private System.Windows.Forms.Label Data6;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button button4;
    }
}

