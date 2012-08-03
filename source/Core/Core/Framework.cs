using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
//using VSDT.PlugInCore;
using System.Reflection;
using VSDT.Common;
using VSDT.PlugIns;

namespace VSDT
{
    public class Framework : IFramework
    {
        #region IFramework 成员
        private PlugInRepository plugIns;
        public PlugInRepository PlugIns
        {
            get
            {
                if (plugIns == null)
                {
                    plugIns = new PlugInRepository(this);
                }
                return plugIns;
            }
            set { plugIns = value; }
        }
        private bool isActive = false;
        public bool IsActive
        {
            get
            {
                return isActive;
            }
        }

        private FrameworkOptions _options;
        public FrameworkOptions Options
        {
            get
            {
               return _options;
            }
            set
            {
              _options = value;
            }
        }

        public IPlugIn GetPlugIn(string location)
        {
            throw new NotImplementedException();
        }
 
        public IPlugIn GetPlugInBySymbolicName(string name)
        {
            throw new NotImplementedException();
        }

        public void RemoveSystemService(Type serviceType, object serviceInstance)
        {
            throw new NotImplementedException();
        }

        public bool RunCommand(string cmd)
        {
            throw new NotImplementedException();
        }
        public void Start(StartMode startMode)
        {
            for (int i = 0; i <  PlugIns.Count; i++)
            {
                PlugIns.IPlugIn plugIn =  PlugIns[i];
                if (plugIn.EnableState == PlugInEnableState.Enable)
                {
                    if (plugIn.StartMode == startMode)//&& plugIn.State!= PlugInState.Active)
                    {
                        plugIn.Start();
                    }
                }
            }
        }
        public void Start()
        {
           
        }

        public void Stop()
        {
            
        }

        #endregion
        private Framework()
        {
            
        }

        private static Framework _winPlatform;
        public static Framework Inistace
        {
            get
            {
                if (_winPlatform == null)
                {
                    _winPlatform = new Framework();
                }
                return _winPlatform;
            }
        }

        #region IFramework 成员


        public EnvDTE80.DTE2 dte
        {
            get;
            set;
        }

        #endregion

        #region IFramework 成员

        private IPlugInPackage package;
        public IPlugInPackage Package
        {
            get
            {
                return package;
            }
            set
            {
                package = value;
            }
        }

        #endregion

        #region IFramework 成员

        private IServiceProvider _serviceProvider;
        public IServiceProvider ServiceProvider
        {
            get
            {
                return _serviceProvider;
            }
            set
            {
                _serviceProvider = value;
            }
        }

        #endregion
    }
}
