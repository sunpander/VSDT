using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

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

            for (int n = 0; n < 3; n++) //旋转圈数
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
 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            JitterForm(this);
        }
    }
}
