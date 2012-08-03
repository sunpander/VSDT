using System;
using System.Xml;
using System.IO;

namespace VSDT.PlugIns
{
    public class PlugInDataParser
    {
        public static PlugInData CreatePlugInData(string path)
        {
            PlugInData plugInData = new PlugInData();
            if (!File.Exists(path))
            {
                throw new Exception(string.Format("文件{0}不存在", path));
            }
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(path);

            if (string.Compare(xmldoc.DocumentElement.Name, "PlugIn", true) == 0)
            {
                plugInData.FilePath = path.Substring(0, path.LastIndexOf("\\"));
                //读取PlugIn属性
                int _count = xmldoc.DocumentElement.Attributes.Count;
                for (int i = 0; i < _count; i++)
                {
                    string _name = xmldoc.DocumentElement.Attributes[i].Name.ToLower();
                    string _value = xmldoc.DocumentElement.Attributes[i].Value;
                    switch (_name)
                    {
                        case "name":
                            plugInData.Name = _value;
                            break;
                        case "symbolicname":
                            plugInData.SymbolicName = _value;
                            break;
                        case "version":
                            plugInData.Version = new Version(_value);
                            break;
                        case "startlevel":
                            int tmp = 50;
                            Int32.TryParse(_value, out tmp);
                            plugInData.StartLevel = tmp;
                            break;
                        case "initializedstate":
                            try{
                            plugInData.InitializedState = (PlugInEnableState)System.Enum.Parse(typeof(PlugInEnableState), _value);
                            }
                            catch
                            {
                                plugInData.InitializedState = PlugInEnableState.Enable;
                            }
                                break;
                        case "startmode":
                            try
                            {
                                plugInData.StartMode = (StartMode)System.Enum.Parse(typeof(StartMode), _value);
                            }
                            catch {
                                plugInData.StartMode = StartMode.NeedLogin;
                            }
                            break;
                        default:
                            break;
                    }
                }
                //读取PlugIn入口点
                XmlNode _node = xmldoc.SelectSingleNode("PlugIn/EntryPoint");
                if (_node != null)
                {
                    _count = _node.Attributes.Count;
                    for (int i = 0; i < _count; i++)
                    {
                        string _name = _node.Attributes[i].Name.ToLower();
                        string _value = _node.Attributes[i].Value;
                        plugInData.EntryPoint = new EntryPointData(_value);
                    }
                }
                //读取插件详细描述信息
                _node = xmldoc.SelectSingleNode("PlugIn/PlugInInfo");
                PlugInInfoData infoData = new PlugInInfoData();
                if (_node != null)
                {
                    _count = _node.Attributes.Count;
                    for (int i = 0; i < _count; i++)
                    {
                        string _name = _node.Attributes[i].Name.ToLower();
                        string _value = _node.Attributes[i].Value;
                        switch (_name)
                        {
                            case "author":
                                infoData.Author = _value;
                                break;
                            case "description":
                                infoData.Description = _value;
                                break;
                            case "updatelocation":
                                infoData.UpdateLocation = _value;
                                break;
                            default:
                                break;
                        }
                    }
                }
                plugInData.PlugInInfo = infoData;
                //读取运行时dll（exe）全路径
                _node = xmldoc.SelectSingleNode("PlugIn/Runtime");
                if (_node != null)
                {
                    foreach (XmlNode item in _node.ChildNodes)
                    {
                        AssemblyData assembly = new AssemblyData(item, plugInData.FilePath);
                        plugInData.Runtime.Assemblies.Add(assembly);
                    }
                }
                //读取扩展节点....此处可能有多个Extension
                XmlNodeList _nodeList = xmldoc.SelectNodes("PlugIn/Extension");
                if (_nodeList != null)
                {
                    for (int i = 0; i < _nodeList.Count; i++)
                    {
                        _node = _nodeList[i];
                        ExtensionData _extensionData = new ExtensionData(_node);
                        plugInData.Extensions.Add(_extensionData);
                    }
                }
            }
            return plugInData;
        }
    }
}

