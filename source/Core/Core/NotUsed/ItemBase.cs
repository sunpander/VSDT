using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VSDT.NotUsed
{
    public abstract class ItemBase: IDisposable  
    {
        public string Name;
        public string Caption;
        public bool Auth = true;

        private CommandStatus status = CommandStatus.Enabled;
        public CommandStatus Status
        {
            get { return status; }
            set
            {
                CommandStatus oldValue = this.status;
                this.status = value;
                if (oldValue != value)
                {
                    OnChanged();
                }
            }
        }

        /// <summary>
        /// This event signals that some of the command's properties have changed.
        /// </summary>
        public event EventHandler Changed;

        /// <summary>
        /// This event signals that this <see cref="Command"/> is executed. Handle this event to implement the
        /// actions to be executed when the command is fired.
        /// </summary>
        public event EventHandler ExecuteAction;


        public ItemBase()
        {
            Name = "Default";
        }
        public ItemBase(string name)
        {
            Name = name;
        }

        public virtual void  Execute()
        {
            if (status == CommandStatus.Enabled)
            {
                OnExecuteAction(this, EventArgs.Empty);
            }
        }
        protected virtual void OnExecuteAction(object sender, EventArgs e)
        {
            if (Status == CommandStatus.Enabled && ExecuteAction != null)
            {
                ExecuteAction(this, e);
            }
        }
        protected virtual void OnChanged()
        {
            if (Changed != null)
            {
                Changed(this, EventArgs.Empty);
            }
        }

        #region IDisposable 成员

        public void Dispose()
        {
           
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
