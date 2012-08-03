using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VSDT.Common.Utility;
using System.Diagnostics;
using System.ComponentModel;
using System.Windows.Forms;

namespace VSDT.Common.Tools
{
    public static class MachineHelp
    {
        public static void RestartProcessByName(string processName)
        {
            if (Process.GetCurrentProcess().ProcessName != processName)
            {
                System.Diagnostics.Process[] devProcess = System.Diagnostics.Process.GetProcessesByName(processName);
                for (int i = 0; i < devProcess.Length; i++)
                {
                    devProcess[i].Kill();
                }
                System.Diagnostics.Process.Start(processName);
            }
            else
            {
                throw new ArgumentException("不能自己重启自己.");
            }
        }
        public static void RestartVS()
        {
            try
            {
                RestartProcessByName("devenv");
            }
            catch (ArgumentException ex)
            {
                //2fe8dc52-b791-42db-805b-fcdebda43d45 为VSDT的packagestring
                string pkgStr = "{2fe8dc52-b791-42db-805b-fcdebda43d45}";
                RestartVS(pkgStr);
            }
            catch (Exception e)
            {
                Log.ShowErrorBox(e);
            }
        }

        public static void RestartVS(string pkgStr)
        {
            try
            {
                string restartSoftware = @"SysPlugins\RestartApplication\RestartApplication.exe";
                string strInstalPath = GetPackageInstalPath(pkgStr);
                if (string.IsNullOrEmpty(strInstalPath.Trim()))
                {
                    throw new Exception(string.Format("未安装Package:{0}", pkgStr));
                }
                string restartToolPath = System.IO.Path.Combine(strInstalPath, restartSoftware);
                if (System.IO.File.Exists(restartToolPath))
                {
                    Process.Start(restartToolPath);
                }
            }
            catch (Exception e)
            {
                Log.ShowErrorBox(e);
            }
        }
        public static string GetPackageInstalPath(string packageStr)
        {
            try
            {
                if (!packageStr.StartsWith("{"))
                {
                    packageStr = "{" + packageStr + "}";
                }
                string keyName = @"Software\Microsoft\VisualStudio\9.0\Packages\" + packageStr;   //{2fe8dc52-b791-42db-805b-fcdebda43d45}
                Microsoft.Win32.RegistryKey rk = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(keyName);
                if (rk == null)
                {
                    rk = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(keyName);
                    if (rk == null)
                        return "";
                }
                string  strValue = rk.GetValue("CodeBase").ToString();
                return strValue.Substring(0, strValue.LastIndexOf("\\"));
            }
            catch
            {
                return "";
            }
        }

        public static void Restart()
        {
            CancelEventArgs e = new CancelEventArgs(true);
            Application.Exit(e);
            if (!e.Cancel)
            {
                Application.Restart();
            }
        }
    }
}
