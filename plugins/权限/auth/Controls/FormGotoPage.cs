using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EF
{
    internal partial class FormGotoPage : DevExpress.XtraEditors.XtraForm
    {
        public FormGotoPage()
        {
            InitializeComponent();
        }
 
        public int PageTo = 0;
         

        public int TotalRecordCount = 0;
        public int PageSize = 0;
        private int pageCount = 0;
        private void btnOk_Click(object sender, EventArgs e)
        {
            Int32.TryParse(this.efDevSpinPageSize.EditValue.ToString(), out PageSize);
            Int32.TryParse(this.efDevSpinPageTo.EditValue.ToString(), out PageTo);
            if (PageSize <= 0) //PageSize > TotalRecordCount 
            {
                EF.EFMessageBox.Show("每页大小输入值有误,大于0。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                efDevSpinPageSize.SelectAll();
                return;
            }
            if (PageTo > pageCount || PageTo <= 0) 
            {
                EFMessageBox.Show("跳转到页数,输入值有误.\r\n 提示:大于0小于总页数", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                efDevSpinPageTo.SelectAll();
                return;
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormGotoPage_Load(object sender, EventArgs e)
        {
            try
            {
                this.efDevSpinPageSize.EditValue = PageSize;
                this.lblTotalRecordCount.Text = string.Format("共{0}条", TotalRecordCount);
                if (PageSize == 0) PageSize = 1;
                
                pageCount = TotalRecordCount / PageSize;
                if (TotalRecordCount > pageCount * PageSize)
                {
                    pageCount++;
                }
                this.lbTotalPageCount.Text = string.Format("共{0}页", pageCount);
                this.efDevSpinPageTo.EditValue = 1;
                //this.efDevSpinPageTo.Properties.MaxValue = TotalRecordCount;
                //this.efDevSpinPageTo.Properties.MinValue = 0;
            }
            catch (Exception)
            {
                
                throw;
            }       
        }

        private void efDevSpinPageSize_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            try
            {
                if (e.NewValue != null && int.TryParse(e.NewValue.ToString(), out PageSize))
                {
                    if (PageSize == 0)
                        return;
                    pageCount = TotalRecordCount / PageSize;
                    if (TotalRecordCount > pageCount * PageSize)
                    {
                        pageCount++;
                    }
                    this.lbTotalPageCount.Text = "";
                    this.lbTotalPageCount.Text = string.Format("共{0}页", pageCount);
                }
            }
            catch (Exception ex)
            {

            }
        }

        //private void efDevSpinEdit1_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        //{
        //    int temp = 0;
        //    if (e.NewValue != null && int.TryParse(e.NewValue.ToString(), out temp))
        //    {
        //        if (temp > TotalRecordCount)
        //        {
        //            efDevSpinPageTo.EditValue = e.OldValue;
        //            efDevSpinPageTo.Text = e.OldValue.ToString();
        //        }
        //    }
        //}
    }
}
