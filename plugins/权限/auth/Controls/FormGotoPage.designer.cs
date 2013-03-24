namespace  EF
{
    partial class FormGotoPage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormGotoPage));
            this.lbTotalPageCount = new DevExpress.XtraEditors.LabelControl();
            this.efDevSpinPageTo = new DevExpress.XtraEditors.SpinEdit();
            this.efLabel1 = new DevExpress.XtraEditors.LabelControl();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnOk = new  DevExpress.XtraEditors.SimpleButton();
            this.efDevSpinPageSize = new DevExpress.XtraEditors.SpinEdit();
            this.efLabel2 = new DevExpress.XtraEditors.LabelControl();
            this.lblTotalRecordCount = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.efDevSpinPageTo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCancel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOk)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.efDevSpinPageSize.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // lbTotalPageCount
            // 
            resources.ApplyResources(this.lbTotalPageCount, "lbTotalPageCount");
            this.lbTotalPageCount.Name = "lbTotalPageCount";
            // 
            // efDevSpinPageTo
            // 
            resources.ApplyResources(this.efDevSpinPageTo, "efDevSpinPageTo");
            this.efDevSpinPageTo.Name = "efDevSpinPageTo";
            this.efDevSpinPageTo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.efDevSpinPageTo.Properties.IsFloatValue = false;
            this.efDevSpinPageTo.Properties.Mask.EditMask = resources.GetString("efDevSpinPageTo.Properties.Mask.EditMask");
            // 
            // efLabel1
            // 
            resources.ApplyResources(this.efLabel1, "efLabel1");
            this.efLabel1.Name = "efLabel1";
            // 
            // btnCancel
            // 
      
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.Name = "btnCancel";
             this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
        
            resources.ApplyResources(this.btnOk, "btnOk");
            this.btnOk.Name = "btnOk";
 
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // efDevSpinPageSize
            // 
            resources.ApplyResources(this.efDevSpinPageSize, "efDevSpinPageSize");
            this.efDevSpinPageSize.Name = "efDevSpinPageSize";
            this.efDevSpinPageSize.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.efDevSpinPageSize.Properties.IsFloatValue = false;
            this.efDevSpinPageSize.Properties.Mask.EditMask = resources.GetString("efDevSpinPageSize.Properties.Mask.EditMask");
            this.efDevSpinPageSize.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(this.efDevSpinPageSize_EditValueChanging);
            // 
            // efLabel2
            // 
            resources.ApplyResources(this.efLabel2, "efLabel2");
            this.efLabel2.Name = "efLabel2";
            // 
            // lblTotalRecordCount
            // 
            resources.ApplyResources(this.lblTotalRecordCount, "lblTotalRecordCount");
            this.lblTotalRecordCount.Name = "lblTotalRecordCount";
            // 
            // FormGotoPage
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblTotalRecordCount);
            this.Controls.Add(this.efLabel2);
            this.Controls.Add(this.efDevSpinPageSize);
            this.Controls.Add(this.lbTotalPageCount);
            this.Controls.Add(this.efDevSpinPageTo);
            this.Controls.Add(this.efLabel1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormGotoPage";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Load += new System.EventHandler(this.FormGotoPage_Load);
            ((System.ComponentModel.ISupportInitialize)(this.efDevSpinPageTo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCancel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOk)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.efDevSpinPageSize.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnOk;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.LabelControl efLabel1;
        private DevExpress.XtraEditors.SpinEdit efDevSpinPageTo;
        private DevExpress.XtraEditors.LabelControl lbTotalPageCount;
        private DevExpress.XtraEditors.SpinEdit efDevSpinPageSize;
        private DevExpress.XtraEditors.LabelControl efLabel2;
        private DevExpress.XtraEditors.LabelControl lblTotalRecordCount;

    }
}