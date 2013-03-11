using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using shf.Utility;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections;

namespace Utility
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonRead_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() == "")
            {
                FolderBrowserDialog folddiag = new FolderBrowserDialog();
                if (folddiag.ShowDialog() != DialogResult.OK)
                    return;
                textBox1.Text = folddiag.SelectedPath;
            }
            string path = textBox1.Text; // @"F:\新建文件夹 (2)\Debug_MQ_99627\MQ";

            FileOperation myFileOper = new FileOperation();
             
            textBox1.Text = path;
 
            ArrayList list =  myFileOper.GetAllDirFileList(new DirectoryInfo(path), "*.cs");
            for (int i = 0; i < list.Count; i++)
            {
                //当前文件名称
                string curFileName = list[i].ToString();
                //文件内容
                string mainFileContent = myFileOper.GetAllFileContent(curFileName);
              
                mainFileContent = mainFileContent.Replace("\r\n", "\n");
                string tmp = mainFileContent;
                //替换  /* */  注释
                ReplaceRemarkCode("\\S*k__BackingField", ref mainFileContent);
                if (tmp != mainFileContent)
                {
                    myFileOper.CreateFile(curFileName, mainFileContent);
                }
            }
        }
        static StringOperation myStrOper = new StringOperation();
        public static void ReplaceRemarkCode(string express, ref string oldCode)
        {
            Regex re = new Regex(express, RegexOptions.None);
            MatchCollection mc = re.Matches(oldCode);
            foreach (Match ma in mc)
            {
               string tmp = ma.Value;
               string tmp2 = tmp.Replace(">k__BackingField", "");
               tmp2 = tmp2.Replace("<", "_");
               oldCode = oldCode.Replace(tmp, tmp2);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() == "")
            {
                FolderBrowserDialog folddiag = new FolderBrowserDialog();
                if (folddiag.ShowDialog() != DialogResult.OK)
                    return;
                textBox1.Text = folddiag.SelectedPath;
            }
            listBox1.Items.Clear();
            string path = textBox1.Text; // @"F:\新建文件夹 (2)\Debug_MQ_99627\MQ";

            FileOperation myFileOper = new FileOperation();
       
            ArrayList list = myFileOper.GetAllDirFileList(new DirectoryInfo(path), "*.cs");
            for (int i = 0; i < list.Count; i++)
            {
                //当前文件名称
                string curFileName = list[i].ToString();
                //文件内容
                string mainFileContent = myFileOper.GetAllFileContent(curFileName);

                mainFileContent = mainFileContent.Replace(textBoxOld.Text, textBoxNew.Text);

                myFileOper.CreateFile(curFileName, mainFileContent);
                listBox1.Items.Add(curFileName);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
