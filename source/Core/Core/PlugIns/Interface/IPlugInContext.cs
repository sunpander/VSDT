using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
 

namespace VSDT.PlugIns
{
    public interface IPlugInContext
    {
         IPlugIn PlugIn { get; set;}
         IFramework Framework { get; set;}

        //event EventHandler<ExtensionEventArgs> ExtensionChanged;
        //event EventHandler<ExtensionPointEventArgs> ExtensionPointChanged;
        VSDT.Commands.MenuManager menuManager { get; }
    }
    public class PlugInContext : IPlugInContext
    {
  
        #region IPlugInContext 成员

        VSDT.Commands.MenuManager IPlugInContext.menuManager
        {
            get { return VSDT.Commands.MenuManager.Instance;   }
        }

        #endregion

        #region IPlugInContext 成员
        private IPlugIn plugIn;
        public IPlugIn PlugIn
        {
            get
            {
                return plugIn;
            }
            set
            {
                plugIn = value;
            }
        }
        private IFramework framework;
        public IFramework Framework
        {
            get
            {
                return framework;
            }
            set
            {
                framework = value;
            }
        }

        #endregion
    }

 
   
}
