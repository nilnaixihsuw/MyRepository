using System.Windows.Forms;

namespace Mediinfo.WinForm.HIS.Update
{
    partial class FTPConfigFrm
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

       

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FTPConfigFrm));
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.mediftpiptb = new System.Windows.Forms.TextBox();
            this.medipwdtb = new System.Windows.Forms.TextBox();
            this.mediusertb = new System.Windows.Forms.TextBox();
            this.medisbtnSave = new System.Windows.Forms.Button();
            this.medisbtnCancel = new System.Windows.Forms.Button();
            this.mediTestConnection = new System.Windows.Forms.Button();
            this.mediftpspareiptb = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tbspareftppoint = new System.Windows.Forms.TextBox();
            this.tbftppoint = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(26, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "FTP地址:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(26, 75);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 12);
            this.label5.TabIndex = 5;
            this.label5.Text = "用户名:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(26, 104);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 12);
            this.label6.TabIndex = 4;
            this.label6.Text = "密码:";
            // 
            // mediftpiptb
            // 
            this.mediftpiptb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mediftpiptb.Location = new System.Drawing.Point(111, 18);
            this.mediftpiptb.Name = "mediftpiptb";
            this.mediftpiptb.Size = new System.Drawing.Size(131, 21);
            this.mediftpiptb.TabIndex = 1;
            this.mediftpiptb.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Mediftpiptb_KeyPress);
            this.mediftpiptb.Validating += new System.ComponentModel.CancelEventHandler(this.Mediftpiptb_Validating);
            // 
            // medipwdtb
            // 
            this.medipwdtb.Location = new System.Drawing.Point(111, 101);
            this.medipwdtb.Name = "medipwdtb";
            this.medipwdtb.PasswordChar = '*';
            this.medipwdtb.Size = new System.Drawing.Size(211, 21);
            this.medipwdtb.TabIndex = 6;
            this.medipwdtb.UseSystemPasswordChar = true;
            this.medipwdtb.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Medipwdtb_KeyPress);
            // 
            // mediusertb
            // 
            this.mediusertb.Location = new System.Drawing.Point(111, 72);
            this.mediusertb.Name = "mediusertb";
            this.mediusertb.Size = new System.Drawing.Size(211, 21);
            this.mediusertb.TabIndex = 5;
            this.mediusertb.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Mediusertb_KeyPress);
            // 
            // medisbtnSave
            // 
            this.medisbtnSave.BackColor = System.Drawing.Color.White;
            this.medisbtnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.medisbtnSave.Location = new System.Drawing.Point(185, 8);
            this.medisbtnSave.Name = "medisbtnSave";
            this.medisbtnSave.Size = new System.Drawing.Size(75, 26);
            this.medisbtnSave.TabIndex = 8;
            this.medisbtnSave.Text = "保存(&S)";
            this.medisbtnSave.UseVisualStyleBackColor = false;
            this.medisbtnSave.Click += new System.EventHandler(this.MedisbtnSave_Click);
            this.medisbtnSave.Enter += new System.EventHandler(this.MedisbtnSave_Enter);
            // 
            // medisbtnCancel
            // 
            this.medisbtnCancel.BackColor = System.Drawing.Color.White;
            this.medisbtnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.medisbtnCancel.Location = new System.Drawing.Point(266, 8);
            this.medisbtnCancel.Name = "medisbtnCancel";
            this.medisbtnCancel.Size = new System.Drawing.Size(75, 26);
            this.medisbtnCancel.TabIndex = 9;
            this.medisbtnCancel.Text = "关闭(&C)";
            this.medisbtnCancel.UseVisualStyleBackColor = false;
            this.medisbtnCancel.Click += new System.EventHandler(this.MedisbtnCancel_Click);
            this.medisbtnCancel.Enter += new System.EventHandler(this.MedisbtnCancel_Enter);
            // 
            // mediTestConnection
            // 
            this.mediTestConnection.BackColor = System.Drawing.Color.White;
            this.mediTestConnection.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mediTestConnection.Location = new System.Drawing.Point(12, 8);
            this.mediTestConnection.Name = "mediTestConnection";
            this.mediTestConnection.Size = new System.Drawing.Size(82, 26);
            this.mediTestConnection.TabIndex = 7;
            this.mediTestConnection.Text = "连接测试(&T)";
            this.mediTestConnection.UseVisualStyleBackColor = false;
            this.mediTestConnection.Click += new System.EventHandler(this.MediTestConnection_Click);
            // 
            // mediftpspareiptb
            // 
            this.mediftpspareiptb.Location = new System.Drawing.Point(111, 45);
            this.mediftpspareiptb.Name = "mediftpspareiptb";
            this.mediftpspareiptb.Size = new System.Drawing.Size(131, 21);
            this.mediftpspareiptb.TabIndex = 3;
            this.mediftpspareiptb.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Mediftpspareiptb_KeyPress);
            this.mediftpspareiptb.Validating += new System.ComponentModel.CancelEventHandler(this.mediftpspareiptb_Validating);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.Red;
            this.label8.Location = new System.Drawing.Point(26, 48);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(77, 12);
            this.label8.TabIndex = 16;
            this.label8.Text = "备用FTP地址:";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.tbspareftppoint);
            this.panel1.Controls.Add(this.tbftppoint);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.mediftpspareiptb);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.mediftpiptb);
            this.panel1.Controls.Add(this.mediusertb);
            this.panel1.Controls.Add(this.medipwdtb);
            this.panel1.Location = new System.Drawing.Point(6, 7);
            this.panel1.Margin = new System.Windows.Forms.Padding(6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(341, 137);
            this.panel1.TabIndex = 18;
            // 
            // tbspareftppoint
            // 
            this.tbspareftppoint.Location = new System.Drawing.Point(289, 45);
            this.tbspareftppoint.Name = "tbspareftppoint";
            this.tbspareftppoint.Size = new System.Drawing.Size(33, 21);
            this.tbspareftppoint.TabIndex = 4;
            this.tbspareftppoint.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Tbspareftppoint_KeyPress);
            this.tbspareftppoint.Validating += new System.ComponentModel.CancelEventHandler(this.tbspareftppoint_Validating);
            // 
            // tbftppoint
            // 
            this.tbftppoint.Location = new System.Drawing.Point(289, 18);
            this.tbftppoint.Name = "tbftppoint";
            this.tbftppoint.Size = new System.Drawing.Size(33, 21);
            this.tbftppoint.TabIndex = 2;
            this.tbftppoint.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Tbftppoint_KeyPress);
            this.tbftppoint.Validating += new System.ComponentModel.CancelEventHandler(this.Tbftppoint_Validating);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(248, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 12);
            this.label3.TabIndex = 19;
            this.label3.Text = "端口:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(248, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 12);
            this.label2.TabIndex = 18;
            this.label2.Text = "端口:";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(222)))), ((int)(((byte)(246)))));
            this.panel2.Controls.Add(this.medisbtnSave);
            this.panel2.Controls.Add(this.medisbtnCancel);
            this.panel2.Controls.Add(this.mediTestConnection);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 151);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(353, 42);
            this.panel2.TabIndex = 19;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // FTPConfigFrm
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(222)))), ((int)(((byte)(246)))));
            this.ClientSize = new System.Drawing.Size(353, 193);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FTPConfigFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FTP服务器连接设置";
            this.Load += new System.EventHandler(this.FTPConfigFrm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        private Label label1;
        private Label label5;
        private Label label6;
        private TextBox mediftpiptb;
        private TextBox medipwdtb;
        private TextBox mediusertb;
        private Button medisbtnSave;
        private Button medisbtnCancel;
        private Button mediTestConnection;
        private TextBox mediftpspareiptb;
        private Label label8;
        private Panel panel1;
        private TextBox tbspareftppoint;
        private TextBox tbftppoint;
        private Label label3;
        private Label label2;
        private Panel panel2;
        private ErrorProvider errorProvider1;
    }  } 