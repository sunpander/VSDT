namespace demo
{
	partial class Form1
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.AlwaysDropDown = new System.Windows.Forms.CheckBox();
            this.AlwaysHoverChange = new System.Windows.Forms.CheckBox();
            this.PersistDropDownName = new System.Windows.Forms.CheckBox();
            this.splitButton1 = new ExoticControls.SplitButton();
            this.splitButton2 = new ExoticControls.SplitButton();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(12, 77);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(268, 110);
            this.textBox1.TabIndex = 2;
            // 
            // AlwaysDropDown
            // 
            this.AlwaysDropDown.AutoSize = true;
            this.AlwaysDropDown.Location = new System.Drawing.Point(12, 11);
            this.AlwaysDropDown.Name = "AlwaysDropDown";
            this.AlwaysDropDown.Size = new System.Drawing.Size(108, 16);
            this.AlwaysDropDown.TabIndex = 4;
            this.AlwaysDropDown.Text = "AlwaysDropDown";
            this.AlwaysDropDown.UseVisualStyleBackColor = true;
            this.AlwaysDropDown.CheckedChanged += new System.EventHandler(this.AlwaysDropDown_CheckedChanged);
            // 
            // AlwaysHoverChange
            // 
            this.AlwaysHoverChange.AutoSize = true;
            this.AlwaysHoverChange.Location = new System.Drawing.Point(12, 33);
            this.AlwaysHoverChange.Name = "AlwaysHoverChange";
            this.AlwaysHoverChange.Size = new System.Drawing.Size(126, 16);
            this.AlwaysHoverChange.TabIndex = 5;
            this.AlwaysHoverChange.Text = "AlwaysHoverChange";
            this.AlwaysHoverChange.UseVisualStyleBackColor = true;
            this.AlwaysHoverChange.CheckedChanged += new System.EventHandler(this.AlwaysHoverChange_CheckedChanged);
            // 
            // PersistDropDownName
            // 
            this.PersistDropDownName.AutoSize = true;
            this.PersistDropDownName.Location = new System.Drawing.Point(12, 55);
            this.PersistDropDownName.Name = "PersistDropDownName";
            this.PersistDropDownName.Size = new System.Drawing.Size(138, 16);
            this.PersistDropDownName.TabIndex = 6;
            this.PersistDropDownName.Text = "PersistDropDownName";
            this.PersistDropDownName.UseVisualStyleBackColor = true;
            this.PersistDropDownName.CheckedChanged += new System.EventHandler(this.PersistDropDownName_CheckedChanged);
            // 
            // splitButton1
            // 
            this.splitButton1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitButton1.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.splitButton1.ImageKey = "Normal";
            this.splitButton1.Location = new System.Drawing.Point(12, 366);
            this.splitButton1.Name = "splitButton1";
            this.splitButton1.Size = new System.Drawing.Size(268, 27);
            this.splitButton1.TabIndex = 0;
            this.splitButton1.Text = "splitButton1";
            this.splitButton1.UseVisualStyleBackColor = true;
            this.splitButton1.MouseLeave += new System.EventHandler(this.splitButton1_MouseLeave);
            this.splitButton1.Click += new System.EventHandler(this.splitButton1_Click);
            this.splitButton1.ButtonClick += new System.EventHandler(this.splitButton1_ButtonClick);
            this.splitButton1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.splitButton1_MouseDown);
            this.splitButton1.MouseHover += new System.EventHandler(this.splitButton1_MouseHover);
            this.splitButton1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.splitButton1_MouseUp);
            this.splitButton1.MouseEnter += new System.EventHandler(this.splitButton1_MouseEnter);
            // 
            // splitButton2
            // 
            this.splitButton2.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.splitButton2.ImageKey = "Normal";
            this.splitButton2.Location = new System.Drawing.Point(81, 224);
            this.splitButton2.Name = "splitButton2";
            this.splitButton2.Size = new System.Drawing.Size(160, 23);
            this.splitButton2.TabIndex = 7;
            this.splitButton2.Text = "test";
            this.splitButton2.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 404);
            this.Controls.Add(this.splitButton2);
            this.Controls.Add(this.PersistDropDownName);
            this.Controls.Add(this.AlwaysHoverChange);
            this.Controls.Add(this.AlwaysDropDown);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.splitButton1);
            this.Name = "Form1";
            this.Text = "SplitButton Demo";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

        private ExoticControls.SplitButton splitButton1;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.CheckBox AlwaysDropDown;
		private System.Windows.Forms.CheckBox AlwaysHoverChange;
		private System.Windows.Forms.CheckBox PersistDropDownName;
        private ExoticControls.SplitButton splitButton2;
	}
}

