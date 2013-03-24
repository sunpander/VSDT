using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace EF
{
    internal class GridCheckMarksSelection
    {
        protected GridView view;
        protected ArrayList selection;
        private GridColumn column;
        private RepositoryItemCheckEdit edit;
        public bool EFMultiSelect = true;
        public GridCheckMarksSelection()
            : base()
        {
            selection = new ArrayList();
        }

        public GridCheckMarksSelection(GridView view)
            : this()
        {
            View = view;
        }
        private string selectionFiledName = "54EA8C67-D921-4ddc-8ECE-114E2459816C";
        public string SelectionFiledName
        {
            get { return selectionFiledName; }
        }
        protected virtual void Attach(GridView view)
        {
            if (view == null) return;
            selection.Clear();
            this.view = view;
            edit = view.GridControl.RepositoryItems.Add("CheckEdit") as RepositoryItemCheckEdit;
            edit.EditValueChanged += new EventHandler(edit_EditValueChanged);
            if (view is DevExpress.XtraGrid.Views.BandedGrid.BandedGridView || view is DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView)
            {
                column =  view.Columns.Add();
            }
            else
            {
                column = new GridColumn(); 
            }
            column.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            column.VisibleIndex = int.MaxValue;
            //column.FieldName = "selected";
            column.FieldName = SelectionFiledName;
            column.Caption = " ";
            column.OptionsColumn.ShowCaption = false;
            column.UnboundType = DevExpress.Data.UnboundColumnType.Boolean;
            column.ColumnEdit = edit;
            column.Fixed = FixedStyle.Left;
            column.Width = 35;
            view.FixedLineWidth = 1;
            view.CustomDrawColumnHeader += new
ColumnHeaderCustomDrawEventHandler(View_CustomDrawColumnHeader);
            view.CustomDrawGroupRow += new
RowObjectCustomDrawEventHandler(View_CustomDrawGroupRow);
            view.CustomUnboundColumnData += new
CustomColumnDataEventHandler(view_CustomUnboundColumnData);
            //  view.RowStyle += new RowStyleEventHandler(view_RowStyle);
            view.MouseDown += new MouseEventHandler(view_MouseDown);
            //view.MouseUp += new MouseEventHandler(view_MouseUp);
            //view.Click += new EventHandler(view_Click);

            edit.Tag = true;
        }
        void view_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                DoClickOnCheckEdit();
            }
        }

        protected virtual void Detach()
        {
            if (view == null) return;
            if (column != null)
                column.Dispose();
            if (edit != null)
            {
                view.GridControl.RepositoryItems.Remove(edit);
                edit.Dispose();
            }

            view.CustomDrawColumnHeader -= new
ColumnHeaderCustomDrawEventHandler(View_CustomDrawColumnHeader);
            view.CustomDrawGroupRow -= new
RowObjectCustomDrawEventHandler(View_CustomDrawGroupRow);
            view.CustomUnboundColumnData -= new
CustomColumnDataEventHandler(view_CustomUnboundColumnData);
            //view.RowStyle -= new RowStyleEventHandler(view_RowStyle);
            view.MouseDown -= new MouseEventHandler(view_MouseDown);
            //view.Click -= new EventHandler(view_Click);
            view = null;
        }

        private Point downPoint = new Point(-1, 0);

        private void DoClickOnCheckEdit()
        {
            if (column.OptionsColumn.AllowEdit == false || column.OptionsColumn.ReadOnly)
            {
                return;
            }
            DateTime timeNow = DateTime.Now;
            GridHitInfo info;
            Point pt = view.GridControl.PointToClient(Control.MousePosition);
            info = view.CalcHitInfo(pt);
            if (info.InColumn && info.Column == column)
            {
                if (SelectedCount == view.DataRowCount)
                    ClearSelection();
                else
                    SelectAll();
            }
            if (info.InRow && view.IsGroupRow(info.RowHandle) && info.HitTest != GridHitTest.RowGroupButton)
            {
                bool selected = IsGroupRowSelected(info.RowHandle);
                SelectGroup(info.RowHandle, !selected);
            }
            // System.Console.WriteLine("total spant " + (DateTime.Now - timeNow).TotalMilliseconds);

        }

        protected void DrawCheckBox(Graphics g, Rectangle r, bool Checked)
        {
            DevExpress.XtraEditors.ViewInfo.CheckEditViewInfo info;
            DevExpress.XtraEditors.Drawing.CheckEditPainter painter;
            DevExpress.XtraEditors.Drawing.ControlGraphicsInfoArgs args;
            info = edit.CreateViewInfo() as DevExpress.XtraEditors.ViewInfo.CheckEditViewInfo;
            painter = edit.CreatePainter() as DevExpress.XtraEditors.Drawing.CheckEditPainter;
            info.EditValue = Checked;
            info.Bounds = r;
            info.CalcViewInfo(g);
            args = new DevExpress.XtraEditors.Drawing.ControlGraphicsInfoArgs(info, new
DevExpress.Utils.Drawing.GraphicsCache(g), r);
            painter.Draw(args);
            args.Cache.Dispose();
        }

        public DataTable GetSelectedDataRow(DataTable result)
        {
            int temp = 0;
            //先对selection内的数据进行 排序
            DataTable dtResult = GetDataSource();
            for (int i = 0; i < selection.Count; i++)
            {
                DataRow dr0 = (selection[0] as DataRowView).Row;
                temp = dtResult.Rows.IndexOf(dr0);
                if (i >= 0)
                {
                    DataRow dr1 = (selection[i] as DataRowView).Row;
                    if (temp > dtResult.Rows.IndexOf(dr1))
                    {
                        object obj = selection[i];
                        selection.RemoveAt(i);
                        selection.Insert(0, obj);
                    }
                }
            }
            temp = 0;
            for (int i = 0; i < selection.Count; i++)
            {
                DataRow dr = (selection[i] as DataRowView).Row;
                if (dr == null || dr.RowState == DataRowState.Deleted || dr.RowState == DataRowState.Detached)
                    continue;
                DataRow dr2 = result.NewRow();
                for (int j = 0; j < dr.Table.Columns.Count; j++)
                {
                    temp = result.Columns.IndexOf(dr.Table.Columns[j].ColumnName);
                    if (temp >= 0)
                    {
                        dr2[temp] = dr[j];
                    }
                }
                result.Rows.Add(dr2);
            }
            return result;
        }
        private DataTable GetDataSource()
        {
            //获取数据源
            DataTable dtResult = new DataTable();
            GridControl control = view.GridControl;
            if (control == null)
                return dtResult;
            if (control.DataSource is System.Windows.Forms.BindingSource)
            {
                object obj = (control.DataSource as System.Windows.Forms.BindingSource).DataSource;
                if (obj is DataSet)
                {
                    DataSet ds = obj as DataSet;
                    string tableName = (control.DataSource as System.Windows.Forms.BindingSource).DataMember;
                    if (ds.Tables.Contains(tableName))
                    {
                        dtResult = ds.Tables[tableName];
                    }
                }
                else if (obj is DataTable)
                {
                    dtResult = obj as DataTable;
                }
            }
            else if (control.DataSource is DataSet)
            {
                DataSet ds = control.DataSource as DataSet;
                string tableName = control.DataMember;
                if (ds.Tables.Contains(tableName))
                {
                    dtResult = ds.Tables[tableName];
                }
            }
            else if (control.DataSource is DataTable)
            {
                dtResult = control.DataSource as DataTable;
            }

            return dtResult;

        }
        private void View_CustomDrawColumnHeader(object sender, ColumnHeaderCustomDrawEventArgs e)
        {
            if (e.Column == column)
            {
                e.Info.InnerElements.Clear();
                e.Painter.DrawObject(e.Info);
                bool tmp = (SelectedCount == view.DataRowCount && SelectedCount > 0);
                DrawCheckBox(e.Graphics, e.Bounds, tmp);
                e.Handled = true;
            }
        }

        private void View_CustomDrawGroupRow(object sender, RowObjectCustomDrawEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.ViewInfo.GridGroupRowInfo info;
            info = e.Info as DevExpress.XtraGrid.Views.Grid.ViewInfo.GridGroupRowInfo;

            info.GroupText = "         " + info.GroupText.TrimStart();
            e.Info.Paint.FillRectangle(e.Graphics, e.Appearance.GetBackBrush(e.Cache), e.Bounds);
            e.Painter.DrawObject(e.Info);

            Rectangle r = info.ButtonBounds;
            r.Offset(r.Width * 2, 0);
            DrawCheckBox(e.Graphics, r, IsGroupRowSelected(e.RowHandle));
            e.Handled = true;
        }

        public GridView View
        {
            get
            {
                return view;
            }
            set
            {
                if (view != value)
                {
                    Detach();
                    Attach(value);
                }
            }
        }

        public GridColumn CheckMarkColumn
        {
            get
            {
                if (column != null && column.ColumnEdit == null)
                {
                    edit = view.GridControl.RepositoryItems.Add("CheckEdit") as RepositoryItemCheckEdit;
                    edit.EditValueChanged += new EventHandler(edit_EditValueChanged);
                    column.ColumnEdit = edit;
                }
                return column;
            }
        }

        public int SelectedCount
        {
            get
            {
                return selection.Count;
            }
        }

        public object GetSelectedRow(int index)
        {
            return selection[index];
        }

        public int GetSelectedIndex(object row)
        {
            return selection.IndexOf(row);
        }

        public void ClearSelection()
        {
            selection.Clear();
            Invalidate();
        }

        private void Invalidate()
        {
            view.BeginUpdate();
            view.EndUpdate();
        }
        public void SelectAll()
        {
            selection.Clear();
            ICollection dataSource = view.DataSource as ICollection;
            int count = dataSource.Count;
            if (!EFMultiSelect && count > 1)
            {
                EFMessageBox.Show("只允许单选。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            DateTime timeNow = DateTime.Now;
            for (int i = 0; i < view.DataRowCount; i++)
            {
                view.SetRowCellValue(i, CheckMarkColumn, true);
            }
            //if (dataSource != null && count == view.DataRowCount)
            //    selection.AddRange(dataSource);  // fast
            //else
            //    for (int i = 0; i < view.DataRowCount; i++)  // slow
            //        selection.Add(view.GetRow(i));
            //Invalidate();
            System.Console.WriteLine("total spant " + (DateTime.Now - timeNow).TotalMilliseconds);
        }

        public void SelectGroup(int rowHandle, bool select)
        {
            if (IsGroupRowSelected(rowHandle) && select) return;
            int count = view.GetChildRowCount(rowHandle);
            if (!EFMultiSelect && select && count > 1)
            {
                EFMessageBox.Show("只允许单选。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            for (int i = 0; i < view.GetChildRowCount(rowHandle); i++)
            {
                int childRowHandle = view.GetChildRowHandle(rowHandle, i);
                if (view.IsGroupRow(childRowHandle))
                    SelectGroup(childRowHandle, select);
                else
                    //SelectRow(childRowHandle, select, false);
                    view.SetRowCellValue(childRowHandle, CheckMarkColumn, select);

            }
            Invalidate();
        }

        public void SelectRow(int rowHandle, bool select)
        {
            SelectRow(rowHandle, select, true);
        }

        private void SelectRow(int rowHandle, bool select, bool invalidate)
        {
            if (IsRowSelected(rowHandle) == select) return;
            if (!EFMultiSelect && select)
            {
                ClearSelection(); //先清空
            }
            object row = view.GetRow(rowHandle);
            if (select)
            {
                selection.Add(row);
            }
            else
            {
                selection.Remove(row);
            }
            if (invalidate)
            {
                //Invalidate();
            }
        }

        public bool IsGroupRowSelected(int rowHandle)
        {
            for (int i = 0; i < view.GetChildRowCount(rowHandle); i++)
            {
                int row = view.GetChildRowHandle(rowHandle, i);
                if (view.IsGroupRow(row))
                {
                    if (!IsGroupRowSelected(row)) return false;
                }
                else
                    if (!IsRowSelected(row)) return false;
            }
            return true;
        }

        public bool IsRowSelected(int rowHandle)
        {
            if (view.IsGroupRow(rowHandle))
                return IsGroupRowSelected(rowHandle);

            object row = view.GetRow(rowHandle);
            return GetSelectedIndex(row) != -1;
        }

        private void view_CustomUnboundColumnData(object sender, CustomColumnDataEventArgs e)
        {
            if (e.Column == CheckMarkColumn)
            {
                if (e.IsGetData)
                    e.Value = IsRowSelected(View.GetRowHandle(e.ListSourceRowIndex));
                else
                {
                    SelectRow(View.GetRowHandle(e.ListSourceRowIndex), (bool)e.Value);
                    //Invalidate();
                }
            }
        }
        private void edit_EditValueChanged(object sender, EventArgs e)
        {
            view.PostEditor();
        }
    }

    /// <summary>
    /// 不可编辑行,辅助类
    /// </summary>
    public class GridRowEditableHelp
    {
        protected GridView view;
        protected ArrayList unEditableRowList;

        public GridRowEditableHelp()
            : base()
        {
            unEditableRowList = new ArrayList();
        }

        public GridRowEditableHelp(GridView view)
            : this()
        {
            View = view;
        }
        public void ClearUnEditableRowList()
        {
            if (unEditableRowList != null)
                unEditableRowList.Clear();
        }
        public virtual void Attach(GridView view)
        {
            if (view == null) return;
            unEditableRowList.Clear();
            this.view = view;
            view.ShowingEditor -= new System.ComponentModel.CancelEventHandler(view_ShowingEditor);
            view.ShowingEditor += new System.ComponentModel.CancelEventHandler(view_ShowingEditor);
        }

        void view_ShowingEditor(object sender, System.ComponentModel.CancelEventArgs e)
        {
            int rowIndexNow = view.FocusedRowHandle;
            if (rowIndexNow < 0)
                return;
            if (!IsRowEditable(rowIndexNow))
                e.Cancel = true;
        }

        protected virtual void Detach()
        {
            if (view == null) return;
            view = null;
        }

        public GridView View
        {
            get
            {
                return view;
            }
            set
            {
                if (view != value)
                {
                    Detach();
                    Attach(value);
                }
            }
        }
        //public void SetRowEditable( bool editable)
        //{

        //}
        public void SetRowEditable(int rowHandle, bool editable)
        {
            if (rowHandle >= view.RowCount)
                return;
            if (IsRowEditable(rowHandle) == editable) return;
            object row = view.GetRow(rowHandle);
            if (!editable)
                unEditableRowList.Add(row);
            else
                unEditableRowList.Remove(row);
        }

        public bool IsRowEditable(int rowHandle)
        {
            object row = view.GetRow(rowHandle);
            int tmp = GetUnEditableRowIndex(row);
            if (tmp >= 0)
            {
                return false;
            }
            return true;
        }
        public int GetUnEditableRowIndex(object row)
        {
            return unEditableRowList.IndexOf(row);
        }
    }
}
