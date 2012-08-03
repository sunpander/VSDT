using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using System.Data;
namespace VSDT.Common.Utility
{
    public static class UtilityFile
    {
        public static string ChooseOneDirectory()
        {
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();
            folderDialog.ShowDialog();

            return folderDialog.SelectedPath;
        }

        public static string ChooseOneFile()
        {
            OpenFileDialog openfile = new OpenFileDialog();
            openfile.ShowDialog();
            return   openfile.FileName;
        }
        public static string ChooseOneFile(string filter)
        {
            OpenFileDialog openfile = new OpenFileDialog();
            openfile.Filter = filter;
            openfile.ShowDialog();
            return openfile.FileName;
        }
        public static string[] ChooseMultiFiles(string filter,bool multiSel)
        {
            OpenFileDialog openfile = new OpenFileDialog();
            openfile.Filter = filter;
            openfile.Multiselect = multiSel;
            openfile.ShowDialog();
            return openfile.FileNames;
        }
        public static string ChooseOneFileToSave()
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.ShowDialog();
            return saveFile.FileName;
        }
        public static bool SaveOneFile(string fileContent)
        {
            try
            {
                string strFileName = ChooseOneFileToSave();
                System.IO.StreamWriter sw = new System.IO.StreamWriter(strFileName, false, System.Text.Encoding.Default);
                sw.WriteLine(fileContent);
                sw.Close();
                return true;
            }
            catch (Exception ex)
            {
                Log.ShowErrorBox(ex);
                return false;
            }
        }
    
        /***************

        #region 创建文件
        /// <summary>
        /// 创建文件
        /// </summary>
        /// <param name="savePath"></param>
        /// <param name="fileName"></param>
        /// <param name="GetHoleCode"></param>
        /// <returns></returns>
        public string CreateFile(string savePath, string fileName, string GetHoleCode)
        {
            string filePathName;


            filePathName = savePath + fileName;
            FileStream newCodeFS = File.Create(filePathName);
            StreamWriter newCodeSW = new StreamWriter(newCodeFS, Encoding.Default);
            newCodeSW.Write(GetHoleCode);
            newCodeSW.Close();
            return filePathName;
        }
        #endregion

        #region 保存文件(重载)
        /// <summary>
        /// 保存文件(重载)
        /// </summary>
        /// <param name="saveFileName"></param>
        /// <param name="GetHoleCode"></param>
        public void CreateFile(string saveFileName, string GetHoleCode)
        {
            FileStream newCodeFS = File.Create(saveFileName);
            StreamWriter newCodeSW = new StreamWriter(newCodeFS, Encoding.Default);
            newCodeSW.Write(GetHoleCode);
            newCodeSW.Close();
        }
        #endregion

        #region 获取文件内容
        /// <summary>
        /// 获取文件内容
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public string GetAllFileContent(string filePath)
        {
            string oldCodeText = "";
            StreamReader CodeReader = new StreamReader(filePath, Encoding.Default);
            oldCodeText = CodeReader.ReadToEnd();
            CodeReader.Close();

            return oldCodeText;
        }
        #endregion

        #region 取得路径下所有文件

        private ArrayList AllDirFilePath = new ArrayList();
        /// <summary>
        /// 取得路径下所有文件
        /// </summary>
        /// <param name="directory">目录</param>
        /// <param name="fileModel">类似*.txt</param>
        /// <returns></returns>
        public ArrayList GetAllDirFileList(DirectoryInfo directory, string fileModel)
        {

            System.IO.FileInfo[] fileInfos = fileModel.Equals("") ? directory.GetFiles() : directory.GetFiles(fileModel);
            foreach (System.IO.FileInfo file in fileInfos)
            {
                AllDirFilePath.Add(file.FullName);
            }
            DirectoryInfo[] SUBDirectories = directory.GetDirectories();
            foreach (DirectoryInfo subdirectory in SUBDirectories)
            {
                GetAllDirFileList(subdirectory, fileModel);
            }
            return AllDirFilePath;
        }
        /// <summary>
        /// 取得路径下所有文件
        /// </summary>
        /// <param name="directory">目录</param>
        /// <param name="fileModel">类似*.txt</param>
        /// <returns></returns>
        public ArrayList GetFileList(DirectoryInfo directory, string fileModel)
        {
            ArrayList AllFilePath = new ArrayList();
            System.IO.FileInfo[] fileInfos = fileModel.Equals("") ? directory.GetFiles() : directory.GetFiles(fileModel);
            foreach (System.IO.FileInfo file in fileInfos)
            {
                AllFilePath.Add(file.FullName);
            }
            return AllFilePath;
        }
        #endregion

        #region 获取子目录
        /// <summary>
        /// 获取子目录
        /// </summary>
        /// <param name="directory"></param>
        /// <returns></returns>
        public ArrayList GetChildPath(DirectoryInfo directory)
        {
            ArrayList DIRPath = new ArrayList();
            DirectoryInfo[] files = directory.GetDirectories();
            foreach (DirectoryInfo file in files)
            {
                DIRPath.Add(file.FullName);
            }
            return DIRPath;
        }
        #endregion

        #region 获取所有子目录
        /// <summary>
        /// 获取所有子目录
        /// </summary>
        private ArrayList AllDIRPath = new ArrayList();
        public ArrayList GetAllChildPath(DirectoryInfo directory)
        {
            AllDIRPath.Clear();
            DirectoryInfo[] files = directory.GetDirectories();
            foreach (DirectoryInfo file in files)
            {
                AllDIRPath.Add(file.FullName);
            }
            DirectoryInfo[] directoies = directory.GetDirectories();
            foreach (DirectoryInfo subDirectory in directoies)
            {
                GetAllChildPath(subDirectory);
            }

            return AllDIRPath;
        }
        #endregion

        #region 选择保存文件
        /// <summary>
        /// 选择保存文件
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string SaveXmlFile(string model)
        {
            string resultfile = "";
            try
            {
                SaveFileDialog save = new SaveFileDialog();
                save.InitialDirectory = "..\\DIXml";
                save.Filter = model;
                save.FilterIndex = 2;
                save.RestoreDirectory = true;
                if (save.ShowDialog() == DialogResult.OK)
                    resultfile = save.FileName;

            }
            catch
            { }
            return resultfile;
        }
        #endregion

        #region 选择打开文件
        /// <summary>
        /// 选择打开文件
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string OpenXmlFile(string model)
        {
            string resultfile = "";
            try
            {
                OpenFileDialog open = new OpenFileDialog();
                open.InitialDirectory = "..\\DIXml";
                open.Filter = model;
                open.FilterIndex = 2;
                open.RestoreDirectory = true;
                if (open.ShowDialog() == DialogResult.OK)
                    resultfile = open.FileName;
            }
            catch
            { }
            return resultfile;
        }
        #endregion

        #region 创建文件夹
        /// <summary>
        /// 创建文件夹
        /// </summary>
        /// <param name="path">创建目录</param>
        public void CreateDirectory(string path)
        {
            Directory.CreateDirectory(path);
        }
        #endregion

        #region  复制现有文件静态方法可以覆盖同名文件
        /// <summary>
        /// 复制现有文件静态方法可以覆盖同名文件
        /// </summary>
        /// <param name="sourceFilePath">源路径</param>
        /// <param name="aimFilePath">目标路径</param>
        /// <param name="isOver">是否覆盖</param>
        public void CopyFile(string sourceFilePath, string aimFilePath, bool isOver)
        {
            File.Copy(sourceFilePath, aimFilePath, isOver);
        }
        #endregion

        #region 判断文件夹是否存在
        /// <summary>
        /// 判断文件夹是否存在
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public bool isDirectoryExist(string path)
        {
            bool isExist = false;
            isExist = Directory.Exists(path);
            return isExist;
        }
        #endregion

        #region 取得当前工作目录
        /// <summary>
        /// 取得当前工作目录
        /// </summary>
        /// <returns></returns>
        public string GetCurrentDirectory()
        {
            string currentDirectory = "";
            currentDirectory = Directory.GetCurrentDirectory();
            return currentDirectory;
        }
        #endregion

        public void MoveToDirectory(string oldDirectory, string newDirectory)
        {
            DirectoryInfo myInfo = new DirectoryInfo(oldDirectory);
            myInfo.MoveTo(newDirectory);
        }
         * ***/
        #region
        ////导出Excel的方法
        //private static void ExportExcel()
        //{


        //    DataSet ds = new DataSet();
        //    System.Data.DataTable dt = new System.Data.DataTable();
        //    dt.Columns.Add("NAME");
        //    dt.Columns.Add("Version");
        //    dt.Columns.Add("SymbolicName");
        //    dt.Columns.Add("State");
        //    dt.Columns.Add("Operate");
        //    for (int i = 0; i < 10; i++)
        //    {
        //        dt.Rows.Add("NAME:" + i, "Version" + i, "sName" + i,  i);
        //    }
        //    ds.Tables.Add(dt);

        //    DataTable dt2 = dt.Copy();
        //    dt2.TableName = "ddf";
           
        //    ds.Tables.Add( dt2 );
            

            
        //    bool fileSaved = false;
        //    //获取路径
        //    string saveFileName = "";
        //    SaveFileDialog saveDialog = new SaveFileDialog();
        //    saveDialog.DefaultExt = "xls";
        //    saveDialog.Filter = "Excel文件|*.xlsx|Excel文件|*.xls";
        //    saveDialog.FileName = "Sheet1";
        //    saveDialog.ShowDialog();
        //    saveFileName = saveDialog.FileName;
        //    if (saveFileName.IndexOf(":") < 0) return; //被点了取消 

        //    //声明一Excel Application 对象
        //    Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
        //    if (xlApp == null)
        //    {
        //        MessageBox.Show("无法创建Excel对象，可能您的机子未安装Excel");
        //        return;
        //    }

        //    //声明一Excel Workbook 对象
        //    Microsoft.Office.Interop.Excel.Workbooks workBooks = xlApp.Workbooks;
        //    //增加一工作薄 Workbook
        //    Microsoft.Office.Interop.Excel.Workbook workBook = workBooks.Add(Microsoft.Office.Interop.Excel.XlWBATemplate.xlWBATWorksheet);
        //    //新建工作薄后默认有一个工作表,取得第一个工作表
        //    Microsoft.Office.Interop.Excel.Worksheet worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workBook.Worksheets[1];//取得sheet1 
        //    int count = ds.Tables.Count;
        //    for (int j = 0; j < count; j++)
        //    {
                
 
        //        addExcelValue(worksheet, ds.Tables[j]);

              
        //            Microsoft.Office.Interop.Excel.Sheets xlSheets = workBook.Sheets as Microsoft.Office.Interop.Excel.Sheets;
        //            //  添加 Sheet
        //            worksheet = (Microsoft.Office.Interop.Excel.Worksheet)xlSheets.Add(xlSheets[1], Type.Missing, Type.Missing, Type.Missing);
                
        //    }

        //    worksheet.Columns.EntireColumn.AutoFit();//列宽自适应。

        //    if (saveFileName != "")
        //    {
        //        try
        //        {
        //            workBook.Saved = true;
        //            workBook.SaveCopyAs(saveFileName);
        //            fileSaved = true;
        //        }
        //        catch (Exception ex)
        //        {
        //            fileSaved = false;
        //            MessageBox.Show("导出文件时出错,文件可能正被打开！\n" + ex.Message);
        //        }
        //    }
        //    else
        //    {
        //        fileSaved = false;
        //    }
        //    xlApp.Quit();
        //    GC.Collect();
        //   // GC.Collect();//强行销毁 
        //    //if (fileSaved && System.IO.File.Exists(saveFileName)) 
        //    //    System.Diagnostics.Process.Start(saveFileName); //打开EXCEL
        //}

        ///// <summary>
        ///// 根据gridview给excel添加列和值
        ///// </summary>
        ///// <param name="worksheet"></param>
        ///// <param name="gridViewExcel"></param>
        //private static void addExcelValue(Microsoft.Office.Interop.Excel.Worksheet worksheet, DataTable gridViewExcel)
        //{
        //    List<string> listFieldName = new List<string>();
        //    string colName = "";
        //    string colValue = "";
        //    worksheet.Name = gridViewExcel.TableName;
        //    //写入字段 
        //    for (int i = gridViewExcel.Columns.Count; i > 0; i--)
        //    {
        //        worksheet.Cells[1, i] = gridViewExcel.Columns[i - 1].Caption;
        //        listFieldName.Add(gridViewExcel.Columns[i - 1].ColumnName);
        //    }
        //    //写入数值
        //    for (int r = 0; r < gridViewExcel.Rows.Count; r++)
        //    {

        //        for (int i = gridViewExcel.Columns.Count; i > 0; i--)
        //        {
        //            colName = listFieldName[i - 1].ToString().Trim();
        //            colValue = gridViewExcel.Rows[r][colName].ToString().Trim();
   
        //            worksheet.Cells[r + 2, gridViewExcel.Columns.Count + 1 - i] = "'"+colValue;
        //        }
        //        System.Windows.Forms.Application.DoEvents();
        //    }
        //}



        #endregion
    }
}
