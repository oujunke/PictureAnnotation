using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace VerificationCodeTest
{
    class SuAnTool
    {
        [DllImport("haoi.dll")]
        public static extern int SendByteEx(string MyUserStr, string GameID, byte[] PicBytes, int lenBytes, int TimeOut, int LostPoint, string BeiZhu, StringBuilder retTid, StringBuilder retStr);

        public static string SuAnDiscern(MemoryStream memoryStream)
        {
            StringBuilder retStr = new StringBuilder(512);
            StringBuilder retTid = new StringBuilder(512);
            try
            {
                SuAnTool.SendByteEx("oufangxin|6DC8149BE2ACA5C2|t:60", "X3106", memoryStream.ToArray(), memoryStream.ToArray().Length, 60, 0, "", retTid, retStr);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
            return retStr.ToString();
        }

        public static MemoryStream StreamToBytes(Stream stream)
        {
            MemoryStream memoryStream = new MemoryStream();
            byte[] buffer = new byte[4096];
            int count;
            while ((count = stream.Read(buffer, 0, 4096)) > 0)
                memoryStream.Write(buffer, 0, count);
            return memoryStream;
        }
    }
}
