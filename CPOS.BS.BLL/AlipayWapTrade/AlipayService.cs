using System.Collections.Generic;
using System.Text;
using System.Web;

namespace JIT.CPOS.BS.BLL.AlipayWapTrade
{
    /// <summary>
    /// 类名：Service
    /// 功能：支付宝各接口构造类
    /// 详细：构造支付宝各接口请求参数
    /// 
    /// 要传递的参数要么不允许为空，要么就不要出现在数组与隐藏控件或URL链接里。
    /// </summary>
    public class Service
    {
        /// <summary>
        /// 构造wap交易创建接口
        /// </summary>
        /// <param name="requrl">请求地址</param>
        /// <param name="key">MD5校验码</param>
        /// <param name="subject">商品名称</param>
        /// <param name="outTradeNo">外部交易号</param>
        /// <param name="totalFee">商品总价</param>
        /// <param name="sellerAccountName">卖家账户</param>
        /// <param name="notifyUrl">商户接收通知URL</param>
        /// <param name="outUser">商户用户唯一ID</param>
        /// <param name="merchantUrl">返回商户URL</param>
        /// <param name="callbackurl">支付成功跳转链接</param>
        /// <param name="service">服务</param>
        /// <param name="secid">签名方式</param>
        /// <param name="partner">合作伙伴ID</param>
        /// <param name="reqid">商户请求ID</param>
        /// <param name="format">请求参数格式</param>
        /// <param name="version">版本</param>
        /// <param name="gatway">网关地址</param>
        /// <param name="sellprivatekey">商户私钥</param>
        /// <param name="input_charset">编码格式</param>
        /// <returns>返回token</returns>
        public string alipay_wap_trade_create_direct(
            string requrl,
            string subject,
            string outTradeNo,
            string totalFee,
            string sellerAccountName,
            string notifyUrl,
            string outUser,
            string merchantUrl,
            string callbackurl,
            string service,
            string secid,
            string partner,
            string reqid,
            string format,
            string version,
            string gatway,
            string sellprivatekey,
            string input_charset)
        {
            //临时请求参数数组
            SortedDictionary<string, string> sParaTemp = new SortedDictionary<string, string>();

            //构造请求参数数组
            string req_Data = "<direct_trade_create_req><subject>" + subject + "</subject><out_trade_no>" +
                outTradeNo + "</out_trade_no><total_fee>" + totalFee + "</total_fee><seller_account_name>" + sellerAccountName +
                "</seller_account_name><notify_url>" + notifyUrl + "</notify_url><out_user>" + outUser +
                "</out_user><merchant_url>" + merchantUrl + "</merchant_url>" +
                "<call_back_url>" + callbackurl + "</call_back_url></direct_trade_create_req>";

            sParaTemp.Add("req_data", req_Data);
            sParaTemp.Add("service", service);
            sParaTemp.Add("sec_id", secid);
            sParaTemp.Add("partner", partner);
            sParaTemp.Add("req_id", reqid);
            sParaTemp.Add("format", format);
            sParaTemp.Add("v", version);

            //构造表单提交HTML数据
            string strResult = Submit.SendPostInfo(sParaTemp, gatway, sellprivatekey, input_charset, "");

            //解码支付宝返回信息
            strResult = HttpUtility.UrlDecode(strResult, Encoding.GetEncoding(input_charset)); //此处可做判断，如果带有error或者错误的信息，可视为创建交易失败，提示用户错误信息

            //调用Res解密方法并验签，最后返回token字符串
            return Res_dataDecrypt(strResult, sellprivatekey, input_charset);

        }

        /// <summary>
        /// 返回token字符串
        /// </summary>
        /// <param name="strResult">创建订单返回信息</param>
        /// <param name="sellprivatekey">商户私钥</param>
        /// <param name="input_charset">编码格式</param>
        /// <returns>token字符串</returns>
        public static string Res_dataDecrypt(string strResult, string sellprivatekey, string input_charset)
        {
            //分解返回数据 用&拆分赋值给result
            string[] result = strResult.Split('&');

            //提取res_data参数
            string res_data = string.Empty;

            for (int i = 0; i < result.Length; i++)
            {
                string data = result[i];
                if (data.IndexOf("res_data=") >= 0)
                {
                    res_data = data.Replace("res_data=", string.Empty);

                    //解密(用"商户私钥"对"res_data"进行解密)
                    res_data = Function.Decrypt(res_data, sellprivatekey, input_charset);

                    //res_data 赋值 给 result[0]
                    result[i] = "res_data=" + res_data;
                }
            }

            //创建待签名数组
            SortedDictionary<string, string> sd = new SortedDictionary<string, string>();
            int count = 0;
            string sparam = "";
            string key = "";
            string value = "";
            for (int i = 0; i < result.Length; i++)
            {
                sparam = result[i];
                count = sparam.IndexOf('=');
                key = sparam.Substring(0, count);
                value = sparam.Substring(count + 1, sparam.Length - (count + 1));
                sd.Add(key, value);
            }

            string sign = sd["sign"];

            //配置待签名数据
            Dictionary<string, string> dicData = Function.FilterPara(sd);
            string req_Data = Function.CreateLinkString(dicData);

            //验签，使用支付宝公钥
            bool vailSign = RSAFromPkcs8.verify(req_Data, sign, Config.Alipaypublick, input_charset);
            if (vailSign)//验签通过
            {
                //得到 request_token 的值
                string token = string.Empty;
                try
                {
                    token = Function.GetStrForXmlDoc(res_data, "direct_trade_create_res/request_token");
                }
                catch
                {
                    //提示 返回token值无效
                    return string.Empty;
                }
                return token;
            }
            else
            {
                //验签不通过，这里商户自己写逻辑
                return string.Empty;
            }
        }

        /// <summary>
        /// 授权并执行
        /// </summary>
        /// <param name="requrl">请求地址</param>
        /// <param name="key">MD5校验码</param>
        /// <param name="secid">签名方式</param>
        /// <param name="partner">合作伙伴ID</param>
        /// <param name="callbackurl">支付成功跳转链接</param>
        /// <param name="format">请求参数格式</param>
        /// <param name="version">版本</param>
        /// <param name="service">服务</param>
        /// <param name="token">创建交易时支付宝返回的token</param>
        /// <param name="gatway">网管地址</param>
        /// <param name="sellprivatekey">商户私钥</param>
        /// <param name="input_charset">编码格式</param>
        /// <param name="strResult">返回最终要跳转的URL字符串</param> 
        public string alipay_Wap_Auth_AuthAndExecute(
            string requrl,
            string secid,
            string partner,
            string callbackurl,
            string format,
            string version,
            string service,
            string token,
            string gatway,
            string sellprivatekey,
            string input_charset)
        {
            //临时请求参数数组
            SortedDictionary<string, string> sParaTemp = new SortedDictionary<string, string>();

            string req_Data = "<auth_and_execute_req><request_token>" + token + "</request_token></auth_and_execute_req>";

            sParaTemp.Add("req_data", req_Data);
            sParaTemp.Add("service", service);
            sParaTemp.Add("sec_id", secid);
            sParaTemp.Add("partner", partner);
            sParaTemp.Add("format", format);
            sParaTemp.Add("v", version);

            return Submit.SendPostRedirect(sParaTemp, gatway, sellprivatekey, input_charset);
        }
    }
}
