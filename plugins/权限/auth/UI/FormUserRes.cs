using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraTreeList.Nodes;
using auth.Services;
using EF;
using DevExpress.XtraGrid.Views.Grid;
using System.Collections;

namespace auth.UI
{
    public partial class FormUserRes : DevExpress.XtraEditors.XtraForm
    {
        #region
 

        //是否维护模式
        private bool isManageMode = false;

        //图标编号
        private const int GROUPICON = 0;
        private const int GROUPICON_GRAY = 1;
        private const int USERICON = 2;

        private const int SAVE = 13;
        private const int DISCARD = 14;

        private const int INITPASS = 13;
        private const int USERSAVE = 14;
        private const int USERDISCARD = 15;
 
 

        //修改权限主客体
        private List<string> listFormGrant = new List<string>();
        private List<string> listFormRevok = new List<string>();
        private List<string> listButtGrant = new List<string>();
        private List<string> listButtRevok = new List<string>();
 
        //主客体
        private enum SUBJTYPE { GROUP, USER, NOAUTHGROUP, NULL };
        private enum OBJTYPE { FORM, BUTTON };

        private SUBJTYPE subjType;
        private string subjEname = "0";
        private string subjDescript = "";

        //节点
        private List<string> nodeName = new List<string>();
        private Dictionary<string, Dictionary<string, object>> auth = new Dictionary<string, Dictionary<string, object>>();
        private Dictionary<string, object> forminfo = new Dictionary<string, object>();

        //图标编号
        private const int FORMICON = 0;
        private const int BUTTICON = 1;
        //imageCollection15
        private const int GROUPICON2 = 5;
 
        //树和列表HitInfo
        DevExpress.XtraTreeList.TreeListHitInfo hiTreeList = null;
       
        DevExpress.XtraTreeList.TreeListHitInfo hiGroup = null;
 
        #endregion

        public FormUserRes()
        {
            InitializeComponent();
            barButtonItemSave.Enabled = false;
            barButtonItemCancel.Enabled = false;
        }
 
        #region 查询
        private DataTable queryGroup()
        {
            DataSet inBlock = new DataSet();
            inBlock.Tables.Add();
            DataRow dr = inBlock.Tables[0].NewRow();
            inBlock.Tables[0].Columns.Add("groupname");
            inBlock.Tables[0].Columns.Add("adminuser");
            inBlock.Tables[0].Columns.Add("userid");
            inBlock.Tables[0].Columns.Add("appname");
            inBlock.Tables[0].Columns.Add("grouptype");

            dr["groupname"] = "";
            dr["adminuser"] = ""; 
            dr["userid"] = CConstString.UserId;
            dr["appname"] = CConstString.AppName;
            dr["grouptype"] = 1;
            inBlock.Tables[0].Rows.Add(dr);
            //调用SERVICE
            DataTable outBlock = DbUserInfo.QueryGroupInfo(inBlock, CConstString.ConnectName);
            return outBlock;
        }

 
        //查询群组的子组和子用户
        private void queryMember(TreeListNode parentNode)
        {
            parentNode.Nodes.Clear();
            DataSet inBlock = new DataSet();
            inBlock.Tables.Add();
            inBlock.Tables[0].Columns.Add("groupname");
            inBlock.Tables[0].Rows.Add(((List<string>)parentNode.Tag)[0]);
 
            //调用SERVICE
            DataSet outBlock =DbUserInfo.QueryGroupChild(inBlock,CConstString.ConnectName);
        }

        private void QryFormButtInList(string formName)
        {
            DataTable outblks = QryButtAuth(formName);

            string buttName = "";
            string buttDesc = "";
            for (int i = 0; i < outblks.Rows.Count; i++)
            {
                buttName = outblks.Rows[i]["name"].ToString();
                buttDesc = outblks.Rows[i]["description"].ToString();
                TreeListNode treeNode;
                if (outblks.Rows[i]["num"].ToString() == "0")
                {
                    treeNode = this.treeListForm.AppendNode(new object[] { buttDesc, buttName }, this.treeListForm.FocusedNode, CheckState.Unchecked);
                }
                else
                {
                    treeNode = this.treeListForm.AppendNode(new object[] { buttDesc, buttName }, this.treeListForm.FocusedNode, CheckState.Checked);
                }
                treeNode.Tag = outblks.Rows[i]["aclid"];
                treeNode.SelectImageIndex = treeNode.ImageIndex = BUTTICON;
            }
        }

        private DataTable QryButtAuth(string formid)
        {
            DataSet inblks = new DataSet();
            inblks.Tables.Add();
            int mode = -1;

            //user mode
            if (subjType == SUBJTYPE.USER) mode = 2;
            else mode = 1;

            inblks.Tables[0].Columns.Add("id");
            inblks.Tables[0].Columns.Add("formid");
            inblks.Tables[0].Columns.Add("mode");
            inblks.Tables[0].Columns.Add("appname");
            inblks.Tables[0].Rows.Add(subjEname, formid, mode, CConstString.AppName);

            DataTable outblks = DbUserRes.QueryButtonAuth(inblks, CConstString.ConnectName);
            return outblks;
        }

        private void QryForms()
        {
            treeListForm.Nodes.Clear();

            DataSet inblk = new DataSet();
            inblk.Tables.Add();
            inblk.Tables[0].Columns.Add("name");
            inblk.Tables[0].Columns.Add("dllname");
            inblk.Tables[0].Columns.Add("appname");
            inblk.Tables[0].Rows.Add(fgtFormName.Text, "", CConstString.AppName);

            DataTable dt = DbResource.QueryFormInfo(inblk, CConstString.ConnectName);

            //outBlock = EI.EITuxedo.CallService("epesformlistinq", inBlock);

            string formName = "";
            string formDesc = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                formName = dt.Rows[i]["NAME"].ToString();
                formDesc = dt.Rows[i]["DESCRIPTION"].ToString();

                TreeListNode tnode = this.treeListForm.AppendNode(new object[] { formDesc, formName }, null);
                tnode.Tag = dt.Rows[i]["ACLID"];
                tnode.SelectImageIndex = tnode.ImageIndex = FORMICON;
            }
            treeListForm.FocusedNode = null;
        }
        #endregion

        #region others
        private void SaveAuth()
        {
            if (listFormGrant.Count > 0 || listFormRevok.Count > 0)
            {
                DataSet inblk = new DataSet();
                inblk.Tables.Add();

                inblk.Tables[0].Columns.Add("groupid");
                inblk.Tables[0].Columns.Add("formaclid");
                inblk.Tables[0].Columns.Add("mode");
                inblk.Tables[0].Columns.Add("username");
                inblk.Tables[0].Columns.Add("appname");

                for (int i = 0; i < listFormGrant.Count; i++)
                {
                    inblk.Tables[0].Rows.Add(subjEname, listFormGrant[i], "insert", CConstString.UserId, CConstString.AppName);
                }
                for (int i = 0; i < listFormRevok.Count; i++)
                {
                    inblk.Tables[0].Rows.Add(subjEname, listFormRevok[i], "delete", CConstString.UserId, CConstString.AppName);
                }
                DbUserRes.UpdateFormAccess(inblk, CConstString.ConnectName);
            }


            if (listButtGrant.Count > 0 || listButtRevok.Count > 0)
            {
                DataSet inblk = new DataSet();
                inblk.Tables.Add();
                inblk.Tables[0].Columns.Add("groupid");
                inblk.Tables[0].Columns.Add("buttonid");
                inblk.Tables[0].Columns.Add("mode");
                for (int i = 0; i < listButtGrant.Count; i++)
                {
                    inblk.Tables[0].Rows.Add(subjEname, listButtGrant[i], "insert");
                }

                for (int i = 0; i < listButtRevok.Count; i++)
                {
                    inblk.Tables[0].Rows.Add(subjEname, listButtRevok[i], "delete");
                }

                DbUserRes.UpdateButtonAccess(inblk, CConstString.ConnectName);
            }


            //刷新结果

            RefreshFormList();


            listFormGrant.Clear();
            listFormRevok.Clear();
            listButtGrant.Clear();
            listButtRevok.Clear();
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

        private bool IsChanged()
        {
            if (isManageMode)
            {
                if (listFormGrant.Count > 0 || listFormRevok.Count > 0 
                    ||listButtGrant.Count > 0 ||  listButtRevok.Count>0)
                {
                    return true;
                }
            }
            return false;
        }
        private void RefreshResList(TreeListNode node)
        {
            if (IsChanged())
            {
                DialogResult rst = EFMessageBox.Show(EP.EPES.EPESC0000089/*已修改群组资源权限，是否保存？*/, EP.EPES.EPESC0000024, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (rst == DialogResult.Yes)
                {
                    SaveAuth();
                }
            }
            listFormGrant.Clear();
            listFormRevok.Clear();
            listButtGrant.Clear();
            listButtRevok.Clear();

            subjType = GetSubjType(node);
            if (node.Tag != null)
            {
                subjEname = node.Tag.ToString();
            }
            else
            {
                subjEname = treeListUser.FocusedNode.GetValue("ID").ToString();
            }
            subjDescript = node.GetDisplayText(2) == null ? "" : node.GetDisplayText(2);
            if (treeListForm.Nodes.Count == 0)
            {
                QryAuthForm();
            }
            else
            {
                RefreshFormList();
            }
        }
        #endregion

        #region 单击画面列表
        private void treeListForm_MouseDown(object sender, MouseEventArgs e)
        {
            hiTreeList = treeListForm.CalcHitInfo(new Point(e.X, e.Y));
            if (hiTreeList.Node != null)
            {
                treeListForm.FocusedNode = hiTreeList.Node;
            }
        }
        #endregion

        #region 双击画面列表
        private void treeListForm_DoubleClick(object sender, EventArgs e)
        {
            TreeListNode node = treeListForm.FocusedNode;
            if (node == null || hiTreeList.HitInfoType == DevExpress.XtraTreeList.HitInfoType.Empty) return;

            node.Nodes.Clear();
            QryFormButtInList(node.Tag.ToString());
            node.ExpandAll();
        }
        #endregion
 
        #region 勾选画面列表节点
        private void treeListForm_BeforeCheckNode(object sender, DevExpress.XtraTreeList.CheckNodeEventArgs e)
        {
            if (!isManageMode)
            {
                EFMessageBox.Show(EP.EPES.EPESC0000091/*非维护模式下无法修改权限！*/, EP.EPES.EPESC0000024, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.CanCheck = false;
                return;
            }
            if (subjType == SUBJTYPE.USER)
            {
                EFMessageBox.Show("不能对用户授权，请操作用户所属或所管理群组！");
                e.CanCheck = false;
            }
            else if (e.Node.ImageIndex == BUTTICON && !e.Node.ParentNode.Checked)
            {
                EF.EFMessageBox.Show("请先勾选按钮对应的画面！");
                e.CanCheck = false;
            }
            else
            {
                e.CanCheck = true;
            }
        }

        private void treeListForm_AfterCheckNode(object sender, DevExpress.XtraTreeList.NodeEventArgs e)
        {
            if (e.Node.ImageIndex == FORMICON)
            {
                string formName = e.Node.Tag.ToString();
                if (e.Node.Checked)
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

                    foreach (TreeListNode node in e.Node.Nodes)
                    {
                        if (node.Checked)
                        {
                            node.CheckState = CheckState.Unchecked;
                            string key = formName + "," + node.Tag.ToString();
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
            else if (e.Node.ImageIndex == BUTTICON)
            {
                //string formName = e.Node.ParentNode.Tag.ToString();
                string key = e.Node.Tag.ToString();
             
                if (e.Node.Checked)
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
        #endregion

        #region 单击 用户列表
        private void treeListUser_MouseDown(object sender, MouseEventArgs e)
        {
            DevExpress.XtraTreeList.TreeListHitInfo hiUser = treeListUser.CalcHitInfo(new Point(e.X, e.Y));
            if (hiUser.Node != null)
            {
              //  treeListUser.FocusedNode = hiUser.Node;
            }
        }
 
        private void treeListUser_AfterFocusNode(object sender, DevExpress.XtraTreeList.NodeEventArgs e)
        {
            if (treeListUser.Nodes.Count == 0) return;
            TreeListNode node = treeListUser.FocusedNode;
            RefreshResList(node);
        }
        #endregion

        #region 双击用户查询父组
        private void treeListUser_DoubleClick(object sender, EventArgs e)
        {
            TreeListNode node = treeListUser.FocusedNode;
            if (node == null) return;
            node.Nodes.Clear();
            string memberid = "";
            if (node.Tag != null)
            {
                memberid = node.Tag.ToString();
            }
            else
            {
                memberid = node.GetValue("ID").ToString();
            }


            // if (node.ImageIndex == USERICON)
            // else if (node.ImageIndex == GROUPICON)
            DataSet inblk = new DataSet();
            inblk.Tables.Add();
            inblk.Tables[0].Columns.Add("memberid");
            inblk.Tables[0].Columns.Add("appname");

            inblk.Tables[0].Rows.Add(memberid, CConstString.AppName);


            //查询用户所属/所管理的群组
            DataTable outblk = DbUserInfo.QueryGroupByMember(inblk, CConstString.ConnectName);


            for (int i = 0; i < outblk.Rows.Count; i++)
            {
                string groupId = outblk.Rows[i]["id"].ToString();
                string groupName = outblk.Rows[i]["name"].ToString();
                string groupDesc = outblk.Rows[i]["groupdescription"].ToString();

                TreeListNode treeNode;

                treeNode = this.treeListUser.AppendNode(new object[3], node);

                node.Nodes[i].SetValue(0, groupId);
                node.Nodes[i].SetValue(1, groupName);
                node.Nodes[i].SetValue(2, groupDesc);


                treeNode.Tag = groupId;
                treeNode.ImageIndex = treeNode.SelectImageIndex = GROUPICON;
            }
            treeListUser.FocusedNode.ExpandAll();
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
 
        private void treeListGroup_AfterFocusNode(object sender, DevExpress.XtraTreeList.NodeEventArgs e)
        {
            if (treeListGroup.FocusedNode == null) return;

            RefreshResList(treeListGroup.FocusedNode);
        }

        private void QryAuthForm()
        {
            if (subjEname == string.Empty  )
            {
                return;
            }
            DataSet inblks = new DataSet();
            inblks.Tables.Add();
            int mode = -1;

            //user mode
            if (subjType == SUBJTYPE.USER) mode = 2;
            else mode = 1;

            inblks.Tables[0].Columns.Add("name");
            inblks.Tables[0].Columns.Add("descript");
            inblks.Tables[0].Columns.Add("groupid");
            inblks.Tables[0].Columns.Add("mode");
            inblks.Tables[0].Columns.Add("appname");
            inblks.Tables[0].Rows.Add("","",subjEname, mode, CConstString.AppName);

            DataTable outBlock = DbUserRes.QueryFormList(inblks, CConstString.ConnectName);

            string formName = "";
            string formDesc = "";
            string cnt = "";
            for (int i = 0; i < outBlock.Rows.Count; i++)
            {
                formName = outBlock.Rows[i]["NAME"].ToString();
                formDesc = outBlock.Rows[i]["DESCRIPTION"].ToString();
                cnt = outBlock.Rows[i]["CNT"].ToString();

                if (cnt == "0")
                {
                    TreeListNode tnode = this.treeListForm.AppendNode(new object[] { formDesc, formName }, null, CheckState.Unchecked);
                    tnode.Tag = outBlock.Rows[i]["ACLID"];
                    tnode.SelectImageIndex = tnode.ImageIndex = FORMICON;
                }
                else
                {
                    TreeListNode tnode = this.treeListForm.AppendNode(new object[] { formDesc, formName }, null, CheckState.Checked);
                    tnode.Tag = outBlock.Rows[i]["ACLID"];
                    tnode.SelectImageIndex = tnode.ImageIndex = FORMICON;
                }
            }
            treeListForm.FocusedNode = null;
        }

        private void efButtonRef_Click(object sender, EventArgs e)
        {
            treeListForm.Nodes.Clear();
            listFormGrant.Clear();
            listFormRevok.Clear();
            listButtGrant.Clear();
            listButtRevok.Clear();

            QryAuthForm();
        }
        /// <summary>
        /// 递归获取画面名列表
        /// </summary>
        /// <param name="tc">树节点</param>
        private void GetFormName(DevExpress.XtraTreeList.Nodes.TreeListNodes tc)
        {
            foreach (DevExpress.XtraTreeList.Nodes.TreeListNode node in tc)
            {
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
                if (node.ImageIndex == FORMICON)
                {
                    SetNodeCheck(node.Nodes);
                }
                if (node.ImageIndex == FORMICON) //form
                {
                    if (forminfo.ContainsKey(node.Tag.ToString()))
                    {
                        node.CheckState = forminfo[node.Tag.ToString()].ToString() == "0" ? CheckState.Unchecked : CheckState.Checked;
                    }
                    else
                    {
                        node.CheckState = CheckState.Unchecked;
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
                    else
                    {

                        node.CheckState = CheckState.Unchecked;
                    }
                }
            }
        }
        private void RefreshFormList()
        {
            nodeName.Clear();
            GetFormName(treeListForm.Nodes);

            int mode = -1;
            //user mode
            if (subjType == SUBJTYPE.USER) mode = 2;
            //group mode
            else mode = 1;

            layoutControlGroup4.Text =((mode==1 ?"群组[":"用户[")+ subjDescript + "]可访问资源");

            DataSet inblks = new DataSet();
            inblks.Tables.Add();
            inblks.Tables[0].Columns.Add("id");
            inblks.Tables[0].Columns.Add("formname");
            inblks.Tables[0].Columns.Add("mode");
            inblks.Tables[0].Columns.Add("appname");

            for (int i = 0; i < nodeName.Count; i++)
            {
                inblks.Tables[0].Rows.Add(subjEname, nodeName[i], mode, CConstString.AppName);
            }
            DataTable outblks = DbUserRes.QueryFormAuth(inblks, CConstString.ConnectName);
            
 

            forminfo.Clear();
            auth.Clear();

            for (int j = 0; j <  outblks.Rows.Count; j++)
            {
                string formname = outblks.Rows[j]["formname"].ToString(); ;
                string formid = outblks.Rows[j]["aclid"].ToString();  
                string buttname = outblks.Rows[j]["buttname"].ToString();
                string buttid = outblks.Rows[j]["buttid"].ToString(); 
                string formcount = outblks.Rows[j]["formcount"].ToString();
                string buttcount = outblks.Rows[j]["buttcount"].ToString();

                if (!forminfo.ContainsKey(formid))
                {
                    forminfo.Add(formid, formcount);

                    if (!auth.ContainsKey(formid))
                    {
                        auth.Add(formid, new Dictionary<string, object>());
                    }
                }
                if (buttname.Trim().Length > 0 && !((Dictionary<string, object>)auth[formid]).ContainsKey(buttid))
                {
                    ((Dictionary<string, object>)auth[formid]).Add(buttid, buttcount);
                }
            }

            SetNodeCheck(treeListForm.Nodes);
        }
        #endregion
     
        #region 双击节点查询子组用户
        private void treeListGroup_DoubleClick(object sender, EventArgs e)
        {
            TreeListNode node = treeListGroup.FocusedNode;
            if (node == null || node.ImageIndex == USERICON) return;

            node.Nodes.Clear();

            treeListGroup.ClearSorting();

            DataSet inBlock = new DataSet();
            inBlock.Tables.Add();


            inBlock.Tables[0].Columns.Add("groupid");
            inBlock.Tables[0].Rows.Add(node.GetValue("ID"));
 

            //调用SERVICE
            DataSet  outBlock = DbUserInfo.QueryGroupChild(inBlock,CConstString.ConnectName);

            string groupName = "";
            string groupDesc = "";
            string groupId = "";
            int i = 0;
            for (i = 0; i < outBlock.Tables[0].Rows.Count; i++)
            {
                groupId = outBlock.Tables[0].Rows[i]["id"].ToString();
                groupName = outBlock.Tables[0].Rows[i]["name"].ToString();
                groupDesc = outBlock.Tables[0].Rows[i]["groupdescription"].ToString();
 
                TreeListNode treeNode;
             
                treeNode = this.treeListGroup.AppendNode(new object[3], node, CheckState.Checked);

                node.Nodes[i].SetValue(0, groupId);
                node.Nodes[i].SetValue(1, groupName);
                node.Nodes[i].SetValue(2, groupDesc);

                treeNode.Tag = groupId;

                treeNode.ImageIndex = treeNode.SelectImageIndex = GROUPICON;

            }

            string ename = "";
            string cname = "";
            string id = "";
            for (int j = 0; j < outBlock.Tables[1].Rows.Count; j++)
            {
                id = outBlock.Tables[1].Rows[j]["id"].ToString(); 
                ename = outBlock.Tables[1].Rows[j]["ename"].ToString(); 
                cname = outBlock.Tables[1].Rows[j]["cname"].ToString();   
                TreeListNode treeNode = this.treeListGroup.AppendNode(new object[3], node, CheckState.Indeterminate);
                node.Nodes[i + j].SetValue(0, id);
                node.Nodes[i + j].SetValue(1, ename);
                node.Nodes[i + j].SetValue(2, cname);
                treeNode.Tag = id;
                treeNode.SelectImageIndex = treeNode.ImageIndex = USERICON;
            }
            treeListGroup.FocusedNode.ExpandAll();
        }

        #endregion

        #region 查询群组/用户 按钮
        private void QryGroup_Click(object sender, EventArgs e)
        {
            treeListGroup.Nodes.Clear();
            DataTable outBlock = queryGroup();
            treeListGroup.DataSource = outBlock;
        }
 
        private void btnQueryRes_Click(object sender, EventArgs e)
        {
            treeListForm.Nodes.Clear();
            listFormGrant.Clear();
            listFormRevok.Clear();
            listButtGrant.Clear();
            listButtRevok.Clear();

            QryAuthForm();
        }
   
        private void QryUser_Click(object sender, EventArgs e)
        {
 
            this.treeListUser.Nodes.Clear();
            DataTable outblk = DbUserInfo.QueryUserInfo(new DataSet(), CConstString.ConnectName); ;
            treeListUser.DataSource = outblk;
            for (int i = 0; i < treeListUser.Nodes.Count; i++)
            {
                treeListUser.Nodes[i].SelectImageIndex = treeListUser.Nodes[i].ImageIndex =  USERICON;
            }
        }
 
        #endregion

        #region group and user tab change
        private void tabbedControlGroup1_SelectedPageChanged(object sender, DevExpress.XtraLayout.LayoutTabPageChangedEventArgs e)
        {
            if (e.Page == layoutControlGroupGroup)
            {
                if (treeListGroup.FocusedNode != null)
                {
                    switch (treeListGroup.FocusedNode.SelectImageIndex)
                    {
                        case GROUPICON:
                            subjType = SUBJTYPE.GROUP;
                            break;
                        case USERICON:
                            subjType = SUBJTYPE.USER;
                            break;
                    }
                    subjEname = treeListGroup.FocusedNode.GetValue("ID").ToString();
                }
            }
            else if (e.Page == layoutControlGroupUser)
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
                    subjEname = treeListGroup.FocusedNode.GetValue("ID").ToString();
                }
            }
        }
        #endregion

        #region 工具条( 编辑 保存 取消)
        private void barButtonItemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                SaveAuth();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
                return;
            }
            barButtonItemEdit.Enabled = true;
            barButtonItemSave.Enabled = false;
            barButtonItemCancel.Enabled = false;
        }

        private void barButtonItemEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                isManageMode = true;

                barButtonItemEdit.Enabled = false;
                barButtonItemSave.Enabled = true;
                barButtonItemCancel.Enabled = true;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }

        private void barButtonItemCancel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                barButtonItemEdit.Enabled = false;
                isManageMode = false;
                barButtonItemEdit.Enabled = true;
                barButtonItemSave.Enabled = false;
                barButtonItemCancel.Enabled = false;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }
        #endregion
 
    }
}