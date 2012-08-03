using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VSDT.WinPlatformMS
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, EventArgs e)
        {
            //gridView1.getsele
            int focusedRow = gridView1.FocusedRowHandle;
            if (gridView1.IsGroupRow(focusedRow))
            {
                int count = gridView1.GetChildRowCount(focusedRow);
                for (int i = 0; i < count; i++)
                {
                    int rowHandle = gridView1.GetChildRowHandle(focusedRow, i); 
                }
            }
        }
    }
}
