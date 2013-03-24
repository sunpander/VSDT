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
using DevExpress.XtraBars;

namespace auth.UI
{
    public partial class FormUsers : DevExpress.XtraEditors.XtraForm
    {
        private TreeListNode parent = null;

        //是否维护模式
        private bool isManageMode = false;

        //图标编号
        private const int GROUPICON = 0;
        //private const int GROUPICON_GRAY = 1;
        private const int USERICON = 2;

        private const int SAVE = 13;
        private const int DISCARD = 14;

        //群组信息相关变量
        //private int groupSaveStatus = 0;//群组信息："0"——不需保存；"1"——新增保存；"2"——修改群组信息保存；"3"——修改群组管理员保存
        //private int userSaveStatus = 0;//用户信息："0"——不需保存；"1"——新增保存；"2"——修改用户信息保存；"3"——修改用户部门信息保存
  
        public FormUsers()
        {
            InitializeComponent();
            this.InitDevGridCustomButtons();
            SetPageState(false);
            this.efDevGridGroupInfo.EmbeddedNavigator.ButtonClick += new NavigatorButtonClickEventHandler(GroupInfoEmbeddedNavigator_ButtonClick);
            barButtonItemCancel.Enabled = false;
            barButtonItemSave.Enabled = false;
        }

        #region 其他方法
        /// <summary>
        /// 初始化grid下方按钮
        /// </summary>
        private void InitDevGridCustomButtons()
        {
            efDevGridUserInfo.IsUseCustomPageBar = true;
            efDevGridGroupInfo.IsUseCustomPageBar = true;
            efDevGridDeptInfo.IsUseCustomPageBar = true;
 
            ((ImageList)this.efDevGridGroupInfo.EmbeddedNavigator.Buttons.ImageList).Images.Add(this.imageListGridPageBar.Images[1]);
            ((ImageList)this.efDevGridGroupInfo.EmbeddedNavigator.Buttons.ImageList).Images.Add(this.imageListGridPageBar.Images[2]);

            this.efDevGridGroupInfo.EmbeddedNavigator.Buttons.CustomButtons.AddRange(new NavigatorCustomButton[] {
            new NavigatorCustomButton(SAVE, EP.EPES.EPESC0000079/*保存*/),
            new NavigatorCustomButton(DISCARD, EP.EPES.EPESC0000080/*放弃*/)            
            });

            efDevGridGroupInfo.EmbeddedNavigator.Buttons.CustomButtons[DISCARD].Visible = false;
            efDevGridGroupInfo.EmbeddedNavigator.Buttons.CustomButtons[SAVE].Visible = false;


            ((ImageList)this.efDevGridUserInfo.EmbeddedNavigator.Buttons.ImageList).Images.Add(this.imageListGridPageBar.Images[1]);
            ((ImageList)this.efDevGridUserInfo.EmbeddedNavigator.Buttons.ImageList).Images.Add(this.imageListGridPageBar.Images[2]);

            this.efDevGridUserInfo.EmbeddedNavigator.Buttons.CustomButtons.AddRange(new NavigatorCustomButton[] {
            new NavigatorCustomButton(SAVE, EP.EPES.EPESC0000079/*保存*/),
            new NavigatorCustomButton(DISCARD, EP.EPES.EPESC0000080/*放弃*/)            
            });

            efDevGridUserInfo.EmbeddedNavigator.Buttons.CustomButtons[DISCARD].Visible = false;
            efDevGridUserInfo.EmbeddedNavigator.Buttons.CustomButtons[SAVE].Visible = false;

            ((ImageList)this.efDevGridDeptInfo.EmbeddedNavigator.Buttons.ImageList).Images.Add(this.imageListGridPageBar.Images[1]);
            ((ImageList)this.efDevGridDeptInfo.EmbeddedNavigator.Buttons.ImageList).Images.Add(this.imageListGridPageBar.Images[2]);

            this.efDevGridDeptInfo.EmbeddedNavigator.Buttons.CustomButtons.AddRange(new NavigatorCustomButton[] {
                   new NavigatorCustomButton(SAVE, EP.EPES.EPESC0000079/*保存*/),
                   new NavigatorCustomButton(DISCARD, EP.EPES.EPESC0000080/*放弃*/)              
                   });

            efDevGridDeptInfo.EmbeddedNavigator.Buttons.CustomButtons[DISCARD].Visible = false;
            efDevGridDeptInfo.EmbeddedNavigator.Buttons.CustomButtons[SAVE].Visible = false;

        }

        /// <summary>
        /// 设置画面状态
        /// </summary>
        /// <param name="blEdit"></param>
        private void SetPageState(bool blEdit)
        {
            gridViewGroupInfo.OptionsBehavior.Editable = blEdit;
            gridViewDeptInfo.OptionsBehavior.Editable = blEdit;
            gridViewUserInfo.OptionsBehavior.Editable = blEdit;

            barButtonItemEdit.Enabled = !blEdit;
            barButtonItemSave.Enabled = blEdit;
            barButtonItemCancel.Enabled = blEdit;

            efDevGridGroupInfo.ShowAddRowButton = blEdit;
            efDevGridGroupInfo.ShowAddCopyRowButton = blEdit;
            efDevGridGroupInfo.ShowDeleteRowButton = blEdit;
            efDevGridGroupInfo.SetAllColumnEditable(blEdit);

            efDevGridGroupInfo.ShowSelectionColumn = blEdit;
            efDevGridUserInfo.ShowSelectionColumn = blEdit;

            efDevGridGroupInfo.EmbeddedNavigator.Buttons.CustomButtons[DISCARD].Visible = blEdit;
            efDevGridGroupInfo.EmbeddedNavigator.Buttons.CustomButtons[SAVE].Visible = blEdit;


            //  efDevGridUserInfo.SelectionColumn.OptionsColumn.AllowEdit = true;

            efDevGridGroupInfo.ShowContextMenu = blEdit;

            //gridViewGroupInfo_FocusedRowChanged(null, null);

            efDevGridUserInfo.ShowAddRowButton = blEdit;
            efDevGridUserInfo.ShowAddCopyRowButton = blEdit;
            efDevGridUserInfo.ShowDeleteRowButton = blEdit;
            efDevGridUserInfo.SetAllColumnEditable(blEdit);
            //colDEPT_ENAME.OptionsColumn.AllowEdit = false;
            colDEPT_CNAME.OptionsColumn.AllowEdit = false;
            colTIMETIRED.OptionsColumn.AllowEdit = false;

            efDevGridUserInfo.ShowContextMenu = blEdit;

            efDevGridUserInfo.EmbeddedNavigator.Buttons.CustomButtons[DISCARD].Visible = blEdit;
            efDevGridUserInfo.EmbeddedNavigator.Buttons.CustomButtons[SAVE].Visible = blEdit;

            isManageMode = blEdit;
        }
  
        /// <summary>
        /// Grid内数据是否改变.
        /// 执行前提是:数据源为一个表,表在操作前事AcceptChange()的
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        private bool isChange(EF.EFDevGrid grid)
        {
            grid.MainView.PostEditor();
            grid.RefreshDataSource();
            DataTable table = grid.DataSource as DataTable;
            if (table == null)
                return false;
            for (int rowIndex = 0; rowIndex < table.Rows.Count; ++rowIndex)
            {
                if (table.Rows[rowIndex].RowState == DataRowState.Deleted || table.Rows[rowIndex].RowState == DataRowState.Added || table.Rows[rowIndex].RowState == DataRowState.Modified)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 检查数据正确性
        /// </summary>
        /// <returns></returns>true 正确 false 存在问题
        private bool groupCheck()
        {
            //检查数据准确性
            for (int i = 0; i < this.gridViewGroupInfo.RowCount; i++)
            {
                if (!efDevGridGroupInfo.GetSelectedColumnChecked(i))
                {
                    continue;
                }
                if (gridViewGroupInfo.GetRowCellValue(i, "NAME") != null)
                {
                    if (gridViewGroupInfo.GetRowCellValue(i, "NAME").ToString().Trim() == "")
                    {
                        EFMessageBox.Show(EP.EPES.EPESC0000125/*群组名不能为空，请重新输入！*/, EP.EPES.EPESC0000024/*提示*/, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    if (Utility.GetByteLength(gridViewGroupInfo.GetRowCellValue(i, "NAME").ToString()) > 128)
                    {
                        EFMessageBox.Show(EP.EPES.EPESC0000171/*群组名不能超过128位，请重新输入！*/, EP.EPES.EPESC0000024/*提示*/, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
                else
                {
                    EFMessageBox.Show(EP.EPES.EPESC0000125/*群组名不能为空，请重新输入！*/, EP.EPES.EPESC0000024/*提示*/, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                //if (gridViewGroupInfo.GetRowCellValue(i, "APPNAME") != null)
                //{
                //    if (gridViewGroupInfo.GetRowCellValue(i, "APPNAME").ToString().Trim() == "")
                //    {
                //        EFMessageBox.Show(EP.EPES.EPESC0000036/*子系统不能为空，请重新输入！*/, EP.EPES.EPESC0000024/*提示*/, MessageBoxButtons.OK, MessageBoxIcon.Error);
                //        return false;
                //    }
                //}
                //else
                //{
                //    EFMessageBox.Show(EP.EPES.EPESC0000036/*子系统不能为空，请重新输入！*/, EP.EPES.EPESC0000024/*提示*/, MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    return false;
                //}

                if (gridViewGroupInfo.GetRowCellValue(i, "GROUPDESCRIPTION") != null)
                {
                    if (Utility.GetByteLength(gridViewGroupInfo.GetRowCellValue(i, "GROUPDESCRIPTION").ToString()) > 100)
                    {
                        EFMessageBox.Show(EP.EPES.EPESC0000127/*群组描述最多允许输入100位，请重新输入！*/, EP.EPES.EPESC0000024/*提示*/, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }
            return true;

        }


        public void CheckSameValue(DataTable dt, string colName)
        {
            DataRow[] drs = null;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                drs = dt.Select(colName + "='" + dt.Rows[i][colName] + "'");
                if (drs != null && drs.Length > 1)
                {
                    throw new Exception("有重复值");
                }
            }
        }

        /// <summary>
        /// 检查数据正确性
        /// </summary>
        /// <returns></returns>true 正确 false 存在问题
        private bool userCheck()
        {
            //检查数据准确性
            for (int i = 0; i < this.gridViewUserInfo.RowCount; i++)
            {
                if (!efDevGridUserInfo.GetSelectedColumnChecked(i))
                {
                    continue;
                }
                if (gridViewUserInfo.GetRowCellValue(i, "ENAME") != null)
                {
                    if (gridViewUserInfo.GetRowCellValue(i, "ENAME").ToString().Trim() == "")
                    {
                        EFMessageBox.Show(EP.EPES.EPESC0000129/*工号不能为空，请重新输入！*/, EP.EPES.EPESC0000024/*提示*/, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    if (Utility.GetByteLength(gridViewUserInfo.GetRowCellValue(i, "ENAME").ToString().Trim()) < 3)
                    {
                        EFMessageBox.Show(EP.EPES.EPESC0000130/*工号不能少于3位，请重新输入！*/, EP.EPES.EPESC0000024/*提示*/, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    if (Utility.GetByteLength(gridViewUserInfo.GetRowCellValue(i, "ENAME").ToString().Trim())
                        > 20)
                    {
                        EFMessageBox.Show(
                            string.Format(EP.EPES.EPESC0000131/*工号不能多于[{0}]位，请重新输入！*/, 20),
                            EP.EPES.EPESC0000024/*提示*/, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
                else
                {
                    EFMessageBox.Show(EP.EPES.EPESC0000129/*工号不能为空，请重新输入！*/, EP.EPES.EPESC0000024/*提示*/, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                if (gridViewUserInfo.GetRowCellValue(i, "CNAME") != null)
                {
                    if (gridViewUserInfo.GetRowCellValue(i, "CNAME").ToString().Trim() == "")
                    {
                        EFMessageBox.Show(EP.EPES.EPESC0000132/*中文姓名不能为空，请重新输入！*/, EP.EPES.EPESC0000024/*提示*/, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    if (Utility.GetByteLength(gridViewUserInfo.GetRowCellValue(i, "CNAME").ToString().Trim()) > 20)
                    {
                        EFMessageBox.Show(EP.EPES.EPESC0000133/*中文姓名不能多于20位，请重新输入！*/, EP.EPES.EPESC0000024/*提示*/, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
                else
                {
                    EFMessageBox.Show(EP.EPES.EPESC0000132/*中文姓名不能为空，请重新输入！*/, EP.EPES.EPESC0000024/*提示*/, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            return true;
        }



        //取消选择所有记录
        private void UnCheckAll(EFDevGrid grid, GridView gridView)
        {
            for (int i = 0; i < gridView.RowCount; i++)
            {
                grid.SetSelectedColumnChecked(i, false);
            }
            gridView.Invalidate();
        }
        #endregion
  
        #region 查询
        private void queryGroup()
        {
            DataSet inBlock = new DataSet();
            inBlock.Tables.Add();
            DataRow dr = inBlock.Tables[0].NewRow();
            inBlock.Tables[0].Columns.Add("groupname");
            inBlock.Tables[0].Columns.Add("adminuser");
            inBlock.Tables[0].Columns.Add("userid");
            inBlock.Tables[0].Columns.Add("appname");
            inBlock.Tables[0].Columns.Add("grouptype");

            dr["groupname"] = eftGroupName.Text;
            dr["adminuser"] = "";// eftGroupAdmin.Text;
            dr["userid"] = CConstString.UserId;
            dr["appname"] = CConstString.AppName;
            dr["grouptype"] = 1;
            inBlock.Tables[0].Rows.Add(dr);
            //调用SERVICE
            DataTable outBlock = DbUserInfo.QueryGroupInfo(inBlock, CConstString.ConnectName);
            outBlock.AcceptChanges();
            efDevGridGroupInfo.DataSource = outBlock;
            gridViewGroupInfo.BestFitColumns();
        }

        private DataTable QueryUser()
        {
            DataSet inblk = new DataSet();
            inblk.Tables.Add();
            inblk.Tables[0].Columns.Add("CNAME");
            inblk.Tables[0].Rows.Add(txtCName.Text.Trim());
            DataTable outblk = DbUserInfo.QueryUserInfo(inblk, CConstString.ConnectName);

            efDevGridUserInfo.DataSource = outblk;
            outblk.AcceptChanges();
            //gridViewUserInfo_FocusedRowChanged(null, null);
            return outblk;
        }

        //查询群组的子组和子用户
        private void queryMember(TreeListNode parentNode)
        {
            parentNode.Nodes.Clear();

            DataSet inBlock = new DataSet();
            inBlock.Tables.Add();


            inBlock.Tables[0].Columns.Add("groupid");
            inBlock.Tables[0].Rows.Add(((List<string>)parentNode.Tag)[1]);


            //调用SERVICE
            DataSet outBlock = DbUserInfo.QueryGroupChild(inBlock, CConstString.ConnectName);

            //返回子组
            string groupName = "";
            string groupDesc = "";
            string groupID = "";
            for (int i = 0; i < outBlock.Tables[0].Rows.Count; i++)
            {
                groupName = outBlock.Tables[0].Rows[i]["name"].ToString();
                groupDesc = outBlock.Tables[0].Rows[i]["groupdescription"].ToString();
                groupID = outBlock.Tables[0].Rows[i]["id"].ToString();


                TreeListNode treeNode = this.treeListMain.AppendNode(new object[] { groupName + "(" + groupDesc + ")", " " }, parentNode);
                treeNode.Tag = new List<string>();
                ((List<string>)treeNode.Tag).Add(groupName);
                ((List<string>)treeNode.Tag).Add(groupID);

                treeNode.SelectImageIndex = treeNode.ImageIndex = GROUPICON;
            }

            ////返回子用户
            string ename = "";
            string cname = "";
            string userid = "";
            string dept = "";
            for (int i = 0; i < outBlock.Tables[1].Rows.Count; i++)
            {
                ename = outBlock.Tables[1].Rows[i]["ename"].ToString();
                cname = outBlock.Tables[1].Rows[i]["cname"].ToString();
                userid = outBlock.Tables[1].Rows[i]["id"].ToString();

                TreeListNode treeNode = this.treeListMain.AppendNode(new object[] { ename + "(" + cname + ")", dept }, parentNode);
                treeNode.Tag = new List<string>();
                ((List<string>)treeNode.Tag).Add(ename);
                ((List<string>)treeNode.Tag).Add(userid);
                treeNode.SelectImageIndex = treeNode.ImageIndex = USERICON;
            }
        }
        #endregion

        #region 保存 方法
        //保存
        private bool SaveGroupInfo()
        {
            DataTable dt = efDevGridGroupInfo.DataSource as DataTable;
            if (dt == null || dt.Rows.Count < 1)
                return false;

            DataTable instable = dt.Clone();
            DataTable deltable = dt.GetChanges(DataRowState.Deleted);
            DataTable updtable = dt.Clone();

            DataRow dr = null;
            for (int rowIndex = 0; rowIndex < gridViewGroupInfo.RowCount; ++rowIndex)
            {
                if (efDevGridGroupInfo.GetSelectedColumnChecked(rowIndex))
                {
                    dr = gridViewGroupInfo.GetDataRow(rowIndex);
                    if (dr.RowState == DataRowState.Added)
                    {
                        instable.Rows.Add(dr.ItemArray);
                    }
                    else if (dr.RowState == DataRowState.Modified)
                    {
                        updtable.Rows.Add(dr.ItemArray);
                    }
                }
            }
            CheckSameValue(instable, "name");
            deltable = dt.GetChanges(DataRowState.Deleted);

            DataSet inBlock = new DataSet();
            inBlock.Tables.Add();
            inBlock.Tables[0].Columns.Add("userid");
            inBlock.Tables[0].Columns.Add("appname");
            inBlock.Tables[0].Columns.Add("grouptype");


            inBlock.Tables[0].Rows.Add(CConstString.UserId, CConstString.AppName, 0);
            if (instable != null && instable.Rows.Count > 0)
            {
                instable.TableName = "INSERT_BLOCK";
                inBlock.Tables.Add(instable);
            }

            if (deltable != null && deltable.Rows.Count > 0)
            {
                deltable.RejectChanges();
                deltable.TableName = "DELETE_BLOCK";
                inBlock.Tables.Add(deltable);
            }

            if (updtable != null && updtable.Rows.Count > 0)
            {
                updtable.TableName = "UPDATE_BLOCK";
                inBlock.Tables.Add(updtable);
            }

            if (inBlock.Tables.Count > 1)
            {
                DbUserInfo.SaveGroupInfo(inBlock, CConstString.ConnectName);
            }
            return true;
        }

        //保存
        private bool SaveUserInfo()
        {
            DataTable dt = efDevGridUserInfo.DataSource as DataTable;
            if (dt == null || dt.Rows.Count < 1)
                return false;

            DataTable instable = dt.Clone();
            DataTable deltable = dt.GetChanges(DataRowState.Deleted);
            DataTable updtable = dt.Clone();

            DataRow dr = null;
            for (int rowIndex = 0; rowIndex < gridViewUserInfo.RowCount; ++rowIndex)
            {
                if (efDevGridUserInfo.GetSelectedColumnChecked(rowIndex))
                {
                    dr = gridViewUserInfo.GetDataRow(rowIndex);
                    if (dr.RowState == DataRowState.Added)
                    {
                        instable.Rows.Add(dr.ItemArray);
                    }
                    else if (dr.RowState == DataRowState.Modified)
                    {
                        updtable.Rows.Add(dr.ItemArray);
                    }
                }
            }
            CheckSameValue(instable, "ename");
            deltable = dt.GetChanges(DataRowState.Deleted);

            DataSet inBlock = new DataSet();
            inBlock.Tables.Add();
            inBlock.Tables[0].Columns.Add("userid");
            inBlock.Tables[0].Columns.Add("appname");

            inBlock.Tables[0].Rows.Add(CConstString.UserId, CConstString.AppName);

            if (instable != null && instable.Rows.Count > 0)
            {
                instable.TableName = "INSERT_BLOCK";
                inBlock.Tables.Add(instable);
            }

            if (deltable != null && deltable.Rows.Count > 0)
            {
                deltable.RejectChanges();
                deltable.TableName = "DELETE_BLOCK";
                inBlock.Tables.Add(deltable);
            }

            if (updtable != null && updtable.Rows.Count > 0)
            {
                updtable.TableName = "UPDATE_BLOCK";
                inBlock.Tables.Add(updtable);
            }

            if (inBlock.Tables.Count > 1)
            {
                DbUserInfo.SaveUserInfo(inBlock, CConstString.ConnectName);
            }
            return true;
        }
        #endregion

        #region 群组grid相关事件 (双击[查询群组下用户],mosemove 拖动时使用,下发分页条按钮[保存])
        private void efDevGridGroupInfo_DoubleClick(object sender, EventArgs e)
        {
            try
            {

                if (this.gridViewGroupInfo.RowCount == 0 || this.gridViewGroupInfo.FocusedRowHandle < 0) return;

                string groupid = "";
                string groupName = "";
                string groupDesc = "";
                if (this.gridViewGroupInfo.GetRowCellValue(gridViewGroupInfo.FocusedRowHandle, "NAME") != null)
                {
                    groupid = gridViewGroupInfo.GetRowCellValue(gridViewGroupInfo.FocusedRowHandle, "ID").ToString();
                    groupName = gridViewGroupInfo.GetRowCellValue(gridViewGroupInfo.FocusedRowHandle, "NAME").ToString();
                }
                if (gridViewGroupInfo.GetRowCellValue(gridViewGroupInfo.FocusedRowHandle, "GROUPDESCRIPTION") != null)
                    groupDesc = gridViewGroupInfo.GetRowCellValue(gridViewGroupInfo.FocusedRowHandle, "GROUPDESCRIPTION").ToString();

                List<string> tag = new List<string>();
                tag.Add(groupName);
                tag.Add(groupid);
                layoutControlGroup4.Text = "群组[" + groupName + "]";
                this.treeListMain.Nodes.Clear();
                TreeListNode treeNode = this.treeListMain.AppendNode(new object[] { groupName + "(" + groupDesc + ")" }, null, tag);

                this.treeListMain.ExpandAll();
            }
            catch (Exception ex)
            {
                EFMessageBox.Show(ex.Message);
            }
        }

        void GroupInfoEmbeddedNavigator_ButtonClick(object sender, NavigatorButtonClickEventArgs e)
        {
            try
            {
                gridViewGroupInfo.PostEditor();
                efDevGridGroupInfo.Parent.Focus();
                switch (e.Button.ImageIndex)
                {
                    case SAVE:
                        if (groupCheck())
                        {
                            if (SaveGroupInfo())
                            {
                                queryGroup();
                            }
                        }
                        break;

                    case DISCARD:
                        queryGroup();
                        break;

                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                EFMessageBox.Show(ex.Message);
            }
        }

        private void gridViewGroupInfo_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                if (!isManageMode)
                {
                    return;
                }
                if (this.efDevGridGroupInfo.DataSource == null || efDevGridGroupInfo.EFChoiceCount == 0)
                {
                    return;
                }
                if (e.Button != MouseButtons.Left) return;

                treeListMain.OptionsBehavior.DragNodes = true;

                List<string> data = new List<string>();

                efDevGridGroupInfo.DoDragDrop(data, DragDropEffects.Copy);
            }
            catch (Exception ex)
            {
                EFMessageBox.Show(ex.Message);
            }
        }
        #endregion
        void UserInfoEmbeddedNavigator_ButtonClick(object sender, NavigatorButtonClickEventArgs e)
        {
            try
            {
                gridViewUserInfo.PostEditor();
                efDevGridUserInfo.RefreshDataSource();
                switch (e.Button.ImageIndex)
                {
                    case SAVE:
                        if (isChange(efDevGridUserInfo))
                        {
                            if (userCheck())
                            {
                                if (SaveUserInfo())
                                {
                                    QueryUser();
                                }
                            }
                        }
                        break;

                    case DISCARD:
                        QueryUser();
                        break;

                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                EFMessageBox.Show(ex.Message);
            }
        }

        private void efDevGridUserInfo_DoubleClick(object sender, EventArgs e)
        {
            try
            {

                if (this.gridViewUserInfo.RowCount == 0 || this.gridViewUserInfo.FocusedRowHandle < 0) 
                    return;

                string userid = "";
                string userDesc = "";
             
                if (this.gridViewUserInfo.GetRowCellValue(gridViewUserInfo.FocusedRowHandle, "ID") != null)
                {
                    userid = gridViewUserInfo.GetRowCellValue(gridViewUserInfo.FocusedRowHandle, "ID").ToString();
                }
                if (gridViewUserInfo.GetRowCellValue(gridViewUserInfo.FocusedRowHandle, "CNAME") != null)
                {
                    userDesc = gridViewUserInfo.GetRowCellValue(gridViewUserInfo.FocusedRowHandle, "CNAME").ToString();
                }
                layoutControlGroup4.Text = "用户[" + userDesc + "]所在群组";
                this.treeListMain.Nodes.Clear();


                DataSet blk = new DataSet();
                blk.Tables.Add();
                blk.Tables[0].Columns.Add("memberid");
                blk.Tables[0].Rows.Add(userid);
                DataTable dt = DbUserInfo.QueryGroupByMember(blk, CConstString.ConnectName);

                if (dt != null && dt.Rows.Count > 0)
                {
                    string groupDesc = "";
                    string groupName = "";
                    List<string> tag = new List<string>();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        groupName = dt.Rows[i]["NAME"].ToString();
                        groupDesc = dt.Rows[i]["GROUPDESCRIPTION"].ToString();
                        tag.Add(groupName);
                        tag.Add(dt.Rows[i]["ID"].ToString());

                        this.treeListMain.AppendNode(new object[] { groupName + "(" + groupDesc + ")" }, null, tag);

                    }
                    this.treeListMain.ExpandAll();
                }
            }
            catch (Exception ex)
            {
                EFMessageBox.Show(ex.Message);
            }
        }

        private void gridViewUserInfo_MouseMove(object sender, MouseEventArgs e)
        {
            if (!isManageMode)
            {
                return;
            }
            if (this.efDevGridUserInfo.DataSource == null || efDevGridUserInfo.EFChoiceCount == 0)
            {
                return;
            }
            if (e.Button != MouseButtons.Left) return;

            treeListMain.OptionsBehavior.DragNodes = true;

            List<string> data = new List<string>();
            efDevGridUserInfo.DoDragDrop(data, DragDropEffects.Copy);
        }

        #region 树相关事件
        private void treeListMain_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            if (e.Node == null
            || e.Node.Nodes.Count > 0
            || e.Node.Tag == null
            || e.Node.ImageIndex == USERICON)
            {
                return;
            }
            queryMember(e.Node);
        }
      
        private void treeListMain_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            //不允许拖拽treelist的节点
            treeListMain.OptionsBehavior.DragNodes = false;
        }

        private void treeListMain_DragEnter(object sender, DragEventArgs e)
        {
            if (isManageMode)
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void treeListMain_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                DevExpress.XtraTreeList.TreeListHitInfo hi = treeListMain.CalcHitInfo(treeListMain.PointToClient(new Point(e.X, e.Y)));

                TreeListNode parentNode = null;

                //拖拽至用户节点
                if (hi.Node.ImageIndex == USERICON)
                {
                    parentNode = hi.Node.ParentNode;
                }
                //拖拽至群组节点
                else if (hi.Node.ImageIndex == GROUPICON  )
                {
                    parentNode = hi.Node;
                }

                DataSet inBlock = new DataSet();
                inBlock.Tables.Add();

                //为群组新增子组
                if (tabbedControlGroup1.SelectedTabPage == layoutControlGroup3)
                {
                    string parentGroup = ((List<string>)parentNode.Tag)[0];

                    if (parentGroup == "admingroup"
                        || parentGroup == "formgroup"
                        || parentGroup == "usermanager"
                        || parentGroup == "groupmanager"
                        || parentGroup == "resourcemanager")
                    {
                        EFMessageBox.Show(EP.EPES.EPESC0000193/*系统群组下不可挂子组，请将角色用户直接添加到该组下！*/, EP.EPES.EPESC0000024/*提示*/, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    inBlock.Tables[0].Columns.Add("ID");
                    inBlock.Tables[0].Columns.Add("userid");
                    string groupid = ((List<string>)parentNode.Tag)[1];
                    inBlock.Tables[0].Rows.Add(groupid, CConstString.UserId);


                    inBlock.Tables.Add();
                    inBlock.Tables[1].Columns.Add("ID");

                    for (int i = 0; i < this.gridViewGroupInfo.RowCount; i++)
                    {
                        if (efDevGridGroupInfo.GetSelectedColumnChecked(i))
                        {
                            string tmp = this.gridViewGroupInfo.GetRowCellValue(i, "ID").ToString();
                            if (groupid == tmp)
                            {
                                throw new Exception("拖动的组与父组一样.");
                            }
                            inBlock.Tables[1].Rows.Add(tmp);
                        }
                    }

                    string msg = DbUserInfo.InsertGroupChildGroup(inBlock, CConstString.ConnectName);


                    if (msg == "ok")
                    {
                        queryMember(parentNode);

                        //取消列表框中所有checkbox选中状态
                        UnCheckAll(efDevGridGroupInfo, gridViewGroupInfo);
                    }
                }
                //为群组新增子用户
                else if (tabbedControlGroup1.SelectedTabPage == layoutControlGroup2)
                {
                    inBlock.Tables[0].Columns.Add("groupid");
                    inBlock.Tables[0].Columns.Add("groupname");
                    inBlock.Tables[0].Columns.Add("user");
                    inBlock.Tables[0].Columns.Add("authmode");

                    inBlock.Tables[0].Rows.Add(((List<string>)parentNode.Tag)[1], ((List<string>)parentNode.Tag)[0], CConstString.UserId, 0);

                    inBlock.Tables.Add();
                    inBlock.Tables[1].Columns.Add("userid");
                    inBlock.Tables[1].Columns.Add("username");

                    for (int i = 0; i < this.gridViewUserInfo.RowCount; i++)
                    {
                        if (efDevGridUserInfo.GetSelectedColumnChecked(i))
                        {
                            inBlock.Tables[1].Rows.Add(this.gridViewUserInfo.GetRowCellValue(i, "ID"), this.gridViewUserInfo.GetRowCellValue(i, "ENAME"));
                        }
                    }

                    DbUserInfo.InsertGroupChildUser(inBlock, CConstString.ConnectName);

                    queryMember(parentNode);

                    // 取消列表框中所有checkbox选中状态
                    UnCheckAll(efDevGridUserInfo, gridViewUserInfo);

                }

                treeListMain.OptionsBehavior.DragNodes = false;

            }
            catch (Exception ex)
            {
                EFMessageBox.Show(ex.Message + ex.StackTrace);
            }
        }


        private void treeListMain_SelectionChanged(object sender, EventArgs e)
        {
            if (treeListMain.Selection.Count == 1)
            {
                parent = treeListMain.Selection[0].ParentNode;
                return;
            }
            if (treeListMain.Selection.Count > 1)
            {
                for (int i = 1; i < treeListMain.Selection.Count; i++)
                {
                    //只有同级节点可被选中
                    if (treeListMain.Selection[i].ParentNode != parent)
                    {
                        treeListMain.Selection[i].Selected = false;
                    }
                }

                //只有同级节点可被选中
                if (treeListMain.FocusedNode.ParentNode != parent)
                {
                    treeListMain.FocusedNode.Selected = false;
                    treeListMain.FocusedNode = treeListMain.Selection[0];
                }
            }
        }

        //弹出右键菜单
        private void treeListMain_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                DevExpress.XtraTreeList.TreeListHitInfo hi = treeListMain.CalcHitInfo(e.Location);
                if (hi != null)
                {
                    if (hi.Node != null)
                    {
                        if ((treeListMain.Selection.Count != 0 && !treeListMain.Selection.Contains(hi.Node)) || treeListMain.Selection.Count == 0)
                        {
                            treeListMain.Selection.Clear();
                            treeListMain.Selection.Add(hi.Node);

                        }
                    }
                }
            }
        }


        #endregion

        #region 查询按钮事件
        private void btnQueryUserInfo_Click(object sender, EventArgs e)
        {
            QueryUser();
        }

        private void btnQueryGroupInfo_Click(object sender, EventArgs e)
        {
            this.treeListMain.Nodes.Clear();

            queryGroup();
        }
        #endregion

        #region  弹出菜单... 删除组成员信息
        private void itemDeleteFromGroup_Click(object sender, EventArgs e)
        {
            if (this.treeListMain.FocusedNode == null || this.treeListMain.FocusedNode.ParentNode == null)
            {
                return;
            }
            if (this.treeListMain.FocusedNode.Tag == null || this.treeListMain.FocusedNode.ParentNode.Tag == null)
            {
                return;
            }
            if (treeListMain.Selection.Count == 0)
            {
                return;
            }
            DataSet inBlock = new DataSet();
            inBlock.Tables.Add();
            inBlock.Tables[0].Columns.Add("groupid");
            inBlock.Tables[0].Columns.Add("user");



            inBlock.Tables[0].Rows.Add(((List<string>)parent.Tag)[1], CConstString.UserId);
  
            //子组
            inBlock.Tables.Add("childgroup");
            inBlock.Tables[1].Columns.Add("id");

            for (int i = 0; i < this.treeListMain.Selection.Count; i++)
            {
                //子组节点
                if (this.treeListMain.Selection[i].ImageIndex == GROUPICON   )
                {
                    inBlock.Tables[1].Rows.Add(((List<string>)this.treeListMain.Selection[i].Tag)[1]);
                }
            }

            //子用户
            inBlock.Tables.Add("childuser");
            inBlock.Tables[2].Columns.Add("id");

            for (int i = 0, j = 1; i < this.treeListMain.Selection.Count; i++)
            {
                //子用户节点
                if (this.treeListMain.Selection[i].ImageIndex == USERICON)
                {
                    inBlock.Tables[2].Rows.Add(((List<string>)this.treeListMain.Selection[i].Tag)[1]);
                }
            }
            DbUserInfo.DeleteGroupMember(inBlock, CConstString.ConnectName);

            queryMember(parent);
            parent = null;
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if (!isManageMode || treeListMain.FocusedNode==null || treeListMain.FocusedNode.ParentNode == null)
            {
                e.Cancel = true;
            }
        }
        #endregion
        
        #region 工具栏按钮事件(编辑,保存,取消)
        private void barButtonItemCancel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SetPageState(false);
        }

        private void barButtonItemEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SetPageState(true);
        }
        private void barButtonItemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                bool success = true;

                if (isChange(efDevGridGroupInfo))
                {
                    if (groupCheck())
                    {
                        if (SaveGroupInfo())
                        {
                            queryGroup();
                        }
                        else
                        {
                            success = false;
                        }
                    }
                    else
                    {
                        success = false;
                    }
                }
                if (isChange(efDevGridUserInfo))
                {
                    if (userCheck())
                    {
                        if (SaveUserInfo())
                        {
                            QueryUser();
                        }
                        else
                        {
                            success = false;
                        }
                    }
                    else
                    {
                        success = false;
                    }
                }
                if (success)
                {
                    SetPageState(false);
                }
            }
            catch (Exception ex)
            {
                EFMessageBox.Show(ex.Message + ex.StackTrace);
            }
        }
        #endregion
       
        public Bar ChildBar
        {
            get
            {
                return bar2;
            }
        }


    }
}