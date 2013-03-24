using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraBars;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using System.IO;
using System.Collections;
using System.Reflection;
using DevExpress.XtraTreeList;
using DevExpress.XtraEditors.Repository;


namespace EP
{

    /// <summary>
    /// 本页面主要实现客体管理功能，包括画面、按钮及菜单树管理。
    /// </summary>	
    public partial class FormResource : DevExpress.XtraEditors.XtraForm
    {
        #region 私有变量
        //图标编号
        private const int FOLDERICON = 0;
        private const int FOLDERICON_EXPAND = 1;
        private const int FORMICON = 2;
        private const int BUTTICON = 3;
        private const int RESGROUPICON = 0;
        private const int OTHICON = 7;

        private const int SAVE = 13;
        private const int DISCARD = 14;

        private string formName = ""; //所属画面名
        private string appName = ""; //所属画面名的子系统

        private ArrayList buttons = new ArrayList();
        private int x = 0;
        private int y = 0;

        private bool dragFromGrid = false;

        private Hashtable root_hash = new Hashtable(); //菜单树
        private Hashtable func_hash = new Hashtable(); //菜单树
        private Hashtable chart_hash = new Hashtable(); //菜单树
        private DataTable dtMenuTree = new DataTable();
        #endregion

        #region 画面初始化
        public FormResource()
        {
            InitializeComponent();
        }

        private void FormESOBJ_Load(object sender, EventArgs e) { }
        //{
        //    InitDevGridCustomButtons();

        //    InitOthType(this.GetResType());

        //    if (EPESCommon.AuthMode == AUTHMODE.MODE_CLASSIC)
        //    {
        //        this.xtraTabPageResGroup.PageVisible = false;
        //        this.chkAuth.Visible = false;
        //        this.chkOth.Visible = false;
        //    }

        //    //按钮操作模式
        //    DataTable dt = new DataTable();
        //    dt.Columns.Add("value").Caption = " ";
        //    dt.Columns.Add("desc").Caption = EP.EPES.EPESC0000030/*操作模式*/;
        //    dt.Rows.Add("A", EP.EPES.EPESC0000058/*单步操作*/);
        //    dt.Rows.Add("B", EP.EPES.EPESC0000032/*多步操作*/);
        //    RepositoryItemLookUpEdit repositoryItemLookUpEdit = new RepositoryItemLookUpEdit();
        //    //RepositoryItemComboBox repositoryItemComboBox = new RepositoryItemComboBox();
        //    if (repositoryItemLookUpEdit != null)
        //    {
        //        repositoryItemLookUpEdit.DataSource = dt;
        //        repositoryItemLookUpEdit.DisplayMember = "desc";
        //        repositoryItemLookUpEdit.ValueMember = "value";
        //        gridViewButtInfo.Columns["OPTYPE"].ColumnEdit = repositoryItemLookUpEdit;
        //    }

        //    this.fgDevGridFormInfo.EmbeddedNavigator.ButtonClick += new NavigatorButtonClickEventHandler(FormInfoEmbeddedNavigator_ButtonClick);
        //    this.fgDevGridButtInfo.EmbeddedNavigator.ButtonClick += new NavigatorButtonClickEventHandler(ButtInfoEmbeddedNavigator_ButtonClick);
        //    this.fgDevGridOth.EmbeddedNavigator.ButtonClick += new NavigatorButtonClickEventHandler(OthEmbeddedNavigator_ButtonClick);

        //    //获取当前系统中所有的APPNAME信息
        //    EI.EIInfo inBlockAPP = new EI.EIInfo();
        //    EI.EIInfo outBlockAPP = new EI.EIInfo(); 
        //    RepositoryItemComboBox repositoryItemComboBox = new RepositoryItemComboBox();

        //    inBlockAPP.SetColName(1, "ename");
        //    inBlockAPP.SetColVal(1, "ename", "");
        //    outBlockAPP = EI.EITuxedo.CallService("epesappinfo", inBlockAPP);
        //    if (outBlockAPP.sys_info.flag != 0)
        //    {
        //        MessageBox.Show(string.Format(EP.EPES.EPESC0000061/*获取APPNAME信息错误：{0}*/, outBlockAPP.sys_info.msg));
        //        return;
        //    }
        //    string stemp = "";

        //    for (int i = 0; i < outBlockAPP.blk_info[0].Row; i++)
        //    {
        //        stemp = outBlockAPP.GetColVal(1, i + 1, "ename") + ": " + outBlockAPP.GetColVal(1, i + 1, "cname");
        //        comboApp.Properties.Items.Add(stemp);
        //        repositoryItemComboBox.Items.Add(outBlockAPP.GetColVal(1, i + 1, "ename"));
        //    }

        //    gridViewFormInfo.Columns["APPNAME"].ColumnEdit = repositoryItemComboBox;
        //    repositoryItemComboBox.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
        //    if (outBlockAPP.blk_info[0].Row > 0)
        //    {
        //        comboApp.SelectedIndex = 0;
        //    }

        //    //设置checkbox选中行样式
        //    DevExpress.Skins.Skin currentSkin;
        //    currentSkin = DevExpress.Skins.CommonSkins.GetSkin(DevExpress.LookAndFeel.UserLookAndFeel.Default);
        //    Color textColor = currentSkin.Colors.GetColor(DevExpress.Skins.CommonColors.WindowText);
        //    Color highlightTextColor = currentSkin.Colors.GetColor(DevExpress.Skins.CommonColors.HighligthText);
        //    Color selectColor = currentSkin.Colors.GetColor(DevExpress.Skins.CommonColors.Highlight);

        //    StyleFormatCondition cnForm;
        //    cnForm = new StyleFormatCondition(FormatConditionEnum.Equal, fgDevGridFormInfo.SelectionColumn, null, false);
        //    cnForm.ApplyToRow = true;
        //    cnForm.Appearance.BackColor = Color.Empty;
        //    cnForm.Appearance.ForeColor = textColor;
        //    gridViewFormInfo.FormatConditions.Add(cnForm);

        //    cnForm = new StyleFormatCondition(FormatConditionEnum.Equal, fgDevGridFormInfo.SelectionColumn, null, true);
        //    cnForm.ApplyToRow = true;
        //    cnForm.Appearance.BackColor = selectColor;
        //    cnForm.Appearance.ForeColor = highlightTextColor;
        //    gridViewFormInfo.FormatConditions.Add(cnForm);

        //    StyleFormatCondition cnBtn;
        //    cnBtn = new StyleFormatCondition(FormatConditionEnum.Equal, fgDevGridButtInfo.SelectionColumn, null, false);
        //    cnBtn.ApplyToRow = true;
        //    cnBtn.Appearance.BackColor = Color.Empty;
        //    cnBtn.Appearance.ForeColor = textColor;
        //    gridViewButtInfo.FormatConditions.Add(cnBtn);

        //    cnBtn = new StyleFormatCondition(FormatConditionEnum.Equal, fgDevGridButtInfo.SelectionColumn, null, true);
        //    cnBtn.ApplyToRow = true;
        //    cnBtn.Appearance.BackColor = selectColor;
        //    cnBtn.Appearance.ForeColor = highlightTextColor;
        //    gridViewButtInfo.FormatConditions.Add(cnBtn);

        //    StyleFormatCondition cnOth;
        //    cnOth = new StyleFormatCondition(FormatConditionEnum.Equal, fgDevGridOth.SelectionColumn, null, false);
        //    cnOth.ApplyToRow = true;
        //    cnOth.Appearance.BackColor = Color.Empty;
        //    cnOth.Appearance.ForeColor = textColor;
        //    gridViewOthInfo.FormatConditions.Add(cnOth);

        //    cnOth = new StyleFormatCondition(FormatConditionEnum.Equal, fgDevGridOth.SelectionColumn, null, true);
        //    cnOth.ApplyToRow = true;
        //    cnOth.Appearance.BackColor = selectColor;
        //    cnOth.Appearance.ForeColor = highlightTextColor;
        //    gridViewOthInfo.FormatConditions.Add(cnOth);

        //    fgDevGridButtInfo.SetAllColumnEditable(false);
        //    fgDevGridFormInfo.SetAllColumnEditable(false);
        //    fgDevGridOth.SetAllColumnEditable(false);

        //    fgDevGridFormInfo.SelectionColumn.OptionsColumn.AllowEdit = true;
        //    fgDevGridButtInfo.SelectionColumn.OptionsColumn.AllowEdit = true;
        //    fgDevGridOth.SelectionColumn.OptionsColumn.AllowEdit = true;

        //}

        private void InitDevGridCustomButtons()
        {
            ((ImageList)this.fgDevGridFormInfo.EmbeddedNavigator.Buttons.ImageList).Images.Add(this.imageList.Images[0]);
            ((ImageList)this.fgDevGridFormInfo.EmbeddedNavigator.Buttons.ImageList).Images.Add(this.imageList.Images[1]);

            this.fgDevGridFormInfo.EmbeddedNavigator.Buttons.CustomButtons.AddRange(new NavigatorCustomButton[] {
            new NavigatorCustomButton(SAVE, "保存"),
            new NavigatorCustomButton(DISCARD, "放弃")            
            });

            ((ImageList)this.fgDevGridButtInfo.EmbeddedNavigator.Buttons.ImageList).Images.Add(this.imageList.Images[0]);
            ((ImageList)this.fgDevGridButtInfo.EmbeddedNavigator.Buttons.ImageList).Images.Add(this.imageList.Images[1]);

            this.fgDevGridButtInfo.EmbeddedNavigator.Buttons.CustomButtons.AddRange(new NavigatorCustomButton[] {
            new NavigatorCustomButton(SAVE, "保存"),
            new NavigatorCustomButton(DISCARD, "放弃")
            });

            ((ImageList)this.fgDevGridOth.EmbeddedNavigator.Buttons.ImageList).Images.Add(this.imageList.Images[0]);
            ((ImageList)this.fgDevGridOth.EmbeddedNavigator.Buttons.ImageList).Images.Add(this.imageList.Images[1]);

            this.fgDevGridOth.EmbeddedNavigator.Buttons.CustomButtons.AddRange(new NavigatorCustomButton[] {
                   new NavigatorCustomButton(SAVE, "保存"),
                   new NavigatorCustomButton(DISCARD, "放弃")              
                   });

            fgDevGridOth.EmbeddedNavigator.Buttons.CustomButtons[DISCARD].Visible = false;
            fgDevGridOth.EmbeddedNavigator.Buttons.CustomButtons[SAVE].Visible = false;
            fgDevGridFormInfo.EmbeddedNavigator.Buttons.CustomButtons[DISCARD].Visible = false;
            fgDevGridFormInfo.EmbeddedNavigator.Buttons.CustomButtons[SAVE].Visible = false;
            fgDevGridButtInfo.EmbeddedNavigator.Buttons.CustomButtons[DISCARD].Visible = false;
            fgDevGridButtInfo.EmbeddedNavigator.Buttons.CustomButtons[SAVE].Visible = false;
        }

        private void InitOthType(EI.EIInfo eiInfo)
        {
            if (eiInfo == null)
            {
                MessageBox.Show("获取资源类型失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            fgDevLookUpEditType.Properties.DataSource = eiInfo.Tables[0];// othResType.Tables["OthType"];
            fgDevLookUpEditType.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CODE"));
            fgDevLookUpEditType.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CODE_DESC_1_CONTENT"));
            fgDevLookUpEditType.Properties.ValueMember = "CODE";
            fgDevLookUpEditType.Properties.DisplayMember = "CODE_DESC_1_CONTENT";

            fgDevLookUpEditType.EditValue = eiInfo.Tables[0].Rows[0]["CODE"];

            repositoryItemLookUpEditType.Properties.DataSource = eiInfo.Tables[0];// othResType.Tables["OthType"];
            repositoryItemLookUpEditType.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CODE"));
            repositoryItemLookUpEditType.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CODE_DESC_1_CONTENT"));
            repositoryItemLookUpEditType.Properties.DisplayMember = "CODE_DESC_1_CONTENT";
            repositoryItemLookUpEditType.Properties.ValueMember = "CODE";
        }

        private EI.EIInfo GetResType()
        {
            EI.EIInfo inBlock = new EI.EIInfo();
            EI.EIInfo outBlock;

            inBlock.SetColName(1, "code_class");
            inBlock.SetColVal(1, 1, "code_class", "ES03");

            outBlock = EI.EITuxedo.CallService("epep01_inq3", inBlock);
            if (outBlock.GetSys().flag != 0 || outBlock.Tables[0].Rows.Count == 0)
            {
                return null;
            }
            else
            {
                return outBlock;
            }
        }
        #endregion

        #region 进入/退出维护模式

        private void FormESOBJ_EF_PRE_DO_F3(object sender, EventArgs e)
        {
            fgDevGridFormInfo.ShowAddRowButton = true;
            fgDevGridFormInfo.ShowAddCopyRowButton = true;
            fgDevGridFormInfo.ShowDeleteRowButton = true;
            fgDevGridFormInfo.SetAllColumnEditable(true);

            fgDevGridButtInfo.ShowAddRowButton = true;
            fgDevGridButtInfo.ShowAddCopyRowButton = true;
            fgDevGridButtInfo.ShowDeleteRowButton = true;
            fgDevGridButtInfo.SetAllColumnEditable(true);
            colFNAME.OptionsColumn.AllowEdit = false;
            colAPPNAME1.OptionsColumn.AllowEdit = false;

            fgDevGridOth.ShowAddRowButton = true;
            fgDevGridOth.ShowAddCopyRowButton = true;
            fgDevGridOth.ShowDeleteRowButton = true;
            fgDevGridOth.SetAllColumnEditable(true);

            fgDevGridOth.EmbeddedNavigator.Buttons.CustomButtons[DISCARD].Visible = true;
            fgDevGridOth.EmbeddedNavigator.Buttons.CustomButtons[SAVE].Visible = true;
            fgDevGridFormInfo.EmbeddedNavigator.Buttons.CustomButtons[DISCARD].Visible = true;
            fgDevGridFormInfo.EmbeddedNavigator.Buttons.CustomButtons[SAVE].Visible = true;
            fgDevGridButtInfo.EmbeddedNavigator.Buttons.CustomButtons[DISCARD].Visible = true;
            fgDevGridButtInfo.EmbeddedNavigator.Buttons.CustomButtons[SAVE].Visible = true;

            fgDevGridFormInfo.ShowContextMenu = true;
            fgDevGridButtInfo.ShowContextMenu = true;
            fgDevGridOth.ShowContextMenu = true;

            gridViewFormInfo_FocusedRowChanged_1(null, null);
            gridViewOthInfo_FocusedRowChanged(null, null);
        }

        private void FormESOBJ_EF_DO_F3(object sender, EventArgs e)
        {
            bool succsee = true;

            if (isChange(dataSetESOBJ.TESFORMRESINFO))
            {
                if (formCheck())
                {
                    if (SaveFormInfo())
                    {
                        QueryForm();
                    }
                    else
                    {
                        succsee = false;
                    }
                }
                else
                {
                    succsee = false;
                }
            }

            if (isChange(dataSetESOBJ.TESBUTTONRESINFO))
            {
                if (btnCheck())
                {
                    if (SaveButtInfo())
                    {
                        QueryButton();
                    }
                    else
                    {
                        succsee = false;
                    }
                }
                else
                {
                    succsee = false;
                }
            }

            if (isChange(dataSetESOBJ.TESOTHERRESINFO))
            {
                if (othCheck())
                {
                    if (SaveOth())
                    {
                        queryOth();
                    }
                    else
                    {
                        succsee = false;
                    }
                }
                else
                {
                    succsee = false;
                }
            }

            if (succsee)
            {
                fgDevGridFormInfo.ShowAddRowButton = false;
                fgDevGridFormInfo.ShowAddCopyRowButton = false;
                fgDevGridFormInfo.ShowDeleteRowButton = false;
                fgDevGridFormInfo.SetAllColumnEditAble(false);

                fgDevGridButtInfo.ShowAddRowButton = false;
                fgDevGridButtInfo.ShowAddCopyRowButton = false;
                fgDevGridButtInfo.ShowDeleteRowButton = false;
                fgDevGridButtInfo.SetAllColumnEditAble(false);

                fgDevGridOth.ShowAddRowButton = false;
                fgDevGridOth.ShowAddCopyRowButton = false;
                fgDevGridOth.ShowDeleteRowButton = false;
                fgDevGridOth.SetAllColumnEditAble(false);

                fgDevGridOth.EmbeddedNavigator.Buttons.CustomButtons[DISCARD].Visible = false;
                fgDevGridOth.EmbeddedNavigator.Buttons.CustomButtons[SAVE].Visible = false;
                fgDevGridFormInfo.EmbeddedNavigator.Buttons.CustomButtons[DISCARD].Visible = false;
                fgDevGridFormInfo.EmbeddedNavigator.Buttons.CustomButtons[SAVE].Visible = false;
                fgDevGridButtInfo.EmbeddedNavigator.Buttons.CustomButtons[DISCARD].Visible = false;
                fgDevGridButtInfo.EmbeddedNavigator.Buttons.CustomButtons[SAVE].Visible = false;

                fgDevGridFormInfo.SelectionColumn.OptionsColumn.AllowEdit = true;
                fgDevGridButtInfo.SelectionColumn.OptionsColumn.AllowEdit = true;
                fgDevGridOth.SelectionColumn.OptionsColumn.AllowEdit = true;
                //colselected.OptionsColumn.AllowEdit = true;
                //colselected1.OptionsColumn.AllowEdit = true;
                //gridColumn1.OptionsColumn.AllowEdit = true;
                fgDevGridFormInfo.ShowContextMenu = false;
                fgDevGridButtInfo.ShowContextMenu = false;
                fgDevGridOth.ShowContextMenu = false;
            }
            else
            {
                //this.//ef_args.buttonStatusHold = true;
            }
        }

        private void FormESOBJ_EF_CANCEL_DO_F3(object sender, EventArgs e)
        {
            if (MessageBox.Show(EP.EPES.EPESC0000160/*是否退出维护模式？*/, "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                fgDevGridFormInfo.ShowAddRowButton = false;
                fgDevGridFormInfo.ShowAddCopyRowButton = false;
                fgDevGridFormInfo.ShowDeleteRowButton = false;
                fgDevGridFormInfo.SetAllColumnEditable(false);

                fgDevGridButtInfo.ShowAddRowButton = false;
                fgDevGridButtInfo.ShowAddCopyRowButton = false;
                fgDevGridButtInfo.ShowDeleteRowButton = false;
                fgDevGridButtInfo.SetAllColumnEditable(false);

                fgDevGridOth.ShowAddRowButton = false;
                fgDevGridOth.ShowAddCopyRowButton = false;
                fgDevGridOth.ShowDeleteRowButton = false;
                fgDevGridOth.SetAllColumnEditable(false);

                fgDevGridOth.EmbeddedNavigator.Buttons.CustomButtons[DISCARD].Visible = false;
                fgDevGridOth.EmbeddedNavigator.Buttons.CustomButtons[SAVE].Visible = false;
                fgDevGridFormInfo.EmbeddedNavigator.Buttons.CustomButtons[DISCARD].Visible = false;
                fgDevGridFormInfo.EmbeddedNavigator.Buttons.CustomButtons[SAVE].Visible = false;
                fgDevGridButtInfo.EmbeddedNavigator.Buttons.CustomButtons[DISCARD].Visible = false;
                fgDevGridButtInfo.EmbeddedNavigator.Buttons.CustomButtons[SAVE].Visible = false;

                //colselected.OptionsColumn.AllowEdit = true;
                fgDevGridFormInfo.SelectionColumn.OptionsColumn.AllowEdit = true;
                fgDevGridButtInfo.SelectionColumn.OptionsColumn.AllowEdit = true;
                fgDevGridOth.SelectionColumn.OptionsColumn.AllowEdit = true;
                //colselected1.OptionsColumn.AllowEdit = true;
                //gridColumn1.OptionsColumn.AllowEdit = true;

                fgDevGridFormInfo.ShowContextMenu = false;
                fgDevGridButtInfo.ShowContextMenu = false;
                fgDevGridOth.ShowContextMenu = false;

                if (isChange(dataSetESOBJ.TESFORMRESINFO))
                {
                    QueryForm();
                }

                if (isChange(dataSetESOBJ.TESBUTTONRESINFO))
                {
                    QueryButton();
                }

                if (isChange(dataSetESOBJ.TESOTHERRESINFO))
                {
                    queryOth();
                }
            }
            else
            {
                //this.//ef_args.buttonStatusHold = true;
            }
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

        #endregion

        #region 按钮辅助
        /// <summary>
        /// 在示例画面上"铺"上一层新的按钮和标签
        /// </summary>
        /// <param name="obj">画面实例</param>
        private void SetControls(Object obj)
        {
            x = 0;
            y = 0;
            buttons.Clear();

            GetButtonInfo((Control)obj);

            //2010-12-2 wengfei 添加工具栏四个按钮

          
                 DevExpress.XtraEditors.SimpleButton buttNew = new  DevExpress.XtraEditors.SimpleButton();
                buttNew.Name = "ToolbarNew";
                buttNew.Text = EP.EPES.EPESC0000063/*新增*/;
                buttNew.Location = new Point(12, 655);
                buttNew.Size = new System.Drawing.Size(70, 25);
                buttons.Add(buttNew);

                (( DevExpress.XtraEditors.SimpleButton)buttNew).Click += new EventHandler(TestClick);

                 DevExpress.XtraEditors.SimpleButton buttAddcopy = new  DevExpress.XtraEditors.SimpleButton();
                buttAddcopy.Name = "ToolbarAddCopy";
                buttAddcopy.Text = EP.EPES.EPESC0000064/*复制新增*/;
                buttAddcopy.Location = new Point(100, 655);
                buttAddcopy.Size = new System.Drawing.Size(70, 25);
                buttons.Add(buttAddcopy);

                (( DevExpress.XtraEditors.SimpleButton)buttAddcopy).Click += new EventHandler(TestClick);

                 DevExpress.XtraEditors.SimpleButton buttDelete = new  DevExpress.XtraEditors.SimpleButton();
                buttDelete.Name = "ToolbarDelete";
                buttDelete.Text = EP.EPES.EPESC0000065/*删除*/;
                buttDelete.Location = new Point(192, 655);
                buttDelete.Size = new System.Drawing.Size(70, 25);
                buttons.Add(buttDelete);

                (( DevExpress.XtraEditors.SimpleButton)buttDelete).Click += new EventHandler(TestClick);

                 DevExpress.XtraEditors.SimpleButton buttSave = new  DevExpress.XtraEditors.SimpleButton();
                buttSave.Name = "ToolbarSaveAll";
                buttSave.Text = EP.EPES.EPESC0000066/*存盘*/;
                buttSave.Location = new Point(282, 655);
                buttSave.Size = new System.Drawing.Size(70, 25);
                buttons.Add(buttSave);

                (( DevExpress.XtraEditors.SimpleButton)buttSave).Click += new EventHandler(TestClick);
            
            //取出并添加按钮和标签
            foreach ( DevExpress.XtraEditors.SimpleButton btn in buttons)
            {
                 DevExpress.XtraEditors.LabelControl label = new  DevExpress.XtraEditors.LabelControl();
                label.Text = btn.Name;
                label.AutoSize = true;
                label.BackColor = Color.Yellow;
                label.Location = new Point(btn.Location.X, btn.Location.Y - 15);
                ((Control)obj).Controls.Add(label);
                label.BringToFront();

                ((Control)obj).Controls.Add(btn);
                btn.BringToFront();
            }
        }

        /// <summary>
        /// 遍历返回授权按钮信息
        /// </summary>
        /// <param name="obj">画面实例或控件</param>
        private void GetButtonInfo(Control obj)
        {
            foreach (Control ctrl in obj.Controls)
            {
                x += ctrl.Location.X;
                y += ctrl.Location.Y;

                //标题栏和ButtonBar不作处理
                if (ctrl.Name == "title" || ctrl.Name == "Fn")
                {
                    continue;
                }
                //递归查找子控件
                else if (ctrl.HasChildren)
                {
                    GetButtonInfo(ctrl);
                }

                if (ctrl is  DevExpress.XtraEditors.SimpleButton)// && (( DevExpress.XtraEditors.SimpleButton)ctrl).Authorizable == true)
                {
                     DevExpress.XtraEditors.SimpleButton butt = new  DevExpress.XtraEditors.SimpleButton();
                    butt.Name = ctrl.Name;
                    butt.Text = ctrl.Text;
                    butt.Image = (( DevExpress.XtraEditors.SimpleButton)ctrl).Image;
                    butt.Location = new Point(x, y);
                    butt.Size = ctrl.Size;
                    buttons.Add(butt);

                    ctrl.Hide();
                    (( DevExpress.XtraEditors.SimpleButton)butt).Click += new EventHandler(TestClick);
                }
                //其他所有控件全部不可用
                else
                {
                    ctrl.Enabled = false;
                }
                x -= ctrl.Location.X;
                y -= ctrl.Location.Y;
            }
        }

        /// <summary>
        /// 点击返回按钮名和描述到ESOBJ
        /// </summary>
        private void TestClick(Object sender, EventArgs e)
        {
            dataSetESOBJ.TESBUTTONRESINFO.Rows[gridViewButtInfo.FocusedRowHandle]["NAME"] = (( DevExpress.XtraEditors.SimpleButton)sender).Name;
            dataSetESOBJ.TESBUTTONRESINFO.Rows[gridViewButtInfo.FocusedRowHandle]["DESCRIPTION"] = (( DevExpress.XtraEditors.SimpleButton)sender).Text;
            (( DevExpress.XtraEditors.SimpleButton)sender).Parent.FindForm().Close();
        }

        private void repositoryItemButtonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            Assembly assembForm;
            Type type;
            Object obj = null;
            MethodInfo EFShow;
            object[] objects = new object[1];
             EventArgs ef_args = new  EventArgs();
            string dllName = "";
            string dllPath = "";
            string subSystem = "";
            string subPath = "";
            string CallMode = "";
            string fullName = "";
            string formENameBase = "";
            string startPath = "" + "\\..\\";

            //this.//EFMsgInfo = "";

            if (this.formName != string.Empty)
            {
                subSystem = this.formName.Substring(0, 2);
            }
            else
            {
                //this.//EFMsgInfo = "FormName is empty";
                return;
            }

            if ((subSystem.Substring(0, 1) == "E") && subSystem != "EM")
            {
                subPath = "EP";
            }
            else
            {
                subPath = subSystem;
            }

            //获取dllname
            EI.EIInfo inblk = new EI.EIInfo();
            EI.EIInfo outblk = new EI.EIInfo();
            inblk.SetColName(1, "name");
            inblk.SetColName(3, "mode");
            inblk.SetColName(4, "appname");
            inblk.SetColVal(1, 1, 1, this.formName);
            inblk.SetColVal(1, 1, 3, 0);
            inblk.SetColVal(1, 1, "appname", comboApp.EditValue.ToString().Split(':')[0]);
            outblk = EI.EITuxedo.CallService("epesformactual", inblk);
            if (outblk.sys_info.flag < 0 /*|| outblk.GetColVal(1, "dllname") == string.Empty*/)
            {
                //this.//EFMsgInfo = string.Format(EP.EPES.EPESC0000045/*获取画面{0}的dllname失败*/, formName);
                return;
            }
            else
            {
                dllName = outblk.GetColVal(1, "dllname");
                CallMode = outblk.GetColVal(1, "form_call_mode");
            }

            dllPath = startPath + subPath + "\\" + dllName;

            if (CallMode == "1" || CallMode == "9")
            {
                formENameBase = outblk[1][0, "form_base_name"];
                fullName = subSystem + ".Form" + formENameBase;
            }
            else
            {
                fullName = subSystem + ".Form" + formName;
            }

            //生成画面实例
            try
            {
                assembForm = Assembly.LoadFrom(dllPath);
                type = assembForm.GetType(fullName);
                obj = Activator.CreateInstance(type);
            }
            catch
            {
                //this.//EFMsgInfo = string.Format(EP.EPES.EPESC0000046/*{0}载入失败*/, dllPath);
                if (obj != null)
                    ((DevExpress.XtraEditors.XtraForm)obj).Close();
                return;
            }
            //为授权按钮加标签
            this.SetControls(obj);

            ((System.Windows.Forms.Form)obj).Text = EP.EPES.EPESC0000123/*按钮展示*/;
            EFShow = type.GetMethod("EFShow");

            //ef_args.formEName = "BUTTONTIPS";
            //ef_args.eventId = "START_FORM_BY_EF";
            //ef_args.formDllName = dllName;
            //ef_args.formDllPath = dllPath;
            //ef_args.formCName = EP.EPES.EPESC0000124/*按钮提示*/;
            objects[0] = ef_args;

            EFShow.Invoke(obj, objects);
        }

        private void repstryItemBtnComboBoxOptype_CustomDisplayText(object sender, DevExpress.XtraEditors.Controls.CustomDisplayTextEventArgs e)
        {
            if (e.Value.ToString() == "A")
            {
                e.DisplayText = EP.EPES.EPESC0000058/*单步操作*/;
            }
            else if (e.Value.ToString() == "B")
            {
                e.DisplayText = EP.EPES.EPESC0000032/*多步操作*/;
            }
        }
        #endregion

        #region 菜单树

        #region 查询
        //加载菜单树
        private void LoadTree()
        {
            DataSet inblk = new DataSet();
            inblk.Tables.Add();
            inblk.Tables[0].Columns.Add("USERID", typeof(string));
            inblk.Tables[0].Columns.Add("COMPANYCODE", typeof(string));
            inblk.Tables[0].Columns.Add("ROOT", typeof(string));
            inblk.Tables[0].Columns.Add("FAVORITE", typeof(string));
            inblk.Tables[0].Columns.Add("APPNAME", typeof(string));

            DataRow row = inblk.Tables[0].NewRow();
            row["USERID"] = "admin";
            row["ROOT"] =  "root";
            row["FAVORITE"] = "MYFAVORITE";
            row["APPNAME"] = "CMRT";
            inblk.Tables[0].Rows.Add(row);
       
            

            DataSet outBlock = CServerManager.Instance.CallService("epestree_inqa", inblk);
            this.dtMenuTree = outBlock.Tables["MENUTREE"];

            InitMenu();

            //作为root的子节点
            //for (int i = 1; i <= outBlock.blk_info[0].Row; i++)
            //{
            //    string name = outBlock.GetColVal(i, "name");
            //    string resname = outBlock.GetColVal(i, "resname");
            //    string description = outBlock.GetColVal(i, "description");

            //    //不显示“收藏夹”目录
            //    if (name != "MYFAVORITE")
            //    {
            //        TreeListNode node = this.treeList.AppendNode(new object[] { description + "(" + name + ")" }, treeNode);
            //        node.Tag = name;
            //        if (resname == "FOLDER")
            //        {
            //            node.ImageIndex = node.SelectImageIndex = FOLDERICON;
            //        }
            //        else
            //        {
            //            node.SelectImageIndex = node.ImageIndex = FORMICON;
            //        }
            //    }                
            //}
            //this.treeList.ExpandAll();
        }

        private void InitMenu()
        {
            root_hash.Clear();
            
            treeList.BeginUnboundLoad();
            treeList.Nodes.Clear();

            //TreeListNode treeNode = this.treeList.AppendNode(new object[] { EP.EPES.EPESC0000067/*主菜单*/ }, null);
            //treeNode.ImageIndex = treeNode.SelectImageIndex = FOLDERICON;

            ////2011-7-22 yuxiuwen 修改 加入webroot
            //string treeRoot = fgDevCheckEdit2.Checked ? "webroot" : "root";
            //treeNode.Tag = treeRoot;
            //EI.EIInfo outBlock = this.CallSelectService(" ", treeRoot, 0, 2, comboApp.EditValue.ToString().Split(':')[0]);//查询出父名为root的记录


            try
            {                
                for (int i = 0; i < this.dtMenuTree.Rows.Count; i++)
                {
                    AddMenu(dtMenuTree.Rows[i]);
                }

            }
            catch (Exception ex)
            {
                 MessageBox.Show(this, ex.Message);
            }

            
            treeList.EndUnboundLoad();
        }

        private void AddMenu(DataRow dtRow)
        {
            if (dtRow == null) return;

            string name = dtRow["NAME"].ToString();
            string fname = dtRow["FNAME"].ToString();
            string resname = dtRow["RESNAME"].ToString();
            string description = dtRow["DESCRIPTION"].ToString();

            if (name == "func_menu" || name == "chart_menu" || name == "MYFAVORITE")
            {
                return;
            }
            else if (fname == "root" || fname == "webroot")
            {
                AddRootMenu(dtRow);
            }
            else if(root_hash.Contains(fname))
            {
                AddSingleMenu(dtRow);
            }
        }

        //生成一级目录(page)
        private void AddRootMenu(DataRow dtRow)
        {
            string name = dtRow["NAME"].ToString();
            string fname = dtRow["FNAME"].ToString();
            string description = dtRow["DESCRIPTION"].ToString();

            TreeListNode treeListNode = this.treeList.AppendNode(new object[] { string.Format("{0}[{1}]", description, name)}, null);
            treeListNode.SelectImageIndex = treeListNode.ImageIndex = FOLDERICON;
            treeListNode.Tag = name;

            root_hash.Add(name, treeListNode);
        }

        //生成二级及以上目录
        private void AddSingleMenu(DataRow dtRow)
        {
            string name = dtRow["NAME"].ToString();
            string fname = dtRow["FNAME"].ToString();
            string resname = dtRow["RESNAME"].ToString();
            string description = dtRow["DESCRIPTION"].ToString();

            try
            {
                if (resname == "FOLDER")
                {
                    TreeListNode treeListNode = this.treeList.AppendNode(new object[] { string.Format("{0}[{1}]", description, name) }, (TreeListNode)root_hash[fname]);
                    treeListNode.Tag = name;
                    treeListNode.SelectImageIndex = treeListNode.ImageIndex = FOLDERICON;                   

                    root_hash.Add(name, treeListNode);
                }
                else
                {
                    TreeListNode treeListNode = this.treeList.AppendNode(new object[] { string.Format("{0}[{1}]", description, name) }, (TreeListNode)root_hash[fname]);
                    treeListNode.Tag = name;
                    treeListNode.SelectImageIndex = treeListNode.ImageIndex = FORMICON;
                }
            }
            catch (Exception e)
            {
                string errMsg = string.Format("NODE.NAME = [{0}], NODE.RESNAME = [{1}], NODE.FNAME = [{2}]", name, resname, fname);
                EI.Logger.Error(errMsg);
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

        //点击节点展开子节点
        private void treeList_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            if (e.Node == null
                ||e.Node.Tag == null
                || e.Node.Nodes.Count > 0
                || e.Node.ImageIndex == FORMICON) return;

            queryChildNodes(e.Node);
        }

        //查询子节点
        private void queryChildNodes(TreeListNode parentNode)
        {
            parentNode.Nodes.Clear();
            EI.EIInfo outBlock = this.CallSelectService(" ", parentNode.Tag.ToString(), 0, 2, comboApp.EditValue.ToString().Split(':')[0]);
            for (int i = 1; i <= outBlock.blk_info[0].Row; i++)
            {
                string name = outBlock.GetColVal(i, "name");
                string resname = outBlock.GetColVal(i, "resname");
                string description = outBlock.GetColVal(i, "description");

                //不显示“收藏夹”目录
                if (name != "MYFAVORITE")
                {
                    if (resname == "FOLDER")
                    {
                        TreeListNode node = this.treeList.AppendNode(new object[] { description + "(" + name + ")" }, parentNode);
                        node.Tag = name;
                        node.ImageIndex = node.SelectImageIndex = FOLDERICON;
                    }
                    else
                    {
                        TreeListNode node = this.treeList.AppendNode(new object[] { description + "(" + resname + ")" }, parentNode);
                        node.Tag = name;
                        node.SelectImageIndex = node.ImageIndex = FORMICON;
                    }
                }
            }
        }

        #endregion

        #region 刷新
        private void popupTreeRfgresh_ItemClick(object sender, ItemClickEventArgs e)
        {
            treeList.ClearNodes();

            LoadTree();
        }

        #endregion

        #region 右键菜单

        //弹出右键菜单
        private void treeList_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                DevExpress.XtraTreeList.TreeListHitInfo hi = treeList.CalcHitInfo(e.Location);
                if (hi != null)
                {
                    treeList.FocusedNode = hi.Node;

                    //if (ef_args.b_info[2].Status == EFButtonKeysStatus.PRE_ACTIVE)
                    //{
                    //    this.popupMenuTree.ShowPopup(MousePosition);
                    //}
                }
            }
        }

        //右键菜单管理
        private void popupMenuTree_BeforePopup(object sender, CancelEventArgs e)
        {
            //空白处
            if (treeList.FocusedNode == null)
            {
                popupTreeAdd.Enabled = true;
                popupTreeDelete.Enabled = false;
                popupTreeRfgresh.Visibility = BarItemVisibility.Always;
                popupTreeRfgresh.Enabled = true;
                popupTreeModify.Visibility = BarItemVisibility.Never;
            }
            //根节点
            else if (treeList.Selection[0].ParentNode == null)
            {
                popupTreeAdd.Enabled = true;
                popupTreeDelete.Enabled = true;
                popupTreeRfgresh.Visibility = BarItemVisibility.Always;
                popupTreeRfgresh.Enabled = true;
                popupTreeModify.Visibility = BarItemVisibility.Always;
            }
            //非根节点
            else
            {
                popupTreeRfgresh.Visibility = BarItemVisibility.Never;

                //目录节点
                if (treeList.Selection[0].ImageIndex == FOLDERICON || treeList.Selection[0].ImageIndex == FOLDERICON_EXPAND)
                {
                    popupTreeAdd.Enabled = true;
                    popupTreeDelete.Enabled = true;
                    popupTreeModify.Visibility = BarItemVisibility.Always;
                }
                //画面节点
                else if (treeList.Selection[0].ImageIndex == FORMICON)
                {
                    popupTreeAdd.Enabled = false;
                    popupTreeDelete.Enabled = true;
                    popupTreeModify.Visibility = BarItemVisibility.Never;
                }
            }            
        }

        #endregion

        #region 新增目录

        //新增目录
        private void popupTreeAdd_ItemClick(object sender, ItemClickEventArgs e)
        {
            try { }
            //{
            //    //this.//EFMsgInfo = EP.EPES.EPESC0000068/*新增目录*/;
            //    ES.FormESOBJ_TREEINS FormIns = new ES.FormESOBJ_TREEINS();
            //    FormIns.Icon = this.Icon;
            //    if (this.treeList.FocusedNode == null)
            //    {
            //        FormIns.Fname = fgDevCheckEdit2.Checked ? "webroot" : "root";
            //        FormIns.Treeseq = this.treeList.Nodes.Count;
            //    }
            //    else
            //    {
            //        FormIns.Fname = this.treeList.FocusedNode.Tag.ToString();//传递参数，将选择的节点作为新增的目录的父节点
            //        FormIns.Treeseq = this.treeList.FocusedNode.Nodes.Count;//传递参数，父节点的子节点数作为新增目录的顺序号
            //    }
            //    FormIns.Cursystem = comboApp.EditValue.ToString().Split(':')[0];//传递参数代表当前系统

            //    FormIns.Text = EP.EPES.EPESC0000068/*新增目录*/;
            //    FormIns.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;

            //    DialogResult result = FormIns.ShowDialog();

            //    if (result == DialogResult.OK)
            //    {
            //        //this.//EFMsgInfo = EP.EPES.EPESC0000069/*新增成功*/;

            //        //新增成功后更新树
            //        try
            //        {
            //            if (treeList.FocusedNode != null)
            //            {
            //                queryChildNodes(this.treeList.FocusedNode);

            //                this.treeList.FocusedNode.ExpandAll();
            //            }
            //            else
            //            {
            //                LoadTree();
            //            }
            //        }
            //        catch (Exception err)
            //        {
            //            //this.//EFMsgInfo = err.ToString();
            //        }
            //    }
            //    else if (result == DialogResult.Cancel)
            //    {
            //        //this.//EFMsgInfo = EP.EPES.EPESC0000070/*取消新增*/;
            //    }
            //}
            catch (Exception err)
            {
                //this.//EFMsgInfo = err.ToString();
            }
        }

        #endregion

        #region 新增画面与节点排序

        private DragDropEffects GetDragDropEffect(TreeList tl, TreeListNode dragNode, ref bool updown)
        {
            if (dragFromGrid)
            {
                return DragDropEffects.Copy;
            }

            TreeListNode targetNode;
            Point p = tl.PointToClient(MousePosition);
            targetNode = tl.CalcHitInfo(p).Node;

            if (dragNode != null && targetNode != null
                && dragNode != targetNode
                && dragNode.ParentNode == targetNode.ParentNode)
            {
                //下移
                if (tl.GetNodeIndex(dragNode) < tl.GetNodeIndex(targetNode))
                {
                    updown = true;
                }
                //上移
                else
                {
                    updown = false;
                }
                return DragDropEffects.Move;
            }
            else
                return DragDropEffects.None;
        }

        //设置拖拽节点时显示的图标
        private void treeList_CalcNodeDragImageIndex(object sender, DevExpress.XtraTreeList.CalcNodeDragImageIndexEventArgs e)
        {
            TreeList tl = sender as TreeList;
            bool updown = false;
            if (GetDragDropEffect(tl, tl.FocusedNode, ref updown) != DragDropEffects.Copy)
            {
                if (GetDragDropEffect(tl, tl.FocusedNode, ref updown) == DragDropEffects.None)
                    e.ImageIndex = -1;  //无图标
                else
                {
                    if (updown)
                    {
                        e.ImageIndex = 2; //向下的箭头
                    }
                    else
                    {
                        e.ImageIndex = 1;  // 向上的箭头
                    }
                }
            }           
        }

        private void treeList_DragEnter(object sender, DragEventArgs e)
        {
            //if (ef_args.b_info[2].Status == EFButtonKeysStatus.PRE_ACTIVE)
            //    e.Effect = DragDropEffects.Copy;
            //else
            //{
            //    e.Effect = DragDropEffects.None;
            //    // MessageBox.Show(EP.EPES.EPESC0000071/*操作失败！请进入维护模式进行排序操作！*/, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        private void treeList_DragDrop(object sender, DragEventArgs e)
        {
            if (true)//(ef_args.b_info[2].Status != EFButtonKeysStatus.PRE_ACTIVE)
            {
                e.Effect = DragDropEffects.None;

                 MessageBox.Show(EP.EPES.EPESC0000071/*操作失败！请进入维护模式进行排序操作！*/, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);

                TreeListNode parent = treeList.FocusedNode.ParentNode;

                queryChildNodes(parent);
                return;
            }

            //this.//EFMsgInfo = "";  

            DevExpress.XtraTreeList.TreeListHitInfo hi = treeList.CalcHitInfo(treeList.PointToClient(new Point(e.X, e.Y)));

            string[] format = e.Data.GetFormats();

            //拖拽的是菜单树中的节点——节点排序
            if (format[0] == "DevExpress.XtraTreeList.Nodes.TreeListNode")
            {
                if(hi!=null)
                {
                    if (hi.Node != null)
                    {
                        TreeListNode dragnode = e.Data.GetData(typeof(TreeListNode)) as TreeListNode;
                        TreeListNode targetnode = hi.Node;

                        if (//dragnode.ImageIndex == FORMICON && 
                            targetnode.ParentNode != null 
                            && dragnode.ParentNode == targetnode.ParentNode) //同级目录拖曳
                        {
                            treeList.SetNodeIndex(dragnode, treeList.GetNodeIndex(targetnode));
                            OrderNodes(hi.Node.ParentNode);
                        }
                        else if (dragnode.ImageIndex == FORMICON && targetnode.ParentNode != null 
                            && dragnode.ParentNode != targetnode.ParentNode) //非同级目录拖曳
                        {
                            if (InsertNodeToTree(dragnode, targetnode))
                            {
                                if (targetnode.ImageIndex == FORMICON)
                                {
                                    queryChildNodes(targetnode.ParentNode);
                                }
                                else
                                {
                                    queryChildNodes(targetnode);
                                }

                                RemoveNodeFromTree(dragnode);
                            }                            
                        }
                    }
                }
                e.Effect = DragDropEffects.None;
            }

            //拖拽的是画面信息列表框中的行——新增画面
            else
            {
                TreeListNode parentNode = null;
                int treeseq = 0;

                //拖拽至画面节点
                if (hi.Node.ImageIndex == FORMICON)
                {
                    parentNode = hi.Node.ParentNode;
                }
                //拖拽至目录节点
                else if (hi.Node.Level == 0)
                {
                    MessageBox.Show(EP.EPES.EPESC0000178/*不能将画面挂于根目录下，请先建立子目录*/, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    parentNode = hi.Node;
                }
                treeseq = parentNode.Nodes.Count;

                //新增画面
                EI.EIInfo inBlock = new EI.EIInfo();
                EI.EIInfo outBlock;

                inBlock.SetColName(1, 1, "fname");
                inBlock.SetColName(1, 2, "name");
                inBlock.SetColName(1, 3, "resname");
                inBlock.SetColName(1, 4, "description");
                inBlock.SetColName(1, 5, "shortcut");
                inBlock.SetColName(1, 6, "treeno");
                inBlock.SetColName(1, 7, "treeseq");
                inBlock.SetColName(1, 8, "userid");

                for (int i = 0, j = 1; i < this.gridViewFormInfo.RowCount; i++)
                {
                    //取出选中行
                    if (/*gridViewFormInfo.GetRowCellValue(i, fgDevGridFormInfo.SelectionColumn).ToString() == "True"*/fgDevGridFormInfo.GetSelectedColumnChecked(i))
                    {
                        if (treeseq > 999)
                        {
                            MessageBox.Show(EP.EPES.EPESC0000072/*新增失败*/, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        inBlock.SetColVal(1, j, "name", parentNode.Tag + dataSetESOBJ.TESFORMRESINFO.Rows[i]["NAME"].ToString());
                        inBlock.SetColVal(1, j, "fname", parentNode.Tag);
                        inBlock.SetColVal(1, j, "description", dataSetESOBJ.TESFORMRESINFO.Rows[i]["DESCRIPTION"].ToString());
                        inBlock.SetColVal(1, j, "shortcut", " ");
                        inBlock.SetColVal(1, j, "resname", dataSetESOBJ.TESFORMRESINFO.Rows[i]["NAME"].ToString());
                        inBlock.SetColVal(1, j, "treeno", "0");
                        inBlock.SetColVal(1, j, "treeseq", treeseq.ToString("d3"));

                        treeseq++;
                        j++;
                    }
                }

                inBlock.AddNewBlock();
                inBlock.SetColName(1, "NAME");
                inBlock.SetColVal(2, 1, 1, parentNode.Tag);
                inBlock.SetColName(2, "cursystem");
                inBlock.SetColVal(2, 1, 2, comboApp.EditValue.ToString().Split(':')[0]);

                outBlock = EI.EITuxedo.CallService("epestree_insb", inBlock);

                if (outBlock.sys_info.flag == 0)
                {
                    queryChildNodes(parentNode);

                    //取消列表框中所有checkbox选中状态
                    for (int k = 0; k < dataSetESOBJ.TESFORMRESINFO.Rows.Count; k++)
                    {
                        fgDevGridFormInfo.SetSelectedColumnChecked(k, false);
                        //gridViewFormInfo.SetRowCellValue(k, "selected", false);
                    }
                    this.gridViewFormInfo.Invalidate();
                }

                ShowReturnMsg(outBlock);
            }
        }

        private bool InsertNodeToTree(TreeListNode node, TreeListNode targetNode)
        {
            TreeListNode parentNode = (targetNode.ImageIndex == FORMICON) ? targetNode.ParentNode : targetNode;

            int treeseq = parentNode.Nodes.Count;
            if (treeseq == 999)
            {
                MessageBox.Show("节点数量超过系统上限！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            EI.EIInfo inBlock = new EI.EIInfo();
            EI.EIInfo outBlock;

            inBlock.SetColName(1, 1, "fname");
            inBlock.SetColName(1, 2, "name");
            inBlock.SetColName(1, 3, "resname");
            inBlock.SetColName(1, 4, "description");
            inBlock.SetColName(1, 5, "shortcut");
            inBlock.SetColName(1, 6, "treeno");
            inBlock.SetColName(1, 7, "treeseq");
            inBlock.SetColName(1, 8, "userid");

            //string nodeName = node.Tag.ToString();
            string nodeText = node.GetDisplayText(treeListColumn1);
            string nodeResName = nodeText.Substring(nodeText.IndexOf('(') + 1, nodeText.Length - 1 - nodeText.IndexOf('(') -1);
            string nodeDesc = nodeText.Substring(0, nodeText.IndexOf('('));
            int nodeSeq = parentNode.Nodes.Count ;

            string parentNodeName = "";
            parentNodeName = (parentNode.ImageIndex == FORMICON) ? parentNode.ParentNode.Tag.ToString() : parentNode.Tag.ToString();

            inBlock.SetColVal(1, 1, "name", parentNodeName + nodeResName);
            inBlock.SetColVal(1, 1, "fname", parentNodeName);
            inBlock.SetColVal(1, 1, "description", nodeDesc);
            inBlock.SetColVal(1, 1, "shortcut", " ");
            inBlock.SetColVal(1, 1, "resname", nodeResName);
            inBlock.SetColVal(1, 1, "treeno", "0");
            inBlock.SetColVal(1, 1, "treeseq", nodeSeq.ToString("d3"));

            inBlock.AddNewBlock();
            inBlock.SetColName(1, "NAME");
            inBlock.SetColVal(2, 1, 1, parentNodeName);
            inBlock.SetColName(2, "cursystem");
            inBlock.SetColVal(2, 1, 2, comboApp.EditValue.ToString().Split(':')[0]);

            outBlock = EI.EITuxedo.CallService("epestree_insb", inBlock);

            if (outBlock.sys_info.flag != 0)
            {
                ShowReturnMsg(outBlock);
                return false;
            }                                    

            return true;
        }

        //节点排序
        private void OrderNodes(TreeListNode parent)
        {
            EI.EIInfo inBlock = new EI.EIInfo();
            EI.EIInfo outBlock;

            inBlock.SetColName(1, 1, "name");
            inBlock.SetColName(1, 2, "treeseq");

            for (int i = 0; i < parent.Nodes.Count; i++)
            {
                inBlock.SetColVal(1, i + 1, "name", parent.Nodes[i].Tag);
                inBlock.SetColVal(1, i + 1, "treeseq", i.ToString("d3"));
            }

            inBlock.AddNewBlock();

            inBlock.SetColName(1, "cursystem");

            inBlock.SetColVal(2, 1, 1, comboApp.EditValue.ToString().Split(':')[0]);

            outBlock = EI.EITuxedo.CallService("epestree_upds", inBlock);

            if (outBlock.sys_info.flag == 0)
            {
                queryChildNodes(parent);
            }

            ShowReturnMsg(outBlock);  
        }

        #endregion

        #region 删除节点

        //删除节点
        private void popupTreeDelete_ItemClick(object sender, ItemClickEventArgs e)
        {
            DialogResult result = MessageBox.Show(string.Format(EP.EPES.EPESC0000073/*本节点下所有子节点都会被删除，且无法恢复！确定删除节点 [{0}] ？*/, treeList.FocusedNode.Tag.ToString()), "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.OK)
            {
                RemoveNodeFromTree(treeList.FocusedNode);
            }
        }

        private void treeList_DragLeave(object sender, EventArgs e)
        {
            //if (ef_args.b_info[2].Status != EFButtonKeysStatus.PRE_ACTIVE)
            //{
            //     MessageBox.Show(EPES.EPESC0000155, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}
             DialogResult result = MessageBox.Show(string.Format(EP.EPES.EPESC0000073/*本节点下所有子节点都会被删除，且无法恢复！确定删除节点 [{0}] ？*/, treeList.FocusedNode.Tag.ToString()), "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
             if (result == DialogResult.OK)
             {
                 RemoveNodeFromTree(treeList.FocusedNode);
             }
        }

        private void RemoveNodeFromTree(TreeListNode node)
        {
 
            EI.EIInfo inblkd = new EI.EIInfo();
            EI.EIInfo outblkd = new EI.EIInfo();

            TreeListNode parent = node.ParentNode;

            //权限检验
            inblkd.SetColName(1, 1, "name");
            inblkd.SetColVal(1, 1, node.Tag.ToString());

            inblkd.AddNewBlock();
            inblkd.SetColName(2, 1, "userid");
            inblkd.SetColName(2, 2, "appname");
            inblkd.SetColVal(2, 1, "userid",  "EventArgs.formUserId");
            inblkd.SetColVal(2, 1, "appname", comboApp.EditValue.ToString().Split(':')[0]);

            outblkd = EI.EITuxedo.CallService("epestree_delb", inblkd);
            if (outblkd.sys_info.flag == 0)
            {
                //this.//EFMsgInfo = EP.EPES.EPESC0000074/*删除成功*/;

                //OrderNodes(parent); //重新排序
                if (node.Level == 0)
                {
                    treeList.Nodes.Remove(node);
                }
                else
                {
                    queryChildNodes(parent);
                }                
            }
            else
            {
                //this.//EFMsgInfo = string.Format(EP.EPES.EPESC0000055/*删除失败：{0}*/, outblkd.sys_info.msg);
                //MessageBox.Show(//this.//EFMsgInfo, "提示" , MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region 菜单树图标状态
        private void fgDevCheckEdit2_CheckedChanged(object sender, EventArgs e)
        {
            this.treeList.Nodes.Clear();

            //LoadTree();


            colDLLNAME.Visible = true;
            colDLLPATH.Visible = false;

            fgGroupBoxButt.Text = EP.EPES.EPESC0000189/*按钮信息*/;
            colNAME1.Caption = EP.EPES.EPESC0000191/*按钮名*/;
        }

        private void treeList_AfterExpand(object sender, NodeEventArgs e)
        {
            e.Node.ImageIndex = e.Node.SelectImageIndex = FOLDERICON_EXPAND;
        }

        private void treeList_AfterCollapse(object sender, NodeEventArgs e)
        {
            e.Node.ImageIndex = e.Node.SelectImageIndex = FOLDERICON;
        }
        #endregion

        private void popupTreeModify_ItemClick(object sender, ItemClickEventArgs e)
        {
            try { }
            //{
            //    //this.//EFMsgInfo = "";
            //    ES.FormESOBJ_TREEINS FormIns = new ES.FormESOBJ_TREEINS();
            //    FormIns.Icon = this.Icon;

            //    FormIns.Name = this.treeList.FocusedNode.Tag.ToString();//传递参数，将选择的节点作为新增的目录的父节点
            //    FormIns.Cursystem = comboApp.EditValue.ToString().Split(':')[0];//传递参数代表当前系统
            //    FormIns.Treeseq = this.treeList.FocusedNode.Nodes.Count;//传递参数，父节点的子节点数作为新增目录的顺序号
            //    FormIns.Description = this.treeList.FocusedNode.GetDisplayText(treeListColumn1).Split('(')[0];
            //    FormIns.Fname = this.treeList.FocusedNode.ParentNode.Tag.ToString();
            //    FormIns.Mode = 1;
            //    FormIns.Text = EP.EPES.EPESC0000177/*修改目录*/;
            //    FormIns.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            //    DialogResult result = FormIns.ShowDialog();
            //    if (result == DialogResult.OK)
            //    {
            //        //this.//EFMsgInfo = EP.EPES.EPESC0000077/*执行成功*/;

            //        try
            //        {
            //            TreeListNode node = this.treeList.FocusedNode.ParentNode;
            //            queryChildNodes(node);
            //            node.ExpandAll();
            //        }
            //        catch (Exception err)
            //        {
            //            //this.//EFMsgInfo = err.ToString();
            //        }
            //    }
            //    else if (result == DialogResult.Cancel)
            //    {
            //        //this.//EFMsgInfo = EP.EPES.EPESC0000070/*取消新增*/;
            //    }
            //}
            catch (Exception err)
            {
                //this.//EFMsgInfo = err.ToString();
            }
        }

        private void treeList_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            dragFromGrid = false;
        }

        #endregion

        #region 其他

        private void ShowReturnMsg(EI.EIInfo outBlock)
        {
            //设置返回信息
            if (outBlock.sys_info.flag == 0)
            {
                //this.//EFMsgInfo = EP.EPES.EPESC0000077/*执行成功*/;

            }
            else if (outBlock.sys_info.flag < 0)
            {
                //this.//EFMsgInfo = string.Format(EP.EPES.EPESC0000057/*后台程序调用失败! {0} sqlcode is {1}{2}*/, outBlock.sys_info.msg, outBlock.sys_info.sqlcode.ToString(),
                //    outBlock.sys_info.sqlmes);

               // MessageBox.Show(//this.//EFMsgInfo, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        //切换子系统
        private void fgComboBoxApp_SelectedIndexChanged(object sender, EventArgs e)
        {
            //this.treeList.Nodes.Clear();
            //dataSetESOBJ.TESFORMRESINFO.Rows.Clear();
            //dataSetESOBJ.TESBUTTONRESINFO.Rows.Clear();

            ////查询菜单树
            //LoadTree();
            //QueryResGroup();
        }

        private void QueryResGroup()
        {
            //this.//EFMsgInfo = "";
            this.treeListResGroup.Nodes.Clear();

            EI.EIInfo inblk = new EI.EIInfo();
            inblk.AddColName(1, "id");
            inblk.AddColName(1, "groupname");
            inblk.AddColName(1, "appname");
            inblk.AddColName(1, "companycode");
            inblk.AddColName(1, "mode");
            inblk.AddColName(1, "inodes");

            inblk.SetColVal(1, 1, "id", "0");
            inblk.SetColVal(1, 1, "appname", comboApp.EditValue.ToString().Split(':')[0]);
            inblk.SetColVal(1, 1, "groupname", fgtResGroup.Text);
            inblk.SetColVal(1, 1, "companycode", this.comboComp.EditValue.ToString().Split(':')[0]);
            inblk.SetColVal(1, 1, "inodes", 0);
            inblk.SetColVal(1, 1, "mode", 0);

            EI.Logger.Error("begin call service epesgrgr_inq");
            EI.EIInfo outblk = EI.EITuxedo.CallService("epesgrgr_inq", inblk);
            EI.Logger.Error("end call service, begin binding");

            if (outblk.sys_info.flag == 0)
            {
                //treeListResGroup.DataSource = outblk.Tables[0];
                //outblk.Tables[0].TableName = "TESRESGROUP";
                treeListResGroup.DataSource = outblk.Tables[0];

                //dataSetESOBJ.TESRESGROUP.Clear();
                //outblk.ConvertToStrongType(dataSetESOBJ);
                //dataSetESOBJ.TESRESGROUP.AcceptChanges();
            }
            EI.Logger.Error("binding end");
            //treeListResGroup.FocusedNode = null;
        }

        //查询当前checkbox选中行数
        private int getChoiceCount(GridView gridview)
        {
            int choiceCount = 0;
            for (int i = 0; i < gridview.RowCount; i++)
            {
                if (gridview.GetRowCellValue(i, "selected") != null && (bool)gridview.GetRowCellValue(i, "selected") == true)
                {
                    choiceCount++;
                }
            }
            return choiceCount;
        }
        #endregion

        #region 画面操作
        private void fgButton1_Click(object sender, EventArgs e)
        {
            QueryForm();

            if (gridViewFormInfo.FocusedRowHandle >= 0)
            {
                int t = gridViewFormInfo.GetDataSourceRowIndex(gridViewFormInfo.FocusedRowHandle);
                formName = dataSetESOBJ.TESFORMRESINFO.Rows[t]["NAME"].ToString();
                appName = dataSetESOBJ.TESFORMRESINFO.Rows[t]["APPNAME"].ToString();

                QueryButton();
            }
            else
            {
                dataSetESOBJ.TESBUTTONRESINFO.Rows.Clear();
            }
        }

        private void QueryForm()
        {
            EI.EIInfo inblk = new EI.EIInfo();
            EI.EIInfo outblk = new EI.EIInfo();

            inblk.SetColName(1, "name");
            inblk.SetColName(2, "dllname");
            inblk.SetColName(3, "appname");
            inblk.SetColName(4, "companycode");

            string companyCode = this.comboComp.EditValue.ToString().Split(':')[0];
            inblk.SetColVal(1, 1, 1, fgtFormName.Text);
            inblk.SetColVal(1, 1, 2, fgtDllName.Text);
            inblk.SetColVal(1, 1, 3, comboApp.EditValue.ToString().Split(':')[0]);
            inblk.SetColVal(1, 1, 4, companyCode);

            if (chkAuth.Checked) //未分配到资源组的
            {
                outblk = EI.EITuxedo.CallService("epesform_inq3", inblk);
            }
            else if (chkTree.Checked) //未挂到菜单上的
            {
                outblk = EI.EITuxedo.CallService("epesform_inq4", inblk);
            }
            else
            {
                outblk = EI.EITuxedo.CallService("epesform_inq2", inblk);
            }

            ShowReturnMsg(outblk);

            dataSetESOBJ.TESFORMRESINFO.Rows.Clear();
            outblk.ConvertToStrongType(dataSetESOBJ);
            dataSetESOBJ.TESFORMRESINFO.AcceptChanges();

            gridViewFormInfo_FocusedRowChanged_1(null, null);

            //gridViewFormInfo.BestFitColumns();
        }

        void FormInfoEmbeddedNavigator_ButtonClick(object sender, NavigatorButtonClickEventArgs e)
        {
            fgDevGridFormInfo.Parent.Focus();
            switch (e.Button.ImageIndex)
            { 
                case SAVE:
                    //检查数据
                    if (!formCheck())
                    {
                       break;
                    }
                    //操作数据库
                    if (!SaveFormInfo())
                    {
                       break;
                    }
                    QueryForm();
                    break;
                case DISCARD:
                    QueryForm();
                    break;
                Default:
                    break;
            }
        }

        private bool SaveFormInfo()
        {
            DataTable instable = dataSetESOBJ.TESFORMRESINFO.Clone();// this.dataSetESOBJ.TESFORMRESINFO.GetChanges(DataRowState.Added);
            DataTable deltable = null; //this.dataSetESOBJ.TESFORMRESINFO.GetChanges(DataRowState.Deleted);
            DataTable updtable = dataSetESOBJ.TESFORMRESINFO.Clone();// this.dataSetESOBJ.TESFORMRESINFO.GetChanges(DataRowState.Modified);

            //FilterData(instable);
            //FilterData(updtable);
            DataRow dr = null;
            for (int rowIndex = 0; rowIndex < gridViewFormInfo.RowCount; ++rowIndex)
            {
                if(fgDevGridFormInfo.GetSelectedColumnChecked(rowIndex))
                {
                    dr = gridViewFormInfo.GetDataRow(rowIndex);
                    if(dr.RowState == DataRowState.Added)
                    {
                        instable.Rows.Add(dr.ItemArray);
                    }
                    else if(dr.RowState == DataRowState.Modified)
                    {
                        updtable.Rows.Add(dr.ItemArray);
                    }
                }            
            }
            deltable = dataSetESOBJ.TESFORMRESINFO.GetChanges(DataRowState.Deleted);

            EI.EIInfo inBlock = new EI.EIInfo();

            inBlock.SetColName(1, "appname");
            inBlock.SetColVal(1, "appname", this.comboApp.EditValue.ToString().Split(':')[0]);

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
                EI.EIInfo outBlock = EI.EITuxedo.CallService("epesform_do", inBlock);
                if (outBlock.sys_info.flag < 0)
                {
                    MessageBox.Show(outBlock.sys_info.msg, EP.EPES.EPESC0000024, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }

            if (gridViewFormInfo.FocusedRowHandle < 0)
            {
                formName = "";
            }
            else
            {
                formName = gridViewFormInfo.GetRowCellValue(gridViewFormInfo.FocusedRowHandle, "NAME").ToString();
            }

            QueryButton();
            return true;
        }

        /// <summary>
        /// 检查数据正确性
        /// </summary>
        /// <returns></returns>true 正确 false 存在问题
        private bool formCheck()
        {
            this.tESFORMRESINFOBindingSource.EndEdit();
            //检查数据准确性
            for (int i = 0; i < this.gridViewFormInfo.RowCount; i++)
            {
                if (gridViewFormInfo.GetDataRow(i).RowState == DataRowState.Added || gridViewFormInfo.GetDataRow(i).RowState == DataRowState.Modified)
                {
                    if (/*gridViewFormInfo.GetRowCellValue(i, "selected") == null
                     || gridViewFormInfo.GetRowCellValue(i, "selected").ToString() == "False"*/
                        !fgDevGridFormInfo.GetSelectedColumnChecked(i))
                    {
                        continue;
                    }
                    if (gridViewFormInfo.GetRowCellValue(i, "NAME") != null)
                    {
                        if (gridViewFormInfo.GetRowCellValue(i, "NAME").ToString().Trim() == "")
                        {
                            MessageBox.Show(EP.EPES.EPESC0000033/*画面名不能为空，请重新输入！*/, EP.EPES.EPESC0000034/*输入信息不合法*/, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return false;
                        }
                        string formname = gridViewFormInfo.GetRowCellValue(i, "NAME").ToString();
                        for (int j = 0; j < formname.Length; j++)
                        {
                            if (formname[j] < 48 || (formname[j] > 57 && formname[j] < 65) || formname[j] > 90)
                            {
                                MessageBox.Show(EP.EPES.EPESC0000121/*画面名必须是大写字母与数字的组合，请重新输入！*/, EP.EPES.EPESC0000034/*输入信息不合法*/, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return false;
                            }
                        }
                        if (EPESCommon.GetStringLength(formname) > 128)
                        {
                            MessageBox.Show(EP.EPES.EPESC0000037/*画面名最多允许输入128位，请重新输入！*/, EP.EPES.EPESC0000034/*输入信息不合法*/, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return false;
                        }

                        if (formname.Length < 2)
                        {
                            MessageBox.Show(EP.EPES.EPESC0000038/*请输入2位以上的画面名！*/, EP.EPES.EPESC0000034/*输入信息不合法*/, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return false;
                        }
                    }
                    else
                    {
                        MessageBox.Show(EP.EPES.EPESC0000033/*画面名不能为空，请重新输入！*/, EP.EPES.EPESC0000034/*输入信息不合法*/, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }

                    if (gridViewFormInfo.GetRowCellValue(i, "DESCRIPTION") != null)
                    {
                        if (gridViewFormInfo.GetRowCellValue(i, "DESCRIPTION").ToString().Trim() == "")
                        {
                            MessageBox.Show(EP.EPES.EPESC0000035/*画面描述不能为空，请重新输入！*/, EP.EPES.EPESC0000034/*输入信息不合法*/, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return false;
                        }
                        if (EPESCommon.GetStringLength(gridViewFormInfo.GetRowCellValue(i, "DESCRIPTION").ToString()) > 80)
                        {
                            MessageBox.Show(EP.EPES.EPESC0000039/*画面描述最多允许输入80位，请重新输入！*/, EP.EPES.EPESC0000034/*输入信息不合法*/, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return false;
                        }
                    }
                    else
                    {
                        MessageBox.Show(EP.EPES.EPESC0000035/*画面描述不能为空，请重新输入！*/, EP.EPES.EPESC0000034/*输入信息不合法*/, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }

                    if (gridViewFormInfo.GetRowCellValue(i, "APPNAME") != null)
                    {
                        if (gridViewFormInfo.GetRowCellValue(i, "APPNAME").ToString().Trim() == "")
                        {
                            MessageBox.Show(EP.EPES.EPESC0000036/*子系统不能为空，请重新输入！*/, EP.EPES.EPESC0000034/*输入信息不合法*/, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return false;
                        }
                    }
                    else
                    {
                        MessageBox.Show(EP.EPES.EPESC0000036/*子系统不能为空，请重新输入！*/, EP.EPES.EPESC0000034/*输入信息不合法*/, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }

                    if (gridViewFormInfo.GetRowCellValue(i, "DLLNAME") != null)
                    {
                        if (gridViewFormInfo.GetRowCellValue(i, "DLLNAME").ToString().Trim() != "")
                        {
                            string dllname = gridViewFormInfo.GetRowCellValue(i, "DLLNAME").ToString().ToUpper();
                            if (dllname.Length > 4 && dllname.Substring(dllname.Length - 4, 4) == ".DLL")
                            {
                                for (int j = 0; j < dllname.Length - 4; j++)
                                {
                                    if (dllname[j] < 48 || (dllname[j] > 57 && dllname[j] < 65) || (dllname[j] > 90 && dllname[j] != 95))
                                    {
                                        MessageBox.Show(EP.EPES.EPESC0000040/*动态库必须由数字、字母或下划线加'.DLL'组成，请重新输入！*/, EP.EPES.EPESC0000034/*输入信息不合法*/, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        return false;
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show(EP.EPES.EPESC0000040/*动态库必须由数字、字母或下划线加'.DLL'组成，请重新输入！*/, EP.EPES.EPESC0000034/*输入信息不合法*/, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return false;
                            }
                        }
                        else
                        {

                            MessageBox.Show(EP.EPES.EPESC0000161/*动态库不能为空，请重新输入！*/, EP.EPES.EPESC0000034/*输入信息不合法*/, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return false;

                        }
                    }
                    else
                    {

                        MessageBox.Show(EP.EPES.EPESC0000161/*动态库不能为空，请重新输入！*/, EP.EPES.EPESC0000034/*输入信息不合法*/, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }

                    if (gridViewFormInfo.GetRowCellValue(i, "ABBREV") != null)
                    {
                        if (gridViewFormInfo.GetRowCellValue(i, "ABBREV").ToString().Length > 10)
                        {
                            MessageBox.Show(EP.EPES.EPESC0000041/*缩写最多允许输入10位，请重新输入！*/, EP.EPES.EPESC0000034/*输入信息不合法*/, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return false;
                        }
                    }

                    if (gridViewFormInfo.GetRowCellValue(i, "ICONNUM") != null)
                    {
                        if (gridViewFormInfo.GetRowCellValue(i, "ICONNUM").ToString().Length > 3)
                        {
                            MessageBox.Show(EP.EPES.EPESC0000042/*图标编号最多允许输入3位，请重新输入！*/, EP.EPES.EPESC0000034/*输入信息不合法*/, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return false;
                        }
                    }

                    if (gridViewFormInfo.GetRowCellValue(i, "FORM_CALL_MODE") != null)
                    {
                        if (gridViewFormInfo.GetRowCellValue(i, "FORM_CALL_MODE").ToString().Length > 1)
                        {
                            MessageBox.Show(EP.EPES.EPESC0000043/*调用方式最多允许输入1位，请重新输入！*/, EP.EPES.EPESC0000034/*输入信息不合法*/, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return false;
                        }
                    }

                    if (gridViewFormInfo.GetRowCellValue(i, "DLLPATH") != null)
                    {
                        if (gridViewFormInfo.GetRowCellValue(i, "DLLPATH").ToString().Length > 500)
                        {
                            MessageBox.Show(EP.EPES.EPESC0000044/*调用路径最多允许输入500位，请重新输入！*/, EP.EPES.EPESC0000034/*输入信息不合法*/, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        //设置画面信息子系统列不能修改
        private void gridViewFormInfo_FocusedRowChanged_1(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                if (gridViewFormInfo.FocusedRowHandle < 0) return;

                if (fgDevGridFormInfo.Focused)
                {
                    formName = gridViewFormInfo.GetRowCellValue(gridViewFormInfo.FocusedRowHandle, "NAME").ToString();
                    appName = gridViewFormInfo.GetRowCellValue(gridViewFormInfo.FocusedRowHandle, "APPNAME").ToString();
                    QueryButton();
                }

                if (gridViewFormInfo.FocusedRowHandle >= 0 && gridViewFormInfo.FocusedRowHandle < this.gridViewFormInfo.RowCount)
                {
                    if (this.gridViewFormInfo.GetFocusedDataRow().RowState == DataRowState.Added)
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
                //EFMsgInfo = ex.Message;
            }
        }

        private void gridViewFormInfo_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column != fgDevGridFormInfo.SelectionColumn)
            {
                fgDevGridFormInfo.SetSelectedColumnChecked(e.RowHandle, true);
                //gridViewFormInfo.SetRowCellValue(e.RowHandle, fgDevGridFormInfo.SelectionColumn, true);
            }

        }

        private void gridViewFormInfo_ShownEditor(object sender, EventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            if (view.FocusedColumn == colNAME || view.FocusedColumn == colDLLNAME || view.FocusedColumn == colABBREV)
            {
                PropertyDescriptorCollection pdc = TypeDescriptor.GetProperties(view.ActiveEditor.Properties);
                PropertyDescriptor pd = pdc.Find("CharacterCasing", false);
                if (pd != null)
                {
                    pd.SetValue(view.ActiveEditor.Properties, System.Windows.Forms.CharacterCasing.Upper);
                }
            }
        }

        private void fgDevGridFormInfo_MouseMove(object sender, MouseEventArgs e)
        {
            if (dataSetESOBJ.TESFORMRESINFO.Rows.Count == 0)
            {
                return;
            }
            if (e.Button != MouseButtons.Left) return;

            if (fgDevGridFormInfo.EFChoiceCount == 0 /*getChoiceCount(gridViewFormInfo) == 0*/)
            {
                return;
            }

            EI.EIInfo inBlock = new EI.EIInfo();
            inBlock.SetColName(1, 1, "groupid");
            inBlock.SetColName(1, 2, "appname");
            inBlock.SetColName(1, 3, "restype");
            inBlock.SetColName(1, 4, "groupname");
            inBlock.AddNewBlock();
            inBlock.SetColName(2, 1, "resid");
            inBlock.SetColName(2, 2, "resname");
            inBlock.SetColName(2, 3, "fname");
            inBlock.SetColName(2, 4, "desc");

            inBlock.SetColVal(1, 1, "appname", comboApp.EditValue.ToString().Split(':')[0]);
            inBlock.SetColVal(1, 1, "restype", "form");

            for (int i = 0, j = 1; i < this.gridViewFormInfo.RowCount; i++)
            {
                //取出选中行
                if (//gridViewFormInfo.GetRowCellValue(i, "selected") != null
                    //&& gridViewFormInfo.GetRowCellValue(i, "selected").ToString() == "True" 
                    fgDevGridFormInfo.GetSelectedColumnChecked(i)
                    )
                {
                    inBlock.SetColVal(2, j, "resid", dataSetESOBJ.TESFORMRESINFO.Rows[i]["ACLID"].ToString());
                    inBlock.SetColVal(2, j, "resname", dataSetESOBJ.TESFORMRESINFO.Rows[i]["NAME"].ToString());
                    inBlock.SetColVal(2, j, "fname", " ");
                    inBlock.SetColVal(2, j, "desc", dataSetESOBJ.TESFORMRESINFO.Rows[i]["DESCRIPTION"].ToString());
                    j++;
                }
            }

            dragFromGrid = true;

            fgDevGridFormInfo.DoDragDrop(inBlock, DragDropEffects.Copy);
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
        #endregion

        #region 按钮操作
        void ButtInfoEmbeddedNavigator_ButtonClick(object sender, NavigatorButtonClickEventArgs e)
        {
            fgDevGridButtInfo.Parent.Focus();
            switch (e.Button.ImageIndex)
            {
                case SAVE:
                    //检查数据
                    if (!btnCheck())
                    {
                        break;
                    }
                    //操作数据库
                    if (!SaveButtInfo())
                    {
                        break;
                    }
                    QueryButton();
                    break;
                case DISCARD:
                    QueryButton();
                    break;
                Default:
                    break;
            }
        }

        private bool SaveButtInfo()
        {
            DataTable instable = dataSetESOBJ.TESBUTTONRESINFO.Clone();// this.dataSetESOBJ.TESBUTTONRESINFO.GetChanges(DataRowState.Added);
            DataTable deltable = null; // this.dataSetESOBJ.TESBUTTONRESINFO.GetChanges(DataRowState.Deleted);
            DataTable updtable = dataSetESOBJ.TESBUTTONRESINFO.Clone(); // this.dataSetESOBJ.TESBUTTONRESINFO.GetChanges(DataRowState.Modified);

            //FilterData(instable);
            //FilterData(updtable);

            DataRow dr = null;
            for (int rowIndex = 0; rowIndex < gridViewButtInfo.RowCount; ++rowIndex)
            { 
                if(fgDevGridButtInfo.GetSelectedColumnChecked(rowIndex))
                {
                    dr = gridViewButtInfo.GetDataRow(rowIndex);
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

            deltable = dataSetESOBJ.TESBUTTONRESINFO.GetChanges(DataRowState.Deleted);

            EI.EIInfo inBlock = new EI.EIInfo();
            inBlock.SetColName(1, "appname");
            inBlock.SetColVal(1, "appname", this.comboApp.EditValue.ToString().Split(':')[0]);

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

                EI.EIInfo outBlock = EI.EITuxedo.CallService("epesbutt_do", inBlock);
                if (outBlock.sys_info.flag < 0)
                {
                    MessageBox.Show(outBlock.sys_info.msg, EP.EPES.EPESC0000024, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            return true;
        }

        private void QueryButton()
        {
            if (formName == string.Empty) return;

            //this.//EFMsgInfo = "";

            EI.EIInfo inblk = new EI.EIInfo();
            EI.EIInfo outblk = new EI.EIInfo();

            inblk.SetColName(1, "fname");
            inblk.SetColName(2, "appname");
            inblk.SetColName(3, "companycode");

            string companyCode = this.comboComp.EditValue.ToString().Split(':')[0];
            inblk.SetColVal(1, 1, "fname", formName);
            inblk.SetColVal(1, 1, "appname", appName);
            inblk.SetColVal(1, 1, "companycode", companyCode);

            if (chkAuth.Checked) //查询未分配到资源组的按钮
            {
                inblk.SetColVal(1, 1, "fname", fgtFormName.Text);
                outblk = EI.EITuxedo.CallService("epesbutt_inq3", inblk);
            }
            else
            {
                outblk = EI.EITuxedo.CallService("epesbutt_inq2", inblk);
            }

            ShowReturnMsg(outblk);

            dataSetESOBJ.TESBUTTONRESINFO.Rows.Clear();
            outblk.ConvertToStrongType(dataSetESOBJ);
            dataSetESOBJ.TESBUTTONRESINFO.AcceptChanges();

            //gridViewButtInfo.BestFitColumns();
        }

        //检查按钮信息合法性
        private bool btnCheck()
        {
            this.tESBUTTONRESINFOBindingSource.EndEdit();
            //检查数据准确性
            for (int i = 0; i < this.gridViewButtInfo.RowCount; i++)
            {
                if (//gridViewButtInfo.GetRowCellValue(i, "selected") == null
                 //|| gridViewButtInfo.GetRowCellValue(i, "selected").ToString() == "False"
                    !fgDevGridButtInfo.GetSelectedColumnChecked(i))
                {
                    continue;
                }
                if (gridViewButtInfo.GetRowCellValue(i, "NAME") != null)
                {
                    if (gridViewButtInfo.GetRowCellValue(i, "NAME").ToString().Trim() == "")
                    {
                        MessageBox.Show(EP.EPES.EPESC0000052/*按钮名不能为空，请重新输入！*/, EP.EPES.EPESC0000034/*输入信息不合法*/, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                    if (gridViewButtInfo.GetRowCellValue(i, "NAME").ToString().Length < 2)
                    {
                        MessageBox.Show(EP.EPES.EPESC0000051/*请输入大于两位的按钮名！*/, EP.EPES.EPESC0000034/*输入信息不合法*/, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                    if (EPESCommon.GetStringLength(gridViewButtInfo.GetRowCellValue(i, "NAME").ToString()) > 125)
                    {
                        MessageBox.Show(EP.EPES.EPESC0000050/*按钮名不能超过125位，请重新输入！*/, EP.EPES.EPESC0000034/*输入信息不合法*/, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                }
                else
                {
                    MessageBox.Show(EP.EPES.EPESC0000052/*按钮名不能为空，请重新输入！*/, EP.EPES.EPESC0000034/*输入信息不合法*/, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                if (gridViewButtInfo.GetRowCellValue(i, "FNAME") != null)
                {
                    if (gridViewButtInfo.GetRowCellValue(i, "FNAME").ToString().Trim() == "")
                    {
                        MessageBox.Show(EP.EPES.EPESC0000192/*所属画面不能为空，请重新输入！*/, EP.EPES.EPESC0000034/*输入信息不合法*/, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                }
                else
                {
                    MessageBox.Show(EP.EPES.EPESC0000192/*所属画面不能为空，请重新输入！*/, EP.EPES.EPESC0000034/*输入信息不合法*/, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }


                if (gridViewButtInfo.GetRowCellValue(i, "DESCRIPTION") != null)
                {
                    if (gridViewButtInfo.GetRowCellValue(i, "DESCRIPTION").ToString().Trim() == "")
                    {
                        MessageBox.Show(EP.EPES.EPESC0000048/*按钮描述不能为空，请重新输入！*/, EP.EPES.EPESC0000034/*输入信息不合法*/, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                    if (EPESCommon.GetStringLength(gridViewButtInfo.GetRowCellValue(i, "DESCRIPTION").ToString()) > 75)
                    {
                        MessageBox.Show(EP.EPES.EPESC0000047/*按钮描述不能超过75位，请重新输入！*/, EP.EPES.EPESC0000034/*输入信息不合法*/, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                }
                else
                {
                    MessageBox.Show(EP.EPES.EPESC0000048/*按钮描述不能为空，请重新输入！*/, EP.EPES.EPESC0000034/*输入信息不合法*/, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                //只有按钮名为F1-F12时，操作类型才能选择多步操作
                if (gridViewButtInfo.GetRowCellValue(i, "OPTYPE") != null)
                {
                    if (gridViewButtInfo.GetRowCellValue(i, "OPTYPE").ToString() == "")
                    {
                        MessageBox.Show(EP.EPES.EPESC0000144/*操作类型不能为空，请重新输入！*/, EP.EPES.EPESC0000034/*输入信息不合法*/, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }

                    string name = gridViewButtInfo.GetRowCellValue(i, "NAME").ToString().Trim();
                    int num = 0;
                    if (name.Length > 1 && name.Substring(0, 1) == "F" && name.Substring(1, 1) != "0")
                    {
                        try
                        {
                            num = Int32.Parse(name.Substring(1));
                        }
                        catch
                        {
                            num = -1;
                        }
                        if ((num < 1 || num > 12) && (gridViewButtInfo.GetRowCellValue(i, "OPTYPE").ToString() == "B"))
                        {
                            MessageBox.Show(EP.EPES.EPESC0000053/*按钮名为F1-F12时操作类型才能选择“多步操作”！*/, EP.EPES.EPESC0000122/*无法选择“多步操作”*/, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }
                    }
                    else
                    {
                        if (gridViewButtInfo.GetRowCellValue(i, "OPTYPE").ToString() == "B")
                        {
                            MessageBox.Show(EP.EPES.EPESC0000053/*按钮名为F1-F12时操作类型才能选择“多步操作”！*/, EP.EPES.EPESC0000122/*无法选择“多步操作”*/, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }
                    }
                }
                else
                {
                    MessageBox.Show(EP.EPES.EPESC0000144/*操作类型不能为空，请重新输入！*/, EP.EPES.EPESC0000034/*输入信息不合法*/, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            return true;

        }

        private void gridViewButtInfo_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column != fgDevGridButtInfo.SelectionColumn)
            {
                this.fgDevGridButtInfo.SetSelectedColumnChecked(e.RowHandle, true);
                //gridViewButtInfo.SetRowCellValue(e.RowHandle, "selected", true);
            }
        }

        private void fgDevGridButtInfo_EF_GridBar_AddRow_Event(object sender, NavigatorButtonClickEventArgs e)
        {
            gridViewButtInfo.AddNewRow();
            gridViewButtInfo.RefreshData();
            DataRow dr = gridViewButtInfo.GetDataRow(gridViewButtInfo.RowCount - 1);
            dr["FNAME"] = formName;
            dr["APPNAME"] = appName;

            //fgDevGridButtInfo.SetSelectedColumnChecked(gridViewButtInfo.FocusedRowHandle, true);
            //gridViewButtInfo.SetRowCellValue(gridViewButtInfo.RowCount - 1, "selected", true);
        }

        #endregion

        #region 细部资源
        private void fgButtonQry_Click(object sender, EventArgs e)
        {
            queryOth();
        }

        private void queryOth()
        {
            //查询
            //this.//EFMsgInfo = "";
            string type = "";

            this.gridViewOthInfo.ClearSorting();

            EI.EIInfo inblk = new EI.EIInfo();
            EI.EIInfo outblk = new EI.EIInfo();

            inblk.SetColName(1, "name");
            inblk.SetColName(2, "resourcetype");
            inblk.SetColName(3, "appname");

            if (fgDevLookUpEditType.EditValue == null)
            {
                MessageBox.Show("获取资源类型失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            type = fgDevLookUpEditType.EditValue.ToString();

            if (type.Length < 1)
            {
                type = "0";
            }

            inblk.SetColVal(1, 1, "name", fgtOthName.Text);
            inblk.SetColVal(1, 1, "resourcetype", type);
            inblk.SetColVal(1, 1, "appname", comboApp.EditValue.ToString().Split(':')[0]);

            if (chkOth.Checked)
            {
                outblk = EI.EITuxedo.CallService("epesother_inq3", inblk);
            }
            else
            {
                outblk = EI.EITuxedo.CallService("epesother_inq", inblk);
            }

            dataSetESOBJ.TESOTHERRESINFO.Rows.Clear();
            outblk.ConvertToStrongType(dataSetESOBJ);
            dataSetESOBJ.TESOTHERRESINFO.AcceptChanges();

            gridViewOthInfo_FocusedRowChanged(null, null);

            ShowReturnMsg(outblk);
        }

        private bool SaveOth()
        {
            DataTable instable = this.dataSetESOBJ.TESOTHERRESINFO.Clone(); //.GetChanges(DataRowState.Added);
            DataTable deltable = null;// this.dataSetESOBJ.TESOTHERRESINFO.GetChanges(DataRowState.Deleted);
            DataTable updtable = this.dataSetESOBJ.TESOTHERRESINFO.Clone(); //.GetChanges(DataRowState.Modified);

            //FilterData(instable);
            //FilterData(updtable);

            DataRow dr = null;
            for (int rowIndex = 0; rowIndex < gridViewOthInfo.RowCount; ++rowIndex)
            {
                if (fgDevGridOth.GetSelectedColumnChecked(rowIndex))
                {
                    dr = gridViewOthInfo.GetDataRow(rowIndex);
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

            deltable = dataSetESOBJ.TESOTHERRESINFO.GetChanges(DataRowState.Deleted);

            EI.EIInfo inBlock = new EI.EIInfo();
            inBlock.SetColName(1, "appname");
            inBlock.SetColVal(1, "appname", this.comboApp.EditValue.ToString().Split(':')[0]);

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

                EI.EIInfo outBlock = EI.EITuxedo.CallService("epesother_do", inBlock);
                if (outBlock.sys_info.flag < 0)
                {
                    MessageBox.Show(outBlock.sys_info.msg, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            return true;
        }

        private void gridViewOthInfo_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column != fgDevGridOth.SelectionColumn)
            {
                fgDevGridOth.SetSelectedColumnChecked(e.RowHandle, true);
                //this.gridViewOthInfo.SetRowCellValue(e.RowHandle, "selected", true);
            }
        }

        /// <summary>
        /// 检查数据正确性
        /// </summary>
        /// <returns></returns>true 正确 false 存在问题
        private bool othCheck()
        {
            this.tESOTHERRESINFOBindingSource.EndEdit();
            //检查数据准确性
            for (int i = 0; i < this.gridViewOthInfo.RowCount; i++)
            {
                if (//gridViewOthInfo.GetRowCellValue(i, "selected") == null
                 //|| gridViewOthInfo.GetRowCellValue(i, "selected").ToString() == "False"
                    !fgDevGridOth.GetSelectedColumnChecked(i))
                {
                    continue;
                }
                if (gridViewOthInfo.GetRowCellValue(i, "NAME") != null)
                {
                    if (gridViewOthInfo.GetRowCellValue(i, "NAME").ToString().Trim() == "")
                    {
                        MessageBox.Show(EP.EPES.EPESC0000147/*名称不能为空，请重新输入！*/, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    if (EPESCommon.GetStringLength(gridViewOthInfo.GetRowCellValue(i, "NAME").ToString()) > 128)
                    {
                        MessageBox.Show(EP.EPES.EPESC0000148/*名称不能超过128位，请重新输入！*/, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
                else
                {
                    MessageBox.Show(EP.EPES.EPESC0000147/*名称不能为空，请重新输入！*/, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                if (gridViewOthInfo.GetRowCellValue(i, "DESCRIPTION") != null)
                {
                    if (EPESCommon.GetStringLength(gridViewOthInfo.GetRowCellValue(i, "DESCRIPTION").ToString()) > 80)
                    {
                        MessageBox.Show(EP.EPES.EPESC0000150/*资源描述不能超过80位，请重新输入！*/, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }

                if (gridViewOthInfo.GetRowCellValue(i, "RESOURCETYPE") != null)
                {
                    if (gridViewOthInfo.GetRowCellValue(i, "RESOURCETYPE").ToString().Trim() == "")
                    {
                        MessageBox.Show(EP.EPES.EPESC0000149/*资源类型不能为空，请重新输入！*/, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
                else
                {
                    MessageBox.Show(EP.EPES.EPESC0000149/*资源类型不能为空，请重新输入！*/, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            return true;

        }

        //设置资源名不能修改
        private void gridViewOthInfo_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                if (gridViewOthInfo.FocusedRowHandle < 0) return;

                if (/*e.FocusedRowHandle >= 0 &&*/ gridViewOthInfo.FocusedRowHandle < this.gridViewOthInfo.RowCount)
                {
                    if (this.gridViewOthInfo.GetFocusedDataRow().RowState == DataRowState.Added)
                    {
                        this.gridColumnOthName.OptionsColumn.AllowEdit = true;
                    }
                    else
                    {
                        this.gridColumnOthName.OptionsColumn.AllowEdit = false;
                    }
                }
            }
            catch (Exception ex)
            {
                //EFMsgInfo = ex.Message;
            }
        }

        private void OthEmbeddedNavigator_ButtonClick(object sender, NavigatorButtonClickEventArgs e)
        {
            fgDevGridOth.Parent.Focus();
            switch (e.Button.ImageIndex)
            {
                case SAVE:
                    if (othCheck())
                    {
                        if (SaveOth())
                        {
                            queryOth();
                        }
                    }
                    break;
                case DISCARD:
                    queryOth();
                    break;
                Default:
                    break;
            }
            
        }
        #endregion

        #region 资源组操作
        private void treeListResGroup_MouseUp(object sender, MouseEventArgs e)
        {
            //if (ef_args.b_info[3].Status != EFButtonKeysStatus.PRE_ACTIVE) return;

            if (e.Button == MouseButtons.Right)
            {
                DevExpress.XtraTreeList.TreeListHitInfo hi = treeListResGroup.CalcHitInfo(e.Location);
                if (hi != null && hi.Node != null)
                {
                    if (hi.Node.Level == 0)
                    {
                        treeListResGroup.FocusedNode = hi.Node;
                    }
                    else if ((treeListResGroup.Selection.Count != 0 && !treeListResGroup.Selection.Contains(hi.Node)) || treeListResGroup.Selection.Count == 0)
                    {
                        treeListResGroup.Selection.Clear();
                        treeListResGroup.Selection.Add(hi.Node);
                        treeListResGroup.FocusedNode = hi.Node;
                    }

                    if (hi.Node.Level == 0)//资源组
                    {
                        barButtonItemAddResGroup.Visibility = BarItemVisibility.Always;
                        barButtonItemModResGroup.Visibility = BarItemVisibility.Always;
                        barButtonItemDelResGroup.Visibility = BarItemVisibility.Always;
                        barButtonItemDelRes.Visibility = BarItemVisibility.Never;
                    }
                    else //资源
                    {
                        barButtonItemAddResGroup.Visibility = BarItemVisibility.Never;
                        barButtonItemModResGroup.Visibility = BarItemVisibility.Never;
                        barButtonItemDelResGroup.Visibility = BarItemVisibility.Never;
                        barButtonItemDelRes.Visibility = BarItemVisibility.Always;
                    }
                }
                else //空白处
                {
                    barButtonItemAddResGroup.Visibility = BarItemVisibility.Always;
                    barButtonItemModResGroup.Visibility = BarItemVisibility.Never;
                    barButtonItemDelResGroup.Visibility = BarItemVisibility.Never;
                    barButtonItemDelRes.Visibility = BarItemVisibility.Never;
                }
                this.popupMenuResGroup.ShowPopup(MousePosition);
            }
        }
        
        //新增资源组
        private void barButtonItemAddResGroup_ItemClick(object sender, ItemClickEventArgs e)
        {
            //FormResGroup formResGroup = new FormResGroup();
            //formResGroup.OpType = OperationType.AddResGroup;
            //formResGroup.Appname = comboApp.EditValue.ToString().Split(':')[0];
            //formResGroup.Companycode = comboComp.EditValue.ToString().Split(':')[0];
            //formResGroup.ShowDialog();

            this.QueryResGroup();            
        }

        //修改资源组
        private void barButtonItemModResGroup_ItemClick(object sender, ItemClickEventArgs e)
        {
            //FormResGroup formResGroup = new FormResGroup();
            //formResGroup.OpType = OperationType.ModResGroup;
            //formResGroup.ResGroupID = treeListResGroup.FocusedNode.GetValue(treeListColumnRGID).ToString();
            //string nodeText = treeListResGroup.FocusedNode.GetDisplayText(treeListColumnRGName);
            //formResGroup.ResGroupName = nodeText.Split('[')[1].Trim(']');
            //formResGroup.ResGroupDesc = nodeText.Split('[')[0];
            //formResGroup.Appname = comboApp.EditValue.ToString().Split(':')[0];
            //if (formResGroup.ShowDialog() == DialogResult.OK)
            //{
            //    this.QueryResGroup();
            //}
        }

        //删除资源组
        private void barButtonItemDelResGroup_ItemClick(object sender, ItemClickEventArgs e)
        {
            string resGroupName = treeListResGroup.FocusedNode.GetDisplayText(treeListColumnRGName);
            if (MessageBox.Show(string.Format(EP.EPES.EPESC0000168/*是否删除资源组:{0}*/, resGroupName), "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            DataTable dt = new DataTable("DELETE_BLOCK");
            dt.Columns.Add("ID");

            foreach (var node in treeListResGroup.Selection)
            {
                dt.Rows.Add(new object[] { (node as TreeListNode).GetValue(treeListColumnRGID).ToString() });
            }

            //dt.Rows.Add(new object[] { treeListResGroup.FocusedNode.GetValue(treeListColumnRGID).ToString() });
            EI.EIInfo inBlock = new EI.EIInfo();

            inBlock.SetColName(1, "userid");
            inBlock.SetColVal(1, "userid",  "EventArgs.formUserId");

            inBlock.SetColName(2, "appname");
            inBlock.SetColVal(1, "appname", comboApp.EditValue.ToString().Split(':')[0]);

            inBlock.SetColName(3, "grouptype");
            inBlock.SetColVal(1, "grouptype", 1);

            inBlock.Tables.Add(dt);

            EI.EIInfo outBlock = EI.EITuxedo.CallService("epesgroup_do", inBlock);
            if (outBlock.sys_info.flag < 0)
            {
                MessageBox.Show(outBlock.sys_info.msg, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                //QueryResGroup();
                treeListResGroup.DeleteSelectedNodes();
            }
        }

        //移除资源
        private void barButtonItemDelRes_ItemClick(object sender, ItemClickEventArgs e)
        {
            RemoveResFromGroup(treeListResGroup.FocusedNode.ParentNode);
        }

        private void treeListResGroup_DragLeave(object sender, EventArgs e)
        {
            if (treeListResGroup.FocusedNode.Level == 0) return;

            //if (ef_args.b_info[3].Status != EFButtonKeysStatus.PRE_ACTIVE)
            //{
            //     MessageBox.Show(EP.EPES.EPESC0000169/*操作失败！请进入资源组维护模式操作！*/, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}
            
            //this.//EFMsgInfo = "";

            RemoveResFromGroup(treeListResGroup.FocusedNode.ParentNode);
        }

        private void RemoveResFromGroup(TreeListNode pNode)
        {
            string resID = "";
            string resName = "";
            string resGroupID = pNode.GetValue(treeListColumnRGID).ToString();
            string resGroupName = pNode.GetValue(treeListColumnRGName).ToString();

            EI.EIInfo inblk = new EI.EIInfo();
            inblk.AddColName(1, "resid");
            inblk.AddColName(1, "groupid");
            inblk.AddColName(1, "appname");
            inblk.AddColName(1, "groupname");
            inblk.AddColName(1, "resname");

            for (int i = 0, rowIndex = 0; i < pNode.Nodes.Count; i++)
            {
                if (!pNode.Nodes[i].Selected)
                    continue;

                resID = pNode.Nodes[i].GetValue(treeListColumnRGID).ToString();
                resName = pNode.Nodes[i].GetValue(treeListColumnRGName).ToString();

                inblk.SetColVal(1, rowIndex + 1, "resid", resID);
                inblk.SetColVal(1, rowIndex + 1, "groupid", resGroupID);
                inblk.SetColVal(1, rowIndex + 1, "appname", comboApp.EditValue.ToString().Split(':')[0]);
                inblk.SetColVal(1, rowIndex + 1, "groupname", resGroupName);
                inblk.SetColVal(1, rowIndex + 1, "resname", resName);
                rowIndex++;
            }

            EI.EIInfo outblk = EI.EITuxedo.CallService("epesres_del", inblk);

            if (outblk.sys_info.flag == 0)
            {
                //MessageBox.Show("操作成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //this.//EFMsgInfo = EP.EPES.EPESC0000156/*操作成功！*/;
                //treeListResGroup.FocusedNode = treeListResGroup.FocusedNode.ParentNode;

                treeListResGroup.DeleteSelectedNodes();
                
                //QueryResInGroup(treeListResGroup.FocusedNode);
            }
            else
            {
                MessageBox.Show(outblk.sys_info.msg, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region 拖曳资源到资源组
        private void treeListResGroup_DragEnter(object sender, DragEventArgs e)
        {
            //if (ef_args.b_info[3].Status == EFButtonKeysStatus.PRE_ACTIVE)
            //    e.Effect = DragDropEffects.Copy;
            //else
            //{
            //    e.Effect = DragDropEffects.None;
            //    // MessageBox.Show("操作失败！请进入资源组维护模式操作！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        private void treeListResGroup_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Effect == DragDropEffects.Move)
            {
                e.Effect = DragDropEffects.None;
                return;
            }

            //if (ef_args.b_info[3].Status != EFButtonKeysStatus.PRE_ACTIVE)
            //{
            //    e.Effect = DragDropEffects.None;

            //     MessageBox.Show(EP.EPES.EPESC0000169/*操作失败！请进入资源组维护模式操作！*/, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);

            //    TreeListNode parent = treeListResGroup.FocusedNode.ParentNode;
            //    return;
            //}

            //this.//EFMsgInfo = "";

            DevExpress.XtraTreeList.TreeListHitInfo hi = treeListResGroup.CalcHitInfo(treeListResGroup.PointToClient(new Point(e.X, e.Y)));
            TreeListNode parentNode = null;
            //拖拽至资源组节点
            if (hi.Node.ImageIndex == RESGROUPICON)
            {
                parentNode = hi.Node;
            }
            //拖拽至资源节点
            else
            {
                parentNode = hi.Node.ParentNode;
            }
            object obj = e.Data.GetData(typeof(EI.EIInfo));

            EI.EIInfo inBlock = obj as EI.EIInfo;
            EI.EIInfo outBlock;

            inBlock.AddColName(1, "companycode");

            inBlock.SetColVal(1, 1, "groupid", parentNode.GetValue(treeListColumnRGID).ToString());
            inBlock.SetColVal(1, 1, "groupname", parentNode.GetValue(treeListColumnRGName).ToString());
            inBlock.SetColVal(1, 1, "companycode", this.comboComp.EditValue.ToString().Split(':')[0]);

            outBlock = EI.EITuxedo.CallService("epesres_ins", inBlock);

            if (outBlock.sys_info.flag == 0)
            {
                //QueryResInGroup(parentNode);
                string name = "", fname = "", desc = "", aclid = "";
                string res_type = inBlock.Tables[0].Rows[0]["restype"].ToString();

                for (int i = 0; i < inBlock.Tables[1].Rows.Count; i++)
                {
                    name = inBlock.Tables[1].Rows[i]["resname"].ToString();
                    fname = inBlock.Tables[1].Rows[i]["fname"].ToString();
                    aclid = inBlock.Tables[1].Rows[i]["resid"].ToString();
                    desc = inBlock.Tables[1].Rows[i]["desc"].ToString();

                    TreeListNode node = treeListResGroup.AppendNode(new object[2], parentNode);
                    node.SetValue(treeListColumnRGID, aclid);

                    switch (res_type)
                    {
                        case "form":
                            node.SetValue(treeListColumnRGName, name + "[" + desc + "]");
                            node.ImageIndex = node.SelectImageIndex = FORMICON;
                            break;
                        case "button":
                            node.SetValue(treeListColumnRGName, fname +"/"+name + "[" + desc + "]");
                            node.ImageIndex = node.SelectImageIndex = BUTTICON;
                            break;
                        case "other":
                            node.SetValue(treeListColumnRGName, name + "[" + desc + "]");
                            node.ImageIndex = node.SelectImageIndex = OTHICON;
                            break;
                    }
                }

                UncheckRows();
            }
            else
            {
                 MessageBox.Show(outBlock.sys_info.msg, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //取消列表框中所有checkbox选中状态
        private void UncheckRows()
        {
            for (int k = 0; k < dataSetESOBJ.TESFORMRESINFO.Rows.Count; k++)
            {
                fgDevGridFormInfo.SetSelectedColumnChecked(k, false);
                //this.gridViewFormInfo.SetRowCellValue(k, "selected", false);
            }
            this.gridViewFormInfo.Invalidate();

            for (int k = 0; k < dataSetESOBJ.TESBUTTONRESINFO.Rows.Count; k++)
            {
                fgDevGridButtInfo.SetSelectedColumnChecked(k, false);
                //this.gridViewButtInfo.SetRowCellValue(k, "selected", false);
            }
            this.gridViewButtInfo.Invalidate();

            for (int k = 0; k < dataSetESOBJ.TESOTHERRESINFO.Rows.Count; k++)
            {
                fgDevGridOth.SetSelectedColumnChecked(k, false);
                //this.gridViewOthInfo.SetRowCellValue(k, "selected", false);
            }
            this.gridViewOthInfo.Invalidate();
        }

        private void QueryResInGroup(TreeListNode parentNode)
        {
            parentNode.Nodes.Clear();
            if (parentNode.GetValue(treeListColumnRGID) == null) return;

            EI.EIInfo inblk = new EI.EIInfo();
            inblk.AddColName(1, "groupid");
            inblk.AddColName(1, "appname");
            inblk.SetColVal(1, 1, "groupid", parentNode.GetValue(treeListColumnRGID).ToString());
            inblk.SetColVal(1, 1, "appname", comboApp.EditValue.ToString().Split(':')[0]);

            EI.EIInfo outblk = EI.EITuxedo.CallService("epesres_inq", inblk);

            if (outblk.sys_info.flag < 0)
            {
                 MessageBox.Show(outblk.sys_info.msg, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                    TreeListNode node = treeListResGroup.AppendNode(new object[2], parentNode);
                    node.SetValue(treeListColumnRGName, name + "[" + description + "]");
                    node.SetValue(treeListColumnRGID, aclid);

                    switch(res_type)
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
            }            
        }

        private void treeListResGroup_FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
        {

        }
        private void treeListResGroup_DoubleClick(object sender, EventArgs e)
        {
            TreeListNode node = treeListResGroup.FocusedNode;
            if (node == null) return;

            if (node.Level == 0 && node.Nodes.Count == 0)
            {
                QueryResInGroup(node);
            }
        }

        private void fgDevGridButtInfo_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            if (dataSetESOBJ.TESBUTTONRESINFO.Rows.Count == 0 || fgDevGridButtInfo.EFChoiceCount == 0 /*getChoiceCount(gridViewButtInfo) == 0*/)
            {
                return;
            }

            EI.EIInfo inBlock = new EI.EIInfo();
            inBlock.SetColName(1, 1, "groupid");
            inBlock.SetColName(1, 2, "appname");
            inBlock.SetColName(1, 3, "restype");
            inBlock.SetColName(1, 4, "groupname");
            inBlock.AddNewBlock();
            inBlock.SetColName(2, 1, "resid");
            inBlock.SetColName(2, 2, "resname");
            inBlock.SetColName(2, 3, "fname");
            inBlock.SetColName(2, 4, "desc");

            inBlock.SetColVal(1, 1, "appname", comboApp.EditValue.ToString().Split(':')[0]);
            inBlock.SetColVal(1, 1, "restype", "button");

            for (int i = 0, j = 1; i < this.gridViewButtInfo.RowCount; i++)
            {
                //取出选中行
                if (//gridViewButtInfo.GetRowCellValue(i, "selected") != null
                    //&& gridViewButtInfo.GetRowCellValue(i, "selected").ToString() == "True"
                    fgDevGridButtInfo.GetSelectedColumnChecked(i)
                    )
                {
                    inBlock.SetColVal(2, j, "resid", dataSetESOBJ.TESBUTTONRESINFO.Rows[i]["ACLID"].ToString());
                    inBlock.SetColVal(2, j, "resname", dataSetESOBJ.TESBUTTONRESINFO.Rows[i]["NAME"].ToString());
                    inBlock.SetColVal(2, j, "fname", dataSetESOBJ.TESBUTTONRESINFO.Rows[i]["FNAME"].ToString());
                    inBlock.SetColVal(2, j, "desc", dataSetESOBJ.TESBUTTONRESINFO.Rows[i]["DESCRIPTION"].ToString());
                    j++;
                }
            }

            dragFromGrid = true;

            fgDevGridFormInfo.DoDragDrop(inBlock, DragDropEffects.Copy);
        }

        private void fgDevGridOth_MouseMove(object sender, MouseEventArgs e)
        {
            if (dataSetESOBJ.TESOTHERRESINFO.Rows.Count == 0)
            {
                return;
            }
            if (e.Button != MouseButtons.Left) return;

            if (fgDevGridOth.EFChoiceCount == 0 /*getChoiceCount(gridViewOthInfo) == 0*/)
            {
                return;
            }

            EI.EIInfo inBlock = new EI.EIInfo();
            inBlock.SetColName(1, 1, "groupid");
            inBlock.SetColName(1, 2, "appname");
            inBlock.SetColName(1, 3, "restype");
            inBlock.SetColName(1, 4, "groupname");
            inBlock.AddNewBlock();
            inBlock.SetColName(2, 1, "resid");
            inBlock.SetColName(2, 2, "resname");
            inBlock.SetColName(2, 3, "fname");
            inBlock.SetColName(2, 4, "desc");

            inBlock.SetColVal(1, 1, "appname", comboApp.EditValue.ToString().Split(':')[0]);
            inBlock.SetColVal(1, 1, "restype", "other");

            for (int i = 0, j = 1; i < this.gridViewOthInfo.RowCount; i++)
            {
                //取出选中行
                if (fgDevGridOth.GetSelectedColumnChecked(i))
                {
                    inBlock.SetColVal(2, j, "resid", dataSetESOBJ.TESOTHERRESINFO.Rows[i]["ACLID"].ToString());
                    inBlock.SetColVal(2, j, "resname", dataSetESOBJ.TESOTHERRESINFO.Rows[i]["NAME"].ToString());
                    inBlock.SetColVal(2, j, "fname", " ");
                    inBlock.SetColVal(2, j, "desc", dataSetESOBJ.TESOTHERRESINFO.Rows[i]["DESCRIPTION"].ToString());
                    j++;
                }
            }

            dragFromGrid = true;

            fgDevGridOth.DoDragDrop(inBlock, DragDropEffects.Copy);
        }
        #endregion

        #region 双击资源显示其所属资源组
        private void fgDevGridFormInfo_DoubleClick(object sender, EventArgs e)
        {
            if (EPESCommon.AuthMode == AUTHMODE.MODE_9672
                && gridViewFormInfo.FocusedRowHandle >= 0 
                && xtraTabControlTree_ResGroup.SelectedTabPage == xtraTabPageResGroup)
            {
                object formACLID = gridViewFormInfo.GetRowCellValue(gridViewFormInfo.FocusedRowHandle, "ACLID");
                object formNAME = gridViewFormInfo.GetRowCellValue(gridViewFormInfo.FocusedRowHandle, "NAME");
                if (formACLID != null)
                {
                    QryResInWhichGroup(formNAME.ToString(), formACLID.ToString());
                }
            }
        }

        private void fgDevGridButtInfo_DoubleClick(object sender, EventArgs e)
        {
            if (EPESCommon.AuthMode == AUTHMODE.MODE_9672          
                && gridViewButtInfo.FocusedRowHandle >= 0 
                && xtraTabControlTree_ResGroup.SelectedTabPage == xtraTabPageResGroup)
            {
                object buttACLID = gridViewButtInfo.GetRowCellValue(gridViewButtInfo.FocusedRowHandle, "ACLID");
                object buttNAME = gridViewButtInfo.GetRowCellValue(gridViewButtInfo.FocusedRowHandle, "NAME");
                object buttFNAME = gridViewButtInfo.GetRowCellValue(gridViewButtInfo.FocusedRowHandle, "FNAME");
                if (buttACLID != null)
                {
                    QryResInWhichGroup(string.Format("{0}/{1}", buttFNAME.ToString(), buttNAME.ToString()), buttACLID.ToString());
                }
            }
        }

        private void fgDevGridOth_DoubleClick(object sender, EventArgs e)
        {
            if (EPESCommon.AuthMode == AUTHMODE.MODE_9672
                && gridViewOthInfo.FocusedRowHandle >= 0 
                && xtraTabControlTree_ResGroup.SelectedTabPage == xtraTabPageResGroup)
            {
                object OthACLID = gridViewOthInfo.GetRowCellValue(gridViewOthInfo.FocusedRowHandle, "ACLID");
                object OthNAME = gridViewOthInfo.GetRowCellValue(gridViewOthInfo.FocusedRowHandle, "NAME");
                if (OthACLID != null)
                {
                    QryResInWhichGroup(OthNAME.ToString(), OthACLID.ToString());
                }
            }
        }

        /// <summary>
        /// 双击资源高亮所属的资源组
        /// </summary>
        /// <param name="resID"></param>
        private void QryResInWhichGroup(string resNAME, string resID)
        {
            EI.EIInfo inblk = new EI.EIInfo();
            inblk.AddColName(1, "resid");
            inblk.AddColName(1, "appname");
            inblk.AddColName(1, "companycode");

            string companyCode = this.comboComp.EditValue.ToString().Split(':')[0];
            string company = "["+this.comboComp.EditValue.ToString()+"]";
            inblk.SetColVal(1, 1, "resid", resID);
            inblk.SetColVal(1, 1, "appname", comboApp.EditValue.ToString().Split(':')[0]);
            inblk.SetColVal(1, 1, "companycode", companyCode);

            EI.EIInfo outblk = EI.EITuxedo.CallService("epesresin_inq", inblk);

            if (outblk.sys_info.flag == 0)
            {
                if (outblk.Tables[0].Rows.Count == 0)
                {
                    string msgNoGroup = string.Format(EP.EPES.EPESC0000185/*在账套{0}下，资源{1}未分配到任何资源组*/, company, resNAME);
                     MessageBox.Show(msgNoGroup, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //this.//EFMsgInfo = msgNoGroup;
                    return;
                }

                string id = outblk.Tables[0].Rows[0]["id"].ToString();
                string name = outblk.Tables[0].Rows[0]["name"].ToString();
                string desc = outblk.Tables[0].Rows[0]["description"].ToString();
                string resGroup = string.Format("{0}[{1}]", desc, name);

                string msgGroup = string.Format(EP.EPES.EPESC0000186/*在账套{0}下，资源{1}已经分配到资源组: {2}*/, company, resNAME, resGroup);
                 MessageBox.Show(msgGroup, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //this.//EFMsgInfo = msgGroup;
                //foreach (TreeListNode node in treeListResGroup.Nodes)
                //{
                //    if (node.Level == 0)
                //    {
                //        if (node.GetValue(treeListColumnRGID).ToString() == id)
                //        {
                //            treeListResGroup.FocusedNode = node;
                //            break;
                //        }
                //    }
                //}
            }
            else
            {
                MessageBox.Show(outblk.sys_info.msg, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion


        private void fgDevComboBoxEdit1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.treeList.Nodes.Clear();
            dataSetESOBJ.TESFORMRESINFO.Rows.Clear();
            dataSetESOBJ.TESBUTTONRESINFO.Rows.Clear();
        }

        private void fgDevGridFormInfo_EF_GridBar_AddRow_Event(object sender, NavigatorButtonClickEventArgs e)
        {
            gridViewFormInfo.ClearSorting();
            gridViewFormInfo.AddNewRow();
            gridViewFormInfo.RefreshData();
            gridViewFormInfo.FocusedRowHandle = gridViewFormInfo.RowCount - 1;
        }

        private void fgDevGridOth_EF_GridBar_AddRow_Event(object sender, NavigatorButtonClickEventArgs e)
        {
            gridViewOthInfo.ClearSorting();
            gridViewOthInfo.AddNewRow();
            gridViewOthInfo.RefreshData();
            gridViewOthInfo.FocusedRowHandle = gridViewOthInfo.RowCount - 1;
        }

        private void btnRfgresh_Click(object sender, EventArgs e)
        {
            QueryResGroup();
        }

        private void chkAuth_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAuth.Checked)
            {
                chkTree.CheckState = CheckState.Unchecked;
                toolTipController1.ShowHint(EP.EPES.EPESC0000181/*勾选后将查询未分配到资源组的画面/按钮。\n支持画面名模糊匹配查询。*/, MousePosition);
            }
        }

        private void chkTree_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTree.Checked)
            {
                chkAuth.CheckState = CheckState.Unchecked;
                toolTipController1.ShowHint(EP.EPES.EPESC0000182/*勾选后将查询未挂到菜单树上的画面。\n支持画面名模糊匹配查询。*/, MousePosition);
            }
        }

        private void chkOth_CheckedChanged(object sender, EventArgs e)
        {
            if (chkOth.Checked)
            {
                toolTipController1.ShowHint(EP.EPES.EPESC0000183/*勾选后将查询未分配到资源组的细部资源。\n支持资源名模糊匹配查询。*/, MousePosition);
            }
        }

        private void comboComp_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.treeListResGroup.Nodes.Clear();
        }



        #region 菜单、画面、细部资源导出、导入

        /// <summary>
        /// 取消菜单导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormEPESOBJ_EF_CANCEL_DO_F7(object sender, EventArgs e)
        {
            this.treeList.OptionsView.ShowCheckBoxes = false;

            foreach (TreeListNode node in treeList.Nodes)
            {
                node.CheckState = CheckState.Unchecked;

                CheckSubNodes(node, CheckState.Unchecked);
            }
        }

        /// <summary>
        /// 菜单预导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormEPESOBJ_EF_PRE_DO_F7(object sender, EventArgs e)
        {
            this.treeList.OptionsView.ShowCheckBoxes = true;
        }







 

        private void GetCheckedNode(TreeListNodes nodes, DataTable dt)
        {
            foreach (TreeListNode node in nodes)
            {
                if (node.Checked)
                {
                    dt.Rows.Add(node.Tag.ToString());
                }
                if (node.Nodes.Count != 0)
                {
                    GetCheckedNode(node.Nodes, dt);
                }
            }
        }

        private void treeList_BeforeCheckNode(object sender, CheckNodeEventArgs e)
        {
            e.State = (e.PrevState == CheckState.Checked ? CheckState.Unchecked : CheckState.Checked);

            if (e.Node.ImageIndex != 2)//folder
            {
                CheckSubNodes(e.Node, e.State);
            }

            if (e.State == CheckState.Checked)
            {
                CheckParentNodes(e.Node.ParentNode);
            }
        }

        private void CheckSubNodes(TreeListNode parentNode, CheckState state)
        {
            foreach (TreeListNode node in parentNode.Nodes)
            {
                node.CheckState = state;

                if (node.Nodes.Count != 0)
                {
                    CheckSubNodes(node, state);
                }
            }
        }

        private void CheckParentNodes(TreeListNode parentNode)
        {
            if (parentNode != null)
            {
                parentNode.CheckState = CheckState.Checked;

                CheckParentNodes(parentNode.ParentNode);
            }
        }

        #endregion

        private void fgButtonQueryTree_Click(object sender, EventArgs e)
        {
            try
            {
                LoadTree();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
          
        }



    }
}
