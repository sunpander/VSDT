using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VSDT.Commands
{
    public abstract class BaseCommand
    {
        public BaseCommand() { }
        public BaseCommand(Guid guid, int commamdID)
        {
            this.CommandID = new CommandID(guid, commamdID);
        }
        public BaseCommand(CommandID commamdID)
        {
            this.CommandID = commamdID;
        }

        private CommandID _commandID;
        public CommandID CommandID
        {
            get { return _commandID; }
            set
            {
                if (TagEx != null) //_commandID != null&& 
                {
                    throw new Exception("Menu已注册,CommandID不能修改");
                }
                _commandID = value;
            }
        }

        public string Name
        {
            get;
            set;
        }
        private bool enable = true;
        public bool Enabled
        {
            get { return enable; }
            set { enable = value; }
        }
        public Shortcut Shortcut
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }
        public abstract void Execute();
        public abstract void BeforeQueryStatus();
         
        internal object TagEx;
        //internal event EventHandler ItemChanged;

        //protected virtual void RaiseItemChanged()
        //{
        //    if (ItemChanged != null)
        //    {
        //        ItemChanged(this, EventArgs.Empty);
        //    }
        //}
    }
}
