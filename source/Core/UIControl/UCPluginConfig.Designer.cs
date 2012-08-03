namespace VSDT.UIControl
{
    partial class UCPluginConfig
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
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnUnload = new System.Windows.Forms.Button();
            this.textBoxdescirpt = new System.Windows.Forms.TextBox();
            this.groupBoxPlugInList = new System.Windows.Forms.GroupBox();
            this.gridViewPlugins = new System.Windows.Forms.DataGridView();
            this.SymbolicName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.runTimeState = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBoxPlugInList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewPlugins)).BeginInit();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStart.Location = new System.Drawing.Point(115, 221);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 10;
            this.btnStart.Text = "启动";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStop.Location = new System.Drawing.Point(196, 221);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 23);
            this.btnStop.TabIndex = 11;
            this.btnStop.Text = "禁用";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnUnload
            // 
            this.btnUnload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUnload.Location = new System.Drawing.Point(283, 221);
            this.btnUnload.Name = "btnUnload";
            this.btnUnload.Size = new System.Drawing.Size(75, 23);
            this.btnUnload.TabIndex = 12;
            this.btnUnload.Text = "卸载";
            this.btnUnload.UseVisualStyleBackColor = true;
            // 
            // textBoxdescirpt
            // 
            this.textBoxdescirpt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxdescirpt.Location = new System.Drawing.Point(3, 250);
            this.textBoxdescirpt.Multiline = true;
            this.textBoxdescirpt.Name = "textBoxdescirpt";
            this.textBoxdescirpt.Size = new System.Drawing.Size(355, 89);
            this.textBoxdescirpt.TabIndex = 13;
            // 
            // groupBoxPlugInList
            // 
            this.groupBoxPlugInList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxPlugInList.Controls.Add(this.gridViewPlugins);
            this.groupBoxPlugInList.Location = new System.Drawing.Point(3, 3);
            this.groupBoxPlugInList.Name = "groupBoxPlugInList";
            this.groupBoxPlugInList.Size = new System.Drawing.Size(358, 212);
            this.groupBoxPlugInList.TabIndex = 14;
            this.groupBoxPlugInList.TabStop = false;
            this.groupBoxPlugInList.Text = "插件列表";
            // 
            // gridViewPlugins
            // 
            this.gridViewPlugins.AllowUserToAddRows = false;
            this.gridViewPlugins.AllowUserToDeleteRows = false;
            this.gridViewPlugins.AllowUserToResizeRows = false;
            this.gridViewPlugins.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridViewPlugins.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SymbolicName,
            this.runTimeState});
            this.gridViewPlugins.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridViewPlugins.Location = new System.Drawing.Point(3, 17);
            this.gridViewPlugins.MultiSelect = false;
            this.gridViewPlugins.Name = "gridViewPlugins";
            this.gridViewPlugins.RowTemplate.Height = 23;
            this.gridViewPlugins.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridViewPlugins.Size = new System.Drawing.Size(352, 192);
            this.gridViewPlugins.TabIndex = 0;
            this.gridViewPlugins.SelectionChanged += new System.EventHandler(this.gridViewPlugins_SelectionChanged);
            this.gridViewPlugins.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridViewPlugins_CellContentClick);
            // 
            // SymbolicName
            // 
            this.SymbolicName.DataPropertyName = "SymbolicName";
            this.SymbolicName.HeaderText = "名称";
            this.SymbolicName.Name = "SymbolicName";
            // 
            // runTimeState
            // 
            this.runTimeState.DataPropertyName = "RunTimeState";
            this.runTimeState.HeaderText = "状态";
            this.runTimeState.Name = "runTimeState";
            this.runTimeState.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.runTimeState.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // UCPluginConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBoxPlugInList);
            this.Controls.Add(this.textBoxdescirpt);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnUnload);
            this.Name = "UCPluginConfig";
            this.Size = new System.Drawing.Size(365, 342);
            this.Load += new System.EventHandler(this.UCPlugInConfig_Load);
            this.groupBoxPlugInList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridViewPlugins)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnUnload;
        private System.Windows.Forms.TextBox textBoxdescirpt;
        private System.Windows.Forms.GroupBox groupBoxPlugInList;
        private System.Windows.Forms.DataGridView gridViewPlugins;
        private System.Windows.Forms.DataGridViewTextBoxColumn SymbolicName;
        private System.Windows.Forms.DataGridViewTextBoxColumn runTimeState;
    }
}
