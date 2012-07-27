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
    public partial class CommonButton : UserControl
    {
        bool isSave = false;

        [Category("Qlfui")]
        [Description("鼠标离开按钮时背景图片")]
        public Image NormalImage
        {
            get;
            set;
        }

        [Category("Qlfui")]
        [Description("鼠标移入按钮上方时背景图片")]
        public Image MoveImage
        {
            get;
            set;
        }

        [Category("Qlfui")]
        [Description("按钮被选中时显示的背景图片")]
        public Image ClickImage
        {
            get;
            set;
        }

        [Category("Qlfui")]
        [Description("鼠标点击按钮后背景图片是否改变")]
        public bool IsChangeClickImage
        {
            get { return isSave; }
            set { isSave = value; }
        }

        public CommonButton()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            InitializeComponent();
        }

        private void panel1_Click(object sender, EventArgs e)
        {
            this.OnClick(e);
            if (ClickImage == null)
            { return; }
            BackgroundImage = ClickImage;
        }

        private void panel1_MouseEnter(object sender, EventArgs e)
        {
            this.OnMouseEnter(e);
            if (IsChangeClickImage == false)
            {
                if (BackgroundImage == ClickImage)
                { return; }
                else
                {
                    if (MoveImage == null)
                    { return; }
                    BackgroundImage = MoveImage;
                }
            }
            else
            {
                if (MoveImage == null)
                { return; }
                BackgroundImage = MoveImage;
            }
        }

        private void panel1_MouseLeave(object sender, EventArgs e)
        {
            this.OnMouseLeave(e);
            if (IsChangeClickImage == false)
            {
                if (ClickImage != null)
                { BackgroundImage = ClickImage; }
                else
                {
                    if (NormalImage == null)
                    { return; }
                    BackgroundImage = NormalImage;
                }
            }
            else
            {
                if (NormalImage == null)
                { return; }
                BackgroundImage = NormalImage;
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            this.OnMouseDown(e);
            if (ClickImage == null)
            { return; }
            BackgroundImage = ClickImage;
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            this.OnMouseUp(e);
            if (IsChangeClickImage == false)
            { return; }
            else
            { BackgroundImage = NormalImage; }
        }

    }
}
