namespace EF
{
    partial class FormConfigCaption
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {



            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormConfigCaption));
            this.efDevGrid1 = new EF.EFDevGrid();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.efBtnOk)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.efBtnCancel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.efDevGrid1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // efBtnOk
            // 
            resources.ApplyResources(this.efBtnOk, "efBtnOk");
            this.efBtnOk.Click += new System.EventHandler(this.efBtnOk_Click);
            // 
            // efBtnCancel
            // 
            resources.ApplyResources(this.efBtnCancel, "efBtnCancel");
            // 
            // efDevGrid1
            // 
            this.efDevGrid1.AllowDragRow = false;
            resources.ApplyResources(this.efDevGrid1, "efDevGrid1");
            this.efDevGrid1.CanConfigGridCaption = true;
            this.efDevGrid1.ContextMenuAddCopyNewEnable = true;
            this.efDevGrid1.ContextMenuAddNewEnable = true;
            this.efDevGrid1.ContextMenuChooseAllEnable = true;
            this.efDevGrid1.ContextMenuChooseEnable = true;
            this.efDevGrid1.ContextMenuSaveAsEnable = true;
            this.efDevGrid1.ContextMenuUnChooseAllEnable = true;
            this.efDevGrid1.ContextMenuUnChooseEnable = true;
            this.efDevGrid1.FirstPageButtonEnable = true;
            this.efDevGrid1.InitRowOrdinal = 1;
            this.efDevGrid1.IsUseCustomColWidth = false;
            this.efDevGrid1.IsUseCustomPageBar = false;
            this.efDevGrid1.LastPageButtonEnable = true;
            this.efDevGrid1.MainView = this.gridView1;
            this.efDevGrid1.Name = "efDevGrid1";
            this.efDevGrid1.NextPageButtonEnable = true;
            this.efDevGrid1.PageSize = 20;
            this.efDevGrid1.PrePageButtonEnable = true;
            this.efDevGrid1.RecordCountMessage = "Record {0} of {1}";
            this.efDevGrid1.ShowAddCopyRowButton = false;
            this.efDevGrid1.ShowAddRowButton = false;
            this.efDevGrid1.ShowContextMenu = false;
            this.efDevGrid1.ShowContextMenuAddCopyNew = true;
            this.efDevGrid1.ShowContextMenuAddNew = true;
            this.efDevGrid1.ShowContextMenuChoose = true;
            this.efDevGrid1.ShowContextMenuChooseAll = true;
            this.efDevGrid1.ShowContextMenuSaveAs = true;
            this.efDevGrid1.ShowContextMenuUnChoose = true;
            this.efDevGrid1.ShowContextMenuUnChooseAll = true;
            this.efDevGrid1.ShowDeleteRowButton = false;
            this.efDevGrid1.ShowExportButton = true;
            this.efDevGrid1.ShowFilterButton = true;
            this.efDevGrid1.ShowGroupButton = true;
            this.efDevGrid1.ShowPageButton = true;
            this.efDevGrid1.ShowPageToButton = false;
            this.efDevGrid1.ShowRecordCountMessage = true;
            this.efDevGrid1.ShowRefreshButton = true;
            this.efDevGrid1.ShowSaveLayoutButton = true;
            this.efDevGrid1.ShowSelectedColumn = false;
            this.efDevGrid1.TotalRecordCount = 1;
            this.efDevGrid1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4});
            this.gridView1.GridControl = this.efDevGrid1;
            this.gridView1.IndicatorWidth = 35;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.EnableAppearanceEvenRow = true;
            this.gridView1.OptionsView.EnableAppearanceOddRow = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1
            // 
            resources.ApplyResources(this.gridColumn1, "gridColumn1");
            this.gridColumn1.FieldName = "FieldName";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            // 
            // gridColumn2
            // 
            resources.ApplyResources(this.gridColumn2, "gridColumn2");
            this.gridColumn2.FieldName = "Caption";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            // 
            // gridColumn3
            // 
            resources.ApplyResources(this.gridColumn3, "gridColumn3");
            this.gridColumn3.FieldName = "ColumnName";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            // 
            // gridColumn4
            // 
            resources.ApplyResources(this.gridColumn4, "gridColumn4");
            this.gridColumn4.FieldName = "NewCaption";
            this.gridColumn4.Name = "gridColumn4";
            // 
            // FormConfigCaption
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.efDevGrid1);
            this.Name = "FormConfigCaption";
            this.Load += new System.EventHandler(this.FormConfigCaption_Load);
            this.Controls.SetChildIndex(this.efBtnCancel, 0);
            this.Controls.SetChildIndex(this.efBtnOk, 0);
            this.Controls.SetChildIndex(this.efDevGrid1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.efBtnOk)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.efBtnCancel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.efDevGrid1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private EF.EFDevGrid efDevGrid1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
    }
}