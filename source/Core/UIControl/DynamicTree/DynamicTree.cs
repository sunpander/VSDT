using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VSDT.Common.Utility;

namespace VSDT.UIControl
{
    public partial class DynamicTree : UserControl
    {
        public string DefaultLocalConfigPath = "d:\\tree.xml";
        public DynamicTree()
        {
            InitializeComponent();
            this.txtXmlPath.Text = DefaultLocalConfigPath;
            XmlTreeNode.localConfigPath = this.txtXmlPath.Text;
        }
        public System.Windows.Forms.ContextMenuStrip ContextMenuNow;
        private void btnEditTree_Click(object sender, EventArgs e)
        {
            ContextMenuNow = contextMenuStrip1;
            btnSaveTree.Enabled = true;
            btnUseDefautTree.Enabled = true;
        }
        private void btnUseDefautTree_Click(object sender, EventArgs e)
        {
            TreeNodeItem node1 = new TreeNodeItem();
            XmlTreeNode list = XmlTreeNode.LoadFromXml();
            list.Sort();
            if (list != null)
            {
                DataTable dt = list.GetListNode();
                DataRow[] dr = dt.Select("fname ='root'");
                if (dr.Length > 0)
                {
                    for (int i = 0; i < dr.Length; i++)
                    {
                        string aclid = dr[i]["ACLID"].ToString();
                        TreeNodeItem item = list.GetItemByAclid(aclid);
                        TreeNode treeNode = new TreeNode();
                        treeNode.Text = item.Descript;
                        treeNode.Tag = item;
                        //创建子节点
                        CreateChildNode(treeNode, list, dt, aclid);

                        treeView1.Nodes.Add(treeNode);
                    }
                }
            }
            else
            {
                list = new XmlTreeNode();
                for (int i = 0; i < 5; i++)
                {
                    node1 = new TreeNodeItem();
                    node1.Descript = "descript" + i;
                    node1.Name = "name" + i;
                    node1.FName = "root";
                    node1.DllName = "dllName" + i;
                    node1.Aclid = Guid.NewGuid().ToString();
                    list.listNode.Add(node1);
                }
                for (int i = 0; i < 5; i++)
                {
                    node1 = new TreeNodeItem();
                    node1.Descript = "node descript" + i;
                    node1.Name = "name" + i;
                    node1.FName = "name" + i;
                    node1.DllName = "dllName" + i;
                    node1.Aclid = Guid.NewGuid().ToString();
                    list.listNode.Add(node1);
                }
            }
            XmlTreeNode.SaveToXml(list);
        }
        private void btnSaveTree_Click(object sender, EventArgs e)
        {
            ContextMenuNow = null;
            try
            {
                XmlTreeNode.SaveToXml(treeView1);
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
            AddNode(ToolStripMenuItemAddNode.Text);
        }
        public void AddNode(string  fromTitle)
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
            frm.Text = fromTitle;
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
       
        public class XmlTreeNode
        {
            public List<TreeNodeItem> listNode = new List<TreeNodeItem>();
            private DataTable dtListNode = null;
            private System.Collections.Hashtable hashListNode = null;

            public void Sort()
            {
                hashListNode = new System.Collections.Hashtable();
                //把listNode的内容放入DataTable中,方便排序

                dtListNode = new DataTable();
                dtListNode.Columns.Add("ACLID");
                dtListNode.Columns.Add("FName");
                dtListNode.Columns.Add("SeqNo");



                for (int i = 0; i < listNode.Count; i++)
                {
                    TreeNodeItem item = listNode[i];
                    DataRow dr = dtListNode.NewRow();
                    dr["ACLID"] = item.Aclid;
                    dr["FNAME"] = item.FName;
                    dr["SeqNo"] = item.SeqNo;
                    dtListNode.Rows.Add(dr);
                    if (!hashListNode.Contains(item.Aclid))
                    {

                        hashListNode.Add(item.Aclid, item);
                    }
                }

            }
            public TreeNodeItem GetItemByAclid(string aclid)
            {
                if (hashListNode == null)
                    return null;
                if (hashListNode.ContainsKey(aclid))
                {
                    return hashListNode[aclid] as TreeNodeItem;
                }
                return null;
            }
            public DataTable GetListNode()
            {
                return dtListNode;
            }
            public TreeNodeItem GetItemByFAciId(string aclid)
            {
                if (hashListNode == null)
                    return null;
                if (hashListNode.ContainsKey(aclid))
                {
                    return hashListNode[aclid] as TreeNodeItem;
                }
                return null;
            }
            public static void SaveToXml(XmlTreeNode obj)
            {
                try
                {
                    //先序列化
                    System.Xml.Serialization.XmlSerializer xs = new System.Xml.Serialization.XmlSerializer(obj.GetType());
                    System.IO.StringWriter sw = new System.IO.StringWriter();
                    xs.Serialize(sw, obj);

                    System.IO.File.WriteAllText(localConfigPath, sw.ToString(), Encoding.UTF8);

                    sw.Flush();
                    sw.Close();
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
            }


            public static void SaveToXml(TreeView treeView)
            {
                try
                {
                    if (treeView == null)
                        return;
                    if (treeView.Nodes.Count == 0)
                        return;
                    XmlTreeNode obj = new XmlTreeNode();
                    for (int i = 0; i < treeView.Nodes.Count; i++)
                    {
                        if (treeView.Nodes[i].Tag != null && treeView.Nodes[i].Tag is TreeNodeItem)
                        {

                            obj.listNode.Add(treeView.Nodes[i].Tag as TreeNodeItem);
                            if (treeView.Nodes[i].Nodes.Count > 0)
                            {
                                AddTreeNode(treeView.Nodes[i], obj);
                            }
                        }
                    }
                    SaveToXml(obj);
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
            }

            public static void AddTreeNode(TreeNode node, XmlTreeNode obj)
            {
                if (node == null)
                    return;
                if (node.Nodes.Count == 0)
                    return;
                if (obj == null)
                    return;
                for (int i = 0; i < node.Nodes.Count; i++)
                {
                    if (node.Nodes[i].Tag != null && node.Nodes[i].Tag is TreeNodeItem)
                    {
                        obj.listNode.Add(node.Nodes[i].Tag as TreeNodeItem);
                        if (node.Nodes[i].Nodes.Count > 0)
                        {
                            AddTreeNode(node.Nodes[i], obj);
                        }
                    }
                }
            }
            public static string localConfigPath = "d:\\tree.xml";
            public static XmlTreeNode LoadFromXml()
            {
                try
                {
                    if (!System.IO.File.Exists(localConfigPath))
                    {
                        return null;
                    }
                    //找对应字符串
                    System.Xml.Serialization.XmlSerializer xs = new System.Xml.Serialization.XmlSerializer(typeof(XmlTreeNode));

                    System.IO.StreamReader sr = new System.IO.StreamReader(localConfigPath);

                    object obj = xs.Deserialize(sr);
                    sr.Close();

                    return obj as XmlTreeNode;
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                    return null;
                }
            }
        }

        private void CreateChildNode(TreeNode node, XmlTreeNode list, DataTable dt, string fAclid)
        {
            DataRow[] drTmp = dt.Select("fname ='" + fAclid + "'");
            if (drTmp != null && drTmp.Length > 0)
            {
                for (int i = 0; i < drTmp.Length; i++)
                {
                    string aclid = drTmp[i]["ACLID"].ToString();
                    TreeNodeItem item = list.GetItemByAclid(aclid);
                    TreeNode treeNode = new TreeNode();
                    treeNode.Text = item.Descript;
                    treeNode.Tag = item;
                    //创建子节点
                    CreateChildNode(treeNode, list, dt, aclid);

                    node.Nodes.Add(treeNode);
                }
            }

        }
     
        private void treeView1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                TreeNode selNode = treeView1.GetNodeAt(e.Location);
                if (selNode != null)
                {
                    treeView1.SelectedNode = selNode;
                
                    string descrip = treeView1.SelectedNode.Text;
                    ToolStripMenuItemAddNode.Text = "添加" + descrip;
                    ToolStripMenuItemDelNode.Text = "删除" + descrip;
                }
                treeView1.ContextMenuStrip = this.ContextMenuNow;
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

        private void btnBrowser_Click(object sender, EventArgs e)
        {
            string strFile = UtilityFile.ChooseOneFile("*.xml|XML文件|*.*|所有文件");
            if (string.IsNullOrEmpty(strFile.Trim()))
            {
                return;
            }
            txtXmlPath.Text = strFile;
            XmlTreeNode.localConfigPath = this.txtXmlPath.Text;
        }
    }


    public class TreeNodeItem
    {
        public string Name = "";
        public string Descript = "";
        public string DllName = "";
        public string FName = "";
        public string SeqNo = "";
        public string Aclid = "";
    }
}
