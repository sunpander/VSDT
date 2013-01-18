using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Runtime.InteropServices;
namespace RichTextBox文本变色
{
    public partial class MyRichTextBox : RichTextBox
    {
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);
        public const int WM_HSCROLL = 276;

        public const int WM_VSCROLL = 277;
        public const int WM_SETCURSOR = 32;
        public const int WM_MOUSEWHEEL = 522;
        public const int WM_MOUSEMOVE = 512;
        public const int WM_MOUSELEAVE = 675;
        public const int WM_MOUSELAST = 521;
        public const int WM_MOUSEHOVER = 673;
        public const int WM_MOUSEFIRST = 512;
        public const int WM_MOUSEACTIVATE = 33;
        private List<Control> otherRichTextBox;
        public List<Control> OtherRichTextBox { get { return otherRichTextBox; } set { otherRichTextBox = value; } }

        public static bool HasStart = false;

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (HasStart)
                return;
            if (otherRichTextBox == null)
            {
                return;
            }
            if ((m.Msg == WM_HSCROLL || m.Msg == WM_VSCROLL
            || m.Msg == WM_SETCURSOR || m.Msg == WM_MOUSEWHEEL
            || m.Msg == WM_MOUSEMOVE || m.Msg == WM_MOUSELEAVE
            || m.Msg == WM_MOUSELAST || m.Msg == WM_MOUSEHOVER
            || m.Msg == WM_MOUSEFIRST || m.Msg == WM_MOUSEACTIVATE))
            {
                HasStart = true;
                for (int i = 0; i < otherRichTextBox.Count; i++)
                {
                    if (otherRichTextBox[i] is RichTextBox)
                    {
                        if (otherRichTextBox[i] == this)
                            continue;
                        SendMessage(otherRichTextBox[i].Handle, m.Msg, m.WParam, m.LParam);
                    }

                }
                HasStart = false; ;
            }
        }
    }
}
