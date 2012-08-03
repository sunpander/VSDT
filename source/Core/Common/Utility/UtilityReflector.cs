using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Reflection;
using System.Windows.Forms;

namespace VSDT.Common.Utility
{
    public   class UtilityReflector
    {
        public static System.Reflection.Assembly[] GetAssemblesByPath(string baseName, string path)
        {
            string[] files = System.IO.Directory.GetFiles(path, baseName + "*.dll");
            if ((files == null) || (files.Length == 0))
            {
                return new System.Reflection.Assembly[0];
            }
            List<System.Reflection.Assembly> list = new List<System.Reflection.Assembly>();
            foreach (string str in files)
            {
                if (string.Compare(System.IO.Path.GetFileNameWithoutExtension(str), baseName, true) != 0)
                {
                    try
                    {
                        System.Reflection.Assembly item = System.Reflection.Assembly.LoadFrom(str);
                        list.Add(item);
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            return list.ToArray();
        }

        public bool IsDefinedAttribute<T>(Assembly assembly ,ref object value) where T:Attribute 
        {
            // (查找AssemblyInfo.cs文件内属性)
            bool blDefined = assembly.IsDefined(typeof(T),true);
            if(blDefined)
            {
                value = assembly.GetCustomAttributes(typeof(T), true)[0] as T;
            }else
            {
                value = null;
            }
            return blDefined;
        }

        public static bool ImplementsInterface(Type type, Type interfaceType)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }
            if (interfaceType == null)
            {
                throw new ArgumentNullException("interfaceType");
            }
            return (type.GetInterface(interfaceType.FullName, true) != null);
        }


        public static Object RunStartMethod(string dllFullPathName, string classFullName)
        {
            try
            {
                if (!System.IO.File.Exists(dllFullPathName))
                {
                    string exeFullPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
                    string Path = exeFullPath.Substring(0, exeFullPath.LastIndexOf(System.IO.Path.DirectorySeparatorChar));
                    dllFullPathName = System.IO.Path.Combine(Path, dllFullPathName);

                    if (!System.IO.File.Exists(dllFullPathName))
                    {
                        MessageBox.Show("DLL文件不存在");
                        return null;
                    }
                }

                System.Reflection.Assembly dllAssemblly = System.Reflection.Assembly.LoadFrom(dllFullPathName);
                Type[] types =  dllAssemblly.GetTypes();
                for (int i = 0; i < types.Length; i++)
                {
                     
                }
                object obj = dllAssemblly.CreateInstance(classFullName);


                return obj;
                //某个dll下
                //System.Reflection.Assembly
                //某个名称空间下
                //某个类(Form窗体)
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
                return null;
            }
        }

        public static Object GetObjectByDllName(string dllFullPathName,string classFullName)
        {
            try
            {
                if (!System.IO.File.Exists(dllFullPathName))
                {
                    string exeFullPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
                    string Path = exeFullPath.Substring(0, exeFullPath.LastIndexOf(System.IO.Path.DirectorySeparatorChar));
                    dllFullPathName = System.IO.Path.Combine(Path, dllFullPathName);

                    if (!System.IO.File.Exists(dllFullPathName))
                    {
                        MessageBox.Show("DLL文件不存在");
                        return null;
                    }
                }
                
                System.Reflection.Assembly dllAssemblly = System.Reflection.Assembly.LoadFrom(dllFullPathName);
 
                object obj = dllAssemblly.CreateInstance(classFullName);
                return obj;
                //某个dll下
                //System.Reflection.Assembly
                //某个名称空间下
                //某个类(Form窗体)
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
                return null;
            }
        }

        /// <summary>
        /// 根据类 数组,获取一个类属性值构建的表
        /// 表的列为类的公共属性,每一行表示一个类对象对应值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static System.Data.DataTable GetProtertyTable<T>(List<T> para)
        {
            System.Data.DataTable dtResult = new System.Data.DataTable();
            //Assembly assembly = new Assembly(
            Type type = typeof(T);
            FieldInfo[] fileInfos =   type.GetFields();
            //为表添加列
            for (int i = 0; i < fileInfos.Length; i++)
            {
                FieldInfo infoTmp = fileInfos[i];
                dtResult.Columns.Add(infoTmp.Name, infoTmp.FieldType);
            }
            //添加值
            for (int i = 0; i < para.Count; i++)
            {
                T tmp = para[i];
                dtResult.Rows.Add();
                for (int j = 0; j < dtResult.Columns.Count; j++)
                {
                   dtResult.Rows[i][j] =  type.InvokeMember(dtResult.Columns[j].ColumnName, BindingFlags.GetField, null, tmp, null);
                }
            }
            return dtResult;
        }
    }
}
