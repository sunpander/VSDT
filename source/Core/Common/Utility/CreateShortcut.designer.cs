namespace VSDT.Common.Utility
{
    partial class CreateShortcutForm
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
            this.cbxOndesk = new System.Windows.Forms.CheckBox();
            this.cbxStart = new System.Windows.Forms.CheckBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbxOndesk
            // 
            this.cbxOndesk.AutoSize = true;
            this.cbxOndesk.Checked = true;
            this.cbxOndesk.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbxOndesk.Location = new System.Drawing.Point(16, 22);
            this.cbxOndesk.Name = "cbxOndesk";
            this.cbxOndesk.Size = new System.Drawing.Size(66, 16);
            this.cbxOndesk.TabIndex = 2;
            this.cbxOndesk.Text = "Desktop";
            this.cbxOndesk.UseVisualStyleBackColor = true;
            // 
            // cbxStart
            // 
            this.cbxStart.AutoSize = true;
            this.cbxStart.Location = new System.Drawing.Point(16, 44);
            this.cbxStart.Name = "cbxStart";
            this.cbxStart.Size = new System.Drawing.Size(102, 16);
            this.cbxStart.TabIndex = 3;
            this.cbxStart.Text = "Windows Start";
            this.cbxStart.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(155, 27);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(68, 23);
            this.btnOk.TabIndex = 4;
            this.btnOk.Text = "Continue";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbxStart);
            this.groupBox1.Controls.Add(this.cbxOndesk);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(137, 85);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Shortcut Located";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(155, 56);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(68, 23);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Concel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // CreateShortcutForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(241, 107);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnOk);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CreateShortcutForm";
            this.Text = "CreateShortcut";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox cbxOndesk;
        private System.Windows.Forms.CheckBox cbxStart;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnCancel;
    }
}