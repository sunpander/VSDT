namespace VSDT.WinPlatformDev
{
    partial class FormMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            this.dockManager1 = new DevExpress.XtraBars.Docking.DockManager(this.components);
            this.dockPanel1 = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel1_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.buttonEditMain = new DevExpress.XtraEditors.ButtonEdit();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.barTools = new DevExpress.XtraBars.Bar();
            this.barButtonItemExit = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemTest = new DevExpress.XtraBars.BarButtonItem();
            this.barMenu = new DevExpress.XtraBars.Bar();
            this.barStatus = new DevExpress.XtraBars.Bar();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.treeList1 = new DevExpress.XtraTreeList.TreeList();
            this.xtraTabbedMdiManager1 = new DevExpress.XtraTabbedMdi.XtraTabbedMdiManager(this.components);
            this.imageCollection1 = new DevExpress.Utils.ImageCollection(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
            this.dockPanel1.SuspendLayout();
            this.dockPanel1_Container.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.buttonEditMain.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabbedMdiManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            this.SuspendLayout();
            // 
            // dockManager1
            // 
            this.dockManager1.Form = this;
            this.dockManager1.RootPanels.AddRange(new DevExpress.XtraBars.Docking.DockPanel[] {
            this.dockPanel1});
            this.dockManager1.TopZIndexControls.AddRange(new string[] {
            "DevExpress.XtraBars.BarDockControl",
            "DevExpress.XtraBars.StandaloneBarDockControl",
            "System.Windows.Forms.StatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonStatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonControl"});
            // 
            // dockPanel1
            // 
            this.dockPanel1.Controls.Add(this.dockPanel1_Container);
            this.dockPanel1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Left;
            this.dockPanel1.ID = new System.Guid("e4effc4f-bf22-4d73-a146-c62f4ae0e891");
            this.dockPanel1.Location = new System.Drawing.Point(0, 44);
            this.dockPanel1.Name = "dockPanel1";
            this.dockPanel1.OriginalSize = new System.Drawing.Size(191, 200);
            this.dockPanel1.Size = new System.Drawing.Size(191, 322);
            this.dockPanel1.Text = "功能导航";
            // 
            // dockPanel1_Container
            // 
            this.dockPanel1_Container.Controls.Add(this.labelControl1);
            this.dockPanel1_Container.Controls.Add(this.buttonEditMain);
            this.dockPanel1_Container.Controls.Add(this.treeList1);
            this.dockPanel1_Container.Location = new System.Drawing.Point(3, 25);
            this.dockPanel1_Container.Name = "dockPanel1_Container";
            this.dockPanel1_Container.Size = new System.Drawing.Size(185, 294);
            this.dockPanel1_Container.TabIndex = 0;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(9, 14);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(28, 14);
            this.labelControl1.TabIndex = 2;
            this.labelControl1.Text = "搜索:";
            // 
            // buttonEditMain
            // 
            this.buttonEditMain.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonEditMain.Location = new System.Drawing.Point(44, 11);
            this.buttonEditMain.MenuManager = this.barManager1;
            this.buttonEditMain.Name = "buttonEditMain";
            this.buttonEditMain.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, ((System.Drawing.Image)(resources.GetObject("buttonEditMain.Properties.Buttons"))), new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "", null, null, true)});
            this.buttonEditMain.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.buttonEditMain.Size = new System.Drawing.Size(138, 21);
            this.buttonEditMain.TabIndex = 1;
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.barTools,
            this.barMenu,
            this.barStatus});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.DockManager = this.dockManager1;
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barButtonItemExit,
            this.barButtonItemTest});
            this.barManager1.MainMenu = this.barMenu;
            this.barManager1.MaxItemId = 2;
            this.barManager1.StatusBar = this.barStatus;
            // 
            // barTools
            // 
            this.barTools.BarName = "Tools";
            this.barTools.DockCol = 0;
            this.barTools.DockRow = 1;
            this.barTools.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.barTools.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemExit),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemTest)});
            this.barTools.Text = "Tools";
            // 
            // barButtonItemExit
            // 
            this.barButtonItemExit.Caption = "Exit";
            this.barButtonItemExit.Id = 0;
            this.barButtonItemExit.Name = "barButtonItemExit";
            this.barButtonItemExit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemExit_ItemClick);
            // 
            // barButtonItemTest
            // 
            this.barButtonItemTest.Caption = "测试";
            this.barButtonItemTest.Id = 1;
            this.barButtonItemTest.Name = "barButtonItemTest";
            this.barButtonItemTest.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemTest_ItemClick);
            // 
            // barMenu
            // 
            this.barMenu.BarName = "Main menu";
            this.barMenu.DockCol = 0;
            this.barMenu.DockRow = 0;
            this.barMenu.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.barMenu.OptionsBar.MultiLine = true;
            this.barMenu.OptionsBar.UseWholeRow = true;
            this.barMenu.Text = "Main menu";
            // 
            // barStatus
            // 
            this.barStatus.BarName = "Status bar";
            this.barStatus.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom;
            this.barStatus.DockCol = 0;
            this.barStatus.DockRow = 0;
            this.barStatus.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
            this.barStatus.OptionsBar.AllowQuickCustomization = false;
            this.barStatus.OptionsBar.DrawDragBorder = false;
            this.barStatus.OptionsBar.UseWholeRow = true;
            this.barStatus.Text = "Status bar";
            // 
            // treeList1
            // 
            this.treeList1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.treeList1.Location = new System.Drawing.Point(3, 38);
            this.treeList1.Name = "treeList1";
            this.treeList1.Size = new System.Drawing.Size(179, 253);
            this.treeList1.TabIndex = 0;
            // 
            // xtraTabbedMdiManager1
            // 
            this.xtraTabbedMdiManager1.ClosePageButtonShowMode = DevExpress.XtraTab.ClosePageButtonShowMode.InActiveTabPageAndTabControlHeader;
            this.xtraTabbedMdiManager1.MdiParent = this;
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.Images.SetKeyName(0, "folder.png");
            this.imageCollection1.Images.SetKeyName(1, "window_gear.png");
            this.imageCollection1.Images.SetKeyName(2, "clock.png");
            this.imageCollection1.Images.SetKeyName(3, "folder.png");
            this.imageCollection1.Images.SetKeyName(4, "folder_closed.png");
            this.imageCollection1.Images.SetKeyName(5, "window_gear.png");
            this.imageCollection1.Images.SetKeyName(6, "gear.png");
            this.imageCollection1.Images.SetKeyName(7, "refresh.ico");
            this.imageCollection1.Images.SetKeyName(8, "unknown.png");
            this.imageCollection1.Images.SetKeyName(9, "Comp.png");
            this.imageCollection1.Images.SetKeyName(10, "PASS.GIF");
            this.imageCollection1.Images.SetKeyName(11, "SHELL32_194.ico");
            this.imageCollection1.Images.SetKeyName(12, "clipboard.ico");
            this.imageCollection1.Images.SetKeyName(13, "export1.ico");
            this.imageCollection1.Images.SetKeyName(14, "printer.ico");
            this.imageCollection1.Images.SetKeyName(15, "pagination_first.gif");
            this.imageCollection1.Images.SetKeyName(16, "pagination_prev.gif");
            this.imageCollection1.Images.SetKeyName(17, "pagination_next.gif");
            this.imageCollection1.Images.SetKeyName(18, "pagination_last.gif");
            this.imageCollection1.Images.SetKeyName(19, "user1_lock.ico");
            this.imageCollection1.Images.SetKeyName(20, "Exit.ico");
            this.imageCollection1.Images.SetKeyName(21, "msenv_6834.ico");
            this.imageCollection1.Images.SetKeyName(22, "delete2.ico");
            this.imageCollection1.Images.SetKeyName(23, "Save-icon.png");
            this.imageCollection1.Images.SetKeyName(24, "find.ico");
            this.imageCollection1.Images.SetKeyName(25, "通用排序.ico");
            this.imageCollection1.Images.SetKeyName(26, "通用查询.bmp");
            this.imageCollection1.Images.SetKeyName(27, "urlmon_100.ico");
            this.imageCollection1.Images.SetKeyName(28, "blue_add .ico");
            this.imageCollection1.Images.SetKeyName(29, "blue_delete.ico");
            this.imageCollection1.Images.SetKeyName(30, "blue_save.ico");
            this.imageCollection1.Images.SetKeyName(31, "view.ico");
            this.imageCollection1.Images.SetKeyName(32, "复制新增.ico");
            this.imageCollection1.Images.SetKeyName(33, "module.png");
            this.imageCollection1.Images.SetKeyName(34, "new2.png");
            this.imageCollection1.Images.SetKeyName(35, "formnew.png");
            this.imageCollection1.Images.SetKeyName(36, "关闭.png");
            this.imageCollection1.Images.SetKeyName(37, "打开.png");
            this.imageCollection1.Images.SetKeyName(38, "Excel-icon.png");
            this.imageCollection1.Images.SetKeyName(39, "Info.png");
            this.imageCollection1.Images.SetKeyName(40, "Warning.png");
            this.imageCollection1.Images.SetKeyName(41, "Error.png");
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(891, 388);
            this.Controls.Add(this.dockPanel1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.IsMdiContainer = true;
            this.Name = "FormMain";
            this.Text = "桌面版";
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
            this.dockPanel1.ResumeLayout(false);
            this.dockPanel1_Container.ResumeLayout(false);
            this.dockPanel1_Container.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.buttonEditMain.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabbedMdiManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.Docking.DockManager dockManager1;
        private DevExpress.XtraTabbedMdi.XtraTabbedMdiManager xtraTabbedMdiManager1;
        private DevExpress.XtraBars.Docking.DockPanel dockPanel1;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel1_Container;
        private DevExpress.XtraTreeList.TreeList treeList1;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar barTools;
        private DevExpress.XtraBars.Bar barMenu;
        private DevExpress.XtraBars.Bar barStatus;
        private DevExpress.XtraEditors.ButtonEdit buttonEditMain;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.Utils.ImageCollection imageCollection1;
        private DevExpress.XtraBars.BarButtonItem barButtonItemExit;
        private DevExpress.XtraBars.BarButtonItem barButtonItemTest;

    }
}