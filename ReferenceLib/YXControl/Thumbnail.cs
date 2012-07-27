using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;


namespace YXControl
{
    public partial class Thumbnail : UserControl
    {
        int descriptionOpacity = 80;
        Color normalColor ;
        public Thumbnail()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            InitializeComponent();
            label1.MouseEnter += new EventHandler(panel1_MouseEnter);
            label1.MouseDown += new MouseEventHandler(panel1_MouseDown);
            label1.MouseLeave += new EventHandler(panel1_MouseLeave);
            label1.DoubleClick += new EventHandler(panel1_DoubleClick);
        }


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
            get { return panel5.Location; }
            set { panel5.Location = value; }
        }
        [Category("YXControl")]
        [Description("标题栏宽度")]
        public int TitleWidth
        {
            get { return panel5.Width; }
            set { panel5.Width = value; }
        }
        [Category("YXControl")]
        [Description("标题栏高度")]
        public int TitleHeight
        {
            get { return panel5.Height; }
            set { panel5.Height = value; }
        }

        [Category("YXControl")]
        [Description("标题栏size")]
        public Size TitleSize
        {
            get { return panel5.Size; }
            set { panel5.Size = value; }
        }

        [Category("YXControl")]
        [Description("标题文本对齐方式")]
        public ContentAlignment TitleAlignment
        {
            get { return label1.TextAlign; }
            set { label1.TextAlign = value; }
        }
        [Category("YXControl")]
        [Description("标题背景色")]
        public Color TitleBackColor
        {
            get { return label1.BackColor;}
            set { label1.BackColor = value; }
        }
        #endregion

        #region 图片
        [Category("YXControl")]
        [Description("显示图片")]
        public Image ImagePath
        {
            get { return panel1.BackgroundImage; }
            set { panel1.BackgroundImage = value; }
        }
        [Category("YXControl")]
        [Description("图片显示方式")]
        public ImageLayout ImageLayOut
        {
            get { return panel1.BackgroundImageLayout; }
            set { panel1.BackgroundImageLayout = value; }
        }
        [Category("YXControl")]
        [Description("显示图片宽度")]
        public int ImageWidth
        {
            get { return panel1.Width; }
            set { panel1.Width = value; }
        }
        [Category("YXControl")]
        [Description("显示图片高度")]
        public int ImageHeight
        {
            get { return panel1.Height; }
            set { panel1.Height = value;}
        }
        [Category("YXControl")]
        [Description("图片左上角坐标")]
        public Point ImageLocation
        {
            get { return panel1.Location; }
            set { panel1.Location = value; }
        }
        public Size ImageSize
        {
            get { return panel1.Size; }
            set { panel1.Size = value; }
        }
        #endregion

        #region 注释控件
        [Category("YXControl")]
        [Description("注释：对控件描述")]
        public Point DescriptionLocation
        {
            get ;//{ return label2.Location; }
            set ;//{ label2.Location = value; }
        }
        [Category("YXControl")]
        [Description("注释：对控件描述")]
        public string DescriptionText
        {
            get; //{ return label2.Text; }
            set; //{ label2.Text = value; }
        }
        [Category("YXControl")]
        [Description("注释控件的宽度")]
        public int DescriptionWidth
        {
            get; //{ return label2.Width; }
            set; //{ label2.Width = value; }
        }
        [Category("YXControl")]
        [Description("注释控件的高度")]
        public int DescriptionHeight
        {
            get; //{ return label2.Height; }
            set; //{ label2.Height = value; }
        }
        [Category("YXControl")]
        [Description("注释文本字体")]
        public Font DescriptionFont
        {
            get;// { return label2.Font; }
            set;// { label2.Font = value; }
        }
        [Category("YXControl")]
        [Description("注释控件的背景")]
        public Color DescriptionBackColor
        {
            get ;//{ return label2.BackColor; }
            set;// { label2.BackColor = Color.FromArgb(descriptionOpacity,value); }
        }
        [Category("YXControl")]
        [Description("注释控件的背景透明度")]
        public int DescriptionOpacity
        {
            get { return descriptionOpacity; }
            set
            {
                if (descriptionOpacity > 255)
                { descriptionOpacity = 255; }
                else if (descriptionOpacity < 0)
                { descriptionOpacity = 0; }
                else
                { descriptionOpacity = value; }
            }
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

        [Category("YXControl")]
        [Description("是否显示注释控件")]
        public bool IsShowRemark
        {
            get;
            set;
        }

        #endregion

        public Color TopBackColor
        {
            get { return panel1.BackColor; }
            set { panel1.BackColor = value; }
        }
       
        public Color BottomBackColor
        {
            get { return panel5.BackColor; }
            set { panel5.BackColor = value; }
        }

        [Category("YXControl")]
        [Description("鼠标悬浮时边框颜色")]
        public Color OverColor
        {
            get;
            set;
        }
        [Category("YXControl")]
        [Description("鼠标离开时边框颜色")]
        public Color NormalColor
        {
            get { return normalColor; }
            set { normalColor = value; }
        }

        #region 鼠标事件
        private void label1_MouseEnter(object sender, EventArgs e)
        {
            IsshowRemark();
            if (EnterText != null)
            { DescriptionText = EnterText; }
            BackColor = OverColor;
            this.OnMouseEnter(e);
        }

        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            IsshowRemark();
            if (DownText != null)
            { DescriptionText = DownText; }
            this.OnMouseDown(e);
        }

        private void label1_MouseLeave(object sender, EventArgs e)
        {
            //label2.Visible = false;
            if (LeaveText != null)
            { DescriptionText = LeaveText; }
            BackColor = NormalColor;
            this.OnMouseLeave(e);
        }

        private void panel1_Click(object sender, EventArgs e)
        {
            this.OnClick(e);
        }
        private void label1_Click(object sender, EventArgs e)
        {
            this.OnClick(e);
        }

        private void panel1_MouseEnter(object sender, EventArgs e)
        {
            BackColor = OverColor;
            this.OnMouseEnter(e);
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            this.OnMouseDown(e);
        }

        private void panel1_MouseLeave(object sender, EventArgs e)
        {
            BackColor = NormalColor;
            this.OnMouseLeave(e);
        }

        private void panel1_DoubleClick(object sender, EventArgs e)
        {
            this.OnDoubleClick(e);
        }
        #endregion

        private void Thumbnail_Load(object sender, EventArgs e)
        {
            normalColor = BackColor;
            this.Type(this,10,0.1);
        }

        #region about description
        private void IsshowRemark()
        {
            //if (IsShowRemark == true)
            //{
            //    label2.Visible = true;
            //    label2.Text = DescriptionText;
            //}
            //else
            //{ label2.Visible = false; }
        }

        public void ReadDescriptionText(string enterText, string downText, string leaveText)
        {
            EnterText = enterText;
            DownText = downText;
            LeaveText = leaveText;
        }
        #endregion

        
        private void Type(Control sender, int p_1, double p_2)
        {
            GraphicsPath oPath = new GraphicsPath();
            oPath.AddClosedCurve(new Point[] { new Point(0, p_1), new Point(p_1, 0), new Point(sender.Width - p_1, 0), new Point(sender.Width, p_1), new Point(sender.Width, sender.Height - p_1), new Point(sender.Width - p_1, sender.Height), new Point(p_1, sender.Height), new Point(0, sender.Height - p_1) }, (float)p_2);
            sender.Region = new Region(oPath);
        }

    }
}
