using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace VSDT.Common
{
    /// <summary>
    /// 通用配置信息类
    /// (1)各个模块如果用到了通用配置项,先建一个类保存配置信息,其中属性为public 且是可序列化的
    /// (2)调用LoadConfigByType 传入配置类的类型.可获取已保存的该类的对象
    /// (3)调用SaveNewConfig    传入配置类的对象,可以改类类型为主键,保存改对象的内容
    /** 简单例子,把TTA_AddressConfig换成 你的配置文件类的名称
    public static TTA_AddressConfig LoadConfig()
        {
            object obj =  XMLConfigUtility.LoadConfigByType(typeof(TTA_AddressConfig));
               if (obj is TTA_AddressConfig)
                   return obj as TTA_AddressConfig;
                else
                   return new TTA_AddressConfig();
        }

        public void SaveConfig()
        {
            XMLConfigUtility.SaveNewConfig(this);
    }*/
    /// </summary>
    public class XMLConfigUtility
    {
        //private static bool Debug = false;
       
        /// <summary>
        /// 添加配置信息类 对象
        /// </summary>
        /// <paramKey name="obj">可序列化的类对象</paramKey>
        /// <returns>是否成功</returns>
        public static bool SaveNewConfig(object obj)
        {
            string fname = obj.GetType().FullName;
            //去掉+ ,当是内部类时会出现加号
            fname = fname.Replace("+", ".");
            try
            {
                //配置文件 
                fileName = System.IO.Path.Combine(ConfigDirectory, fileName);
                //先序列化
                System.Xml.Serialization.XmlSerializer xs = new System.Xml.Serialization.XmlSerializer(obj.GetType());
                StringBuilder sb = new StringBuilder();
                System.IO.StringWriter sw = new System.IO.StringWriter(sb);
                xs.Serialize(sw, obj);
                sw.Close();
                //插入序列化后的字符串
                InsertObjString(fname, sb.ToString());
            }
            catch (Exception ex)
            {
                Log.ShowMsgBox("错误:"+ex.Message);
                return false;
            }
            return true;
        }
        /// <summary>
        /// 通过类型获取 配置信息类 对象
        /// </summary>
        /// <paramKey name="type">可序列化的类类型</paramKey>
        /// <returns></returns>
        public static object LoadConfigByType(Type type)
        {
            try
            {
                //配置文件 
                fileName = System.IO.Path.Combine(ConfigDirectory, fileName);

                string fname = type.FullName;
                //去掉+ ,当是内部类时会出现加号
                fname = fname.Replace("+", ".");
                //找到对应节点
                string value = GetObjString(fname);
                if (value.Equals(string.Empty))
                {
                    return new object();
                }

                //找对应字符串
                System.Xml.Serialization.XmlSerializer xs = new System.Xml.Serialization.XmlSerializer(type);
                //System.IO.StreamReader sr = new System.IO.StreamReader(localConfigPath);
                System.IO.StringReader sr = new System.IO.StringReader(value);
                object obj = xs.Deserialize(sr);

                //反序列化
                return obj;
            }
            catch  
            {
                return new object();
            }
        }
        /// <summary>
        /// 配置信息文件名
        /// </summary>
        internal static string fileName = "iPlat4CToolkit.config";
        public static string ConfigDirectory = "";
        private static void InsertObjString(string name, string value)
        {
            value = value.Trim();
            value = value.Replace("<?xml version=\"1.0\" encoding=\"utf-16\"?>", "");
            value = value.Trim();
            if (!System.IO.File.Exists(fileName))
            {
                InsertFirstTime(name, value);
            }
            else
            {
                InsertNew(name, value);
            }
        }

        private static void InsertFirstTime(string name, string value)
        {
            //创建
            if (fileName.Contains("\\"))
            {
                string path = fileName.Substring(0, fileName.LastIndexOf("\\"));
                if (!System.IO.Directory.Exists(path))
                {
                    System.IO.Directory.CreateDirectory(path);
                }
            }
            //创建一个XML对象
            XmlDocument myxml = new XmlDocument();
            //创建一个根节点
            XmlElement xmlRoot = myxml.CreateElement("configuration");
            //添加根节点
            myxml.AppendChild(xmlRoot);
            //configSections [ Section List ]
            XmlElement xmlSectionList = myxml.CreateElement("configSections");
            XmlElement xmlSection = myxml.CreateElement("Sections");
            xmlSection.SetAttribute("NAME", name);
            xmlSectionList.AppendChild(xmlSection);
            xmlRoot.AppendChild(xmlSectionList);//添加到根节点中
            //Section Detail
            XmlElement xmlDetailsList = myxml.CreateElement("sectionsDetails");
            XmlElement xmlDetails = myxml.CreateElement(name);
            xmlDetails.InnerXml = value;
            xmlDetailsList.AppendChild(xmlDetails);
            xmlRoot.AppendChild(xmlDetailsList);//添加到根节点中
            myxml.Save(fileName);
        }

        private static void InsertNew(string name, string value)
        {
            //string name = nameNow;
            //string value = valueNow;
            //创建一个XML对象
            XmlDocument myxml = new XmlDocument();
            //读取已经有的xml
            myxml.Load(fileName);
            //声明一个节点存储根节点
            XmlNode root = myxml.DocumentElement;

            bool hasSection = false;
            foreach (XmlNode var in root.ChildNodes)
            {
                if (var.Name.Equals("configSections"))
                {
                    foreach (XmlNode var2 in var)
                    {
                        if (var2.Attributes["NAME"] == null)
                            return;
                        if (var2.Attributes["NAME"].Value.ToString().Equals(name))
                        {
                            hasSection = true;
                            break;
                        }
                    }
                }
            }
            if (hasSection)
            {
                //已存在,更新内容
                foreach (XmlNode var in root.ChildNodes)
                {
                    if (var.Name.Equals("sectionsDetails") && hasSection)
                    {
                        foreach (XmlNode var3 in var)
                        {
                            if (var3.Name.Equals(name) && var3.InnerXml != value)
                            {
                                var3.InnerXml = value; //更新内容
                            }
                        }
                    }
                }
            }
            else
            {
                //否则直接插入
                foreach (XmlNode var in root.ChildNodes)
                {
                    if (var.Name.Equals("configSections"))
                    {
                        XmlElement xmlSection = myxml.CreateElement("Sections");
                        xmlSection.SetAttribute("NAME", name);
                        var.AppendChild(xmlSection);

                    }
                    if (var.Name.Equals("sectionsDetails"))
                    {
                        XmlElement xmlSection = myxml.CreateElement(name);
                        xmlSection.InnerXml = value;
                        var.AppendChild(xmlSection);
                    }
                }
            }
            myxml.Save(fileName);
        }
   
        private static string GetObjString(string name)
        {
            if (!System.IO.File.Exists(fileName))
            {
                return string.Empty;
            }
            try
            {
                //创建一个XML对象
                XmlDocument myxml = new XmlDocument();
                //读取已经有的xml
                myxml.Load(fileName);
                //声明一个节点存储根节点
                XmlNode root = myxml.DocumentElement;

                bool hasSection = false;
                foreach (XmlNode var in root.ChildNodes)
                {
                    if (var.Name.Equals("configSections"))
                    {
                        foreach (XmlNode var2 in var)
                        {
                            if (var2.Attributes["NAME"] == null)
                                return string.Empty;
                            if (var2.Attributes["NAME"].Value.ToString().Equals(name))
                            {
                                hasSection = true;
                                break;
                            }
                        }
                    }
                    if (var.Name.Equals("sectionsDetails") && hasSection)
                    {
                        foreach (XmlNode var3 in var)
                        {
                            if (var3.Name.Equals(name))
                            {
                                string value = var3.InnerXml;
                                return value;
                            }
                        }
                    }
                }
            }
            catch  
            {
                return string.Empty;
            }
            return string.Empty;
        }
    }
}
