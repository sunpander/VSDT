using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VSDT.UIControl
{
    public partial class FormAddTreeNode : Form
    {
        public FormAddTreeNode()
        {
            InitializeComponent();
        }


        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (EditNodeItem == null)
                {
                    EditNodeItem = new TreeNodeItem();
                }
                EditNodeItem.Aclid = Aclid;
                EditNodeItem.FName = FAclid;
                EditNodeItem.Descript = txtDescript.Text;
                EditNodeItem.DllName = txtDllName.Text;
                EditNodeItem.Name = txtName.Text;

                DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        public string FAclid = "";
        public string Aclid = "";
        public TreeNodeItem EditNodeItem;
      
        private void FormAddTreeNode_Load(object sender, EventArgs e)
        {

            if (EditNodeItem == null)
            {
                EditNodeItem = new TreeNodeItem();
            }
            else
            {
                Aclid = EditNodeItem.Aclid;
                FAclid = EditNodeItem.FName;
            }
            txtAclid.Text = Aclid;
            txtFName.Text = FAclid;
        }
    }
}
