namespace EF
{
    partial class FormSetColumnFixed
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
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.efDevSpinEdit1 = new DevExpress.XtraEditors.SpinEdit();
            this.efLabel2 = new DevExpress.XtraEditors.LabelControl();
            this.efGroupBox1 = new DevExpress.XtraEditors.GroupControl();
            this.efLabel1 = new DevExpress.XtraEditors.LabelControl();
            this.efDevSpinEdit2 = new DevExpress.XtraEditors.SpinEdit();
            this.btnApply = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.efDevSpinEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.efGroupBox1)).BeginInit();
            this.efGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.efDevSpinEdit2.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // efDevSpinEdit1
            // 
            this.efDevSpinEdit1.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.efDevSpinEdit1.Location = new System.Drawing.Point(20, 58);
            this.efDevSpinEdit1.Name = "efDevSpinEdit1";
            this.efDevSpinEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.efDevSpinEdit1.Properties.IsFloatValue = false;
            this.efDevSpinEdit1.Properties.Mask.EditMask = "N00";
            this.efDevSpinEdit1.Properties.MaxValue = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.efDevSpinEdit1.Size = new System.Drawing.Size(150, 21);
            this.efDevSpinEdit1.TabIndex = 4;
            this.efDevSpinEdit1.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(this.efDevSpinEdit1_EditValueChanging);
            this.efDevSpinEdit1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.efDevSpinEdit1_KeyDown);
            // 
            // efLabel2
            // 
            this.efLabel2.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.efLabel2.Location = new System.Drawing.Point(20, 26);
            this.efLabel2.Name = "efLabel2";
            this.efLabel2.Size = new System.Drawing.Size(150, 26);
            this.efLabel2.TabIndex = 3;
            this.efLabel2.Text = "左停靠的列数";
            // 
            // efGroupBox1
            // 
            this.efGroupBox1.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.efGroupBox1.Appearance.Options.UseBackColor = true;
            this.efGroupBox1.Controls.Add(this.efLabel1);
            this.efGroupBox1.Controls.Add(this.efDevSpinEdit2);
            this.efGroupBox1.Controls.Add(this.efLabel2);
            this.efGroupBox1.Controls.Add(this.efDevSpinEdit1);
            this.efGroupBox1.Location = new System.Drawing.Point(3, 3);
            this.efGroupBox1.Name = "efGroupBox1";
            this.efGroupBox1.Size = new System.Drawing.Size(353, 153);
            this.efGroupBox1.TabIndex = 5;
            this.efGroupBox1.Text = "列停靠方式";
            // 
            // efLabel1
            // 
            this.efLabel1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.efLabel1.Location = new System.Drawing.Point(20, 85);
            this.efLabel1.Name = "efLabel1";
            this.efLabel1.Size = new System.Drawing.Size(150, 26);
            this.efLabel1.TabIndex = 3;
            this.efLabel1.Text = "右停靠的列数";
            // 
            // efDevSpinEdit2
            // 
            this.efDevSpinEdit2.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.efDevSpinEdit2.Location = new System.Drawing.Point(20, 117);
            this.efDevSpinEdit2.Name = "efDevSpinEdit2";
            this.efDevSpinEdit2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.efDevSpinEdit2.Properties.IsFloatValue = false;
            this.efDevSpinEdit2.Properties.Mask.EditMask = "N00";
            this.efDevSpinEdit2.Properties.MaxValue = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.efDevSpinEdit2.Size = new System.Drawing.Size(150, 21);
            this.efDevSpinEdit2.TabIndex = 4;
            this.efDevSpinEdit2.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(this.efDevSpinEdit1_EditValueChanging);
            this.efDevSpinEdit2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.efDevSpinEdit1_KeyDown);
            // 
            // btnApply
            // 
            this.btnApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnApply.Enabled = false;
            this.btnApply.Location = new System.Drawing.Point(275, 162);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(75, 23);
            this.btnApply.TabIndex = 8;
            this.btnApply.Text = "应用";
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(194, 162);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(113, 162);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 6;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // FormSetColumnFixed
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(358, 197);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.efGroupBox1);
            this.Name = "FormSetColumnFixed";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "设置";
            this.TopMost = true;
            this.Shown += new System.EventHandler(this.FrmSetColumnFixed_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.efDevSpinEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.efGroupBox1)).EndInit();
            this.efGroupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.efDevSpinEdit2.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl efLabel2;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private DevExpress.XtraEditors.SpinEdit efDevSpinEdit1;
        private DevExpress.XtraEditors.GroupControl efGroupBox1;
        private DevExpress.XtraEditors.SimpleButton btnApply;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.LabelControl efLabel1;
        private DevExpress.XtraEditors.SpinEdit efDevSpinEdit2;
    }
}