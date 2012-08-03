using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VSDT.PlugIns;
using VSDT.Common;
using EnvDTE;
using EnvDTE80;
 

namespace VSDT
{
     public class PlunInRuntime
     {
         #region 单例
         private PlunInRuntime() {
        }
        private static PlunInRuntime instance = new PlunInRuntime();
        public static PlunInRuntime Instance { get { return instance; } }
       
        public static DTE2 DTEObject {get ;set;}
        public static IServiceProvider ServiceProvider { get; set; }
        public static IPlugInPackage Package { get; set; }
         
         #endregion
        private Framework _framework;
         //只读,所有地方置使用这一个Framework
        public IFramework  Framework { get { return _framework; } }

        private PlugInRuntimeState state= PlugInRuntimeState.Stopped;
        public PlugInRuntimeState State { get { return state; } }
         /// <summary>
         /// 初始化插件信息
         /// </summary>
        private void InitializePlugIns()
        {
            //读取已安装的插件信息
            List<PlugInInfo> installedPlugIns = plugInAdmin.GetLocalPlugIns();
            Framework.PlugIns.Clear();
            for (int i = 0; i < installedPlugIns.Count; i++)
            {
                PlugInInfo infoTemp = installedPlugIns[i];
                if (infoTemp.PlugInState == PlugInEnableState.Uninstall)
                {
                    plugInAdmin.Uninstall(infoTemp);
                    continue;
                }
                IPlugIn plugIn = PlugInFactory.CreatePlugIn(installedPlugIns[i].PlugInData);
                //Framework.PlugIns.Contains(plugIn,
                Framework.PlugIns.Add(plugIn);
            }
        }
        private PlugInManager plugInAdmin = new PlugInManager();
         /// <summary>
         /// 查找本地默认目录下,已安装的插件.并启动<br></br>
         /// 寻找PligIns目录下的plugin.conf文件,并执行start方法
         /// </summary>
        public void Start()
        {
            try
            {
                
                if (state == PlugInRuntimeState.Started)
                {
                    throw new Exception("已启动");
                }
                state = PlugInRuntimeState.Starting;
                //初始化框架---之后所有相关都使用这一个Framework对象
                _framework = VSDT.Framework.Inistace;
                _framework.dte = DTEObject;
                _framework.Package = Package;
                _framework.ServiceProvider = ServiceProvider;
                FrameworkOptions option = new FrameworkOptions();
                option.StartUpDir = Common.Utility.UtilityEnvironment.GetFrameworkBinPath();
                _framework.Options = option;
                //初始化插件信息
                InitializePlugIns();
                //启动自动启动项
                Start(StartMode.Autorun);
                state = PlugInRuntimeState.Started;
            }
            catch (Exception ex)
            {
                Log.ShowErrorBox(ex);
            }
        }

        private void Start(StartMode startMode)
        {
            for (int i = 0; i < Framework.PlugIns.Count; i++)
            {
                PlugIns.IPlugIn plugIn = Framework.PlugIns[i];
                if (plugIn.StartMode ==startMode )//&& plugIn.State!= PlugInState.Active)
                {
                    plugIn.Start();
                }
            }
        }
        public void Stop() {
            try
            {
                if (state == PlugInRuntimeState.Stopped)
                {
                    throw new Exception("已启动");
                }
                state = PlugInRuntimeState.Stopping;

                //停止
                for (int i = 0; i < Framework.PlugIns.Count; i++)
                {
                    PlugIns.IPlugIn plugIn = Framework.PlugIns[i];
                    if (plugIn.RunTimeState == PlugInRuntimeState.Started)
                    {
                        plugIn.Stop();
                    }
                }
                state = PlugInRuntimeState.Stopped;
            }
            catch (Exception ex)
            {
                Log.ShowErrorBox(ex);
            }
        }
    }
}
