using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid.Menu;
using DevExpress.Utils.Menu;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Registrator;
using DevExpress.XtraGrid.Columns;
using System.IO;
namespace EF
{
    /// <summary>
    /// 继承自DevXpress的GridControl. GridControl自带的方法均可使用,为了项目级风格一致,
    /// 以及编程中的方便性,特封装以下属性   :
    ///      PageSize               每页大小(目前默认为20)
    ///      TotalRecordCount       总记录数
    ///      RecordCountMessage  	分页信息
    ///      InitRowOrdinal        	起始行标(在gridview1前方默认有行标号，默认从1 开始，当修改该值时，将从该值开始)
    ///      ShowRowIndicator       是否显示行标(为false时,第一列不显示行序号)
    ///      ShowSelectionColumn	是否显示选择列，设为true时，运行时会增加一个选择列 
    ///      EFMultiSelect          选择列是否允许多行选中
    ///      AllowDragRow           是否允许行拖动，设为true时，可以拖动行调整行顺序。
    ///      ShowContextMenu        是否使用弹出菜单，设为ture时，使用内定的弹出菜单。
    ///      IsUseCustomPageBar     是否使用下方自定义按钮. 设为true时，使用自定义按钮  
    ///      CheckByteLength        单元格验证长度时,检查字节长度
    ///      EFChoiceCount          选择列,勾选行的条数
    ///      SelectionColumn        选择列的列对象
    ///      ------------------------------------------------------------------------------------------------
    ///      当ShowContextMenu      设为true时，以下属性才有效，有关弹出菜单的。
    ///      ----------------------有关弹出菜单的------------------------------------------
    ///      ContextMenuAddCopyNewEnable	弹出菜单中[复制新增]，是否可用。
    ///      ContextMenuAddNewEnable		弹出菜单中[新增]，是否可用。
    ///      ContextMenuChooseAllEnable		弹出菜单中[选择所有]，是否可用。 
    ///      ContextMenuChooseEnable		弹出菜单中[选择]，是否可用。
    ///      ContextMenuSaveAsEnable		弹出菜单中[另存为]，是否可用。
    ///      ContextMenuUnChooseAllEnable	弹出菜单中[全部不选]，是否可用。
    ///      ContextMenuUnChooseEnable		弹出菜单中[不选择]，是否可用。
    ///      ShowContextMenuAddCopyNew		弹出菜单中[复制新增]，是否显示
    ///      ShowContextMenuAddNew			弹出菜单中[新增]，是否显示
    ///      ShowContextMenuChoose			弹出菜单中[选择]，是否显示
    ///      ShowContextMenuChooseAll		弹出菜单中[选择所有]，是否显示
    ///      ShowContextMenuSaveAs			弹出菜单中[另存为]，是否显示
    ///      ShowContextMenuUnChoose		弹出菜单中[不选择]，是否显示
    ///      ShowContextMenuUnChooseAll		弹出菜单中[全部不选]，是否显示
    ///      ------------------------------------------------------------------------------------------------
    ///      当使用自定义按钮时，自定义的按钮包括(翻页的四个按钮，新增，复制新增，删除，导出，显示行过滤，
    ///      显示分组，刷新，保存配置 ，跳转到按钮.以及分页信息显示框..都有对应的默认实现，以及是否可用，
    ///      是否可见属性。如下  IsUseCustomPageBar 设为true时有效
    ///      ---------------------------------------------有关EFDevGrid下方自定义按钮的------------
    ///      FirstPageButtonEnable		第一页是否可用
    ///      LastPageButtonEnable		最后一页是否可用
    ///      NextPageButtonEnable		下一页是否可用
    ///      PrePageButtonEnable		上一页是否可用
    ///      ShowAddCopyRowButton		是否显示复制新增一行按钮
    ///      ShowAddRowButton			是否显示新增一行按钮
    ///      ShowDeleteRowButton		是否显示删除一行按钮
    ///      ShowExportButton			是否显示导出按钮
    ///      ShowFilterButton			是否显示过滤行按钮
    ///      ShowGroupButton			是否显示按列分组按钮
    ///      ShowPageButton				是否显示翻页按钮
    ///      ShowPageToButton			是否显示跳转到按钮
    ///      ShowRecordCountMessage		是否显示记录条数信息按钮
    ///      ShowRefreshButton			是否显示刷新按钮
    ///      ShowSaveLayoutButton		是否显示保存布局按钮
    ///      CanConfigGridCaption		是否可以配置列标题
    ///      ---------------------------------------------------------------------------------------------------------
    ///如果要自定义按钮点击事件，可以在EFDevGrid的事件中找到相应事件，双击事件重写事件实现，将不调用默认实现。
    ///     EF_GridBar_AddCopyRow_Event 分页条中复制新增按钮点击事件
    ///     EF_GridBar_AddRow_Event     分页条中新增按钮点击事件
    ///     EF_GridBar_Fisrt_Event      分页条中第一条点击事件
    ///     EF_GridBar_Last_Event       分页条中最后一条点击事件
    ///     EF_GridBar_NextPage_Event   分页条中下一页点击事件
    ///     EF_GridBar_PageTo_Event     分页条中删除一行点击事件
    ///     EF_GridBar_PrePage_Event    分页条中上一页点击事件
    ///     EF_GridBar_Refresh_Event    分页条中刷新点击事件				
    ///     EF_GridBar_Remove_Event     分页条中删除一行点击事件
    ///     EF_GridBar_SaveLayout_Event 分页条中保存布局按钮点击事件		

    /// </summary>
    public partial class EFDevGrid : DevExpress.XtraGrid.GridControl
    {
        #region 构造方法
        /// <summary>
        /// 构造方法
        /// </summary>
        public EFDevGrid()
        {
            InitializeComponent();
            //创建默认gridView
            //CreateDefaultView();
            //设置默认属性
            SetDefaultProperty();
            //为换肤添加事件
            //EFApperance.Default.SkinChanged += new EventHandler(Default_SkinChanged);
        }

        private void SetDefaultProperty()
        {
            //加载事件
            this.Load += new EventHandler(EFDevGrid_Load);
            ////添加选择列
            colSelectedTemp = getCheckOptionColumn();
            if (this.MainView is DevExpress.XtraGrid.Views.Grid.GridView)
            {
                ((DevExpress.XtraGrid.Views.Grid.GridView)this.MainView).OptionsView.ColumnAutoWidth = false;
                //不自动调整列宽.
                ((DevExpress.XtraGrid.Views.Grid.GridView)this.MainView).OptionsView.ColumnAutoWidth = false;
                //使用交叉色.
                ((DevExpress.XtraGrid.Views.Grid.GridView)this.MainView).OptionsView.EnableAppearanceEvenRow = true;
                ((DevExpress.XtraGrid.Views.Grid.GridView)this.MainView).OptionsView.EnableAppearanceOddRow = true;
                //不显示汇总栏panel
                ((DevExpress.XtraGrid.Views.Grid.GridView)this.MainView).OptionsView.ShowGroupPanel = false;
                //不显示过滤行
                ((DevExpress.XtraGrid.Views.Grid.GridView)this.MainView).OptionsView.ShowAutoFilterRow = false;
                //为行添加行号
                ((DevExpress.XtraGrid.Views.Grid.GridView)this.MainView).IndicatorWidth = 35; //默认显示行号的宽度
                ((DevExpress.XtraGrid.Views.Grid.GridView)this.MainView).CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.gridView1_CustomDrawRowIndicator);
            }
		}
        private int initRowOrdinal = 1;

        /// <summary>
        /// 行号初始值.默认为1
        /// </summary>
        [Description("行号初始值.默认为1")]
        [Category("EF")]
        [DefaultValue(1)]
        public int InitRowOrdinal
        {
            get { return initRowOrdinal; }
            set { initRowOrdinal = value; }
        }

        private int totalRecordCount = 1;
        /// <summary>
        /// 总记录.默认为1
        /// </summary>
        [Description("总记录.默认为1")]
        [Category("EF")]
        [DefaultValue(1)]
        public int TotalRecordCount
        {
            get { return totalRecordCount; }
            set { totalRecordCount = value; }
        }

        #endregion

        #region 检查字节长度
        private bool checkByteLength = true;
        /// <summary>
        /// 单元格是TextEdit验证长度时,检查字节长度
        /// </summary>
        [Description("单元格验证长度时,检查字节长度")]
        [Category("EF")]
        [DefaultValue(true)]
        public bool CheckByteLength
        {
            get { return checkByteLength; }
            set
            {
                checkByteLength = value;
                if (value)
                {
                    this.MainView.ValidatingEditor -= new DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventHandler(MainView_ValidatingEditor);
                    this.MainView.ValidatingEditor += new DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventHandler(MainView_ValidatingEditor);
                }
                else
                {
                    this.MainView.ValidatingEditor -= new DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventHandler(MainView_ValidatingEditor);
                }
            }
        }

        private void MainView_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            if (!CheckByteLength)
                return;
           // GridView gridView1 = gridView1;
            if (gridView1 == null)
                return;
            int lenMax = 0;
            if (gridView1.FocusedColumn.ColumnEdit is DevExpress.XtraEditors.Repository.RepositoryItemTextEdit)
            {
                DevExpress.XtraEditors.Repository.RepositoryItemTextEdit textEdit = gridView1.FocusedColumn.ColumnEdit as DevExpress.XtraEditors.Repository.RepositoryItemTextEdit;
                 lenMax = textEdit.MaxLength;
            }
            if (gridView1.FocusedColumn.ColumnEdit is DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit)
            {
                DevExpress.XtraEditors.Repository.RepositoryItemTextEdit textEdit = gridView1.FocusedColumn.ColumnEdit as DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit;
                lenMax = textEdit.MaxLength;
            }
            if (lenMax == 0)
                return;
            int lenNow = EF.Utility.GetByteLength(e.Value.ToString()); 
            if (lenNow > lenMax)
            {
                e.ErrorText = string.Format("最大长度为:{0},当前长度为:{1}", lenMax, lenNow);
                e.Valid = false;
            }
        }
        #endregion

        #region  设置所有列的可编辑性
        /// <summary>
        /// 否决的用(SetAllColumnEditable)
        /// </summary>
        [Obsolete("use  SetAllColumnEditable(bool editable)")]
        public void SetAllColumnEditAble(bool editable)
        {
            if (this.MainView is GridView)
            {
                foreach (DevExpress.XtraGrid.Columns.GridColumn gridCol in ((DevExpress.XtraGrid.Views.Grid.GridView)this.MainView).Columns)
                {
                    gridCol.OptionsColumn.AllowEdit = editable;
                    //gridCol.OptionsColumn.ReadOnly = !editable;
                }
            }
        }
       
        /// <summary>
        /// 使用ShowSelectionColumn时,选择列的FiledName值
        /// </summary>
        public static string SelectionColumnFieldName = "54EA8C67-D921-4ddc-8ECE-114E2459816C";
        /// <summary>
        /// 设置除选择列外其他各列的可编辑性[OptionsColumn.AllowEdit属性]
        /// </summary>
        /// <param name="editable">是否可编辑</param>
        public void SetAllColumnEditableWithoutSelection(bool editable)
        {
            if (gridView1 != null)
            {
                if (ShowSelectedColumn)
                {
                    foreach (GridColumn gridCol in gridView1.Columns)
                    {
                        if (gridCol.FieldName != this.selectedColumnFieldName)
                        {
                            gridCol.OptionsColumn.AllowEdit = editable;
                        }
                    }
                }
                else if (ShowSelectionColumn)
                {
                    foreach (GridColumn gridCol in gridView1.Columns)
                    {

                        if (gridCol.FieldName != SelectionOnRunTime.SelectionFiledName)
                        {
                            gridCol.OptionsColumn.AllowEdit = editable;
                        }
                    }
                }
                else
                {
                    SetAllColumnEditable(editable);
                }
            }
        }
        
        /// <summary>
        /// 设置所有列的可编辑性[OptionsColumn.AllowEdit属性](设置前提是gridview整体可编辑)
        /// </summary>
        /// <param name="editable">是否可编辑</param>
        public void SetAllColumnEditable(bool editable)
        {
            if (this.MainView is GridView)
            {
                foreach (DevExpress.XtraGrid.Columns.GridColumn gridCol in ((DevExpress.XtraGrid.Views.Grid.GridView)this.MainView).Columns)
                {
                     gridCol.OptionsColumn.AllowEdit = editable;
                }
            }
        }

        /// <summary>
        /// 设置所有列的OptionsColumn.ReadOnly属性为readOnly,选择列除外.
        /// </summary>
        /// <param name="readOnly"></param>
        public void SetAllColumnReadOnlyWithoutSelection(bool readOnly)
        {
            if (gridView1 != null)
            {
                if (ShowSelectedColumn)
                {
                    foreach (GridColumn gridCol in gridView1.Columns)
                    {
                        if (gridCol.FieldName != this.selectedColumnFieldName)
                        {
                            gridCol.OptionsColumn.ReadOnly = readOnly;
                        }
                    }
                }
                else if (ShowSelectionColumn)
                {
                    foreach (GridColumn gridCol in gridView1.Columns)
                    {
                        if (gridCol.FieldName != SelectionOnRunTime.SelectionFiledName)
                        {
                            gridCol.OptionsColumn.ReadOnly = readOnly;
                        }
                    }
                }
                else
                {
                    SetAllColumnReadOnly(readOnly);
                }
            }
        }
        /// <summary>
        /// 设置所有列的OptionsColumn.ReadOnly 属性为readOnly
        /// </summary>
        /// <param name="readOnly"></param>
        public void SetAllColumnReadOnly(bool readOnly)
        {
            if (gridView1 != null)
            {
                foreach (GridColumn gridCol in gridView1.Columns)
                {
                    gridCol.OptionsColumn.ReadOnly = readOnly;
                }
            }
        }
        #endregion

        #region 选择列是否允许多选
        private bool eFMultiSelect = true;
        /// <summary>
        /// 选择列是否允许多选
        /// </summary>
        [Description("选择列是否允许多选")]
        [Category("EF")]
        [DefaultValue(true)]
        public bool EFMultiSelect
        {
            get { return eFMultiSelect; }
            set
            {
                eFMultiSelect = value;
                if (selection != null)
                {
                    selection.EFMultiSelect = value;
                }
            }
        }
        #endregion

        #region 除了选择列,其他列不可编辑
        private bool readOnly = false;

        //public bool ReadOnly
        //{
        //    get { return readOnly; }
        //    set { readOnly = value; }
        //}

        #endregion

        #region 弹出菜单,是否显示,是否可用
        /// <summary>
        /// 弹出菜单,是否显示
        /// </summary>
        [Description("弹出菜单,是否显示")]
        [Category("EF")]
        [DefaultValue(false)]
        public bool ShowContextMenu
        {
            get { return showContextMenu; }
            set
            {
                showContextMenu = value;
            }
        }
        private bool showContextMenu = false;

        /// <summary>
        /// 新增一行时,是否自动选中
        /// </summary>
        [Description("新增一行时,是否自动选中")]
        [Category("EF")]
        [DefaultValue(false)]
        public bool AutoSelectNewRow
        {
            get { return autoSelectNewRow; }
            set
            {
                autoSelectNewRow = value;
            }
        }
        private bool autoSelectNewRow = false;
        /// <summary>
        /// 弹出菜单中[选择],是否显示
        /// </summary>
        [Description("弹出菜单中[选择],是否显示")]
        [Category("EF弹出菜单相关")]
        [DefaultValue(true)]
        public bool ShowContextMenuChoose
        {
            get { return showContextMenuChoose; }
            set
            {
                showContextMenuChoose = value;
                if (popMenu != null)
                    popMenu.RefreshPopupMenuStrip(popUpMenuStripName.choose, ContextMenuChooseEnable, value);
            }
        }
        private bool showContextMenuChoose = true;

        /// <summary>
        /// 弹出菜单中[选择],是否可用
        /// </summary>
        [Description("弹出菜单中[选择],是否可用")]
        [Category("EF弹出菜单相关")]
        [DefaultValue(true)]
        public bool ContextMenuChooseEnable
        {
            get { return contextMenuChooseEnable; }
            set
            {
                contextMenuChooseEnable = value;
                if (popMenu != null)
                    popMenu.RefreshPopupMenuStrip(popUpMenuStripName.choose, value, ShowContextMenuChoose);
            }
        }
        private bool contextMenuChooseEnable = true;

        /// <summary>
        /// 弹出菜单中[不选择],是否显示
        /// </summary>
        [Description("弹出菜单中[不选择],是否显示")]
        [Category("EF弹出菜单相关")]
        [DefaultValue(true)]
        public bool ShowContextMenuUnChoose
        {
            get { return showContextMenuUnChoose; }
            set
            {
                showContextMenuUnChoose = value;
                if (popMenu != null)
                    popMenu.RefreshPopupMenuStrip(popUpMenuStripName.unChoose, ContextMenuUnChooseEnable, value);
            }
        }
        private bool showContextMenuUnChoose = true;

        /// <summary>
        /// 弹出菜单中[不选择],是否可用
        /// </summary>
        [Description("弹出菜单中[不选择],是否可用")]
        [Category("EF弹出菜单相关")]
        [DefaultValue(true)]
        public bool ContextMenuUnChooseEnable
        {
            get { return contextMenuUnChooseEnable; }
            set
            {
                contextMenuUnChooseEnable = value;
                if (popMenu != null)
                    popMenu.RefreshPopupMenuStrip(popUpMenuStripName.unChoose, value, ShowContextMenuUnChoose);
            }
        }
        private bool contextMenuUnChooseEnable = true;

        /// <summary>
        /// 弹出菜单中[选择所有],是否显示
        /// </summary>
        [Description("弹出菜单中[选择所有],是否显示")]
        [Category("EF弹出菜单相关")]
        [DefaultValue(true)]
        public bool ShowContextMenuChooseAll
        {
            get { return showContextMenuChooseAll; }
            set
            {
                showContextMenuChooseAll = value;
                if (popMenu != null)
                    popMenu.RefreshPopupMenuStrip(popUpMenuStripName.chooseAll, ContextMenuChooseAllEnable, value);
            }
        }
        private bool showContextMenuChooseAll = true;

        /// <summary>
        /// 弹出菜单中[选择所有],是否可用
        /// </summary>
        [Description("弹出菜单中[选择所有],是否可用")]
        [Category("EF弹出菜单相关")]
        [DefaultValue(true)]
        public bool ContextMenuChooseAllEnable
        {
            get { return contextMenuChooseAllEnable; }
            set
            {
                contextMenuChooseAllEnable = value;
                if (popMenu != null)
                    popMenu.RefreshPopupMenuStrip(popUpMenuStripName.chooseAll, value, ShowContextMenuChooseAll);
            }
        }
        private bool contextMenuChooseAllEnable = true;

        /// <summary>
        /// 弹出菜单中[全部不选],是否显示
        /// </summary>
        [Description("弹出菜单中[全部不选],是否显示")]
        [Category("EF弹出菜单相关")]
        [DefaultValue(true)]
        public bool ShowContextMenuUnChooseAll
        {
            get { return showContextMenuUnChooseAll; }
            set
            {
                showContextMenuUnChooseAll = value;
                if (popMenu != null)
                    popMenu.RefreshPopupMenuStrip(popUpMenuStripName.unChooseAll, ContextMenuUnChooseAllEnable, value);
            }
        }
        private bool showContextMenuUnChooseAll = true;

        /// <summary>
        /// 弹出菜单中[全部不选],是否可用
        /// </summary>
        [Description("弹出菜单中[全部不选],是否可用")]
        [Category("EF弹出菜单相关")]
        [DefaultValue(true)]
        public bool ContextMenuUnChooseAllEnable
        {
            get { return contextMenuUnChooseAllEnable; }
            set
            {
                contextMenuUnChooseAllEnable = value;
                if (popMenu != null)
                    popMenu.RefreshPopupMenuStrip(popUpMenuStripName.unChooseAll, value, ShowContextMenuUnChooseAll);
            }
        }
        private bool contextMenuUnChooseAllEnable = true;

        /// <summary>
        /// 弹出菜单中[新增],是否显示
        /// </summary>
        [Description("弹出菜单中[新增],是否显示")]
        [Category("EF弹出菜单相关")]
        [DefaultValue(true)]
        public bool ShowContextMenuAddNew
        {
            get { return showContextMenuAddNew; }
            set
            {
                showContextMenuAddNew = value;
                if (popMenu != null)
                    popMenu.RefreshPopupMenuStrip(popUpMenuStripName.addNew, ContextMenuAddNewEnable, value);
            }
        }
        private bool showContextMenuAddNew = true;


        /// <summary>
        /// 弹出菜单中[新增],是否显示
        /// </summary>
        [Description("弹出菜单中[设置列停靠],是否显示")]
        [Category("EF弹出菜单相关")]
        [DefaultValue(true)]
        public bool ShowContextMenuSetColumnFixed
        {
            get { return showContextMenuSetColumnFixed; }
            set
            {
                showContextMenuSetColumnFixed = value;
                if (popMenu != null)
                    popMenu.RefreshPopupMenuStrip(popUpMenuStripName.setColumnFixed, true, value);
            }
        }
        private bool showContextMenuSetColumnFixed = true;

        /// <summary>
        /// 弹出菜单中[新增],是否可用
        /// </summary>
        [Description("弹出菜单中[新增],是否可用")]
        [Category("EF弹出菜单相关")]
        [DefaultValue(true)]
        public bool ContextMenuAddNewEnable
        {
            get { return contextMenuAddNewEnable; }
            set
            {
                contextMenuAddNewEnable = value;
                if (popMenu != null)
                    popMenu.RefreshPopupMenuStrip(popUpMenuStripName.addNew, value, ShowContextMenuAddNew);
            }
        }
        private bool contextMenuAddNewEnable = true;

        /// <summary>
        /// 弹出菜单中[复制新增],是否显示
        /// </summary>
        [Description("弹出菜单中[复制新增],是否显示")]
        [Category("EF弹出菜单相关")]
        [DefaultValue(true)]
        public bool ShowContextMenuAddCopyNew
        {
            get { return showContextMenuAddCopyNew; }
            set
            {
                showContextMenuAddCopyNew = value;
                if (popMenu != null)
                    popMenu.RefreshPopupMenuStrip(popUpMenuStripName.addCopyNew, ContextMenuAddCopyNewEnable, value);
            }
        }
        private bool showContextMenuAddCopyNew = true;

        /// <summary>
        /// 弹出菜单中[复制新增],是否可用
        /// </summary>
        [Description("弹出菜单中[复制新增],是否可用")]
        [Category("EF弹出菜单相关")]
        [DefaultValue(true)]
        public bool ContextMenuAddCopyNewEnable
        {
            get { return contextMenuAddCopyNewEnable; }
            set
            {
                contextMenuAddCopyNewEnable = value;
                if (popMenu != null)
                    popMenu.RefreshPopupMenuStrip(popUpMenuStripName.addCopyNew, value, ShowContextMenuAddCopyNew);
            }
        }
        private bool contextMenuAddCopyNewEnable = true;

        /// <summary>
        /// 弹出菜单中[另存为],是否显示
        /// </summary>
        [Description("弹出菜单中[另存为],是否显示")]
        [Category("EF弹出菜单相关")]
        [DefaultValue(true)]
        public bool ShowContextMenuSaveAs
        {
            get { return showContextMenuSaveAs; }
            set
            {
                showContextMenuSaveAs = value;
                if (popMenu != null)
                    popMenu.RefreshPopupMenuStrip(popUpMenuStripName.saveAs, ContextMenuSaveAsEnable, value);
            }
        }
        private bool showContextMenuSaveAs = true;

        /// <summary>
        /// 弹出菜单中[另存为],是否可用
        /// </summary>
        [Description("弹出菜单中[另存为],是否可用")]
        [Category("EF弹出菜单相关")]
        [DefaultValue(true)]
        public bool ContextMenuSaveAsEnable
        {
            get { return contextMenuSaveAsEnable; }
            set
            {
                contextMenuSaveAsEnable = value;
                if (popMenu != null)
                    popMenu.RefreshPopupMenuStrip(popUpMenuStripName.saveAs, value, ShowContextMenuSaveAs);
            }
        }
        private bool contextMenuSaveAsEnable = true;


        private void DoShowContextMenu(DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo hi, MouseEventArgs e)
        {
            // Create the menu.
            // Check whether the header panel button has been clicked.
            if (hi.HitTest == DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitTest.RowCell
                || hi.HitTest == DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitTest.RowIndicator
                || hi.HitTest == DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitTest.Row
                || hi.HitTest == GridHitTest.EmptyRow)
            {
                if (showContextMenu)
                {
                    this.MouseDown -= new MouseEventHandler(EFDevGrid_MouseDown);
                    this.OnMouseDown(e);
                    this.MouseDown += new MouseEventHandler(EFDevGrid_MouseDown);
                    this.InitCustomMenuState(this.MainView as DevExpress.XtraGrid.Views.Grid.GridView);
                    popMenu.Show(hi.HitPoint);
                     
                }
            }
        }
        private GridViewCustomMenu popMenu;
        private void InitCustomMenuState(GridView gridView1)
        {
            if (popMenu == null)
            {
                InitCustomMenu(gridView1);
            }
            for (int i = 0; i < popMenu.Items.Count; i++)
            {
                if (popMenu.Items[i].Tag is GridViewCustomMenu.MenuInfo)
                {
                    GridViewCustomMenu.MenuInfo temp = popMenu.Items[i].Tag as GridViewCustomMenu.MenuInfo;
                    if (temp.MenuItemName == popUpMenuStripName.choose)
                    {
                        temp.Visible = ShowContextMenuChoose;
                        temp.Enable = ContextMenuChooseEnable;
                    }
                    if (temp.MenuItemName == popUpMenuStripName.unChoose)
                    {
                        temp.Visible = ShowContextMenuUnChoose;
                        temp.Enable = ContextMenuUnChooseEnable;
                    }
                    if (temp.MenuItemName == popUpMenuStripName.chooseAll)
                    {
                        temp.Visible = ShowContextMenuChooseAll;
                        temp.Enable = ContextMenuChooseAllEnable;
                    }
                    if (temp.MenuItemName == popUpMenuStripName.unChooseAll)
                    {
                        temp.Visible = ShowContextMenuUnChooseAll;
                        temp.Enable = ContextMenuUnChooseAllEnable;
                    }
                    if (temp.MenuItemName == popUpMenuStripName.addNew)
                    {
                        temp.Visible = ShowContextMenuAddNew;
                        temp.Enable = ContextMenuAddNewEnable;
                    }
                    if (temp.MenuItemName == popUpMenuStripName.addCopyNew)
                    {
                        temp.Visible = ShowContextMenuAddCopyNew;
                        temp.Enable = ContextMenuAddCopyNewEnable;
                    }
                    if (temp.MenuItemName == popUpMenuStripName.saveAs)
                    {
                        temp.Visible = ShowContextMenuSaveAs;
                        temp.Enable = ContextMenuSaveAsEnable;
                    }
                    if (temp.MenuItemName == popUpMenuStripName.copySelectData)
                    {
                        temp.Visible = true;
                        temp.Enable = true;
                    }
                    if (temp.MenuItemName == popUpMenuStripName.copySelectRows)
                    {
                        temp.Visible = (gridView1.OptionsSelection.MultiSelect && (gridView1.OptionsSelection.MultiSelectMode == GridMultiSelectMode.CellSelect));
                        temp.Enable = true;
                    }
                    if (temp.MenuItemName == popUpMenuStripName.copySelectCols)
                    {
                        temp.Visible = (gridView1.OptionsSelection.MultiSelect && (gridView1.OptionsSelection.MultiSelectMode == GridMultiSelectMode.CellSelect));
                        temp.Enable = true;
                    }
                    if (temp.MenuItemName == popUpMenuStripName.setColumnFixed)
                    {
                        temp.Visible = ShowContextMenuSetColumnFixed;
                        temp.Enable = true;
                    }
                }
            }

        }
        private void InitCustomMenu(GridView gridView1)
        {
            popMenu = new GridViewCustomMenu(gridView1,imageListGridPageBar);
            popMenu.Init(new object());
            for (int i = 0; i < popMenu.Items.Count; i++)
            {
                if (popMenu.Items[i].Tag is GridViewCustomMenu.MenuInfo)
                {
                    GridViewCustomMenu.MenuInfo temp = popMenu.Items[i].Tag as GridViewCustomMenu.MenuInfo;
                    if (temp.MenuItemName == popUpMenuStripName.choose)
                    {
                        popMenu.Items[i].Click += new EventHandler(ChoseSelectedRow);
                    }
                    if (temp.MenuItemName == popUpMenuStripName.unChoose)
                    {
                        popMenu.Items[i].Click += new EventHandler(UnChoseSelectedRow);
                    }
                    if (temp.MenuItemName == popUpMenuStripName.chooseAll)
                    {
                        popMenu.Items[i].Click += new EventHandler(ChoseAllRow);
                    }
                    if (temp.MenuItemName == popUpMenuStripName.unChooseAll)
                    {
                        popMenu.Items[i].Click += new EventHandler(UnChoseAllRow);
                    }
                    if (temp.MenuItemName == popUpMenuStripName.addNew)
                    {
                        popMenu.Items[i].Click += new EventHandler(AddNewRow);
                    }
                    if (temp.MenuItemName == popUpMenuStripName.addCopyNew)
                    {
                        popMenu.Items[i].Click += new EventHandler(AddCopyNewRow);
                    }
                    if (temp.MenuItemName == popUpMenuStripName.saveAs)
                    {
                        popMenu.Items[i].Click += new EventHandler(SaveAs);
                    }
                    if (temp.MenuItemName == popUpMenuStripName.copySelectData)
                    {
                        popMenu.Items[i].Click += new EventHandler(CopySelectData);
                    }
                    if (temp.MenuItemName == popUpMenuStripName.copySelectRows)
                    {
                        popMenu.Items[i].Click += new EventHandler(CopySelectRows);
                    }
                    if (temp.MenuItemName == popUpMenuStripName.copySelectCols)
                    {
                        popMenu.Items[i].Click += new EventHandler(CopySelectCols);
                    }
                    if (temp.MenuItemName == popUpMenuStripName.setColumnFixed)
                    {
                        popMenu.Items[i].Click += new EventHandler(SetColumnFixed);
                    }
                }
            }

        }
        private void SetColumnFixed(object sender, EventArgs e)
        {
            if (gridView1 == null)
                return;
            FormSetColumnFixed frm = new FormSetColumnFixed();
            frm.gridView1 = this.gridView1;
            frm.ShowDialog();
        }
        private void CopySelectRows(object sender, EventArgs e)
        {
            GridCell[] selCells = gridView1.GetSelectedCells();
            string selText = ""; //选择 的数据
            if (selCells != null && selCells.Length > 0)
            {
                //找到选择区域  所在行 集合
                List<int> listSelRows = new List<int>();
                for (int i = 0; i < selCells.Length; i++)
                {
                    int row = selCells[i].RowHandle;
                    if (!listSelRows.Contains(row))
                    {
                        listSelRows.Add(row);
                    }
                }
                string tmpValue = "";   //临时值
                string insertTab = "\t";  //插入的\t
                string insertEnter = "\n";//插入的\n
                
                for (int i = 0; i < listSelRows.Count; i++)
                {
                    for (int j = 0; j < gridView1.VisibleColumns.Count; j++)
                    {
                        tmpValue = gridView1.GetRowCellDisplayText(listSelRows[i], gridView1.VisibleColumns[j]);
                        if (tmpValue.Equals("Indeterminate"))
                        {
                            tmpValue = "";
                        }
                        selText = selText + tmpValue +(j==gridView1.VisibleColumns.Count-1?"": insertTab);
                    }
                    if (i != listSelRows.Count - 1)
                    {
                        selText = selText + insertEnter;
                    }
                }
            }
            else
            {
                selText = gridView1.GetFocusedDisplayText();
                if (selText.Equals("Indeterminate"))
                {
                    selText = " ";
                }
            }
            if (selText == "")
                selText = " ";
            Clipboard.SetText(selText);
        }
        private void CopySelectCols(object sender, EventArgs e)
        {
            GridCell[] selCells = gridView1.GetSelectedCells();
            string selText = ""; //选择 的数据
            if (selCells != null && selCells.Length > 0)
            {
                //找到选择区域  所在列 集合
                List<int> listSelCols = new List<int>();
                for (int i = 0; i < selCells.Length; i++)
                {
                    int col = selCells[i].Column.VisibleIndex;
                    if (!listSelCols.Contains(col))
                    {
                        listSelCols.Add(col);
                    }
                }
                string tmpValue = "";   //临时值
                string insertTab = "\t";  //插入的\t
                string insertEnter = "\n";//插入的\n



                for (int j = 0; j < gridView1.DataRowCount; j++)
                {
                    for (int i = 0; i < listSelCols.Count; i++)
                    {
                        tmpValue = gridView1.GetRowCellDisplayText(j, gridView1.VisibleColumns[listSelCols[i]]);
                        if (tmpValue.Equals("Indeterminate"))
                        {
                            tmpValue = "";
                        }
                        selText = selText + tmpValue + (i==listSelCols.Count-1 ?"": insertTab);
                    }
                    if (j != gridView1.DataRowCount - 1)
                    {
                        selText = selText + insertEnter;
                    }
                }
            }
            else
            {
                selText = gridView1.GetFocusedDisplayText();
                if (selText.Equals("Indeterminate"))
                {
                    selText = " ";
                }
            }
            if (selText == "")
                selText = " ";
            Clipboard.SetText(selText);
        }
        private void CopySelectData(object sender, EventArgs e)
        {
          GridCell[] selCells =  gridView1.GetSelectedCells();
          string selText = ""; //选择 的数据
          if (selCells != null && selCells.Length > 0)
          {
              //先找到选择区域左上角的位置
              int colLeft = selCells[0].Column.VisibleIndex; ;
              for (int i = 1; i < selCells.Length; i++)
              {
                  int colTmp = selCells[i].Column.VisibleIndex;
                  if (colLeft > colTmp)
                  {
                      colLeft = colTmp;
                  }
              }
              string tmpValue = "";   //临时值
              string insertTab = "";  //插入的\t
              string insertEnter = "";//插入的\n
              int nCount = 0;         //占位符个数
              for (int i = 0; i < selCells.Length; i++)
              {
                  tmpValue = "";   //临时值
                  insertTab = "";  //插入的\t
                  insertEnter = "";//插入的\n
                  nCount = 0;         //占位符个数

                  tmpValue = gridView1.GetRowCellDisplayText(selCells[i].RowHandle, selCells[i].Column);
                  if (tmpValue.Equals("Indeterminate"))
                  {
                      tmpValue = "";
                  }
                  if (i == 0)  //第一个单元格
                  {
                      nCount= selCells[i].Column.VisibleIndex - colLeft;
                      if (nCount > 0)
                          insertTab = "".PadLeft(nCount, '\t');
                      selText = insertTab + tmpValue;
                  }
                  else if (selCells[i].RowHandle == selCells[i - 1].RowHandle ) //同一行时,加\t
                  {
                      nCount = selCells[i].Column.VisibleIndex - selCells[i - 1].Column.VisibleIndex;
                      if (nCount > 0)
                          insertTab = "".PadLeft(nCount, '\t');
                      selText = selText + insertTab + tmpValue;
                  }
                  else   //不同行,先加\n后加\t
                  {
                      nCount = selCells[i].RowHandle - selCells[i - 1].RowHandle;
                      if (nCount>0)
                      {
                          insertEnter = "".PadLeft(nCount,'\n');
                      }
                      nCount = selCells[i].Column.VisibleIndex -colLeft ;
                      if (nCount > 0)
                          insertTab = "".PadLeft(nCount, '\t');
                      selText = selText + insertEnter + insertTab + tmpValue;
                  }
              }
          }
          else
          {
              selText = gridView1.GetFocusedDisplayText();
              if (selText.Equals("Indeterminate"))
              {
                  selText = " ";
              }
          }
          if (selText == "")
              selText = " ";
          Clipboard.SetText(selText);
           
        }
        private void ChoseSelectedRow(object sender, EventArgs e)
        {
            try
            {
                int[] selRows = gridView1.GetSelectedRows();
                for (int i = 0; i < selRows.Length; i++)
                {
                    SetSelectedColumnChecked(selRows[i], true);
                }
                this.RefreshDataSource();
                //MessageBox.Show("ChoseSelectedRow");
                if (popMenu != null)
                    popMenu.GenerateCloseUpEvent();
            }
            catch (Exception ex)
            {
                EFMessageBox.Show(ex.Message);
            }
        }
        private void UnChoseSelectedRow(object sender, EventArgs e)
        {
            try
            {
                int[] selRows = gridView1.GetSelectedRows();
                for (int i = 0; i < selRows.Length; i++)
                {
                    SetSelectedColumnChecked(selRows[i], false);
                }
                this.RefreshDataSource();
                if (popMenu != null)
                    popMenu.GenerateCloseUpEvent();
            }
            catch (Exception ex)
            {
                EFMessageBox.Show(ex.Message);
            }
        }
       
        private void ChoseAllRow(object sender, EventArgs e)
        {
            try
            {
                if (!EFMultiSelect&&gridView1!=null && gridView1.RowCount>1)
                {
                    EFMessageBox.Show("只允许单选。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                InitSelectedColumnData(true);
                if (popMenu != null)
                    popMenu.GenerateCloseUpEvent();
            }
            catch (Exception ex)
            {
                EFMessageBox.Show(ex.Message);
            }
        }
        private void UnChoseAllRow(object sender, EventArgs e)
        {
            try
            {
                InitSelectedColumnData(false);
                if (popMenu != null)
                    popMenu.GenerateCloseUpEvent();
            }
            catch (Exception ex)
            {
                EFMessageBox.Show(ex.Message);
            }
        }
        private void AddNewRow(object sender, EventArgs e)
        {
            try
            {
                if (EF_GridBar_AddRow_Event != null)
                {
                    DevExpress.XtraEditors.NavigatorButtonClickEventArgs eNew = new NavigatorButtonClickEventArgs(EmbeddedNavigator.Buttons.CustomButtons[9]);
                    EF_GridBar_AddRow_Event(sender, eNew);
                }
                else
                {
                    if (this.DataSource == null)
                    {
                        throw new Exception("The DataSource of EFDevGrid is null!");
                    }
                    if (this.MainView is DevExpress.XtraGrid.Views.Grid.GridView)
                    {
                        ((DevExpress.XtraGrid.Views.Grid.GridView)this.MainView).AddNewRow();//  新增一行
                        if (ShowSelectedColumn)
                        {
                            ((DevExpress.XtraGrid.Views.Grid.GridView)this.MainView).SetFocusedRowCellValue(selectedColumnFieldName, true);
                        }
                    }
                }
                if (popMenu != null)
                    popMenu.GenerateCloseUpEvent();
            }
            catch (Exception ex)
            {
                EFMessageBox.Show(ex.Message);
            }
        }
        private void AddCopyNewRow(object sender, EventArgs e)
        {
            try
            {
                //if (this.DataSource is System.Windows.Forms.BindingSource)
                //{
                //    (this.DataSource as System.Windows.Forms.BindingSource).EndEdit();
                //}
                //addFocusedRowCopy(); //复制新增焦点行
                if (EF_GridBar_AddCopyRow_Event != null)
                {
                    DevExpress.XtraEditors.NavigatorButtonClickEventArgs eNew = new NavigatorButtonClickEventArgs(EmbeddedNavigator.Buttons.CustomButtons[10]);

                    EF_GridBar_AddCopyRow_Event(sender, eNew);
                }
                else
                {
                    if (this.DataSource == null)
                    {
                        throw new Exception("The DataSource of EFDevGrid is null!");
                    }
                    addFocusedRowCopy(); //复制新增焦点行
                }
                //gridView1.FocusedRowHandle = gridView1.DataRowCount - 1;
                // MessageBox.Show("AddCopyNewRow");
                if (popMenu != null)
                    popMenu.GenerateCloseUpEvent();
            }
            catch (Exception ex)
            {
                EFMessageBox.Show(ex.Message);
            }
        }

        private void SaveAs(object sender, EventArgs e)
        {
            try
            {
                ExportTheData();//导出数据
            }
            catch (Exception ex)
            {
                EFMessageBox.Show(ex.Message);
            }
        }

        private void addFocusedRowCopy()
        {
            if (this.DataSource is System.Windows.Forms.BindingSource)
            {
                (this.DataSource as System.Windows.Forms.BindingSource).EndEdit();
            }
            if (gridView1 !=null)
            {
                DataRow drRow = gridView1.GetFocusedDataRow();
                if (drRow == null)
                {
                    gridView1.AddNewRow();
                    return;
                }
                #region 取得数据源表结构
                if (this.DataSource is System.Windows.Forms.BindingSource)
                {
                    object obj = (this.DataSource as System.Windows.Forms.BindingSource).DataSource;
                    if (obj is DataSet)
                    {
                        DataSet ds = obj as DataSet;
                        string tableName = (this.DataSource as System.Windows.Forms.BindingSource).DataMember;
                        if (ds.Tables.Contains(tableName))
                        {
                            dtDataSourceTemp = ds.Tables[tableName];
                        }
                    }
                    else if (obj is DataTable)
                    {
                        dtDataSourceTemp = obj as DataTable;
                    }
                }
                else if (this.DataSource is DataTable)
                {
                    dtDataSourceTemp = this.DataSource as DataTable;
                }
                if (dtDataSourceTemp != null)
                {
                    DataRow drSource = dtDataSourceTemp.NewRow();
                    for (int nIndex = 0; nIndex < drSource.Table.Columns.Count; nIndex++)
                    {
                        drSource[nIndex] = drRow[nIndex];
                    }
                    int i = gridView1.FocusedRowHandle;
                    int rowIndex = gridView1.GetFocusedDataSourceRowIndex();
                    dtDataSourceTemp.Rows.InsertAt(drSource, rowIndex + 1);
                    if (showSelectedColumn)
                    {
                        //使用的是早期的选择列.复制新增时(插入一新行时)选择列对应列表也该插入一行
                        if (checkOptionList.Count < dtDataSourceTemp.Rows.Count)
                        {
                            checkOptionList.Insert(rowIndex + 1, true);
                        }
                    }
                    ((GridView)this.MainView).FocusedRowHandle = rowIndex + 1;
                    SetSelectedColumnChecked(rowIndex + 1, true);
                    //((GridView)this.MainView).SetFocusedRowCellValue(selectedColumnFieldName, true);
                    ((GridView)this.MainView).UnselectRow(rowIndex);
                    ((GridView)this.MainView).SelectRow(rowIndex + 1);
                }
                #endregion
            }
        }

 
        #endregion

        protected GridView gridView1
        {
            get
            {
                return this.MainView as GridView;
            }
        }
        #region 行的可编辑性

        private bool controlRowEdit = true;
        [Description("控制行的可编辑属性")]
        [Category("EF")]
        [DefaultValue(true)]
        protected bool ControlRowEdit
        {
            get { return controlRowEdit; }
            set
            {
                controlRowEdit = value;
                if (!value)
                {
                    return;
                }
            }
        }
        private GridRowEditableHelp _rowEditableHelp;
        protected GridRowEditableHelp rowEditableHelp
        {
            get{
                if(_rowEditableHelp == null)
                {
                    _rowEditableHelp = new GridRowEditableHelp();
                    _rowEditableHelp.Attach(gridView1);
                    return _rowEditableHelp;
                }
                return _rowEditableHelp;
            }
        } 

        /// <summary>
        /// 设置行是否可编辑 
        /// </summary>
        /// <param name="rowHandle">行号</param>
        /// <param name="editable">是否可编辑</param>
        public void SetRowEditable(int rowHandle, bool editable)
        {
            if (!ControlRowEdit)
                return;
            rowEditableHelp.SetRowEditable(rowHandle, editable);
        }

        /// <summary>
        /// 获取行是否可编辑
        /// </summary>
        /// <param name="rowHandle">行号</param>
        /// <returns>true可编辑,false不可编辑</returns>
        public bool GetRowEditable(int rowHandle)
        {
            if (!ControlRowEdit)
                return true;
            return rowEditableHelp.IsRowEditable(rowHandle);
        }
        #endregion

        #region 只有在运行时看见的选择列
        /// <summary>
        /// 选择列是否可见(用于替换ShowSelectedColumn)
        /// </summary>
        [Description("选择列是否可见(用于替换ShowSelectedColumn)")]
        [Category("EF")]
        [DefaultValue(false)]
        public bool ShowSelectionColumn
        {
            get { return showSelectionColumn; }
            set
            {

                if (value == showSelectionColumn && !showSelectionColumn)
                    return;
                showSelectionColumn = value;
                    if (value)
                    {
                        ShowSelectedColumn = false;
                        if (!DesignMode)
                        {
                            SelectionOnRunTime.CheckMarkColumn.Visible = value;
                            SelectionOnRunTime.CheckMarkColumn.VisibleIndex = 0;
                            if (gridView1.Columns.IndexOf(SelectionOnRunTime.CheckMarkColumn) < 0)
                            {
                                gridView1.Columns.Insert(0, SelectionOnRunTime.CheckMarkColumn);
                            }
                            if (this.DataSource != null)
                                this.RefreshDataSource();
                        }
                    }
                    else
                    {
                        if (!DesignMode)
                        {
                            SelectionOnRunTime.CheckMarkColumn.Visible = value;
                        }
                        return;
                    }
                 
            }
        }
        private bool showSelectionColumn = false;

        private GridCheckMarksSelection selection;
        internal GridCheckMarksSelection SelectionOnRunTime
        {
            get
            {
                if (!this.DesignMode && (this.MainView is GridView))
                {
                    if (selection == null)
                    {
                        selection = new GridCheckMarksSelection(gridView1);
                        selection.EFMultiSelect = this.eFMultiSelect;
                        selection.CheckMarkColumn.Visible = true;
                        selection.CheckMarkColumn.VisibleIndex = 0;
                    }
                }
                return selection;
            }
        }

        #endregion
 
        #region 选择列
        /// <summary>
        /// 选择列对象
        /// </summary>
        public GridColumn SelectionColumn
        {
            get
            {
                if (ShowSelectionColumn)
                {
                    return SelectionOnRunTime.CheckMarkColumn;
                }
                else if(ShowSelectedColumn)
                {
                    //使用的是旧的选择列的话
                }
                return null;
            }
        }

        /// <summary>
        /// 选择列是否可见(设计时加载到画面上)
        /// </summary>
        [Description("选择列是否可见")]
        [Category("EF")]
        [DefaultValue(false)]
        public bool ShowSelectedColumn
        {
            get { return showSelectedColumn; }
            set
            {
                if (value == showSelectedColumn && !showSelectedColumn)
                    return;
                showSelectedColumn = value;
                if (value)
                {
                    ShowSelectionColumn = false;
                }
                if (!DesignMode)
                {
                    RefreshSelectedColumn(value);
                }
            }
        }
        private bool showSelectedColumn = false;

        private DataTable dtDataSourceTemp = new DataTable();
        private System.Collections.ArrayList checkOptionList =new System.Collections.ArrayList();
        private DevExpress.XtraGrid.Columns.GridColumn colSelectedTemp = null;

        /// <summary>
        /// 选中行数
        /// </summary>	
        [Description("获得当前选中行数"), Category("EF属性"),Browsable(false)]
        public int EFChoiceCount
        {
            //没有使用选择列,返回0
            get
            {
                if (!ShowSelectedColumn && !ShowSelectionColumn)
                {
                    return 0;
                }
                //运行时选择列
                if (ShowSelectionColumn && SelectionOnRunTime !=null)
                {
                    return SelectionOnRunTime.SelectedCount;
                }
                else if (ShowSelectedColumn)
                {
                    return GetSelectedDataRow().Rows.Count;
                }
                else
                {
                    return 0;
                }
            }
        }
        /// <summary>
        /// 获取当前EFDevGrid的数据源对应的DataTable的Clone
        /// </summary>
        /// <returns></returns>
        private DataTable GetDataSourceTable()
        {
            DataTable dtResult = new DataTable();
            if (this.DataSource is System.Windows.Forms.BindingSource)
            {
                object obj = (this.DataSource as System.Windows.Forms.BindingSource).DataSource;
                if (obj is DataSet)
                {
                    DataSet ds = obj as DataSet;
                    string tableName = (this.DataSource as System.Windows.Forms.BindingSource).DataMember;
                    if (ds.Tables.Contains(tableName))
                    {
                        dtDataSourceTemp = ds.Tables[tableName];
                    }
                }
                else if (obj is DataTable)
                {
                    dtDataSourceTemp = obj as DataTable;
                }
                else if (obj is System.Windows.Forms.BindingSource)
                {
                    DataSet ds = (obj as System.Windows.Forms.BindingSource).DataSource as DataSet;
                    if (ds != null)
                    {
                        string tableName = (this.DataSource as System.Windows.Forms.BindingSource).DataMember;
                        if (ds.Tables.Contains(tableName))
                        {
                            dtDataSourceTemp = ds.Tables[tableName];
                        }
                    }
                }
            }
            else if (this.DataSource is DataSet)
            {
                DataSet ds = this.DataSource as DataSet;
                string tableName = this.DataMember;
                if (ds.Tables.Contains(tableName))
                {
                    dtDataSourceTemp = ds.Tables[tableName];
                }
            }
            else if (this.DataSource is DataTable)
            {
                dtDataSourceTemp = this.DataSource as DataTable;
            }
            if (dtDataSourceTemp != null)
            {
                dtResult = dtDataSourceTemp.Clone();
            }
            return dtResult;
        }
        /// <summary>
        /// 获取所有选择行的数据
        /// </summary>
        /// <returns>选择列被勾选的行组成的表</returns>
        public DataTable GetSelectedDataRow()
        {
            DataTable result = new DataTable();
            //没有使用选择列,返回空表
            if (!ShowSelectedColumn && !ShowSelectionColumn)
            {
                return result;
            }
            #region 取得数据源表结构
            result = GetDataSourceTable();
            //if (this.DataSource is System.Windows.Forms.BindingSource)
            //{
            //    object obj = (this.DataSource as System.Windows.Forms.BindingSource).DataSource;
            //    if (obj is DataSet)
            //    {
            //        DataSet ds = obj as DataSet;
            //        string tableName = (this.DataSource as System.Windows.Forms.BindingSource).DataMember;
            //        if (ds.Tables.Contains(tableName))
            //        {
            //            dtDataSourceTemp = ds.Tables[tableName];
            //        }
            //    }
            //    else if (obj is DataTable)
            //    {
            //        dtDataSourceTemp = obj as DataTable;
            //    }
            //}
            //else if (this.DataSource is DataTable)
            //{
            //    dtDataSourceTemp = this.DataSource as DataTable;
            //}
            //if (dtDataSourceTemp != null)
            //{
            //    result = dtDataSourceTemp.Clone();
            //}
            #endregion
            //运行时选择列
            if (ShowSelectionColumn)
            {
                return SelectionOnRunTime.GetSelectedDataRow(result);
            }
            //旧的选择列 SelectedColumn
            if (this.MainView is DevExpress.XtraGrid.Views.Grid.GridView)
            {
                DevExpress.XtraGrid.Views.Grid.GridView gridView1 = this.MainView as DevExpress.XtraGrid.Views.Grid.GridView;
                DevExpress.XtraGrid.Columns.GridColumn col = gridView1.Columns.ColumnByFieldName(this.selectedColumnFieldName);
                if (col != null && gridView1.DataRowCount > 0)
                {
                    bool temp = false;
                    for (int i = 0; i < gridView1.DataRowCount; i++)
                    {
                        temp = false;
                        if (gridView1.GetRowCellValue(i, col) != null)
                        {
                            bool.TryParse(gridView1.GetRowCellValue(i, col).ToString(), out temp);
                            if (temp)
                            {
                                DataRow dr = gridView1.GetDataRow(i);
                                //复制行数据 
                                DataRow dr2 = result.NewRow();
                                for (int j = 0; j < dr.Table.Columns.Count; j++)
                                {
                                    if (dr2.Table.Columns.Contains(dr.Table.Columns[j].ColumnName))
                                    {
                                        dr2[dr.Table.Columns[j].ColumnName] = dr[j];
                                    }
                                }
                                result.Rows.Add(dr2);
                            }
                        }
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 设置选择列 是否被选中
        /// </summary>
        /// <param name="rowIndex">要设置的行(从0开始)</param>
        /// <param name="check">要设置的值</param>
        public void SetSelectedColumnChecked(int rowIndex,bool check)
        {
            try
            {
                if (rowIndex < 0 && gridView1.FocusedRowHandle == rowIndex)
                {
                     //设计时选择列
                    if (ShowSelectedColumn)
                    {
                        gridView1.SetFocusedRowCellValue(this.selectedColumnFieldName, check);
                    }
                    else if(ShowSelectionColumn)
                    {
                        gridView1.SetFocusedRowCellValue(SelectionColumn, check);
                    }
                    //if (this.DataSource != null)
                    //    this.RefreshDataSource();
                    return;
                }
                if (rowIndex < 0 || rowIndex > this.MainView.DataRowCount)
                {
                    throw new ArgumentOutOfRangeException();
                }
                if (rowIndex < this.MainView.DataRowCount && gridView1 != null )   
                {
                    if (!EFMultiSelect && check)
                    {
                        InitSelectedColumnData(false);
                    }
                    //设计时选择列
                    if (ShowSelectedColumn)
                    {
                        if (gridView1.Columns.ColumnByFieldName(this.selectedColumnFieldName) != null)
                        {
                            gridView1.SetRowCellValue(rowIndex, this.selectedColumnFieldName, check);
                        }
                    }
                    //运行时选择列
                    if (ShowSelectionColumn)
                    {
                        gridView1.SetRowCellValue(rowIndex, SelectionColumn, check);
                        //SelectionOnRunTime.SelectRow(rowIndex, check);
                        //if (gridView1 != null && SelectionColumn != null)
                        //    gridView1.RefreshRowCell(rowIndex, SelectionColumn);
                    }
                }
            }
            catch (ArgumentOutOfRangeException ex1)
            {
                throw ex1;
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }
        }
        /// <summary>
        /// 获取选择列 是否为选中
        /// </summary>
        /// <param name="rowHandle">要获取的行(从0开始)</param>
        /// <returns>选中true,没选中false</returns>
        public bool GetSelectedColumnChecked(int rowHandle)
        {
            try
            {
                if(rowHandle < this.MainView.DataRowCount)
                {
                    if (ShowSelectedColumn && gridView1 != null && gridView1.Columns.ColumnByFieldName(this.selectedColumnFieldName) != null)
                    {
                        object obj = gridView1.GetRowCellValue(rowHandle, gridView1.Columns.ColumnByFieldName(this.selectedColumnFieldName));

                       bool value = false;
                       if (obj == null)
                           return false;
                       bool.TryParse( obj.ToString(),out value);
                       return value;
                    }
                    //运行时选择列
                    if (ShowSelectionColumn && gridView1!=null )//&& gridView1.Columns.ColumnByFieldName(this.selectedColumnFieldName) != null)
                    {
                        return SelectionOnRunTime.IsRowSelected(rowHandle);
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
                return false;
            }
        }
        private string selectedColumnFieldName = "selected";

        private void RefreshSelectedColumn(bool value)
        {
            if (this.Created&& gridView1!=null&& gridView1.Columns.ColumnByFieldName(selectedColumnFieldName) == null)
            {
                if (colSelectedTemp != null)
                {
                    //对bandGridView单独处理
                    if (gridView1 is DevExpress.XtraGrid.Views.BandedGrid.BandedGridView)
                    {
                        DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colTmp = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
                        colTmp.Caption = " ";
                        colTmp.UnboundType = DevExpress.Data.UnboundColumnType.Boolean;
                        colTmp.FieldName = this.selectedColumnFieldName;

                        gridView1.Columns.Insert(0, colTmp);

                        DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand1 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
                        gridBand1.Caption = " ";
                        gridBand1.Columns.Add(colTmp);
                        gridBand1.Visible = true;
                        gridBand1.Width = 75;
                        (gridView1 as DevExpress.XtraGrid.Views.BandedGrid.BandedGridView).Bands.Insert(0, gridBand1);
                    }
                    else
                    {
                        gridView1.Columns.Insert(0, colSelectedTemp);
                    }
                }
            }
            GridColumn col = gridView1.Columns.ColumnByFieldName(selectedColumnFieldName);
            if (col != null)
            {
                gridView1.BeginUpdate();
                col.Visible = value;
                col.VisibleIndex = value ? 0 : -1;
                col.Fixed = FixedStyle.Left;
                if (col.ColumnEdit != null)
                {
                    //改变立即生效
                    col.ColumnEdit.EditValueChanged -= new EventHandler(repositoryItemCheckEdit1_EditValueChanged);
                    col.ColumnEdit.EditValueChanged += new EventHandler(repositoryItemCheckEdit1_EditValueChanged);
                }
                gridView1.EndUpdate();
            }
        }

        private void EFDevGrid_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            try
            {
                if (!this.ShowSelectedColumn)
                {
                    return;
                }
                if (checkOptionList == null)
                {
                    InitSelectedColumnData(false);
                }
                else if(checkOptionList.Count < this.MainView.DataRowCount) 
                {
                    do
                    {
                        checkOptionList.Add(false);//默认都是false,不选中的
                    } while (checkOptionList.Count < this.MainView.DataRowCount);
                }
                if (e.ListSourceRowIndex >= checkOptionList.Count)
                {
                    checkOptionList.Add(false);
                }
                //是选中列
                // MessageBox.Show(e.Column.FieldName + (e.Column.FieldName == this.checkOptionColumnName));
                if (e.Column.FieldName == this.selectedColumnFieldName)
                {
                    if (e.IsGetData)
                        e.Value =(bool)checkOptionList[e.ListSourceRowIndex];
                    else
                    {
                        if (!EFMultiSelect&& (bool)e.Value)
                        {
                            InitSelectedColumnData(false);
                        }
                        checkOptionList[e.ListSourceRowIndex] = (bool)e.Value;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("EFDevGrid_CustomUnboundColumnData:" + ex.Message);
            }
        }


        private void InitSelectedColumnData(bool value)
        {
            //有多少条数据,添加多少条记录
            if (ShowSelectedColumn)
            {
                checkOptionList = new System.Collections.ArrayList();
                for (int i = 0; i < this.MainView.DataRowCount; i++)
                {
                    checkOptionList.Add(value);//默认都是false,不选中的
                }
            }
            else if (ShowSelectionColumn && SelectionOnRunTime!=null)
            {
                if (value)
                {
                    SelectionOnRunTime.SelectAll();
                }
                else
                    SelectionOnRunTime.ClearSelection();
                    
            }
            this.RefreshDataSource();
        }
        private void InitSelectedColumnData()
        {
            InitSelectedColumnData(false);
        }
        private void EFDevGrid_DataSourceChanged(object sender, EventArgs e)
        {
            try
            {
                //初始化 选择列(全为false)
                InitSelectedColumnData();
            
                if (this.DataSource is DataTable)
                {
                    dtDataSourceTemp = this.DataSource as DataTable;
                }
                else if (this.DataSource is DataSet)
                {
                    DataSet ds = this.DataSource as DataSet;
                    if (ds.Tables.Contains(this.DataMember))
                    {
                        dtDataSourceTemp = ds.Tables[this.DataMember];
                    }
                }
				else if (this.DataSource is System.Windows.Forms.BindingSource)
				{
                    (this.DataSource as System.Windows.Forms.BindingSource).DataSourceChanged -= new EventHandler(EFDevGrid_DataSourceChanged);
                    (this.DataSource as System.Windows.Forms.BindingSource).DataSourceChanged +=new EventHandler(EFDevGrid_DataSourceChanged);
					object obj = (this.DataSource as System.Windows.Forms.BindingSource).DataSource;
					if (obj is DataSet)
					{
						DataSet ds = obj as DataSet;
						string tableName = (this.DataSource as System.Windows.Forms.BindingSource).DataMember;
						if (ds.Tables.Contains(tableName))
						{
							dtDataSourceTemp = ds.Tables[tableName];
						}
					}
					else if (obj is DataTable)
					{
						dtDataSourceTemp = obj as DataTable;
					}
				}
				else
				{
					dtDataSourceTemp = null;
				}
                if (dtDataSourceTemp != null)
                {
                    dtDataSourceTemp.TableCleared+=new DataTableClearEventHandler(dtDataSourceTemp_TableCleared);
                    dtDataSourceTemp.RowDeleting += new DataRowChangeEventHandler(dtDataSourceTemp_RowDeleting);
                    //dtDataSourceTemp.RowChanging += new DataRowChangeEventHandler(dtDataSourceTemp_RowChanging);
                    this.RefreshDataSource();
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("EFDevGrid_DataSourceChanged" + ex.Message);
            }
        }
        private   DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
        private DevExpress.XtraGrid.Columns.GridColumn getCheckOptionColumn()
        {

            DevExpress.XtraGrid.Columns.GridColumn gridColumn1 = gridView1.Columns.ColumnByFieldName(selectedColumnFieldName);
            if (gridColumn1 != null)
            {
                return gridColumn1;
            }
            
            gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            // 
            // repositoryItemCheckEdit1
            // 
            repositoryItemCheckEdit1.AutoHeight = false;
            repositoryItemCheckEdit1.EditValueChanged += new EventHandler(edit_EditValueChanged);
            // 
            // gridColumn1
            // 
            gridColumn1.Caption = " ";//EP.EPEF.EPEFC0000012/*选择*/;
            if (repositoryItemCheckEdit1 != null)
            gridColumn1.ColumnEdit = repositoryItemCheckEdit1;
            gridColumn1.FieldName = selectedColumnFieldName;
            gridColumn1.Name = selectedColumnFieldName;
            gridColumn1.UnboundType = DevExpress.Data.UnboundColumnType.Boolean;
            gridColumn1.Visible = ShowSelectedColumn;
            gridColumn1.VisibleIndex = 0;
            gridColumn1.Fixed = FixedStyle.Left;
            gridColumn1.Width = 35;
            //gridColumn1.Disposed += new EventHandler(gridColumn1_Disposed);
            //gridColumn1.ColumnHandle = 0;
            //gridColumn1.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            gridView1.FixedLineWidth = 1;
             return gridColumn1;
        }
        private void edit_EditValueChanged(object sender, EventArgs e)
        {
            if(gridView1!=null)
            gridView1.PostEditor();
        }
        void repositoryItemCheckEdit1_EditValueChanged(object sender, EventArgs e)
        {
            gridView1.PostEditor();
        }
        #endregion

        #region Grid加载

        private void EFDevGrid_Load(object sender, EventArgs e)
        {
            try
            {
                foreach (DevExpress.XtraGrid.Views.Base.BaseView baseView in this.Views)
                {
                    if (baseView is DevExpress.XtraGrid.Views.Grid.GridView)
                    {
                        //为view添加行号显示事件
                        ((DevExpress.XtraGrid.Views.Grid.GridView)baseView).CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(gridView1_CustomDrawRowIndicator);
                        //添加不绑定列[选择列]事件,添加一个选择列
                        ((DevExpress.XtraGrid.Views.Grid.GridView)baseView).CustomUnboundColumnData += new DevExpress.XtraGrid.Views.Base.CustomColumnDataEventHandler(EFDevGrid_CustomUnboundColumnData);
                        this.DefaultViewChanged += new EventHandler(EFDevGrid_DefaultViewChanged);
                        //数据源改变事件
                        this.DataSourceChanged += new EventHandler(EFDevGrid_DataSourceChanged);
                        GetDataSourceTable();//get的时候会顺便设置dtDataSourceTemp
                        if (dtDataSourceTemp != null)
                        {
                            dtDataSourceTemp.RowDeleting += new DataRowChangeEventHandler(dtDataSourceTemp_RowDeleting);
                            dtDataSourceTemp.TableCleared += new DataTableClearEventHandler(dtDataSourceTemp_TableCleared);
                        }
                        //设置自动滚动辅助类对象
                        autoScrollHelper = new AutoScrollHelper(baseView as DevExpress.XtraGrid.Views.Grid.GridView);
                        //设置检查单元格验证事件
                        if (CheckByteLength)
                        {
                            this.MainView.ValidatingEditor -= new DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventHandler(MainView_ValidatingEditor);
                            this.MainView.ValidatingEditor += new DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventHandler(MainView_ValidatingEditor);
                        }
                    }
                }
                if (this.LevelTree != null)
                {
                    for (int ii = 0; ii < this.LevelTree.Nodes.Count; ii++)
                    {
                        DevExpress.XtraGrid.Views.Base.BaseView baseView = this.LevelTree.Nodes[ii].LevelTemplate;
                        if (baseView != null && baseView is DevExpress.XtraGrid.Views.Grid.GridView)
                        {
                            (baseView as DevExpress.XtraGrid.Views.Grid.GridView).CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(gridView1_CustomDrawRowIndicator);
                        }
                    }
                }
                //添加选择列
                AddSelectedColumn();
                //初始化弹出菜单
                if (popMenu == null && (this.MainView is DevExpress.XtraGrid.Views.Grid.GridView))
                {
                    InitCustomMenu(this.MainView as DevExpress.XtraGrid.Views.Grid.GridView);
                    this.MouseDown -= new System.Windows.Forms.MouseEventHandler(this.EFDevGrid_MouseDown);
                    this.MouseDown += new MouseEventHandler(EFDevGrid_MouseDown);
                }
                //添加值改变事件--处理多行同时选中或取消选中
                if (gridView1!=null)
                {
                    gridView1.RowCellClick += new RowCellClickEventHandler(EFDevGrid_RowCellClick);
                    gridView1.TopRowChanged += new EventHandler(gridView1_TopRowChanged);
                }
                //设置行的可编辑属性
                this.MainView.MouseWheel += new MouseEventHandler(MainView_MouseWheel);
                this.MouseDown += new MouseEventHandler(EFDevGrid_MouseDownTemp);
                //this.VisibleChanged += new EventHandler(EFDevGrid_VisibleChanged);
                if (null != this.FindForm())
                {
                    this.FindForm().Shown += new EventHandler(EFDevGrid_Shown);
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("EFDevGrid_Load" + ex.Message);
            }
        }

        /// <summary>
        /// 通过鼠标 滑轮  操控横向滚动条,当ctrl键按下的时候
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void MainView_MouseWheel(object sender, MouseEventArgs e)
        {
            if (System.Diagnostics.Debugger.IsAttached)
            {
                 return;
            }
            if (Control.ModifierKeys == Keys.Shift)
            {
                GridView view = (sender as GridView);
                if (view == null) return;
                view.LeftCoord -= e.Delta;
                throw new DevExpress.Utils.HideException();
            }
        }
        void EFDevGrid_Shown(object sender, EventArgs e)
        {
            try
            {
                //如果gridView内的数据是在窗体load事件中加载的.那么选择列会显示为null需"移动"下,刷新后才能看到效果
                if (Visible)
                    gridView1.RefreshData();
            }
            catch
            {
            }
        }

        private void AddSelectedColumn()
        {
            if (ShowSelectionColumn)
            {
                if (SelectionOnRunTime != null)
                {
                    SelectionOnRunTime.CheckMarkColumn.VisibleIndex = 0;
                    return;
                }
            }
  
            if (ShowSelectedColumn && !(this.MainView is DevExpress.XtraGrid.Views.BandedGrid.BandedGridView))
            {
                GridColumn col = gridView1.Columns.ColumnByFieldName(this.selectedColumnFieldName);
                if (null == col )
                {
                    if (colSelectedTemp != null)
                    {
                        gridView1.Columns.Insert(0, colSelectedTemp);
                        colSelectedTemp.Visible = ShowSelectedColumn;
                    }
                }
                if (col!=null &&　col.ColumnEdit != null)
                {
                    col.ColumnEdit.EditValueChanged -=new EventHandler(repositoryItemCheckEdit1_EditValueChanged);
                    col.ColumnEdit.EditValueChanged+=new EventHandler(repositoryItemCheckEdit1_EditValueChanged);
                }
            }
        }

        private void EFDevGrid_MouseDownTemp(object sender, MouseEventArgs e)
        {
            MouseDownYtemp = e.Y;
        }
        private int MouseDownYtemp = 0;
 
        void EFDevGrid_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            try
            {
                if (e.Column.FieldName == selectedColumnFieldName || e.Column == SelectionColumn)
                {
                    if (e.Y != MouseDownYtemp)
                    {
                        return; //先点击的grid的话,返回
                    }
                    if (e.Button != MouseButtons.Left)
                    {
                        return;//右键的话直接返回
                    }
                    if (!e.Column.OptionsColumn.AllowEdit)
                    {
                        return;//不可编辑返回
                    }
                    if (e.Column.OptionsColumn.ReadOnly)
                    {
                        return;//只读返回
                    }
                    if (e.CellValue != null)
                    {
                        int t =e.Clicks;
                        if (isDraging)
                        {
                            return;
                        }
                        //GridView gridView1 = this.MainView as GridView;
                        if (gridView1 != null)
                        {
                            if (!EFMultiSelect)
                            {
                                return;
                            }
                            bool tmp = false;
                            bool.TryParse(e.CellValue.ToString(), out tmp);
                            if (e.Clicks > 1)
                                return;
                            int[] selRows = gridView1.GetSelectedRows();
                            for (int i = 0; i < selRows.Length; i++)
                            {
                               //当使用SetRowEdit时，选择列仍可编辑的问题 bug
                                if (rowEditableHelp.IsRowEditable(selRows[i]))
                                {
                                    this.SetSelectedColumnChecked(selRows[i], !tmp);
                                }
                            }
                            this.RefreshDataSource();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("EFDevGrid_RowCellClick" + ex.Message);
            }
        }
 
        void dtDataSourceTemp_TableCleared(object sender, DataTableClearEventArgs e)
        {
            InitSelectedColumnData(false);
        }

        void EFDevGrid_DefaultViewChanged(object sender, EventArgs e)
        {
            if (ShowSelectedColumn && !(this.MainView is DevExpress.XtraGrid.Views.BandedGrid.BandedGridView))
            {
                if (  this.MainView is DevExpress.XtraGrid.Views.Grid.GridView )
                {
                    //MessageBox.Show("change"+((DevExpress.XtraGrid.Views.Grid.GridView)MainView).Columns.Count );
                    if (colSelectedTemp != null)
                    {
                        if (((DevExpress.XtraGrid.Views.Grid.GridView)MainView).Columns.ColumnByFieldName(selectedColumnFieldName) == null)
                        {
                            ((DevExpress.XtraGrid.Views.Grid.GridView)MainView).Columns.Insert(0, colSelectedTemp);
                            colSelectedTemp.Visible = ShowSelectedColumn;
                        }
                        else //有的话先删除,再插入
                        {
                            //((DevExpress.XtraGrid.Views.Grid.GridView)MainView).Columns.Remove(((DevExpress.XtraGrid.Views.Grid.GridView)MainView).Columns.ColumnByFieldName(checkOptionColumnName)) ;
                            //((DevExpress.XtraGrid.Views.Grid.GridView)MainView).Columns.Insert(0, colSelectedTemp);

                            //colSelectedTemp.Visible = ShowSelectedColumn;
                        }
                    }
                }
            }
        }
        private bool isDraging = false;
        void dtDataSourceTemp_RowDeleting(object sender, DataRowChangeEventArgs e)
        {
            if (isDraging)
                return;
    
            int i = dtDataSourceTemp.Rows.IndexOf(e.Row);
            if (i > -1)
            {
                //此处的i有问题.主要是dtDataSourceTemp没有排除删除状态的行.
                int delRow = i;
                for (int j = 0; j < delRow; j++)
                {
                    if (dtDataSourceTemp.Rows[j].RowState == DataRowState.Deleted)
                    {
                        i--;
                    }
                }
                if (ShowSelectedColumn && checkOptionList.Count > i)
                {
                    checkOptionList.RemoveAt(i);
                }
                if (ShowSelectionColumn && SelectionOnRunTime != null)
                {
                    SelectionOnRunTime.SelectRow(i, false);
                }
            }
        }
        #endregion

        #region 显示行号
        private bool showRowIndicator = true;
        /// <summary>
        /// 是否显示行号
        /// </summary>
        [DefaultValue(true),Description("是否显示行号"),Category("EF")]
        public bool ShowRowIndicator
        {
            get { return showRowIndicator; }
            set { showRowIndicator = value; }
        }
        private int GetIndicatorWidth()
        {
            int length = (this.MainView.DataRowCount + initRowOrdinal).ToString().Length;
            int width = 0;
            if (length < 3)
            {
                width = 38;
            }
            else
            {
                width = 17 + 7 * length;
            }
            return width;
        }
        private void gridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (!showRowIndicator)
            {
                return;
            }
            if (e.Info.IsRowIndicator&&string.IsNullOrEmpty(e.Info.DisplayText))
            {
                if (e.RowHandle > -1)
                {
                    e.Info.DisplayText = (e.RowHandle + initRowOrdinal).ToString();
                    int width = GetIndicatorWidth();

                    if (this.MainView is GridView)
                    {
                        if (gridView1.IndicatorWidth != width)
                        {
                            gridView1.IndicatorWidth = width;
                            gridView1.RefreshData();
                        }
                    }
                }
                else
                {
                    e.Info.DisplayText = "";
                }
            }
        }


        void gridView1_TopRowChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            try
            {
                if (!showRowIndicator)
                {
                    return;
                }
                int width = GetIndicatorWidth();
                if (gridView1 != null)
                {
                    if (gridView1.IndicatorWidth != width)
                    {
                        gridView1.IndicatorWidth = width;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("gridView1_TopRowChanged error:" + ex.Message);
            }
        }
        #endregion

        #region 自定义列宽
        /// <summary>
        /// 自定义列宽,并设置列标题居中(数据量大时,速度慢)
        /// </summary>
        [Description("自定义列宽,并设置列标题居中")]
        [Browsable(false)]
        [Category("EF")]
        [DefaultValue(false)]
        public bool IsUseCustomColWidth
        {
            get { return isUseCustomColWidth; }
            set
            {
                if (value) SetColumnsWidthAuto();
                isUseCustomColWidth = value;
            }
        }
        private bool isUseCustomColWidth = false;
        
        /// <summary>
        /// 自定义宽度(内部调用的BestFitColumns())
        /// </summary>
        public void SetColumnsWidthAuto()
        {
            foreach (DevExpress.XtraGrid.Views.Base.BaseView baseView in this.Views)
            {
                //((DevExpress.XtraGrid.Views.Grid.GridView)this.MainView).FocusedRowHandle = 0;
                //最适合列宽
                ((DevExpress.XtraGrid.Views.Grid.GridView)baseView).BestFitColumns();
                //列标题居中; 
                foreach (DevExpress.XtraGrid.Columns.GridColumn gridCol in ((DevExpress.XtraGrid.Views.Grid.GridView)baseView).Columns)
                {
                    //gridCol.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    gridCol.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                }
            }
        }
        #endregion

        #region pageSize
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Provides access to the embedded data navigator.")]
        [Category("Appearance")]
        public override ControlNavigator EmbeddedNavigator { get { return base.EmbeddedNavigator; } }
        private int pageSize;
        [Description("EFDevGrid每页条数")]
        [Category("EF")]
        [DefaultValue(20)]
        public int PageSize
        {
            get
            {
                if (pageSize > 0)
                    return pageSize;
                else
                    return 20;//默认20条
            }
            set { pageSize = value; }
        }
        #endregion

        #region grid下分页条
        private bool isUseCustomPageBar = false;
        /// <summary>
        /// 是否使用自定义分页条
        /// </summary>
        [Description("是否使用自定义的分页条")]
        [Category("EF")]
        [DefaultValue(false)]
        public bool IsUseCustomPageBar
        {
            get { return isUseCustomPageBar; }
            set
            {
                if (value)
                {
                    if (!isUseCustomPageBar)
                    {
                        //创建按钮
                        SetGridPageBar();
                        //设置按钮 --避免先设置按钮是否可用,然后设置显示
                        FirstPageButtonEnable = firstPageButtonEnable;
                        LastPageButtonEnable = lastPageButtonEnable;
                        NextPageButtonEnable = nextPageButtonEnable;
                        PrePageButtonEnable = prePageButtonEnable;
                        this.UseEmbeddedNavigator = true;
                        isUseCustomPageBar = true;
                        //设置是否显示
                        SetPageButtonVisible();
                    }
                }
                else
                {
                    isUseCustomPageBar = false;
                    this.UseEmbeddedNavigator = false;
                    this.EmbeddedNavigator.Buttons.CustomButtons.Clear();
                }
            }
        }
        private bool nextPageButtonEnable = true;
        /// <summary>
        /// 下一页是否可用
        /// </summary>
        [Description("下一页是否可用")]
        [Category("EF分页条相关")]
        [DefaultValue(true)]
        public bool NextPageButtonEnable
        {
            get { return nextPageButtonEnable; }
            set
            {
                nextPageButtonEnable = value;
                SetPageButtonEnable(2, value);
            }
        }

        private bool prePageButtonEnable = true;
        /// <summary>
        /// 上一页是否可用
        /// </summary>
        [Description("上一页是否可用")]
        [Category("EF分页条相关")]
        [DefaultValue(true)]
        public bool PrePageButtonEnable
        {
            get { return prePageButtonEnable; }
            set
            {
                prePageButtonEnable = value;
                SetPageButtonEnable(1, value);
            }
        }

        private bool lastPageButtonEnable = true;
        /// <summary>
        /// 最后一页是否可用
        /// </summary>
        [Description("最后一页是否可用")]
        [Category("EF分页条相关")]
        [DefaultValue(true)]
        public bool LastPageButtonEnable
        {
            get { return lastPageButtonEnable; }
            set
            {
                lastPageButtonEnable = value;
                SetPageButtonEnable(3, value);
            }
        }

        private bool firstPageButtonEnable = true;
        /// <summary>
        /// 第一页是否可用
        /// </summary>
        [Description("第一页是否可用")]
        [Category("EF分页条相关")]
        [DefaultValue(true)]
        public bool FirstPageButtonEnable
        {
            get { return firstPageButtonEnable; }
            set
            {
                firstPageButtonEnable = value;
                SetPageButtonEnable(0, value);
            }
        }
        private void SetPageButtonEnable(int imgIndex, bool enable)
        {
            foreach (DevExpress.XtraEditors.NavigatorCustomButton btn in this.EmbeddedNavigator.Buttons.CustomButtons)
            {
                if (btn.ImageIndex == imgIndex)
                {
                    btn.Enabled = enable;
                }
            }
        }

        private void SetPageButtonVisible()
        {
            //showPageButton = true;
            //showRecordMessage = false;
            //showRefreshButton = true;
            //默认将新增,复制新增,删除,以及记录信息隐藏
            //this.ShowRecordMessage = false;
            this.ShowDeleteRowButton = false;
            this.ShowAddCopyRowButton = false;
            this.ShowAddRowButton = false;
            this.ShowPageToButton = false;
        }

        private bool showPageButton = true;
        /// <summary>
        /// 是否显示翻页按钮
        /// </summary>
        [Description("是否显示翻页按钮")]
        [Category("EF分页条相关")]
        [DefaultValue(true)]
        public bool ShowPageButton
        {
            set
            {
                setPageBarButtonVisible(value, 0);
                setPageBarButtonVisible(value, 1);
                setPageBarButtonVisible(value, 2);
                setPageBarButtonVisible(value, 3);
                //this.EmbeddedNavigator.Buttons.First.Visible = value;
                //this.EmbeddedNavigator.Buttons.PrevPage.Visible = value;
                //this.EmbeddedNavigator.Buttons.NextPage.Visible = value;
                //this.EmbeddedNavigator.Buttons.Last.Visible = value;
                showPageButton = value;
            }
            get
            {
                return showPageButton;
            }
        }
        bool showRecordCountMessage = false;
        /// <summary>
        /// 是否显示记录条数信息按钮
        /// </summary>
        [Description("是否显示记录条数信息按钮,")]
        [Category("EF分页条相关")]
        [DefaultValue(false)]
        public bool ShowRecordCountMessage
        {
            set
            {
                showRecordCountMessage = value;
                if (value)
                {
                    this.EmbeddedNavigator.TextLocation = NavigatorButtonsTextLocation.Center;
                }
                else
                {
                    this.EmbeddedNavigator.TextLocation = NavigatorButtonsTextLocation.None;
                }
            }
            get
            {
                return showRecordCountMessage;
            }
        }

        #region 没用
        //private bool showRecordMessage = false;
        /// <summary>
        /// 请用ShowRecordCountMessage
        /// </summary>
        [Description("是否显示记录条数信息按钮.不推荐使用")]
        [Category("EF")]
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Obsolete("用ShowRecordCountMessage")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool ShowRecordMessage
        {
            set
            {
                //if (value)
                //    //显示当前条数
                //    this.EmbeddedNavigator.TextLocation = DevExpress.XtraEditors.NavigatorButtonsTextLocation.Begin;
                //else
                //    this.EmbeddedNavigator.TextLocation = DevExpress.XtraEditors.NavigatorButtonsTextLocation.None;
                //showRecordMessage = value;
            }
            get
            {
                return false;
            }
        }
        #endregion
        private bool showFilterButton = true;
        /// <summary>
        /// 是否显示过滤行按钮
        /// </summary>
        [Description("是否显示过滤行按钮")]
        [Category("EF分页条相关")]
        [DefaultValue(true)]
        public bool ShowFilterButton
        {
            set
            {
                setPageBarButtonVisible(value, 4);
                showFilterButton = value;
            }
            get
            {
                return showFilterButton;
            }
        }
        private bool showGroupButton = true;
        /// <summary>
        /// 是否显示按列分组按钮
        /// </summary>
        [Description("是否显示按列分组按钮")]
        [Category("EF分页条相关")]
        [DefaultValue(true)]
        public bool ShowGroupButton
        {
            set
            {
                setPageBarButtonVisible(value, 5);
                showGroupButton = value;
            }
            get
            {
                return showGroupButton;
            }
        }
        private bool showExportButton = true;
        /// <summary>
        /// 是否显示导出按钮
        /// </summary>
        [Description("是否显示导出按钮")]
        [Category("EF分页条相关")]
        [DefaultValue(true)]
        public bool ShowExportButton
        {
            set
            {
                setPageBarButtonVisible(value, 6);
                showExportButton = value;
            }
            get
            {
                return showExportButton;
            }
        }
        private bool showRefreshButton = true;
        /// <summary>
        /// 是否显示刷新按钮
        /// </summary>
        [Description("是否显示刷新按钮")]
        [Category("EF分页条相关")]
        [DefaultValue(true)]
        public bool ShowRefreshButton
        {
            set
            {
                setPageBarButtonVisible(value, 7);
                showRefreshButton = value;
            }
            get
            {
                return showRefreshButton;
            }
        }
        private bool showSaveLayoutButton = true;
        /// <summary>
        /// 是否显示保存布局按钮
        /// </summary>
        [Description("是否显示保存布局按钮")]
        [Category("EF分页条相关")]
        [DefaultValue(true)]
        public bool ShowSaveLayoutButton
        {
            set
            {
                setPageBarButtonVisible(value, 8);
                showSaveLayoutButton = value;
            }
            get
            {
                return showSaveLayoutButton;
            }
        }

        private bool showAddRowButton = false;
        /// <summary>
        /// 是否显示新增一行按钮
        /// </summary>
        [Description("是否显示新增一行按钮")]
        [Category("EF分页条相关")]
        [DefaultValue(false)]
        public bool ShowAddRowButton
        {
            set
            {
                setPageBarButtonVisible(value, 9);
                showAddRowButton = value;
            }
            get
            {
                return showAddRowButton;
            }
        }
        private bool showAddCopyRowButton = false;
        /// <summary>
        /// 是否显示复制新增一行按钮
        /// </summary>
        [Description("是否显示复制新增一行按钮")]
        [Category("EF分页条相关")]
        [DefaultValue(false)]
        public bool ShowAddCopyRowButton
        {
            set
            {
                setPageBarButtonVisible(value, 11);
                showAddCopyRowButton = value;
            }
            get
            {
                return showAddCopyRowButton;
            }
        }
        private bool showDeleteRowButton = false;
        /// <summary>
        /// 是否显示删除一行按钮
        /// </summary>
        [Description("是否显示删除一行按钮")]
        [Category("EF分页条相关")]
        [DefaultValue(false)]
        public bool ShowDeleteRowButton
        {
            set
            {
                setPageBarButtonVisible(value, 10);
                showDeleteRowButton = value;
            }
            get
            {
                return showDeleteRowButton;
            }
        }

        private bool showPageToButton = false;
        /// <summary>
        /// 是否显示跳转到按钮
        /// </summary>
        [Description("是否显示跳转到按钮")]
        [Category("EF分页条相关")]
        [DefaultValue(false)]
        public bool ShowPageToButton
        {
            set
            {
                setPageBarButtonVisible(value, 12);
                showPageToButton = value;
            }
            get
            {
                return showPageToButton;
            }
        }

        private void setPageBarButtonVisible(bool value, int imgIndex)
        {
            if (this.isUseCustomPageBar)
            {
                foreach (DevExpress.XtraEditors.NavigatorCustomButton btn in this.EmbeddedNavigator.Buttons.CustomButtons)
                {
                    if (btn.ImageIndex == imgIndex)
                    {
                        btn.Visible = value;
                    }
                }
            }
        }
        private void SetGridPageBar()
        {
            //使用自带分页
            this.UseEmbeddedNavigator = true;
            //this.BeginUpdate();
            this.EmbeddedNavigator.Buttons.First.Visible = false;
            this.EmbeddedNavigator.Buttons.Prev.Visible = false;
            this.EmbeddedNavigator.Buttons.PrevPage.Visible = false;
            this.EmbeddedNavigator.Buttons.Next.Visible = false;
            this.EmbeddedNavigator.Buttons.NextPage.Visible = false;
            this.EmbeddedNavigator.Buttons.Last.Visible = false;

            this.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            //显示当前条数
            this.EmbeddedNavigator.TextLocation =ShowRecordCountMessage?DevExpress.XtraEditors.NavigatorButtonsTextLocation.Center: NavigatorButtonsTextLocation.None;
             //初始化自定义按钮
             InitCustomPageBar();
            //添加分页条点击事件
            //先剪掉 是为了避免重复添加
            this.EmbeddedNavigator.ButtonClick -= new DevExpress.XtraEditors.NavigatorButtonClickEventHandler(this.gridControl_EmbeddedNavigator_ButtonClick);
            this.EmbeddedNavigator.ButtonClick += new DevExpress.XtraEditors.NavigatorButtonClickEventHandler(this.gridControl_EmbeddedNavigator_ButtonClick);
        }

        private void InitCustomPageBar()
        {
            if (this.EmbeddedNavigator.CustomButtons.Count == 0)
            {
                this.EmbeddedNavigator.Buttons.ImageList = imageListGridPageBar;
                this.EmbeddedNavigator.CustomButtons.AddRange(new DevExpress.XtraEditors.NavigatorCustomButton[] {
            new DevExpress.XtraEditors.NavigatorCustomButton(0,"第一页"),
            new DevExpress.XtraEditors.NavigatorCustomButton(1,"上一页"),
            new DevExpress.XtraEditors.NavigatorCustomButton(2,"下一页"),
            new DevExpress.XtraEditors.NavigatorCustomButton(3,"最后一页"),
            new DevExpress.XtraEditors.NavigatorCustomButton(4,"过滤行"),
            new DevExpress.XtraEditors.NavigatorCustomButton(5,"分组栏"),
            new DevExpress.XtraEditors.NavigatorCustomButton(6,"导出"),
            new DevExpress.XtraEditors.NavigatorCustomButton(7,"刷新"),
            new DevExpress.XtraEditors.NavigatorCustomButton(8,"保存配置"),
            new DevExpress.XtraEditors.NavigatorCustomButton(9,"新增一行"),
            new DevExpress.XtraEditors.NavigatorCustomButton(11,"复制新增"),
            new DevExpress.XtraEditors.NavigatorCustomButton(10,"删除选择行"),
            new DevExpress.XtraEditors.NavigatorCustomButton(12,"跳转")});
            }
        }
        private string _recourdCountMessage =" {0}/{1} ";
        /// <summary>
        /// 记录信息
        /// </summary>
        [Description("grid内记录信息")]
        [Category("EF分页条相关")]
        [DefaultValue(" {0}/{1} ")]
        //[DesignTimeVisible(false)]
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string RecordCountMessage
        {
            get {
                this.EmbeddedNavigator.TextStringFormat = _recourdCountMessage;
                return this.EmbeddedNavigator.TextStringFormat; }
            set
            {
                _recourdCountMessage = value;
                this.EmbeddedNavigator.TextStringFormat = value;
            }
        }

        private bool canConfigGridCaption = true;
        /// <summary>
        /// 是否可以保存配置时设置列标题
        /// </summary>
        [Description("是否可以保存配置时设置列标题")]
        [Category("EF分页条相关")]
        [DefaultValue(true)]
        public bool CanConfigGridCaption
        {
            get { return canConfigGridCaption; }
            set { canConfigGridCaption = value; }
        }
        public class EFNavigatorButtonClickEventArgs : DevExpress.XtraEditors.NavigatorButtonClickEventArgs
        {
            public EFNavigatorButtonClickEventArgs(NavigatorButtonBase button)
                : base(button)
            {

            }
            public int PageTo;
            public int PageSize;
            public int TotalRecordCount;
        }

        /// <summary>分页条点击事件</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void EFGridBarClickEvent(object sender, DevExpress.XtraEditors.NavigatorButtonClickEventArgs e);
        public delegate void EFGridBarPageToClickEvent(object sender, EFNavigatorButtonClickEventArgs e);
        /// <summary>
        /// 分页条中删除一行点击事件
        /// </summary>
        [Description("分页条中删除一行点击事件"), Category("Grid分页条事件")]
        public event EFGridBarPageToClickEvent EF_GridBar_PageTo_Event;
        /// <summary>
        /// 分页条中第一条点击事件
        /// </summary>
        [Description("分页条中第一条点击事件"),  EditorBrowsable(EditorBrowsableState.Never), Browsable(false), Obsolete("用EF_GridBar_First_Event")]
        public event EFGridBarClickEvent EF_GridBar_Fisrt_Event;
        /// <summary>分页条中第一条点击事件</summary>
        [Description("分页条中第一条点击事件"), Category("Grid分页条事件")]
        public event EFGridBarClickEvent EF_GridBar_First_Event;
        
        /// <summary>分页条中上一页点击事件</summary>
        [Description("分页条中上一页点击事件"), Category("Grid分页条事件")]
        public event EFGridBarClickEvent EF_GridBar_PrePage_Event;
        /// <summary>分页条中下一页点击事件</summary>
        [Description("分页条中下一页点击事件"), Category("Grid分页条事件")]
        public event EFGridBarClickEvent EF_GridBar_NextPage_Event;
        /// <summary>分页条中最后一条点击事件</summary>
        [Description("分页条中最后一条点击事件"), Category("Grid分页条事件")]
        public event EFGridBarClickEvent EF_GridBar_Last_Event;
        /// <summary>分页条中刷新点击事件</summary>
        [Description("分页条中刷新点击事件"), Category("Grid分页条事件")]
        public event EFGridBarClickEvent EF_GridBar_Refresh_Event;
        /// <summary>
        /// 分页条中保存布局按钮点击事件
        /// </summary>
        [Description("分页条中保存布局按钮点击事件"), Category("Grid分页条事件")]
        public event EFGridBarClickEvent EF_GridBar_SaveLayout_Event;
        /// <summary>
        /// 分页条中新增按钮点击事件
        /// </summary>
        [Description("分页条中新增按钮点击事件"), Category("Grid分页条事件")]
        public event EFGridBarClickEvent EF_GridBar_AddRow_Event;
        /// <summary>
        /// 分页条中复制新增按钮点击事件
        /// </summary>
        [Description("分页条中复制新增按钮点击事件"), Category("Grid分页条事件")]
        public event EFGridBarClickEvent EF_GridBar_AddCopyRow_Event;
        /// <summary>分页条中保存布局点击事件</summary>
        [Description("分页条中删除一行点击事件"), Category("Grid分页条事件")]
        public event EFGridBarClickEvent EF_GridBar_Remove_Event;

        private void RefreshDataUpdated()
        {
            if (gridView1 != null)
            {
                int j = gridView1.GetFocusedDataSourceRowIndex();
                bool check =  this.GetSelectedColumnChecked(gridView1.FocusedRowHandle);

                gridView1.BeginDataUpdate();
                gridView1.EndDataUpdate();
                
                j = gridView1.GetRowHandle(j);
                gridView1.FocusedRowHandle = j;
                if (j < 0 && j != gridView1.FocusedRowHandle)
                    return;
                this.SetSelectedColumnChecked(j, check);
            }
        }
        /// <summary>
        /// grid中分页条点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridControl_EmbeddedNavigator_ButtonClick(object sender, DevExpress.XtraEditors.NavigatorButtonClickEventArgs e)
        {
            try
            {
                this.EmbeddedNavigator.ButtonClick -= new DevExpress.XtraEditors.NavigatorButtonClickEventHandler(this.gridControl_EmbeddedNavigator_ButtonClick);
                if (gridView1.State == GridState.Editing)
                {
                    if (gridView1.ActiveEditor.OldEditValue != gridView1.ActiveEditor.EditValue)
                    {
                        gridView1.SetFocusedValue(gridView1.ActiveEditor.EditValue);
                    }
                    RefreshDataUpdated();
                }
                if (this.DataSource is System.Windows.Forms.BindingSource)
                {
                    (this.DataSource as System.Windows.Forms.BindingSource).EndEdit();
                }
                
                #region 自定义按钮
                if (e.Button.ButtonType == DevExpress.XtraEditors.NavigatorButtonType.Custom)
                {
                    #region 替换为原来自带的按钮
                    if (e.Button.ImageIndex == 0) //第一条
                    {
                        if (EF_GridBar_Fisrt_Event != null)
                        {
                            EF_GridBar_Fisrt_Event(sender, e);
                        }
                        else if (EF_GridBar_First_Event != null)
                        {
                            EF_GridBar_First_Event(sender, e);
                        }
                        else
                        {
                            if (this.MainView is DevExpress.XtraGrid.Views.Grid.GridView)
                            {
                                ((DevExpress.XtraGrid.Views.Grid.GridView)this.MainView).MoveFirst();//  .FocusedRowHandle = 0;
                            }
                        }
                    }
                    else if (e.Button.ImageIndex == 1) //上一页
                    {
                        if (EF_GridBar_PrePage_Event != null)
                        {
                            EF_GridBar_PrePage_Event(sender, e);
                        }
                        else
                        {
                            if (this.MainView is DevExpress.XtraGrid.Views.Grid.GridView)
                            {
                                ((DevExpress.XtraGrid.Views.Grid.GridView)this.MainView).MovePrevPage();
                            }
                        }
                    }
                    if (e.Button.ImageIndex == 2) //下一页
                    {
                        if (EF_GridBar_NextPage_Event != null)
                        {
                            EF_GridBar_NextPage_Event(sender, e);
                        }
                        else
                        {
                            if (this.MainView is DevExpress.XtraGrid.Views.Grid.GridView)
                            {
                                ((DevExpress.XtraGrid.Views.Grid.GridView)this.MainView).MoveNextPage();
                            }
                        }
                    }
                    else if (e.Button.ImageIndex == 3) //最后一条
                    {
                        if (EF_GridBar_Last_Event != null)
                        {
                            EF_GridBar_Last_Event(sender, e);
                        }
                        else
                        {
                            if (this.MainView is DevExpress.XtraGrid.Views.Grid.GridView)
                            {
                                ((DevExpress.XtraGrid.Views.Grid.GridView)this.MainView).MoveLast();
                            }
                        }
                    }
                    #endregion
                    if (e.Button.ImageIndex == 4) //显示过滤行
                    {
                        if (gridView1!=null)
                        {
                            gridView1.OptionsView.ShowAutoFilterRow = !gridView1.OptionsView.ShowAutoFilterRow;
                        }
                    }
                    else if (e.Button.ImageIndex == 5) //显示汇总栏
                    {
                        if (gridView1 != null)
                        {
                            gridView1.OptionsView.ShowGroupPanel = !gridView1.OptionsView.ShowGroupPanel;
                        }
                    }
                    else if (e.Button.ImageIndex == 6) //导出
                    {
                        ExportTheData();
                    }
                    else if (e.Button.ImageIndex == 7) //刷新
                    {
                        if (EF_GridBar_Refresh_Event != null)
                        {
                            EF_GridBar_Refresh_Event(sender, e);
                        }
                    }
                    else if (e.Button.ImageIndex == 8) //保存Grid布局
                    {
                        //添加一个配置标题项
                        if (CanConfigGridCaption)
                        {
                            if (this.MainView is DevExpress.XtraGrid.Views.Grid.GridView)
                            {
                                if (EF.EFMessageBox.Show("是否配置列标题？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                {
                                    EF.FormConfigCaption frm = new EF.FormConfigCaption();
                                    frm.GridViewCongif = this.MainView as DevExpress.XtraGrid.Views.Grid.GridView;
                                    frm.ShowDialog(); //配置列标题
                                }
                            }
                        }
                        if (EF_GridBar_SaveLayout_Event != null)
                        {
                            EF_GridBar_SaveLayout_Event(sender, e);
                        }
                        else
                        {
                            SaveLayout(ConfigEnum.UserConfig);//默认用户级配置
                            // SaveLayout(ConfigEnum.UserConfig);//默认项目级配置
                        }
                    }
                    else if (e.Button.ImageIndex == 9) //新增
                    {
                        if (EF_GridBar_AddRow_Event != null)
                        {
                            EF_GridBar_AddRow_Event(sender, e);
                        }
                        else
                        {
                            if (this.MainView is DevExpress.XtraGrid.Views.Grid.GridView)
                            {
                                gridView1.AddNewRow();//  新增一行
                                //SetSelectedColumnChecked(MainView.RowCount - 1, true);
                                if (AutoSelectNewRow&&ShowSelectionColumn)
                                {
                                    gridView1.SetFocusedRowCellValue(this.SelectionColumn, true);

                                }
                                gridView1.SetFocusedRowCellValue(selectedColumnFieldName, true);
                            }
                        }
                    }
                    else if (e.Button.ImageIndex == 11) //复制新增
                    {
                        if (EF_GridBar_AddCopyRow_Event != null)
                        {
                            EF_GridBar_AddCopyRow_Event(sender, e);
                        }
                        else
                        {
                            addFocusedRowCopy(); //复制新增焦点行
                        }
                    }
                    else if (e.Button.ImageIndex == 10) //删除
                    {
                        if (EF_GridBar_Remove_Event != null)
                        {
                            EF_GridBar_Remove_Event(sender, e);
                        }
                        else
                        {
                            if (gridView1 != null)
                            {
                                DevExpress.XtraGrid.Columns.GridColumn col = null;
                                if (ShowSelectedColumn)
                                {
                                    col = gridView1.Columns.ColumnByFieldName(this.selectedColumnFieldName);
                                }

                                if (ShowSelectionColumn)
                                {
                                    col = SelectionOnRunTime.CheckMarkColumn;
                                }
                                if (col != null)
                                {
                                    bool temp = false;
                                    for (int i = gridView1.RowCount; i >= 0; i--)
                                    {
                                        temp = this.GetSelectedColumnChecked(i);
                                        if (temp)
                                        {
                                            gridView1.DeleteRow(i);
                                        }
                                    }
                                }

                                else
                                {
                                    gridView1.DeleteSelectedRows();//  删除选择行
                                }
                            }
                        }
                    }
                    else if (e.Button.ImageIndex == 12) //跳转页
                    {
                        EF.FormGotoPage frm = new EF.FormGotoPage();

                        frm.StartPosition = FormStartPosition.CenterParent;
                        frm.TotalRecordCount = TotalRecordCount;
                        frm.PageSize = PageSize;
                        if (frm.ShowDialog() == DialogResult.OK)
                        {
                            if (EF_GridBar_PageTo_Event != null)
                            {
                                EFNavigatorButtonClickEventArgs args = new EFNavigatorButtonClickEventArgs(e.Button);
                                args.PageTo = frm.PageTo;
                                args.PageSize = frm.PageSize;
                                args.TotalRecordCount = frm.TotalRecordCount;
                                EF_GridBar_PageTo_Event(sender, args);
                            }
                        }
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }
            finally
            {
                this.EmbeddedNavigator.ButtonClick += new DevExpress.XtraEditors.NavigatorButtonClickEventHandler(this.gridControl_EmbeddedNavigator_ButtonClick);
            }
        }
        #endregion

        #region 保存和加载布局:布局的文件名称为 (传入的ClassName+窗体Form的名称+"_"+gridView的名称)
        public enum ConfigEnum
        {
            UserConfig,
            EPConfig,
            Default //默认先查找用户配置,没有则查找项目配置
        }
        /// <summary>
        /// 以窗体Form的名称+"_"+gridView的名称,以及默认配置模式[暂定用户级]保存grid配置
        /// </summary>
        public void SaveLayout()
        {
            SaveLayout(ConfigEnum.Default);
        }
        /// <summary>
        /// 以窗体Form的名称+"_"+gridView的名称,以及制定的配置模式保存grid配置
        /// </summary>
        /// <param name="ConfigModule">用户级和项目级</param>
        public void SaveLayout(ConfigEnum ConfigModule)
        {
            this.SaveLayout(ConfigModule, string.Empty, string.Empty);
        }
        /// <summary>
        ///  文件目录不存在则创建,XML配置名称为: className +"窗体名称_"+gridView的名称.XML. 
        /// </summary>  
        /// <param name="ConfigModule">配置默认,用户级和项目级</param>
        /// <param name="className">类名,当画面是配置画面时,为了区分不同窗体而添加类名 </param>   
        /// <param name="moduleName">一级模块名称(如DE,为空时取窗体名称的第4至6位)</param>
        public void SaveLayout(ConfigEnum ConfigModule, string moduleName, string className)
        {
            //模块名称
            string formName = this.FindForm().Name;
            if (moduleName.Trim().Equals(""))
            {
                moduleName = formName.Length > 6 ? formName.Substring(4, 2) : formName;
            }
            if (formName.StartsWith("Form"))
            {
                formName = formName.Substring(4);
            }
            //XML路径
            string fileDirectory = System.Reflection.Assembly.GetExecutingAssembly().Location;
            fileDirectory = Path.GetDirectoryName(fileDirectory);
            
            if (ConfigModule == ConfigEnum.UserConfig)
            {
                //位于UserConfig下,用户名文件夹下,一级模块下
                fileDirectory = fileDirectory + "\\..\\UserConfig\\" + "formUserId" + "\\" + moduleName + "\\";
            }
            else if (ConfigModule == ConfigEnum.EPConfig)
            {
                //位于一级模块下
                fileDirectory = fileDirectory + "\\..\\" + moduleName + "\\";
            }
            else
            {
                //位于UserConfig下,用户名文件夹下,一级模块下 (默认用户配置)
                fileDirectory = fileDirectory + "\\..\\UserConfig\\" + "EF_Args.formUserId" + "\\" + moduleName + "\\";
            }
            string fileDirectory2 = "";
            try
            {
                fileDirectory2 = fileDirectory + "EC.UserConfig.Instance.Culture"+"\\";
            }
            catch   {  }
            //文件目录不存在则创建[逻辑上,当是项目配置时,模块目录一定存在,否则报错]
            if (!System.IO.Directory.Exists(fileDirectory))
            {
                System.IO.Directory.CreateDirectory(fileDirectory);
            }
            if (fileDirectory2 != "" && !System.IO.Directory.Exists(fileDirectory2))
            {
                System.IO.Directory.CreateDirectory(fileDirectory2);
            }

            //为了应对多个View的情况,文件名以GridView为主           
            foreach (DevExpress.XtraGrid.Views.Base.BaseView view in this.Views)
            {
                //string fileName = className.Trim() + formName + "_" + view.Name + ".xml";
                //  ---2011-10-27 把 XML配置名称 改为 "窗体名称_"+View的名称+_className.XML
                string fileName = formName + "_" + view.Name + (className.Trim().Equals("") ? "" : ("_" + className.Trim())) + ".xml";
                string filePath = fileDirectory + fileName;
                string filePath2 = fileDirectory2 + fileName;
                bool isFirstConfig = false;
                //if (System.IO.File.Exists(filePath))
                //{
                    if (System.IO.File.Exists(filePath2))
                    {
                        //文件已存在,则提示,是否覆盖
                        if (EF.EFMessageBox.Show("配置文件已存在,是否替换？", "提示", MessageBoxButtons.YesNo) != DialogResult.Yes)
                        {
                            return;
                        }
                    }
                //}
                else
                {
                    isFirstConfig = true;
                }
                //添加默认保存 所有可保存项
                if (view is DevExpress.XtraGrid.Views.Grid.GridView)
                {
                    (view as DevExpress.XtraGrid.Views.Grid.GridView).OptionsLayout.StoreAllOptions = true;
                    (view as DevExpress.XtraGrid.Views.Grid.GridView).OptionsLayout.StoreAppearance = true;
                    (view as DevExpress.XtraGrid.Views.Grid.GridView).OptionsLayout.StoreDataSettings = true;
                    (view as DevExpress.XtraGrid.Views.Grid.GridView).OptionsLayout.StoreVisualOptions = true;
                    (view as DevExpress.XtraGrid.Views.Grid.GridView).OptionsLayout.Columns.StoreAllOptions = true;
                    (view as DevExpress.XtraGrid.Views.Grid.GridView).OptionsLayout.Columns.StoreAppearance = true;
                    (view as DevExpress.XtraGrid.Views.Grid.GridView).OptionsLayout.Columns.StoreLayout = true;
                }
                if (isFirstConfig)
                {
                    //即使是第一次配置也不保存到默认目录下.只放到对应语言目录下
                    //view.SaveLayoutToXml(filePath);
                }
                view.SaveLayoutToXml(filePath2);
            }
        }

        /// <summary>
        /// 加载已保存的布局。
        /// 寻找(以窗体Form的名称+"_"+gridView的名称)的配置文件.加载布局..先查找用户目录,没有则查找项目级目录,没有则不操作
        /// </summary>
        public void LoadLayout()
        {
              LoadLayout(ConfigEnum.Default);
        }
        /// <summary>
        /// 加载已保存的布局。
        /// 寻找(以窗体Form的名称+"_"+gridView的名称)的配置文件.加载布局..根据参数(配置模式)选择对应目录下的配置文件.
        /// </summary>
        /// <param name="ConfigModule">配置模式：用户级UserConfig ，项目级EPConfig </param>
        public void LoadLayout(ConfigEnum configModule)
        {
              LoadLayout(configModule, "", "");
        }
        /// <summary>
        /// 加载已保存的布局。
        /// 寻找(窗体Form的名称+"_"+gridView的名称+ className)的配置文件.加载布局..根据参数(配置模式)选择对应目录下的配置文件.
        /// </summary>
        /// <param name="ConfigModule">配置模式,默认时先查找用户配置,再查找项目级配置..</param>
        /// <param name="moduleName">一级模块名称.如DE,FI等</param>
        /// <param name="className">类名称[当窗体名称_+gridView名称不能满足唯一xml文件名需求时,在开头添加类名称</param>
        public void LoadLayout(ConfigEnum configModule, string moduleName, string className)
        {
            //模块名称
            string formName = this.FindForm().Name;
            if (moduleName.Trim().Equals(""))
            {
                moduleName = formName.Length > 6 ? formName.Substring(4, 2) : formName;
            }
            if (formName.StartsWith("Form"))
            {
                formName = formName.Substring(4);
            }
            //XML路径
            string fileDirectory = System.Reflection.Assembly.GetExecutingAssembly().Location;
            fileDirectory = Path.GetDirectoryName(fileDirectory);

            string fileDirectoryUser = fileDirectory + "\\..\\UserConfig\\" + "EF_Args.formUserId" + "\\" + moduleName + "\\";
            string fileDirectoryEP = fileDirectory + "\\..\\" + moduleName + "\\";

            //先查看用户级配置,目录不存在读取项目配置
            foreach (DevExpress.XtraGrid.Views.Base.BaseView view in this.Views)
            {
                //添加默认加载所有保存项
                if (view is DevExpress.XtraGrid.Views.Grid.GridView)
                {
                    (view as DevExpress.XtraGrid.Views.Grid.GridView).OptionsLayout.StoreAllOptions = true;
                    (view as DevExpress.XtraGrid.Views.Grid.GridView).OptionsLayout.StoreAppearance = true;
                    (view as DevExpress.XtraGrid.Views.Grid.GridView).OptionsLayout.StoreDataSettings = true;
                    (view as DevExpress.XtraGrid.Views.Grid.GridView).OptionsLayout.StoreVisualOptions = true;
                    (view as DevExpress.XtraGrid.Views.Grid.GridView).OptionsLayout.Columns.StoreAllOptions = true;
                    (view as DevExpress.XtraGrid.Views.Grid.GridView).OptionsLayout.Columns.StoreAppearance = true;
                    (view as DevExpress.XtraGrid.Views.Grid.GridView).OptionsLayout.Columns.StoreLayout = true;
                }
                //  ---2011-10-27 把 XML配置名称 改为 "窗体名称_"+View的名称+_className.XML
                string fileName = formName + "_" + view.Name + (className.Trim().Equals("") ? "" : ("_" + className.Trim())) + ".xml";
                // string fileName = className.Trim() + formName + "_" + view.Name + ".xml";
                string filePath = "";  //配置文件位置
                if (configModule == ConfigEnum.EPConfig)
                {
                    filePath = fileDirectoryEP + fileName;

                }
                else if (configModule == ConfigEnum.UserConfig)
                {
                    filePath = fileDirectoryUser + fileName;
                }
                else //默认配置.先读取用户级配置,不存在则读取项目级配置
                {
                    filePath = fileDirectoryUser + fileName;
                    string tmp2 = filePath;
                    if (!System.IO.File.Exists(filePath)&& !IsFileExit(ref tmp2))  //用户配置不存在,则读取项目级配置
                    {
                        filePath = fileDirectoryEP + fileName;
                    }
                }
                //根据配置模式,找到xml文件.存在,则读取
                if (IsFileExit(ref filePath))//   (System.IO.File.Exists(filePath))
                {
                    string EPConfigXML = fileDirectoryEP + fileName; //项目级配置xml
                    //项目级配置xml 如果存在就麻烦了---不存在,则直接读取配置文件
                    //---先加载项目级配置,把不可见的项,设置为不可配置
                    //---如果是用户配置则 "再加载用户级配置",否则退出
                    //---再把不可配置项设为不可见
                    if (IsFileExit(ref EPConfigXML))
                    {
                        view.RestoreLayoutFromXml(EPConfigXML);
                        if (view is DevExpress.XtraGrid.Views.Grid.GridView)
                        {
                            foreach (DevExpress.XtraGrid.Columns.GridColumn gridCol in ((DevExpress.XtraGrid.Views.Grid.GridView)view).Columns)
                            {
                                gridCol.OptionsColumn.ShowInCustomizationForm = gridCol.Visible;
                            }
                        }
                        //然后加载用户级配置
                        if (filePath.Equals(EPConfigXML))
                        {
                            break;
                        }
                        view.RestoreLayoutFromXml(filePath);
                        //然后再次检查,把项目级不可见的,也设为不可见
                        if (view is DevExpress.XtraGrid.Views.Grid.GridView)
                        {
                            foreach (DevExpress.XtraGrid.Columns.GridColumn gridCol in ((DevExpress.XtraGrid.Views.Grid.GridView)view).Columns)
                            {
                                //如果本来是不可配置的,设为不可见..否则不用管
                                if (!gridCol.OptionsColumn.ShowInCustomizationForm)
                                {
                                    gridCol.Visible = false;
                                }
                                //gridCol.Visible = gridCol.OptionsColumn.ShowInCustomizationForm;
                                //
                            }
                        }
                       // return true;
                    }
                    else
                    {
                        view.RestoreLayoutFromXml(filePath);
                    }
                }
                else
                {
                    //return false;
                }
            }
           // return true;
        }

        private bool IsFileExit(ref string fileFullPath)
        {
            string filePath = fileFullPath;
            try
            {
                string culture ="\\"+  "EC.UserConfig.Instance.Culture";
                int last = filePath.LastIndexOf("\\");
                filePath = filePath.Insert(last, culture);
                if (File.Exists(filePath))
                {
                    fileFullPath = filePath;
                    return true;
                }
                return File.Exists(fileFullPath);
            }
            catch { }
            return File.Exists(fileFullPath);
        }
        #endregion

        #region 导出按钮事件
        private void ExportTheData()
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Excel files(*.xls)|*.xls|Excel files(*.xlsx)|*.xlsx|txt files (*.txt)|*.txt|html files (*.html)|*.html|All files (*.*)|*.*";
            saveFileDialog1.RestoreDirectory = true;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (saveFileDialog1.FilterIndex == 3)
                {
                    //导出为txt
                    ExportTo(new DevExpress.XtraExport.ExportTxtProvider(saveFileDialog1.FileName));
                }
                else if (saveFileDialog1.FilterIndex == 4)
                {
                    //导出为html
                    ExportTo(new DevExpress.XtraExport.ExportHtmlProvider(saveFileDialog1.FileName));
                }
                else if (saveFileDialog1.FilterIndex == 1)//默认导出excel,为1时也为excel
                {
                    if (EFMessageBox.Show("是否将数值型以文本格式导出?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                    {
                        ExportTo(new DevExpress.XtraExport.ExportXlsProvider(saveFileDialog1.FileName), true);
                    }
                    else
                    {
                        ExportTo(new DevExpress.XtraExport.ExportXlsProvider(saveFileDialog1.FileName));
                    }
                }
                else if (saveFileDialog1.FilterIndex == 2)//默认导出excel,为1时也为excel
                {
                    //DevExpress.XtraPrinting.XlsxExportOptions option = new DevExpress.XtraPrinting.XlsxExportOptions();
                    this.ExportToXlsx(saveFileDialog1.FileName);
                }
            }
        }
        private void ExportTo(DevExpress.XtraExport.IExportProvider provider)
        {
            ExportTo(provider, false);
        }
        private void ExportTo(DevExpress.XtraExport.IExportProvider provider,bool blAsTextStyle)
        {
            
            Cursor currentCursor = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;
            this.FindForm().Refresh();
            DevExpress.XtraGrid.Export.BaseExportLink link = this.MainView.CreateExportLink(provider);
            (link as DevExpress.XtraGrid.Export.GridViewExportLink).ExpandAll = false;
            (link as DevExpress.XtraGrid.Export.GridViewExportLink).ExportCellsAsDisplayText = blAsTextStyle;
            //link.Progress += new DevExpress.XtraGrid.Export.ProgressEventHandler(Export_Progress);
            //(link as DevExpress.XtraGrid.Export.GridViewExportLink).ExportAppearance.VertLine.Options.us

            //link.ExportAppearance.AsQueryable();
            link.ExportTo(true);

            provider.Dispose();
            //link.Progress -= new DevExpress.XtraGrid.Export.ProgressEventHandler(Export_Progress);
            Cursor.Current = currentCursor;
        }
        private void Export_Progress(object sender, DevExpress.XtraGrid.Export.ProgressEventArgs e)
        {
            if (e.Phase == DevExpress.XtraGrid.Export.ExportPhase.Link)
            {
                //progressBarControl1.Position = e.Position;
                this.Update();
            }
        }
        #endregion

        #region 行拖动
        private bool allowDragRow = false;
        /// <summary>
        /// 是否允许行拖动
        /// </summary>
        [Description("是否允许行拖动")]
        [Category("EF")]
        [DefaultValue(false)]
        public bool AllowDragRow
        {
            get { return allowDragRow; }
            set
            {
                if (value == allowDragRow)
                    return;
                allowDragRow = value;
                //如果允许行拖动,则添加相关事件(鼠标点击,移动,拖动进入,移动,结束)
                if (allowDragRow)
                {
                    this.AllowDrop = true;
                    this.Paint -= new PaintEventHandler(EFDevGrid_Paint);
                    this.DragOver -= new System.Windows.Forms.DragEventHandler(this.EFDevGrid_DragOver);
                    this.DragEnter -= new System.Windows.Forms.DragEventHandler(this.EFDevGrid_DragEnter);
                    this.DragDrop -= new System.Windows.Forms.DragEventHandler(this.EFDevGrid_DragDrop);
                    this.MouseMove -= new System.Windows.Forms.MouseEventHandler(this.EFDevGrid_MouseMove);
                    this.MouseDown -= new System.Windows.Forms.MouseEventHandler(this.EFDevGrid_MouseDown);
                    this.DragLeave -= new EventHandler(EFDevGrid_DragLeave);

                    this.Paint += new PaintEventHandler(EFDevGrid_Paint);
                    this.DragOver += new System.Windows.Forms.DragEventHandler(this.EFDevGrid_DragOver);
                    this.DragEnter += new System.Windows.Forms.DragEventHandler(this.EFDevGrid_DragEnter);
                    this.DragDrop += new System.Windows.Forms.DragEventHandler(this.EFDevGrid_DragDrop);
                    this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.EFDevGrid_MouseMove);
                    this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EFDevGrid_MouseDown);
                    this.DragLeave += new EventHandler(EFDevGrid_DragLeave);
                }
                else
                {
                    //this.AllowDrop = false;
                    this.Paint -= new PaintEventHandler(EFDevGrid_Paint);
                    this.DragOver -= new System.Windows.Forms.DragEventHandler(this.EFDevGrid_DragOver);
                    this.DragEnter -= new System.Windows.Forms.DragEventHandler(this.EFDevGrid_DragEnter);
                    this.DragDrop -= new System.Windows.Forms.DragEventHandler(this.EFDevGrid_DragDrop);
                    this.MouseMove -= new System.Windows.Forms.MouseEventHandler(this.EFDevGrid_MouseMove);
                    //this.MouseDown -= new System.Windows.Forms.MouseEventHandler(this.EFDevGrid_MouseDown);
                    this.DragLeave -= new EventHandler(EFDevGrid_DragLeave);
                }
            }
        }

        void EFDevGrid_DragLeave(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            destRowIndex = -1;
            Refresh();
        }
        /// <summary>
        /// 点击信息
        /// </summary>
        private GridHitInfo downHitInfo;
        /// <summary>
        /// 拖动时,自动滚动
        /// </summary>
        private AutoScrollHelper autoScrollHelper;
        /// <summary>
        /// 拖动起始行
        /// </summary>
        private int sourceRowIndex = -1;
        /// <summary>
        /// 拖动目标行
        /// </summary>
        private int destRowIndex = -1;

        /// <summary>
        /// 鼠标点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EFDevGrid_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                downHitInfo = null;
                GridHitInfo hitInfo = gridView1.CalcHitInfo(new Point(e.X, e.Y));
                if (e.Button == MouseButtons.Left && hitInfo.InRow)
                    downHitInfo = hitInfo;
                if (e.Button == MouseButtons.Right)
                {
                   // gridView1.FocusedRowHandle =   hitInfo.RowHandle;
                   // gridView1.SelectRow(hitInfo.RowHandle);
                    DoShowContextMenu(hitInfo,e);
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// 鼠标移动事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EFDevGrid_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button != MouseButtons.Left) return;
                if (downHitInfo == null) return;
                Rectangle dragRect = new Rectangle(new Point(
                    downHitInfo.HitPoint.X - SystemInformation.DragSize.Width / 2,
                    downHitInfo.HitPoint.Y - SystemInformation.DragSize.Height / 2), SystemInformation.DragSize);
                if (!dragRect.Contains(new Point(e.X, e.Y)))
                {
                    object data = gridView1.GetDataRow(downHitInfo.RowHandle);
                    if (data != null)   //DataSource是个表
                    {
                        sourceRowIndex = downHitInfo.RowHandle;
                        List<DataRow> row = new List<DataRow>();

                        if(false)// (ShowSelectionColumn)
                        {
                            for (int i = 0; i < gridView1.DataRowCount; i++)
                            {
                                if (GetSelectedColumnChecked(i))
                                {
                                    row.Add(gridView1.GetDataRow(i));
                                }
                            }
                        }
                        else
                        {
                            int[] rows = gridView1.GetSelectedRows();
                            if (rows.Length > 0)
                            {
                                for (int i = 0; i < rows.Length; i++)
                                {
                                    row.Add(gridView1.GetDataRow((int)rows[i]));
                                }
                            }
                            else
                            {
                                row.Add(gridView1.GetDataRow(downHitInfo.RowHandle));
                            }
                        }
                        DragDropEffects dropeffect = this.DoDragDrop(row, DragDropEffects.Move);
                    }
                    else           //不是表,如数组等
                    {
                        // MessageBox.Show("ll");
                    }
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// 拖动进入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EFDevGrid_DragEnter(object sender, DragEventArgs e)
        {
            if (downHitInfo != null)
            {
                e.Effect = DragDropEffects.Move;
            }
        }
        /// <summary>
        /// 拖动结束
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EFDevGrid_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                isDraging = true;
                DragDropFunction(sender, e);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine( ex.Message);
            }
            finally
            {
                //去掉那根线 - 刷新,触发Pain事件
                destRowIndex = -1;
                Refresh();
                isDraging = false;
            }
        }
        private void DragDropFunction(object sender, DragEventArgs e)
        {
            GridControl gridSender = sender as GridControl;
            DataTable tableSource = null;
            //DataSource是表,是数据集,是bingSource
            if (gridSender.DataSource is DataTable)
            {
                tableSource = gridSender.DataSource as DataTable;
            }
            else if (gridSender.DataSource is DataSet)
            {
                DataSet ds = gridSender.DataSource as DataSet;
                if (ds.Tables.Contains(gridSender.DataMember))
                {
                    tableSource = ds.Tables[gridSender.DataMember];
                }
            }
            else if (gridSender.DataSource is System.Windows.Forms.BindingSource)
            {
                object obj = (gridSender.DataSource as System.Windows.Forms.BindingSource).DataSource;
                if (obj is DataSet)
                {
                    DataSet ds = obj as DataSet;
                    string tableName = (gridSender.DataSource as System.Windows.Forms.BindingSource).DataMember;
                    if (ds.Tables.Contains(tableName))
                    {
                        tableSource = ds.Tables[tableName];
                    }
                }
                else if (obj is DataTable)
                {
                    tableSource = obj as DataTable;
                }
            }
            else if (gridSender.DataSource is System.Array)
            {
                //当是DataSource时 数组时
                DragOverArray();
            }
            List<DataRow> rowParamList = e.Data.GetData(typeof(List<DataRow>)) as List<DataRow>;
            if (destRowIndex < 0 || destRowIndex == sourceRowIndex)
            {
                System.Console.WriteLine("Target and source  is  indentical.");
                return;
            }
            if (false)// (ShowSelectionColumn)
            {
                int temp = destRowIndex;
                for (int i = 0; i < temp; i++)
                {
                    if (GetSelectedColumnChecked(i))
                    {
                        destRowIndex--;
                    }
                }
            }
            else
            {
                 int temp = destRowIndex;
                 for (int i = 0; i < temp; i++)
                 {
                     if(gridView1.IsRowSelected(i))
                         destRowIndex--;
                 }
                //if (destRowIndex > sourceRowIndex)
                //{
                //    destRowIndex--;
                //}
            }
            bool blCheck = false;
            bool blEditable = true;
            try
            {
                blCheck=GetSelectedColumnChecked(sourceRowIndex);
                blEditable = GetRowEditable(sourceRowIndex);

                SetSelectedColumnChecked(sourceRowIndex,false);
            }
            catch { }
            if (rowParamList != null && tableSource != null)
            {
                if (rowParamList.Count > 0)
                {
                    List<DataRow> rowDest = new List<DataRow>();
                    List<DataRowState> rowDestState = new List<DataRowState>();
                    for (int i = 0; i < rowParamList.Count; i++)
                    {
                        DataRow dr = tableSource.NewRow();
                        for (int col = 0; col < rowParamList[i].Table.Columns.Count; col++)
                        {
                            dr[col] = rowParamList[i][col];
                        }
                        DataRowState drs = rowParamList[i].RowState;
                        tableSource.Rows.Remove(rowParamList[i]);
                        //移除并保存原有行信息--
                        rowDest.Add(dr);
                        rowDestState.Add(drs);
                    }
                    //移动行
                    for (int i = 0; i < rowDest.Count; i++)
                    {
                        tableSource.Rows.InsertAt(rowDest[i], destRowIndex + i);
                        rowDest[i].AcceptChanges();

                        if (DataRowState.Added == rowDestState[i])
                        {
                            rowDest[i].SetAdded();
                        }
                        else if (DataRowState.Modified == rowDestState[i])
                        {
                            rowDest[i].SetModified();
                        }
                        gridView1.FocusedRowHandle = destRowIndex + i;


                        System.Console.WriteLine("from" + sourceRowIndex + "to:" + destRowIndex + "blCheck" + blCheck);
                        //移动行的选择列的值
                        try
                        {
                            if (false)// (ShowSelectionColumn)
                            {
                                blCheck = true;
                            }
                            SetSelectedColumnChecked(destRowIndex + i, blCheck);
                            SetRowEditable(destRowIndex + i, blEditable);
                        }
                        catch { }
                    }
                }
            }
        }

        private void DragOverArray()
        {
            // MessageBox.Show("ok");
        }
        /// <summary>
        /// 拖动时移动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EFDevGrid_DragOver(object sender, DragEventArgs e)
        {
            try
            {
                Point p = this.PointToClient(new Point(e.X, e.Y));
                GridView view = this.GetViewAt(p) as GridView;
                if (view == null)
                    return;
                GridHitInfo downHitInfoTemp = view.CalcHitInfo(p);
                //DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo downHitInfoTemp
                //    = (DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo)this.MainView.CalcHitInfo(p);
                if (downHitInfoTemp.HitTest == GridHitTest.EmptyRow)
                {
                    destRowIndex = gridView1.DataRowCount;  
                }
                else
                {
                    destRowIndex = downHitInfoTemp.RowHandle;
                }
                //destRowIndex = downHitInfoTemp.RowHandle;
                if (autoScrollHelper != null)
                {
                    autoScrollHelper.ScrollIfNeeded();
                }
                else
                {
                    autoScrollHelper = new AutoScrollHelper(gridView1);
                    autoScrollHelper.ScrollIfNeeded();
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }
            finally
            {
                //会画那根线,刷新,触发Pain事件
                Refresh();
            }
        }

        private void EFDevGrid_Paint(object sender, PaintEventArgs e)
        {
            // SetGridPageBar();
            //System.Console.WriteLine("paint"); ;
            //if (this.DefaultView is DevExpress.XtraGrid.Views.Grid.GridView)
            //{
            //    if (((DevExpress.XtraGrid.Views.Grid.GridView)DefaultView).Columns.ColumnByFieldName(checkOptionColumnName) != null)
            //    {
            //        ((DevExpress.XtraGrid.Views.Grid.GridView)DefaultView).Columns.ColumnByFieldName(checkOptionColumnName).Visible = ShowSelectedColumn;
            //    }
            //}
            if (destRowIndex < 0)
            {
                return;
            }
            GridControl grid = (GridControl)sender;
            GridView view = (GridView)grid.MainView;

            bool isBottomLine = (destRowIndex == view.DataRowCount);
            int rowNowIndex = destRowIndex;
            if (isBottomLine)
                rowNowIndex--;
            //if (!isBottomLine && destRowIndex > sourceRowIndex)
            //{
            //     rowNowIndex = destRowIndex + 1;
            //}
            GridViewInfo viewInfo = view.GetViewInfo() as GridViewInfo;
            //如果是向下移,横线,下移
            GridRowInfo rowInfo = viewInfo.GetGridRowInfo(rowNowIndex);
           
            if (rowInfo == null) return;

            Point p1, p2;
            if (isBottomLine)
            {
                p1 = new Point(rowInfo.Bounds.Left, rowInfo.Bounds.Bottom - 1);
                p2 = new Point(rowInfo.Bounds.Right, rowInfo.Bounds.Bottom - 1);
            }
            else
            {
                p1 = new Point(rowInfo.Bounds.Left, rowInfo.Bounds.Top - 1);
                p2 = new Point(rowInfo.Bounds.Right, rowInfo.Bounds.Top - 1);
            }
            e.Graphics.DrawLine(new Pen(Brushes.Black, 2), p1, p2);
        }

        /// <summary>
        /// 行滚动辅助类
        /// </summary>
        internal class AutoScrollHelper
        {
            public AutoScrollHelper(GridView view)
            {
                fGrid = view.GridControl;
                fView = view;
                fScrollInfo = new ScrollInfo(this, view);
            }

            GridControl fGrid;
            GridView fView;
            ScrollInfo fScrollInfo;
            public int ThresholdInner = 20;
            public int ThresholdOutter = 100;
            public int HorizontalScrollStep = 10;
            public int ScrollTimerInterval
            {
                get
                {
                    return fScrollInfo.scrollTimer.Interval;
                }
                set
                {
                    fScrollInfo.scrollTimer.Interval = value;
                }
            }

            public void ScrollIfNeeded()
            {
                Point pt = fGrid.PointToClient(Control.MousePosition);
                Rectangle rect = fView.ViewRect;
                fScrollInfo.GoLeft = (pt.X > rect.Left - ThresholdOutter) && (pt.X < rect.Left +
    ThresholdInner);
                fScrollInfo.GoRight = (pt.X > rect.Right - ThresholdInner) && (pt.X < rect.Right +
    ThresholdOutter);
                fScrollInfo.GoUp = (pt.Y < rect.Top + ThresholdInner) && (pt.Y > rect.Top - ThresholdOutter);
                fScrollInfo.GoDown = (pt.Y > rect.Bottom - ThresholdInner) && (pt.Y < rect.Bottom +
    ThresholdOutter);
                //            Console.WriteLine("{0} {1} {2} {3} {4}", pt, fScrollInfo.GoLeft, fScrollInfo.GoRight,
                //fScrollInfo.GoUp, fScrollInfo.GoDown);
            }

            internal class ScrollInfo
            {
                internal Timer scrollTimer;
                GridView view = null;
                bool left, right, up, down;

                AutoScrollHelper owner;
                public ScrollInfo(AutoScrollHelper owner, GridView view)
                {
                    this.owner = owner;
                    this.view = view;
                    this.scrollTimer = new Timer();
                    this.scrollTimer.Tick += new EventHandler(scrollTimer_Tick);
                }
                public bool GoLeft
                {
                    get { return left; }
                    set
                    {
                        if (left != value)
                        {
                            left = value;
                            CalcInfo();
                        }
                    }
                }
                public bool GoRight
                {
                    get { return right; }
                    set
                    {
                        if (right != value)
                        {
                            right = value;
                            CalcInfo();
                        }
                    }
                }
                public bool GoUp
                {
                    get { return up; }
                    set
                    {
                        if (up != value)
                        {
                            up = value;
                            CalcInfo();
                        }
                    }
                }
                public bool GoDown
                {
                    get { return down; }
                    set
                    {
                        if (down != value)
                        {
                            down = value;
                            CalcInfo();
                        }
                    }
                }
                private void scrollTimer_Tick(object sender, System.EventArgs e)
                {
                    owner.ScrollIfNeeded();

                    if (GoDown)
                        view.TopRowIndex++;
                    if (GoUp)
                        view.TopRowIndex--;
                    if (GoLeft)
                        view.LeftCoord -= owner.HorizontalScrollStep;
                    if (GoRight)
                        view.LeftCoord += owner.HorizontalScrollStep;

                    if ((Control.MouseButtons & MouseButtons.Left) == MouseButtons.None)
                        scrollTimer.Stop();
                }
                void CalcInfo()
                {
                    if (!(GoDown && GoLeft && GoRight && GoUp))
                        scrollTimer.Stop();

                    if (GoDown || GoLeft || GoRight || GoUp)
                        scrollTimer.Start();
                }


            }
        }
        #endregion
 

		#region  显示  
        /// <summary>
        /// 设置grid数据(前提是EFDevGrid的数据源不为空)
        /// </summary>
        /// <param name="inblk">要设置的数据</param>
		public void SetGridValue(DataSet inblk)
		{
            if (inblk == null)
            {
                //清空数据
                gridView1.SelectAll();
                gridView1.DeleteSelectedRows();
                return;
            }
            if (inblk.Tables.Count== 0)
                return;
			if (DataSource is DataSet)
			{
                DataSource = inblk;
			}
			else if (DataSource is DataTable)
			{
                //如果值表的话,直接设置数据源为当前表
                DataSource = inblk.Tables[0];
			}
		}
		#endregion

		#region  获取grid的值
        /// <summary>
        /// 获取Grid的值对应的数据集(没有则新建)
        /// </summary>
        /// <returns>返回数据源对应的数据集</returns>
		public DataSet GetGridValue()
		{
            DataTable dt=  GetDataSourceTable();
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            return ds;
            //DataSet ds = null;
            //if (DataSource is DataTable)
            //{
            //    ds = new DataSet();
            //    ds.Tables.Add(DataSource as DataTable);
            //}
            //else if (DataSource is DataSet)
            //{
            //    ds = DataSource as DataSet;
            //}
            //else if (DataSource is BindingSource)
            //{
            //    object datasource1 = (DataSource as BindingSource).DataSource;
            //    if (datasource1 is DataTable)
            //    {
            //        ds = new DataSet();
            //        ds.Tables.Add(datasource1 as DataTable);
            //    }
            //    else if (datasource1 is DataSet)
            //    {
            //        ds = datasource1 as DataSet;
            //    }
            //}
            //return ds;
		}
		#endregion
        /// <summary>
        /// EPEDExcel 
        /// </summary>
        public enum EPEDEXCELOption
        {
            None,
            Import,
            Export,
            Both
        };
        private EPEDEXCELOption _epedexcel = EPEDEXCELOption.Both;
        [DefaultValue(EPEDEXCELOption.Both)]
        [Description("该grid是否支持使用Epedexcel导入导出")]
        [Category("EF")]
        public EPEDEXCELOption Epedexcel
        {
            get { return _epedexcel; }
            set { _epedexcel = value; }
        }
	}

    #region 弹出菜单,辅助类
    enum popUpMenuStripName
    {
        choose,//选择
        unChoose,//不选择
        chooseAll, //全部选择
        unChooseAll, //全部不选择
        addNew, //新增
        addCopyNew, //复制新增
        delete,//删除
        saveAs,//导出
        saveConfig,//保存配置
        showGroupPanel,//分组栏
        showFilterRow,//过滤行
        setColumnFixed,//设置列停靠
        refresh,//刷新
        gotoPage,//跳转
        copySelectData, //复制选择的数据
        copySelectRows, //复制选择的数据 所在行
        copySelectCols, //复制选择的数据  所在列
        none
    }
    internal class GridViewCustomMenu : GridViewMenu
    {
        internal class MenuInfo
        {
            public MenuInfo()
            {
                this.MenuItemName = popUpMenuStripName.none;
                this.Visible = true;
                this.Enable = true;
            }
            public MenuInfo(popUpMenuStripName menuName)
            {
                this.MenuItemName = menuName;
                this.Visible = true;
                this.Enable = true;
            }
            public MenuInfo(popUpMenuStripName menuName, bool visible, bool enable)
            {
                this.MenuItemName = menuName;
                this.Visible = visible;
                this.Enable = enable;
            }
            public popUpMenuStripName MenuItemName;
            public bool Visible = true;
            public bool Enable = true;
            public int ImageIndex = 0;
        }
        public void RefreshPopupMenuStrip(popUpMenuStripName menu, bool enable, bool visible)
        {
            for (int i = 0; i < Items.Count; i++)
            {
                if (Items[i].Tag is GridViewCustomMenu.MenuInfo)
                {
                    GridViewCustomMenu.MenuInfo temp = Items[i].Tag as GridViewCustomMenu.MenuInfo;
                    if (temp.MenuItemName == menu)
                    {
                        temp.Visible = visible;
                        temp.Enable = enable;
                        Items[i].Tag = temp;
                        break;
                    }
                }
            }
        }

        public GridViewCustomMenu(DevExpress.XtraGrid.Views.Grid.GridView view) : base(view) { }
        public GridViewCustomMenu(DevExpress.XtraGrid.Views.Grid.GridView view,ImageList imageList) : base(view) {
            this.imageList = imageList;
        }

        private ImageList imageList = null;
        // Create menu items.
        // This method is automatically called by the menu's public Init method.
        protected override void CreateItems()
        {
            Items.Clear();
            DXMenuItem itemTemp = null;
            itemTemp = new DXMenuItem("选择");//, new EventHandler(test), GridMenuImages.Column.Images[0]);//选择
            itemTemp.Tag = new MenuInfo(popUpMenuStripName.choose);
            Items.Add(itemTemp);
            itemTemp = new DXMenuItem("不选");//, new EventHandler(test), GridMenuImages.Column.Images[0]) ;//不选
            itemTemp.Tag = new MenuInfo(popUpMenuStripName.unChoose);
            Items.Add(itemTemp);

            itemTemp = new DXMenuItem("全选");//, new EventHandler(test), GridMenuImages.Column.Images[0]);//全选
            itemTemp.Tag = new MenuInfo(popUpMenuStripName.chooseAll);
            itemTemp.BeginGroup = true;
            Items.Add(itemTemp);


            itemTemp = new DXMenuItem("全不选");//, new EventHandler(test), GridMenuImages.Column.Images[0]);//全不选
            itemTemp.Tag = new MenuInfo(popUpMenuStripName.unChooseAll);
            Items.Add(itemTemp);


            itemTemp = new DXMenuItem("新增");//, new EventHandler(test), GridMenuImages.Column.Images[0]);//新增
            itemTemp.Tag = new MenuInfo(popUpMenuStripName.addNew);
            itemTemp.BeginGroup = true;
            if (imageList != null && imageList.Images.Count > 9)
            {
                itemTemp.Image = imageList.Images[9];
            }
            Items.Add(itemTemp);

            itemTemp = new DXMenuItem("复制新增");//, new EventHandler(test), GridMenuImages.Column.Images[0]);//复制新增
            itemTemp.Tag = new MenuInfo(popUpMenuStripName.addCopyNew);
            if (imageList != null && imageList.Images.Count > 11)
            {
                itemTemp.Image = imageList.Images[11];
            }
            Items.Add(itemTemp);

            //itemTemp = new DXMenuItem("删除");//, new EventHandler(test), GridMenuImages.Column.Images[0]);//删除
            //itemTemp.Tag = new MenuInfo(popUpMenuStripName.delete);
            //if (imageList != null && imageList.Images.Count > 10)
            //{
            //    itemTemp.Image = imageList.Images[10];
            //}
            //Items.Add(itemTemp);

            itemTemp = new DXMenuItem("导出");//, new EventHandler(test), GridMenuImages.Column.Images[0]);//另存为
            itemTemp.Tag = new MenuInfo(popUpMenuStripName.saveAs);
            if (imageList != null && imageList.Images.Count > 6)
            {
                itemTemp.Image = imageList.Images[6];
            }
            itemTemp.BeginGroup = true;
            Items.Add(itemTemp);

            itemTemp = new DXMenuItem("复制"); 
            itemTemp.Tag = new MenuInfo(popUpMenuStripName.copySelectData);
            Items.Add(itemTemp);

            itemTemp = new DXMenuItem("复制行"); 
            itemTemp.Tag = new MenuInfo(popUpMenuStripName.copySelectRows);
            Items.Add(itemTemp);
            itemTemp = new DXMenuItem("复制列"); 
            itemTemp.Tag = new MenuInfo(popUpMenuStripName.copySelectCols);
            Items.Add(itemTemp);

            itemTemp = new DXMenuItem("设置列停靠");
            itemTemp.Tag = new MenuInfo(popUpMenuStripName.setColumnFixed);
            Items.Add(itemTemp);

            //itemTemp = new DXMenuItem("保存配置");//, new EventHandler(test), GridMenuImages.Column.Images[0]);//另存为
            //itemTemp.Tag = new MenuInfo(popUpMenuStripName.saveConfig);
            //if (imageList != null && imageList.Images.Count > 8)
            //{
            //    itemTemp.Image = imageList.Images[8];
            //}
            //Items.Add(itemTemp);

            //itemTemp = new DXMenuItem("刷新");//, new EventHandler(test), GridMenuImages.Column.Images[0]);//另存为
            //itemTemp.Tag = new MenuInfo(popUpMenuStripName.refresh);
            //if (imageList != null && imageList.Images.Count > 7)
            //{
            //    itemTemp.Image = imageList.Images[7];
            //}
            //Items.Add(itemTemp);

            //itemTemp = new DXMenuItem("跳转");//, new EventHandler(test), GridMenuImages.Column.Images[0]);//另存为
            //itemTemp.Tag = new MenuInfo(popUpMenuStripName.gotoPage);
            //if (imageList != null && imageList.Images.Count > 12)
            //{
            //    itemTemp.Image = imageList.Images[12];
            //}
            //Items.Add(itemTemp);

            //itemTemp = new DXMenuItem("过滤行");//, new EventHandler(test), GridMenuImages.Column.Images[0]);//另存为
            //itemTemp.Tag = new MenuInfo(popUpMenuStripName.gotoPage);
            //if (imageList != null && imageList.Images.Count > 4)
            //{
            //    itemTemp.Image = imageList.Images[4];
            //}
            //Items.Add(itemTemp);

            //itemTemp = new DXMenuItem("分组");//, new EventHandler(test), GridMenuImages.Column.Images[0]);//另存为
            //itemTemp.Tag = new MenuInfo(popUpMenuStripName.gotoPage);
            //if (imageList != null && imageList.Images.Count > 5)
            //{
            //    itemTemp.Image = imageList.Images[5];
            //}
            //Items.Add(itemTemp);
        }
        protected override void OnBeforePopup()
        {
            for (int i = 0; i < Items.Count; i++)
            {
                if (Items[i].Tag is MenuInfo)
                {
                    MenuInfo temp = Items[i].Tag as MenuInfo;
                    Items[i].Visible = temp.Visible;
                    //横线..BeginGroup..是否显示分组
                    if ((temp.MenuItemName == popUpMenuStripName.chooseAll || temp.MenuItemName == popUpMenuStripName.addNew
                              || temp.MenuItemName == popUpMenuStripName.saveAs) && i > 1)
                    {
                        if (Items[i - 1].Visible || Items[i - 2].Visible)
                        {
                            Items[i].BeginGroup = true;
                        }
                        else
                        {
                            Items[i].BeginGroup = false;
                        }
                    }
                    Items[i].Enabled = temp.Enable;
                }
            }
            //解决线的问题
            base.OnBeforePopup();
        }
        protected override void OnMenuItemClick(object sender, EventArgs e)
        {
            base.OnMenuItemClick(sender, e);
        }
    }
    #endregion
}
