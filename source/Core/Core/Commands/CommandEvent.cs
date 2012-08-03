using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VSDT.Commands
{
    public delegate void CommandChangedEvent(object sender, CommandChangedArgs e);

    public class CommandChangedArgs : EventArgs
    {
        public CommandChangedArgs(EnumChangeType type, object value)
        {
            this.ChangeType = type;
            this.Value = value;
        }
        public EnumChangeType ChangeType;
        public enum EnumChangeType
        {
            Add,
            Remove
        }
        public object Value;
    }

    public class MenuChangedArgs : BaseCollection<VSMenuCommand>.ChangedArgs
    {

        public MenuChangedArgs(VSMenuCommand item, EnumChangeType type, object value)
        {
            this.Item = item;
            this.ChangeType = type;
            this.Value = value;
        }
    }
    public delegate void MenuChangedEvent(object sender, MenuChangedArgs e);
}
