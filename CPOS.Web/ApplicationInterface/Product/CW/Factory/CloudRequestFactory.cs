using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Text;
using System.IO;
using System.Xml;

namespace JIT.CPOS.Web.ApplicationInterface.Product.CW
{
    enum EBodyType : uint
    {
        EType_XML = 0,
        EType_JSON
    };

    /// <summary>
    /// 云通讯第三方请求逻辑处理类。 
    /// </summary>
    public class CloudRequestFactory
    {
        /// <summary>
        /// 初始化。
        /// </summary>

        public CloudRequestFactory()
        {
        }

        private EBodyType m_bodyType = EBodyType.EType_JSON;

        /// <summary>
        /// 服务器api版本
        /// </summary>
        const string softVer = "2013-12-26";

        /// <summary>
        /// 创建子帐号
        /// </summary>
        /// <param name="restAddress">服务器地址</param>
        /// <param name="restPort">服务器端口</param>
        /// <param name="accountSid">主帐号</param>
        /// <param name="accountToken">主帐号令牌</param>
        /// <param name="friendlyName">主帐号令牌</param>
        /// <returns>包体内容</returns>
        public Dictionary<string,object> CreateSubAccount(string restAddress, string restPort, string mainAccount, string mainToken, string appId, string friendlyName)
        {
            if (friendlyName == null)
                throw new ArgumentNullException("friendlyName");

            try
            {
                string date = DateTime.Now.ToString("yyyyMMddhhmmss");

                // 构建URL内容
                string sigstr = MD5Encrypt(mainAccount + mainToken + date);
                string uriStr = string.Format("https://{0}:{1}/{2}/Accounts/{3}/SubAccounts?sig={4}", restAddress, restPort, softVer, mainAccount, sigstr);
                Uri address = new Uri(uriStr);


                // 创建网络请求  
                HttpWebRequest request = WebRequest.Create(address) as HttpWebRequest;
                setCertificateValidationCallBack();

                // 构建Head
                request.Method = "POST";

                Encoding myEncoding = Encoding.GetEncoding("utf-8");
                byte[] myByte = myEncoding.GetBytes(mainAccount + ":" + date);
                string authStr = Convert.ToBase64String(myByte);
                request.Headers.Add("Authorization", authStr);


                // 构建Body
                StringBuilder data = new StringBuilder();

                if (m_bodyType == EBodyType.EType_XML)
                {
                    request.Accept = "application/xml";
                    request.ContentType = "application/xml;charset=utf-8";

                    data.Append("<?xml version='1.0' encoding='utf-8'?><SubAccount>");
                    data.Append("<appId>").Append(appId).Append("</appId>");
                    data.Append("<friendlyName>").Append(friendlyName).Append("</friendlyName>");
                    data.Append("</SubAccount>");
                }
                else
                {
                    request.Accept = "application/json";
                    request.ContentType = "application/json;charset=utf-8";

                    data.Append("{");
                    data.Append("\"appId\":\"").Append(appId).Append("\"");
                    data.Append(",\"friendlyName\":\"").Append(friendlyName).Append("\"");
                    data.Append("}");
                }

                byte[] byteData = UTF8Encoding.UTF8.GetBytes(data.ToString());

                // 开始请求
                using (Stream postStream = request.GetRequestStream())
                {
                    postStream.Write(byteData, 0, byteData.Length);
                }

                // 获取请求
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    StreamReader reader = new StreamReader(response.GetResponseStream());
                    string responseStr = reader.ReadToEnd();

                    if (responseStr != null && responseStr.Length > 0)
                    {
                        Dictionary<string, object> responseResult = new Dictionary<string, object> { { "statusCode", "0" }, { "statusMsg", "成功" }, { "data", null } };

                        if (m_bodyType == EBodyType.EType_XML)
                        {
                            XmlDocument resultXml = new XmlDocument();
                            resultXml.LoadXml(responseStr);
                            XmlNodeList nodeList = resultXml.SelectSingleNode("Response").ChildNodes;
                            foreach (XmlNode item in nodeList)
                            {
                                if (item.Name == "statusCode")
                                {
                                    responseResult["statusCode"] = item.InnerText;
                                }
                                else if (item.Name == "statusMsg")
                                {
                                    responseResult["statusMsg"] = item.InnerText;
                                }
                                else if (item.Name == "SubAccount")
                                {
                                    Dictionary<string, object> retData = new Dictionary<string, object>();
                                    foreach (XmlNode subItem in item.ChildNodes)
                                    {
                                        retData.Add(subItem.Name, subItem.InnerText);
                                    }
                                    responseResult["data"] = new Dictionary<string, object> { { item.Name, retData } };
                                }
                            }
                        }
                        else
                        {
                            responseResult.Clear();
                            responseResult["resposeBody"] = responseStr;
                        }

                        return responseResult;
                    }
                    return new Dictionary<string, object> { { "statusCode", 172002 }, { "statusMsg", "无返回" }, { "data", null } };
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="source">原内容</param>
        /// <returns>加密后内容</returns>
        public static string MD5Encrypt(string source)
        {
            // Create a new instance of the MD5CryptoServiceProvider object.
            System.Security.Cryptography.MD5 md5Hasher = System.Security.Cryptography.MD5.Create();

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(source));

            // Create a new Stringbuilder to collect the bytes and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("X2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        /// <summary>
        /// 设置服务器证书验证回调
        /// </summary>
        public void setCertificateValidationCallBack()
        {
            System.Net.ServicePointManager.ServerCertificateValidationCallback = CertificateValidationResult;
        }


        /// <summary>
        ///  证书验证回调函数  
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="cer"></param>
        /// <param name="chain"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public bool CertificateValidationResult(object obj, System.Security.Cryptography.X509Certificates.X509Certificate cer, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors error)
        {
            return true;
        }
  

    }
}