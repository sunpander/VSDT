using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using System.Data.Common;

namespace auth.Services
{
    interface IDbCommand
    {
        void SetCommandText(string cmdText);
        int ExecuteNonQuery();
        int ExecuteQuery(ref DataTable table, int startRecord, int maxRecords);
        int ExecuteQuery(ref DataTable table);
        int ExecuteReader();
        object ExecuteScalar();

        void Close();
        bool Read();
        string GetString(int iColumn);
    }
    public class CDataParameterCollection
    {
        private Dictionary<string, object> parameters = new Dictionary<string, object>();
        public void Set(string name, object value)
        {
            name = name.Trim();
            if (string.IsNullOrEmpty(name))
            {
                return;
            }
            if (name.StartsWith("@") || name.StartsWith(":"))
            {
                name = name.Substring(1);
            }
            if (parameters.ContainsKey(name))
            {
                parameters[name] = value;
            }
            else
            {
                parameters.Add(name, value);
            }
        }

        public int Count
        {
            get
            {
                return parameters.Count;
            }
        }
        public Dictionary<string, object> Key_Value
        {
            get
            {
                return parameters;
            }
        }

    }

    public class CDbCommand : IDbCommand
    {
        public CDataParameterCollection Parameters = new CDataParameterCollection();

        protected string CommandText = "";
        protected Database db = null;
        string ConnectString = "";
        public CDbCommand(string conn)
        {
            this.ConnectString = conn;
            CreateDb();
        }

        public CDbCommand(string cmdText, string conn)
        {
            this.CommandText = cmdText;
            this.ConnectString = conn;
            CreateDb();
        }
        protected void CreateDb()
        {
            if (db == null)
                db = EnterpriseLibraryContainer.Current.GetInstance<Database>(ConnectString);
        }
        private DbCommand BuildCommand(DbConnection dbConnection, Dictionary<string, object> parameters, CommandType type)
        {
            DbCommand cmd = dbConnection.CreateCommand();
            cmd.CommandText = CommandText;
            cmd.CommandType = type;
            if (Parameters.Count > 0)
            {
                foreach (var item in Parameters.Key_Value)
                {
                    db.AddInParameter(cmd, "@" + item.Key, DbType.String, item.Value);
                }
            }
            return cmd;
        }
        #region IDBCommand 成员
        public void SetCommandText(string cmdText)
        {
            this.CommandText = cmdText;
        }

        public int ExecuteNonQuery()
        {
            using (DbConnection dbConnection = this.db.CreateConnection())
            {
                DbCommand cmd = BuildCommand(dbConnection, Parameters.Key_Value, CommandType.Text);

                return this.db.ExecuteNonQuery(cmd);
            }
        }

        public int ExecuteQuery(ref DataTable table, int startRecord, int maxRecords)
        {
            if (maxRecords < 0)
            {
                return ExecuteQuery(ref table);
            }
            else
            {
                DataSet ds = new DataSet();
                using (DbConnection dbConnection = this.db.CreateConnection())
                {
                    DbCommand dbCommand = BuildCommand(dbConnection, Parameters.Key_Value, CommandType.Text);

                    DbDataAdapter dbDataAdapter = this.db.GetDataAdapter();
                    dbCommand.Connection = dbConnection;
                    dbDataAdapter.SelectCommand = dbCommand;
                    dbConnection.Open();
                    dbDataAdapter.Fill(ds, startRecord, maxRecords, "ListTable");
                    dbConnection.Close();
                }
                table = ds.Tables[0];
                return table.Rows.Count;
            }

        }

        public int ExecuteQuery(ref DataTable table)
        {
            if (table == null)
                table = new DataTable();
            using (DbConnection dbConnection = this.db.CreateConnection())
            {
                DbCommand cmd = BuildCommand(dbConnection, Parameters.Key_Value, CommandType.Text);

                DataSet ds = new DataSet();
                ds = this.db.ExecuteDataSet(cmd);

                table = ds.Tables[0];
            }
            return table.Rows.Count;
        }

        public object ExecuteScalar()
        {
            using (DbConnection dbConnection = this.db.CreateConnection())
            {
                DbCommand cmd = BuildCommand(dbConnection, Parameters.Key_Value, CommandType.Text);

                object obj = db.ExecuteScalar(cmd);

                return obj;
            }
        }

        public void Close()
        {
            throw new NotImplementedException();
        }

        public int ExecuteReader()
        {
            throw new NotImplementedException();
        }

        public bool Read()
        {
            throw new NotImplementedException();
        }

        public string GetString(int iColumn)
        {
            throw new NotImplementedException();
        }

        #endregion
    }

}
