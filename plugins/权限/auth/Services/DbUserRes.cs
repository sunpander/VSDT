using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace auth.Services
{
    public class DbUserRes
    {
        //epesbuttauthinq
        public static DataTable QueryButtonAuth(DataSet bcls_rec, string conn)
        {
            try
            {
                string subj = bcls_rec.Tables[0].Rows[0]["id"].ToString();
                string i_formname = bcls_rec.Tables[0].Rows[0]["formid"].ToString();
                string mode = bcls_rec.Tables[0].Rows[0]["mode"].ToString();
                string i_appname = bcls_rec.Tables[0].Rows[0]["appname"].ToString();


                CDbCommand tree_inqa = new CDbCommand(conn);
                string strSql = "";
                if (mode == "1") //根据群组id查  and form name query button
                {
                    //通过群组id查询  button
                     strSql = " SELECT BI.ACLID, BI.NAME, BI.DESCRIPTION, "
                                    + "(SELECT COUNT(*) FROM TESGROUPACCESS WHERE ACLID = BI.ACLID AND ACCESSERID = @id ) as num"
                                    + " FROM TESBUTTONRESINFO BI "
                                    + " WHERE     BI.FNAME in (select name from tesformresinfo where aclid= @formname)    ORDER BY BI.NAME ";
               
                }
                else if (mode == "2")
                {
                    //通过用户id查询 按钮信息
                    strSql=" WITH GROUP_MEMBER_N(memberid, groupid) "
                                    +"AS"
                                    +" ( SELECT	memberid, groupid FROM	TESGROUPMEMBER"
                                    +"	WHERE	memberid	IN	( SELECT	GROUPID"
                                    +"							 FROM	TESGROUPMEMBER WHERE	MEMBERID = @id"
                                    +"							 )"
                                    +" UNION ALL "
                                    +"	SELECT	np1.memberid, np1.groupid  FROM	GROUP_MEMBER_N n, TESGROUPMEMBER np1"
                                    +"    WHERE  	n.groupid	= np1.memberid"
                                    +" ) "
                                    + " SELECT BI.ACLID, BI.NAME, BI.DESCRIPTION, (SELECT COUNT(*) FROM TESGROUPACCESS"
                                    +"							  WHERE ACLID = BI.ACLID AND ACCESSERID IN"
                                    +"									  ( SELECT DISTINCT groupid   FROM	GROUP_MEMBER_N "
                                    +"														 UNION ALL"
                                    +"													     SELECT GROUPID FROM TESGROUPMEMBER WHERE memberid = @id "
                                    +"									  ) "
                                    +"								 ) as NUM"
                                    +" FROM TESBUTTONRESINFO BI "
                                    + " WHERE   BI.FNAME in (select name from tesformresinfo where aclid= @formname)  "
                                    +" ORDER BY BI.NAME ";
                }
                DataTable dt = new DataTable();
                if (!string.IsNullOrEmpty(strSql))
                {
                    tree_inqa.SetCommandText(strSql);
                    tree_inqa.Parameters.Set("id", subj);
                    tree_inqa.Parameters.Set("formname", i_formname);
                    tree_inqa.Parameters.Set("appname", i_appname);
                  
                    tree_inqa.ExecuteQuery(ref dt);
                    return dt;
                }
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //epesformauthinq
        public static DataTable QueryFormAuth(DataSet bcls_rec, string conn)
        {
            try
            {
                string subj = bcls_rec.Tables[0].Rows[0]["id"].ToString();//id
                ;
                string mode = bcls_rec.Tables[0].Rows[0]["mode"].ToString();

                string formlist = "";
                for (int i = 0; i < bcls_rec.Tables[0].Rows.Count; i++)
                {
                    formlist = formlist  + bcls_rec.Tables[0].Rows[i]["formname"].ToString() + ",";
                }
                formlist = formlist.TrimEnd(',');
                CDbCommand tree_inqa = new CDbCommand(conn);
                string strSql = " SELECT FORM.ACLID, FORM.NAME FORMNAME,(SELECT COUNT(*) FROM TESGROUPACCESS WHERE ACCESSERID in ([@id]) AND ACLID = FORM.ACLID) FORMCOUNT, "
                            + " COALESCE(BUTTON.NAME, ' ') BUTTNAME,COALESCE(BUTTON.ACLID, '0') BUTTID, (SELECT COUNT(*) FROM TESGROUPACCESS WHERE ACCESSERID in ([@id]) AND ACLID =BUTTON.ACLID) BUTTCOUNT"
                            + " "
                            + "FROM   (SELECT BI.ACLID, BI.NAME, BI.FNAME, BI.DESCRIPTION FROM TESBUTTONRESINFO BI) BUTTON"
                            + "   RIGHT JOIN  (SELECT FI.ACLID, FI.NAME, FI.DESCRIPTION FROM TESFORMRESINFO  FI)FORM			"
                            + "ON FORM.NAME = BUTTON.FNAME WHERE FORM.ACLID IN (" + formlist + ") ORDER BY FORMNAME, BUTTNAME ";

                if (mode == "1") //根据群组id查按钮
                {
                    //通过群组id查询  button
                    strSql = strSql.Replace("[@id]", subj);
                }
                else if (mode == "2")
                {
                    //通过用户id查询 按钮信息
                    DataSet ds = new DataSet();
                    ds.Tables.Add();
                    ds.Tables[0].Columns.Add("memberid");
                    ds.Tables[0].Rows.Add(subj);//subj为userid
                    DataTable dtGroup = DbUserInfo.QueryGroupByMember(ds, conn);
                    if (dtGroup == null || dtGroup.Rows.Count == 0)
                    {
                        //不在任何组,返回空表    
                        return new DataTable();
                    }
                    string groupIdList = "";
                    for (int i = 0; i < dtGroup.Rows.Count; i++)
                    {
                        groupIdList = groupIdList + dtGroup.Rows[i]["ID"].ToString() + ",";
                    }
                    groupIdList = groupIdList.TrimEnd(',');

                    strSql = strSql.Replace("[@id]", groupIdList);

                }
                else
                {
                    throw new Exception("mode只能是1[根据组]或者2[根据用户]");
                }
                DataTable dt = new DataTable();

                tree_inqa.SetCommandText(strSql);
                tree_inqa.ExecuteQuery(ref dt);
                return dt;
 
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //epesformlistinq
        internal static DataTable QueryFormList(DataSet bcls_rec, string conn)
        {
            //程序用变量
  
            string strSql =  " SELECT FI.ACLID, FI.NAME, FI.DESCRIPTION,"
                            +"      (SELECT   COUNT(*) AS Expr1 FROM TESGROUPACCESS WHERE   ACLID = FI.ACLID  AND  ACCESSERID in ( [@groupid]) )  AS CNT"
                            +" FROM      TESFORMRESINFO AS FI"
                            +"   WHERE   (FI.NAME LIKE @ename + '%')  AND (FI.DESCRIPTION LIKE @cname+ '%')"
                            +" ORDER BY FI.NAME ";

            try
            {
                string ename = bcls_rec.Tables[0].Rows[0]["name"].ToString();
                string descript = bcls_rec.Tables[0].Rows[0]["descript"].ToString();
                string mode = bcls_rec.Tables[0].Rows[0]["mode"].ToString();
                string groupid = bcls_rec.Tables[0].Rows[0]["groupid"].ToString();
                string appname = bcls_rec.Tables[0].Rows[0]["appname"].ToString();

                CDbCommand tree_inqa = new CDbCommand(conn);
                if (mode == "1")//query group form
                {
                    strSql = strSql.Replace("[@groupid]", groupid);
                }
                else if (mode == "2")//query user
                {
                    DataSet ds = new DataSet();
                    ds.Tables.Add();
                    ds.Tables[0].Columns.Add("memberid");
                    ds.Tables[0].Rows.Add(groupid);//groupid为userid
                    DataTable dtGroup = DbUserInfo.QueryGroupByMember(ds, conn);
                    string groupIdList = "";
                    for (int i = 0; i < dtGroup.Rows.Count; i++)
                    {
                        groupIdList = groupIdList + dtGroup.Rows[i]["ID"].ToString() + ",";
                    }
                    groupIdList = groupIdList.TrimEnd(',');

                    strSql = strSql.Replace("[@groupid]", groupid);
                }
                else
                {
                    throw new Exception("mode只能是1[根据组]或者2[根据用户]");
                }

                tree_inqa.SetCommandText(strSql);
                tree_inqa.Parameters.Set("ename", ename);
                tree_inqa.Parameters.Set("cname", descript);

                DataTable dt = new DataTable();
                tree_inqa.ExecuteQuery(ref dt);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
 
        internal static string   UpdateFormAccess(DataSet bcls_rec, string conn)
        {
            //程序用变量
            string strDeleteSql = " delete from TESGROUPACCESS where aclid = @formaclid and ACCESSERID = @groupid ";
            string strInsertSql = " INSERT INTO TESGROUPACCESS(ACLID, ACCESSERID, ACCESSERTYPE, ACCESSCODE) "
                                + "  VALUES( @formaclid, @groupid, 2, 5) ";
            try
            {
                for (int i = 0; i < bcls_rec.Tables[0].Rows.Count; i++)
                {

                    //string ename = bcls_rec.Tables[0].Rows[i]["username"].ToString();
                     string mode = bcls_rec.Tables[0].Rows[i]["mode"].ToString();
                     string formaclid = bcls_rec.Tables[0].Rows[i]["formaclid"].ToString();
                    string groupid = bcls_rec.Tables[0].Rows[i]["groupid"].ToString();
      

                    CDbCommand tree_inqa = new CDbCommand(conn);
                    if (mode == "insert")
                    {
                        tree_inqa.SetCommandText(strInsertSql);
                    }
                    else if (mode == "delete")
                    {
                        tree_inqa.SetCommandText(strDeleteSql);
                    }
                    tree_inqa.Parameters.Set("formaclid", formaclid);
                    tree_inqa.Parameters.Set("groupid", groupid);
                    tree_inqa.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return "";
        }

        internal static void UpdateButtonAccess(DataSet bcls_rec, string conn)
        {
            //程序用变量
            string strDeleteSql = " delete from TESGROUPACCESS where aclid = @buttonid and ACCESSERID = @groupid ";
            string strInsertSql = " INSERT INTO TESGROUPACCESS(ACLID, ACCESSERID, ACCESSERTYPE, ACCESSCODE) "
                                + "  VALUES( @buttonid, @groupid, 2, 5) ";
            try
            {
                for (int i = 0; i < bcls_rec.Tables[0].Rows.Count; i++)
                {
                    //string ename = bcls_rec.Tables[0].Rows[i]["username"].ToString();
                    string mode = bcls_rec.Tables[0].Rows[i]["mode"].ToString();
                    string buttonid = bcls_rec.Tables[0].Rows[i]["buttonid"].ToString();
                    string groupid = bcls_rec.Tables[0].Rows[i]["groupid"].ToString();

                    CDbCommand tree_inqa = new CDbCommand(conn);
                    if (mode == "insert")
                    {
                        tree_inqa.SetCommandText(strInsertSql);
                    }
                    else if (mode == "delete")
                    {
                        tree_inqa.SetCommandText(strDeleteSql);
                    }
                    tree_inqa.Parameters.Set("buttonid", buttonid);
                    tree_inqa.Parameters.Set("groupid", groupid);
                    tree_inqa.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="formName"></param>
        /// <param name="conn"></param>
        public static DataTable QueryAccess(int userID, string formName, string conn)
        {
            try
            {
                CDbCommand tree_inqa = new CDbCommand(conn);
                string strSql = "";

                //通过用户id查询 按钮信息
                strSql = " WITH GROUP_MEMBER_N(memberid, groupid) "
                                + "AS"
                                + " ( SELECT	memberid, groupid FROM	TESGROUPMEMBER"
                                + "	WHERE	memberid	IN	( SELECT	GROUPID"
                                + "							 FROM	TESGROUPMEMBER WHERE	MEMBERID = @id"
                                + "							 )"
                                + " UNION ALL "
                                + "	SELECT	np1.memberid, np1.groupid  FROM	GROUP_MEMBER_N n, TESGROUPMEMBER np1"
                                + "    WHERE  	n.groupid	= np1.memberid"
                                + " ) "
                                + " SELECT  BI.NAME, BI.DESCRIPTION,'BUTTON' type, (SELECT COUNT(*) FROM TESGROUPACCESS"
                                + "							  WHERE ACLID = BI.ACLID AND ACCESSERID IN"
                                + "									  ( SELECT DISTINCT groupid   FROM	GROUP_MEMBER_N "
                                + "														 UNION ALL"
                                + "											 SELECT GROUPID FROM TESGROUPMEMBER WHERE memberid = @id "
                                + "									  ) "
                                + "								 ) as NUM"
                                + " FROM TESBUTTONRESINFO BI  "
                                + " WHERE BI.FNAME = @formname "
                                + " UNION ALL "
                                + " SELECT  FI.NAME, FI.DESCRIPTION,'FORM' type, (SELECT COUNT(*) FROM TESGROUPACCESS"
                                + "							  WHERE ACLID = FI.ACLID AND ACCESSERID IN"
                                + "									  ( SELECT DISTINCT groupid   FROM	GROUP_MEMBER_N "
                                + "														 UNION ALL"
                                + "											 SELECT GROUPID FROM TESGROUPMEMBER WHERE memberid = @id "
                                + "									  ) "
                                + "								 ) as NUM"
                                + " FROM TESFORMRESINFO FI  "
                                + " WHERE FI.NAME = @formname ";
                DataTable dt = new DataTable();
                if (!string.IsNullOrEmpty(strSql))
                {
                    tree_inqa.SetCommandText(strSql);
                    tree_inqa.Parameters.Set("id", userID);
                    tree_inqa.Parameters.Set("formname", formName);
                   // tree_inqa.Parameters.Set("appname", i_appname);

                    tree_inqa.ExecuteQuery(ref dt);
                    return dt;
                }
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<string> QueryAccessFomList(int userID, string conn)
        {
            try
            {
                List<string> list = new List<string>();
                DataSet ds = new DataSet();
                ds.Tables.Add();
                ds.Tables[0].Columns.Add("mode");
                ds.Tables[0].Columns.Add("id");
                ds.Tables[0].Rows.Add("2", userID);
                DataTable dt = DbUserRes.QueryFormAuth(ds, conn);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string formName = dt.Rows[i]["NAME"].ToString();
                        if (!list.Contains(formName))
                        {
                            list.Add(formName);
                        }
                    }
                }
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
