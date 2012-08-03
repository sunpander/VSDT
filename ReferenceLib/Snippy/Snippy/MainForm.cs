using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using SnippyLib;
using System.Xml;
using System.IO;
using Snippy.SnippyLib;

namespace Snippy
{

    public class MainForm : Form
    {
        // Fields
        private App _app;
        private bool _dirtySnippet;
        private bool _dirtySnippetFile;
        private CheckedListBox clbSnippetTypes;
        private IContainer components;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private GroupBox groupBox5;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label7;
        private RichTextBox rtbCode;
        private StatusBarPanel spBuffer;
        private StatusBarPanel spFile;
        private StatusBar statusBar1;
        private TextBox txtAuthor;
        private TextBox txtDescription;
        private TextBox txtHackForTheming;
        private TextBox txtShortcut;
        private GroupBox groupBox4;
        private TreeView treeView1;
        private TextBox textBox1;
        private Label label6;
        private Label label5;
        private ComboBox languageComboBox;
        private Button btnNew;
        private Button btnSaveAs;
        private Button btnSave;
        private Button btnOpen;
        private TextBox textBox2;
        private TextBox txtTitle;
        private Button btnDelete;
        private TextBox textBox3;

        private static string snippedExtension = ".snippet";
        // Methods
        public MainForm()
        {
            this.InitializeComponent();
            this._app = new App();
            this.languageComboBox.SelectedIndex = 0;
            this.setFileDirty(false);
        }

 

        private void clbSnippetTypes_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            this.OnDataChanged(sender, e);
        }

        private bool confirmLoseChanges()
        {
            if (this._dirtySnippetFile)
            {
                switch (MessageBox.Show("The contents of the " + this._app.CurrentFile + " file have changed.\n\nDo you want to save the changes?", "Snippy", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation))
                {
                    case DialogResult.Cancel:
                        return false;

                    case DialogResult.Yes:
                        this.save();
                        break;
                }
            }
            return true;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }



        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtAuthor = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.txtShortcut = new System.Windows.Forms.TextBox();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rtbCode = new System.Windows.Forms.RichTextBox();
            this.txtHackForTheming = new System.Windows.Forms.TextBox();
            this.statusBar1 = new System.Windows.Forms.StatusBar();
            this.spFile = new System.Windows.Forms.StatusBarPanel();
            this.spBuffer = new System.Windows.Forms.StatusBarPanel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.clbSnippetTypes = new System.Windows.Forms.CheckedListBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnSaveAs = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnOpen = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.languageComboBox = new System.Windows.Forms.ComboBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spFile)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spBuffer)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtAuthor);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtDescription);
            this.groupBox1.Controls.Add(this.txtShortcut);
            this.groupBox1.Controls.Add(this.txtTitle);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(4, 315);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(616, 119);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // txtAuthor
            // 
            this.txtAuthor.Location = new System.Drawing.Point(96, 37);
            this.txtAuthor.Name = "txtAuthor";
            this.txtAuthor.Size = new System.Drawing.Size(304, 22);
            this.txtAuthor.TabIndex = 7;
            this.txtAuthor.TextChanged += new System.EventHandler(this.OnDataChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 40);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(35, 14);
            this.label7.TabIndex = 6;
            this.label7.Text = "作者:";
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(8, 66);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 19);
            this.label4.TabIndex = 8;
            this.label4.Text = "描述:";
            // 
            // txtDescription
            // 
            this.txtDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDescription.Location = new System.Drawing.Point(96, 63);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(508, 49);
            this.txtDescription.TabIndex = 9;
            this.txtDescription.TextChanged += new System.EventHandler(this.OnDataChanged);
            // 
            // txtShortcut
            // 
            this.txtShortcut.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtShortcut.Location = new System.Drawing.Point(472, 11);
            this.txtShortcut.Name = "txtShortcut";
            this.txtShortcut.Size = new System.Drawing.Size(132, 21);
            this.txtShortcut.TabIndex = 5;
            this.txtShortcut.TextChanged += new System.EventHandler(this.OnDataChanged);
            // 
            // txtTitle
            // 
            this.txtTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTitle.Location = new System.Drawing.Point(96, 11);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(304, 21);
            this.txtTitle.TabIndex = 3;
            this.txtTitle.TextChanged += new System.EventHandler(this.OnDataChanged);
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(412, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 19);
            this.label3.TabIndex = 4;
            this.label3.Text = "快捷键:";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(8, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 19);
            this.label1.TabIndex = 2;
            this.label1.Text = "标题:";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.rtbCode);
            this.groupBox2.Controls.Add(this.txtHackForTheming);
            this.groupBox2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(4, 440);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(820, 268);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "代码";
            // 
            // rtbCode
            // 
            this.rtbCode.AcceptsTab = true;
            this.rtbCode.AllowDrop = true;
            this.rtbCode.AutoWordSelection = true;
            this.rtbCode.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbCode.DetectUrls = false;
            this.rtbCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbCode.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbCode.Location = new System.Drawing.Point(3, 18);
            this.rtbCode.Name = "rtbCode";
            this.rtbCode.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedBoth;
            this.rtbCode.ShowSelectionMargin = true;
            this.rtbCode.Size = new System.Drawing.Size(814, 247);
            this.rtbCode.TabIndex = 13;
            this.rtbCode.Text = "";
            this.rtbCode.WordWrap = false;
            this.rtbCode.TextChanged += new System.EventHandler(this.OnDataChanged);
            // 
            // txtHackForTheming
            // 
            this.txtHackForTheming.Enabled = false;
            this.txtHackForTheming.Location = new System.Drawing.Point(8, 42);
            this.txtHackForTheming.Multiline = true;
            this.txtHackForTheming.Name = "txtHackForTheming";
            this.txtHackForTheming.Size = new System.Drawing.Size(800, 222);
            this.txtHackForTheming.TabIndex = 1000;
            this.txtHackForTheming.TabStop = false;
            // 
            // statusBar1
            // 
            this.statusBar1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusBar1.Location = new System.Drawing.Point(0, 714);
            this.statusBar1.Name = "statusBar1";
            this.statusBar1.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
            this.spFile,
            this.spBuffer});
            this.statusBar1.ShowPanels = true;
            this.statusBar1.Size = new System.Drawing.Size(830, 21);
            this.statusBar1.SizingGrip = false;
            this.statusBar1.TabIndex = 2;
            this.statusBar1.Text = "stbStatus";
            // 
            // spFile
            // 
            this.spFile.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.spFile.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
            this.spFile.Name = "spFile";
            this.spFile.Text = "    Untitled.snippet    ";
            this.spFile.ToolTipText = "The current snippet file you\'re editing/viewing";
            this.spFile.Width = 115;
            // 
            // spBuffer
            // 
            this.spBuffer.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring;
            this.spBuffer.Name = "spBuffer";
            this.spBuffer.Width = 715;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.textBox2);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(4, 268);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(820, 41);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(70, 12);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(596, 22);
            this.textBox2.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(8, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 15);
            this.label2.TabIndex = 0;
            this.label2.Text = "Sni&ppet:";
            // 
            // clbSnippetTypes
            // 
            this.clbSnippetTypes.CheckOnClick = true;
            this.clbSnippetTypes.Items.AddRange(new object[] {
            "Expansion",
            "SurroundsWith",
            "Refactoring"});
            this.clbSnippetTypes.Location = new System.Drawing.Point(12, 19);
            this.clbSnippetTypes.Name = "clbSnippetTypes";
            this.clbSnippetTypes.Size = new System.Drawing.Size(176, 38);
            this.clbSnippetTypes.TabIndex = 8;
            this.clbSnippetTypes.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.clbSnippetTypes_ItemCheck);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.clbSnippetTypes);
            this.groupBox5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox5.Location = new System.Drawing.Point(624, 315);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(200, 119);
            this.groupBox5.TabIndex = 8;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Snippet Types";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.textBox3);
            this.groupBox4.Controls.Add(this.btnDelete);
            this.groupBox4.Controls.Add(this.btnSaveAs);
            this.groupBox4.Controls.Add(this.btnSave);
            this.groupBox4.Controls.Add(this.btnOpen);
            this.groupBox4.Controls.Add(this.btnNew);
            this.groupBox4.Controls.Add(this.treeView1);
            this.groupBox4.Controls.Add(this.textBox1);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.languageComboBox);
            this.groupBox4.Location = new System.Drawing.Point(4, 1);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(817, 250);
            this.groupBox4.TabIndex = 9;
            this.groupBox4.TabStop = false;
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(322, 178);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 1012;
            this.btnDelete.Text = "删除";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnSaveAs
            // 
            this.btnSaveAs.Location = new System.Drawing.Point(322, 148);
            this.btnSaveAs.Name = "btnSaveAs";
            this.btnSaveAs.Size = new System.Drawing.Size(75, 24);
            this.btnSaveAs.TabIndex = 1011;
            this.btnSaveAs.Text = "另存为";
            this.btnSaveAs.UseVisualStyleBackColor = true;
            this.btnSaveAs.Click += new System.EventHandler(this.mnuSaveAs_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(322, 119);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 1010;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.mnuSave_Click);
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(322, 90);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(75, 23);
            this.btnOpen.TabIndex = 1009;
            this.btnOpen.Text = "打开";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.mnuLoad_Click);
            // 
            // btnNew
            // 
            this.btnNew.Location = new System.Drawing.Point(322, 55);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(75, 23);
            this.btnNew.TabIndex = 1008;
            this.btnNew.Text = "新建";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.mnuNew_Click);
            // 
            // treeView1
            // 
            this.treeView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.treeView1.Location = new System.Drawing.Point(8, 49);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(293, 185);
            this.treeView1.TabIndex = 1007;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(70, 17);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(402, 21);
            this.textBox1.TabIndex = 1006;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(11, 17);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 15);
            this.label6.TabIndex = 1005;
            this.label6.Text = "location:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(501, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 15);
            this.label5.TabIndex = 1004;
            this.label5.Text = "&Language:";
            // 
            // languageComboBox
            // 
            this.languageComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.languageComboBox.FormattingEnabled = true;
            this.languageComboBox.Items.AddRange(new object[] {
            "csharp"});
            this.languageComboBox.Location = new System.Drawing.Point(582, 20);
            this.languageComboBox.Name = "languageComboBox";
            this.languageComboBox.Size = new System.Drawing.Size(137, 23);
            this.languageComboBox.Sorted = true;
            this.languageComboBox.TabIndex = 1003;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(436, 49);
            this.textBox3.Multiline = true;
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new System.Drawing.Size(355, 185);
            this.textBox3.TabIndex = 1013;
            // 
            // MainForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(830, 735);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.statusBar1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "C#";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.MainForm_Closing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spFile)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spBuffer)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        private void languageComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.OnDataChanged(sender, e);
        }



        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.Run(new MainForm());
        }

        private void MainForm_Closing(object sender, CancelEventArgs e)
        {
            if (!this.confirmLoseChanges())
            {
                e.Cancel = true;
            }
        }
 

        private void mnuExit_Click(object sender, EventArgs e)
        {
            if (this.confirmLoseChanges())
            {
                Application.Exit();
            }
        }

        private void mnuLoad_Click(object sender, EventArgs e)
        {
           
        }

        private void OpenSnippetFile(string path)
        {
            try
            {
                this._app.LoadFile(path);
            }
            catch (XmlException)
            {
                MessageBox.Show("Failed to load " + path + " as a valid XML file.", "Error - Snippy", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                //return;
            }
            this._app.SetCurrentSnippet(0);
            this.refreshForm(true);
            this._dirtySnippet = false;
            this.setFileDirty(false);
        }

        private void mnuNew_Click(object sender, EventArgs e)
        {
            if (this.confirmLoseChanges())
            {
                this.newFile();
            }
        }

        private void mnuNewSnippet_Click(object sender, EventArgs e)
        {
            this._app.AppendNewSnippet();
            this.refreshForm(true);
            this.txtTitle.Focus();
            this._dirtySnippet = false;
            this._dirtySnippetFile = true;
        }

        private void mnuSave_Click(object sender, EventArgs e)
        {
            this.save();
        }

        private void mnuSaveAs_Click(object sender, EventArgs e)
        {
            this.SaveAs();
        }

        private void newFile()
        {
            this._app.CreateNewFile();
            this.refreshForm(true);
            this.txtTitle.Focus();
            this._dirtySnippet = false;
            this._dirtySnippetFile = false;
        }

        private void OnDataChanged(object sender, EventArgs e)
        {
            this._dirtySnippet = true;
            this.setFileDirty(true);
        }
        //66792201
        private void refreshActiveSnippetList()
        {
            this.textBox2.Text = new FileInfo(this._app.CurrentFile).Name.Replace(snippedExtension,"");
        }

        private void refreshCodeCombos()
        {
            this.languageComboBox.Text = this._app.CurrentSnippet.CodeLanguageAttribute;
        }

        private void refreshForm(bool shouldRefreshActiveSnippetList)
        {
            this.txtTitle.Text = this._app.CurrentSnippet.Title;
            this.txtShortcut.Text = this._app.CurrentSnippet.Shortcut;
            this.txtDescription.Text = this._app.CurrentSnippet.Description;
            this.txtAuthor.Text = this._app.CurrentSnippet.Author;
            this.rtbCode.Text = this._app.CurrentSnippet.Code;

            this.refreshSnippetTypes();
            this.refreshStatusBar();
            this.refreshCodeCombos();
            if (shouldRefreshActiveSnippetList)
            {
                this.refreshActiveSnippetList();
            }
        }



        private void refreshSnippetTypes()
        {
            for (int i = 0; i < this.clbSnippetTypes.Items.Count; i++)
            {
                this.clbSnippetTypes.SetItemChecked(i, false);
            }
            foreach (SnippetType type in this._app.CurrentSnippet.SnippetTypes)
            {
                string str = type.Value;
                if (str != null)
                {
                    if (str != "Expansion")
                    {
                        if (str == "SurroundsWith")
                        {
                            goto Label_008D;
                        }
                        if (str == "Refactoring")
                        {
                            goto Label_00AF;
                        }
                    }
                    else
                    {
                        this.clbSnippetTypes.SetItemChecked(0, true);
                    }
                }
                continue;
            Label_008D:
                if (this.clbSnippetTypes.Items.Count > 1)
                {
                    this.clbSnippetTypes.SetItemChecked(1, true);
                }
                continue;
            Label_00AF:
                if (this.clbSnippetTypes.Items.Count > 2)
                {
                    this.clbSnippetTypes.SetItemChecked(2, true);
                }
            }
        }

        private void refreshStatusBar()
        {
            this.spFile.Text = " " + new FileInfo(this._app.CurrentFile).Name;
            if (this._dirtySnippetFile)
            {
                this.spFile.Text = this.spFile.Text + "* ";
            }
            else
            {
                this.spFile.Text = this.spFile.Text + " ";
            }
        }
        private void save()
        {
            if (this._app.CurrentFile == "Untitled.snippet")
            {
                this.SaveAs();
            }
            else
            {
                string path = System.IO.Path.Combine(this.textBox1.Text, this.textBox2.Text + snippedExtension);
                SaveAs(path);
            }
        }
        private void SaveAs(string path)
        {
            this.updateInMemorySnippet();
            this._app.SaveAs(path);
            this.refreshActiveSnippetList();
            this._dirtySnippet = false;
            this.setFileDirty(false);
            try
            {
                BindTreeView();
                int k = treeView1.Nodes.IndexOfKey(this.textBox2.Text);
                treeView1.SelectedNode = treeView1.Nodes[k];
                treeView1.Focus();
            }
            catch { }
        }
        private void SaveAs()
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.DefaultExt = "snippet";
            dialog.Filter = "Snippet files (*.snippet)|*.snippet";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                SaveAs(dialog.FileName);
            }
        }

        private void setFileDirty(bool flag)
        {
            this._dirtySnippetFile = flag;
            this.refreshStatusBar();
        }



        private void updateInMemorySnippet()
        {
            if (this._dirtySnippet)
            {
                this._app.CurrentSnippet.Title = this.txtTitle.Text;
                this._app.CurrentSnippet.Shortcut = this.txtShortcut.Text;
                this._app.CurrentSnippet.Description = this.txtDescription.Text;
                this._app.CurrentSnippet.Author = this.txtAuthor.Text;
                this.updateSnippetTypes();

                this._app.CurrentSnippet.Code = this.rtbCode.Text;
                this._app.CurrentSnippet.CodeLanguageAttribute = this.languageComboBox.Text;

                this._app.CurrentSnippet.CodeKindAttribute = null;

                this._dirtySnippet = false;
            }
        }



        private void updateSnippetTypes()
        {
            this._app.CurrentSnippet.ClearSnippetTypes();
            for (int i = 0; i < this.clbSnippetTypes.Items.Count; i++)
            {
                if (this.clbSnippetTypes.GetItemChecked(i))
                {
                    switch (i)
                    {
                        case 0:
                            this._app.CurrentSnippet.AddSnippetType("Expansion");
                            break;

                        case 1:
                            this._app.CurrentSnippet.AddSnippetType("SurroundsWith");
                            break;

                        case 2:
                            this._app.CurrentSnippet.AddSnippetType("Refactoring");
                            break;
                    }
                }
            }
        }
        string VSpath = "";
        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                this.textBox1.Text = PathUtil.GetCodeNipPath();
                this.languageComboBox.SelectedIndex = 0;
                BindTreeView();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            this.textBox3.Text = "好玩的代码段:\n\n 在C#的cs文件中输入for按两下tab键,自动生成一段代码.\n\n..称为代码段,同样你可以键自己的代码段,当你输入[快捷键]的内容时,[标题和描述]会显示在提示框里,双击tab时,会自动插入[代码]的内容....类似$name$可预定义一些会被替换掉的内容,这里不包含此功能";
        }

        private void BindTreeView()
        {
            //列出所有文件
            DirectoryInfo dir = new DirectoryInfo(this.textBox1.Text);
            if (dir.Exists)
            {
                treeView1.Nodes.Clear();
                FileInfo[] files = dir.GetFiles("*.snippet");
                foreach (var item in files)
                {
                    TreeNode node = new TreeNode(item.Name);
                    node.Text = item.Name.Replace(item.Extension, "");
                    node.Tag = item.FullName;
                    node.Name = node.Text;
                    treeView1.Nodes.Add(node);
                }
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Tag != null)
            {
                string path = e.Node.Tag.ToString();
                OpenSnippetFile(path);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

            if (treeView1.SelectedNode.Tag!= null)
            {
                string path = treeView1.SelectedNode.Tag.ToString();
                System.IO.File.Delete(path);
                BindTreeView();
            }
        }
    }
}

 

 
