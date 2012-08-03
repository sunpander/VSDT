using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VSDT.NotUsed
{
    public class CommandItem:ItemBase
    {
        public new  event  EventHandler ExecuteAction;

        public CommandItem():base()
        {
             
        }
        public CommandItem(string name)
        {
            this.Name = name;
        }
      
        public bool BeginGroup = false;
        public bool HasChild
        {
            get
            {
                if (_children == null)
                    return false;
                if (_children.Count == 0)
                    return false;
                return true;
            }
        }
        private List<CommandItem> _children;
        public List<CommandItem> Children
        {
            get
            {
                if (_children == null)
                {
                    _children = new List<CommandItem>();
                }
                return _children;
            }
            set
            {
                _children = value;
            }
        }

        protected override void  OnExecuteAction(object sender, EventArgs e)
        {
            if (ExecuteAction != null)
            {
                ExecuteAction(this, EventArgs.Empty);
            }
        }
    }

    public delegate void ProjectCommandClickEvent(object sender,  EventArgs e);
    public class ContextEventArgs : EventArgs
    {
        //EnvDTE90.DTE
    }
    public class ProjectCommandItem : CommandItem
    {
        public event ProjectCommandClickEvent test;
        protected override void OnExecuteAction(object sender, EventArgs e)
        {
            //base.OnExecuteAction(sender, e);
        }
    }


}
