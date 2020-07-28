using System;
using System.Collections.Generic;
using System.Text;

namespace PictureAnnotationForm.Utils
{
    /// <summary>
    /// 日记帮助类
    /// </summary>
    public class LogUtils
    {
        /// <summary>
        /// 日记输出,参数
        /// </summary>
        public static event Action<LogType, string> PrintLog;
        internal static void Log(string log)
        {
            PrintLog?.Invoke(LogType.Log, log);
        }
        internal static void Error(string log)
        {
            PrintLog?.Invoke(LogType.Error, log);
        }
        public enum LogType{
            Log,
            Error
        }
    }
}
