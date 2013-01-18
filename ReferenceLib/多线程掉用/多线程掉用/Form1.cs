using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 多线程掉用
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("ID",typeof(int));
                dt.Columns.Add("Name");
                for (int i = 0; i < 10; i++)
                {
                    dt.Rows.Add(i, "NAME" + i);
                }
                FormProgressWindows frm = new FormProgressWindows();
                frm.TableTcList = dt;
                frm.ShowDialog();

            }
            catch (Exception ex)
            {
                
                System.Windows.Forms.MessageBox.Show(ex.Message);
                //throw;
            }
        }
    }
}
