using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VSDT.Commands
{
    public class CommandID
    {
        public CommandID(Guid guid, int intID)
        {
            GuidID = guid;
            IntID = intID;
        }

        public int IntID
        {
            get;
            private set;
        }

        public Guid GuidID
        {
            get;
            private set;
        }
        public override bool Equals(object obj)
        {
            if (obj != null && obj is CommandID)
            {
                CommandID cmd = obj as CommandID;
                if (cmd.GuidID == this.GuidID && cmd.IntID == this.IntID)
                    return true;
            }
            return false;
        }
        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }
        public override string ToString()
        {
            return GuidID.ToString() + IntID;
        }
    }
}
