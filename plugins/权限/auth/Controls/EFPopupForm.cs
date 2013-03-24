using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace EF
{
    /// <summary>
    /// 弹出窗体,包含确定,取消按钮
    /// </summary>
    public partial class EFPopupForm : DevExpress.XtraEditors.XtraForm
    {
        public EFPopupForm()
        {
            InitializeComponent();
        }

        private void efBtnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
