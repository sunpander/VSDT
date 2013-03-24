using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace auth.Services
{
    class DbTreeInfo
    {
        internal static int DeleteTreeNode(System.Data.DataSet inblkd, string p)
        {
            throw new NotImplementedException();
        }

        internal static int UpdateTreeNode(System.Data.DataSet inBlock, string p)
        {
            throw new NotImplementedException();
        }

        internal static int AddTreeNode(System.Data.DataSet bcls_rec, string conn)
        {
            //先判断 是否会形成死循环(节点嵌套)

            //然后判断名称是否重复

            //进行新增


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
            return 0;
        }
        //epestree_inqb
        internal static System.Data.DataTable QueryTreeNode(System.Data.DataSet inBlock, string p)
        {
            throw new NotImplementedException();
        }
    }
}
