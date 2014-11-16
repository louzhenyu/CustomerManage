using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// hash的算法类型
    /// </summary>
    public enum HashProviderType
    {
        SHA1,
        MD5,
        LMD5
    }

    /// <summary>
    /// 加密/解密管理器
    /// </summary>
    public class EncryptManager
    {
        /// <summary>
        /// 获取字符串的hash值
        /// </summary>
        /// <param name="input">字符串</param>
        /// <param name="providerType">hash算法类型</param>
        /// <returns></returns>
        public static string Hash(string input, HashProviderType providerType)
        {
            string ret;
            switch (providerType)
            {
                case HashProviderType.SHA1:
                    SHA1 sha1 = new SHA1CryptoServiceProvider();
                    byte[] sha1_in = UTF8Encoding.Default.GetBytes(input);
                    byte[] sha1_out = sha1.ComputeHash(sha1_in);
                    ret = BitConverter.ToString(sha1_out).Replace("-", "").ToLower();
                    break;
                case HashProviderType.LMD5:
                    MD5 lmd5 = new MD5CryptoServiceProvider();
                    byte[] lmd5_in = UTF8Encoding.Default.GetBytes(input);
                    byte[] lmd5_out = lmd5.ComputeHash(lmd5_in);
                    ret = BitConverter.ToString(lmd5_out).Replace("-", "").ToLower();
                    ret = ret.Substring(8, 16);
                    break;
                case HashProviderType.MD5:
                    MD5 md5 = new MD5CryptoServiceProvider();
                    byte[] md5_in = UTF8Encoding.Default.GetBytes(input);
                    byte[] md5_out = md5.ComputeHash(md5_in);
                    ret = BitConverter.ToString(md5_out).Replace("-", "").ToLower();
                    return ret;
                default:
                    ret = input;
                    break;
            }
            return ret;
        }
    }
}
