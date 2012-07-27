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
    public partial class CalendarCell : UserControl
    {
#region 自定义变量
        private Color m_BGColor;
        private Color m_DateColor;
        private Color m_LunarColor;
        private Color m_HoverBGColor;
        private Color m_HoverDateColor;
        private Color m_HoverLunarColor;
        private Color m_ClickedBGColor;
        private Color m_ClickedDateColor;
        private Color m_ClickedLunarColor;
#endregion
#region 自定义属性

        public Color BGColor
        {
            get { return m_BGColor; }
            set { m_BGColor = value; this.BackColor = value; }
        }

        public Color DateColor
        {
            get { return m_DateColor; }
            set { m_DateColor = value; label1.ForeColor = value; }
        }

        public Color LunarColor
        {
            get { return m_LunarColor; }
            set { m_LunarColor = value; label2.ForeColor = value; ; }
        }

        public Color HoverBGColor
        {
            get { return m_HoverBGColor; }
            set { m_HoverBGColor = value; }
        }

        public Color HoverDateColor
        {
            get { return m_HoverDateColor; }
            set { m_HoverDateColor = value; }
        }

        public Color HoverLunarColor
        {
            get { return m_HoverLunarColor; }
            set { m_HoverLunarColor = value; }
        }

        public Color ClickedBGColor
        {
            get { return m_ClickedBGColor; }
            set { m_ClickedBGColor = value; }
        }

        public Color ClickedDateColor
        {
            get { return m_ClickedDateColor; }
            set { m_ClickedDateColor = value; }
        }

        public Color ClickedLunarColor
        {
            get { return m_ClickedLunarColor; }
            set { m_ClickedLunarColor = value; }
        }
#endregion
        public bool Clicked = false;

        public CalendarCell()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            InitializeComponent();
            HoverBGColor = Color.PeachPuff;
            HoverDateColor = Color.Gray;
            HoverLunarColor = Color.Gray;
            ClickedBGColor = Color.DarkRed;
            ClickedDateColor = Color.DarkGray;
            ClickedLunarColor = Color.DarkGray;
        }

        public void ResetColor()
        {
            if (Clicked)
            {
                this.BackColor = ClickedBGColor;
                label1.ForeColor = ClickedDateColor;
                label2.ForeColor = ClickedLunarColor;
            }
            else
            {
                this.BackColor = BGColor;
                label1.ForeColor = DateColor;
                label2.ForeColor = LunarColor;
            }

        }

        private void label1_MouseEnter(object sender, EventArgs e)
        {
            this.OnMouseEnter(e);
        }

        private void label1_MouseLeave(object sender, EventArgs e)
        {
            this.OnMouseLeave(e);
        }

        private void CalendarCell_MouseEnter(object sender, EventArgs e)
        {
            if (Clicked)
            {
                return;
            }
            this.BackColor=HoverBGColor;
            label1.ForeColor=HoverDateColor;
            label2.ForeColor=HoverLunarColor;

        }

        private void CalendarCell_MouseLeave(object sender, EventArgs e)
        {
            if (Clicked)
            {
                return;
            }
            ResetColor();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.OnClick(e);
        }

        private void CalendarCell_Click(object sender, EventArgs e)
        {
            this.Clicked = true;
            this.BackColor = ClickedBGColor;
            label1.ForeColor = ClickedDateColor;
            label2.ForeColor = ClickedLunarColor;
        }
    }
}
