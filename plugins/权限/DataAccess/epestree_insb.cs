 

int EPGetNextSeq(  string & seq_name, long& seq,InterfaceDataBase  conn) ;

int f_epestree_insb(DataSetbcls_rec,DataSetbcls_ret,InterfaceDataBase  conn);


BM2F_ENTERACE(epestree_insb)


int f_epestree_insb(DataSet bcls_rec, DataSet bcls_ret,InterfaceDataBase  conn) 
{
	char FunctionEname[31]="estreeinfo_insb";	
	//EDLog(1, 1, " **************%s begin*****************",FunctionEname);

	int doFlag = 0;
	int i;
	char 	father[128+1] = "";
	char 	nodename[128+1] = "";
	int 	circleFlag = 0;
	int 	iCount = 0;
	long 	aclid;
	char 	cursystem[30]="";
	char cul_name[33] = "";
	char desc[501] = "";
	int rescount = 0;

	#include "testreeinfo.h"

	try
	{
		bcls_rec->GetSYS(&s);

		/* ***** 获取输入参数 ***** */
		bcls_rec->GetColVal(2, 1, 1,father);
		bcls_rec->GetColVal(2, 1, 2,cursystem);

		//EDLog(1, 1, "[estreeinfo_insb]  appname [%s] father [%s]", cursystem, father);

		/* ***** 程序处理 ***** */
		for (i = 1; i <= bcls_rec->GetRow(1); i++ ) 
		{
			//取得单行传入信息
			bcls_rec->GetColVal(1, i, (T_INFO *)&testreeinfo_info);
			//EDLog(1,1,"testreeinfo.name [%s]",testreeinfo.name);
			strcpy(nodename, testreeinfo.name);
			
			{
				CDbCommand cmd_tmp(conn);
				switch(conn->DatabaseKind)
				{
				case DB_KIND_MSSQL:
					cmd_tmp.SetCommandText(
											" WITH TREENODE(name, fname) AS (SELECT   NAME, FNAME"
											"  FROM      TESTREEINFO"
											"    WHERE   (NAME IN"
											"     (SELECT   FNAME"
											"       FROM      TESTREEINFO AS TESTREEINFO_2"
											"        WHERE   (NAME = @father)))"
											"        UNION ALL"
											"        SELECT   np1.NAME, np1.FNAME"
											"        FROM      TREENODE AS n CROSS JOIN"
											"        TESTREEINFO AS np1"
											"        WHERE   (n.fname = np1.NAME))"
											"    SELECT   COUNT(*) AS Expr1"
											"    FROM      (SELECT DISTINCT fname AS node_name"
											"                     FROM      TREENODE AS TREENODE_1"
											"                     UNION ALL"
											"                     SELECT   FNAME AS node_name"
											"                     FROM      TESTREEINFO AS TESTREEINFO_1"
											"                     WHERE   (NAME = @father)) AS derivedtbl_1"
											"    WHERE   (node_name = @nodename) ");
					break;
				case DB_KIND_DB2:
					cmd_tmp.SetCommandText("  WITH TREENODE(name, fname) AS  "
											"  (  "
											" 		SELECT	name, fname FROM	TESTREEINFO  "
											" 		WHERE	name	IN	( SELECT	fname FROM	TESTREEINFO WHERE	name = @father ) "
											" 		UNION ALL  "
											"		SELECT	np1.name, np1.fname  "
											" 		FROM	 TREENODE n, TESTREEINFO np1  "
											" 		WHERE  	n.fname	= np1.name   "
											"  )  "
											"  SELECT COUNT(*) FROM ("
											"  SELECT distinct fname AS node_name FROM	TREENODE  "
											"  UNION ALL  "
											"  SELECT FNAME AS node_name FROM TESTREEINFO WHERE NAME = @father )"
											"  WHERE node_name = @nodename ");
					break;
				case DB_KIND_ORACLE:
				default:
					cmd_tmp.SetCommandText(" SELECT COUNT(*) FROM " 
											" (	SELECT FNAME FROM TESTREEINFO CONNECT BY PRIOR FNAME = NAME " 
											" START WITH NAME = @father " 
											" ) " 
											" WHERE FNAME = @nodename " );
					break;
				}
				cmd_tmp.Parameters.Set("father", father);
				cmd_tmp.Parameters.Set("nodename", nodename);
				cmd_tmp.ExecuteReader();
				if(cmd_tmp.Read())
				{
					cmd_tmp.Get(1,circleFlag);
				}
				cmd_tmp.Close();
			}
			if(strcmp(father, nodename) == 0 || circleFlag > 0)
			{
				throw CApplicationException(-1, _RES("EPESS0000030")/*新增失败,输入的节点名与父节点名重复，将形成节点环*/, s.msg);
			}

			{
				CDbCommand cmd_tmp(conn);
				cmd_tmp.SetCommandText(" SELECT COUNT(*) FROM TESTREEINFO WHERE NAME = @nodename AND APPNAME = @cursystem ");
				cmd_tmp.Parameters.Set("nodename", nodename);
				cmd_tmp.Parameters.Set("cursystem", cursystem);
				cmd_tmp.ExecuteReader();
				if(cmd_tmp.Read())
				{
					cmd_tmp.Get(1,iCount);
				}
				cmd_tmp.Close();
			}
			if(iCount > 0)
			{
				throw CApplicationException(-1, _RES("EPESS0000031")/*新增失败,输入的节点名已存在*/, s.svc_name);			
			}
			if ( strcmp(testreeinfo.resname,"FOLDER") == 0 )
			{
				char seq_name[100] = "ES_OBJ_SEQNO";
				EPGetNextSeq(seq_name,aclid,conn);
				testreeinfo.aclid  = aclid +8000000;
			}
			else
			{
				{
					CDbCommand cmd_tmp(conn);
					switch(conn->DatabaseKind)
					{
					case DB_KIND_MSSQL:
						cmd_tmp.SetCommandText(" SELECT top 1 ACLID " 
							" FROM TESFORMRESINFO " 
							" WHERE NAME = @testreeinfo.resname " 
							" AND APPNAME = @cursystem " 
							);
						break;
					case DB_KIND_DB2:
						cmd_tmp.SetCommandText(" SELECT ACLID " 
							" FROM TESFORMRESINFO " 
							" WHERE NAME = @testreeinfo.resname " 
							" AND APPNAME = @cursystem " 
							" FETCH FIRST 1 ROWS ONLY " 
							);
						break;
					case DB_KIND_ORACLE:
					default:
						cmd_tmp.SetCommandText(" SELECT ACLID " 
							" FROM TESFORMRESINFO " 
							" WHERE NAME = @testreeinfo.resname " 
							" AND APPNAME = @cursystem " 
							" AND ROWNUM = 1 " 
							);
						break;
					}
					cmd_tmp.Parameters.Set("testreeinfo.resname", testreeinfo.resname);
					cmd_tmp.Parameters.Set("cursystem", cursystem);
					cmd_tmp.ExecuteReader();
					if(cmd_tmp.Read())
					{
						cmd_tmp.Get(1,aclid);
					}
					cmd_tmp.Close();
				}
				testreeinfo.aclid  = aclid ;
			}
			strcpy(testreeinfo.appname,cursystem);
			EDLog(1,1,"name [%s]  appname[%s]",testreeinfo.name,testreeinfo.appname);

			{
				CDbCommand cmd_tmp(conn);
				cmd_tmp.SetCommandText(" INSERT INTO TESTREEINFO(name, " 
											" fname, " 
											" description, " 
											" shortcut, " 
											" aclid, " 
											" resname, " 
											" treeno, " 
											" treeseq, " 
											" appname " 
											" ) " 
											" VALUES (@testreeinfo.name, " 
											" @testreeinfo.fname, " 
											" @testreeinfo.description, " 
											" @testreeinfo.shortcut, " 
											" @testreeinfo.aclid, " 
											" @testreeinfo.resname, " 
											" @testreeinfo.treeno, " 
											" @testreeinfo.treeseq, " 
											" @testreeinfo.appname " 
											" ) " );
				cmd_tmp.Parameters.Set("testreeinfo.name", testreeinfo.name);
				cmd_tmp.Parameters.Set("testreeinfo.fname", testreeinfo.fname);
				cmd_tmp.Parameters.Set("testreeinfo.description", testreeinfo.description);
				cmd_tmp.Parameters.Set("testreeinfo.shortcut", 0);
				cmd_tmp.Parameters.Set("testreeinfo.aclid", testreeinfo.aclid);
				cmd_tmp.Parameters.Set("testreeinfo.resname", testreeinfo.resname);
				cmd_tmp.Parameters.Set("testreeinfo.treeno", testreeinfo.treeno);
				cmd_tmp.Parameters.Set("testreeinfo.treeseq", testreeinfo.treeseq);
				cmd_tmp.Parameters.Set("testreeinfo.appname", testreeinfo.appname);cmd_tmp.ExecuteNonQuery();cmd_tmp.Close();
			}

			//2011-7-26 YUXIUWEN 修改
			CDbCommand resexist(conn);
			resexist.SetCommandText("SELECT COUNT(*) FROM TESTREEINFO_RES WHERE ACLID = @aclid ");
			resexist.Parameters.Set("aclid", testreeinfo.aclid);
			resexist.ExecuteReader();
			if(resexist.Read())
			{
				resexist.Get(1, rescount);
			}
			resexist.Close();

			if(rescount == 0)
			{
				CDbCommand cul_info(conn);
				cul_info.SetCommandText(" SELECT ICUNAME FROM TI18NCULTURE ");
				cul_info.ExecuteReader();
				while(cul_info.Read())
				{
					cul_info.Get(1, cul_name);
					
					CDbCommand cmd_tmp(conn);
					cmd_tmp.SetCommandText( " INSERT INTO TESTREEINFO_RES( CULTURE, ACLID, DESCRIPTION) " 
												" VALUES (	@culture, " 
												" 			@aclid, " 
												" 			@description ) " );

					if(strcmp(s.culture, cul_name) != 0)	//非当前语言
					{
						strcpy(desc, "");
						if(strcmp(testreeinfo.resname,"FOLDER") == 0)
						{
							sprintf(desc, "~%s", testreeinfo.description);
						}
						else
						{
							CDbCommand res_inq(conn);
							res_inq.SetCommandText("SELECT DESCRIPTION FROM TESFORMRESINFO_RES "
								" WHERE ACLID = @aclid "
								" AND CULTURE = @culture ");
							res_inq.Parameters.Set("aclid", testreeinfo.aclid);
							res_inq.Parameters.Set("culture", cul_name);
							res_inq.ExecuteReader();
							if(res_inq.Read())
							{
								res_inq.Get(1, desc);
							}
							else
							{
								sprintf(desc, "~%s", testreeinfo.description);
							}
							res_inq.Close();
						}
					}
					else
					{
						strcpy(desc, testreeinfo.description);
					}
					cmd_tmp.Parameters.Set("description", desc);
					cmd_tmp.Parameters.Set("culture", cul_name);
					cmd_tmp.Parameters.Set("aclid", testreeinfo.aclid);
					
					cmd_tmp.ExecuteNonQuery();
					
					cmd_tmp.Close();
				}
				
				cul_info.Close();
			}
			Log::Info("EPES", "tree", _RES("EPESS0000111")/*新增菜单树节点[{4}]*/, "ADD_TREE", "", "", "", testreeinfo.name);
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
