using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VSDT.PlugIns
{
    /// <summary>
    /// 插件入口
    /// </summary>
    public interface IPlugInEntryPoint
    {
        void Start(IPlugInContext context);
        void Stop(IPlugInContext context);
    }
}