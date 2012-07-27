using System;
using System.Text;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Runtime.InteropServices;
namespace YXControl
{
    public struct RECT
    {
        public int x1;public int y1;public int x2;public int y2;
    }
    public class WordControl : System.Windows.Forms.UserControl
    {

        #region "API usage declarations"
        [DllImport("user32.dll")]
        public static extern int FindWindow(string strclassName, string strWindowName);

        [DllImport("user32.dll")]
        static extern int SetParent(int hWndChild, int hWndNewParent);

        [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
        static extern bool SetWindowPos(
            int hWnd,               
            int hWndInsertAfter,    
            int X,                  
            int Y,                  
            int cx,                 
            int cy,               
            uint uFlags            
            );

        [DllImport("user32.dll", EntryPoint = "MoveWindow")]
        static extern bool MoveWindow(
            int hWnd,
            int X,
            int Y,
            int nWidth,
            int nHeight,
            bool bRepaint
            );

        [DllImport("user32.dll", EntryPoint = "DrawMenuBar")]
        static extern Int32 DrawMenuBar(
            Int32 hWnd
            );

        [DllImport("user32.dll", EntryPoint = "GetMenuItemCount")]
        static extern Int32 GetMenuItemCount(
            Int32 hMenu
            );

        [DllImport("user32.dll", EntryPoint = "GetSystemMenu")]
        static extern Int32 GetSystemMenu(
            Int32 hWnd,
            bool bRevert
            );

        [DllImport("user32.dll", EntryPoint = "RemoveMenu")]
        static extern Int32 RemoveMenu(
            Int32 hMenu,
            Int32 nPosition,
            Int32 wFlags
            );

        [DllImport("user32.dll", EntryPoint = "GetClientRect", SetLastError = true)]
        public static extern bool GetClientRect(int hWnd, ref RECT lpRect);

        public delegate bool EnumChildProc(int hwnd, IntPtr lParam);
        [DllImport("user32.dll", EntryPoint = "EnumChildWindows")]
        public static extern bool EnumChildWindows(int hwndParent, EnumChildProc EnumFunc, IntPtr lParam);

        [DllImport("User32.Dll ", EntryPoint = "GetClassName")]
        public static extern void GetClassName(int hwnd, StringBuilder s, int nMaxCount);        



        private const int MF_BYPOSITION = 0x400;
        private const int MF_REMOVE = 0x1000;


        const int SWP_DRAWFRAME = 0x20;
        const int SWP_NOMOVE = 0x2;
        const int SWP_NOSIZE = 0x1;
        const int SWP_NOZORDER = 0x4;

        #endregion

     
        public Word._Document document;
        public Word.ApplicationClass applicationClass = null;
        public Word.Application application=null;
        public int wordWnd = 0;
        public string filename = null;
        private  bool deactivateevents = false;
        private int ContentHeight = 0;

        public int GetContentHeight() 
        {
            return this.ContentHeight;
        }


       
        private System.ComponentModel.Container components = null;

        public WordControl()
        {
            InitializeComponent();
        }
     
        protected override void Dispose(bool disposing)
        {
            CloseControl();
            if (disposing)
            {
                if (components != null)
                    components.Dispose();
            }
            base.Dispose(disposing);

        }

        #region Component Designer generated code
     
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // WordControl
            // 
            this.Name = "WordControl";
            this.Size = new System.Drawing.Size(439, 336);
            this.Resize += new System.EventHandler(this.OnResize);
            this.SizeChanged += new EventHandler(this.OnResize);
            this.ResumeLayout(false);

        }
        #endregion


        public void PreActivate()
        {
            if (applicationClass == null) applicationClass = new Word.ApplicationClass();
        }


       
        public void CloseControl()
        {
          
            try
            {
                deactivateevents = true;
                object dummy = null;
                object dummy2 = (object)false;
                document.Close(ref dummy, ref dummy, ref dummy);

                applicationClass.Quit(ref dummy2, ref dummy, ref dummy);
                deactivateevents = false;
            }
            catch (Exception ex)
            {
                String strErr = ex.Message;
            }
        }


     
        private void OnClose(Word.Document doc, ref bool cancel)
        {
            if (!deactivateevents)
            {
                cancel = true;
            }
        }

     
        private void OnOpenDoc(Word.Document doc)
        {
            OnNewDoc(doc);
        }

       
        private void OnNewDoc(Word._Document doc)
        {
            if (!deactivateevents)
            {
                deactivateevents = true;
                object dummy = null;
                doc.Close(ref dummy, ref dummy, ref dummy);
                deactivateevents = false;
            }
        }

     
        private void OnQuit()
        {
            applicationClass = null;
        }


        private bool EnumCP(int hwnd, IntPtr lParam)
        {

            System.Text.StringBuilder sbClassName = new StringBuilder(255);
            GetClassName(hwnd, sbClassName, 255);
            if ("_WwG" == sbClassName.ToString())
            {
                RECT lr = new RECT();
                GetClientRect(hwnd, ref lr);
                this.ContentHeight = lr.y2 - lr.y1;
                return false;
            }

            return true;

        }
        private void GetWordContentHeight(int hwnd)
        {
            if (hwnd != 0)
            {
                EnumChildWindows(hwnd, EnumCP, IntPtr.Zero);
            }
        }
      
        public void LoadDocument(string t_filename)
        {
            deactivateevents = true;
            filename = t_filename;

            if (applicationClass == null) applicationClass = new Word.ApplicationClass();
            try
            {
                applicationClass.CommandBars.AdaptiveMenus = false;
                applicationClass.DocumentBeforeClose += new Word.ApplicationEvents2_DocumentBeforeCloseEventHandler(OnClose);
                applicationClass.NewDocument += new Word.ApplicationEvents2_NewDocumentEventHandler(OnNewDoc);
                applicationClass.DocumentOpen += new Word.ApplicationEvents2_DocumentOpenEventHandler(OnOpenDoc);
                applicationClass.ApplicationEvents2_Event_Quit += new Word.ApplicationEvents2_QuitEventHandler(OnQuit);
                
            }
            catch { }




            if (document != null)
            {
                try
                {
                    object dummy = null;
                    applicationClass.Documents.Close(ref dummy, ref dummy, ref dummy);
                }
                catch { }
            }

            if (wordWnd == 0) wordWnd = FindWindow("Opusapp", null);
            if (wordWnd != 0)
            {
                SetParent(wordWnd, this.Handle.ToInt32());

                object fileName = filename;
                object newTemplate = false;
                object docType = 0;
                object readOnly = true;
                object isVisible = true;
                object missing = System.Reflection.Missing.Value;


                try
                {
                    if (applicationClass == null)
                    {
                        throw new WordInstanceException();
                    }

                    if (applicationClass.Documents == null)
                    {
                        throw new DocumentInstanceException();
                    }

                    if (applicationClass != null && applicationClass.Documents != null)
                    {
                        document = applicationClass.Documents.Add(ref fileName, ref newTemplate, ref docType, ref isVisible);
                    }

                    if (document == null)
                    {
                        throw new ValidDocumentException();
                    }
                }
                catch
                {
                }

                try
                {
                    applicationClass.ActiveWindow.DisplayRightRuler = false;
                    applicationClass.ActiveWindow.DisplayScreenTips = false;
                    applicationClass.ActiveWindow.DisplayVerticalRuler = false;
                    applicationClass.ActiveWindow.DisplayRightRuler = false;
                    applicationClass.ActiveWindow.ActivePane.DisplayRulers = false;
                    applicationClass.ActiveWindow.ActivePane.View.Type = Word.WdViewType.wdWebView;
                    //wd.ActiveWindow.ActivePane.View.Type = Word.WdViewType.wdPrintView;//wdWebView; // .wdNormalView;
                }
                catch
                {

                }
                int counter = applicationClass.ActiveWindow.Application.CommandBars.Count;
                for (int i = 1; i <= counter; i++)
                {
                    try
                    {

                        String nm = applicationClass.ActiveWindow.Application.CommandBars[i].Name;
                        if (nm == "Standard")
                        {
                            int count_control = applicationClass.ActiveWindow.Application.CommandBars[i].Controls.Count;
                            for (int j = 1; j <= 2; j++)
                            {
                                applicationClass.ActiveWindow.Application.CommandBars[i].Controls[j].Enabled = false;
                            }
                        }

                        if (nm == "Menu Bar")
                        {
                            applicationClass.ActiveWindow.Application.CommandBars[i].Enabled = false;
                        }

                        nm = "";
                    }
                    catch 
                    {
                        //MessageBox.Show(ex.ToString());
                    }
                }



                try
                {
                    applicationClass.Visible = true;
                    applicationClass.Activate();

                    SetWindowPos(wordWnd, this.Handle.ToInt32(), 0, 0, this.Bounds.Width, this.Bounds.Height, SWP_NOZORDER | SWP_NOMOVE | SWP_DRAWFRAME | SWP_NOSIZE);
                    OnResize();
                    this.GetWordContentHeight(wordWnd);

                }
                catch
                {
                    MessageBox.Show("Error: do not load the document into the control until the parent window is shown!");
                }

               
                try
                {
                    int hMenu = GetSystemMenu(wordWnd, false);
                    if (hMenu > 0)
                    {
                        int menuItemCount = GetMenuItemCount(hMenu);
                        RemoveMenu(hMenu, menuItemCount - 1, MF_REMOVE | MF_BYPOSITION);
                        RemoveMenu(hMenu, menuItemCount - 2, MF_REMOVE | MF_BYPOSITION);
                        RemoveMenu(hMenu, menuItemCount - 3, MF_REMOVE | MF_BYPOSITION);
                        RemoveMenu(hMenu, menuItemCount - 4, MF_REMOVE | MF_BYPOSITION);
                        RemoveMenu(hMenu, menuItemCount - 5, MF_REMOVE | MF_BYPOSITION);
                        RemoveMenu(hMenu, menuItemCount - 6, MF_REMOVE | MF_BYPOSITION);
                        RemoveMenu(hMenu, menuItemCount - 7, MF_REMOVE | MF_BYPOSITION);
                        RemoveMenu(hMenu, menuItemCount - 8, MF_REMOVE | MF_BYPOSITION);
                        DrawMenuBar(wordWnd);
                    }
                }
                catch { };



                this.Parent.Focus();

            }
            deactivateevents = false;
        }

        public void RestoreWord()
        {
            try
            {
                int counter = applicationClass.ActiveWindow.Application.CommandBars.Count;
                for (int i = 0; i < counter; i++)
                {
                    try
                    {
                        applicationClass.ActiveWindow.Application.CommandBars[i].Enabled = true;
                    }
                    catch
                    {

                    }
                }
            }
            catch { };

        }

     
        private void OnResize()
        {
            int borderWidth = SystemInformation.Border3DSize.Width;
            int borderHeight = SystemInformation.Border3DSize.Height;
            int captionHeight = SystemInformation.CaptionHeight;
            int statusHeight = SystemInformation.ToolWindowCaptionHeight;
            MoveWindow(
                wordWnd,
                -2 * borderWidth,
                -2 * borderHeight - captionHeight,
                this.Bounds.Width + 4 * borderWidth,
                this.Bounds.Height + captionHeight + 4 * borderHeight + statusHeight,
                true);
        }

        private void OnResize(object sender, System.EventArgs e)
        {
            OnResize();
        }


    
        public void RestoreCommandBars()
        {
            try
            {
                int counter = applicationClass.ActiveWindow.Application.CommandBars.Count;
                for (int i = 1; i <= counter; i++)
                {
                    try
                    {

                        String nm = applicationClass.ActiveWindow.Application.CommandBars[i].Name;
                        if (nm == "Standard")
                        {
                            int count_control = applicationClass.ActiveWindow.Application.CommandBars[i].Controls.Count;
                            for (int j = 1; j <= 2; j++)
                            {
                                applicationClass.ActiveWindow.Application.CommandBars[i].Controls[j].Enabled = true;
                            }
                        }
                        if (nm == "Menu Bar")
                        {
                            applicationClass.ActiveWindow.Application.CommandBars[i].Enabled = true;
                        }
                        nm = "";
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
            }
            catch { }

        }


    }

    public class DocumentInstanceException : Exception
    { }

    public class ValidDocumentException : Exception
    { }

    public class WordInstanceException : Exception
    { }

}
