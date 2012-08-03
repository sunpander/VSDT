using System;
using System.Collections.Generic;
using System.Text;

namespace VSDT.Common.Utility
{
    /// <summary>
    /// 初次使用时,连接数据库后,创建表以及基础数据
    /// 0.连接数据库--
    /// 1.创建基础表
    /// 2.插入基础数据
    /// </summary>
    class DatabaseSql
    {
        public enum DataBaseObject
        {
            TI18NMODULE,//模块表

            TI18NCATALOG,//资源包表
            TI18NRESCATALOG, //资源

            TI18NCULTURE,//语言表

            TI18NLOCAL,//资源表
      
            TI18NRES, //资源等级关系表
            TI18NRESLEVEL,//等级表(提示,错误,警告等)--level表

            TI18NRESKIND,//类型(前台,后台,通用)

            TI18NUSERINFO,//用户信息
            TI18NUSERMODULE,//用户与可访问模块
            TI18NLOGEVENT,//日志表

            VI18NRESOURCES, //视图 
            TI18NANALYZERESULT, //自动分析结果
            TI18NANALYZELOCAL //自动分析结果以及local表关系表

        }
        /// <summary>
        /// 创建数据库对象
        /// </summary>
        /// <param name="objName"></param>
        /// <returns></returns>
        public string GetCreateTableSql(DataBaseObject objName)
        {
            string createSql = "";
            switch (objName)
            {
                case DataBaseObject.TI18NMODULE:
                    createSql = "CREATE TABLE  TI18NMODULE "
                                + "( "
                                + "  NAME         VARCHAR(31)   NOT NULL,"
                                + "  DESCRIPTION  VARCHAR(127)  NOT NULL,"
                                + "  NEXT_SEQ     INTEGER    DEFAULT 0   NOT NULL "
                                + " )";
                    break;
                case DataBaseObject.TI18NCATALOG:
                    createSql = "CREATE TABLE  TI18NCATALOG "
                                + "( "
                                + "  ID           CHAR(50)   NOT NULL,"
                                + "  MODULE       VARCHAR(31)    NOT NULL,"
                                + "  NAME         VARCHAR(31)    NOT NULL,"
                                + "  KIND         CHAR(1 BYTE)    NOT NULL,"
                                + "  DESCRIPTION  VARCHAR(127),"
                                + "  NEXT_SEQ     INTEGER    DEFAULT 0     NOT NULL"
                                + ")";
                    break;
                case DataBaseObject.TI18NCULTURE:
                    createSql = " CREATE TABLE  TI18NCULTURE"
                                + "("
                                + "   NAME         VARCHAR(31)                NOT NULL,"
                                + "   DESCRIPTION  VARCHAR(63)                DEFAULT ' '                   NOT NULL,"
                                + "   ISDEFAULT    CHAR(1)                     DEFAULT 0                     NOT NULL,"
                                + "   ICUNAME      VARCHAR(31)                DEFAULT ' '                   NOT NULL"
                                + " )";
                    break;
                case DataBaseObject.TI18NRESKIND:
                    createSql = " CREATE TABLE  TI18NRESKIND"
                                + " ("
                                + "   NAME         CHAR(1 BYTE),"
                                + "   DESCRIPTION  VARCHAR2(63 BYTE)"
                                + " )";
                    break;
                case DataBaseObject.TI18NRESLEVEL:
                    createSql = " CREATE TABLE  TI18NRESLEVEL"
                                + " ("
                                + "   NAME         VARCHAR2(30 BYTE)    NOT NULL,"
                                + "   DESCRIPTION  VARCHAR2(100 BYTE)"
                                + " )";
                    break;
                //资源表
                case DataBaseObject.TI18NLOCAL:
                    createSql = " CREATE TABLE  TI18NLOCAL"
                                + " ("
                                + "   ID       CHAR(12)    NOT NULL,"
                                + "   CULTURE  VARCHAR(31)    NOT NULL,"
                                + "   CONTENT  VARCHAR(4000)   NOT NULL"
                                + " )";
                    break;

                //关系表(资源--等级)
                case DataBaseObject.TI18NRES:
                    createSql =  " CREATE TABLE  TI18NRES"
                                +" ("
                                +"   ID       CHAR(12 BYTE)                        NOT NULL,"
                                +"   \"LEVEL\"  VARCHAR(30 BYTE)"
                                +" )";
                    break;
                //关系表(资源--资源包)
                case DataBaseObject.TI18NRESCATALOG:
                    createSql = " CREATE TABLE  TI18NRESCATALOG"
                                +" ("
                                +"   CID  CHAR(50 BYTE),"
                                +"   RID  CHAR(12 BYTE)"
                                +" )";
                    break;

                    //用户
                case DataBaseObject.TI18NUSERINFO:
                    createSql = " CREATE TABLE  TI18NUSERINFO"
                                + " ("
                                + "   ID        VARCHAR2(50 BYTE)                   DEFAULT 0                     NOT NULL,"
                                + "   ENAME     VARCHAR2(18 BYTE)                   DEFAULT ' ',"
                                + "   CNAME     VARCHAR2(50 BYTE)                   DEFAULT ' ',"
                                + "   PASSWORD  VARCHAR2(50 BYTE)                   DEFAULT ' ',"
                                + "   ISADMIN   CHAR(1 BYTE)                        DEFAULT '0'"
                                + " )";
                    break;
                case DataBaseObject.TI18NUSERMODULE:
                    createSql = " CREATE TABLE  TI18NUSERMODULE"
                                + " ("
                                + "  USERID  VARCHAR2(50 BYTE),"
                                + "  NAME    VARCHAR2(31 BYTE)"
                                + " )";
                    break;

                    //日志
                case DataBaseObject.TI18NLOGEVENT:
                    createSql = " CREATE TABLE  TI18NLOGEVENT"
                                + " ("
                                + "   ID             VARCHAR(50)              NOT NULL,"
                                + "   LOG_TIME       VARCHAR(16),"
                                + "   LOG_USERID     VARCHAR(50),"
                                + "   LOG_USERCNAME  VARCHAR(50),"
                                + "   LOG_MESSAGE    VARCHAR(500)"
                                + " )";
                    break;

                case DataBaseObject.VI18NRESOURCES:
                    createSql = " CREATE   VIEW  VI18NRESOURCES"
                                +" ("
                                +"    ID,"
                                +"    CATALOGID,"
                                +"    MODULE,"
                                +"    CATALOGNAME,"
                                +"    KINDID,"
                                +"    KINDNAME,"
                                +"    DESCRIPTION,"
                                +"    RESKEY,"
                                +"    CULTUREID,"
                                +"    CULTURENAME,"
                                +"    RESCONTENT,"
                                +"    LEVELID,"
                                +"    LEVELNAME"
                                +" )"
                                +" AS"
                                +"    SELECT DISTINCT ti18nlocal.ID || '_' || ti18nlocal.CULTURE AS ID,"
                                +"                    ti18ncatalog.ID AS catalogID,"
                                +"                    ti18ncatalog.MODULE AS module,"
                                +"                    ti18ncatalog.NAME AS catalogName,"
                                +"                    ti18ncatalog.KIND AS kindID,"
                                +"                    ti18nreskind.description AS kindName,"
                                +"                    ti18ncatalog.DESCRIPTION,"
                                +"                    ti18nlocal.ID AS reskey,"
                                +"                    ti18nlocal.CULTURE AS cultureID,"
                                +"                    ti18nculture.name AS cultureNAME,"
                                +"                    ti18nlocal.CONTENT AS rescontent,"
                                +"                    ti18nres.\"LEVEL\" AS levelID,"
                                +"                    ti18nreslevel.description AS levelName"
                                +"      FROM ti18ncatalog,"
                                +"           ti18nlocal,"
                                +"           ti18nres,"
                                +"           ti18nrescatalog,"
                                +"           ti18nreslevel,"
                                +"           ti18nreskind,"
                                +"           ti18nculture"
                                +"     WHERE     ti18nlocal.id = ti18nres.id"
                                +"           AND ti18nlocal.id = ti18nrescatalog.rid"
                                +"           AND ti18nrescatalog.cid = ti18ncatalog.id"
                                +"           AND ti18ncatalog.KIND = ti18nreskind.name"
                                +"           AND ti18nlocal.culture = ti18nculture.name"
                                +"           AND ti18nres.\"LEVEL\" = ti18nreslevel.name;";
                    break;
                case DataBaseObject.TI18NANALYZERESULT:
                    createSql = " CREATE TABLE  TI18NANALYZERESULT"
                                + "("
                                + "  ID                VARCHAR(50)           NOT NULL,"
                                + "  ITEMNAME          VARCHAR(100)          DEFAULT '  ',"
                                + " ITEMTYPE          VARCHAR(2)            DEFAULT '  ',"
                                + "  ITEMRELATIVEPATH  VARCHAR(200)          DEFAULT '  ',"
                                + " PROJECT           VARCHAR(50)           DEFAULT 'XXXX',"
                                + "  RESNAME           VARCHAR(100)          DEFAULT ' ',"
                                + "  RESVALUE          VARCHAR(4000)         DEFAULT ' ',"
                                + "  ROWINDEXINITEM    NUMBER                      DEFAULT -1,"
                                + "  CULTURE           VARCHAR(31)           DEFAULT 'zh-CHS',"
                                + " GRIDNAME          VARCHAR(50)           DEFAULT ' '"
                                + ")";
                    break;
                case DataBaseObject.TI18NANALYZELOCAL:
                    createSql="CREATE TABLE  TI18NANALYZELOCAL"
                                +"("
                                +"  ID                VARCHAR(50)           NOT NULL,"
                                +" keyTo          VARCHAR(12)          DEFAULT '  ',"
                                +"  resContent          VARCHAR(4000)            DEFAULT '  ' "
                                +")";
                    break;

            }
            return createSql;
        }
    }
}
/*****************************************
CREATE TABLE  TI18NMODULE
(
  NAME         VARCHAR(31)                NOT NULL,
  DESCRIPTION  VARCHAR(127)               NOT NULL,
  NEXT_SEQ     INTEGER              DEFAULT 0                     NOT NULL
)

CREATE TABLE  TI18NCATALOG
(
  ID           CHAR(50)                    NOT NULL,
  MODULE       VARCHAR(31)                NOT NULL,
  NAME         VARCHAR(31)                NOT NULL,
  KIND         CHAR(1 BYTE)                     NOT NULL,
  DESCRIPTION  VARCHAR(127),
  NEXT_SEQ     INTEGER                          DEFAULT 0                     NOT NULL
)


CREATE TABLE  TI18NCULTURE
(
  NAME         VARCHAR(31)                NOT NULL,
  DESCRIPTION  VARCHAR(63)                DEFAULT ' '                   NOT NULL,
  ISDEFAULT    CHAR(1)                     DEFAULT 0                     NOT NULL,
  ICUNAME      VARCHAR(31)                DEFAULT ' '                   NOT NULL
)

CREATE TABLE  TI18NLOCAL
(
  ID       CHAR(12)                        NOT NULL,
  CULTURE  VARCHAR(31)                    NOT NULL,
  CONTENT  VARCHAR(4000)                  NOT NULL
)

CREATE TABLE  TI18NLOGEVENT
(
  ID             VARCHAR(50)              NOT NULL,
  LOG_TIME       VARCHAR(16),
  LOG_USERID     VARCHAR(50),
  LOG_USERCNAME  VARCHAR(50),
  LOG_MESSAGE    VARCHAR(500)
)
CREATE TABLE  TI18NRES
(
  ID       CHAR(12 BYTE)                        NOT NULL,
  "LEVEL"  VARCHAR(30 BYTE)
)

CREATE TABLE  TI18NRESCATALOG
(
  CID  CHAR(50 BYTE),
  RID  CHAR(12 BYTE)
)

CREATE TABLE  TI18NRESKIND
(
  NAME         CHAR(1 BYTE),
  DESCRIPTION  VARCHAR2(63 BYTE)
)

CREATE TABLE  TI18NRESLEVEL
(
  NAME         VARCHAR2(30 BYTE)                NOT NULL,
  DESCRIPTION  VARCHAR2(100 BYTE)
)

CREATE TABLE  TI18NUSERINFO
(
  ID        VARCHAR2(50 BYTE)                   DEFAULT 0                     NOT NULL,
  ENAME     VARCHAR2(18 BYTE)                   DEFAULT ' ',
  CNAME     VARCHAR2(50 BYTE)                   DEFAULT ' ',
  PASSWORD  VARCHAR2(50 BYTE)                   DEFAULT ' ',
  ISADMIN   CHAR(1 BYTE)                        DEFAULT '0'
)

CREATE TABLE  TI18NUSERMODULE
(
  USERID  VARCHAR2(50 BYTE),
  NAME    VARCHAR2(31 BYTE)
)

CREATE   VIEW  VI18NRESOURCES
(
   ID,
   CATALOGID,
   MODULE,
   CATALOGNAME,
   KINDID,
   KINDNAME,
   DESCRIPTION,
   RESKEY,
   CULTUREID,
   CULTURENAME,
   RESCONTENT,
   LEVELID,
   LEVELNAME
)
AS
   SELECT DISTINCT ti18nlocal.ID || '_' || ti18nlocal.CULTURE AS ID,
                   ti18ncatalog.ID AS catalogID,
                   ti18ncatalog.MODULE AS module,
                   ti18ncatalog.NAME AS catalogName,
                   ti18ncatalog.KIND AS kindID,
                   ti18nreskind.description AS kindName,
                   ti18ncatalog.DESCRIPTION,
                   ti18nlocal.ID AS reskey,
                   ti18nlocal.CULTURE AS cultureID,
                   ti18nculture.name AS cultureNAME,
                   ti18nlocal.CONTENT AS rescontent,
                   ti18nres."LEVEL" AS levelID,
                   ti18nreslevel.description AS levelName
     FROM ti18ncatalog,
          ti18nlocal,
          ti18nres,
          ti18nrescatalog,
          ti18nreslevel,
          ti18nreskind,
          ti18nculture
    WHERE     ti18nlocal.id = ti18nres.id
          AND ti18nlocal.id = ti18nrescatalog.rid
          AND ti18nrescatalog.cid = ti18ncatalog.id
          AND ti18ncatalog.KIND = ti18nreskind.name
          AND ti18nlocal.culture = ti18nculture.name
          AND ti18nres."LEVEL" = ti18nreslevel.name;



******************************************/

/**  把一个模块的内容移植到另一个模块
select * from (
select    'PS00'||substr( id,5,8) as  tte, "LEVEL"  from ti18nres where id like 'PSHR'||'%' order by id asc) dd2,
 
 
(select * from ti18nres where id like 'PS00'||'%' order by id asc) dd3  where   dd2. tte =dd3.id
 
select  'PS00'||substr( id,5,8),culture,content from ti18nlocal where id like 'PSHR'||'%' order by id asc
 

 
select * from ti18nres where id  like 'PS00'||'%'
insert into  ti18nres  (  select    'PS00'||substr( id,5,8), "LEVEL",remark  from ti18nres where id like 'PSHR'||'%' )
select * from ti18nres where id  like 'PS00'||'%' order by id asc
 
select * from ti18nlocal where id  like 'PS00'||'%' order by id asc
insert into  ti18nlocal    ( select  'PS00'||substr( id,5,8),culture,content from ti18nlocal where id like 'PSHR'||'%' )
select * from ti18nlocal where id  like 'PS00'||'%' order by id asc
   
select * from ti18nrescatalog where rid  like 'PS00'||'%'
insert into  ti18nrescatalog(cid,rid)   (  select cid,'PS00'||substr(rid,5,8)  from ti18nrescatalog where rid like 'PSHR'||'%'  )
select * from ti18nrescatalog where rid  like 'PS00'||'%' order by rid asc
 

select * from ti18ncatalog where module = 'PSHR' and kind = 'C'
select * from ti18ncatalog where module = 'PSHR' and kind = 'S'
select * from ti18ncatalog where module = 'PSHR' and kind = '0'
    
select * from ti18ncatalog where module = 'PS00' and kind = 'C' and  rownum=1
select * from ti18ncatalog where module = 'PS00' and kind = 'S'
select * from ti18ncatalog where module = 'PS00' and kind = '0'

update ti18nrescatalog set cid = '6b9704e7-41bc-4f3d-945c-7d1669e361ce              '    where  rid like 'PS00C'||'%'  and rid > 'PS00C0000002'  
update ti18nrescatalog set cid = '0c1486dc-fec0-42f3-8f5c-dcaf40fc3f4f'    where  rid like 'PS00S'||'%'  and rid > 'PS00C0000002'  
 
select * from ti18nrescatalog,ti18ncatalog  where rid  like 'PS00'||'%'  and module = 'PS00' and kind = 'S'  and ti18ncatalog.id = ti18nrescatalog.cid
  
 
select * from ti18nrescatalog,ti18ncatalog  where rid  like 'PS00'||'%'  and module = 'PS00' and kind = 'C'  and ti18ncatalog.id = ti18nrescatalog.cid
  
select max(id)from ti18nres where id like 'PS00C'||'%'
select max(id)from ti18nres where id like 'PS00S'||'%'
update ti18ncatalog set next_seq ='47' where module = 'PS00' and kind = 'C' and  rownum=1
update ti18ncatalog set next_seq ='45' where module = 'PS00' and kind = 'S' and  rownum=1
***/
/************  清空某一模块下信息
select max(id)from ti18nres where id like 'GCRSC'||'%'
select max(id)from ti18nres where id like 'GCRSS'||'%'

select * from ti18nres where  id not  like 'MMTR'||'%'
select * from ti18nlocal where id not  like 'MMTR'||'%'
select * from ti18nrescatalog where rid like 'MMTR'||'%'
select * from ti18ncatalog where name like 'MMTR'||'%'
select * from ti18nmodule where  name   like 'MMTR'||'%'

select * from vi18nresources where module like 'MMTR'||'%'
 
--PSTR MMTR YMTR 3

delete  from ti18nres where id  like 'YMTR'||'%';
delete from ti18nlocal where id like 'YMTR'||'%';
delete  from ti18nrescatalog where rid like 'YMTR'||'%';
delete from ti18ncatalog where name like 'YMTR'||'%';
delete from ti18nmodule where  name   like 'YMTR'||'%';

update ti18ncatalog set next_seq = 0 where  name like 'PSTR'||'%'
**************/