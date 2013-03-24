using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DataAccess
{
    public class Tuxedo
    {
        public static DataSet CallService(string str)
        {
            DataSet ds = new DataSet();
            return ds;
        }
        public static DataSet CallService(ServiceName str)
        {
            DataSet ds = new DataSet();
            return ds;
        }
        public enum ServiceName
        {
            
        }
    }
}
