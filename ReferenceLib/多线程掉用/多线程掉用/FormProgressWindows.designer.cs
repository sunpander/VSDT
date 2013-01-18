using System.Windows.Forms;
namespace 多线程掉用
{
    partial class FormProgressWindows
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormProgressWindows));
            this.efGroupBox1 = new System.Windows.Forms.GroupBox();
            this.lblCount = new System.Windows.Forms.Label();
            this.lblPercent = new System.Windows.Forms.Label();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.lblWork = new System.Windows.Forms.Label();
            this.progressBarControl1 = new System.Windows.Forms.ProgressBar();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnPause = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.chkIgnoreError = new System.Windows.Forms.CheckBox();
            this.efGroupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // efGroupBox1
            // 
            resources.ApplyResources(this.efGroupBox1, "efGroupBox1");
            this.efGroupBox1.Controls.Add(this.lblCount);
            this.efGroupBox1.Controls.Add(this.lblPercent);
            this.efGroupBox1.Controls.Add(this.txtLog);
            this.efGroupBox1.Controls.Add(this.lblWork);
            this.efGroupBox1.Controls.Add(this.progressBarControl1);
            this.efGroupBox1.Name = "efGroupBox1";
            this.efGroupBox1.TabStop = false;
            // 
            // lblCount
            // 
            resources.ApplyResources(this.lblCount, "lblCount");
            this.lblCount.Name = "lblCount";
            // 
            // lblPercent
            // 
            resources.ApplyResources(this.lblPercent, "lblPercent");
            this.lblPercent.Name = "lblPercent";
            // 
            // txtLog
            // 
            resources.ApplyResources(this.txtLog, "txtLog");
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            // 
            // lblWork
            // 
            resources.ApplyResources(this.lblWork, "lblWork");
            this.lblWork.Name = "lblWork";
            // 
            // progressBarControl1
            // 
            resources.ApplyResources(this.progressBarControl1, "progressBarControl1");
            this.progressBarControl1.Name = "progressBarControl1";
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnPause
            // 
            resources.ApplyResources(this.btnPause, "btnPause");
            this.btnPause.Name = "btnPause";
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            // 
            // chkIgnoreError
            // 
            resources.ApplyResources(this.chkIgnoreError, "chkIgnoreError");
            this.chkIgnoreError.BackColor = System.Drawing.Color.Transparent;
            this.chkIgnoreError.Name = "chkIgnoreError";
            this.chkIgnoreError.UseVisualStyleBackColor = true;
            this.chkIgnoreError.CheckedChanged += new System.EventHandler(this.chkIgnoreError_CheckedChanged);
            // 
            // FormProgressWindows
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chkIgnoreError);
            this.Controls.Add(this.btnPause);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.efGroupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormProgressWindows";
            this.Load += new System.EventHandler(this.FormEPEXProgressWindows_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormEPEXProgressWindows_FormClosing);
            this.efGroupBox1.ResumeLayout(false);
            this.efGroupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private  GroupBox efGroupBox1;
        private  Button btnCancel;
        private  Button btnPause;
      
        
        private  TextBox txtLog;
        private  Label lblWork;
        private ProgressBar progressBarControl1;
        private  Label lblCount;
        private  Label lblPercent;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private  CheckBox chkIgnoreError;
    }
}