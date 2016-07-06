using System;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace CPOS.Common
{
    /// <summary>
    /// 类名：HashEncrypt
    /// 作用：对传入的字符串进行Hash运算，返回通过Hash算法加密过的字串。
    /// 属性：［无］
    /// 构造函数额参数：
    /// IsReturnNum:是否返回为加密后字符的Byte代码
    /// IsCaseSensitive：是否区分大小写。
    /// 方法：此类提供MD5，SHA1，SHA256，SHA512等四种算法，加密字串的长度依次增大。
    public class HashEncryptHelper
    {
        /// <summary>
        /// sha256加密返回base64编码
        /// </summary>
        /// <param name="strIN"></param>
        /// <returns></returns>
        public static string SHA256Encrypt(string strIN)
        {
            System.Security.Cryptography.SHA256 s256 = new System.Security.Cryptography.SHA256Managed();
            byte[] byte1;
            byte1 = s256.ComputeHash(Encoding.UTF8.GetBytes(strIN));
            s256.Clear();
            return Convert.ToBase64String(byte1);
        }
        /// <summary>
        /// sha256加密返回base64编码
        /// </summary>
        /// <param name="strIN"></param>
        /// <returns></returns>
        public static byte[] SHA256EncryptOutByte(string strIN)
        {
            System.Security.Cryptography.SHA256 s256 = new System.Security.Cryptography.SHA256Managed();
            byte[] byte1;
            byte1 = s256.ComputeHash(Encoding.UTF8.GetBytes(strIN));
            s256.Clear();
            return byte1;
        }
    }
}
