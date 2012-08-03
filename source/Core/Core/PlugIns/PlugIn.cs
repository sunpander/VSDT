using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VSDT.PlugIns;
using VSDT.Commands;
using VSDT.Common;

namespace VSDT.PlugIns
{
    public class PlugIn : IPlugIn
    {
        public PlugIn(Guid guid)
        {
            this.plugInID = guid;
        }
        #region IPlugIn 成员

        private Guid plugInID;
        private IPlugInEntryPoint entryPoint;
        public IPlugInEntryPoint EntryPoint { get { return entryPoint; } }
        public IPlugInContext Context;
        /// <summary>
        /// 插件目录
        /// </summary>
        public string Location
        {
            get { return location; }
            internal set
            {
                location = value;
            }
        }
        private string location;
        public string Name;

        private int startLevel;
        /// <summary>
        /// 是否启用
        /// </summary>
        private PlugInEnableState enableState;
        /// <summary>
        /// 当前状态
        /// </summary>
        public PlugInRuntimeState runTimeState;
        /// <summary>
        /// 启用方式
        /// </summary>
        private StartMode startMode;

        public Version Version { get; set; }

        public string SymbolicName { get; set; }

        private int commandCount = 0;
        private Dictionary<string, object> listLoadClass = new Dictionary<string, object>();
        private Dictionary<VSMenuCommand, object> directory = new Dictionary<VSMenuCommand, object>();

        /// <summary>
        /// 读取入口点,执行入口点方法
        /// </summary>
        private void StartFromCode()
        {
            if (EntryPoint != null)
            {
                EntryPoint.Start(Context);
                return;
            }
            string entryPointTypeName = PlugInInfo.PlugInData.EntryPoint.Type;
            int runtimeDllCount = PlugInInfo.PlugInData.Runtime.Assemblies.Count;
            for (int i = runtimeDllCount - 1; i >= 0; i--)
            {
                string runtimeAssemblyPath = PlugInInfo.PlugInData.Runtime.Assemblies[i].Path;

                System.Reflection.Assembly dllAssemblly = System.Reflection.Assembly.LoadFrom(runtimeAssemblyPath);
                Type type = dllAssemblly.GetType(entryPointTypeName);
                if (type != null)
                {
                    object obj = Activator.CreateInstance(type);
                    entryPoint = obj as IPlugInEntryPoint;
                    if (EntryPoint != null)
                    {
                        PlugInContext context = new PlugInContext();
                        context.Framework = Framework.Inistace;
                        context.PlugIn = this;
                        
                        this.Context = context;
                        EntryPoint.Start(context);
                    }
                    //System.Reflection.MethodInfo method = type.GetMethod(ConstantString.EntryPointStartMethod);
                    //if (method != null)
                    //{
                    //    PlugInContext context = new PlugInContext();
                    //    context.Framework = Framework.Inistace;
                    //    context.PlugIn = this;
                    //    this.Context = context;
                    //    method.Invoke(obj, new object[] { context });
                    //}
                }
            }
        }
        /// <summary>
        /// 从xml启动插件
        /// </summary>
        private void StartFromXML()
        {
            List<ExtensionData> list = this.PlugInInfo.PlugInData.Extensions;
            for (int i = 0; i < list.Count; i++)
            {
                if (string.Compare(list[i].Point, ConstantString.VSMenuPoint, true) == 0)
                {
                    List<System.Xml.XmlNode> listNodes = list[i].ChildNodes;
                    for (int j = 0; j < listNodes.Count; j++)
                    {
                        if (string.Compare(listNodes[j].Name, "Application", true) == 0)
                        {
                            string title = listNodes[j].Attributes["Title"].Value.ToString();

                            foreach (System.Xml.XmlNode nodeTemp in listNodes[j].ChildNodes)
                            {
                                if (string.Compare(nodeTemp.Name, "Menu", true) == 0)
                                {
                                    string text = nodeTemp.Attributes["Text"].Value.ToString();
                                    string className = nodeTemp.Attributes["Class"].Value.ToString();
                                    string commandID = nodeTemp.Attributes["ID"].Value.ToString();
                                    int id = int.Parse(commandID);
                                    Guid guid = this.plugInID;
                                    VSDT.Commands.VSMenuCommand cmd = new VSDT.Commands.VSMenuCommand();
                                    cmd.CommandID = new VSDT.Commands.CommandID(guid, id);
                                    cmd.Click += new EventHandler(cmd_Click);
                                    cmd.Caption = text;
                                    if (string.Compare(title, ConstantString.TopMenu, true) == 0)
                                    {
                                        cmd.Position = VSDT.MenuCommandPlace.MainMenu;
                                    }
                                    else if (string.Compare(title, ConstantString.SubMenu, true) == 0)
                                    {
                                        cmd.Position = VSDT.MenuCommandPlace.SubMenu;
                                    }
                                    VSDT.Commands.MenuManager.Instance.RegisterCommand(cmd);
                                    directory.Add(cmd, className);
                                }
                            }
                        }
                    }
                }
            }
        }

        void cmd_Click(object sender, EventArgs e)
        {
            try
            {
                VSMenuCommand cmd = sender as VSMenuCommand;
                if (cmd != null)
                {

                    object className = "";
                    if (directory.TryGetValue(cmd, out className))
                    {
                        if (className is string)
                        {
                            if (className.ToString().EndsWith(".exe"))
                            {
                                string exeFullPath = System.IO.Path.Combine(Location, className.ToString());
                                //启动单例
                                //System.Diagnostics.Process.Start(exeFullPath);
                                System.Diagnostics.Process proc = new System.Diagnostics.Process();
                                proc.StartInfo = new System.Diagnostics.ProcessStartInfo(exeFullPath);
                                bool bl = proc.Start();
                                directory[cmd] = proc;
                            }
                            else //当做窗体或自定义控件
                            {
                                object obj = LoadClass(className.ToString(), true);
                                if (obj != null)
                                {
                                    if (obj is System.Windows.Forms.Form)
                                    {
                                        (obj as System.Windows.Forms.Form).StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
                                        (obj as System.Windows.Forms.Form).Show();
                                        (obj as System.Windows.Forms.Form).Activate();
                                    }
                                }
                            }
                        }
                        else if (className is System.Diagnostics.Process)
                        {
                            (className as System.Diagnostics.Process).Start();
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Log.ShowErrorBox(ex);
            }
        }
        private object LoadClass(string className)
        {
            return LoadClass(className, false);
        }
        private object LoadClass(string className, bool needForm)
        {
            if (listLoadClass.ContainsKey(className))
            {
                object ojb = listLoadClass[className];
                if (ojb != null)
                {
                    if (ojb is System.Windows.Forms.Form)
                    {
                        System.Windows.Forms.Form frm = ojb as System.Windows.Forms.Form;
                        if (frm.Created)
                        {
                            return ojb;
                        }
                    }
                }
            }
            if (this.PlugInInfo.PlugInData.Runtime != null)
            {
                List<AssemblyData> list = this.PlugInInfo.PlugInData.Runtime.Assemblies;
                for (int i = 0; i < list.Count; i++)
                {
                    string path = System.IO.Path.Combine(Location, list[i].Path);
                    if (System.IO.File.Exists(path))
                    {
                        System.Reflection.Assembly assembly = System.Reflection.Assembly.LoadFrom(path);

                        Type type = assembly.GetType(className.ToString(), false);
                        if (type != null)
                        {
                            object obj = assembly.CreateInstance(className.ToString(), true);
                            if (needForm)
                            {
                                //如果期望得到一个form.
                                if (obj is System.Windows.Forms.Form)
                                {

                                }
                                else if (obj is System.Windows.Forms.Control)
                                {
                                    System.Windows.Forms.Control ctrl = obj as System.Windows.Forms.Control;
                                    System.Windows.Forms.Form frm = new System.Windows.Forms.Form();
                                    frm.Controls.Add(ctrl);
                                    ctrl.Dock = System.Windows.Forms.DockStyle.Fill;
                                    obj = frm;
                                }
                            }
                            if (listLoadClass.ContainsKey(className))
                            {
                                listLoadClass[className] = obj;
                            }
                            else
                            {
                                listLoadClass.Add(className, obj);
                            }
                            return obj;
                        }
                    }
                }
            }
            return null;
        }
        /// <summary>
        /// 启动
        /// </summary>
        public void Start()
        {
            try
            {
                if (enableState != PlugInEnableState.Enable)
                {
                    throw new Exception("插件不可用");
                }
                if (runTimeState == PlugInRuntimeState.Started)
                {
                    throw new Exception("插件已启动");
                }
                runTimeState = PlugInRuntimeState.Starting;
                //先读取xml配置
                StartFromXML();
                //如果定义了入口点,则执行入口点方法
                if (PlugInInfo.PlugInData.EntryPoint != null && !string.IsNullOrEmpty(PlugInInfo.PlugInData.EntryPoint.Type))
                {
                    //如果定义入口点,则去找start方法
                    StartFromCode();
                }
                runTimeState = PlugInRuntimeState.Started;
            }
            catch (Exception ex)
            {
                Log.ShowErrorBox(ex);
            }
        }
        /// <summary>
        /// 停止
        /// </summary>
        public void Stop()
        {
            try
            {
                if (runTimeState != PlugInRuntimeState.Started)
                {
                    //未启动
                    throw new Exception("未启动");
                }
                runTimeState = PlugInRuntimeState.Stopping;
                //先读取xml配置..停止通过xml启动的项
                StopFromXML();
                //如果定义了入口点,则执行入口点方法
                if (PlugInInfo.PlugInData.EntryPoint != null && !string.IsNullOrEmpty(PlugInInfo.PlugInData.EntryPoint.Type))
                {
                    //如果定义入口点,则去找stop方法
                    StopFromCode();
                }
                runTimeState = PlugInRuntimeState.Stopped;
            }
            catch (Exception ex) { Log.ShowErrorBox(ex); }

        }

        private void StopFromCode()
        {
            if (EntryPoint != null)
            {
                EntryPoint.Stop(Context);
                return;
            }
            string entryPointTypeName = PlugInInfo.PlugInData.EntryPoint.Type;
            int runtimeDllCount = PlugInInfo.PlugInData.Runtime.Assemblies.Count;
            for (int i = runtimeDllCount - 1; i >= 0; i--)
            {
                string runtimeAssemblyPath = PlugInInfo.PlugInData.Runtime.Assemblies[i].Path;

                System.Reflection.Assembly dllAssemblly = System.Reflection.Assembly.LoadFrom(runtimeAssemblyPath);
                Type type = dllAssemblly.GetType(entryPointTypeName);
                if (type != null)
                {
                    object obj = Activator.CreateInstance(type);
                    entryPoint = obj as IPlugInEntryPoint;
                    if (EntryPoint != null)
                    {
                        PlugInContext context = new PlugInContext();
                        context.Framework = Framework.Inistace;
                        context.PlugIn = this;

                        this.Context = context;
                        EntryPoint.Stop(Context);
                    }
                    //if (type != null)
                    //{
                    //    System.Reflection.MethodInfo method = type.GetMethod(ConstantString.EntryPointStopMethod);
                    //    if (method != null)
                    //    {
                    //        PlugInContext context = new PlugInContext();
                    //        method.Invoke(obj, new object[] { context });
                    //    }
                    //}
                }
            }
        }

        private void StopFromXML()
        {
            foreach (KeyValuePair<VSMenuCommand, object> item in directory)
            {
                //如果是已启动的进程,则关掉 进程
                if (item.Value is System.Diagnostics.Process)
                {
                    try
                    {
                        System.Diagnostics.Process[] process = System.Diagnostics.Process.GetProcessesByName((item.Value as System.Diagnostics.Process).ProcessName);
                        if (process != null && process.Length > 0)
                        {
                            (item.Value as System.Diagnostics.Process).Kill();
                        }
                    }
                    catch (Exception ex) { Log.ShowErrorBox(ex); }
                }
                //如果是窗体则关掉窗体
                else if (item.Value is System.Windows.Forms.Form)
                {
                    (item.Value as System.Windows.Forms.Form).Close();
                }
                VSDT.Commands.MenuManager.Instance.UnRegisterCommand(item.Key);
            }
            listLoadClass.Clear();
            directory.Clear();
        }
        /// <summary>
        /// 卸载
        /// </summary>
        public void Uninstall()
        {

        }

        #endregion



        #region IPlugIn 成员   plugInInfo
        private PlugInInfo _plugInInfo;
        public PlugInInfo PlugInInfo
        {
            get
            {
                return _plugInInfo;
            }
            set
            {
                _plugInInfo = value;
            }
        }

        #endregion

        #region IPlugIn 成员 StartLevel

        int IPlugIn.StartLevel
        {
            get
            {
                return startLevel;
            }
            set
            {
                startLevel = value;
            }
        }

        public StartMode StartMode
        {
            get
            {
                return startMode;
            }
            set
            {
                startMode = value;
            }
        }

        public PlugInEnableState EnableState
        {
            get
            {
                return enableState;
            }
            set
            {
                if (value != enableState)
                {
                    if (value == PlugInEnableState.Enable)
                    {
                        enableState = value;
                        Start();
                    }
                    else if (value == PlugInEnableState.Disable)
                    {
                        enableState = value;
                        Stop();
                    }
                    else if (value == PlugInEnableState.Uninstall)
                    {
                        enableState = value;
                        Uninstall();
                    }
                    //DoEnableChanging(enableState,value);
                }
            }
        }

        public PlugInRuntimeState RunTimeState
        {
            get
            {
                return runTimeState;
            }
            set
            {
                runTimeState = value;
            }
        }
        #endregion

        #region IPlugIn 成员 PlugInID

        public Guid PlugInID
        {
            get { return plugInID; }
        }

        #endregion
    }
}