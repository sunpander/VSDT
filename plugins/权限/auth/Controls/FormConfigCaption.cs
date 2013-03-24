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
    /// <summary>
    /// 配置gridview的列标题
    /// </summary>
    internal partial class FormConfigCaption : EFPopupForm
    {
        public FormConfigCaption()
        {
            InitializeComponent();
        }

        private DevExpress.XtraGrid.Views.Grid.GridView gridViewCongif;

        public DevExpress.XtraGrid.Views.Grid.GridView GridViewCongif
        {
            get { return gridViewCongif; }
            set { gridViewCongif = value; }
        }

        private void FormConfigCaption_Load(object sender, EventArgs e)
        {
            try
            {
                BindData();
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(string.Format("加载配置列标题窗体,出错:{0}", ex.Message));
            }
        }
        /// <summary>
        ///根据gridviewConfig加载数据
        /// </summary>
        private void BindData()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ColumnName");
            dt.Columns.Add("FieldName");
            dt.Columns.Add("Caption");
            dt.Columns.Add("NewCaption");
            DataRow dr;
            if (this.gridViewCongif != null)
            {
                foreach (DevExpress.XtraGrid.Columns.GridColumn gridColumn in gridViewCongif.Columns)
                {
                    //如果列不可配置,则标题也不可配置
                    if (!gridColumn.OptionsColumn.ShowInCustomizationForm)
                    {
                        continue;
                    }
                    string caption = gridColumn.Caption;
                    string fieldName = gridColumn.FieldName;
                    string name = gridColumn.Name;
      
                    if (string.IsNullOrEmpty(caption.Trim()))
                    {
                        caption = ((DevExpress.Data.IDataColumnInfo)gridColumn).Caption;
                    }
                    string newCaption = caption;
                    dr = dt.NewRow();
                    dr["ColumnName"] = name;
                    dr["FieldName"] = fieldName;
                    dr["Caption"] = caption;
                    dr["NewCaption"] = newCaption;
                    dt.Rows.Add(dr);                    
                }
                this.efDevGrid1.DataSource = dt;
            }
        }
 

        private void efBtnOk_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = (DataTable)this.efDevGrid1.DataSource;
                if (dt != null && dt.Rows.Count > 0)
                {
                    if (dt.Columns.Contains("ColumnName") && dt.Columns.Contains("NewCaption"))
                    {
                        foreach (DevExpress.XtraGrid.Columns.GridColumn gridColumn in gridViewCongif.Columns)
                        {

                            string name = gridColumn.Name;

                            DataRow[] drs = dt.Select("ColumnName = '" + name + "'");
                            if (drs.Length > 0)
                            {
                                string newCaption = drs[0]["NewCaption"].ToString();
                                if (newCaption.Trim().Equals(""))
                                    continue;
                                gridColumn.Caption = newCaption;
                            }
                        }
                        //保存所有项
                        gridViewCongif.OptionsLayout.Columns.StoreAllOptions = true;
                    }
                }
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }
        }
    }
}
