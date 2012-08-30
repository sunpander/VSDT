using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Security;
using System.Drawing;

namespace WindowsFormsApplication1
{

    public partial class ButtonComboBox : System.Windows.Forms.ComboBox
    {
        public static class NativeMethods
        {


            [StructLayout(LayoutKind.Sequential)]
            public struct RECT
            {
                public int left;
                public int top;
                public int right;
                public int bottom;
                public RECT(int left, int top, int right, int bottom)
                {
                    this.left = left;
                    this.top = top;
                    this.right = right;
                    this.bottom = bottom;
                }

                public RECT(Rectangle r)
                {
                    this.left = r.Left;
                    this.top = r.Top;
                    this.right = r.Right;
                    this.bottom = r.Bottom;
                }

                public static NativeMethods.RECT FromXYWH(int x, int y, int width, int height)
                {
                    return new NativeMethods.RECT(x, y, x + width, y + height);
                }

                public Size Size
                {
                    get
                    {
                        return new Size(this.right - this.left, this.bottom - this.top);
                    }
                }
            }
        }
        [DllImport("user32.dll", EntryPoint = "SetWindowLong", CharSet = CharSet.Auto)]
        public static extern IntPtr SetWindowLongPtr32(HandleRef hWnd, int nIndex, FPtr dwNewLong);
        [DllImport("user32.dll", EntryPoint = "SetWindowLongPtr", CharSet = CharSet.Auto)]
        public static extern IntPtr SetWindowLongPtr64(HandleRef hWnd, int nIndex, FPtr dwNewLong);

        [DllImport("user32.dll", EntryPoint = "GetWindowLong", CharSet = CharSet.Auto)]
        public static extern IntPtr GetWindowLong32(HandleRef hWnd, int nIndex);
        [DllImport("user32.dll", EntryPoint = "GetWindowLongPtr", CharSet = CharSet.Auto)]
        public static extern IntPtr GetWindowLongPtr64(HandleRef hWnd, int nIndex);

        [SuppressUnmanagedCodeSecurity, SecurityCritical, DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr CallWindowProc(IntPtr wndProc, IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int GetSystemMetrics(int nIndex);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool GetClientRect(HandleRef hWnd, [In, Out] ref NativeMethods.RECT rect);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, int lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr SetCapture(HandleRef hwnd);

        public delegate IntPtr FPtr(IntPtr hWnd, int message, IntPtr wParam, IntPtr lParam);

        public static IntPtr MAKELPARAM(int low, int high)
        {
            return (IntPtr)((high << 0x10) | (low & 0xffff));
        }

        public static int HIWORD(int n)
        {
            return ((n >> 0x10) & 0xffff);
        }

        public static int LOWORD(int n)
        {
            return (n & 0xffff);
        }

        public static IntPtr GetWindowLong(HandleRef hWnd, int nIndex)
        {
            if (IntPtr.Size == 4)
            {
                return GetWindowLong32(hWnd, nIndex);
            }
            return GetWindowLongPtr64(hWnd, nIndex);
        }

        public static IntPtr SetWindowLong(HandleRef hWnd, int nIndex, FPtr dwNewLong)
        {
            if (IntPtr.Size == 4)
            {
                return SetWindowLongPtr32(hWnd, nIndex, dwNewLong);
            }
            return SetWindowLongPtr64(hWnd, nIndex, dwNewLong);
        }


        public ButtonComboBox()
        {
            InitializeComponent();
            m_hListBox = IntPtr.Zero;
        }

        public ButtonComboBox(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
            m_hListBox = IntPtr.Zero;
        }

        public delegate void ImageClickedDelegate(int index);

        public event ImageClickedDelegate OnImageClicked;
  
        IntPtr m_hListBox;
        IntPtr m_pWndProc;
        HandleRef hr;
        FPtr cb;
        ListBox m_listBox;
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case 0x0134: //WM_CTLCOLORLISTBOX
                    if (m_hListBox == IntPtr.Zero)
                    {
                        if (m.LParam != IntPtr.Zero)
                        {
                            m_hListBox = m.LParam;
                            cb = new FPtr(this.ComboBoxListBoxProc);
                            hr = new HandleRef(this, m_hListBox);
                            m_pWndProc = GetWindowLong(hr, -4);
                            SetWindowLong(hr, -4, cb);
                        }
                    }
                    break;
            }
            base.WndProc(ref m);
        }
        short m_nHighLightItem = -1;
        IntPtr ComboBoxListBoxProc(IntPtr hWnd, int message, IntPtr wParam, IntPtr lParam)
        {
            switch (message)
            {
                case 0x0201: //WM_LBUTTONDOWN
                case 0x0203: //WM_LBUTTONDBLCLK
                    int pos = lParam.ToInt32();
                    short y = (short)(pos >> 16);
                    short x = (short)(pos & 0xFFFF);
                    int z = GetSystemMetrics(2); //SM_CXVSCROLL
                    int iconWidth = this.ItemHeight;
                    Console.WriteLine("WM_LBUTTONDOWN x={0}, y={1}, pos={2:X}", x, y, pos);
                        m_nHighLightItem = (short)this.IndexFromPoint(x, y);
                        if (m_nHighLightItem >= 0)
                        {
                            if (x > this.DropDownWidth - (z + iconWidth) && x < this.DropDownWidth - z)
                            {
                                if (OnImageClicked != null)
                                    OnImageClicked(m_nHighLightItem);
                                this.Items.RemoveAt(m_nHighLightItem);
                                //IntPtr kk = SetCapture(hr);
                                if (this.Items.Count > 0)
                                    return IntPtr.Zero;
                            }
                        }
                    break;
                case 0x0200: //WM_MOUSEMOVE
                    int pos1 = lParam.ToInt32();
                    short y1 = (short)(pos1 >> 16);
                    short x1 = (short)(pos1 & 0xFFFF);
                    m_nHighLightItem = (short)this.IndexFromPoint(x1, y1);
                    //Console.WriteLine("WM_MOUSEMOVE x1={0}, y1={1}, pos={2:X}, m_nHighLightItem={3}", x1, y1, pos1, m_nHighLightItem);
                    break;
            }
            return CallWindowProc(m_pWndProc, hWnd, message, wParam, lParam);
        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            if (m_nHighLightItem >= 0 && e.Index == m_nHighLightItem)
            {
                e.Graphics.FillRectangle(new SolidBrush(Color.Blue), e.Bounds);
            }
            else
            {
                e.Graphics.FillRectangle(new SolidBrush(this.BackColor), e.Bounds);
            }
            e.Graphics.DrawString((string)this.Items[e.Index], this.Font, new SolidBrush(Color.Black), e.Bounds);
            int z = GetSystemMetrics(2); //SM_CXVSCROLL
            Rectangle rectIcon = e.Bounds;
            rectIcon.Width = rectIcon.Height = this.ItemHeight;
            rectIcon.X = this.DropDownWidth - rectIcon.Width - z;
            if (e.Index != m_nHighLightItem)
            {
                rectIcon.Inflate(-2, -2);
            }
            e.Graphics.DrawIcon(this.ButtonIcon, rectIcon);
            base.OnDrawItem(e);
        }

        public int GetClientHeight()
        {
            NativeMethods.RECT rect = new NativeMethods.RECT();
            GetClientRect(hr, ref rect);
            return rect.bottom;
        }

        public int IndexFromPoint(int x, int y)
        {
            NativeMethods.RECT rect = new NativeMethods.RECT();
            GetClientRect(hr, ref rect);
            if (((rect.left <= x) && (x < rect.right)) && ((rect.top <= y) && (y < rect.bottom)))
            {
                int n = (int)SendMessage(hr, 0x1a9, 0, (int)MAKELPARAM(x, y));
                if (HIWORD(n) == 0)
                {
                    return LOWORD(n);
                }
            }
            return -1;
        }

        public Icon ButtonIcon { get; set; }

    }
}
