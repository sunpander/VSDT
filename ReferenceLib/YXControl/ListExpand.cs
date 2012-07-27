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
    public partial class ListExpand : UserControl
    {
        #region 变量
        //int ws = 0;
        //private Point p1;
        Color normalColor = Color.Lavender;
        Color normalForeColor = Color.Black;
        Color overForeColor = Color.Black;
        Color titlePanelColor = Color.LightSteelBlue;
        Color overTitleColor = Color.White;
        Color overColor = Color.LightPink;
        Font titleFont = new Font("微软雅黑", 12, FontStyle.Bold);
        Font contextFont = new Font("微软雅黑", 11);
        Control currentControl = null;
        Control currentItem = null;
        int lineheight = 40;
        #endregion

        #region 属性
        public Font TitleFont
        {
            get { return titleFont; }
            set { titleFont = value; }
        }

        public Color OverTitleColor
        {
            get { return overTitleColor; }
            set { overTitleColor = value; }
        }

        public Color TitlePanelColor
        {
            get { return titlePanelColor; }
            set { titlePanelColor = value; }
        }


        public Font ContextFont
        {
            get { return contextFont; }
            set { contextFont = value; }
        }

        public Color OverColor
        {
            get { return overColor; }
            set { overColor = value; }
        }

        public Color NormalColor
        {
            get { return normalColor; }
            set { normalColor = value; }
        }

        public Cursor ContentOverCursor
        {
            get;
            set;
        }


        public Color OverForeColor
        {
            get { return overForeColor; }
            set { overForeColor = value; }
        }

        public Color NormalForeColor
        {
            get { return normalForeColor; }
            set { normalForeColor = value; }
        }

        public Control CurrentControl
        {
            get { return currentControl; }
        }

        public int LineHeight
        {
            get { return lineheight; }
            set { lineheight = value; }
        }

        public Color BorderLineColor
        {
            get { return panel2.BackColor; }
            set { panel2.BackColor = value; }
        }

        public Control ParentControl
        {
            get { return panel2; }
        }

        public Control CurrentItem
        {
            get { return currentItem; }
            set { currentItem = value; }
        }

        #endregion

        int oldWidth = 0;

        public delegate void UserHandler(object sender, System.EventArgs e);
        public event UserHandler ParentClick;

        public ListExpand()
        {
            InitializeComponent();
        }

        public void InitItems(string[] header, Dictionary<string, ArrayList> data, int[] size)
        {
            CreateCell(header, data, size);
            if (panel2.Controls.Count > 0)
            {
                normalColor = panel2.Controls[0].BackColor;
                normalForeColor = panel2.Controls[0].Controls[0].ForeColor;
            }
            initEvent();
        }

        public void AddItems(ArrayList item)
        {
            Panel p = new Panel();
            p.Height = LineHeight+1;
            p.Width = panel2.Width;
            p.Top = panel2.Controls.Count * (LineHeight + 1) + 1;
            p.BackColor = NormalColor;
            p.Dock = DockStyle.Top;
            p.BringToFront();
            p.MouseClick += new MouseEventHandler(panel2_MouseClick);
            for (int i = 0; i < item.Count; ++i)
            {
                int positionleft = 0;
                int width = 0;
                if (panel2.Controls.Count == 0) { positionleft = i * 150; width = 150; }
                else { positionleft = panel2.Controls[panel2.Controls.Count-1].Controls[i].Left; width = panel2.Controls[panel2.Controls.Count-1].Controls[i].Width; }
                string strType = item[i].GetType().ToString();
                if (strType == "System.String" || strType == "System.Int32" || strType == "System.Single" || strType == "System.Double")
                {
                    Label lb = new Label();
                    lb.Width = width;
                    lb.Top = (LineHeight - lb.Height) / 2;
                    lb.TextAlign = ContentAlignment.MiddleLeft;
                    lb.Left = positionleft;
                    lb.Font = ContextFont;
                    lb.ForeColor = NormalForeColor;
                    lb.AutoSize = false;
                    lb.Click += new EventHandler(lb_Click);
                    lb.Text = item[i].ToString();
                    p.Controls.Add(lb);
                }
                else
                {
                    Control c = item[i] as Control;
                    c.Click += new EventHandler(lb_Click);
                    c.Left = positionleft;
                    c.Top = (p.Height - c.Height) / 2;
                    p.Controls.Add(c);
                }

            }
            Panel p1 = new Panel();
            p1.BackColor = BorderLineColor;
            p1.Width = p.Width;
            p1.Height = 1;
            p1.Top = LineHeight;
            p1.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
            p.Controls.Add(p1);
            panel2.Controls.Add(p);
            RefreshEvent();
        }

        public void DeleteItems(int index)
        {  panel2.Controls.RemoveAt(index);  }

        private void CreateCell(string[] headerMessage, Dictionary<string, ArrayList> dataMessage,int[] size)
        {
            if ((headerMessage == null || headerMessage.Length == 0) && (dataMessage == null || dataMessage.Count == 0)) { panel1.Visible = false; return; }
            #region
            else if ((headerMessage == null || headerMessage.Length == 0) && dataMessage != null && dataMessage.Count != 0)
            {
                panel1.Visible = false;
                if (size.Length < dataMessage.Count) { return; }
                panel2.Dock = DockStyle.Fill;
                for (int j = 0; j < dataMessage.Values.ElementAt<ArrayList>(0).Count; ++j)
                {
                    int childControlLeft = 0;
                    Panel p = new Panel();
                    p.Height = LineHeight + 1;
                    p.Width = panel2.Width;
                    p.Top = j * (LineHeight + 1) + 1;
                    p.BackColor = NormalColor;
                    p.Dock = DockStyle.Top;
                    p.BringToFront();
                    p.MouseClick += new MouseEventHandler(panel2_MouseClick);
                    for (int i = 0; i < dataMessage.Count; ++i)
                    {
                        string strType = dataMessage.Values.ElementAt<ArrayList>(i)[j].GetType().ToString();
                        if (strType == "System.String" || strType == "System.Int32" || strType == "System.Single" || strType == "System.Double")
                        {
                            Label lb = new Label();
                            lb.Width = size[i];
                            lb.Top = (LineHeight - lb.Height) / 2;
                            lb.Name = j + dataMessage.Keys.ElementAt<string>(i);
                            lb.TextAlign = ContentAlignment.MiddleLeft;
                            lb.Left = childControlLeft;
                            lb.Font = ContextFont;
                            lb.ForeColor = NormalForeColor;
                            lb.AutoSize = false;
                            lb.Click += new EventHandler(lb_Click);
                            lb.Text = dataMessage.Values.ElementAt<ArrayList>(i)[j].ToString();
                            p.Controls.Add(lb);
                            childControlLeft += lb.Width;
                        }
                        else
                        {
                            Control c = dataMessage.Values.ElementAt<ArrayList>(i)[j] as Control;
                            c.Click += new EventHandler(lb_Click);
                            c.Left = childControlLeft;
                            c.Top = (p.Height - c.Height) / 2;
                            p.Controls.Add(c);
                            childControlLeft += size[i];
                        }
                    }
                    Panel p1 = new Panel();
                    p1.BackColor = BorderLineColor;
                    p1.Width = p.Width;
                    p1.Height = 1;
                    p1.Top = LineHeight;
                    p1.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
                    p.Controls.Add(p1);
                    panel2.Controls.Add(p);
                    this.Width = childControlLeft;
                    oldWidth = childControlLeft;
                }
            }
            #endregion
            else if (headerMessage.Length > 0 && (dataMessage == null || dataMessage.Count == 0))
            {
                if (size.Length < headerMessage.Length) { return; }
                int w = 0;
                for (int i = 0; i < headerMessage.Length; ++i)
                {
                    Label lb = new Label();
                    lb.AutoSize = false;
                    lb.Width = size[i];
                    lb.Height = 40;
                    lb.Name = "lb" + headerMessage[i].ToString();
                    lb.TextAlign = ContentAlignment.MiddleLeft;
                    lb.Left = w;
                    lb.Text = headerMessage[i].ToString();
                    lb.ForeColor = NormalForeColor;
                    lb.Font = TitleFont;
                    lb.BackColor = Color.Transparent;
                    panel1.Controls.Add(lb);
                    w += lb.Width;
                }
                this.Width = w;
                oldWidth = w;
            }
            #region
            else if (headerMessage.Length > 0 && dataMessage.Count > 0)
            {
                if (size.Length < dataMessage.Count) { return; }
                int w = 0;
                for (int i = 0; i < headerMessage.Length; ++i)
                {
                    Label lb = new Label();
                    lb.AutoSize = false;
                    lb.Width = size[i];
                    lb.Height = 40;
                    lb.Name = "lb" + headerMessage[i].ToString();
                    lb.TextAlign = ContentAlignment.MiddleLeft;
                    lb.Left = w;
                    lb.Text = headerMessage[i].ToString();
                    lb.ForeColor = NormalForeColor;
                    lb.Font = TitleFont;
                    lb.BackColor = Color.Transparent;
                    panel1.Controls.Add(lb);
                    w += lb.Width;
                }
                for (int j = 0; j < dataMessage.Values.ElementAt<ArrayList>(0).Count; ++j)
                {
                    int childControlLeft = 0;
                    Panel p = new Panel();
                    p.Height = LineHeight + 1;
                    p.Width = panel2.Width;
                    p.Top = j * (LineHeight + 1) + 1;
                    p.BackColor = NormalColor;
                    p.Dock = DockStyle.Top;
                    p.BringToFront();
                    p.MouseClick += new MouseEventHandler(panel2_MouseClick);
                    for (int i = 0; i < dataMessage.Count; ++i)
                    {
                        string strType = dataMessage.Values.ElementAt<ArrayList>(i)[j].GetType().ToString();
                        if (strType == "System.String" || strType == "System.Int32" || strType == "System.Single" || strType == "System.Double")
                        {
                            Label lb = new Label();
                            lb.Width = panel1.Controls[i].Width;
                            lb.Top = (LineHeight - lb.Height) / 2;
                            lb.Name = j + dataMessage.Keys.ElementAt<string>(i);
                            lb.TextAlign = ContentAlignment.MiddleLeft;
                            lb.Left = panel1.Controls[i].Left;
                            lb.Font = ContextFont;
                            lb.ForeColor = NormalForeColor;
                            lb.Click += new EventHandler(lb_Click);
                            lb.Text = dataMessage.Values.ElementAt<ArrayList>(i)[j].ToString();
                            p.Controls.Add(lb);
                            childControlLeft += lb.Width;
                        }
                        else
                        {
                            Control c = dataMessage.Values.ElementAt<ArrayList>(i)[j] as Control;
                            c.Click += new EventHandler(lb_Click);
                            c.Left = panel1.Controls[i].Left;
                            c.Top = (p.Height - c.Height) / 2;
                            p.Controls.Add(c);
                            childControlLeft += panel1.Controls[i].Width;
                        }
                    }
                    Panel p1 = new Panel();
                    p1.BackColor = BorderLineColor;
                    p1.Width = p.Width;
                    p1.Height = 1;
                    p1.Top = LineHeight;
                    p1.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
                    p.Controls.Add(p1);
                    panel2.Controls.Add(p);
                }
                this.Width = w;
                oldWidth = w;
            }
            #endregion
        }

        private void lb_Click(object sender, EventArgs e)
        {
            Control currentlabel = (Control)sender;
            currentControl = currentlabel;
            if (ParentClick!=null){ ParentClick(sender, e); }
        }
        //拖动
        private void lb_SizeChanged(object sender, EventArgs e)
        {
            //Control currentcontrol = (Control)sender;
            //if (currentcontrol.GetType().Name == "Label")
            //{
            //    for (int i = 0; i < panel1.Controls.Count; ++i)
            //    {
            //        if (panel1.Controls[i].Left > currentcontrol.Left)
            //        { panel1.Controls[i].Left += ws; }
            //    }
            //    foreach (Panel l in panel2.Controls)
            //    {
            //        for (int m = 0; m < panel1.Controls.Count; ++m)
            //        {  l.Controls[m].Left = panel1.Controls[m].Left;  }
            //    }
            //}
            
        }

        private void lb_MouseEnter(object sender, EventArgs e)
        {
            Control currentcontrol = (Control)sender;
            if (currentcontrol.GetType().Name == "Label")
            {
                currentcontrol.BackColor = OverTitleColor;
                currentcontrol.ForeColor = OverForeColor;
            }
        }
        //拖动
        private void lb_MouseDown(object sender, MouseEventArgs e)
        {
            //Control currentcontrol = (Control)sender;
            //if (currentcontrol.Cursor == Cursors.SizeWE)
            //{
            //    p1.X = e.X;
            //    p1.Y = e.Y;
            //}
        }

        private void lb_MouseLeave(object sender, EventArgs e)
        {
            Control currentcontrol = (Control)sender;
            currentcontrol.BackColor = TitlePanelColor;
            currentcontrol.Cursor = Cursors.Default;
            currentcontrol.ForeColor = NormalForeColor;
        }
        //拖动
        private void lb_MouseMove(object sender, MouseEventArgs e)
        {
            //Control currentcontrol = (Control)sender;
            //if (currentcontrol.GetType().Name == "Label")
            //{
            //    Point p = PointToClient(Control.MousePosition);
            //    if (p.X >= currentcontrol.Left + currentcontrol.Width - 8 && p.X < currentcontrol.Left + currentcontrol.Width)
            //    { currentcontrol.Cursor = Cursors.SizeWE; }
            //    else
            //    { currentcontrol.Cursor = Cursors.Default; }
            //}
            //if (e.Button == MouseButtons.Left && currentcontrol.Cursor == Cursors.SizeWE)
            //{
            //    ws = e.X - p1.X;
            //    currentcontrol.Width = currentcontrol.Width + e.X - p1.X;
            //    p1.X = e.X;
            //    p1.Y = e.Y; //'记录光标拖动的当前点  
            //}
        }
        
        private void initEvent()
        {
            for (int i = 0; i < panel1.Controls.Count; ++i)
            {
                panel1.Controls[i].MouseEnter += new EventHandler(lb_MouseEnter);
                panel1.Controls[i].MouseDown += new MouseEventHandler(lb_MouseDown);
                panel1.Controls[i].MouseLeave += new EventHandler(lb_MouseLeave);
                panel1.Controls[i].SizeChanged += new EventHandler(lb_SizeChanged);
                panel1.Controls[i].MouseMove += new MouseEventHandler(lb_MouseMove);
            }
            foreach (Panel p in panel2.Controls)
            {
                foreach (Control l in p.Controls)
                { 
                    l.MouseEnter += new EventHandler(l_MouseEnter);
                    l.MouseLeave += new EventHandler(l_MouseLeave);
                }
                p.MouseEnter += new EventHandler(p_MouseEnter);
                p.MouseLeave += new EventHandler(p_MouseLeave);
            }
            panel2.MouseWheel += new MouseEventHandler(panel2_MouseWheel);
            panel2.MouseClick += new MouseEventHandler(panel2_MouseClick);
            this.SizeChanged += new EventHandler(ListExpand_SizeChanged);
        }

        private void RefreshEvent()
        {
            foreach (Panel p in panel2.Controls)
            {
                foreach (Control l in p.Controls)
                {
                    l.MouseEnter += new EventHandler(l_MouseEnter);
                    l.MouseLeave += new EventHandler(l_MouseLeave);
                }
                p.MouseEnter += new EventHandler(p_MouseEnter);
                p.MouseLeave += new EventHandler(p_MouseLeave);
            }
        }

        private void p_MouseLeave(object sender, EventArgs e)
        {
            Panel currentpanel = (Panel)sender;
            currentpanel.BackColor = NormalColor;
            foreach (Control l in currentpanel.Controls)
            {
                if (l.GetType().Name == "Label")
                {
                    l.BackColor = NormalColor;
                    l.ForeColor = NormalForeColor;
                }
            }
            currentItem = null;
        }

        private void p_MouseEnter(object sender, EventArgs e)
        {
            Panel currentpanel = (Panel)sender;
            currentpanel.BackColor = OverColor;
            foreach (Control l in currentpanel.Controls)
            {
                if (l.GetType().Name == "Label")
                {
                    l.BackColor = OverColor;
                    l.ForeColor = OverForeColor;
                }
            }
            currentItem = currentpanel;
        }

        private void l_MouseLeave(object sender, EventArgs e)
        {
            Control currentlabel = (Control)sender;
            currentlabel.Parent.BackColor = NormalColor;
            foreach (Control l in currentlabel.Parent.Controls)
            {
                if (l.GetType().Name == "Label")
                {
                    l.BackColor = NormalColor;
                    l.ForeColor = NormalForeColor;
                }
                l.Cursor = Cursors.Default;
            }
            currentItem = null;
        }

        private void l_MouseEnter(object sender, EventArgs e)
        {
            Control currentlabel = (Control)sender;
            currentlabel.Parent.BackColor = OverColor;
            foreach (Control l in currentlabel.Parent.Controls)
            {
                if (l.GetType().Name == "Label")
                {
                    l.BackColor = OverColor;
                    l.ForeColor = OverForeColor;
                }
                l.Cursor = ContentOverCursor;
            }
            currentItem = currentlabel.Parent;
        }

        private void panel2_MouseWheel(object sender, MouseEventArgs e)
        {
            int mVSValue = this.panel2.VerticalScroll.Value;
            int pScrollValueDelta = e.Delta;

            if ((mVSValue - pScrollValueDelta) <= this.panel2.VerticalScroll.Minimum)
            {
                this.panel2.VerticalScroll.Value = this.panel2.VerticalScroll.Minimum;
            }
            else if ((mVSValue - pScrollValueDelta) >= this.panel2.VerticalScroll.Maximum)
            {
                this.panel2.VerticalScroll.Value = this.panel2.VerticalScroll.Maximum;
            }
            else
            {
                this.panel2.VerticalScroll.Value -= pScrollValueDelta;
            }

            if (this.panel2.VerticalScroll.Value != mVSValue)
            {
                return;
            }

            this.panel2.Refresh();
            this.panel2.Invalidate();
            this.panel2.Update();
        }

        private void panel2_MouseClick(object sender, EventArgs e)
        { panel2.Focus(); }

        private void ListExpand_Load(object sender, EventArgs e)
        {
            panel1.BackColor = TitlePanelColor;
        }

        private void ListExpand_SizeChanged(object sender, EventArgs e)
        {
            //if (panel1.Visible == true)
            //{
            //    int leftPosition = 0;
            //    int m = (this.Width - oldWidth) / panel1.Controls.Count;
            //    foreach (Label l1 in panel1.Controls)
            //    {
            //        l1.Left = leftPosition;
            //        l1.Width += m;
            //        leftPosition += l1.Width;
            //    }
            //    if (panel2.Controls.Count > 0)
            //    {
            //        foreach (Panel p in panel2.Controls)
            //        {
            //            for (int i = 0; i < panel1.Controls.Count; i++)
            //            { p.Controls[i].Left += panel1.Controls[i].Left; }
            //        }
            //    }
            //}
            //else
            //{
            //    int m = (this.Width - oldWidth) / panel1.Controls.Count;
            //    foreach (Panel p in panel2.Controls)
            //    {
            //        for (int i = 0; i < panel2.Controls.Count - 1; i++)
            //        { p.Controls[i].Left += i*m; }
            //    }
            //}
        }
    }

    
}
