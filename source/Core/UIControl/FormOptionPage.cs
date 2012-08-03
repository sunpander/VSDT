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
    public partial class FormOptionPage : Form
    {
        public FormOptionPage()
        {
            InitializeComponent();
        }

        private void FormOptionPage_Load(object sender, EventArgs e)
        {
            InitilizeTreeView();
        }

        private void InitilizeTreeView()
        {
            TreeNode node3 = new TreeNode();
            node3.Name = "frameConfiguration";
            node3.Text ="pluginConfiguration";
            node3.Tag = new UCPluginConfig();
            this.treeView1.Nodes.Add(node3);
            //foreach (Plugin plugin in FrameContext.Instance.Plugins)
            //{
            //    try
            //    {
            //        if (plugin.IDTExtensibility is IOption)
            //        {
            //            if (!plugin.Enable)
            //            {
            //                continue;
            //            }
            //            IOption iDTExtensibility = plugin.IDTExtensibility as IOption;
            //            node3.Nodes.Add(iDTExtensibility.GetNode());
            //        }
            //    }
            //    catch
            //    {
            //    }
            //}
            node3.Expand();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            UserControl tag = null;
            if (this.panel.Controls.Count > 0)
            {
                foreach (Control control in this.panel.Controls)
                {
                    try
                    {
                        this.panel.Controls.Remove(control);
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            if ((e.Node.Tag != null) && (e.Node.Tag is UserControl))
            {
                tag = e.Node.Tag as UserControl;
            }
            else if (e.Node is OptionNode)
            {
                tag = ((OptionNode)e.Node).UI;
            }
            if (tag != null)
            {
                tag.Dock = DockStyle.Fill;
                this.panel.Controls.Clear();
                this.panel.Controls.Add(tag);
            }
        }

    }
}
