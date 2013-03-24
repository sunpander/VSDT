namespace EF
{
    partial class EFPopupForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EFPopupForm));
            this.efBtnOk = new DevExpress.XtraEditors.SimpleButton();
            this.efBtnCancel = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.efBtnOk)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.efBtnCancel)).BeginInit();
            this.SuspendLayout();
            // 
            // efBtnOk
            // 
            resources.ApplyResources(this.efBtnOk, "efBtnOk");
            this.efBtnOk.Name = "efBtnOk";
            // 
            // efBtnCancel
            // 
            resources.ApplyResources(this.efBtnCancel, "efBtnCancel");
            this.efBtnCancel.Name = "efBtnCancel";
            this.efBtnCancel.Click += new System.EventHandler(this.efBtnCancel_Click);
            // 
            // EFPopupForm
            // 
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.efBtnCancel);
            this.Controls.Add(this.efBtnOk);
            this.Name = "EFPopupForm";
            ((System.ComponentModel.ISupportInitialize)(this.efBtnOk)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.efBtnCancel)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public DevExpress.XtraEditors.SimpleButton efBtnOk;
        public DevExpress.XtraEditors.SimpleButton efBtnCancel;

    }
}
