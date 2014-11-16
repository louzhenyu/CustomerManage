using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.Compression;

namespace JIT.CPOS.Common
{
    public class CommonCompress
    {
        /// <summary>  
        /// 压缩字符串  
        /// </summary>  
        /// <param name="strUncompressed">未压缩的字符串</param>  
        /// <returns>压缩的字符串</returns>  
        public static string StringCompress(string strUncompressed)
        {
            byte[] bytData = System.Text.Encoding.Unicode.GetBytes(strUncompressed);
            MemoryStream ms = new MemoryStream();
            Stream s = new GZipStream(ms, CompressionMode.Compress);
            s.Write(bytData, 0, bytData.Length);
            s.Close();
            byte[] dataCompressed = (byte[])ms.ToArray();
            return System.Convert.ToBase64String(dataCompressed, 0, dataCompressed.Length);
        }

        /// <summary>  
        /// 解压缩字符串  
        /// </summary>  
        /// <param name="strCompressed">压缩的字符串</param>  
        /// <returns>未压缩的字符串</returns>  
        public static string StringDeCompress(string strCompressed)
        {
            System.Text.StringBuilder strUncompressed = new System.Text.StringBuilder();
            int totalLength = 0;
            byte[] bInput = System.Convert.FromBase64String(strCompressed); ;
            byte[] dataWrite = new byte[4096];
            Stream s = new GZipStream(new MemoryStream(bInput), CompressionMode.Decompress);
            while (true)
            {
                int size = s.Read(dataWrite, 0, dataWrite.Length);
                if (size > 0)
                {
                    totalLength += size;
                    strUncompressed.Append(System.Text.Encoding.Unicode.GetString(dataWrite, 0, size));
                }
                else
                {
                    break;
                }
            }
            s.Close();
            return strUncompressed.ToString();
        }  

    }
}
