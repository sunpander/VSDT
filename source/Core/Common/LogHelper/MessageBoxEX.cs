using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace iPlat4C.EnvDTE.PlugInCore.Common.LogHelper
{
    public partial class MessageBoxEX : Form
    {
        public MessageBoxEX()
        {
            InitializeComponent();
        }
    
        private const string Abort = "Abort";
        private const int BUTTON_LOCATION_Y = 8;
        private const string Cancel = "Cancel";
        //private IContainer components = null;
        private DialogResult defaultDialogResult;
        private const string Ignore = "Ignore";
        private bool isShowTimer = false;
        private Label lbMessage;
        private MessageBoxButtons messageBoxButtons;
        private MessageBoxIcon messageBoxIcon;
        private const string No = "No";
        private const string None = "None";
        private const string OK = "OK";
        private const int ONE_BUTTON_WIDTH = 0x73;
        private Panel panel1;
        private Panel panel2;
        private PictureBox pictureBox1;
        private const string Retry = "Retry";
        private int seconds = 0;
        private const int THREE_BUTTON_WIDTH = 0x131;
        private Timer timer1;
        private const int TWO_BUTTON_WIDTH = 210;
        private const string Yes = "Yes";

        internal MessageBoxEX(string text, string caption, MessageBoxButtons messageBoxButtons, MessageBoxIcon messageBoxIcon, int seconds, DialogResult defaultDialogResult)
        {
          
            this.Message = text;
            this.Caption = caption;
            this.messageBoxButtons = messageBoxButtons;
            this.messageBoxIcon = messageBoxIcon;
            this.defaultDialogResult = defaultDialogResult;
            if (seconds > 0)
            {
                this.isShowTimer = true;
                this.seconds = seconds;
            }
            this.Initilize();
        }

        private void btn_Click(object sender, EventArgs e)
        {
            switch (((Button)sender).Name)
            {
                case "OK":
                    base.DialogResult = DialogResult.OK;
                    break;

                case "Cancel":
                    base.DialogResult = DialogResult.Cancel;
                    break;

                case "Abort":
                    base.DialogResult = DialogResult.Abort;
                    break;

                case "Ignore":
                    base.DialogResult = DialogResult.Ignore;
                    break;

                case "No":
                    base.DialogResult = DialogResult.No;
                    break;

                case "None":
                    base.DialogResult = DialogResult.None;
                    break;

                case "Retry":
                    base.DialogResult = DialogResult.Retry;
                    break;

                case "Yes":
                    base.DialogResult = DialogResult.Yes;
                    break;

                default:
                    base.DialogResult = DialogResult.None;
                    break;
            }
            base.Close();
        }

        private Button CreateNewButton()
        {
            Button button = new Button();
            this.panel1.Controls.Add(button);
            return button;
        }

      
        private void FrmMessageBox_Load(object sender, EventArgs e)
        {
            Control[] controlArray = this.panel1.Controls.Find(this.defaultDialogResult.ToString(), false);
            if (controlArray.Length == 1)
            {
                Button button = controlArray[0] as Button;
                button.TabIndex = 0;
                button.Select();
            }
        }



        private void Initilize()
        {
            Button button;
            int num5;
            switch (this.messageBoxIcon)
            {
                case MessageBoxIcon.Hand:
                   // this.pictureBox1.Image = Resources.error_Hand_Stop2;
                    break;

                case MessageBoxIcon.Question:
                   // this.pictureBox1.Image = Resources.Question;
                    break;

                case MessageBoxIcon.Exclamation:
                   // this.pictureBox1.Image = Resources.Exclamation_Warning;
                    break;

                case MessageBoxIcon.Asterisk:
                   // this.pictureBox1.Image = Resources.Asterisk_Information;
                    break;

                default:
                    this.pictureBox1.Image = null;
                    break;
            }
            switch (this.messageBoxButtons)
            {
                case MessageBoxButtons.OK:
                    button = this.CreateNewButton();
                    button.Name = button.Text = "OK";
                    button.Click += new EventHandler(this.btn_Click);
                    break;

                case MessageBoxButtons.OKCancel:
                    button = this.CreateNewButton();
                    button.Name = button.Text = "OK";
                    button.Click += new EventHandler(this.btn_Click);
                    button = this.CreateNewButton();
                    button.Name = button.Text = "Cancel";
                    button.Click += new EventHandler(this.btn_Click);
                    break;

                case MessageBoxButtons.AbortRetryIgnore:
                    button = this.CreateNewButton();
                    button.Name = button.Text = "Abort";
                    button.Click += new EventHandler(this.btn_Click);
                    button = this.CreateNewButton();
                    button.Name = button.Text = "Retry";
                    button.Click += new EventHandler(this.btn_Click);
                    button = this.CreateNewButton();
                    button.Name = button.Text = "Ignore";
                    button.Click += new EventHandler(this.btn_Click);
                    break;

                case MessageBoxButtons.YesNoCancel:
                    button = this.CreateNewButton();
                    button.Name = button.Text = "Yes";
                    button.Click += new EventHandler(this.btn_Click);
                    button = this.CreateNewButton();
                    button.Name = button.Text = "No";
                    button.Click += new EventHandler(this.btn_Click);
                    button = this.CreateNewButton();
                    button.Name = button.Text = "Cancel";
                    button.Click += new EventHandler(this.btn_Click);
                    break;

                case MessageBoxButtons.YesNo:
                    button = this.CreateNewButton();
                    button.Name = button.Text = "Yes";
                    button.Click += new EventHandler(this.btn_Click);
                    button = this.CreateNewButton();
                    button.Name = button.Text = "No";
                    button.Click += new EventHandler(this.btn_Click);
                    break;

                case MessageBoxButtons.RetryCancel:
                    button = this.CreateNewButton();
                    button.Name = button.Text = "Retry";
                    button.Click += new EventHandler(this.btn_Click);
                    button = this.CreateNewButton();
                    button.Name = button.Text = "Cancel";
                    button.Click += new EventHandler(this.btn_Click);
                    break;

                default:
                    button = this.CreateNewButton();
                    button.Text = button.Name = "OK";
                    button.Click += new EventHandler(this.btn_Click);
                    break;
            }
            int count = this.panel1.Controls.Count;
            int num2 = 0;
            if (count == 1)
            {
                num2 = 0x73;
            }
            else if (count == 2)
            {
                num2 = 210;
            }
            else
            {
                num2 = 0x131;
            }
            int height = this.lbMessage.Height + 0x6b;
            int width = this.lbMessage.Width + 80;
            if (width < num2)
            {
                width = num2;
            }
            base.Size = new Size(width, height);
            switch (count)
            {
                case 1:
                    button = (Button)this.panel1.Controls[0];
                    num5 = (width - 0x4b) / 2;
                    button.Location = new Point(num5, 8);
                    break;

                case 2:
                    num5 = (width - 150) / 3;
                    button = (Button)this.panel1.Controls[0];
                    button.Location = new Point(num5, 8);
                    button = (Button)this.panel1.Controls[1];
                    button.Location = new Point((num5 * 2) + 0x4b, 8);
                    break;

                case 3:
                    num5 = (width - 0xe1) / 4;
                    button = (Button)this.panel1.Controls[0];
                    button.Location = new Point(num5, 8);
                    button = (Button)this.panel1.Controls[1];
                    button.Location = new Point((num5 * 2) + 0x4b, 8);
                    button = (Button)this.panel1.Controls[2];
                    button.Location = new Point((num5 * 3) + 150, 8);
                    break;
            }
            if (this.isShowTimer)
            {
                this.timer1.Start();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (this.isShowTimer)
            {
                this.seconds--;
                if (this.seconds <= 0)
                {
                    this.timer1.Stop();
                    base.DialogResult = this.defaultDialogResult;
                    base.Close();
                }
                else
                {
                    try
                    {
                        Control[] controlArray = this.panel1.Controls.Find(this.defaultDialogResult.ToString(), false);
                        if (controlArray.Length == 1)
                        {
                            Button button = controlArray[0] as Button;
                            button.Text = button.Name + string.Format("({0})", this.seconds.ToString());
                        }
                    }
                    catch
                    {
                    }
                }
            }
        }

        internal string Caption
        {
            get
            {
                return this.Text;
            }
            set
            {
                this.Text = value;
            }
        }

        internal string Message
        {
            get
            {
                return this.lbMessage.Text;
            }
            set
            {
                this.lbMessage.Text = value;
            }
        }
    }
}
