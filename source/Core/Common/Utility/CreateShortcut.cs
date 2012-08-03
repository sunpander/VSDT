using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
 
using VSDT.Common;
namespace VSDT.Common.Utility
{
    internal partial class CreateShortcutForm : Form
    {
        public CreateShortcutForm()
        {
            InitializeComponent();
        }
        public bool IsCreateOnDesktop 
        {
            get
            {
                return cbxOndesk.Checked;
            }
            set
            {
                cbxOndesk.Checked = value;
            }
        }
        public bool IsCreateOnStartPrograms
        {
            get
            {
                return cbxStart.Checked;
            }
            set
            {
                cbxStart.Checked = value;
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }        
    }


    public class CreateShortcut
    {
        public bool ShowCreateForm = true;

        public string Description = "";
        public string TargetPath = "";
        public string Name = "";
        public string WorkingDirectory = "";
        public bool IsCreateOnDesktop= true;

        public bool IsCreateOnStartPrograms = false;
        public bool IsExecuteWithParameter = false;
        private string ExecuteParameter = "";
        public void Create()
        {
            if (string.IsNullOrEmpty(TargetPath))
            {
                Log.ShowMsgBox("目标文件为空");
                return;
            }
            System.IO.FileInfo info = new FileInfo(TargetPath);
            if (!info.Exists)
            {
                Log.ShowMsgBox("目标文件不存在");
                return;
            }
            if (string.IsNullOrEmpty(Name.Trim()))
            {
                Name = info.Name.Replace(info.Extension,"");
            }
            if (string.IsNullOrEmpty(WorkingDirectory))
            {
                WorkingDirectory = info.DirectoryName;
            }
            if (ShowCreateForm)
            {
                CreateShortcutForm frm = new CreateShortcutForm();
                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.IsCreateOnStartPrograms = IsCreateOnStartPrograms;
                frm.IsCreateOnDesktop = IsCreateOnDesktop;
                if (DialogResult.OK == frm.ShowDialog())
                {
                    IsCreateOnStartPrograms = frm.IsCreateOnStartPrograms;
                    IsCreateOnDesktop = frm.IsCreateOnDesktop;
                }
                else
                {
                    return;
                }
            }
            if (IsCreateOnStartPrograms)
            {
                CreatShortcut(Environment.SpecialFolder.Programs);
            }
            if (IsCreateOnDesktop)
            {
                CreatShortcut(Environment.SpecialFolder.Desktop);
            }
        }

        
        public void CreateWithParameter(string str)
        {
            IsExecuteWithParameter = true;
            ExecuteParameter = str;
            Create(); 
        }

        public void CreateOnDesktop()
        {
            CreatShortcut(Environment.SpecialFolder.Desktop);
        }
        public void CreateOnStartPrograms()
        {
            CreatShortcut(Environment.SpecialFolder.Programs);
        }

        private void CreatShortcut(Environment.SpecialFolder folder)
        {
            string path = Environment.GetFolderPath(folder) + "\\" + Name + ".lnk";
            if (System.IO.File.Exists(path))
            {
                File.Delete(path);
            }

            //IWshRuntimeLibrary.WshShell shell = new IWshRuntimeLibrary.WshShell();
            //IWshRuntimeLibrary.IWshShortcut shortcut = (IWshRuntimeLibrary.IWshShortcut)shell.CreateShortcut(path);
            //if (IsExecuteWithParameter)
            //{
            //    //if (TargetPath.EndsWith("exe"))
            //    //{
            //        shortcut.Arguments = ExecuteParameter;
            //    //}
            //}
            //shortcut.TargetPath = TargetPath  ;          //shadowCopyPath + Path.DirectorySeparatorChar + @"Baosight.SR.iPlat4c.Launch.exe";
            //shortcut.WorkingDirectory = WorkingDirectory;

            //shortcut.Description = Description;     //String.Format("iPlac4C平台{0}环境启动快捷方式", projectName);
            //shortcut.Save();
        }

    }
}
