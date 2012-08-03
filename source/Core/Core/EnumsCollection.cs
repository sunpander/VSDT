using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VSDT
{
    /// <summary>
    /// 运行状态
    /// </summary>
    public enum PlugInRuntimeState
    {
        /// <summary>
        /// 已安装
        /// </summary>
        Installed = 0,
        /// <summary>
        /// 正在启动
        /// </summary>
        Starting =1,
        /// <summary>
        /// 已启动
        /// </summary>
        Started = 2,
        /// <summary>
        /// 正在停止
        /// </summary>
        Stopping = 3,
        /// <summary>
        /// 停止
        /// </summary>
        Stopped = 4,
        /// <summary>
        /// 卸载
        /// </summary>
        Uninstalled = 5,
        /// <summary>
        /// 未知
        /// </summary>
        None
    }
    /// <summary>
    /// 启动模式
    /// </summary>
    public enum StartMode
    {
        /// <summary>
        /// 自动启动
        /// </summary>
        Autorun =0,
        /// <summary>
        /// 需要登录
        /// </summary>
        NeedLogin=1,
        /// <summary>
        /// 需要验证权限
        /// </summary>
        NeetPermission=2
    }
    /// <summary>
    /// 是否可用
    /// </summary>
    public enum PlugInEnableState
    {
        /// <summary>
        /// 启用
        /// </summary>
        Enable,
        /// <summary>
        /// 禁用
        /// </summary>
        Disable,
        /// <summary>
        /// 卸载
        /// </summary>
        Uninstall  
    }

    public enum ResolutionType
    {
        Mandatory = 0,
        Optional = 1,
    }

    public enum CollectionChangedAction
    {
        Add = 0,
        Remove = 1,
        Reset = 2,
    }


    public enum MenuCommandPlace
    {
        /// <summary>
        /// 菜单栏
        /// </summary>
        MainMenu = 0x1,//1
        /// <summary>
        /// 子菜单
        /// </summary>
        SubMenu = 0x2,//10
        /// <summary>
        ///  项目弹出菜单
        /// </summary>
        ProjectItemContextMenu = 0x8,//1000
        /// <summary>
        ///  项目弹出菜单
        /// </summary>
        ProjectItemSubContextMenu = 0x10,//1000
        /// <summary>
        /// 代码窗体弹出菜单
        /// </summary>
        CodeWindowContextMenu = 0x20,//10000
        /// <summary>
        /// 代码窗体弹出菜单
        /// </summary>
        CodeWindowSubContextMenu = 0x40//100000
    }
    public enum EnumChangeType
    {
        Add,
        Remove,
        Update
    }
}
