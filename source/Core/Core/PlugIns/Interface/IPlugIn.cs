using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VSDT.Common;
using VSDT.Commands;

namespace VSDT.PlugIns
{
    public interface IPlugIn
    {
        Guid PlugInID { get; }
        //IPlugInContext Context { get; set;}
        //string Location { get; set;}
        //string Name { get; set;}
        int StartLevel { get; set;}

         /// <summary>
         /// 是否启用
         /// </summary>
       PlugInEnableState EnableState { get; set; }
         /// <summary>
         /// 当前状态
         /// </summary>
        PlugInRuntimeState RunTimeState { get; set; }
         /// <summary>
         /// 启用方式
         /// </summary>
       StartMode StartMode { get; set; }

         Version Version { get; set;}
        string SymbolicName { get; set;}
        //Type LoadClass(string className);
        //object LoadResource(string resourceName);
        PlugInInfo PlugInInfo { get; set; }
        string Location { get; }
        void Start();
        void Stop();
        void Uninstall();
    }
   
}