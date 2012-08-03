using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VSDT
{
    public class Setting
    {
        public int Name
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public int Value
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }
    }

    public class Settings
    {
        public int Name
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public List<Category> Categorys
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }
    }

    public class Section
    {
        public int Name
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public List<Setting> Settings
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }
    }

    public class Language
    {
        public int Name
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public List<Section> Sections
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }
    }

    public class Page
    {
        public int Name
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public List<Language> Languages
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }
    }

    public interface ISettingNode
    {
    }

    public class Category
    {
        public int Name
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public List<Page> Pages
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }
    }

    public class SettingsManager
    {
    }
}
