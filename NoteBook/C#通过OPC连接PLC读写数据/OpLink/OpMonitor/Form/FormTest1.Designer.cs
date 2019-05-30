namespace OpLink
{
    partial class FormTest1
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tsslversion = new System.Windows.Forms.ToolStripStatusLabel();
            this.txtGroupIsActive = new System.Windows.Forms.TextBox();
            this.tsslServerStartTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.button2 = new System.Windows.Forms.Button();
            this.tsslServerState = new System.Windows.Forms.ToolStripStatusLabel();
            this.listView1 = new System.Windows.Forms.ListView();
            this.txtGroupDeadband = new System.Windows.Forms.TextBox();
            this.txtUpdateRate = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.TagName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OpcTagName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Qualities = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TimeStamps = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DataType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblState = new System.Windows.Forms.Label();
            this.btnSetGroupPro = new System.Windows.Forms.Button();
            this.listBoxGroups = new System.Windows.Forms.ListBox();
            this.listBoxItems = new System.Windows.Forms.ListBox();
            this.Frame1 = new System.Windows.Forms.GroupBox();
            this.cmbServerName = new System.Windows.Forms.ComboBox();
            this.btnReConnServer = new System.Windows.Forms.Button();
            this.btnConnServer = new System.Windows.Forms.Button();
            this.txtRemoteServerIP = new System.Windows.Forms.TextBox();
            this.btnDisconnServer = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.lblOPCNodeName = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtIsSubscribed = new System.Windows.Forms.TextBox();
            this.txtIsActive = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtDataType = new System.Windows.Forms.TextBox();
            this.txtTimeStamps = new System.Windows.Forms.TextBox();
            this.txtQualities = new System.Windows.Forms.TextBox();
            this.txtTagValue = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnWriteIn = new System.Windows.Forms.Button();
            this.txtWriteTagValue = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.Frame1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tsslversion
            // 
            this.tsslversion.Name = "tsslversion";
            this.tsslversion.Size = new System.Drawing.Size(131, 17);
            this.tsslversion.Text = "toolStripStatusLabel1";
            // 
            // txtGroupIsActive
            // 
            this.txtGroupIsActive.AcceptsReturn = true;
            this.txtGroupIsActive.BackColor = System.Drawing.SystemColors.Window;
            this.txtGroupIsActive.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtGroupIsActive.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGroupIsActive.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtGroupIsActive.Location = new System.Drawing.Point(138, 26);
            this.txtGroupIsActive.MaxLength = 0;
            this.txtGroupIsActive.Name = "txtGroupIsActive";
            this.txtGroupIsActive.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtGroupIsActive.Size = new System.Drawing.Size(90, 20);
            this.txtGroupIsActive.TabIndex = 94;
            this.txtGroupIsActive.Text = "true";
            // 
            // tsslServerStartTime
            // 
            this.tsslServerStartTime.Name = "tsslServerStartTime";
            this.tsslServerStartTime.Size = new System.Drawing.Size(131, 17);
            this.tsslServerStartTime.Text = "toolStripStatusLabel1";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(945, 291);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 113;
            this.button2.Text = "新增测试";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // tsslServerState
            // 
            this.tsslServerState.Name = "tsslServerState";
            this.tsslServerState.Size = new System.Drawing.Size(131, 17);
            this.tsslServerState.Text = "toolStripStatusLabel1";
            // 
            // listView1
            // 
            this.listView1.CheckBoxes = true;
            this.listView1.Location = new System.Drawing.Point(546, 2);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(381, 160);
            this.listView1.TabIndex = 115;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.SmallIcon;
            // 
            // txtGroupDeadband
            // 
            this.txtGroupDeadband.AcceptsReturn = true;
            this.txtGroupDeadband.BackColor = System.Drawing.SystemColors.Window;
            this.txtGroupDeadband.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtGroupDeadband.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGroupDeadband.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtGroupDeadband.Location = new System.Drawing.Point(138, 67);
            this.txtGroupDeadband.MaxLength = 0;
            this.txtGroupDeadband.Name = "txtGroupDeadband";
            this.txtGroupDeadband.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtGroupDeadband.Size = new System.Drawing.Size(90, 20);
            this.txtGroupDeadband.TabIndex = 94;
            this.txtGroupDeadband.Text = "0";
            // 
            // txtUpdateRate
            // 
            this.txtUpdateRate.AcceptsReturn = true;
            this.txtUpdateRate.BackColor = System.Drawing.SystemColors.Window;
            this.txtUpdateRate.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtUpdateRate.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUpdateRate.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtUpdateRate.Location = new System.Drawing.Point(138, 185);
            this.txtUpdateRate.MaxLength = 0;
            this.txtUpdateRate.Name = "txtUpdateRate";
            this.txtUpdateRate.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtUpdateRate.Size = new System.Drawing.Size(90, 20);
            this.txtUpdateRate.TabIndex = 94;
            this.txtUpdateRate.Text = "250";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle10;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.TagName,
            this.OpcTagName,
            this.Value,
            this.Qualities,
            this.TimeStamps,
            this.DataType});
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle11;
            this.dataGridView1.Location = new System.Drawing.Point(348, 175);
            this.dataGridView1.Name = "dataGridView1";
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle12;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(579, 300);
            this.dataGridView1.TabIndex = 114;
            // 
            // TagName
            // 
            this.TagName.HeaderText = "名称";
            this.TagName.Name = "TagName";
            // 
            // OpcTagName
            // 
            this.OpcTagName.HeaderText = "对应地址";
            this.OpcTagName.Name = "OpcTagName";
            // 
            // Value
            // 
            this.Value.HeaderText = "Tag值";
            this.Value.Name = "Value";
            // 
            // Qualities
            // 
            this.Qualities.HeaderText = "质量";
            this.Qualities.Name = "Qualities";
            // 
            // TimeStamps
            // 
            this.TimeStamps.HeaderText = "时间戳";
            this.TimeStamps.Name = "TimeStamps";
            // 
            // DataType
            // 
            this.DataType.HeaderText = "类型";
            this.DataType.Name = "DataType";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslServerState,
            this.tsslServerStartTime,
            this.tsslversion});
            this.statusStrip1.Location = new System.Drawing.Point(0, 527);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1326, 22);
            this.statusStrip1.TabIndex = 111;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblState
            // 
            this.lblState.AutoSize = true;
            this.lblState.Location = new System.Drawing.Point(943, 269);
            this.lblState.Name = "lblState";
            this.lblState.Size = new System.Drawing.Size(41, 12);
            this.lblState.TabIndex = 110;
            this.lblState.Text = "label6";
            // 
            // btnSetGroupPro
            // 
            this.btnSetGroupPro.BackColor = System.Drawing.SystemColors.Control;
            this.btnSetGroupPro.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnSetGroupPro.Enabled = false;
            this.btnSetGroupPro.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSetGroupPro.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnSetGroupPro.Location = new System.Drawing.Point(236, 214);
            this.btnSetGroupPro.Name = "btnSetGroupPro";
            this.btnSetGroupPro.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnSetGroupPro.Size = new System.Drawing.Size(75, 24);
            this.btnSetGroupPro.TabIndex = 96;
            this.btnSetGroupPro.Text = "设置";
            this.btnSetGroupPro.UseVisualStyleBackColor = false;
            this.btnSetGroupPro.Click += new System.EventHandler(this.btnSetGroupPro_Click);
            // 
            // listBoxGroups
            // 
            this.listBoxGroups.FormattingEnabled = true;
            this.listBoxGroups.ItemHeight = 12;
            this.listBoxGroups.Location = new System.Drawing.Point(348, 2);
            this.listBoxGroups.Name = "listBoxGroups";
            this.listBoxGroups.Size = new System.Drawing.Size(192, 160);
            this.listBoxGroups.TabIndex = 109;
            this.listBoxGroups.SelectedIndexChanged += new System.EventHandler(this.listBoxGroups_SelectedIndexChanged);
            // 
            // listBoxItems
            // 
            this.listBoxItems.FormattingEnabled = true;
            this.listBoxItems.ItemHeight = 12;
            this.listBoxItems.Location = new System.Drawing.Point(1048, 315);
            this.listBoxItems.Name = "listBoxItems";
            this.listBoxItems.Size = new System.Drawing.Size(249, 160);
            this.listBoxItems.TabIndex = 108;
            // 
            // Frame1
            // 
            this.Frame1.BackColor = System.Drawing.SystemColors.Control;
            this.Frame1.Controls.Add(this.cmbServerName);
            this.Frame1.Controls.Add(this.btnReConnServer);
            this.Frame1.Controls.Add(this.btnConnServer);
            this.Frame1.Controls.Add(this.txtRemoteServerIP);
            this.Frame1.Controls.Add(this.btnDisconnServer);
            this.Frame1.Controls.Add(this.label4);
            this.Frame1.Controls.Add(this.lblOPCNodeName);
            this.Frame1.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Frame1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Frame1.Location = new System.Drawing.Point(7, 2);
            this.Frame1.Name = "Frame1";
            this.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Frame1.Size = new System.Drawing.Size(317, 216);
            this.Frame1.TabIndex = 107;
            this.Frame1.TabStop = false;
            this.Frame1.Text = "List Available OPC Servers";
            // 
            // cmbServerName
            // 
            this.cmbServerName.FormattingEnabled = true;
            this.cmbServerName.Location = new System.Drawing.Point(67, 48);
            this.cmbServerName.Name = "cmbServerName";
            this.cmbServerName.Size = new System.Drawing.Size(183, 22);
            this.cmbServerName.TabIndex = 97;
            // 
            // btnReConnServer
            // 
            this.btnReConnServer.BackColor = System.Drawing.SystemColors.Control;
            this.btnReConnServer.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnReConnServer.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReConnServer.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnReConnServer.Location = new System.Drawing.Point(67, 173);
            this.btnReConnServer.Name = "btnReConnServer";
            this.btnReConnServer.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnReConnServer.Size = new System.Drawing.Size(146, 24);
            this.btnReConnServer.TabIndex = 96;
            this.btnReConnServer.Text = "ReConnect";
            this.btnReConnServer.UseVisualStyleBackColor = false;
            this.btnReConnServer.Click += new System.EventHandler(this.btnReConnServer_Click);
            // 
            // btnConnServer
            // 
            this.btnConnServer.BackColor = System.Drawing.SystemColors.Control;
            this.btnConnServer.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnConnServer.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConnServer.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnConnServer.Location = new System.Drawing.Point(67, 86);
            this.btnConnServer.Name = "btnConnServer";
            this.btnConnServer.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnConnServer.Size = new System.Drawing.Size(146, 24);
            this.btnConnServer.TabIndex = 96;
            this.btnConnServer.Text = "Connect";
            this.btnConnServer.UseVisualStyleBackColor = false;
            this.btnConnServer.Click += new System.EventHandler(this.btnConnServer_Click);
            // 
            // txtRemoteServerIP
            // 
            this.txtRemoteServerIP.AcceptsReturn = true;
            this.txtRemoteServerIP.BackColor = System.Drawing.SystemColors.Window;
            this.txtRemoteServerIP.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtRemoteServerIP.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRemoteServerIP.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtRemoteServerIP.Location = new System.Drawing.Point(67, 25);
            this.txtRemoteServerIP.MaxLength = 0;
            this.txtRemoteServerIP.Name = "txtRemoteServerIP";
            this.txtRemoteServerIP.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtRemoteServerIP.Size = new System.Drawing.Size(183, 20);
            this.txtRemoteServerIP.TabIndex = 94;
            this.txtRemoteServerIP.Text = "127.0.0.1";
            // 
            // btnDisconnServer
            // 
            this.btnDisconnServer.BackColor = System.Drawing.SystemColors.Control;
            this.btnDisconnServer.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnDisconnServer.Enabled = false;
            this.btnDisconnServer.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDisconnServer.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnDisconnServer.Location = new System.Drawing.Point(67, 116);
            this.btnDisconnServer.Name = "btnDisconnServer";
            this.btnDisconnServer.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnDisconnServer.Size = new System.Drawing.Size(146, 21);
            this.btnDisconnServer.TabIndex = 6;
            this.btnDisconnServer.Text = "Disconnect From Server";
            this.btnDisconnServer.UseVisualStyleBackColor = false;
            this.btnDisconnServer.Click += new System.EventHandler(this.btnDisconnServer_Click);
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.SystemColors.Control;
            this.label4.Cursor = System.Windows.Forms.Cursors.Default;
            this.label4.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label4.Location = new System.Drawing.Point(19, 26);
            this.label4.Name = "label4";
            this.label4.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label4.Size = new System.Drawing.Size(77, 19);
            this.label4.TabIndex = 95;
            this.label4.Text = "IP";
            // 
            // lblOPCNodeName
            // 
            this.lblOPCNodeName.BackColor = System.Drawing.SystemColors.Control;
            this.lblOPCNodeName.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblOPCNodeName.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOPCNodeName.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblOPCNodeName.Location = new System.Drawing.Point(19, 52);
            this.lblOPCNodeName.Name = "lblOPCNodeName";
            this.lblOPCNodeName.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblOPCNodeName.Size = new System.Drawing.Size(77, 19);
            this.lblOPCNodeName.TabIndex = 95;
            this.lblOPCNodeName.Text = "服务器";
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox3.Controls.Add(this.btnSetGroupPro);
            this.groupBox3.Controls.Add(this.txtGroupIsActive);
            this.groupBox3.Controls.Add(this.txtGroupDeadband);
            this.groupBox3.Controls.Add(this.txtUpdateRate);
            this.groupBox3.Controls.Add(this.txtIsSubscribed);
            this.groupBox3.Controls.Add(this.txtIsActive);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.groupBox3.Location = new System.Drawing.Point(7, 224);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.groupBox3.Size = new System.Drawing.Size(317, 244);
            this.groupBox3.TabIndex = 106;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "更改组属性";
            // 
            // txtIsSubscribed
            // 
            this.txtIsSubscribed.AcceptsReturn = true;
            this.txtIsSubscribed.BackColor = System.Drawing.SystemColors.Window;
            this.txtIsSubscribed.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtIsSubscribed.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIsSubscribed.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtIsSubscribed.Location = new System.Drawing.Point(138, 145);
            this.txtIsSubscribed.MaxLength = 0;
            this.txtIsSubscribed.Name = "txtIsSubscribed";
            this.txtIsSubscribed.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtIsSubscribed.Size = new System.Drawing.Size(90, 20);
            this.txtIsSubscribed.TabIndex = 94;
            this.txtIsSubscribed.Text = "true";
            // 
            // txtIsActive
            // 
            this.txtIsActive.AcceptsReturn = true;
            this.txtIsActive.BackColor = System.Drawing.SystemColors.Window;
            this.txtIsActive.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtIsActive.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIsActive.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtIsActive.Location = new System.Drawing.Point(138, 107);
            this.txtIsActive.MaxLength = 0;
            this.txtIsActive.Name = "txtIsActive";
            this.txtIsActive.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtIsActive.Size = new System.Drawing.Size(90, 20);
            this.txtIsActive.TabIndex = 94;
            this.txtIsActive.Text = "true";
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.SystemColors.Control;
            this.label5.Cursor = System.Windows.Forms.Cursors.Default;
            this.label5.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label5.Location = new System.Drawing.Point(19, 26);
            this.label5.Name = "label5";
            this.label5.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label5.Size = new System.Drawing.Size(139, 19);
            this.label5.TabIndex = 95;
            this.label5.Text = "DefaultGroupIsActive";
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.SystemColors.Control;
            this.label6.Cursor = System.Windows.Forms.Cursors.Default;
            this.label6.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label6.Location = new System.Drawing.Point(19, 67);
            this.label6.Name = "label6";
            this.label6.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label6.Size = new System.Drawing.Size(125, 19);
            this.label6.TabIndex = 95;
            this.label6.Text = "DefaultGroupDeadBand";
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.SystemColors.Control;
            this.label9.Cursor = System.Windows.Forms.Cursors.Default;
            this.label9.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label9.Location = new System.Drawing.Point(19, 185);
            this.label9.Name = "label9";
            this.label9.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label9.Size = new System.Drawing.Size(77, 19);
            this.label9.TabIndex = 95;
            this.label9.Text = "UpdateRate";
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.SystemColors.Control;
            this.label8.Cursor = System.Windows.Forms.Cursors.Default;
            this.label8.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label8.Location = new System.Drawing.Point(19, 145);
            this.label8.Name = "label8";
            this.label8.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label8.Size = new System.Drawing.Size(77, 19);
            this.label8.TabIndex = 95;
            this.label8.Text = "IsSubscribed";
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.SystemColors.Control;
            this.label7.Cursor = System.Windows.Forms.Cursors.Default;
            this.label7.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label7.Location = new System.Drawing.Point(19, 107);
            this.label7.Name = "label7";
            this.label7.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label7.Size = new System.Drawing.Size(77, 19);
            this.label7.TabIndex = 95;
            this.label7.Text = "IsActive";
            // 
            // txtDataType
            // 
            this.txtDataType.AcceptsReturn = true;
            this.txtDataType.BackColor = System.Drawing.SystemColors.Window;
            this.txtDataType.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtDataType.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDataType.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtDataType.Location = new System.Drawing.Point(115, 143);
            this.txtDataType.MaxLength = 0;
            this.txtDataType.Name = "txtDataType";
            this.txtDataType.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtDataType.Size = new System.Drawing.Size(183, 20);
            this.txtDataType.TabIndex = 94;
            // 
            // txtTimeStamps
            // 
            this.txtTimeStamps.AcceptsReturn = true;
            this.txtTimeStamps.BackColor = System.Drawing.SystemColors.Window;
            this.txtTimeStamps.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTimeStamps.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTimeStamps.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtTimeStamps.Location = new System.Drawing.Point(115, 107);
            this.txtTimeStamps.MaxLength = 0;
            this.txtTimeStamps.Name = "txtTimeStamps";
            this.txtTimeStamps.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtTimeStamps.Size = new System.Drawing.Size(183, 20);
            this.txtTimeStamps.TabIndex = 94;
            // 
            // txtQualities
            // 
            this.txtQualities.AcceptsReturn = true;
            this.txtQualities.BackColor = System.Drawing.SystemColors.Window;
            this.txtQualities.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtQualities.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtQualities.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtQualities.Location = new System.Drawing.Point(115, 67);
            this.txtQualities.MaxLength = 0;
            this.txtQualities.Name = "txtQualities";
            this.txtQualities.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtQualities.Size = new System.Drawing.Size(183, 20);
            this.txtQualities.TabIndex = 94;
            // 
            // txtTagValue
            // 
            this.txtTagValue.AcceptsReturn = true;
            this.txtTagValue.BackColor = System.Drawing.SystemColors.Window;
            this.txtTagValue.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTagValue.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTagValue.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtTagValue.Location = new System.Drawing.Point(115, 26);
            this.txtTagValue.MaxLength = 0;
            this.txtTagValue.Name = "txtTagValue";
            this.txtTagValue.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtTagValue.Size = new System.Drawing.Size(183, 20);
            this.txtTagValue.TabIndex = 94;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.SystemColors.Control;
            this.label3.Cursor = System.Windows.Forms.Cursors.Default;
            this.label3.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label3.Location = new System.Drawing.Point(19, 26);
            this.label3.Name = "label3";
            this.label3.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label3.Size = new System.Drawing.Size(77, 19);
            this.label3.TabIndex = 95;
            this.label3.Text = "Tag值";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox1.Controls.Add(this.txtTagValue);
            this.groupBox1.Controls.Add(this.txtQualities);
            this.groupBox1.Controls.Add(this.txtDataType);
            this.groupBox1.Controls.Add(this.txtTimeStamps);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.groupBox1.Location = new System.Drawing.Point(933, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.groupBox1.Size = new System.Drawing.Size(317, 165);
            this.groupBox1.TabIndex = 105;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "当前值";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.SystemColors.Control;
            this.label2.Cursor = System.Windows.Forms.Cursors.Default;
            this.label2.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label2.Location = new System.Drawing.Point(19, 67);
            this.label2.Name = "label2";
            this.label2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label2.Size = new System.Drawing.Size(77, 19);
            this.label2.TabIndex = 95;
            this.label2.Text = "品质";
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.SystemColors.Control;
            this.label10.Cursor = System.Windows.Forms.Cursors.Default;
            this.label10.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label10.Location = new System.Drawing.Point(19, 143);
            this.label10.Name = "label10";
            this.label10.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label10.Size = new System.Drawing.Size(77, 19);
            this.label10.TabIndex = 95;
            this.label10.Text = "数据类型";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.Control;
            this.label1.Cursor = System.Windows.Forms.Cursors.Default;
            this.label1.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(19, 107);
            this.label1.Name = "label1";
            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label1.Size = new System.Drawing.Size(77, 19);
            this.label1.TabIndex = 95;
            this.label1.Text = "时间戳";
            // 
            // btnWriteIn
            // 
            this.btnWriteIn.Location = new System.Drawing.Point(155, 18);
            this.btnWriteIn.Name = "btnWriteIn";
            this.btnWriteIn.Size = new System.Drawing.Size(132, 23);
            this.btnWriteIn.TabIndex = 95;
            this.btnWriteIn.Text = "写入";
            this.btnWriteIn.UseVisualStyleBackColor = true;
            // 
            // txtWriteTagValue
            // 
            this.txtWriteTagValue.AcceptsReturn = true;
            this.txtWriteTagValue.BackColor = System.Drawing.SystemColors.Window;
            this.txtWriteTagValue.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtWriteTagValue.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtWriteTagValue.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtWriteTagValue.Location = new System.Drawing.Point(22, 19);
            this.txtWriteTagValue.MaxLength = 0;
            this.txtWriteTagValue.Name = "txtWriteTagValue";
            this.txtWriteTagValue.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtWriteTagValue.Size = new System.Drawing.Size(127, 20);
            this.txtWriteTagValue.TabIndex = 94;
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox2.Controls.Add(this.btnWriteIn);
            this.groupBox2.Controls.Add(this.txtWriteTagValue);
            this.groupBox2.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.groupBox2.Location = new System.Drawing.Point(933, 185);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.groupBox2.Size = new System.Drawing.Size(317, 58);
            this.groupBox2.TabIndex = 104;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "写入值";
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(945, 331);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 112;
            this.btnRefresh.Text = "刷新";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(1107, 269);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(47, 12);
            this.label11.TabIndex = 116;
            this.label11.Text = "label11";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(945, 369);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 117;
            this.button1.Text = "初始化";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(945, 408);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 118;
            this.button3.Text = "新增分组";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // FormTest1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1326, 549);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.lblState);
            this.Controls.Add(this.listBoxGroups);
            this.Controls.Add(this.listBoxItems);
            this.Controls.Add(this.Frame1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnRefresh);
            this.Name = "FormTest1";
            this.Text = "FormTest1";
            this.Load += new System.EventHandler(this.FormTest1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.Frame1.ResumeLayout(false);
            this.Frame1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripStatusLabel tsslversion;
        public System.Windows.Forms.TextBox txtGroupIsActive;
        private System.Windows.Forms.ToolStripStatusLabel tsslServerStartTime;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ToolStripStatusLabel tsslServerState;
        private System.Windows.Forms.ListView listView1;
        public System.Windows.Forms.TextBox txtGroupDeadband;
        public System.Windows.Forms.TextBox txtUpdateRate;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn TagName;
        private System.Windows.Forms.DataGridViewTextBoxColumn OpcTagName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Value;
        private System.Windows.Forms.DataGridViewTextBoxColumn Qualities;
        private System.Windows.Forms.DataGridViewTextBoxColumn TimeStamps;
        private System.Windows.Forms.DataGridViewTextBoxColumn DataType;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Label lblState;
        public System.Windows.Forms.Button btnSetGroupPro;
        private System.Windows.Forms.ListBox listBoxGroups;
        private System.Windows.Forms.ListBox listBoxItems;
        public System.Windows.Forms.GroupBox Frame1;
        private System.Windows.Forms.ComboBox cmbServerName;
        public System.Windows.Forms.Button btnConnServer;
        public System.Windows.Forms.TextBox txtRemoteServerIP;
        public System.Windows.Forms.Button btnDisconnServer;
        public System.Windows.Forms.Label label4;
        public System.Windows.Forms.Label lblOPCNodeName;
        public System.Windows.Forms.GroupBox groupBox3;
        public System.Windows.Forms.TextBox txtIsSubscribed;
        public System.Windows.Forms.TextBox txtIsActive;
        public System.Windows.Forms.Label label5;
        public System.Windows.Forms.Label label6;
        public System.Windows.Forms.Label label9;
        public System.Windows.Forms.Label label8;
        public System.Windows.Forms.Label label7;
        public System.Windows.Forms.TextBox txtDataType;
        public System.Windows.Forms.TextBox txtTimeStamps;
        public System.Windows.Forms.TextBox txtQualities;
        public System.Windows.Forms.TextBox txtTagValue;
        public System.Windows.Forms.Label label3;
        public System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.Label label10;
        public System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnWriteIn;
        public System.Windows.Forms.TextBox txtWriteTagValue;
        public System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button button1;
        public System.Windows.Forms.Button btnReConnServer;
        private System.Windows.Forms.Button button3;
    }
}