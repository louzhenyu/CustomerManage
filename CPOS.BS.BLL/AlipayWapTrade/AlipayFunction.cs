using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Xml;

namespace JIT.CPOS.BS.BLL.AlipayWapTrade
{
    /// <summary>
    /// 类名：Function
    /// 功能：支付宝接口公用函数类
    /// 详细：该类是请求、通知返回两个文件所调用的公用函数核心处理文件，不需要修改
    /// </summary>
    public class Function
    {
        /// <summary>
        /// 签名
        /// </summary>
        /// <param name="dicArrayPre">待签名字符串</param>
        /// <param name="privatekey">商户私钥</param>
        /// <param name="input_charset">编码格式</param>
        /// <returns>经过UrlEncode转码后的签名字符串</returns>
        public static string BuildMysign(SortedDictionary<string, string> dicArrayPre, string privatekey, string input_charset)
        {
            Dictionary<string, string> dicArray = FilterPara(dicArrayPre);
            string prestr = CreateLinkString(dicArray);
            string mysign = RSAFromPkcs8.sign(prestr, privatekey, input_charset);
            mysign = HttpUtility.UrlEncode(mysign, Encoding.GetEncoding(input_charset)); //此处需要对签名进行Encode，否则出现+号等特殊字符，通过base64转换并post提交给支付宝服务器会丢失，变成空格
            return mysign;
        }

        /// <summary>
        /// 验签（不排序 Notify验签用这个）
        /// </summary>
        /// <param name="content">待验签字符串</param>
        /// <param name="signedString">签名（支付宝返回sign）</param>
        /// <param name="publickey">支付宝公钥</param>
        /// <param name="input_charset">编码格式</param>
        /// <returns>返回验签结果，true(相同)，false(不相同)</returns>
        public static bool Verify(string content, string signedString, string publickey, string input_charset)
        {
            bool b = RSAFromPkcs8.verify(content, signedString, publickey, input_charset);
            return b;
        }

        /// <summary>
        /// 验签（排序验签）
        /// </summary>
        /// <param name="sArrary">待签名数组</param>
        /// <param name="signedString">签名（支付宝返回sign）</param>
        /// <param name="publickey">支付宝公钥</param>
        /// <param name="input_charset">编码格式</param>
        /// <returns>返回验签结果，true(相同)，false(不相同)</returns>
        public static bool Verify(SortedDictionary<string, string> sArrary, string signedString, string publickey,
            string input_charset)
        {
            Dictionary<string, string> sPara = Function.FilterPara(sArrary);
            string content = Function.CreateLinkString(sPara);
            bool b = RSAFromPkcs8.verify(content, signedString, publickey, input_charset);
            return b;
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="content">待解密字符串</param>
        /// <param name="privateKey">商户私钥</param>
        /// <param name="input_charset">编码格式</param>
        /// <returns>返回明文</returns>
        public static string Decrypt(string content, string privateKey, string input_charset)
        {
            string strDecryptData = RSAFromPkcs8.decryptData(content, privateKey, input_charset);
            return strDecryptData;
        }

        /// <summary>
        /// 除去数组中的空值和签名参数并以字母a到z的顺序排序
        /// </summary>
        /// <param name="dicArrayPre">过滤前的参数组</param>
        /// <returns>过滤后的参数组</returns>
        public static Dictionary<string, string> FilterPara(SortedDictionary<string, string> dicArrayPre)
        {
            Dictionary<string, string> dicArray = new Dictionary<string, string>();
            foreach (KeyValuePair<string, string> temp in dicArrayPre)
            {
                if (temp.Key.ToLower() != "sign" && temp.Key.ToLower() != "sign_type" && temp.Value != "" && temp.Value != null)//&& temp.Key.ToLower() != "sec_id"
                {
                    dicArray.Add(temp.Key.ToLower(), temp.Value);
                }
            }

            return dicArray;
        }

        /// <summary>
        /// 把数组所有元素，按照“参数=参数值”的模式用“&”字符拼接成字符串
        /// </summary>
        /// <param name="dicArray">需要拼接的数组</param>
        /// <returns>拼接完成以后的字符串</returns>
        public static string CreateLinkString(Dictionary<string, string> dicArray)
        {
            StringBuilder prestr = new StringBuilder();
            foreach (KeyValuePair<string, string> temp in dicArray)
            {
                prestr.Append(temp.Key + "=" + temp.Value + "&");
            }

            //去掉最後一個&字符
            int nLen = prestr.Length;
            prestr.Remove(nLen - 1, 1);
            BaseService.WriteLog("拼接完成以后的参数字符串prestr：" + prestr.ToString());

            return prestr.ToString();
        }

        /// <summary>
        /// 返回 XML字符串 节点value
        /// </summary>
        /// <param name="xmlDoc">XML格式 数据</param>
        /// <param name="xmlNode">节点</param>
        /// <returns>节点value</returns>
        public static string GetStrForXmlDoc(string xmlDoc, string xmlNode)
        {
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(xmlDoc);
            XmlNode xn = xml.SelectSingleNode(xmlNode);
            return xn.InnerText;
        }
    }
}
