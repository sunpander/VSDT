using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VSDT.PlugIns
{
    public class PlugInFactory  
    {
        #region IPlugInFactory 成员
        public static IPlugIn CreatePlugIn(PlugInData plugInData)
        {
            PlugIn plugIn = new PlugIn(Guid.NewGuid());
            plugIn.PlugInInfo = new PlugInInfo(plugInData);
            plugIn.Name = plugInData.Name;
            plugIn.Location = Common.Utility.UtilityEnvironment.GetPath(plugInData.FilePath);
            plugIn.StartMode = plugInData.StartMode;
            plugIn.EnableState = plugInData.InitializedState;
            plugIn.SymbolicName = plugInData.SymbolicName;
            plugIn.Version = plugInData.Version;
            return plugIn;
        }

        #endregion
    }
}
