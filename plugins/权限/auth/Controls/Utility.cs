using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Resources;
using System.IO;

namespace EF
{
 
	public enum EFProdOpeationType
	{
		Plan,
		Command,
		Material
	}

	public class UniqueOpenedForms
	{
		private static Hashtable _forms = new Hashtable();
		public static bool Contains(string formName)
		{
			return _forms.ContainsKey(formName);
		}

		public static Form Get(string formName)
		{
			return (Form)_forms[formName];
		}

		public static void Add(string formName, Form form)
		{
			if(!_forms.ContainsKey(formName))
				_forms.Add(formName, form);			
		}

		public static void Remove(string formName)
		{
			if(_forms.ContainsKey(formName))
				_forms.Remove(formName);			
		}

	}

	/// <summary>
	/// 
	/// </summary>
	public class QueryConditionCollection : ArrayList
	{
		public int Add(string qc)
		{
			return this.Add((object)qc);
		}

		public string GetConditions(bool bWhere)
		{
			if(Count == 0)
				return string.Empty;
			
			string cond = string.Empty;
			if(bWhere)
				cond = " Where";
			else
				cond = " And";
			int i=0;
			foreach(string qc in this)
			{
				if(i++==0)
					cond += " "+qc;
				else
					cond += " AND "+qc;

			}
			return cond;

		}
	}

	/// <summary>
	/// 
	/// </summary>
	public struct QueryPagerInfo
	{
		public QueryPagerInfo(string _sql, int _start, int _count)
		{
			sql= _sql;
			start = _start;
			count = _count;
		}
		public string	sql;
		public int		start;
		public int		count;
	}
	/// <summary>
	/// Utility 的摘要说明。
	/// </summary>
	public class Utility
	{
 
 
		/// <summary>
		/// 读取指定清单资源中的指定键的值
		/// </summary>
		/// <param name="s_res_name">清单资源</param>
		/// <param name="s_key_name">键名</param>
		/// <returns>键值</returns>
		public static string ReadFromResource(System.Reflection.Assembly assemboy, string s_res_name , string s_key_name)
		{
			ResourceReader reader = new ResourceReader(assemboy.GetManifestResourceStream(s_res_name+".resources"));
			IDictionaryEnumerator en = reader.GetEnumerator();
			while(en.MoveNext())
			{
				if((string)en.Key == s_key_name)
				{
					return (string)en.Value;
				}
			}
			return string.Empty;
		}


		public static string ReadAndReplaceResource(System.Reflection.Assembly assemboy
			, string s_res_name
			, string s_key_name
			, string[] entries, bool ignoreCase)
		{
			string s = EF.Utility.ReadFromResource(assemboy, s_res_name, s_key_name);
			s = EF.Utility.BatchReplace(s, entries, ignoreCase);
			return s;
		}

		public static string BatchReplace(string s, string[] entries, bool ignoreCase)
		{
			if(entries.Length % 2 == 1)
				return s;

			int count = entries.Length / 2;
			System.Text.StringBuilder sb = null;
			if(ignoreCase)
			{
				sb = new System.Text.StringBuilder(s);
				for(int i=0; i<count; i++)
				{
					sb.Replace(entries[i*2], entries[i*2+1]);
				}
			}
			else
			{
				sb = new System.Text.StringBuilder(s);
				for(int i=0; i<count; i++)
				{
					sb.Replace(entries[i*2], entries[i*2+1]);
				}
			}
			return sb.ToString();
		}
  
		public static string GetQueryCountSql(string deatialSql)
		{
			int nBraces = 0;
			int nBrackets = 0;
			int nParentheses = 0;
			int nSingleQuotationMarks = 0;
			int nDoubleQuotationMarks = 0;

			string tmpDeatialSql = deatialSql.Trim().ToUpper();
			int nStartPos = 6;
			int nFromPos = 6;
			do
			{
				nFromPos = tmpDeatialSql.IndexOf("FROM", nStartPos);
				for(int i=nStartPos; i<nFromPos; i++)
				{
					switch(tmpDeatialSql[i])
					{
						case '{':
							nBraces++;
							break;
						case '}':
							nBraces--;
							break;
						case '[':
							nBrackets++;
							break;
						case ']':
							nBrackets--;
							break;
						case '(':
							nParentheses++;
							break;
						case ')':
							nParentheses--;
							break;
						case '\'':
							nSingleQuotationMarks = ~nSingleQuotationMarks;
							break;
						case '"':
							nDoubleQuotationMarks = ~nDoubleQuotationMarks;
							break;
					}					
				}
				if( nBraces*nBrackets*nParentheses*nSingleQuotationMarks*nDoubleQuotationMarks == 0)
				{
					return deatialSql.Substring(0, 7) + " COUNT(*) " + deatialSql.Substring(nFromPos);
				}
				nStartPos = nFromPos;
			}while(nFromPos>=0);
			
			return string.Empty;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="time"></param>
		/// <returns></returns>
		public static string GetProdShiftNo(DateTime time)
		{
			if(time.Hour < 8)
				return "1";
			else if(time.Hour < 16)
				return "2";
			else 
				return "3";
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="time"></param>
		/// <returns></returns>
		public static string GetProdShiftGroup(DateTime time)
		{
			TimeSpan ts = new TimeSpan(0);
			string sShiftNo = GetProdShiftNo(time);
			//假设2006-2-20 为连续两天的夜班丁组中的第一天
			if(sShiftNo == "1" || sShiftNo == "2")
			{
				ts = time-new DateTime(2006, 2, 20);
			}
			else if(sShiftNo =="3")
			{
				ts = time-new DateTime(2006, 2, 19);
			}

			int iShiftValue = (3-(int)Math.Floor((ts.TotalDays % 8)/2.0f)+(int)Math.Floor(time.Hour/8.0f))%4;
			char cShiftGroup = '1';
			cShiftGroup = (char)((int)cShiftGroup + iShiftValue); 

			return new string(cShiftGroup, 1);

		}
 
        /// <summary>
        /// 根据当前iPlat4C.xml中的字符编码获取字符串长度
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        static public int GetByteLength(string strValue)
        {
            //编码问题:
            //默认是gbk
            //当是gbk    时: 汉字对应2 位,其他为1位
            //当是unicode时: 全为2位
            //当是utf-8  时: 中文为3位,其他为1位
            System.Text.Encoding encoding = System.Text.Encoding.Default;
            try
            {
                encoding = System.Text.Encoding.GetEncoding("EC.ProjectConfig.Instance.CurrentServers[0].CharSet");
            }
            catch { 
              encoding = System.Text.Encoding.Default;
            }
            return encoding.GetByteCount(strValue);
             
        }




	


		/// <summary>
		/// 从Grid中取值赋到相应的控件上
		/// </summary>


		static public bool ControlArrayContains(Control [] controlArray, Control controlMatch)
		{
			if (controlArray == null || controlArray.Length < 1)
				return false;

			for (int i=0; i< controlArray.Length; i++)
			{
				if (controlArray[i] == controlMatch)
					return true;
			}

			return false;
		}
 
 
		static public double CalcCoilMatOuterDia(double matWt, double matInnerDia, double matWidth)
		{
			matInnerDia = matInnerDia /1000;
			matWidth = matWidth / 1000;
			return 1000*2*Math.Sqrt((matWt/(Math.PI*matWidth*7.8)+ (matInnerDia/2)*(matInnerDia/2)));
		} 
  
        /// <summary>
        /// 新增指定行
        /// </summary>
        /// <param name="dtSoure"></param>
        /// <param name="drRow"></param>
        /// <returns></returns>
        public static DataRow AddCopyRow(DataTable dtSoure, DataRow drRow)
        {
            try
            {
                DataRow dr = dtSoure.NewRow();
                for (int nIndex = 0; nIndex < dtSoure.Columns.Count; nIndex++)
                {
                    dr[dtSoure.Columns[nIndex].ColumnName] = drRow[dtSoure.Columns[nIndex].ColumnName];
                }
                //如果没有主键约束,则新增
                if (dtSoure.Constraints.Count < 1)
                {
                    dtSoure.Rows.Add(dr);
                }
                return dr;
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
                return null;
            }
        }
        public static void CheckUnSavedChange(System.Windows.Forms.BindingSource tBindSource)
        {
            CheckUnSavedChange(true, tBindSource);
        }
        /// <summary>
        /// 查询是否有更改
        /// </summary>
        /// <param name="isQuery">
        /// 是否是查询,true .是查询时,检查是否有更改,
        ///            false 当不是查询时,但输入有错误,则提示错误</param>
        /// <param name="tBindSource"></param>
        public static void CheckUnSavedChange(bool isQuery, System.Windows.Forms.BindingSource tBindSource)
        {
            try
            {
                tBindSource.EndEdit();
                DataSet dsBindSet = new DataSet();
                if (tBindSource.DataSource is DataSet)
                {
                    dsBindSet = (DataSet)tBindSource.DataSource;
                }
                else if (tBindSource.DataSource is DataTable)
                {
                    DataTable dt = (DataTable)tBindSource.DataSource;
                    dsBindSet = dt.DataSet;
                }
                if (dsBindSet.HasChanges())
                {
                    throw new Exception("数据已被更改。");
                }
            }
            catch (Exception ex)
            {
                if (!isQuery)
                {
                    if (!ex.Message.Equals("数据已被更改。"))
                        throw new Exception(ex.Message);

                }
                DialogResult dr = EF.EFMessageBox.Show("继续将丢失未保存的更改，是否继续？", "epEname", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dr == DialogResult.No)
                {
                    throw new Exception("点击存盘.保存所做的更改。");
                }
            }
        }
        /// <summary>
        /// LayoutControlHelper 类
        /// LayoutControl控件的辅助类.
        /// 包含属性:layoutControl控件,configType配置类型,className类名称,moduleName模块名称
        /// </summary>
        public class LayoutControlHelper
        {
            /// <summary>
            /// 控件LayoutControl
            /// </summary>
            public DevExpress.XtraLayout.LayoutControl layoutControl;
            /// <summary>
            /// 配置类型
            /// </summary>
            public ConfigEnum configType = ConfigEnum.Default;
            /// <summary>
            /// 类名称(配置名称为:: 类名称+form名称+_layoutControl名称.xml )
            /// </summary>
            public string className = "";
            /// <summary>
            /// 模块名,默认取窗体名称的第4至6位
            /// </summary>
            public string moduleName = "";

            public LayoutControlHelper(DevExpress.XtraLayout.LayoutControl layoutControl)
            {
                this.layoutControl = layoutControl;
                layoutControl.OptionsCustomizationForm.ShowPropertyGrid = true;//显示属性配置框
                //为保存按钮定义事件
                layoutControl.ShowCustomization += new System.EventHandler(this.layoutControl1_ShowCustomization);
            }

            private void layoutControl1_ShowCustomization(object sender, EventArgs e)
            {
                layoutControl = (DevExpress.XtraLayout.LayoutControl)sender;
                if (layoutControl != null)
                {
                    if (layoutControl.CustomizationForm != null)
                    {
                        (layoutControl.CustomizationForm as DevExpress.XtraLayout.Customization.CustomizationForm).buttonsPanel1.UnRegister();
                        //防止多次添加事件,先移除,再添加
                        (layoutControl.CustomizationForm as DevExpress.XtraLayout.Customization.CustomizationForm).buttonsPanel1.SaveLayoutButton.Click -= new EventHandler(efBtnSaveLayout_Click);
                        (layoutControl.CustomizationForm as DevExpress.XtraLayout.Customization.CustomizationForm).buttonsPanel1.SaveLayoutButton.Click += new EventHandler(efBtnSaveLayout_Click);
                    }
                }
            }
            private void efBtnSaveLayout_Click(object sender, EventArgs e)
            {
                SaveLayout(layoutControl, this.configType, className, moduleName);
            }
            #region 保存和加载布局:布局的文件名称为 (传入的ClassName+窗体Form的名称+"_"+EFDevLayoutControl的名称)
            public enum ConfigEnum
            {
                UserConfig,
                EPConfig,
                Default //默认先查找用户配置,没有则查找项目配置
            }
            /******************************8
            /// <summary>
            /// 以窗体Form的名称+"_"+EFDevLayoutControl的名称,以及默认配置模式[暂定用户级]保存grid配置
            /// </summary>
            public void SaveLayout(DevExpress.XtraLayout.LayoutControl layoutControl)
            {
                SaveLayout(layoutControl, ConfigEnum.Default);
            }
            /// <summary>
            /// 以窗体Form的名称+"_"+gridView的名称,以及制定的配置模式保存grid配置
            /// </summary>
            /// <param name="ConfigModule">用户级和项目级</param>
            public void SaveLayout(DevExpress.XtraLayout.LayoutControl layoutControl, ConfigEnum ConfigModule)
            {
                this.SaveLayout(layoutControl, ConfigModule, string.Empty, string.Empty);
            }
            ******************************/
            /// <summary>
            ///  文件目录不存在则创建,XML配置名称为: className +"窗体名称_"+EFDevLayoutControl的名称.XML. 
            ///  ---2011-10-27 把 XML配置名称 改为 "窗体名称_"+EFDevLayoutControl的名称+_className.XML
            /// </summary>  
            /// <param name="ConfigModule">配置默认,用户级和项目级</param>
            /// <param name="className">类名,当画面是配置画面时,为了区分不同窗体而添加类名 </param>   
            /// <param name="moduleName">一级模块名称(如DE,为空时取窗体名称的第4至6位)</param>
            public void SaveLayout(DevExpress.XtraLayout.LayoutControl layoutControl, ConfigEnum ConfigModule, string className, string moduleName)
            {
                //模块名称
                string formName = layoutControl.FindForm().Name;
                if (moduleName.Trim().Equals(""))
                {
                    moduleName = formName.Length > 6 ? formName.Substring(4, 2) : formName;
                }
                if (formName.StartsWith("Form"))
                {
                    formName = formName.Substring(4);
                }
                //XML路径
                string fileDirectory = System.Reflection.Assembly.GetExecutingAssembly().Location;
                fileDirectory = Path.GetDirectoryName(fileDirectory);
                if (ConfigModule == ConfigEnum.UserConfig)
                {
                    //位于UserConfig下,用户名文件夹下,一级模块下
                    fileDirectory = fileDirectory + "\\..\\UserConfig\\" + "EF.EF_Args.formUserId" + "\\" + moduleName + "\\";
                }
                else if (ConfigModule == ConfigEnum.EPConfig)
                {
                    //位于一级模块下
                    fileDirectory = fileDirectory + "\\..\\" + moduleName + "\\";
                }
                else
                {
                    //位于UserConfig下,用户名文件夹下,一级模块下 (默认用户配置)
                    fileDirectory = fileDirectory + "\\..\\UserConfig\\" + "EF.EF_Args.formUserId" + "\\" + moduleName + "\\";
                }
                string fileDirectory2 = "";
                try
                {
                    fileDirectory2 = fileDirectory + "EC.UserConfig.Instance.Culture" + "\\";
                }
                catch { }
                //文件目录不存在则创建[逻辑上,当是项目配置时,模块目录一定存在,否则报错]
                if (!System.IO.Directory.Exists(fileDirectory))
                {
                    System.IO.Directory.CreateDirectory(fileDirectory);
                }
                if (fileDirectory2 != "" && !System.IO.Directory.Exists(fileDirectory2))
                {
                    System.IO.Directory.CreateDirectory(fileDirectory2);
                }
                //保存文件名
                //string fileName = className.Trim() + formName + "_" + layoutControl.Name + ".xml";
                //  ---2011-10-27 把 XML配置名称 改为 "窗体名称_"+EFDevLayoutControl的名称+_className.XML
                string fileName = formName + "_" + layoutControl.Name +(className.Trim().Equals("")?"":("_"+className.Trim()))+ ".xml";
                string filePath = fileDirectory + fileName;
                string filePath2 = fileDirectory2 + fileName;
                bool isFirstConfig = false;
                if (System.IO.File.Exists(filePath))
                {
                    if (System.IO.File.Exists(filePath2))
                    {
                        //文件已存在,则提示,是否覆盖
                        if (EF.EFMessageBox.Show("配置文件已存在,是否替换？","提示", MessageBoxButtons.YesNo) != DialogResult.Yes)
                        {
                            return;
                        }
                    }
                }
                else
                {
                    isFirstConfig = false;
                }
                if (isFirstConfig)
                {
                    //即使是第一次配置也不保存到默认目录下.只放到对应语言目录下
                   // layoutControl.SaveLayoutToXml(filePath);
                }
                layoutControl.SaveLayoutToXml(filePath2);
            }
            public void LoadLayout()
            {
                  LoadLayout(layoutControl, this.configType, className, moduleName);
            }
            /// <summary>
            /// 寻找(以窗体Form的名称+"_"+EFDevLayoutControl的名称)的配置文件.加载布局..先查找用户目录,没有则查找项目级目录,没有则不操作
            /// </summary>
            public void LoadLayout(DevExpress.XtraLayout.LayoutControl layoutControl)
            {
                  LoadLayout(layoutControl, this.configType, className, moduleName);
            }
            /// <summary>
            ///  寻找(以窗体Form的名称+"_"+EFDevLayoutControl的名称)的配置文件.加载布局..根据参数(配置模式)选择对应目录下的配置文件.
            /// </summary>
            /// <param name="ConfigModule"></param>
            public void LoadLayout(DevExpress.XtraLayout.LayoutControl layoutControl, ConfigEnum ConfigModule)
            {
                  LoadLayout(layoutControl, this.configType, className, moduleName);
            }
            /// <summary>
            ///  寻找(以className+窗体Form的名称+"_"+EFDevLayoutControl的名称)的配置文件.加载布局..根据参数(配置模式)选择对应目录下的配置文件.
            /// </summary>
            /// <param name="ConfigModule">配置模式,默认时先查找用户配置,再查找项目级配置..</param>
            /// <param name="className">类名称[当窗体名称_+EFDevLayoutControl名称不能满足唯一xml文件名需求时,在开头添加类名称</param>
            /// <param name="moduleName">一级模块名称.如DE,FI等</param>
            public void LoadLayout(DevExpress.XtraLayout.LayoutControl layoutControl, ConfigEnum ConfigModule, string className, string moduleName)
            {
                //模块名称
                string formName = layoutControl.FindForm().Name;
                if (moduleName.Trim().Equals(""))
                {
                    moduleName = formName.Length > 6 ? formName.Substring(4, 2) : formName;
                }
                if (formName.StartsWith("Form"))
                {
                    formName = formName.Substring(4);
                }
                //XML路径
                string fileDirectory = System.Reflection.Assembly.GetExecutingAssembly().Location;
                fileDirectory = Path.GetDirectoryName(fileDirectory);
                string fileDirectoryUser = fileDirectory + "\\..\\UserConfig\\" + "EF.EF_Args.formUserId" + "\\" + moduleName + "\\";
                string fileDirectoryEP = fileDirectory + "\\..\\" + moduleName + "\\";

                //先查看用户级配置,目录不存在读取项目配置
                //  ---2011-10-27 把 XML配置名称 改为 "窗体名称_"+EFDevLayoutControl的名称+_className.XML
                string fileName = formName + "_" + layoutControl.Name + (className.Trim().Equals("") ? "" : ("_" + className.Trim())) + ".xml";
                //string fileName = className.Trim() + formName + "_" + layoutControl.Name + ".xml";
                string filePath = "";
                if (ConfigModule == ConfigEnum.EPConfig)
                {
                    filePath = fileDirectoryEP + fileName;
                }
                else if (ConfigModule == ConfigEnum.UserConfig)
                {
                    filePath = fileDirectoryUser + fileName;
                }
                else //默认配置.先读取用户级配置,不存在则读取项目级配置
                {
                    filePath = fileDirectoryUser + fileName;
                    if (!  IsFileExit(ref filePath))//不存在,则读取项目级的
                    {
                        filePath = fileDirectoryEP + fileName;
                    }
                }

                if   (IsFileExit(ref filePath))    // (System.IO.File.Exists(filePath))  //存在,则读取
                {

                    //string EPConfigXML = fileDirectoryEP + fileName; //项目级配置xml
                    //项目级配置xml 如果存在就麻烦了---不存在,则直接读取配置文件
                    //---先加载项目级配置,把不可见的项,设置为不可配置
                    //---如果是用户配置则 "再加载用户级配置",否则退出
                    //---再把不可配置项设为不可见
                    //if (System.IO.File.Exists(EPConfigXML))
                    //{
                    //    layoutControl.RestoreLayoutFromXml(EPConfigXML);
                    //    if (view is DevExpress.XtraGrid.Views.Grid.GridView)
                    //    {
                    //        foreach (DevExpress.XtraGrid.Columns.GridColumn gridCol in ((DevExpress.XtraGrid.Views.Grid.GridView)view).Columns)
                    //        {
                    //            gridCol.OptionsColumn.ShowInCustomizationForm = gridCol.Visible;
                    //        }
                    //    }
                    //    //然后加载用户级配置
                    //    if (filePath.Equals(EPConfigXML))
                    //    {
                    //        break;
                    //    }
                    //    view.RestoreLayoutFromXml(filePath);
                    //    //然后再次检查,把项目级不可见的,也设为不可见
                    //    if (view is DevExpress.XtraGrid.Views.Grid.GridView)
                    //    {
                    //        foreach (DevExpress.XtraGrid.Columns.GridColumn gridCol in ((DevExpress.XtraGrid.Views.Grid.GridView)view).Columns)
                    //        {
                    //            gridCol.Visible = gridCol.OptionsColumn.ShowInCustomizationForm;
                    //        }
                    //    }
                    //}
                    //else
                    //{
                    //    view.RestoreLayoutFromXml(filePath);
                    //}
                    layoutControl.RestoreLayoutFromXml(filePath);
                   // return true;
                }
                else
                {
                   // return false;
                }
            }

            private bool IsFileExit(ref string fileFullPath)
            {
                string filePath = fileFullPath;
                try
                {
                    string culture = "\\" + "EC.UserConfig.Instance.Culture";
                    int last = filePath.LastIndexOf("\\");
                    filePath = filePath.Insert(last, culture);
                    if (File.Exists(filePath))
                    {
                        fileFullPath = filePath;
                        return true;
                    }
                    return File.Exists(fileFullPath);
                }
                catch { }
                return File.Exists(fileFullPath);
            }
            #endregion

		}

		public static bool SetGridColumn(EF.EFDevGrid[] efGrid, string[] edFuncId)
		{
			int items = efGrid.Length < edFuncId.Length ? efGrid.Length : edFuncId.Length;
			if (items < 1)
			{
				return false;
			}

			try
			{
				string dll_path = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\EFX.dll";
				

				System.Reflection.Assembly assembly_EFX = System.Reflection.Assembly.LoadFrom(dll_path);

				Type myType = assembly_EFX.GetType("EFX.EFCGrid");

				System.Reflection.MethodInfo InitSingleGridColumn_MethodInfo
					= myType.GetMethod("InitMultiGridColumn", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static);

				if (InitSingleGridColumn_MethodInfo != null)
				{
					InitSingleGridColumn_MethodInfo.Invoke(null, new object[] { efGrid, edFuncId });
				}
			}
			catch (Exception e)
			{
				EF.EFMessageBox.Show(e.Message);
				return false;
			}

			return true;

/*
			EI.EIInfo inBlk = new EI.EIInfo();
			inBlk.SetColName(1, "func_id");
			int i = 0;
			for (i = 0; i < items; ++i)
			{
				inBlk.SetColVal(i + 1, "func_id", edFuncId[i]);
			}
			
			EI.EIInfo outBlk = EI.EITuxedo.CallService("eped54_inq_cfg1", inBlk);
			if (outBlk.GetSys().flag == 0)
			{
				try
				{
					string item_ename;
					string item_cname;
					string item_type;
					string item_len;
					string code_class;
					string form_edit_flag;
					string item_default_value;
					string item_hide_flag;

					DataSet DSsource = new DataSet();
					DataTable dtsource = null;
					outBlk.GetBlockVal(DSsource);
					DevExpress.XtraGrid.Columns.GridColumn gridcolumn = null;
					DevExpress.XtraGrid.Views.Grid.GridView gridview = null;
					for (i = 0; i < items; ++i)
					{
						DataTable dt = new DataTable();
						dt.TableName = edFuncId[i];
						dtsource = DSsource.Tables[edFuncId[i]];
						if (dtsource.Rows.Count < 1)
						{
							throw new Exception("Not found func_id :" + edFuncId[i]);
						}
						efGrid[i].BeginUpdate();
						if (efGrid[i].MainView is DevExpress.XtraGrid.Views.Grid.GridView)
						{
							gridview = efGrid[i].MainView as DevExpress.XtraGrid.Views.Grid.GridView;
							gridview.Columns.Clear();
						}
						else
						{
							efGrid[i].MainView.Dispose();
							gridview = new DevExpress.XtraGrid.Views.Grid.GridView(efGrid[i]);
							efGrid[i].MainView = gridview;
						}
						gridview.BeginUpdate();
						efGrid[i].Tag = dtsource;
						for (int row = 0; row < dtsource.Rows.Count; ++row)
						{
							item_hide_flag = dtsource.Rows[row]["item_hide_flag"].ToString().Trim();
							item_ename = dtsource.Rows[row]["item_ename"].ToString().Trim();
							item_cname = dtsource.Rows[row]["item_cname"].ToString().Trim();
							item_type = dtsource.Rows[row]["item_type"].ToString().Trim();
							item_len = dtsource.Rows[row]["item_len"].ToString().Trim();
							code_class = dtsource.Rows[row]["code_class"].ToString().Trim();
							form_edit_flag = dtsource.Rows[row]["form_edit_flag"].ToString().Trim();
							item_default_value = dtsource.Rows[row]["item_default_value"].ToString().Trim();

							gridcolumn = new DevExpress.XtraGrid.Columns.GridColumn();
							gridview.Columns.Add(gridcolumn);
							gridcolumn.Caption = item_cname;
							gridcolumn.Name = item_ename;
							gridcolumn.FieldName = item_ename;
							gridcolumn.Visible = !("1" == item_hide_flag || "2" == item_hide_flag);
							if (gridcolumn.Visible)
							{
								gridcolumn.VisibleIndex = gridview.Columns.Count;
							}
							gridcolumn.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
							gridcolumn.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;

							DataColumn col = dt.Columns.Add(item_ename);
							col.Caption = item_cname;
							if (item_type == "N")
							{
								if (item_len.Contains(","))
								{
									col.DataType = typeof(double);
									if (item_default_value != string.Empty)
									{
										col.DefaultValue = double.Parse(item_default_value);
									}
								}
								else
								{
									col.DataType = typeof(int);
									if (item_default_value != string.Empty)
									{
										col.DefaultValue = int.Parse(item_default_value);
									}
								}
								if (item_default_value == string.Empty)
								{
									col.DefaultValue = 0;
								}
								gridcolumn.ColumnEdit = new DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit();
							}
							else if (item_type == "O" || item_type == "H")
							{
								DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lookupedit =
									new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
								gridcolumn.ColumnEdit = lookupedit;
								lookupedit.DataSource = DSsource.Tables["CODECLASS:" + code_class];
								lookupedit.AutoHeight = false;
								lookupedit.Name = "LookUpEdit_" + code_class;
								lookupedit.DisplayMember = lookupedit.ValueMember = "CODE";
								if ("H" == item_type)
								{
									lookupedit.DisplayMember = "CODE_DESC_1_CONTENT";
								}
								lookupedit.Properties.NullText = "";
								col.DefaultValue = item_default_value;
								lookupedit.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CODE", "代码"));
								if (dtsource.Rows[row]["CODE_BIND_ENAME1"].ToString().Trim() == string.Empty
									&& dtsource.Rows[row]["CODE_BIND_ENAME2"].ToString().Trim() == string.Empty
									&& dtsource.Rows[row]["CODE_BIND_ENAME3"].ToString().Trim() == string.Empty
									&& dtsource.Rows[row]["CODE_BIND_ENAME4"].ToString().Trim() == string.Empty
									&& dtsource.Rows[row]["CODE_BIND_ENAME5"].ToString().Trim() == string.Empty)
								{
									//lookupedit.DisplayMember = "CODE_DESC_1_CONTENT";
									lookupedit.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CODE_DESC_1_CONTENT", "描述"));
								}
								else
								{
									//lookupedit.DisplayMember = "CODE";
									if (dtsource.Rows[row]["CODE_BIND_ENAME1"].ToString().Trim() != string.Empty)
									{
										lookupedit.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CODE_DESC_1_CONTENT", "描述1"));
									}
									if (dtsource.Rows[row]["CODE_BIND_ENAME2"].ToString().Trim() != string.Empty)
									{
										lookupedit.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CODE_DESC_2_CONTENT", "描述2"));
									}
									if (dtsource.Rows[row]["CODE_BIND_ENAME3"].ToString().Trim() != string.Empty)
									{
										lookupedit.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CODE_DESC_3_CONTENT", "描述3"));
									}
									if (dtsource.Rows[row]["CODE_BIND_ENAME4"].ToString().Trim() != string.Empty)
									{
										lookupedit.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CODE_DESC_4_CONTENT", "描述4"));
									}
									if (dtsource.Rows[row]["CODE_BIND_ENAME5"].ToString().Trim() != string.Empty)
									{
										lookupedit.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CODE_DESC_5_CONTENT", "描述5"));
									}
									lookupedit.EditValueChanged += new EventHandler(EF.CustomCols.SetGridColumn_lookupedit_EditValueChanged);
								}
							}
							else if (item_type == "F")		// 弹出画面
							{
								DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit buttedit =
									new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
								gridcolumn.ColumnEdit = buttedit;
								buttedit.Name = code_class;
								buttedit.Tag = code_class;
								col.DefaultValue = item_default_value;
								buttedit.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(EF.CustomCols.SetGridColumn_buttedit_ButtonClick);
							}	
							else if ("D" == item_type)		// 时间日期
							{
								DevExpress.XtraEditors.Repository.RepositoryItemDateEdit dateEdit =
									new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
								gridcolumn.ColumnEdit = dateEdit;
								string strEditMask = dtsource.Rows[row]["editmask"].ToString().Trim();
								if (strEditMask == string.Empty)
								{
									strEditMask = "yyyyMMdd";
								}
								//dateEdit.EditMask = strEditMask;
								//dateEdit.Mask.UseMaskAsDisplayFormat = true;

								dateEdit.DisplayFormat.FormatString = strEditMask;
								dateEdit.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
								dateEdit.DisplayFormat.Format = new EF.CustomFormatter();

								dateEdit.EditFormat.FormatString = strEditMask;
								//dateEdit.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;

								dateEdit.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
								dateEdit.EditFormat.Format = new EF.CustomFormatter1();

								dateEdit.ParseEditValue += new DevExpress.XtraEditors.Controls.ConvertEditValueEventHandler(dateEdit_ParseEditValue);
								//col.DataType = typeof(DateTime);
								//dateEdit.FormatEditValue += new DevExpress.XtraEditors.Controls.ConvertEditValueEventHandler(dateEdit_FormatEditValue);
							
								//col.DefaultValue = "";
							}
							else if ("B" == item_type)		// 布尔
							{
								col.DataType = typeof(Boolean);
								gridcolumn.ColumnEdit = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
								if ("1" == item_default_value || "true" == item_default_value.ToLower())
								{
									col.DefaultValue = true;
								}
								else
								{
									col.DefaultValue = false;
								}
							}
							else
							{
								col.DataType = typeof(string);
								gridcolumn.ColumnEdit = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
								col.DefaultValue = item_default_value;
							}

							gridcolumn.ColumnEdit.Leave += new EventHandler(EF.CustomCols.CustomGridColumnEdit_Leave);
							//gridcolumn.ColumnEdit.EditValueChanged += new EventHandler(EF.CustomCols.CustomGridColumnEdit_EditValueChanged);

							if (form_edit_flag == "0")
							{
								gridcolumn.ColumnEdit.ReadOnly = true;
							}
						}
						dt.Rows.Add();
                        efGrid[i].ShowSelectionColumn = efGrid[i].ShowSelectionColumn;
                        efGrid[i].ShowSelectedColumn = efGrid[i].ShowSelectedColumn;
						efGrid[i].DataSource = dt;
						gridview.BestFitColumns();
						gridview.EndUpdate();
						efGrid[i].EndUpdate();
					}
					return true;
				}
				catch (Exception e)
				{
					//MessageBox.Show(e.Message);
				}
			}
			return false;
 */
		}

		static void dateEdit_ParseEditValue(object sender, DevExpress.XtraEditors.Controls.ConvertEditValueEventArgs e)
		{
			if (e.Value is string)
			{
				string strTemp = e.Value.ToString();
				//e.Handled = true;
				try
				{
					DateTime dt = DateTime.Parse(strTemp);
					e.Value = DateTime.ParseExact(strTemp, "yyyyMMddHHmmss", null);
					e.Handled = true;
				}
				catch (Exception ex)
				{
					e.Value = strTemp;
				}
			}
		}
  
	}
}
