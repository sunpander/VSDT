using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VSDT.Common;
using VSDT.Common.Utility;
using System.Xml;
namespace VSDT.Common.Tools
{
    public partial class FormGenConfigClass : Form
    {
        public FormGenConfigClass()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 画面状态
        /// </summary>
        public enum FormState
        {
            View, //浏览
            Edit  //编辑
        }
        public FormState formStateNow;//画面当前状态
       
        /// <summary>
        /// 打开xml文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpenXml_Click(object sender, EventArgs e)
        {
            try
            {
                string filePath = UtilityFile.ChooseOneFile("xml文件|*.xml");
                if (string.IsNullOrEmpty(filePath))
                    return;
                textBoxFileName.Text = filePath;
                treeView1.Nodes.Clear();
                //创建一个XML对象
                XmlDocument myxml = new XmlDocument();
                //读取已经有的xml
                myxml.Load(filePath);
                //声明一个节点存储根节点
                XmlNode root = myxml.DocumentElement;
                textBoxInnerText.Text = root.InnerText;

                TreeNode node = createNode(root);
                treeView1.Nodes.Add(node);    
            }
            catch (Exception ex)
            {
                Log.ShowErrorBox(ex);
            }

        }
        
        /// <summary>
        /// 根据XmlNode创建树节点
        /// </summary>
        /// <param name="xmlNode"></param>
        /// <returns></returns>
        private TreeNode createNode(XmlNode xmlNode)
        {
            TreeNode treeNode = new TreeNode(xmlNode.Name);
            treeNode.Tag = xmlNode;
            if (xmlNode.HasChildNodes)
            {
                if (xmlNode.FirstChild.NodeType == XmlNodeType.Text)
                {
                    return treeNode;
                }
                for (int i = 0; i < xmlNode.ChildNodes.Count; i++)
			    {
                    TreeNode nodeChild = createNode(xmlNode.ChildNodes[i]);
                    treeNode.Nodes.Add(nodeChild);
                }
            }
            return treeNode;

        }
        
        /// <summary>
        /// 节点点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (formStateNow == FormState.View)
            {

                if (null != e.Node.Tag && e.Node.Tag is XmlNode)
                {
                    XmlNode xmlNode = e.Node.Tag as XmlNode;
                    if (xmlNode == null)
                        return;
                    dataGridView1.Columns.Clear(); //清空属性
                    int count = xmlNode.Attributes.Count;
                    if (count > 0)
                    {
                        DataTable dt = new DataTable();
                        dt.Rows.Add(dt.NewRow());
                        for (int i = 0; i < count; i++)
                        {
                            dt.Columns.Add(xmlNode.Attributes[i].Name);
                            dt.Rows[0][xmlNode.Attributes[i].Name] = xmlNode.Attributes[i].Value;
                        }
                        dataGridView1.DataSource = dt;
                    }
                    if (xmlNode.FirstChild != null && xmlNode.FirstChild.NodeType == XmlNodeType.Text)
                    {
                        textBoxInnerText.Text = xmlNode.FirstChild.Value;
                    }
                }
            }
            else
            {
                if (null != e.Node.Tag && e.Node.Tag is EditXmlNode)
                {
                    EditXmlNode xmlNode = e.Node.Tag as EditXmlNode;
                    if (xmlNode == null)
                        return;
                    SetValueByEditXmlNode(xmlNode);
                }
            }
        }

        /// <summary>
        /// 类型下拉框事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxType.SelectedValue == typeof(DateTime))
            {
                dateTimePicker1.Enabled = true;
                checkBox1.Enabled = false;
                textBoxValue.Enabled = false;
            }
            else if (comboBoxType.SelectedValue == typeof(bool))
            {
                dateTimePicker1.Enabled = false;
                checkBox1.Enabled = true;
                textBoxValue.Enabled = false;
            }
            else
            {
                dateTimePicker1.Enabled = false;
                textBoxValue.Enabled = true;
                checkBox1.Enabled = false;
            }
        }

        /// <summary>
        /// 添加节点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddNode_Click(object sender, EventArgs e)
        {
            formStateNow = FormState.Edit;
            if (treeView1.SelectedNode != null)
            {
                TreeNode node = new TreeNode(textBoxConfigName.Text);

              
               

                node.Tag = GetEditXmlNode();
                 

                treeView1.SelectedNode.Nodes.Add(node);
            }
            else
            {
                TreeNode node = new TreeNode(textBoxConfigName.Text);
            

                node.Tag = GetEditXmlNode();
                treeView1.Nodes.Add(node);
            }
        }

        /// <summary>
        /// 编辑xml项
        /// </summary>
        public class EditXmlNode
        {
            public string name;
            public DataTable Attribute = new DataTable();
            public object DefaultValue;
            public Type ValueType;
        }
        /// <summary>
        /// 单独节点项
        /// </summary>
        public class Item
        {
            public string name;
            public object value;
            public Type type;
        }
        /// <summary>
        /// 根据EditXmlNode设置画面值
        /// </summary>
        /// <param name="xmlNode"></param>
        private void SetValueByEditXmlNode(EditXmlNode xmlNode)
        {
            dataGridView1.Columns.Clear(); //清空属性

            dataGridView1.DataSource = xmlNode.Attribute;

            comboBoxType.SelectedValue = xmlNode.ValueType;
            if (typeof(DateTime) == xmlNode.ValueType)
            {
                dateTimePicker1.Value = Convert.ToDateTime(xmlNode.DefaultValue);
                //dateTimePicker1.Enabled = true;
                //textBoxValue.Enabled = false;
                //checkBox1.Enabled = false;

            }
            else if (typeof(bool) == xmlNode.ValueType)
            {
                checkBox1.Checked = Convert.ToBoolean(xmlNode.DefaultValue);
                //dateTimePicker1.Enabled = false;
                //textBoxValue.Enabled = false;
                //checkBox1.Enabled = true;
            }
            else
            {
                textBoxValue.Text = xmlNode.DefaultValue.ToString();
                //dateTimePicker1.Enabled = false;
                //textBoxValue.Enabled = true;
                //checkBox1.Enabled = false;
            }
            textBoxConfigName.Text = xmlNode.name;
            textBoxInnerText.Text = xmlNode.DefaultValue.ToString();
 
        }
        /// <summary>
        /// 获取EditXmlNode值
        /// </summary>
        /// <returns></returns>
        private EditXmlNode GetEditXmlNode()
        {
            EditXmlNode xmlNode = new EditXmlNode();
            Item item = GetItem();
            xmlNode.ValueType = item.type;
            xmlNode.DefaultValue = item.value;
            xmlNode.name = item.name;
            return xmlNode;
        }
       
        /// <summary>
        /// 获取节点
        /// </summary>
        /// <returns></returns>
        private Item GetItem()
        {
            Item xmlItem = new Item();
            xmlItem.name = textBoxConfigName.Text;
            xmlItem.type = comboBoxType.SelectedValue as Type;
            if (typeof(DateTime) == xmlItem.type)
            {
                xmlItem.value = dateTimePicker1.Value;
            }
            else if (typeof(bool) == xmlItem.type)
            {
                xmlItem.value = checkBox1.Checked;
            }
            else
            {
                xmlItem.value = textBoxValue.Text;
            }
            return xmlItem;
        }
   
        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("Type", typeof(Type));
                dt.Columns.Add("Text");
                dt.Rows.Add(typeof(string), "字符串型");
                dt.Rows.Add(typeof(decimal), "数值型");
                dt.Rows.Add(typeof(DateTime), "日期型");
                dt.Rows.Add(typeof(bool), "布尔型");
                comboBoxType.DataSource = dt;
                comboBoxType.DisplayMember = "Text";
                comboBoxType.ValueMember = "Type";

                formStateNow = FormState.Edit;
                btnOpenXml.Enabled = false;
            }
            catch (Exception ex)
            {
                Log.ShowErrorBox(ex);
            }
        }

        /// <summary>
        /// 保存当前编辑节点值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveEdit_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode == null)
                return;
            treeView1.SelectedNode.Tag = GetEditXmlNode();
        }
        
        /// <summary>
        /// 添加属性
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddAttribute_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode == null || treeView1.SelectedNode.Tag == null)
                return;
            EditXmlNode xmlNode = treeView1.SelectedNode.Tag as EditXmlNode;
            DataTable dt = xmlNode.Attribute;
            Item item = GetItem();
            int index = dt.Columns.IndexOf(item.name);
            if (index>=0)
                dt.Columns.RemoveAt(index);
            dt.Columns.Add(item.name, item.type);

            xmlNode.Attribute = dt;
            treeView1.SelectedNode.Tag = xmlNode;
        }

        /// <summary>
        /// 生成配置类 文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGenerateClass_Click(object sender, EventArgs e)
        {
            try
            {
                treeView1.Tag = textBoxFileName.Text + ".cs";
                GenereateConfigClass generate = new GenereateConfigClass(treeView1);
               
                generate.StartGenerate();
            }
            catch (Exception eX)
            {
                Log.ShowErrorBox(eX);
            }
        }
        /// <summary>
        /// [生成配置类 文件 ]辅助类
        /// </summary>
        public class GenereateConfigClass
        {
            DataTable dtProperty;  //类属性(列类型,列名称)
            DataSet dsInnerClass;  //内部类对象(一个DataTable表示一个内部类)
            public string ClassName = ""; //类名称
            //参数字符串
            private StringBuilder PublicParamater = new StringBuilder();
            //内部类字符串
            private StringBuilder PublicInnerClass = new StringBuilder();
            //配置类内容
            public StringBuilder ConfigClassContent = new StringBuilder();
            /// <summary>
            /// 构造方法,根据treeView设置内部类以及属性
            /// </summary>
            /// <param name="treeViewConfig"></param>
            public GenereateConfigClass(TreeView treeViewConfig)
            {
                ClassName = treeViewConfig.Tag.ToString();
                if (treeViewConfig == null)
                    throw new ArgumentNullException();
                dtProperty = new DataTable();
                dtProperty.Rows.Add(dtProperty.NewRow());
                dsInnerClass = new DataSet();
                for (int i = 0; i < treeViewConfig.Nodes.Count; i++)
                {
                    TreeNode node = treeViewConfig.Nodes[i];
                    if (node.Nodes.Count > 0)
                    {
                        DataTable dt2 = GetInnerClassTable(node);
                        dsInnerClass.Tables.Add(dt2);
                        continue;
                    }
                    if (node.Tag != null && node.Tag is EditXmlNode)
                    {
                        EditXmlNode xmlNode = node.Tag as EditXmlNode;
                        dtProperty.Columns.Add(xmlNode.name, xmlNode.ValueType);
                        dtProperty.Rows[0][xmlNode.name] = xmlNode.DefaultValue;
                    }
                }
            }
            
            /// <summary>
            /// 启动生成
            /// </summary>
            public void StartGenerate()
            {
                ConfigClassContent.Append( string.Format("public class {0} \r{1} ", ClassName,"{"));
                ConfigClassContent.Append(CreatePublicParameter(dtProperty));

                ConfigClassContent.Append(CreatePublicInnertClass(dsInnerClass));
                ConfigClassContent.Append("\r");
                ConfigClassContent.Append(GetCommonMethod());
                ConfigClassContent.Append("\r");
                ConfigClassContent.Append("}");

                UtilityFile.SaveOneFile(ConfigClassContent.ToString());
            }

            /// <summary>
            /// 根据表获取公共 属性字符串
            /// </summary>
            /// <param name="dtTmp"></param>
            /// <returns></returns>
            private string CreatePublicParameter(DataTable dtTmp)
            {
                string strResult = "\r";
                for (int i = 0; i < dtTmp.Columns.Count; i++)
                {
                    string strTmp = "\tpublic {0} {1} {2} ";
                    if (dtTmp.Columns[i].DataType == typeof(DateTime))
                    {
                        strTmp = string.Format(strTmp, "DateTime", dtTmp.Columns[i].ColumnName,"{get;set;}");
                    }
                    else if (dtTmp.Columns[i].DataType == typeof(bool))
                    {
                        strTmp = string.Format(strTmp, "bool", dtTmp.Columns[i].ColumnName, "{get;set;}");
                    }
                    else
                    {
                        strTmp = string.Format(strTmp, "string", dtTmp.Columns[i].ColumnName, "{get;set;}");
                    }
                    
                    strResult  += (strTmp+"\r");
                }
                return strResult;              
            }
            /// <summary>
            /// 根据数据集,设置内部类字符串
            /// </summary>
            /// <param name="dsTmp"></param>
            /// <returns></returns>
            private string CreatePublicInnertClass(DataSet dsTmp)
            {
                for (int j = 0; j < dsTmp.Tables.Count; j++)
                {
                    DataTable dtTmp = dsTmp.Tables[0];

                    string strTmp = string.Format("\tpublic class {0} \r\t{1}", dtTmp.TableName,"{");
                    string strTmp2 =  CreatePublicParameter(dtTmp);
                    strTmp2 =strTmp2.Replace("\t", "\t\t");
                    strTmp = strTmp + strTmp2 + "\t}";
                    strTmp += "\r";
                    PublicInnerClass.Append(strTmp);
                }
                return PublicInnerClass.ToString();
            }
            /// <summary>
            /// 获取公共方法字符串
            /// </summary>
            /// <returns></returns>
            private string GetCommonMethod()
            {
                string str =  
                            " #region The public method" +"\r" +
                            "        public static #ConfigClassName# LoadConfig()"+"\r" +
                            "        {"+"\r" +
                            "            try"+"\r" +
                            "            {"+"\r" +"\r" +
                            "                object obj = XMLConfigUtility.LoadConfigByType(typeof(#ConfigClassName#));" + "\r" +
                            "            if (obj is #ConfigClassName#)"+"\r" +
                            "                    return obj as #ConfigClassName#;"+"\r" +
                            "                else"+"\r" +
                            "                    return new #ConfigClassName#();"+"\r" +
                            "            }"+"\r" +
                            "            catch"+"\r" +
                            "            {"+"\r" +
                            "                return new #ConfigClassName#();"+"\r" +
                            "            }"+"\r" +
                            "        }"+"\r" +
                            ""+"\r" +
                            "        public bool SaveConfig()"+"\r" +
                            "        {"+"\r" +
                            "            try"+"\r" +
                            "            {"+"\r" +
                            "                return XMLConfigUtility.SaveNewConfig(this);"+"\r" +
                            "            }"+"\r" +
                            "            catch (Exception ex)"+"\r" +
                            "            {"+"\r" +
                            "                DevExpress.XtraEditors.XtraMessageBox.Show(ex)" +"\r" +
                            "                return false;"+"\r" +
                            "            }"+"\r" +
                            "        }"+"\r" +
                            "        #endregion ";
                return str.Replace("#ConfigClassName#",ClassName);
            }
            /// <summary>
            /// 根据树节点TreeNode获取内部类字符串
            /// </summary>
            /// <param name="nodePara"></param>
            /// <returns></returns>
            private DataTable GetInnerClassTable(TreeNode nodePara)
            {
                DataTable dt = new DataTable(nodePara.Text);
                dt.Rows.Add(dt.NewRow());
                for (int i = 0; i < nodePara.Nodes.Count; i++)
                {
                    TreeNode node = nodePara.Nodes[i];
                    if (node.Nodes.Count > 0)
                    {
                        DataTable dt2 = GetInnerClassTable(node);
                    }
                    if (node.Tag != null && node.Tag is EditXmlNode)
                    {
                        EditXmlNode xmlNode = node.Tag as EditXmlNode;
                        dt.Columns.Add(xmlNode.name, xmlNode.ValueType);
                        dt.Rows[0][xmlNode.name] = xmlNode.DefaultValue;
                    }
                }
                return dt;
            }
        }

        /// <summary>
        /// 画面状态改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButtonEdit_CheckedChanged(object sender, EventArgs e)
        {
            ControlOperate.SetGroupBoxEnable(groupBoxEdit, radioButtonEdit.Checked);
            btnOpenXml.Enabled = !radioButtonEdit.Checked;
            formStateNow = radioButtonEdit.Checked ? FormState.Edit : FormState.View;
            textBoxFileName.ReadOnly = !radioButtonEdit.Checked;
            treeView1.Nodes.Clear();
            textBoxFileName.Text = "";
        }
    }
}