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
    public partial class TaskTip :UserControl
    {
        string type = null;
        Color backColor = Color.Red;
        int alpha = 20;

        public TaskTip()
        {
            InitializeComponent();
        }
        
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (type == "balloon")
            {
                Point[] p = new Point[3] { new Point(this.Width / 3, (this.Height / 4) * 3), new Point(this.Width / 4, this.Height), new Point(this.Width / 2, (this.Height / 4) * 3) };
                e.Graphics.DrawEllipse(new Pen(Color.FromArgb(Alpha, BackGroundColor)), new Rectangle(0, 0, this.Width, (this.Height / 4) * 3));
                e.Graphics.FillEllipse(new SolidBrush(Color.FromArgb(Alpha, BackGroundColor)), new Rectangle(0, 0, this.Width, (this.Height / 4) * 3));
                e.Graphics.FillClosedCurve(new SolidBrush(Color.FromArgb(Alpha, BackGroundColor)), p);
            }
            else if(type == "bleb")
            {
                e.Graphics.DrawEllipse(new Pen(Color.FromArgb(Alpha, BackGroundColor)), new Rectangle(0, 0, this.Width, (this.Height / 4) * 3));
                e.Graphics.FillEllipse(new SolidBrush(Color.FromArgb(Alpha, BackGroundColor)), new Rectangle(0, 0, this.Width, (this.Height / 4) * 3));
                e.Graphics.FillEllipse(new SolidBrush(Color.FromArgb(Alpha, BackGroundColor)), new Rectangle(this.Width / 6, (this.Height / 4) * 3, this.Width / 3, this.Height / 4));
            }
            else if (type == "rectangle")
            {
                int radius = 8;
                GraphicsPath gp = new GraphicsPath();
                gp.AddLine(radius, 0, this.Width - (radius * 2), 0); //追加线段
                gp.AddArc(this.Width - (radius * 2), 0, radius * 2, radius * 2, 270, 90);  //追加椭圆弧
                gp.AddLine(this.Width, radius, this.Width, (this.Height/4)*3 - (radius * 2));
                gp.AddArc(this.Width - (radius * 2), (this.Height/4) * 3 - (radius * 2), radius * 2, radius * 2, 0, 90);
                gp.AddLine(this.Width - (radius * 2), (this.Height/4) * 3, radius, (this.Height/4) * 3);
                gp.AddArc(0, (this.Height/4) * 3 - (radius * 2), radius * 2, radius * 2, 90, 90);
                gp.AddLine(0, (this.Height/4) * 3 - (radius * 2), 0, radius);
                gp.AddArc(0, 0, radius * 2, radius * 2, 180, 90);
                gp.CloseFigure();
                e.Graphics.FillPath(new SolidBrush(Color.FromArgb(Alpha, BackGroundColor)), gp);
                gp.Dispose();
               // Point[] p = new Point[3] { new Point(this.Width / 3, (this.Height / 4) * 3), new Point(this.Width / 4, this.Height), new Point(this.Width / 2, (this.Height / 4) * 3) };
                Point[] p = new Point[3] { new Point(20, (this.Height / 4) * 3), new Point(13, this.Height), new Point(32, (this.Height / 4) * 3) };
                e.Graphics.FillClosedCurve(new SolidBrush(Color.FromArgb(Alpha, BackGroundColor)), p);
            }
        }

        public int Alpha
        {
            get { return alpha; }
            set { alpha = value; }
        }

        public Color BackGroundColor
        {
            get { return backColor; }
            set { backColor = value; }
        }

        public void TipShow(Control parentControl,string tipText,TipType tiptype)
        {
            type = tiptype.ToString();
            label1.Text = tipText;
            parentControl.Controls.Add(this);
            this.BringToFront();
        }

    }
    public enum TipType
    {
        balloon,
        bleb,
        rectangle,
    }
}
