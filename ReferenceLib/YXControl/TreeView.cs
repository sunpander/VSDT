using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace YXControl
{
    public partial class TreeView : UserControl
    {
        //int descriptionOpacity = 80;
        Color normalColor;
        Color normalForeColor;
        public TreeView()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            InitializeComponent();
            label2.Top = label3.Top = label1.Top;
            label2.Height = label3.Height = label1.Height ;
            label2.Left = label1.Left + label1.Width;
            label3.Left = label2.Left + label2.Width;
            label1.MouseEnter += new EventHandler(pictureBox1_MouseEnter);
            label2.MouseEnter += new EventHandler(pictureBox1_MouseEnter);
            label3.MouseEnter += new EventHandler(pictureBox1_MouseEnter);
            label1.MouseDown += new MouseEventHandler(pictureBox1_MouseDown);
            label2.MouseDown += new MouseEventHandler(pictureBox1_MouseDown);
            label3.MouseDown += new MouseEventHandler(pictureBox1_MouseDown);
            label1.MouseLeave += new EventHandler(pictureBox1_MouseLeave);
            label2.MouseLeave += new EventHandler(pictureBox1_MouseLeave);
            label3.MouseLeave += new EventHandler(pictureBox1_MouseLeave);
            pictureBox1.Click += new EventHandler(panel1_Click);
            label1.Click += new EventHandler(panel1_Click);
            label2.Click += new EventHandler(panel1_Click);
            label3.Click += new EventHandler(panel1_Click);
            panel1.DoubleClick += new EventHandler(panel1_DoubleClick);
            pictureBox1.DoubleClick += new EventHandler(panel1_DoubleClick);
            label1.DoubleClick += new EventHandler(panel1_DoubleClick);
            label2.DoubleClick += new EventHandler(panel1_DoubleClick);
            label3.DoubleClick += new EventHandler(panel1_DoubleClick);
        }

        #region 图片
        [Category("YXControl")]
        [Description("图片路径")]
        public Image ImagePath
        {
            get { return pictureBox1.BackgroundImage; }
            set { pictureBox1.BackgroundImage = value; }
        }

        [Category("YXControl")]
        [Description("显示图片宽度")]
        public int ImageWidth
        {
            get { return pictureBox1.Width; }
            set { pictureBox1.Width = value; }
        }

        [Category("YXControl")]
        [Description("显示图片高度")]
        public int ImageHeight
        {
            get { return pictureBox1.Height; }
            set { pictureBox1.Height = value; }
        }

        [Category("YXControl")]
        [Description("图片左上角坐标")]
        public Point ImageLocation
        {
            get { return pictureBox1.Location; }
            set { pictureBox1.Location = value; }
        }

        public Size ImageSize
        {
            get { return pictureBox1.Size; }
            set { pictureBox1.Size = value; }
        }

        #endregion

        #region 标题
        [Category("YXControl")]
        [Description("标题")]
        public string Title
        {
            get { return label1.Text; }
            set { label1.Text = value; }
        }

        [Category("YXControl")]
        [Description("字体设置")]
        public Font TitleFontStyles
        {
            get { return label1.Font; }
            set { label1.Font = value; }
        }

        [Category("YXControl")]
        [Description("标题左上角位置")]
        public Point TitleLocation
        {
            get { return label1.Location; }
            set { label1.Location = value; }
        }
        
        [Category("YXControl")]
        [Description("标题文本对齐方式")]
        public ContentAlignment TitleAlignment
        {
            get { return label1.TextAlign; }
            set { label1.TextAlign = value; }
        }

        public Size TitleSize
        {
            get { return label1.Size; }
            set { label1.Size = value; }
        }

        #endregion

        #region 类型（大小）
        [Category("YXControl")]
        [Description("类型大小")]
        public string TypeText
        {
            get { return label2.Text; }
            set { label2.Text = value; }
        }

        [Category("YXControl")]
        [Description("该栏字体样式")]
        public Font TypeFontStyle
        {
            get { return label2.Font; }
            set { label2.Font = value; }
        }

        [Category("YXControl")]
        [Description("该栏左上角位置")]
        public Point TypeLocation
        {
            get { return label2.Location; }
            set { label2.Location = value; }
        }

        [Category("YXControl")]
        [Description("该栏文本对齐方式")]
        public ContentAlignment TypeAlignment
        {
            get { return label2.TextAlign; }
            set { label2.TextAlign = value; }
        }

        public Size TypeSize
        {
            get { return label2.Size; }
            set { label2.Size = value; }
        }

        #endregion

        #region 内容简介(描述)
        [Category("YXControl")]
        [Description("内容简介")]
        public string ContextIntroduce
        {
            get { return label3.Text; }
            set { label3.Text = value; }
        }

        [Category("YXControl")]
        [Description("内容栏字体样式")]
        public Font ContextFontStyle
        {
            get { return label3.Font; }
            set { label3.Font = value; }
        }

        [Category("YXControl")]
        [Description("内容栏左上角位置")]
        public Point ContextLocation
        {
            get { return label3.Location; }
            set { label3.Location = value; }
        }

        [Category("YXControl")]
        [Description("内容栏宽度")]
        public int ContextWidth
        {
            get { return label3.Width; }
            set { label3.Width = value; }
        }

        [Category("YXControl")]
        [Description("内容栏高度")]
        public int ContextHeight
        {
            get { return label3.Height; }
            set { label3.Height = value; }
        }

        [Category("YXControl")]
        [Description("内容栏文本对齐方式")]
        public ContentAlignment ContextAlignment
        {
            get { return label3.TextAlign; }
            set { label3.TextAlign = value; }
        }

        public Size ContextSize
        {
            get { return label3.Size; }
            set { label3.Size = value; }
        }

        #endregion

        #region 注释图标
        [Category("YXControl")]
        [Description("注释图标：对控件的描述")]
        public Point DescriptionLocation
        {
            get; //{ return label4.Location; }
            set; //{ label4.Location = value; }
        }
        [Category("YXControl")]
        [Description("注释图标：对控件的描述")]
        public string DescriptionText
        {
            get; // { return label4.Text; }
            set; // { label4.Text = value; }
        }
        [Category("YXControl")]
        [Description("注释控件的宽度")]
        public int DescriptionWidth
        {
            get; // { return label4.Width; }
            set; //{ label4.Width = value; }
        }
        [Category("YXControl")]
        [Description("注释控件的高度")]
        public int DescriptionHeight
        {
            get; // { return label4.Height; }
            set; // { label4.Height = value; }
        }
        [Category("YXControl")]
        [Description("注释的文本的字体样式")]
        public Font DescriptionFont
        {
            get; // { return label4.Font; }
            set; // { label4.Font = value; }
        }
        [Category("YXControl")]
        [Description("注释控件的背景色")]
        public Color DescriptionBackColor
        {
            get; // { return label4.BackColor; }
            set; // { label4.BackColor = Color.FromArgb(descriptionOpacity, value); }
        }
        [Category("YXControl")]
        [Description("注释控件的背景透明度")]
        public int DescriptionOpacity
        {
            get; // { return descriptionOpacity; }
            set; 
            //{
            //    if (descriptionOpacity > 255)
            //    { descriptionOpacity = 255; }
            //    else if (descriptionOpacity < 0)
            //    { descriptionOpacity = 0; }
            //    else
            //    { descriptionOpacity = value; }
            //}
        }
       
        [Category("YXControl")]
        [Description("是否显示注释控件")]
        public bool IsShowRemark
        {
            get;
            set;
        }
        [Category("YXControl")]
        [Description("鼠标进入时显示的注释文本")]
        public string EnterText
        {
            get;
            set;
        }
        [Category("YXControl")]
        [Description("鼠标按下时显示的注释文本")]
        public string DownText
        {
            get;
            set;
        }
        [Category("YXControl")]
        [Description("鼠标离开时显示的注释文本")]
        public string LeaveText
        {
            get;
            set;
        }
        #endregion

        [Category("YXControl")]
        [Description("鼠标进入时控件背景色")]
        public Color MoveBackgroundColor
        {
            get;
            set;
        }
        
        [Category("YXControl")]
        [Description("鼠标离开时控件背景色")]
        public Color NormalColor
        {
            get { return normalColor; }
            set { normalColor = value; }
        }

        [Category("YXControl")]
        [Description("鼠标进入时控件前景色")]
        public Color MoveForeColor
        {
            get;
            set;
        }

        [Category("YXControl")]
        [Description("鼠标离开时控件前景色")]
        public Color NormalForeColor
        {
            get { return normalForeColor; }
            set { normalForeColor = value; }
        }


        private void TreeView_Load(object sender, EventArgs e)
        {
            //label4.Visible = false;
            normalColor = BackColor;
        }

        private void panel1_Click(object sender, EventArgs e)
        {
            this.OnClick(e);
        }
        
        #region 鼠标移动事件
        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            //IsshowRemark();
            //if (EnterText != null)
            //{ DescriptionText = EnterText; }
            panel1_MouseEnter(null,null);
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            //IsshowRemark();
            //if (DownText != null)
            //{ DescriptionText = DownText; }
            panel1_MouseDown(null,null);
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            panel1_MouseLeave(null,null);
        }

        private void panel1_MouseEnter(object sender, EventArgs e)
        {
            BackColor = MoveBackgroundColor;
            foreach (Control c in this.Controls)
            { c.ForeColor = MoveForeColor; }
            this.OnMouseEnter(e);
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            this.OnMouseDown(e);
        }

        private void panel1_MouseLeave(object sender, EventArgs e)
        {
            BackColor = NormalColor;
            foreach (Control c in this.Controls)
            {  c.ForeColor = NormalForeColor; }
            this.OnMouseLeave(e);
        }

        private void panel1_DoubleClick(object sender,EventArgs e)
        { this.OnDoubleClick(e); }

        #endregion
        
        private void IsshowRemark()
        {
            if (IsShowRemark == true)
            {
                //label4.Visible = true;
                //label4.Text = DescriptionText;
            }
            else
            { //label4.Visible = false;
            }
        }
       
    }
}
