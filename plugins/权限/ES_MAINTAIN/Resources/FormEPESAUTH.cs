using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraGrid.Views.Grid;
using System.Collections;
using EF;
using DevExpress.XtraBars;

namespace EP
{
    /// <summary>
    /// 本页面主要实现群组授权功能，及其相关的辅助功能。
    /// </summary>
    /// Title: FormESAUTH
    /// Copyright: Baosight Software LTD.co Copyright (c) 2009
    /// Company: 上海宝信软件股份有限公司
    /// Author: 于修文
    /// Version: 3.6_DP01
    /// Use Case: 203167, 203168, 203170, 203174, 203175
    /// History:
    ///		2011-01-04  翁菲    [创建]
    ///		2011-01-25  于修文  [修改]功能实现；
    ///		2011-02-11  于修文  [修改]用户反查父组；
    ///		2011-05-06  翁菲    [修改]群组细部资源权限查询与授权
    ///		
    public partial class FormEPESAUTH : DevExpress.XtraEditors.XtraForm
    {
        #region 私有成员
        //选中子系统/帐套
        private string selectedCompanyCode = "";
        private string selectedAppname = "";

        //用户翻页
        private int page_index = 0;		
        private int pagecount = 1000;
        private int total_page = 0;

        //修改权限主客体
        private List<string> listFormGrant = new List<string>();
        private List<string> listFormRevok = new List<string>();
        private List<string> listButtGrant = new List<string>();
        private List<string> listButtRevok = new List<string>();
        private List<string> listGroupGrant = new List<string>();
        private List<string> listGroupRevok = new List<string>();
        
        private Hashtable htGroupResAdd = new Hashtable();
        private Hashtable htGroupResRmv = new Hashtable();
        private Hashtable htResGroupAdd = new Hashtable();
        private Hashtable htResGroupRmv = new Hashtable();

        //修改权限细部资源
        private List<string> listOthResGrant = new List<string>();
        private List<string> listOthResRevok = new List<string>();

        //主客体
        private enum SUBJTYPE { GROUP, USER, NOAUTHGROUP, NULL};
        private enum OBJTYPE { FORM, BUTTON };

        private SUBJTYPE subjType;
        private string subjEname = "";
        private OBJTYPE objType;
        private string formEname = "";
        private string buttEname = "";

        //节点
        private List<string> nodeName = new List<string>();
        private Dictionary<string, Dictionary<string, object>> auth = new Dictionary<string, Dictionary<string, object>>();
        private Dictionary<string, object> forminfo = new Dictionary<string, object>();

        //图标编号
        private const int FOLDERICON = 6;
        private const int FOLDERICON_EXPAND = 7;
        private const int FORMICON   = 1;
        private const int BUTTICON   = 2;
        
        //imageCollection15
        private const int USERICON   = 0;
        private const int GROUPICON = 3;
        private const int GROUPICON2 = 5;

        //imageCollection1
        private const int GROUP_ICON = 0;
        private const int GROUP_GRAY = 1;
        private const int GROUP_USER = 2;

        private const int RESGROUPICON = 9;
        private const int OTHICON = 8;

        //树和列表HitInfo
        DevExpress.XtraTreeList.TreeListHitInfo hiTreeList = null;
        DevExpress.XtraTreeList.TreeListHitInfo hiTreeRes = null;
        DevExpress.XtraTreeList.TreeListHitInfo hiGroup = null;
        DevExpress.XtraTreeList.TreeListHitInfo hiResGroup = null;

        private bool isManageMode = false;
        private string para_groupname = "";
        private string para_appname = "";
        #endregion

        #region 初始化
        public FormEPESAUTH()
        {
            InitializeComponent();
        }

        private void FormESAUTH_Load(object sender, EventArgs e)
        {
            //判断是否为9672
            if (EPESCommon.AuthMode == AUTHMODE.MODE_CLASSIC)
            {
                this.lblR.PageVisible = false;
                fgButtonTree.Visible = true;
                InitOthTypes();
            }
            else
            {
                fgButtonTree.Visible = false;
                this.xtraTabPageList.PageVisible = false;
                //this.xtraTabPageTree.PageVisible = false;
                this.xtraTabPageOtherRes.PageVisible = false;
                this.xtraTabControlObj.SelectedTabPage = lblR;
            }

            //初始化子系统
            EI.EIInfo inblk = new EI.EIInfo();
            EI.EIInfo outblk = /*ES.ESCommon.appInfo;*/ EI.EITuxedo.CallService("epesappinfo", inblk);
            
			string appname="";
            
            for (int i = 1; i <= outblk.blk_info[0].row; i++)
            {
                appname = outblk.GetColVal(1, i, "ename");
                comboApp.Properties.Items.Add(appname);
            }

            comboApp.SelectedIndex = 0;
            selectedAppname = comboApp.SelectedItem.ToString();

            //初始化帐套
            //if (EC.ProjectConfig.Instance.CurrentProject.HASCOMPANY)
            //{
                EI.EIInfo compInfo = EI.EITuxedo.CallService("epescompanyinfo", new EI.EIInfo());
                string companyCode = "";
                string companyName = "";
                for (int i = 1; i <= compInfo.blk_info[0].Row; i++)
                {
                    companyCode = compInfo.GetColVal(i, "companycode");
                    companyName = compInfo.GetColVal(i, "companyname");
                    comboComp.Properties.Items.Add(companyCode + ": " + companyName);
                }

                if (comboComp.Properties.Items.Count > 0)
                {
                    //comboComp.Properties.Items.Add(EP.EPES.EPESC0000086/*ALL: 无限制*/);
                    comboComp.SelectedIndex = 0;
                    selectedCompanyCode = comboComp.SelectedItem.ToString().Split(':')[0];
                }
                if (comboComp.Properties.Items.Count == 1)
                {
                    this.labelControlComp.Visible = false;
                    this.comboComp.Visible = false;
                }
            //}
            //else
            //{
            //    this.labelControlComp.Visible = false;
            //    this.comboComp.Visible = false;
            //}

            fgButtonList.Enabled = false;
            fgButtonTree.Enabled = false;
            fgButtonGroupSave.Enabled = false;
            fgButtonResGroup.Enabled = false;
            fgButtonOthRes.Enabled = false;

            if (para_groupname != null && para_groupname != "" && para_appname != null && para_appname != "")
            {
                fgtGEname.Text = para_groupname;
                if (comboApp.Properties.Items.Contains(para_appname))
                {
                    comboApp.SelectedItem = para_appname;
                    selectedAppname = para_appname;
                    QryGroup();
                }
                else
                {
                    MessageBox.Show(EP.EPES.EPESC0000162/*子系统参数不正确！*/, EP.EPES.EPESC0000024, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            this.fgLabel1.Text = "";
        }

        private void InitOthTypes()
        {
            //初始化资源类型
            EI.EIInfo outBlock = ESOTHER_Get_Resource_ALL();

            //判断调用是否正确
            if (outBlock != null)
            {
                string temp = "";
                for (int i = 1; i <= outBlock.blk_info[0].row; i++)
                {
                    temp = outBlock.GetColVal(i, "code") + "|" + outBlock.GetColVal(i, "code_desc_1_content");
                    comboOthResType.Properties.Items.Add(temp);
                }

                comboOthResType.SelectedIndex = 0;
            }
        }

        private void xtraTabPageUser_Resize(object sender, EventArgs e)
        {
            treeListUser.Height = xtraTabPageUser.Height - 32;
        }

        private void xtraTabPageTree_Resize(object sender, EventArgs e)
        {
            treeListRes.Height = xtraTabPageTree.Height - 32;
        }
        #endregion

        #region 查询群组/用户
        private void QryGroup_Click(object sender, EventArgs e)
        {
            QryGroup();
        }
        private void QryGroup()
        {
            //this.EFMsgInfo = "";
            this.treeListGroup.Nodes.Clear();

            EI.EIInfo inBlock = new EI.EIInfo();
            EI.EIInfo outBlock;

            inBlock.SetColName(1, "groupname");
            inBlock.SetColVal(1, "groupname", fgtGEname.Text);
            inBlock.SetColName(2, "adminuser");
            inBlock.SetColVal(1, "adminuser", fgtGAdmin.Text);
            inBlock.SetColName(3, "userid");
            inBlock.SetColVal(1, "userid", this.EFUserId);
            inBlock.SetColName(4, "appname");
            inBlock.SetColVal(1, "appname", this.selectedAppname);
            inBlock.SetColName(5, "companycode");
            string comp = EPESCommon.AuthMode == AUTHMODE.MODE_9672 ? "" : this.selectedCompanyCode;
            inBlock.SetColVal(1, "companycode", comp);
            inBlock.SetColName(6, "grouptype");
            inBlock.SetColVal(1, "grouptype", 0);

            outBlock = EI.EITuxedo.CallService("epesgroup_inq2", inBlock);

            treeListGroup.DataSource = outBlock.Tables[0];
        }

        private void QryUser_Click(object sender, EventArgs e)
        {
            page_index = 1;
            this.fgButtUp.Enabled = false;
            this.fgButtDown.Enabled = false;
            this.treeListUser.Nodes.Clear();

            //this.EFMsgInfo = "";

            EI.EIInfo outblk = QryUser();

            if (Convert.ToInt32(outblk.blk_info[1].colvalue[0, 0]) > pagecount)
            {
                this.lblPageInfo.Visible = true;
                this.fgButtUp.Visible = true;
                this.fgButtDown.Visible = true;

                int totalcount = Convert.ToInt32(outblk.blk_info[1].colvalue[0, 0]);
                total_page = totalcount / pagecount;
                if (totalcount % pagecount != 0 || totalcount == 0) total_page++;
                this.fgButtDown.Enabled = true;

                this.lblPageInfo.Text = string.Format(EP.EPES.EPESC0000112/*第{0}页/共{1}页*/, page_index.ToString(), total_page.ToString());
            }
            else
            {
                this.lblPageInfo.Visible = false;
                this.fgButtUp.Visible = false;
                this.fgButtDown.Visible = false;
            }
        }

        private void fgButtUp_Click(object sender, EventArgs e)
        {
            page_index--;
            this.fgButtDown.Enabled = true;
            this.treeListUser.Nodes.Clear();

            EI.EIInfo outblk = QryUser();

            if (page_index == 1)
                this.fgButtUp.Enabled = false;
        }

        private void fgButtDown_Click(object sender, EventArgs e)
        {
            this.fgButtUp.Enabled = true;
            page_index++;
            this.treeListUser.Nodes.Clear();

            EI.EIInfo outblk = QryUser();

            if (page_index < total_page)
                this.fgButtDown.Enabled = true;
            else if (page_index == total_page)
                this.fgButtDown.Enabled = false;
        }

        private EI.EIInfo QryUser()
        {
            EI.EIInfo inblk = new EI.EIInfo();
            EI.EIInfo outblk;

            inblk.SetColName(1, "ename");
            inblk.SetColName(2, "cname");
            inblk.SetColName(3, "userid");
            inblk.SetColName(4, "dept_ename");
            inblk.SetColName(5, "q_appname");
            inblk.SetColName(6, "page_index");

            inblk.SetColVal(1, "cname", fgtUCname.Text);
            inblk.SetColVal(1, "ename", fgtUEname.Text);

            inblk.SetColVal(1, "userid", this.EFUserId);
            inblk.SetColVal(1, "dept_ename", "ALL");
            inblk.SetColVal(1, "q_appname", selectedAppname);
            inblk.SetColVal(1, "page_index", page_index);
            outblk = EI.EITuxedo.CallService("epesuser_inq2", inblk);

            outblk.Tables[0].Columns.Add("adminuserename1");
            outblk.Tables[0].Columns.Add("adminuserename2");
            for (int i = 0; i < outblk.Tables[0].Columns.Count; i++)
            {
                outblk.Tables[0].Columns[i].AllowDBNull = true;
                outblk.Tables[0].Columns[i].ColumnName = outblk.Tables[0].Columns[i].ColumnName.ToLower();
            }
            treeListUser.DataSource = outblk.Tables[0];

            return outblk;
        }
        #endregion

        #region 双击节点查询子组用户
        private void treeListGroup_DoubleClick(object sender, EventArgs e)
        {
            TreeListNode node = treeListGroup.FocusedNode;
            if (node == null || node.ImageIndex == GROUP_USER) return;

            if (subjType == SUBJTYPE.NOAUTHGROUP)
            {
                EFMsgInfo = EP.EPES.EPESC0000087/*您没有该群组的查询权限！*/;
                return;
            }

            node.Nodes.Clear();

            treeListGroup.ClearSorting();

            EI.EIInfo inBlock = new EI.EIInfo();
            EI.EIInfo outBlock;

            int mode = 2;

            if (formEname != string.Empty)
            {
                if (this.objType == OBJTYPE.BUTTON) 
                    mode = 1;
                else if (this.objType == OBJTYPE.FORM) 
                    mode = 0;
            }

            inBlock.SetColName(1, "groupname");
            inBlock.SetColName(2, "userid");
            inBlock.SetColName(3, "formname");
            inBlock.SetColName(4, "buttname");
            inBlock.SetColName(5, "mode");
            inBlock.SetColName(6, "appname");
            inBlock.SetColName(7, "companycode");
            inBlock.SetColName(8, "loginuser");
            inBlock.SetColName(9, "deptename");

            inBlock.SetColVal(1, 1, node.GetDisplayText(0));
            inBlock.SetColVal(1, 2, this.EFUserId);
           
            //资源到群组，带权限查询
            if (fgDevCheckEdit2.Checked)
            {
                inBlock.SetColVal(1, "formname", this.formEname);
                inBlock.SetColVal(1, "buttname", this.buttEname);
            }
            else
            {
                inBlock.SetColVal(1, "formname", "");
                inBlock.SetColVal(1, "buttname", "");
            }

            inBlock.SetColVal(1, "mode", mode);//FROM == 0, BUTTON == 1, other == 2
            inBlock.SetColVal(1, "appname", this.selectedAppname);
            string comp = (EPESCommon.AuthMode == AUTHMODE.MODE_9672) ? "" : this.selectedCompanyCode;
            inBlock.SetColVal(1, "companycode", comp);
            inBlock.SetColVal(1, "loginuser", this.EFUserId);
            inBlock.SetColVal(1, "deptename", "ALL");

            //调用SERVICE
            outBlock = EI.EITuxedo.CallService("epessubmem_inq", inBlock);

            string groupName = "", groupDesc = "", admin1 = "", admin2 = "", groupid = "";
            string isadmin = outBlock.GetColVal(4, 1, "isadmin");
            int i = 0;
            for (i = 0; i < outBlock.blk_info[0].Row; i++)
            {
                groupName = outBlock.GetColVal(1, i + 1, "name");
                groupDesc = outBlock.GetColVal(1, i + 1, "groupdescription");
                admin1 = outBlock.GetColVal(1, i + 1, "adminuserename1");
                admin2 = outBlock.GetColVal(1, i + 1, "adminuserename2");
                groupid = outBlock.GetColVal(1, i + 1, "id");

                TreeListNode treeNode;
                if (outBlock.blk_info[1].colvalue[i, 0] == "0")
                {
                    treeNode = this.treeListGroup.AppendNode(new object[5], node, CheckState.Unchecked);
                }
                else if (outBlock.blk_info[1].colvalue[i, 0] == "cnt")
                {
                    treeNode = this.treeListGroup.AppendNode(new object[5], node, CheckState.Indeterminate);
                }
                else
                {
                    treeNode = this.treeListGroup.AppendNode(new object[5], node, CheckState.Checked);
                }
                node.Nodes[i].SetValue(0, groupName);
                node.Nodes[i].SetValue(1, groupDesc);
                node.Nodes[i].SetValue(2, admin1);
                node.Nodes[i].SetValue(3, admin2);
                node.Nodes[i].SetValue(4, groupid);

                treeNode.Tag = groupName;
                if (admin1 == this.EFUserId || admin2 == this.EFUserId || isadmin == "1")
                {
                    treeNode.ImageIndex = treeNode.SelectImageIndex = GROUP_ICON;
                }
                else
                {
                    treeNode.ImageIndex = treeNode.SelectImageIndex = GROUP_GRAY;
                }
            }

            string ename = "", cname = "", userid = "";
            for (int j = 0; j < outBlock.blk_info[2].Row; j++)
            {
                ename = outBlock.GetColVal(3, j + 1, "ename");
                cname = outBlock.GetColVal(3, j + 1, "cname");
                userid = outBlock.GetColVal(3, j + 1, "id");

                TreeListNode treeNode = this.treeListGroup.AppendNode(new object[5], node, CheckState.Indeterminate);

                node.Nodes[i+j].SetValue(0, ename);
                node.Nodes[i+j].SetValue(1, cname);
                node.Nodes[i + j].SetValue(4, userid);
                treeNode.Tag = ename;
                treeNode.SelectImageIndex = treeNode.ImageIndex = GROUP_USER;
            }

            treeListGroup.FocusedNode.ExpandAll();
        }

        #endregion

        #region 查询子树权限信息
        private void QrySubAuthTree(TreeListNode node)
        {
            node.Nodes.Clear();
            if (subjEname == string.Empty) return;

            EI.EIInfo inBlock = new EI.EIInfo();
            EI.EIInfo outBlock ;

            int mode = -1;

            //user mode
            if (subjType == SUBJTYPE.USER) mode = 2;
            //group nested mode
            else if (subjType == SUBJTYPE.GROUP && EPESCommon.AuthMode == AUTHMODE.MODE_9672) mode = 0;
            //group mode
            else mode = 1;

            inBlock.SetColName(1, "subjename");
            inBlock.SetColName(2, "fname");
            inBlock.SetColName(3, "mode");
            inBlock.SetColName(4, "appname");
            inBlock.SetColName(5, "companycode");

            inBlock.SetColVal(1, "subjename", subjEname);
            inBlock.SetColVal(1, "fname", node.Tag.ToString());
            inBlock.SetColVal(1, "mode", mode);
 
            inBlock.SetColVal(1, "appname", this.selectedAppname);
            inBlock.SetColVal(1, "companycode", this.selectedCompanyCode);

            outBlock = EI.EITuxedo.CallService("epessubtree_inq", inBlock);

            for (int i = 0; i < outBlock.Tables[0].Rows.Count; i++)
            {
                string name = outBlock.Tables[0].Rows[i]["NAME"].ToString();
                string resname = outBlock.Tables[0].Rows[i]["RESNAME"].ToString();
                string description = outBlock.Tables[0].Rows[i]["DESCRIPTION"].ToString();
                string cnt = outBlock.Tables[0].Rows[i]["CNT"].ToString();

                if (name == "MYFAVORITE") continue;

                if (resname == "FOLDER")
                {
                    TreeListNode tnode = this.treeListRes.AppendNode(new object[] { description + "(" + name + ")" }, node, CheckState.Indeterminate);
                    tnode.Tag = name;
                    tnode.SelectImageIndex = tnode.ImageIndex = FOLDERICON;
                }
                else
                {
                    if (cnt == "0")
                    {
                        TreeListNode tnode = this.treeListRes.AppendNode(new object[] { description + "(" + resname + ")" }, node, CheckState.Unchecked);
                        tnode.Tag = resname;
                        tnode.SelectImageIndex = tnode.ImageIndex = FORMICON;
                    }
                    else
                    {
                        TreeListNode tnode = this.treeListRes.AppendNode(new object[] { description + "(" + resname + ")" }, node, CheckState.Checked);
                        tnode.Tag = resname;
                        tnode.SelectImageIndex = tnode.ImageIndex = FORMICON;
                    }
                }
            }
        }

        private EI.EIInfo CallSelectService(string name, string fname, long treeno, long mode, string cursystem)
        {
            EI.EIInfo inBlock = new EI.EIInfo();
            EI.EIInfo outBlock = null;

            inBlock.SetColName(1, "name");
            inBlock.SetColName(2, "fname");
            inBlock.SetColName(3, "treeno");
            inBlock.SetColName(4, "mode");
            inBlock.SetColName(5, "cursystem");

            inBlock.SetColVal(1, "name", name);
            inBlock.SetColVal(1, "fname", fname);
            inBlock.SetColVal(1, "treeno", treeno);
            inBlock.SetColVal(1, "mode", mode);
            inBlock.SetColVal(1, "cursystem", cursystem);

            outBlock = EI.EITuxedo.CallService("epestree_inqb", inBlock);//根据mode不同，查询条件不同
            return outBlock;
        }
        #endregion

        #region 初始化菜单树
        private void LoadTree()
        {
            this.treeListRes.Nodes.Clear();

            TreeListNode treeNode = this.treeListRes.AppendNode(new object[] { EP.EPES.EPESC0000067/*主菜单*/ }, null, CheckState.Indeterminate);
            treeNode.Tag = "root";
            treeNode.SelectImageIndex = treeNode.ImageIndex = FOLDERICON;
            EI.EIInfo outBlock = this.CallSelectService(" ", "root", 0, 2, this.selectedAppname);//查询出父名为root的记录

            //作为root的子节点
            for (int i = 1; i <= outBlock.blk_info[0].Row; i++)
            {
                string name = outBlock.GetColVal(i, "name");
                string resname = outBlock.GetColVal(i, "resname");
                string description = outBlock.GetColVal(i, "description");

                if (name == "MYFAVORITE") continue;

                TreeListNode node = this.treeListRes.AppendNode(new object[] { description + "(" + name + ")" }, treeNode, CheckState.Indeterminate);

                if (resname == "FOLDER")
                {
                    node.Tag = name;
                    node.SelectImageIndex = node.ImageIndex = FOLDERICON;
                }
                else
                {
                    node.Tag = resname;
                    node.SelectImageIndex = node.ImageIndex = FORMICON;
                }
            }
            this.treeListRes.ExpandAll();
        }

        #endregion

        #region 切换子系统/帐套
        private void comboApp_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.selectedAppname = comboApp.SelectedItem.ToString();
            ClearAll();
        }

        private void comboComp_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.selectedCompanyCode = comboComp.SelectedItem.ToString().Split(':')[0];

            //if (selectedCompanyCode == "ALL")
            //{
            //    selectedCompanyCode = "";
            //}
            ClearAll();
        }

        private void ClearAll()
        {
            //清空界面
            this.treeListGroup.DataSource = null;
            this.treeListGroup.Nodes.Clear();
            this.treeListUser.DataSource = null;
            this.treeListUser.Nodes.Clear();
            this.treeListRes.Nodes.Clear();
            this.treeListForm.Nodes.Clear();
            this.treeListResGroup.DataSource = null;
            this.treeListResGroup.Nodes.Clear();
            this.fgLabel1.Text = "";
            //this.EFMsgInfo = "";

            //清空全局变量
            this.formEname = "";
            this.buttEname = "";
            this.subjEname = "";

            //重置按钮
            this.fgButtonGroupSave.Enabled = false;
            this.fgButtonTree.Enabled = false;
            this.fgButtonOthRes.Enabled = false;
            if (fgDevCheckEdit1.Checked)
            {
                this.fgButtonList.Enabled = false;
            }

            //初始化菜单树
            LoadTree();
        }
        #endregion

        #region 切换群组资源/资源群组模式
        private void fgDevCheckEdit2_CheckedChanged(object sender, EventArgs e)
        {
            ClearAll(); 

            this.xtraTabPageUser.PageVisible = fgDevCheckEdit2.Checked ? false : true;
            this.fgButtonQryGroup.Visible = fgDevCheckEdit2.Checked ? false : true;
            this.fgButtonRfgreshRG.Visible = fgDevCheckEdit2.Checked ? false : true;

            this.treeListGroup.OptionsView.ShowCheckBoxes = fgDevCheckEdit2.Checked? true : false;
            this.treeListRes.OptionsView.ShowCheckBoxes = fgDevCheckEdit2.Checked ? false : true;
            this.treeListForm.OptionsView.ShowCheckBoxes = fgDevCheckEdit2.Checked ? false : true;
            this.treeListResGroup.OptionsView.ShowCheckBoxes = fgDevCheckEdit2.Checked ? false : true;

            //画面列表的按钮
            fgButtonList.Text = fgDevCheckEdit2.Checked ? EP.EPES.EPESC0000088/*查询*/ : EP.EPES.EPESC0000079/*保存*/;
            fgButtonList.Enabled = fgDevCheckEdit2.Checked ? true : false;

            //资源组的按钮
            fgButtonResGroup.Text = fgDevCheckEdit2.Checked ? EP.EPES.EPESC0000088/*查询*/ : EP.EPES.EPESC0000079/*保存*/;
            fgButtonResGroup.Enabled = fgDevCheckEdit2.Checked ? true : false;
            
            //菜单树上的按钮
            fgButtonTree.Visible = fgDevCheckEdit2.Checked ? false : true;
            fgButtonTree.Enabled = false;

            //群组列表的按钮
            fgButtonGroupSave.Visible = fgDevCheckEdit2.Checked ? true : false;
            fgButtonGroupSave.Enabled = false;

            //嵌套勾选
            //this.labelControlCheck.Visible = fgDevCheckEdit2.Checked ? false : true;
            //this.checkEditNested.Visible = fgDevCheckEdit2.Checked ? false : true;

            //刷新按钮
            fgButtonRfg.Visible = fgDevCheckEdit2.Checked ? false : true;

            //左右换位
            this.xtraTabControlObj.Parent = fgDevCheckEdit2.Checked ? splitContainerControl1.Panel1 : splitContainerControl1.Panel2;
            this.xtraTabControlObj.Dock = DockStyle.Fill;
            this.xtraTabControlSubj.Parent = fgDevCheckEdit2.Checked ? splitContainerControl1.Panel2 : splitContainerControl1.Panel1;
            this.xtraTabControlSubj.Dock = DockStyle.Fill;
            this.splitContainerControl1.SplitterPosition = fgDevCheckEdit2.Checked ? 400 : 700;

            if (EPESCommon.AuthMode == AUTHMODE.MODE_CLASSIC)
            {
                LoadTree();
            }
            else
            {
                this.xtraTabPageTree.PageVisible = fgDevCheckEdit2.Checked? false:true;
            }

            if (EPESCommon.AuthMode == AUTHMODE.MODE_CLASSIC)
            {
                this.xtraTabPageOtherRes.PageVisible = fgDevCheckEdit2.Checked ? false : true;
            }
        }
        #endregion

        #region 切换群组/用户Tab页
        private void xtraTabControl2_SelectedPageChanging(object sender, DevExpress.XtraTab.TabPageChangingEventArgs e)
        {
            if (fgButtonTree.Enabled)
            {
                DialogResult rst = MessageBox.Show(EP.EPES.EPESC0000089/*已修改群组资源权限，是否保存？*/, EP.EPES.EPESC0000024, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (rst == DialogResult.Yes)
                {
                    SaveAuth();
                    fgButtonTree.Enabled = false;
                }
                else
                {
                    if (treeListForm.Nodes.Count == 0)
                    {
                        QryAuthForm();
                    }
                    else
                    {
                        RfgreshFormList();
                    }
                    fgButtonTree.Enabled = false;
                }
            }
            else if (fgButtonList.Enabled)
            {
                DialogResult rst = MessageBox.Show(EP.EPES.EPESC0000089/*已修改群组资源权限，是否保存？*/, EP.EPES.EPESC0000024, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (rst == DialogResult.Yes)
                {
                    SaveAuth();
                    fgButtonList.Enabled = false;
                }
                else
                {
                    if (treeListForm.Nodes.Count == 0)
                    {
                        QryAuthForm();
                    }
                    else
                    {
                        RfgreshFormList();
                    }
                    fgButtonList.Enabled = false;
                }
            }
            else if (fgButtonOthRes.Enabled)
            {
                DialogResult rst = MessageBox.Show(EP.EPES.EPESC0000089/*已修改群组资源权限，是否保存？*/, EP.EPES.EPESC0000024, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (rst == DialogResult.Yes)
                {
                    SaveOthResAuth();
                    fgButtonOthRes.Enabled = false;
                }
            }
            else if (fgButtonResGroup.Enabled)
            {
                DialogResult rst = MessageBox.Show(EP.EPES.EPESC0000089/*已修改群组资源权限，是否保存？*/, EP.EPES.EPESC0000024, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (rst == DialogResult.Yes)
                {
                    SaveGroupToResGroup();
                }
                else
                {
                    QryParentResGroup();
                    fgButtonResGroup.Enabled = false;
                }
            }

            this.fgLabel1.Text = "";

            if (xtraTabControlSubj.SelectedTabPage == xtraTabPageUser) //用户
            {
                subjType = SUBJTYPE.USER;
                subjEname = "";
            }
            else    //群组
            {
                subjType = SUBJTYPE.GROUP;
                subjEname = "";
            }

            this.treeListRes.Nodes.Clear();
            this.treeListForm.Nodes.Clear();
            this.treeListOthRes.Nodes.Clear();
        }

        private void xtraTabControlSubj_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (e.Page == xtraTabPageGroup)
            {                
                if (treeListGroup.FocusedNode != null)
                {
                    switch (treeListGroup.FocusedNode.SelectImageIndex)
                    { 
                        case GROUP_ICON:
                            subjType = SUBJTYPE.GROUP;
                            break;
                        case GROUP_GRAY:
                            subjType = SUBJTYPE.NOAUTHGROUP;
                            break;
                        case GROUP_USER:
                            subjType = SUBJTYPE.USER;
                            break;
                    }
                    subjEname = treeListGroup.FocusedNode.GetDisplayText(treeListColumn2);
                }
            }
            else if (e.Page == xtraTabPageUser)
            {                
                if (treeListUser.FocusedNode != null)
                {
                    switch (treeListUser.FocusedNode.SelectImageIndex)
                    { 
                        case USERICON:
                            subjType = SUBJTYPE.USER;
                            break;
                        case GROUPICON:
                            subjType = SUBJTYPE.GROUP;
                            break;
                        case GROUPICON2:
                            subjType = SUBJTYPE.NOAUTHGROUP;
                            break;
                    }
                    subjEname = treeListUser.FocusedNode.GetDisplayText(0);
                }
            }

            if (xtraTabControlObj.SelectedTabPage == lblR)
            {
                if (subjEname != string.Empty)
                {
                    QryParentResGroup();
                }
                else
                {
                    foreach (TreeListNode node in treeListResGroup.Nodes)
                    {
                        node.CheckState = CheckState.Unchecked;
                    }
                }
            }
        }
        #endregion

        #region 切换菜单树/画面列表/细部资源
        private void xtraTabControl1_SelectedPageChanging(object sender, DevExpress.XtraTab.TabPageChangingEventArgs e)
        {
            if (fgDevCheckEdit1.Checked)
            {
                if (e.PrevPage == xtraTabPageList)
                {
                    if (fgButtonList.Enabled)
                    {
                        DialogResult rst = MessageBox.Show(EP.EPES.EPESC0000089/*已修改群组资源权限，是否保存？*/, EP.EPES.EPESC0000024, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (rst == DialogResult.Yes)
                        {
                            SaveAuth();
                            fgButtonList.Enabled = false;
                        }
                        else
                        {
                            fgButtonList.Enabled = false;

                            if (treeListForm.Nodes.Count == 0)
                            {
                                QryAuthForm();
                            }
                            else
                            {
                                RfgreshFormList();
                            }
                        }
                    }
                    if (e.Page == xtraTabPageTree)
                    {
                        if (treeListRes.Nodes.Count == 0)
                        {
                            LoadTree();
                        }
                        else
                        {
                            RfgreshTree();
                        }
                    }
                    else if (e.Page == xtraTabPageOtherRes)
                    {
                        QryOthResAuth();
                    }

                }
                else if (e.PrevPage == xtraTabPageTree)
                {
                    if (fgButtonTree.Enabled && EPESCommon.AuthMode == AUTHMODE.MODE_CLASSIC)
                    {
                        DialogResult rst = MessageBox.Show(EP.EPES.EPESC0000089/*已修改群组资源权限，是否保存？*/, EP.EPES.EPESC0000024, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (rst == DialogResult.Yes)
                        {
                            SaveAuth();
                            fgButtonTree.Enabled = false;
                        }
                        else
                        {
                            RfgreshTree();
                            fgButtonTree.Enabled = false;
                        }
                    }

                    if (e.Page == xtraTabPageList)
                    {
                        if (treeListForm.Nodes.Count == 0)
                        {
                            QryAuthForm();
                        }
                        else
                        {
                            RfgreshFormList();
                        }
                    }
                    else if(e.Page == xtraTabPageOtherRes)
                    {
                        QryOthResAuth();
                    }
                }
                else if (e.PrevPage == xtraTabPageOtherRes)
                {
                    if (fgButtonOthRes.Enabled)
                    {
                        DialogResult rst = MessageBox.Show(EP.EPES.EPESC0000089/*已修改群组资源权限，是否保存？*/, EP.EPES.EPESC0000024, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (rst == DialogResult.Yes)
                        {
                            if (!SaveOthResAuth())
                            {
                                e.Cancel = true;
                            }
                            else
                            {
                                fgButtonOthRes.Enabled = false;
                            }    
                        }
                        else
                        {
                            fgButtonOthRes.Enabled = false;
                        }                        
                    }
                }
            }           
            else
            {
                if (e.PrevPage == xtraTabPageList && treeListRes.Nodes.Count == 0 && e.Page != xtraTabPageOtherRes)
                {
                    LoadTree();
                }
            }
        }
        #endregion

        #region 单击群组列表
        private void treeListGroup_MouseDown(object sender, MouseEventArgs e)
        {
            hiGroup = treeListGroup.CalcHitInfo(new Point(e.X, e.Y));
            if (hiGroup.Node != null)
            {
                treeListGroup.FocusedNode = hiGroup.Node;
            }
        }

        private void treeListGroup_BfgoreFocusNode(object sender, DevExpress.XtraTreeList.BfgoreFocusNodeEventArgs e)
        {
            if (treeListGroup.FocusedNode == null) return;
            string ename = treeListGroup.FocusedNode.GetDisplayText(treeListColumn2);

            switch (treeListGroup.FocusedNode.ImageIndex)
            {
                case 0:
                    subjType = SUBJTYPE.GROUP;
                    break;
                case 1:
                    subjType = SUBJTYPE.NOAUTHGROUP;
                    break;
                case 2:
                    subjType = SUBJTYPE.USER;
                    break;
            }

            subjEname = ename;

            if (fgDevCheckEdit1.Checked)
            {
                //this.EFMsgInfo = "";
                if ((xtraTabControlObj.SelectedTabPage == xtraTabPageList && fgButtonList.Enabled)
                    || (xtraTabControlObj.SelectedTabPage == xtraTabPageTree && fgButtonTree.Enabled))
                {
                    DialogResult rst = MessageBox.Show(EP.EPES.EPESC0000089/*已修改群组资源权限，是否保存？*/, EP.EPES.EPESC0000024, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (rst == DialogResult.Yes)
                    {
                        SaveAuth();
                    }
                }
                else if (xtraTabControlObj.SelectedTabPage == xtraTabPageOtherRes && fgButtonOthRes.Enabled)
                {
                    DialogResult rst = MessageBox.Show(EP.EPES.EPESC0000089/*已修改群组资源权限，是否保存？*/, EP.EPES.EPESC0000024, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (rst == DialogResult.Yes)
                    {
                        SaveOthResAuth();
                    }
                }
                else if (xtraTabControlObj.SelectedTabPage == lblR && fgButtonResGroup.Enabled)
                {
                    DialogResult rst = MessageBox.Show(EP.EPES.EPESC0000089/*已修改群组资源权限，是否保存？*/, EP.EPES.EPESC0000024, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (rst == DialogResult.Yes)
                    {
                        SaveGroupToResGroup();
                    }
                }

                listFormGrant.Clear();
                listFormRevok.Clear();
                listButtGrant.Clear();
                listButtRevok.Clear();
                listOthResGrant.Clear();
                listOthResRevok.Clear();
                htGroupResAdd.Clear();
                htGroupResRmv.Clear();
                htResGroupAdd.Clear();
                htResGroupRmv.Clear();
            }
        }

        private void treeListGroup_AfterFocusNode(object sender, DevExpress.XtraTreeList.NodeEventArgs e)
        {
            if (treeListGroup.FocusedNode == null) return;

            if (fgDevCheckEdit2.Checked) return;

            string ename = treeListGroup.FocusedNode.GetDisplayText(treeListColumn2);

            switch (treeListGroup.FocusedNode.ImageIndex)
            {
                case 0:
                    subjType = SUBJTYPE.GROUP;
                    break;
                case 1:
                    subjType = SUBJTYPE.NOAUTHGROUP;
                    break;
                case 2:
                    subjType = SUBJTYPE.USER;
                    break;
            }

            subjEname = ename;

            if (subjType == SUBJTYPE.NOAUTHGROUP)
            {
                treeListForm.Nodes.Clear();
                LoadTree();
                EFMsgInfo = EP.EPES.EPESC0000087/*您没有该群组的查询权限！*/;
                return;
            }

            if (xtraTabControlObj.SelectedTabPage == xtraTabPageTree) //formtree
            {
                if (treeListRes.Nodes.Count == 0)
                {
                    LoadTree();
                }
                else
                {
                    RfgreshTree();
                }
                fgButtonTree.Enabled = false;
            }
            else if (xtraTabControlObj.SelectedTabPage == xtraTabPageList) //formlist
            {
                if (treeListForm.Nodes.Count == 0)
                {
                    QryAuthForm();
                }
                else
                {
                    RfgreshFormList();
                }
                if (fgDevCheckEdit1.Checked)
                {
                    fgButtonList.Enabled = false;
                }
            }
            else if(xtraTabControlObj.SelectedTabPage == xtraTabPageOtherRes) //细部资源
            {
                treeListOthRes.Nodes.Clear();
                QryOthResAuth();
                fgButtonOthRes.Enabled = false;
            }
            else if (xtraTabControlObj.SelectedTabPage == lblR) //资源组
            {
                htResGroupAdd.Clear();
                htGroupResAdd.Clear();
                htGroupResRmv.Clear();
                htResGroupRmv.Clear();
                QryParentResGroup();
                fgButtonResGroup.Enabled = false;
            }
        }

        //Hashtable htResGroup = new Hashtable();
        private void QryParentResGroup()
        {
            EI.EIInfo inblk = new EI.EIInfo();
            inblk.AddColName(1, "id");
            inblk.AddColName(1, "groupname");
            inblk.AddColName(1, "appname");
            inblk.AddColName(1, "companycode");
            inblk.AddColName(1, "mode");
            inblk.AddColName(1, "inodes");

            string groupid = GetSubjID();
            if(groupid == string.Empty) return;

            inblk.SetColVal(1, 1, "id", groupid);
            inblk.SetColVal(1, 1, "appname", this.selectedAppname);
            inblk.SetColVal(1, 1, "groupname", fgtRGName.Text);
            inblk.SetColVal(1, 1, "companycode", comboComp.SelectedItem.ToString().Split(':')[0]);
            inblk.SetColVal(1, 1, "inodes", treeListResGroup.Nodes.Count);

            int mode = -1;
            mode = (subjType == SUBJTYPE.USER) ? 2 : 1;

            inblk.SetColVal(1, 1, "mode", mode);
            EI.Logger.Info("begin call service @ "+DateTime.Now.ToString("HH:mm:ss.fff"));
            EI.EIInfo outblk = EI.EITuxedo.CallService("epesgrgr_inq", inblk);
            EI.Logger.Info("call service end @ " + DateTime.Now.ToString("HH:mm:ss.fff"));

            if (outblk.sys_info.flag == 0)
            {
                Hashtable ht = new Hashtable();
                //htResGroup.Clear();
                //treeListResGroup.TopVisibleNodeIndexChanged -= new EventHandler(treeListResGroup_TopVisibleNodeIndexChanged);
                //treeListResGroup.SizeChanged -= new EventHandler(treeListResGroup_TopVisibleNodeIndexChanged);
                if (treeListResGroup.Nodes.Count == 0)
                {
                    treeListResGroup.DataSource = outblk.Tables[0];
                }

                EI.Logger.Info("binding data end @ " + DateTime.Now.ToString("HH:mm:ss.fff"));

                for (int i = 0; i < outblk.Tables[1].Rows.Count; i++)
                {
                    ht.Add(outblk.Tables[1].Rows[i]["ID"].ToString(), outblk.Tables[1].Rows[i]["NAME"].ToString());
                }

                EI.Logger.Info("begin to foreach @ " + DateTime.Now.ToString("HH:mm:ss.fff"));

                foreach (TreeListNode node in treeListResGroup.Nodes)
                {
                    if (node.Level == 0)
                    {
                        node.Checked = ht.ContainsKey(node.GetValue(treeListColumnRGID)) ? true : false;
                    }
                }


                //for (int i = treeListResGroup.;
                //    i < GetNodeNum();
                //    i++)
                //{
                //    treeListResGroup.Nodes[i].Checked = htResGroup.ContainsKey(treeListResGroup.Nodes[i].GetValue(treeListColumnRGID)) ? true : false;
                //}
                //treeListResGroup.TopVisibleNodeIndexChanged += new EventHandler(treeListResGroup_TopVisibleNodeIndexChanged);
                //treeListResGroup.SizeChanged += new EventHandler(treeListResGroup_TopVisibleNodeIndexChanged);

                EI.Logger.Info("end @ " + DateTime.Now.ToString("HH:mm:ss.fff"));
                //this.EFMsgInfo = EP.EPES.EPESC0000156/*操作成功！*/;
            }
            else
            {
                MessageBox.Show(outblk.sys_info.msg, EP.EPES.EPESC0000024/*提示*/, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        int GetNodeNum()
        {
            int topVisibleNodeIndex = treeListResGroup.TopVisibleNodeIndex;
            int totalNodesNum = treeListResGroup.Nodes.Count;
            int visibleNodesNum = treeListResGroup.ViewInfo.CalcVisibleNodeCount(treeListResGroup.Nodes[topVisibleNodeIndex]);
            
            if (totalNodesNum == 0) return 0;
            
            return (topVisibleNodeIndex + visibleNodesNum > totalNodesNum) ? totalNodesNum:topVisibleNodeIndex + visibleNodesNum +1;
        
        }

        //void treeListResGroup_TopVisibleNodeIndexChanged(object sender, EventArgs e)
        //{
        //    object resGroupID = null;

        //    for (int i = treeListResGroup.TopVisibleNodeIndex;
        //        i < GetNodeNum();
        //        i++)
        //    {
        //        resGroupID = treeListResGroup.Nodes[i].GetValue(treeListColumnRGID);
        //        if (htGroupResAdd.Contains(resGroupID) || htGroupResRmv.Contains(resGroupID))
        //            continue;

        //        treeListResGroup.Nodes[i].Checked = htResGroup.ContainsKey(resGroupID) ? true : false;
        //    }
        //}

        private string GetSubjID()
        {
            string id = "";

            if (xtraTabControlSubj.SelectedTabPage == xtraTabPageGroup)
            {
                if (treeListGroup.FocusedNode != null)
                {
                    id = treeListGroup.FocusedNode.GetDisplayText(treeListColumnGID);
                }
            }
            else
            {
                if (treeListUser.FocusedNode != null)
                {
                    id = treeListUser.FocusedNode.GetDisplayText(treeListColumnID);
                }
            }
            return id;
        }
        #endregion

        #region 单击树菜单节点
        private void treeListRes_Click(object sender, EventArgs e)
        {
            if (hiTreeRes == null || hiTreeRes.Column == null || hiTreeRes.Node == null) return;
            
            TreeListNode node = treeListRes.FocusedNode;
            if (node == null || node.Tag == null) return;

            if (fgDevCheckEdit2.Checked)
            {
                //this.EFMsgInfo = "";

                if (fgButtonGroupSave.Enabled)
                {
                    DialogResult rst = MessageBox.Show(EP.EPES.EPESC0000089/*已修改群组资源权限，是否保存？*/, EP.EPES.EPESC0000024, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (rst == DialogResult.Yes)
                    {
                        SaveResToGroup();
                    }
                }
                switch (node.ImageIndex)
                {
                    case 1:
                        QryFormGroup(node.Tag.ToString());
                        treeListGroup.FocusedNode = null;
                        treeListForm.FocusedNode = null;
                        objType = OBJTYPE.FORM;
                        formEname = node.Tag.ToString();
                        buttEname = "";
                        break;
                    case 2:
                        QryButtGroup(node.ParentNode.Tag.ToString(), node.Tag.ToString());
                        treeListGroup.FocusedNode = null;
                        treeListForm.FocusedNode = null;
                        objType = OBJTYPE.BUTTON;
                        formEname = node.ParentNode.Tag.ToString();
                        buttEname = node.Tag.ToString();
                        break;
                }

                listGroupGrant.Clear();
                listGroupRevok.Clear();
                fgButtonGroupSave.Enabled = false;
            }
        }

        private void treeListRes_MouseDown(object sender, MouseEventArgs e)
        {
            hiTreeRes = treeListRes.CalcHitInfo(new Point(e.X, e.Y));
            if (hiTreeRes.Node != null)
            {
                treeListRes.FocusedNode = hiTreeRes.Node;
            }
        }

        private void treeListRes_AfterCollapse(object sender, DevExpress.XtraTreeList.NodeEventArgs e)
        {
            if (e.Node.ImageIndex == FOLDERICON_EXPAND)
            {
                e.Node.ImageIndex = e.Node.SelectImageIndex = FOLDERICON;
            }
        }

        private void treeListRes_AfterExpand(object sender, DevExpress.XtraTreeList.NodeEventArgs e)
        {
            if (e.Node.ImageIndex == FOLDERICON)
            {
                e.Node.ImageIndex = e.Node.SelectImageIndex = FOLDERICON_EXPAND;
            }
        }

        #endregion

        #region 单击画面列表
        private void treeListForm_Click(object sender, EventArgs e)
        {
            if (hiTreeList == null || hiTreeList.Column == null || hiTreeList.Node == null) return;
            
            TreeListNode node = treeListForm.FocusedNode;
            if (node == null || node.Tag == null) return;

            if (fgDevCheckEdit2.Checked)
            {
                //this.EFMsgInfo = "";

                if (fgButtonGroupSave.Enabled)
                {
                    DialogResult rst = MessageBox.Show(EP.EPES.EPESC0000089/*已修改群组资源权限，是否保存？*/, EP.EPES.EPESC0000024, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (rst == DialogResult.Yes)
                    {
                        SaveResToGroup();
                    }
                }

                if (node.Level == 0)
                {
                    QryFormGroup(node.Tag.ToString());
                    treeListRes.FocusedNode = null;
                    objType = OBJTYPE.FORM;
                    formEname = node.Tag.ToString();
                    buttEname = "";
                }
                else if (node.Level == 1)
                {
                    QryButtGroup(node.ParentNode.Tag.ToString(), node.Tag.ToString());
                    treeListRes.FocusedNode = null;
                    objType = OBJTYPE.BUTTON;
                    formEname = node.ParentNode.Tag.ToString();
                    buttEname = node.Tag.ToString();
                }

                listGroupGrant.Clear();
                listGroupRevok.Clear();
                fgButtonGroupSave.Enabled = false;
            }
        }

        private void treeListForm_MouseDown(object sender, MouseEventArgs e)
        {
            hiTreeList = treeListForm.CalcHitInfo(new Point(e.X, e.Y));
            if (hiTreeList.Node != null)
            {
                treeListForm.FocusedNode = hiTreeList.Node;
            }
        }
        #endregion

        #region 单击用户列表
        private void treeListUser_MouseDown(object sender, MouseEventArgs e)
        {
            DevExpress.XtraTreeList.TreeListHitInfo hiUser = treeListUser.CalcHitInfo(new Point(e.X, e.Y));
            if (hiUser.Node != null)
            {
                treeListUser.FocusedNode = hiUser.Node;
            }
        }

        private void treeListUser_BfgoreFocusNode(object sender, DevExpress.XtraTreeList.BfgoreFocusNodeEventArgs e)
        {
            if (treeListUser.Nodes.Count == 0) return;

            if ((xtraTabControlObj.SelectedTabPage == xtraTabPageList && fgButtonList.Enabled)
                || (xtraTabControlObj.SelectedTabPage == xtraTabPageTree && fgButtonTree.Enabled)
                || (xtraTabControlObj.SelectedTabPage == xtraTabPageOtherRes && fgButtonOthRes.Enabled))
            {
                DialogResult rst = MessageBox.Show(EP.EPES.EPESC0000089/*已修改群组资源权限，是否保存？*/, EP.EPES.EPESC0000024, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (rst == DialogResult.Yes)
                {
                    SaveAuth();
                }

                listFormGrant.Clear();
                listFormRevok.Clear();
                listButtGrant.Clear();
                listButtRevok.Clear();
                listOthResGrant.Clear();
                listOthResRevok.Clear();
                htGroupResAdd.Clear();
                htGroupResRmv.Clear();
            }
        }

        private void treeListUser_AfterFocusNode(object sender, DevExpress.XtraTreeList.NodeEventArgs e)
        {
            if (treeListUser.Nodes.Count == 0) return;
            //this.EFMsgInfo = "";

            TreeListNode node = treeListUser.FocusedNode;
            subjType = GetSubjType(treeListUser.FocusedNode);
            subjEname = node.GetDisplayText(0);

            if (subjType == SUBJTYPE.NOAUTHGROUP)
            {
                treeListForm.Nodes.Clear();
                LoadTree();
                EFMsgInfo = EP.EPES.EPESC0000087/*您没有该群组的查询权限！*/;
                return;
            }

            if (xtraTabControlObj.SelectedTabPage == xtraTabPageTree) //formtree
            {
                if (treeListRes.Nodes.Count == 0)
                {
                    LoadTree();
                }
                else
                {
                    RfgreshTree();
                }
                fgButtonTree.Enabled = false;
            }
            else if (xtraTabControlObj.SelectedTabPage == xtraTabPageList)            //formlist
            {
                if (treeListForm.Nodes.Count == 0)
                {
                    QryAuthForm();
                }
                else
                {
                    RfgreshFormList();
                }
                fgButtonList.Enabled = false;
            }
            else if (xtraTabControlObj.SelectedTabPage == xtraTabPageOtherRes) //细部资源
            {
                QryOthResAuth();
                fgButtonOthRes.Enabled = false;
            }
            else if (xtraTabControlObj.SelectedTabPage == lblR)
            {
                QryParentResGroup();
            }
        }

        private SUBJTYPE GetSubjType(TreeListNode node)
        {
            switch (node.ImageIndex)
            {
                case GROUPICON:
                    return SUBJTYPE.GROUP;
                case USERICON:
                    return SUBJTYPE.USER;
                case GROUPICON2:
                    return SUBJTYPE.NOAUTHGROUP;
            }
            return SUBJTYPE.NULL;
        }

        #endregion

        #region 双击用户查询父组
        private void treeListUser_DoubleClick(object sender, EventArgs e)
        {
            TreeListNode node = treeListUser.FocusedNode;
            if (node == null) return;

            node.Nodes.Clear();

            string ename = node.GetDisplayText(0);
            string company = EPESCommon.AuthMode == AUTHMODE.MODE_9672 ? "" : selectedCompanyCode;

            if (node.ImageIndex == USERICON)
            {
                EI.EIInfo inblk = new EI.EIInfo();
                EI.EIInfo outblk = new EI.EIInfo();

                inblk.SetColName(1, "username");
                inblk.SetColName(2, "appname");
                inblk.SetColName(3, "companycode");
                inblk.SetColName(4, "loginuser");

                inblk.SetColVal(1, 1, ename);
                inblk.SetColVal(1, 2, selectedAppname);
                
                inblk.SetColVal(1, "companycode", company);
                inblk.SetColVal(1, "loginuser", this.EFUserId);

                //查询用户所属/所管理的群组
                outblk = EI.EITuxedo.CallService("epesugroup_inq", inblk);

                string groupID = "", groupName = "", groupDesc = "", admin1 = "", admin2 = "";
                string isadmin = outblk.GetColVal(2, 1, "isadmin");

                for (int i = 0; i < outblk.blk_info[0].Row; i++)
                {
                    groupID = outblk.GetColVal(1, i + 1, "id");
                    groupName = outblk.GetColVal(1, i + 1, "name");
                    groupDesc = outblk.GetColVal(1, i + 1, "groupdescription");
                    admin1 = outblk.GetColVal(1, i + 1, "adminuserename1");
                    admin2 = outblk.GetColVal(1, i + 1, "adminuserename2");

                    TreeListNode treeNode;

                    treeNode = this.treeListUser.AppendNode(new object[5], node);
                
                    node.Nodes[i].SetValue(0, groupName);
                    node.Nodes[i].SetValue(1, groupDesc);
                    node.Nodes[i].SetValue(2, admin1);
                    node.Nodes[i].SetValue(3, admin2);
                    node.Nodes[i].SetValue(4, groupID);

                    treeNode.Tag = groupName;

                    if (admin1 == this.EFUserId || admin2 == this.EFUserId || isadmin == "1")
                    {
                        treeNode.ImageIndex = treeNode.SelectImageIndex = GROUPICON;
                    }
                    else
                    {
                        treeNode.ImageIndex = treeNode.SelectImageIndex = GROUPICON2;
                    }
                }
            }
            else if (node.ImageIndex == GROUPICON)
            {
                EI.EIInfo inblk = new EI.EIInfo();
                EI.EIInfo outblk = new EI.EIInfo();

                inblk.SetColName(1, "username");
                inblk.SetColName(2, "mode");
                inblk.SetColName(3, "appname");
                inblk.SetColName(4, "companycode");
                inblk.SetColName(5, "loginuser");

                inblk.SetColVal(1, 1, ename);
                inblk.SetColVal(1, 2, 5);
                inblk.SetColVal(1, 3, selectedAppname);
                inblk.SetColVal(1, "companycode", company);
                inblk.SetColVal(1, "loginuser", this.EFUserId);

                //查询群组的父组
                outblk = EI.EITuxedo.CallService("epesusergrp_inq", inblk);

                string groupID = "", groupName = "", groupDesc = "", admin1 = "", admin2 = "";
                string isadmin = outblk.GetColVal(2, 1, "isadmin");
                for (int i = 0; i < outblk.blk_info[0].Row; i++)
                {
                    groupID = outblk.GetColVal(1, i + 1, "id");
                    groupName = outblk.GetColVal(1, i + 1, "ename");
                    groupDesc = outblk.GetColVal(1, i + 1, "cname");
                    admin1 = outblk.GetColVal(1, i + 1, "adminuserename1");
                    admin2 = outblk.GetColVal(1, i + 1, "adminuserename2");

                    TreeListNode treeNode;

                    treeNode = this.treeListUser.AppendNode(new object[5], node);

                    node.Nodes[i].SetValue(0, groupName);
                    node.Nodes[i].SetValue(1, groupDesc);
                    node.Nodes[i].SetValue(2, admin1);
                    node.Nodes[i].SetValue(3, admin2);
                    node.Nodes[i].SetValue(4, groupID);

                    treeNode.Tag = groupName;

                    if (admin1 == this.EFUserId || admin2 == this.EFUserId || isadmin == "1")
                    {
                        treeNode.ImageIndex = treeNode.SelectImageIndex = GROUPICON;
                    }
                    else
                    {
                        treeNode.ImageIndex = treeNode.SelectImageIndex = GROUPICON2;
                    }
                }
            }
            treeListUser.FocusedNode.ExpandAll();
        }

        #endregion

        #region 双击画面列表
        private void treeListForm_DoubleClick(object sender, EventArgs e)
        {
            TreeListNode node = treeListForm.FocusedNode;
            if (node == null || hiTreeList.HitInfoType == DevExpress.XtraTreeList.HitInfoType.Empty) return;

            node.Nodes.Clear();

            if (fgDevCheckEdit1.Checked)
            {
                QryFormButtInList(node.Tag.ToString());
            }
            else
            {
                QryButtResInList(node.Tag.ToString());
            }

            node.ExpandAll();
        }
        #endregion

        #region 双击菜单树
        private void treeListRes_DoubleClick(object sender, EventArgs e)
        {
            TreeListNode node = treeListRes.FocusedNode;
            if (node == null || hiTreeRes.HitInfoType == DevExpress.XtraTreeList.HitInfoType.Empty) return;

            node.Nodes.Clear();

            if (fgDevCheckEdit1.Checked) //群组到资源
            {
                if (subjType == SUBJTYPE.NOAUTHGROUP)
                {
                    EFMsgInfo = EP.EPES.EPESC0000090/*您没有群组的查询权限！*/;
                    return;
                }

                if (node.ImageIndex == FOLDERICON || node.ImageIndex == FOLDERICON_EXPAND)
                {
                    QrySubAuthTree(node);
                }
                else if (node.ImageIndex == FORMICON)
                {
                    QryFormButtInTree(node.Tag.ToString());
                }
            }
            else //资源到群组
            {
                switch (node.ImageIndex)
                {
                    case FOLDERICON:
                    case FOLDERICON_EXPAND:
                        EI.EIInfo outBlock = CallSelectService("", node.Tag.ToString(), 0, 2, this.selectedAppname);
                        for (int i = 1; i <= outBlock.blk_info[0].Row; i++)
                        {
                            string name = outBlock.GetColVal(i, "name");
                            string resname = outBlock.GetColVal(i, "resname");
                            string description = outBlock.GetColVal(i, "description");

                            if (name == "MYFAVORITE") continue;

                            if (resname == "FOLDER")
                            {
                                TreeListNode newNode = this.treeListRes.AppendNode(new object[] { description + "(" + name + ")" }, node);
                                newNode.Tag = name;
                                newNode.SelectImageIndex = newNode.ImageIndex = FOLDERICON;
                            }
                            else
                            {
                                TreeListNode newNode = this.treeListRes.AppendNode(new object[] { description + "(" + resname + ")" }, node);
                                newNode.Tag = resname;
                                newNode.SelectImageIndex = newNode.ImageIndex = FORMICON;
                            }
                        }
                        break;
                    case FORMICON:
                        QryButtResInTree(node.Tag.ToString());
                        
                        break;
                    case BUTTICON:
                        break;
                }
            }

            node.ExpandAll();
        }
        #endregion

        #region 勾选菜单树节点
        private void treeListRes_BfgoreCheckNode(object sender, DevExpress.XtraTreeList.CheckNodeEventArgs e)
        {
            if (EPESCommon.AuthMode == AUTHMODE.MODE_9672)
            {
                e.CanCheck = false;
                return;
            }
            if (!isManageMode)
            {
                MessageBox.Show(EP.EPES.EPESC0000091/*非维护模式下无法修改权限！*/, EP.EPES.EPESC0000024, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.CanCheck = false;
                return;
            }
            if (subjType == SUBJTYPE.USER)
            {
                EFMsgInfo = EP.EPES.EPESC0000092/*不能对用户授权，请操作用户所属或所管理群组！*/;
                e.CanCheck = false;
            }
            else if(e.Node.ImageIndex == FOLDERICON || e.Node.ImageIndex == FOLDERICON_EXPAND)
            {
                e.CanCheck = false;
            }
            else if (checkEditNested.Checked)
            {
                EFMsgInfo = EP.EPES.EPESC0000093/*嵌套查询模式下不能修改权限！*/;
                e.CanCheck = false;
            }
            else if (e.Node.ImageIndex == BUTTICON && !e.Node.ParentNode.Checked)
            {
                EFMsgInfo = EP.EPES.EPESC0000094/*请先勾选按钮对应的画面！*/;
                e.CanCheck = false;
            }
            else
            {
                e.CanCheck = true;
                e.State = (e.PrevState == CheckState.Checked ? CheckState.Unchecked : CheckState.Checked);
            }
        }

        private void treeListRes_AfterCheckNode(object sender, DevExpress.XtraTreeList.NodeEventArgs e)
        {
            AfterCheckNode(e.Node);
        }
        #endregion

        #region 勾选画面列表节点
        private void treeListForm_BfgoreCheckNode(object sender, DevExpress.XtraTreeList.CheckNodeEventArgs e)
        {
            if (!isManageMode)
            {
                MessageBox.Show(EP.EPES.EPESC0000091/*非维护模式下无法修改权限！*/, EP.EPES.EPESC0000024, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.CanCheck = false;
                return;
            }
            if (subjType == SUBJTYPE.USER)
            {
                EFMsgInfo = EP.EPES.EPESC0000092/*不能对用户授权，请操作用户所属或所管理群组！*/;
                e.CanCheck = false;
            }
            else if (checkEditNested.Checked)
            {
                EFMsgInfo = EP.EPES.EPESC0000093/*嵌套查询模式下不能修改权限！*/;
                e.CanCheck = false;
            }
            else if (e.Node.ImageIndex == BUTTICON && !e.Node.ParentNode.Checked)
            {
                EFMsgInfo = EP.EPES.EPESC0000094/*请先勾选按钮对应的画面！*/;
                e.CanCheck = false;
            }
            else
            {
                e.CanCheck = true;
            }
        }

        private void treeListForm_AfterCheckNode(object sender, DevExpress.XtraTreeList.NodeEventArgs e)
        {
            AfterCheckNode(e.Node);
        }
        #endregion

        #region 勾选群组列表
        private void treeListGroup_BfgoreCheckNode(object sender, DevExpress.XtraTreeList.CheckNodeEventArgs e)
        {
            if (!isManageMode)
            {
                MessageBox.Show(EP.EPES.EPESC0000091/*非维护模式下无法修改权限！*/, EP.EPES.EPESC0000024, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.CanCheck = false;
                return;
            }
            if (e.Node.ImageIndex == GROUP_USER)
            {
                EFMsgInfo = EP.EPES.EPESC0000092/*不能对用户授权，请操作用户所属或所管理群组！*/;
                e.CanCheck = false;
            }
            else if (e.Node.ImageIndex == GROUP_GRAY)
            {
                EFMsgInfo = EP.EPES.EPESC0000095/*您不是该群组管理员，无权更改其权限！*/;
                e.CanCheck = false;                
            }
            else if (e.Node.Level > 0)
            {
                EFMsgInfo = EP.EPES.EPESC0000170/*子组权限仅供查询，不能修改！*/;
                e.CanCheck = false;
            }
            else
            {
                e.CanCheck = true;
                e.State = (e.PrevState == CheckState.Checked ? CheckState.Unchecked : CheckState.Checked);
            }
        }

        private void treeListGroup_AfterCheckNode(object sender, DevExpress.XtraTreeList.NodeEventArgs e)
        {
            if (xtraTabControlObj.SelectedTabPage == lblR)
            {
                TreeListNode resGroup = treeListResGroup.FocusedNode.Level == 0 ? treeListResGroup.FocusedNode : treeListResGroup.FocusedNode.ParentNode;

                string resgroupid = resGroup.GetValue(treeListColumnRGID).ToString();
                string groupid = e.Node.GetDisplayText(treeListColumnGID);

                if (e.Node.Checked) //勾选资源组
                {
                    if (htResGroupRmv.ContainsKey(groupid))
                    {
                        htResGroupRmv.Remove(groupid);
                    }
                    else if (!htResGroupAdd.ContainsKey(groupid))
                    {
                        htResGroupAdd.Add(groupid, resgroupid);
                    }
                }
                else //取消勾选
                {
                    if (htResGroupAdd.Contains(groupid))
                    {
                        htResGroupAdd.Remove(groupid);
                    }
                    else if (!htResGroupRmv.Contains(groupid))
                    {
                        htResGroupRmv.Add(groupid, resgroupid);
                    }
                }
            }
            else
            {
                string groupname = e.Node.Tag.ToString();
                if (e.Node.Checked)
                {
                    if (listGroupRevok.Contains(groupname))
                    {
                        listGroupRevok.Remove(groupname);
                    }
                    else if (!listGroupGrant.Contains(groupname))
                    {
                        listGroupGrant.Add(groupname);
                    }
                }
                else
                {
                    if (listGroupGrant.Contains(groupname))
                    {
                        listGroupGrant.Remove(groupname);
                    }
                    else if (!listGroupRevok.Contains(groupname))
                    {
                        listGroupRevok.Add(groupname);
                    }
                }
            }
            fgButtonGroupSave.Enabled = true;
        }
        #endregion

        #region 查询有画面权限的群组
        private void QryFormGroup(string formName)
        {
            EI.EIInfo inblks = new EI.EIInfo();
            EI.EIInfo outblks = new EI.EIInfo();

            // 设置调用服务的查询条件
            inblks.SetColName(1, "fname");
            inblks.SetColName(2, "appname");
            inblks.SetColName(3, "mode");
            inblks.SetColName(4, "buttname");
            inblks.SetColName(5, "username");
            inblks.SetColName(6, "companycode");
            inblks.SetColName(7, "groupname");
            inblks.SetColName(8, "adminuser");
            inblks.SetColName(9, "loginuser");

            inblks.SetColVal(1, 1, "fname", formName);
            inblks.SetColVal(1, 1, "appname", this.selectedAppname);
            inblks.SetColVal(1, 1, "mode", 1);
            inblks.SetColVal(1, 1, "buttname", "");
            inblks.SetColVal(1, 1, "username", this.EFUserId.Trim());
            inblks.SetColVal(1, 1, "companycode", this.selectedCompanyCode);
            inblks.SetColVal(1, 1, "groupname", fgtGEname.Text);
            inblks.SetColVal(1, 1, "adminuser", fgtGAdmin.Text);
            inblks.SetColVal(1, 1, "loginuser", this.EFUserId);

            // 调用后台服务并将结果赋给Grid控件
            outblks = EI.EITuxedo.CallService("epesresgrp_inq", inblks);
            
            outblks.blk_now = 0;
            this.treeListGroup.Nodes.Clear();

            string groupName = "";
            string groupDesc = "";
            string admin1 = "";
            string admin2 = "";
            string isadmin = outblks.GetColVal(3, 1, "isadmin");
            for (int i = 0; i < outblks.blk_info[0].Row; i++)
            {
                groupName = outblks.GetColVal(1, i + 1, "name");
                groupDesc = outblks.GetColVal(1, i + 1, "groupdescription");
                admin1 = outblks.GetColVal(1, i + 1, "adminuserename");
                admin2 = outblks.GetColVal(1, i + 1, "adminuserename2");
                TreeListNode treeNode = this.treeListGroup.AppendNode(new object[] { groupName, groupDesc, admin1, admin2 }, null, CheckState.Checked);
                treeNode.Tag = groupName;
                if (admin1 == this.EFUserId || admin2 == this.EFUserId || isadmin == "1")
                {
                    treeNode.ImageIndex = treeNode.SelectImageIndex = GROUP_ICON;
                }
                else
                {
                    treeNode.ImageIndex = treeNode.SelectImageIndex = GROUP_GRAY;
                }
            }
            outblks.blk_now = 1;
            for (int i = 0; i < outblks.blk_info[1].Row; i++)
            {
                groupName = outblks.GetColVal(2, i + 1, "name");
                groupDesc = outblks.GetColVal(2, i + 1, "groupdescription");
                admin1 = outblks.GetColVal(2, i + 1, "adminuserename");
                admin2 = outblks.GetColVal(2, i + 1, "adminuserename2");
                TreeListNode treeNode = this.treeListGroup.AppendNode(new object[] { groupName, groupDesc, admin1, admin2 }, null, CheckState.Unchecked);
                treeNode.Tag = groupName;
                if (admin1 == this.EFUserId || admin2 == this.EFUserId || isadmin == "1")
                {
                    treeNode.ImageIndex = treeNode.SelectImageIndex = GROUP_ICON;
                }
                else
                {
                    treeNode.ImageIndex = treeNode.SelectImageIndex = GROUP_GRAY;
                }
            }
        }
        #endregion

        #region 查询有按钮权限的群组
        private void QryButtGroup(string formName, string buttName)
        {
            EI.EIInfo inblks = new EI.EIInfo();
            EI.EIInfo outblks = new EI.EIInfo();

            // 设置调用服务的查询条件
            inblks.SetColName(1, "fname");
            inblks.SetColName(2, "appname");
            inblks.SetColName(3, "mode");
            inblks.SetColName(4, "buttname");
            inblks.SetColName(5, "username");
            inblks.SetColName(6, "companycode");
            inblks.SetColName(7, "groupname");
            inblks.SetColName(8, "adminuser");
            inblks.SetColName(9, "loginuser");

            inblks.SetColVal(1, 1, "fname", formName);
            inblks.SetColVal(1, 1, "appname", this.selectedAppname);
            inblks.SetColVal(1, 1, "mode", 2);
            inblks.SetColVal(1, 1, "buttname", buttName);
            inblks.SetColVal(1, 1, "username", this.EFUserId.Trim());
            inblks.SetColVal(1, 1, "companycode", this.selectedCompanyCode);
            inblks.SetColVal(1, 1, "groupname", fgtGEname.Text);
            inblks.SetColVal(1, 1, "adminuser", fgtGAdmin.Text);
            inblks.SetColVal(1, 1, "loginuser", this.EFUserId);
            
            // 调用后台服务并将结果赋给Grid控件
            outblks = EI.EITuxedo.CallService("epesresgrp_inq", inblks);

            treeListGroup.Nodes.Clear();

            string groupName = "", groupDesc = "", admin1 = "", admin2 = "";
            string isadmin = outblks.GetColVal(3, 1, "isadmin");
            for (int i = 0; i < outblks.blk_info[0].Row; i++)
            {
                groupName = outblks.GetColVal(1, i + 1, "name");
                groupDesc = outblks.GetColVal(1, i + 1, "groupdescription");
                admin1 = outblks.GetColVal(1, i + 1, "adminuserename");
                admin2 = outblks.GetColVal(1, i + 1, "adminuserename2");
                TreeListNode treeNode = this.treeListGroup.AppendNode(new object[] { groupName, groupDesc, admin1, admin2 }, null, CheckState.Checked);
                treeNode.Tag = groupName;
                if (admin1 == this.EFUserId || admin2 == this.EFUserId || isadmin == "1")
                {
                    treeNode.ImageIndex = treeNode.SelectImageIndex = GROUP_ICON;
                }
                else
                {
                    treeNode.ImageIndex = treeNode.SelectImageIndex = GROUP_GRAY;
                }
            }
            outblks.blk_now = 1;
            for (int i = 0; i < outblks.blk_info[1].Row; i++)
            {
                groupName = outblks.GetColVal(2, i + 1, "name");
                groupDesc = outblks.GetColVal(2, i + 1, "groupdescription");
                admin1 = outblks.GetColVal(2, i + 1, "adminuserename");
                admin2 = outblks.GetColVal(2, i + 1, "adminuserename2");
                TreeListNode treeNode = this.treeListGroup.AppendNode(new object[] { groupName, groupDesc, admin1, admin2 }, null, CheckState.Unchecked);
                treeNode.Tag = groupName;
                if (admin1 == this.EFUserId || admin2 == this.EFUserId || isadmin == "1")
                {
                    treeNode.ImageIndex = treeNode.SelectImageIndex = GROUP_ICON;
                }
                else
                {
                    treeNode.ImageIndex = treeNode.SelectImageIndex = GROUP_GRAY;
                }
            }
        }
        #endregion

        #region 查询画面
        private void QryForms()
        {
            treeListForm.Nodes.Clear();

            EI.EIInfo inBlock = new EI.EIInfo();
            EI.EIInfo outBlock = null;

            inBlock.SetColName(1, "name");
            inBlock.SetColName(2, "cname");
            inBlock.SetColName(3, "subjename");
            inBlock.SetColName(4, "mode");
            inBlock.SetColName(5, "appname");
            inBlock.SetColName(6, "companycode");
            inBlock.SetColName(7, "not_in_tree");

            inBlock.SetColVal(1, "name", fgtFormName.Text);
            inBlock.SetColVal(1, "cname", fgtFormDesc.Text);
            inBlock.SetColVal(1, "subjename", "admingroup");
            inBlock.SetColVal(1, "mode", 1);
            inBlock.SetColVal(1, "appname", this.selectedAppname);
            inBlock.SetColVal(1, "companycode", this.selectedCompanyCode);
            inBlock.SetColVal(1, "not_in_tree", this.checkNotInTree.Checked ? 1 : 0);

            outBlock = EI.EITuxedo.CallService("epesformlistinq", inBlock);

            string formName = "", formDesc = "";
            for (int i = 0; i < outBlock.blk_info[0].Row; i++)
            {
                formName = outBlock.Tables[0].Rows[i]["NAME"].ToString();
                formDesc = outBlock.Tables[0].Rows[i]["DESCRIPTION"].ToString();

                TreeListNode tnode = this.treeListForm.AppendNode(new object[] { formDesc , formName}, null);
                tnode.Tag = formName;
                tnode.SelectImageIndex = tnode.ImageIndex = FORMICON;
            }
            treeListForm.FocusedNode = null;
        }

        private void QryAuthForm()
        {
            if (subjEname == string.Empty || subjType == SUBJTYPE.NOAUTHGROUP)
            {
                EFMsgInfo = EP.EPES.EPESC0000097/*选择群组查询其有权限的资源*/;
                return;
            }

            //if (fgtFormName.Text.Length < 2 && fgtFormDesc.Text.Length < 2)
            //{
            //    MessageBox.Show(EP.EPES.EPESC0000098/*请输入至少两位画面名！*/, EP.EPES.EPESC0000024, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}

            EI.EIInfo inBlock = new EI.EIInfo();
            EI.EIInfo outBlock = null;

            int mode = -1;

            if (subjType == SUBJTYPE.USER) mode = 2;
            else if (subjType == SUBJTYPE.GROUP && checkEditNested.Checked) mode = 0;
            else mode = 1;

            inBlock.SetColName(1, "name");
            inBlock.SetColName(2, "cname");
            inBlock.SetColName(3, "subjename");
            inBlock.SetColName(4, "mode");
            inBlock.SetColName(5, "appname");
            inBlock.SetColName(6, "companycode");
            inBlock.SetColName(7, "not_in_tree");

            inBlock.SetColVal(1, "name", fgtFormName.Text);
            inBlock.SetColVal(1, "cname", fgtFormDesc.Text);
            inBlock.SetColVal(1, "subjename", subjEname);
            inBlock.SetColVal(1, "mode", mode);
            inBlock.SetColVal(1, "appname", this.selectedAppname);
            inBlock.SetColVal(1, "companycode", this.selectedCompanyCode);
            inBlock.SetColVal(1, "not_in_tree", this.checkNotInTree.Checked ? 1 : 0);

            outBlock = EI.EITuxedo.CallService("epesformlistinq", inBlock);

            string formName = "", formDesc = "", cnt = "";
            for (int i = 0; i < outBlock.blk_info[0].Row; i++)
            {
                formName = outBlock.Tables[0].Rows[i]["NAME"].ToString();
                formDesc = outBlock.Tables[0].Rows[i]["DESCRIPTION"].ToString();
                cnt = outBlock.Tables[0].Rows[i]["CNT"].ToString();

                if (cnt == "0")
                {
                    TreeListNode tnode = this.treeListForm.AppendNode(new object[] { formDesc, formName }, null, CheckState.Unchecked);
                    tnode.Tag = formName;
                    tnode.SelectImageIndex = tnode.ImageIndex = FORMICON;
                }
                else
                {
                    TreeListNode tnode = this.treeListForm.AppendNode(new object[] { formDesc, formName }, null, CheckState.Checked);
                    tnode.Tag = formName;
                    tnode.SelectImageIndex = tnode.ImageIndex = FORMICON;
                }

                //if (i > 500)
                //{
                //    MessageBox.Show(EP.EPES.EPESC0000099/*查询画面数量超出范围，请输入查询条件限制查询结果！*/, EP.EPES.EPESC0000024, MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    break;
                //}
            }
            treeListForm.FocusedNode = null;
        }

        private void fgButtonRfg_Click(object sender, EventArgs e)
        {
            treeListForm.Nodes.Clear();
            listFormGrant.Clear();
            listFormRevok.Clear();
            listButtGrant.Clear();
            listButtRevok.Clear();
            htGroupResAdd.Clear();
            htGroupResRmv.Clear();

            QryAuthForm();
            fgButtonList.Enabled = false;
        }
        #endregion

        #region 查询画面按钮
        private void QryButtResInList(string formName)
        {
            EI.EIInfo outblk =  QryFormButt(formName);

            string buttName = "";
            string buttDesc = "";
            for (int i = 0; i < outblk.blk_info[0].Row; i++)
            {

                buttName = outblk.GetColVal(1, i + 1, "name");
                buttDesc = outblk.GetColVal(1, i + 1, "description");
                TreeListNode treeNode = this.treeListForm.AppendNode(new object[] {buttDesc, buttName }, this.treeListForm.FocusedNode);
                treeNode.Tag = buttName;
                treeNode.SelectImageIndex = treeNode.ImageIndex = BUTTICON;
            }
        }

        private void QryButtResInTree(string formName)
        {
            EI.EIInfo outblk = QryFormButt(formName);

            string buttName = "";
            string buttDesc = "";
            for (int i = 0; i < outblk.blk_info[0].Row; i++)
            {
                buttName = outblk.GetColVal(1, i + 1, "name");
                buttDesc = outblk.GetColVal(1, i + 1, "description");
                TreeListNode treeNode = this.treeListRes.AppendNode(new object[] {buttName+" "+buttDesc}, this.treeListRes.FocusedNode);
                treeNode.Tag = buttName;
                treeNode.SelectImageIndex = treeNode.ImageIndex = BUTTICON;
            }
        }

        private EI.EIInfo QryFormButt(string formName)
        {
            EI.EIInfo inblk = new EI.EIInfo();
            EI.EIInfo outblk;

            int col;
            col = 1;
            inblk.SetColName(col++, "bname");  //按钮名
            inblk.SetColName(col++, "aclid"); //fgLabelText2.EFEname.ToLower()
            inblk.SetColName(col++, "mode");
            inblk.SetColName(col++, "fname");
            inblk.SetColName(col++, "appname");

            inblk.SetColVal(1, 1, "fname", formName);
            inblk.SetColVal(1, 1, "mode", 0);
            inblk.SetColVal(1, 1, "appname", this.selectedAppname);
            inblk.AddNewBlock();
            inblk.SetColName(2, 1, "userid");
            inblk.SetColName(2, 2, "appname");
            inblk.SetColVal(2, 1, "userid", this.EFUserId);
            inblk.SetColVal(2, 1, "appname",  EF_Args.epEname);

            outblk = EI.EITuxedo.CallService("epesbutt_inq2", inblk);
            return outblk;
        }

        #endregion

        #region 查询授权画面下按钮权限
        private void QryFormButtInTree(string formName)
        {
            EI.EIInfo outblks = QryButtAuth(formName);

            string buttName = "";
            string buttDesc = "";
            for (int i = 0; i < outblks.blk_info[0].Row; i++)
            {
                if (outblks.blk_info[1].colvalue[i, 0] == "0")
                {
                    buttName = outblks.GetColVal(1, i + 1, "name");
                    buttDesc = outblks.GetColVal(1, i + 1, "cname");
                    TreeListNode treeNode = this.treeListRes.AppendNode(new object[] { buttName + " " + buttDesc }, this.treeListRes.FocusedNode, CheckState.Unchecked);
                    treeNode.Tag = buttName;
                    treeNode.SelectImageIndex = treeNode.ImageIndex = BUTTICON;
                }
                else
                {
                    buttName = outblks.GetColVal(1, i + 1, "name");
                    buttDesc = outblks.GetColVal(1, i + 1, "cname");
                    TreeListNode treeNode = this.treeListRes.AppendNode(new object[] { buttName + " " + buttDesc }, this.treeListRes.FocusedNode, CheckState.Checked);
                    treeNode.Tag = buttName;
                    treeNode.SelectImageIndex = treeNode.ImageIndex = BUTTICON;
                }
            }
        }

        private void QryFormButtInList(string formName)
        {
            EI.EIInfo outblks = QryButtAuth(formName);

            string buttName = "";
            string buttDesc = "";
            for (int i = 0; i < outblks.blk_info[0].Row; i++)
            {
                buttName = outblks.GetColVal(1, i + 1, "name");
                buttDesc = outblks.GetColVal(1, i + 1, "cname");
                TreeListNode treeNode;
                if (outblks.blk_info[1].colvalue[i, 0] == "0")
                {
                    treeNode = this.treeListForm.AppendNode(new object[] {buttDesc  , buttName }, this.treeListForm.FocusedNode, CheckState.Unchecked);
                }
                else
                {
                    treeNode = this.treeListForm.AppendNode(new object[] { buttDesc , buttName }, this.treeListForm.FocusedNode, CheckState.Checked);               
                }
                treeNode.Tag = buttName;
                treeNode.SelectImageIndex = treeNode.ImageIndex = BUTTICON;
            }
        }

        private EI.EIInfo QryButtAuth(string formName)
        {
            EI.EIInfo inblks = new EI.EIInfo();
            EI.EIInfo outblks = new EI.EIInfo();

            int mode = -1;
            if (subjType == SUBJTYPE.USER) mode = 2;
            else if (subjType == SUBJTYPE.GROUP && EPESCommon.AuthMode == AUTHMODE.MODE_9672) mode = 0;
            else mode = 1;

            inblks.SetColName(1, "subjename");
            inblks.SetColName(2, "formname");
            inblks.SetColName(3, "mode");
            inblks.SetColName(4, "appname");
            inblks.SetColName(5, "companycode");

            inblks.SetColVal(1, "subjename", subjEname);
            inblks.SetColVal(1, "formname", formName);
            inblks.SetColVal(1, "mode", mode);
            inblks.SetColVal(1, "appname", this.selectedAppname);
            inblks.SetColVal(1, "companycode", this.selectedCompanyCode);

            outblks = EI.EITuxedo.CallService("epesbuttauthinq", inblks);
            return outblks;
        }
        #endregion

        #region 刷新已展开树节点的权限
        /// <summary>
        /// 对于展开的树查询上面画面、按钮的权限
        /// </summary>
        private void RfgreshTree()
        {
            nodeName.Clear();
            GetFormName(treeListRes.Nodes);

            EI.EIInfo inblk = new EI.EIInfo();
            EI.EIInfo outblk;
            int mode = -1;
            if (subjType == SUBJTYPE.USER) mode = 2;
            else if (subjType == SUBJTYPE.GROUP && EPESCommon.AuthMode == AUTHMODE.MODE_9672) mode = 0;
            else mode = 1;

            inblk.SetColName(1, "ename");
            inblk.SetColName(2, "formname");
            inblk.SetColName(3, "mode");
            inblk.SetColName(4, "appname");
            inblk.SetColName(5, "companycode");

            inblk.SetColVal(1, 1, "ename", subjEname);

            for (int i = 0; i < nodeName.Count; i++)
            {
                inblk.SetColVal(1, i + 1, "formname", nodeName[i]);
            }
            inblk.SetColVal(1, 1, "mode", mode);
            inblk.SetColVal(1, 1, "appname", this.selectedAppname);
            inblk.SetColVal(1, 1, "companycode", this.selectedCompanyCode);

            outblk = EI.EITuxedo.CallService("epesformauthinq", inblk);

            forminfo.Clear();
            auth.Clear();

            for (int j = 1; j <= outblk[0].Row; j++)
            {
                string formname = outblk.GetColVal(1, j, "formname");
                string buttname = outblk.GetColVal(1, j, "buttname");
                string formcount = outblk.GetColVal(2, j, "formcount");
                string buttcount = outblk.GetColVal(2, j, "buttcount");

                if (!forminfo.ContainsKey(formname))
                {
                    forminfo.Add(formname, formcount);

                    if (!auth.ContainsKey(formname))
                    {
                        auth.Add(formname, new Dictionary<string, object>());
                    }
                }
                if (buttname.Trim().Length > 0 && !((Dictionary<string, object>)auth[formname]).ContainsKey(buttname))
                {
                    ((Dictionary<string, object>)auth[formname]).Add(buttname, buttcount);
                }
            }

            SetNodeCheck(treeListRes.Nodes);
        }

        private void RfgreshFormList()
        {
            nodeName.Clear();
            GetFormName(treeListForm.Nodes);

            EI.EIInfo inblk = new EI.EIInfo();
            EI.EIInfo outblk;
            int mode = -1;
            if (subjType == SUBJTYPE.USER) mode = 2;
            else if (subjType == SUBJTYPE.GROUP && checkEditNested.Checked) mode = 0;
            else mode = 1;

            inblk.SetColName(1, "ename");
            inblk.SetColName(2, "formname");
            inblk.SetColName(3, "mode");
            inblk.SetColName(4, "appname");
            inblk.SetColName(5, "companycode");

            inblk.SetColVal(1, 1, "ename", subjEname);

            for (int i = 0; i < nodeName.Count; i++)
            {
                inblk.SetColVal(1, i + 1, "formname", nodeName[i]);
            }
            inblk.SetColVal(1, 1, "mode", mode);
            inblk.SetColVal(1, 1, "appname", this.selectedAppname);
            inblk.SetColVal(1, 1, "companycode", this.selectedCompanyCode);

            outblk = EI.EITuxedo.CallService("epesformauthinq", inblk);

            forminfo.Clear();
            auth.Clear();

            for (int j = 1; j <= outblk[0].Row; j++)
            {
                string formname = outblk.GetColVal(1, j, "formname");
                string buttname = outblk.GetColVal(1, j, "buttname");
                string formcount = outblk.GetColVal(2, j, "formcount");
                string buttcount = outblk.GetColVal(2, j, "buttcount");

                if (!forminfo.ContainsKey(formname))
                {
                    forminfo.Add(formname, formcount);

                    if (!auth.ContainsKey(formname))
                    {
                        auth.Add(formname, new Dictionary<string, object>());
                    }
                }
                if (buttname.Trim().Length > 0 && !((Dictionary<string, object>)auth[formname]).ContainsKey(buttname))
                {
                    ((Dictionary<string, object>)auth[formname]).Add(buttname, buttcount);
                }
            }

            SetNodeCheck(treeListForm.Nodes);
        }

        /// <summary>
        /// 递归获取画面名列表
        /// </summary>
        /// <param name="tc">树节点</param>
        private void GetFormName(DevExpress.XtraTreeList.Nodes.TreeListNodes tc)
        {
            foreach (DevExpress.XtraTreeList.Nodes.TreeListNode node in tc)
            {
                if (node.ImageIndex == FOLDERICON || node.ImageIndex == FOLDERICON_EXPAND)
                {
                    GetFormName(node.Nodes);
                }
                if (node.ImageIndex == FORMICON) //form
                {
                    nodeName.Add(node.Tag.ToString());
                }
            }
        }

        /// <summary>
        /// 递归设置树节点CheckStatus
        /// </summary>
        /// <param name="tc">树节点</param>
        private void SetNodeCheck(DevExpress.XtraTreeList.Nodes.TreeListNodes tc)
        {
            foreach (DevExpress.XtraTreeList.Nodes.TreeListNode node in tc)
            {
                if (node.ImageIndex == FOLDERICON || node.ImageIndex == FOLDERICON_EXPAND|| node.ImageIndex == FORMICON)
                {
                    SetNodeCheck(node.Nodes);
                }
                if (node.ImageIndex == FORMICON) //form
                {
                    if (forminfo.ContainsKey(node.Tag.ToString()))
                    {
                        node.CheckState = forminfo[node.Tag.ToString()].ToString() == "0" ? CheckState.Unchecked : CheckState.Checked;
                    }
                }
                else if (node.ImageIndex == BUTTICON) //button
                {
                    string form = node.ParentNode.Tag.ToString();
                    string butt = node.Tag.ToString();
                    if (auth.ContainsKey(form) && ((Dictionary<string, object>)auth[form]).ContainsKey(butt))
                    {
                        node.CheckState = ((Dictionary<string, object>)auth[form])[butt].ToString() == "0" ? CheckState.Unchecked : CheckState.Checked;
                    }
                }
            }
        }
        #endregion

        #region 保存群组资源权限
        private void fgButtonTree_Click(object sender, EventArgs e)
        {
            SaveAuth();
            fgButtonTree.Enabled = false;
            treeListRes.Focus();
        }

        private void fgButtonList_Click(object sender, EventArgs e)
        {
            if (fgDevCheckEdit2.Checked)//Form Query 
            {
                //this.EFMsgInfo = "";
                QryForms();
            }
            else //Auth Save
            {
                SaveAuth();
                fgButtonList.Enabled = false;
                treeListForm.Focus();
            }
        }

        private void SaveAuth()
        {
            //this.EFMsgInfo = "";

            if (listFormGrant.Count > 0)
            {
                EI.EIInfo inblk = new EI.EIInfo();
                EI.EIInfo outblk;

                string code = subjEname;
                int mode = 2;

                inblk.SetColName(1, "ei_row_num");
                inblk.SetColName(2, "name");
                inblk.SetColName(3, "cname");
                inblk.SetColName(5, "mode");
                inblk.SetColName(6, "code");
                inblk.SetColName(7, "username");
                inblk.SetColName(8, "appname");
                inblk.AddNewBlock();
                inblk.SetColName(2, 1, "userid");
                inblk.SetColName(2, 2, "appname");

                for (int i = 0; i < listFormGrant.Count; i++ )
                {
                    inblk.SetColVal(1, i+1, "name", listFormGrant[i]);
                }

                inblk.SetColVal(1, 5, mode);
                inblk.SetColVal(1, 6, code);
                inblk.SetColVal(1, 7, this.EFUserId.Trim());
                inblk.SetColVal(1, "appname", this.selectedAppname);
                inblk.SetColVal(2, 1, "userid", this.EFUserId);
                inblk.SetColVal(2, 1, "appname", this.selectedAppname);

                outblk = EI.EITuxedo.CallService("epesform_grant", inblk);

                if (outblk.sys_info.flag == 0)
                {
                    //this.EFMsgInfo = string.Format(EP.EPES.EPESC0000118/*成功执行 sqlcode is {0}*/, outblk.sys_info.sqlcode.ToString());
                }
                else
                {
                    MessageBox.Show(outblk.sys_info.msg, EP.EPES.EPESC0000024, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            if (listFormRevok.Count > 0)
            {
                EI.EIInfo inblk2 = new EI.EIInfo();
                EI.EIInfo outblk2;

                string code = subjEname;
                int mode = 2;

                inblk2.SetColName(2, "name");
                inblk2.SetColName(3, "cname");
                inblk2.SetColName(5, "mode");
                inblk2.SetColName(6, "code");
                inblk2.SetColName(7, "username");
                inblk2.SetColName(8, "appname");
                inblk2.AddNewBlock();
                inblk2.SetColName(2, 1, "userid");
                inblk2.SetColName(2, 2, "appname");

                for (int j = 0; j < listFormRevok.Count; j++ )
                {
                    inblk2.SetColVal(1, j+1, "name", listFormRevok[j]);
                }
                inblk2.SetColVal(1, 5, mode);
                inblk2.SetColVal(1, 6, code);
                inblk2.SetColVal(1, 7, this.EFUserId.Trim());
                inblk2.SetColVal(1, "appname", this.selectedAppname);
                inblk2.SetColVal(2, 1, "userid", this.EFUserId);
                inblk2.SetColVal(2, 1, "appname", this.selectedAppname);

                outblk2 = EI.EITuxedo.CallService("epesform_revoke", inblk2);

                if (outblk2.sys_info.flag == 0)
                {
                    //this.EFMsgInfo = string.Format(EP.EPES.EPESC0000118/*成功执行 sqlcode is {0}*/, outblk2.sys_info.sqlcode.ToString());
                }
                else
                {
                    MessageBox.Show(outblk2.sys_info.msg, EP.EPES.EPESC0000024, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            if (listButtGrant.Count > 0 || listButtRevok.Count > 0)
            {
                EI.EIInfo inblk = new EI.EIInfo();
                EI.EIInfo outblk;

                inblk.SetColName(1, "buttname");
                inblk.SetColName(2, "formname");
                inblk.SetColName(3, "groupname");
                inblk.SetColName(4, "mode");
                inblk.SetColName(5, "appname");
                inblk.SetColName(10, "username");

                string buttname = "", formname = "";
                int i = 0;
                for ( i = 0; i < listButtGrant.Count; i++)
                {
                    int len = listButtGrant[i].Length;
                    buttname = listButtGrant[i].Substring(listButtGrant[i].IndexOf(',')+1, len - listButtGrant[i].IndexOf(',')-1);
                    formname = listButtGrant[i].Substring(0,listButtGrant[i].IndexOf(','));
                    inblk.SetColVal(1, i + 1, 1, buttname);
                    inblk.SetColVal(1, i + 1, 2, formname);
                    inblk.SetColVal(1, i + 1, 4, 1);
                }

                for (int j = i ; j < i + listButtRevok.Count; j++)
                {
                    int len = listButtRevok[j-i].Length;
                    buttname = listButtRevok[j - i].Substring(listButtRevok[j - i].IndexOf(',') + 1, len - listButtRevok[j - i].IndexOf(',') - 1);
                    formname = listButtRevok[j - i].Substring(0, listButtRevok[j - i].IndexOf(','));
                    inblk.SetColVal(1, j + 1, 1, buttname);
                    inblk.SetColVal(1, j + 1, 2, formname);
                    inblk.SetColVal(1, j + 1, 4, 0);
                }

                inblk.SetColVal(1, 3, subjEname);
                inblk.SetColVal(1, "appname", this.selectedAppname);
                inblk.SetColVal(1, "username", this.EFUserId);

                outblk = EI.EITuxedo.CallService("epesbuttauthupd", inblk);

                if (outblk.sys_info.flag == 0)
                {
                    //this.EFMsgInfo = string.Format(EP.EPES.EPESC0000118/*成功执行 sqlcode is {0}*/, outblk.sys_info.sqlcode.ToString());
                }

                else
                {
                    MessageBox.Show(outblk.sys_info.msg, EP.EPES.EPESC0000024, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }


            //刷新结果
            if (xtraTabControlObj.SelectedTabPage == xtraTabPageList)
            {
                RfgreshFormList();
            }
            else if(xtraTabControlObj.SelectedTabPage == xtraTabPageTree)
            {
                RfgreshTree();
            }

            listFormGrant.Clear();
            listFormRevok.Clear();
            listButtGrant.Clear();
            listButtRevok.Clear();

            SaveOthResAuth();

        }
        #endregion

        #region 保存资源群组权限
        private void fgButtonGroupSave_Click(object sender, EventArgs e)
        {
            if (xtraTabControlObj.SelectedTabPage == lblR)
            {
                SaveResGroupToGroup();
            }
            else
            {
                SaveResToGroup();
                treeListGroup.Focus();
            }
        }

        /// <summary>
        /// 资源到群组模式
        /// </summary>
        private void SaveResGroupToGroup()
        {
            EI.EIInfo inblk = new EI.EIInfo();
            inblk.AddColName(1, "appname");
            inblk.SetColVal(1, 1, "appname",  EF_Args.epEname);

            if (htResGroupAdd.Count > 0)
            {
                DataTable dt = new DataTable("RES_GROUP_ADD");
                dt.Columns.Add("groupid");
                dt.Columns.Add("resgroupid");

                foreach (System.Collections.DictionaryEntry item in htResGroupAdd)
                {
                    string groupid = item.Key.ToString();
                    string resgroupid = item.Value.ToString();

                    dt.Rows.Add(new object[] { groupid, resgroupid });
                }
                inblk.Tables.Add(dt);
            }
            if (htResGroupRmv.Count > 0)
            {
                DataTable dt = new DataTable("RES_GROUP_RMV");
                dt.Columns.Add("groupid");
                dt.Columns.Add("resgroupid");

                foreach (System.Collections.DictionaryEntry item in htResGroupRmv)
                {
                    string groupid = item.Key.ToString();
                    string resgroupid = item.Value.ToString();

                    dt.Rows.Add(new object[] { groupid, resgroupid });
                }
                inblk.Tables.Add(dt);
            }
            EI.EIInfo outblk = EI.EITuxedo.CallService("epesgrgr_upd", inblk);

            if (outblk.sys_info.flag == 0)
            {
                TreeListNode node = treeListResGroup.FocusedNode.Level == 0 ? treeListResGroup.FocusedNode : treeListResGroup.FocusedNode.ParentNode;

                QryGroupInResGroup(node.GetValue(treeListColumnRGID).ToString());

                htResGroupAdd.Clear();
                htResGroupRmv.Clear();

                fgButtonGroupSave.Enabled = false;
                //this.EFMsgInfo = EP.EPES.EPESC0000156/*操作成功！*/; 
            }
            else
            {
                MessageBox.Show(outblk.sys_info.msg, EP.EPES.EPESC0000024/*提示*/, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SaveResToGroup()
        {
            //this.EFMsgInfo = "";

            if (objType == OBJTYPE.FORM)
            {
                if (listGroupGrant.Count > 0)
                {
                    EI.EIInfo inblk = new EI.EIInfo();
                    EI.EIInfo outblk;

                    inblk.SetColName(1, "name");
                    inblk.SetColName(7, "fname");
                    inblk.SetColName(8, "mode");
                    inblk.SetColName(9, "buttname");
                    inblk.SetColName(10, "username");
                    inblk.SetColName(11, "appname");

                    for (int i = 0; i < listGroupGrant.Count; i++)
                    {
                        inblk.SetColVal(1, i + 1, "name", listGroupGrant[i]);
                    }

                    inblk.SetColVal(1, 1, "fname", formEname);
                    inblk.SetColVal(1, 1, "mode", 1); //mode==1 新增群组权限
                    inblk.SetColVal(1, 1, "buttname", "");
                    inblk.SetColVal(1, 1, "username", this.EFUserId.Trim());
                    inblk.SetColVal(1, 1, "appname", this.selectedAppname);

                    outblk = EI.EITuxedo.CallService("epesresauth_inq", inblk);

                    if (outblk.sys_info.flag != 0)
                    {
                        MessageBox.Show(outblk.sys_info.msg, EP.EPES.EPESC0000024, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                if (listGroupRevok.Count > 0)
                {
                    EI.EIInfo inblk = new EI.EIInfo();
                    EI.EIInfo outblk;

                    inblk.SetColName(1, "name");
                    inblk.SetColName(7, "fname");
                    inblk.SetColName(8, "mode");
                    inblk.SetColName(9, "buttname");
                    inblk.SetColName(10, "username");
                    inblk.SetColName(11, "appname");

                    for (int i = 0; i < listGroupRevok.Count; i++)
                    {
                        inblk.SetColVal(1, i + 1, "name", listGroupRevok[i]);
                    }

                    inblk.SetColVal(1, 1, "fname", formEname);
                    inblk.SetColVal(1, 1, "mode", 2); //mode==2 删除群组权限
                    inblk.SetColVal(1, 1, "buttname", "");
                    inblk.SetColVal(1, 1, "username", this.EFUserId.Trim());
                    inblk.SetColVal(1, 1, "appname", this.selectedAppname);

                    outblk = EI.EITuxedo.CallService("epesresauth_inq", inblk);

                    if (outblk.sys_info.flag != 0)
                    {
                        MessageBox.Show(outblk.sys_info.msg, EP.EPES.EPESC0000024, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else //修改按钮群组权限
            {
                if (listGroupGrant.Count > 0)
                {
                    EI.EIInfo inblk = new EI.EIInfo();
                    EI.EIInfo outblk;

                    inblk.SetColName(1, "name");
                    inblk.SetColName(7, "fname");
                    inblk.SetColName(8, "mode");
                    inblk.SetColName(9, "buttname");
                    inblk.SetColName(10, "username");
                    inblk.SetColName(11, "appname");

                    for (int i = 0; i < listGroupGrant.Count; i++)
                    {
                        inblk.SetColVal(1, i + 1, "name", listGroupGrant[i]);
                    }
                    inblk.SetColVal(1, 1, "fname", formEname);
                    inblk.SetColVal(1, 1, "mode", 1);
                    inblk.SetColVal(1, 1, "buttname", buttEname);
                    inblk.SetColVal(1, 1, "username", this.EFUserId.Trim());
                    inblk.SetColVal(1, 1, "appname", this.selectedAppname);

                    outblk = EI.EITuxedo.CallService("epesresauth_inq", inblk);
                    if (outblk.sys_info.flag != 0)
                    {
                        MessageBox.Show(outblk.sys_info.msg, EP.EPES.EPESC0000024, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                if (listGroupRevok.Count > 0)
                {
                    EI.EIInfo inblk = new EI.EIInfo();
                    EI.EIInfo outblk;

                    inblk.SetColName(1, "name");
                    inblk.SetColName(7, "fname");
                    inblk.SetColName(8, "mode");
                    inblk.SetColName(9, "buttname");
                    inblk.SetColName(10, "username");
                    inblk.SetColName(11, "appname");

                    for (int i = 0; i < listGroupRevok.Count; i++)
                    {
                        inblk.SetColVal(1, i + 1, "name", listGroupRevok[i]);
                    }
                    inblk.SetColVal(1, 1, "fname", formEname);
                    inblk.SetColVal(1, 1, "mode", 2);
                    inblk.SetColVal(1, 1, "buttname", buttEname);
                    inblk.SetColVal(1, 1, "username", this.EFUserId.Trim());
                    inblk.SetColVal(1, 1, "appname", this.selectedAppname);

                    outblk = EI.EITuxedo.CallService("epesresauth_inq", inblk);
                    if (outblk.sys_info.flag != 0)
                    {
                        MessageBox.Show(outblk.sys_info.msg, EP.EPES.EPESC0000024, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        fgButtonResGroup.Enabled = false;
                    }
                }
            }

            if (objType == OBJTYPE.FORM)
            {
                QryFormGroup(formEname);
            }
            else
            {
                QryButtGroup(formEname, buttEname);
            }

            listGroupGrant.Clear();
            listGroupRevok.Clear();
            fgButtonGroupSave.Enabled = false;
        }
        #endregion

        #region 勾选的节点粗体显示

        private void treeListOthRes_CustomDrawNodeCell(object sender, DevExpress.XtraTreeList.CustomDrawNodeCellEventArgs e)
        {
            if (treeListOthRes.OptionsView.ShowCheckBoxes && e.Node.Checked)
            {
                e.Appearance.Font = new Font(DevExpress.Utils.AppearanceObject.DfgaultFont, FontStyle.Bold);
            }
        }

        private void treeListForm_CustomDrawNodeCell(object sender, DevExpress.XtraTreeList.CustomDrawNodeCellEventArgs e)
        {
            if (treeListForm.OptionsView.ShowCheckBoxes && e.Node.Checked)
            {
                e.Appearance.Font = new Font(DevExpress.Utils.AppearanceObject.DfgaultFont, FontStyle.Bold);
            }
        }

        private void treeListRes_CustomDrawNodeCell(object sender, DevExpress.XtraTreeList.CustomDrawNodeCellEventArgs e)
        {
            if (treeListRes.OptionsView.ShowCheckBoxes && e.Node.Checked)
            {
                e.Appearance.Font = new Font(DevExpress.Utils.AppearanceObject.DfgaultFont, FontStyle.Bold);
            }
        }

        private void treeListGroup_CustomDrawNodeCell(object sender, DevExpress.XtraTreeList.CustomDrawNodeCellEventArgs e)
        {
            if (treeListGroup.OptionsView.ShowCheckBoxes && e.Node.Checked)
            {
                e.Appearance.Font = new Font(DevExpress.Utils.AppearanceObject.DfgaultFont, FontStyle.Bold);
            }
        }

        private void treeListResGroup_CustomDrawNodeCell(object sender, DevExpress.XtraTreeList.CustomDrawNodeCellEventArgs e)
        {
            if (treeListResGroup.OptionsView.ShowCheckBoxes && e.Node.Checked)
            {
                e.Appearance.Font = new Font(DevExpress.Utils.AppearanceObject.DfgaultFont, FontStyle.Bold);
            }
        }
        #endregion

        #region 查询细部资源权限
        private EI.EIInfo ESOTHER_Get_Resource_ALL()
        {
            EI.EIInfo inBlock = new EI.EIInfo();
            EI.EIInfo outBlock;

            inBlock.SetColName(1, "code_class");
            inBlock.SetColVal(1, 1, "code_class", "ES03");

            outBlock = EI.EITuxedo.CallService("epep01_inq3", inBlock);
            if (outBlock.GetSys().flag == 0)
            {
                return outBlock;
            }
            else
            {
                MessageBox.Show(EP.EPES.EPESC0000100/*获取资源代码信息错误！*/, EP.EPES.EPESC0000024, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        private void QryOthResAuth()
        {
            treeListOthRes.Nodes.Clear();

            if (subjEname == string.Empty || subjType == SUBJTYPE.NOAUTHGROUP)
            {
                EFMsgInfo = EP.EPES.EPESC0000101/*选择群组或用户查询其有权限的资源*/;
                return;
            }

            string type = "";
            string[] array = comboOthResType.Text.Split('|');

            type = array[0];

            if (type.Length < 1)
            {
                type = "0";
            }

            int mode = -1;

            if (subjType == SUBJTYPE.USER) mode = 2;
            else mode = 1;

            EI.EIInfo inblk = new EI.EIInfo();
            EI.EIInfo outblk = new EI.EIInfo();

            inblk.SetColName(1, "name");
            inblk.SetColVal(1, 1, fgtOthName.Text);

            inblk.SetColName(2, "username");
            inblk.SetColVal(1, 2, subjEname);

            inblk.SetColName(3, "type");
            inblk.SetColVal(1, 3, type);

            inblk.SetColName(4, "appname");
            inblk.SetColVal(1, 4, this.selectedAppname);

            inblk.SetColName(5, "companycode");
            inblk.SetColVal(1, 5, this.selectedCompanyCode);

            inblk.SetColName(6, "mode");
            inblk.SetColVal(1, 6, mode);

            outblk = EI.EITuxedo.CallService("epesothauth_inq", inblk);

            if (outblk != null)
            {
                if (outblk.blk_info[0] != null && outblk.blk_info[1] != null)
                {
                    string name = "";
                    string desc = "";
                    for (int i = 0; i < outblk.blk_info[0].Row; i++)
                    {
                        name = outblk.GetColVal(1, i + 1, "name");
                        desc = outblk.GetColVal(1, i + 1, "desc");

                        if (outblk.blk_info[1].colvalue[i, 0] == "0")
                        {
                            TreeListNode tnode = this.treeListOthRes.AppendNode(new object[] { name, desc }, null, CheckState.Unchecked);
                            tnode.Tag = name;
                        }
                        else
                        {
                            TreeListNode tnode = this.treeListOthRes.AppendNode(new object[] { name, desc }, null, CheckState.Checked);
                            tnode.Tag = name;
                        }
                    }
                    treeListOthRes.FocusedNode = null;
                }
            }
        }

        #endregion

        #region 保存细部资源权限
        private void fgButtonOthRes_Click(object sender, EventArgs e)
        {
            if (SaveOthResAuth())
            {
                fgButtonOthRes.Enabled = false;
                treeListOthRes.Focus();
            }
        }

        private bool SaveOthResAuth()
        {
            //this.EFMsgInfo = "";

            if (listOthResGrant.Count > 0)
            {           
                EI.EIInfo inblki = new EI.EIInfo();
                EI.EIInfo outblki = new EI.EIInfo();
                long type = 0;
                //设置列名
                inblki.SetColName(1, "name");
                inblki.SetColName(2, "owner");
                inblki.SetColName(3, "ownertype");
                inblki.SetColName(4, "type");
                inblki.SetColName(5, "appname");

                for (int j = 0; j < listOthResGrant.Count; j++)
                {
                    inblki.SetColVal(1, j + 1, "name", listOthResGrant[j]);
                    inblki.SetColVal(1, j + 1, "owner", this.subjEname);
                    inblki.SetColVal(1, j + 1, "appname",this.selectedAppname);
                    inblki.SetColVal(1, j + 1, "ownertype", 2);

                    if ((!read.Checked) && (!write.Checked) && (!execute.Checked))
                    {
                        MessageBox.Show(EP.EPES.EPESC0000102/*请给资源分配相应的权限！*/, EP.EPES.EPESC0000024, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }

                    type = 0;
                    string substr = "";
                    if (listOthResGrant[j].Length >= 2)
                        substr = listOthResGrant[j].Substring(0, 2);

                    if (substr == "BB" || substr == "BS")
                    {
                        if (execute.Checked)
                        {
                            type = 0 + 1;
                        }
                        if (write.Checked)
                        {
                            type = type + 2;
                        }
                        type = type + 10;
                        inblki.SetColVal(1, j+1, "type", "13");
                    }
                    else
                    {
                        if (execute.Checked)
                        {
                            type = 0 + 1;
                        }

                        if (write.Checked)
                        {
                            type = type + 2;
                        }
                        type = type + 20;
                        inblki.SetColVal(1, j + 1, "type", "23");
                    }
                }

                outblki = EI.EITuxedo.CallService("epesgroth_ins", inblki);

                if (outblki.sys_info.flag != 0)
                {
                    MessageBox.Show(outblki.sys_info.msg, EP.EPES.EPESC0000024, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }

            if (listOthResRevok.Count > 0)
            {
                EI.EIInfo inblkd = new EI.EIInfo();
                EI.EIInfo outblkd = new EI.EIInfo();
                //设置列名
                inblkd.SetColName(1, "name");
                inblkd.SetColName(2, "owner");
                inblkd.SetColName(3, "ownertype");
                inblkd.SetColName(4, "appname");

                for (int j = 0; j < listOthResRevok.Count; j++)
                {
                    inblkd.SetColVal(1, j + 1, "name", listOthResRevok[j]);
                    inblkd.SetColVal(1, j + 1, "owner", this.subjEname);
                    inblkd.SetColVal(1, j + 1, "appname", this.selectedAppname);
                    inblkd.SetColVal(1, j + 1, "ownertype", 2);
                }

                outblkd = EI.EITuxedo.CallService("epesgroth_del", inblkd);

                if (outblkd.sys_info.flag != 0)
                {
                    MessageBox.Show(outblkd.sys_info.msg, EP.EPES.EPESC0000024, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }

            //刷新结果
            QryOthResAuth();
            listOthResGrant.Clear();
            listOthResRevok.Clear();

            return true;            
        }

        private void treeListOthRes_AfterCheckNode(object sender, DevExpress.XtraTreeList.NodeEventArgs e)
        {
            fgButtonOthRes.Enabled = true;

            string name = e.Node.Tag.ToString();

            if (e.Node.Checked)
            {
                if (listOthResRevok.Contains(name))
                {
                    listOthResRevok.Remove(name);
                }
                else if (!listOthResGrant.Contains(name))
                {
                    listOthResGrant.Add(name);
                }
            }
            else
            {
                if (listOthResGrant.Contains(name))
                {
                    listOthResGrant.Remove(name);
                }
                else if (!listOthResRevok.Contains(name))
                {
                    listOthResRevok.Add(name);
                }
            }
        }

        private void treeListOthRes_BfgoreCheckNode(object sender, DevExpress.XtraTreeList.CheckNodeEventArgs e)
        {
            if (!isManageMode)
            {
                MessageBox.Show(EP.EPES.EPESC0000091/*非维护模式下无法修改权限！*/, EP.EPES.EPESC0000024, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.CanCheck = false;
                return;
            }
            if (subjType == SUBJTYPE.USER)
            {
                EFMsgInfo = EP.EPES.EPESC0000092/*不能对用户授权，请操作用户所属或所管理群组！*/;
                e.CanCheck = false;
            }
            else
            {
                e.CanCheck = true;
            }
        }
        #endregion

        #region 查询用户资源权限来源
        private void treeListForm_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                DevExpress.XtraTreeList.TreeListHitInfo hi = treeListForm.CalcHitInfo(e.Location);
                if (hi != null && hi.Node != null)
                {
                    treeListForm.FocusedNode = hi.Node;
                    this.popupMenuList.ShowPopup(MousePosition);
                }                
            }
        }

        private void treeListRes_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                DevExpress.XtraTreeList.TreeListHitInfo hi = treeListRes.CalcHitInfo(e.Location);
                if (hi != null && hi.Node != null)
                {
                    treeListRes.FocusedNode = hi.Node;
                    this.popupMenuList.ShowPopup(MousePosition);
                }                
            }
        }

        private void treeListOthRes_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                DevExpress.XtraTreeList.TreeListHitInfo hi = treeListOthRes.CalcHitInfo(e.Location);
                if (hi != null && hi.Node != null)
                {
                    treeListOthRes.FocusedNode = hi.Node;
                    this.popupMenuList.ShowPopup(MousePosition);
                }                
            }
        }

        private void popupMenuList_BfgorePopup(object sender, CancelEventArgs e)
        {
            if (subjType == SUBJTYPE.USER)
            {
                barButtonItemAuthSource.Visibility = BarItemVisibility.Always;
                barButtonItemSelAll.Visibility = BarItemVisibility.Never;
                barButtonItemUnSelAll.Visibility = BarItemVisibility.Never;

                if (xtraTabControlObj.SelectedTabPage == xtraTabPageList)
                {
                    barButtonItemAuthSource.Enabled = treeListForm.FocusedNode.Checked ? true:false;
                }
                else if (xtraTabControlObj.SelectedTabPage == xtraTabPageTree)
                {
                    barButtonItemAuthSource.Enabled = treeListRes.FocusedNode.Checked ? true:false;
                }
                else if (xtraTabControlObj.SelectedTabPage == xtraTabPageOtherRes)
                {
                    barButtonItemAuthSource.Enabled = treeListOthRes.FocusedNode.Checked ? true:false;
                }                
            }
            else 
            {
                if (fg_args.b_info[2].Status == EFButtonKeysStatus.PRE_ACTIVE && EPESCommon.AuthMode == AUTHMODE.MODE_CLASSIC) //维护模式
                {
                    barButtonItemAuthSource.Visibility = BarItemVisibility.Never;

                    if (xtraTabControlObj.SelectedTabPage == xtraTabPageList)
                    {
                        if (treeListForm.FocusedNode.SelectImageIndex == FORMICON)
                        {
                            barButtonItemSelAll.Visibility = BarItemVisibility.Always;
                            barButtonItemUnSelAll.Visibility = BarItemVisibility.Always;
                        }
                        else
                        {
                            barButtonItemSelAll.Visibility = BarItemVisibility.Never;
                            barButtonItemUnSelAll.Visibility = BarItemVisibility.Never;
                        }
                    }
                    else if (xtraTabControlObj.SelectedTabPage == xtraTabPageTree)
                    {
                        if (treeListRes.FocusedNode.SelectImageIndex == FOLDERICON_EXPAND
                            || treeListRes.FocusedNode.SelectImageIndex == FORMICON)
                        {
                            barButtonItemSelAll.Visibility = BarItemVisibility.Always;
                            barButtonItemUnSelAll.Visibility = BarItemVisibility.Always;
                        }
                        else
                        {
                            barButtonItemSelAll.Visibility = BarItemVisibility.Never;
                            barButtonItemUnSelAll.Visibility = BarItemVisibility.Never;
                        }
                    }
                    else if (xtraTabControlObj.SelectedTabPage == xtraTabPageOtherRes)
                    {
                        barButtonItemSelAll.Visibility = BarItemVisibility.Never;
                        barButtonItemUnSelAll.Visibility = BarItemVisibility.Never;
                    }
                }
                else
                {
                    barButtonItemAuthSource.Visibility = BarItemVisibility.Never;
                    barButtonItemSelAll.Visibility = BarItemVisibility.Never;
                    barButtonItemUnSelAll.Visibility = BarItemVisibility.Never;
                }
            }            
        }

        //查询权限来源
        private void barButtonItemAuthSource_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {          
            ES.FormESAUTHSOUR authsour = new ES.FormESAUTHSOUR();

            //查询画面资源权限来源
            if (xtraTabControlObj.SelectedTabPage == xtraTabPageList)
            {
                EI.EIInfo inblk = new EI.EIInfo();
                EI.EIInfo outblk;    

                inblk.SetColName(1,"subjname");
                inblk.SetColName(2, "resname");
                inblk.SetColName(3, "mode");
                inblk.SetColName(4, "app");
                inblk.SetColName(5, "company");
                inblk.SetColName(6, "userid");
                inblk.SetColName(7, "fname");  //按钮的父画面名

                int mode = 0;
                string fname = "";
                //画面
                if (treeListForm.FocusedNode.ImageIndex == FORMICON)
                {
                    mode = 0;
                }
                //按钮
                else if (treeListForm.FocusedNode.ImageIndex == BUTTICON)
                {
                    mode = 1;
                    fname = treeListForm.FocusedNode.ParentNode.Tag.ToString();
                }

                inblk.SetColVal(1, 1, "subjname", this.subjEname);
                inblk.SetColVal(1, 1, "resname", treeListForm.FocusedNode.Tag.ToString());
                inblk.SetColVal(1, 1, "mode", mode);
                inblk.SetColVal(1, 1, "app", this.selectedAppname);
                inblk.SetColVal(1, 1, "company", this.selectedCompanyCode);
                inblk.SetColVal(1, 1, "userid",  EF_Args.formUserId);
                inblk.SetColVal(1, 1, "fname", fname);

                outblk = EI.EITuxedo.CallService("epesauthsourinq",inblk);

                authsour.result = outblk;
                authsour.user = this.subjEname;
                authsour.res = treeListForm.FocusedNode.Tag.ToString();
                //authsour.Icon = this.Icon;

                authsour.ShowDialog();
            }
            //查询菜单树资源权限来源
            else if (xtraTabControlObj.SelectedTabPage == xtraTabPageTree)
            {
                EI.EIInfo inblk = new EI.EIInfo();
                EI.EIInfo outblk;

                inblk.SetColName(1, "subjname");
                inblk.SetColName(2, "resname");
                inblk.SetColName(3, "mode");
                inblk.SetColName(4, "app");
                inblk.SetColName(5, "company");
                inblk.SetColName(6, "userid");
                inblk.SetColName(7, "fname");  //按钮的父画面名

                int mode = 0;
                string fname = "";
                //画面
                if (treeListRes.FocusedNode.ImageIndex == FORMICON)
                {
                    mode = 0;
                }
                //按钮
                else if (treeListRes.FocusedNode.ImageIndex == BUTTICON)
                {
                    mode = 1;
                    fname = treeListRes.FocusedNode.ParentNode.Tag.ToString();
                }

                inblk.SetColVal(1, 1, "subjname", this.subjEname);
                inblk.SetColVal(1, 1, "resname", treeListRes.FocusedNode.Tag.ToString());
                inblk.SetColVal(1, 1, "mode", mode);
                inblk.SetColVal(1, 1, "app", this.selectedAppname);
                inblk.SetColVal(1, 1, "company", this.selectedCompanyCode);
                inblk.SetColVal(1, 1, "userid",  EF_Args.formUserId);
                inblk.SetColVal(1, 1, "fname", fname);

                outblk = EI.EITuxedo.CallService("epesauthsourinq", inblk);

                authsour.result = outblk;
                authsour.user = this.subjEname;
                authsour.res = treeListRes.FocusedNode.Tag.ToString();

                authsour.ShowDialog();
            }
            //查询细部资源权限来源
            else if (xtraTabControlObj.SelectedTabPage == xtraTabPageOtherRes)
            {
                EI.EIInfo inblk = new EI.EIInfo();
                EI.EIInfo outblk;

                inblk.SetColName(1, "subjname");
                inblk.SetColName(2, "resname");
                inblk.SetColName(3, "mode");
                inblk.SetColName(4, "app");
                inblk.SetColName(5, "company");
                inblk.SetColName(6, "userid");
                inblk.SetColName(7, "fname");  //按钮的父画面名
                
                inblk.SetColVal(1, 1, "subjname", this.subjEname);
                inblk.SetColVal(1, 1, "resname", treeListOthRes.FocusedNode.Tag.ToString());
                inblk.SetColVal(1, 1, "mode", 2);
                inblk.SetColVal(1, 1, "app", this.selectedAppname);
                inblk.SetColVal(1, 1, "company", this.selectedCompanyCode);
                inblk.SetColVal(1, 1, "userid",  EF_Args.formUserId);
                inblk.SetColVal(1, 1, "fname", "");

                outblk = EI.EITuxedo.CallService("epesauthsourinq", inblk);

                authsour.result = outblk;
                authsour.user = this.subjEname;
                authsour.res = treeListOthRes.FocusedNode.Tag.ToString();

                authsour.ShowDialog();
            }
        }
        #endregion

        #region 浏览/维护模式
        private void FormESAUTH_EF_PRE_DO_F3(object sender, EF_Args e)
        {
            isManageMode = true;
        }

        private void FormESAUTH_EF_DO_F3(object sender, EF_Args e)
        {
            Manage_OK_CANCEL();
        }

        private void FormESAUTH_EF_CANCEL_DO_F3(object sender, EF_Args e)
        {
            Manage_OK_CANCEL();
        }

        private void Manage_OK_CANCEL()
        {
            if (fgButtonGroupSave.Enabled)
            {
                DialogResult rst = MessageBox.Show(EP.EPES.EPESC0000103/*已修改资源群组权限，是否保存？*/, EP.EPES.EPESC0000024, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (rst == DialogResult.Yes)
                {
                    if (EPESCommon.AuthMode == AUTHMODE.MODE_CLASSIC)
                    {
                        SaveResToGroup();
                        treeListGroup.Focus();
                    }
                    else
                    {
                        SaveResGroupToGroup();
                        TreeListNode node = (treeListResGroup.FocusedNode.Level == 0) ? treeListResGroup.FocusedNode : treeListResGroup.FocusedNode.ParentNode;

                        QryGroupInResGroup(node.GetValue(treeListColumnRGID).ToString());
                    }
                }
                else
                {
                    if (EPESCommon.AuthMode == AUTHMODE.MODE_CLASSIC)
                    {
                        TreeListNode node = xtraTabControlObj.SelectedTabPage == xtraTabPageList ? treeListForm.FocusedNode : treeListRes.FocusedNode;

                        if (node.ImageIndex == FORMICON)
                        {
                            QryFormGroup(node.Tag.ToString());
                            treeListRes.FocusedNode = null;
                            objType = OBJTYPE.FORM;
                            formEname = node.Tag.ToString();
                            buttEname = "";
                        }
                        else if (node.ImageIndex == BUTTICON)
                        {
                            QryButtGroup(node.ParentNode.Tag.ToString(), node.Tag.ToString());
                            treeListRes.FocusedNode = null;
                            objType = OBJTYPE.BUTTON;
                            formEname = node.ParentNode.Tag.ToString();
                            buttEname = node.Tag.ToString();
                        }
                    }
                    else
                    {
                        TreeListNode node = (treeListResGroup.FocusedNode.Level == 0) ? treeListResGroup.FocusedNode : treeListResGroup.FocusedNode.ParentNode;
                        QryGroupInResGroup(node.GetValue(treeListColumnRGID).ToString());
                    }
                }
                fgButtonGroupSave.Enabled = false;
            }

            else if (fgDevCheckEdit1.Checked && fgButtonList.Enabled)
            {
                DialogResult rst = MessageBox.Show(EP.EPES.EPESC0000103/*已修改资源群组权限，是否保存？*/, EP.EPES.EPESC0000024, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (rst == DialogResult.Yes)
                {
                    SaveAuth();
                    fgButtonList.Enabled = false;
                }
                else
                {                   
                    fgButtonList.Enabled = false;

                    if (treeListForm.Nodes.Count == 0)
                    {
                        QryAuthForm();
                    }
                    else
                    {
                        RfgreshFormList();
                    }
                }
            }

            else if (fgButtonTree.Enabled)
            {
                DialogResult rst = MessageBox.Show(EP.EPES.EPESC0000089/*已修改群组资源权限，是否保存？*/, EP.EPES.EPESC0000024, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (rst == DialogResult.Yes)
                {
                    SaveAuth();
                    fgButtonTree.Enabled = false;
                }
                else
                {
                    RfgreshTree();
                    fgButtonTree.Enabled = false;
                }
            }

            else if (fgButtonOthRes.Enabled)
            {
                DialogResult rst = MessageBox.Show(EP.EPES.EPESC0000089/*已修改群组资源权限，是否保存？*/, EP.EPES.EPESC0000024, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (rst == DialogResult.Yes)
                {
                    if (!SaveOthResAuth())
                    {
                        //this.//fg_args.buttonStatusHold = true;
                        return;
                    }
                    else
                    {
                        fgButtonOthRes.Enabled = false;
                    }
                }
                else
                {
                    QryOthResAuth();
                    fgButtonOthRes.Enabled = false;
                }
            }

            else if (fgDevCheckEdit1.Checked && fgButtonResGroup.Enabled)
            {
                DialogResult rst = MessageBox.Show(EP.EPES.EPESC0000089/*已修改群组资源权限，是否保存？*/, EP.EPES.EPESC0000024, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (rst == DialogResult.Yes)
                {
                    if (!SaveGroupToResGroup())
                    {
                        //this.//fg_args.buttonStatusHold = true;
                        return;
                    }
                    else
                    {
                        fgButtonResGroup.Enabled = false;
                    }
                }
                else
                {
                    QryParentResGroup();
                    fgButtonResGroup.Enabled = false;
                }
            }

            isManageMode = false;
        }
        #endregion

        #region 带参数调用
        private void FormEPESAUTH_EF_START_FORM_BY_EF(object sender, EF_Args i_args)
        {
            para_groupname = i_args.callParams[0];
            para_appname = i_args.callParams[1];
        }
        #endregion

        #region 资源组操作
        private void treeListResGroup_BfgoreCheckNode(object sender, DevExpress.XtraTreeList.CheckNodeEventArgs e)
        {
            if (e.Node.Level > 0)
            {
                e.CanCheck = false;
                return;
            }
            if (!isManageMode)
            {
                MessageBox.Show(EP.EPES.EPESC0000091/*非维护模式下无法修改权限！*/, EP.EPES.EPESC0000024, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.CanCheck = false;
                return;
            }
            if (subjType == SUBJTYPE.USER)
            {
                EFMsgInfo = EP.EPES.EPESC0000092/*不能对用户授权，请操作用户所属或所管理群组！*/;
                e.CanCheck = false;
                return;
            }
            else
            {
                e.CanCheck = true;
                e.State = (e.PrevState == CheckState.Checked ? CheckState.Unchecked : CheckState.Checked);
            }
        }

        private void treeListResGroup_AfterCheckNode(object sender, DevExpress.XtraTreeList.NodeEventArgs e)
        {
            fgButtonResGroup.Enabled = true;

            string resgroupid = e.Node.GetValue(treeListColumnRGID).ToString();
            string groupid = GetSubjID();

            if (e.Node.Checked) //勾选资源组
            {
                if (htGroupResRmv.ContainsKey(resgroupid))
                {
                    htGroupResRmv.Remove(resgroupid);
                }
                else if (!htGroupResAdd.ContainsKey(resgroupid))
                {
                    htGroupResAdd.Add(resgroupid, groupid);
                }
            }
            else //取消勾选
            {
                if (htGroupResAdd.Contains(resgroupid))
                {
                    htGroupResAdd.Remove(resgroupid);
                }
                else if (!htGroupResRmv.Contains(resgroupid))
                {
                    htGroupResRmv.Add(resgroupid, groupid);
                }
            }
        }

        private void fgButtonResGroup_Click(object sender, EventArgs e)
        {
            if (fgDevCheckEdit2.Checked)//Query Res Group 
            {
                QueryResGroup();
            }
            else
            {
                SaveGroupToResGroup();
            }
        }

        private void QueryResGroup()
        {
            //this.EFMsgInfo = "";
            this.treeListResGroup.Nodes.Clear();
            this.treeListResGroup.DataSource = null;

            EI.EIInfo inblk = new EI.EIInfo();
            inblk.AddColName(1, "id");
            inblk.AddColName(1, "groupname");
            inblk.AddColName(1, "appname");
            inblk.AddColName(1, "companycode");
            inblk.AddColName(1, "mode");
            inblk.AddColName(1, "inodes");

            inblk.SetColVal(1, 1, "id", "0");
            inblk.SetColVal(1, 1, "appname", this.selectedAppname);
            inblk.SetColVal(1, 1, "groupname", fgtRGName.Text);
            inblk.SetColVal(1, 1, "companycode", this.selectedCompanyCode);
            inblk.SetColVal(1, 1, "inodes", treeListResGroup.Nodes.Count);
            inblk.SetColVal(1, 1, "mode", 1);

            EI.EIInfo outblk = EI.EITuxedo.CallService("epesgrgr_inq", inblk);

            if (outblk.sys_info.flag == 0)
            {
                treeListResGroup.DataSource = outblk.Tables[0];

                //this.EFMsgInfo = EP.EPES.EPESC0000156/*操作成功！*/;
            }
            /*
            EI.EIInfo inBlock = new EI.EIInfo();
            EI.EIInfo outBlock;

            inBlock.SetColName(1, "groupname");
            inBlock.SetColVal(1, "groupname", "");
            inBlock.SetColName(2, "adminuser");
            inBlock.SetColName(2, "adminuser");
            inBlock.SetColVal(1, "adminuser", "");
            inBlock.SetColName(3, "userid");
            inBlock.SetColVal(1, "userid", this.EFUserId);
            inBlock.SetColName(4, "appname");
            inBlock.SetColVal(1, "appname", this.selectedAppname);
            inBlock.SetColName(5, "companycode");
            inBlock.SetColVal(1, "companycode", "");
            inBlock.SetColName(6, "grouptype");
            inBlock.SetColVal(1, "grouptype", 1);

            //调用SERVICE
            outBlock = EI.EITuxedo.CallService("epesgroup_inq2", inBlock);

            for (int i = 0; i < outBlock.Tables[0].Rows.Count; i++)
            {
                string resGroupID = outBlock.Tables[0].Rows[i]["ID"].ToString();
                string resGroupName = outBlock.Tables[0].Rows[i]["NAME"].ToString();
                string resGroupDesc = outBlock.Tables[0].Rows[i]["GROUPDESCRIPTION"].ToString();

                TreeListNode node = treeListResGroup.AppendNode(new object[] { resGroupDesc + "[" + resGroupName + "]" }, null);
                node.Tag = resGroupID;
                node.ImageIndex = node.SelectImageIndex = RESGROUPICON;
            }
            */
            treeListResGroup.FocusedNode = null;
        }

        /// <summary>
        /// 群组到资源模式
        /// </summary>
        private bool SaveGroupToResGroup()
        {
            EI.EIInfo inblk = new EI.EIInfo();
            inblk.AddColName(1, "appname");
            inblk.SetColVal(1, 1, "appname",  EF_Args.epEname);

            if (htGroupResAdd.Count > 0)
            {
                DataTable dt = new DataTable("GROUP_RES_ADD");
                dt.Columns.Add("groupid");
                dt.Columns.Add("resgroupid");

                foreach (System.Collections.DictionaryEntry item in htGroupResAdd)
                {
                    string resgroupid = item.Key.ToString();
                    string groupid = item.Value.ToString();

                    dt.Rows.Add(new object[] { groupid, resgroupid });
                }
                inblk.Tables.Add(dt);
            }
            if (htGroupResRmv.Count > 0)
            {
                DataTable dt = new DataTable("GROUP_RES_RMV");
                dt.Columns.Add("groupid");
                dt.Columns.Add("resgroupid");

                foreach (System.Collections.DictionaryEntry item in htGroupResRmv)
                {
                    string resgroupid = item.Key.ToString();
                    string groupid = item.Value.ToString();

                    dt.Rows.Add(new object[] { groupid, resgroupid });
                }
                inblk.Tables.Add(dt);
            }
            EI.EIInfo outblk = EI.EITuxedo.CallService("epesgrgr_upd", inblk);

            if (outblk.sys_info.flag == 0)
            {
                QryParentResGroup();

                htGroupResAdd.Clear();
                htGroupResRmv.Clear();

                fgButtonResGroup.Enabled = false;

                //this.EFMsgInfo = EP.EPES.EPESC0000156/*操作成功！*/;
                return true;
            }
            else
            {
                MessageBox.Show(outblk.sys_info.msg, EP.EPES.EPESC0000024/*提示*/, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void treeListResGroup_DoubleClick(object sender, EventArgs e)
        {
            TreeListNode fnode = treeListResGroup.FocusedNode;
            if (fnode == null || hiResGroup.HitInfoType == DevExpress.XtraTreeList.HitInfoType.Empty) return;

            if (fnode.Nodes.Count > 0)
            {             
                fnode.Expanded = fnode.Expanded ? false : true;
                return;
            }

            fnode.Nodes.Clear();

            EI.EIInfo inblk = new EI.EIInfo();
            inblk.AddColName(1, "groupid");
            inblk.AddColName(1, "appname");
            inblk.SetColVal(1, 1, "groupid", fnode.GetValue(treeListColumnRGID).ToString());
            inblk.SetColVal(1, 1, "appname", this.selectedAppname);

            EI.EIInfo outblk = EI.EITuxedo.CallService("epesres_inq", inblk);

            if (outblk.sys_info.flag < 0)
            {
                 MessageBox.Show(outblk.sys_info.msg, EP.EPES.EPESC0000024/*提示*/, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                string aclid = "", name = "", description = "", res_name = "", res_type = "";
                for (int i = 0; i < outblk.Tables[0].Rows.Count; i++)
                {
                    aclid = outblk.Tables[0].Rows[i]["aclid"].ToString();
                    name = outblk.Tables[0].Rows[i]["name"].ToString();
                    description = outblk.Tables[0].Rows[i]["description"].ToString();
                    res_name = outblk.Tables[0].Rows[i]["res_name"].ToString();
                    res_type = outblk.Tables[0].Rows[i]["res_type"].ToString();

                    TreeListNode node = treeListResGroup.AppendNode(new object[2] , fnode, CheckState.Indeterminate);
                    node.SetValue(treeListColumnRGName, name + "[" + description + "]");
                    node.SetValue(treeListColumnRGID, aclid);
                    //node.Tag = aclid;
                    switch (res_type)
                    {
                        case "FORM":
                            node.ImageIndex = node.SelectImageIndex = FORMICON;
                            break;
                        case "BUTT":
                            node.ImageIndex = node.SelectImageIndex = BUTTICON;
                            break;
                        case "OTH":
                            node.ImageIndex = node.SelectImageIndex = OTHICON;
                            break;
                    }
                }
                fnode.ExpandAll();
            }

            fnode.ExpandAll();
        }

        private void treeListResGroup_MouseDown(object sender, MouseEventArgs e)
        {
            hiResGroup = treeListResGroup.CalcHitInfo(new Point(e.X, e.Y));
            if (hiResGroup.Node != null)
            {
                treeListResGroup.FocusedNode = hiResGroup.Node;
            }
        }

        private void treeListResGroup_Click(object sender, EventArgs e)
        {
            if (fgDevCheckEdit2.Checked)
            {
                if (hiResGroup == null || hiResGroup.Column == null || hiResGroup.Node == null) return;

                TreeListNode node = (treeListResGroup.FocusedNode.Level == 0)? treeListResGroup.FocusedNode:treeListResGroup.FocusedNode.ParentNode;
                if (node == null) return;

                if (fgButtonGroupSave.Enabled)
                {
                    DialogResult rst = MessageBox.Show(EP.EPES.EPESC0000089/*已修改群组资源权限，是否保存？*/, EP.EPES.EPESC0000024, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (rst == DialogResult.Yes)
                    {
                        SaveResGroupToGroup();
                    }
                    else
                    {
                        htResGroupAdd.Clear();
                        htResGroupRmv.Clear();
                        fgButtonGroupSave.Enabled = false;
                    }
                }

                QryGroupInResGroup(node.GetValue(treeListColumnRGID).ToString());
            }
        }

        private void QryGroupInResGroup(string resGroupID)
        {
            EI.EIInfo inblk = new EI.EIInfo();
            inblk.AddColName(1, "resgroupid");
            inblk.AddColName(1, "appname");
            inblk.AddColName(1, "companycode");
            inblk.AddColName(1, "name");
            inblk.AddColName(1, "adminusername");
            inblk.AddColName(1, "inodes");

            inblk.SetColVal(1, "resgroupid", resGroupID);
            inblk.SetColVal(1, "appname", this.selectedAppname);
            string comp = EPESCommon.AuthMode == AUTHMODE.MODE_9672 ? "" : this.selectedCompanyCode;
            inblk.SetColVal(1, "companycode", comp);
            inblk.SetColVal(1, "name", fgtGEname.Text);
            inblk.SetColVal(1, "adminusername", fgtGAdmin.Text);
            inblk.SetColVal(1, "inodes", treeListGroup.Nodes.Count);

            EI.EIInfo outblk = EI.EITuxedo.CallService("epesresgr_inq", inblk);

            if (outblk.sys_info.flag == 0)
            {
                Hashtable ht = new Hashtable();
                if (treeListGroup.Nodes.Count == 0)
                {
                    string groupID = "", groupName = "", groupDesc = "", groupAdmin1 = "", groupAdmin2 = "";
                    for (int i = 0; i < outblk.Tables[0].Rows.Count; i++)
                    {
                        groupID = outblk.Tables[0].Rows[i]["id"].ToString();
                        groupName = outblk.Tables[0].Rows[i]["name"].ToString();
                        groupDesc = outblk.Tables[0].Rows[i]["description"].ToString();
                        groupAdmin1 = outblk.Tables[0].Rows[i]["adminuserename1"].ToString();
                        groupAdmin2 = outblk.Tables[0].Rows[i]["adminuserename2"].ToString();

                        TreeListNode node = treeListGroup.AppendNode(new object[] { groupName, groupDesc, groupAdmin1, groupAdmin2, groupID }, null);
                        node.Tag = groupID;
                    }
                }

                for (int i = 0; i < outblk.Tables[1].Rows.Count; i++)
                {
                    ht.Add(outblk.Tables[1].Rows[i]["id"].ToString(), outblk.Tables[1].Rows[i]["name"].ToString());
                }

                foreach (TreeListNode node in treeListGroup.Nodes)
                {
                    if (node.Level == 0)
                    {
                        node.Checked = ht.ContainsKey(node.Tag.ToString()) ? true : false;
                    }
                }
            }
            else
            {
                MessageBox.Show(outblk.sys_info.msg, EP.EPES.EPESC0000024/*提示*/, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region 画面按钮全选/全不选
        private void barButtonItemSelAll_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (xtraTabControlObj.SelectedTabPage == xtraTabPageList)//画面列表
            {
                if (!treeListForm.FocusedNode.Checked)
                {
                    treeListForm.FocusedNode.CheckState = CheckState.Checked;
                    AfterCheckNode(treeListForm.FocusedNode);
                }
                CheckNodes(treeListForm.FocusedNode.Nodes, CheckState.Checked);
            }
            else if (xtraTabControlObj.SelectedTabPage == xtraTabPageTree)//菜单树
            {
                if (treeListRes.FocusedNode.ImageIndex == FOLDERICON_EXPAND)
                {
                    CheckNodes(treeListRes.FocusedNode.Nodes, CheckState.Checked);
                }
                else if (treeListRes.FocusedNode.ImageIndex == FORMICON)
                {
                    if (!treeListRes.FocusedNode.Checked)
                    {
                        treeListRes.FocusedNode.CheckState = CheckState.Checked;
                        AfterCheckNode(treeListRes.FocusedNode);
                    }
                    CheckNodes(treeListRes.FocusedNode.Nodes, CheckState.Checked);
                }
            }
        }

        private void CheckNodes(TreeListNodes nodes, CheckState state)
        {
            foreach (TreeListNode node in nodes)
            {
                if (node.ImageIndex == FOLDERICON || node.ImageIndex == FOLDERICON_EXPAND)
                    continue;

                node.CheckState = state;

                AfterCheckNode(node);
            }
        }

        private void AfterCheckNode(TreeListNode node)
        {
            SetSaveButtonEnable();

            if (node.ImageIndex == FORMICON)
            {
                string formName = node.Tag.ToString();
                if (node.Checked)
                {
                    if (listFormRevok.Contains(formName))
                    {
                        listFormRevok.Remove(formName);
                    }
                    else if (!listFormGrant.Contains(formName))
                    {
                        listFormGrant.Add(formName);
                    }
                }
                else
                {
                    if (listFormGrant.Contains(formName))
                    {
                        listFormGrant.Remove(formName);
                    }
                    else if (!listFormRevok.Contains(formName))
                    {
                        listFormRevok.Add(formName);
                    }

                    foreach (TreeListNode nd in node.Nodes)
                    {
                        if (nd.Checked)
                        {
                            nd.CheckState = CheckState.Unchecked;
                            string key = formName + "," + nd.Tag.ToString();
                            if (listButtGrant.Contains(key))
                            {
                                listButtGrant.Remove(key);
                            }
                            else if (!listButtRevok.Contains(key))
                            {
                                listButtRevok.Add(key);
                            }
                        }
                    }
                }
            }
            else if (node.ImageIndex == BUTTICON)
            {
                string formName = node.ParentNode.Tag.ToString();
                string buttName = node.Tag.ToString();
                string key = formName + "," + buttName;

                if (node.Checked)
                {
                    if (listButtRevok.Contains(key))
                    {
                        listButtRevok.Remove(key);
                    }
                    else if (!listButtGrant.Contains(key))
                    {
                        listButtGrant.Add(key);
                    }
                }
                else
                {
                    if (listButtGrant.Contains(key))
                    {
                        listButtGrant.Remove(key);
                    }
                    else if (!listButtRevok.Contains(key))
                    {
                        listButtRevok.Add(key);
                    }
                }
            }
        }

        private void SetSaveButtonEnable()
        {
            if (xtraTabControlObj.SelectedTabPage == xtraTabPageList)
            {
                fgButtonList.Enabled = true;
            }
            else if (xtraTabControlObj.SelectedTabPage == xtraTabPageTree)
            {
                fgButtonTree.Enabled = true;
            }
        }

        private void barButtonItemUnSelAll_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (xtraTabControlObj.SelectedTabPage == xtraTabPageList)
            {
                CheckNodes(treeListForm.FocusedNode.Nodes, CheckState.Unchecked);
            }
            else if (xtraTabControlObj.SelectedTabPage == xtraTabPageTree)
            {
                CheckNodes(treeListRes.FocusedNode.Nodes, CheckState.Unchecked);
            }
        }
        #endregion

        private void fgButtonRfgreshRG_Click(object sender, EventArgs e)
        {
            treeListResGroup.Nodes.Clear();
            QryParentResGroup();
        }

        private void comboOthResType_SelectedIndexChanged(object sender, EventArgs e)
        {
            QryOthResAuth();
        }



        //private string GetSubjEname()
        //{
        //    string subjEname = "";
        //    if (xtraTabControlSubj.SelectedTabPage == xtraTabPageGroup)
        //    {
        //        if (treeListGroup.FocusedNode != null)
        //        {
        //            subjEname = treeListGroup.FocusedNode.GetDisplayText(treeListColumn2);
        //        }
        //    }
        //    else if(xtraTabControlSubj.SelectedTabPage == xtraTabPageUser)
        //    {
        //        if (treeListUser.FocusedNode != null)
        //        {
        //            subjEname = treeListUser.FocusedNode.GetDisplayText(0);
        //        }
        //    }
        //    return subjEname;
        //}


    }
}
