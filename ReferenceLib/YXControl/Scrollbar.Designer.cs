namespace YXControl
{
    partial class Scrollbar
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
            this.LeftPart = new System.Windows.Forms.Label();
            this.RightPart = new System.Windows.Forms.Label();
            this.LeftPanel = new System.Windows.Forms.Panel();
            this.MainPanel = new System.Windows.Forms.Panel();
            this.ScrollLabel = new System.Windows.Forms.Label();
            this.RightPanel = new System.Windows.Forms.Panel();
            this.MainPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // LeftPart
            // 
            this.LeftPart.BackColor = System.Drawing.Color.Transparent;
            this.LeftPart.Dock = System.Windows.Forms.DockStyle.Left;
            this.LeftPart.Location = new System.Drawing.Point(0, 0);
            this.LeftPart.Name = "LeftPart";
            this.LeftPart.Size = new System.Drawing.Size(100, 30);
            this.LeftPart.TabIndex = 0;
            this.LeftPart.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // RightPart
            // 
            this.RightPart.BackColor = System.Drawing.Color.Transparent;
            this.RightPart.Dock = System.Windows.Forms.DockStyle.Right;
            this.RightPart.Location = new System.Drawing.Point(300, 0);
            this.RightPart.Name = "RightPart";
            this.RightPart.Size = new System.Drawing.Size(100, 30);
            this.RightPart.TabIndex = 1;
            this.RightPart.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // LeftPanel
            // 
            this.LeftPanel.BackColor = System.Drawing.Color.Transparent;
            this.LeftPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.LeftPanel.Location = new System.Drawing.Point(0, 0);
            this.LeftPanel.Name = "LeftPanel";
            this.LeftPanel.Size = new System.Drawing.Size(3, 30);
            this.LeftPanel.TabIndex = 2;
            this.LeftPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.LeftPanel_MouseDown);
            // 
            // MainPanel
            // 
            this.MainPanel.Controls.Add(this.ScrollLabel);
            this.MainPanel.Controls.Add(this.RightPanel);
            this.MainPanel.Controls.Add(this.LeftPanel);
            this.MainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainPanel.Location = new System.Drawing.Point(100, 0);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(200, 30);
            this.MainPanel.TabIndex = 3;
            this.MainPanel.SizeChanged += new System.EventHandler(this.MainPanel_SizeChanged);
            // 
            // Scroll
            // 
            this.ScrollLabel.BackColor = System.Drawing.Color.Transparent;
            this.ScrollLabel.Location = new System.Drawing.Point(0, 0);
            this.ScrollLabel.Name = "Scroll";
            this.ScrollLabel.Size = new System.Drawing.Size(17, 30);
            this.ScrollLabel.TabIndex = 0;
            this.ScrollLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ScrollLabel.LocationChanged += new System.EventHandler(this.Scroll_LocationChanged);
            this.ScrollLabel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Scroll_MouseMove);
            this.ScrollLabel.Move += new System.EventHandler(this.Scroll_LocationChanged);
            this.ScrollLabel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Scroll_MouseDown);
            this.ScrollLabel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Scroll_MouseUp);
            // 
            // RightPanel
            // 
            this.RightPanel.BackColor = System.Drawing.Color.Transparent;
            this.RightPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.RightPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RightPanel.Location = new System.Drawing.Point(0, 0);
            this.RightPanel.Name = "RightPanel";
            this.RightPanel.Size = new System.Drawing.Size(200, 30);
            this.RightPanel.TabIndex = 0;
            this.RightPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.RightPanel_MouseDown);
            // 
            // Scrollbar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.Controls.Add(this.MainPanel);
            this.Controls.Add(this.RightPart);
            this.Controls.Add(this.LeftPart);
            this.Name = "Scrollbar";
            this.Size = new System.Drawing.Size(400, 30);
            this.MainPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label LeftPart;
        private System.Windows.Forms.Label RightPart;
        private System.Windows.Forms.Panel LeftPanel;
        private System.Windows.Forms.Panel MainPanel;
        private System.Windows.Forms.Label ScrollLabel;
        private System.Windows.Forms.Panel RightPanel;
    }
}
