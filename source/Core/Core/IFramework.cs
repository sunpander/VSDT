using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VSDT.PlugIns;
using EnvDTE80;
 
namespace VSDT
{
    public interface IFramework
    {
        PlugInRepository PlugIns { get; }
        bool IsActive { get; }
        IServiceProvider ServiceProvider { get; set; }
        DTE2 dte { get; set; }
        IPlugInPackage Package { get; set; }
        //ICommandRepository Commands { get; }
        //EventManager EventManager { get; }
        FrameworkOptions Options { get; }
        IPlugIn GetPlugIn(string location);
        IPlugIn GetPlugInBySymbolicName(string name);
        //IServiceManager ServiceContainer { get; }
        //void AddSystemService(object serviceInstance, params Type[] serviceTypes);
        //void AddSystemService(Type serviceType, params object[] serviceInstances);
        //void RemoveSystemService(Type serviceType, object serviceInstance);
        //bool RunCommand(string cmd);

        void Start();
        void Stop();
    }
}
