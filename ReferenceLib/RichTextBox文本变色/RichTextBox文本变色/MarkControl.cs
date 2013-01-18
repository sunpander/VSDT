using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
namespace RichTextBox文本变色
{
    public partial class MarkControl : UserControl
    {
        public MarkControl()
        {
            InitializeComponent();
        }
        #region 组件设计器生成的代码
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



        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Location = new System.Drawing.Point(66, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(20, 41);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            // 
            // UserControl1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.Controls.Add(this.pictureBox1);
            this.Name = "UserControl1";
            this.Size = new System.Drawing.Size(447, 47);
            this.Load += new System.EventHandler(this.UserControl1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Drawing.Pen _myPen;
        private System.Drawing.Pen _myBluePen;
        private Font _myFont;
        private SolidBrush _myBrush;
        private SolidBrush _myBlueBrush;

        public static int CharWidth = 6;
        public Color PanelBackColor
        {
            set
            {
                this.BackColor = value;
            }
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            if (this.BackColor == _myPen.Color)
            {
                _myPen.Color = this.ParentForm.ForeColor;
                _myBrush = new SolidBrush(Color.Red);
            }
            e.Graphics.Clear(this.BackColor);
            for (int i = 1; i < this.Width; i++)
            {
                if (i % 10 == 0)
                {
                    e.Graphics.DrawString(i.ToString(), _myFont, _myBrush, i * CharWidth - 6, 1);

                    e.Graphics.DrawLine(_myPen, i * CharWidth, 12, i * CharWidth, 25);
                }
                else
                    e.Graphics.DrawLine(_myPen, i * CharWidth, 18, i * CharWidth, 25);
            }
            base.OnPaint(e);
        }

        private void UserControl1_Load(object sender, EventArgs e)
        {
            _myFont = new Font("Lucida Console", 8);
            _myBrush = new SolidBrush(Color.Black);
            _myBlueBrush = new SolidBrush(Color.Blue);

            _myPen = new System.Drawing.Pen(Color.Black);
            _myPen.ResetTransform();
            _myPen.Width = 1;
            _myPen.StartCap = LineCap.Round;
            _myPen.EndCap = LineCap.Round;
            _myPen.DashStyle = DashStyle.Solid;



            _myBluePen = new System.Drawing.Pen(Color.Blue);
            _myBluePen.ResetTransform();
            _myBluePen.Width = 1;
            _myBluePen.StartCap = LineCap.ArrowAnchor;
            _myBluePen.EndCap = LineCap.Round;
            _myBluePen.DashStyle = DashStyle.Solid;

        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (pictureBox1.Tag != null)
            {
                e.Graphics.DrawString(pictureBox1.Tag.ToString(), _myFont, _myBlueBrush, 1, 1);
                e.Graphics.DrawLine(_myBluePen, 6, 8, 6, 45);
            }
        }

        public int PointX
        {
            set
            {
                if (value <= 0)
                    pictureBox1.Visible = false;
                else
                    pictureBox1.Visible = true;
                pictureBox1.Tag = value;
                pictureBox1.Location = new Point((value - 1) * CharWidth, 2);
                pictureBox1.Refresh();
            }
        }
    }
}
