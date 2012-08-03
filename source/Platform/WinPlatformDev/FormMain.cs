using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VSDT.Common.Utility;
using DevExpress.XtraEditors;

namespace VSDT.WinPlatformDev
{
    public partial class FormMain :XtraForm
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void barButtonItemExit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void barButtonItemTest_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                FormGenConfigClass frm = new FormGenConfigClass();
                frm.MdiParent = this;

                frm.Show();


                Form1 frm2 = new Form1();
                frm2.MdiParent = this;
                frm2.Show();


                Form2 frm22 = new Form2();
                if (frm22.IsMdiContainer)
                {
                    frm22.IsMdiContainer = false;
                    frm22.MdiParent = this;
                }
                else
                {
                    frm22.MdiParent = this;
                }

                frm22.StartPosition = FormStartPosition.CenterScreen;
                frm22.Show();
                frm22.Activate();

                UserControl1 cl = new UserControl1();

                Form1 frm211 = new Form1();
             
                frm211.Controls.Add(cl);
                frm211.IsMdiContainer = false;
                frm211.MdiParent = this;
                frm211.Show();
             
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
 
    }
}
