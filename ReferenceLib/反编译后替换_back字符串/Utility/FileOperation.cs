using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
 
using System.Text;
using System.IO;
using System.Xml;
using System.Windows.Forms;
using System.Collections;
using System.Text.RegularExpressions;


namespace shf.Utility
{
    public  class FileOperation
    {       
        #region 创建文件
        /// <summary>
        /// 创建文件
        /// </summary>
        /// <param name="savePath"></param>
        /// <param name="fileName"></param>
        /// <param name="GetHoleCode"></param>
        /// <returns></returns>
        public  string CreateFile(string savePath, string fileName, string GetHoleCode)
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
        public  void CreateFile(string saveFileName, string GetHoleCode)
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
        public  string GetAllFileContent(string filePath)
        {
            string oldCodeText = "";
            StreamReader CodeReader = new StreamReader(filePath, Encoding.Default);           
            oldCodeText = CodeReader.ReadToEnd();
            CodeReader.Close();
           
            return oldCodeText;
        }
        #endregion

        #region 取得路径下所有文件

        private  ArrayList AllDirFilePath = new ArrayList();
        /// <summary>
        /// 取得路径下所有文件
        /// </summary>
        /// <param name="directory">目录</param>
        /// <param name="fileModel">类似*.txt</param>
        /// <returns></returns>
        public   ArrayList GetAllDirFileList(DirectoryInfo directory, string fileModel)
        {
          
            System.IO.FileInfo[] fileInfos =fileModel.Equals("")?directory.GetFiles(): directory.GetFiles(fileModel);
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
        public  ArrayList GetChildPath(DirectoryInfo directory)
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
        private  ArrayList AllDIRPath = new ArrayList();
        public  ArrayList GetAllChildPath(DirectoryInfo directory)
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
        public  string SaveXmlFile(string model)
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
        public  string OpenXmlFile(string model)
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
        public  void CreateDirectory(string path)
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
        public  void CopyFile(string sourceFilePath, string aimFilePath, bool isOver)
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
        public  bool isDirectoryExist(string path)
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
        public  string GetCurrentDirectory()
        {
            string currentDirectory = "";
            currentDirectory = Directory.GetCurrentDirectory();
            return currentDirectory;
        } 
        #endregion

        public  void MoveToDirectory(string oldDirectory, string newDirectory)
        {
            DirectoryInfo myInfo = new DirectoryInfo(oldDirectory);
            myInfo.MoveTo(newDirectory);

        }
    }

    public static class FileReader
    {
        public static bool IsUnicode(Encoding encoding)
        {
            int codepage = encoding.CodePage;
            // return true if codepage is any UTF codepage
            return codepage == 65001 || codepage == 65000 || codepage == 1200 || codepage == 1201;
        }

        public static string ReadFileContent(string fileName, ref Encoding encoding, Encoding defaultEncoding)
        {
            using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                using (StreamReader reader = OpenStream(fs, encoding, defaultEncoding))
                {
                    encoding = reader.CurrentEncoding;
                    return reader.ReadToEnd();
                }
            }
        }

        public static StreamReader OpenStream(FileStream fs, Encoding suggestedEncoding, Encoding defaultEncoding)
        {
            if (fs.Length > 3)
            {
                // the autodetection of StreamReader is not capable of detecting the difference
                // between ISO-8859-1 and UTF-8 without BOM.
                int firstByte = fs.ReadByte();
                int secondByte = fs.ReadByte();
                switch ((firstByte << 8) | secondByte)
                {
                    case 0x0000: // either UTF-32 Big Endian or a binary file; use StreamReader
                    case 0xfffe: // Unicode BOM (UTF-16 LE or UTF-32 LE)
                    case 0xfeff: // UTF-16 BE BOM
                    case 0xefbb: // start of UTF-8 BOM
                        // StreamReader autodetection works
                        fs.Position = 0;
                        return new StreamReader(fs);
                    default:
                        return AutoDetect(fs, (byte)firstByte, (byte)secondByte, defaultEncoding);
                }
            }
            else
            {
                if (suggestedEncoding != null)
                {
                    return new StreamReader(fs, suggestedEncoding);
                }
                else
                {
                    return new StreamReader(fs);
                }
            }
        }

        static StreamReader AutoDetect(FileStream fs, byte firstByte, byte secondByte, Encoding defaultEncoding)
        {
            int max = (int)Math.Min(fs.Length, 500000); // look at max. 500 KB
            const int ASCII = 0;
            const int Error = 1;
            const int UTF8 = 2;
            const int UTF8Sequence = 3;
            int state = ASCII;
            int sequenceLength = 0;
            byte b;
            for (int i = 0; i < max; i++)
            {
                if (i == 0)
                {
                    b = firstByte;
                }
                else if (i == 1)
                {
                    b = secondByte;
                }
                else
                {
                    b = (byte)fs.ReadByte();
                }
                if (b < 0x80)
                {
                    // normal ASCII character
                    if (state == UTF8Sequence)
                    {
                        state = Error;
                        break;
                    }
                }
                else if (b < 0xc0)
                {
                    // 10xxxxxx : continues UTF8 byte sequence
                    if (state == UTF8Sequence)
                    {
                        --sequenceLength;
                        if (sequenceLength < 0)
                        {
                            state = Error;
                            break;
                        }
                        else if (sequenceLength == 0)
                        {
                            state = UTF8;
                        }
                    }
                    else
                    {
                        state = Error;
                        break;
                    }
                }
                else if (b >= 0xc2 && b < 0xf5)
                {
                    // beginning of byte sequence
                    if (state == UTF8 || state == ASCII)
                    {
                        state = UTF8Sequence;
                        if (b < 0xe0)
                        {
                            sequenceLength = 1; // one more byte following
                        }
                        else if (b < 0xf0)
                        {
                            sequenceLength = 2; // two more bytes following
                        }
                        else
                        {
                            sequenceLength = 3; // three more bytes following
                        }
                    }
                    else
                    {
                        state = Error;
                        break;
                    }
                }
                else
                {
                    // 0xc0, 0xc1, 0xf5 to 0xff are invalid in UTF-8 (see RFC 3629)
                    state = Error;
                    break;
                }
            }
            fs.Position = 0;
            switch (state)
            {
                case ASCII:
                case Error:
                    // when the file seems to be ASCII or non-UTF8,
                    // we read it using the user-specified encoding so it is saved again
                    // using that encoding.
                    if (IsUnicode(defaultEncoding))
                    {
                        // the file is not Unicode, so don't read it using Unicode even if the
                        // user has choosen Unicode as the default encoding.

                        // If we don't do this, SD will end up always adding a Byte Order Mark
                        // to ASCII files.
                        defaultEncoding = Encoding.Default; // use system encoding instead
                    }
                    return new StreamReader(fs, defaultEncoding);
                default:
                    return new StreamReader(fs);
            }
        }
    }
}
