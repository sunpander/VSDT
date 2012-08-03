using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using VSDT.Common;

namespace VSDT.UIControl
{
    public partial class FormOptionPage2 : Form
    {
        public FormOptionPage2()
        {
            InitializeComponent();
        }

        private void FormOptionPage_Load(object sender, EventArgs e)
        {

        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            try
            {
            }
            catch (Exception ex)
            {
                Log.ShowErrorBox(ex);
            }
        }

        private void btnDefaultSetting_Click(object sender, EventArgs e)
        {
            try
            {
 
            }
            catch (Exception ex)
            {
                 Log.ShowErrorBox(ex);
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
             }
            catch (Exception ex)
            {
                Log.ShowErrorBox(ex);
            }
        }

        private void btnEditTree_Click(object sender, EventArgs e)
        {
            btnSaveTree.Enabled = true;
            btnUseDefautTree.Enabled = true;
        }

        private void btnSaveTree_Click(object sender, EventArgs e)
        {
            treeView1.ContextMenuStrip = null;
            try
            {

                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            btnSaveTree.Enabled = false;
            btnUseDefautTree.Enabled = false;
        }

        private void ToolStripMenuItemAddNode_Click(object sender, EventArgs e)
        {
            TreeNode node = treeView1.SelectedNode;

            TreeNodeItem nodeItem = new TreeNodeItem();
            nodeItem.Aclid = Guid.NewGuid().ToString();

            if (node == null)
            {
                //创建新的
                nodeItem.FName = "root";
            }
            else
            {
                if (node.Tag != null)
                {
                    nodeItem.FName = (node.Tag as TreeNodeItem).Aclid;
                }
            }
            FormAddTreeNode frm = new FormAddTreeNode();
            frm.EditNodeItem = nodeItem;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                nodeItem = frm.EditNodeItem;


                TreeNode nodeTmp = new TreeNode();
                nodeTmp.Text = frm.EditNodeItem.Descript;
                nodeTmp.Tag = frm.EditNodeItem;

                if (node == null)
                {
                    treeView1.Nodes.Add(nodeTmp);
                }
                else
                {
                    node.Nodes.Add(nodeTmp);
                }
            }
        }

        private void ToolStripMenuItemDelNode_Click(object sender, EventArgs e)
        {
            TreeNode node = treeView1.SelectedNode;
            if (node != null)
            {
                node.Remove();
            }
        }
 

         private void btnUseDefautTree_Click(object sender, EventArgs e)
        {

            
        }

        private void treeView1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                treeView1.ContextMenuStrip = this.contextMenuStrip1;
                TreeNode selNode = treeView1.GetNodeAt(e.Location);
                if (selNode != null)
                {
                    treeView1.SelectedNode = selNode;
                }
            }
        }

        private void treeView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (treeView1.GetNodeAt(e.Location) != null)
            {
                TreeNode node = treeView1.GetNodeAt(e.Location);
                if (node.Tag != null && node.Tag is TreeNodeItem)
                {
                    TreeNodeItem item = node.Tag as TreeNodeItem;
                    item.DllName = "";
                    item.Name = "";
                }
            }
        }
    }
}
