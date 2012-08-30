using System;
using System.Data;
using System.Text;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Collections.Generic;

using SpreadsheetGear.Windows.Forms;
#if(Devxpress)
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
#endif
namespace WindowsFormsApplication1
{
	public partial class FormEPEDEXCEL : Form
	{
		#region Windows API Import Section
		[DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true, CharSet = CharSet.Unicode)]
		internal static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

		[DllImport("user32.dll", EntryPoint = "GetTopWindow", SetLastError = true, CharSet = CharSet.Unicode)]
		internal static extern IntPtr GetTopWindow(IntPtr hWnd);

		[DllImport("user32.dll", EntryPoint = "FindWindowEx", SetLastError = true, CharSet = CharSet.Unicode)]
		internal static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass,
			string lpszWindow);

		[DllImport("user32.dll", EntryPoint = "SetWindowPos", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

		[DllImport("user32.dll", EntryPoint = "GetWindowTextLength", SetLastError = true,
			CharSet = CharSet.Unicode)]
		internal static extern int GetWindowTextLength(IntPtr hWnd);

		[DllImport("user32.dll", EntryPoint = "GetWindowText", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

		[DllImport("user32.dll", EntryPoint = "GetClassName", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern int GetClassName(IntPtr hWnd, StringBuilder buf, int nMaxCount);

		[DllImport("user32.dll", EntryPoint = "SendMessage", SetLastError = true, CharSet = CharSet.Unicode)]
		internal static extern IntPtr SendMessage(IntPtr hWnd, uint wMsg, IntPtr wParam, string lParam);

		[DllImport("user32.dll", EntryPoint = "SendMessage", SetLastError = true, CharSet = CharSet.Auto)]
		internal static extern IntPtr SendMessage(IntPtr hWnd, uint wMsg, IntPtr wParam, IntPtr lParam);

		[DllImport("user32.dll", EntryPoint = "GetWindow", SetLastError = true)]
		internal static extern IntPtr GetWindow(IntPtr hWnd, uint wCmd);

		[DllImport("user32.dll", EntryPoint = "IsWindow", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool IsWindow(IntPtr hWnd);

		[DllImport("user32.dll", SetLastError = true)]
		internal static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
		[DllImport("user32.dll", SetLastError = true)]
		internal static extern int GetWindowLong(IntPtr hWnd, int nIndex);

		[DllImport("user32.dll", SetLastError = true)]
		internal static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);
		[DllImport("user32.dll", SetLastError = true)]
		internal static extern bool RemoveMenu(IntPtr hMenu, uint uPosition, uint uFlags);
		[DllImport("user32.dll", SetLastError = true)]
		internal static extern bool DrawMenuBar(IntPtr hWnd);
		[DllImport("user32.dll", SetLastError = true)]
		internal static extern uint GetMenuItemCount(IntPtr hMenu);

		[DllImport("user32.dll", SetLastError = true)]
		internal static extern bool DeleteMenu(IntPtr hMenu, uint uPosition, uint uFlags);

		[DllImport("user32.dll")]
		internal static extern bool GetCursorPos(out Point lpPoint);

		[DllImport("user32.dll")]
		internal static extern IntPtr WindowFromPoint(Point Point);

		[DllImport("user32.dll")]
		internal static extern IntPtr SetCapture(IntPtr hWnd);

		[DllImport("user32.dll")]
		internal static extern bool ReleaseCapture();
		#endregion

		#region Windows Message Declaration Section
		internal const uint SWP_NOACTIVATE = 0x10;
		internal const uint SWP_SHOWWINDOW = 0x40;
		#endregion

		private bool bStartCapture = false;
		private FormBorderStyle m_formBorderStype;
		private IntPtr m_WHandle = IntPtr.Zero;
		private Control m_ctrlGrid = null;
		private WorkbookView m_workbookView = null;

		public FormEPEDEXCEL()
		{
			// 该调用是 Windows 窗体设计器所必需的。
			InitializeComponent();
		}
		
		private System.Drawing.Size m_formSizeTemp;
		private System.Drawing.Point m_formPointTemp;
		private FormBorderStyle m_FormBorderStyle;

		private void efBtn_capture_MouseDown(object sender, MouseEventArgs e)
		{
			if (SetCapture(efBtn_capture.Handle) == null)
			{
				MessageBox.Show("SetCapture Failed");
				return;
			}
			Cursor = new Cursor( WindowsFormsApplication1.Properties.Resources.IconCapture.ToBitmap().GetHicon());
			efBtn_capture.Image = null;
			bStartCapture = true;
			timer1.Start();

			m_formSizeTemp = this.Size;
			m_formPointTemp = this.Location;
			this.Left = 0;
			this.Top = 0;

			this.Height = 5;
			this.Width = 5;
		}

		private void efBtn_capture_MouseUp(object sender, MouseEventArgs e)
		{
			bStartCapture = false;	
			if (!ReleaseCapture())
			{
				MessageBox.Show("ReleaseCapture Failed");
			}
			timer1.Stop();

			efBtn_capture.Image = WindowsFormsApplication1.Properties.Resources.IconCapture.ToBitmap();
			Cursor = Cursors.Default;
			 CreateGrid();

			this.Left = m_formPointTemp.X;
			this.Top = m_formPointTemp.Y;

			this.Height = m_formSizeTemp.Height;
			this.Width = m_formSizeTemp.Width;
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			if (bStartCapture)
			{
				Rectangle rect;
				Point p;
				Control ctrl = null;
				if (GetCursorPos(out p))
				{
					//获取鼠标处的window的handle
					IntPtr hwndCurWindow = WindowFromPoint(p);
					if (/*hwndCurWindow != m_WHandle && */hwndCurWindow != IntPtr.Zero)
					{
						try
						{
							if (m_WHandle != IntPtr.Zero)
							{
								if ((ctrl = Control.FromHandle(m_WHandle)) != null)
								{
									ctrl.Refresh();
								}
							}

							ctrl = Control.FromHandle(hwndCurWindow);

							if (ctrl.Controls.Count > 0)
							{
								Control ctrlCurrent = ctrl.Controls[0];
								Control ctrlOld = null;

								while (ctrlCurrent != null)
								{
									if (ctrlOld == null ||  ctrlOld.Parent != ctrlCurrent )
									{
										if (ctrlCurrent.Controls.Count > 0)
										{
											ctrlCurrent = ctrlCurrent.Controls[0];
											continue;
										}
									}
									// check it
									if (ctrlCurrent.Visible)
									{
#if(Devxpress)
										if (ctrlCurrent is  GridControl  )
                                        {
											rect = ctrlCurrent.RectangleToScreen(ctrlCurrent.DisplayRectangle);

											if (rect.Contains(p))
											{
												ctrl = ctrlCurrent;
												break;
											}
										}
#endif
                                        if (ctrlCurrent is DataGridView)
										{
											rect = ctrlCurrent.RectangleToScreen(ctrlCurrent.DisplayRectangle);

											if (rect.Contains(p))
											{
												ctrl = ctrlCurrent;
												break;
											}
										}
									}
									if (ctrlCurrent == ctrl)
									{
										break;
									}
									// for
									ctrlOld = ctrlCurrent;
									int index = ctrlCurrent.Parent.Controls.GetChildIndex(ctrlCurrent) + 1;
									if (index == ctrlCurrent.Parent.Controls.Count)
									{
										ctrlCurrent = ctrlCurrent.Parent;
									}
									else
									{
										ctrlCurrent = ctrlCurrent.Parent.Controls[index];
									}
								}
							}
                            if (ctrl != null)
                            {
                                bool ok = false;
#if(Devxpress)
                               if (  ctrl is GridControl )
                                {
                                   ok =true;
                                }
#endif
                                if (  ctrl is DataGridView )
                                {
                                    ok =true;
                                }
                                if (ok)
                                {
                                    m_WHandle = ctrl.Handle;
                                    efTB_gird_handle.Text = string.Format("{0}", m_WHandle.ToString("X"));

                                    Control ctrlParent = ctrl;
                                    while (ctrlParent != null)
                                    {
                                        if (ctrlParent is Form)
                                        {
                                            Form formBase = ctrlParent as Form;
                                            efTB_form_cname.Text = formBase.Text;
                                            efTB_form_ename.Text = formBase.Name;
                                            break;
                                        }
                                        ctrlParent = ctrlParent.Parent;
                                    }
                                    ControlPaint.DrawBorder(ctrl.CreateGraphics(), ctrl.DisplayRectangle, Color.BlueViolet, ButtonBorderStyle.Solid);
                                }
                            }
						}
						catch (Exception ex)
						{
							this.Text = "error:"+ex.Message;
						}
					}
				}
			}
		}

        private void CreateGrid()
        {
            Control ctrl = Control.FromHandle(m_WHandle);
            if (ctrl == null)
            {
                throw new Exception("请选择目标GRID");
            }
            ctrl.Refresh();
            try
            {
                m_ctrlGrid = null;
#if(Devxpress)
                if (ctrl is GridControl)
                {
                    GridControl efGridControlSource = ctrl as GridControl;
                    GridView gridViewSource = efGridControlSource.MainView as GridView;

                    m_ctrlGrid = new GridControl();
                    efGroupBox2.Controls.Clear();
                    efGroupBox2.Controls.Add(m_ctrlGrid);
                    m_ctrlGrid.Parent = efGroupBox2;
                    m_ctrlGrid.Visible = true;
                    m_ctrlGrid.Dock = DockStyle.Fill;

                    GridControl efGridControlDest = m_ctrlGrid as GridControl;

                    GridView gridViewDesc = new GridView(efGridControlDest);
                    efGridControlDest.MainView = gridViewDesc;
                    gridViewDesc.OptionsView.ShowGroupPanel = false;

                    gridViewDesc.OptionsView.AllowHtmlDrawHeaders
                        = gridViewSource.OptionsView.AllowHtmlDrawHeaders;

                    gridViewDesc.Appearance.HeaderPanel.TextOptions.Trimming
                        = gridViewSource.Appearance.HeaderPanel.TextOptions.Trimming;

                    gridViewDesc.Appearance.HeaderPanel.TextOptions.WordWrap
                        = gridViewSource.Appearance.HeaderPanel.TextOptions.WordWrap;

                    gridViewDesc.Appearance.HeaderPanel.Options.UseTextOptions
                        = gridViewSource.Appearance.HeaderPanel.Options.UseTextOptions;

                    System.Data.DataSet dsDataSource = null;
                    // 复制数据源
                    if (efGridControlSource.DataSource is BindingSource)
                    {
                        BindingSource bindingSource = efGridControlSource.DataSource as BindingSource;
                        if (bindingSource.DataSource is System.Data.DataSet)
                        {
                            dsDataSource = (bindingSource.DataSource as System.Data.DataSet).Clone();
                            efGridControlDest.DataMember = bindingSource.DataMember;
                        }
                        else if (bindingSource.DataSource is DataTable)
                        {
                            dsDataSource = new System.Data.DataSet();
                            DataTable dt = (bindingSource.DataSource as DataTable).Clone();
                            dsDataSource.Tables.Add(dt);
                            efGridControlDest.DataMember = dt.TableName;
                        }
                        else
                        {
                        }
                    }
                    else if (efGridControlSource.DataSource is System.Data.DataSet)
                    {
                        dsDataSource = (efGridControlSource.DataSource as System.Data.DataSet).Copy();
                        efGridControlDest.DataMember = efGridControlSource.DataMember;
                    }
                    else if (efGridControlSource.DataSource is System.Data.DataTable)
                    {
                        dsDataSource = new System.Data.DataSet();
                        DataTable dt = (efGridControlSource.DataSource as System.Data.DataTable).Copy();
                        dsDataSource.Tables.Add(dt);
                        efGridControlDest.DataMember = dt.TableName;
                    }


                    efGridControlDest.BeginUpdate();
                    gridViewDesc.BeginUpdate();

                    GridColumn gridColumnDesc = null;

                    foreach (GridColumn gridcol in gridViewSource.VisibleColumns)
                    {
                        gridColumnDesc = new GridColumn();
                        gridColumnDesc.Name = gridcol.Name;
                        gridColumnDesc.FieldName = gridcol.FieldName;
                        gridColumnDesc.Caption = gridcol.Caption;
                        gridViewDesc.Columns.Add(gridColumnDesc);
                        gridColumnDesc.Visible = true;
                        gridColumnDesc.VisibleIndex = gridViewDesc.VisibleColumns.Count;
                        gridColumnDesc.OptionsColumn.ReadOnly = true;
                    }
                    efGridControlDest.DataSource = dsDataSource;
                    gridViewDesc.OptionsView.ColumnAutoWidth = false;
                    gridViewDesc.IndicatorWidth = 30;
                    gridViewDesc.BestFitColumns();
                    gridViewDesc.OptionsCustomization.AllowColumnMoving = false;
                    gridViewDesc.EndUpdate();
                    efGridControlDest.EndUpdate();
                }
#endif
                if (ctrl is DataGridView)
                {
                    DataGridView gridSource = ctrl as DataGridView;

                    efGroupBox2.Controls.Clear();

                    m_ctrlGrid = new DataGridView();
                    DataGridView gridDesc = m_ctrlGrid as DataGridView;
                    efGroupBox2.Controls.Add(gridDesc);
                    gridDesc.Parent = efGroupBox2;
                    gridDesc.Dock = DockStyle.Fill;
                    gridDesc.Columns.Clear();
                    gridDesc.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
                    DataGridViewColumn colDesc;
                    DataGridViewColumn colSource;
                    for (int colIndex = 0; colIndex < gridSource.Columns.Count; colIndex++)
                    {
                        colSource = gridSource.Columns[colIndex];
                        colDesc = new DataGridViewColumn();
                        colDesc.HeaderText = colSource.HeaderText;
                        colDesc.Name = colSource.Name;
                        colDesc.ValueType = colSource.ValueType;
                        colDesc.CellTemplate = colSource.CellTemplate;

                        gridDesc.Columns.Add(colDesc);
                    }
                    gridDesc.DataSource = gridSource.DataSource;
                }
                if (m_ctrlGrid != null)
                {
                    m_ctrlGrid.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
        }

		private void efBtn_excel_Click(object sender, EventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.InitialDirectory = "c:\\";
			openFileDialog.Filter = "txt files (*.xls)|*.xls";
			openFileDialog.FilterIndex = 2;
			openFileDialog.Multiselect = false;
			openFileDialog.RestoreDirectory = true;

			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				try
				{
					efGroupBox3.Controls.Clear();
					m_workbookView = new WorkbookView();
					m_workbookView.GetLock();
					efGroupBox3.Controls.Add(m_workbookView);
					m_workbookView.Parent = efGroupBox3;
					m_workbookView.Dock = DockStyle.Fill;
					SpreadsheetGear.IWorkbook workbook = SpreadsheetGear.Factory.GetWorkbook(openFileDialog.FileName, System.Globalization.CultureInfo.CurrentCulture);
					m_workbookView.ActiveWorkbook = workbook;
					m_workbookView.ReleaseLock();
				}
				catch (Exception ex)
				{
					MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
				}
			}
		}

		private void efBtn_import_preview_Click(object sender, EventArgs e)
		{
			try
			{
				if (m_workbookView != null)
				{
					m_workbookView.GetLock();
					System.Data.DataSet ds = null;
					DataTable dt = null;
					if (efRB_col_seq.Checked)
					{
						ds = m_workbookView.ActiveWorkbook.GetDataSet(SpreadsheetGear.Data.GetDataFlags.FormattedText | SpreadsheetGear.Data.GetDataFlags.NoColumnHeaders);
						dt = ds.Tables[m_workbookView.ActiveSheet.Name.Trim()];
						dt.Rows.RemoveAt(0);
					}
					else
					{
						ds = m_workbookView.ActiveWorkbook.GetDataSet(SpreadsheetGear.Data.GetDataFlags.FormattedText);
						dt = ds.Tables[m_workbookView.ActiveSheet.Name.Trim()];
					}
					m_workbookView.ReleaseLock();

					/////////
#if(Devxpress)
					if (m_ctrlGrid is  GridControl)
					{
						if (SetGridData(( GridControl)m_ctrlGrid, dt) != 0)
						{
							throw new Exception("预览导入失败");
						}
					}
#endif
				    if (m_ctrlGrid is  DataGridView)
					{
						if (SetGridData(( DataGridView)m_ctrlGrid, dt) != 0)
						{
							throw new Exception("预览导入失败");
						}
					}
				}
				else
				{
					throw new Exception("请先打开EXCEL文件");
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
 
        private int SetGridData(DataGridView GridControl, DataTable dtSource)
        {
            return 0;
        }
        #if(Devxpress)
        private int SetGridData( GridControl GridControl, DataTable dtSource)
        {
            if (GridControl == null || dtSource == null)
            {
                return -1;
            }
            GridView gridView = GridControl.FocusedView as GridView;
            if (gridView == null)
            {
                return -1;
            }

            try
            {
                int colSourceIndex = 0;
                int dataRows = 0;
                string colDescName = string.Empty;
                string colSourceName = string.Empty;

                DataTable dtDesc = (GridControl.DataSource as System.Data.DataSet).Tables[GridControl.DataMember];
                dtDesc.Rows.Clear();

                Dictionary<int, string> ColMapping = new Dictionary<int, string>();
                if (efRB_col_seq.Checked)
                {
                    GridColumn gridColumn = null;// GridControl.SelectionColumn;
                    if (gridColumn != null)
                    {
                        gridColumn.Visible = false;
                        //gridColumn.VisibleIndex = -gridColumn.VisibleIndex;
                    }
                    for (colSourceIndex = 0; colSourceIndex < dtSource.Columns.Count; ++colSourceIndex)
                    {
                        ColMapping.Add(colSourceIndex, gridView.VisibleColumns[colSourceIndex].FieldName);
                    }
                    if (gridColumn != null)
                    {
                        gridColumn.Visible = true;
                    }
                }
                else if (efRB_col_cname.Checked)
                {
                    for (colSourceIndex = 0; colSourceIndex < dtSource.Columns.Count; ++colSourceIndex)
                    {
                        colDescName = string.Empty;
                        colSourceName = dtSource.Columns[colSourceIndex].Caption.Trim();
                        foreach (GridColumn gridcol in gridView.Columns)
                        {
                            colDescName = gridcol.Caption.Replace("<br>", "");
                            colDescName = colDescName.Replace(" ", "");
                            if (colDescName == colSourceName)
                            {
                                colDescName = gridcol.FieldName;
                                break;
                            }
                            colDescName = string.Empty;
                        }
                        if (colDescName != string.Empty)
                        {
                            ColMapping.Add(colSourceIndex, colDescName);
                        }
                    }
                }
                else if (efRB_col_ename.Checked)
                {
                    for (colSourceIndex = 0; colSourceIndex < dtSource.Columns.Count; ++colSourceIndex)
                    {
                        colDescName = string.Empty;
                        colSourceName = dtSource.Columns[colSourceIndex].ColumnName.Trim();
                        foreach (GridColumn gridcol in gridView.Columns)
                        {
                            if (gridcol.FieldName == colSourceName)
                            {
                                colDescName = gridcol.FieldName;
                            }
                        }
                        if (colDescName != string.Empty)
                        {
                            ColMapping.Add(colSourceIndex, colDescName);
                        }
                    }
                }

                for (dataRows = 0; dataRows < dtSource.Rows.Count; ++dataRows)
                {
                    DataRow drDesc = dtDesc.NewRow();
                    DataRow drSource = dtSource.Rows[dataRows];

                    Dictionary<int, string>.Enumerator enumerator =  ColMapping.GetEnumerator();
                    while (enumerator.MoveNext())
                    {
                        //if (drDesc.Table.Columns[enumerator.Current.Value].DataType == typeof(Decimal))
                        //{
                        //    drDesc[enumerator.Current.Value] = Convert.ToDecimal(drSource[enumerator.Current.Key]);
                        //}
                        //else
                        //{
                            drDesc[enumerator.Current.Value] = drSource[enumerator.Current.Key];
                        //}
                    }
                    dtDesc.Rows.Add(drDesc);
                }
                GridControl.DataSource = null;
                GridControl.DataSource = dtDesc.DataSet;
                GridControl.DataMember = dtDesc.TableName;

                for (dataRows = 0; dataRows < dtDesc.Rows.Count; ++dataRows)
                {
                    //GridControl.SetSelectedColumnChecked(dataRows, true);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return -1;
            }
            return 0;
        }
#endif
        //C1FlexGrid
        //private int SetGridData( GridControl grid, DataTable dtSource,bool tmp)
        //{
        //    if (grid == null || dtSource == null)
        //    {
        //        return -1;
        //    }
        //    try
        //    {
        //        int colSourceIndex = 0;
        //        int colDescIndex = 1;

        //        Dictionary<int, int> ColMapping = new Dictionary<int, int>();
        //        if (efRB_col_seq.Checked)
        //        {
        //            //for (colSourceIndex = 0, colDescIndex=1; colSourceIndex < dtSource.Columns.Count && colDescIndex <grid.Cols.Count; ++colSourceIndex, ++colDescIndex)
        //            //{
        //            //    if (grid.Cols[colDescIndex].Name == "check_option")
        //            //    {
        //            //        colDescIndex++;
        //            //    }
        //            //    ColMapping.Add(colSourceIndex, colDescIndex);
        //            //}
        //        }
        //        else if (efRB_col_cname.Checked)
        //        {
        //            string colSourceCname = string.Empty;
        //            for (colSourceIndex = 0; colSourceIndex < dtSource.Columns.Count; ++colSourceIndex)
        //            {
        //                colSourceCname = dtSource.Columns[colSourceIndex].Caption.Trim();
        //                for (colDescIndex=1; colDescIndex<grid.Cols.Count; ++colDescIndex)
        //                {
        //                    if (grid.Cols[colDescIndex].Caption == colSourceCname)
        //                    {
        //                        break;
        //                    }
        //                }
        //                if (colDescIndex < grid.Cols.Count)
        //                {
        //                    ColMapping.Add(colSourceIndex, colDescIndex);
        //                }
        //            }
        //        }
        //        else if (efRB_col_ename.Checked)
        //        {
        //            string colSourceEname = string.Empty;
        //            for (colSourceIndex = 0; colSourceIndex < dtSource.Columns.Count; ++colSourceIndex)
        //            {
        //                colSourceEname = dtSource.Columns[colSourceIndex].ColumnName.Trim();
        //                for (colDescIndex = 1; colDescIndex < grid.Cols.Count; ++colDescIndex)
        //                {
        //                    if (grid.Cols[colDescIndex].Name == colSourceEname)
        //                    {
        //                        break;
        //                    }
        //                }
        //                if (colDescIndex < grid.Cols.Count)
        //                {
        //                    ColMapping.Add(colSourceIndex, colDescIndex);
        //                }
        //            }
        //        }
        //        for (int dataRows = 0; dataRows < dtSource.Rows.Count; ++dataRows)
        //        {
        //            DataRow drSource = dtSource.Rows[dataRows];
        //            C1.Win.C1FlexGrid.Row drDesc = grid.Rows.Add();
        //            Dictionary<int, int>.Enumerator enumerator = ColMapping.GetEnumerator();
        //            while (enumerator.MoveNext())
        //            {
        //                drDesc[enumerator.Current.Value] = drSource[enumerator.Current.Key];
        //            }
        //            drDesc["check_option"] = true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //        return -1;
        //    }
        //    return 0;
        //}

		private void efBtn_confirm_Click(object sender, EventArgs e)
		{
			try
			{
				Control ctrl = Control.FromHandle(m_WHandle);
#if(Devxpress)
				if (m_ctrlGrid is  GridControl)
				{
					DataTable dtSource = null;
					 GridControl efGridControl = ctrl as  GridControl;

					if (efGridControl.DataSource is BindingSource)
					{
						BindingSource bindingSource = efGridControl.DataSource as BindingSource;
						if (bindingSource.DataSource is System.Data.DataSet)
						{
							dtSource = (bindingSource.DataSource as System.Data.DataSet).Tables[bindingSource.DataMember];
						}
						else if (bindingSource.DataSource is DataTable)
						{
							dtSource = bindingSource.DataSource as DataTable;
						}
					}
					else if (efGridControl.DataSource is System.Data.DataSet)
					{
						dtSource = (efGridControl.DataSource as System.Data.DataSet).Tables[efGridControl.DataMember];
					}
					else if (efGridControl.DataSource is System.Data.DataTable)
					{
						dtSource = efGridControl.DataSource as DataTable;
					}

					 GridControl GridControlLocal =  m_ctrlGrid as  GridControl;
					//DataTable dtLoacl = (GridControlLocal.DataSource as System.Data.DataSet).Tables[GridControlLocal.DataMember];
					if (dtSource != null)
					{
						//GridView gridView = GridControlLocal.FocusedView as GridView;
						//for (int rowIndex=0; rowIndex<gridView.RowCount; ++rowIndex)
						//{
						//    if (GridControlLocal.GetSelectedColumnChecked(rowIndex))
						//    {
						//        DataRow dr = gridView.GetDataRow(rowIndex);
						//        dtSource.Rows.Add(dr.ItemArray);
						//    }
						//}
						//(efGridControl.FocusedView as GridView).BeginDataUpdate();
						efGridControl.BeginUpdate();
						dtSource.Rows.Clear();
						//dtSource.Merge(GridControlLocal.GetSelectedDataRow(),true, MissingSchemaAction.Ignore);
						efGridControl.EndUpdate();
						(efGridControl.FocusedView as GridView).RefreshData();
						(efGridControl.FocusedView as GridView).BestFitColumns();
					}
					else
					{
						throw new Exception("未能获取到数据源");
					}
				}
#endif
                //C1FlexGrid
                //if (m_ctrlGrid is  GridControl)
                //{
                    // GridControl gridRemote = ctrl as  GridControl;
                    // GridControl gridLocal = m_ctrlGrid as  GridControl;
 
                    //C1.Win.C1FlexGrid.Row drRemote = null;

                    //for (int rowIndex = 1; rowIndex <= gridLocal.EFUserRows; ++rowIndex)
                    //{
                    //    if ((bool)gridLocal.Rows[rowIndex]["check_option"])
                    //    {
                    //        drRemote = gridRemote.Rows.Add();
                    //        for (int colIndex = 1; colIndex <= gridLocal.EFUserCols; ++colIndex)
                    //        {
                    //            if (!gridRemote.Cols.Contains(gridLocal.Cols[colIndex].Name))
                    //            {
                    //                continue;
                    //            }

                    //            drRemote[gridLocal.Cols[colIndex].Name] = gridLocal.Rows[rowIndex][colIndex];
                    //        }
                    //    }
                    //}
                    //gridRemote.AutoSizeCols();
				//}
				efBtn_cancel_Click(sender, e);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void efBtn_cancel_Click(object sender, EventArgs e)
		{
			if (m_WHandle != IntPtr.Zero)
			{
				Control ctrl = Control.FromHandle(m_WHandle);
				if (ctrl != null)
				{
					ctrl.Refresh();
				}
			}
			this.Dispose();
		}

		// 导出excel模板
		private void efBtn_export_model_Click(object sender, EventArgs e)
		{
			SaveFileDialog saveFileDialog = new SaveFileDialog();

			saveFileDialog.Filter = "Excel files (*.xls)|*.xls";
			saveFileDialog.FilterIndex = 2;
			saveFileDialog.RestoreDirectory = true;
			saveFileDialog.FileName = "Template";

			try
			{
				if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
				{
                    #if(Devxpress)
					if (m_ctrlGrid is  GridControl)
					{
						GridControl currentGridControl = m_ctrlGrid as  GridControl;
						GridView currentGridView = currentGridControl.FocusedView as GridView;

						DataSet dsSource = currentGridControl.DataSource as DataSet;
						if (dsSource == null)
						{
							return;
						}
						DataTable dtExport = dsSource.Tables[currentGridControl.DataMember].Clone();
						int index = 0;
						for (index=1; index <currentGridView.VisibleColumns.Count; ++index)
						{
							string strColEname = currentGridView.VisibleColumns[index].FieldName;
                            if (!dtExport.Columns.Contains(strColEname))
                            {
                                dtExport.Columns.Add(strColEname);
                                //dtExport.Columns[strColEname].SetOrdinal(index - 1);
                            }
                            else if (string.IsNullOrEmpty(strColEname.Trim()))
                            {
                                dtExport.Columns.Add(currentGridView.VisibleColumns[index].Name);
                            }
                        }
						index--;
                        //while (index < dtExport.Columns.Count)
                        //{
                        //    dtExport.Columns.RemoveAt(index);
                        //}

						SpreadsheetGear.IWorkbook workbook = SpreadsheetGear.Factory.GetWorkbook();
						SpreadsheetGear.IWorksheet workSheet = workbook.Worksheets[0];
                        workSheet.Name = string.IsNullOrEmpty(dtExport.TableName) ? "tmp" : dtExport.TableName;

						for (index = 0; index < dtExport.Columns.Count; ++index)
						{
							if (efRB_col_cname.Checked || efRB_col_seq.Checked)
							{

                                string strCaption = "";
                                if (null != currentGridView.Columns.ColumnByFieldName(dtExport.Columns[index].ColumnName))
                                {
                                    strCaption = currentGridView.Columns.ColumnByFieldName(dtExport.Columns[index].ColumnName).Caption;
                                }
                                else
                                {
                                    continue;
                                }
								strCaption = strCaption.Replace("<br>", "");
								workSheet.Cells[0, index].Formula = strCaption.Replace(" ", "");
							}
							else
							{
								workSheet.Cells[0, index].Formula = dtExport.Columns[index].ColumnName;
							}
							workSheet.Cells[0, index].Columns.AutoFit();
							workSheet.Cells[0, index].Interior.Color = Color.Gray;
							workSheet.Cells[0, index].Borders.LineStyle = SpreadsheetGear.LineStyle.Continuous;

							SpreadsheetGear.IRange iColumnRange = workSheet.Cells[0, index].EntireColumn;
							if (dtExport.Columns[index].DataType == typeof(DateTime))
							{
								GridColumn gridColumn = currentGridView.Columns.ColumnByFieldName(dtExport.Columns[index].ColumnName);
								iColumnRange.NumberFormat = gridColumn.DisplayFormat.FormatString;
							}
							else if (dtExport.Columns[index].DataType == typeof(string))
							{
								iColumnRange.NumberFormat = "@";
							}
						}

						dtExport.Merge(dsSource.Tables[currentGridControl.DataMember], true, MissingSchemaAction.Ignore);
						dtExport.AcceptChanges();
						if (dtExport.Rows.Count > 0)
						{
							SpreadsheetGear.IRange range = workSheet.Cells["A2"];
							range.CopyFromDataTable(dtExport, SpreadsheetGear.Data.SetDataFlags.NoColumnHeaders);
						}

						workbook.SaveAs(saveFileDialog.FileName, SpreadsheetGear.FileFormat.XLS97);
						return;
					}
#endif
 
                    if (m_ctrlGrid is DataGridView)
                    {
                        DataGridView currentGridControl = m_ctrlGrid as DataGridView;
         
                        DataTable dsSource = currentGridControl.DataSource as DataTable;
                        if (dsSource == null)
                        {
                            return;
                        }
                        DataTable dtExport = dsSource.Clone();
                        int index = 0;
                        for (index = 1; index < currentGridControl.Columns.Count; ++index)
                        {
                            string strColEname = currentGridControl.Columns[index].Name;
                            if (!dtExport.Columns.Contains(strColEname))
                            {
                                dtExport.Columns.Add(strColEname);
                                //dtExport.Columns[strColEname].SetOrdinal(index - 1);
                            }
                        }
                        //index--;
                        //while (index < dtExport.Columns.Count)
                        //{
                        //    dtExport.Columns.RemoveAt(index);
                        //}

                        SpreadsheetGear.IWorkbook workbook = SpreadsheetGear.Factory.GetWorkbook();
                        SpreadsheetGear.IWorksheet workSheet = workbook.Worksheets[0];
                        workSheet.Name = string.IsNullOrEmpty(dtExport.TableName) ? "tmp" : dtExport.TableName;

                        for (index = 0; index < dtExport.Columns.Count; ++index)
                        {
                            if (efRB_col_cname.Checked || efRB_col_seq.Checked)
                            {
                                string strCaption = currentGridControl.Columns[dtExport.Columns[index].ColumnName].HeaderText;
                                strCaption = strCaption.Replace("<br>", "");
                                workSheet.Cells[0, index].Formula = strCaption.Replace(" ", "");
                            }
                            else
                            {
                                workSheet.Cells[0, index].Formula = dtExport.Columns[index].ColumnName;
                            }
                            workSheet.Cells[0, index].Columns.AutoFit();
                            workSheet.Cells[0, index].Interior.Color = Color.Gray;
                            workSheet.Cells[0, index].Borders.LineStyle = SpreadsheetGear.LineStyle.Continuous;

                            SpreadsheetGear.IRange iColumnRange = workSheet.Cells[0, index].EntireColumn;
                            if (dtExport.Columns[index].DataType == typeof(DateTime))
                            {
                                //GridColumn gridColumn = currentGridControl.Columns[dtExport.Columns[index].ColumnName].di
                                //iColumnRange.NumberFormat = gridColumn.DisplayFormat.FormatString;
                            }
                            else if (dtExport.Columns[index].DataType == typeof(string))
                            {
                                iColumnRange.NumberFormat = "@";
                            }
                        }

                        dtExport.Merge(dsSource , true, MissingSchemaAction.Ignore);
                        dtExport.AcceptChanges();
                        if (dtExport.Rows.Count > 0)
                        {
                            SpreadsheetGear.IRange range = workSheet.Cells["A2"];
                            range.CopyFromDataTable(dtExport, SpreadsheetGear.Data.SetDataFlags.NoColumnHeaders);
                        }

                        workbook.SaveAs(saveFileDialog.FileName, SpreadsheetGear.FileFormat.XLS97);
                        return;
                    }
 
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.StackTrace);
			}
		}

		/// <summary>
		/// 导出到excel
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void efBtn_export_preview_Click(object sender, EventArgs e)
		{
			Control ctrlSource = Control.FromHandle(m_WHandle);

			if (null == ctrlSource)
			{
				 MessageBox.Show("Please Capture GridControl");
				return;
			}
#if(Devxpress)
			if (ctrlSource is  GridControl)
			{
				 GridControl efGridControlSource = ctrlSource as  GridControl;

				(efGridControlSource.MainView as GridView).BeginDataUpdate();
				DataTable dtSource = ((m_ctrlGrid as  GridControl).DataSource as DataSet).Tables[(m_ctrlGrid as  GridControl).DataMember];
				dtSource.Rows.Clear();
				if (efGridControlSource.DataSource is DataTable)
				{
					dtSource.Merge(efGridControlSource.DataSource as DataTable);
				}
				else if (efGridControlSource.DataSource is DataSet)
				{
					DataSet ds = efGridControlSource.DataSource as DataSet;
					dtSource.Merge(ds.Tables[efGridControlSource.DataMember]);
				}
				else if (efGridControlSource.DataSource is BindingSource)
				{
					BindingSource bindingSource = (efGridControlSource.DataSource as BindingSource);
					if (bindingSource.DataSource is DataTable)
					{
						dtSource.Merge(bindingSource.DataSource as DataTable);
					}
					else if (bindingSource.DataSource is DataSet)
					{
						DataSet ds = bindingSource.DataSource as DataSet;
						dtSource.Merge(ds.Tables[bindingSource.DataMember]);
					}
				}
				(efGridControlSource.MainView as GridView).EndDataUpdate();
				((m_ctrlGrid as  GridControl).FocusedView as GridView).BestFitColumns();
			}
#endif
		    if (ctrlSource is  DataGridView)
			{
                DataGridView efGrid = ctrlSource as DataGridView;

                //EI.EIInfo eiInfo = new EI.EIInfo();
                //eiInfo.SetBlockVal(efGrid, true);

                //eiInfo.GetBlockVal(( GridControl)m_ctrlGrid);
                //(m_ctrlGrid as  GridControl).AutoSizeCols();
                //EF.Utility.SelecteAllGridRows((m_ctrlGrid as  GridControl));
			}
		}

		private void tableLayoutPanel4_Paint(object sender, PaintEventArgs e)
		{

		}

		private void efLabel6_Click(object sender, EventArgs e)
		{

		}
	}
}

