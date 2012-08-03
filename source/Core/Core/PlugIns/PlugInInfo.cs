using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VSDT.PlugIns
{
    public class PlugInInfo
    {
        public PlugInInfo() { }
        public PlugInInfo(PlugInData data)
        {
            this.PlugInData = data;
            FilePath = data.FilePath;
            this.PlugInState = data.InitializedState;
        }
        public string FilePath { get; set; }
        public PlugInData PlugInData { get; set; }
        public PlugInEnableState PlugInState { get; set; }
    }
}
