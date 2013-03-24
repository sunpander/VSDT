using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace auth.Services
{
    public  class DbUserInfo
    {
        public static DataTable QueryUserInfo(DataSet bcls_rec, string conn)
        {
            //程序用变量
            string strSql = "  select * from tesuserinfo where cname like @cname +'%'";
            try
            {
                string cname = "";
                if (bcls_rec.Tables.Count > 0 && bcls_rec.Tables[0].Rows.Count > 0)
                {
                    cname = bcls_rec.Tables[0].Rows[0][0].ToString();
                }

                CDbCommand tree_inqa = new CDbCommand(conn);
                tree_inqa.SetCommandText(strSql);
                tree_inqa.Parameters.Set("cname", cname);
                DataTable dt = new DataTable();
                tree_inqa.ExecuteQuery(ref dt);
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static DataTable QueryGroupInfo(DataSet bcls_rec, string conn)
        {
            //程序用变量
            try
            {
                string groupname = bcls_rec.Tables[0].Rows[0]["groupname"].ToString();
                string adminuser = bcls_rec.Tables[0].Rows[0]["adminuser"].ToString();
                string userid = bcls_rec.Tables[0].Rows[0]["userid"].ToString();
                string i_appname = bcls_rec.Tables[0].Rows[0]["appname"].ToString();
                string grouptype = "1";// bcls_rec.Tables[0].Rows[0]["grouptype"].ToString();
 
                CDbCommand tree_inqa = new CDbCommand(conn);

                tree_inqa.SetCommandText("select ID,NAME,GROUPDESCRIPTION FROM TESGROUPINFO where name like @groupname+'%' and grouptype = @grouptype");
                tree_inqa.Parameters.Set("groupname", groupname);
                //tree_inqa.Parameters.Set("adminuser", adminuser);
               // tree_inqa.Parameters.Set("i_appname", i_appname);
                tree_inqa.Parameters.Set("grouptype", grouptype);

                DataTable dt = new DataTable();
                tree_inqa.ExecuteQuery(ref dt);
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

 
        public static string SaveGroupInfo(DataSet bcls_rec, string conn)
        {
            string msg = "ok";
            int fetchRowCount = 0;
            try
            {
                //CDbCommand cmdForm(conn);
                CDbCommand cmd = new CDbCommand(conn);
                string name = "";
                string descript = "";
                 
            
                string appName = "";
         
                string groupType = "1";

                string sqlInsertGroup = " INSERT INTO  [TESGROUPINFO]"
                                        +"           ([NAME],[GROUPDESCRIPTION],[GROUPTYPE]"
                                        +"           ,[APPNAME] )"
                                        +"     VALUES"
                                        +"           (@name,@groupdescription,@grouptype "
                                        +"           ,@appname ) ";
                string sqlUpdGroup = " UPDATE  [TESGROUPINFO]"
                                        + "   SET [NAME] = @name,[GROUPDESCRIPTION] = @description"
                                        + "      ,[GROUPTYPE] = @grouptype,[APPNAME] =@appname"
                                        + "       "
                                        + " WHERE  id = @id ";
                  // 新增
                int blkIns = bcls_rec.Tables.IndexOf("INSERT_BLOCK");
                if (blkIns >= 0)
                {
                    for (fetchRowCount = 0; fetchRowCount < bcls_rec.Tables[blkIns].Rows.Count; ++fetchRowCount)
                    {
                        name = bcls_rec.Tables[blkIns].Rows[fetchRowCount]["name"].ToString();
                        descript = bcls_rec.Tables[blkIns].Rows[fetchRowCount]["groupdescription"].ToString();
                       // appName = bcls_rec.Tables[blkIns].Rows[fetchRowCount]["appname"].ToString();
                        //判断群组名是否重复
				        cmd.SetCommandText(" select COUNT(*) FROM TESGROUPINFO WHERE NAME = @groupname and APPNAME = @appname " );
                        cmd.Parameters.Set("groupname", name);
                        cmd.Parameters.Set("appname", appName);
                 
                        object obj = cmd.ExecuteScalar();
                        if (Convert.ToInt32(obj) > 0)
                        {
                            msg = "输入的群组名[" + name + "]已存在，请重新输入！";
                            throw new Exception(msg);
                        }

                        cmd.SetCommandText(sqlInsertGroup);
                        cmd.Parameters.Set("name", name);
                        cmd.Parameters.Set("groupdescription", descript);
                        cmd.Parameters.Set("grouptype", groupType);
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
                        name = bcls_rec.Tables[blkDel].Rows[fetchRowCount]["name"].ToString();
                        string groupid = bcls_rec.Tables[blkDel].Rows[fetchRowCount]["id"].ToString();
                        if (name == "usermanager" || name == "groupmanager" || name == "admingroup")
                        {
                            //if ( userid != "admin")
                            //{
                            //    msg = "系统群组只有admin超级管理员才能操作！";
                            //    throw new Exception(msg);
                            //}
                        }
                        //删除组下所有能否访问的资源
                        cmd.SetCommandText(" delete FROM TESGROUPACCESS WHERE  ACCESSERID = @groupid ");
                        cmd.Parameters.Set("groupid", groupid);
                        cmd.ExecuteNonQuery();


                        //删除所有父组是该组的记录
                        cmd.SetCommandText("delete FROM TESGROUPMEMBER WHERE  GROUPID  = @groupid ");
                        cmd.Parameters.Set("groupid", groupid);
                        cmd.ExecuteNonQuery();

                        //删除所有子组是该组的记录
                        cmd.SetCommandText(" delete FROM TESGROUPMEMBER  WHERE  MEMBERID = @groupid ");
                        cmd.Parameters.Set("groupid", groupid);
                        cmd.ExecuteNonQuery();

                        //删除组信息表中的记录	
                        cmd.SetCommandText("delete from TESGROUPINFO where id = @groupid ");
                        cmd.Parameters.Set("groupid", groupid);
                        cmd.ExecuteNonQuery();
                    }
                }
                // 修改
                int blkUpd = bcls_rec.Tables.IndexOf("UPDATE_BLOCK");
                if (blkUpd >= 0)
                {
                    for (fetchRowCount = 0; fetchRowCount < bcls_rec.Tables[blkUpd].Rows.Count; ++fetchRowCount)
                    {
                        name = bcls_rec.Tables[blkUpd].Rows[fetchRowCount]["name"].ToString();
                        //if (name == "usermanager" || name == "groupmanager" || name == "admingroup")
                        //{
                            //if ( userid != "admin")
                            //{
                            //    msg = "系统群组只有admin超级管理员才能操作！";
                            //    throw new Exception(msg);
                            //}
                        //}
                        descript = bcls_rec.Tables[blkUpd].Rows[fetchRowCount]["groupdescription"].ToString();
                        int aclid = Convert.ToInt32(bcls_rec.Tables[blkUpd].Rows[fetchRowCount]["id"]);
                        //判断画面是否存在
                        cmd.SetCommandText(" SELECT COUNT(*) FROM TESGROUPINFO WHERE NAME = @name and APPNAME = @appname  AND ID != @aclid ");
                        cmd.Parameters.Set("name", name);
                        cmd.Parameters.Set("aclid", aclid);
                        cmd.Parameters.Set("appname", appName);
                        int formCount = Convert.ToInt32(cmd.ExecuteScalar());
                        if (formCount > 0)
                        {
                            msg = "操作失败！修改的画面已存在！";
                            throw new Exception(msg);
                        }
                        //更新群组信息表
                        cmd.SetCommandText(sqlUpdGroup);
                        cmd.Parameters.Set("id", aclid);
                        cmd.Parameters.Set("description", descript);
                        cmd.Parameters.Set("name", name);
                        cmd.Parameters.Set("appname", appName);
                        cmd.Parameters.Set("grouptype", groupType);
                        cmd.Parameters.Set("appname", appName);
                        cmd.ExecuteNonQuery();
                    }
                }
                msg = "处理成功。";
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return msg;
        }
        
        public static string SaveUserInfo(DataSet bcls_rec, string conn)
        {
            string msg = "ok";
            int fetchRowCount = 0;
            try
            {
                CDbCommand cmd = new CDbCommand(conn);
                // 新增
                int blkIns = bcls_rec.Tables.IndexOf("INSERT_BLOCK");
                if (blkIns >= 0)
                {
                    for (fetchRowCount = 0; fetchRowCount < bcls_rec.Tables[blkIns].Rows.Count; ++fetchRowCount)
                    {
                        string ename = bcls_rec.Tables[blkIns].Rows[fetchRowCount]["ename"].ToString();
                        string cname = bcls_rec.Tables[blkIns].Rows[fetchRowCount]["cname"].ToString();
                        //新增用户
                        cmd.SetCommandText(" select count(*) from  TESUSERINFO  where ename = @ename");

                        cmd.Parameters.Set("ename", ename);

                        int num = Convert.ToInt32(cmd.ExecuteScalar());
                        if (num > 0)
                        {
                            throw new Exception("用户名[" + ename + "]已存在");
                        }

                        //新增用户
                        cmd.SetCommandText(" INSERT INTO TESUSERINFO   ( ENAME, CNAME)  VALUES (@ename,  @cname)");

                        cmd.Parameters.Set("ename", ename);
                        cmd.Parameters.Set("cname", cname);
                        cmd.ExecuteNonQuery();
                    }
                }
            
                // 删除
                int blkDel = bcls_rec.Tables.IndexOf("DELETE_BLOCK");
                if (blkDel >= 0)
                {
                    for (fetchRowCount = 0; fetchRowCount < bcls_rec.Tables[blkDel].Rows.Count; ++fetchRowCount)
                    {
                        int id = Convert.ToInt32(bcls_rec.Tables[blkDel].Rows[fetchRowCount]["aclid"]);
                        cmd.SetCommandText("delete from tesuserinfo where id=@id ");
                        cmd.Parameters.Set("id", id);
                        int delNum = cmd.ExecuteNonQuery();

                        //删除用户挂组信息
                        cmd.SetCommandText(" delete FROM TESGROUPMEMBER WHERE  MEMBERID = @id ");
                        cmd.Parameters.Set("id", id);
                        cmd.ExecuteNonQuery();
                    }
                }
                // 修改
                int blkUpd = bcls_rec.Tables.IndexOf("UPDATE_BLOCK");
                if (blkUpd >= 0)
                {
                    for (fetchRowCount = 0; fetchRowCount < bcls_rec.Tables[blkUpd].Rows.Count; ++fetchRowCount)
                    {
                        string ename = bcls_rec.Tables[blkUpd].Rows[fetchRowCount]["ename"].ToString();
                        string cname = bcls_rec.Tables[blkUpd].Rows[fetchRowCount]["cname"].ToString();

                        int id = Convert.ToInt32(bcls_rec.Tables[blkUpd].Rows[fetchRowCount]["id"]);
                        //判断名称是否存在
                        cmd.SetCommandText("select count(*) from  TESUSERINFO  where ename = @ename and id !=@id ");
                        cmd.Parameters.Set("ename", ename);
                        cmd.Parameters.Set("id", id);
                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        if (count > 0)
                        {
                            msg = "操作失败！修改的名称已存在！";
                            throw new Exception(msg);
                        }

                        //更新用户信息表
                        cmd.SetCommandText("update TESUSERINFO set ename =@ename ,cname=@cname where id=@id");
                        cmd.Parameters.Set("ename", ename);
                        cmd.Parameters.Set("cname", cname);
                        cmd.Parameters.Set("id", id);
                        cmd.ExecuteNonQuery();
                    }
                }
                msg = "处理成功。";
            }
            catch (Exception ex)
            {
                //msg = ex.Message + "[" + ex.StackTrace + "]";
                throw ex;
            }
            return msg;
        }

        public static string InsertGroupChildGroup(DataSet bcls_rec, string conn)
        {
            string groupid = bcls_rec.Tables[0].Rows[0][0].ToString();
            bool deadLoop = false;
            CDbCommand tesgroupmember_q = new CDbCommand(conn);
            for (int i = 0; i < bcls_rec.Tables[1].Rows.Count; i++)
            {
                //取得单行传入信息
                string cgroupid = bcls_rec.Tables[1].Rows[i][0].ToString();
                tesgroupmember_q.SetCommandText("WITH n(memberid,groupid,membertype) AS "
                    + " (SELECT memberid,groupid,membertype "
                    + " FROM TESGROUPMEMBER  WHERE GROUPID in(@groupid,@cgroupid) "
                    + " UNION ALL "
                    + " SELECT nplus1.memberid, nplus1.groupid, nplus1.membertype "
                    + " FROM TESGROUPMEMBER as nplus1, n "
                    + " WHERE n.memberid = nplus1.groupid ) "
                    + " SELECT top 2 memberid,groupid,membertype FROM n WHERE membertype = 2 "
                    );
                tesgroupmember_q.Parameters.Set("cgroupid", cgroupid);
                tesgroupmember_q.Parameters.Set("groupid", groupid);
                DataTable dtTmp = new DataTable();
                tesgroupmember_q.ExecuteQuery(ref dtTmp);
                if (dtTmp != null && dtTmp.Rows.Count > 0)
                {
                    string lmemberid = dtTmp.Rows[0][0].ToString();

                    string lgroupid = dtTmp.Rows[0][1].ToString();

                    if (lmemberid == groupid || lmemberid == cgroupid)
                    {
                        deadLoop = true;
                    }
                }

                if (cgroupid == groupid)
                {
                    deadLoop = true;
                }
                if (deadLoop)
                {
                    string msg = "插入的子组号 [{0}]将引起数据库中组挂组的循环,无法插入数据库/[{1}";
                    throw new Exception(msg);
                }
                else
                {
                    tesgroupmember_q.SetCommandText(" INSERT INTO TESGROUPMEMBER(memberid,groupid,membertype) "
                         + " VALUES (@cgroupid,  @groupid,  '2'  ) ");
                    tesgroupmember_q.Parameters.Set("cgroupid", cgroupid);
                    tesgroupmember_q.Parameters.Set("groupid", groupid);
                    tesgroupmember_q.ExecuteNonQuery();
                }
            }
            return "";
        }

        public static string InsertGroupChildUser(DataSet bcls_rec, string conn)
        {
            //获得输入参数
            string groupid = bcls_rec.Tables[0].Rows[0]["groupid"].ToString();
            string groupname = bcls_rec.Tables[0].Rows[0]["groupname"].ToString();
            /*建立连接*/
            CDbCommand cmd = new CDbCommand(conn);
            //对输入信息循环处理
            for (int i = 0; i < bcls_rec.Tables[1].Rows.Count; i++)
            {
                string userid = bcls_rec.Tables[1].Rows[i]["userid"].ToString();
                string username = bcls_rec.Tables[1].Rows[i]["username"].ToString();
                //判断该群组下是否已存在该用户
                cmd.SetCommandText(" select COUNT(*) FROM TESGROUPMEMBER WHERE memberid = @memberid and groupid = @groupid and membertype = 1 "
                        );

                cmd.Parameters.Set("memberid", userid);
                cmd.Parameters.Set("groupid", groupid);
                int num = Convert.ToInt32(cmd.ExecuteScalar());

                if (num > 0)
                {
                    string msg = "群组[" + groupname + "]下已存在子用户[" + username + "]!";

                    throw new Exception(msg);
                }
                cmd.SetCommandText(" INSERT INTO TESGROUPMEMBER(memberid,groupid,membertype) "
                                 + " VALUES (@userid,  @groupid, 1) ");
                cmd.Parameters.Set("userid", userid);
                cmd.Parameters.Set("groupid", groupid);
                cmd.ExecuteNonQuery();
            }
            return "ok";
        }

        public static DataSet QueryGroupChild(DataSet bcls_rec, string conn)
        {

            try
            {
                //获得输入参数
                string groupid = bcls_rec.Tables[0].Rows[0]["groupid"].ToString();
          
                CDbCommand group_inq = new CDbCommand(conn);
                string sql = "	  SELECT U.ID, U.NAME, U.GROUPDESCRIPTION  FROM TESGROUPINFO U "
            + "WHERE U.ID IN(SELECT MEMBERID FROM TESGROUPMEMBER "
              + "  WHERE MEMBERTYPE = 2 AND GROUPID= @groupid )";
                group_inq.SetCommandText(sql);
                group_inq.Parameters.Set("groupid", groupid);

                DataTable dtGroup = new DataTable();
                group_inq.ExecuteQuery(ref dtGroup);

                //返回子用户
                CDbCommand user_inq = new CDbCommand(conn);
                string cmdText = " SELECT U.ID, U.ENAME, U.CNAME  FROM TESUSERINFO U "
                            + " WHERE U.ID IN(SELECT MEMBERID FROM TESGROUPMEMBER "
                            + " WHERE MEMBERTYPE = 1 AND GROUPID= @groupid )";

                user_inq.SetCommandText(cmdText);
                user_inq.Parameters.Set("groupid", groupid);

                DataTable dtUser = new DataTable();
                user_inq.ExecuteQuery(ref dtUser);

                DataSet ds = new DataSet();
                ds.Tables.Add("Group");
                ds.Tables.Add("Users");
                ds.Tables[0].Merge(dtGroup);
                ds.Tables[1].Merge(dtUser);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
 
        public static string DeleteGroupMember(DataSet bcls_rec, string conn)
        {
            string groupid = bcls_rec.Tables[0].Rows[0]["groupid"].ToString();
            CDbCommand cmd = new CDbCommand(conn);
            //删除子组
            for (int i = 0; i < bcls_rec.Tables[1].Rows.Count; i++)
            {
                //取得单行传入信息
                string subgroupid = bcls_rec.Tables[1].Rows[i][0].ToString();
                cmd.SetCommandText("DELETE FROM TESGROUPMEMBER "
                    + " WHERE MEMBERID = @subgroupid   AND GROUPID = @groupid  AND MEMBERTYPE= 2"
                    );
                cmd.Parameters.Set("subgroupid", subgroupid);
                cmd.Parameters.Set("groupid", groupid);
                cmd.ExecuteNonQuery();
            }

            //删除子用户
            for (int i = 0; i < bcls_rec.Tables[2].Rows.Count; i++)
            {
                //取得单行传入信息
                string subuserid = bcls_rec.Tables[2].Rows[i][0].ToString();
                cmd.SetCommandText("DELETE FROM TESGROUPMEMBER   WHERE MEMBERID = @subuserid "
                                  + " AND GROUPID = @groupid  AND MEMBERTYPE=1 "
                              );
                cmd.Parameters.Set("subuserid", subuserid);
                cmd.Parameters.Set("groupid", groupid);
                cmd.ExecuteNonQuery();
            }
            return "ok";
        }

        public static DataTable QueryGroupByMember(DataSet bcls_rec, string conn)
        {
            //程序用变量
            try
            {
                string memberid = bcls_rec.Tables[0].Rows[0]["memberid"].ToString();
                 
                CDbCommand tree_inqa = new CDbCommand(conn);
                string sqlMemGroup =  " WITH GROUP_MEMBER_N(memberid, groupid) AS"
                +" ( SELECT	memberid, groupid FROM	TESGROUPMEMBER"
                +"				 WHERE	memberid IN	(  SELECT GROUPID FROM	TESGROUPMEMBER WHERE memberid = @memberid  )"
                +"	union all	 "
                +"   SELECT np1.memberid, np1.groupid FROM GROUP_MEMBER_N n,TESGROUPMEMBER np1  WHERE n.groupid= np1.memberid"
                +"  )"
                +" select * from tesgroupinfo where id in ("
                +" SELECT distinct groupid FROM	GROUP_MEMBER_N"
                +" UNION ALL"
                +" SELECT GROUPID  FROM TESGROUPMEMBER  WHERE memberid = @memberid) ";
                tree_inqa.SetCommandText(sqlMemGroup);
                tree_inqa.Parameters.Set("memberid", memberid);
              
                DataTable dt = new DataTable();
                tree_inqa.ExecuteQuery(ref dt);
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
