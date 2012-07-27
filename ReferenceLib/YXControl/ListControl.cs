using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;



namespace YXControl
{
    public partial class ListControl : UserControl
    {
        Control currentcontrol = null;
        ArrayList arr = new ArrayList();
        Dictionary<string, ArrayList> dic = new Dictionary<string, ArrayList>();
        public delegate void UserHandler(object sender, System.EventArgs e);
        public event UserHandler ParentClick;

        public string Title
        {
            get { return label1.Text; }
            set { label1.Text = value; }
        }

        public Font TitleFont
        {
            get { return label1.Font; }
            set { label1.Font = value; }
        }

        public ContentAlignment TitleAlign
        {
            get { return label1.TextAlign; }
            set { label1.TextAlign = value; }
        }

        public Color TitleForeColor
        {
            get { return label1.ForeColor; }
            set { label1.ForeColor = value; }
        }

        public Control CurrentControl
        {
            get { return currentcontrol; }
        }

        public ListControl(ArrayList a,Dictionary<string,ArrayList> d)
        {
           // SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            InitializeComponent();
            arr = a;
            dic = d;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            //DrawRoundRect(e.Graphics, this.Left, this.Top, this.Width, this.Height, 12, ColorTranslator.FromHtml("#EDEDED"));
        }

        private void DrawRoundRect(Graphics g, float x, float y, float width, float height, float radius, Color color)
        {
            System.Drawing.Drawing2D.GraphicsPath gp = new System.Drawing.Drawing2D.GraphicsPath();
            gp.AddLine(x + radius, y, x + width - (radius * 2), y); //追加线段
            gp.AddArc(x + width - (radius * 2), y, radius * 2, radius * 2, 270, 90);  //追加椭圆弧
            gp.AddLine(x + width, y + radius, x + width, y + height - (radius * 2));
            gp.AddArc(x + width - (radius * 2), y + height - (radius * 2), radius * 2, radius * 2, 0, 90);
            gp.AddLine(x + width - (radius * 2), y + height, x + radius, y + height);
            gp.AddArc(x, y + height - (radius * 2), radius * 2, radius * 2, 90, 90);
            gp.AddLine(x, y + height - (radius * 2), x, y + radius);
            gp.AddArc(x, y, radius * 2, radius * 2, 180, 90);
            gp.CloseFigure();
            Pen pen = new System.Drawing.Pen(ColorTranslator.FromHtml("#CCCCCC"), 1);
            g.DrawPath(pen, gp);
            Rectangle rec = new Rectangle((int)x, (int)y, (int)width, (int)height);
            Brush brush = new System.Drawing.SolidBrush(color);
            g.FillPath(brush, gp);
            gp.Dispose();
        }

        private void ListControl_Load(object sender, EventArgs e)
        {
            CreateList(arr,dic);
            this.Focus();
        }

        private void CreateList(ArrayList a,Dictionary<string, ArrayList> d)
        {
            if (a.Count == 0 || d.Count == 0) return;
            for(int i = 0; i < d.Values.ElementAt<ArrayList>(0).Count; i++)
            {
                int w = 1;
                Panel p = new Panel();
                p.Height = 30;
                p.Width = panel2.Width;
                p.Top = i * 31+1;
                p.BackColor = panel2.BackColor;
                p.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
                for (int j = 0; j < a.Count; j++)
                {
                    Label lb = new Label();
                    lb.AutoSize = false;
                    lb.Width = int.Parse(a[j].ToString().Split(',')[1]);
                    lb.Height = 30;
                    lb.Name = a[j].ToString().Split(',')[0]+i;
                    lb.TextAlign = ContentAlignment.MiddleLeft;
                    lb.Left = w;
                    lb.Text = d[a[j].ToString().Split(',')[0]][i].ToString();
                    lb.ForeColor = Color.Black;
                    if (i % 2 == 0) { lb.BackColor = Color.LightGray; }
                    else { lb.BackColor = Color.LightGoldenrodYellow; }
                    lb.Click += new EventHandler(lb_Click);
                    p.Controls.Add(lb);
                    w += lb.Width + 1;
                }
                panel2.Controls.Add(p);
            }
        }
        
        private void lb_Click(object sender, EventArgs e)
        {
            Label currentlabel = (Label)sender;
            currentcontrol = currentlabel;
            if (ParentClick != null) { ParentClick(sender, e); }
        }

       
        
    }
}
