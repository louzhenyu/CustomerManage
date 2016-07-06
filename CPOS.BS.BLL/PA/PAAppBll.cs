using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using CPOS.Common;
using JIT.CPOS.BS.Entity;
using JIT.Utility.Web;

namespace JIT.CPOS.BS.BLL.PA
{
    public static class PAAppBLL
    {
        /// <summary>
        /// 获取签名
        /// </summary>
        /// <typeparam name="T">请求实体类型</typeparam>
        /// <param name="pObj">请求实体</param>
        /// <returns></returns>
        public static void GetSecuritySign<T>(this T pObj)
        {
            Type type = pObj.GetType();
            PropertyInfo[] pro = type.GetProperties();
            PropertyInfo signProp = null;
            Dictionary<string, string> dic = new Dictionary<string, string>();
            foreach (PropertyInfo p in pro)
            {
                // 获取签名的字段
                if (p.GetCustomAttributes(typeof(SignatureFieldAttribute), false).Any())
                {
                    signProp = p;
                }
                // 可以采用特性来进行需要签名或者不需要签名的字段排除
                if (p.GetCustomAttributes(typeof(IgnoreSignatureAttribute), false).Any())
                {
                    continue;
                }
                if (p.GetValue(pObj, null) == null)
                {
                    dic.Add(p.Name, string.Empty);
                }
                else
                {
                    string tmpValue = p.GetValue(pObj, null).ToString();
                    //tmpValue = System.Web.HttpUtility.UrlEncode(tmpValue, Encoding.UTF8);
                    dic.Add(p.Name, tmpValue);
                }
            }
            if (dic.Count == 0)
            {
                return;
            }

            // 排序
            string[] keys = dic.Select(d => d.Key).ToArray();
            Array.Sort(keys, string.CompareOrdinal);

            string signValue = string.Empty;
            foreach (string key in keys)
            {
                signValue += string.Format("{0}={1}&", key, dic[key]);
            }
            signValue = signValue.Substring(0, signValue.Length - 1);
            // sha256加密
            byte[] sha256SignValue = HashEncryptHelper.SHA256EncryptOutByte(signValue);
            // RSA签名
            string rsaSginValue = BouncyCastleHelper.PrivateKeyEncrypt(sha256SignValue, ConfigHelper.GetAppSetting("PrivateKey", "MIICcwIBADANBgkqhkiG9w0BAQEFAASCAl0wggJZAgEAAoGBAIBGPyb6ovk75UvvnyEhonj9ocshUqJ0yxUyT1GAJ2Y3V7qAS/vzprQREJrWDEtRnlnoyuUuKRcrlXBriPSxy2nVoW5KsV3zXBQsi0GtoS9CNyVy+J92ZRCsEu/XRGWQ5+lvNfKVnJDK3BW+cyVfWMPFa35bvURlSq3wFxBnrSVLAgEDAoGAFWEKhn8bKYn7jKfv2trwaX+a9zA4cGjMg4hijZVb5l6OnxVh/1NGc1gtbyOstzhFDvwh0N0G2THuPWdBfh2h5ryF+QeR7X0AszWgD5HJ910Rc42PePwYEMA1b8bk3aAWBqwh36pHA3uNP0J71qQ4wpQMG1UWvWm3gEZkP6DKPlMCQQC4pyvrDJJ3nsDaRguyQ0bNSER8J474eN5dHXMUZRkiE40F0qW2WL5iLzljwSFErp+VrLdxYIJFZii7cBAx5oZ5AkEAsdZsMjk6jbkgEITYkKIcRoYrpXQ6lVvNzbTeGYIarlA0YJgO4Zm9g11gy8r8QVWUt3Yi69RRpZeDjk2CcQko4wJAexodR122+mnV5tldIYIvM4WC/W+0paXpk2j3Yu4QwWJeA+HDzuXUQXTQ7Strgx8VDnMk9kBW2O7F0kq1dpmu+wJAdo7yzCYnCSYVYFiQYGwS2a7Hw6LRuOfeiSM+u6wRyYrNlbq0lmZ+V5OV3TH9gOO4ek7B8o2Lw7pXtDOsS1twlwJAb/L+zygG9xbsxHcsFIcx2XLi+XZKRi/4dvV/E+EbsRWPmQkgZGQc5xlP4LWA9PrOtrtmingw4L/OU1yb/FGd3w=="));
            // 签名
            signProp.SetValue(pObj, rsaSginValue, null);
        }


        /// <summary>
        /// 验证签名
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pObj"></param>
        /// <returns></returns>
        public static bool CheckSecuritySign<T>(this T pObj)
        {
            bool flag = false;
            string signStr = string.Empty;

            Type type = pObj.GetType();
            PropertyInfo[] pro = type.GetProperties();
            Dictionary<string, string> dic = new Dictionary<string, string>();
            foreach (PropertyInfo p in pro)
            {
                // 获取签名字段的签名值
                if (p.GetCustomAttributes(typeof(SignatureFieldAttribute), false).Any())
                {
                    signStr = p.GetValue(pObj, null).ToString();
                }

                // 可以采用特性来进行需要签名或者不需要签名的字段排除
                if (p.GetCustomAttributes(typeof(IgnoreSignatureAttribute), false).Any())
                {
                    continue;
                }
                if (p.GetValue(pObj, null) == null)
                {
                    dic.Add(p.Name, string.Empty);
                }
                else
                {
                    dic.Add(p.Name, p.GetValue(pObj, null).ToString());
                }
            }
            if (dic.Count == 0)
            {
                return flag;
            }
            // 排序
            string[] keys = dic.Select(d => d.Key).ToArray();
            Array.Sort(keys, string.CompareOrdinal);

            string signValue = string.Empty;
            foreach (string key in keys)
            {
                signValue += string.Format("{0}={1}&", key, dic[key]);
            }
            signValue = signValue.Substring(0, signValue.Length - 1);
            // sha256加密
            string sha256SignValue = HashEncryptHelper.SHA256Encrypt(signValue);
            // RSA签名解密出来的值
            string rsaSginValue = BouncyCastleHelper.PublicKeyDecrypt(signStr, System.Configuration.ConfigurationManager.AppSettings["PAPublicKey"]);

            // 用自己SHA256加密后跟签名里面的做对比是否一样防止串改
            return sha256SignValue.Equals(rsaSginValue);
        }

        /// <summary>
        /// DateTime时间格式转换为Unix时间戳格式
        /// </summary>
        /// <param name=”time”></param>
        /// <returns></returns>
        public static int ConvertDateTimeInt(System.DateTime time)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            return (int)(time - startTime).TotalSeconds;
        }

    }
}
