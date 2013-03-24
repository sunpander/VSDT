using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EP
{
    public class EPESCommon
    {
        /// <summary>
        /// 辅助类，返回当前授权模式为经典模式或9672模式
        /// 2011-11-30 yuxiuwen 创建；
        /// </summary>
        public static AUTHMODE AuthMode
        {
            get
            {
                
                    return AUTHMODE.MODE_CLASSIC;
                
            }
        }

        public static int GetStringLength(string str)
        {
            System.Text.Encoding encoding = System.Text.Encoding.Default;
            try
            {
                encoding = System.Text.Encoding.GetEncoding("");
            }
            catch 
            {

            }

            return encoding.GetByteCount(str);
        }
    }

    public enum AUTHMODE
    { 
        MODE_CLASSIC, MODE_9672
    }
}
