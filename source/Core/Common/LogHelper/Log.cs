using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VSDT.Common
{
 
    public delegate void CommonLogMsgEvent(LogMsgEventArgs e);
    public class LogMsgEventArgs : EventArgs
    {
        public LogMsgEventArgs(string msg)
        {
            Message = msg;
        }
        public string Message;
    }
    public class Log
    {
        public static bool ISDEBUG = true;

        public static event CommonLogMsgEvent LogMsgEvent;

        public static void LogMsg(string msg)
        {
            if (LogMsgEvent != null)
            {
                LogMsgEventArgs args = new LogMsgEventArgs(msg);

                LogMsgEvent(args);
            }
           
             System.Console.WriteLine(msg);
           
        }

        public static void ShowMsgBox(string msg)
        {
            MessageBox.Show(msg);
        }
        public static void ShowErrorBox(string msg)
        {
            if (ISDEBUG)
            {
                MessageBox.Show(msg);
            }
            else
            {
                LogMsg(msg);
            }
        }
        public static void ShowErrorBox(Exception ex)
        {
            if (ISDEBUG)
            {
                MessageBox.Show(BuildExceptionString(ex)); ;
            }
            else
            {
                LogMsg(BuildExceptionString(ex));
            }
        }
        private static string BuildExceptionString(Exception exception)
        {
            string errMessage = string.Empty;

            errMessage +=
                exception.Message + Environment.NewLine + exception.StackTrace;

            while (exception.InnerException != null)
            {
                errMessage += BuildInnerExceptionString(exception.InnerException);

                exception = exception.InnerException;
            }

            return errMessage;
        }

        private static string BuildInnerExceptionString(Exception innerException)
        {
            string errMessage = string.Empty;

            errMessage += Environment.NewLine + " InnerException ";
            errMessage += Environment.NewLine + innerException.Message + Environment.NewLine + innerException.StackTrace;

            return errMessage;
        } 
    }
}
