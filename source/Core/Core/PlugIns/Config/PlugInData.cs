using System;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using System.Reflection;

namespace VSDT.PlugIns
{
    public class PlugInData
    {
        /// <summary>
        /// 入口点
        /// </summary>
        public EntryPointData EntryPoint { get; set; }
        /// <summary>
        /// 插件自身描述信息
        /// </summary>
        public PlugInInfoData PlugInInfo { get; set; }
        /// <summary>
        /// 扩展点(向外)
        /// </summary>
        public List<ExtensionData> Extensions { get { if (extensions == null) extensions = new List<ExtensionData>(); return extensions; } }
        private List<ExtensionData> extensions;
        /// <summary>
        /// 初始状态
        /// </summary>
        public PlugInEnableState InitializedState { get; set; }
        /// <summary>
        /// 启动模式
        /// </summary>
        public StartMode StartMode { get; set; }

        /// <summary>
        /// 运行时
        /// </summary>
        private RuntimeData runtime;

        public RuntimeData Runtime
        {
            get { if (runtime == null)runtime = new RuntimeData(); return runtime; }
            set { runtime = value; }
        }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 路径
        /// </summary>
        public string FilePath { get; set; }
        /// <summary>
        /// 启动级别
        /// </summary>
        public int StartLevel { get; set; }
        /// <summary>
        /// 符号名 
        /// </summary>
        public string SymbolicName { get; set; }
        /// <summary>
        /// 版本信息
        /// </summary>
        public Version Version { get; set; }
        public string Xmlns { get; set; }
    }

    public class EntryPointData
    {
        public EntryPointData(string type) {this.Type = type; }

        public string Type { get; set; }
    }

    public class AssemblyData
    {
        public AssemblyData(string path,bool share)
        {
            Path = path;
            Share = share;
        }
         public AssemblyData(XmlNode node ,string direcotry)
        {
              if (string.Compare(node.Name, "Assembly", true) == 0)
               {
                        Path =System.IO.Path.Combine(direcotry, node.Attributes["Path"].Value);
               }
         }
        public string Path { get; set; }
        //public string[] PathArray { get; }
        public bool Share { get; set; }
    }

    public class PlugInInfoData
    {
        public PlugInInfoData() { }
        public string Author { get; set; }
        public string Category { get; set; }
        public string ContactAddress { get; set; }
        public string Copyright { get; set; }
        private string description = "";
        public string Description { get{ return description;} set{description = value;} }
        public string DocumentLocation { get; set; }
        public string ManifestVersion { get; set; }
        public string UpdateLocation { get; set; }
    }
    
    public class RuntimeData
    {
        public RuntimeData(){}
        private List<AssemblyData> _assemblies ;
        public List<AssemblyData> Assemblies { 
            get{
                if(_assemblies == null)
                    _assemblies = new List<AssemblyData>();
                return _assemblies;
            }
        }
        public void AddAssembly(AssemblyData assembly)
        {
            _assemblies.Add(assembly);
        }
    }

    public class ExtensionData
    {
        public ExtensionData(XmlNode node)
        {
             this.Point =   node.Attributes["Point"].Value;
             childNodes = new List<XmlNode>();
             if( node.ChildNodes!=null)
             {
                 for (int i = 0; i < node.ChildNodes.Count; i++)
			     {
                     childNodes.Add(node.ChildNodes[i]);
			     }
             }
        }
        private List<XmlNode> childNodes;
        public List<XmlNode> ChildNodes { get{if(childNodes==null) childNodes=new List<XmlNode>();return childNodes;} }
        public string Point { get; set; }
    }

}
 