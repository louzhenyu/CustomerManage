using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using JIT.CPOS.Web.ApplicationInterface.Project.HuaAn.Send;

namespace JIT.CPOS.Web.ApplicationInterface.Project.HuaAn
{
    /// <summary>
    /// 华安Factory.
    /// </summary>
    public class HuaAnFactory
    {
        /// <summary>
        /// 构造请求表单内容。
        /// </summary>
        /// <param name="dt">当前日期</param>
        /// <param name="strXml">请求的内容</param>
        /// <param name="txcode">交易代码</param>
        /// <param name="seqNO">世联通讯流水号</param>
        /// <returns>
        /// 
        /// </returns>
        public HanAnRequestMessage FormRequestContent(DateTime dt, string strXml, int txcode, string seqNO)
        {
            string sysData = dt.ToString("yyyyMMdd");
            string sysTime = dt.ToString("HHmmss");

            StringBuilder sb = new StringBuilder();
            sb.Append("verNum" + HuaAnConfigurationAppSitting.Version);
            sb.Append("merchantID" + HuaAnConfigurationAppSitting.MerchantID);
            sb.Append("sysdate" + sysData);
            sb.Append("systime" + sysTime);
            sb.Append("txcode" + txcode);
            sb.Append("seqNO" + seqNO);

            //1. 加密content.
            //string key = "5dOf9FHI1Y5hW2TNvVFY4w==";
            string contentAES = Utility.AESEncrypt(strXml, HuaAnConfigurationAppSitting.AesKey);  //AES后的content
            sb.Append("content" + contentAES);

            //2. 拼接 校验串 ：
            //string key2 = "123456";
            string strMaccode = string.Concat(sb.ToString(), HuaAnConfigurationAppSitting.MacCodeKey);
            strMaccode = Utility.GenMD5(strMaccode);

            HanAnRequestMessage message = new HanAnRequestMessage()
            {
                verNum = HuaAnConfigurationAppSitting.Version,
                merchantID = HuaAnConfigurationAppSitting.MerchantID,
                sysdate = sysData,
                systime = sysTime,
                txcode = txcode.ToString(),
                seqNO = seqNO,
                maccode = strMaccode,
                content = contentAES
            };

            return message;
        }

        /// <summary>
        /// 构造form请求参数。
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static IDictionary<string, string> SetFormPara(HanAnRequestMessage message)
        {
            IDictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("verNum", message.verNum);
            parameters.Add("merchantID", message.merchantID);
            parameters.Add("sysdate", message.sysdate);
            parameters.Add("systime", message.systime);
            parameters.Add("txcode", message.txcode);
            parameters.Add("seqNO", message.seqNO);
            parameters.Add("maccode", HttpUtility.UrlEncode(message.maccode));
            parameters.Add("content", HttpUtility.UrlEncode(message.content));

            return parameters;
        }

        /// <summary>
        /// 生成世联通讯流水号。
        /// </summary>
        /// <returns></returns>
        public static string GenerateSeqNO()
        {
            string date = DateTime.Now.ToString("yyyyMMddHHmmssfff");

            int[] array = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            Random rand = new Random();
            for (int i = 10; i > 1; i--)
            {
                int index = rand.Next(i);
                int tmp = array[index];
                array[index] = array[i - 1];
                array[i - 1] = tmp;
            }
            int result = 0;
            for (int i = 0; i < 6; i++)
                result = result * 10 + array[i];

            return string.Concat(date, result);
        }

        #region


        #endregion

    }
}