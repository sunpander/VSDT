using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VSDT.NotUsed
{
    public delegate void CommandChangedEvent(object sender, CommandChangedArgs e);
    public class  CommandChangedArgs: EventArgs
    {
        public CommandChangedArgs(ItemBase item, EnumChangeType type, object value)
        {
            this.CommandItem = item;
            this.ChangeType = type;
            this.Value = value;
        }
        public ItemBase CommandItem;
        public  EnumChangeType  ChangeType;
        public enum EnumChangeType
        {
            Add,
            Remove
        }
        public object Value;
    }
    public   class CommandManager
    {
        public static List<ItemBase> dtItems;

        #region 通过HashTable存储
        private static System.Collections.Hashtable commandTable = new System.Collections.Hashtable();
     //   private static System.Collections.DictionaryEntry commandTable = new System.Collections.Hashtable();

        public static System.Collections.Hashtable CommandTable
        {
            get { return commandTable; }
        }

        public static void AddItem2(ItemBase item)
        {
            if (item == null)
            {
                throw new Exception("参数不能为空.");
            }
            if (commandTable.ContainsKey(item.Name))   
            {
                throw new Exception("名称已存在.");
            }
            commandTable.Add(item.Name, item);
        }
        public static void RemoveItem2(ItemBase item)
        {
            if (item == null)
            {
                throw new Exception("参数不能为空.");
            }
            if (commandTable.ContainsKey(item.Name))
            {
                commandTable.Remove(item.Name);
            }
            else
            {
                throw new Exception("不存在.");
            }
        }

 
        #endregion

        public   event CommandChangedEvent commandChangedEvent;
        public    void AddItem(ItemBase item)
        {
            if (dtItems == null)
            {
                dtItems = new List<ItemBase>();
            }
            if (item == null)
            {
                throw new Exception("参数不能为空.");
            }
            if (Contains(item.Name))
            {
                throw new Exception("名称已存在.");
            }
            dtItems.Add(item);
            if (commandChangedEvent != null)
            {
                CommandChangedArgs args = new CommandChangedArgs(item, CommandChangedArgs.EnumChangeType.Add, null);
                commandChangedEvent(null,args);
            }
        }
        public   bool RemoveItem(ItemBase item)
        {
            if (dtItems == null)
            {
                dtItems = new List<ItemBase>();
            }
            if (item == null)
            {
                throw new Exception("参数不能为空.");
            }
            if (commandChangedEvent != null)
            {
                CommandChangedArgs args = new CommandChangedArgs(item, CommandChangedArgs.EnumChangeType.Remove, null);
                commandChangedEvent(null, args);
            }
            return dtItems.Remove(item);
         }


        private readonly static CommandManager instance = new CommandManager();
        public static CommandManager Instance
        {
            get
            {
                return instance;
            }
        }
        public static bool Contains(string  name)
        {
            for (int i = 0; i < dtItems.Count; i++)
            {
                if (dtItems[i].Name == name)
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 获取命令项
        /// </summary>
        /// <returns></returns>
        public static List<ItemBase> GetAllItems()
        {
            return dtItems;
        }

        public static List<string> GetAllCommandItemsName()
        {
            if (dtItems == null)
                return new List<string>();
            List<string> _listItemName = new List<string>();
            for (int i = 0; i < dtItems.Count; i++)
            {
                if (dtItems[i] is CommandItem)
                {
                    _listItemName.Add(dtItems[i].Name);
                }
            }
            return _listItemName;
        }
        
        public static ItemBase GetItemByName(string name)
        {
            if (dtItems == null)
                return null;
            for (int i = 0; i < dtItems.Count; i++)
            {
                if (dtItems[i].Name == name)
                    return dtItems[i];
            }
            return null;
        }

        public static bool RemoveItemByName(string name)
        {
            for (int i = 0; i < dtItems.Count; i++)
            {
                if (dtItems[i].Name == name)
                {
                     dtItems.RemoveAt(i);
                     return true;
                }
            }
            return false;
        }
    }
}
