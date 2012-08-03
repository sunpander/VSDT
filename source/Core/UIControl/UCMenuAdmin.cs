using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VSDT.UIControl
{
    public partial class UCMenuAdmin : UserControl
    {
        public UCMenuAdmin()
        {
            InitializeComponent();
        }
        public readonly static Guid ConstGuid= new Guid("{90F9F5B4-ED6F-4f88-8B1C-1AB491CF05C3}");
        
        private void btnAdd_Click(object sender, EventArgs e)
        {

        }

        private void comboBoxPlace_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxPlace.SelectedIndex == 0)
            {
                treeViewMenu.Nodes.Clear();

                TreeNode node1 = new TreeNode("iPlat4C(新)");
                treeViewMenu.Nodes.Add(node1);
            }
        }
    }
}
