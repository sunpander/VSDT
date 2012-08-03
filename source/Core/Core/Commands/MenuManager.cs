using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VSDT.Commands
{
    public class MenuManager : ICommandManagerService
    {
        private static CommandMenuCollection MenuCollection = new CommandMenuCollection();
        //private CommandCollection CommandCollection;
        private static MenuManager _menuManager = new MenuManager();
        public static MenuManager Instance
        {
            get { return _menuManager; }
        }
       
        public event MenuChangedEvent ChangedEvent;

        public MenuManager()
        {
            MenuCollection.ChangedEvent += new BaseCollection<VSMenuCommand>.CollectionChangedEvent(MenuCollection_ChangedEvent);
        }

        void MenuCollection_ChangedEvent(object sender, BaseCollection<VSMenuCommand>.ChangedArgs e)
        {
            if (ChangedEvent != null)
            {
                ChangedEvent(this, new MenuChangedArgs(e.Item, e.ChangeType, e.Item));
            }
        }
        #region ICommandManagerService 成员

        public VSMenuCommand FindCommand(CommandID cmdID)
        {
            return MenuCollection.FindCommand(cmdID);
        }
        public void RegisterCommand(VSMenuCommand command)
        {
            try
            {
                if (MenuCollection.Contains(command))
                {
                    throw new ApplicationException("试图注册按钮,但按钮已注册过");
                }
                if (command.CommandID == null)
                {
                    throw new Exception("试图注册按钮,但按钮的CommandID为空");
                }
                MenuCollection.Add(command);
                //如果Add成功,添加改变事件
                command.TagEx = new object();
            }
            catch (ApplicationException ex) { }
        }
 
        public void UnRegisterCommand(VSMenuCommand command)
        {
            MenuCollection.Remove(command);
            command.TagEx = null;
        }
        public void UnRegisterCommand(CommandID commandID)
        {
            VSMenuCommand command = new VSMenuCommand();
            command.CommandID = commandID;
            MenuCollection.Remove(command);
            command.TagEx = null;
        }

        public List<VSMenuCommand> GetRegisteredCommands()
        {
            return MenuCollection.GetAllItems();
        }

        public List<VSMenuCommand> GetRegisteredCommandsByPlace(MenuCommandPlace place)
        {
            return MenuCollection.GetItemsByPlace(place);
        }
        #endregion
    }

}
