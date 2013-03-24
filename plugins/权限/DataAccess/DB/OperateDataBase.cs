using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;

namespace DataAccess
{
    /// <summary>
    /// 直接操作数据库中表
    /// </summary>
    public class Operate
    {

        












        public static int ExecuteSqlNoQuery(string strSql)
        {
            int result = 0;
            using (InterfaceDataBase db = DataBaseManager.GetDataBase())
            {
                try
                {
                    result = db.RunSqlNoneQuery(strSql);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return result;
        }
        public static DataTable ExecuteSqlQuery(string strSql)
        {
            DataTable result  ;
            using (InterfaceDataBase db = DataBaseManager.GetDataBase())
            {
                try
                {
                    result = db.RunSqlQuery(strSql);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return result;
        }
        /// <summary>
        /// 查询用户所有可操作的表
        /// </summary>
        /// <returns></returns>
        public static System.Data.DataTable QueryTables()
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            using (InterfaceDataBase db = DataBaseManager.GetDataBase())
            {
                try
                {
                    string strSql = "select USER_TABLES.TABLE_NAME,USER_TAB_COMMENTS.COMMENTS from  user_tables left join   user_tab_comments on USER_TABLES.TABLE_NAME = USER_TAB_COMMENTS.TABLE_NAME ";
                    switch (DataBaseManager.GetDataBaseType())
                    {
                        case DataBaseManager.DataBaseType.MSSqlServer:
                            break;
                        case DataBaseManager.DataBaseType.Oracle:
                            break;
                        case DataBaseManager.DataBaseType.DB2:
                            strSql = "select  tabNAME as table_name, SYSCAT.TABLES.REMARKS as comments  from   syscat.tables   where   tabschema = current   schema  or tabschema='ES' or tabschema='TTA'";
                            break;
                        case DataBaseManager.DataBaseType.Default:
                            break;
                        default:
                            break;
                    }
                    dt = db.RunSqlQuery(strSql);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return dt;
        }

        /// <summary>
        /// 查询用户所有可操作的表
        /// </summary>
        /// <returns></returns>
        public static System.Data.DataTable QueryResxTables(string tableName)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            using (InterfaceDataBase db = DataBaseManager.GetDataBase())
            {
                try
                {
                    string strSqlOra = "select  table_name,comments from all_tab_comments   ";
                    //string strSqlOra = "select USER_TABLES.TABLE_NAME,USER_TAB_COMMENTS.COMMENTS from  user_tables left join   user_tab_comments on USER_TABLES.TABLE_NAME = USER_TAB_COMMENTS.TABLE_NAME where USER_TABLES.TABLE_NAME like '%'||@TABLENAME||'%' ";
                    //Dictionary<string, object> parameter = new Dictionary<string, object>();
                    //parameter.Add("TABLENAME", tableName);
                    string strSqlDB2 = "select  tabNAME as table_name, SYSCAT.TABLES.REMARKS as comments  from   syscat.tables   where   tabschema = current   schema  or tabschema='ES' or tabschema='TTA'";
                    string strSql = strSqlOra;
                    switch (DataBaseManager.GetDataBaseType())
                    {
                        case DataBaseManager.DataBaseType.MSSqlServer:
                            break;
                        case DataBaseManager.DataBaseType.Oracle:
                            strSql = strSqlOra;
                            break;
                        case DataBaseManager.DataBaseType.DB2:
                            strSql = strSqlDB2;
                            break;
                        case DataBaseManager.DataBaseType.Default:
                            strSql = strSqlOra;
                            break;
                        default:
                            break;
                    }
                    dt = db.RunSqlQuery(strSql);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return dt;
        }

        /// <summary>
        /// 获取表的所有列名称以及列名称描述
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static System.Data.DataTable QueryColumnByTableName( string tableName)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            using (InterfaceDataBase db = DataBaseManager.GetDataBase())
            {
                try
                {
                    //string strSqlOra = "select USER_TABLES.TABLE_NAME as TABLE_NAME,USER_COL_COMMENTS.COMMENTS  ,USER_COL_COMMENTS.COLUMN_NAME  from  user_tables left join    user_col_comments on USER_TABLES.TABLE_NAME = user_col_comments.TABLE_NAME "
                    //          + " where USER_TABLES.TABLE_NAME=@TABLE_NAME ";
                    string strSqlOra = "select  table_name,column_name,comments  from  all_col_comments where TABLE_NAME =@TABLE_NAME ";
                    Dictionary<string, object> parameter = new Dictionary<string, object>();
                    parameter.Add("TABLE_NAME", tableName);
                    string strSql = strSqlOra;

                    switch (DataBaseManager.GetDataBaseType())
                    {
                        case DataBaseManager.DataBaseType.MSSqlServer:
                            break;
                        case DataBaseManager.DataBaseType.Oracle:
                            strSql = strSqlOra;
                            break;
                        case DataBaseManager.DataBaseType.DB2:
                            strSql = "select name as column_name,  REMARKS  as comments from  Sysibm.syscolumns where TBNAME =@TABLE_NAME ";
                            break;
                        case DataBaseManager.DataBaseType.Default:
                            break;
                        default:
                            strSql = strSqlOra;
                            break;
                    }
                    dt = db.RunSqlQuery(strSql,parameter);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return dt;
        }
       
        /// <summary>
        /// 查询表中数据
        /// </summary>
        /// <param name="tableName">表名称</param>
        /// <returns></returns>
        public static System.Data.DataTable QueryDataByTableName(string tableName)
        {
            if(tableName.Trim().Equals(string.Empty))
                return null;
            System.Data.DataTable dt = new System.Data.DataTable();

            using (InterfaceDataBase db = DataBaseManager.GetDataBase())
            {
                try
                {
                    //string strSqlOra = "select  * from @TABLE_NAME  ";
                    //Dictionary<string, object> parameter = new Dictionary<string, object>();
                    //parameter.Add("TABLE_NAME", tableName);
                    //dtLogResult = db.RunSqlQuery(strSqlOra, parameter);
                    string strSql = "select  * from " + tableName ;
                    dt = db.RunSqlQuery(strSql);
                    dt.TableName = tableName;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return dt;
        }
 
        /// <summary>
        /// 删除行(根据表名称,以及行内容)
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="dr"></param>
        /// <returns></returns>
        public static int RemoveDataRow(string tableName,DataRow dr)
        {
            if (tableName.Trim().Equals(string.Empty)|| dr==null)
                return 0;
            int result = 0;
            using (InterfaceDataBase db = DataBaseManager.GetDataBase())
            {
                try
                {
                    //string strSqlOra = "select  * from @TABLE_NAME  ";
                    //Dictionary<string, object> parameter = new Dictionary<string, object>();
                    //parameter.Add("TABLE_NAME", tableName);
                    //dtLogResult = db.RunSqlQuery(strSqlOra, parameter);
                    string strSql = "delete from " + tableName +" where 1=1 and ";
                    if (dr.RowState == DataRowState.Deleted)
                    {
                        dr.RejectChanges();
                    }
                    Dictionary<string, object> parameter = new Dictionary<string, object>();
                    int count = dr.Table.Columns.Count;
                    for (int i = 0; i < count; i++)
                    {
                        string colNameNow = dr.Table.Columns[i].ColumnName.Trim();
                        object colValue = dr[i];
                        if (colValue == System.DBNull.Value)
                        {
                            strSql = strSql + dr.Table.Columns[i].ColumnName + " is null " ;

                        }
                        else
                        {
                            strSql = strSql + dr.Table.Columns[i].ColumnName + " =@" + dr.Table.Columns[i].ColumnName;
                            parameter.Add(colNameNow, colValue);
                        }
                        if (i != count - 1)
                        {
                            strSql = strSql + " and ";
                        }
                    }
                    result = db.RunSqlNoneQuery(strSql,parameter);
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
            return result;
        }
        /// <summary>
        /// 更新行 中某 一列
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="dr"></param>
        /// <returns></returns>
        public static int UpdateDataRowCell(string tableName, DataRow dr,string columnName )
        {
            columnName = columnName.Trim();
            tableName = tableName.Trim();
            if (tableName.Equals(string.Empty) || dr == null||columnName.Equals(string.Empty))
                return 0;
            int result = 0;
            using (InterfaceDataBase db = DataBaseManager.GetDataBase())
            {
                try
                {
                    string strSql = "update " + tableName + " set " + columnName + "= @" + columnName + "  where 1=1 and ";
                    Dictionary<string, object> parameter = new Dictionary<string, object>();
                    int count = dr.Table.Columns.Count;
                    for (int i = 0; i < count; i++)
                    {
                        string colNameNow= dr.Table.Columns[i].ColumnName.Trim();
                        object colValue = dr[i];
                        parameter.Add(colNameNow, colValue);
                        if (colNameNow == columnName)
                            continue;
                        if (colValue == System.DBNull.Value)
                        {
                            strSql = strSql + dr.Table.Columns[i].ColumnName + " is null ";

                        }
                        else
                        {
                            strSql = strSql + dr.Table.Columns[i].ColumnName + " =@" + dr.Table.Columns[i].ColumnName;
                        }
                        if (i < count - 1)
                        {
                            strSql = strSql + " and ";
                        }
                    }
                    strSql = strSql.Trim();
                    if (strSql.EndsWith("and"))
                    {
                        strSql = strSql.Remove(strSql.Length - 3);
                    }
                    result = db.RunSqlNoneQuery(strSql,parameter);
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
            return result;
        }

        /// <summary>
        /// 新增一行
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="dr"></param>
        /// <returns></returns>
        internal static int AddDataRow(string tableName, DataRow dr)
        {
            tableName = tableName.Trim();
            if (tableName.Equals(string.Empty) || dr == null  )
                return 0;
            int result = 0;
            using (InterfaceDataBase db = DataBaseManager.GetDataBase())
            {
                try
                {
                    Dictionary<string, object> parameter = new Dictionary<string, object>();

                    string strSql = "insert into " + tableName + " ";
                    string strBefore = "(";
                    string strAfter = " values(";
                    int count = dr.Table.Columns.Count;
                    for (int i = 0; i < count; i++)
                    {
                        string colNameNow = dr.Table.Columns[i].ColumnName.Trim();
                        object colValue = dr[i];
                        strBefore = strBefore + colNameNow;
                        strAfter = strAfter + " @"+colNameNow;
                        if (i != count - 1)
                        {
                            strBefore = strBefore + ",";
                            strAfter = strAfter + ",";
                        }
                        else
                        {
                            strBefore = strBefore + ") ";
                            strAfter = strAfter + ") ";
                        }
                        parameter.Add(colNameNow, colValue);
                    }
                    strSql = strSql + strBefore + strAfter;
                    result = db.RunSqlNoneQuery(strSql, parameter);
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
            return result;
        }

        /// <summary>
        /// 导入到数据库,约定dtData中第一行表示数据库中列.有一个STATUS列表示状态
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="dtData"></param>
        /// <returns></returns>
        internal static int ImportToDataBase(string tableName, DataTable dtData)
        {
            tableName = tableName.Trim();
            if (tableName == "")
                return 0;
            int result = 0;
            if (dtData != null && dtData.Rows.Count > 0)
            {
                //首先获取要导入的列名称

                try
                {
                    //组合sql语句
                    string strSql = "insert into " + tableName + " ";
                    string strBefore = "(";
                    string strAfter = " values(";
                    int count = dtData.Columns.Count;
                    List<int> colIndex = new List<int>();
                    for (int i = 0; i < count; i++)
                    {
                        object colValue = dtData.Rows[0][i];
                        if(colValue == null || colValue.ToString().Trim().Equals(""))
                            continue;
                        //特殊列--状态
                        if (dtData.Columns[i].ColumnName == "STATUS")
                            continue;
                        colIndex.Add(i);
                        string colNameNow = colValue.ToString();
                        strBefore = strBefore + colNameNow  + ",";
                        strAfter = strAfter + " @" + colNameNow + ",";
                    }
                    strBefore = strBefore.TrimEnd(',') + ")"; ;
                    strAfter = strAfter.TrimEnd(',')+")";
                    strSql = strSql + strBefore + strAfter;

                    Dictionary<string, object> parameter = new Dictionary<string, object>();
                    using (InterfaceDataBase db = DataBaseManager.GetDataBase())
                    {
                        for (int i = 1; i < dtData.Rows.Count; i++)
                        {
                            try
                            {
                                parameter.Clear();
                                for (int j = 0; j < colIndex.Count; j++)
                                {
                                    int temp = colIndex[j];
                                    parameter.Add(dtData.Rows[0][temp].ToString(), dtData.Rows[i][temp]);
                                }
                                int result2 = db.RunSqlNoneQuery(strSql, parameter);
                                dtData.Rows[i]["STATUS"] = result2;
                                result += result2;
                            }
                            catch 
                            {
                                dtData.Rows[i]["STATUS"] = "error";// +ex.Message;
                                //单条记录插入失败时怎么办?;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
            return result;
        }
        internal static int UpdateResTableCol(string tableName, string culture, string contentCol, string content, string[] resKeyCols, string[] resKeyValues)
        {
            int result = 0;
            if (resKeyCols.Length != resKeyValues.Length)
            {
                throw new ApplicationException("主键列数量,与主键值数量必须一致");
            }
            using (InterfaceDataBase db = DataBaseManager.GetDataBase())
            {
                DbTransaction dbTrans = db.GetTransction();
                try
                {
                    string strSqlUpdate = string.Format("update {0}  set {1} =@CONTENT where culture =@CULTURE ", tableName, contentCol);
                    string strSqlInsert = "";
                    string strSqlInsPre = string.Format("insert into {0}({1},CULTURE ", tableName, contentCol);
                    string strSqlInsLast = "values( @CONTENT,@CULTURE  ";
                    Dictionary<string, object> parameter = new Dictionary<string, object>();
                    parameter.Clear();
                    parameter.Add("CONTENT", content);
                    parameter.Add("CULTURE", culture);
                    string keyCol, keyValue;
                    for (int i = 0; i < resKeyCols.Length; i++)
                    {
                        keyCol = resKeyCols[i].ToUpper();
                        keyValue = resKeyValues[i];
                        if (keyCol == "CULTURE")
                            continue;
                        strSqlUpdate += string.Format(" and {0} = @{1} ", keyCol, keyCol);
                        strSqlInsPre += string.Format(" ,{0}  ", keyCol);
                        strSqlInsLast += string.Format(" ,@{0} ", keyCol);
                        parameter.Add(keyCol, keyValue);
                    }
                    strSqlInsert = string.Format("{0}) {1} )", strSqlInsPre, strSqlInsLast);
                    System.Console.WriteLine("insert:" + strSqlInsert);
                    System.Console.WriteLine("update:" + strSqlUpdate);
                    //如果修改不成功,就当做插入的
                    int temp = db.RunSqlNoneQuery(strSqlUpdate, parameter, dbTrans);
                    if (temp == 0)
                    {
                        temp = db.RunSqlNoneQuery(strSqlInsert, parameter, dbTrans);
                    }
                    result = temp;
                    dbTrans.Commit();//提交
                }
                catch (Exception ex)
                {

                    dbTrans.Rollback();//回滚
                    throw ex;
                }
            }
            return result;
        }
 
        internal static int UpdateResTableCol(string tableName, string reskey, string culture, string contentCol, string content)
        {
            int result = 0;
            using (InterfaceDataBase db = DataBaseManager.GetDataBase())
            {
                DbTransaction dbTrans = db.GetTransction();
                try
                {
                    string strSqlUpdate = string.Format("update {0}  set {1} =@CONTENT where ACLID = @ACLID and culture =@CULTURE ", tableName, contentCol);
                    string strSqlInsert = string.Format("insert into {0}(ACLID,CULTURE,{1}) values( @ACLID,@CULTURE,@CONTENT) ", tableName, contentCol); 
                    Dictionary<string, object> parameter = new Dictionary<string, object>();

                    parameter.Clear();
                    parameter.Add("ACLID", reskey);
                    parameter.Add("CONTENT", content);
                    parameter.Add("CULTURE", culture);
                    //如果修改不成功,就当做插入的
                    int temp = db.RunSqlNoneQuery(strSqlUpdate, parameter, dbTrans);
                    if (temp == 0)
                    {
                        temp = db.RunSqlNoneQuery(strSqlInsert, parameter, dbTrans);
                    }
                    result = temp ;
                    dbTrans.Commit();//提交
                }
                catch (Exception ex)
                {

                    dbTrans.Rollback();//回滚
                    throw ex;
                }
            }
            return result;
        }



        public static DataTable QueryColumnInfoByColName(string colName)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            using (InterfaceDataBase db = DataBaseManager.GetDataBase())
            {
                try
                {
                    string strSql = "";
                    switch (DataBaseManager.GetDataBaseType())
                    {
                        case DataBaseManager.DataBaseType.MSSqlServer:
                            break;
                        case DataBaseManager.DataBaseType.Oracle:
                            strSql = string.Format(" select  t1.column_name as item_name,t2.comments  as item_desc,     t1.data_type as item_type,t1.data_length as item_len, "
                                                    + " t1.data_scale as item_scale, t1.data_precision  as ITEM_PRECISION ,t1.data_default   "
                                                    + " from  ALL_TAB_COLUMNS T1 ,ALL_COL_COMMENTS T2 "
                                                    + " where T1.OWNER = T2.OWNER   AND T1.TABLE_NAME = T2.TABLE_NAME   AND T1.COLUMN_NAME = T2.COLUMN_NAME"
                                                    + " and t1.column_name = '{0}'  and  rownum = 1    order by  column_id ", colName.ToUpper());
                            break;
                        case DataBaseManager.DataBaseType.DB2:
                            strSql = string.Format(" select t1.name as item_name,t1.remarks as item_desc, t1.typename as item_type,t1.length as item_len,"
                                                    + " t1.scale as item_scale,t1.length as ITEM_PRECISION,t1.default as data_default ,t1.keyseq "
                                                    + " from  Sysibm.syscolumns t1 where  1=1"
                                                    + " and t1.name = '{0}'    order by colNo ", colName.ToUpper());
                            break;
                        case DataBaseManager.DataBaseType.Default:
                            break;
                        default:
                            break;
                    }
                    dt = db.RunSqlQuery(strSql);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return dt;
        }


        internal void ExecuteQuery(Tables tables)
        {
            throw new NotImplementedException();
        }
    }
}
