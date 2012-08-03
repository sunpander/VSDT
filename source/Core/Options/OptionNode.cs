using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VSDT
{
    public class OptionNode :  TreeNode
    {
        private string name;
        private UserControl ui;

        public UserControl UI
        {
            get
            {
                return this.ui;
            }
            set
            {
                this.ui = value;
            }
        }
    }
}
