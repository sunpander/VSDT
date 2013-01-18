using System.Windows.Forms;
using System.Data;
using System;
namespace RichTextBox文本变色
{
    public partial class MyPanel :  UserControl
    {
        public MyPanel()
        {
            InitializeComponent();
        }

 
        private ImageList imageList1;

        public string LabelInfo
        {
            get { return this.labelInfo.Text; }
            set { this.labelInfo.Text = value; }
        }

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MyPanel));
            this.labelInfo = new System.Windows.Forms.Label();
            this.btnDel = new  Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.myRichTextBox1 = new RichTextBox文本变色.MyRichTextBox();
             this.SuspendLayout();
            // 
            // labelInfo
            // 
            this.labelInfo.AutoSize = true;
            this.labelInfo.BackColor = System.Drawing.Color.Transparent;
            this.labelInfo.Location = new System.Drawing.Point(3, 19);
            this.labelInfo.Name = "labelInfo";
            this.labelInfo.Size = new System.Drawing.Size(96, 14);
            this.labelInfo.TabIndex = 1;
            this.labelInfo.Text = "日志ID:0909099";
            // 
            // btnDel
            // 
            this.btnDel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
         
            this.btnDel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDel.BackgroundImage")));
            this.btnDel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            
            this.btnDel.ImageIndex = 1;
            this.btnDel.ImageList = this.imageList1;
             this.btnDel.Location = new System.Drawing.Point(556, 3);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(41, 43);
        
            this.btnDel.TabIndex = 3;
          
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "delete.png");
            this.imageList1.Images.SetKeyName(1, "delete2.png");
            // 
            // myRichTextBox1
            // 
            this.myRichTextBox1.Font = new System.Drawing.Font("新宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.myRichTextBox1.Location = new System.Drawing.Point(100, 0);
            this.myRichTextBox1.Name = "myRichTextBox1";
            this.myRichTextBox1.OtherRichTextBox = null;
            this.myRichTextBox1.Size = new System.Drawing.Size(430, 43);
            this.myRichTextBox1.TabIndex = 0;
            this.myRichTextBox1.Text = "";
            // 
            // MyPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnDel);
            this.Controls.Add(this.labelInfo);
            this.Controls.Add(this.myRichTextBox1);
            this.Name = "MyPanel";
            this.Size = new System.Drawing.Size(600, 49);
             this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public MyRichTextBox myRichTextBox1;
        private System.Windows.Forms.Label labelInfo;
        public  Button btnDel;

        //public event EventHandler DoDeleteEvent;
        //private void btnDel_Click(object sender, System.EventArgs e)
        //{
            
        //}
    }
}
