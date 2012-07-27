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
    public partial class IconWords : UserControl
    {
        public IconWords()
        {
            InitializeComponent();
            BindEvent();
        }

        private void BindEvent()
        {
            ImagePanel.MouseEnter += new EventHandler(DescriptionLabel_MouseEnter);
            ImagePanel.MouseLeave += new EventHandler(DescriptionLabel_MouseLeave);
            ImagePanel.Click += new EventHandler(DescriptionLabel_Click);
            ImagePanel.DoubleClick += new EventHandler(DescriptionLabel_DoubleClick);

            TitleLabel.MouseEnter += new EventHandler(DescriptionLabel_MouseEnter);
            TitleLabel.MouseLeave += new EventHandler(DescriptionLabel_MouseLeave);
            TitleLabel.Click += new EventHandler(DescriptionLabel_Click);
            TitleLabel.DoubleClick += new EventHandler(DescriptionLabel_DoubleClick);

            DescriptionLabel.MouseEnter += new EventHandler(DescriptionLabel_MouseEnter);
            DescriptionLabel.MouseLeave += new EventHandler(DescriptionLabel_MouseLeave);
            DescriptionLabel.Click += new EventHandler(DescriptionLabel_Click);
            DescriptionLabel.DoubleClick += new EventHandler(DescriptionLabel_DoubleClick);
            this.Load += new EventHandler(IconWords_Load);
        }

        public Color NormalColor
        {
            get ;
            set ;
        }

        public Color OverColor
        {
            get;
            set;
        }

        public Control Image
        { get { return ImagePanel; } }

        public Control Title
        { get { return TitleLabel; } }

        public Control Description
        { get { return DescriptionLabel; } }

        private void DescriptionLabel_DoubleClick(object sender, EventArgs e) { this.OnDoubleClick(e); }

        private void DescriptionLabel_Click(object sender, EventArgs e) { this.OnClick(e); }

        private void DescriptionLabel_MouseLeave(object sender, EventArgs e){ this.BackColor = NormalColor;  }

        private void DescriptionLabel_MouseEnter(object sender, EventArgs e){ this.BackColor = OverColor; }

        private void IconWords_Load(object sender, EventArgs e)
        { this.Type(this, 8, 0.1); }

        private void Type(Control sender, int p_1, double p_2)
        {
            GraphicsPath oPath = new GraphicsPath();
            oPath.AddClosedCurve(new Point[] { new Point(0, p_1), new Point(p_1, 0), new Point(sender.Width - p_1, 0), new Point(sender.Width, p_1), new Point(sender.Width, sender.Height - p_1), new Point(sender.Width - p_1, sender.Height), new Point(p_1, sender.Height), new Point(0, sender.Height - p_1) }, (float)p_2);
            sender.Region = new Region(oPath);
        }
    }
}
