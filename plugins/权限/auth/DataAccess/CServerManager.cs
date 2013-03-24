using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace auth.Services
{
    public class CServerManager
    {
        private static System.Threading.Mutex _instanceMutex = new System.Threading.Mutex();

        private static CServerManager _instance;

        public static CServerManager Instance
        {
            get
            {
                try
                {
                    _instanceMutex.WaitOne();
                    if (_instance == null)
                    {
                        _instance = new CServerManager();
                    }
                    return _instance;
                }
                finally
                {
                    _instanceMutex.ReleaseMutex();
                }
            }
        }


        public DataSet CallService(string service_name, DataSet i_blks)
        {
            DataSet ds = new DataSet();
            switch (service_name)
            {
                case "epestree_inqa":
                  //  ds=  DbTreeInfo.f_epestree_inqa(i_blks, CConstString.ConnectName); 
                    break;
            }
            return ds;
        }

        public int CallServiceAsync(string service_name, DataSet i_blks)
        {
            return 0;
        }
    }
}
