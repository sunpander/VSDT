using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SQLite;
using System.Data.Common;

namespace SQLiteTest
{
    class SqlLitHelp
    {
    }


    public abstract class SqlLiteHelper
    {
        public static string ConnSqlLiteDbPath = string.Empty;
        public static string ConnString
        {
            get
            {
                return string.Format(@"Data Source={0}", ConnSqlLiteDbPath);
            }
        }

        // 取datatable
        public static DataTable GetDataTable(out string sError, string sSQL)
        {
            DataTable dt = null;
            sError = string.Empty;

            try
            {
                SQLiteConnection conn = new SQLiteConnection(ConnString);
                conn.Open();
                SQLiteCommand cmd = new SQLiteCommand();
                cmd.CommandText = sSQL;
                cmd.Connection = conn;
                SQLiteDataAdapter dao = new SQLiteDataAdapter(cmd);
                dt = new DataTable();
                dao.Fill(dt);
            }
            catch (Exception ex)
            {
                sError = ex.Message;
            }

            return dt;
        }

        // 取某个单一的元素
        public static object GetSingle(out string sError, string sSQL)
        {
            DataTable dt = GetDataTable(out sError, sSQL);
            if (dt != null && dt.Rows.Count > 0)
            {
                return dt.Rows[0][0];
            }

            return null;
        }

        // 取最大的ID
        public static Int32 GetMaxID(out string sError, string sKeyField, string sTableName)
        {
            DataTable dt = GetDataTable(out sError, "select ifnull(max([" + sKeyField + "]),0) as MaxID from [" + sTableName + "]");
            if (dt != null && dt.Rows.Count > 0)
            {
                return Convert.ToInt32(dt.Rows[0][0].ToString());
            }

            return 0;
        }

        // 执行 insert,update,delete 动作，也可以使用事务
        public static bool UpdateData(out string sError, string sSQL, bool bUseTransaction)
        {
            int iResult = 0;
            sError = string.Empty;

            if (!bUseTransaction)
            {
                try
                {
                    SQLiteConnection conn = new SQLiteConnection(ConnString);
                    conn.Open();
                    SQLiteCommand comm = new SQLiteCommand(conn);
                    comm.CommandText = sSQL;
                    iResult = comm.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    sError = ex.Message;
                    iResult = -1;
                }
            }
            else // 使用事务
            {
                DbTransaction trans = null;
                try
                {
                    SQLiteConnection conn = new SQLiteConnection(ConnString);
                    conn.Open();
                    trans = conn.BeginTransaction();
                    SQLiteCommand comm = new SQLiteCommand(conn);
                    comm.CommandText = sSQL;
                    iResult = comm.ExecuteNonQuery();
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    sError = ex.Message;
                    iResult = -1;
                    trans.Rollback();
                }
            }

            return iResult > 0;
        }
    }
}
