using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VSDT.Commands
{
    public class BaseCollection<T>
    {
        public class ChangedArgs : EventArgs
        {
            public ChangedArgs() { }
            public ChangedArgs(T item, EnumChangeType type, object value)
            {
                this.Item = item;
                this.ChangeType = type;
                this.Value = value;
            }
            public T Item;
            public EnumChangeType ChangeType;

            public object Value;
        }
        public delegate void CollectionChangedEvent(object sender, ChangedArgs e);
        public event CollectionChangedEvent ChangedEvent;

        private List<T> _commandMenuList = new List<T>();
        public T Add(T item)
        {
            int tmpIndex = _commandMenuList.IndexOf(item);
            if (tmpIndex < 0)
            {
                _commandMenuList.Add(item);
                ChangedArgs args = new BaseCollection<T>.ChangedArgs(item, EnumChangeType.Add, null);
                DoChangedEvent(args);


                return item;
            }

            return _commandMenuList[tmpIndex];
        }
        public virtual void DoChangedEvent(ChangedArgs args)
        {
            if (ChangedEvent != null)
            {
                ChangedEvent(this, args);
            }
        }
        public bool Contains(T item)
        {
            return _commandMenuList.Contains(item);
        }

        public int Count
        {
            get { return _commandMenuList.Count; }
        }
        public bool Remove(T item)
        {
            bool blRemove = false;
            if (this._commandMenuList.Contains(item))
            {
                blRemove = _commandMenuList.Remove(item);
                ChangedArgs args = new BaseCollection<T>.ChangedArgs(item, EnumChangeType.Remove, null);
                DoChangedEvent(args);
            }
            return blRemove;

        }
        public bool RemoveAt(int index)
        {
            if (index < 0 || index > _commandMenuList.Count)
            {
                return false;
            }
            ChangedArgs args = new BaseCollection<T>.ChangedArgs(_commandMenuList[index], EnumChangeType.Remove, null);
            DoChangedEvent(args);
            _commandMenuList.RemoveAt(index);
            return true;
        }
        public void Clear()
        {
            _commandMenuList.Clear();
        }
        public List<T> GetAllItems()
        {
            return _commandMenuList;
        }
    }
}
