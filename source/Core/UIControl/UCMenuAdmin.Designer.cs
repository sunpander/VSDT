namespace VSDT.UIControl
{
    partial class UCMenuAdmin
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
            this.treeViewMenu = new System.Windows.Forms.TreeView();
            this.comboBoxPlace = new System.Windows.Forms.ComboBox();
            this.textBoxDescription = new System.Windows.Forms.TextBox();
            this.label描述 = new System.Windows.Forms.Label();
            this.labelID = new System.Windows.Forms.Label();
            this.textBoxID = new System.Windows.Forms.TextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // treeViewMenu
            // 
            this.treeViewMenu.Location = new System.Drawing.Point(19, 55);
            this.treeViewMenu.Name = "treeViewMenu";
            this.treeViewMenu.Size = new System.Drawing.Size(159, 204);
            this.treeViewMenu.TabIndex = 0;
            // 
            // comboBoxPlace
            // 
            this.comboBoxPlace.FormattingEnabled = true;
            this.comboBoxPlace.Items.AddRange(new object[] {
            "主菜单栏(iPlat4C)",
            "编辑窗体弹出菜单",
            "资源管理器弹出菜单"});
            this.comboBoxPlace.Location = new System.Drawing.Point(19, 19);
            this.comboBoxPlace.Name = "comboBoxPlace";
            this.comboBoxPlace.Size = new System.Drawing.Size(156, 20);
            this.comboBoxPlace.TabIndex = 1;
            this.comboBoxPlace.SelectedIndexChanged += new System.EventHandler(this.comboBoxPlace_SelectedIndexChanged);
            // 
            // textBoxDescription
            // 
            this.textBoxDescription.Location = new System.Drawing.Point(255, 106);
            this.textBoxDescription.Name = "textBoxDescription";
            this.textBoxDescription.Size = new System.Drawing.Size(100, 21);
            this.textBoxDescription.TabIndex = 2;
            this.textBoxDescription.Text = "描述";
            // 
            // label描述
            // 
            this.label描述.AutoSize = true;
            this.label描述.Location = new System.Drawing.Point(210, 109);
            this.label描述.Name = "label描述";
            this.label描述.Size = new System.Drawing.Size(29, 12);
            this.label描述.TabIndex = 3;
            this.label描述.Text = "描述";
            // 
            // labelID
            // 
            this.labelID.AutoSize = true;
            this.labelID.Location = new System.Drawing.Point(214, 156);
            this.labelID.Name = "labelID";
            this.labelID.Size = new System.Drawing.Size(23, 12);
            this.labelID.TabIndex = 4;
            this.labelID.Text = "ID:";
            // 
            // textBoxID
            // 
            this.textBoxID.Location = new System.Drawing.Point(258, 157);
            this.textBoxID.Name = "textBoxID";
            this.textBoxID.Size = new System.Drawing.Size(100, 21);
            this.textBoxID.TabIndex = 5;
            this.textBoxID.Text = "ID";
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(216, 19);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 6;
            this.btnAdd.Text = "添加";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // UCMenuAdmin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.textBoxID);
            this.Controls.Add(this.labelID);
            this.Controls.Add(this.label描述);
            this.Controls.Add(this.textBoxDescription);
            this.Controls.Add(this.comboBoxPlace);
            this.Controls.Add(this.treeViewMenu);
            this.Name = "UCMenuAdmin";
            this.Size = new System.Drawing.Size(510, 304);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView treeViewMenu;
        private System.Windows.Forms.ComboBox comboBoxPlace;
        private System.Windows.Forms.TextBox textBoxDescription;
        private System.Windows.Forms.Label label描述;
        private System.Windows.Forms.Label labelID;
        private System.Windows.Forms.TextBox textBoxID;
        private System.Windows.Forms.Button btnAdd;
    }
}
