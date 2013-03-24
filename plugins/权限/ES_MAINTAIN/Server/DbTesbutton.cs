using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace EP.Server
{
    public  class DbTesbutton
    {
        string strSQL = "";
        public DataSet Query(DataSet dsIn, string conn)
        {
            CDbCommand tree_inqa = new CDbCommand(conn);

            tree_inqa.SetCommandText(strSQL);
             
            DataTable dt = new DataTable();
            tree_inqa.ExecuteQuery(ref dt);

            DataSet bcls_ret = new DataSet();
            bcls_ret.Tables.Add("MENUTREE");
            bcls_ret.Tables[0].Merge(dt);
            return bcls_ret;
        }
        public int QueryCount(DataSet dsIn, string conn)
        {
            return 0;
        }
        public int Delete(DataSet dsIn, string conn)
        {
            return 0;
        }
        public int Update(DataSet dsIn, string conn)
        {
            return 0;
        }
        /// <summary>
        /// return table Name
        /// </summary>
        /// <returns></returns>
        public virtual string GetTableName()
        {
            throw new NotImplementedException("GetTableName");
        }
        /// <summary>
        /// return columns
        /// </summary>
        /// <returns></returns>
        public virtual string GetColumns()
        {
            throw new NotImplementedException("GetTableName");
        }
        public virtual string GetConnectionName()
        {
            throw new NotImplementedException("GetTableName");
        }
    }
}
