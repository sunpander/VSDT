using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using System.Reflection;

namespace VSDT.Common.Utility
{
    public static class UtilityEnvironment
    {
 
        private const string CURSOR_PATH = "cursors";
        private const string FRAME_CONFIG_FILE = @"config\SuperFrame.config";
        private const string FRAME_CONFIG_PATH = @"config\";
        private const string FRAME_EXE_PATH = "";
        private const string FRAME_FORM_PATH = "";
        private const string FRAME_LANGUAGE_PATH = @"language\";
        private static string frameHomePath;
        private const string HDCOPY_PATH = @"hdcopy\";
        private const string HOME = "SF_HOME2";
        private const string IMAGE_PATH = @"image\";
        private const string LOG_PATH = @"log\";
        private const string PLUGINS_PATH = @"plugins\";
        private const string RESOURCE_PATH = "resource";

        
        public static string GetCursorsPath()
        {
            return Path.Combine(GetFrameworkHomePath(), "cursors");
        }

        public static string GetFrameworkBinPath()
        {
            return Path.Combine(GetFrameworkHomePath(), "");
        }

        public static string GetFrameworkConfigPath()
        {
            return Path.Combine(GetFrameworkHomePath(), @"config\");
        }

        public static string GetFrameworkFormPath()
        {
            return Path.Combine(GetFrameworkHomePath(), "");
        }

        public static string GetFrameworkHomePath()
        {
            if (string.IsNullOrEmpty(frameHomePath))
            {
                frameHomePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
                
            }
            return GetPath(frameHomePath);
        }
 
        public static string GetImagePath()
        {
            return Path.Combine(GetFrameworkHomePath(), @"image\");
        }

        public static string GetLogPath()
        {
            return Path.Combine(GetFrameworkHomePath(), @"log\");
        }

        public static string GetPluginsPath()
        {
            return Path.Combine(GetFrameworkHomePath(), @"plugins\");
        }

        public static string GetResourcePath()
        {
            return Path.Combine(GetFrameworkHomePath(), "resource");
        }

        public static List<string> GetPlugInDirectory()
        {
            List<string> list = new List<string>();
            string str = Assembly.GetExecutingAssembly().Location;
            System.IO.FileInfo file = new System.IO.FileInfo(str);
            str = file.DirectoryName;
            str = System.IO.Path.Combine(str, "PlugIn");
            list.Add(str);
            return list;
        }

        public static string GetPath(string fullPath)
        {
            if (Directory.Exists(fullPath))
            {
                //本来就是一个路径的话,返回
                return fullPath;
            }
            if (!fullPath.Contains("."))
            {
                //不包含点的话,认为就是一个路径,返回
                return fullPath;
            }
            if (fullPath.Contains("\\"))
            {
                return fullPath.Substring(0, fullPath.LastIndexOf("\\"));
            }
            if (fullPath.Contains("/"))
            {
                return fullPath.Substring(0, fullPath.LastIndexOf("/"));
            }
            return fullPath;
        }

        #region 取得当前工作目录
        /// <summary>
        /// 取得当前工作目录
        /// </summary>
        /// <returns></returns>
        public static string GetCurrentDirectory()
        {
            string currentDirectory = "";
            currentDirectory = Directory.GetCurrentDirectory();
            return currentDirectory;
        }
        #endregion
        public static ArrayList GetPlugInConfigPath()
        {
            string path = Path.Combine(GetCurrentDirectory(), "PlugIns");
            string plugInXmlName = "PlugIn.conf";
            return GetFilesFromDirectory(path, plugInXmlName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dirPath"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static ArrayList GetFilesFromDirectory(string dirPath, string fileName)
        {
            DirectoryInfo directory = new DirectoryInfo(dirPath);
            ArrayList list = new ArrayList();
            if (directory.Exists)
            {
                System.IO.FileInfo[] fileInfos;
                if (string.IsNullOrEmpty(fileName))
                {
                    fileInfos = directory.GetFiles();

                    foreach (System.IO.FileInfo file in fileInfos)
                    {
                        list.Add(file.FullName);
                    }
                }
                else if (fileName.Contains(".") && fileName.StartsWith("*"))
                {
                    fileInfos = directory.GetFiles(fileName);

                    foreach (System.IO.FileInfo file in fileInfos)
                    {
                        list.Add(file.FullName);
                    }
                }
                else
                {
                    string externName = fileName.Substring(fileName.LastIndexOf('.'));

                    fileInfos = directory.GetFiles("*" + externName);
                    foreach (System.IO.FileInfo file in fileInfos)
                    {
                        if (file.Name == fileName)
                        {
                            list.Add(file.FullName);
                        }
                    }
                }
                DirectoryInfo[] SUBDirectories = directory.GetDirectories();
                foreach (DirectoryInfo subdirectory in SUBDirectories)
                {
                    ArrayList listTmp = GetFilesFromDirectory(subdirectory.FullName, fileName);
                    if (listTmp != null)
                    {
                        list.AddRange(listTmp);
                    }
                }

                return list;
            }
            return list;
        }

        public static ArrayList GetPlugInConfigFiles(string fileName)
        {
            //获取本地配置
            string path = "";// Path.Combine(Directory.GetCurrentDirectory(), ConstantString.PlugInDefaultDirecotry);

            if (Assembly.GetExecutingAssembly() != null)
            {
                path = Assembly.GetExecutingAssembly().Location;
            }
            path = path.Substring(0, path.LastIndexOf("\\"));
            return GetFilesFromDirectory(path, fileName);
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
                string strValue = rk.GetValue("CodeBase").ToString();
                return strValue.Substring(0, strValue.LastIndexOf("\\"));
            }
            catch
            {
                return "";
            }
        }

        public static string GetPackageInstalPath()
        {
            try
            {
                return  GetPackageInstalPath( "2fe8dc52-b791-42db-805b-fcdebda43d45");
                
            }
            catch
            {
                return "";
            }
        }

        public static string GetVisualStudio2008Path()
        {
            try
            {
                string keyName = @"Software\Microsoft\VisualStudio\9.0\";   //{2fe8dc52-b791-42db-805b-fcdebda43d45}
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
