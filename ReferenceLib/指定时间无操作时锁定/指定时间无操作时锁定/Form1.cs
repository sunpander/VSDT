using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace 指定时间无操作时锁定
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int.TryParse(textBox1.Text, out lockTime);
            locked = false;
        }
        [DllImport("User32.dll")]
        private static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);
        [DllImport("user32")]
        private static extern long SetWindowPos(IntPtr hwnd, int hWndInsertAfter, int X, int y, int cx, int cy, int wFlagslong);

        private struct LASTINPUTINFO
        {
            public uint cbSize;

            public uint dwTime;
        }
        LASTINPUTINFO lastInPut = new LASTINPUTINFO();
        private bool locked = false;
        //锁定时间，
        internal int lockTime = 30;
        private void timer1_Tick(object sender, EventArgs e)
        {
            labelTimeNow.Text = DateTime.Now.ToLongTimeString();
            //配置时间内无操作锁定
            lastInPut.cbSize = (uint)System.Runtime.InteropServices.Marshal.SizeOf(lastInPut);
            GetLastInputInfo(ref lastInPut);

            long noOperationTime = System.Environment.TickCount - lastInPut.dwTime;

            if (!locked && lockTime != 0 && noOperationTime > lockTime * 1000  )
            {
                locked = true;
                MessageBox.Show("已锁定(可已showdialog方式弹出登陆框登陆)");
                locked = false;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            labelTimeNow.Text = DateTime.Now.ToLongTimeString();
            timer1.Start();
        }
    }
}
