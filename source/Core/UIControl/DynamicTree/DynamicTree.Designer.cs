namespace VSDT.UIControl
{
    partial class DynamicTree
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.btnSaveTree = new System.Windows.Forms.Button();
            this.btnEditTree = new System.Windows.Forms.Button();
            this.btnUseDefautTree = new System.Windows.Forms.Button();
            this.txtXmlPath = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ToolStripMenuItemAddNode = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemDelNode = new System.Windows.Forms.ToolStripMenuItem();
            this.btnBrowser = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.treeView1);
            this.groupBox2.Location = new System.Drawing.Point(6, 37);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(0);
            this.groupBox2.Size = new System.Drawing.Size(208, 274);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Location = new System.Drawing.Point(0, 14);
            this.treeView1.Margin = new System.Windows.Forms.Padding(0);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(208, 260);
            this.treeView1.TabIndex = 0;
            this.treeView1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.treeView1_MouseDoubleClick);
            this.treeView1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.treeView1_MouseDown);
            // 
            // btnSaveTree
            // 
            this.btnSaveTree.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSaveTree.Enabled = false;
            this.btnSaveTree.Location = new System.Drawing.Point(86, 317);
            this.btnSaveTree.Name = "btnSaveTree";
            this.btnSaveTree.Size = new System.Drawing.Size(54, 23);
            this.btnSaveTree.TabIndex = 6;
            this.btnSaveTree.Text = "保存树";
            this.btnSaveTree.UseVisualStyleBackColor = true;
            this.btnSaveTree.Click += new System.EventHandler(this.btnSaveTree_Click);
            // 
            // btnEditTree
            // 
            this.btnEditTree.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnEditTree.Location = new System.Drawing.Point(6, 317);
            this.btnEditTree.Name = "btnEditTree";
            this.btnEditTree.Size = new System.Drawing.Size(54, 23);
            this.btnEditTree.TabIndex = 5;
            this.btnEditTree.Text = "编辑树";
            this.btnEditTree.UseVisualStyleBackColor = true;
            this.btnEditTree.Click += new System.EventHandler(this.btnEditTree_Click);
            // 
            // btnUseDefautTree
            // 
            this.btnUseDefautTree.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnUseDefautTree.Enabled = false;
            this.btnUseDefautTree.Location = new System.Drawing.Point(160, 317);
            this.btnUseDefautTree.Name = "btnUseDefautTree";
            this.btnUseDefautTree.Size = new System.Drawing.Size(54, 23);
            this.btnUseDefautTree.TabIndex = 7;
            this.btnUseDefautTree.Text = "默认树";
            this.btnUseDefautTree.UseVisualStyleBackColor = true;
            this.btnUseDefautTree.Click += new System.EventHandler(this.btnUseDefautTree_Click);
            // 
            // txtXmlPath
            // 
            this.txtXmlPath.Location = new System.Drawing.Point(6, 10);
            this.txtXmlPath.Name = "txtXmlPath";
            this.txtXmlPath.Size = new System.Drawing.Size(151, 21);
            this.txtXmlPath.TabIndex = 8;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemAddNode,
            this.ToolStripMenuItemDelNode});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(125, 48);
            // 
            // ToolStripMenuItemAddNode
            // 
            this.ToolStripMenuItemAddNode.Name = "ToolStripMenuItemAddNode";
            this.ToolStripMenuItemAddNode.Size = new System.Drawing.Size(124, 22);
            this.ToolStripMenuItemAddNode.Text = "添加节点";
            this.ToolStripMenuItemAddNode.Click += new System.EventHandler(this.ToolStripMenuItemAddNode_Click);
            // 
            // ToolStripMenuItemDelNode
            // 
            this.ToolStripMenuItemDelNode.Name = "ToolStripMenuItemDelNode";
            this.ToolStripMenuItemDelNode.Size = new System.Drawing.Size(124, 22);
            this.ToolStripMenuItemDelNode.Text = "删除节点";
            this.ToolStripMenuItemDelNode.Click += new System.EventHandler(this.ToolStripMenuItemDelNode_Click);
            // 
            // btnBrowser
            // 
            this.btnBrowser.Location = new System.Drawing.Point(163, 10);
            this.btnBrowser.Name = "btnBrowser";
            this.btnBrowser.Size = new System.Drawing.Size(51, 23);
            this.btnBrowser.TabIndex = 9;
            this.btnBrowser.Text = "浏览";
            this.btnBrowser.UseVisualStyleBackColor = true;
            this.btnBrowser.Click += new System.EventHandler(this.btnBrowser_Click);
            // 
            // DynamicTree
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnBrowser);
            this.Controls.Add(this.txtXmlPath);
            this.Controls.Add(this.btnUseDefautTree);
            this.Controls.Add(this.btnSaveTree);
            this.Controls.Add(this.btnEditTree);
            this.Controls.Add(this.groupBox2);
            this.Name = "DynamicTree";
            this.Size = new System.Drawing.Size(220, 343);
            this.groupBox2.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSaveTree;
        private System.Windows.Forms.Button btnEditTree;
        private System.Windows.Forms.Button btnUseDefautTree;
        private System.Windows.Forms.TextBox txtXmlPath;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemAddNode;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemDelNode;
        private System.Windows.Forms.Button btnBrowser;
        public System.Windows.Forms.TreeView treeView1;
        public System.Windows.Forms.GroupBox groupBox2;
    }
}
