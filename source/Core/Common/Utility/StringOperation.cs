using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;

namespace Baosight.iPlat4CToolkit.I18NResourceManagement.Utility
{
    public class StringOperation
    {
        public StringOperation()
        {
        }
        #region 
        public static string ToDBC(string input)
        {
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                //全角空格为12288  
                if (c[i] == 12288)
                {
                    //半角空格为32  
                    c[i] = (char)32;
                    continue;
                }
                //半角(33-126)与全角(65281-65374)的对应关系是：相差65248  
                if (c[i] > 65280 && c[i] < 65375)
                    c[i] = (char)(c[i] - 65248);
            }
            return new string(c);
        }
        #endregion
        #region 截取自定义字符串
        public string GetSTR(string zzExpress, string codePield)
        {
            string name;
            Regex r = new Regex(zzExpress, RegexOptions.Compiled);
            name = r.Match(codePield).Value;
            return name;
        }
        #endregion

        #region 截取自定义字符串(不区分大小写)
        public string GetNoDXSTR(string zzExpress, string codePield)
        {
            string name;
            Regex r = new Regex(zzExpress, RegexOptions.IgnoreCase);
            name = r.Match(codePield).Value.Trim();
            return name;
        }
        #endregion

        #region 截取自定义字符串(不区分大小写单行模式)
        public string GetNoDXSingleSTR(string zzExpress, string codePield)
        {
            string name;
            Regex r = new Regex(zzExpress, RegexOptions.IgnoreCase | RegexOptions.Singleline);
            name = r.Match(codePield).Value;
            return name;
        }
        #endregion

        #region 获取所需查找字符串位置(从左往右)
        public int GetSTRPoint(string zzExpress, string codePield)
        {
            int point;
            Regex r = new Regex(zzExpress, RegexOptions.IgnoreCase | RegexOptions.Singleline);
            point = r.Match(codePield).Index;
            return point;
        }
        #endregion

        #region 获取所需查找字符串位置(从右往左)
        public int GetSTRRightPoint(string zzExpress, string codePield)
        {
            int point;
            Regex r = new Regex(zzExpress, RegexOptions.RightToLeft);
            point = r.Match(codePield).Index;
            return point;
        }
        #endregion

        #region 判断字符串是否存在
        public bool CheckPoint(string condition, string codePied)
        {
            bool IsMatch;
            Regex r = new Regex(condition, RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline);
            IsMatch = r.IsMatch(codePied);
            return IsMatch;
        }
        #endregion

        #region 获取语句的变量专用
        public Hashtable GetSQLVarriable(string ruleSTR, string sqlSTR)
        {
            Hashtable varTable = new Hashtable();
            Regex r = new Regex(ruleSTR, RegexOptions.Compiled);
            varTable.Clear();
            foreach (Match m in r.Matches(sqlSTR))
            {
                string varriable = m.Value.Substring(1, m.Value.Length - 1).Trim();
                if (varTable[varriable] == null)
                {
                    varTable.Add(varriable, varriable);
                }
            }
            return varTable;
        }
        #endregion

        #region 获取语句的变量专用(list)
        public List<string> GetSqlVarriableList(string zzExpress, string codePield)
        {
            List<string> varriableList = new List<string>();
            Regex r = new Regex(zzExpress, RegexOptions.Compiled);
            varriableList.Clear();
            foreach (Match m in r.Matches(codePield))
            {
                string varriable = m.Value.Substring(1, m.Value.Length - 1).Trim();
                if (!varriableList.Contains(varriable))
                {
                    varriableList.Add(varriable);
                }
            }
            return varriableList;
        }
        #endregion

        #region 统计设定字符数量
        public int GetSetWordCount(string condition, string codePield)
        {
            int count = 0;
            Regex r = new Regex(condition, RegexOptions.Compiled);

            count = r.Matches(codePield).Count;
            return count;
        }
        #endregion

        #region 获取语句集合
        public ArrayList GetSTRContent(string ruleSTR, string sqlSTR)
        {
            ArrayList varTable = new ArrayList();
            Regex r = new Regex(ruleSTR, RegexOptions.IgnoreCase | RegexOptions.Singleline);
            varTable.Clear();
            foreach (Match m in r.Matches(sqlSTR))
            {
                string varriable = (string)m.Value;
                varTable.Add(varriable);
            }
            return varTable;
        }
        #endregion

        #region 替换代码
        public string ReplaceSTR(string codePield, string zzExpress, string newCode)
        {
            string newCodePield = "";
            newCodePield = Regex.Replace(codePield, zzExpress, newCode, RegexOptions.Compiled);
            return newCodePield;
        }
        #endregion

        #region 替换字符不区分大小写
        public string ReplaceDXSTR(string codePield, string zzExpress, string newCode)
        {
            string newCodePield = "";
            newCodePield = Regex.Replace(codePield, zzExpress, newCode, RegexOptions.IgnoreCase);
            return newCodePield;
        }
        #endregion

        #region 获取查找Match集合
        public Match GetSTRMatch(string zzExpress, string codePield)
        {
            Regex r = new Regex(zzExpress, RegexOptions.IgnoreCase | RegexOptions.Singleline);
            Match myMatch = r.Match(codePield);
            return myMatch;
        }
        #endregion

        #region 获取GUID
        public string GetGUID()
        {
            string myGuid = "";
            Guid guid = Guid.NewGuid();
            myGuid = guid.ToString();
            return myGuid;
        }
        #endregion
    }
}