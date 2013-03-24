using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace auth.Services
{
    public class DbResource
    {
        /// <summary>
        /// 查询窗体
        /// </summary>
        /// <param name="bcls_rec"></param>
        /// <param name="conn"></param>
        /// <returns></returns>
        public static DataTable QueryFormInfo(DataSet bcls_rec, string conn)
        {
            //程序用变量
            string strName = "";
            string strdllName = "";
            string strAppname = "";
            string strSql = " SELECT FORM.NAME, FORM.DESCRIPTION, FORM.DLLNAME, CAST(FORM.ACLID AS VARCHAR) AS ACLID, FORM.ABBREV, FORM.ICONNUM, FORM.FORM_CALL_MODE, FORM.APPNAME, FORM.DLLPATH "
                    + " FROM TESFORMRESINFO FORM "
                    + " WHERE "
                    + " NAME like  @name+'%' and dllname like @dllname+'%' " //and appname= @cursystem  "
                    + " ORDER BY name ASC ";

            try
            {
                strName = bcls_rec.Tables[0].Rows[0]["name"].ToString();
                strdllName = bcls_rec.Tables[0].Rows[0]["dllname"].ToString();
                strAppname = bcls_rec.Tables[0].Rows[0]["appname"].ToString();

                CDbCommand tree_inqa = new CDbCommand(conn);

                tree_inqa.SetCommandText(strSql);
                tree_inqa.Parameters.Set("name", strName);
                tree_inqa.Parameters.Set("dllname", strdllName);

                DataTable dt = new DataTable();
                tree_inqa.ExecuteQuery(ref dt);
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        /// <summary>
        /// 查询按钮
        /// </summary>
        /// <param name="bcls_rec"></param>
        /// <param name="conn"></param>
        /// <returns></returns>
        public static DataTable QueryButtonInfo(DataSet bcls_rec, string conn)
        {
            //程序用变量
            string strFName = "";

            string strSql = " SELECT BUTT.NAME, BUTT.FNAME, BUTT.ACLID, BUTT.DESCRIPTION, BUTT.OPTYPE, BUTT.APPNAME "
                            + " FROM TESBUTTONRESINFO BUTT "
                            + " WHERE    FNAME =@fname "
                            + " ORDER BY name ASC";

            try
            {
                strFName = bcls_rec.Tables[0].Rows[0]["fname"].ToString();


                CDbCommand tree_inqa = new CDbCommand(conn);

                tree_inqa.SetCommandText(strSql);
                tree_inqa.Parameters.Set("fname", strFName);

                DataTable dt = new DataTable();
                tree_inqa.ExecuteQuery(ref dt);
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        /// <summary>
        /// 保存窗体
        /// </summary>
        /// <param name="bcls_rec"></param>
        /// <param name="conn"></param>
        /// <returns></returns>
        public static string SaveFormInfo(DataSet bcls_rec, string conn)
        {
            string msg = "ok";
            int fetchRowCount = 0;
            try
            {
                //CDbCommand cmdForm(conn);
                CDbCommand cmd = new CDbCommand(conn);
                string name = "";
                string descript = "";
                string dllName = "";
                string abbrev = "";
                string fromCallMode = "0";
                string appName = "";
                string dllPath = "";
                string sqlInsertFrm = "INSERT INTO  [TESFORMRESINFO] "
                  + " ([NAME],[DESCRIPTION],[DLLNAME] ,[ABBREV],[ICONNUM] ,[FORM_CALL_MODE],[APPNAME] ,[DLLPATH])"
                  + " VALUES "
                  + " (@name, @description, @dllname,   @abbrev,@iconnum,@form_call_mode,@appname,@dllpath)";
                string sqlDel = "delete from TESFORMRESINFO where aclid = @aclid";

                string sqlDelBtnAuthByFrm = " delete FROM TESGROUPACCESS "
                                + " WHERE ACLID in (select ACLID from TESBUTTONRESINFO "
                                + " WHERE FNAME = @name ) ";
                string sqlDelFrmAuth = " delete FROM TESGROUPACCESS where ACLID = @aclid ";
                string sqlQueryBtn = " SELECT ACLID,NAME,DESCRIPTION FROM TESBUTTONRESINFO WHERE FNAME = @name";
               
                string sqlDelBtnAuthById = " DELETE FROM TESGROUPACCESS WHERE ACLID = @aclid ";
                string sqlDelBtn = "DELETE FROM TESBUTTONRESINFO WHERE ACLID = @aclid ";


                string sqlQuerySameFrm = " SELECT COUNT(*) FROM TESFORMRESINFO WHERE NAME = @name  AND ACLID != @aclid ";
            
                string sqlUpdFrm =  "UPDATE [TESFORMRESINFO]"
                               + "  SET [NAME] = @NAME,[DESCRIPTION]=@DESCRIPTION,[DLLNAME] = @DLLNAME "
                               + "    ,[ABBREV] = @ABBREV,[ICONNUM] = @ICONNUM,[FORM_CALL_MODE] = @FORM_CALL_MODE "
                               + "     ,[APPNAME] = @APPNAME ,[DLLPATH] = @DLLPATH "
                               + " WHERE  aclid = @aclid";

                string sqlUpdBtnFname = " update TESBUTTONRESINFO set fname = @fname where fname=(select top 1 name from TESFORMRESINFO where aclid=@aclid)  ";
                // 新增
                int blkIns = bcls_rec.Tables.IndexOf("INSERT_BLOCK");
                if (blkIns >= 0)
                {
                    for (fetchRowCount = 0; fetchRowCount < bcls_rec.Tables[blkIns].Rows.Count; ++fetchRowCount)
                    {
                        name = bcls_rec.Tables[blkIns].Rows[fetchRowCount]["name"].ToString();
                        descript = bcls_rec.Tables[blkIns].Rows[fetchRowCount]["description"].ToString();
                        dllName = bcls_rec.Tables[blkIns].Rows[fetchRowCount]["dllname"].ToString();
                        abbrev = bcls_rec.Tables[blkIns].Rows[fetchRowCount]["abbrev"].ToString();
                        dllPath = bcls_rec.Tables[blkIns].Rows[fetchRowCount]["dllpath"].ToString();
                        if (null != bcls_rec.Tables[blkIns].Rows[fetchRowCount]["form_call_mode"])
                        {
                            fromCallMode = bcls_rec.Tables[blkIns].Rows[fetchRowCount]["form_call_mode"].ToString();
                        }

                        //判断画面名是否重复
                        cmd.SetCommandText(" select COUNT(*) FROM TESFORMRESINFO WHERE NAME = @name ");
                        cmd.Parameters.Set("name", name);
                        object obj = cmd.ExecuteScalar();
                        if (Convert.ToInt32(obj) > 0)
                        {
                            msg = "输入的画面名[" + name + "]已存在，请重新输入！";
                            throw new Exception(msg);
                        }

                        cmd.SetCommandText(sqlInsertFrm);
                        cmd.Parameters.Set("description", descript);
                        cmd.Parameters.Set("dllname", dllName);
                        cmd.Parameters.Set("abbrev", abbrev);
                        cmd.Parameters.Set("iconnum", 0);
                        cmd.Parameters.Set("form_call_mode", fromCallMode);
                        cmd.Parameters.Set("appname", appName);
                        cmd.Parameters.Set("dllpath", dllPath);
                        cmd.ExecuteNonQuery();
                    }
                }
                // 删除
                int blkDel = bcls_rec.Tables.IndexOf("DELETE_BLOCK");
                if (blkDel >= 0)
                {
                    for (fetchRowCount = 0; fetchRowCount < bcls_rec.Tables[blkDel].Rows.Count; ++fetchRowCount)
                    {
                        int aclidFrm = Convert.ToInt32(bcls_rec.Tables[blkDel].Rows[fetchRowCount]["aclid"]);
                        cmd.SetCommandText(sqlDel);
                        cmd.Parameters.Set("aclid", aclidFrm);
                        int delNum = cmd.ExecuteNonQuery();
                        if (delNum > 0)
                        {
                            //删除画面下所有按钮的授权信息
                            cmd.SetCommandText(sqlDelBtnAuthByFrm);
                            cmd.Parameters.Set("name", name);
                            cmd.ExecuteNonQuery();
                            //删除授权信息中所有该画面的信息
                            cmd.SetCommandText(sqlDelFrmAuth);
                            cmd.Parameters.Set("aclid", aclidFrm);
                            cmd.ExecuteNonQuery();
                            //删除画面下所有的按钮 
                            cmd.SetCommandText(sqlQueryBtn);
                            cmd.Parameters.Set("name", name);
                            DataTable dtBtnInfo = new DataTable();
                            cmd.ExecuteQuery(ref dtBtnInfo);

                            for (int i = 0; i < dtBtnInfo.Rows.Count; i++)
                            {
                                int aclidBtn = Convert.ToInt32(dtBtnInfo.Rows[i]["aclid"]);
                                //删除按钮授权信息
                                cmd.SetCommandText(sqlDelBtnAuthById);
                                cmd.Parameters.Set("aclid", aclidBtn);
                                cmd.ExecuteNonQuery();
                                //从按钮表删除
                                cmd.SetCommandText(sqlDelBtn);
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }
                }
                // 修改
                int blkUpd = bcls_rec.Tables.IndexOf("UPDATE_BLOCK");
                if (blkUpd >= 0)
                {
                    for (fetchRowCount = 0; fetchRowCount < bcls_rec.Tables[blkUpd].Rows.Count; ++fetchRowCount)
                    {
                        name = bcls_rec.Tables[blkUpd].Rows[fetchRowCount]["name"].ToString();
                        descript = bcls_rec.Tables[blkUpd].Rows[fetchRowCount]["description"].ToString();
                        dllName = bcls_rec.Tables[blkUpd].Rows[fetchRowCount]["dllname"].ToString();
                        abbrev = bcls_rec.Tables[blkUpd].Rows[fetchRowCount]["abbrev"].ToString();
                        dllPath = bcls_rec.Tables[blkUpd].Rows[fetchRowCount]["dllpath"].ToString();
                        if (null != bcls_rec.Tables[blkUpd].Rows[fetchRowCount]["form_call_mode"])
                        {
                            fromCallMode = bcls_rec.Tables[blkUpd].Rows[fetchRowCount]["form_call_mode"].ToString();
                        }

                        int aclidFrm = Convert.ToInt32(bcls_rec.Tables[blkUpd].Rows[fetchRowCount]["aclid"]);
                        //判断画面是否存在
                        cmd.SetCommandText(sqlQuerySameFrm);
                        cmd.Parameters.Set("name", name);
                        cmd.Parameters.Set("aclid", aclidFrm);
                        int formCount = Convert.ToInt32(cmd.ExecuteScalar());
                        if (formCount > 0)
                        {
                            msg = "操作失败！修改的画面已存在！";
                            throw new Exception(msg);
                        }
                        //更新画面名时更新按钮表的fname原画面名
                        cmd.SetCommandText(sqlUpdBtnFname);
                        cmd.Parameters.Set("aclid", aclidFrm);
                        cmd.Parameters.Set("fname", name);
                        cmd.ExecuteNonQuery();

                        //更新画面信息表
                        cmd.SetCommandText(sqlUpdFrm);

                        cmd.Parameters.Set("description", descript);
                        cmd.Parameters.Set("dllname", dllName);
                        cmd.Parameters.Set("abbrev", abbrev);
                        cmd.Parameters.Set("iconnum", 0);
                        cmd.Parameters.Set("form_call_mode", fromCallMode);
                        cmd.Parameters.Set("appname", appName);
                        cmd.Parameters.Set("dllpath", dllPath);
                        cmd.Parameters.Set("aclid", aclidFrm);
                        cmd.ExecuteNonQuery();
                    }
                }
                msg = "处理成功。";
            }
            catch (Exception ex)
            {
                msg = ex.Message + "[" + ex.StackTrace + "]";
            }
            return msg;
        }
        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="bcls_rec"></param>
        /// <param name="conn"></param>
        /// <returns></returns>
        public static string SaveButtonInfo(DataSet bcls_rec, string conn)
        {
            string msg = "ok";
            int fetchRowCount = 0;
            try
            {
                CDbCommand cmd = new CDbCommand(conn);
                string name = "";
                string fname = "";
                string descript = "";
                string optype = "";
                string appName = "";

           

                string sqlQueryBtn = " select COUNT(*) FROM TESBUTTONRESINFO WHERE NAME = @name and FNAME = @fname ";
                string sqlInsertBtn = "INSERT INTO  [TESBUTTONRESINFO] "
                                     + " ([NAME],[FNAME],[DESCRIPTION],[OPTYPE] ,[APPNAME])"
                                     + " VALUES "
                                     + " (@name,@fname, @description,  @optype,@appname)";
                string sqlDelBtn = "DELETE FROM TESBUTTONRESINFO WHERE ACLID = @aclid ";

                string sqlDelBtnAuthById = " DELETE FROM TESGROUPACCESS WHERE ACLID = @aclid ";

                string sqlQuerySameBtn = " SELECT COUNT(*) FROM TESBUTTONRESINFO WHERE NAME = @name and FNAME = @fname  AND ACLID != @aclid ";
                
                string sqlUpdBtn = "UPDATE [TESFORMRESINFO]"
                              + "  SET [NAME] = @name,[DESCRIPTION]=@description,[FNAME] = @fanme , "
                              + "  [OPTYPE] = @optype,  [APPNAME] = @APPNAME   "
                              + " WHERE  aclid = @aclid";
                // 新增
                int blkIns = bcls_rec.Tables.IndexOf("INSERT_BLOCK");
                if (blkIns >= 0)
                {
                    for (fetchRowCount = 0; fetchRowCount < bcls_rec.Tables[blkIns].Rows.Count; ++fetchRowCount)
                    {
                        name = bcls_rec.Tables[blkIns].Rows[fetchRowCount]["name"].ToString();
                        descript = bcls_rec.Tables[blkIns].Rows[fetchRowCount]["description"].ToString();
                        fname = bcls_rec.Tables[blkIns].Rows[fetchRowCount]["fname"].ToString();
                        optype = bcls_rec.Tables[blkIns].Rows[fetchRowCount]["optype"].ToString();
                        appName = bcls_rec.Tables[blkIns].Rows[fetchRowCount]["appName"].ToString();

                        //判断画面名是否重复
                        cmd.SetCommandText(sqlQueryBtn);
                        cmd.Parameters.Set("name", name);
                        cmd.Parameters.Set("fname", fname);
                        object obj = cmd.ExecuteScalar();
                        if (Convert.ToInt32(obj) > 0)
                        {
                            msg = "输入的按钮名[" + name + "]在画面["+fname+"]已存在，请重新输入！";
                            throw new Exception(msg);
                        }

                        cmd.SetCommandText(sqlInsertBtn);
                        cmd.Parameters.Set("description", descript);
                        cmd.Parameters.Set("name", name);
                        cmd.Parameters.Set("fname", fname);
                        cmd.Parameters.Set("optype", optype);
                        cmd.Parameters.Set("appname", appName);
                        cmd.ExecuteNonQuery();
                    }
                }
                // 删除
                int blkDel = bcls_rec.Tables.IndexOf("DELETE_BLOCK");
                if (blkDel >= 0)
                {
                    for (fetchRowCount = 0; fetchRowCount < bcls_rec.Tables[blkDel].Rows.Count; ++fetchRowCount)
                    {
                        int aclidBtn = Convert.ToInt32(bcls_rec.Tables[blkDel].Rows[fetchRowCount]["aclid"]);
                        cmd.SetCommandText(sqlDelBtn);
                        cmd.Parameters.Set("aclid", aclidBtn);
                        int delNum = cmd.ExecuteNonQuery();
                        if (delNum > 0)
                        {
                            //删除按钮授权信息
                            cmd.SetCommandText(sqlDelBtnAuthById);
                            cmd.Parameters.Set("aclid", aclidBtn);
                            cmd.ExecuteNonQuery();
                            //从按钮表删除
                            cmd.SetCommandText(sqlDelBtn);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                // 修改
                int blkUpd = bcls_rec.Tables.IndexOf("UPDATE_BLOCK");
                if (blkUpd >= 0)
                {
                    for (fetchRowCount = 0; fetchRowCount < bcls_rec.Tables[blkUpd].Rows.Count; ++fetchRowCount)
                    {
                        name = bcls_rec.Tables[blkUpd].Rows[fetchRowCount]["name"].ToString();
                        descript = bcls_rec.Tables[blkUpd].Rows[fetchRowCount]["description"].ToString();
                        fname = bcls_rec.Tables[blkUpd].Rows[fetchRowCount]["fname"].ToString();
                        optype = bcls_rec.Tables[blkUpd].Rows[fetchRowCount]["optype"].ToString();
                        appName = bcls_rec.Tables[blkUpd].Rows[fetchRowCount]["appname"].ToString();

                        int aclidBtn = Convert.ToInt32(bcls_rec.Tables[blkUpd].Rows[fetchRowCount]["aclid"]);
                        //判断画面是否存在
                        cmd.SetCommandText(sqlQuerySameBtn);
                        cmd.Parameters.Set("name", name);
                        cmd.Parameters.Set("fname", fname);
                        cmd.Parameters.Set("aclid", aclidBtn);
                        int formCount = Convert.ToInt32(cmd.ExecuteScalar());
                        if (formCount > 0)
                        {
                            msg = "操作失败！修改的按钮名已存在！";
                            throw new Exception(msg);
                        }
                     
                        //更新画面信息表
                        cmd.SetCommandText(sqlUpdBtn);
                        cmd.Parameters.Set("description", descript);
                        cmd.Parameters.Set("name", name);
                        cmd.Parameters.Set("fname", fname);
                        cmd.Parameters.Set("optype", optype);
                        cmd.Parameters.Set("appname", appName);
                        cmd.Parameters.Set("aclid", aclidBtn);
                        cmd.ExecuteNonQuery();
                    }
                }
                msg = "处理成功。";
            }
            catch (Exception ex)
            {
                msg = ex.Message + "[" + ex.StackTrace + "]";
            }
            return msg;
        }
    }
}
