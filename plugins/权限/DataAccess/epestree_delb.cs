 
 
 
 
/******************************************************************************
   NAME:			estreeinfo_delb
   PURPOSE:		从Tree菜单中删除画面信息
   REVISIONS:
   Ver        Date        Author           Description
   ---------  ----------  ---------------  ------------------------------------
   	1.0				2007-10-01	vivitea						授权更新
		1.1				2008-04-17	vivitea						根据统一授权认证进行修改，加入子系统
	1.2				2009-08-03	 yuxiuwen						修复删除节点不能删除画面关系的缺陷，取消对用户操作权限的判断
		
   NOTES:
******************************************************************************/


int f_epestree_delb(DataSetbcls_rec,DataSetbcls_ret,InterfaceDataBase  conn);
int f_es_isuseringroup(string username, string groupname, string appname, InterfaceDataBase  conn);


BM2F_ENTERACE(epestree_delb)


int f_epestree_delb(DataSet bcls_rec, DataSet bcls_ret,InterfaceDataBase  conn) 
{
	char FunctionEname[31]="estreeinfo_delb";	

	EDLog(1, 1, " **************%s begin*****************",FunctionEname);	

	int doFlag = 0;
	char i_form_name[129] = "";
	char aclid[20] = "";
	int fetchRowCount;
	char userid[19] = "";
	char i_appname[19] = "";
	int nodeexist = 0;
	int nodecount = 0;
  
	//使用的表结构变量
	#include "testreeinfo.h"

	try
	{
		bcls_rec->GetSYS(&s);

		bcls_rec->GetColVal(2, 1,"userid",userid);
		bcls_rec->GetColVal(2, 1,"appname",i_appname);				
		bcls_rec->GetColVal(1, 1, "name", i_form_name);

		EDLog(1,1,"[ESTREEINFO_DELB]  userid[%s]  appname[%s] ",userid,i_appname);

		CDbCommand treenameQ(conn);
		treenameQ.SetCommandText(" select * from testreeinfo " 
								" where appname = @i_appname " 
								" and fname = @i_form_name " );

		{
			CDbCommand cmd_tmp(conn);
			cmd_tmp.SetCommandText(" select count(1) from testreeinfo " 
									" where name = @i_form_name " 
									" and RESNAME = 'FOLDER' ");

			cmd_tmp.Parameters.Set("i_form_name", i_form_name);
			cmd_tmp.ExecuteReader();
			if(cmd_tmp.Read())
			{
				cmd_tmp.Get(1,fetchRowCount);
			}
			cmd_tmp.Close();
		}
		
		if(fetchRowCount > 0)  //找到目录节点
		{
			//判断是否有子目录
			CDbCommand sub_folder_count(conn);
			sub_folder_count.SetCommandText(" select count(1) from testreeinfo " 
											" where appname = @i_appname " 
											" and fname = @i_form_name " 
											" and RESNAME = 'FOLDER' " );
			sub_folder_count.Parameters.Set("i_appname", i_appname);
			sub_folder_count.Parameters.Set("i_form_name", i_form_name);
			sub_folder_count.ExecuteReader();
			if(sub_folder_count.Read())
			{
				sub_folder_count.Get(1,fetchRowCount);
			}
			sub_folder_count.Close();
			if(fetchRowCount > 0)  //目标节点下有目录节点
			{
				throw CApplicationException(-1, _RES("EPESS0000029")/*不能直接删除多个节点数据，请重新选择！*/, s.svc_name);
			}
			else //删除目标节点下的画面节点
			{	
				treenameQ.Parameters.Set("i_appname", i_appname);
				treenameQ.Parameters.Set("i_form_name", i_form_name);
				treenameQ.ExecuteReader();
				for (fetchRowCount = 0; ;) 
				{ 
					if(treenameQ.Read())
					{
						EIDbMapping::Fetch((T_INFO*)&testreeinfo_info,treenameQ);
					}
					else break;

					EDLog(1, 1, "[%s], [%ld], [%s]", testreeinfo.name, testreeinfo.aclid, testreeinfo.fname);
					//2011-7-26 yuxiuwen 修改
					CDbCommand node_count(conn);
					node_count.SetCommandText("SELECT COUNT(*) FROM TESTREEINFO WHERE ACLID = @aclid ");
					node_count.Parameters.Set("aclid", testreeinfo.aclid);
					node_count.ExecuteReader();
					node_count.Read();
					node_count.Get(1, nodecount);
					node_count.Close();
					
					EDLog(1, 1, "node_count = %d", nodecount);
					
					//只有一个画面节点，删除其对应的资源
					if(nodecount == 1)
					{						
						CDbCommand del_res(conn);
						del_res.SetCommandText(" DELETE FROM TESTREEINFO_RES WHERE ACLID = @aclid " );
						sprintf(aclid, "%ld", testreeinfo.aclid);
						del_res.Parameters.Set("aclid", aclid);
						del_res.ExecuteNonQuery();
						del_res.Close();								
					}
					//删除树节点
					CDbCommand del_tree(conn);
					del_tree.SetCommandText(" delete from testreeinfo where appname = @i_appname "
											" and name = @nodename and fname = @i_form_name and resname != 'FOLDER' ");
					del_tree.Parameters.Set("i_appname", i_appname);
					del_tree.Parameters.Set("nodename", testreeinfo.name);
					del_tree.Parameters.Set("i_form_name", i_form_name);
					del_tree.ExecuteNonQuery();
					del_tree.Close();

					Log::Info("EPES", "tree", _RES("EPESS0000112")/*删除菜单树节点[{4}]*/, "DEL_TREE", "", "", "", testreeinfo.name);
				}
				treenameQ.Close();							
					
				//2011-7-26 yuxiuwen 修改
				{
					CDbCommand id_inq(conn);
					id_inq.SetCommandText(" SELECT ACLID FROM TESTREEINFO WHERE NAME = @name AND APPNAME = @appname AND RESNAME = 'FOLDER' ");
					id_inq.Parameters.Set("name", i_form_name);
					id_inq.Parameters.Set("appname", i_appname);
					id_inq.ExecuteReader();
					
					if(id_inq.Read())
					{
						id_inq.Get(1, aclid);
						
						CDbCommand cmd_tmp(conn);
						cmd_tmp.SetCommandText(" DELETE FROM TESTREEINFO_RES WHERE ACLID = @aclid " );
						cmd_tmp.Parameters.Set("aclid", aclid);
						cmd_tmp.ExecuteNonQuery();
						cmd_tmp.Close();
					}
					id_inq.Close();
				}
				//删除目录节点					
				{
					CDbCommand cmd_tmp(conn);
					cmd_tmp.SetCommandText(" delete from testreeinfo where appname = @i_appname and name = @i_form_name and resname = 'FOLDER' ");
					cmd_tmp.Parameters.Set("i_appname", i_appname);
					cmd_tmp.Parameters.Set("i_form_name", i_form_name);
					cmd_tmp.ExecuteNonQuery();
					cmd_tmp.Close();
				}
				Log::Info("EPES", "tree", _RES("EPESS0000113")/*删除菜单树节点[{4}]*/, "DEL_TREE", "", "", "", i_form_name);
			}
		}
		else  //选中的是画面
		{			
			{
				CDbCommand cmd_tmp(conn);
				cmd_tmp.SetCommandText(" select * from testreeinfo " 
										" where appname = @i_appname "
										" and name = @i_form_name ");
				cmd_tmp.Parameters.Set("i_appname", i_appname);
				cmd_tmp.Parameters.Set("i_form_name", i_form_name);
				EIDbMapping::Select((T_INFO*)&testreeinfo_info,cmd_tmp);
				cmd_tmp.Close();
			}

			//2011-7-26 yuxiuwen 修改
			{
				CDbCommand id_inq(conn);
				id_inq.SetCommandText(" SELECT ACLID FROM TESTREEINFO WHERE NAME = @name AND APPNAME = @appname AND RESNAME != 'FOLDER' ");
				id_inq.Parameters.Set("name", i_form_name);
				id_inq.Parameters.Set("appname", i_appname);
				id_inq.ExecuteReader();
				
				if(id_inq.Read())
				{
					id_inq.Get(1, aclid);
				}
				id_inq.Close();
			}

			{
				CDbCommand cmd_tmp(conn);
				cmd_tmp.SetCommandText(" delete from TESTREEINFO " 
											" where name = @i_form_name " 
											" and appname = @i_appname " );
				cmd_tmp.Parameters.Set("i_form_name", i_form_name);
				cmd_tmp.Parameters.Set("i_appname", i_appname);
				cmd_tmp.ExecuteNonQuery();
				cmd_tmp.Close();
			}

			Log::Info("EPES", "tree", _RES("EPESS0000113")/*删除菜单树节点[{4}]*/, "DEL_TREE", "", "", "", i_form_name);

			{
				CDbCommand cmd_tmp(conn);
				cmd_tmp.SetCommandText(" select count(*) from testreeinfo where aclid = @aclid ");
				cmd_tmp.Parameters.Set("aclid", aclid);
				cmd_tmp.ExecuteReader();
				nodeexist = 0;
				if(cmd_tmp.Read())
				{
					cmd_tmp.Get(1, nodeexist);
				}
				cmd_tmp.Close();

				if(nodeexist == 0)
				{
					CDbCommand del_res(conn);
					del_res.SetCommandText(" DELETE FROM TESTREEINFO_RES WHERE ACLID = @aclid " );
					del_res.Parameters.Set("aclid", aclid);
					del_res.ExecuteNonQuery();
					del_res.Close();					
				}
			}
		}
	}
	catch(  ApplicationException ex)
	{
		 doFlag = ex.GetCode();
		  
	}
	catch(  Exception ex)
	{
		 doFlag = -1;
		  
	}
	
	//EDLog(1, 1, "s.msg = [%s]", s.msg);
	 								
	bcls_ret->SetSYS(s);	
	return (doFlag);
}
