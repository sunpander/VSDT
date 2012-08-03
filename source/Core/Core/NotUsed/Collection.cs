using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.ComponentModel.Design.Serialization;
using System.ComponentModel;

namespace VSDT.NotUsed
{
    public interface ICollectionItem
    {
        Collection Collection { get; }
        string ItemName { get; }
        void SetCollection(Collection newCollection);
        event EventHandler ItemChanged;
    }
    public class CollectionItemEventArgs : EventArgs
    {
        object item;
        public CollectionItemEventArgs(object item)
        {
            this.item = item;
        }
        public object Item { get { return item; } }
    }
    public delegate void CollectionItemEventHandler(object sender, CollectionItemEventArgs e);
    public class Collection : CollectionBase
    {
        int lockUpdate = 0;
        public event CollectionChangeEventHandler CollectionChanged;
        public event CollectionItemEventHandler CollectionItemChanged;
        protected virtual ICollectionItem CreateItem()
        {
            return null;
        }
        protected virtual ICollectionItem Add()
        {
            ICollectionItem item = CreateItem();
            return item;
        }
        protected ICollectionItem this[string name]
        {
            get
            {
                for (int n = Count - 1; n >= 0; n--)
                {
                    ICollectionItem item = List[n] as ICollectionItem;
                    if (item.ItemName == name) return item;
                }
                return null;
            }
        }
        public virtual void Add(ICollectionItem item)
        {
            AddItem(item);
        }
        public virtual object Insert(int index, ICollectionItem item)
        {
            List.Insert(index, item);
            return item;
        }
        public virtual int IndexOf(ICollectionItem item)
        {
            return List.IndexOf(item);
        }
        protected virtual bool CheckIndex(int index)
        {
            if (index < 0 || index >= Count) return false;
            return true;
        }
        public virtual void Move(int fromIndex, int toIndex)
        {
            if (fromIndex == toIndex || !CheckIndex(fromIndex) || !CheckIndex(toIndex)) return;
            object oldItem = InnerList[fromIndex];
            InnerList.RemoveAt(fromIndex);
            if (toIndex >= InnerList.Count)
                InnerList.Add(oldItem);
            else
                InnerList.Insert(toIndex, oldItem);
            RaiseOnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Refresh, null));
        }
        internal ICollectionItem AddItem(ICollectionItem item)
        {
            if (List.Contains(item) && item.Collection == this) return item;
            List.Add(item);
            return item;
        }
        internal void RemoveItem(ICollectionItem item)
        {
            List.Remove(item);
        }
        public virtual void Remove(ICollectionItem item)
        {
            RemoveItem(item);
        }
        protected virtual void BeginUpdate()
        {
            lockUpdate++;
        }
        protected virtual void EndUpdate()
        {
            if (--lockUpdate == 0)
                RaiseOnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Refresh, null));
        }
        protected override void OnInsert(int index, object item)
        {
            base.OnInsert(index, item);
            ICollectionItem colItem = item as ICollectionItem;
            if (colItem == null) throw new ArgumentException("invalid collectionItem");
            if (colItem.Collection != null)
            {
                if (colItem.Collection == this && !List.Contains(item)) return;
                throw new ArgumentException("already in collection");
            }
        }
        protected override void OnInsertComplete(int index, object item)
        {
            base.OnInsertComplete(index, item);
            ICollectionItem colItem = item as ICollectionItem;
            colItem.SetCollection(this);
            colItem.ItemChanged += new EventHandler(Collection_CollectionItemChanged);
            RaiseOnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Add, item));
        }
        protected override void OnRemoveComplete(int index, object item)
        {
            base.OnRemoveComplete(index, item);
            ICollectionItem colItem = item as ICollectionItem;
            colItem.SetCollection(null);
            colItem.ItemChanged -= new EventHandler(Collection_CollectionItemChanged);
            RaiseOnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Remove, item));
        }
        protected override void OnClear()
        {
            int count = Count;
            if (count == 0) return;
            try
            {
                for (int n = count - 1; n >= 0; n--)
                {
                    RemoveAt(n);
                }
            }
            finally
            {
            }
        }
        void Collection_CollectionItemChanged(object sender, EventArgs e)
        {
            OnCollectionItemChanged(sender);
        }
        protected virtual void OnCollectionItemChanged(object item)
        {
            if (lockUpdate != 0) return;
            if (CollectionItemChanged != null)
                CollectionItemChanged(this, new CollectionItemEventArgs(item));
        }
        protected virtual void RaiseOnCollectionChanged(CollectionChangeEventArgs e)
        {
            if (lockUpdate != 0) return;
            if (CollectionChanged != null)
                CollectionChanged(this, e);
        }
    }

    public class NavItemCollection : Collection 
    {
        //NavBarControl navBar;
        //public NavItemCollection(NavBarControl navBar)
        //{
        //    this.navBar = navBar;
        //}
        //protected NavBarControl NavBar { get { return navBar; } }
        public NavBarItem Add(NavBarItem item)
        {
            //if (item.NavBar == null) item.SetNavBarCore(NavBar);
            return AddItem(item) as NavBarItem;
        }
        [Description("Gets an item of the collection by its index.")]
        public virtual NavBarItem this[int index]
        {
            get { return List[index] as NavBarItem; }
        }
        [Description("Gets an item of the collection by its index.")]
        public new virtual NavBarItem this[string name]
        {
            get { return base[name] as NavBarItem; }
        }
        public new virtual NavBarItem Add()
        {
            return AddItem(CreateItem()) as NavBarItem;
        }
        public virtual void AddRange(NavBarItem[] items)
        {
            foreach (NavBarItem item in items) AddItem(item);
        }
        protected override ICollectionItem CreateItem()
        {
            return new NavBarItem();
        }
    }
    public class NavBarItem : NavElement
    {
     
      
        bool enabled, canDrag;
        string styleDisabledName;
        public NavBarItem(string caption)
            : this()
        {
            Caption = caption;
        }
        public NavBarItem()
        {
            this.canDrag = true;
            this.enabled = true;
            this.styleDisabledName = string.Empty;
       
         }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
               
                
            }
            base.Dispose(disposing);
        }
   
        [Browsable(false)]
        public NavItemCollection Collection { get { return ((ICollectionItem)this).Collection as NavItemCollection; } }
    
        public virtual string StyleDisabledName
        {
            get { return styleDisabledName; }
            set
            {
                if (value == null) value = string.Empty;
                if (StyleDisabledName == value) return;
                styleDisabledName = value;
                RaiseItemChanged();
            }
        }
 
        public virtual bool Enabled
        {
            get { return enabled; }
            set
            {
                if (Enabled == value) return;
                enabled = value;
                RaiseItemChanged();
            }
        }
        
        public virtual bool CanDrag
        {
            get { return canDrag; }
            set
            {
                if (CanDrag == value) return;
                canDrag = value;
            }
        }
   
 
    }

    public class NavElement : ComponentCollectionItem 
    {
        string caption;
         
        int smallImageIndex, largeImageIndex;
      //Image smallImage, largeImage;
        string hint;
        
        bool visible;
        object tag;
        public NavElement()
        {
            tag = null;
            hint = string.Empty;
            visible = true;
        
            caption = DefaultCaption;
            largeImageIndex = smallImageIndex = -1;
            //smallImage = largeImage = null;
         }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //navBar = null;
                //DestroyAppearances();
            }
            base.Dispose(disposing);
        }
   
        protected void FireChanged()
        {
            if (!DesignMode || IsLoading) return;
            System.ComponentModel.Design.IComponentChangeService srv = GetService(typeof(System.ComponentModel.Design.IComponentChangeService)) as System.ComponentModel.Design.IComponentChangeService;
            if (srv == null) return;
            srv.OnComponentChanged(this, null, null, null);
        }
        protected override void RaiseItemChanged()
        {
            FireChanged();
            base.RaiseItemChanged();
        }
        protected internal bool IsLoading
        {
            get { return true; }
        }
       
        public override string ToString() { return string.Format("{0} {1}", Name, Caption); }
        
   
      
        [Description("Gets or sets the element's hint text."), DefaultValue(""), Category("Appearance"), Localizable(true)]
        public virtual string Hint
        {
            get { return hint; }
            set
            {
                if (value == null) value = string.Empty;
                hint = value;
            }
        }
        protected virtual void OnVisibleChanged() { }
       public bool Visible
        {
            get { return visible; }
            set
            {
                if (Visible == value) return;
                visible = value;
                RaiseItemChanged();
                OnVisibleChanged();
            }
        }
    
        public object Tag
        {
            get { return tag; }
            set
            {
                tag = value;
            }
        }
   
    
        protected internal virtual string DefaultCaption { get { return "Element"; } }
        internal bool ShouldSerializeCaption() { return DefaultCaption != Caption; }
         public string Caption
        {
            get { return caption; }
            set
            {
                if (Caption == value) return;
                caption = value;
                RaiseItemChanged();
            }
        }
    }

    public class ComponentCollectionItem : Component, ICollectionItem
    {
        static readonly object itemChangedEvent = new object();
        string name;
        Collection collection;
        public ComponentCollectionItem()
        {
            collection = null;
            name = "";
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Collection oldCollection = collection;
                collection = null;
                if (oldCollection != null) oldCollection.Remove(this);
            }
            base.Dispose(disposing);
        }
 
        public string Name
        {
            get
            {
                if (Site != null) return Site.Name;
                return name;
            }
            set
            {
                if (value == null) return;
                name = value;
            }
        }
        string ICollectionItem.ItemName { get { return Name; } }
        Collection ICollectionItem.Collection { get { return collection; } }
        void ICollectionItem.SetCollection(Collection newCollection)
        {
            collection = newCollection;
        }
        protected virtual void RaiseItemChanged()
        {
            EventHandler handler = this.Events[itemChangedEvent] as EventHandler;
            if (handler != null) handler(this, EventArgs.Empty);
        }
        [Description("Fires when item property values are changed.")]
        public event EventHandler ItemChanged
        {
            add { Events.AddHandler(itemChangedEvent, value); }
            remove { Events.RemoveHandler(itemChangedEvent, value); }
        }
    }
}
