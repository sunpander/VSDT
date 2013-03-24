using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace auth
{
    public partial class XtraForm1 : DevExpress.XtraEditors.XtraForm
    {
        public XtraForm1()
        {
            InitializeComponent();
        }

        private void XtraForm1_Load(object sender, EventArgs e)
        {
             DataTable dt= new DataTable();
            dt.Columns.Add("CODE");
            dt.Columns.Add("NAME");

            dt.Rows.Add("A", "一班");
            dt.Rows.Add("B", "二班");
            dt.Rows.Add("C", "三班");
            dt.Rows.Add("D", "四班");
            this.repositoryItemLookUpEdit1.DataSource = dt;
            this.repositoryItemLookUpEdit1.DisplayMember = "NAME";
            this.repositoryItemLookUpEdit1.ValueMember = "CODE";
            try
            {
                dataSet1.ReadXml("test.xml");
            }
            catch (Exception ex)
            {
               
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            dataSet1.WriteXml("test.xml");
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                dataSet1.ReadXml("test.xml");
            }
            catch (Exception ex)
            {

            }
        }
    }
}