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
    public partial class RuningTask : UserControl
    {
        bool isMouseClick;

        public RuningTask()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            InitializeComponent();
            pictureBox1.Click += new EventHandler(panel1_Click);
            label1.Click += new EventHandler(panel1_Click);
            pictureBox1.MouseEnter += new EventHandler(panel1_MouseEnter);
            pictureBox1.MouseLeave += new EventHandler(panel1_MouseLeave);
            pictureBox1.MouseDown += new MouseEventHandler(panel1_MouseDown);
            label1.MouseEnter += new EventHandler(panel1_MouseEnter);
            label1.MouseLeave += new EventHandler(panel1_MouseLeave);
            label1.MouseDown += new MouseEventHandler(panel1_MouseDown);
            //label1.Width = this.Width - pictureBox1.Width;
            panel1.MouseUp+=new MouseEventHandler(panel1_MouseUp);
            pictureBox1.MouseUp += new MouseEventHandler(panel1_MouseUp);
            label1.MouseUp += new MouseEventHandler(panel1_MouseUp);
        }

        [Category("YXControl")]
        [Description("标签图标")]
        public Image TaskIcon
        {
            get { return pictureBox1.BackgroundImage; }
            set { pictureBox1.BackgroundImage = value; }
        }

        [Category("YXControl")]
        [Description("标签文本")]
        public string Context
        {
            get { return label1.Text; }
            set { label1.Text = value; }
        }

        [Category("YXControl")]
        [Description("标签文本样式")]
        public Font ContextFont
        {
            get { return label1.Font; }
            set { label1.Font = value; }
        }

        [Category("YXControl")]
        [Description("获得控件的关闭按钮")]
        public Control CloseButton
        {
            get { return pictureBox2; }
        }

        [Category("YXControl")]
        [Description("获取控件当前是否处于激活状态")]
        public bool IsActived
        {
            get { return isMouseClick; }
        }

        private void panel1_Click(object sender, EventArgs e)
        {
            this.OnClick(e);
        }

        private void panel1_MouseEnter(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
            if (isMouseClick == true)
            {
                try
                { BackgroundImage = new Bitmap(System.Environment.CurrentDirectory + "\\Images\\default\\task\\activedOver.jpg"); }
                catch { };
            }
            else
            {
                try
                { BackgroundImage = new Bitmap(System.Environment.CurrentDirectory + "\\Images\\default\\task\\deactivedOver.jpg"); }
                catch { };
            }
            this.Invalidate();
            this.label1.Invalidate();
            this.pictureBox1.Invalidate();
            toolTip1.SetToolTip(panel1, Context);
            this.OnMouseEnter(e);
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (isMouseClick == false)
            { isMouseClick = true; }
            else
            { isMouseClick = false; }
            try
            { BackgroundImage = new Bitmap(System.Environment.CurrentDirectory + "\\Images\\default\\task\\activedIcon.jpg"); }
            catch { };
            this.Invalidate();
            this.label1.Invalidate();
            this.pictureBox1.Invalidate();
            this.OnMouseDown(e);
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            if (isMouseClick == true)
            {
                try
                { BackgroundImage = new Bitmap(System.Environment.CurrentDirectory + "\\Images\\default\\task\\activedOver.jpg"); }
                catch { };
            }
            else
            {
                try
                { BackgroundImage = new Bitmap(System.Environment.CurrentDirectory + "\\Images\\default\\task\\deactivedOver.jpg"); }
                catch { };
            }
            this.Invalidate();
            this.label1.Invalidate();
            this.pictureBox1.Invalidate();
            this.OnMouseUp(e);
        }

        private void panel1_MouseLeave(object sender, EventArgs e)
        {
            if (isMouseClick == true)
            {
                try
                { BackgroundImage = new Bitmap(System.Environment.CurrentDirectory + "\\Images\\default\\task\\activedIcon.jpg"); }
                catch { };
            }
            else
            {
                try
                { BackgroundImage = new Bitmap(System.Environment.CurrentDirectory + "\\Images\\default\\task\\deactivedIcon.jpg"); }
                catch { };
            }
            this.Invalidate();
            this.label1.Invalidate();
            this.pictureBox1.Invalidate();
            this.OnMouseLeave(e);
        }

        private void RuningTask_Load(object sender, EventArgs e)
        {

            if (isMouseClick == true)
            {
                try
                { BackgroundImage = new Bitmap(System.Environment.CurrentDirectory + "\\Images\\default\\task\\activedIcon.jpg"); }
                catch { };
            }
            else
            {
                try
                { BackgroundImage = new Bitmap(System.Environment.CurrentDirectory + "\\Images\\default\\task\\deactivedIcon.jpg"); }
                catch { };
            }
            try
            { pictureBox2.BackgroundImage = new Bitmap(System.Environment.CurrentDirectory + "\\Images\\default\\task\\close.png"); }
            catch { };
            if (pictureBox2.BackgroundImage == null && pictureBox2.Image == null)
            { pictureBox2.Visible = false; }
        }

        public void ActivedTask()
        { 
            isMouseClick = true;
            try
            { BackgroundImage = new Bitmap(System.Environment.CurrentDirectory + "\\Images\\default\\task\\activedIcon.jpg"); }
            catch { };
        }

        public void DeactivedTask()
        { 
            isMouseClick = false;
            try
            { BackgroundImage = new Bitmap(System.Environment.CurrentDirectory + "\\Images\\default\\task\\deactivedIcon.jpg"); }
            catch { };
        }
    }
}
