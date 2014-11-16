using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

namespace JIT.CPOS.Web.ApplicationInterface.Product.CW
{
    public class CWHelper
    {
        #region  MD5加密
        /// <summary>
        /// MD5加密。
        /// </summary>
        /// <param name="strMaccode">加密字符串。</param>
        /// <returns>MD5后的字符串。</returns>
        public static string MD5Encrypt(string strMaccode)
        {
            System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] startArray = Encoding.UTF8.GetBytes(strMaccode);

            byte[] overArray = md5.ComputeHash(startArray);
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < overArray.Length; i++)
            {
                builder.AppendFormat("{0:x2}", overArray[i]);
            }
            string returnValue = builder.ToString();

            return returnValue;
        }
        #endregion
    }
}