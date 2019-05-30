namespace OpMonitor
{
    partial class Setting
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.treeTags = new System.Windows.Forms.TreeView();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.contextMenuBlock = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolTagAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.toolBlockDel = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuGroup = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolBlockAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.toolGroupsRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.dataGridTags = new System.Windows.Forms.DataGridView();
            this.TagName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OpcTagName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Qualities = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TimeStamps = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DataType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Group = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Message = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuTags = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolTagOAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTagDel = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTagsRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.lblGroup = new System.Windows.Forms.Label();
            this.lblBlock = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.chkRefresh = new System.Windows.Forms.CheckBox();
            this.cmbInterval = new System.Windows.Forms.ComboBox();
            this.timerRefresh = new System.Windows.Forms.Timer(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.groupBoxWatch = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnSet = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tsslServerState = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslversion = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslServerStartTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.contextMenuBlock.SuspendLayout();
            this.contextMenuGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridTags)).BeginInit();
            this.contextMenuTags.SuspendLayout();
            this.groupBoxWatch.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeTags
            // 
            this.treeTags.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeTags.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.treeTags.Location = new System.Drawing.Point(3, 17);
            this.treeTags.Name = "treeTags";
            this.treeTags.Size = new System.Drawing.Size(205, 619);
            this.treeTags.TabIndex = 0;
            this.treeTags.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.treeTags_MouseDoubleClick);
            this.treeTags.MouseDown += new System.Windows.Forms.MouseEventHandler(this.treeTags_MouseDown);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnRefresh.Location = new System.Drawing.Point(226, 43);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(68, 28);
            this.btnRefresh.TabIndex = 1;
            this.btnRefresh.Text = "刷新";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // contextMenuBlock
            // 
            this.contextMenuBlock.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolTagAdd,
            this.toolBlockDel});
            this.contextMenuBlock.Name = "toolSave";
            this.contextMenuBlock.Size = new System.Drawing.Size(133, 48);
            // 
            // toolTagAdd
            // 
            this.toolTagAdd.Name = "toolTagAdd";
            this.toolTagAdd.Size = new System.Drawing.Size(132, 22);
            this.toolTagAdd.Text = "Tag新增";
            this.toolTagAdd.Click += new System.EventHandler(this.toolTagAdd_Click);
            // 
            // toolBlockDel
            // 
            this.toolBlockDel.Name = "toolBlockDel";
            this.toolBlockDel.Size = new System.Drawing.Size(132, 22);
            this.toolBlockDel.Text = "Block删除";
            this.toolBlockDel.Click += new System.EventHandler(this.toolBlockDel_Click);
            // 
            // contextMenuGroup
            // 
            this.contextMenuGroup.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolBlockAdd,
            this.toolGroupsRefresh});
            this.contextMenuGroup.Name = "contextMenuGroup";
            this.contextMenuGroup.Size = new System.Drawing.Size(133, 48);
            // 
            // toolBlockAdd
            // 
            this.toolBlockAdd.Name = "toolBlockAdd";
            this.toolBlockAdd.Size = new System.Drawing.Size(132, 22);
            this.toolBlockAdd.Text = "Block新增";
            this.toolBlockAdd.Click += new System.EventHandler(this.toolBlockAdd_Click);
            // 
            // toolGroupsRefresh
            // 
            this.toolGroupsRefresh.Name = "toolGroupsRefresh";
            this.toolGroupsRefresh.Size = new System.Drawing.Size(132, 22);
            this.toolGroupsRefresh.Text = "刷新";
            this.toolGroupsRefresh.Click += new System.EventHandler(this.toolGroupsRefresh_Click);
            // 
            // dataGridTags
            // 
            this.dataGridTags.AllowUserToAddRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridTags.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridTags.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridTags.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.TagName,
            this.OpcTagName,
            this.Value,
            this.Qualities,
            this.TimeStamps,
            this.DataType,
            this.Group,
            this.Message});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridTags.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridTags.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridTags.Location = new System.Drawing.Point(3, 17);
            this.dataGridTags.MultiSelect = false;
            this.dataGridTags.Name = "dataGridTags";
            this.dataGridTags.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridTags.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridTags.RowTemplate.Height = 23;
            this.dataGridTags.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridTags.Size = new System.Drawing.Size(895, 539);
            this.dataGridTags.TabIndex = 118;
            this.dataGridTags.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridTags_CellMouseUp);
            // 
            // TagName
            // 
            this.TagName.HeaderText = "名称";
            this.TagName.Name = "TagName";
            this.TagName.ReadOnly = true;
            // 
            // OpcTagName
            // 
            this.OpcTagName.HeaderText = "对应地址";
            this.OpcTagName.Name = "OpcTagName";
            this.OpcTagName.ReadOnly = true;
            this.OpcTagName.Width = 250;
            // 
            // Value
            // 
            this.Value.HeaderText = "数值";
            this.Value.Name = "Value";
            this.Value.ReadOnly = true;
            // 
            // Qualities
            // 
            this.Qualities.HeaderText = "质量";
            this.Qualities.Name = "Qualities";
            this.Qualities.ReadOnly = true;
            this.Qualities.Width = 75;
            // 
            // TimeStamps
            // 
            this.TimeStamps.HeaderText = "时间戳";
            this.TimeStamps.Name = "TimeStamps";
            this.TimeStamps.ReadOnly = true;
            this.TimeStamps.Width = 150;
            // 
            // DataType
            // 
            this.DataType.HeaderText = "类型";
            this.DataType.Name = "DataType";
            this.DataType.ReadOnly = true;
            // 
            // Group
            // 
            this.Group.HeaderText = "分组";
            this.Group.Name = "Group";
            this.Group.ReadOnly = true;
            this.Group.Visible = false;
            this.Group.Width = 50;
            // 
            // Message
            // 
            this.Message.HeaderText = "备注";
            this.Message.Name = "Message";
            this.Message.ReadOnly = true;
            this.Message.Width = 200;
            // 
            // contextMenuTags
            // 
            this.contextMenuTags.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolTagOAdd,
            this.toolTagDel,
            this.toolTagsRefresh});
            this.contextMenuTags.Name = "contextMenuGroup";
            this.contextMenuTags.Size = new System.Drawing.Size(123, 70);
            // 
            // toolTagOAdd
            // 
            this.toolTagOAdd.Name = "toolTagOAdd";
            this.toolTagOAdd.Size = new System.Drawing.Size(122, 22);
            this.toolTagOAdd.Text = "Tag新增";
            this.toolTagOAdd.Click += new System.EventHandler(this.toolTagAdd_Click);
            // 
            // toolTagDel
            // 
            this.toolTagDel.Name = "toolTagDel";
            this.toolTagDel.Size = new System.Drawing.Size(122, 22);
            this.toolTagDel.Text = "删除";
            this.toolTagDel.Click += new System.EventHandler(this.toolTagDel_Click);
            // 
            // toolTagsRefresh
            // 
            this.toolTagsRefresh.Name = "toolTagsRefresh";
            this.toolTagsRefresh.Size = new System.Drawing.Size(122, 22);
            this.toolTagsRefresh.Text = "刷新";
            this.toolTagsRefresh.Click += new System.EventHandler(this.toolTagsRefresh_Click);
            // 
            // lblGroup
            // 
            this.lblGroup.AutoSize = true;
            this.lblGroup.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblGroup.Location = new System.Drawing.Point(64, 14);
            this.lblGroup.Name = "lblGroup";
            this.lblGroup.Size = new System.Drawing.Size(63, 14);
            this.lblGroup.TabIndex = 122;
            this.lblGroup.Text = "lblGroup";
            // 
            // lblBlock
            // 
            this.lblBlock.AutoSize = true;
            this.lblBlock.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblBlock.Location = new System.Drawing.Point(64, 36);
            this.lblBlock.Name = "lblBlock";
            this.lblBlock.Size = new System.Drawing.Size(63, 14);
            this.lblBlock.TabIndex = 123;
            this.lblBlock.Text = "lblBlock";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(16, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 14);
            this.label2.TabIndex = 120;
            this.label2.Text = "Block:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(16, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 14);
            this.label1.TabIndex = 121;
            this.label1.Text = "Group:";
            // 
            // chkRefresh
            // 
            this.chkRefresh.AutoSize = true;
            this.chkRefresh.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chkRefresh.Location = new System.Drawing.Point(343, 22);
            this.chkRefresh.Name = "chkRefresh";
            this.chkRefresh.Size = new System.Drawing.Size(82, 18);
            this.chkRefresh.TabIndex = 124;
            this.chkRefresh.Text = "观察模式";
            this.chkRefresh.UseVisualStyleBackColor = true;
            this.chkRefresh.CheckedChanged += new System.EventHandler(this.chkRefresh_CheckedChanged);
            // 
            // cmbInterval
            // 
            this.cmbInterval.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbInterval.FormattingEnabled = true;
            this.cmbInterval.Items.AddRange(new object[] {
            "250",
            "500",
            "1000",
            "2000",
            "4000"});
            this.cmbInterval.Location = new System.Drawing.Point(96, 15);
            this.cmbInterval.Name = "cmbInterval";
            this.cmbInterval.Size = new System.Drawing.Size(53, 22);
            this.cmbInterval.TabIndex = 125;
            this.cmbInterval.SelectedIndexChanged += new System.EventHandler(this.cmbInterval_SelectedIndexChanged);
            // 
            // timerRefresh
            // 
            this.timerRefresh.Interval = 2000;
            this.timerRefresh.Tick += new System.EventHandler(this.timerRefresh_Tick);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(159, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(175, 14);
            this.label3.TabIndex = 120;
            this.label3.Text = "注：观察模式下不允许修改";
            // 
            // groupBoxWatch
            // 
            this.groupBoxWatch.Controls.Add(this.cmbInterval);
            this.groupBoxWatch.Controls.Add(this.label4);
            this.groupBoxWatch.Controls.Add(this.label3);
            this.groupBoxWatch.Enabled = false;
            this.groupBoxWatch.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBoxWatch.Location = new System.Drawing.Point(443, 15);
            this.groupBoxWatch.Name = "groupBoxWatch";
            this.groupBoxWatch.Size = new System.Drawing.Size(344, 50);
            this.groupBoxWatch.TabIndex = 126;
            this.groupBoxWatch.TabStop = false;
            this.groupBoxWatch.Text = "观察模式";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(5, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(91, 14);
            this.label4.TabIndex = 120;
            this.label4.Text = "刷新间隔(ms)";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.lblBlock);
            this.groupBox1.Controls.Add(this.lblGroup);
            this.groupBox1.Location = new System.Drawing.Point(6, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(187, 62);
            this.groupBox1.TabIndex = 127;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "当前节点";
            // 
            // btnSet
            // 
            this.btnSet.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSet.Location = new System.Drawing.Point(226, 12);
            this.btnSet.Name = "btnSet";
            this.btnSet.Size = new System.Drawing.Size(68, 28);
            this.btnSet.TabIndex = 1;
            this.btnSet.Text = "设置";
            this.btnSet.UseVisualStyleBackColor = true;
            this.btnSet.Click += new System.EventHandler(this.btnSet_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.treeTags);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(211, 639);
            this.groupBox2.TabIndex = 128;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Tag总览";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dataGridTags);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(901, 559);
            this.groupBox3.TabIndex = 129;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Tag明细";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslServerState,
            this.tsslversion,
            this.tsslServerStartTime});
            this.statusStrip1.Location = new System.Drawing.Point(0, 639);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1112, 22);
            this.statusStrip1.TabIndex = 130;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tsslServerState
            // 
            this.tsslServerState.Name = "tsslServerState";
            this.tsslServerState.Size = new System.Drawing.Size(131, 17);
            this.tsslServerState.Text = "toolStripStatusLabel1";
            // 
            // tsslversion
            // 
            this.tsslversion.Name = "tsslversion";
            this.tsslversion.Size = new System.Drawing.Size(131, 17);
            this.tsslversion.Text = "toolStripStatusLabel1";
            // 
            // tsslServerStartTime
            // 
            this.tsslServerStartTime.Name = "tsslServerStartTime";
            this.tsslServerStartTime.Size = new System.Drawing.Size(131, 17);
            this.tsslServerStartTime.Text = "toolStripStatusLabel1";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(211, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.btnSet);
            this.splitContainer1.Panel1.Controls.Add(this.btnRefresh);
            this.splitContainer1.Panel1.Controls.Add(this.chkRefresh);
            this.splitContainer1.Panel1.Controls.Add(this.groupBoxWatch);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox3);
            this.splitContainer1.Size = new System.Drawing.Size(901, 639);
            this.splitContainer1.SplitterDistance = 76;
            this.splitContainer1.TabIndex = 131;
            // 
            // Setting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1112, 661);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.statusStrip1);
            this.Name = "Setting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Monitor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Setting_FormClosing);
            this.Load += new System.EventHandler(this.Setting_Load);
            this.contextMenuBlock.ResumeLayout(false);
            this.contextMenuGroup.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridTags)).EndInit();
            this.contextMenuTags.ResumeLayout(false);
            this.groupBoxWatch.ResumeLayout(false);
            this.groupBoxWatch.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView treeTags;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.ContextMenuStrip contextMenuBlock;
        private System.Windows.Forms.ContextMenuStrip contextMenuGroup;
        private System.Windows.Forms.ToolStripMenuItem toolBlockAdd;
        private System.Windows.Forms.ToolStripMenuItem toolTagAdd;
        private System.Windows.Forms.ToolStripMenuItem toolBlockDel;
        private System.Windows.Forms.DataGridView dataGridTags;
        private System.Windows.Forms.ContextMenuStrip contextMenuTags;
        private System.Windows.Forms.ToolStripMenuItem toolTagDel;
        private System.Windows.Forms.ToolStripMenuItem toolGroupsRefresh;
        private System.Windows.Forms.Label lblGroup;
        private System.Windows.Forms.Label lblBlock;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripMenuItem toolTagOAdd;
        private System.Windows.Forms.CheckBox chkRefresh;
        private System.Windows.Forms.ComboBox cmbInterval;
        private System.Windows.Forms.ToolStripMenuItem toolTagsRefresh;
        private System.Windows.Forms.Timer timerRefresh;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBoxWatch;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnSet;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tsslServerState;
        private System.Windows.Forms.ToolStripStatusLabel tsslServerStartTime;
        private System.Windows.Forms.ToolStripStatusLabel tsslversion;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridViewTextBoxColumn TagName;
        private System.Windows.Forms.DataGridViewTextBoxColumn OpcTagName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Value;
        private System.Windows.Forms.DataGridViewTextBoxColumn Qualities;
        private System.Windows.Forms.DataGridViewTextBoxColumn TimeStamps;
        private System.Windows.Forms.DataGridViewTextBoxColumn DataType;
        private System.Windows.Forms.DataGridViewTextBoxColumn Group;
        private System.Windows.Forms.DataGridViewTextBoxColumn Message;
    }
}