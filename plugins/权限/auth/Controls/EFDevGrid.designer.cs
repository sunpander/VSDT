namespace EF
{
    partial class EFDevGrid
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EFDevGrid));
            this.imageListGridPageBar = new System.Windows.Forms.ImageList(this.components);
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // imageListGridPageBar
            // 
            this.imageListGridPageBar.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListGridPageBar.ImageStream")));
            this.imageListGridPageBar.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListGridPageBar.Images.SetKeyName(0, "first.ico");
            this.imageListGridPageBar.Images.SetKeyName(1, "pre.png");
            this.imageListGridPageBar.Images.SetKeyName(2, "next.png");
            this.imageListGridPageBar.Images.SetKeyName(3, "last.ico");
            this.imageListGridPageBar.Images.SetKeyName(4, "Filter.png");
            this.imageListGridPageBar.Images.SetKeyName(5, "branch_element.png");
            this.imageListGridPageBar.Images.SetKeyName(6, "export1.ico");
            this.imageListGridPageBar.Images.SetKeyName(7, "refresh.png");
            this.imageListGridPageBar.Images.SetKeyName(8, "window_split_ver.ico");
            this.imageListGridPageBar.Images.SetKeyName(9, "add2.png");
            this.imageListGridPageBar.Images.SetKeyName(10, "delete2.png");
            this.imageListGridPageBar.Images.SetKeyName(11, "copynew.ico");
            this.imageListGridPageBar.Images.SetKeyName(12, "goto.ico");
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList imageListGridPageBar;

    }
}
