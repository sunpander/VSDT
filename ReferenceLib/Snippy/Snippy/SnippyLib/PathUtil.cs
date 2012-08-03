using System;
using System.Collections.Generic;
using System.Text;

namespace Snippy.SnippyLib
{
    class PathUtil
    {
        public static string GetCodeNipPath()
        {
            //D:\Program Files\Microsoft Visual Studio 9.0\Common7\IDE
            string strInstalPath = GetVisualStudio2008Path();
            int i = strInstalPath.IndexOf(@"Common7\IDE");
            string folder = strInstalPath.Substring(0, i);
            string path = System.IO.Path.Combine(folder,   @"VC#\Snippets\2052\Visual C#");
            return path;
        }

        public static string GetVisualStudio2008Path()
        {
            try
            {
                string keyName = @"Software\Microsoft\VisualStudio\9.0\";    
                Microsoft.Win32.RegistryKey rk = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(keyName);
                if (rk == null)
                {
                    rk = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(keyName);
                    if (rk == null)
                        return "";
                }
                string strValue = rk.GetValue("InstallDir").ToString();
                return strValue.Substring(0, strValue.LastIndexOf("\\"));
            }
            catch
            {
                return "";
            }
        }
    }
}
