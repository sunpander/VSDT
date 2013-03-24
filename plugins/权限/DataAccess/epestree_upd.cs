 
 
 
 


BM2F_ENTERACE(epestree_upd)


int f_epestree_upd(DataSet bcls_rec, DataSet bcls_ret,InterfaceDataBase  conn) 
{
	/*程序用变量*/
	int doFlag = 0;

	char name[128] = "";
	char fname[128] = "";
	char description[128] = "";
	char appname[18] = "";
	char aclid[20] = "";

	try
	{
		bcls_rec->GetSYS(&s);

		bcls_rec->GetColVal(1, 1, "name",		name);
		bcls_rec->GetColVal(1, 1, "fname",		fname);
		bcls_rec->GetColVal(1, 1, "description",description);
		bcls_rec->GetColVal(1, 1, "appname",	appname);

		EDLog(1,1,"name : [%s], fname:[%s], desc:[%s]",name, fname, description);	

		//修改treeinfo
		CDbCommand cmd_tmp(conn);
		cmd_tmp.SetCommandText( " UPDATE TESTREEINFO "
								" SET DESCRIPTION = @description "
								" WHERE NAME = @name "
								" AND FNAME = @fname"
								" AND APPNAME = @appname " );


		cmd_tmp.Parameters.Set("description", description);
		cmd_tmp.Parameters.Set("name", name);
		cmd_tmp.Parameters.Set("fname", fname);
		cmd_tmp.Parameters.Set("appname", appname);

		cmd_tmp.ExecuteNonQuery();
		cmd_tmp.Close();

		//查询节点aclid
		CDbCommand id_inq(conn);
		id_inq.SetCommandText("SELECT ACLID FROM TESTREEINFO WHERE NAME = @name AND FNAME = @fname AND APPNAME = @appname ");
		id_inq.Parameters.Set("name", name);
		id_inq.Parameters.Set("fname", fname);
		id_inq.Parameters.Set("appname", appname);
		id_inq.ExecuteReader();
		id_inq.Read();
		id_inq.Get(1, aclid);
		id_inq.Close();

		//修改资源表
		CDbCommand res_upd(conn);
		res_upd.SetCommandText(" UPDATE TESTREEINFO_RES "
								" SET DESCRIPTION = @description "
								" WHERE ACLID = @aclid "
								" AND CULTURE = @culture");
		res_upd.Parameters.Set("description", description);
		res_upd.Parameters.Set("aclid", aclid);
		res_upd.Parameters.Set("culture", s.culture);
		res_upd.ExecuteNonQuery();
		res_upd.Close();

		Log::Info("EPES", "tree", _RES("EPESS0000110")/*更新菜单树节点[{4}]*/, "UPD_TREE", "", "", "", name);
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
