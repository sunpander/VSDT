using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VSDT.Commands
{
    public class VSMenuCommand : BaseCommand
    {
        public event EventHandler QueryStatus;

        public event EventHandler Click;


       // public MenuCommandPlace MenuPlace;
        public string Caption
        {
            get;

            set;
        }

        public string Description
        {
            get;
            set;
        }

        public MenuCommandPlace Position
        {
            get;
            set;
        }

        public int Status
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }
        private bool visible = true;
        public bool Visible
        {
            get { return visible; }

            set { visible = value; }
        }

        public object Tag
        {
            get;
            set;
        }

        public VSMenuCommand Copy()
        {
            VSMenuCommand cmdMenu = new VSMenuCommand();
            cmdMenu.Caption = this.Caption;
            cmdMenu.CommandID = this.CommandID;
            cmdMenu.Description = this.Description;
            cmdMenu.Enabled = this.Enabled;
            cmdMenu.Name = this.Name;
            cmdMenu.Position = this.Position;
            return cmdMenu;
        }

        public override void Execute()
        {
            if (Click != null)
            {
                this.Click(this, EventArgs.Empty);
            }
        }

        public override bool Equals(object obj)
        {
            VSMenuCommand vsTmp = obj as VSMenuCommand;
            if (vsTmp == null)
                return false;
            if (vsTmp.CommandID == null)
            {
                if (this.CommandID == null)
                    return true;
                else
                    return false;
            }
            return vsTmp.CommandID.Equals(this.CommandID);
        }
        public override int GetHashCode()
        {
            return this.CommandID.GetHashCode();
        }



        public override void BeforeQueryStatus()
        {
            if (QueryStatus != null)
            {
                QueryStatus(this, EventArgs.Empty);
            }
        }
    }
}
