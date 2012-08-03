using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VSDT.Commands
{
    internal class CommandMenuCollection : BaseCollection<VSMenuCommand>
    {
        private PlaceMenuCollection placeMenuCollection;
        //private static bool isSorted = false;
        public PlaceMenuCollection SortedMenuCommand
        {
            get
            {
                //if (isSorted)
                //{
                //    return placeMenuCollection;
                // }
                placeMenuCollection = new PlaceMenuCollection();
                //isSorted = true;
                List<VSMenuCommand> _list = this.GetAllItems();
                int count = _list.Count;

                Type enmuType = typeof(VSDT.MenuCommandPlace);
                string[] enumNames = Enum.GetNames(enmuType);
                for (int i = 0; i < count; i++)
                {
                    //循环遍历
                    foreach (string name in enumNames)
                    {
                        VSDT.MenuCommandPlace tmpPlace = (VSDT.MenuCommandPlace)Enum.Parse(enmuType, name);

                        if ((_list[i].Position & tmpPlace) == tmpPlace)
                        {
                            if (!placeMenuCollection.ContainsKey(tmpPlace))
                            {
                                placeMenuCollection.Add(tmpPlace, new List<VSMenuCommand>());
                            }
                            placeMenuCollection[tmpPlace].Add(_list[i]);
                        }
                    }
                }
                return placeMenuCollection;
            }
        }

        public List<VSMenuCommand> GetItemsByPlace(MenuCommandPlace place)
        {
            if (SortedMenuCommand.ContainsKey(place))
            {
                return SortedMenuCommand[place];
            }
            return null;
        }
        public VSMenuCommand FindCommand(CommandID cmdID)
        {
            List<VSMenuCommand> list = this.GetAllItems();
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].CommandID.Equals(cmdID))
                {
                    return list[i];
                }
            }
            return null;
        }
    }
}
