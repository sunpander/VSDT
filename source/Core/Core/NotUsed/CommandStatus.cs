using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VSDT 
{
    public enum CommandStatus
    {
        Enabled,
        Disabled,
        Unavailable 
    }

    public enum CommandPlace
    {
        /// <summary>
        /// 菜单栏
        /// </summary>
        MainMenu,  
        /// <summary>
        /// 子菜单
        /// </summary>
        SubMenu,  
        /// <summary>
        /// 工具栏
        /// </summary>
        ToolBar,   
        /// <summary>
        /// 工程项目弹出菜单
        /// </summary>
        ProjectContextMenu, 
        /// <summary>
        ///  项目弹出菜单
        /// </summary>
        ProjectItemContextMenu,
        /// <summary>
        /// 代码窗体弹出菜单
        /// </summary>
        CodeViewContextMenu
    }
}
