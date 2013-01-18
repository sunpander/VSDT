using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace RichTextBox文本变色
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        ArrayList textSplitList = new ArrayList();
        ColorListClass myColorList;
        ColorObjCollection hotColor;
        private void Form1_Load(object sender, EventArgs e)
        {
            myColorList = new ColorListClass();
            foreach (DictionaryEntry colorObj in myColorList.COLORLIST)
            {
                string colorName = colorObj.Key.ToString();
                ColorComboBox.Items.Add(colorName);
            }
            ColorComboBox.SelectedIndex = 5;

            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("DATA");
            for (int i = 1322; i >1; i--)
            {
                dt.Rows.Add("Id" + i, "".PadLeft(i,'e'));
            }
            dataGridView1.DataSource = dt;
        }

        private void dataGridView1_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ArrayList list = new ArrayList();
            list.Add("a");
            list.Add("23vewr B");
            list.Add("aasdasd");
            list.Add("实施");
            list.Add("美女");
            list.Add("升官");
            string colorName = this.ColorComboBox.Text.Trim();
            if (this.myColorList.COLORLIST.Contains(colorName))
            {
                myCompareFrame1.hotColor = this.hotColor = (ColorObjCollection)this.myColorList.COLORLIST[colorName];
            }
            myCompareFrame1.OnAddNewBox("1", "adfadf", list, "adf");
        }
    }
}
