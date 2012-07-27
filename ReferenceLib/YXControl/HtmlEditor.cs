using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Net.Mail;
using System.IO;

namespace YXControl
{
    /// <summary>
    /// Html编辑器
    /// 源代码来自互联网，由三角猫(DeltaCat)做了进一步扩展和部分修正
    /// http://www.zu14.cn/
    /// 关注技术，支持开源
    /// </summary>
    [Description("Html编辑器"), ClassInterface(ClassInterfaceType.AutoDispatch)]
    public partial class HtmlEditor : UserControl
    {
        public HtmlEditor()
        {
            dataUpdate = 0;

            InitializeComponent();

            InitializeControls();
        }

        #region 扩展属性

        /// <summary>
        /// 获取和设置当前的Html文本
        /// </summary>
        [Category("Qlfui")]
        [Description("正文内容")]
        public string Html
        {
            get
            {
                return webBrowserBody.DocumentText;
            }
            set
            {
                if (!value.Contains("\r\n"))
                { webBrowserBody.DocumentText = value; }//webBrowserBody.Document.Body.InnerHtml = value; }
                //else
                //{  webBrowserBody.Document.Body.InnerHtml = value.Replace("\r\n", "<br>");  }
            }
        }

        /// <summary>
        /// 获取集成了图片资源的HTML BODY 的 MailMessage
        /// </summary>
        public MailMessage XMailMessage
        {
            get
            {
                MailMessage msg = new MailMessage();
                msg.IsBodyHtml = true;

                string html = webBrowserBody.DocumentText;

                HtmlElementCollection images = webBrowserBody.Document.Images;
                if (images.Count > 0)
                {
                    List<string> imgs = new List<string>();

                    foreach (HtmlElement image in images)
                    {
                        string imagePath = image.GetAttribute("src");
                        if (imagePath.StartsWith("file:", StringComparison.OrdinalIgnoreCase))
                        {
                            imgs.Add(imagePath);
                        }
                    }

                    for (int i = 0; i < imgs.Count; i++)
                    {
                        string cid = string.Format("em_image_{0:00}", i);
                        string imagePath = Path.GetFullPath(imgs[i].Replace("%20", " ").Replace("file:///", ""));
                        Attachment attach = new Attachment(imagePath);
                        attach.ContentId = cid;
                        attach.Name = Path.GetFileNameWithoutExtension(imagePath);
                        msg.Attachments.Add(attach);

                        html.Replace(imgs[i], string.Format("cid:{0}", cid));
                    }
                }

                msg.Body = html;

                return msg;
            }
        }

        /// <summary>
        /// 获取插入的图片名称集合
        /// </summary>
        public string[] Images
        {
            get
            {
                List<string> images = new List<string>();

                foreach (HtmlElement element in webBrowserBody.Document.Images)
                {
                    string image = element.GetAttribute("src");
                    if (!images.Contains(image))
                    {
                        images.Add(image);
                    }
                }

                return images.ToArray();
            }
        }

        #endregion

        #region 方法

        /// <summary>
        /// 控件初始化
        /// </summary>
        private void InitializeControls()
        {
            BeginUpdate();

            //工具栏
            foreach (FontFamily family in FontFamily.Families)
            {
                toolStripComboBoxName.Items.Add(family.Name);
            }

            toolStripComboBoxSize.Items.AddRange(FontSize.All.ToArray());

            //浏览器
            webBrowserBody.DocumentText = string.Empty;
            webBrowserBody.Document.Click += new HtmlElementEventHandler(webBrowserBody_DocumentClick);
            webBrowserBody.Document.Focusing += new HtmlElementEventHandler(webBrowserBody_DocumentFocusing);
            webBrowserBody.Document.ExecCommand("EditMode", false, null);
            webBrowserBody.Document.ExecCommand("LiveResize", false, null);

            EndUpdate();
        }

        /// <summary>
        /// 刷新按钮状态
        /// </summary>
        private void RefreshToolBar()
        {
            BeginUpdate();

            try
            {
                mshtml.IHTMLDocument2 document = (mshtml.IHTMLDocument2)webBrowserBody.Document.DomDocument;

                toolStripComboBoxName.Text = document.queryCommandValue("FontName").ToString();
                toolStripComboBoxSize.SelectedItem = FontSize.Find((int)document.queryCommandValue("FontSize"));
                toolStripButtonBold.Checked = document.queryCommandState("Bold");
                toolStripButtonItalic.Checked = document.queryCommandState("Italic");
                toolStripButtonUnderline.Checked = document.queryCommandState("Underline");

                toolStripButtonNumbers.Checked = document.queryCommandState("InsertOrderedList");
                toolStripButtonBullets.Checked = document.queryCommandState("InsertUnorderedList");

                toolStripButtonLeft.Checked = document.queryCommandState("JustifyLeft");
                toolStripButtonCenter.Checked = document.queryCommandState("JustifyCenter");
                toolStripButtonRight.Checked = document.queryCommandState("JustifyRight");
                toolStripButtonFull.Checked = document.queryCommandState("JustifyFull");
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }
            finally
            {
                EndUpdate();
            }
        }

        #endregion

        #region 更新相关

        private int dataUpdate;
        private bool Updating
        {
            get
            {
                return dataUpdate != 0;
            }
        }

        private void BeginUpdate()
        {
            ++dataUpdate;
        }
        private void EndUpdate()
        {
            --dataUpdate;
        }

        #endregion

        #region 工具栏

        private void toolStripComboBoxName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Updating)
            {
                return;
            }

            webBrowserBody.Document.ExecCommand("FontName", false, toolStripComboBoxName.Text);
        }
        private void toolStripComboBoxSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Updating)
            {
                return;
            }

            int size = (toolStripComboBoxSize.SelectedItem == null) ? 1 : (toolStripComboBoxSize.SelectedItem as FontSize).Value;
            webBrowserBody.Document.ExecCommand("FontSize", false, size);
        }
        private void toolStripButtonBold_Click(object sender, EventArgs e)
        {
            if (Updating)
            {
                return;
            }

            webBrowserBody.Document.ExecCommand("Bold", false, null);
            RefreshToolBar();
        }
        private void toolStripButtonItalic_Click(object sender, EventArgs e)
        {
            if (Updating)
            {
                return;
            }

            webBrowserBody.Document.ExecCommand("Italic", false, null);
            RefreshToolBar();
        }
        private void toolStripButtonUnderline_Click(object sender, EventArgs e)
        {
            if (Updating)
            {
                return;
            }

            webBrowserBody.Document.ExecCommand("Underline", false, null);
            RefreshToolBar();
        }
        private void toolStripButtonColor_Click(object sender, EventArgs e)
        {
            if (Updating)
            {
                return;
            }

            int fontcolor = (int)((mshtml.IHTMLDocument2)webBrowserBody.Document.DomDocument).queryCommandValue("ForeColor");

            ColorDialog dialog = new ColorDialog();
            dialog.Color = Color.FromArgb(0xff, fontcolor & 0xff, (fontcolor >> 8) & 0xff, (fontcolor >> 16) & 0xff);

            DialogResult result = dialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                string color = dialog.Color.Name;
                if (!dialog.Color.IsNamedColor)
                {
                    color = "#" + color.Remove(0, 2);
                }

                webBrowserBody.Document.ExecCommand("ForeColor", false, color);
            }
            RefreshToolBar();
        }

        private void toolStripButtonNumbers_Click(object sender, EventArgs e)
        {
            if (Updating)
            {
                return;
            }

            webBrowserBody.Document.ExecCommand("InsertOrderedList", false, null);
            RefreshToolBar();
        }
        private void toolStripButtonBullets_Click(object sender, EventArgs e)
        {
            if (Updating)
            {
                return;
            }

            webBrowserBody.Document.ExecCommand("InsertUnorderedList", false, null);
            RefreshToolBar();
        }
        private void toolStripButtonOutdent_Click(object sender, EventArgs e)
        {
            if (Updating)
            {
                return;
            }

            webBrowserBody.Document.ExecCommand("Outdent", false, null);
            RefreshToolBar();
        }
        private void toolStripButtonIndent_Click(object sender, EventArgs e)
        {
            if (Updating)
            {
                return;
            }

            webBrowserBody.Document.ExecCommand("Indent", false, null);
            RefreshToolBar();
        }

        private void toolStripButtonLeft_Click(object sender, EventArgs e)
        {
            if (Updating)
            {
                return;
            }

            webBrowserBody.Document.ExecCommand("JustifyLeft", false, null);
            RefreshToolBar();
        }
        private void toolStripButtonCenter_Click(object sender, EventArgs e)
        {
            if (Updating)
            {
                return;
            }

            webBrowserBody.Document.ExecCommand("JustifyCenter", false, null);
            RefreshToolBar();
        }
        private void toolStripButtonRight_Click(object sender, EventArgs e)
        {
            if (Updating)
            {
                return;
            }

            webBrowserBody.Document.ExecCommand("JustifyRight", false, null);
            RefreshToolBar();
        }
        private void toolStripButtonFull_Click(object sender, EventArgs e)
        {
            if (Updating)
            {
                return;
            }

            webBrowserBody.Document.ExecCommand("JustifyFull", false, null);
            RefreshToolBar();
        }

        private void toolStripButtonLine_Click(object sender, EventArgs e)
        {
            if (Updating)
            {
                return;
            }

            webBrowserBody.Document.ExecCommand("InsertHorizontalRule", false, null);
            RefreshToolBar();
        }
        private void toolStripButtonHyperlink_Click(object sender, EventArgs e)
        {
            if (Updating)
            {
                return;
            }

            webBrowserBody.Document.ExecCommand("CreateLink", true, null);
            RefreshToolBar();
        }
        private void toolStripButtonPicture_Click(object sender, EventArgs e)
        {
            if (Updating)
            {
                return;
            }

            webBrowserBody.Document.ExecCommand("InsertImage", true, null);
            RefreshToolBar();
        }

        private void toolStripButtonUnDo_Click(object sender, EventArgs e)
        {
            if (Updating)
            {
                return;
            }

            webBrowserBody.Document.ExecCommand("UnDo", false, null);
            RefreshToolBar();
        }

        private void toolStripButtonRedo_Click(object sender, EventArgs e)
        {
            if (Updating)
            {
                return;
            }

            webBrowserBody.Document.ExecCommand("ReDo", false, null);
            RefreshToolBar();
        }

        #endregion

        #region 浏览器

        private void webBrowserBody_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
        }

        private void webBrowserBody_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.IsInputKey)
            {
                return;
            }

            RefreshToolBar();
        }

        private void webBrowserBody_DocumentClick(object sender, HtmlElementEventArgs e)
        {
            RefreshToolBar();
        }

        private void webBrowserBody_DocumentFocusing(object sender, HtmlElementEventArgs e)
        {
            RefreshToolBar();
        }

        #endregion

        #region 字体大小转换

        private class FontSize
        {
            private static List<FontSize> allFontSize = null;
            public static List<FontSize> All
            {
                get
                {
                    if (allFontSize == null)
                    {
                        allFontSize = new List<FontSize>();
                        allFontSize.Add(new FontSize(8, 1));
                        allFontSize.Add(new FontSize(10, 2));
                        allFontSize.Add(new FontSize(12, 3));
                        allFontSize.Add(new FontSize(14, 4));
                        allFontSize.Add(new FontSize(18, 5));
                        allFontSize.Add(new FontSize(24, 6));
                        allFontSize.Add(new FontSize(36, 7));
                    }

                    return allFontSize;
                }
            }

            public static FontSize Find(int value)
            {
                if (value < 1)
                {
                    return All[0];
                }

                if (value > 7)
                {
                    return All[6];
                }

                return All[value - 1];
            }

            private FontSize(int display, int value)
            {
                displaySize = display;
                valueSize = value;
            }

            private int valueSize;
            public int Value
            {
                get
                {
                    return valueSize;
                }
            }

            private int displaySize;
            public int Display
            {
                get
                {
                    return displaySize;
                }
            }

            public override string ToString()
            {
                return displaySize.ToString();
            }
        }

        #endregion

        #region 下拉框

        private class ToolStripComboBoxEx : ToolStripComboBox
        {
            public override Size GetPreferredSize(Size constrainingSize)
            {
                Size size = base.GetPreferredSize(constrainingSize);
                size.Width = Math.Max(Width, 0x20);
                return size;
            }
        }

        #endregion

    }
}
