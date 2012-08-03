using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VSDT.Common.Utility
{
    public class ControlOperate
    {
        public static void SetGroupBoxEnable(GroupBox groupBox1, bool enable)
        {
            for (int i = 0; i < groupBox1.Controls.Count; i++)
            {
                if (groupBox1.Controls[i] is GroupBox)
                {
                    SetGroupBoxEnable(groupBox1.Controls[i] as GroupBox, enable);
                }
                else
                {
                    groupBox1.Controls[i].Enabled = enable;
                }
            }
        }
    }
}
