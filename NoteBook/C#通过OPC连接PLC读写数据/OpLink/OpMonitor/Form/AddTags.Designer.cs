namespace OpMonitor
{
    partial class AddTags
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
            this.dataGridTags = new System.Windows.Forms.DataGridView();
            this.TagName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OpcTagName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Qualities = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TimeStamps = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DataType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Group = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeViewBranches = new System.Windows.Forms.TreeView();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.listItems = new System.Windows.Forms.CheckedListBox();
            this.lblGroup = new System.Windows.Forms.Label();
            this.lblBlock = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.chkAll = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tsslItemsNum = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslTagsNum = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnCheckRepeat = new System.Windows.Forms.Button();
            this.btnAddTags = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.contextMenuTags = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripTagDel = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridTags)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.contextMenuTags.SuspendLayout();
            this.SuspendLayout();
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
            this.Group});
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
            this.dataGridTags.Name = "dataGridTags";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridTags.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridTags.RowTemplate.Height = 23;
            this.dataGridTags.Size = new System.Drawing.Size(716, 222);
            this.dataGridTags.TabIndex = 117;
            this.dataGridTags.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridTags_CellMouseUp);
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
            this.OpcTagName.Width = 300;
            // 
            // Value
            // 
            this.Value.HeaderText = "Tag值";
            this.Value.Name = "Value";
            this.Value.Visible = false;
            // 
            // Qualities
            // 
            this.Qualities.HeaderText = "质量";
            this.Qualities.Name = "Qualities";
            this.Qualities.Visible = false;
            // 
            // TimeStamps
            // 
            this.TimeStamps.HeaderText = "时间戳";
            this.TimeStamps.Name = "TimeStamps";
            this.TimeStamps.Visible = false;
            // 
            // DataType
            // 
            this.DataType.HeaderText = "类型";
            this.DataType.Name = "DataType";
            this.DataType.Visible = false;
            // 
            // Group
            // 
            this.Group.HeaderText = "分组";
            this.Group.Name = "Group";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.splitContainer1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(722, 169);
            this.groupBox1.TabIndex = 119;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "OPC Item列表";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 17);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeViewBranches);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(716, 149);
            this.splitContainer1.SplitterDistance = 238;
            this.splitContainer1.TabIndex = 0;
            // 
            // treeViewBranches
            // 
            this.treeViewBranches.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewBranches.Location = new System.Drawing.Point(0, 0);
            this.treeViewBranches.Name = "treeViewBranches";
            this.treeViewBranches.Size = new System.Drawing.Size(238, 149);
            this.treeViewBranches.TabIndex = 1;
            this.treeViewBranches.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewBranches_AfterSelect);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.listItems);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.lblGroup);
            this.splitContainer2.Panel2.Controls.Add(this.lblBlock);
            this.splitContainer2.Panel2.Controls.Add(this.label2);
            this.splitContainer2.Panel2.Controls.Add(this.label1);
            this.splitContainer2.Panel2.Controls.Add(this.chkAll);
            this.splitContainer2.Size = new System.Drawing.Size(474, 149);
            this.splitContainer2.SplitterDistance = 343;
            this.splitContainer2.TabIndex = 2;
            // 
            // listItems
            // 
            this.listItems.CheckOnClick = true;
            this.listItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listItems.FormattingEnabled = true;
            this.listItems.Location = new System.Drawing.Point(0, 0);
            this.listItems.Name = "listItems";
            this.listItems.Size = new System.Drawing.Size(343, 149);
            this.listItems.TabIndex = 0;
            // 
            // lblGroup
            // 
            this.lblGroup.AutoSize = true;
            this.lblGroup.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblGroup.Location = new System.Drawing.Point(3, 53);
            this.lblGroup.Name = "lblGroup";
            this.lblGroup.Size = new System.Drawing.Size(63, 14);
            this.lblGroup.TabIndex = 2;
            this.lblGroup.Text = "lblGroup";
            // 
            // lblBlock
            // 
            this.lblBlock.AutoSize = true;
            this.lblBlock.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblBlock.Location = new System.Drawing.Point(3, 96);
            this.lblBlock.Name = "lblBlock";
            this.lblBlock.Size = new System.Drawing.Size(63, 14);
            this.lblBlock.TabIndex = 2;
            this.lblBlock.Text = "lblBlock";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(3, 79);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 14);
            this.label2.TabIndex = 2;
            this.label2.Text = "Block:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(3, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 14);
            this.label1.TabIndex = 2;
            this.label1.Text = "Group:";
            // 
            // chkAll
            // 
            this.chkAll.AutoSize = true;
            this.chkAll.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chkAll.Location = new System.Drawing.Point(2, 3);
            this.chkAll.Name = "chkAll";
            this.chkAll.Size = new System.Drawing.Size(54, 18);
            this.chkAll.TabIndex = 1;
            this.chkAll.Text = "全选";
            this.chkAll.UseVisualStyleBackColor = true;
            this.chkAll.CheckedChanged += new System.EventHandler(this.chkAll_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dataGridTags);
            this.groupBox2.Location = new System.Drawing.Point(0, 169);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(722, 242);
            this.groupBox2.TabIndex = 120;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Tag新增";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslItemsNum,
            this.tsslTagsNum});
            this.statusStrip1.Location = new System.Drawing.Point(0, 452);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(722, 22);
            this.statusStrip1.TabIndex = 121;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tsslItemsNum
            // 
            this.tsslItemsNum.Name = "tsslItemsNum";
            this.tsslItemsNum.Size = new System.Drawing.Size(65, 17);
            this.tsslItemsNum.Text = "Item数量:0";
            // 
            // tsslTagsNum
            // 
            this.tsslTagsNum.Name = "tsslTagsNum";
            this.tsslTagsNum.Size = new System.Drawing.Size(59, 17);
            this.tsslTagsNum.Text = "Tag数量:0";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnCheckRepeat);
            this.panel1.Controls.Add(this.btnAddTags);
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 394);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(722, 58);
            this.panel1.TabIndex = 122;
            // 
            // btnCheckRepeat
            // 
            this.btnCheckRepeat.Location = new System.Drawing.Point(421, 20);
            this.btnCheckRepeat.Name = "btnCheckRepeat";
            this.btnCheckRepeat.Size = new System.Drawing.Size(75, 23);
            this.btnCheckRepeat.TabIndex = 0;
            this.btnCheckRepeat.Text = "检查重复性";
            this.btnCheckRepeat.UseVisualStyleBackColor = true;
            this.btnCheckRepeat.Click += new System.EventHandler(this.btnCheckRepeat_Click);
            // 
            // btnAddTags
            // 
            this.btnAddTags.Location = new System.Drawing.Point(519, 20);
            this.btnAddTags.Name = "btnAddTags";
            this.btnAddTags.Size = new System.Drawing.Size(75, 23);
            this.btnAddTags.TabIndex = 0;
            this.btnAddTags.Text = "新增";
            this.btnAddTags.UseVisualStyleBackColor = true;
            this.btnAddTags.Click += new System.EventHandler(this.btnAddTags_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(610, 20);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // contextMenuTags
            // 
            this.contextMenuTags.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripTagDel});
            this.contextMenuTags.Name = "contextMenuTags";
            this.contextMenuTags.Size = new System.Drawing.Size(95, 26);
            // 
            // toolStripTagDel
            // 
            this.toolStripTagDel.Name = "toolStripTagDel";
            this.toolStripTagDel.Size = new System.Drawing.Size(94, 22);
            this.toolStripTagDel.Text = "删除";
            this.toolStripTagDel.Click += new System.EventHandler(this.toolStripTagDel_Click);
            // 
            // AddTags
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(722, 474);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "AddTags";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "AddTags";
            this.Load += new System.EventHandler(this.AddTags_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridTags)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.contextMenuTags.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridTags;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckedListBox listItems;
        private System.Windows.Forms.TreeView treeViewBranches;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tsslItemsNum;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnAddTags;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.CheckBox chkAll;
        private System.Windows.Forms.Label lblGroup;
        private System.Windows.Forms.Label lblBlock;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ContextMenuStrip contextMenuTags;
        private System.Windows.Forms.ToolStripMenuItem toolStripTagDel;
        private System.Windows.Forms.Button btnCheckRepeat;
        private System.Windows.Forms.DataGridViewTextBoxColumn TagName;
        private System.Windows.Forms.DataGridViewTextBoxColumn OpcTagName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Value;
        private System.Windows.Forms.DataGridViewTextBoxColumn Qualities;
        private System.Windows.Forms.DataGridViewTextBoxColumn TimeStamps;
        private System.Windows.Forms.DataGridViewTextBoxColumn DataType;
        private System.Windows.Forms.DataGridViewTextBoxColumn Group;
        private System.Windows.Forms.ToolStripStatusLabel tsslTagsNum;
    }
}