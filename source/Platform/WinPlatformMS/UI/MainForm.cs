using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using VSDT.WinPlatformMS.Properties;
using WHC.OrderWater.Commons;
 
namespace VSDT.WinPlatformMS.UI
{
    public class MainForm : Form
    {
        private BackgroundWorker annualWorker;
        private IContainer components = null;
        private ContextMenuStrip contextMenuStrip1;
        private DockPanel dockPanel;
        //private FrmItemDetail frmItemDetail = new FrmItemDetail();
        //private FrmPurchase frmPuchase = new FrmPurchase();
        //private FrmStockSearch frmStockSearch = new FrmStockSearch();
        //private FrmTakeOut frmTakeOut = new FrmTakeOut();
        private bool m_bSaveLayout = true;
        private DeserializeDockContent m_deserializeDockContent;
        private MenuStrip mainMenu;
        private ToolStrip mainTool;
        private MainToolWindow mainToolWin = new MainToolWindow();
        private ToolStripMenuItem menu_Basic_Security;
        private ToolStripMenuItem menu_Basic_SystemMessage;
        private ToolStripMenuItem menu_Help;
        private ToolStripMenuItem menu_Help_About;
        private ToolStripMenuItem menu_Help_Bank;
        private ToolStripMenuItem menu_Help_Help;
        private ToolStripMenuItem menu_Help_Register;
        private ToolStripMenuItem menu_Help_Support;
        private ToolStripMenuItem menu_run_systemLog;
        private ToolStripMenuItem menu_System;
        private ToolStripMenuItem menu_System_Exit;
        private ToolStripMenuItem menu_System_Password;
        private ToolStripMenuItem menu_Tool_Caculator;
        private ToolStripMenuItem menu_Tool_Notepad;
        private ToolStripMenuItem menu_Tool_Paint;
        private ToolStripMenuItem menu_ToolSet;
        private ToolStripMenuItem menu_Window;
        private ToolStripMenuItem menu_Window_CloseAll;
        private ToolStripMenuItem menu_Window_CloseOther;
        private ToolStripMenuItem menu_Window_Refresh;
        private NotifyIcon notifyIcon1;
        private ToolStripMenuItem notifyMenu_About;
        private ToolStripMenuItem notifyMenu_Exit;
        private ToolStripMenuItem notifyMenu_Show;
        private ToolStripProgressBar progressBar;
        private bool ShowToolBar = true;
        private StatusStrip statusStrip1;
        private ToolStripMenuItem testToolStripMenuItem;
        private System.Timers.Timer timerCurrent;
        private ToolStripButton tool_Dict;
        private ToolStripButton tool_MainWin;
        private ToolStripButton tool_Quit;
        private ToolStripSeparator toolStripButton2;
        private ToolStripLabel toolStripLabel1;
        private ToolStripSeparator toolStripSeparator10;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripSeparator toolStripSeparator8;
        private ToolStripSeparator toolStripSeparator9;
        private ToolStripStatusLabel toolStripStatusLabel2;
        private ToolStripStatusLabel toolStripStatusLabel3;
        private ToolStripLabel tslTime;
        public ToolStripStatusLabel tsslDate;
        public ToolStripStatusLabel tsslLogin;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private BackgroundWorker worker;

        public MainForm()
        {
            this.InitializeComponent();
            //Splasher.Status = "正在展示相关的内容...";
            Thread.Sleep(100);
            if (!File.Exists(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "DockPanel.config")))
            {
                this.mainToolWin.Show(this.dockPanel, DockState.DockLeft);
                //if (Portal.gc.HasFunction("Purchase"))
                //{
                //    this.frmPuchase.Show(this.dockPanel);
                //}
                //if (Portal.gc.HasFunction("TakeOut"))
                //{
                //    this.frmTakeOut.Show(this.dockPanel);
                //}
                //if (Portal.gc.HasFunction("StockSearch"))
                //{
                //    this.frmStockSearch.Show(this.dockPanel);
                //}
                //if (Portal.gc.HasFunction("ItemDetail"))
                //{
                //    this.frmItemDetail.Show(this.dockPanel);
                //}
            }
            //Splasher.Status = "初始化完毕...";
            //Thread.Sleep(50);
            //Splasher.Close();
            this.worker = new BackgroundWorker();
            this.worker.DoWork += new DoWorkEventHandler(this.worker_DoWork);
            this.worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.worker_RunWorkerCompleted);
            this.worker.WorkerReportsProgress = true;
            this.worker.WorkerSupportsCancellation = true;
            this.worker.ProgressChanged += new ProgressChangedEventHandler(this.worker_ProgressChanged);
            this.annualWorker = new BackgroundWorker();
            this.annualWorker.DoWork += new DoWorkEventHandler(this.annualWorker_DoWork);
            this.annualWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.annualWorker_RunWorkerCompleted);
            this.annualWorker.WorkerReportsProgress = true;
            this.annualWorker.WorkerSupportsCancellation = true;
            this.annualWorker.ProgressChanged += new ProgressChangedEventHandler(this.annualWorker_ProgressChanged);
        }

        private void annualWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            //this.ExecuteAnnualCostReport();
        }

        private void annualWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.progressBar.Value = e.ProgressPercentage;
        }

        private void annualWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //MessageUtil.ShowTips("年度汇总操作顺利完成！");
            //this.menu_AnnualStatistic.Enabled = true;
            this.progressBar.Visible = false;
        }

        public void CloseAllDocuments()
        {
            if (this.dockPanel.DocumentStyle == DocumentStyle.SystemMdi)
            {
                foreach (Form form in base.MdiChildren)
                {
                    form.Close();
                }
            }
            else
            {
                IDockContent[] contentArray = this.dockPanel.DocumentsToArray();
                foreach (IDockContent content in contentArray)
                {
                    content.DockHandler.Close();
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }
 
    
        private DockContent FindDocument(string text)
        {
            if (this.dockPanel.DocumentStyle == DocumentStyle.SystemMdi)
            {
                foreach (Form form in base.MdiChildren)
                {
                    if (form.Text == text)
                    {
                        return (form as DockContent);
                    }
                }
                return null;
            }
            foreach (DockContent content in this.dockPanel.Documents)
            {
                if (content.DockHandler.TabText == text)
                {
                    return content;
                }
            }
            return null;
        }

        //private ReportAnnualCostHeaderInfo GetAnnualMainHeader()
        //{
        //    ReportAnnualCostHeaderInfo info = new ReportAnnualCostHeaderInfo();
        //    info.CreateDate = DateTime.Now;
        //    info.Creator = Portal.gc.LoginInfo.get_FullName();
        //    info.ReportYear = DateTime.Now.Year;
        //    return info;
        //}

        private IDockContent GetContentFromPersistString(string persistString)
        {
            if (persistString == typeof(MainToolWindow).ToString())
            {
                return this.mainToolWin;
            }
            //if ((persistString == typeof(FrmPurchase).ToString()) && Portal.gc.HasFunction("Purchase"))
            //{
            //    return this.frmPuchase;
            //}
            //if ((persistString == typeof(FrmTakeOut).ToString()) && Portal.gc.HasFunction("TakeOut"))
            //{
            //    return this.frmTakeOut;
            //}
            //if ((persistString == typeof(FrmStockSearch).ToString()) && Portal.gc.HasFunction("StockSearch"))
            //{
            //    return this.frmStockSearch;
            //}
            //if ((persistString == typeof(FrmItemDetail).ToString()) && Portal.gc.HasFunction("ItemDetail"))
            //{
            //    return this.frmItemDetail;
            //}
            return null;
        }

        //private ReportMonthlyHeaderInfo GetMainHeader()
        //{
        //    ReportMonthlyHeaderInfo info = new ReportMonthlyHeaderInfo();
        //    info.CreateDate = DateTime.Now;
        //    info.Creator = Portal.gc.LoginInfo.get_FullName();
        //    info.ReportMonth = DateTime.Now.Month;
        //    info.ReportYear = DateTime.Now.Year;
        //    info.YearMonth = DateTime.Now.ToString("yyyy年MM月");
        //    return info;
        //}

        private void InitAuthorizedUI(){ 
        //{
        //    this.tool_Report.Enabled = Portal.gc.HasFunction("Report");
        //    this.tool_Dict.Enabled = Portal.gc.HasFunction("Dictionary");
        //    this.tool_ItemDetail.Enabled = Portal.gc.HasFunction("ItemDetail");
        //    this.tool_Purchase.Enabled = Portal.gc.HasFunction("Purchase");
        //    this.tool_StockSearch.Enabled = Portal.gc.HasFunction("StockSearch");
        //    this.tool_TakeOut.Enabled = Portal.gc.HasFunction("TakeOut");
        //    this.menu_WareHouse.Enabled = Portal.gc.HasFunction("WareHouse");
        //    this.menu_Dictionary.Enabled = Portal.gc.HasFunction("Dictionary");
        //    this.menu_run_systemLog.Enabled = Portal.gc.HasFunction("LoginLog");
        //    this.menu_Parameters.Enabled = Portal.gc.HasFunction("Parameters");
        //    this.menu_MonthlyStatistic.Enabled = Portal.gc.HasFunction("MonthlyStatistic");
        //    this.menu_AnnualStatistic.Enabled = Portal.gc.HasFunction("AnnualStatistic");
        //    this.menu_ClearAll.Enabled = Portal.gc.HasFunction("ClearAllData");
        //    this.menu_ImportItemDetail.Enabled = Portal.gc.HasFunction("ImportItemDetail");
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.menu_System = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_System_Password = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_run_systemLog = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_Basic_SystemMessage = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_Basic_Security = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.menu_System_Exit = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_ToolSet = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_Tool_Caculator = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_Tool_Notepad = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_Tool_Paint = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_Window = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_Window_CloseAll = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_Window_CloseOther = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_Window_Refresh = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_Help = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_Help_About = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_Help_Help = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_Help_Register = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            this.menu_Help_Support = new System.Windows.Forms.ToolStripMenuItem();
            this.testToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_Help_Bank = new System.Windows.Forms.ToolStripMenuItem();
            this.mainTool = new System.Windows.Forms.ToolStrip();
            this.tool_MainWin = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripSeparator();
            this.tool_Dict = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tool_Quit = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.tslTime = new System.Windows.Forms.ToolStripLabel();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.notifyMenu_About = new System.Windows.Forms.ToolStripMenuItem();
            this.notifyMenu_Show = new System.Windows.Forms.ToolStripMenuItem();
            this.notifyMenu_Exit = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tsslDate = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslLogin = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.progressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.dockPanel = new WeifenLuo.WinFormsUI.Docking.DockPanel();
            this.mainMenu.SuspendLayout();
            this.mainTool.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenu
            // 
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu_System,
            this.menu_ToolSet,
            this.menu_Window,
            this.menu_Help});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Size = new System.Drawing.Size(1016, 25);
            this.mainMenu.TabIndex = 6;
            this.mainMenu.Text = "menuStrip1";
            // 
            // menu_System
            // 
            this.menu_System.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu_System_Password,
            this.menu_run_systemLog,
            this.menu_Basic_SystemMessage,
            this.menu_Basic_Security,
            this.toolStripSeparator8,
            this.menu_System_Exit});
            this.menu_System.Name = "menu_System";
            this.menu_System.Size = new System.Drawing.Size(59, 21);
            this.menu_System.Text = "系统(&S)";
            // 
            // menu_System_Password
            // 
            this.menu_System_Password.Name = "menu_System_Password";
            this.menu_System_Password.Size = new System.Drawing.Size(163, 22);
            this.menu_System_Password.Text = "修改登录密码(&P)";
            this.menu_System_Password.Click += new System.EventHandler(this.menu_System_Password_Click);
            // 
            // menu_run_systemLog
            // 
            this.menu_run_systemLog.Name = "menu_run_systemLog";
            this.menu_run_systemLog.Size = new System.Drawing.Size(163, 22);
            this.menu_run_systemLog.Text = "系统登录日志(&L)";
            this.menu_run_systemLog.Click += new System.EventHandler(this.menu_run_systemLog_Click);
            // 
            // menu_Basic_SystemMessage
            // 
            this.menu_Basic_SystemMessage.Name = "menu_Basic_SystemMessage";
            this.menu_Basic_SystemMessage.Size = new System.Drawing.Size(163, 22);
            this.menu_Basic_SystemMessage.Text = "系统提示信息(&S)";
            this.menu_Basic_SystemMessage.Click += new System.EventHandler(this.menu_Basic_SystemMessage_Click);
            // 
            // menu_Basic_Security
            // 
            this.menu_Basic_Security.Name = "menu_Basic_Security";
            this.menu_Basic_Security.Size = new System.Drawing.Size(163, 22);
            this.menu_Basic_Security.Text = "权限系统登录(&P)";
            this.menu_Basic_Security.Click += new System.EventHandler(this.menu_Basic_Security_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(160, 6);
            // 
            // menu_System_Exit
            // 
            this.menu_System_Exit.Name = "menu_System_Exit";
            this.menu_System_Exit.Size = new System.Drawing.Size(163, 22);
            this.menu_System_Exit.Text = "退出(&X)";
            this.menu_System_Exit.Click += new System.EventHandler(this.menu_System_Exit_Click);
            // 
            // menu_ToolSet
            // 
            this.menu_ToolSet.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu_Tool_Caculator,
            this.menu_Tool_Notepad,
            this.menu_Tool_Paint});
            this.menu_ToolSet.Name = "menu_ToolSet";
            this.menu_ToolSet.Size = new System.Drawing.Size(83, 21);
            this.menu_ToolSet.Text = "辅助工具(&T)";
            // 
            // menu_Tool_Caculator
            // 
            this.menu_Tool_Caculator.Name = "menu_Tool_Caculator";
            this.menu_Tool_Caculator.Size = new System.Drawing.Size(164, 22);
            this.menu_Tool_Caculator.Text = "系统计算工具(&C)";
            this.menu_Tool_Caculator.Click += new System.EventHandler(this.menu_Tool_Caculator_Click);
            // 
            // menu_Tool_Notepad
            // 
            this.menu_Tool_Notepad.Name = "menu_Tool_Notepad";
            this.menu_Tool_Notepad.Size = new System.Drawing.Size(164, 22);
            this.menu_Tool_Notepad.Text = "记事本(&N)";
            this.menu_Tool_Notepad.Click += new System.EventHandler(this.menu_Tool_Notepad_Click);
            // 
            // menu_Tool_Paint
            // 
            this.menu_Tool_Paint.Name = "menu_Tool_Paint";
            this.menu_Tool_Paint.Size = new System.Drawing.Size(164, 22);
            this.menu_Tool_Paint.Text = "绘图板(&P)";
            this.menu_Tool_Paint.Click += new System.EventHandler(this.menu_Tool_Paint_Click);
            // 
            // menu_Window
            // 
            this.menu_Window.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu_Window_CloseAll,
            this.menu_Window_CloseOther,
            this.menu_Window_Refresh});
            this.menu_Window.Name = "menu_Window";
            this.menu_Window.Size = new System.Drawing.Size(64, 21);
            this.menu_Window.Text = "窗口(&W)";
            // 
            // menu_Window_CloseAll
            // 
            this.menu_Window_CloseAll.Name = "menu_Window_CloseAll";
            this.menu_Window_CloseAll.Size = new System.Drawing.Size(190, 22);
            this.menu_Window_CloseAll.Text = "关闭所有窗口(&A)";
            this.menu_Window_CloseAll.Click += new System.EventHandler(this.menu_Window_CloseAll_Click);
            // 
            // menu_Window_CloseOther
            // 
            this.menu_Window_CloseOther.Name = "menu_Window_CloseOther";
            this.menu_Window_CloseOther.Size = new System.Drawing.Size(190, 22);
            this.menu_Window_CloseOther.Text = "除此之外全部关闭(&O)";
            this.menu_Window_CloseOther.Click += new System.EventHandler(this.menu_Window_CloseOther_Click);
            // 
            // menu_Window_Refresh
            // 
            this.menu_Window_Refresh.Name = "menu_Window_Refresh";
            this.menu_Window_Refresh.Size = new System.Drawing.Size(190, 22);
            this.menu_Window_Refresh.Text = "刷新所有窗口(&R)";
            this.menu_Window_Refresh.Click += new System.EventHandler(this.menu_Window_Refresh_Click);
            // 
            // menu_Help
            // 
            this.menu_Help.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu_Help_About,
            this.menu_Help_Help,
            this.menu_Help_Register,
            this.toolStripSeparator10,
            this.menu_Help_Support,
            this.testToolStripMenuItem,
            this.menu_Help_Bank});
            this.menu_Help.Name = "menu_Help";
            this.menu_Help.Size = new System.Drawing.Size(61, 21);
            this.menu_Help.Text = "帮助(&H)";
            // 
            // menu_Help_About
            // 
            this.menu_Help_About.Name = "menu_Help_About";
            this.menu_Help_About.Size = new System.Drawing.Size(139, 22);
            this.menu_Help_About.Text = "关于(&A)";
            this.menu_Help_About.Click += new System.EventHandler(this.menu_Help_About_Click);
            // 
            // menu_Help_Help
            // 
            this.menu_Help_Help.Name = "menu_Help_Help";
            this.menu_Help_Help.Size = new System.Drawing.Size(139, 22);
            this.menu_Help_Help.Text = "帮助(&F)";
            this.menu_Help_Help.Click += new System.EventHandler(this.menu_Help_Help_Click);
            // 
            // menu_Help_Register
            // 
            this.menu_Help_Register.Name = "menu_Help_Register";
            this.menu_Help_Register.Size = new System.Drawing.Size(139, 22);
            this.menu_Help_Register.Text = "注册(&R)";
            this.menu_Help_Register.Click += new System.EventHandler(this.menu_Help_Register_Click);
            // 
            // toolStripSeparator10
            // 
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            this.toolStripSeparator10.Size = new System.Drawing.Size(136, 6);
            // 
            // menu_Help_Support
            // 
            this.menu_Help_Support.Name = "menu_Help_Support";
            this.menu_Help_Support.Size = new System.Drawing.Size(139, 22);
            this.menu_Help_Support.Text = "技术支持(&S)";
            this.menu_Help_Support.Click += new System.EventHandler(this.menu_Help_Support_Click);
            // 
            // testToolStripMenuItem
            // 
            this.testToolStripMenuItem.Name = "testToolStripMenuItem";
            this.testToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.testToolStripMenuItem.Text = "test";
            this.testToolStripMenuItem.Visible = false;
            this.testToolStripMenuItem.Click += new System.EventHandler(this.testToolStripMenuItem_Click);
            // 
            // menu_Help_Bank
            // 
            this.menu_Help_Bank.Name = "menu_Help_Bank";
            this.menu_Help_Bank.Size = new System.Drawing.Size(139, 22);
            this.menu_Help_Bank.Text = "支付购买(&P)";
            this.menu_Help_Bank.Click += new System.EventHandler(this.menu_Help_Bank_Click);
            // 
            // mainTool
            // 
            this.mainTool.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.mainTool.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tool_MainWin,
            this.toolStripButton2,
            this.tool_Dict,
            this.toolStripSeparator4,
            this.tool_Quit,
            this.toolStripSeparator9,
            this.toolStripLabel1,
            this.tslTime});
            this.mainTool.Location = new System.Drawing.Point(0, 25);
            this.mainTool.Name = "mainTool";
            this.mainTool.Size = new System.Drawing.Size(1016, 25);
            this.mainTool.TabIndex = 7;
            this.mainTool.Text = "toolStrip1";
            // 
            // tool_MainWin
            // 
            this.tool_MainWin.ImageTransparentColor = System.Drawing.Color.White;
            this.tool_MainWin.Name = "tool_MainWin";
            this.tool_MainWin.Size = new System.Drawing.Size(48, 22);
            this.tool_MainWin.Text = "导航条";
            this.tool_MainWin.Click += new System.EventHandler(this.tool_MainWin_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(6, 25);
            // 
            // tool_Dict
            // 
            this.tool_Dict.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tool_Dict.Name = "tool_Dict";
            this.tool_Dict.Size = new System.Drawing.Size(60, 22);
            this.tool_Dict.Text = "数据字典";
            this.tool_Dict.Click += new System.EventHandler(this.tool_Dict_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // tool_Quit
            // 
            this.tool_Quit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tool_Quit.Name = "tool_Quit";
            this.tool_Quit.Size = new System.Drawing.Size(60, 22);
            this.tool_Quit.Text = "退出系统";
            this.tool_Quit.Click += new System.EventHandler(this.tool_Quit_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(44, 22);
            this.toolStripLabel1.Text = "时间：";
            // 
            // tslTime
            // 
            this.tslTime.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.tslTime.ForeColor = System.Drawing.Color.Red;
            this.tslTime.Name = "tslTime";
            this.tslTime.Size = new System.Drawing.Size(61, 22);
            this.tslTime.Text = "00:00:00";
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyIcon1.BalloonTipText = "酒店管理系统";
            this.notifyIcon1.BalloonTipTitle = "[深田之星]酒店管理系统";
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon1.Text = "[深田之星]酒店管理系统";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.notifyMenu_About,
            this.notifyMenu_Show,
            this.notifyMenu_Exit});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(181, 70);
            // 
            // notifyMenu_About
            // 
            this.notifyMenu_About.Name = "notifyMenu_About";
            this.notifyMenu_About.Size = new System.Drawing.Size(180, 22);
            this.notifyMenu_About.Text = "关于(&A)";
            this.notifyMenu_About.Click += new System.EventHandler(this.notifyMenu_About_Click);
            // 
            // notifyMenu_Show
            // 
            this.notifyMenu_Show.Name = "notifyMenu_Show";
            this.notifyMenu_Show.Size = new System.Drawing.Size(180, 22);
            this.notifyMenu_Show.Text = "显示/隐藏主窗口(&S)";
            this.notifyMenu_Show.Click += new System.EventHandler(this.notifyMenu_Show_Click);
            // 
            // notifyMenu_Exit
            // 
            this.notifyMenu_Exit.Name = "notifyMenu_Exit";
            this.notifyMenu_Exit.Size = new System.Drawing.Size(180, 22);
            this.notifyMenu_Exit.Text = "退出(&X)";
            this.notifyMenu_Exit.Click += new System.EventHandler(this.notifyMenu_Exit_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslDate,
            this.toolStripStatusLabel2,
            this.tsslLogin,
            this.toolStripStatusLabel3,
            this.progressBar,
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 691);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1016, 22);
            this.statusStrip1.TabIndex = 10;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tsslDate
            // 
            this.tsslDate.Name = "tsslDate";
            this.tsslDate.Size = new System.Drawing.Size(104, 17);
            this.tsslDate.Text = "当前时间日期信息";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(11, 17);
            this.toolStripStatusLabel2.Text = "|";
            // 
            // tsslLogin
            // 
            this.tsslLogin.Name = "tsslLogin";
            this.tsslLogin.Size = new System.Drawing.Size(104, 17);
            this.tsslLogin.Text = "当前登录用户信息";
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(11, 17);
            this.toolStripStatusLabel3.Text = "|";
            // 
            // progressBar
            // 
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(300, 16);
            this.progressBar.ToolTipText = "正在执行月结操作，请稍候...";
            this.progressBar.Visible = false;
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(131, 17);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // dockPanel
            // 
            this.dockPanel.ActiveAutoHideContent = null;
            this.dockPanel.BackColor = System.Drawing.SystemColors.Control;
            this.dockPanel.DefaultFloatWindowSize = new System.Drawing.Size(150, 150);
            this.dockPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dockPanel.DockBottomPortion = 150;
            this.dockPanel.DockLeftPortion = 200;
            this.dockPanel.DockRightPortion = 200;
            this.dockPanel.DockTopPortion = 150;
            this.dockPanel.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
            this.dockPanel.Location = new System.Drawing.Point(0, 50);
            this.dockPanel.Name = "dockPanel";
            this.dockPanel.RightToLeftLayout = true;
            this.dockPanel.ShowDocumentIcon = true;
            this.dockPanel.Size = new System.Drawing.Size(1016, 641);
            this.dockPanel.TabIndex = 14;
            // 
            // MainForm
            // 
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1016, 713);
            this.Controls.Add(this.dockPanel);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.mainTool);
            this.Controls.Add(this.mainMenu);
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.Color.Black;
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.mainMenu;
            this.Name = "MainForm";
            this.Text = "仓库管理系统";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.MaximizedBoundsChanged += new System.EventHandler(this.MainForm_MaximizedBoundsChanged);
            this.Move += new System.EventHandler(this.MainForm_Move);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            this.mainTool.ResumeLayout(false);
            this.mainTool.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        public void InitMainFromStatus()
        {
            //CCalendar calendar = new CCalendar();
            //this.tsslDate.Text = string.Format("当前日期：{0}", calendar.GetDateInfo(DateTime.Now).Fullinfo);
            //this.tsslLogin.Text = string.Format("当前用户：{0}({1})", Portal.gc.LoginInfo.get_FullName(), Portal.gc.LoginInfo.get_Name());
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (base.WindowState != FormWindowState.Minimized)
            {
                e.Cancel = true;
                base.WindowState = FormWindowState.Minimized;
                this.notifyIcon1.ShowBalloonTip(0xbb8, "程序最小化提示", "图标已经缩小到托盘，打开窗口请双击图标即可。", ToolTipIcon.Info);
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
               // AppConfig config = new AppConfig();
              //  string str = config.AppConfigGet("Manufacturer");
              //  string str2 = config.AppConfigGet("ApplicationName");
              //  string str3 = string.Format("{0}-{1}", str, str2);
              ////  Portal.gc.gAppUnit = str;
              ////  Portal.gc.gAppMsgboxTitle = str3;
              //  this.Text = str3;
              //  this.notifyIcon1.BalloonTipText = str3;
              //  this.notifyIcon1.BalloonTipTitle = str3;
              //  this.notifyIcon1.Text = str3;
            }
            catch
            {
            }
            this.m_deserializeDockContent = new DeserializeDockContent(this.GetContentFromPersistString);
            string path = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "DockPanel.config");
            if (File.Exists(path))
            {
                this.dockPanel.LoadFromXml(path, this.m_deserializeDockContent);
            }
            this.ValidateRegisterStatus();
            //if (!Portal.gc.bRegisted)
            //{
            //}
            this.InitAuthorizedUI();
            this.InitMainFromStatus();
        }

        private void MainForm_MaximizedBoundsChanged(object sender, EventArgs e)
        {
            base.Hide();
        }

        private void MainForm_Move(object sender, EventArgs e)
        {
            if ((this != null) && (base.WindowState == FormWindowState.Minimized))
            {
                base.Hide();
                this.notifyIcon1.ShowBalloonTip(0xbb8, "程序最小化提示", "图标已经缩小到托盘，打开窗口请双击图标即可。", ToolTipIcon.Info);
            }
        }

        private void menu_AnnualStatistic_Click(object sender, EventArgs e)
        {
            if ((MessageUtil.ShowYesNoAndTips("您是否需要执行年度汇总操作？\r\n年度汇总可能会比较耗时，任务执行过程中请勿退出。") == DialogResult.Yes) && !this.annualWorker.IsBusy)
            {
             //   this.menu_AnnualStatistic.Enabled = false;
                this.progressBar.Visible = true;
                this.annualWorker.RunWorkerAsync();
            }
        }

        private void menu_Basic_LogonUser_Click(object sender, EventArgs e)
        {
        }

        private void menu_Basic_Security_Click(object sender, EventArgs e)
        {
            //Portal.StartLogin();
        }

        private void menu_Basic_SystemMessage_Click(object sender, EventArgs e)
        {
            string str2;
            //string wareHouse = Portal.gc.ManagedWareHouse[0].Value;
            //bool flag = BLLFactory<Stock>.Instance.CheckStockLowWarning(wareHouse);
            //if (flag)
            //{
            //    str2 = string.Format("{0} 库存已经处于低库存预警状态\r\n请及时补充库存", wareHouse);
            //    Portal.gc.Notify(string.Format("{0} 低库存预警", wareHouse), str2);
            //}
            //bool flag2 = BLLFactory<Stock>.Instance.CheckStockHighWarning(wareHouse);
            //if (flag2)
            //{
            //    str2 = string.Format("{0} 库存量已经高过超预警库存量\r\n请注意减少库存积压", wareHouse);
            //    Portal.gc.Notify(string.Format("{0} 超库存预警", wareHouse), str2);
            //}
            //if (!(flag || flag2))
            //{
            //    str2 = string.Format("暂无相关的系统提示信息", new object[0]);
            //    Portal.gc.Notify(str2, str2);
            //}
        }

        private void menu_ClearAll_Click(object sender, EventArgs e)
        {
            if (MessageUtil.ShowYesNoAndWarning("本操作是危险操作，仅在系统使用的时候初始化数据库使用，请在操作前确保数据库做了备份或不需备份！\r\n按【是】执行，【否】退出操作。") == DialogResult.Yes)
            {
                try
                {
                    string condition = " 1= 1";
                    //BLLFactory<ItemDetail>.Instance.DeleteByCondition(condition);
                    //BLLFactory<LoginLog>.Instance.DeleteByCondition(condition);
                    //BLLFactory<PurchaseHeader>.Instance.DeleteByCondition(condition);
                    //BLLFactory<PurchaseDetail>.Instance.DeleteByCondition(condition);
                    //BLLFactory<ReportAnnualCostHeader>.Instance.DeleteByCondition(condition);
                    //BLLFactory<ReportAnnualCostDetail>.Instance.DeleteByCondition(condition);
                    //BLLFactory<ReportMonthlyHeader>.Instance.DeleteByCondition(condition);
                    //BLLFactory<ReportMonthlyDetail>.Instance.DeleteByCondition(condition);
                    //BLLFactory<ReportMonthlyCostDetail>.Instance.DeleteByCondition(condition);
                    //BLLFactory<Stock>.Instance.DeleteByCondition(condition);
                    MessageUtil.ShowTips("基础业务数据已经清除，不过保留字典及库房信息。\r\n如需删除字典及库房资料，请进入相应的界面进行删除即可。");
                }
                catch (Exception exception)
                {
                    MessageUtil.ShowError(exception.Message);
                    LogHelper.Error(exception);
                }
            }
        }

        private void menu_Dictionary_Click(object sender, EventArgs e)
        {
            this.tool_Dict_Click(null, null);
        }

        private void menu_Help_About_Click(object sender, EventArgs e)
        {
           // Portal.gc.About();
        }

        private void menu_Help_Bank_Click(object sender, EventArgs e)
        {
            Process.Start("http://www.iqidi.com/Payment.htm");
        }

        private void menu_Help_Help_Click(object sender, EventArgs e)
        {
           // Portal.gc.Help();
        }

        private void menu_Help_Register_Click(object sender, EventArgs e)
        {
            //Portal.gc.ShowRegDlg();
        }

        private void menu_Help_Support_Click(object sender, EventArgs e)
        {
            Process.Start("http://www.iqidi.com/Support.aspx");
        }

        private void menu_ImportItemDetail_Click(object sender, EventArgs e)
        {
            //if (!Portal.gc.bRegisted)
            //{
            //    MessageUtil.ShowWarning("非常抱歉，您不是注册用户，不能使用该功能。");
            //}
            //else
            //{
            //  //  new FrmImportExcelData().ShowDialog();
            //}
        }

        private void menu_MonthlyStatistic_Click(object sender, EventArgs e)
        {
            if ((MessageUtil.ShowYesNoAndTips("您是否需要执行月结？\r\n月结可能会比较耗时，任务执行过程中请勿退出。") == DialogResult.Yes) && !this.worker.IsBusy)
            {
                //this.menu_MonthlyStatistic.Enabled = false;
                this.progressBar.Visible = true;
                this.worker.RunWorkerAsync();
            }
        }

        private void menu_run_systemLog_Click(object sender, EventArgs e)
        {
         //   this.ShowContent("操作人员登录记录", typeof(FrmLogHistroy));
        }

        private void menu_System_Exit_Click(object sender, EventArgs e)
        {
            this.tool_Quit_Click(null, null);
        }

        private void menu_System_Password_Click(object sender, EventArgs e)
        {
           // new FrmModifyPassword().ShowDialog();
        }

        private void menu_Tool_Alipay_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.alipay.com/");
        }

        private void menu_Tool_Caculator_Click(object sender, EventArgs e)
        {
            Process.Start("calc.exe");
        }

        private void menu_Tool_Notepad_Click(object sender, EventArgs e)
        {
            Process.Start("notepad.exe");
        }

        private void menu_Tool_Paint_Click(object sender, EventArgs e)
        {
            Process.Start("msPaint.exe");
        }

        private void menu_Tool_Taobao_Click(object sender, EventArgs e)
        {
            Process.Start("http://www.taobao.com/");
        }

        private void menu_WareHouse_Click(object sender, EventArgs e)
        {
            //new FrmWareHouse().ShowDialog();
        }

        private void menu_Window_CloseAll_Click(object sender, EventArgs e)
        {
            this.CloseAllDocuments();
        }

        private void menu_Window_CloseOther_Click(object sender, EventArgs e)
        {
            if (this.dockPanel.DocumentStyle == DocumentStyle.SystemMdi)
            {
                Form activeMdiChild = base.ActiveMdiChild;
                foreach (Form form2 in base.MdiChildren)
                {
                    if (form2 != activeMdiChild)
                    {
                        form2.Close();
                    }
                }
            }
            else
            {
                foreach (IDockContent content in this.dockPanel.DocumentsToArray())
                {
                    if (!content.DockHandler.IsActivated)
                    {
                        content.DockHandler.Close();
                    }
                }
            }
        }

        private void menu_Window_Refresh_Click(object sender, EventArgs e)
        {
            this.RefreshAllForms();
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.notifyMenu_Show_Click(sender, e);
        }

        private void notifyMenu_About_Click(object sender, EventArgs e)
        {
           // Portal.gc.About();
        }

        private void notifyMenu_Exit_Click(object sender, EventArgs e)
        {
            this.SaveLayout();
            try
            {
                base.ShowInTaskbar = false;
               // Portal.gc.Quit();
            }
            catch
            {
            }
        }

        private void notifyMenu_Show_Click(object sender, EventArgs e)
        {
            if (base.WindowState == FormWindowState.Minimized)
            {
                base.WindowState = FormWindowState.Maximized;
                base.Show();
                base.BringToFront();
                base.Activate();
                base.Focus();
            }
            else
            {
                base.WindowState = FormWindowState.Minimized;
                base.Hide();
            }
        }

        public void RefreshAllForms()
        {
            BaseDock dock;
            if (this.dockPanel.DocumentStyle == DocumentStyle.SystemMdi)
            {
                foreach (Form form in base.MdiChildren)
                {
                    dock = form as BaseDock;
                    if (dock != null)
                    {
                        dock.FormOnLoad();
                    }
                }
            }
            else
            {
                IDockContent[] contentArray = this.dockPanel.DocumentsToArray();
                foreach (IDockContent content in contentArray)
                {
                    dock = content.DockHandler.Form as BaseDock;
                    if (dock != null)
                    {
                        dock.FormOnLoad();
                    }
                }
            }
        }

        private void SaveLayout()
        {
            string fileName = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "DockPanel.config");
            if (this.m_bSaveLayout)
            {
                this.dockPanel.SaveAsXml(fileName);
            }
            else if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }
        }

        public DockContent ShowContent(string caption, Type formType)
        {
            DockContent content = this.FindDocument(caption);
            if (content == null)
            {
                content = new Form1();
              //  content = ChildWinManagement.LoadMdiForm(Portal.gc.MainDialog, formType) as DockContent;
            }
            content.Show(this.dockPanel);
            content.BringToFront();
            return content;
        }

        public void ShowMainToolWin()
        {
            this.mainToolWin.Show(this.dockPanel);
        }

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
           // Portal.gc.Notify("系统信息提示", "图标已经缩小到托盘，打开窗口请双击图标即可。\r\n软件来自爱启迪技术有限公司！\r\n软件支持网站：Http://www.iqidi.com");
        }

        private void timerCurrent_Tick(object sender, EventArgs e)
        {
            this.tslTime.Text = DateTime.Now.ToLongTimeString();
        }

        private void tool_Dict_Click(object sender, EventArgs e)
        {
             new Form2().ShowDialog();
        }

        private void tool_ItemDetail_Click(object sender, EventArgs e)
        {
            this.ShowContent("备件信息", typeof(Form1));
        }

        private void tool_MainWin_Click(object sender, EventArgs e)
        {
            if (this.ShowToolBar)
            {
                this.mainToolWin.Hide();
            }
            else
            {
                this.mainToolWin.Show(this.dockPanel);
            }
            this.ShowToolBar = !this.ShowToolBar;
        }

     

        private void tool_Quit_Click(object sender, EventArgs e)
        {
            this.ShowContent("备件入库", typeof(Form1));
            return;
            if (DialogResult.Yes == MessageUtil.ShowYesNoAndTips("您确定退出此系统么？"))
            {
                base.ShowInTaskbar = false;
                this.SaveLayout();
            }
        }

        private void tool_Report_Click(object sender, EventArgs e)
        {
           // new FrmReports().ShowDialog();
        }

        private void tool_StockSearch_Click(object sender, EventArgs e)
        {
           // this.ShowContent("库存查询", typeof(FrmStockSearch));
        }

        private void tool_TakeOut_Click(object sender, EventArgs e)
        {
           // this.ShowContent("备件出库", typeof(FrmTakeOut));
        }

        private void ValidateRegisterStatus()
        {
            //Portal.gc.CheckRegister();
            //bool flag = Portal.gc.CheckTimeString();
            //if ((!Portal.gc.bRegisted && !flag) && (RegDlg.Instance().ShowDialog() != DialogResult.OK))
            //{
            //    Portal.gc.Quit();
            //}
            //if (!Portal.gc.bRegisted)
            //{
            //    this.Text = this.Text + " [未注册]";
            //    this.menu_Help_Register.Enabled = true;
            //    TimeSpan span = new TimeSpan(DateTime.Now.Ticks - Portal.gc.FirstRunTime.Ticks);
            //    int num = UIConstants.SoftwareProbationDay - Math.Abs(span.Days);
            //    string str = string.Format(" [还剩下{0}天]", num);
            //    this.Text = this.Text + str;
            //}
            //else
            //{
            //    this.Text = this.Text + " [已注册]";
            //    this.menu_Help_Register.Enabled = false;
            //}
        }

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            //this.ExecuteDeptMonthReport();
            //this.ExecuteEachWareMonthlyReport();
            //this.ExecuteAllWareItemTypeMonthlyReport();
            //this.ExecuteEachPartCostMonthlyReport();
        }

        private void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.progressBar.Value = e.ProgressPercentage;
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageUtil.ShowTips("月结操作顺利完成！");
            this.progressBar.Visible = false;
        }
    }
}

