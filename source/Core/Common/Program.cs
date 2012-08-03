using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace VSDTCommonTools
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            try
            {
                if (args.Length > 0)
                {
                    string dllName = "";
                    string formName = "";
                    if (args.Length == 1)
                    {
                        dllName = "VSDT.Common.dll";
                        formName = args[0];
                        if (!formName.Contains("."))
                        {
                            formName = "VSDT.Common.Tools." + formName;
                        }
                    }
                    else if (args.Length == 2)
                    {
                        dllName = args[0];
                        formName = args[1];
                    }
                    else
                    {
                        dllName = args[0];
                        formName = args[1];
                    }
                    string exeFullPath = System.Reflection.Assembly.GetExecutingAssembly().Location; ;
                    string Path = exeFullPath.Substring(0, exeFullPath.LastIndexOf(System.IO.Path.DirectorySeparatorChar));
                    string dllFullName = System.IO.Path.Combine(Path, dllName);

                    System.Reflection.Assembly dllAssemblly = System.Reflection.Assembly.LoadFrom(dllFullName);

                    object obj = dllAssemblly.CreateInstance(formName);
                    if (obj != null && obj is Form)
                    {
                        (obj as Form).StartPosition = FormStartPosition.CenterScreen;
                        (obj as Form).ShowDialog();
                    }
                    //某个dll下
                    //System.Reflection.Assembly
                    //某个名称空间下
                    //某个类(Form窗体)
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
            return;
        }
    }
}
