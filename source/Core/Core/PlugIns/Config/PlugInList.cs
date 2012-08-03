using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VSDT.Common;

namespace VSDT.PlugIns.Config
{
    public class PlugInStatusList
    {
        [Serializable]
        public class PlugInStatus
        {
            public string path;
            public PlugInEnableState PlugInState { get; set; }
        }
        public List<PlugInStatus> listStatus = new List<PlugInStatus>();

        private string configDir = "";
        private string configFileName = "";

        public PlugInStatusList()
        {
            configDir = VSDT.Common.Utility.UtilityEnvironment.GetFrameworkConfigPath();
            configFileName = "plugins.conf";

            VSDT.Common.XMLConfigUtility.ConfigDirectory = configDir;
            VSDT.Common.XMLConfigUtility.fileName = configFileName;
        }

        public static PlugInStatusList LoadConfig()
        {
            object obj = XMLConfigUtility.LoadConfigByType(typeof(PlugInStatusList));
            if (obj is PlugInStatusList)
                return obj as PlugInStatusList;
            else
                return new PlugInStatusList();
        }

        public void SaveConfig()
        {
            VSDT.Common.XMLConfigUtility.ConfigDirectory = configDir;
            VSDT.Common.XMLConfigUtility.fileName = configFileName;
            XMLConfigUtility.SaveNewConfig(this);
        }
    }
}