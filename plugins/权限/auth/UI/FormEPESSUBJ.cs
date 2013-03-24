using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraTreeList.Nodes;
using System.Collections;
using DevExpress.XtraBars;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.Controls;



namespace EP
{
    /// <summary>
    /// 本页面主要实现主体管理功能，包括用户及群组管理。
    /// </summary>
    public partial class FormEPESSUBJ : DevExpress.XtraEditors.XtraForm
    {
        private string selectedCompanyCode = "";
        private TreeListNode parent = null;

        private string adminuserename1 = "";
        private string adminuserename2 = "";

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

        //群组信息相关变量
        private int groupSaveStatus = 0;//群组信息："0"——不需保存；"1"——新增保存；"2"——修改群组信息保存；"3"——修改群组管理员保存


        //用户信息相关变量
        private int pageIndex = 1;
        private int totalPage = 0;
        private int pageCount = 1000;
        private string userEname = "";
        private string userCname = "";
        private string userDeptName = "";

        private int userSaveStatus = 0;//用户信息："0"——不需保存；"1"——新增保存；"2"——修改用户信息保存；"3"——修改用户部门信息保存


        //部门信息相关变量
        private int deptSaveStatus = 0;//部门信息："0"——不需保存；"1"——新增保存；"2"——修改保存；

        //图标编号
        private const int NEXTPAGE = 2;
        private const int PREPAGE = 1;
        private const int FIRSTPAGE = 0;
        private const int LASTPAGE = 3;
   
        
#region 初始化

        public FormEPESSUBJ()
        {
            InitializeComponent();
        }

        private void FormESSUBJ_Load(object sender, EventArgs e) { }
        //{
        //    this.InitDevGridCustomButtons();

        //    this.fgDevGridGroupInfo.EmbeddedNavigator.ButtonClick += new NavigatorButtonClickEventHandler(GroupInfoEmbeddedNavigator_ButtonClick);
        //    this.fgDevGridUserInfo.EmbeddedNavigator.ButtonClick += new NavigatorButtonClickEventHandler(UserInfoEmbeddedNavigator_ButtonClick);
        //    this.fgDevGridDeptInfo.EmbeddedNavigator.ButtonClick += new NavigatorButtonClickEventHandler(DeptInfoEmbeddedNavigator_ButtonClick);

        //    //查询用户管辖范围内的部门信息
        //    getUserDept();          

        //    //获取当前系统中所有的APPNAME信息
        //    RepositoryItemComboBox repstryItemGroupComboBoxApp = new RepositoryItemComboBox();

        //    EI.EIInfo inBlockAPP = new EI.EIInfo();
        //    EI.EIInfo outBlockAPP = new EI.EIInfo();

        //    inBlockAPP.SetColName(1, "ename");
        //    inBlockAPP.SetColVal(1, "ename", "");
        //    outBlockAPP = EI.EITuxedo.CallService("epesappinfo", inBlockAPP);
        //    if (outBlockAPP.sys_info.flag != 0)
        //    {
        //        MessageBox.Show(string.Format(EP.EPES.EPESC0000061/*获取APPNAME信息错误：{0}*/, outBlockAPP.sys_info.msg), EP.EPES.EPESC0000024, MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        return;
        //    }
        //    String stemp = "";
        //    //fgComboBoxApp.Items.Clear();

        //    for (int i = 0; i < outBlockAPP.blk_info[0].Row; i++)
        //    {
        //        stemp = outBlockAPP.GetColVal(1, i + 1, "ename") + ": " + outBlockAPP.GetColVal(1, i + 1, "cname");
        //        //fgComboBoxApp.Items.Add(stemp);
        //        fgDevComboBoxEditApp.Properties.Items.Add(stemp);
        //        repstryItemGroupComboBoxApp.Items.Add(outBlockAPP.GetColVal(1, i + 1, "ename"));
        //    }

        //    gridViewGroupInfo.Columns["APPNAME"].ColumnEdit = repstryItemGroupComboBoxApp;

        //    //群组信息的子系统列只允许从下拉列表中选择
        //    repstryItemGroupComboBoxApp.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;

        //    if (outBlockAPP.blk_info[0].Row > 0)
        //    {
        //        //fgComboBoxApp.SelectedIndex = 0;
        //        fgDevComboBoxEditApp.SelectedIndex = 0;
        //    }

        //    //获取帐套信息
        //    InitComboBoxCompany();               

        //    //用户信息的是否有效列只允许从下拉列表中选择
        //    repstryItemComboBoxUserIsEnable.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;

        //    //用户信息的部门号列只允许从下拉列表中选择
        //    repstryItemComboBoxUserDeptName.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;


        //    //设置checkbox选中行样式
        //    DevExpress.Skins.Skin currentSkin;
        //    currentSkin = DevExpress.Skins.CommonSkins.GetSkin(DevExpress.LookAndFeel.UserLookAndFeel.Default);
        //    Color textColor = currentSkin.Colors.GetColor(DevExpress.Skins.CommonColors.WindowText);
        //    Color highlightTextColor = currentSkin.Colors.GetColor(DevExpress.Skins.CommonColors.HighligthText);
        //    Color selectColor = currentSkin.Colors.GetColor(DevExpress.Skins.CommonColors.Highlight);

        //    StyleFormatCondition cnGroup;
        //    cnGroup = new StyleFormatCondition(FormatConditionEnum.Equal, fgDevGridGroupInfo.SelectionColumn, null, false);
        //    cnGroup.ApplyToRow = true;
        //    cnGroup.Appearance.BackColor = Color.Empty;
        //    cnGroup.Appearance.ForeColor = textColor;
        //    gridViewGroupInfo.FormatConditions.Add(cnGroup);

        //    cnGroup = new StyleFormatCondition(FormatConditionEnum.Equal, fgDevGridGroupInfo.SelectionColumn, null, true);
        //    cnGroup.ApplyToRow = true;
        //    cnGroup.Appearance.BackColor = selectColor;
        //    cnGroup.Appearance.ForeColor = highlightTextColor;
        //    gridViewGroupInfo.FormatConditions.Add(cnGroup);

        //    StyleFormatCondition cnUser;
        //    cnUser = new StyleFormatCondition(FormatConditionEnum.Equal, fgDevGridUserInfo.SelectionColumn, null, false);
        //    cnUser.ApplyToRow = true;
        //    cnUser.Appearance.BackColor = Color.Empty;
        //    cnUser.Appearance.ForeColor = textColor;
        //    gridViewUserInfo.FormatConditions.Add(cnUser);

        //    cnUser = new StyleFormatCondition(FormatConditionEnum.Equal, fgDevGridUserInfo.SelectionColumn, null, true);
        //    cnUser.ApplyToRow = true;
        //    cnUser.Appearance.BackColor = selectColor;
        //    cnUser.Appearance.ForeColor = highlightTextColor;
        //    gridViewUserInfo.FormatConditions.Add(cnUser);

        //    StyleFormatCondition cnDept;
        //    cnDept = new StyleFormatCondition(FormatConditionEnum.Equal, fgDevGridDeptInfo.SelectionColumn, null, false);
        //    cnDept.ApplyToRow = true;
        //    cnDept.Appearance.BackColor = Color.Empty;
        //    cnDept.Appearance.ForeColor = textColor;
        //    gridViewDeptInfo.FormatConditions.Add(cnDept);

        //    cnDept = new StyleFormatCondition(FormatConditionEnum.Equal, fgDevGridDeptInfo.SelectionColumn, null, true);
        //    cnDept.ApplyToRow = true;
        //    cnDept.Appearance.BackColor = selectColor;
        //    cnDept.Appearance.ForeColor = highlightTextColor;
        //    gridViewDeptInfo.FormatConditions.Add(cnDept);


        //    fgDevGridGroupInfo.ShowAddRowButton = false;
        //    fgDevGridGroupInfo.ShowAddCopyRowButton = false;
        //    fgDevGridGroupInfo.ShowDeleteRowButton = false;
        //    fgDevGridGroupInfo.SetAllColumnEditable(false);

        //    fgDevGridUserInfo.ShowAddRowButton = false;
        //    fgDevGridUserInfo.ShowAddCopyRowButton = false;
        //    fgDevGridUserInfo.ShowDeleteRowButton = false;
        //    fgDevGridUserInfo.SetAllColumnEditable(false);

        //    fgDevGridDeptInfo.ShowAddRowButton = false;
        //    fgDevGridDeptInfo.ShowAddCopyRowButton = false;
        //    fgDevGridDeptInfo.ShowDeleteRowButton = false;
        //    fgDevGridDeptInfo.SetAllColumnEditable(false);

        //    fgDevGridUserInfo.SelectionColumn.OptionsColumn.AllowEdit = true;

        //}

        //获取帐套信息
        private void InitComboBoxCompany()
        {
            if (EPESCommon.AuthMode == AUTHMODE.MODE_9672)
            {
                fgLabelComp.Visible = false;
                //fgComboBoxComp.Visible = false;
                fgDevComboBoxEditComp.Visible = false;
                this.selectedCompanyCode = "";
                return;
            }

            //若为多帐套系统
            EI.EIInfo outBlock = EI.EITuxedo.CallService("epescompanyinfo", new EI.EIInfo());
           
           string companyCode = "";
           string companyName = "";
           for (int i = 1; i <= outBlock.blk_info[0].Row; i++)
           {
               companyCode = outBlock.GetColVal(i, "companycode");
               companyName = outBlock.GetColVal(i, "companyname");
               //fgComboBoxComp.Items.Add(companyCode + ": " + companyName);
               fgDevComboBoxEditComp.Properties.Items.Add(companyCode + ": " + companyName);
           }

           if (outBlock.blk_info[0].Row > 0)
           {
               //fgDevComboBoxEditComp.Properties.Items.Add("ALL");
               fgDevComboBoxEditComp.SelectedIndex = 0;
               this.selectedCompanyCode = fgDevComboBoxEditComp.EditValue.ToString().Split(':')[0];
           }
           
           if(outBlock.blk_info[0].Row == 1)
           {
               fgLabelComp.Visible = false;
               //fgComboBoxComp.Visible = false;
               fgDevComboBoxEditComp.Visible = false;
           }
           
           // fgComboBoxComp.SelectedIndexChanged += new EventHandler(fgComboBoxComp_SelectedIndexChanged);
        }

        //查询用户管辖范围内的部门信息
        private void getUserDept()
        {

            //获取部门信息
            EI.EIInfo inblku = new EI.EIInfo();
            EI.EIInfo outblku = new EI.EIInfo();
            inblku.SetColName(1, "ename");
            inblku.SetColVal(1, 1, "");
            inblku.SetColName(2, "cname");
            inblku.SetColVal(1, 2, "");
            inblku.SetColName(3, "user_name");
            inblku.SetColVal(1, 3,  "formUserId");
            inblku.SetColName(4, "appname");
            inblku.SetColVal(1, 4,  "epAppName");

            outblku = EI.EITuxedo.CallService("epesusdept_inq", inblku);
            if (outblku.sys_info.flag < 0)
            {
                MessageBox.Show(string.Format(EP.EPES.EPESC0000106/*获取用户管辖部门信息错误：{0}*/, outblku.sys_info.msg), EP.EPES.EPESC0000009/*错误*/, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            object obj_qry = 0;
            string v_all = "";
            fgDevComboBoxEditDept.Properties.Items.Add(EP.EPES.EPESC0000107/*ALL:所有部门*/);
            combDept.Properties.Items.Add(EP.EPES.EPESC0000107/*ALL:所有部门*/);
            for (int i = 1; i <= outblku.blk_info[0].Row; i++)
            {
                v_all = outblku.GetColVal(i, "ename") + ": " + outblku.GetColVal(i, "cname");
                obj_qry = v_all;
                fgDevComboBoxEditDept.Properties.Items.Add(obj_qry);
                combDept.Properties.Items.Add(obj_qry);
                repstryItemComboBoxUserDeptName.Items.Add(outblku.GetColVal(i, "ename"));
            }
            if (outblku.blk_info[0].Row > 0)
            {
                fgDevComboBoxEditDept.SelectedIndex = 0;
                combDept.SelectedIndex = 0;
            }

            //获取用户信息的部门标识列下拉列表集合
            //this.repositoryItemLookUpEditDeptNo.DisplayMember = "id";
            //this.repositoryItemLookUpEditDeptNo.ValueMember = "id";
            //this.repositoryItemLookUpEditDeptNo.DataSource = outblku.Tables[0];
            this.repositoryItemLookUpEditDeptName.DisplayMember = "ename";
            this.repositoryItemLookUpEditDeptName.ValueMember = "ename";
            this.repositoryItemLookUpEditDeptName.DataSource = outblku.Tables[0];
        }

#endregion
              
       
#region 群组信息

        #region 查询

        //查询群组信息
        private void fgButtonQryGrp_Click(object sender, EventArgs e)
        {
            if (this.fgDevComboBoxEditApp.EditValue == null)
            {
                MessageBox.Show(EP.EPES.EPESC0000108/*请选择子系统！*/, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            this.treeListMain.Nodes.Clear();

            queryGroup();
        }
        
        private void queryGroup()
        {
            //清除报信栏
            //this.//EFMsgInfo = "";

            EI.EIInfo inBlock = new EI.EIInfo();
            EI.EIInfo outBlock;

            inBlock.SetColName(1, "groupname");
            inBlock.SetColVal(1, "groupname", fgtGroupName.Text);
            inBlock.SetColName(2, "adminuser");
            inBlock.SetColName(2, "adminuser");
            inBlock.SetColVal(1, "adminuser", fgtGroupAdmin.Text);
            inBlock.SetColName(3, "userid");
            inBlock.SetColVal(1, "userid", "XXLoginUserIDXX");
            inBlock.SetColName(4, "appname");
            inBlock.SetColVal(1, "appname", fgDevComboBoxEditApp.EditValue.ToString().Split(':')[0]);
            inBlock.SetColName(5, "companycode");
            inBlock.SetColVal(1, "companycode", this.selectedCompanyCode);
            inBlock.SetColName(6, "grouptype");
            inBlock.SetColVal(1, "grouptype", 0);

            //调用SERVICE
            outBlock = EI.EITuxedo.CallService("epesgroup_inq2", inBlock);

            dataSetEPESSUBJ.TESGROUPINFO.Clear();
            outBlock.ConvertToStrongType(dataSetEPESSUBJ);
            dataSetEPESSUBJ.TESGROUPINFO.AcceptChanges();

            gridViewGroupInfo_FocusedRowChanged(null, null);

            //gridViewGroupInfo.BestFitColumns();

            ShowReturnMsg(outBlock);

            groupSaveStatus = 0;

        }

        #endregion

        void GroupInfoEmbeddedNavigator_ButtonClick(object sender, NavigatorButtonClickEventArgs e)
        {
            fgDevGridGroupInfo.Parent.Focus();
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

                Default:
                    break;
            }
        }

        //保存
        private bool SaveGroupInfo()
        {
            DataTable instable = this.dataSetEPESSUBJ.TESGROUPINFO.Clone() ;
            DataTable deltable = null; // this.dataSetEPESSUBJ.TESGROUPINFO.GetChanges(DataRowState.Deleted);
            DataTable updtable = this.dataSetEPESSUBJ.TESGROUPINFO.Clone() ;

            //FilterData(instable);
            //FilterData(updtable);
            DataRow dr = null;
            for (int rowIndex = 0; rowIndex < gridViewGroupInfo.RowCount; ++rowIndex)
            {
                if (fgDevGridGroupInfo.GetSelectedColumnChecked(rowIndex))
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

            deltable = dataSetEPESSUBJ.TESGROUPINFO.GetChanges(DataRowState.Deleted);

            EI.EIInfo inBlock = new EI.EIInfo();

            inBlock.SetColName(1, "userid");
            inBlock.SetColVal(1, "userid", "XXLoginUserIDXX");

            inBlock.SetColName(2, "appname");
            inBlock.SetColVal(1, "appname", this.fgDevComboBoxEditApp.EditValue.ToString().Split(':')[0]);

            inBlock.SetColName(3, "grouptype");
            inBlock.SetColVal(1, "grouptype", 0);

            inBlock.SetColName(4, "companycode");
            inBlock.SetColVal(1, "companycode", this.selectedCompanyCode);

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

                EI.EIInfo outBlock = EI.EITuxedo.CallService("epesgroup_do", inBlock);
                if (outBlock.sys_info.flag < 0)
                {
                    MessageBox.Show(outBlock.sys_info.msg, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 过滤未选中行
        /// </summary>
        /// <param name="table"></param>
        private static void FilterData(DataTable table)
        {
            if (table != null)
            {
                if (table.Rows.Count > 0)
                {
                    for (int i = table.Rows.Count - 1; i >= 0; i--)
                    {
                        if (table.Rows[i]["selected"].ToString() == "False")
                        {
                            table.Rows.RemoveAt(i);
                        }
                    }
                }
            }
        }

        //private bool groupCheck(int row)
        //{
        //    if (dataSetEPESSUBJ.TESGROUPINFO.Rows[row].Field<string>("NAME") != null)
        //    {
        //        if (dataSetEPESSUBJ.TESGROUPINFO.Rows[row].Field<string>("NAME").Trim() == "")
        //        {
        //            MessageBox.Show(EP.EPES.EPESC0000125/*群组名不能为空，请重新输入！*/, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //            return false;
        //        }
        //        if (dataSetEPESSUBJ.TESGROUPINFO.Rows[row].Field<string>("NAME").Length > 127)
        //        {
        //            MessageBox.Show("群组名不能超过127位，请重新输入！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //            return false;
        //        }
        //    }
        //    else
        //    {
        //        MessageBox.Show(EP.EPES.EPESC0000125/*群组名不能为空，请重新输入！*/, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        return false;
        //    }

        //    if (dataSetEPESSUBJ.TESGROUPINFO.Rows[row].Field<string>("APPNAME") != null)
        //    {
        //        if (dataSetEPESSUBJ.TESGROUPINFO.Rows[row].Field<string>("APPNAME").Trim() == "")
        //        {
        //            MessageBox.Show(EP.EPES.EPESC0000036/*子系统不能为空，请重新输入！*/, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //            return false;
        //        }
        //    }
        //    else
        //    {
        //        MessageBox.Show(EP.EPES.EPESC0000036/*子系统不能为空，请重新输入！*/, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        return false;
        //    }

        //    if (dataSetEPESSUBJ.TESGROUPINFO.Rows[row].Field<string>("GROUPDESCRIPTION") != null)
        //    {
        //        if (dataSetEPESSUBJ.TESGROUPINFO.Rows[row].Field<string>("GROUPDESCRIPTION").Length > 100)
        //        {
        //            MessageBox.Show(EP.EPES.EPESC0000127/*群组描述最多允许输入100位，请重新输入！*/, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //            return false;
        //        }
        //    }

        //    if (dataSetEPESSUBJ.TESGROUPINFO.Rows[row].Field<string>("ADMINUSERNAME1") != null )
        //    {
        //        if (dataSetEPESSUBJ.TESGROUPINFO.Rows[row].Field<string>("ADMINUSERNAME1").Trim().Length < 2 )
        //        {
        //            MessageBox.Show(EP.EPES.EPESC0000128/*群组管理员不能为空，请重新输入！*/, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //            return false;
        //        }
        //    }
        //    else
        //    {
        //        MessageBox.Show(EP.EPES.EPESC0000128/*群组管理员不能为空，请重新输入！*/, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        return false;
        //    }

        //    if (dataSetEPESSUBJ.TESGROUPINFO.Rows[row].Field<string>("ADMINUSERNAME2") != null)
        //    {
        //        if (dataSetEPESSUBJ.TESGROUPINFO.Rows[row].Field<string>("ADMINUSERNAME2").Trim().Length < 2)
        //        {
        //            MessageBox.Show(EP.EPES.EPESC0000128/*群组管理员不能为空，请重新输入！*/, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //            return false;
        //        }
        //    }
        //    else
        //    {
        //        MessageBox.Show(EP.EPES.EPESC0000128/*群组管理员不能为空，请重新输入！*/, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        return false;
        //    }

        //    return true;
        //}


         /// <summary>
        /// 检查数据正确性
        /// </summary>
        /// <returns></returns>true 正确 false 存在问题
        private bool groupCheck()
        {
            this.tESGROUPINFOBindingSource.EndEdit();
            //检查数据准确性
            for (int i = 0; i < this.gridViewGroupInfo.RowCount; i++)
            {
                if(//gridViewGroupInfo.GetRowCellValue(i, "selected") == null
                    //|| gridViewGroupInfo.GetRowCellValue(i, "selected").ToString() == "False"
                    !fgDevGridGroupInfo.GetSelectedColumnChecked(i))
                {
                    continue;
                }
                if (gridViewGroupInfo.GetRowCellValue(i,"NAME") != null)
                {
                    if (gridViewGroupInfo.GetRowCellValue(i, "NAME").ToString().Trim() == "")
                    {
                        MessageBox.Show(EP.EPES.EPESC0000125/*群组名不能为空，请重新输入！*/, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    if (EPESCommon.GetStringLength(gridViewGroupInfo.GetRowCellValue(i, "NAME").ToString()) > 128)
                    {
                        MessageBox.Show(EP.EPES.EPESC0000171/*群组名不能超过128位，请重新输入！*/, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
                else
                {
                    MessageBox.Show(EP.EPES.EPESC0000125/*群组名不能为空，请重新输入！*/, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                if (gridViewGroupInfo.GetRowCellValue(i, "APPNAME") != null)
                {
                    if (gridViewGroupInfo.GetRowCellValue(i, "APPNAME").ToString().Trim() == "")
                    {
                        MessageBox.Show(EP.EPES.EPESC0000036/*子系统不能为空，请重新输入！*/, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
                else
                {
                    MessageBox.Show(EP.EPES.EPESC0000036/*子系统不能为空，请重新输入！*/, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                if (gridViewGroupInfo.GetRowCellValue(i, "GROUPDESCRIPTION") != null)
                {
                    if (EPESCommon.GetStringLength(gridViewGroupInfo.GetRowCellValue(i, "GROUPDESCRIPTION").ToString()) > 100)
                    {
                        MessageBox.Show(EP.EPES.EPESC0000127/*群组描述最多允许输入100位，请重新输入！*/, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }

                if (gridViewGroupInfo.GetRowCellValue(i, "ADMINUSERNAME1") != null)
                {
                    if (EPESCommon.GetStringLength(gridViewGroupInfo.GetRowCellValue(i, "ADMINUSERNAME1").ToString().Trim()) < 2)
                    {
                        MessageBox.Show(EP.EPES.EPESC0000128/*群组管理员不能为空，请重新输入！*/, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
                else
                {
                    MessageBox.Show(EP.EPES.EPESC0000128/*群组管理员不能为空，请重新输入！*/, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                if (gridViewGroupInfo.GetRowCellValue(i, "ADMINUSERNAME2") != null)
                {
                    if (EPESCommon.GetStringLength(gridViewGroupInfo.GetRowCellValue(i, "ADMINUSERNAME2").ToString().Trim()) < 2)
                    {
                        MessageBox.Show(EP.EPES.EPESC0000128/*群组管理员不能为空，请重新输入！*/, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
                else
                {
                    MessageBox.Show(EP.EPES.EPESC0000128/*群组管理员不能为空，请重新输入！*/, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

            }
            return true;

        }
        private void repositoryItemLookUpEditAdmin1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            //this.//EFMsgInfo = "";

            LookUpEdit be = sender as LookUpEdit;

            string admin1 = be.Text;

            if (admin1.Length < 2)
            {
                //this.//EFMsgInfo = EP.EPES.EPESC0000109/*请输入两位查询字符*/;
                return;
            }

            string app = gridViewGroupInfo.GetRowCellValue(this.gridViewGroupInfo.FocusedRowHandle, "APPNAME").ToString();

            //if (app.Length < 2)
            //{
            //    MessageBox.Show("请先选择需要新增的群组子系统！");
            //    return;
            //}


            //FormESSUBJADMIN frm = new FormESSUBJADMIN(val.ToString(), app, MousePosition);
            //frm.ShowDialog();

            EI.EIInfo inBlocks = new EI.EIInfo();
            EI.EIInfo outBlocks;


            inBlocks.SetColName(1, "ename");
            inBlocks.SetColName(2, "cname");
            inBlocks.SetColName(3, "userid");
            inBlocks.SetColName(4, "dept_ename");
            inBlocks.SetColName(5, "q_appname");

            inBlocks.SetColVal(1, "ename", admin1);
            inBlocks.SetColVal(1, "cname", " ");
            inBlocks.SetColVal(1, "userid",  "EventArgs.formUserId");
            inBlocks.SetColVal(1, "dept_ename", " ");
            inBlocks.SetColVal(1, "q_appname", app);

            outBlocks = EI.EITuxedo.CallService("epesuser_inq2", inBlocks);

            DataSet ds = new DataSet();
            outBlocks.GetBlockVal(ds);

            repositoryItemLookUpEditAdmin1.DataSource = ds.Tables[0];
            repositoryItemLookUpEditAdmin1.DisplayMember = "ename";
            repositoryItemLookUpEditAdmin1.ValueMember = "cname";

            LookUpColumnInfoCollection coll = repositoryItemLookUpEditAdmin1.Properties.Columns;
            coll.Add(new LookUpColumnInfo("ename", 0));
            coll.Add(new LookUpColumnInfo("cname", 0));

            repositoryItemLookUpEditAdmin1.Properties.BestFitMode = BestFitMode.BestFitResizePopup;

            string rst = repositoryItemLookUpEditAdmin1.ValueMember;
            if (rst != string.Empty)
            {
                gridViewGroupInfo.SetRowCellValue(gridViewGroupInfo.FocusedRowHandle, colADMINUSERNAME1, rst);
            }

        }
#endregion


#region 群组用户关系

        #region 查询群组下的子组及用户

        private void fgDevGridGroupInfo_DoubleClick(object sender, EventArgs e)
        {
            //this.//EFMsgInfo = "";
            if (this.gridViewGroupInfo.RowCount == 0 || this.gridViewGroupInfo.FocusedRowHandle < 0) return;

            string groupid = "";
            string groupName = "";
            string groupDesc = "";

            if (this.gridViewGroupInfo.GetRowCellValue(gridViewGroupInfo.FocusedRowHandle, "NAME") != null)
            {
                groupid = gridViewGroupInfo.GetRowCellValue(gridViewGroupInfo.FocusedRowHandle, "ID").ToString();
                groupName = gridViewGroupInfo.GetRowCellValue(gridViewGroupInfo.FocusedRowHandle, "NAME").ToString();
                adminuserename1 = gridViewGroupInfo.GetRowCellValue(gridViewGroupInfo.FocusedRowHandle, "ADMINUSERNAME1").ToString();
                adminuserename2 = gridViewGroupInfo.GetRowCellValue(gridViewGroupInfo.FocusedRowHandle, "ADMINUSERNAME2").ToString();
            }
            if (gridViewGroupInfo.GetRowCellValue(gridViewGroupInfo.FocusedRowHandle, "GROUPDESCRIPTION") != null)
                groupDesc = gridViewGroupInfo.GetRowCellValue(gridViewGroupInfo.FocusedRowHandle, "GROUPDESCRIPTION").ToString();

            List<string> tag = new List<string>();
            tag.Add(groupName);
            tag.Add(groupid);

            this.treeListMain.Nodes.Clear();
            TreeListNode treeNode = this.treeListMain.AppendNode(new object[] { groupName + "(" + groupDesc + ")" }, null, tag);

            this.treeListMain.ExpandAll();

        }
        
        private void treeListMain_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            if (e.Node == null
            || e.Node.Nodes.Count > 0
            || e.Node.Tag == null
            || e.Node.ImageIndex == USERICON)
            {
                return;
            }
            ////根节点
            //if (e.Node.ParentNode == null)
            //{
            //    if ("XXLoginUserIDXX" != adminuserename1 && "XXLoginUserIDXX" != adminuserename2 && "XXLoginUserIDXX" != "admin")
            //    {
            //        e.Node.SelectImageIndex = e.Node.ImageIndex = GROUPICON_GRAY;
            //    }
            //    else
            //    {
            //        e.Node.SelectImageIndex = e.Node.ImageIndex = GROUPICON;
            //    }
            //}
            if (e.Node.ImageIndex == GROUPICON_GRAY)
            {
                //this.//EFMsgInfo = EP.EPES.EPESC0000110/*登录的用户无权限查看该群组下的成员！*/;
                return;
            }
            queryMember(e.Node);
            
        }

        //查询群组的子组和子用户
        private void queryMember(TreeListNode parentNode)
        {          
            parentNode.Nodes.Clear();

            EI.EIInfo inBlock = new EI.EIInfo();
            EI.EIInfo outBlock;

            inBlock.SetColName(1, "groupname");
            inBlock.SetColName(2, "userid");
            inBlock.SetColName(3, "formname");
            inBlock.SetColName(4, "buttname");
            inBlock.SetColName(5, "mode");
            inBlock.SetColName(6, "appname");
            inBlock.SetColName(7, "companycode");
            inBlock.SetColName(8, "loginuser");
            inBlock.SetColName(9, "deptename");

            inBlock.SetColVal(1, 1, ((List<string>)parentNode.Tag)[0]);
            inBlock.SetColVal(1, 2, "XXLoginUserIDXX");

            inBlock.SetColVal(1, "formname", "");
            inBlock.SetColVal(1, "buttname", "");
            inBlock.SetColVal(1, "mode", 2);

            inBlock.SetColVal(1, "appname", this.fgDevComboBoxEditApp.EditValue.ToString().Split(':')[0]);
            inBlock.SetColVal(1, "companycode", this.selectedCompanyCode);
            inBlock.SetColVal(1, "loginuser", "XXLoginUserIDXX");
            inBlock.SetColVal(1, "deptename", this.combDept.EditValue.ToString().Trim().Split(':')[0]);

            //调用SERVICE
            outBlock = EI.EITuxedo.CallService("epessubmem_inq", inBlock);

            if (outBlock.sys_info.flag != 0)
            {
                MessageBox.Show(outBlock.sys_info.msg, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //返回子组
            string groupName = "";
            string groupDesc = "";
            string groupID = "";
            string admin1 = "";
            string admin2 = "";
            string isadmin = outBlock.GetColVal(4, 1, "isadmin");
            for (int i = 0; i < outBlock.blk_info[0].Row; i++)
            {
                groupName = outBlock.GetColVal(1, i + 1, "name");
                groupDesc = outBlock.GetColVal(1, i + 1, "groupdescription");
                groupID = outBlock.GetColVal(1, i + 1, "id");
                admin1 = outBlock.GetColVal(1, i + 1, "adminuserename1");
                admin2 = outBlock.GetColVal(1, i + 1, "adminuserename2");

                TreeListNode treeNode = this.treeListMain.AppendNode(new object[] { groupName + "(" + groupDesc + ")", " " }, parentNode);
                treeNode.Tag = new List<string>();
                ((List<string>)treeNode.Tag).Add(groupName);
                ((List<string>)treeNode.Tag).Add(groupID);
                if ("XXLoginUserIDXX" == admin1 || "XXLoginUserIDXX" == admin2 || isadmin == "1")
                {
                    treeNode.SelectImageIndex = treeNode.ImageIndex = GROUPICON;
                }
                else
                {
                    treeNode.SelectImageIndex = treeNode.ImageIndex = GROUPICON_GRAY;
                }
            }

            //返回子用户
            string ename = "";
            string cname = "";
            string userid = "";
            string dept = "";
            for (int i = 0; i < outBlock.blk_info[2].Row; i++)
            {
                ename = outBlock.GetColVal(3, i + 1, "ename");
                cname = outBlock.GetColVal(3, i + 1, "cname");
                userid = outBlock.GetColVal(3, i + 1, "id");
                dept = outBlock.GetColVal(3, i + 1, "dept");
                TreeListNode treeNode = this.treeListMain.AppendNode(new object[] { ename + "(" + cname +")", dept }, parentNode);
                treeNode.Tag = new List<string>();
                ((List<string>)treeNode.Tag).Add(ename);
                ((List<string>)treeNode.Tag).Add(userid);
                treeNode.SelectImageIndex = treeNode.ImageIndex = USERICON;
            }
        }

        #endregion

        #region 为群组新增子组及用户

        private void treeListMain_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            //不允许拖拽treelist的节点
            treeListMain.OptionsBehavior.DragNodes = false;
        }


        private void gridViewGroupInfo_MouseMove(object sender, MouseEventArgs e)
        {
            if (groupSaveStatus != 0)
            {
                return;
            }
            if (this.fgDevGridGroupInfo.DataSource == null || fgDevGridGroupInfo.EFChoiceCount == 0)
            {
                return;
            }
            if (e.Button != MouseButtons.Left) return;

            treeListMain.OptionsBehavior.DragNodes = true;

            List<string> data = new List<string>();

            fgDevGridGroupInfo.DoDragDrop(data, DragDropEffects.Copy);
        }

        private void gridViewUserInfo_MouseMove(object sender, MouseEventArgs e)
        {
            if (userSaveStatus != 0)
            {
                return;
            }
            if (this.fgDevGridUserInfo.DataSource == null || fgDevGridUserInfo.EFChoiceCount == 0)
            {
                return;
            }
            if (e.Button != MouseButtons.Left) return;

            treeListMain.OptionsBehavior.DragNodes = true;

            List<string> data = new List<string>();
            fgDevGridUserInfo.DoDragDrop(data, DragDropEffects.Copy);
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
            //this.//EFMsgInfo = "";

            DevExpress.XtraTreeList.TreeListHitInfo hi = treeListMain.CalcHitInfo(treeListMain.PointToClient(new Point(e.X, e.Y)));

            TreeListNode parentNode = null;

            //拖拽至用户节点
            if (hi.Node.ImageIndex == USERICON)
            {
                parentNode = hi.Node.ParentNode;
            }
            //拖拽至群组节点
            else if (hi.Node.ImageIndex == GROUPICON || hi.Node.ImageIndex == GROUPICON_GRAY)
            {
                parentNode = hi.Node;
            }

            EI.EIInfo inBlock = new EI.EIInfo();
            EI.EIInfo outBlock = new EI.EIInfo();

            //为群组新增子组
            if (xtraTabControl1.SelectedTabPage == xtraTabPageGroup)
            {
                string parentGroup = ((List<string>)parentNode.Tag)[0];

                if (parentGroup == "admingroup"
                    || parentGroup == "formgroup"
                    || parentGroup == "usermanager"
                    || parentGroup == "groupmanager"
                    || parentGroup == "resourcemanager")
                {
                    MessageBox.Show(EP.EPES.EPESC0000193/*系统群组下不可挂子组，请将角色用户直接添加到该组下！*/, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                inBlock.SetColName(1, 1, "ID");
                inBlock.SetColName(1, 2, "userid");

                inBlock.SetColVal(1, 1, "ID", ((List<string>)parentNode.Tag)[1]);
                inBlock.SetColVal(1, 1, "userid", "XXLoginUserIDXX");

                inBlock.AddNewBlock();
                inBlock.SetColName(1, "ID");

                for (int i = 0, j = 1; i < this.gridViewGroupInfo.RowCount; i++)
                {
                    if (fgDevGridGroupInfo.GetSelectedColumnChecked(i))
                    {
                        inBlock.SetColVal(2, j, "ID", this.gridViewGroupInfo.GetRowCellValue(i, "ID").ToString());
                        j++;
                    }
                }

                outBlock = EI.EITuxedo.CallService("epesgrgr_ins", inBlock);

                if (outBlock.sys_info.flag == 0)
                {
                    queryMember(parentNode);

                    //取消列表框中所有checkbox选中状态
                    UnCheckAll(fgDevGridGroupInfo, gridViewGroupInfo);
                }
            }
            //为群组新增子用户
            else if (xtraTabControl1.SelectedTabPage == xtraTabPageUser)
            {
                inBlock.SetColName(1, 1, "groupid");
                inBlock.SetColName(1, 2, "groupname");
                inBlock.SetColName(1, 3, "user");
                inBlock.SetColName(1, 4, "authmode");

                inBlock.SetColVal(1, 1, "groupid", ((List<string>)parentNode.Tag)[1]);
                inBlock.SetColVal(1, 1, "groupname", ((List<string>)parentNode.Tag)[0]);
                inBlock.SetColVal(1, 1, "user", "XXLoginUserIDXX");
                inBlock.SetColVal(1, 1, "authmode", (EPESCommon.AuthMode == AUTHMODE.MODE_9672) ? 1 : 0);

                inBlock.AddNewBlock();
                inBlock.SetColName(1, "userid");
                inBlock.SetColName(2, "username");

                for (int i = 0, j = 1; i < this.gridViewUserInfo.RowCount; i++)
                {
                    if (fgDevGridUserInfo.GetSelectedColumnChecked(i))
                    {
                        inBlock.SetColVal(2, j, "userid", this.gridViewUserInfo.GetRowCellValue(i, "ID").ToString());
                        inBlock.SetColVal(2, j, "username", this.gridViewUserInfo.GetRowCellValue(i, "ENAME").ToString());
                        j++;
                    }
                }

                outBlock = EI.EITuxedo.CallService("epesgrus_ins", inBlock);

                if (outBlock.sys_info.flag == 0)
                {
                    queryMember(parentNode);

                   // 取消列表框中所有checkbox选中状态
                    UnCheckAll(fgDevGridUserInfo, gridViewUserInfo);
                }
            }

            ShowReturnMsg(outBlock);

            treeListMain.OptionsBehavior.DragNodes = false;
        }

        #endregion

        #region 为群组删除子组或用户

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
                        //this.//EFMsgInfo = EP.EPES.EPESC0000111/*只能选择同级节点！*/;
                    }
                }

                //只有同级节点可被选中
                if (treeListMain.FocusedNode.ParentNode != parent)
                {
                    treeListMain.FocusedNode.Selected = false;
                    treeListMain.FocusedNode = treeListMain.Selection[0];

                    //this.//EFMsgInfo = EP.EPES.EPESC0000111/*只能选择同级节点！*/;
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

                        if (hi.Node.Level > 0 && isManageMode)
                        {
                            this.popupMenuTreeList.ShowPopup(MousePosition);

                        }
                    }
                }
                
            }
        }

        private void popupMenuTreeList_BeforePopup(object sender, CancelEventArgs e)
        {
            if (treeListMain.Selection.Count == 1)
            {
                if (treeListMain.Selection[0].ParentNode == null)
                {
                    this.barBtnItemDeleteUser.Enabled = false;
                }
                else
                {
                    this.barBtnItemDeleteUser.Enabled = true;
                }
                
            }
            else
            {
                this.barBtnItemDeleteUser.Enabled = true;
            }
        }

        //删除群组成员
        private void barButtonItemDeleteUser_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //if (this.treeListMain.FocusedNode == null || this.treeListMain.FocusedNode.ParentNode == null)
            //{
            //    return;
            //}

            if (treeListMain.Selection.Count == 0)
            {
                return;
            }

            EI.EIInfo inBlock = new EI.EIInfo();
            EI.EIInfo outBlock = new EI.EIInfo();

            inBlock.SetColName(1, 1, "groupid");
            inBlock.SetColName(1, 2, "user");

            inBlock.SetColVal(1, 1, "groupid", ((List<string>)parent.Tag)[1]);
            inBlock.SetColVal(1, 1, "user", "XXLoginUserIDXX");

            //子组
            inBlock.AddNewBlock();
            inBlock.SetColName(1, "id");

            for (int i = 0, j = 1; i < this.treeListMain.Selection.Count; i++)
            {
                //子组节点
                if (this.treeListMain.Selection[i].ImageIndex == GROUPICON || this.treeListMain.Selection[i].ImageIndex == GROUPICON_GRAY)
                {
                    inBlock.SetColVal(2, j, "id", ((List<string>)this.treeListMain.Selection[i].Tag)[1]);
                    j++;
                }
            }

            //子用户
            inBlock.AddNewBlock();
            inBlock.SetColName(1, "id");

            for (int i = 0, j = 1; i < this.treeListMain.Selection.Count; i++)
            {
                //子用户节点
                if (this.treeListMain.Selection[i].ImageIndex == USERICON)
                {
                    inBlock.SetColVal(3, j, "id", ((List<string>)this.treeListMain.Selection[i].Tag)[1]);
                    j++;
                }
            }

            outBlock = EI.EITuxedo.CallService("epessubmem_del", inBlock);

            ShowReturnMsg(outBlock);

            if (outBlock.sys_info.flag == 0)
            {
                queryMember(parent);
                parent = null;
            }
        }

        #endregion

#endregion


#region 用户信息

        #region 查询

        //查询用户信息
        private void fgButtonQryUsr_Click(object sender, EventArgs e)
        {
            pageIndex = 1;
            QueryUser();
        }

        private void QueryUser()
        {
            userEname = fgtEname.Text;
            userCname = fgtCname.Text;
            userDeptName = this.fgDevComboBoxEditDept.EditValue.ToString().Trim().Split(':')[0];

            EI.EIInfo outblk = queryUser();

            int totalcount = Convert.ToInt32(outblk.blk_info[1].colvalue[0, 0]);
            totalPage = totalcount / pageCount;
            if (totalcount % pageCount != 0 || totalcount == 0) totalPage++;

            //设置翻页按钮状态(是否可用)
            SetPageState();
        }
        
        //查询用户信息
        private EI.EIInfo queryUser()
        {
            //this.//EFMsgInfo = "";

            EI.EIInfo inblk = new EI.EIInfo();
            EI.EIInfo outblk = new EI.EIInfo();

            inblk.SetColName(1, "ename");
            inblk.SetColName(2, "cname");
            inblk.SetColName(3, "userid");
            inblk.SetColName(4, "dept_ename");
            inblk.SetColName(5, "q_appname");
            inblk.SetColName(6, "page_index");

            inblk.SetColVal(1, "cname", userCname);
            inblk.SetColVal(1, "ename", userEname);
            inblk.SetColVal(1, "userid", "XXLoginUserIDXX");
            inblk.SetColVal(1, "dept_ename", userDeptName);
            inblk.SetColVal(1, "q_appname", fgDevComboBoxEditApp.EditValue.ToString().Split(':')[0]);
            inblk.SetColVal(1, "page_index", pageIndex);

            outblk = EI.EITuxedo.CallService("epesuser_inq2", inblk);

            dataSetEPESSUBJ.TESUSERINFO.Clear();
            outblk.ConvertToStrongType(dataSetEPESSUBJ);
            dataSetEPESSUBJ.TESUSERINFO.AcceptChanges();

            gridViewUserInfo_FocusedRowChanged(null, null);

            ShowReturnMsg(outblk);

            userSaveStatus = 0;
        
            return outblk;
        }
        /// <summary>
        /// 设置翻页按钮状态
        /// </summary>
        private void SetPageState()
        {
            //页数信息(第几页共几页)
            this.fgDevGridUserInfo.RecordCountMessage = string.Format("{0}/{1}", pageIndex , totalPage);

            //上一页,第一页按钮,是否可用
            this.fgDevGridUserInfo.PrePageButtonEnable = this.fgDevGridUserInfo.FirstPageButtonEnable = !(pageIndex <= 1); // 不能上一页和第一页
            //下一页,最后一页 ,是否可用
            this.fgDevGridUserInfo.NextPageButtonEnable = this.fgDevGridUserInfo.LastPageButtonEnable = !(pageIndex >= totalPage);
        }

        #endregion

        void UserInfoEmbeddedNavigator_ButtonClick(object sender, NavigatorButtonClickEventArgs e)
        {
            fgDevGridUserInfo.Parent.Focus();
            switch (e.Button.ImageIndex)
            {
                case FIRSTPAGE:
                    pageIndex = 1;
                    QueryUser();
                    break;
                case PREPAGE:
                    pageIndex--;
                    QueryUser();
                    break;
                case NEXTPAGE:
                    pageIndex++;
                    QueryUser();
                    break;
                case LASTPAGE:
                    pageIndex = totalPage;
                    QueryUser();
                    break;
                case USERSAVE:
                    if (userCheck())
                    {
                        if (SaveUserInfo())
                        {
                            QueryUser();
                        }
                    }
                    break;
                case USERDISCARD:
                    QueryUser();
                    break;
                case INITPASS:
                    InitUserPassword();
                    break;
                Default:
                    break;
            }
        }

        //保存
        private bool SaveUserInfo()
        {
            DataTable instable = this.dataSetEPESSUBJ.TESUSERINFO.Clone();
            DataTable deltable = null;
            DataTable updtable = this.dataSetEPESSUBJ.TESUSERINFO.Clone();

            //FilterData(instable);
            //FilterData(updtable);
            DataRow dr = null;
            for (int rowIndex = 0; rowIndex < gridViewUserInfo.RowCount; ++rowIndex)
            {
                if (fgDevGridUserInfo.GetSelectedColumnChecked(rowIndex))
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

            deltable = dataSetEPESSUBJ.TESUSERINFO.GetChanges(DataRowState.Deleted);

            EI.EIInfo inBlock = new EI.EIInfo();

            inBlock.SetColName(1, "userid");
            inBlock.SetColVal(1, "userid", "XXLoginUserIDXX");

            inBlock.SetColName(2, "appname");
            inBlock.SetColVal(1, "appname", this.fgDevComboBoxEditApp.EditValue.ToString().Split(':')[0]);

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

                EI.EIInfo outBlock = EI.EITuxedo.CallService("epesuser_do", inBlock);
                if (outBlock.sys_info.flag < 0)
                {
                    MessageBox.Show(outBlock.sys_info.msg, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            return true;
        }

         /// <summary>
        /// 检查数据正确性
        /// </summary>
        /// <returns></returns>true 正确 false 存在问题
        private bool userCheck()
        {
            this.tESUSERINFOBindingSource.EndEdit();
            //检查数据准确性
            for (int i = 0; i < this.gridViewUserInfo.RowCount; i++)
            {
                if (//gridViewUserInfo.GetRowCellValue(i, "selected") == null
                    //|| gridViewUserInfo.GetRowCellValue(i, "selected").ToString() == "False"
                   ! fgDevGridUserInfo.GetSelectedColumnChecked(i))
                {
                    continue;
                }
                if (gridViewUserInfo.GetRowCellValue(i,"ENAME") != null)
                {
                    if (gridViewUserInfo.GetRowCellValue(i, "ENAME").ToString().Trim() == "")
                    {
                        MessageBox.Show(EP.EPES.EPESC0000129/*工号不能为空，请重新输入！*/, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    if (EPESCommon.GetStringLength(gridViewUserInfo.GetRowCellValue(i, "ENAME").ToString().Trim()) < 3)
                    {
                        MessageBox.Show(EP.EPES.EPESC0000130/*工号不能少于3位，请重新输入！*/, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    if (EPESCommon.GetStringLength(gridViewUserInfo.GetRowCellValue(i, "ENAME").ToString().Trim()) 
                        >6)
                    {
                        MessageBox.Show(
                            string.Format(EP.EPES.EPESC0000131/*工号不能多于[{0}]位，请重新输入！*/, 6),
                            "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
                else
                {
                    MessageBox.Show(EP.EPES.EPESC0000129/*工号不能为空，请重新输入！*/, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                if (gridViewUserInfo.GetRowCellValue(i, "CNAME") != null)
                {
                    if (gridViewUserInfo.GetRowCellValue(i, "CNAME").ToString().Trim() == "")
                    {
                        MessageBox.Show(EP.EPES.EPESC0000132/*中文姓名不能为空，请重新输入！*/, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    if (EPESCommon.GetStringLength(gridViewUserInfo.GetRowCellValue(i, "CNAME").ToString().Trim()) > 20)
                    {
                        MessageBox.Show(EP.EPES.EPESC0000133/*中文姓名不能多于20位，请重新输入！*/, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
                else
                {
                    MessageBox.Show(EP.EPES.EPESC0000132/*中文姓名不能为空，请重新输入！*/, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                if (gridViewUserInfo.GetRowCellValue(i, "ISENABLE") != null)
                {
                    if (gridViewUserInfo.GetRowCellValue(i, "ISENABLE").ToString().Trim() == "")
                    {
                        MessageBox.Show(EP.EPES.EPESC0000134/*是否有效不能为空，请重新输入！*/, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
                else
                {
                    MessageBox.Show(EP.EPES.EPESC0000134/*是否有效不能为空，请重新输入！*/, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                if (gridViewUserInfo.GetRowCellValue(i, "DEPTID") != null)
                {
                    if (gridViewUserInfo.GetRowCellValue(i, "DEPTID").ToString().Trim() == "")
                    {
                        MessageBox.Show(EP.EPES.EPESC0000135/*部门标识不能为空，请重新输入！*/, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
                else
                {
                    MessageBox.Show(EP.EPES.EPESC0000135/*部门标识不能为空，请重新输入！*/, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                if (gridViewUserInfo.GetRowCellValue(i, "PASSWD_PERIOD") != null)
                {
                    if (gridViewUserInfo.GetRowCellValue(i, "PASSWD_PERIOD").ToString().Trim() == "")
                    {
                        MessageBox.Show(EP.EPES.EPESC0000136/*有效天数不能为空，请重新输入！*/, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    else if (Convert.ToInt32(gridViewUserInfo.GetRowCellValue(i, "PASSWD_PERIOD").ToString()) > 180)
                    {
                        MessageBox.Show(EP.EPES.EPESC0000187/*有效天数不能超过180天*/, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
                else
                {
                    MessageBox.Show(EP.EPES.EPESC0000136/*有效天数不能为空，请重新输入！*/, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

            }

            return true;

        }

         #region 初始化用户密码

         private void InitUserPassword()
        {
            //this.//EFMsgInfo = "";

            if (fgDevGridUserInfo.EFChoiceCount < 1)
            {
                //this.//EFMsgInfo = EP.EPES.EPESC0000114/* "未选中行!*/;
                return;
            }

            EI.EIInfo inblku = new EI.EIInfo();
            EI.EIInfo outblku = new EI.EIInfo();

            inblku.AddColName(1, "ename");
            inblku.AddNewBlock();
            inblku.AddColName(2, "loginname");
            inblku.AddColName(2, "appname");

            inblku.SetColVal(2, 1, "loginname",  "EventArgs.formUserId");
            inblku.SetColVal(2, 1, "appname",  "EventArgs.epEname");

            for (int i = 0, j = 1; i < this.gridViewUserInfo.RowCount; i++)
            {
                //取出选中行
                if (fgDevGridUserInfo.GetSelectedColumnChecked(i))
                {
                    inblku.SetColVal(1, j, "ename", this.gridViewUserInfo.GetRowCellValue(i, "ENAME").ToString());
                    j++;
                }
            }

            outblku = EI.EITuxedo.CallService("epesuserpw_ini", inblku);

            ShowReturnMsg(outblku);

            if (outblku.sys_info.flag == 0)
            {
                MessageBox.Show(EP.EPES.EPESC0000115/*初始化密码成功*/, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //UnCheckAll(gridViewUserInfo, colselected1);
                QueryUser();
            }

            //else if (outblku.sys_info.flag < 0)
            //{
            //    MessageBox.Show(outblku.sys_info.msg, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        #endregion

        #region 修改锁定配置

        //修改用户锁定配置
        private void fgButtonUserConfig_Click(object sender, EventArgs e)
        {

        }

        #endregion
#endregion


#region 部门信息

        #region 查询

        //查询部门信息
        private void fgButtonQryDept_Click(object sender, EventArgs e)
        {
            queryDept();
        }

        //查询部门信息
        private void queryDept()
        {
            //this.//EFMsgInfo = "";
            EI.EIInfo inblku = new EI.EIInfo();
            EI.EIInfo outBlock;
            inblku.SetColName(1, "ename");
            inblku.SetColVal(1, 1, fgtDeptEname.Text);
            inblku.SetColName(2, "cname");
            inblku.SetColVal(1, 2, fgtDetpCname.Text);
            inblku.SetColName(3, "user_name");
            inblku.SetColVal(1, 3,  "EventArgs.formUserId");
            inblku.SetColName(4, "appname");
            inblku.SetColVal(1, 4, this.fgDevComboBoxEditApp.EditValue.ToString().Split(':')[0]);

            outBlock = EI.EITuxedo.CallService("epesdept_inq2", inblku);

            dataSetEPESSUBJ.TESDEPTINFO.Rows.Clear();
            outBlock.ConvertToStrongType(dataSetEPESSUBJ);
            dataSetEPESSUBJ.TESDEPTINFO.AcceptChanges();

            //gridViewDeptInfo.BestFitColumns();

            ShowReturnMsg(outBlock);

            deptSaveStatus = 0;
        }

        #endregion

        private void DeptInfoEmbeddedNavigator_ButtonClick(object sender, NavigatorButtonClickEventArgs e)
        {
            fgDevGridDeptInfo.Parent.Focus();
            switch (e.Button.ImageIndex)
            {
                case SAVE:
                    if (deptCheck())
                    {
                        if (SaveDeptInfo())
                        {
                            queryDept();
                        }
                    }
                    break;
                case DISCARD:
                    queryDept();
                    break;
                Default:
                    break;
            }
        }


        //保存
        private bool SaveDeptInfo()
        {
            DataTable instable = this.dataSetEPESSUBJ.TESDEPTINFO.Clone();
            DataTable deltable = null; // this.dataSetEPESSUBJ.TESDEPTINFO.GetChanges(DataRowState.Deleted);
            DataTable updtable = this.dataSetEPESSUBJ.TESDEPTINFO.Clone();

            //FilterData(instable);
            //FilterData(updtable);
            DataRow dr = null;
            for (int rowIndex = 0; rowIndex < gridViewDeptInfo.RowCount; ++rowIndex)
            {
                if (fgDevGridDeptInfo.GetSelectedColumnChecked(rowIndex))
                {
                    dr = gridViewDeptInfo.GetDataRow(rowIndex);
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

            deltable = dataSetEPESSUBJ.TESDEPTINFO.GetChanges(DataRowState.Deleted);

            EI.EIInfo inBlock = new EI.EIInfo();

            inBlock.SetColName(1, "userid");
            inBlock.SetColVal(1, "userid", "XXLoginUserIDXX");

            inBlock.SetColName(2, "appname");
            inBlock.SetColVal(1, "appname", this.fgDevComboBoxEditApp.EditValue.ToString().Split(':')[0]);

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

                EI.EIInfo outBlock = EI.EITuxedo.CallService("epesdept_do", inBlock);
                if (outBlock.sys_info.flag < 0)
                {
                    MessageBox.Show(outBlock.sys_info.msg, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            return true;
        }

         /// <summary>
        /// 检查数据正确性
        /// </summary>
        /// <returns></returns>true 正确 false 存在问题
        private bool deptCheck()
        {
            this.tESDEPTINFOBindingSource.EndEdit();
            //检查数据准确性
            for (int i = 0; i < this.gridViewDeptInfo.RowCount; i++)
            {
                if (//gridViewDeptInfo.GetRowCellValue(i, "selected") == null
                    //|| gridViewDeptInfo.GetRowCellValue(i, "selected").ToString() == "False"
                    ! fgDevGridDeptInfo.GetSelectedColumnChecked(i))
                {
                    continue;
                }
                //检查输入合法性
                if (gridViewDeptInfo.GetRowCellValue(i,"ENAME") != null)
                {
                    if (gridViewDeptInfo.GetRowCellValue(i, "ENAME").ToString().Trim() == "")
                    {
                        MessageBox.Show(EP.EPES.EPESC0000137/*部门号不能为空，请重新输入！*/, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    if (EPESCommon.GetStringLength(gridViewDeptInfo.GetRowCellValue(i, "ENAME").ToString()) > 18)
                    {
                        MessageBox.Show(EP.EPES.EPESC0000138/*部门号不能超过18位，请重新输入！*/, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
                else
                {
                    MessageBox.Show(EP.EPES.EPESC0000137/*部门号不能为空，请重新输入！*/, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                if (gridViewDeptInfo.GetRowCellValue(i, "CNAME") != null)
                {
                    if (gridViewDeptInfo.GetRowCellValue(i, "CNAME").ToString().Trim() == "")
                    {
                        MessageBox.Show(EP.EPES.EPESC0000139/*部门中文名称不能为空，请重新输入！*/, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    if (EPESCommon.GetStringLength(gridViewDeptInfo.GetRowCellValue(i, "CNAME").ToString()) > 50)
                    {
                        MessageBox.Show(EP.EPES.EPESC0000140/*部门中文名不能超过50位，请重新输入！*/, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
                else
                {
                    MessageBox.Show(EP.EPES.EPESC0000139/*部门中文名称不能为空，请重新输入！*/, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                if (gridViewDeptInfo.GetRowCellValue(i, "FDEPARTMENT") != null)
                {
                    if (EPESCommon.GetStringLength(gridViewDeptInfo.GetRowCellValue(i, "FDEPARTMENT").ToString()) > 50)
                    {
                        MessageBox.Show(EP.EPES.EPESC0000141/*上层部门不能超过50位，请重新输入！*/, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }

                    if (gridViewDeptInfo.GetRowCellValue(i, "FDEPARTMENT").ToString() == gridViewDeptInfo.GetRowCellValue(i, "ENAME").ToString())
                    {
                         MessageBox.Show(EP.EPES.EPESC0000201/*上层部门号不能与部门号相同！若无上层部门，上层部门号可不填写*/, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }

                if (gridViewDeptInfo.GetRowCellValue(i, "DEPART_LEVEL") != null)
                {
                    if (EPESCommon.GetStringLength(gridViewDeptInfo.GetRowCellValue(i, "DEPART_LEVEL").ToString()) > 2)
                    {
                        MessageBox.Show(EP.EPES.EPESC0000142/*部门标识不能超过2位，请重新输入！*/, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }

                if (gridViewDeptInfo.GetRowCellValue(i, "DEPT_ADMIN1") != null)
                {
                    if (EPESCommon.GetStringLength(gridViewDeptInfo.GetRowCellValue(i, "DEPT_ADMIN1").ToString()) > 32)
                    {
                        MessageBox.Show(EP.EPES.EPESC0000143/*部门管理员不能超过32位，请重新输入！*/, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }

                if (gridViewDeptInfo.GetRowCellValue(i, "DEPT_ADMIN2") != null)
                {
                    if (EPESCommon.GetStringLength(gridViewDeptInfo.GetRowCellValue(i, "DEPT_ADMIN2").ToString()) > 32)
                    {
                        MessageBox.Show(EP.EPES.EPESC0000143/*部门管理员不能超过32位，请重新输入！*/, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }

            return true;

        }

#endregion


#region 其他

        //获取当前checkbox选中行数
        private int getChoiceCount(GridView gridview)
        {
            int choiceCount = 0;
            for (int i = 0; i < gridview.RowCount; i++)
            {
                if (gridview.GetRowCellValue(i, "selected") != null)
                {
                    if ((bool)gridview.GetRowCellValue(i, "selected") == true)
                    {
                        choiceCount++;
                    }
                }
                
            }
            return choiceCount;
        }

        private void ShowReturnMsg(EI.EIInfo outBlock)
        {
            //设置返回信息
            if (outBlock.sys_info.flag == 0)
            {
                //this.//EFMsgInfo = string.Format(EP.EPES.EPESC0000118/*成功执行 sqlcode is {0}*/, outBlock.sys_info.sqlcode.ToString());

            }
            else if (outBlock.sys_info.flag < 0)
            {
                //this.//EFMsgInfo = string.Format(EP.EPES.EPESC0000119/*后台程序调用失败! {0}*/, outBlock.sys_info.msg);

                //MessageBox.Show(//this.//EFMsgInfo, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        //取消选择所有记录
        private void UnCheckAll(EF.EFDevGrid grid, GridView gridView)
        {
            for (int i = 0; i < gridView.RowCount; i++)
            {
                grid.SetSelectedColumnChecked(i, false);
            }
            gridView.Invalidate();
        }

      
#endregion

        private void InitDevGridCustomButtons()
        {
            fgDevGridUserInfo.EmbeddedNavigator.Buttons.CustomButtons[PREPAGE].Visible = true;
            fgDevGridUserInfo.EmbeddedNavigator.Buttons.CustomButtons[NEXTPAGE].Visible = true;
            fgDevGridUserInfo.EmbeddedNavigator.Buttons.CustomButtons[PREPAGE].Enabled = false;
            fgDevGridUserInfo.EmbeddedNavigator.Buttons.CustomButtons[NEXTPAGE].Enabled = false;
            fgDevGridUserInfo.EmbeddedNavigator.Buttons.CustomButtons[FIRSTPAGE].Visible = true;
            fgDevGridUserInfo.EmbeddedNavigator.Buttons.CustomButtons[LASTPAGE].Visible = true;
            fgDevGridUserInfo.EmbeddedNavigator.Buttons.CustomButtons[FIRSTPAGE].Enabled = false;
            fgDevGridUserInfo.EmbeddedNavigator.Buttons.CustomButtons[LASTPAGE].Enabled = false;

            ((ImageList)this.fgDevGridGroupInfo.EmbeddedNavigator.Buttons.ImageList).Images.Add(this.imageListGridPageBar.Images[1]);
            ((ImageList)this.fgDevGridGroupInfo.EmbeddedNavigator.Buttons.ImageList).Images.Add(this.imageListGridPageBar.Images[2]);

            this.fgDevGridGroupInfo.EmbeddedNavigator.Buttons.CustomButtons.AddRange(new NavigatorCustomButton[] {
            new NavigatorCustomButton(SAVE, "保存"),
            new NavigatorCustomButton(DISCARD, "放弃")            
            });

            fgDevGridGroupInfo.EmbeddedNavigator.Buttons.CustomButtons[DISCARD].Visible = false;
            fgDevGridGroupInfo.EmbeddedNavigator.Buttons.CustomButtons[SAVE].Visible = false;


            ((ImageList)this.fgDevGridUserInfo.EmbeddedNavigator.Buttons.ImageList).Images.Add(this.imageListGridPageBar.Images[5]);
            ((ImageList)this.fgDevGridUserInfo.EmbeddedNavigator.Buttons.ImageList).Images.Add(this.imageListGridPageBar.Images[1]);
            ((ImageList)this.fgDevGridUserInfo.EmbeddedNavigator.Buttons.ImageList).Images.Add(this.imageListGridPageBar.Images[2]);


            this.fgDevGridUserInfo.EmbeddedNavigator.Buttons.CustomButtons.AddRange(new NavigatorCustomButton[] {
            new NavigatorCustomButton(INITPASS, EP.EPES.EPESC0000120/*初始化密码*/),
            new NavigatorCustomButton(USERSAVE, "保存"),
            new NavigatorCustomButton(USERDISCARD, "放弃")
            });

            fgDevGridUserInfo.EmbeddedNavigator.Buttons.CustomButtons[INITPASS].Visible = false;
            fgDevGridUserInfo.EmbeddedNavigator.Buttons.CustomButtons[USERDISCARD].Visible = false;
            fgDevGridUserInfo.EmbeddedNavigator.Buttons.CustomButtons[USERSAVE].Visible = false;

            ((ImageList)this.fgDevGridDeptInfo.EmbeddedNavigator.Buttons.ImageList).Images.Add(this.imageListGridPageBar.Images[1]);
            ((ImageList)this.fgDevGridDeptInfo.EmbeddedNavigator.Buttons.ImageList).Images.Add(this.imageListGridPageBar.Images[2]);

            this.fgDevGridDeptInfo.EmbeddedNavigator.Buttons.CustomButtons.AddRange(new NavigatorCustomButton[] {
                   new NavigatorCustomButton(SAVE, "保存"),
                   new NavigatorCustomButton(DISCARD, "放弃")              
                   });

            fgDevGridDeptInfo.EmbeddedNavigator.Buttons.CustomButtons[DISCARD].Visible = false;
            fgDevGridDeptInfo.EmbeddedNavigator.Buttons.CustomButtons[SAVE].Visible = false;
            
        }

 

        private bool isChange(DataTable table)
        {
            for (int rowIndex = 0; rowIndex < table.Rows.Count; ++rowIndex)
            {
                if (table.Rows[rowIndex].RowState == DataRowState.Deleted || table.Rows[rowIndex].RowState == DataRowState.Added || table.Rows[rowIndex].RowState == DataRowState.Modified)
                {
                    return true;
                }
            }
            return false;

        }

        private void repstryItemGroupBtnEditAdmin1_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            //this.//EFMsgInfo = "";

            ButtonEdit be = sender as ButtonEdit;
            object val = be.EditValue;
            if (val == null) return;

            string app = gridViewGroupInfo.GetRowCellValue(this.gridViewGroupInfo.FocusedRowHandle, "APPNAME").ToString();

            //if (app.Length < 2)
            //{
            //    MessageBox.Show("请先选择需要新增的群组子系统！");
            //    return;
            //}

            adminQry(val, gridViewGroupInfo, colADMINUSERNAME1,app);
        }

        private void repstryItemGroupBtnEditAdmin2_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            //this.//EFMsgInfo = "";

            ButtonEdit be = sender as ButtonEdit;
            object val = be.EditValue;
            if (val == null) return;

            string app = gridViewGroupInfo.GetRowCellValue(this.gridViewGroupInfo.FocusedRowHandle, "APPNAME").ToString();

            //if (app.Length < 2)
            //{
            //    MessageBox.Show("请先选择需要新增的群组子系统！");
            //    return;
            //}

            adminQry(val, gridViewGroupInfo, colADMINUSERNAME2, app);
        }

        private void gridViewGroupInfo_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column != fgDevGridGroupInfo.SelectionColumn)
            {
                //this.gridViewGroupInfo.SetRowCellValue(gridViewGroupInfo.FocusedRowHandle, "selected", true);
                fgDevGridGroupInfo.SetSelectedColumnChecked(e.RowHandle, true);
                //gridViewGroupInfo.SetRowCellValue(e.RowHandle, "selected", true);
            }
        }

        private void gridViewUserInfo_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column != fgDevGridUserInfo.SelectionColumn)
            {
                fgDevGridUserInfo.SetSelectedColumnChecked(e.RowHandle, true);
                //gridViewUserInfo.SetRowCellValue(e.RowHandle, "selected", true);
            }
        }

        private void gridViewDeptInfo_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column != fgDevGridDeptInfo.SelectionColumn)
            {
                //this.gridViewDeptInfo.SetRowCellValue(gridViewDeptInfo.FocusedRowHandle, "selected", true);
                fgDevGridDeptInfo.SetSelectedColumnChecked(e.RowHandle, true);
                //gridViewDeptInfo.SetRowCellValue(e.RowHandle, "selected", true);
            }

        }

        private void repstryItemLookUpEditUserDeptNo_EditValueChanged(object sender, EventArgs e)
        {
            LookUpEdit be = sender as LookUpEdit;
            string ename = be.GetColumnValue("ename").ToString();
            string cname = be.GetColumnValue("cname").ToString();

            gridViewUserInfo.SetFocusedRowCellValue(colDEPT_ENAME, ename);
            gridViewUserInfo.SetFocusedRowCellValue(colDEPT_CNAME, cname);
        }

        private void repositoryItemDeptBtnEditAdmin1_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            //this.//EFMsgInfo = "";

            ButtonEdit be = sender as ButtonEdit;
            object val = be.EditValue;
            if (val == null) 
                return;

            string app = fgDevComboBoxEditApp.EditValue.ToString().Split(':')[0];

            adminQry(val, gridViewDeptInfo, colDEPT_ADMIN1,app);
        }

        private void repositoryItemDeptBtnEditAdmin2_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            //this.//EFMsgInfo = "";

            ButtonEdit be = sender as ButtonEdit;
            object val = be.EditValue;
            if (val == null)
                return;

            string app = fgDevComboBoxEditApp.EditValue.ToString().Split(':')[0];

            adminQry(val,gridViewDeptInfo,colDEPT_ADMIN2,app);           
        }  

        private void adminQry(object val,GridView grid, DevExpress.XtraGrid.Columns.GridColumn col,string app)
        {
             if (val.ToString().Length < 2)
            {
                //this.//EFMsgInfo = EP.EPES.EPESC0000109/*请输入两位查询字符*/;
                return;
            }

            //FormESSUBJADMIN frm = new FormESSUBJADMIN(val.ToString(), app, MousePosition);
            //frm.ShowDialog();

             string rst = "";// frm.Ename;
            if (rst != string.Empty)
            {
                grid.SetRowCellValue(grid.FocusedRowHandle, col, rst);
            }
        }

        //设置群组信息子系统列不能修改
        private void gridViewGroupInfo_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                if (gridViewGroupInfo.FocusedRowHandle >= 0 && gridViewGroupInfo.FocusedRowHandle < this.gridViewGroupInfo.RowCount)
                {
                    if (this.gridViewGroupInfo.GetFocusedDataRow().RowState == DataRowState.Added)
                    {
                        this.colAPPNAME.OptionsColumn.AllowEdit = true;
                    }
                    else
                    {
                        this.colAPPNAME.OptionsColumn.AllowEdit = false;
                    }
                }
            }
            catch (Exception ex)
            {
                ////EFMsgInfo = ex.Message;
            }
        }

        //设置用户信息工号不能修改
        private void gridViewUserInfo_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                if (gridViewUserInfo.FocusedRowHandle >= 0 && gridViewUserInfo.FocusedRowHandle < this.gridViewUserInfo.RowCount)
                {
                    if (this.gridViewUserInfo.GetFocusedDataRow().RowState == DataRowState.Added)
                    {
                        this.colENAME.OptionsColumn.AllowEdit = true;
                       
                    }
                    else
                    {
                        this.colENAME.OptionsColumn.AllowEdit = false;
                        
                    }
                }
            }
            catch (Exception ex)
            {
                ////EFMsgInfo = ex.Message;
            }
        }

        private void FormEPESSUBJ_EF_CANCEL_DO_F4(object sender, EventArgs e)
        {
            if (MessageBox.Show(EP.EPES.EPESC0000160/*是否退出维护模式？*/, "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                fgDevGridUserInfo.ShowAddRowButton = false;
                fgDevGridUserInfo.ShowAddCopyRowButton = false;
                fgDevGridUserInfo.ShowDeleteRowButton = false;
                fgDevGridUserInfo.SetAllColumnEditable(false);
                fgDevGridUserInfo.SelectionColumn.OptionsColumn.AllowEdit = true;

                fgDevGridUserInfo.EmbeddedNavigator.Buttons.CustomButtons[INITPASS].Visible = false;
                fgDevGridUserInfo.EmbeddedNavigator.Buttons.CustomButtons[DISCARD].Visible = false;
                fgDevGridUserInfo.EmbeddedNavigator.Buttons.CustomButtons[SAVE].Visible = false;

                fgDevGridUserInfo.ShowContextMenu = false;

                if (isChange(dataSetEPESSUBJ.TESUSERINFO))
                {
                    QueryUser();
                }
            }
            else
            {
                //this.//ef_args.buttonStatusHold = true;
            }
        }

        private void FormEPESSUBJ_EF_CANCEL_DO_F5(object sender, EventArgs e)
        {
            if (MessageBox.Show(EP.EPES.EPESC0000160/*是否退出维护模式？*/, "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                fgDevGridDeptInfo.ShowAddRowButton = false;
                fgDevGridDeptInfo.ShowAddCopyRowButton = false;
                fgDevGridDeptInfo.ShowDeleteRowButton = false;
                fgDevGridDeptInfo.SetAllColumnEditable(false);
                fgDevGridDeptInfo.SelectionColumn.OptionsColumn.AllowEdit = true;

                fgDevGridDeptInfo.EmbeddedNavigator.Buttons.CustomButtons[DISCARD].Visible = false;
                fgDevGridDeptInfo.EmbeddedNavigator.Buttons.CustomButtons[SAVE].Visible = false;

                fgDevGridDeptInfo.ShowContextMenu = false;

                if (isChange(dataSetEPESSUBJ.TESDEPTINFO))
                {
                    queryDept();
                }
            }
            else
            {
                //this.//ef_args.buttonStatusHold = true;
            }
        }

        private void FormEPESSUBJ_EF_DO_F4(object sender, EventArgs e)
        {
            bool success = true;

            if (isChange(dataSetEPESSUBJ.TESUSERINFO))
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
                fgDevGridUserInfo.ShowAddRowButton = false;
                fgDevGridUserInfo.ShowAddCopyRowButton = false;
                fgDevGridUserInfo.ShowDeleteRowButton = false;
                fgDevGridUserInfo.SetAllColumnEditable(false);
                fgDevGridUserInfo.SelectionColumn.OptionsColumn.AllowEdit = true;

                fgDevGridUserInfo.EmbeddedNavigator.Buttons.CustomButtons[INITPASS].Visible = false;
                fgDevGridUserInfo.EmbeddedNavigator.Buttons.CustomButtons[USERDISCARD].Visible = false;
                fgDevGridUserInfo.EmbeddedNavigator.Buttons.CustomButtons[USERSAVE].Visible = false;

                fgDevGridUserInfo.ShowContextMenu = false;
            }
            else
            {
                //this.//ef_args.buttonStatusHold = true;
            }
        }

        private void FormEPESSUBJ_EF_DO_F5(object sender, EventArgs e)
        {
            bool success = true;

            if (isChange(dataSetEPESSUBJ.TESDEPTINFO))
            {
                if (deptCheck())
                {
                    if (SaveDeptInfo())
                    {
                        queryDept();
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
                fgDevGridDeptInfo.ShowAddRowButton = false;
                fgDevGridDeptInfo.ShowAddCopyRowButton = false;
                fgDevGridDeptInfo.ShowDeleteRowButton = false;
                fgDevGridDeptInfo.SetAllColumnEditable(false);
                fgDevGridDeptInfo.SelectionColumn.OptionsColumn.AllowEdit = true;

                fgDevGridDeptInfo.EmbeddedNavigator.Buttons.CustomButtons[DISCARD].Visible = false;
                fgDevGridDeptInfo.EmbeddedNavigator.Buttons.CustomButtons[SAVE].Visible = false;

                fgDevGridDeptInfo.ShowContextMenu = false;
            }
            else
            {
                //this.//ef_args.buttonStatusHold = true;
            }
        }


        private void FormEPESSUBJ_EF_PRE_DO_F4(object sender, EventArgs e)
        {
            xtraTabControl1.SelectedTabPage = xtraTabPageUser;

            fgDevGridUserInfo.ShowAddRowButton = true;
            fgDevGridUserInfo.ShowAddCopyRowButton = true;
            fgDevGridUserInfo.ShowDeleteRowButton = true;
            fgDevGridUserInfo.SetAllColumnEditable(true);
            //colDEPT_ENAME.OptionsColumn.AllowEdit = false;
            colDEPT_CNAME.OptionsColumn.AllowEdit = false;
            colTIMETIRED.OptionsColumn.AllowEdit = false;

            fgDevGridUserInfo.ShowContextMenu = true;

            fgDevGridUserInfo.EmbeddedNavigator.Buttons.CustomButtons[INITPASS].Visible = true;
            fgDevGridUserInfo.EmbeddedNavigator.Buttons.CustomButtons[USERDISCARD].Visible = true;
            fgDevGridUserInfo.EmbeddedNavigator.Buttons.CustomButtons[USERSAVE].Visible = true;

            gridViewUserInfo_FocusedRowChanged(null, null);
        }

        private void FormEPESSUBJ_EF_PRE_DO_F5(object sender, EventArgs e)
        {
            xtraTabControl1.SelectedTabPage = xtraTabPageDept;

            fgDevGridDeptInfo.ShowAddRowButton = true;
            fgDevGridDeptInfo.ShowAddCopyRowButton = true;
            fgDevGridDeptInfo.ShowDeleteRowButton = true;
            fgDevGridDeptInfo.SetAllColumnEditable(true);

            fgDevGridDeptInfo.EmbeddedNavigator.Buttons.CustomButtons[DISCARD].Visible = true;
            fgDevGridDeptInfo.EmbeddedNavigator.Buttons.CustomButtons[SAVE].Visible = true;

            fgDevGridDeptInfo.ShowContextMenu = true;
        }

        //群组维护
        private void FormESSUBJ_EF_PRE_DO_F3(object sender,  EventArgs e)
        {
            xtraTabControl1.SelectedTabPage = xtraTabPageGroup;

            fgDevGridGroupInfo.ShowAddRowButton = true;
            fgDevGridGroupInfo.ShowAddCopyRowButton = true;
            fgDevGridGroupInfo.ShowDeleteRowButton = true;
            fgDevGridGroupInfo.SetAllColumnEditable(true);

            fgDevGridGroupInfo.EmbeddedNavigator.Buttons.CustomButtons[DISCARD].Visible = true;
            fgDevGridGroupInfo.EmbeddedNavigator.Buttons.CustomButtons[SAVE].Visible = true;

            fgDevGridUserInfo.SelectionColumn.OptionsColumn.AllowEdit = true;

            fgDevGridGroupInfo.ShowContextMenu = true;

            gridViewGroupInfo_FocusedRowChanged(null, null);

            isManageMode = true;
        }

        private void FormESSUBJ_EF_DO_F3(object sender,  EventArgs e)
        {
            bool success = true;

            if (isChange(dataSetEPESSUBJ.TESGROUPINFO))
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

            if (success)
            {
                fgDevGridGroupInfo.ShowAddRowButton = false;
                fgDevGridGroupInfo.ShowAddCopyRowButton = false;
                fgDevGridGroupInfo.ShowDeleteRowButton = false;
                fgDevGridGroupInfo.SetAllColumnEditable(false);
                fgDevGridGroupInfo.SelectionColumn.OptionsColumn.AllowEdit = true;

                fgDevGridGroupInfo.EmbeddedNavigator.Buttons.CustomButtons[DISCARD].Visible = false;
                fgDevGridGroupInfo.EmbeddedNavigator.Buttons.CustomButtons[SAVE].Visible = false;

                fgDevGridGroupInfo.ShowContextMenu = false;

                isManageMode = false;
            }
            else
            {
                //this.//ef_args.buttonStatusHold = true;
            }
        }

        private void FormESSUBJ_EF_CANCEL_DO_F3(object sender,  EventArgs e)
        {
            if (MessageBox.Show(EP.EPES.EPESC0000160/*是否退出维护模式？*/, "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                fgDevGridGroupInfo.ShowAddRowButton = false;
                fgDevGridGroupInfo.ShowAddCopyRowButton = false;
                fgDevGridGroupInfo.ShowDeleteRowButton = false;
                fgDevGridGroupInfo.SetAllColumnEditable(false);
                fgDevGridGroupInfo.SelectionColumn.OptionsColumn.AllowEdit = true;

                fgDevGridGroupInfo.EmbeddedNavigator.Buttons.CustomButtons[DISCARD].Visible = false;
                fgDevGridGroupInfo.EmbeddedNavigator.Buttons.CustomButtons[SAVE].Visible = false;

                fgDevGridGroupInfo.ShowContextMenu = false;

                isManageMode = false;

                if (isChange(dataSetEPESSUBJ.TESGROUPINFO))
                {
                    queryGroup();
                }
            }
            else
            {
                //this.//ef_args.buttonStatusHold = true;
            }
        }

        private void fgDevComboBoxEditApp_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.treeListMain.Nodes.Clear();
            dataSetEPESSUBJ.TESGROUPINFO.Rows.Clear();
        }

        private void fgDevComboBoxEditComp_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.treeListMain.Nodes.Clear();

            //if (fgDevComboBoxEditComp.EditValue.ToString().Split(':')[0] == "ALL")
            //{
            //    selectedCompanyCode = "";
            //}
            //else
            //{
                selectedCompanyCode = fgDevComboBoxEditComp.EditValue.ToString().Split(':')[0];
            //}
            dataSetEPESSUBJ.TESGROUPINFO.Rows.Clear();
        }

        private void fgDevGridUserInfo_DoubleClick(object sender, EventArgs e)
        {
            //this.//EFMsgInfo = "";
            if (this.gridViewUserInfo.RowCount == 0 || this.gridViewUserInfo.FocusedRowHandle < 0) return;

            this.treeListMain.Nodes.Clear();

            string username = "";

            if (this.gridViewUserInfo.GetRowCellValue(gridViewUserInfo.FocusedRowHandle, "ENAME") != null)
            {
                username = gridViewUserInfo.GetRowCellValue(gridViewUserInfo.FocusedRowHandle, "ENAME").ToString();
            }

            EI.EIInfo inblk = new EI.EIInfo();
            EI.EIInfo outblk = new EI.EIInfo();

            inblk.SetColName(1, "username");
            inblk.SetColName(2, "appname");
            inblk.SetColName(3, "companycode");
            inblk.SetColName(4, "loginuser");

            inblk.SetColVal(1, 1, username);
            inblk.SetColVal(1, 2, fgDevComboBoxEditApp.EditValue.ToString().Split(':')[0]);
            inblk.SetColVal(1, "companycode", "");
            inblk.SetColVal(1, "loginuser", "XXLoginUserIDXX");

            //查询用户所属/所管理的群组
            outblk = EI.EITuxedo.CallService("epesugroup_inq", inblk);

            string groupID = "", groupName = "", groupDesc = "", admin1 = "", admin2 = "";
            string isadmin = outblk.GetColVal(2, 1, "isadmin");

            this.treeListMain.Nodes.Clear();
            for (int i = 0; i < outblk.blk_info[0].Row; i++)
            {
                groupID = outblk.GetColVal(1, i + 1, "id");
                groupName = outblk.GetColVal(1, i + 1, "name");
                groupDesc = outblk.GetColVal(1, i + 1, "groupdescription");
                admin1 = outblk.GetColVal(1, i + 1, "adminuserename1");
                admin2 = outblk.GetColVal(1, i + 1, "adminuserename2");

                List<string> tag = new List<string>();
                tag.Add(groupName);
                tag.Add(groupID);

                TreeListNode treeNode = this.treeListMain.AppendNode(new object[] { groupName + "(" + groupDesc + ")" }, null, tag);
                //treeNode.Tag = groupName;

                if (admin1 == "XXLoginUserIDXX" || admin2 == "XXLoginUserIDXX" || isadmin == "1")
                {
                    treeNode.ImageIndex = treeNode.SelectImageIndex = GROUPICON;
                }
                else
                {
                    treeNode.ImageIndex = treeNode.SelectImageIndex = GROUPICON_GRAY;
                }
            }

            this.treeListMain.ExpandAll();
        }

        private void fgDevGridUserInfo_EF_GridBar_AddRow_Event(object sender, NavigatorButtonClickEventArgs e)
        {
            gridViewUserInfo.ClearSorting();
            gridViewUserInfo.AddNewRow();
            gridViewUserInfo.RefreshData();
            gridViewUserInfo.FocusedRowHandle = gridViewUserInfo.RowCount - 1;

        }

        private void fgDevGridGroupInfo_EF_GridBar_AddRow_Event(object sender, NavigatorButtonClickEventArgs e)
        {
            gridViewGroupInfo.ClearSorting();
            gridViewGroupInfo.AddNewRow();
            gridViewGroupInfo.RefreshData();
            gridViewGroupInfo.FocusedRowHandle = gridViewGroupInfo.RowCount - 1;
        }

        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            //fgDevGridUserInfo.Height = xtraTabControl1.Height - 60;
            //fgDevGridDeptInfo.Height = xtraTabControl1.Height - 60;
        }

        private void combDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            //this.//EFMsgInfo = "";
            if (treeListMain.Nodes.FirstNode == null
            || treeListMain.Nodes.FirstNode.Tag == null)
            {
                return;
            }

            queryMember(treeListMain.Nodes.FirstNode);

        }

        private void repositoryItemLookUpEditDeptNo_EditValueChanged(object sender, EventArgs e)
        {
            //LookUpEdit be = sender as LookUpEdit;
            //string ename = be.GetColumnValue("ename").ToString();
            //string cname = be.GetColumnValue("cname").ToString();

            //gridViewUserInfo.SetFocusedRowCellValue(colDEPT_ENAME, ename);
            //gridViewUserInfo.SetFocusedRowCellValue(colDEPT_CNAME, cname);
        }

        private void repositoryItemLookUpEditDeptName_EditValueChanged(object sender, EventArgs e)
        {
            LookUpEdit be = sender as LookUpEdit;
            string id = be.GetColumnValue("id").ToString();
            string cname = be.GetColumnValue("cname").ToString();

            gridViewUserInfo.SetFocusedRowCellValue(colDEPTID, id);
            gridViewUserInfo.SetFocusedRowCellValue(colDEPT_CNAME, cname);
        }


     }
}
