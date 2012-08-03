using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VSDT
{
     public interface IPlugInPackage
    {
         object GetService(Type type);

        // object ShowToolWindowEX(ToolWindowsEX toolWindows,string title,Guid guid);

         object ShowToolWindowEX(UserControl toolWindows, string title, Guid guid);

    }

}
