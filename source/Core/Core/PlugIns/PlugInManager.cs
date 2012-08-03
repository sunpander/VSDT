using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VSDT.Common;
using System.IO;
using System.Collections;
using VSDT.Common.Utility;
using System.Reflection;
using VSDT.PlugIns.Config;

namespace VSDT.PlugIns
{
    internal class PlugInManager 
    {
        /// <summary>
        /// 通过plugIn.conf文件获取已安装的插件信息
        /// </summary>
        /// <returns></returns>
        public List<PlugInInfo> GetLocalPlugIns()
        {
            List<PlugInInfo> listLocalPlugInData = new List<PlugInInfo>();

            List<VSDT.PlugIns.Config.PlugInStatusList.PlugInStatus> listStatus =  PlugInStatusList.LoadConfig().listStatus;
            //获取所有插件文件
            ArrayList listPlugInFiles = UtilityEnvironment.GetPlugInConfigFiles(ConstantString.PlugInConfigFileName);
            for (int i = 0; i < listPlugInFiles.Count; i++)
            {
                PlugInData data = PlugInDataParser.CreatePlugInData(listPlugInFiles[i].ToString());
                PlugInInfo info = new PlugInInfo();
                info.PlugInData = data;
                info.FilePath = data.FilePath;
                if (listStatus.Count > 0)
                {
                    info.PlugInState = listStatus.Single(t => t.path == info.FilePath).PlugInState;
                }
                else
                {
                    info.PlugInState = data.InitializedState;
                }
                listLocalPlugInData.Add(info);
            }
            //排序
            listLocalPlugInData.Sort(new Comparison<PlugInInfo>(ComparePlugInByStartLevel));
            return listLocalPlugInData;
        }
        /// <summary>
        /// 按StartLevel排序
        /// </summary>
        /// <param name="paraA"></param>
        /// <param name="paraB"></param>
        /// <returns></returns>
        private int ComparePlugInByStartLevel(PlugInInfo paraA, PlugInInfo paraB)
        {
            if (paraA == null && paraB == null)
                return 0;
            if (paraA == null && paraB != null)
                return -1;
            if (paraA != null && paraB == null)
                return 1;
            if (paraA != null && paraB != null)
                return paraA.PlugInData.StartLevel - paraB.PlugInData.StartLevel;
            return 0;
        }
        /// <summary>
        /// 卸载
        /// </summary>
        /// <param name="plugInInfo"></param>
        public void Uninstall(PlugInInfo plugInInfo)
        {
            //卸载(直接删除文件)
            try
            {
                Directory.Delete(plugInInfo.PlugInData.FilePath, true);
            }
            catch (Exception ex)
            {
                //删除出错时
            }
        }



        public void Start(PlugInInfo plugInInfo)
        {
            try
            {
                IPlugIn plugIn = PlugInFactory.CreatePlugIn(plugInInfo.PlugInData);
                plugIn.Start();
            }
            catch (Exception ex) { Log.ShowErrorBox(ex); }
        }
        public void Stop(PlugInInfo plugInInfo)
        {
            try
            {
                //PlugInFactory factory = new PlugInFactory();
                IPlugIn plugIn = PlugInFactory.CreatePlugIn(plugInInfo.PlugInData);
                plugIn.Stop();
            }
            catch (Exception ex) { Log.ShowErrorBox(ex); }
        }

        #region   成员
        public IPlugIn InstallPlugIn(string location)
        {
            throw new NotImplementedException();
        }

        public IPlugIn InstallPlugIn(string location, Stream stream)
        {
            throw new NotImplementedException();
        }
 
        public void Update(string PlugInSymbolicName)
        {
             
        }
        #endregion
    }
}
