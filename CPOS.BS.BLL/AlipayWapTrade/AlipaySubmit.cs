using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace JIT.CPOS.BS.BLL.AlipayWapTrade
{
    /// <summary>
    /// 类名：Submit
    /// 功能：支付宝各接口请求提交类
    /// 详细：构造支付宝各接口表单HTML文本，获取远程HTTP数据
    /// </summary>
    public class Submit
    {
        /// <summary>
        /// 生成要请求给支付宝的参数数组
        /// </summary>
        /// <param name="sParaTemp">请求前的参数数组</param>
        /// <param name="sellprivatekey">商户私钥</param>
        /// <param name="input_charset">编码格式</param>
        /// <returns>构造请求字符串</returns>
        private static string BuildRequestParaToString(SortedDictionary<string, string> sParaTemp, string key,
            string input_charset)
        {
            //待签名请求参数数组
            Dictionary<string, string> sPara = new Dictionary<string, string>();
            sPara = BuildRequestPara(sParaTemp, key, input_charset);

            string strRequestData;
            strRequestData = Function.CreateLinkString(sPara);
            return strRequestData;
        }

        /// <summary>
        /// 生成要请求给支付宝的参数数组
        /// </summary>
        /// <param name="sParaTemp">请求前的参数数组</param>
        /// <param name="sellprivatekey">商户私钥</param>
        /// <param name="input_charset">编码格式</param>
        /// <returns>构造请求数组</returns>
        private static Dictionary<string, string> BuildRequestPara(SortedDictionary<string, string> sParaTemp,
            string sellprivatekey, string input_charset)
        {
            //签名结果
            string mysign = "";

            //获得签名结果
            mysign = Function.BuildMysign(sParaTemp, sellprivatekey, input_charset);
            Dictionary<string, string> sPara = Function.FilterPara(sParaTemp);
            sPara.Add("sign", mysign);

            return sPara;
        }

        /// <summary>
        /// 构造HTTP的POST请求
        /// </summary>
        /// <param name="sParaTemp">请求参数数组</param>
        /// <param name="gateway">网关地址</param>
        /// <param name="sellprivatekey">商户私钥</param>
        /// <param name="input_charset">编码格式</param>
        /// <returns>支付宝返回结果</returns>
        public static string SendPostInfo(SortedDictionary<string, string> sParaTemp, string gateway, string key, string input_charset, string signType)
        {
            //待请求参数数组字符串
            string strRequestData = "";

            strRequestData = BuildRequestParaToString(sParaTemp, key, input_charset);

            if (signType == "&sign_type=0001")
            {
                strRequestData += signType;
            }

            //把数组转换成流中所需字节数组类型
            Encoding code = Encoding.GetEncoding(input_charset);

            //测试
            byte[] bytesRequestData = code.GetBytes(strRequestData);

            //构造请求地址
            string strUrl = gateway;

            //设置HttpWebRequest基本信息
            HttpWebRequest myReq = (HttpWebRequest)HttpWebRequest.Create(strUrl);
            myReq.Method = "post";
            myReq.ContentType = "application/x-www-form-urlencoded";

            //填充POST数据
            myReq.ContentLength = bytesRequestData.Length;

            using (Stream reqStream = myReq.GetRequestStream())
            {
                reqStream.Write(bytesRequestData, 0, bytesRequestData.Length);
            }
            using (WebResponse wr = myReq.GetResponse())
            {
                //在这里对接收到的页面内容进行处理
                Stream myStream = wr.GetResponseStream();
                StreamReader sr = new StreamReader(myStream, code);
                string strResult = sr.ReadToEnd();
                return strResult;
            }
        }

        /// <summary>
        /// 构造最终请求字符串
        /// </summary>
        /// <param name="sParaTemp">请求参数数组</param>
        /// <param name="gateway">网关</param>
        /// <param name="sellprivatekey">商户私钥</param>
        /// <param name="input_charset">编码格式</param>
        /// <returns>返回最终请求字符串</returns>
        public static string SendPostRedirect(SortedDictionary<string, string> sParaTemp, string gateway, string sellprivatekey, string input_charset)
        {
            //待请求参数数组字符串
            string strRequestData = BuildRequestParaToString(sParaTemp, sellprivatekey, input_charset);
            //返回请求字符串
            return Config.Req_url + "?" + strRequestData;
        }

        /// <summary>
        /// 构造HTTP的GET请求，获取支付宝的返回XML处理结果
        /// </summary>
        /// <param name="sParaTemp">请求参数数组</param>
        /// <param name="gateway">网关地址</param>
        /// <param name="sellprivatekey">商户私钥</param>
        /// <param name="input_charset">编码格式</param>
        /// <returns>支付宝返回XML处理结果</returns>
        public static string SendGetInfo(SortedDictionary<string, string> sParaTemp, string gateway,
            string sellprivatekey, string input_charset)
        {
            //待请求参数数组字符串
            string strRequestData = BuildRequestParaToString(sParaTemp, sellprivatekey, input_charset);

            string strResult;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(gateway + "?" + strRequestData);

            request.Method = "GET";
            request.Accept = "application/json";

            Encoding code = Encoding.GetEncoding(input_charset);

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream(), code);
            StringBuilder output = new StringBuilder();
            strResult = reader.ReadToEnd();

            response.Close();

            return strResult;
        }
    }
}
