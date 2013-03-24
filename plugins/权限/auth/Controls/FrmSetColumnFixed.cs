using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;

namespace EF
{
    public partial class FormSetColumnFixed : DevExpress.XtraEditors.XtraForm
    {
        private int maxColumnCount = 0;
        public GridView gridView1;

        private int nPreLeftFixColumnCount;
        private int nPreRightFixColumnCount;

        public FormSetColumnFixed()
        {
            InitializeComponent();
        }

        private void efDevSpinEdit1_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            if (e.NewValue == null)
                return;

            decimal tmp1 = 0;
            decimal tmp2 = 0;
            if (sender == efDevSpinEdit1)
            {
                decimal.TryParse(e.NewValue.ToString(), out tmp1);
                tmp2 = (decimal)efDevSpinEdit2.Value;
            }
            else
            {
                tmp1 = (decimal)efDevSpinEdit1.Value;
                decimal.TryParse(e.NewValue.ToString(), out tmp2);
            }


            if ((tmp1 + tmp2) >= maxColumnCount)
            {
                e.Cancel = true;
                //errorProvider1.SetError((Control)sender, string.Format("左停靠与右停靠的列数之和必须小于{0}。", maxColumnCount));
            }
            //else
            //{
            //    errorProvider1.Clear();
            //}

        }

        private bool Apply()
        {
            decimal fixedColumnCount = efDevSpinEdit1.Value;
            decimal fixedColumnCount2 = efDevSpinEdit2.Value;

            if ((fixedColumnCount + fixedColumnCount2) >= maxColumnCount)
            {
                EF.EFMessageBox.Show(this, string.Format("左停靠与右停靠的列数之和必须小于{0}。", maxColumnCount), "错误");
                return false;
            }
            for (int i = maxColumnCount - nPreRightFixColumnCount; i < maxColumnCount; i++)
            {
                gridView1.VisibleColumns[i].Fixed = FixedStyle.None;
            }
            for (int i = nPreLeftFixColumnCount - 1; i >= 0; i--)
            {
                gridView1.VisibleColumns[i].Fixed = FixedStyle.None;
            }

            for (int i = 0; i < fixedColumnCount; i++)
            {
                gridView1.VisibleColumns[i].Fixed = FixedStyle.Left;
            }


            for (int i = maxColumnCount - 1; i > maxColumnCount - fixedColumnCount2 - 1; i--)
            {
                gridView1.VisibleColumns[i].Fixed = FixedStyle.Right;
            }

            nPreLeftFixColumnCount = Convert.ToInt32(fixedColumnCount);
            nPreRightFixColumnCount = Convert.ToInt32(fixedColumnCount2);
            btnApply.Enabled = false;
            return true;

        }
        private void Init()
        {
            maxColumnCount = gridView1.VisibleColumns.Count;
            for (int i = 0; i < maxColumnCount; i++)
            {
                if (gridView1.VisibleColumns[i].Fixed != FixedStyle.Left)
                    break;
                nPreLeftFixColumnCount++;
            }
            for (int i = maxColumnCount - 1; i >= 0; i--)
            {
                if (gridView1.VisibleColumns[i].Fixed != FixedStyle.Right)
                    break;
                nPreRightFixColumnCount++;
            }

            this.efDevSpinEdit1.EditValue = nPreLeftFixColumnCount;
            this.efDevSpinEdit2.EditValue = nPreRightFixColumnCount;
            btnApply.Enabled = false;
        }
        private void FrmSetColumnFixed_Shown(object sender, EventArgs e)
        {
            this.efDevSpinEdit1.Focus();
            Init();
            this.efDevSpinEdit1.ValueChanged += new EventHandler(ValueChanged);
            this.efDevSpinEdit2.ValueChanged += new EventHandler(ValueChanged);
        }

        private void efDevSpinEdit1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.Close();
            }
        }

        void ValueChanged(object sender, EventArgs e)
        {
            if (nPreLeftFixColumnCount != efDevSpinEdit1.Value)
            {
                btnApply.Enabled = true;
            }
            if (nPreRightFixColumnCount != efDevSpinEdit2.Value)
            {
                btnApply.Enabled = true;
            }
        }

        //点击应用按钮
        private void btnApply_Click(object sender, EventArgs e)
        {
            Apply();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (btnApply.Enabled && Apply())
                this.Close();
        }

    }
}
