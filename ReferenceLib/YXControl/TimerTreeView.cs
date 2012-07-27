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
    public partial class TimerTreeView : UserControl
    {
        public TimerTreeView()
        {
            InitializeComponent();
        }
        public string Context
        {
            get { return label1.Text; }
            set { label1.Text = value; }
        }

        public Control UpdateButton
        {
            get { return button1; }
        }

        public Control CancelButton
        {
            get { return button2; }
        }
    }
}
