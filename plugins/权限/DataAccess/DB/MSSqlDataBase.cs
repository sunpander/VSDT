using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Data.SqlClient;

using System.Collections.Generic;
using System.Data.Common;
using System.Data.OleDb;

namespace DataAccess
{
    ///// <summary>
    ///// SqlServer的数据库操作类(没用)
    ///// </summary>
    //public class MSSqlDataBase : InterfaceDataBase 
    //{
    //    private SqlConnection _connection;
    //    public MSSqlDataBase(string strConnectioin)
    //    {
    //        _connection = new SqlConnection(strConnectioin);
    //    }
    //    /// <summary>
    //    ///构造一个已含有SqlParameter对象的SqlCommand
    //    /// </summary>
    //    /// <paramKey colName="storedProcNameOrSqlString">存储过程名或带参数的SQL查询字符串</paramKey>
    //    /// <paramKey colName="parameters">Array of IDataParameter objects</paramKey>
    //    /// <paramKey colName="type">是存储过程还是查询字串</paramKey>
    //    /// <returns>返回SqlCommand对象，已装入参数</returns>
    //    /// 
    //    private SqlCommand BuildQueryCommand(string storedProcNameOrSqlString, Dictionary<string, object> parameters, CommandType type)
    //    {
    //        SqlCommand command = new SqlCommand(storedProcNameOrSqlString, _connection);
    //        command.CommandType = type;
    //        if (parameters != null && parameters.Count != 0)
    //        {
    //            foreach (KeyValuePair<string, object> kvp in parameters)
    //            {
    //                SqlParameter para = new SqlParameter(kvp.Key, kvp.Value);
    //                command.Parameters.Add(para);
    //            }    
    //        }
    //        return command;
    //    }
 
    //    #region InterfaceDataBase 成员
    //    /// <summary>
    //    /// 通过sql语句返回DataTable
    //    /// </summary>
    //    /// <paramKey colName="strSqlDel"></paramKey>
    //    /// <returns></returns>
    //    DataTable InterfaceDataBase.RunSqlQuery(string strSql)
    //    {
    //        SqlDataAdapter adapter = new SqlDataAdapter(strSql, _connection);
    //        DataTable dataTable = new DataTable();
    //        try
    //        {
    //            _connection.Open();
    //            adapter.Fill(dataTable);
    //        }
    //        finally
    //        {
    //            _connection.Close();
    //        }
    //        return dataTable;
    //    }

    //    DataTable InterfaceDataBase.RunSqlQuery(string strSql, Dictionary<string, object> parameters)
    //    {
    //        DataTable dataTable = new DataTable();
    //        try
    //        {
    //            _connection.Open();
    //            SqlDataAdapter sqlDA = new SqlDataAdapter();
    //            sqlDA.SelectCommand = BuildQueryCommand(strSql, parameters, CommandType.Text);
    //            sqlDA.Fill(dataTable);
    //        }
    //        finally
    //        {
    //            _connection.Close();
    //        }
    //        return dataTable;
    //    }

    //    int InterfaceDataBase.RunSqlNoneQuery(string strSql)
    //    {
    //        int result;
    //        try
    //        {
    //            _connection.Open();
    //            SqlCommand command = new SqlCommand(strSql, _connection);
    //            result = command.ExecuteNonQuery();
    //        }
    //        finally
    //        {
    //            _connection.Close();
    //        }
    //        return result;
    //    }

    //    int InterfaceDataBase.RunSqlNoneQuery(string strSql, Dictionary<string, object> parameters)
    //    {
    //        int result;
    //        try
    //        {
    //            _connection.Open();
    //            SqlCommand command = BuildQueryCommand(strSql, parameters, CommandType.Text);
    //            result = command.ExecuteNonQuery(); 
    //        }
    //        finally
    //        {
    //            _connection.Close();
    //        }
    //        return result;
    //    }


    //    int InterfaceDataBase.RunSqlNoneQuery(string strSql, DbTransaction trans)
    //    {
    //        int result;
    //        try
    //        {
    //            SqlTransaction sqlTrans = (SqlTransaction)trans;
    //            SqlCommand command = new SqlCommand(strSql, sqlTrans.Connection);
    //            command.Transaction = sqlTrans;
    //            result = command.ExecuteNonQuery();
    //        }
    //        finally
    //        { 
    //        }
    //        return result;
    //    }

    //    int InterfaceDataBase.RunSqlNoneQuery(string strSql, Dictionary<string, object> parameters, DbTransaction trans)
    //    {
    //        int result;
    //        try
    //        {
      
    //            SqlCommand command = BuildQueryCommand(strSql, parameters, CommandType.Text);
    //            command.Transaction = (SqlTransaction)trans;
    //            command.Connection = ((SqlTransaction)trans).Connection;
    //            result = command.ExecuteNonQuery();
    //        }
    //        finally
    //        {
    //            _connection.Close();
    //        }
    //        return result;
    //    }
    //    #endregion


    //    #region IDisposable 成员

    //    void IDisposable.Dispose()
    //    {
    //        //释放托管资源
    //        if (_connection != null)
    //        {
    //            _connection.Dispose();
    //        }
    //    }

    //    #endregion


    //    #region InterfaceDataBase 成员


    //    DbTransaction InterfaceDataBase.GetTransction()
    //    {
    //        if (_connection != null)
    //        {
    //            if (_connection.State == ConnectionState.Closed)
    //            {
    //                _connection.Open();
    //            }
    //            return (DbTransaction)_connection.BeginTransaction();
    //        }
    //        return null;
    //    }

    //    #endregion

    //    #region InterfaceDataBase 成员


    //    DataTable InterfaceDataBase.RunSqlQuery(string strSql, DbTransaction trans)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    DataTable InterfaceDataBase.RunSqlQuery(string strSql, Dictionary<string, object> parameters, DbTransaction trans)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    #endregion
    //}
    ///// <summary>
    ///// Orcale的数据库操作类 (没用)
    ///// </summary>
    //public class OracleDataBase : InterfaceDataBase
    //{
    //    private OracleConnection   _connection;
    //    public OracleDataBase(string strConnectioin)
    //    {
    //        _connection = new OracleConnection(strConnectioin);
    //    }
    //    /// <summary>
    //    ///构造一个已含有OracleCommand对象的OracleCommand
    //    /// </summary>
    //    /// <paramKey colName="storedProcNameOrSqlString">存储过程名或带参数的SQL查询字符串</paramKey>
    //    /// <paramKey colName="parameters">Array of IDataParameter objects</paramKey>
    //    /// <paramKey colName="type">是存储过程还是查询字串</paramKey>
    //    /// <returns>返回SqlCommand对象，已装入参数</returns>
    //    /// 
    //    private OracleCommand BuildQueryCommand(string storedProcNameOrSqlString, Dictionary<string, object> parameters, CommandType type)
    //    {
    //        storedProcNameOrSqlString = convertSql(storedProcNameOrSqlString);
    //        System.Data.OracleClient.OracleCommand command = new OracleCommand(storedProcNameOrSqlString,_connection);
    //        command.CommandType = type;

    //        if (parameters != null && parameters.Count != 0)
    //        {
    //            foreach (KeyValuePair<string, object> kvp in parameters)
    //            {
    //                OracleParameter para = new OracleParameter(kvp.Key, kvp.Value);
    //                command.Parameters.Add(para);
    //            }
    //        }
    //        return command;
    //    }

    //    #region InterfaceDataBase 成员
    //    /// <summary>
    //    /// 通过sql语句返回DataTable
    //    /// </summary>
    //    /// <paramKey colName="strSqlDel"></paramKey>
    //    /// <returns></returns>
    //    DataTable InterfaceDataBase.RunSqlQuery(string strSql)
    //    {
    //        OracleDataAdapter adapter = new OracleDataAdapter(convertSql(strSql), _connection);
    //        DataTable dataTable = new DataTable();
    //        try
    //        {
    //            if (_connection.State != ConnectionState.Open)
    //            {
    //                _connection.Open();
    //            }
    //            adapter.Fill(dataTable);
    //        }
    //        finally
    //        {
    //           // _connection.Close();
    //        }
    //        return dataTable;
    //    }

    //    DataTable InterfaceDataBase.RunSqlQuery(string strSql, Dictionary<string, object> parameters)
    //    {
    //        DataTable dataTable = new DataTable();
    //        try
    //        {
    //            if (_connection.State != ConnectionState.Open)
    //            {
    //                _connection.Open();
    //            }
    //            OracleDataAdapter adapter = new OracleDataAdapter(convertSql(strSql), _connection);
    //            adapter.SelectCommand = BuildQueryCommand(strSql, parameters, CommandType.Text);
    //            adapter.Fill(dataTable);
    //        }
    //        finally
    //        {
               
    //        }
    //        return dataTable;
    //    }

    //    int InterfaceDataBase.RunSqlNoneQuery(string strSql)
    //    {
    //        int result;
    //        try
    //        {
    //            _connection.Open();
    //            OracleCommand command = new OracleCommand(convertSql(strSql), _connection);
    //            result = command.ExecuteNonQuery();
    //        }
    //        finally
    //        {
    //            _connection.Close();
    //        }
    //        return result;
    //    }

 


    //    int InterfaceDataBase.RunSqlNoneQuery(string strSql, Dictionary<string, object> parameters)
    //    {
    //        int result;
    //        try
    //        {
    //            if (_connection.State != ConnectionState.Open)
    //            {
    //                _connection.Open();
    //            }
    //            OracleCommand command = BuildQueryCommand(convertSql(strSql), parameters, CommandType.Text);
    //            result = command.ExecuteNonQuery();
    //        }
    //        finally
    //        {
    //            //_connection.Close();
    //        }
    //        return result;
    //    }
    //    int InterfaceDataBase.RunSqlNoneQuery(string strSql, DbTransaction trans)
    //    {
    //        int result;
    //        try
    //        {
    //            OracleTransaction oraTrans = (OracleTransaction)trans;
    //            OracleCommand command = new OracleCommand(convertSql(strSql),oraTrans.Connection,oraTrans);
    //            result = command.ExecuteNonQuery();
    //        }
    //        finally
    //        {
 
    //        }
    //        return result;
    //    }

    //    int InterfaceDataBase.RunSqlNoneQuery(string strSql, Dictionary<string, object> parameters, DbTransaction trans)
    //    {
    //        int result;
    //        try
    //        {
    //            OracleTransaction oraTrans = (OracleTransaction)trans;
    //            OracleCommand command = BuildQueryCommand(convertSql(strSql), parameters, CommandType.Text);
    //            command.Transaction = oraTrans;
    //            command.Connection = oraTrans.Connection;
                
    //            result = command.ExecuteNonQuery();
    //        }
    //        finally
    //        {
    //        }
    //        return result;
    //    }
    //    #endregion
    //    private string convertSql(string sql)
    //    {
    //        return sql.Replace("@", ":");
    //    }

    //    #region IDisposable 成员

    //    void IDisposable.Dispose()
    //    {
    //        //释放托管资源
    //        if (_connection != null)
    //        {
    //            _connection.Dispose();
    //        }
    //    }

    //    #endregion


    //    #region InterfaceDataBase 成员


    //    DbTransaction InterfaceDataBase.GetTransction()
    //    {
    //        //throw new NotImplementedException();
    //        if (_connection != null)
    //        {
    //            if (_connection.State == ConnectionState.Closed)
    //            {
    //                _connection.Open();
    //            }
    //            return (DbTransaction)_connection.BeginTransaction();
    //        }
    //        return null;
    //    }

    //    #endregion

    //    #region InterfaceDataBase 成员
    //    DataTable InterfaceDataBase.RunSqlQuery(string strSql, DbTransaction trans)
    //    {
    //        OracleTransaction oraTrans = (OracleTransaction)trans;
    //        OracleDataAdapter adapter = new OracleDataAdapter(convertSql(strSql), oraTrans.Connection);
    //        DataTable dataTable = new DataTable();
    //        try
    //        {
    //            adapter.SelectCommand.Connection = oraTrans.Connection;
    //            adapter.SelectCommand.Transaction = oraTrans;
    //            adapter.Fill(dataTable);
    //        }
    //        finally
    //        {
    //            // _connection.Close();
    //        }
    //        return dataTable;
    //    }

    //    DataTable InterfaceDataBase.RunSqlQuery(string strSql, Dictionary<string, object> parameters, DbTransaction trans)
    //    {
    //        DataTable dataTable = new DataTable();
    //        try
    //        {
    //            OracleTransaction oraTrans = (OracleTransaction)trans;
    //            OracleDataAdapter adapter = new OracleDataAdapter(convertSql(strSql), oraTrans.Connection);
    //            adapter.SelectCommand = BuildQueryCommand(strSql, parameters, CommandType.Text);
    //            adapter.SelectCommand.Transaction = oraTrans;
    //            adapter.SelectCommand.Connection = oraTrans.Connection; 
    //            adapter.Fill(dataTable);
    //        }
    //        finally
    //        {

    //        }
    //        return dataTable;
    //    }

    //    #endregion
    //}
    /// <summary>
    /// OleDb的操作类
    /// </summary>
    public class OleDbDataBase : InterfaceDataBase
    {
        private System.Data.OleDb.OleDbConnection _connection;
        public OleDbDataBase(string strConnectioin)
        {
            _connection = new System.Data.OleDb.OleDbConnection(strConnectioin);
        }
        /// <summary>
        ///构造一个已含有OracleCommand对象的OracleCommand
        /// </summary>
        /// <paramKey colName="storedProcNameOrSqlString">存储过程名或带参数的SQL查询字符串</paramKey>
        /// <paramKey colName="parameters">Array of IDataParameter objects</paramKey>
        /// <paramKey colName="type">是存储过程还是查询字串</paramKey>
        /// <returns>返回SqlCommand对象，已装入参数</returns>
        /// 
        private System.Data.OleDb.OleDbCommand BuildQueryCommand(string storedProcNameOrSqlString, Dictionary<string, object> parameters, CommandType type)
        {
            storedProcNameOrSqlString = convertSql(storedProcNameOrSqlString);
            OleDbCommand command = new OleDbCommand();
            command.CommandType = type;
            //command.CommandText = storedProcNameOrSqlString;
            #region 创建parameter ,,转换sql语句
            int start = 0;
            bool startNow = false;
            int end = 0;
            string paramKey;
            string strSql = storedProcNameOrSqlString;
           // string strSql2 = storedProcNameOrSqlString;
            for (int i = 0; i < strSql.Length; i++)
            {
                if (strSql[i].Equals('@'))
                {
                    start = i;
                    startNow = true;
                }
                if (startNow)
                {
                    if ( strSql[i].Equals(' ') || strSql[i].Equals('|')
                        || (i == strSql.Length - 1) || strSql[i].Equals(',')
                        ||strSql[i].Equals(')') || strSql[i].Equals('='))
                    {
                        end = i;
                        if (i == strSql.Length - 1 && !strSql[i].Equals(')'))
                        {
                            paramKey = strSql.Substring(start + 1, end - start ).Trim();
                        }
                        else
                        {
                            paramKey = strSql.Substring(start + 1, end - start - 1).Trim();
                        }
                        //根据param找 sql参数值
                        object value = DBNull.Value;
                        parameters.TryGetValue(paramKey, out value);
                        //OracleParameter para = new OracleParameter(kvp.Key, kvp.Value);
                        OleDbParameter para = new OleDbParameter(paramKey, value);
                        command.Parameters.Add(para);
                        int tmp = storedProcNameOrSqlString.IndexOf("@" + paramKey);
                        if (tmp > -1)
                        {
                            storedProcNameOrSqlString = storedProcNameOrSqlString.Remove(tmp, paramKey.Length + 1);

                            storedProcNameOrSqlString = storedProcNameOrSqlString.Insert(tmp, "?");
                        }
                        startNow = false;
                    }
                }
            }
            #endregion
            command.CommandText = storedProcNameOrSqlString;
            command.Connection = _connection;
            return command;
        }

        #region InterfaceDataBase 成员
        /// <summary>
        /// 通过sql语句返回DataTable
        /// </summary>
        /// <paramKey colName="strSqlDel"></paramKey>
        /// <returns></returns>
        DataTable InterfaceDataBase.RunSqlQuery(string strSql)
        {
            OleDbDataAdapter adapter = new OleDbDataAdapter(convertSql(strSql), _connection);
            DataTable dataTable = new DataTable();
            try
            {
                if (_connection.State != ConnectionState.Open)
                {
                    _connection.Open();
                }
                adapter.Fill(dataTable);
            }
            finally
            {
                _connection.Close();
            }
            return dataTable;
        }

        DataTable InterfaceDataBase.RunSqlQuery(string strSql, Dictionary<string, object> parameters)
        {
            DataTable dataTable = new DataTable();
            try
            {
                if (_connection.State != ConnectionState.Open)
                {
                    _connection.Open();
                }
                OleDbDataAdapter adapter = new OleDbDataAdapter(convertSql(strSql), _connection);
                adapter.SelectCommand = BuildQueryCommand(strSql, parameters, CommandType.Text);
                adapter.Fill(dataTable);
            }
            finally
            {
                _connection.Close();
            }
            return dataTable;
        }

        int InterfaceDataBase.RunSqlNoneQuery(string strSql)
        {
            int result;
            try
            {
                _connection.Open();
                OleDbCommand command = new OleDbCommand(convertSql(strSql), _connection);
                result = command.ExecuteNonQuery();
            }
            finally
            {
                _connection.Close();
            }
            return result;
        }




        int InterfaceDataBase.RunSqlNoneQuery(string strSql, Dictionary<string, object> parameters)
        {
            int result;
            try
            {
                if (_connection.State != ConnectionState.Open)
                {
                    _connection.Open();
                }
                OleDbCommand command = BuildQueryCommand(convertSql(strSql), parameters, CommandType.Text);
                result = command.ExecuteNonQuery();
            }
            finally
            {
                //_connection.Close();
            }
            return result;
        }
        int InterfaceDataBase.RunSqlNoneQuery(string strSql, DbTransaction trans)
        {
            int result;
            try
            {
                OleDbTransaction oraTrans = (OleDbTransaction)trans;
                OleDbCommand command = new OleDbCommand(convertSql(strSql), oraTrans.Connection, oraTrans);
                result = command.ExecuteNonQuery();
            }
            finally
            {

            }
            return result;
        }

        int InterfaceDataBase.RunSqlNoneQuery(string strSql, Dictionary<string, object> parameters, DbTransaction trans)
        {
            int result;
            try
            {
                OleDbTransaction oraTrans = (OleDbTransaction)trans;
                OleDbCommand command = BuildQueryCommand(convertSql(strSql), parameters, CommandType.Text);
                command.Transaction = oraTrans;
                command.Connection = oraTrans.Connection;
                
                result = command.ExecuteNonQuery();
             
            }
            finally
            {
            }
            return result;
        }
        #endregion
        private string convertSql(string sql)
        {
            //return sql.Replace("@", ":");
            //去掉类似 @id
            return sql;
        }

        #region IDisposable 成员

        void IDisposable.Dispose()
        {
            //释放托管资源
            if (_connection != null)
            {
                _connection.Dispose();
            }
        }

        #endregion


        #region InterfaceDataBase 成员


        DbTransaction InterfaceDataBase.GetTransction()
        {
            //throw new NotImplementedException();
            if (_connection != null)
            {
                if (_connection.State == ConnectionState.Closed)
                {
                    _connection.Open();
                }
                return (DbTransaction)_connection.BeginTransaction();
            }
            return null;
        }

        #endregion

        #region InterfaceDataBase 成员
        DataTable InterfaceDataBase.RunSqlQuery(string strSql, DbTransaction trans)
        {
            OleDbTransaction oraTrans = (OleDbTransaction)trans;
            OleDbDataAdapter adapter = new OleDbDataAdapter(convertSql(strSql), oraTrans.Connection);
            DataTable dataTable = new DataTable();
            try
            {
                adapter.SelectCommand.Connection = oraTrans.Connection;
                adapter.SelectCommand.Transaction = oraTrans;
                adapter.Fill(dataTable);
            }
            finally
            {
                //_connection.Close();
            }
            return dataTable;
        }

        DataTable InterfaceDataBase.RunSqlQuery(string strSql, Dictionary<string, object> parameters, DbTransaction trans)
        {
            DataTable dataTable = new DataTable();
            try
            {
                OleDbTransaction oraTrans = (OleDbTransaction)trans;
                OleDbDataAdapter adapter = new OleDbDataAdapter(convertSql(strSql), oraTrans.Connection);
                adapter.SelectCommand = BuildQueryCommand(strSql, parameters, CommandType.Text);
                adapter.SelectCommand.Transaction = oraTrans;
                adapter.SelectCommand.Connection = oraTrans.Connection;
                adapter.Fill(dataTable);
            }
            finally
            {

            }
            return dataTable;
        }

        #endregion
    }
}
 