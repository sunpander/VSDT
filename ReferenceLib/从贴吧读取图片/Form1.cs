using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HtmlAgilityPack;
using System.Text.RegularExpressions;
using System.Net;
using System.IO;
using Word = Microsoft.Office.Interop.Word;
namespace WindowsFormsApplication3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public void JitterForm(Form frm)
        {
            Point pOld = frm.Location;//原来的位置
            int radius = 3;//半径

            for (int n = 0; n < 10; n++) //旋转圈数
            {
                //右半圆逆时针
                for (int i = -radius; i <= radius; i++)
                {
                    int x = Convert.ToInt32(Math.Sqrt(radius * radius - i * i));
                    int y = -i;

                    frm.Location = new Point(pOld.X + x, pOld.Y + y);
                    System.Threading.Thread.Sleep(10);
                }
                //左半圆逆时针
                for (int j = radius; j >= -radius; j--)
                {
                    int x = -Convert.ToInt32(Math.Sqrt(radius * radius - j * j));
                    int y = -j;

                    frm.Location = new Point(pOld.X + x, pOld.Y + y);
                    System.Threading.Thread.Sleep(10);
                }
            }

            frm.Location = pOld;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBoxUrl.Text ="http://tieba.baidu.com/p/1780017244"; 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            JitterForm(this);
        }

        private void buttonReadImgSrc_Click(object sender, EventArgs e)
        {
            textBoxUrl.Text = "http://tieba.baidu.com/f/search/res?ie=utf-8&kw=%E6%B5%B7%E8%B4%BC%E7%8E%8B&qw=%E6%89%8B%E7%BB%98&rn=30&un=&sm=1"; //test
            if (!listBoxTitleSrc.Items.Contains(textBoxUrl.Text))
            {
                listBoxTitleSrc.Items.Add(textBoxUrl.Text);
            }
            if (listBoxTitleSrc.Items.Count == 0)
            {
                return;
            }
            for (int i = 0; i < listBoxTitleSrc.Items.Count; i++)
            {
                string tmpUrl = listBoxTitleSrc.Items[i].ToString();
                List<string> list = GetUrlList(textBoxUrl.Text, "//div[@class='s_post']", "href=\"/p/(\\d*?)\\?");
                listBoxImgSrc.Items.Clear();
                foreach (var item in list)
                {
                    string tmp = "http://tieba.baidu.com/p/" + item.ToString();
                    listBoxImgSrc.Items.Add(tmp);
                }
            }
            listBoxTitleSrc.Items.Clear();
        }

        private void buttonSaveToImgFolder_Click(object sender, EventArgs e)
        {
            try
            {
                FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
                if (folderBrowser.ShowDialog() != DialogResult.OK)
                    return;
                imgFolder = folderBrowser.SelectedPath;
                int i = Directory.GetFiles(imgFolder, "*.jpg").Length;  
                foreach (var item in listBoxImgSrc.Items)
                {
                    string urlPath = item.ToString();

                    Uri uri = new Uri(urlPath);
                    System.Net.WebClient webClient = new System.Net.WebClient();
                    webClient.Proxy = null;
                    webClient.DownloadFile(urlPath, Path.Combine(imgFolder, i + ".jpg"));
                    i++;
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }
        string imgFolder = @"D:\\";
        private void buttonWriteToDoc_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(imgFolder) || !Directory.Exists(imgFolder))
                {
                    MessageBox.Show("error"); ;
                }
                int imgCount=  Directory.GetFiles(imgFolder, "*.jpg").Length;
                if (imgCount == 0)
                    return;
                //word
                object srcFileName = Path.Combine(imgFolder, "image.doc");  

                object Nothing = System.Reflection.Missing.Value;
                object format = Word.WdSaveFormat.wdFormatDocument;
                Word.Application wordApp = new Word.ApplicationClass();

                Word.Document wordDoc2= wordApp.Documents.Add(ref Nothing, ref Nothing, ref Nothing, ref Nothing);
                //Word.Document wordDoc2 = wordApp.Documents.Open(ref srcFileName, ref format, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing);
                try
                {
                    int index = 0;
                    while (index < imgCount)
                    {
                        string path = Path.Combine(imgFolder, index + ".jpg");
                        if (File.Exists(path))
                        {
                            wordApp.Selection.InlineShapes.AddPicture(path, ref Nothing, ref Nothing, ref Nothing);
                        }
                        index++;
                    }
                    wordDoc2.SaveAs(ref srcFileName, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing);
                }
                catch { }
                finally
                {
                    //关闭网页wordDoc2
                    wordDoc2.Close(ref Nothing, ref Nothing, ref Nothing);
                    if (wordDoc2 != null)
                    {
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(wordDoc2);
                        wordDoc2 = null;
                    }
                    //关闭wordApp
                    wordApp.Quit(ref Nothing, ref Nothing, ref Nothing);
                    if (wordApp != null)
                    {
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(wordApp);
                        wordApp = null;
                    }
                }
                GC.Collect();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }

        private void buttonReadTitleSrc_Click(object sender, EventArgs e)
        {
            textBoxUrl.Text = "http://tieba.baidu.com/f/search/res?ie=utf-8&kw=%E6%B5%B7%E8%B4%BC%E7%8E%8B&qw=%E6%89%8B%E7%BB%98&rn=30&un=&sm=1"; //test
            List<string> list= GetUrlList(textBoxUrl.Text, "//div[@class='s_post']", "href=\"/p/(\\d*?)\\?");
            listBoxTitleSrc.Items.Clear();
            foreach (var item in list)
            {
                string tmp = "http://tieba.baidu.com/p/" + item.ToString();
                listBoxTitleSrc.Items.Add(tmp);
            }
        }

        private List<string> GetUrlList(string url, string xpath, string regex)
        {
            List<string> listResult = new List<string>();
            try
            {
                Uri uri = new Uri(url);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
                WebResponse response = request.GetResponse();

                Stream stream = response.GetResponseStream();
                StreamReader read = new StreamReader(stream, Encoding.GetEncoding("gb2312"));
                string str = read.ReadToEnd();

                HtmlAgilityPack.HtmlDocument html = new HtmlAgilityPack.HtmlDocument();
                html.LoadHtml(str);
                HtmlNode htmlNode = html.DocumentNode;
                HtmlNodeCollection hnc = htmlNode.SelectNodes(xpath);
                if (hnc.Count < 1)
                    return listResult;
                System.Text.RegularExpressions.Regex re = new System.Text.RegularExpressions.Regex(regex, System.Text.RegularExpressions.RegexOptions.None);
                foreach (HtmlNode node in hnc)
                {
                    System.Text.RegularExpressions.MatchCollection mc = re.Matches(node.InnerHtml);
                    foreach (System.Text.RegularExpressions.Match ma in mc)
                    {
                        string tmp = ma.Groups[1].Value;

                        if (listResult.Contains(tmp))
                            continue;
                        listResult.Add(tmp);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            return listResult;
        }
    }
}
