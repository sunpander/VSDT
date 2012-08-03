using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
 
using WeifenLuo.WinFormsUI.Docking;
using UtilityLibrary.WinControls;
 
namespace VSDT.WinPlatformMS.UI
{

    public class MainToolWindow : DockContent
    {
        private IContainer components = null;
     //   private AppConfig config = new AppConfig();
        private ImageList imageList;
        private ImageList imageList_BasicInfo;
        private OutlookBar outlookBar1 = null;
        private ImageList imageList1;
        private Panel panel1;

        public MainToolWindow()
        {
            this.InitializeComponent();
            this.InitializeOutlookbar();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainToolWindow));
            this.panel1 = new System.Windows.Forms.Panel();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(192, 493);
            this.panel1.TabIndex = 0;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "gra_h_yuuri-k004.jpg");
            this.imageList1.Images.SetKeyName(1, "20124410100016025310.jpg");
            this.imageList1.Images.SetKeyName(2, "20124410100016034138.jpg");
            this.imageList1.Images.SetKeyName(3, "20124410100016025310.jpg");
            this.imageList1.Images.SetKeyName(4, "20124410100016034138.jpg");
            this.imageList1.Images.SetKeyName(5, "gra_h_yuuri-k004.jpg");
            this.imageList1.Images.SetKeyName(6, "900_3910137117905555815776.jpg");
            // 
            // MainToolWindow
            // 
            this.ClientSize = new System.Drawing.Size(192, 493);
            this.Controls.Add(this.panel1);
            this.HideOnClose = true;
            this.Name = "MainToolWindow";
            this.TabText = "工具窗口";
            this.Text = "工具窗口";
            this.Load += new System.EventHandler(this.MainToolWindow_Load);
            this.ResumeLayout(false);

        }

        private void InitializeOutlookbar()
        {
             this.outlookBar1 = new OutlookBar();
            //if (Portal.gc.HasFunction("WareMis"))
            //{
               OutlookBarBand band = new OutlookBarBand("仓库管理");
                band.SmallImageList = this.imageList1;
                band.LargeImageList = this.imageList1;

                band.Items.Add(new OutlookBarItem("备件入库", 0));

                band.Items.Add(new OutlookBarItem("备件出库", 1));

                band.Items.Add(new OutlookBarItem("库存查询", 2));

                band.Items.Add(new OutlookBarItem("备件信息", 3));

                band.Items.Add(new OutlookBarItem("数据字典", 4));

                band.Items.Add(new OutlookBarItem("业务报表", 5));
       
                    band.Items.Add(new OutlookBarItem("库房管理", 6));
            
                band.Background = SystemColors.AppWorkspace;
                band.TextColor = Color.White;
                this.outlookBar1.Bands.Add(band);
    
            this.outlookBar1.Dock = DockStyle.Fill;
           // this.outlookBar1.SetCurrentBand(0);
            this.outlookBar1.ItemClicked += new OutlookBarItemClickedHandler(this.OnOutlookBarItemClicked);
            this.outlookBar1.ItemDropped += new OutlookBarItemDroppedHandler(this.OnOutlookBarItemDropped);
            this.panel1.Controls.AddRange(new Control[] { this.outlookBar1 });
        }

        private void MainToolWindow_Load(object sender, EventArgs e)
        {

            OutlookBarBand band = new OutlookBarBand("仓库管理");
            band.SmallImageList = this.imageList1;
            band.LargeImageList = this.imageList1;

            band.Items.Add(new OutlookBarItem("备件入库1", 0));

            band.Items.Add(new OutlookBarItem("备件出库2", 1));

            band.Items.Add(new OutlookBarItem("库存查询3", 2));

            band.Items.Add(new OutlookBarItem("备件信息4", 3));

            band.Items.Add(new OutlookBarItem("数据字典5", 4));

            band.Items.Add(new OutlookBarItem("业务报表6", 5));

            band.Items.Add(new OutlookBarItem("库房管理7", 6));

            band.Background = SystemColors.AppWorkspace;
            band.TextColor = Color.White;
            this.outlookBar1.Bands.Add(band);
        }

        private void OnOutlookBarItemClicked(OutlookBarBand band, OutlookBarItem item)
        {
            switch (item.Text)
            {
                //case "备件入库":
                //    Portal.gc.MainDialog.ShowContent("备件入库", typeof(FrmPurchase));
                //    break;

                //case "备件出库":
                //    Portal.gc.MainDialog.ShowContent("备件出库", typeof(FrmTakeOut));
                //    break;

                //case "库存查询":
                //    Portal.gc.MainDialog.ShowContent("库存查询", typeof(FrmStockSearch));
                //    break;

                //case "备件信息":
                //    Portal.gc.MainDialog.ShowContent("备件信息", typeof(FrmItemDetail));
                //    break;

                //case "数据字典":
                //    new FrmDictionary().Show();
                //    break;

                //case "业务报表":
                //    new FrmReports().ShowDialog();
                //    break;

                //case "库房管理":
                //    new FrmWareHouse().ShowDialog();
                //    break;
            }
        }

        private void OnOutlookBarItemDropped(OutlookBarBand band, OutlookBarItem item)
        {
        }
    }
}

