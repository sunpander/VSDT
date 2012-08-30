using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
  
        }

        private void button1_Click(object sender, EventArgs e)
        {

            DataTable dt= new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("Name");
            for(int i =0;i<10;i++)
            {
                 dt.Rows.Add("Id"+i,"NAME"+i);
            }
            dataGridView1.DataSource = dt;
            gridControl1.DataSource = dt;
            gridControl2.DataSource = dt;


            gridView1.OptionsSelection.EnableAppearanceFocusedRow = false;
            FormEPEDEXCEL frm = new FormEPEDEXCEL();
            frm.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //任务栏不显示
            this.ShowInTaskbar = false;
            //托盘图标
            this.components = new System.ComponentModel.Container();
            NotifyIcon icon = new NotifyIcon(components);
   
            icon.Icon = WindowsFormsApplication1.Properties.Resources.IconCapture;
            icon.Text = "notifyIcon1";
            icon.Visible = true;
            //气泡提示
            icon.BalloonTipIcon = ToolTipIcon.Info;
            icon.BalloonTipText = "内容";
            icon.BalloonTipTitle = "title";
            icon.ShowBalloonTip(3000);
 
        }

        private void buttonComboBox1_OnImageClicked(int index)
        {
            MessageBox.Show("delete"+index);

        }

        private void buttonComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (buttonComboBox1.SelectedIndex < 0)
                return;
            MessageBox.Show(buttonComboBox1.SelectedItem.ToString());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            buttonComboBox1.ButtonIcon = WindowsFormsApplication1.Properties.Resources.IconCapture;
            buttonComboBox1.Items.Add("145434434");
            buttonComboBox1.Items.Add("245434434");
            buttonComboBox1.Items.Add("345434434");
          
        }
    }
}
