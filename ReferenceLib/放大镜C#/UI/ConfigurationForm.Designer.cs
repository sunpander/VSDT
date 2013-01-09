namespace Magnifier20070401
{
    partial class ConfigurationForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigurationForm));
            this.lbl_ZoomFactor = new System.Windows.Forms.Label();
            this.cb_Symmetry = new System.Windows.Forms.CheckBox();
            this.btn_Close = new System.Windows.Forms.Button();
            this.tb_ZoomFactor = new System.Windows.Forms.TrackBar();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cb_CloseOnMouseUp = new System.Windows.Forms.CheckBox();
            this.cb_DoubleBuffered = new System.Windows.Forms.CheckBox();
            this.cb_RememberLastPoint = new System.Windows.Forms.CheckBox();
            this.cb_ReturnToOrigin = new System.Windows.Forms.CheckBox();
            this.cb_ShowInTaskbar = new System.Windows.Forms.CheckBox();
            this.cb_HideMouseCursor = new System.Windows.Forms.CheckBox();
            this.cb_TopMostWindow = new System.Windows.Forms.CheckBox();
            this.lbl_ZF = new System.Windows.Forms.Label();
            this.lbl_SF = new System.Windows.Forms.Label();
            this.lbl_MW = new System.Windows.Forms.Label();
            this.lbl_MH = new System.Windows.Forms.Label();
            this.tb_SpeedFactor = new System.Windows.Forms.TrackBar();
            this.tb_Width = new System.Windows.Forms.TrackBar();
            this.tb_Height = new System.Windows.Forms.TrackBar();
            this.lbl_SpeedFactor = new System.Windows.Forms.Label();
            this.lbl_Width = new System.Windows.Forms.Label();
            this.lbl_Height = new System.Windows.Forms.Label();
            this.pb_About = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.tb_ZoomFactor)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tb_SpeedFactor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tb_Width)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tb_Height)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_About)).BeginInit();
            this.SuspendLayout();
            // 
            // lbl_ZoomFactor
            // 
            this.lbl_ZoomFactor.Location = new System.Drawing.Point(323, 20);
            this.lbl_ZoomFactor.Name = "lbl_ZoomFactor";
            this.lbl_ZoomFactor.Size = new System.Drawing.Size(60, 16);
            this.lbl_ZoomFactor.TabIndex = 21;
            this.lbl_ZoomFactor.Text = "?";
            // 
            // cb_Symmetry
            // 
            this.cb_Symmetry.Checked = true;
            this.cb_Symmetry.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_Symmetry.Location = new System.Drawing.Point(99, 208);
            this.cb_Symmetry.Name = "cb_Symmetry";
            this.cb_Symmetry.Size = new System.Drawing.Size(104, 20);
            this.cb_Symmetry.TabIndex = 18;
            this.cb_Symmetry.Text = "Keep symmetry";
            this.cb_Symmetry.CheckedChanged += new System.EventHandler(this.cb_Symmetry_CheckedChanged);
            // 
            // btn_Close
            // 
            this.btn_Close.Location = new System.Drawing.Point(467, 236);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(104, 28);
            this.btn_Close.TabIndex = 16;
            this.btn_Close.Text = "Close";
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // tb_ZoomFactor
            // 
            this.tb_ZoomFactor.Location = new System.Drawing.Point(99, 12);
            this.tb_ZoomFactor.Name = "tb_ZoomFactor";
            this.tb_ZoomFactor.Size = new System.Drawing.Size(220, 42);
            this.tb_ZoomFactor.TabIndex = 15;
            this.tb_ZoomFactor.Scroll += new System.EventHandler(this.tb_ZoomFactor_Scroll);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cb_CloseOnMouseUp);
            this.groupBox1.Controls.Add(this.cb_DoubleBuffered);
            this.groupBox1.Controls.Add(this.cb_RememberLastPoint);
            this.groupBox1.Controls.Add(this.cb_ReturnToOrigin);
            this.groupBox1.Controls.Add(this.cb_ShowInTaskbar);
            this.groupBox1.Controls.Add(this.cb_HideMouseCursor);
            this.groupBox1.Controls.Add(this.cb_TopMostWindow);
            this.groupBox1.Location = new System.Drawing.Point(391, 16);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(180, 188);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = " Boolean Settings ";
            // 
            // cb_CloseOnMouseUp
            // 
            this.cb_CloseOnMouseUp.Location = new System.Drawing.Point(12, 24);
            this.cb_CloseOnMouseUp.Name = "cb_CloseOnMouseUp";
            this.cb_CloseOnMouseUp.Size = new System.Drawing.Size(148, 16);
            this.cb_CloseOnMouseUp.TabIndex = 1;
            this.cb_CloseOnMouseUp.Text = "Close On Mouse Up";
            this.cb_CloseOnMouseUp.CheckedChanged += new System.EventHandler(this.cb_CloseOnMouseUp_CheckedChanged);
            // 
            // cb_DoubleBuffered
            // 
            this.cb_DoubleBuffered.Location = new System.Drawing.Point(12, 44);
            this.cb_DoubleBuffered.Name = "cb_DoubleBuffered";
            this.cb_DoubleBuffered.Size = new System.Drawing.Size(148, 16);
            this.cb_DoubleBuffered.TabIndex = 1;
            this.cb_DoubleBuffered.Text = "Double Buffered";
            this.cb_DoubleBuffered.CheckedChanged += new System.EventHandler(this.cb_DoubleBuffered_CheckedChanged);
            // 
            // cb_RememberLastPoint
            // 
            this.cb_RememberLastPoint.Location = new System.Drawing.Point(12, 84);
            this.cb_RememberLastPoint.Name = "cb_RememberLastPoint";
            this.cb_RememberLastPoint.Size = new System.Drawing.Size(148, 16);
            this.cb_RememberLastPoint.TabIndex = 1;
            this.cb_RememberLastPoint.Text = "Remember Last Point";
            this.cb_RememberLastPoint.CheckedChanged += new System.EventHandler(this.cb_RememberLastPoint_CheckedChanged);
            // 
            // cb_ReturnToOrigin
            // 
            this.cb_ReturnToOrigin.Location = new System.Drawing.Point(12, 104);
            this.cb_ReturnToOrigin.Name = "cb_ReturnToOrigin";
            this.cb_ReturnToOrigin.Size = new System.Drawing.Size(148, 16);
            this.cb_ReturnToOrigin.TabIndex = 1;
            this.cb_ReturnToOrigin.Text = "Return To Origin";
            this.cb_ReturnToOrigin.CheckedChanged += new System.EventHandler(this.cb_ReturnToOrigin_CheckedChanged);
            // 
            // cb_ShowInTaskbar
            // 
            this.cb_ShowInTaskbar.Location = new System.Drawing.Point(12, 124);
            this.cb_ShowInTaskbar.Name = "cb_ShowInTaskbar";
            this.cb_ShowInTaskbar.Size = new System.Drawing.Size(148, 16);
            this.cb_ShowInTaskbar.TabIndex = 1;
            this.cb_ShowInTaskbar.Text = "Show In Taskbar";
            this.cb_ShowInTaskbar.CheckedChanged += new System.EventHandler(this.cb_ShowInTaskbar_CheckedChanged);
            // 
            // cb_HideMouseCursor
            // 
            this.cb_HideMouseCursor.Location = new System.Drawing.Point(12, 64);
            this.cb_HideMouseCursor.Name = "cb_HideMouseCursor";
            this.cb_HideMouseCursor.Size = new System.Drawing.Size(148, 16);
            this.cb_HideMouseCursor.TabIndex = 1;
            this.cb_HideMouseCursor.Text = "Hide Mouse Cursor";
            this.cb_HideMouseCursor.CheckedChanged += new System.EventHandler(this.cb_HideMouseCursor_CheckedChanged);
            // 
            // cb_TopMostWindow
            // 
            this.cb_TopMostWindow.Location = new System.Drawing.Point(12, 144);
            this.cb_TopMostWindow.Name = "cb_TopMostWindow";
            this.cb_TopMostWindow.Size = new System.Drawing.Size(148, 16);
            this.cb_TopMostWindow.TabIndex = 1;
            this.cb_TopMostWindow.Text = "Top Most Window";
            this.cb_TopMostWindow.CheckedChanged += new System.EventHandler(this.cb_TopMostWindow_CheckedChanged);
            // 
            // lbl_ZF
            // 
            this.lbl_ZF.Location = new System.Drawing.Point(7, 20);
            this.lbl_ZF.Name = "lbl_ZF";
            this.lbl_ZF.Size = new System.Drawing.Size(88, 16);
            this.lbl_ZF.TabIndex = 8;
            this.lbl_ZF.Text = "Zoom Factor";
            // 
            // lbl_SF
            // 
            this.lbl_SF.Location = new System.Drawing.Point(7, 64);
            this.lbl_SF.Name = "lbl_SF";
            this.lbl_SF.Size = new System.Drawing.Size(88, 16);
            this.lbl_SF.TabIndex = 9;
            this.lbl_SF.Text = "Speed Factor";
            // 
            // lbl_MW
            // 
            this.lbl_MW.Location = new System.Drawing.Point(7, 112);
            this.lbl_MW.Name = "lbl_MW";
            this.lbl_MW.Size = new System.Drawing.Size(88, 16);
            this.lbl_MW.TabIndex = 7;
            this.lbl_MW.Text = "Magnifier Width";
            // 
            // lbl_MH
            // 
            this.lbl_MH.Location = new System.Drawing.Point(7, 160);
            this.lbl_MH.Name = "lbl_MH";
            this.lbl_MH.Size = new System.Drawing.Size(88, 16);
            this.lbl_MH.TabIndex = 10;
            this.lbl_MH.Text = "Magnifier Height";
            // 
            // tb_SpeedFactor
            // 
            this.tb_SpeedFactor.Location = new System.Drawing.Point(99, 60);
            this.tb_SpeedFactor.Name = "tb_SpeedFactor";
            this.tb_SpeedFactor.Size = new System.Drawing.Size(220, 42);
            this.tb_SpeedFactor.TabIndex = 14;
            this.tb_SpeedFactor.Scroll += new System.EventHandler(this.tb_SpeedFactor_Scroll);
            // 
            // tb_Width
            // 
            this.tb_Width.Location = new System.Drawing.Point(99, 108);
            this.tb_Width.Name = "tb_Width";
            this.tb_Width.Size = new System.Drawing.Size(220, 42);
            this.tb_Width.TabIndex = 13;
            this.tb_Width.Scroll += new System.EventHandler(this.tb_Width_Scroll);
            // 
            // tb_Height
            // 
            this.tb_Height.Enabled = false;
            this.tb_Height.Location = new System.Drawing.Point(99, 156);
            this.tb_Height.Name = "tb_Height";
            this.tb_Height.Size = new System.Drawing.Size(220, 42);
            this.tb_Height.TabIndex = 12;
            this.tb_Height.Scroll += new System.EventHandler(this.tb_Height_Scroll);
            // 
            // lbl_SpeedFactor
            // 
            this.lbl_SpeedFactor.Location = new System.Drawing.Point(323, 64);
            this.lbl_SpeedFactor.Name = "lbl_SpeedFactor";
            this.lbl_SpeedFactor.Size = new System.Drawing.Size(60, 16);
            this.lbl_SpeedFactor.TabIndex = 22;
            this.lbl_SpeedFactor.Text = "?";
            // 
            // lbl_Width
            // 
            this.lbl_Width.Location = new System.Drawing.Point(323, 112);
            this.lbl_Width.Name = "lbl_Width";
            this.lbl_Width.Size = new System.Drawing.Size(60, 16);
            this.lbl_Width.TabIndex = 19;
            this.lbl_Width.Text = "?";
            // 
            // lbl_Height
            // 
            this.lbl_Height.Location = new System.Drawing.Point(323, 164);
            this.lbl_Height.Name = "lbl_Height";
            this.lbl_Height.Size = new System.Drawing.Size(60, 16);
            this.lbl_Height.TabIndex = 20;
            this.lbl_Height.Text = "?";
            // 
            // pb_About
            // 
            this.pb_About.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pb_About.Image = ((System.Drawing.Image)(resources.GetObject("pb_About.Image")));
            this.pb_About.Location = new System.Drawing.Point(267, 232);
            this.pb_About.Name = "pb_About";
            this.pb_About.Size = new System.Drawing.Size(35, 32);
            this.pb_About.TabIndex = 17;
            this.pb_About.TabStop = false;
            this.pb_About.MouseLeave += new System.EventHandler(this.pb_About_MouseLeave);
            this.pb_About.Click += new System.EventHandler(this.pb_About_Click);
            this.pb_About.MouseEnter += new System.EventHandler(this.pb_About_MouseEnter);
            // 
            // ConfigurationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(586, 275);
            this.Controls.Add(this.lbl_ZoomFactor);
            this.Controls.Add(this.cb_Symmetry);
            this.Controls.Add(this.pb_About);
            this.Controls.Add(this.btn_Close);
            this.Controls.Add(this.tb_ZoomFactor);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lbl_ZF);
            this.Controls.Add(this.lbl_SF);
            this.Controls.Add(this.lbl_MW);
            this.Controls.Add(this.lbl_MH);
            this.Controls.Add(this.tb_SpeedFactor);
            this.Controls.Add(this.tb_Width);
            this.Controls.Add(this.tb_Height);
            this.Controls.Add(this.lbl_SpeedFactor);
            this.Controls.Add(this.lbl_Width);
            this.Controls.Add(this.lbl_Height);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "ConfigurationForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Configuration";
            ((System.ComponentModel.ISupportInitialize)(this.tb_ZoomFactor)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tb_SpeedFactor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tb_Width)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tb_Height)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_About)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_ZoomFactor;
        private System.Windows.Forms.CheckBox cb_Symmetry;
        private System.Windows.Forms.PictureBox pb_About;
        private System.Windows.Forms.Button btn_Close;
        private System.Windows.Forms.TrackBar tb_ZoomFactor;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox cb_CloseOnMouseUp;
        private System.Windows.Forms.CheckBox cb_DoubleBuffered;
        private System.Windows.Forms.CheckBox cb_RememberLastPoint;
        private System.Windows.Forms.CheckBox cb_ReturnToOrigin;
        private System.Windows.Forms.CheckBox cb_ShowInTaskbar;
        private System.Windows.Forms.CheckBox cb_HideMouseCursor;
        private System.Windows.Forms.CheckBox cb_TopMostWindow;
        private System.Windows.Forms.Label lbl_ZF;
        private System.Windows.Forms.Label lbl_SF;
        private System.Windows.Forms.Label lbl_MW;
        private System.Windows.Forms.Label lbl_MH;
        private System.Windows.Forms.TrackBar tb_SpeedFactor;
        private System.Windows.Forms.TrackBar tb_Width;
        private System.Windows.Forms.TrackBar tb_Height;
        private System.Windows.Forms.Label lbl_SpeedFactor;
        private System.Windows.Forms.Label lbl_Width;
        private System.Windows.Forms.Label lbl_Height;
    }
}