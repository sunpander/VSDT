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
    public partial class Scrollbar : UserControl
    {
        public Scrollbar()
        {
            InitializeComponent();
            MainPanel.Controls.SetChildIndex(ScrollLabel,0);
            MainPanel.Controls.SetChildIndex(LeftPanel,1);
        }

        public Control RightControl
        {
            get { return RightPart; }
        }

        public Control LeftControl
        {
            get { return LeftPart; }
        }

        public Control Scrolls
        {
            get { return ScrollLabel; }
        }

        public Color HasPlayColor
        {
            get { return LeftPanel.BackColor; }
            set { LeftPanel.BackColor = value; }
        }

        public Image HasPlayBackImage
        {
            get { return LeftPanel.BackgroundImage; }
            set { LeftPanel.BackgroundImage = value; }
        }

        public Color NotPlayColor
        {
            get { return RightPanel.BackColor; }
            set { RightPanel.BackColor = value; }
        }

        public Image NotPlayBackImage
        {
            get { return RightPanel.BackgroundImage; }
            set { RightPanel.BackgroundImage = value; }
        }

        public Control HasPlayControl
        {
            get { return LeftPanel; }
        }

        public Control NotPlayControl
        {
            get { return RightPanel; }
        }

        private Point p1; int ws = 0; bool ismove = false;

        private void Scroll_LocationChanged(object sender, EventArgs e)
        {
            if (ScrollLabel.Left > MainPanel.Width - ScrollLabel.Width) { ismove = false; ScrollLabel.Left = MainPanel.Width - ScrollLabel.Width; LeftPanel.Width = ScrollLabel.Left; }
            else if (ScrollLabel.Left < 0) { ismove = false; ScrollLabel.Left = 0; }
            else{ LeftPanel.Left = LeftPanel.Top = 0;  LeftPanel.Width = ScrollLabel.Left ;  }
        }

        private void Scroll_MouseDown(object sender, MouseEventArgs e)
        { p1.X = e.X; p1.Y = e.Y; ismove = true; }

        private void Scroll_MouseMove(object sender, MouseEventArgs e)
        {
            if (ismove == false) return;
            Point p = PointToClient(Control.MousePosition);
            if (p.X >= LeftPart.Width+ScrollLabel.Width && p.X <= this.Width - RightPart.Width - ScrollLabel.Width )
            {
                if ((ScrollLabel.Left + RightPart.Width) >= MainPanel.Left && (ScrollLabel.Left + ScrollLabel.Width) < MainPanel.Left + MainPanel.Width) { ismove = true; }
                else { ismove = false; } if (e.Button == MouseButtons.Left && ismove == true) { ws = e.X - p1.X; ScrollLabel.Left += ws; }
            }
            else if (ScrollLabel.Left > 0 && ScrollLabel.Left<= ScrollLabel.Width) { ScrollLabel.Left = 0; }
            else if (ScrollLabel.Left < MainPanel.Width - ScrollLabel.Width && ScrollLabel.Left>= MainPanel.Width - 2*ScrollLabel.Width) { ScrollLabel.Left = MainPanel.Width - ScrollLabel.Width;}
        }

        private void Scroll_MouseUp(object sender, MouseEventArgs e)
        { ismove = false; }

        private void MainPanel_SizeChanged(object sender, EventArgs e)
        {
            LeftPanel.Height = MainPanel.Height;
            RightPanel.Height = MainPanel.Height;
            ScrollLabel.Height = MainPanel.Height;
        }

        private void RightPanel_MouseDown(object sender, MouseEventArgs e)
        {
            Point p = PointToClient( Control.MousePosition);
            ScrollLabel.Left = p.X -LeftPart.Width ;
            LeftPanel.Width = ScrollLabel.Left;
        }

        private void LeftPanel_MouseDown(object sender, MouseEventArgs e)
        {
            Point p = PointToClient(Control.MousePosition);
            ScrollLabel.Left = p.X - LeftPart.Width;
            LeftPanel.Width = ScrollLabel.Left;
        }

        public void SetPosition(double number)
        {
            ScrollLabel.Left = (int)((MainPanel.Width - ScrollLabel.Width) * number);
            LeftPanel.Width = ScrollLabel.Left;
        }

        
    }
}
