BM2F_ENTERACE(epestree_inqb)


int f_epestree_inqb(DataSet bcls_rec, DataSet bcls_ret,InterfaceDataBase  conn) 
{
	//程序用变量
	int doFlag = 0;
	int fetchRowCount;

	char name[128+1] = "";
	char fname[128+1] = "";
	long treeno = -10;
	long mode = -10;
	char cursystem[20]="";

	//使用的表结构变量
	#include "testreeinfo.h"

	try
	{
		bcls_rec->GetSYS(&s);

		//获得输入参数
		bcls_rec->GetColVal(1, 1, 1,name);
		bcls_rec->GetColVal(1, 1, 2,fname);
		bcls_rec->GetColVal(1, 1, 3,&treeno);
		bcls_rec->GetColVal(1, 1, 4,&mode);
		bcls_rec->GetColVal(1, 1, 5,cursystem);

		EDLog(1,1,"-------------%s %s %d %d",name,fname,treeno,mode);

		CDbCommand testreeinfo_2(conn);
		switch(conn->DatabaseKind)
		{
			case DB_KIND_MSSQL:
				testreeinfo_2.SetCommandText(
					" SELECT TREE.NAME, TREE.FNAME, TREERES.DESCRIPTION, TREE.SHORTCUT, TREE.ACLID, TREE.RESNAME, TREE.TREENO, TREE.TREESEQ, TREE.APPNAME "
					" FROM TESTREEINFO TREE , TESTREEINFO_RES TREERES "
					" WHERE TREE.ACLID = TREERES.ACLID "
					" AND CULTURE = @culture "
					" AND FNAME  =  rtrim(@fname)  and appname = isnull(rtrim(@cursystem),appname) " 
					" ORDER BY treeseq ASC ");
				break;
			case DB_KIND_ORACLE:
			case DB_KIND_DB2:
			default:
				testreeinfo_2.SetCommandText(
					" SELECT TREE.NAME, TREE.FNAME, TREERES.DESCRIPTION, TREE.SHORTCUT, TREE.ACLID, TREE.RESNAME, TREE.TREENO, TREE.TREESEQ, TREE.APPNAME "
					" FROM TESTREEINFO TREE , TESTREEINFO_RES TREERES "
					" WHERE TREE.ACLID = TREERES.ACLID "
					" AND CULTURE = @culture "
					" AND FNAME  =  trimn(@fname)  and appname = nvl(trimn(@cursystem),appname) " 
					" ORDER BY treeseq ASC " );
			break;
		}

		EDLog(1,1,"cursor 2");
		// flag =  Eswritelog(bcls_rec , fname," " ," ","select testreeinfo", "0","根据fname、appname查询表testreeinfo");
		testreeinfo_2.Parameters.Set("fname", fname);
		testreeinfo_2.Parameters.Set("cursystem", cursystem);
		testreeinfo_2.Parameters.Set("culture", s.culture);
		testreeinfo_2.ExecuteReader(); 
		for (fetchRowCount = 0; ;) 
		{ 
			if(testreeinfo_2.Read())
			{
				EIDbMapping::Fetch((T_INFO*)&testreeinfo_info,testreeinfo_2);
			} 
			else break;

			fetchRowCount ++;
			bcls_ret->SetColVal(1, fetchRowCount, (T_INFO *)&testreeinfo_info);
		}
		testreeinfo_2.Close(); 
		
	}
	catch(  ApplicationException ex)
	{
		 doFlag = ex.GetCode();
		  
	}
	catch(  Exception ex)
	{
		 doFlag = -1;
		  
	}
	
	EDLog(1, 1, "s.msg = [%s]", s.msg);
	 								
	bcls_ret->SetSYS(s);	
	return (doFlag);
} 
