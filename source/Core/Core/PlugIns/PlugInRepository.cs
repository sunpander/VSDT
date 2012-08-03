using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VSDT.PlugIns
{
    /// <summary>
    /// PlugIn集合
    /// </summary>
    public class PlugInRepository:List<IPlugIn>
    {
         public PlugInRepository(IFramework framework)
        {

        }

        //public IPlugIn GetPlugIn(long plugInID)
        //{
        //    for (int i = 0; i < this._plugInList.Count; i++)
        //    {
        //        if (this._plugInList[i].PlugInID == plugInID)
        //            return this._plugInList[i];
        //    }
        //    return null;
        //}
        //public IPlugIn GetPlugIn(string location)
        //{
        //    for (int i = 0; i < this._plugInList.Count; i++)
        //    {
        //        if (this._plugInList[i].Location == location)
        //            return this._plugInList[i];
        //    }
        //    return null;
        //}
        //public IPlugIn GetPlugInBySymbolicName(string name)
        //{
        //    for (int i = 0; i < this._plugInList.Count; i++)
        //    {
        //        if (this._plugInList[i].SymbolicName == name)
        //            return this._plugInList[i];
        //    }
        //    return null;
        //}
        //public IPlugIn RemovePlugIn(long plugInID)
        //{
        //    for (int i = 0; i < this._plugInList.Count; i++)
        //    {
        //        if (this._plugInList[i].PlugInID == plugInID)
        //        {
        //            IPlugIn plugIn = this._plugInList[i];
        //            this._plugInList.RemoveAt(i);
        //            return plugIn;
        //        }
        //    }
        //    return null;
        //}
        //public IPlugIn RemovePlugIn(string location)
        //{
        //    for (int i = 0; i < this._plugInList.Count; i++)
        //    {
        //        if (this._plugInList[i].Location == location)
        //        {
        //            IPlugIn plugIn = this._plugInList[i];
        //            this._plugInList.RemoveAt(i);
        //            return plugIn;
        //        }
        //    }
        //    return null;
        //}
    }
}
