using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VSDT.Common;
using System.IO;
using VSDT.PlugIns;

namespace VSDT.Core.PlugIns
{
    public interface IPlugInManager
    {
        List<PlugInInfo> GetLocalPlugIns();
        void Start(string PlugInSymbolicName);    
        void Stop(string PlugInSymbolicName);
        void Uninstall(string PlugInSymbolicName);
        void Update(string PlugInSymbolicName);
    }
}
