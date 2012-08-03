using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Globalization;

namespace VSDT.Common
{
    /// <summary>
    /// FTP处理操作类
    /// 功能：
    /// 下载文件
    /// 上传文件
    /// 上传文件的进度信息
    /// 下载文件的进度信息
    /// 删除文件
    /// 列出文件
    /// 列出目录
    /// 进入子目录
    /// 退出当前目录返回上一层目录
    /// 判断远程文件是否存在
    /// 判断远程文件是否存在
    /// 删除远程文件    
    /// 建立目录
    /// 删除目录
    /// 文件（目录）改名
    /// </summary>
    /// <remarks>
    /// 创建人： 
    /// 创建时间： 
    /// </remarks>
    #region 文件信息结构
    public class FileStruct
    {
        public string Flags;
        public string Owner;
        public string Group;
        public bool IsDirectory;
        public DateTime CreateTime;
        public string Name;
        public string RelPath;
        public double Size;
    }
    public enum FileListStyle
    {
        UnixStyle,
        WindowsStyle,
        Unknown
    }
    #endregion
    /// <summary>
    /// 1.判断文件夹是否存在
    /// </summary>
    public class FtpController:IDisposable
    {
        #region 属性信息
        FileListStyle _directoryListStyle = FileListStyle.Unknown;
        FtpWebRequest Request = null;
        /// <summary>
        /// FTP响应对象
        /// </summary>
        FtpWebResponse Response = null;

        private Uri _Uri;
        /// <summary>
        /// FTP服务器地址
        /// </summary>
        public Uri Uri
        {
            get
            {
                if (_DirectoryPath == "/")
                {
                    return _Uri;
                }
                else
                {
                    string strUri = _Uri.ToString();
                    if (strUri.EndsWith("/"))
                    {
                        strUri = strUri.Substring(0, strUri.Length - 1);
                    }
                    return new Uri(strUri + this.DirectoryPath);
                }
            }
            set
            {
                if (value.Scheme != Uri.UriSchemeFtp)
                {
                    throw new Exception("Ftp 地址格式错误!");
                }
                _Uri = new Uri(value.GetLeftPart(UriPartial.Authority));
                _DirectoryPath = value.AbsolutePath;
                if (!_DirectoryPath.EndsWith("/"))
                {
                    _DirectoryPath += "/";
                }
            }
        }

        private string _ftpUrl;
        /// <summary>
        /// 地址
        /// </summary>
        public string FtpUrl
        {
            get { return _ftpUrl; }
            set { _ftpUrl = value; }
        }

        private string _DirectoryPath;
        /// <summary>
        /// 当前工作目录
        /// </summary>
        public string DirectoryPath
        {
            get { return _DirectoryPath; }
            set { _DirectoryPath = value; }
        }

        private string _UserName;
        /// <summary>
        /// FTP登录用户
        /// </summary>
        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }

        private string _ErrorMsg;
        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMsg
        {
            get { return _ErrorMsg; }
            set { _ErrorMsg = value; }
        }

        private string _Password;
        /// <summary>
        /// FTP登录密码
        /// </summary>
        public string Password
        {
            get { return _Password; }
            set { _Password = value; }
        }

        private WebProxy _Proxy = null;
        /// <summary>
        /// 连接FTP服务器的代理服务
        /// </summary>
        public WebProxy Proxy
        {
            get
            {
                return _Proxy;
            }
            set
            {
                _Proxy = value;
            }
        }

        private bool userPassive = false;
        /// <summary>
        /// 主动模式还是被动模式
        /// </summary>
        public bool UserPassive
        {
            get { return userPassive; }
            set { userPassive = value; }
        }

        private Encoding defaultEncoding = Encoding.Default;
        /// <summary>
        /// 默认编码
        /// </summary>
        public Encoding DefaultEncoding
        {
            get { return defaultEncoding; }
            set { defaultEncoding = value; }
        }

        #endregion
        #region 事件
        public delegate void De_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e);
        public delegate void De_DownloadDataCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e);
        public delegate void De_UploadProgressChanged(object sender, UploadProgressChangedEventArgs e);
        public delegate void De_UploadFileCompleted(object sender, UploadFileCompletedEventArgs e);

        /// <summary>
        /// 异步下载进度发生改变触发的事件
        /// </summary>
        public event De_DownloadProgressChanged DownloadProgressChanged;
        /// <summary>
        /// 异步下载文件完成之后触发的事件
        /// </summary>
        public event De_DownloadDataCompleted DownloadDataCompleted;
        /// <summary>
        /// 异步上传进度发生改变触发的事件
        /// </summary>
        public event De_UploadProgressChanged UploadProgressChanged;
        /// <summary>
        /// 异步上传文件完成之后触发的事件
        /// </summary>
        public event De_UploadFileCompleted UploadFileCompleted;
        #endregion
        #region 构造析构函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <paramKey colName="FtpUri">FTP地址</paramKey>
        /// <paramKey colName="strUserName">登录用户名</paramKey>
        /// <paramKey colName="strPassword">登录密码</paramKey>
        public FtpController(Uri FtpUri, string strUserName, string strPassword)
        {
            _ftpUrl = FtpUri.AbsolutePath;
            this._Uri = new Uri(FtpUri.GetLeftPart(UriPartial.Authority));
            _DirectoryPath = FtpUri.AbsolutePath;
            if (!_DirectoryPath.EndsWith("/"))
            {
                _DirectoryPath += "/";
            }
            this._UserName = strUserName;
            this._Password = strPassword;
            this._Proxy = null;
        }
        public FtpController(string strUri, string strUserName, string strPassword)
        {
            _ftpUrl = strUri;
            this._Uri = new Uri(strUri);
            _DirectoryPath = "/";
 
            this._UserName = strUserName;
            this._Password = strPassword;
            this._Proxy = null;
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <paramKey colName="FtpUri">FTP地址</paramKey>
        /// <paramKey colName="strUserName">登录用户名</paramKey>
        /// <paramKey colName="strPassword">登录密码</paramKey>
        /// <paramKey colName="objProxy">连接代理</paramKey>
        public FtpController(Uri FtpUri, string strUserName, string strPassword, WebProxy objProxy)
        {
            _ftpUrl = FtpUri.AbsolutePath;
            this._Uri = new Uri(FtpUri.GetLeftPart(UriPartial.Authority));
            _DirectoryPath = FtpUri.AbsolutePath;
            if (!_DirectoryPath.EndsWith("/"))
            {
                _DirectoryPath += "/";
            }
            this._UserName = strUserName;
            this._Password = strPassword;
            this._Proxy = objProxy;
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        public FtpController()
        {
            this._UserName = "anonymous";  //匿名用户
            this._Password = "@anonymous";
            this._Uri = null;
            this._Proxy = null;
        }

        /// <summary>
        /// 析构函数
        /// </summary>
        ~FtpController()
        {
            if (Response != null)
            {
                Response.Close();
                Response = null;
            }
            if (Request != null)
            {
                Request.Abort();
                Request = null;
            }
        }
        #endregion
        #region 建立连接
        /// <summary>
        /// 建立FTP链接,返回响应对象
        /// </summary>
        /// <paramKey colName="uri">FTP地址</paramKey>
        /// <paramKey colName="FtpMathod">操作命令</paramKey>
        private FtpWebResponse Open(Uri uri, string FtpMathod)
        {
            try
            {
                Request = (FtpWebRequest)WebRequest.Create(uri);
                Request.Method = FtpMathod;
                Request.UseBinary = true;
                Request.Credentials = new NetworkCredential(this.UserName, this.Password);
                if (this.Proxy != null)
                {
                    Request.Proxy = this.Proxy;
                }
                return (FtpWebResponse)Request.GetResponse();
            }
            catch (Exception ep)
            {
                ErrorMsg = ep.ToString();
                throw ep;
            }
        }

        public bool IsValidate()
        {
           FtpWebResponse fp = this.Open(Uri, WebRequestMethods.Ftp.ListDirectory)  ;
           if (fp != null)
               return true;
           else
               return false;
        }
        /// <summary>
        /// 建立FTP链接,返回请求对象
        /// </summary>
        /// <paramKey colName="uri">FTP地址</paramKey>
        /// <paramKey colName="FtpMathod">操作命令</paramKey>
        private FtpWebRequest OpenRequest(Uri uri, string FtpMathod)
        {
            try
            {
                Request = (FtpWebRequest)WebRequest.Create(uri);
                Request.Method = FtpMathod;
                Request.UseBinary = true;
                Request.Credentials = new NetworkCredential(this.UserName, this.Password);
                if (this.Proxy != null)
                {
                    Request.Proxy = this.Proxy;
                }
                return Request;
            }
            catch (Exception ep)
            {
                ErrorMsg = ep.ToString();
                throw ep;
            }
        }

        public string GetCurrentWorkDirectory()
        {
            return GetCurrentWorkDirectory(FtpUrl);
        }
        private string GetCurrentWorkDirectory(string ftpUri)
        {
            Uri uri = new Uri(ftpUri);
            FtpWebResponse response = Open(uri, WebRequestMethods.Ftp.PrintWorkingDirectory);
            string currentdir = response.StatusDescription;
            response.Close();
            return currentdir;
        }
        #endregion

        #region  同步下载
        public void DownloadFile(string RemoteFileName, string LocalFullPath)
        {
            DownloadFile(RemoteFileName, LocalFullPath, false);
        }
        #endregion
        private void DownloadFile(string RemoteFileName, string LocalFullPath,bool Asyn)
        {
            try
            {
                if (!IsValidFileChars(RemoteFileName))
                {
                    throw new Exception("非法文件名或目录名!");
                }
                if (Directory.Exists(LocalFullPath)) 
                {
                    //如果传入 的 本地路径是个 文件夹,而非文件全路径
                    LocalFullPath = Path.Combine(LocalFullPath, RemoteFileName);
                }
                string filePath = LocalFullPath;
                if (LocalFullPath.Contains("\\"))
                {
                    filePath = LocalFullPath.Substring(0, LocalFullPath.LastIndexOf("\\"));
                }
                // "本地文件路径不存在!则创建
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }
                client = new MyWebClient();
                client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(client_DownloadProgressChanged);
                client.DownloadFileCompleted += new System.ComponentModel.AsyncCompletedEventHandler(client_DownloadFileCompleted);
                client.Credentials = new NetworkCredential(this.UserName, this.Password);
                if (this.Proxy != null)
                {
                    client.Proxy = this.Proxy;
                }
                client.usePassive = this.userPassive;
                //设置路径
                if (this.Uri.ToString().EndsWith("/"))
                {
                    RemoteFileName = RemoteFileName.TrimStart('/');
                }
                if (Asyn)
                {
                    client.DownloadFileAsync(new Uri(this.Uri.ToString() + RemoteFileName), LocalFullPath);
                }
                else
                {
                    client.DownloadFile(new Uri(this.Uri.ToString() + RemoteFileName), LocalFullPath);
                }
            }
            catch (Exception ep)
            {
                ErrorMsg = ep.ToString();
                throw ep;
            }
        }
        #region 异步下载文件
        /// <summary>
        /// 从FTP服务器异步下载文件，指定本地完整路径文件名
        /// </summary>
        /// <paramKey colName="RemoteFileName">远程文件名</paramKey>
        /// <paramKey colName="LocalFullPath">本地完整路径文件名</paramKey>
        public void DownloadFileAsync(string RemoteFileName, string LocalFullPath)
        {
            DownloadFile(RemoteFileName, LocalFullPath, true);
        }


        /// <summary>
        /// 异步下载文件完成之后触发的事件
        /// </summary>
        /// <paramKey colName="sender">下载对象</paramKey>
        /// <paramKey colName="e">数据信息对象</paramKey>
        void client_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            if (DownloadDataCompleted != null)
            {
                DownloadDataCompleted(sender, e);
            }
        }

        /// <summary>
        /// 异步下载进度发生改变触发的事件
        /// </summary>
        /// <paramKey colName="sender">下载对象</paramKey>
        /// <paramKey colName="e">进度信息对象</paramKey>
        void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            if (DownloadProgressChanged != null)
            {
                DownloadProgressChanged(sender, e);
            }
        }
        #endregion
 
        #region 异步上传文件
        /// <summary>
        /// 异步上传文件到FTP服务器, 上传一个文件时,目录不存在则创建
        /// </summary>
        /// <paramKey colName="LocalFullPath">本地带有完整路径的文件名</paramKey>
        /// <paramKey colName="RemoteFileName">要在FTP服务器上面保存文件名</paramKey>
        /// <paramKey colName="OverWriteRemoteFile">是否覆盖远程服务器上面同名的文件</paramKey>
        public void UploadFileAsync(string LocalFullPath, string RemoteFileName, bool OverWriteRemoteFile)
        {
            try
            {
                if (!IsValidFileChars(RemoteFileName) || !IsValidFileChars(Path.GetFileName(LocalFullPath)) || !IsValidPathChars(Path.GetDirectoryName(LocalFullPath)))
                {
                    throw new Exception("非法文件名或目录名!");
                }
                if (!OverWriteRemoteFile && FileExist(RemoteFileName))
                {
                    throw new Exception("FTP服务上面已经存在同名文件！");
                }
                if (RemoteFileName.Contains("/"))
                {
                    string str = RemoteFileName.Substring(0, RemoteFileName.LastIndexOf("/"));
                    if (!str.Equals(""))
                    {
                        MakeDirectory(str);//先创建文件所在目录
                        //RemoteFileName = RemoteFileName.TrimStart('/');
                    }
                  
                }
                if (File.Exists(LocalFullPath))
                {
                    client = new MyWebClient();
               
                    client.UploadProgressChanged += new UploadProgressChangedEventHandler(client_UploadProgressChanged);
                    client.UploadFileCompleted += new UploadFileCompletedEventHandler(client_UploadFileCompleted);
                    client.Credentials = new NetworkCredential(this.UserName, this.Password);
                    if (this.Proxy != null)
                    {
                        client.Proxy = this.Proxy;
                    }
                    client.usePassive = this.userPassive;
                    //如果文件名称不为空,并且ftp地址最后不是一/结束,则添加/
                    if (!this.Uri.ToString().EndsWith("/") && !RemoteFileName.StartsWith("/") && !RemoteFileName.Equals(""))
                    {
                        RemoteFileName = "/" + RemoteFileName;
                    }
                    if (this.Uri.ToString().EndsWith("/"))
                    {
                        RemoteFileName = RemoteFileName.TrimStart('/');
                    }
                    client.timeOut = 200;
                    client.UploadFileAsync(new Uri(this.Uri.ToString() + RemoteFileName), LocalFullPath);
                    
                }
                else
                {
                    throw new Exception("本地文件不存在!");
                }
            }
            catch (Exception ep)
            {
                ErrorMsg = ep.ToString();
                throw ep;
            }
        }
        MyWebClient client;
        public void StopAsync()
        {
            if (client != null)
                client.CancelAsync();
        }
        /// <summary>
        /// 异步上传文件完成之后触发的事件
        /// </summary>
        /// <paramKey colName="sender">下载对象</paramKey>
        /// <paramKey colName="e">数据信息对象</paramKey>
        void client_UploadFileCompleted(object sender, UploadFileCompletedEventArgs e)
        {
            if (UploadFileCompleted != null)
            {
                UploadFileCompleted(sender, e);
            }
        }

        /// <summary>
        /// 异步上传进度发生改变触发的事件
        /// </summary>
        /// <paramKey colName="sender">下载对象</paramKey>
        /// <paramKey colName="e">进度信息对象</paramKey>
        void client_UploadProgressChanged(object sender, UploadProgressChangedEventArgs e)
        {
            if (UploadProgressChanged != null)
            {
                UploadProgressChanged(sender, e);
            }
        }

        #endregion

        #region 列出目录文件信息
        /// <summary>
        /// 列出FTP服务器上面当前目录的所有文件和目录
        /// </summary>
        public FileStruct[] ListFilesAndDirectories()
        {
            return ListFilesAndDirectories("");
        }
        public FileStruct[] ListFilesAndDirectories(string strFolder)
        {
            string relPath = strFolder;
            if (strFolder.Contains("\\"))
            {
                strFolder = strFolder.Replace("\\", "/");
            }
            strFolder = strFolder.TrimStart('/');
            string strUrl = _ftpUrl;
            if (_ftpUrl.EndsWith("/"))
            {
                strUrl = _ftpUrl + strFolder;
            }
            else //if (!strFolder.Trim().Equals(""))
            {
                strUrl = _ftpUrl + "/" + strFolder;
            }
            Uri uri = new Uri(strUrl);

            Request = (FtpWebRequest)WebRequest.Create(uri);
            Request.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
            Request.Credentials = new NetworkCredential(this.UserName, this.Password);
            if (this.Proxy != null)
            {
                Request.Proxy = this.Proxy;
            }
            Response = (FtpWebResponse)Request.GetResponse();

           
            //StreamReader stream = new StreamReader(Response.GetResponseStream(), Encoding.Default);
            StreamReader stream = new StreamReader(Response.GetResponseStream(), DefaultEncoding);
           // StreamReader stream = new StreamReader(Response.GetResponseStream(), Encoding.UTF8);
            string Datastring = stream.ReadToEnd();
            stream.Close();
            stream.Dispose();
            FileStruct[] list = GetList(Datastring, relPath);
            Dispose();
            return list;
        }
        /// <summary>
        /// 列出FTP服务器上面当前目录的所有文件
        /// </summary>
        public FileStruct[] ListFiles(string folder)
        {
            FileStruct[] listAll = ListFilesAndDirectories(folder);
            List<FileStruct> listFile = new List<FileStruct>();
            foreach (FileStruct file in listAll)
            {
                if (!file.IsDirectory)
                {
                    listFile.Add(file);
                }
            }
            return listFile.ToArray();
        }
        public FileStruct[] ListFiles()
        {
            return ListFiles("");
        }
        /// <summary>
        /// 列出FTP服务器上面当前目录的所有的目录
        /// </summary>
        public FileStruct[] ListDirectories(string folder)
        {
            FileStruct[] listAll = ListFilesAndDirectories(folder);
            List<FileStruct> listDirectory = new List<FileStruct>();
            foreach (FileStruct file in listAll)
            {
                if (file.IsDirectory)
                {
                    listDirectory.Add(file);
                }
            }
            return listDirectory.ToArray();
        }
        public FileStruct[] ListDirectories()
        {
            return ListDirectories("");
        }
        /// <summary>
        /// 获得文件和目录列表
        /// </summary>
        /// <paramKey colName="datastring">FTP返回的列表字符信息</paramKey>
        /// <paramKey colName="relPath">相对路径</paramKey>
        private FileStruct[] GetList(string datastring, string relPath)
        {
            List<FileStruct> myListArray = new List<FileStruct>();
            string[] dataRecords = datastring.Split('\n');
            _directoryListStyle = GuessFileListStyle(dataRecords);
            foreach (string s in dataRecords)
            {
                if (_directoryListStyle != FileListStyle.Unknown && s != "")
                {
                    try
                    {
                        FileStruct f = new FileStruct();
                        f.Name = "..";
                        switch (_directoryListStyle)
                        {
                            case FileListStyle.UnixStyle:

                                f = ParseFileStructFromUnixStyleRecord(s, relPath);

                                break;
                            case FileListStyle.WindowsStyle:
                                f = ParseFileStructFromWindowsStyleRecord(s, relPath);
                                break;
                        }
                        if (!(f.Name == "." || f.Name == ".."))
                        {
                            myListArray.Add(f);
                        }
                    }
                    catch { } //默认第一行,不管
                }
            }
            return myListArray.ToArray();
        }
        /// <summary>
        /// 获得文件和目录列表
        /// </summary>
        /// <paramKey colName="datastring">FTP返回的列表字符信息</paramKey>
        /// <paramKey colName="relPath">相对路径</paramKey>
        private FileStruct[] GetList(string datastring)
        {
            return GetList(datastring, "");
        }

        /// <summary>
        /// 从Windows格式中返回文件信息
        /// </summary>
        /// <paramKey colName="Record">文件信息</paramKey>
        /// <paramKey colName="RelPath">相对路径</paramKey>
        private FileStruct ParseFileStructFromWindowsStyleRecord(string Record, string RelPath)
        {
            FileStruct f = new FileStruct();
            string processstr = Record.Trim();
            string dateStr = processstr.Substring(0, 8);
            processstr = (processstr.Substring(8, processstr.Length - 8)).Trim();
            string timeStr = processstr.Substring(0, 7);
            processstr = (processstr.Substring(7, processstr.Length - 7)).Trim();
            DateTimeFormatInfo myDTFI = new CultureInfo("en-US", false).DateTimeFormat;
            myDTFI.ShortTimePattern = "t";
            f.CreateTime = DateTime.Parse(dateStr + " " + timeStr, myDTFI);
            if (processstr.Substring(0, 5) == "<DIR>")
            {
                f.IsDirectory = true;
                processstr = (processstr.Substring(5, processstr.Length - 5)).Trim();
                f.Size = 0;
            }
            else
            {
                string[] strs = processstr.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);   // true);
                double size = 0;
                double.TryParse(strs[0], out size);
                f.Size = size;
                processstr = processstr.Substring(processstr.IndexOf(' ') + 1);

                f.IsDirectory = false;
            }
            f.RelPath = RelPath;
            f.Name = processstr;
            return f;
        }


        /// <summary>
        /// 判断文件列表的方式Window方式还是Unix方式
        /// </summary>
        /// <paramKey colName="recordList">文件信息列表</paramKey>
        private FileListStyle GuessFileListStyle(string[] recordList)
        {
            foreach (string s in recordList)
            {
                if (s.Length > 10
                 && Regex.IsMatch(s.Substring(0, 10), "(-|d)(-|r)(-|w)(-|x)(-|r)(-|w)(-|x)(-|r)(-|w)(-|x)"))
                {
                    return FileListStyle.UnixStyle;
                }
                else if (s.Length > 8
                 && Regex.IsMatch(s.Substring(0, 8), "[0-9][0-9]-[0-9][0-9]-[0-9][0-9]"))
                {
                    return FileListStyle.WindowsStyle;
                }
            }
            return FileListStyle.Unknown;
        }

        /// <summary>
        /// 从Unix格式中返回文件信息
        /// </summary>
        /// <paramKey colName="Record">文件信息</paramKey>
        /// <paramKey colName="RelPath">相对路径</paramKey>
        private FileStruct ParseFileStructFromUnixStyleRecord(string Record, string RelPath)
        {
            FileStruct f = new FileStruct();
            string processstr = Record.Trim();
            f.Flags = processstr.Substring(0, 10);
            f.IsDirectory = (f.Flags[0] == 'd');
            processstr = (processstr.Substring(11)).Trim();
            _cutSubstringFromStringWithTrim(ref processstr, ' ', 0);   //跳过一部分
            f.Owner = _cutSubstringFromStringWithTrim(ref processstr, ' ', 0);
            f.Group = _cutSubstringFromStringWithTrim(ref processstr, ' ', 0);
            try
            {
                f.Size = int.Parse(_cutSubstringFromStringWithTrim(ref processstr, ' ', 0));   //跳过一部分
            }
            catch { }
            string yearOrTime = processstr.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[2];
            if (yearOrTime.IndexOf(":") >= 0)  //time
            {
                processstr = processstr.Replace(yearOrTime, DateTime.Now.Year.ToString());
            }
            f.CreateTime = DateTime.Parse(_cutSubstringFromStringWithTrim(ref processstr, ' ', 8));
            f.Name = processstr;   //最后就是名称
            f.RelPath = RelPath;//相对路径
            return f;
        }

        /// <summary>
        /// 按照一定的规则进行字符串截取
        /// </summary>
        /// <paramKey colName="s">截取的字符串</paramKey>
        /// <paramKey colName="c">查找的字符</paramKey>
        /// <paramKey colName="startIndex">查找的位置</paramKey>
        private string _cutSubstringFromStringWithTrim(ref string s, char c, int startIndex)
        {
            int pos1 = s.IndexOf(c, startIndex);
            string retString = s.Substring(0, pos1);
            s = (s.Substring(pos1)).Trim();
            return retString;
        }
        #endregion

        #region 目录或文件存在的判断
        /// <summary>
        /// 判断当前目录下指定的子目录是否存在
        /// </summary>
        /// <paramKey colName="RemoteDirectoryName">指定的目录名</paramKey>
        public bool DirectoryExist(string RemoteDirectoryName)
        {
            try
            {
                return DirectoryExist(RemoteDirectoryName, "");
            }
            catch (Exception ep)
            {
                ErrorMsg = ep.ToString();
                throw ep;
            }
        }

        public bool DirectoryExist(string RemoteDirectoryName,string Folder)
        {
            try
            {
                if (!IsValidPathChars(RemoteDirectoryName))
                {
                    throw new Exception("目录名非法！");
                }

                FileStruct[] listDir = ListDirectories(Folder);
                foreach (FileStruct dir in listDir)
                {
                    if (dir.Name == RemoteDirectoryName)
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ep)
            {
                ErrorMsg = ep.ToString();
                throw ep;
            }
        }
        /// <summary>
        /// 判断一个远程文件是否存在服务器当前目录下面
        /// </summary>
        /// <paramKey colName="RemoteFileName">远程文件名</paramKey>
        public bool FileExist(string RemoteFileName)
        {
            try
            {
                if (!IsValidFileChars(RemoteFileName))
                {
                    throw new Exception("文件名非法！");
                }
                FileStruct[] listFile = ListFiles();
                foreach (FileStruct file in listFile)
                {
                    if (file.Name == RemoteFileName)
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ep)
            {
                ErrorMsg = ep.ToString();
                throw ep;
            }
        }
        #endregion
        #region 删除文件
        /// <summary>
        /// 从FTP服务器上面删除一个文件
        /// </summary>
        /// <paramKey colName="RemoteFileName">远程文件名</paramKey>
        public void DeleteFile(string RemoteFileName)
        {
            try
            {
                if (!IsValidFileChars(RemoteFileName))
                {
                    throw new Exception("文件名非法！");
                }
                Response = Open(new Uri(this.Uri.ToString() + RemoteFileName), WebRequestMethods.Ftp.DeleteFile);
            }
            catch (Exception ep)
            {
                ErrorMsg = ep.ToString();
                throw ep;
            }
        }
        #endregion
 
 
        #region 建立、删除子目录
        /// <summary>
        /// 在FTP服务器上当前工作目录建立一个子目录
        /// </summary>
        /// <paramKey colName="DirectoryName">子目录名称</paramKey>
        public bool MakeDirectory(string DirectoryName)
        {
            try
            {
                if (!IsValidPathChars(DirectoryName))
                {
                    throw new Exception("目录名非法！");
                }
                //if (DirectoryExist(DirectoryName))
                //{
                //    //throw new Exception("服务器上面已经存在同名的文件名或目录名！");
                //    return true;
                //}
                //创建目录 时,上级目录不存在,则创建
                string strFolder = DirectoryName;
                string[] str = strFolder.Split('/');
                string str123 = "";
                string strTemp = "";
                for(int i=0;i<str.Length;i++)
                {
                    if (str[i].Equals(""))
                        continue;

                    if (!DirectoryExist(str[i], strTemp))
                    {
                        str123 = this.Uri.ToString().TrimEnd('/') + strTemp + "/" + str[i];
                        FtpWebResponse ftpRes =  Open(new Uri(str123), WebRequestMethods.Ftp.MakeDirectory);
                        ftpRes.Close();
                    }
                    strTemp = strTemp + "/" + str[i];
                }
 
               // Response = Open(new Uri(this.Uri.ToString() + DirectoryName), WebRequestMethods.Ftp.MakeDirectory);
                return true;
            }
            catch (Exception ep)
            {
                ErrorMsg = ep.ToString();
                throw ep;
            }
        }
        /// <summary>
        /// 从当前工作目录中删除一个子目录
        /// </summary>
        /// <paramKey colName="DirectoryName">子目录名称</paramKey>
        public bool RemoveDirectory(string DirectoryName)
        {
            try
            {
                if (!IsValidPathChars(DirectoryName))
                {
                    throw new Exception("目录名非法！");
                }
                if (!DirectoryExist(DirectoryName))
                {
                    throw new Exception("服务器上面不存在指定的文件名或目录名！");
                }
                Response = Open(new Uri(this.Uri.ToString() + DirectoryName), WebRequestMethods.Ftp.RemoveDirectory);
                Dispose();
                return true;
            }
            catch (Exception ep)
            {
                ErrorMsg = ep.ToString();
                throw ep;
            }
        }
        #endregion
        #region 文件、目录名称有效性判断
        /// <summary>
        /// 判断目录名中字符是否合法
        /// </summary>
        /// <paramKey colName="DirectoryName">目录名称</paramKey>
        public bool IsValidPathChars(string DirectoryName)
        {
            char[] invalidPathChars = Path.GetInvalidPathChars();
            char[] DirChar = DirectoryName.ToCharArray();
            foreach (char C in DirChar)
            {
                if (Array.BinarySearch(invalidPathChars, C) >= 0)
                {
                    return false;
                }
            }
            return true;
        }
        /// <summary>
        /// 判断文件名中字符是否合法
        /// </summary>
        /// <paramKey colName="FileName">文件名称</paramKey>
        public bool IsValidFileChars(string FileName)
        {
            //char[] invalidFileChars = Path.GetInvalidFileNameChars();
            //char[] NameChar = FileName.ToCharArray();
            //foreach (char C in NameChar)
            //{
            //    if (Array.BinarySearch(invalidFileChars, C) >= 0)
            //    {
            //        return false;
            //    }
            //}
            return true;
        }
        #endregion
        #region 目录切换操作
        /// <summary>
        /// 进入一个目录
        /// </summary>
        /// <paramKey colName="DirectoryName">
        /// 新目录的名字。 
        /// 说明：如果新目录是当前目录的子目录，则直接指定子目录。如: SubDirectory1/SubDirectory2 ； 
        /// 如果新目录不是当前目录的子目录，则必须从根目录一级一级的指定。如： ./NewDirectory/SubDirectory1/SubDirectory2
        /// </paramKey>
        public bool GotoDirectory(string DirectoryName)
        {
            string CurrentWorkPath = this.DirectoryPath;
            try
            {
                DirectoryName = DirectoryName.Replace("\\", "/");
                string[] DirectoryNames = DirectoryName.Split(new char[] { '/' });
                if (DirectoryNames[0] == ".")
                {
                    this.DirectoryPath = "/";
                    if (DirectoryNames.Length == 1)
                    {
                        return true;
                    }
                    Array.Clear(DirectoryNames, 0, 1);
                }
                bool Success = false;
                foreach (string dir in DirectoryNames)
                {
                    if (dir != null)
                    {
                        Success = EnterOneSubDirectory(dir);
                        if (!Success)
                        {
                            this.DirectoryPath = CurrentWorkPath;
                            return false;
                        }
                    }
                }
                return Success;

            }
            catch (Exception ep)
            {
                this.DirectoryPath = CurrentWorkPath;
                ErrorMsg = ep.ToString();
                throw ep;
            }
        }
        /// <summary>
        /// 从当前工作目录进入一个子目录
        /// </summary>
        /// <paramKey colName="DirectoryName">子目录名称</paramKey>
        private bool EnterOneSubDirectory(string DirectoryName)
        {
            try
            {
                if (DirectoryName.IndexOf("/") >= 0 || !IsValidPathChars(DirectoryName))
                {
                    throw new Exception("目录名非法!");
                }
                if (DirectoryName.Length > 0 && DirectoryExist(DirectoryName))
                {
                    if (!DirectoryName.EndsWith("/"))
                    {
                        DirectoryName += "/";
                    }
                    _DirectoryPath += DirectoryName;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ep)
            {
                ErrorMsg = ep.ToString();
                throw ep;
            }
        }
        /// <summary>
        /// 从当前工作目录往上一级目录
        /// </summary>
        public bool ComeoutDirectory()
        {
            if (_DirectoryPath == "/")
            {
                ErrorMsg = "当前目录已经是根目录！";
                throw new Exception("当前目录已经是根目录！");
            }
            char[] sp = new char[1] { '/' };

            string[] strDir = _DirectoryPath.Split(sp, StringSplitOptions.RemoveEmptyEntries);
            if (strDir.Length == 1)
            {
                _DirectoryPath = "/";
            }
            else
            {
                _DirectoryPath = String.Join("/", strDir, 0, strDir.Length - 1);
            }
            return true;

        }
        #endregion
        #region 重载WebClient，支持FTP进度
        internal class MyWebClient : WebClient
        {
            public bool usePassive = false;
            public int timeOut = -1;

            protected override WebRequest GetWebRequest(Uri address)
            {
                FtpWebRequest req = (FtpWebRequest)base.GetWebRequest(address);
                //如果 UsePassive 被设置为 true，FTP 服务器可能不会发送文件的大小，而且下载进度可能始终为零。
                //如果 UsePassive 被设置为 false，则防火墙可能会引发警报并阻止文件下载。
                req.UsePassive = usePassive;
                if (timeOut > 0)
                {
                    req.Timeout = timeOut;
                }
                return req;
            }
        }
        #endregion

        #region IDisposable 成员

        public void Dispose()
        {
            if (Response != null)
            {
                Response.Close();
                Response = null;
            }
            if (Request != null)
            {
                Request.Abort();

                Request = null;
            }
        }

        #endregion
    }
}
