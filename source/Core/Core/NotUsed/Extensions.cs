using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using VSDT.PlugIns;
using System.Reflection;

namespace VSDT.NotUsed
{
    public class ExtensionManager : IExtensionManager
    {
        private List<ExtensionPoint> _extentionList;
        private List<Extension> _extentionList2;
        void AddExtension(string point, Extension extension) { }
        ExtensionPoint GetExtensionPoint(string point) { return _extentionList[0]; }
        List<ExtensionPoint> GetExtensionPoints() { return _extentionList; }
        List<ExtensionPoint> GetExtensionPoints(IPlugIn PlugIn) { return _extentionList; }
        List<Extension> GetExtensions(string extensionPoint) { return _extentionList2; }
        void RemoveExtension(Extension extension) { }


        #region IExtensionManager 成员

        event EventHandler<ExtensionEventArgs> IExtensionManager.ExtensionChanged
        {
            add { throw new NotImplementedException(); }
            remove { throw new NotImplementedException(); }
        }

        event EventHandler<ExtensionPointEventArgs> IExtensionManager.ExtensionPointChanged
        {
            add { throw new NotImplementedException(); }
            remove { throw new NotImplementedException(); }
        }

        void IExtensionManager.AddExtension(string point, Extension extension)
        {
            throw new NotImplementedException();
        }

        ExtensionPoint IExtensionManager.GetExtensionPoint(string point)
        {
            throw new NotImplementedException();
        }

        List<ExtensionPoint> IExtensionManager.GetExtensionPoints()
        {
            throw new NotImplementedException();
        }

        List<ExtensionPoint> IExtensionManager.GetExtensionPoints(IPlugIn PlugIn)
        {
            throw new NotImplementedException();
        }

        List<Extension> IExtensionManager.GetExtensions(string extensionPoint)
        {
            throw new NotImplementedException();
        }

        void IExtensionManager.RemoveExtension(Extension extension)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
    public class Extension
    {
        public Extension() { }

        public List<XmlNode> Data { get; internal set; }
        public IPlugIn Owner { get; internal set; }
    }
    public class ExtensionPoint
    {
        public IList<Extension> Extensions { get; set; }
        public IPlugIn Owner { get; internal set; }
        public string Point { get; internal set; }
        public string Schema { get; internal set; }

        public void AddExtension(Extension extension) { }
        public void RemoveExtension(Extension extension) { }
    }
    public class ExtensionEventArgs : EventArgs
    {
        public CollectionChangedAction Action { get; set; }
        public IPlugIn PlugIn { get; set; }
        public Extension Extension { get; internal set; }
        public IExtensionManager ExtensionManager { get; set; }
        public string ExtensionPoint { get; set; }
    }
    public class ExtensionPointData
    {
        public ExtensionPointData() { }
        private List<XmlNode> childNodes = new List<XmlNode>();
        public List<XmlNode> ChildNodes { get { return childNodes; } }
        public string Point { get; set; }
        public string Schema { get; set; }
    }
    public class ExtensionPointEventArgs : EventArgs
    {
        public CollectionChangedAction Action { get; set; }
        public IPlugIn PlugIn { get; set; }
        public IExtensionManager ExtensionManager { get; set; }
        public ExtensionPoint ExtensionPoint { get; internal set; }
    }
    class Extensions
    {
    }
    public interface IExtensionManager
    {
        event EventHandler<ExtensionEventArgs> ExtensionChanged;
        event EventHandler<ExtensionPointEventArgs> ExtensionPointChanged;

        void AddExtension(string point, Extension extension);
        ExtensionPoint GetExtensionPoint(string point);
        List<ExtensionPoint> GetExtensionPoints();
        List<ExtensionPoint> GetExtensionPoints(IPlugIn PlugIn);
        List<Extension> GetExtensions(string extensionPoint);
        void RemoveExtension(Extension extension);
    }


    public class ServiceData
    {
        public ServiceData() { }

        public string[] Interfaces { get; set; }
        public string Type { get; set; }
    }
    public class VersionRange : IComparable<VersionRange>, IComparable, IEquatable<VersionRange>
    {
        public VersionRange() { }
        public VersionRange(string version) { }
        public VersionRange(string minVersion, string maxVersion) { }

        //public static bool operator !=(VersionRange v1, VersionRange v2)  
        //public static bool operator <(VersionRange v1, VersionRange v2) 
        //public static bool operator <=(VersionRange v1, VersionRange v2)  
        //public static bool operator ==(VersionRange v1, VersionRange v2) 
        //public static bool operator >(VersionRange v1, VersionRange v2) 
        //public static bool operator >=(VersionRange v1, VersionRange v2)  

        public bool IsIncludedEqualMaxVersion { get; set; }
        public bool IsIncludedEqualMinVersion { get; set; }
        public Version MaxVersion { get; set; }
        public Version MinVersion { get; set; }

        //public int CompareTo(object obj);
        //public int CompareTo(VersionRange other);
        //public override bool Equals(object obj);
        //public bool Equals(VersionRange other);
        //public override int GetHashCode();
        //public bool IsIncluded(Version version);
        //public override string ToString();

        #region IComparable<VersionRange> 成员

        public int CompareTo(VersionRange other)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IComparable 成员

        public int CompareTo(object obj)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IEquatable<VersionRange> 成员

        public bool Equals(VersionRange other)
        {
            throw new NotImplementedException();
        }

        #endregion
    }

    public interface IRuntimeService
    {
        List<Assembly> LoadPlugInAssembly(string PlugInSymbolicName);
    }
    public interface IExceptionHandler
    {
        void Handle(object ex);
    }

    public interface IStartLevelManager
    {
        int InitialPlugInStartLevel { get; set; }
        int StartLevel { get; set; }

        void ChangePlugInStartLevel(IPlugIn PlugIn, int startLevel);
        void ChangeStartLevel(int startLevel);
    }



    public interface IPlugInPersistent
    {
        List<string> InstalledPlugInLocation { get; set; }
        //List<UnInstallPlugInOption> UnInstalledPlugInLocation { get; set; }

        object Load(string file);
        void Save(string file);
        void SaveInstallLocation(string path);
        void SaveUnInstallLocation(string path);
        void SaveUnInstallLocation(string path, bool needRemove);
    }
}
