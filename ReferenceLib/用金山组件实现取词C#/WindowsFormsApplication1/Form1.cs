using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using XDICTGRB;
using YoudaoGetWord32Lib;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form, IXDictGrabSink
    {
        ToolTip toolTip1;
        
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.SetBounds(0, 0, 100, 100);
            //this.Visible = false;

            GrabProxy gp = new GrabProxy();
            gp.GrabInterval = 1;//指抓取时间间隔 
            gp.GrabMode = XDictGrabModeEnum.XDictGrabMouse;//设定取词的属性 
            gp.GrabEnabled = true;//是否取词的属性 
            gp.AdviseGrab(this);
            
            toolTip1 = new ToolTip();
            toolTip1.ShowAlways = true;
            toolTip1.ReshowDelay = 0;
            toolTip1.InitialDelay = 0;  
        }
    
        int IXDictGrabSink.QueryWord(string WordString, int lCursorX, int lCursorY, string SentenceString, ref int lLoc, ref int lStart) 
        {
            this.richTextBox1.Text = SentenceString;//鼠标所在语句 
            toolTip1.Show(SentenceString, this, lCursorX, lCursorY);
            return 1; 
        } 
        
        
    }
}
