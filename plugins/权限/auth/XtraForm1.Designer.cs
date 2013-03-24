namespace auth
{
    partial class XtraForm1
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
            this.components = new System.ComponentModel.Container();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.dataTable1BindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataSet1 = new auth.DataSet1();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNAME = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDESCRIPTION = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemMemoEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.colSEX = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.colAGE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemSpinEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.colCLASS = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemLookUpEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.colBIRTHDAY = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemDateEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.colPASS = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colPHOTO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDataColumn67 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDataColumn66 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDataColumn65 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDataColumn64 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDataColumn63 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDataColumn62 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDataColumn61 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDataColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnRefresh = new DevExpress.XtraEditors.SimpleButton();
            this.repositoryItemButtonEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable1BindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit1)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            this.gridControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gridControl1.DataSource = this.dataTable1BindingSource;
            this.gridControl1.Location = new System.Drawing.Point(3, 12);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemMemoEdit1,
            this.repositoryItemComboBox1,
            this.repositoryItemSpinEdit1,
            this.repositoryItemLookUpEdit1,
            this.repositoryItemDateEdit1,
            this.repositoryItemCheckEdit1,
            this.repositoryItemButtonEdit1});
            this.gridControl1.Size = new System.Drawing.Size(621, 339);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.UseEmbeddedNavigator = true;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // dataTable1BindingSource
            // 
            this.dataTable1BindingSource.DataMember = "DataTable1";
            this.dataTable1BindingSource.DataSource = this.dataSet1;
            // 
            // dataSet1
            // 
            this.dataSet1.DataSetName = "DataSet1";
            this.dataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colID,
            this.colNAME,
            this.colDESCRIPTION,
            this.colSEX,
            this.colAGE,
            this.colCLASS,
            this.colBIRTHDAY,
            this.colPASS,
            this.colPHOTO,
            this.colDataColumn67,
            this.colDataColumn66,
            this.colDataColumn65,
            this.colDataColumn64,
            this.colDataColumn63,
            this.colDataColumn62,
            this.colDataColumn61,
            this.colDataColumn6});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            // 
            // colID
            // 
            this.colID.FieldName = "ID";
            this.colID.Name = "colID";
            this.colID.Visible = true;
            this.colID.VisibleIndex = 0;
            // 
            // colNAME
            // 
            this.colNAME.FieldName = "NAME";
            this.colNAME.Name = "colNAME";
            this.colNAME.Visible = true;
            this.colNAME.VisibleIndex = 1;
            // 
            // colDESCRIPTION
            // 
            this.colDESCRIPTION.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colDESCRIPTION.FieldName = "DESCRIPTION";
            this.colDESCRIPTION.Name = "colDESCRIPTION";
            this.colDESCRIPTION.Visible = true;
            this.colDESCRIPTION.VisibleIndex = 2;
            this.colDESCRIPTION.Width = 98;
            // 
            // repositoryItemMemoEdit1
            // 
            this.repositoryItemMemoEdit1.Name = "repositoryItemMemoEdit1";
            // 
            // colSEX
            // 
            this.colSEX.ColumnEdit = this.repositoryItemComboBox1;
            this.colSEX.FieldName = "SEX";
            this.colSEX.Name = "colSEX";
            this.colSEX.Visible = true;
            this.colSEX.VisibleIndex = 3;
            // 
            // repositoryItemComboBox1
            // 
            this.repositoryItemComboBox1.AutoHeight = false;
            this.repositoryItemComboBox1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBox1.Items.AddRange(new object[] {
            "男",
            "女",
            "保密"});
            this.repositoryItemComboBox1.Name = "repositoryItemComboBox1";
            // 
            // colAGE
            // 
            this.colAGE.ColumnEdit = this.repositoryItemSpinEdit1;
            this.colAGE.FieldName = "AGE";
            this.colAGE.Name = "colAGE";
            this.colAGE.Visible = true;
            this.colAGE.VisibleIndex = 4;
            // 
            // repositoryItemSpinEdit1
            // 
            this.repositoryItemSpinEdit1.AutoHeight = false;
            this.repositoryItemSpinEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.repositoryItemSpinEdit1.Name = "repositoryItemSpinEdit1";
            // 
            // colCLASS
            // 
            this.colCLASS.Caption = "Class";
            this.colCLASS.ColumnEdit = this.repositoryItemLookUpEdit1;
            this.colCLASS.FieldName = "CLASS";
            this.colCLASS.Name = "colCLASS";
            this.colCLASS.Visible = true;
            this.colCLASS.VisibleIndex = 5;
            this.colCLASS.Width = 98;
            // 
            // repositoryItemLookUpEdit1
            // 
            this.repositoryItemLookUpEdit1.AutoHeight = false;
            this.repositoryItemLookUpEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemLookUpEdit1.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CODE", "代码"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("NAME", "名称")});
            this.repositoryItemLookUpEdit1.Name = "repositoryItemLookUpEdit1";
            // 
            // colBIRTHDAY
            // 
            this.colBIRTHDAY.Caption = "Birthday";
            this.colBIRTHDAY.ColumnEdit = this.repositoryItemDateEdit1;
            this.colBIRTHDAY.FieldName = "BIRTHDAY";
            this.colBIRTHDAY.Name = "colBIRTHDAY";
            this.colBIRTHDAY.Visible = true;
            this.colBIRTHDAY.VisibleIndex = 6;
            this.colBIRTHDAY.Width = 98;
            // 
            // repositoryItemDateEdit1
            // 
            this.repositoryItemDateEdit1.AutoHeight = false;
            this.repositoryItemDateEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemDateEdit1.Name = "repositoryItemDateEdit1";
            this.repositoryItemDateEdit1.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            // 
            // colPASS
            // 
            this.colPASS.Caption = "Passed";
            this.colPASS.ColumnEdit = this.repositoryItemCheckEdit1;
            this.colPASS.FieldName = "PASS";
            this.colPASS.Name = "colPASS";
            this.colPASS.Visible = true;
            this.colPASS.VisibleIndex = 7;
            this.colPASS.Width = 98;
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            // 
            // colPHOTO
            // 
            this.colPHOTO.Caption = "photo";
            this.colPHOTO.FieldName = "PHOTO";
            this.colPHOTO.Name = "colPHOTO";
            this.colPHOTO.Visible = true;
            this.colPHOTO.VisibleIndex = 8;
            this.colPHOTO.Width = 98;
            // 
            // colDataColumn67
            // 
            this.colDataColumn67.Caption = "OtherInfo";
            this.colDataColumn67.ColumnEdit = this.repositoryItemButtonEdit1;
            this.colDataColumn67.FieldName = "DataColumn67";
            this.colDataColumn67.Name = "colDataColumn67";
            this.colDataColumn67.Visible = true;
            this.colDataColumn67.VisibleIndex = 9;
            this.colDataColumn67.Width = 98;
            // 
            // colDataColumn66
            // 
            this.colDataColumn66.FieldName = "DataColumn66";
            this.colDataColumn66.Name = "colDataColumn66";
            this.colDataColumn66.Visible = true;
            this.colDataColumn66.VisibleIndex = 10;
            this.colDataColumn66.Width = 98;
            // 
            // colDataColumn65
            // 
            this.colDataColumn65.FieldName = "DataColumn65";
            this.colDataColumn65.Name = "colDataColumn65";
            this.colDataColumn65.Visible = true;
            this.colDataColumn65.VisibleIndex = 11;
            this.colDataColumn65.Width = 98;
            // 
            // colDataColumn64
            // 
            this.colDataColumn64.FieldName = "DataColumn64";
            this.colDataColumn64.Name = "colDataColumn64";
            this.colDataColumn64.Visible = true;
            this.colDataColumn64.VisibleIndex = 12;
            this.colDataColumn64.Width = 98;
            // 
            // colDataColumn63
            // 
            this.colDataColumn63.FieldName = "DataColumn63";
            this.colDataColumn63.Name = "colDataColumn63";
            this.colDataColumn63.Visible = true;
            this.colDataColumn63.VisibleIndex = 13;
            this.colDataColumn63.Width = 98;
            // 
            // colDataColumn62
            // 
            this.colDataColumn62.FieldName = "DataColumn62";
            this.colDataColumn62.Name = "colDataColumn62";
            this.colDataColumn62.Visible = true;
            this.colDataColumn62.VisibleIndex = 14;
            this.colDataColumn62.Width = 98;
            // 
            // colDataColumn61
            // 
            this.colDataColumn61.FieldName = "DataColumn61";
            this.colDataColumn61.Name = "colDataColumn61";
            this.colDataColumn61.Visible = true;
            this.colDataColumn61.VisibleIndex = 15;
            this.colDataColumn61.Width = 98;
            // 
            // colDataColumn6
            // 
            this.colDataColumn6.FieldName = "DataColumn6";
            this.colDataColumn6.Name = "colDataColumn6";
            this.colDataColumn6.Visible = true;
            this.colDataColumn6.VisibleIndex = 16;
            this.colDataColumn6.Width = 98;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSave.Location = new System.Drawing.Point(13, 365);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "保存";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRefresh.Location = new System.Drawing.Point(132, 365);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 2;
            this.btnRefresh.Text = "刷新";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // repositoryItemButtonEdit1
            // 
            this.repositoryItemButtonEdit1.AutoHeight = false;
            this.repositoryItemButtonEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.repositoryItemButtonEdit1.Name = "repositoryItemButtonEdit1";
            // 
            // XtraForm1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(892, 401);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.gridControl1);
            this.Name = "XtraForm1";
            this.Text = "本画面只为了测试Grid内空间的使用";
            this.Load += new System.EventHandler(this.XtraForm1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable1BindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private System.Windows.Forms.BindingSource dataTable1BindingSource;
        private DataSet1 dataSet1;
        private DevExpress.XtraGrid.Columns.GridColumn colID;
        private DevExpress.XtraGrid.Columns.GridColumn colNAME;
        private DevExpress.XtraGrid.Columns.GridColumn colDESCRIPTION;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn colSEX;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBox1;
        private DevExpress.XtraGrid.Columns.GridColumn colAGE;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit repositoryItemSpinEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn colCLASS;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn colBIRTHDAY;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit repositoryItemDateEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn colPASS;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn colPHOTO;
        private DevExpress.XtraGrid.Columns.GridColumn colDataColumn67;
        private DevExpress.XtraGrid.Columns.GridColumn colDataColumn66;
        private DevExpress.XtraGrid.Columns.GridColumn colDataColumn65;
        private DevExpress.XtraGrid.Columns.GridColumn colDataColumn64;
        private DevExpress.XtraGrid.Columns.GridColumn colDataColumn63;
        private DevExpress.XtraGrid.Columns.GridColumn colDataColumn62;
        private DevExpress.XtraGrid.Columns.GridColumn colDataColumn61;
        private DevExpress.XtraGrid.Columns.GridColumn colDataColumn6;
        private DevExpress.XtraEditors.SimpleButton btnRefresh;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEdit1;
    }
}