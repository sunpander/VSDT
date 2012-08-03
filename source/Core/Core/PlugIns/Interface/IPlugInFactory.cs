using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VSDT.PlugIns
{
    public interface IPlugInFactory
    {
        IPlugIn CreatePlugIn(PlugInData PlugInData);
    }
}
