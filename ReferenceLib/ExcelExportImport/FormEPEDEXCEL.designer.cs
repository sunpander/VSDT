using System.Windows.Forms;

namespace WindowsFormsApplication1
{
	partial class FormEPEDEXCEL
	{
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
		#region 设计器生成的代码
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormEPEDEXCEL));
            this.efGroupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.efBtn_capture = new System.Windows.Forms.Button();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.efTB_gird_handle = new System.Windows.Forms.TextBox();
            this.efTB_form_ename = new System.Windows.Forms.TextBox();
            this.efTB_form_cname = new System.Windows.Forms.TextBox();
            this.efLabel1 = new System.Windows.Forms.Label();
            this.efLabel2 = new System.Windows.Forms.Label();
            this.efLabel3 = new System.Windows.Forms.Label();
            this.efGroupBox2 = new System.Windows.Forms.GroupBox();
            this.efGroupBox3 = new System.Windows.Forms.GroupBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.efBtn_open_excel = new System.Windows.Forms.Button();
            this.efGroupBox4 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.efBtn_cancel = new System.Windows.Forms.Button();
            this.efBtn_export_model = new System.Windows.Forms.Button();
            this.efBtn_confirm = new System.Windows.Forms.Button();
            this.efGroupBox5 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.efRB_col_ename = new System.Windows.Forms.RadioButton();
            this.efRB_col_cname = new System.Windows.Forms.RadioButton();
            this.efRB_col_seq = new System.Windows.Forms.RadioButton();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.efBtn_export_preview = new System.Windows.Forms.Button();
            this.efBtn_import_preview = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel8 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel9 = new System.Windows.Forms.TableLayoutPanel();
            this.efGroupBox1.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
            this.efGroupBox4.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.efGroupBox5.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel8.SuspendLayout();
            this.tableLayoutPanel9.SuspendLayout();
            this.SuspendLayout();
            // 
            // efGroupBox1
            // 
            this.efGroupBox1.Controls.Add(this.tableLayoutPanel6);
            this.efGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.efGroupBox1.Location = new System.Drawing.Point(0, 0);
            this.efGroupBox1.Margin = new System.Windows.Forms.Padding(0);
            this.efGroupBox1.Name = "efGroupBox1";
            this.efGroupBox1.Size = new System.Drawing.Size(525, 121);
            this.efGroupBox1.TabIndex = 0;
            this.efGroupBox1.TabStop = false;
            this.efGroupBox1.Text = "导入GIRD列信息";
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel6.ColumnCount = 2;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.Controls.Add(this.efBtn_capture, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this.tableLayoutPanel7, 1, 0);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(3, 17);
            this.tableLayoutPanel6.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 1;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(519, 101);
            this.tableLayoutPanel6.TabIndex = 0;
            // 
            // efBtn_capture
            // 
            this.efBtn_capture.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.efBtn_capture.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("efBtn_capture.BackgroundImage")));
            this.efBtn_capture.Location = new System.Drawing.Point(10, 13);
            this.efBtn_capture.Margin = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.efBtn_capture.Name = "efBtn_capture";
            this.efBtn_capture.Size = new System.Drawing.Size(88, 75);
            this.efBtn_capture.TabIndex = 6;
            this.efBtn_capture.MouseDown += new System.Windows.Forms.MouseEventHandler(this.efBtn_capture_MouseDown);
            this.efBtn_capture.MouseUp += new System.Windows.Forms.MouseEventHandler(this.efBtn_capture_MouseUp);
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.ColumnCount = 2;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel7.Controls.Add(this.efTB_gird_handle, 1, 2);
            this.tableLayoutPanel7.Controls.Add(this.efTB_form_ename, 1, 0);
            this.tableLayoutPanel7.Controls.Add(this.efTB_form_cname, 1, 1);
            this.tableLayoutPanel7.Controls.Add(this.efLabel1, 0, 0);
            this.tableLayoutPanel7.Controls.Add(this.efLabel2, 0, 1);
            this.tableLayoutPanel7.Controls.Add(this.efLabel3, 0, 2);
            this.tableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel7.Location = new System.Drawing.Point(108, 0);
            this.tableLayoutPanel7.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 3;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.tableLayoutPanel7.Size = new System.Drawing.Size(411, 101);
            this.tableLayoutPanel7.TabIndex = 7;
            // 
            // efTB_gird_handle
            // 
            this.efTB_gird_handle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.efTB_gird_handle.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.efTB_gird_handle.Location = new System.Drawing.Point(88, 73);
            this.efTB_gird_handle.Name = "efTB_gird_handle";
            this.efTB_gird_handle.ReadOnly = true;
            this.efTB_gird_handle.Size = new System.Drawing.Size(335, 21);
            this.efTB_gird_handle.TabIndex = 5;
            // 
            // efTB_form_ename
            // 
            this.efTB_form_ename.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.efTB_form_ename.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.efTB_form_ename.Location = new System.Drawing.Point(88, 6);
            this.efTB_form_ename.Name = "efTB_form_ename";
            this.efTB_form_ename.ReadOnly = true;
            this.efTB_form_ename.Size = new System.Drawing.Size(335, 21);
            this.efTB_form_ename.TabIndex = 2;
            // 
            // efTB_form_cname
            // 
            this.efTB_form_cname.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.efTB_form_cname.Dock = System.Windows.Forms.DockStyle.Fill;
            this.efTB_form_cname.Location = new System.Drawing.Point(88, 36);
            this.efTB_form_cname.Name = "efTB_form_cname";
            this.efTB_form_cname.ReadOnly = true;
            this.efTB_form_cname.Size = new System.Drawing.Size(335, 21);
            this.efTB_form_cname.TabIndex = 3;
            // 
            // efLabel1
            // 
            this.efLabel1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.efLabel1.Location = new System.Drawing.Point(22, 9);
            this.efLabel1.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
            this.efLabel1.Name = "efLabel1";
            this.efLabel1.Size = new System.Drawing.Size(60, 14);
            this.efLabel1.TabIndex = 0;
            this.efLabel1.Text = "目标画面名";
            // 
            // efLabel2
            // 
            this.efLabel2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.efLabel2.Location = new System.Drawing.Point(10, 42);
            this.efLabel2.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
            this.efLabel2.Name = "efLabel2";
            this.efLabel2.Size = new System.Drawing.Size(72, 14);
            this.efLabel2.TabIndex = 1;
            this.efLabel2.Text = "目标画面描述";
            // 
            // efLabel3
            // 
            this.efLabel3.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.efLabel3.Location = new System.Drawing.Point(14, 76);
            this.efLabel3.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
            this.efLabel3.Name = "efLabel3";
            this.efLabel3.Size = new System.Drawing.Size(68, 14);
            this.efLabel3.TabIndex = 4;
            this.efLabel3.Text = "GRID Handle";
            // 
            // efGroupBox2
            // 
            this.efGroupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.efGroupBox2.Location = new System.Drawing.Point(0, 124);
            this.efGroupBox2.Margin = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.efGroupBox2.Name = "efGroupBox2";
            this.efGroupBox2.Size = new System.Drawing.Size(1002, 339);
            this.efGroupBox2.TabIndex = 1;
            this.efGroupBox2.TabStop = false;
            this.efGroupBox2.Text = "目标GRID";
            // 
            // efGroupBox3
            // 
            this.efGroupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.efGroupBox3.Location = new System.Drawing.Point(0, 466);
            this.efGroupBox3.Margin = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.efGroupBox3.Name = "efGroupBox3";
            this.efGroupBox3.Size = new System.Drawing.Size(1002, 274);
            this.efGroupBox3.TabIndex = 2;
            this.efGroupBox3.TabStop = false;
            this.efGroupBox3.Text = "Excel数据源";
            // 
            // timer1
            // 
            this.timer1.Interval = 200;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // efBtn_open_excel
            // 
            this.efBtn_open_excel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.efBtn_open_excel.Location = new System.Drawing.Point(9, 9);
            this.efBtn_open_excel.Name = "efBtn_open_excel";
            this.efBtn_open_excel.Size = new System.Drawing.Size(102, 32);
            this.efBtn_open_excel.TabIndex = 7;
            this.efBtn_open_excel.Text = "打开EXCEL文件";
            this.efBtn_open_excel.Click += new System.EventHandler(this.efBtn_excel_Click);
            // 
            // efGroupBox4
            // 
            this.efGroupBox4.Controls.Add(this.tableLayoutPanel2);
            this.efGroupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.efGroupBox4.Location = new System.Drawing.Point(3, 0);
            this.efGroupBox4.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.efGroupBox4.Name = "efGroupBox4";
            this.efGroupBox4.Size = new System.Drawing.Size(247, 121);
            this.efGroupBox4.TabIndex = 2;
            this.efGroupBox4.TabStop = false;
            this.efGroupBox4.Text = "操作区域";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.efBtn_cancel, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.efBtn_export_model, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.efBtn_open_excel, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.efBtn_confirm, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 17);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(241, 101);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // efBtn_cancel
            // 
            this.efBtn_cancel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.efBtn_cancel.Location = new System.Drawing.Point(131, 59);
            this.efBtn_cancel.Name = "efBtn_cancel";
            this.efBtn_cancel.Size = new System.Drawing.Size(99, 32);
            this.efBtn_cancel.TabIndex = 9;
            this.efBtn_cancel.Text = "取消";
            this.efBtn_cancel.Click += new System.EventHandler(this.efBtn_cancel_Click);
            // 
            // efBtn_export_model
            // 
            this.efBtn_export_model.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.efBtn_export_model.Location = new System.Drawing.Point(9, 59);
            this.efBtn_export_model.Name = "efBtn_export_model";
            this.efBtn_export_model.Size = new System.Drawing.Size(102, 32);
            this.efBtn_export_model.TabIndex = 10;
            this.efBtn_export_model.Text = "导出EXCEL模板";
            this.efBtn_export_model.Click += new System.EventHandler(this.efBtn_export_model_Click);
            // 
            // efBtn_confirm
            // 
            this.efBtn_confirm.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.efBtn_confirm.Location = new System.Drawing.Point(131, 9);
            this.efBtn_confirm.Name = "efBtn_confirm";
            this.efBtn_confirm.Size = new System.Drawing.Size(99, 32);
            this.efBtn_confirm.TabIndex = 8;
            this.efBtn_confirm.Text = "确定";
            this.efBtn_confirm.Click += new System.EventHandler(this.efBtn_confirm_Click);
            // 
            // efGroupBox5
            // 
            this.efGroupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.efGroupBox5.Controls.Add(this.tableLayoutPanel3);
            this.efGroupBox5.Location = new System.Drawing.Point(528, 0);
            this.efGroupBox5.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.efGroupBox5.Name = "efGroupBox5";
            this.efGroupBox5.Size = new System.Drawing.Size(224, 121);
            this.efGroupBox5.TabIndex = 3;
            this.efGroupBox5.TabStop = false;
            this.efGroupBox5.Text = "数据列匹配";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel4, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel5, 1, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 17);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(218, 101);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.AutoSize = true;
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel4.Controls.Add(this.efRB_col_ename, 0, 2);
            this.tableLayoutPanel4.Controls.Add(this.efRB_col_cname, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.efRB_col_seq, 0, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel4.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 3;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(101, 101);
            this.tableLayoutPanel4.TabIndex = 0;
            this.tableLayoutPanel4.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel4_Paint);
            // 
            // efRB_col_ename
            // 
            this.efRB_col_ename.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.efRB_col_ename.AutoSize = true;
            this.efRB_col_ename.BackColor = System.Drawing.Color.Transparent;
            this.efRB_col_ename.Location = new System.Drawing.Point(3, 75);
            this.efRB_col_ename.Name = "efRB_col_ename";
            this.efRB_col_ename.Size = new System.Drawing.Size(83, 16);
            this.efRB_col_ename.TabIndex = 2;
            this.efRB_col_ename.Text = "按列名匹配";
            this.efRB_col_ename.UseVisualStyleBackColor = false;
            // 
            // efRB_col_cname
            // 
            this.efRB_col_cname.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.efRB_col_cname.AutoSize = true;
            this.efRB_col_cname.BackColor = System.Drawing.Color.Transparent;
            this.efRB_col_cname.Location = new System.Drawing.Point(3, 41);
            this.efRB_col_cname.Name = "efRB_col_cname";
            this.efRB_col_cname.Size = new System.Drawing.Size(95, 16);
            this.efRB_col_cname.TabIndex = 1;
            this.efRB_col_cname.Text = "按列标题匹配";
            this.efRB_col_cname.UseVisualStyleBackColor = false;
            // 
            // efRB_col_seq
            // 
            this.efRB_col_seq.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.efRB_col_seq.AutoSize = true;
            this.efRB_col_seq.BackColor = System.Drawing.Color.Transparent;
            this.efRB_col_seq.Checked = true;
            this.efRB_col_seq.Location = new System.Drawing.Point(3, 8);
            this.efRB_col_seq.Name = "efRB_col_seq";
            this.efRB_col_seq.Size = new System.Drawing.Size(95, 16);
            this.efRB_col_seq.TabIndex = 0;
            this.efRB_col_seq.TabStop = true;
            this.efRB_col_seq.Text = "按列顺序匹配";
            this.efRB_col_seq.UseVisualStyleBackColor = false;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 1;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel5.Controls.Add(this.efBtn_export_preview, 0, 1);
            this.tableLayoutPanel5.Controls.Add(this.efBtn_import_preview, 0, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(101, 0);
            this.tableLayoutPanel5.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 2;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(117, 101);
            this.tableLayoutPanel5.TabIndex = 1;
            // 
            // efBtn_export_preview
            // 
            this.efBtn_export_preview.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.efBtn_export_preview.Location = new System.Drawing.Point(8, 59);
            this.efBtn_export_preview.Margin = new System.Windows.Forms.Padding(0);
            this.efBtn_export_preview.Name = "efBtn_export_preview";
            this.efBtn_export_preview.Size = new System.Drawing.Size(101, 32);
            this.efBtn_export_preview.TabIndex = 8;
            this.efBtn_export_preview.Text = "导出预览";
            this.efBtn_export_preview.Click += new System.EventHandler(this.efBtn_export_preview_Click);
            // 
            // efBtn_import_preview
            // 
            this.efBtn_import_preview.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.efBtn_import_preview.Location = new System.Drawing.Point(8, 9);
            this.efBtn_import_preview.Margin = new System.Windows.Forms.Padding(0);
            this.efBtn_import_preview.Name = "efBtn_import_preview";
            this.efBtn_import_preview.Size = new System.Drawing.Size(101, 32);
            this.efBtn_import_preview.TabIndex = 7;
            this.efBtn_import_preview.Text = "导入预览";
            this.efBtn_import_preview.Click += new System.EventHandler(this.efBtn_import_preview_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.efGroupBox5, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.efGroupBox1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel8, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1002, 121);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // tableLayoutPanel8
            // 
            this.tableLayoutPanel8.AutoSize = true;
            this.tableLayoutPanel8.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel8.ColumnCount = 2;
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel8.Controls.Add(this.efGroupBox4, 1, 0);
            this.tableLayoutPanel8.Dock = System.Windows.Forms.DockStyle.Right;
            this.tableLayoutPanel8.Location = new System.Drawing.Point(752, 0);
            this.tableLayoutPanel8.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel8.Name = "tableLayoutPanel8";
            this.tableLayoutPanel8.RowCount = 1;
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel8.Size = new System.Drawing.Size(250, 121);
            this.tableLayoutPanel8.TabIndex = 1;
            // 
            // tableLayoutPanel9
            // 
            this.tableLayoutPanel9.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel9.ColumnCount = 1;
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel9.Controls.Add(this.efGroupBox2, 0, 1);
            this.tableLayoutPanel9.Controls.Add(this.tableLayoutPanel1, 0, 0);
            this.tableLayoutPanel9.Controls.Add(this.efGroupBox3, 0, 2);
            this.tableLayoutPanel9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel9.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel9.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel9.Name = "tableLayoutPanel9";
            this.tableLayoutPanel9.RowCount = 3;
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel9.Size = new System.Drawing.Size(1002, 740);
            this.tableLayoutPanel9.TabIndex = 4;
            // 
            // FormEPEDEXCEL
            // 
            this.ClientSize = new System.Drawing.Size(1002, 740);
            this.Controls.Add(this.tableLayoutPanel9);
            this.Name = "FormEPEDEXCEL";
            this.Text = "EXCEL导入画面";
            this.TopMost = true;
            this.efGroupBox1.ResumeLayout(false);
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel7.ResumeLayout(false);
            this.tableLayoutPanel7.PerformLayout();
            this.efGroupBox4.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.efGroupBox5.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel8.ResumeLayout(false);
            this.tableLayoutPanel9.ResumeLayout(false);
            this.tableLayoutPanel9.PerformLayout();
            this.ResumeLayout(false);

		}
		#endregion

		private System.ComponentModel.IContainer components = null;
		private  GroupBox efGroupBox1;
		private  GroupBox efGroupBox2;
		private  GroupBox efGroupBox3;
		private  Label efLabel1;
		private  Label efLabel2;
		private  TextBox efTB_form_ename;
		private  TextBox efTB_form_cname;
		private  TextBox efTB_gird_handle;
		private  Label efLabel3;
		private  Button efBtn_capture;
		private Timer timer1;
		private  Button efBtn_open_excel;
		private  GroupBox efGroupBox4;
		private  GroupBox efGroupBox5;
		private  RadioButton efRB_col_seq;
		private  RadioButton efRB_col_ename;
		private  RadioButton efRB_col_cname;
		private  Button efBtn_import_preview;
		private  Button efBtn_confirm;
		private  Button efBtn_cancel;
		private  Button efBtn_export_model;
		private  Button efBtn_export_preview;
		private TableLayoutPanel tableLayoutPanel1;
		private TableLayoutPanel tableLayoutPanel2;
		private TableLayoutPanel tableLayoutPanel3;
		private TableLayoutPanel tableLayoutPanel4;
		private TableLayoutPanel tableLayoutPanel5;
		private TableLayoutPanel tableLayoutPanel6;
		private TableLayoutPanel tableLayoutPanel7;
		private TableLayoutPanel tableLayoutPanel8;
		private TableLayoutPanel tableLayoutPanel9;
	}
}