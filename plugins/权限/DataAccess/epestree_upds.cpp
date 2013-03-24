 
 
 
 


 


int f_epestree_upds(DataSet bcls_rec, DataSet bcls_ret,InterfaceDataBase  conn) 
{
	/*程序用变量*/
	int doFlag = 0;
	int i;

	char name[128] = "";
	char treeseq[4] = "";
	char app[18] = "";
	
	try
	{
		bcls_rec->GetColVal(2,1,"cursystem",app);
		EDLog(1,1,"APPNAME : %s",app);

		//对输入信息循环处理
		for (i = 1; i <= bcls_rec->GetRow(1); i++ )
		{
			bcls_rec->GetColVal(1, i, "name",name);
			bcls_rec->GetColVal(1, i, "treeseq",treeseq);
			EDLog(1,1,"name : %s",name);
			EDLog(1,1,"treeseq : %s",treeseq);		

			CDbCommand cmd_tmp(conn);
			cmd_tmp.SetCommandText( " UPDATE TESTREEINFO "
											 " SET TREESEQ = @treeseq "
											 " WHERE NAME = @name "
											 " AND APPNAME = @app " );
			
			cmd_tmp.Parameters.Set("treeseq", treeseq);
			cmd_tmp.Parameters.Set("name", name);
			cmd_tmp.Parameters.Set("app", app);
			
			cmd_tmp.ExecuteNonQuery();
			
			cmd_tmp.Close();

			Log::Info("EPES", "tree", _RES("EPESS0000109")/*菜单树节点[{4}]更新次序*/, "ORD_TREE", "", "", "", name);			
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
	
	//EDLog(1, 1, (  char*)"s.msg = [%s]", s.msg);
	 								
	bcls_ret->SetSYS(s);	
	return (doFlag);
}
