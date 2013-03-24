using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraBars;

namespace auth
{
    public partial class FormTEST : Form
    {
        public FormTEST()
        {
            InitializeComponent();
        }

        private void FormTEST_Load(object sender, EventArgs e)
        {

        }

        public Bar ChildBar
        {
            get
            {
                return bar2;
            }
        }
    }
}
