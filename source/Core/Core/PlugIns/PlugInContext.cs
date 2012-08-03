using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VSDT.PlugIns;
 

namespace VSDT 
{
    public class PlugInContext : IPlugInContext
    {
        #region IPlugInContext 成员
        public IPlugIn PlugIn
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public IFramework Framework
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        //public event EventHandler<ExtensionEventArgs> ExtensionChanged;

        //public event EventHandler<ExtensionPointEventArgs> ExtensionPointChanged;

        #endregion

        #region IPlugInContext 成员

        public VSDT.Commands.MenuManager menuManager
        {
            get { throw new NotImplementedException(); }
        }

        #endregion
    }
}
