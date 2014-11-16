using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace JIT.CPOS.Web.ApplicationInterface.Project.HuaAn
{
    /// <summary>
    /// 华安接口工具类定义。
    /// </summary>
    public class Utility
    {
        #region AES加密，解密
        /// <summary>
        /// AES加密
        /// </summary>
        /// <param name="toEncrypt">需要加密的字符串</param>
        /// <param name="key">密钥:密钥是经过Base64编码过的，所以在使用的时候要进行解码处理。</param>
        /// <returns>AES加密后的返回的字符串。</returns>
        public static string AESEncrypt(string toEncrypt, string key)
        {
            // byte[] keyArray = UTF8Encoding.UTF8.GetBytes(key);
            byte[] keyArray = Convert.FromBase64String(key);   //将密钥进行Base64解码处理（因为key本来就是经过Base64编码过的，故此处需要进行解码处理）
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

            RijndaelManaged rDel = new RijndaelManaged();
            rDel.Key = keyArray;
            rDel.Mode = CipherMode.ECB;
            rDel.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = rDel.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        /// <summary>
        /// AES解密。
        /// </summary>
        /// <param name="toDecrypt">解密字符串。</param>
        ///  /// <param name="key">密钥。</param>
        /// <returns>Aes 解密后的字符串。</returns>
        public static string AESDecrypt(string toDecrypt, string key)
        {
            //  byte[] keyArray = UTF8Encoding.UTF8.GetBytes(key);
            byte[] keyArray = Convert.FromBase64String(key);   //将密钥进行Base64解码处理（因为key本来就是经过Base64编码过的，故此处需要进行解码处理）
            byte[] toEncryptArray = Convert.FromBase64String(toDecrypt);

            RijndaelManaged rDel = new RijndaelManaged();
            rDel.Key = keyArray;
            rDel.Mode = CipherMode.ECB;
            rDel.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = rDel.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return UTF8Encoding.UTF8.GetString(resultArray);
        }
        #endregion


        #region  MD5加密
        /// <summary>
        /// MD5加密。
        /// </summary>
        /// <param name="strMaccode">加密字符串。</param>
        /// <returns>MD5后的字符串。</returns>
        public static string GenMD5(string strMaccode)
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

        /// <summary>
        /// Base64编码
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string Base64Encode(string data)
        {
            try
            {
                byte[] encData_byte = new byte[data.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(data);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception e)
            {
                throw new Exception("Error in base64Encode" + e.Message);
            }
        }

        /// <summary>
        /// Base64解码
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string Base64Decode(string data)
        {
            try
            {
                System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
                System.Text.Decoder utf8Decode = encoder.GetDecoder();
                byte[] todecode_byte = Convert.FromBase64String(data);
                int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
                char[] decoded_char = new char[charCount];
                utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
                string result = new String(decoded_char);
                return result;
            }
            catch (Exception e)
            {
                throw new Exception("Error in base64Decode" + e.Message);
            }
        }

        #endregion


        #region  解析XML。
        /// <summary>  
        /// 将XML序列化为对象。
        /// </summary>  
        /// <param name="type">类型</param>  
        /// <param name="strXml">XML字符串</param>  
        /// <returns></returns>  
        public static object XMLDeserialize(Type type, string strXml)
        {
            try
            {
                using (StringReader sr = new StringReader(strXml))
                {
                    XmlSerializer xmldes = new XmlSerializer(type);
                    return xmldes.Deserialize(sr);
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        #endregion

        #region 根据请求对象获取xml表单(From表单content)
        /// <summary>
        /// 获取请求xml
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string GetRequsetXml(object obj)
        {
            string xml = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><order>";
            foreach (System.Reflection.PropertyInfo p in obj.GetType().GetProperties())
            {
                xml += "<" + p.Name + ">" + p.GetValue(obj, null) + "</" + p.Name + ">";
            }
            xml += "</order>";
            return xml;
        }
        #endregion
    }

    /// <summary>
    /// http请求辅助类。
    /// </summary>
    public sealed class HttpHelper
    {
        // Cookie容器，用于保持会话。
        private static CookieContainer ms_cookieContainer = new CookieContainer();
        private static string ms_userAgent = "User-Agent: Mozilla/5.0 (Windows NT 6.1; Trident/7.0; rv:11.0) like Gecko";

        /// <summary>  
        /// 创建GET方式的HTTP请求  
        /// </summary>  
        public static HttpWebResponse CreateGetHttpResponse(string url, string referer, int timeout, CookieCollection cookies)
        {
            HttpWebRequest request = null;
            request = CreateRequest(url);
            request.Referer = referer;
            request.Method = "GET";
            request.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";

            if (cookies != null)
            {
                request.CookieContainer.Add(cookies);
            }

            return request.GetResponse() as HttpWebResponse;
        }

        private static HttpWebRequest CreateRequest(string url)
        {
            HttpWebRequest request = null;
            if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
            {
                //对服务端证书进行有效性校验（非第三方权威机构颁发的证书，如自己生成的，不进行验证，这里返回true）
                //ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                request = WebRequest.Create(url) as HttpWebRequest;
                // request.ProtocolVersion = HttpVersion.Version10;    //http版本，默认是1.1,这里设置为1.0
            }
            else
            {
                request = WebRequest.Create(url) as HttpWebRequest;
            }
            request.CookieContainer = ms_cookieContainer;
            request.UserAgent = ms_userAgent;

            return request;
        }

        /// <summary>  
        /// 创建POST方式的HTTP请求  
        /// </summary>  
        public static HttpWebResponse CreatePostHttpResponse(string url, string referer, IDictionary<string, string> parameters, int timeout, CookieCollection cookies)
        {
            HttpWebRequest request = CreateRequest(url);

            request.Method = "POST";
            request.Referer = referer;
            request.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";

            if (cookies != null)
            {
                request.CookieContainer.Add(cookies);
            }

            //发送POST数据  
            if (!(parameters == null || parameters.Count == 0))
            {
                StringBuilder buffer = new StringBuilder();
                int i = 0;
                foreach (string key in parameters.Keys)
                {
                    if (i > 0)
                    {
                        buffer.AppendFormat("&{0}={1}", key, parameters[key]);
                    }
                    else
                    {
                        buffer.AppendFormat("{0}={1}", key, parameters[key]);
                        i++;
                    }
                }
                byte[] data = Encoding.ASCII.GetBytes(buffer.ToString());
                using (Stream stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
            }
            string[] values = request.Headers.GetValues("Content-Type");
            return request.GetResponse() as HttpWebResponse;
        }

        private static readonly string DefaultUserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";

        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true; //总是接受  
        }

        /// <summary>  
        /// 创建POST方式的HTTP请求  
        /// </summary>  
        /// <param name="url">请求的URL</param>  
        /// <param name="parameters">随同请求POST的参数名称及参数值字典</param>  
        /// <param name="timeout">请求的超时时间</param>  
        /// <param name="userAgent">请求的客户端浏览器信息，可以为空</param>  
        /// <param name="requestEncoding">发送HTTP请求时所用的编码</param>  
        /// <param name="cookies">随同HTTP请求发送的Cookie信息，如果不需要身份验证可以为空</param>  
        /// <returns></returns>  
        public static HttpWebResponse CreatePostHttpResponse(string url, IDictionary<string, string> parameters, int? timeout, string userAgent, Encoding requestEncoding, CookieCollection cookies)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException("url");
            }
            if (requestEncoding == null)
            {
                throw new ArgumentNullException("requestEncoding");
            }
            HttpWebRequest request = null;
            //如果是发送HTTPS请求  
            if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                request = WebRequest.Create(url) as HttpWebRequest;
                request.ProtocolVersion = HttpVersion.Version10;
            }
            else
            {
                request = WebRequest.Create(url) as HttpWebRequest;
            }
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";

            if (!string.IsNullOrEmpty(userAgent))
            {
                request.UserAgent = userAgent;
            }
            else
            {
                request.UserAgent = DefaultUserAgent;
            }

            if (timeout.HasValue)
            {
                request.Timeout = timeout.Value;
            }
            if (cookies != null)
            {
                request.CookieContainer = new CookieContainer();
                request.CookieContainer.Add(cookies);
            }
            //如果需要POST数据  
            if (!(parameters == null || parameters.Count == 0))
            {
                StringBuilder buffer = new StringBuilder();
                int i = 0;
                foreach (string key in parameters.Keys)
                {
                    if (i > 0)
                    {
                        buffer.AppendFormat("&{0}={1}", key, parameters[key]);
                    }
                    else
                    {
                        buffer.AppendFormat("{0}={1}", key, parameters[key]);
                    }
                    i++;
                }
                byte[] data = requestEncoding.GetBytes(buffer.ToString());
                using (Stream stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
            }
            return request.GetResponse() as HttpWebResponse;
        }

        /// <summary>
        /// 创建POST方式的HTTP请求  
        /// </summary>
        /// <param name="url">Post Url。</param>
        /// <param name="requestData">提交数据。</param>
        /// <returns></returns>
        public static string CreatePostHttpResponse(string url, string requestData)
        {
            //Web访问对象。
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);

            //转成网络流。
            byte[] postBytes = Encoding.UTF8.GetBytes(requestData);

            //设置。
            request.Method = "POST";
            int postBytesLength = postBytes.Length;
            request.ContentLength = postBytesLength;
            request.ContentType = "application/json";
            request.MaximumAutomaticRedirections = 1;
            request.AllowAutoRedirect = true;

            //发送请求。
            using (Stream outstream = request.GetRequestStream())
            {
                outstream.Write(postBytes, 0, postBytesLength);
            }

            // 获得接口返回值。
            string result = string.Empty;
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                if (response != null)
                {
                    result = GetResponseString(response);

                }
            }
            return result;
        }

        /// <summary>
        /// 获取请求的数据
        /// </summary>
        public static string GetResponseString(HttpWebResponse webresponse)
        {
            using (Stream stream = webresponse.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                {
                    return reader.ReadToEnd();
                }
            }
        }

    }
}