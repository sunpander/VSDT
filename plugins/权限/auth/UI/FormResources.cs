using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using DevExpress.XtraEditors;
using auth.Services;
using DevExpress.XtraTreeList.Nodes;
using EF;
using DevExpress.XtraBars;
using DevExpress.XtraTreeList;

namespace auth.UI
{
    /// <summary>
   /*样式:上面是画面,下面是按钮
    功能:画面管理,按钮管理...  按钮 :查询, 维护(grid上显示增删改),保存,取消
    后台服务:
    查询画面 ---------传入画面名,含模糊查询       ---返回执行结果
    查询按钮--传入画面名                  ---返回执行结果
    画面保存----------传入画面的新增,修改,删除信息---返回执行结果
    按钮保存----------传入按钮的新增,修改,删除信息---返回执行结果
    [菜单管理.其他资源管理.暂时先不做] 
    */
    /// </summary>
    public partial class FormResources : DevExpress.XtraEditors.XtraForm
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
     
  
        #endregion
        bool isManageMode = false;
        public FormResources()
        {
            InitializeComponent();
        }


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

            fgDevGridFormInfo.EmbeddedNavigator.Buttons.CustomButtons[DISCARD].Visible = false;
            fgDevGridFormInfo.EmbeddedNavigator.Buttons.CustomButtons[SAVE].Visible = false;
            fgDevGridButtInfo.EmbeddedNavigator.Buttons.CustomButtons[DISCARD].Visible = false;
            fgDevGridButtInfo.EmbeddedNavigator.Buttons.CustomButtons[SAVE].Visible = false;

            this.fgDevGridFormInfo.EmbeddedNavigator.ButtonClick += new NavigatorButtonClickEventHandler(FormInfoEmbeddedNavigator_ButtonClick);
            this.fgDevGridButtInfo.EmbeddedNavigator.ButtonClick += new NavigatorButtonClickEventHandler(ButtInfoEmbeddedNavigator_ButtonClick);
        }

  
        private void SetPageState(bool editMode)
        {
            barButtonItemEdit.Enabled = !editMode;
            barButtonItemSave.Enabled = editMode;
            barButtonItemCancel.Enabled = editMode;

            fgDevGridFormInfo.ShowAddRowButton = editMode;
            fgDevGridFormInfo.ShowAddCopyRowButton = editMode;
            fgDevGridFormInfo.ShowDeleteRowButton = editMode;
            fgDevGridFormInfo.SetAllColumnEditable(editMode);

            fgDevGridButtInfo.ShowAddRowButton = editMode;
            fgDevGridButtInfo.ShowAddCopyRowButton = editMode;
            fgDevGridButtInfo.ShowDeleteRowButton = editMode;
            fgDevGridButtInfo.SetAllColumnEditable(editMode);
            colFNAME.OptionsColumn.AllowEdit = false;
            colAPPNAME1.OptionsColumn.AllowEdit = false;

            fgDevGridFormInfo.EmbeddedNavigator.Buttons.CustomButtons[DISCARD].Visible = editMode;
            fgDevGridFormInfo.EmbeddedNavigator.Buttons.CustomButtons[SAVE].Visible = editMode;
            fgDevGridButtInfo.EmbeddedNavigator.Buttons.CustomButtons[DISCARD].Visible = editMode;
            fgDevGridButtInfo.EmbeddedNavigator.Buttons.CustomButtons[SAVE].Visible = editMode;

            fgDevGridFormInfo.ShowContextMenu = editMode;
            fgDevGridButtInfo.ShowContextMenu = editMode;

        }


        #region 查询
        /// <summary>
        /// 查询按钮
        /// </summary>
        private void QueryButton()
        {
            layoutControlGroup2.Text = "画面["+formName+"]按钮";
            if (formName == string.Empty) return;
            DataSet inblk = new DataSet();
            inblk.Tables.Add();
            inblk.Tables[0].Columns.Add("fname");
            inblk.Tables[0].Rows.Add(formName );

            DataTable dt = DbResource.QueryButtonInfo(inblk, CConstString.ConnectName);
            dt.AcceptChanges();
            fgDevGridButtInfo.DataSource = dt;
            gridViewButtInfo.BestFitColumns();
        }


        private void QueryForm()
        {
            DataSet inblk = new DataSet();
            inblk.Tables.Add();
            inblk.Tables[0].Columns.Add("name");
            inblk.Tables[0].Columns.Add("dllname");
            inblk.Tables[0].Columns.Add("appname");
            inblk.Tables[0].Rows.Add(fgtFormName.Text, fgtDllName.Text, "");

            DataTable dt = DbResource.QueryFormInfo(inblk, CConstString.ConnectName);
            dt.AcceptChanges();
            fgDevGridFormInfo.DataSource = dt;
            gridViewFormInfo.BestFitColumns();
            if (dt != null & dt.Rows.Count > 0)
            {
                formName = dt.Rows[0]["name"].ToString();
                QueryButton();
            }
        }
        #endregion


        #region 其他方法


        private void CancelEdit()
        {
            if (MessageBox.Show("是否退出维护模式？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                SetPageState(false);

                if (isChange(fgDevGridFormInfo))
                {
                    QueryForm();
                }

                if (isChange(fgDevGridButtInfo))
                {
                    QueryButton();
                }
            }
        }


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
        private bool formCheck()
        {
            //检查数据准确性
            for (int i = 0; i < this.gridViewFormInfo.RowCount; i++)
            {
                if (gridViewFormInfo.GetDataRow(i).RowState == DataRowState.Added || gridViewFormInfo.GetDataRow(i).RowState == DataRowState.Modified)
                {
                    if (/*gridViewFormInfo.GetRowCellValue(i, "selected") == null
                     || gridViewFormInfo.GetRowCellValue(i, "selected").ToString() == "False"*/
                        !fgDevGridFormInfo.GetSelectedColumnChecked(i))
                    {
                        //continue;
                    }
                    if (gridViewFormInfo.GetRowCellValue(i, "NAME") != null)
                    {
                        if (gridViewFormInfo.GetRowCellValue(i, "NAME").ToString().Trim() == "")
                        {
                            MessageBox.Show(EP.EPES.EPESC0000033/*画面名不能为空，请重新输入！*/, EP.EPES.EPESC0000034/*输入信息不合法*/, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return false;
                        }
                        string formname = gridViewFormInfo.GetRowCellValue(i, "NAME").ToString();
                        //for (int j = 0; j < formname.Length; j++)
                        //{
                        //    if (formname[j] < 48 || (formname[j] > 57 && formname[j] < 65) || formname[j] > 90)
                        //    {
                        //        MessageBox.Show(EP.EPES.EPESC0000121/*画面名必须是大写字母与数字的组合，请重新输入！*/, EP.EPES.EPESC0000034/*输入信息不合法*/, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        //        return false;
                        //    }
                        //}
                        if (formname.Length > 128)
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
                    }
                    else
                    {
                        MessageBox.Show(EP.EPES.EPESC0000035/*画面描述不能为空，请重新输入！*/, EP.EPES.EPESC0000034/*输入信息不合法*/, MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private bool btnCheck()
        {

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
                    //if (Utility.GetByteLength(gridViewButtInfo.GetRowCellValue(i, "NAME").ToString()) > 125)
                    //{
                    //    MessageBox.Show(EP.EPES.EPESC0000050/*按钮名不能超过125位，请重新输入！*/, EP.EPES.EPESC0000034/*输入信息不合法*/, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //    return false;
                    //}
                }
                else
                {
                    MessageBox.Show(EP.EPES.EPESC0000052/*按钮名不能为空，请重新输入！*/, EP.EPES.EPESC0000034/*输入信息不合法*/, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                //if (gridViewButtInfo.GetRowCellValue(i, "FNAME") != null)
                //{
                //    if (gridViewButtInfo.GetRowCellValue(i, "FNAME").ToString().Trim() == "")
                //    {
                //        MessageBox.Show(EP.EPES.EPESC0000192/*所属画面不能为空，请重新输入！*/, EP.EPES.EPESC0000034/*输入信息不合法*/, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //        return false;
                //    }
                //}
                //else
                //{
                //    MessageBox.Show(EP.EPES.EPESC0000192/*所属画面不能为空，请重新输入！*/, EP.EPES.EPESC0000034/*输入信息不合法*/, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    return false;
                //}


                if (gridViewButtInfo.GetRowCellValue(i, "DESCRIPTION") != null)
                {
                    if (gridViewButtInfo.GetRowCellValue(i, "DESCRIPTION").ToString().Trim() == "")
                    {
                        MessageBox.Show(EP.EPES.EPESC0000048/*按钮描述不能为空，请重新输入！*/, EP.EPES.EPESC0000034/*输入信息不合法*/, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                    //if (Utility.GetByteLength(gridViewButtInfo.GetRowCellValue(i, "DESCRIPTION").ToString()) > 75)
                    //{
                    //    MessageBox.Show(EP.EPES.EPESC0000047/*按钮描述不能超过75位，请重新输入！*/, EP.EPES.EPESC0000034/*输入信息不合法*/, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //    return false;
                    //}
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

        #endregion

        #region 保存
        private void SaveData()
        {
            bool succsee = true;

            if (isChange(fgDevGridFormInfo))
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

            if (isChange(fgDevGridButtInfo))
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

            if (succsee)
            {
                SetPageState(false);
            }
        }

        private bool SaveFormInfo()
        {
            DataTable dt = fgDevGridFormInfo.DataSource as DataTable;
            if (dt == null || dt.Rows.Count < 1)
                return false;

            DataTable instable = dt.Clone();
            DataTable deltable = dt.GetChanges(DataRowState.Deleted);
            DataTable updtable = dt.Clone();

            DataRow dr = null;
            for (int rowIndex = 0; rowIndex < gridViewFormInfo.RowCount; ++rowIndex)
            {
                if (fgDevGridFormInfo.GetSelectedColumnChecked(rowIndex))
                {
                    dr = gridViewFormInfo.GetDataRow(rowIndex);
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

            DataSet inBlock = new DataSet();
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
            if (inBlock.Tables.Count > 0)
            {
                string msg = DbResource.SaveFormInfo(inBlock, CConstString.ConnectName);
                MessageBox.Show(msg, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
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


        private bool SaveButtInfo()
        {
            DataTable dt = fgDevGridButtInfo.DataSource as DataTable;
            if (dt == null || dt.Rows.Count < 1)
                return false;

            DataTable instable = dt.Clone();
            DataTable deltable = dt.GetChanges(DataRowState.Deleted);
            DataTable updtable = dt.Clone();

            DataRow dr = null;
            for (int rowIndex = 0; rowIndex < gridViewButtInfo.RowCount; ++rowIndex)
            {
                if (fgDevGridButtInfo.GetSelectedColumnChecked(rowIndex))
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

            DataSet inBlock = new DataSet();
            if (instable != null && instable.Rows.Count > 0)
            {
                for (int i = 0; i < instable.Rows.Count; i++)
                {
                    instable.Rows[i]["fname"] = formName;
                    instable.Rows[i]["appname"] = CConstString.AppName;
                }
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
                for (int i = 0; i < updtable.Rows.Count; i++)
                {
                    updtable.Rows[i]["fname"] = formName;
                    updtable.Rows[i]["appname"] = CConstString.AppName;
                }
                updtable.TableName = "UPDATE_BLOCK";
                inBlock.Tables.Add(updtable);
            }
            if (inBlock.Tables.Count > 0)
            {
                string msg = DbResource.SaveButtonInfo(inBlock, CConstString.ConnectName);
                MessageBox.Show(msg, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            QueryButton();
            return true;
        }
        #endregion




        #region Grid下方分页条按钮事件
        void FormInfoEmbeddedNavigator_ButtonClick(object sender, NavigatorButtonClickEventArgs e)
        {
            gridViewFormInfo.PostEditor();
            switch (e.Button.ImageIndex)
            {
                case SAVE:
                    if (isChange(fgDevGridFormInfo))
                    {
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
                    }
                    break;
                case DISCARD:
                    QueryForm();
                    break;
                Default:
                    break;
            }
        }
        void ButtInfoEmbeddedNavigator_ButtonClick(object sender, NavigatorButtonClickEventArgs e)
        {
            fgDevGridButtInfo.Parent.Focus();
            switch (e.Button.ImageIndex)
            {
                case SAVE:
                    if (isChange(fgDevGridButtInfo))
                    {
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
                    }
                    break;
                case DISCARD:
                    QueryButton();
                    break;
                default:
                    break;
            }
        }
        #endregion

        private void Form1_Load(object sender, EventArgs e)
        {
            InitDevGridCustomButtons();
            SetPageState(false);
            //QueryForm();
        }

        #region 画面Grid焦点行改变 事件(重新查询所含按钮)
        private void gridViewFormInfo_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (gridViewFormInfo.FocusedRowHandle >= 0)
            {
                if (gridViewFormInfo.GetFocusedRowCellValue("NAME") == null)
                    return;
                formName = gridViewFormInfo.GetFocusedRowCellValue("NAME").ToString();
                QueryButton();
            }
        }
        #endregion

        #region 查询按钮事件
        private void btnQuery_Click(object sender, EventArgs e)
        {
            QueryForm();
        }
        #endregion

        #region 工具栏 按钮事件(编辑,保存,取消)
        private void barButtonItemEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SetPageState(true);
        }

        private void barButtonItemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                SaveData();
            }
            catch (Exception ex)
            {
                EF.EFMessageBox.Show(ex.Message);
            }
        }

        private void barButtonItemCancel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                CancelEdit();
            }
            catch (Exception ex)
            {
                EF.EFMessageBox.Show(ex.Message);
            }
        }
        #endregion



        #region 菜单树

        #region 查询

        //加载菜单树
        private void LoadTree()
        {
            TreeListNode treeNode = this.treeList.AppendNode(new object[] { EP.EPES.EPESC0000067/*主菜单*/ }, null);

            //treeNode.SelectImageIndex = FOLDERICON_EXPAND; //选中时的图标设为打开的文件夹
            treeNode.ImageIndex = treeNode.SelectImageIndex = FOLDERICON;
            string treeRoot =   "root";
            treeNode.Tag = treeRoot;

            DataTable outBlock = this.CallSelectService(" ", treeRoot, 0, 2, CConstString.AppName);//查询出父名为root的记录
        
            //作为root的子节点
            for (int i = 0; i <  outBlock.Rows.Count; i++)
            {
                string name = outBlock.Rows[i]["name"].ToString();
                string resname = outBlock.Rows[i]["resname"].ToString();
                string description =outBlock.Rows[i]["description"].ToString();

                //不显示“收藏夹”目录
                if (name != "MYFAVORITE")
                {
                    TreeListNode node = this.treeList.AppendNode(new object[] { description + "(" + name + ")" }, treeNode);
                    node.Tag = name;
                    if (resname == "FOLDER")
                    {
                        //node.SelectImageIndex = FOLDERICON_EXPAND; //选中时的图标设为打开的文件夹
                        node.ImageIndex = node.SelectImageIndex = FOLDERICON;
                    }
                    else
                    {
                        node.SelectImageIndex = node.ImageIndex = FORMICON;
                    }
                }

            }
            this.treeList.ExpandAll();
        }

        private DataTable CallSelectService(string name, string fname, long treeno, long mode, string cursystem)
        {
          
            DataSet inBlock = new DataSet();
            inBlock.Tables.Add();


            inBlock.Tables[0].Columns.Add("name");
            inBlock.Tables[0].Columns.Add("fname");
            inBlock.Tables[0].Columns.Add("treeno");
            inBlock.Tables[0].Columns.Add("mode");
            inBlock.Tables[0].Columns.Add("cursystem");

            inBlock.Tables[0].Rows.Add(name, fname, treeno, mode, cursystem);


            DataTable outBlock = DbTreeInfo.QueryTreeNode(inBlock, CConstString.ConnectName); 
            return outBlock;
        }

        //点击节点展开子节点
        private void treeList_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            if (e.Node == null
                || e.Node.Tag == null
                || e.Node.Nodes.Count > 0
                || e.Node.ImageIndex == FORMICON) return;

            queryChildNodes(e.Node);

        }

        //查询子节点
        private void queryChildNodes(TreeListNode parentNode)
        {
           parentNode.Nodes.Clear();
           DataTable outBlock = this.CallSelectService(" ", parentNode.Tag.ToString(), 0, 2, CConstString.AppName);
          for (int i = 0; i <  outBlock.Rows.Count; i++)
            {
                string name = outBlock.Rows[i]["name"].ToString();
                string resname = outBlock.Rows[i]["resname"].ToString();
                string description = outBlock.Rows[i]["description"].ToString();

                //不显示“收藏夹”目录
                if (name != "MYFAVORITE")
                {
                    if (resname == "FOLDER")
                    {
                        TreeListNode node = this.treeList.AppendNode(new object[] { description + "(" + name + ")" }, parentNode);
                        node.Tag = name;
                        //node.SelectImageIndex = FOLDERICON_EXPAND; //选中时的图标设为打开的文件夹
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

        //刷新主菜单
        private void popupTreeRefresh_ItemClick(object sender, ItemClickEventArgs e)
        {

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
                    if (hi.Node != null)
                    {
                        treeList.FocusedNode = hi.Node;

                        if (hi.Node.Level >= 0 && isManageMode)
                        {
                            this.popupMenuTree.ShowPopup(MousePosition);
                        }
                    }
                }

            }
        }

        //右键菜单管理
        private void popupMenuTree_BeforePopup(object sender, CancelEventArgs e)
        {

        }

        #endregion

        #region 新增目录

        //新增目录
        private void popupTreeAdd_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        #endregion

        #region 新增画面与节点排序
        bool dragFromGrid = false;
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
            if (isManageMode)
                e.Effect = DragDropEffects.Copy;
            else
            {
                e.Effect = DragDropEffects.None;
                EF.EFMessageBox.Show(EP.EPES.EPESC0000071/*操作失败！请进入维护模式进行排序操作！*/, EP.EPES.EPESC0000024/*提示*/, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void treeList_DragDrop(object sender, DragEventArgs e)
        {
            if (!isManageMode)
            {
                e.Effect = DragDropEffects.None;

                EF.EFMessageBox.Show(EP.EPES.EPESC0000071/*操作失败！请进入维护模式进行排序操作！*/, EP.EPES.EPESC0000024/*提示*/, MessageBoxButtons.OK, MessageBoxIcon.Error);

                TreeListNode parent = treeList.FocusedNode.ParentNode;

                queryChildNodes(parent);

                return;
            }
 
            DevExpress.XtraTreeList.TreeListHitInfo hi = treeList.CalcHitInfo(treeList.PointToClient(new Point(e.X, e.Y)));

            string[] format = e.Data.GetFormats();

            //拖拽的是菜单树中的节点——节点排序
            if (format[0] == "DevExpress.XtraTreeList.Nodes.TreeListNode")
            {
                if (hi != null)
                {
                    if (hi.Node != null)
                    {
                        TreeListNode dragnode = e.Data.GetData(typeof(TreeListNode)) as TreeListNode;
                        TreeListNode targetnode = hi.Node;

                        treeList.SetNodeIndex(dragnode, treeList.GetNodeIndex(targetnode));

                        OrderNodes(hi.Node.ParentNode);
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
                else
                {
                    parentNode = hi.Node;
                }
                treeseq = parentNode.Nodes.Count;

                //新增画面
                DataSet inBlock = new DataSet();
                inBlock.Tables.Add();


                inBlock.Tables[0].Columns.Add("fname");
                inBlock.Tables[0].Columns.Add("name");
                inBlock.Tables[0].Columns.Add("resname");
                inBlock.Tables[0].Columns.Add("description");
                inBlock.Tables[0].Columns.Add("shortcut");
                inBlock.Tables[0].Columns.Add("treeno");
                inBlock.Tables[0].Columns.Add("treeseq");
                inBlock.Tables[0].Columns.Add("userid");

                for (int i = 0, j = 1; i < this.gridViewFormInfo.RowCount; i++)
                {
                    //取出选中行
                    if (fgDevGridFormInfo.GetSelectedColumnChecked(i))
                    {
                        if (treeseq > 999)
                        {
                            EFMessageBox.Show(EP.EPES.EPESC0000072/*新增失败*/, EP.EPES.EPESC0000024/*提示*/, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        DataRow dr = inBlock.Tables[0].NewRow();

                        dr["name"] = parentNode.Tag + gridViewFormInfo.GetRowCellValue(i, "NAME").ToString();

                        dr["description"] = parentNode.Tag + gridViewFormInfo.GetRowCellValue(i, "DESCRIPTION").ToString();

                        dr["resname"] = parentNode.Tag + gridViewFormInfo.GetRowCellValue(i, "NAME").ToString();
                        dr["fname"] = parentNode.Tag;

                        dr["shortcut"] = " ";

                        dr["treeno"] = 0;
                        dr["treeseq"] = treeseq.ToString("d3");


                        treeseq++;
                        j++;
                    }
                }

               int flag  = DbTreeInfo.AddTreeNode(inBlock, CConstString.ConnectName);

             
                if ( flag == 0)
                {
                    queryChildNodes(parentNode);

                    //取消列表框中所有checkbox选中状态
                    for (int k = 0; k < gridViewFormInfo.DataRowCount; k++)
                    {
                        fgDevGridFormInfo.SetSelectedColumnChecked(k, false);// gridViewFormInfo..SetRowCellValue(k, gridColumnFormCheck, false);
                    }
                    this.gridViewFormInfo.Invalidate();
                }

                 
            }
        }

        //节点排序
        private void OrderNodes(TreeListNode parent)
        {
            DataSet inBlock = new DataSet();
            inBlock.Tables.Add();


            inBlock.Tables[0].Columns.Add("name");
            inBlock.Tables[0].Columns.Add("treeseq");

            for (int i = 0; i < parent.Nodes.Count; i++)
            {
                inBlock.Tables[0].Rows.Add(parent.Nodes[i].Tag, i.ToString("d3"));
            }



            int flag = DbTreeInfo.UpdateTreeNode(inBlock, CConstString.ConnectName);// EI.EITuxedo.CallService("epestree_upds", inBlock);

            if (flag == 0)
            {
                queryChildNodes(parent);
            }
        }

        #endregion

        #region 删除节点

        //删除节点
        private void popupTreeDelete_ItemClick(object sender, ItemClickEventArgs e)
        {

        }
        #endregion


        #endregion

    }
}
