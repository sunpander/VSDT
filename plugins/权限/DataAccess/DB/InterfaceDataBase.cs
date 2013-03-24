using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;

namespace DataAccess
{
    /// <summary>
    /// 数据库操作类接口
    /// </summary>
    public interface InterfaceDataBase:IDisposable
    {
        DataTable RunSqlQuery(string strSql);
        DataTable RunSqlQuery(string strSql, DbTransaction trans);
        DataTable RunSqlQuery(string strSql, Dictionary<string,object>   parameters);
        DataTable RunSqlQuery(string strSql, Dictionary<string, object> parameters, DbTransaction trans);
        int RunSqlNoneQuery(string strSql);
        int RunSqlNoneQuery(string strSql, DbTransaction trans);
        int RunSqlNoneQuery(string strSql, Dictionary<string, object> parameters);
        int RunSqlNoneQuery(string strSql, Dictionary<string, object> parameters,DbTransaction trans );
        DbTransaction GetTransction();
    }



    /// <summary>
    /// 数据库管理类
    /// </summary>
    public class DataBaseManager : IDisposable
    {
        public enum DataBaseType
        {
            MSSqlServer,
            Oracle,
            DB2,
            Default 
        } 
        public static InterfaceDataBase GetDataBase()
        {
            return GetDataBase(DataBaseType.Default);
        }
        public static DataBaseType GetDataBaseType()
        {
            return DataBaseType.MSSqlServer;

        }
        private static InterfaceDataBase GetDataBase(DataBaseType baseType)
        {
            InterfaceDataBase dataBase = null;
            switch (baseType)
            {
                case DataBaseType.MSSqlServer:
                case DataBaseType.Oracle:
                case DataBaseType.DB2:
                case DataBaseType.Default:
                    dataBase = new OleDbDataBase("");
                    break;
            }
            return dataBase;
        }

        #region IDisposable 成员

        void IDisposable.Dispose()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
