using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualStudio.Shell;

namespace VSDT.Core.ToolWindows
{
    public abstract class ToolWindowsEX : ToolWindowPane
    {
        private UserControl _Control;
        public ToolWindowsEX():base(null) { }
        public ToolWindowsEX(UserControl obj)
            : base(null)
        {
            base.BitmapResourceID = 300;
            base.BitmapIndex = 0;
             this._Control = obj as UserControl ;
            
        }

        public abstract UserControl GetControl();

        public override IWin32Window Window
        {
            get
            {
                return this.GetControl();
            }
        }
    }

    public class CommonToolWindowsEX : ToolWindowsEX
    {
        private UserControl control = null;
        public CommonToolWindowsEX(UserControl obj)
            : base(null)
        {
            control = obj;
        }
        public override UserControl GetControl()
        {
            return control;
        }
    }
}
